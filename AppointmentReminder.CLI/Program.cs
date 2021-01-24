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
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string configPath = AppDomain.CurrentDomain.BaseDirectory + "settings.json";
            if (args.Length > 0)
            
                configPath = args[0];


            Console.WriteLine("Using settings path: " + configPath);
            if (!File.Exists(configPath))
            {
                Console.WriteLine("Error: Config file cannot be found in the specified path.");
                Console.ReadLine();
                return;
            }

            var config = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(configPath));
            var reminder = new ZBAppReminder(config);

            PhoneService.Init(config.TwilioAccountSID, config.TwilioAuthToken);

            //check create logs folder
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + Logs.LogFolderName))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + Logs.LogFolderName);

            var tomorrow = DateTime.Now.AddDays(1);
#if DEBUG
            tomorrow = new DateTime(2021, 5, 31);
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
