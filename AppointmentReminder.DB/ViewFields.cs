using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class ViewFields
    {
        public int Id { get; set; }
        public bool DayView { get; set; }
        public string Nm { get; set; }
        public int? UserParmsId { get; set; }
    }
}
