using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentReminder.Core
{
    public class AppSettings
    {
        public string AccountSID { get; set; }
        public string AuthToken { get; set; }
        public string TwilioPhone { get; set; }
        public string CAPDBPath { get; set; }
        public string ReminderMessage { get; set; }
        public string ErrorLogPhone { get; set; }
        public string ZoomAPIKey { get; set; }
        public string ZoomAPISecret { get; set; }
    }
}
