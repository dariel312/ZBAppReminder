using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class EmpProdRpt1
    {
        public int Id { get; set; }
        public string Services { get; set; }
        public string EmpName { get; set; }
        public string Position { get; set; }
        public int? EmpId { get; set; }
        public int? ApptHours { get; set; }
        public int? HoursPercent { get; set; }
        public int? TotAvalHours { get; set; }
        public int? TotApptHours { get; set; }
        public int? EmpUtil { get; set; }
    }
}
