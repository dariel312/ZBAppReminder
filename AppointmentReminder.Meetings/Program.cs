using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentReminder.Core;
using AppointmentReminder.Core.Models.Zoom;
using Newtonsoft.Json;

namespace AppointmentReminder.Meetings
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var config = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(baseDir + "settings.json"));
            
            //services
            var cap = new AppointmentDBContext(config.DataBasePath);
            var zoom = new ZoomService(config.ZoomAPIKey, config.ZoomAPISecret);

            //main functionality
            var sync = new AppointmentSync(config, cap, zoom);
        }
    }
}
