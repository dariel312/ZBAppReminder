using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace AppointmentReminder.Core
{
    public class AppReminder
    {
        public TextWriter Output { get; set; }


        private AppSettings config;
        private DataProvider db;

        public AppReminder(AppSettings Config)
        {
            this.config = Config;
            this.db = new DataProvider(config.DataBasePath);
            TwilioClient.Init(config.AccountSID, config.AuthToken);
        }
        public void CheckSendTextReminders()
        {
            var tomorrow = DateTime.Today.AddDays(1);
            var appts = db.GetAppointments(tomorrow, tomorrow).ToList();
            var customers = db.GetCustomers().ToList();
            var employees = db.GetEmployees().ToList();

            //Merge refrences
            foreach (var app in appts)
                app.Customer = customers.First(m => app.CustomerID == m.CustomerID);

            foreach (var app in appts)
                app.Employee = employees.First(m => app.EmployeeID == m.EmployeeID);

            Output.WriteLine($"Found { appts.Count } appointments tomorrow.");
            Output.WriteLine("ID\tFirstName\tLastName\tTelephone\tSid");

            //Send texts
            foreach (var item in appts)
            {
                string sid = sendReminder(item);
                Output.WriteLine(item.CustomerID + "\t" + item.Customer.FirstName + "\t" + item.Customer.LastName + "\t" + item.Customer.Telephone + "\t" + sid);
            }
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
        private string sendReminder(Transaction app)
        {
            string text = config.ReminderMessage.Replace("\\n", "\n");
            string message = string.Format(text, app.Employee.FirstName, app.StartTime.ToString("t"));

            return PhoneService.SendMessage(app.Customer.Telephone, config.TwilioPhone, message);
        }

    }
}
