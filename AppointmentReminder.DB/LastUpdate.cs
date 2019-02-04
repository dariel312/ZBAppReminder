using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class LastUpdate
    {
        public int LastUpdateId { get; set; }
        public int? GlobalsUpdateNumber { get; set; }
        public int? EmpUpdateNumber { get; set; }
        public int? CustUpdateNumber { get; set; }
        public int? ApptUpdateNumber { get; set; }
    }
}
