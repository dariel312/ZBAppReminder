using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class MergeFiles
    {
        public int DocId { get; set; }
        public string Title { get; set; }
        public bool OldStyle { get; set; }
    }
}
