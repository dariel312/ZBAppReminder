using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class TablesDateRange
    {
        public int Id { get; set; }
        public int? Table1 { get; set; }
        public DateTime? Tm { get; set; }
        public string FldRange { get; set; }
    }
}
