using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class SchedulableWls1
    {
        public int Id { get; set; }
        public int? Wlid { get; set; }
        public DateTime? OpenStartTime { get; set; }
    }
}
