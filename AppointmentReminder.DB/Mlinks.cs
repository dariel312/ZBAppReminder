using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class Mlinks
    {
        public Mlinks()
        {
            Transactions = new HashSet<Transactions>();
        }

        public int MlinkId { get; set; }
        public int? EmpId { get; set; }
        public int? CustId { get; set; }
        public string Note { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? StartTime { get; set; }
        public int? Clr { get; set; }
        public bool Off { get; set; }
        public int? RecurId { get; set; }
        public bool AllDayTask { get; set; }
        public int? ServiceId { get; set; }
        public int? ApptClr { get; set; }
        public DateTime? RemAdvance { get; set; }
        public int? Recur1Meeting2 { get; set; }
        public int? UserNumber { get; set; }
        public int? RemDays { get; set; }
        public DateTime? RemDate { get; set; }
        public int? Bclr { get; set; }
        public string Services { get; set; }

        public Service Service { get; set; }
        public ICollection<Transactions> Transactions { get; set; }
    }
}
