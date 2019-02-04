using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class Qbparms
    {
        public int Id { get; set; }
        public string FlPath { get; set; }
        public bool AutoSync { get; set; }
        public bool UseBillToAddress { get; set; }
        public DateTime? TimeOfLastQbquery { get; set; }
        public bool IncludeInactiveCusts { get; set; }
    }
}
