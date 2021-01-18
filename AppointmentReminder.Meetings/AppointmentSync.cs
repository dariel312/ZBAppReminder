using AppointmentReminder.Core;
using AppointmentReminder.Core.Models.Zoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentReminder.Meetings
{
    public class AppointmentSync
    {
        private AppointmentDBContext db;
        private ZoomService zoom;
        AppSettings config;

        public AppointmentSync(AppSettings Config, AppointmentDBContext CAP, ZoomService Zoom)
        {
            this.config = Config;
            this.db = CAP;
            this.zoom = Zoom;
        }



        public async Task main ()
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



            //check create logs folder
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + Logs.LogFolderName))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + Logs.LogFolderName);

            var tomorrow = DateTime.Now.AddDays(1);
#if DEBUG
            tomorrow = new DateTime(2020, 11, 30);
         
            Console.ReadLine();
#endif
            Console.ReadLine();
        }
    }
}
