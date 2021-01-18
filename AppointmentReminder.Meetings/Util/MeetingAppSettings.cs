using AppointmentReminder.Meetings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentReminder.Meetings
{
    internal class MeetingAppSettings
    {
        public string ZoomAPIKey { get; set; }
        public string ZoomAPISecret { get; set; }
        public string CAPDBPath { get; set; }
        public string CacheFilePath { get; set; }
        public EmployeeID[] Employees { get; set; }
    }
}
