using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class CheckIn
    {
        public int Id { get; set; }
        public int? TranId { get; set; }
        public int? CheckedInStatus { get; set; }
        public DateTime? Dt { get; set; }
        public DateTime? CheckInTime { get; set; }
    }
}
