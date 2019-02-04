using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class CustAndApptAttachments
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? LinkedRecId { get; set; }
        public int? RecType { get; set; }
        public string SourceFile { get; set; }
        public string FileName { get; set; }
        public string Descp { get; set; }
    }
}
