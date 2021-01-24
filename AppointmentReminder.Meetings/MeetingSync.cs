using AppointmentReminder.Core;
using AppointmentReminder.Core.Models;
using AppointmentReminder.Core.Models.Zoom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentReminder.Meetings
{
    public class MeetingSync
    {
        private AppointmentDBContext db;
        private ZoomService zoom;
        private TextWriter log;
        private MeetingCache cache;
        private IEnumerable<EmployeeID> employees;
        private readonly string createdMsg;
        private readonly string updatedMsg;
        private readonly string deletedMsg;
        private readonly string fromPhone;

        public MeetingSync(AppointmentDBContext CAP, ZoomService Zoom, MeetingCache Cache, TextWriter Logger, AppSettings config)
        {
            this.log = Logger;
            this.db = CAP;
            this.zoom = Zoom;
            this.cache = Cache;
            this.employees = config.Employees;
            this.createdMsg = config.MeetingCreatedMsg;
            this.updatedMsg = config.MeetingUpdateMsg;
            this.deletedMsg = config.MeetingDeletedMsg;
            this.fromPhone = config.TwilioFromPhone;
        }



        public async Task SyncAppointments(DateTime SyncStartDate, DateTime SyncEndDate)
        {
            log.WriteLine($"Syncing appointments at {DateTimeOffset.Now.ToString()}");
            log.WriteLine($"Getting appointments from CAP.");
            var apts = db.GetAppointments(SyncStartDate, SyncEndDate);
            var capEmps = db.GetEmployees();

            //iterate on CAP Appointments
            foreach (var appt in apts)
            {
                //check if cached
                var cached = this.cache.GetMeeting(appt.TransactionID);
                var capEmp = capEmps.FirstOrDefault(m => m.EmployeeID == appt.EmployeeID);
                var zoomEmp = this.getEmployee(appt.EmployeeID);

                if (cached != null)
                {
                    //check update times
                    //NOTE: We're comparing DateTimeOffset to Datetime. We're converting to datetime to remove time zones, but this may cause issues when time changes to DST.
                    var isSameTime = cached.StartTime.DateTime == appt.StartTime && cached.EndTime.DateTime == appt.EndTime;

                    if (!isSameTime)
                    {
                        log.WriteLine($"Found an updated meeting Zoom Meeting:");
                        log.WriteLine(JsonConvert.SerializeObject(cached));
                        log.WriteLine($"Cap Meeting:");
                        log.WriteLine(JsonConvert.SerializeObject(appt));

                        //update meeting
                        await this.updateMeeting(appt, cached);

                        //update cache
                        cached.StartTime = new DateTimeOffset(appt.StartTime);
                        cached.EndTime = new DateTimeOffset(appt.EndTime);
                        this.cache.CacheMeeting(cached);

                        //send txt
                        this.sendUpdated(appt, cached, capEmp);
                    }
                }
                else
                {
                    log.WriteLine($"Found a new meeting Appointment");
                    log.WriteLine($"Cap Meeting:");
                    log.WriteLine(JsonConvert.SerializeObject(appt));

                    //add meeting as new
                    var meeting = await this.createMeeting(zoomEmp.ZoomId, $"{appt.Customer.FirstName} {appt.Customer.LastName} | Z&B Accounting", appt.StartTime, appt.EndTime);

                    //send txt
                    this.sendCreated(appt, meeting, capEmp);

                    //cache new meeting
                    cache.CacheMeeting(new CachedMeeting()
                    {
                        ZoomId = meeting.id,
                        StartTime = meeting.start_time.ToLocalTime(),
                        EndTime = meeting.start_time.AddMinutes(meeting.duration).ToLocalTime(),
                        JoinUrl = meeting.join_url,
                        Password = meeting.password,
                        CustomerPhone = appt.Customer.Telephone,
                        TranId = appt.TransactionID
                    });
                }

            }

            //deleted meetings
            log.WriteLine($"Looking for deleted appointments...");
            var aptIds = apts.Select(m => m.TransactionID);
            var deleted = cache
                .Except(aptIds)
                .Where(m => m.StartTime >= SyncStartDate)
                .ToList();

            log.WriteLine($"Found {deleted.Count()} appointments in cache.");

            foreach(var apt in deleted)
            {

                log.WriteLine($"Deleting cached meeting.");
                log.WriteLine($"Cached meeting:");
                log.WriteLine(JsonConvert.SerializeObject(apt));

                //delete in zoom
                await this.deleteMeeting(apt);

                //Send txt
                this.sendDeleted(apt);

                //remove from cache
                this.cache.RemoveMeeting(apt);
            }

            log.WriteLine("Meeting Sync complete!");
        }

        //helpers
        private EmployeeID getEmployee(int EmpId)
        {
            //add meeting as new
            var emp = employees.FirstOrDefault(m => m.EmpId == EmpId);

            if (emp == null)
                this.log.WriteLine($"Error - Meeting found without a configured employee id ({EmpId}). Skipping.");

            return emp;
        }

        private async Task<MeetingModel> createMeeting(string HostId, string Title, DateTime StartTime, DateTime EndTime)
        {
            var dur = (int)(EndTime - StartTime).TotalMinutes;
            MeetingModel meeting = null;

            var newMeeting = new CreateMeetingRequest()
            {
                topic = Title,
                start_time = StartTime,
                type = MeetingType.Scheduled,
                duration = dur,
                timezone = MeetingTimeZone.Eastern,
                password = null,
                agenda = "",
                settings = new MeetingSettings() { }
            };

            try
            {
                log.WriteLine("Creating zoom meeting...");
                meeting = await zoom.CreateMeeting("5_bsxLnbSOO3Efj6y-n_yQ", newMeeting);
                log.WriteLine("Zoom meeting created");
                log.WriteLine(JsonConvert.SerializeObject(meeting));
            }
            catch (Exception ex)
            {
                log.WriteLine("Error creating zoom meeting");
                log.WriteLine(ex.Message);
                log.WriteLine(ex.StackTrace);
            }

            return meeting;
        }

        private async Task updateMeeting(Transaction appt, CachedMeeting cached)
        {
            try
            {

                log.WriteLine($"Updating meeting in zoom...");
                await this.zoom.UpdateMeeting(cached.ZoomId, new UpdateMeetingRequest()
                {
                    start_time = appt.StartTime,
                    duration = (int)(appt.EndTime - appt.StartTime).TotalMinutes
                });
                log.WriteLine($"Zoom meeting updated.");
            }
            catch (Exception ex)
            {
                log.WriteLine("Error updating zoom meeting");
                log.WriteLine(ex.Message);
                log.WriteLine(ex.StackTrace);
            }
        }

        private async Task deleteMeeting(CachedMeeting cached)
        {
            try
            {
                log.WriteLine($"Deleting a meeting in zoom...");
                await this.zoom.DeleteMeeting(cached.ZoomId);
                log.WriteLine($"Zoom meeting deleted.");
            }
            catch (Exception ex)
            {
                log.WriteLine("Error deleting zoom meeting");
                log.WriteLine(ex.Message);
                log.WriteLine(ex.StackTrace);
            }
        }

        private void sendCreated(Transaction appt, MeetingModel meeting, Employee emp)
        {
            var msg = string.Format(this.createdMsg, emp.FirstName, meeting.start_time.ToLocalTime().ToString("g"), meeting.join_url, meeting.password);

            try
            {
                this.log.WriteLine($"Sending creation text message to {appt.Customer.Telephone}");
                PhoneService.SendMessage(appt.Customer.Telephone, this.fromPhone, msg);
            }
            catch (Exception ex)
            {
                this.log.WriteLine($"Error sending text to {appt.Customer.Telephone} - {appt.Customer.FirstName } {appt.Customer.LastName} ({appt.CustomerID})");
                this.log.WriteLine(ex.Message);
                this.log.WriteLine(ex.StackTrace);
            }

        }

        private void sendDeleted(CachedMeeting meeting)
        {
            var msg = string.Format(this.deletedMsg, meeting.StartTime.ToLocalTime().ToString("g"));

            try
            {
                this.log.WriteLine($"Sending deleted text message to {meeting.CustomerPhone}");
                PhoneService.SendMessage(meeting.CustomerPhone, this.fromPhone, msg);
            }
            catch (Exception ex)
            {
                this.log.WriteLine($"Error sending text to {meeting.CustomerPhone}");
                this.log.WriteLine(ex.Message);
                this.log.WriteLine(ex.StackTrace);
            }

        }

        private void sendUpdated(Transaction appt, CachedMeeting meeting, Employee emp)
        {
            var msg = string.Format(this.updatedMsg, emp.FirstName, meeting.StartTime.ToLocalTime().ToString("g"), meeting.JoinUrl, meeting.Password);

            try
            {
                this.log.WriteLine($"Sending updated text message to {appt.Customer.Telephone}");
                PhoneService.SendMessage(appt.Customer.Telephone, this.fromPhone, msg);
            }
            catch (Exception ex)
            {
                this.log.WriteLine($"Error sending text to {appt.Customer.Telephone} - {appt.Customer.FirstName } {appt.Customer.LastName} ({appt.CustomerID})");
                this.log.WriteLine(ex.Message);
                this.log.WriteLine(ex.StackTrace);
            }

        }
    }
}
