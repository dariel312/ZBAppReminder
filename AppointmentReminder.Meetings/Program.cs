using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentReminder.Core;
using Newtonsoft.Json;

namespace AppointmentReminder.Meetings
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string configPath = AppDomain.CurrentDomain.BaseDirectory + "settings.json";

            if (args.Length > 0)
            {
                configPath = args[0];
            }


            Console.WriteLine("Using settings path: " + configPath);
            if (!File.Exists(configPath))
            {
                Console.WriteLine("Error: Config file cannot be found in the specified path.");
                Console.ReadLine();
                return;
            }
            
            //setup
            var config = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(configPath));
            
            //check/create logs folder
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + Logs.LogFolderName))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + Logs.LogFolderName);

            //services
#if DEBUG
            var logs = Console.Out; //TODO: move to file
#else
            var logs = File.CreateText(baseDir + Logs.LogFolderName + '/' + DateTime.Now.ToString(Logs.LogNameFormat) + Logs.LogFileExtension);
#endif
            var cap = new AppointmentDBContext(config.CAPDBPath);
            var zoom = new ZoomService(config.ZoomAPIKey, config.ZoomAPISecret);
            var cache = new MeetingCache(config.CacheFilePath);
            PhoneService.Init(config.TwilioAccountSID, config.TwilioAuthToken);

            //main functionality
            var sync = new MeetingSync(cap, zoom, cache, logs, config);


#if DEBUG
            //test date
            var syncDate = new DateTime(2021, 5, 31);

            //sync appointments 
            await sync.SyncAppointments(syncDate, DateTime.MaxValue);
#else
            var syncDate = DateTime.Now.Subtract(DateTime.Now.TimeOfDay);

            //sync appointments 
            await sync.SyncAppointments(syncDate, DateTime.MaxValue);
#endif
            //save cache
            cache.RemoveBefore(syncDate.AddDays(-7));
            cache.Dispose();
        }
    }
}
