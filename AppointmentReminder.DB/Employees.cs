using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class Employees
    {
        public Employees()
        {
            Transactions = new HashSet<Transactions>();
        }

        public int EmpId { get; set; }
        public string EmpNo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleI { get; set; }
        public string EmpName { get; set; }
        public string GenList { get; set; }
        public string Phone { get; set; }
        public string AltPhone { get; set; }
        public string Email { get; set; }
        public bool Hide { get; set; }
        public string Position { get; set; }
        public string Qblink { get; set; }
        public DateTime? SundayAvailSt { get; set; }
        public DateTime? MondayAvailSt { get; set; }
        public DateTime? TuesdayAvailSt { get; set; }
        public DateTime? WednesdayAvailSt { get; set; }
        public DateTime? ThursdayAvailSt { get; set; }
        public DateTime? FridayAvailSt { get; set; }
        public DateTime? SaturdayAvailSt { get; set; }
        public DateTime? SundayAvailEnd { get; set; }
        public DateTime? MondayAvailEnd { get; set; }
        public DateTime? TuesdayAvailEnd { get; set; }
        public DateTime? WednesdayAvailEnd { get; set; }
        public DateTime? ThursdayAvailEnd { get; set; }
        public DateTime? FridayAvailEnd { get; set; }
        public DateTime? SaturdayAvailEnd { get; set; }
        public string GoogleCalEmail { get; set; }
        public string GoogleCalPassword { get; set; }
        public int? UserNumber { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateDeleted { get; set; }
        public int? MyobemployeeId { get; set; }
        public bool Qbdeleted { get; set; }
        public DateTime? LastEditDtTm { get; set; }

        public ICollection<Transactions> Transactions { get; set; }
    }
}
