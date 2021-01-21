using AppointmentReminder.Core;
using AppointmentReminder.Core.Models.Zoom;
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


        public MeetingSync(AppointmentDBContext CAP, ZoomService Zoom, MeetingCache Cache, TextWriter Logger, IEnumerable<EmployeeID> Employees)
        {
            this.log = Logger;
            this.db = CAP;
            this.zoom = Zoom;
            this.cache = Cache;
            this.employees = Employees;
        }



        public async Task SyncAppointments(DateTime StartDate, DateTime EndDate)
        {

            log.WriteLine($"Getting appointments from CAP.");
            var apts = db.GetAppointments(StartDate, EndDate);

            log.WriteLine($"Getting meetings from Zoom.");
            var zoomMeetings = await getScheduled();
            log.WriteLine($"Loaded {zoomMeetings.Count()} meetings from Zoom.");

            foreach (var appt in apts)
            {
                //check if cached
                var cached = this.cache.GetMeeting(appt.TransactionID);

                if (cached != null)
                {
                    //check update times
                }
                else
                {
                    //add meeting as new
                    var emp = this.getEmployee(appt.EmployeeID);
                    var meeting = await this.createMeeting(emp.ZoomId, $"{appt.Customer.FirstName} {appt.Customer.LastName} | Z&B Accounting", appt.StartTime, appt.EndTime);

                    //cache new meeting
                    cache.AddMeeting(new CachedMeeting() { 
                        ZoomId = meeting.id,
                        StartTime = meeting.start_time,
                        EndTime = meeting.start_time.AddMinutes(meeting.duration),
                        JoinUrl = meeting.join_url,
                        TranId = appt.TransactionID
                    });
                }

            }
            //remove expired meetings
        }

        //get meetings for all employees
        private async Task<List<MeetingListItem>> getScheduled()
        {
            var results = new List<MeetingListItem>();
            foreach (var emp in this.employees)
            {
                var resp = await this.zoom.ListMeetings(emp.ZoomId);
                results.AddRange(resp.meetings);
            }

            return results;
        }

        private EmployeeID getEmployee(int EmpId)
        {
            //add meeting as new
            var emp = employees.FirstOrDefault(m => m.EmpId == EmpId);

            if (emp == null)
                this.log.WriteLine($"Error - Meeting found without a configured employee id ({EmpId}). Skipping.");

            return emp;
        }
        
        async Task<MeetingModel> createMeeting(string HostId, string Title, DateTime StartTime, DateTime EndTime)
        {
            var dur = (int)(EndTime - StartTime).TotalMinutes;
            var newMeeting = new CreateMeetingRequest()
            {
                topic = Title,
                start_time = StartTime,
                type = MeetingType.Scheduled,
                duration = dur,
                timezone = MeetingTimeZone.Eastern,
                password = null,
                agenda = "",
                settings = new MeetingSettings()
                {

                }
            };

            var meeting = await zoom.CreateMeeting("5_bsxLnbSOO3Efj6y-n_yQ", newMeeting);

            return meeting;
        }
    }
}
