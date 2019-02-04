using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class CustRpt1
    {
        public int Id { get; set; }
        public DateTime? RecDt { get; set; }
        public string TranInfo { get; set; }
        public string CustInfo { get; set; }
        public int? CustId { get; set; }
        public string CustName { get; set; }
    }
}
