using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class UserEmpInfo
    {
        public int Id { get; set; }
        public int? EmpColWidth { get; set; }
        public int? EmpId { get; set; }
        public int? UserParmsId { get; set; }
    }
}
