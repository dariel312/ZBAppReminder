using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class ServiceSort
    {
        public int Id { get; set; }
        public int? ServiceId { get; set; }
        public int? UserIdno { get; set; }
        public int? SortData { get; set; }
    }
}
