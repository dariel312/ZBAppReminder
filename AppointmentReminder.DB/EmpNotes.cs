using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class EmpNotes
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public string Note { get; set; }
        public DateTime? Ndt { get; set; }
    }
}
