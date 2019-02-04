using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class CamregTable
    {
        public int Id { get; set; }
        public int? CamuserId { get; set; }
        public string Key1 { get; set; }
        public string Key2 { get; set; }
        public string DataFld { get; set; }
    }
}
