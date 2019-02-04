using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class Recurs
    {
        public Recurs()
        {
            Transactions = new HashSet<Transactions>();
        }

        public int RecurId { get; set; }
        public DateTime? StartDate { get; set; }
        public bool Repeat { get; set; }
        public int? Daily { get; set; }
        public int? EveryNoOfDays { get; set; }
        public int? EveryWeekDay { get; set; }
        public int? Weekly { get; set; }
        public int? EveryNoWeeks { get; set; }
        public int? Sunday { get; set; }
        public int? Monday { get; set; }
        public int? Tuesday { get; set; }
        public int? Wednesday { get; set; }
        public int? Thursday { get; set; }
        public int? Friday { get; set; }
        public int? Saturday { get; set; }
        public int? Monthly { get; set; }
        public int? MoDayNumber { get; set; }
        public int? WeekNo { get; set; }
        public int? MdayNo1to7 { get; set; }
        public int? EveryNoMonths { get; set; }
        public int? Yearly { get; set; }
        public DateTime? YearlyOnDate { get; set; }
        public int? YearWeek { get; set; }
        public int? YearlyMo { get; set; }
        public int? YearDay { get; set; }
        public DateTime? RepeatUntil { get; set; }
        public int? NumRepeats { get; set; }
        public int? UserNumber { get; set; }

        public ICollection<Transactions> Transactions { get; set; }
    }
}
