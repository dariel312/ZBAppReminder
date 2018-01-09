using System;
using System.Collections.Generic;
using System.Text;
using AppointmentReminder.Core;

namespace AppointmentReminder.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var rem = new AppReminder(new AppSettings()
            {
                AccountSID = "ACa77a27c580f1705b0a8e0b08d5295da0",
                AuthToken = "facddeab476aaf4c6fa3c6cc5ca69837",
            });
            Console.WriteLine(PhoneService.SendPhoneCall("3055824961", "3059164696", "http://66.176.76.31/reminder_call.aspx"));
            Console.ReadLine();
        }
    }
}
