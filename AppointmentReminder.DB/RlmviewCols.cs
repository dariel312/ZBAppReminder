using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class RlmviewCols
    {
        public int Id { get; set; }
        public int? ListId { get; set; }
        public int? UserIdno { get; set; }
        public string FldNm { get; set; }
        public string ColTitle { get; set; }
        public int? DataTyp { get; set; }
        public int? ColWidth { get; set; }
        public bool ColVisible { get; set; }
    }
}
