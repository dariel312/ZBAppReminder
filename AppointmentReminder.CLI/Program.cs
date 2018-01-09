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
            var reminder = new AppReminder(config);

            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + Logs.LogFolderName))
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + Logs.LogFolderName);

#if DEBUG
                reminder.Output = Console.Out; //For testing output to console
#else
                reminder.Output = File.CreateText(baseDir + Logs.LogFolderName + '/' + DateTime.Now.ToString(Logs.LogNameFormat) + Logs.LogFileExtension);
#endif
                reminder.CheckSendTextReminders();
            }
            catch (Exception ex)
            {
                File.WriteAllText(baseDir + Logs.LogFolderName + @"/Error " + DateTime.Now.ToString(Logs.LogNameFormat) + Logs.LogFileExtension, ex.ToString());
                PhoneService.SendMessage(config.ErrorLogPhone, config.TwilioPhone, ex.ToString());
            }

#if DEBUG
            Console.ReadLine();
#else
            reminder.Output.Close();
#endif
        }
    }
}
