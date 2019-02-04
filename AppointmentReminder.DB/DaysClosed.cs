using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class DaysClosed
    {
        public int Id { get; set; }
        public int? Typ { get; set; }
        public string Description { get; set; }
        public int? HolidayNo { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public int? WeekNo { get; set; }
        public bool OffThisDay { get; set; }
        public DateTime? ClosedOnDate { get; set; }
    }
}
