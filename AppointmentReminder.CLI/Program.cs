using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentReminder.Core;
using System.IO;
using Newtonsoft.Json;

namespace AppointmentReminder.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var config = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(baseDir + "settings.json"));
            var reminder = new ZBAppReminder(config);

            //check create logs folder
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + Logs.LogFolderName))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + Logs.LogFolderName);

            var tomorrow = DateTime.Now.AddDays(1);
#if DEBUG
            tomorrow = new DateTime(2020, 11, 30);
            reminder.Output = Console.Out; //For testing output to console
            reminder.CheckSendTextReminders(tomorrow, tomorrow);
            Console.ReadLine();
#else
            try
            {
                reminder.Output = File.CreateText(baseDir + Logs.LogFolderName + '/' + DateTime.Now.ToString(Logs.LogNameFormat) + Logs.LogFileExtension);
                reminder.CheckSendTextReminders(tomorrow, tomorrow);
            }
            catch (Exception ex)
            {
                File.WriteAllText(baseDir + Logs.LogFolderName + @"/Error " + DateTime.Now.ToString(Logs.LogNameFormat) + Logs.LogFileExtension, ex.ToString());
                PhoneService.SendMessage(config.ErrorLogPhone, config.TwilioPhone, ex.ToString());
            }
             reminder.Output.Close();
#endif
        }
    }
}
