using System;
using System.Collections.Generic;

namespace AppManager.DB
{
    public partial class GlobalParms
    {
        public int Id { get; set; }
        public string MiscParms { get; set; }
        public int? RefreshTime { get; set; }
        public DateTime? SundayOpen { get; set; }
        public DateTime? MondayOpen { get; set; }
        public DateTime? TuesdayOpen { get; set; }
        public DateTime? WednesdayOpen { get; set; }
        public DateTime? ThursdayOpen { get; set; }
        public DateTime? FridayOpen { get; set; }
        public DateTime? SaturdayOpen { get; set; }
        public DateTime? SundayClose { get; set; }
        public DateTime? MondayClose { get; set; }
        public DateTime? TuesdayClose { get; set; }
        public DateTime? WednesdayClose { get; set; }
        public DateTime? ThursdayClose { get; set; }
        public DateTime? FridayClose { get; set; }
        public DateTime? SaturdayClose { get; set; }
        public int? TimeInterval { get; set; }
        public string CompanyName { get; set; }
        public short? TmFmat { get; set; }
        public bool AddEmpsToQb { get; set; }
        public bool AddCustToQb { get; set; }
        public string DayFont { get; set; }
        public int? DayFontSz { get; set; }
        public string MoFont { get; set; }
        public int? MoFontSz { get; set; }
        public bool DisplayCuName { get; set; }
        public bool DisplayCoName { get; set; }
        public bool DisplayPhone { get; set; }
        public bool DisplayAltPhone { get; set; }
        public bool DisplayEmail { get; set; }
        public bool DisplayCuNameWk { get; set; }
        public bool DisplayCoNameWk { get; set; }
        public bool DisplayPhoneWk { get; set; }
        public bool DisplayAltPhoneWk { get; set; }
        public bool DisplayEmailWk { get; set; }
        public int? FirstDayOfWeek { get; set; }
        public int? BackgroundColor { get; set; }
        public bool EmpsLinkedToQb { get; set; }
        public bool ApptsInitialized { get; set; }
        public string VinType { get; set; }
        public string CustsAreCalled { get; set; }
        public int? RefreshRate { get; set; }
        public bool ShowMinutes { get; set; }
        public bool DisplayAddressWk { get; set; }
        public bool DisplayAddress { get; set; }
        public string EmpSortOrder { get; set; }
        public int? DaysToShow { get; set; }
        public DateTime? DateOfLastBackup { get; set; }
        public int? BackupRem { get; set; }
    }
}
