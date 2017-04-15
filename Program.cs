using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AppointmentReminder
{
    static class Program
    {

        static void Main()
        {
#if DEBUG
            AppReminder myService = new AppReminder();
            myService.OnDebug();
            Thread.Sleep(Timeout.Infinite);
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]{new AppReminder()};
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
