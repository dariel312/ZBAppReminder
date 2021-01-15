using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentReminder.Core;

namespace AppointmentReminder.Meetings
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var zoom = new ZoomService("", "");
            var jwt = zoom.CreateJWT();
            var resp =  await zoom.ListUsers();

            Console.WriteLine(jwt);
            Console.WriteLine(resp.data.Count());
            Console.ReadLine();
        }
    }
}
