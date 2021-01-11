﻿using System;
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
        public string DataBasePath { get; set; }
        public string ReminderMessage { get; set; }
        public string ErrorLogPhone { get; set; }
    }
}