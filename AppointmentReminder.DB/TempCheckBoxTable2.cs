﻿using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class TempCheckBoxTable2
    {
        public int Id { get; set; }
        public int? OtherTableId { get; set; }
        public bool CheckFlag { get; set; }
    }
}