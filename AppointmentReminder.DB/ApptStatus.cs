using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class ApptStatus
    {
        public int Aid { get; set; }
        public int? ApptStatusNum { get; set; }
        public string ApptStatus1 { get; set; }
        public DateTime? DateDeleted { get; set; }
        public int? IconNumber { get; set; }
    }
}
