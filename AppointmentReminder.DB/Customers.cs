using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class Customers
    {
        public int CustId { get; set; }
        public string Customer { get; set; }
        public string Coname { get; set; }
        public string LastName { get; set; }
        public string MiddleI { get; set; }
        public string FirstName { get; set; }
        public string AcctNo { get; set; }
        public string Phone { get; set; }
        public string AltPhone { get; set; }
        public string Email { get; set; }
        public string Qblink { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateDeleted { get; set; }
        public string Notes { get; set; }
        public string Address { get; set; }
        public int? MyobcustId { get; set; }
        public bool Qbdeleted { get; set; }
        public string Zip { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public DateTime? LastEditDtTm { get; set; }


        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
