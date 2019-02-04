using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class CustomerListing
    {
        public int Id { get; set; }
        public int? Pos { get; set; }
        public int? Fwidth { get; set; }
        public int? SrtOrder { get; set; }
        public string FldName { get; set; }
        public string FldTitle { get; set; }
    }
}
