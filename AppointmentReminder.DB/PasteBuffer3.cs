using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class PasteBuffer3
    {
        public int TranId { get; set; }
        public int? EmpId { get; set; }
        public int? CustId { get; set; }
        public string Note { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? StartTime { get; set; }
        public int? Clr { get; set; }
        public int? Bclr { get; set; }
        public bool Off { get; set; }
        public bool AllDayTask { get; set; }
        public int? ApptClr { get; set; }
        public int? MlinkId { get; set; }
        public int? RecurId { get; set; }
        public DateTime? RemAdvance { get; set; }
        public string Services { get; set; }
    }
}
