using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.ServiceProcess;
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
        private static List<Appointment> apps = new List<Appointment>();

        public AppReminder()
        {
            InitializeComponent();
        }

        private void DoWork()
        {
            while (!paused)
            {
                // Check to see if it's time to set updates and that we haven't sent them for today
                //if (Settings.Instance.TimeOfDay.Hour == DateTime.Now.Hour)
                if(true)
            {
                    if (lastCheck.Date != DateTime.Now.Date)
                    {
                        StartWriter();
                        apps = new List<Appointment>();

                        LoadAppointments();
                        CheckValidAppointments();
                        CheckEmployee();

                        Writer.WriteLine("Found " + apps.Count + " appointments tomorrow.");
                        Writer.WriteLine("Sending texts.");
                        Writer.WriteLine("ID\tFirstName\tLastName\tTelephone\tSid");

                        foreach (var item in apps)
                        {
                            string sid = SendText(item);
                            Writer.WriteLine(item.ID + "\t" + item.FirstName + "\t" + item.LastName + "\t" + item.Telephone + "\t" + sid);
                        }

                        Writer.WriteLine("Texting complete.");
                        lastCheck = DateTime.Now;

                        CloseWriter();
                    }
                }

                Thread.Sleep(1000 * 60 * 10); //10 minutes

            }
            
        }

        //Load and check apps
        public void LoadAppointments()
        {
            OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Settings.Instance.DataBasePath + ";");

            //Get Apps
            DateTime tomorrow = DateTime.Today.AddDays(1);
            string sqlCmd = "SELECT * from Transactions WHERE StartTime > #" + tomorrow.ToString("d") + " 12:00:00 AM# AND StartTime < #" + tomorrow.ToString("d") + " 11:59:00 PM#";

            try
            {
                connection.Open();
                OleDbCommand cmd = new OleDbCommand(sqlCmd, connection, connection.BeginTransaction());
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = (int)reader["CustID"];

                    Appointment a = new Appointment();
                    a.StartTime = (DateTime)reader["StartTime"];
                    a.EmpID = (int)reader["EmpID"];

                    a.ID = id;

                    apps.Add(a);
                }

                foreach (Appointment item in apps)
                {
                    connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Settings.Instance.DataBasePath + ";");
                    connection.Open();
                    OleDbCommand cmd1 = new OleDbCommand("SELECT * from Customers WHERE CustID = " + item.ID, connection, connection.BeginTransaction());
                    reader = cmd1.ExecuteReader();

                    while (reader.Read())
                    {
                        item.FirstName = (string)reader["FirstName"];
                        item.LastName = (string)reader["LastName"];
                        item.Telephone = (string)reader["Phone"];
                    }

                    connection.Dispose();
                }
            }
            catch (Exception ex) { Writer.WriteLine("Load Appointments: " + ex.InnerException.Message+ '\t' +ex.Message); }

        }
        public void CheckValidAppointments()
        {
            List<Appointment> removals = new List<Appointment>();

            foreach (Appointment item in apps)
            {
                string number = item.Telephone.Replace("-", "").Replace(" ", "").Replace("+", "");
                byte[] chars = Encoding.ASCII.GetBytes(number.ToCharArray());

                //Remove if empty or has letters
                if (number == "")
                    removals.Add(item);
                else
                {
                    foreach (char digit in chars)
                    {
                        //Make sure all digits are numbers
                        if (digit < 48 || digit > 57)
                        {
                            removals.Add(item);
                            break;
                        }
                    }
                }
            }

            //Remove invalid Appointments
            foreach (Appointment item in removals)
                apps.Remove(item);

        }
        public void CheckEmployee()
        {
            foreach (var item in apps)
            {
                if (item.EmpID == 470)
                    item.Employee = "Lorena";
                else if (item.EmpID == 471)
                    item.Employee = "Lina";

            }
        }

        //Twilio
        public static string SendText(Appointment app)
        {

            TwilioRestClient twilio = new TwilioRestClient(Settings.Instance.AccountSID, Settings.Instance.AuthToken);
            string text = "*REMINDER FROM Z&B*\nYour appointment with {0} at Z&B Accounting is tomorrow at {1}. If you need to cancel your appointment "
                            + "do not respond to this message. Please call the office at (305) 557-2389.";
            string message = string.Format(text, app.Employee, app.StartTime.ToString("t"));

            Message twilioMessage = twilio.SendMessage(Settings.Instance.TwilioPhone, app.Telephone, message, new string[0], "");

            if (twilioMessage.Status == "Failed")
                return twilioMessage.ErrorMessage;
            else return twilioMessage.Sid;
        }

        //Writer
        private void StartWriter()
        {
            Writer = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + @"Logs\" + DateTime.Now.ToString("g").Replace("/", "-").Replace(":", ".") + ".txt");
            Writer.AutoFlush = true;
        }
        private void CloseWriter()
        {
            Writer.Close();
        }

        //Service
        public void OnDebug()
        {
            OnStart(null);
        }
        protected override void OnStart(string[] args)
        {

            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Logs"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Logs");

            ////Load Settings
            if (!Settings.LoadSettings())
                Environment.Exit(1);

#if DEBUG
            DoWork();
#else

            worker = new Thread(DoWork);
            worker.Start();
#endif
        }
        protected override void OnStop()
        {
            worker.Abort();
        }



    }
}
