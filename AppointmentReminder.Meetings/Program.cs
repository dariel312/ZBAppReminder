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
            //setup
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var config = JsonConvert.DeserializeObject<MeetingAppSettings>(File.ReadAllText(baseDir + "settings.json"));
            
            //check/create logs folder
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + Logs.LogFolderName))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + Logs.LogFolderName);

            //services
            var logs = Console.Out; //TODO: move to file
            var cap = new AppointmentDBContext(config.CAPDBPath);
            var zoom = new ZoomService(config.ZoomAPIKey, config.ZoomAPISecret);
            var cache = new MeetingCache(config.CacheFilePath);

       
            //main functionality
            var sync = new MeetingSync(cap, zoom, cache, logs, config.Employees);
            var tomorrow = DateTime.Now.AddDays(1);

#if DEBUG
            tomorrow = new DateTime(2020, 11, 30);
#endif
            //sync appointments for specified dates
            await sync.SyncAppointments(tomorrow, tomorrow);

   
            //save cache
            cache.Flush();
        }
    }
}
