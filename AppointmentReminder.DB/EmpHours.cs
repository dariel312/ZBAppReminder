using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class EmpHours
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public DateTime? AsOfDate { get; set; }
        public string SchedDesc { get; set; }
        public DateTime? Day1In { get; set; }
        public DateTime? Day2In { get; set; }
        public DateTime? Day3In { get; set; }
        public DateTime? Day4In { get; set; }
        public DateTime? Day5In { get; set; }
        public DateTime? Day6In { get; set; }
        public DateTime? Day7In { get; set; }
        public DateTime? Day1Out { get; set; }
        public DateTime? Day2Out { get; set; }
        public DateTime? Day3Out { get; set; }
        public DateTime? Day4Out { get; set; }
        public DateTime? Day5Out { get; set; }
        public DateTime? Day6Out { get; set; }
        public DateTime? Day7Out { get; set; }
        public int? UserNumber { get; set; }
    }
}
