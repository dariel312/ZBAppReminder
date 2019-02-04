using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class RepeaterStatusTable4
    {
        public int Id { get; set; }
        public int? StatusId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
