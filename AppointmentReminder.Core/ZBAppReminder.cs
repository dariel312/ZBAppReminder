using AppointmentReminder.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace AppointmentReminder.Core
{
    public class ZBAppReminder
    {
        public TextWriter Output { get; set; }


        private AppSettings config;
        private AppointmentDBContext db;

        public ZBAppReminder(AppSettings Config)
        {
            this.config = Config;
            this.db = new AppointmentDBContext(config.CAPDBPath);
            TwilioClient.Init(config.AccountSID, config.AuthToken);
        }

        /// <summary>
        /// Checks DB for Appointsments between Start and End Date then sends reminders.
        /// </summary>
        /// <param name="StartDay"></param>
        /// <param name="EndDay"></param>
        public void CheckSendTextReminders(DateTime StartDay, DateTime EndDay)
        {
            var appts = db.GetAppointments(StartDay, EndDay).ToList();
            var employees = db.GetEmployees().ToList();

            //Set employee for each transation
            foreach (var app in appts)
                app.Employee = employees.First(m => app.EmployeeID == m.EmployeeID);

            appts = checkValidApps(appts);

            Output.WriteLine($"Found { appts.Count } appointments tomorrow.");
            Output.WriteLine("ID\tFirstName\tLastName\tTelephone\tSid");

            //Send texts
            foreach (var item in appts)
            {
                try
                {
                    string sid = sendReminder(item);
                    Output.WriteLine(item.CustomerID + "\t" + item.Customer.FirstName + "\t" + item.Customer.LastName + "\t" + item.Customer.Telephone + "\t" + sid);
                }
                catch(TwilioException ex)
                {
                    Output.WriteLine($"Error sending text to {item.Customer.FirstName} {item.Customer.LastName} - {item.Customer.Telephone}. Message: {ex.Message}");
                }
            }
        }

        private List<Transaction> checkValidApps(List<Transaction> apps)
        {
            List<Transaction> removals = new List<Transaction>();

            foreach (Transaction item in apps)
            {
                string number = item.Customer.Telephone.Replace("-", "").Replace(" ", "").Replace("+", "");

                //Remove if empty or has letters
                if (number == string.Empty || !long.TryParse(number, out var num))
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
