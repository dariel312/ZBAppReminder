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
                //if cached - check update times
                //if not cached -- create new then cache
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

        async Task createMeeting()
        {
            var newMeeting = new CreateMeetingRequest()
            {
                topic = "Test Meeting",
                start_time = DateTime.Now.AddHours(2),
                type = MeetingType.Scheduled,
                duration = 60,
                timezone = MeetingTimeZone.Eastern,
                password = "123123",
                agenda = "This is a test meeting ;)",
                settings = new MeetingSettings()
                {

                }
            };

            var meeting = await zoom.CreateMeeting("5_bsxLnbSOO3Efj6y-n_yQ", newMeeting);



        }
    }
}
