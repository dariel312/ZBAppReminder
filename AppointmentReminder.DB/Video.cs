using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class Video
    {
        public int VideoId { get; set; }
        public int? Pattern1X { get; set; }
        public int? Pattern1Y { get; set; }
        public int? Pattern2X { get; set; }
        public int? Pattern2Y { get; set; }
        public int? Pattern3X { get; set; }
        public int? Pattern3Y { get; set; }
        public bool Interlaced { get; set; }
        public bool Lcd { get; set; }
        public bool WideScreen { get; set; }
    }
}
