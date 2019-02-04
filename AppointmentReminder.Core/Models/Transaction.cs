using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentReminder.Core.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public DateTime StartTime { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public Customer Customer { get; set; }

    }
}
