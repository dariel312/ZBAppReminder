using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class Rlmsort
    {
        public int Id { get; set; }
        public int? ListId { get; set; }
        public int? UserIdno { get; set; }
        public string SortOrder { get; set; }
    }
}
