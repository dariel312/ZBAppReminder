using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentReminder.Core;
using System.IO;

namespace AppointmentReminder.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Logs"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Logs");

            var config = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "settings.json"));
            var reminder = new AppReminder(config);
            AppSettings settings = new AppSettings()
            {
                AccountSID = "ACa77a27c580f1705b0a8e0b08d5295da0",
                AuthToken = "facddeab476aaf4c6fa3c6cc5ca69837",
                TwilioPhone = "3059164696",
                DataBasePath = "C:/Users/Dariel/Source/Repos/AppointmentReminder/AppointmentReminder/ApptDB.mdb",
                ReminderMessage = "*REMINDER FROM Z&B Accounting*\nYour appointment with {0} at Z&B Accounting is tomorrow at {1}. If you need to cancel your appointment do not respond to this message. Please call the office at (305) 557-2389."

            };
            AppReminder p = new AppReminder(settings);
            p.Output = Console.Out;
            p.CheckSendReminders();
        }
    }
}
