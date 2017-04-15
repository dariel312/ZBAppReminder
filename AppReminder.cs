using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.ServiceProcess;
using System.Linq;
using System.Text;
using System.Threading;
using Twilio;

namespace AppointmentReminder
{
    public partial class AppReminder : ServiceBase
    {
        private bool paused = false;
        private Thread worker;
        private DateTime lastCheck;
        private StreamWriter Writer;

        //Entities
        private List<Transaction> apps;
        private List<Customer> customers;
        private List<Employee> employees;

        public AppReminder()
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
                if (Properties.Settings.Default.TimeOfDay == DateTime.Now.Hour)
                {
                    if (lastCheck.Date != DateTime.Now.Date)
                    {
                        try
                        {
                            openWriter();
                            update();
                            closeWriter();
                        }
                        catch(Exception ex)
                        {
                            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"Logs\Error " + DateTime.Now.ToString("yyyy-MM-dd h.mm tt") + ".txt", ex.ToString());
                            sendText(Properties.Settings.Default.ErrorLogPhone, ex.ToString());
                        }
                    }
                }
                Thread.Sleep(1000 * 60 * 10); //10 minutes
            }

        }
        private void update()
        {
            apps = DataProvider.GetAppointments().ToList();
            customers = DataProvider.GetCustomers().ToList();
            employees = DataProvider.GetEmployees().ToList();

            transactionMergeCustomer();
            transactionMergeEmployee();
            apps = checkValidApps(apps);

            Writer.WriteLine("Found " + apps.Count + " appointments tomorrow.");
            Writer.WriteLine("ID\tFirstName\tLastName\tTelephone\tSid");

            //Send texts
            foreach (var item in apps)
            {
                string sid = sendReminder(item);
                Writer.WriteLine(item.CustomerID + "\t" + item.Customer.FirstName + "\t" + item.Customer.LastName + "\t" + item.Customer.Telephone + "\t" + sid);
            }

            lastCheck = DateTime.Now;
        }
        private void transactionMergeCustomer()
        {
            foreach (var app in apps)
                app.Customer = customers.First(m => app.CustomerID == m.CustomerID);
        }
        private void transactionMergeEmployee()
        {
            foreach (var app in apps)
                app.Employee = employees.First(m => app.EmployeeID == m.EmployeeID);
        }
        private List<Transaction> checkValidApps(List<Transaction> apps)
        {
            List<Transaction> removals = new List<Transaction>();

            foreach (Transaction item in apps)
            {
                string number = item.Customer.Telephone.Replace("-", "").Replace(" ", "").Replace("+", "");
                long num = 0;
                //Remove if empty or has letters
                if (number == "" || !long.TryParse(number, out num))
                    removals.Add(item);
            }

            //Remove invalid Appointments
            foreach (Transaction item in removals)
                apps.Remove(item);

            return apps;
        }
        private static string sendReminder(Transaction app)
        {
            var twilio = new TwilioRestClient(Properties.Settings.Default.AccountSID, Properties.Settings.Default.AuthToken);
            string text = Properties.Settings.Default.ReminderMessage.Replace("\\n", "\n");
            string message = string.Format(text, app.Employee.FirstName, app.StartTime.ToString("t"));

            Message twilioMessage = twilio.SendMessage(Properties.Settings.Default.TwilioPhone, app.Customer.Telephone, message, new string[0], "");

            if (twilioMessage.Status == "Failed")
                return twilioMessage.ErrorMessage;
            else
                return twilioMessage.Sid;
        }
        private static string sendText(string to, string message)
        {
            var twilio = new TwilioRestClient(Properties.Settings.Default.AccountSID, Properties.Settings.Default.AuthToken);
            Message twilioMessage = twilio.SendMessage(Properties.Settings.Default.TwilioPhone, to, message, new string[0], "");

            if (twilioMessage.Status == "Failed")
                return twilioMessage.ErrorMessage;
            else
                return twilioMessage.Sid;
        }
        private void openWriter()
        {
            Writer = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + @"Logs\" + DateTime.Now.ToString("yyyy-MM-dd h.mm tt") + ".txt");
            Writer.AutoFlush = true;
        }
        private void closeWriter()
        {
            Writer.Close();
        }

       


    }
}
