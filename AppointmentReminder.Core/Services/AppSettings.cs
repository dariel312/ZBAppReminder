using AppointmentReminder.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentReminder.Core
{
    public class EmployeeID
    {
        public int EmpId { get; set; }
        public string ZoomId { get; set; }
    }

    public class AppSettings
    {
        public string TwilioAccountSID { get; set; }
        public string TwilioAuthToken { get; set; }
        public string TwilioFromPhone { get; set; }
        public string CAPDBPath { get; set; }
        public string ReminderMsg { get; set; }
        public string ErrorLogPhone { get; set; }

        public string ZoomAPIKey { get; set; }
        public string ZoomAPISecret { get; set; }
        public string CacheFilePath { get; set; }
        public EmployeeID[] Employees { get; set; }
        public string MeetingCreatedMsg { get; set; }
        public string MeetingUpdateMsg { get; set; }
        public string MeetingDeletedMsg { get; set; }

     
    }
}
