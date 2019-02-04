using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class Service
    {
        public Service()
        {
            Mlinks = new HashSet<Mlinks>();
            Transactions = new HashSet<Transactions>();
            WaitingList = new HashSet<WaitingList>();
        }

        public int ServiceId { get; set; }
        public string Service1 { get; set; }
        public bool Qbdeleted { get; set; }
        public string Qblink { get; set; }
        public int? Fclr { get; set; }
        public int? Bclr { get; set; }
        public int? Duration { get; set; }
        public DateTime? LastEditDtTm { get; set; }

        public ICollection<Mlinks> Mlinks { get; set; }
        public ICollection<Transactions> Transactions { get; set; }
        public ICollection<WaitingList> WaitingList { get; set; }
    }
}
