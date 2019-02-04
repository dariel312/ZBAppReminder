using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class ApptUrls
    {
        public int Id { get; set; }
        public int? TranId { get; set; }
        public string ApptUrl { get; set; }
        public DateTime? StartTime { get; set; }
        public int? EmpId { get; set; }
    }
}
