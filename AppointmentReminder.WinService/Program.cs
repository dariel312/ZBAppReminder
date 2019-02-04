using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AppointmentReminder.WinService
{
    static class Program
    {
        /*
         * THIS PROJECT HAS BEEN REPLACED IN FAVOR OF TEH CLI
         */
        static void Main()
        {
#if DEBUG
            ReminderService myService = new ReminderService();
            myService.OnDebug();
            Thread.Sleep(Timeout.Infinite);
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]{new ReminderService()};
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
