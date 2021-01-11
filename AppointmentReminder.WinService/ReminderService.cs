using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.ServiceProcess;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AppointmentReminder.Core;

namespace AppointmentReminder.WinService
{
    public partial class ReminderService : ServiceBase
    {
        private bool paused = false;
        private Thread worker;
        private DateTime lastCheck;
        private TextWriter Writer;
        private ZBAppReminder reminder;
        private ServiceAppSettings config;

        public ReminderService()
        {
            InitializeComponent();
        }
        public void OnDebug()
        {
            OnStart(null);
        }
        protected override void OnStart(string[] args)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Logs"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Logs");

            config = JsonConvert.DeserializeObject<ServiceAppSettings>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "settings.json"));
            reminder = new ZBAppReminder(config);

#if DEBUG
            doWork();
#else
            worker = new Thread(doWork);
            worker.Start();
#endif
        }

        protected override void OnStop()
        {
            worker.Abort();
        }

        private void doWork()
        {
            while (!paused)
            {

                // Check to see if it's time to set updates and that we haven't sent them for today
                if (config.CheckTextTimeOfDay == DateTime.Now.Hour)
                {
                    if (lastCheck.Date != DateTime.Now.Date)
                    {
                        //try
                        //{
                            openWriter();
                            reminder.CheckSendTextReminders(DateTime.Now, DateTime.Now);
                            closeWriter();
                        //}
                        //catch(Exception ex)
                        //{
                        //    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"Logs\Error " + DateTime.Now.ToString("yyyy-MM-dd h.mm tt") + ".txt", ex.ToString());
                        //   // sendText(Properties.Settings.Default.ErrorLogPhone, ex.ToString());
                        //}
                    }
                }
                Thread.Sleep(1000 * 60 * 10); //10 minutes
            }

        }

        private void openWriter()
        {
            Writer = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + Logs.LogFolderName + '/' + DateTime.Now.ToString(Logs.LogNameFormat) + Logs.LogFileExtension);
            reminder.Output = Writer;
        }
        private void closeWriter()
        {
            Writer.Close();
        }

       


    }
}
