using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class EmpSort
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? UserIdno { get; set; }
        public int? SortData { get; set; }
    }
}
