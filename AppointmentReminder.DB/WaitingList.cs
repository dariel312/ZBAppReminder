using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class WaitingList
    {
        public int Wlid { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartTm { get; set; }
        public DateTime? EndTm { get; set; }
        public int? Duration { get; set; }
        public DateTime? ApptStart { get; set; }
        public DateTime? ApptEnd { get; set; }
        public string EmpList { get; set; }
        public int? CustId { get; set; }
        public int? ServiceId { get; set; }
        public string Note { get; set; }
        public bool AllDayTask { get; set; }
        public DateTime? RemAdvance { get; set; }
        public int? ApptClr { get; set; }
        public int? ColPos { get; set; }
        public DateTime? DtTmStamp { get; set; }
        public int? Adays { get; set; }
        public int? Bclr { get; set; }
        public string Services { get; set; }

        public Service Service { get; set; }
    }
}
