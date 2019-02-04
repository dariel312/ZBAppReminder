using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AppManager.DB
{
    public partial class AppManagerContext : DbContext
    {
        public AppManagerContext()
        {
        }

        public AppManagerContext(DbContextOptions<AppManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApptStatus> ApptStatus { get; set; }
        public virtual DbSet<ApptUrls> ApptUrls { get; set; }
        public virtual DbSet<AuditTrail> AuditTrail { get; set; }
        public virtual DbSet<Camdbver> Camdbver { get; set; }
        public virtual DbSet<CamregTable> CamregTable { get; set; }
        public virtual DbSet<CheckIn> CheckIn { get; set; }
        public virtual DbSet<ClrSchemes> ClrSchemes { get; set; }
        public virtual DbSet<CustAndApptAttachments> CustAndApptAttachments { get; set; }
        public virtual DbSet<CustomerListing> CustomerListing { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<CustomFlds> CustomFlds { get; set; }
        public virtual DbSet<CustRpt1> CustRpt1 { get; set; }
        public virtual DbSet<CustRpt2> CustRpt2 { get; set; }
        public virtual DbSet<CustRpt3> CustRpt3 { get; set; }
        public virtual DbSet<CustRpt4> CustRpt4 { get; set; }
        public virtual DbSet<DaysClosed> DaysClosed { get; set; }
        public virtual DbSet<EmpHours> EmpHours { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<EmpNotes> EmpNotes { get; set; }
        public virtual DbSet<EmpProdRpt1> EmpProdRpt1 { get; set; }
        public virtual DbSet<EmpProdRpt2> EmpProdRpt2 { get; set; }
        public virtual DbSet<EmpSort> EmpSort { get; set; }
        public virtual DbSet<GlobalParms> GlobalParms { get; set; }
        public virtual DbSet<LastUpdate> LastUpdate { get; set; }
        public virtual DbSet<MergeFiles> MergeFiles { get; set; }
        public virtual DbSet<Mlinks> Mlinks { get; set; }
        public virtual DbSet<PasteBuffer1> PasteBuffer1 { get; set; }
        public virtual DbSet<PasteBuffer2> PasteBuffer2 { get; set; }
        public virtual DbSet<PasteBuffer3> PasteBuffer3 { get; set; }
        public virtual DbSet<PasteBuffer4> PasteBuffer4 { get; set; }
        public virtual DbSet<Qbparms> Qbparms { get; set; }
        public virtual DbSet<Recurs> Recurs { get; set; }
        public virtual DbSet<RepeaterStatusTable1> RepeaterStatusTable1 { get; set; }
        public virtual DbSet<RepeaterStatusTable2> RepeaterStatusTable2 { get; set; }
        public virtual DbSet<RepeaterStatusTable3> RepeaterStatusTable3 { get; set; }
        public virtual DbSet<RepeaterStatusTable4> RepeaterStatusTable4 { get; set; }
        public virtual DbSet<Rlmsort> Rlmsort { get; set; }
        public virtual DbSet<RlmviewCols> RlmviewCols { get; set; }
        public virtual DbSet<SchedulableWls1> SchedulableWls1 { get; set; }
        public virtual DbSet<SchedulableWls2> SchedulableWls2 { get; set; }
        public virtual DbSet<SchedulableWls3> SchedulableWls3 { get; set; }
        public virtual DbSet<SchedulableWls4> SchedulableWls4 { get; set; }
        public virtual DbSet<SearchList> SearchList { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceSort> ServiceSort { get; set; }
        public virtual DbSet<TablesDateRange> TablesDateRange { get; set; }
        public virtual DbSet<TempCheckBoxTable1> TempCheckBoxTable1 { get; set; }
        public virtual DbSet<TempCheckBoxTable2> TempCheckBoxTable2 { get; set; }
        public virtual DbSet<TempCheckBoxTable3> TempCheckBoxTable3 { get; set; }
        public virtual DbSet<TempCheckBoxTable4> TempCheckBoxTable4 { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<UserEmpInfo> UserEmpInfo { get; set; }
        public virtual DbSet<Video> Video { get; set; }
        public virtual DbSet<ViewFields> ViewFields { get; set; }
        public virtual DbSet<WaitingList> WaitingList { get; set; }
        public virtual DbSet<WpasteBuffer> WpasteBuffer { get; set; }

        // Unable to generate entity type for table 'Jet.LastDateAccessed'. Please see the warning messages.
        // Unable to generate entity type for table 'Jet.UserParms'. Please see the warning messages.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApptStatus>(entity =>
            {
                entity.HasKey(e => e.Aid);

                entity.Property(e => e.Aid).HasColumnName("AID");

                entity.Property(e => e.ApptStatus1)
                    .HasColumnName("ApptStatus")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<ApptUrls>(entity =>
            {
                entity.ToTable("ApptURLs");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApptUrl).HasColumnName("ApptURL");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.TranId).HasColumnName("TranID");
            });

            modelBuilder.Entity<AuditTrail>(entity =>
            {
                entity.HasKey(e => e.TranId);

                entity.Property(e => e.TranId).HasColumnName("TranID");

                entity.Property(e => e.AllDayTask).HasColumnType("bit");

                entity.Property(e => e.Bclr).HasColumnName("BClr");

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.MlinkId).HasColumnName("MlinkID");

                entity.Property(e => e.Off)
                    .HasColumnName("OFF")
                    .HasColumnType("bit");

                entity.Property(e => e.OldId).HasColumnName("OldID");

                entity.Property(e => e.RecurId).HasColumnName("RecurID");

                entity.Property(e => e.Services).HasMaxLength(254);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");
            });

            modelBuilder.Entity<Camdbver>(entity =>
            {
                entity.ToTable("CAMDBVer");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Dbver).HasColumnName("DBVer");
            });

            modelBuilder.Entity<CamregTable>(entity =>
            {
                entity.ToTable("CAMRegTable");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CamuserId).HasColumnName("CAMUserID");

                entity.Property(e => e.Key1).HasMaxLength(255);

                entity.Property(e => e.Key2).HasMaxLength(255);
            });

            modelBuilder.Entity<CheckIn>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TranId).HasColumnName("TranID");
            });

            modelBuilder.Entity<ClrSchemes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Bclr).HasColumnName("BClr");

                entity.Property(e => e.Fclr).HasColumnName("FClr");
            });

            modelBuilder.Entity<CustAndApptAttachments>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LinkedRecId).HasColumnName("LinkedRecID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CustomerListing>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FldName).HasMaxLength(30);

                entity.Property(e => e.FldTitle).HasMaxLength(30);

                entity.Property(e => e.Fwidth).HasColumnName("FWidth");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustId);

                entity.HasIndex(e => e.AcctNo)
                    .HasName("AcctNo1");

                entity.HasIndex(e => e.AltPhone)
                    .HasName("AltPhone");

                entity.HasIndex(e => e.Coname)
                    .HasName("Coname");

                entity.HasIndex(e => e.CustId)
                    .HasName("ClientID")
                    .IsUnique();

                entity.HasIndex(e => e.Customer)
                    .HasName("Client");

                entity.HasIndex(e => e.Email)
                    .HasName("Email");

                entity.HasIndex(e => e.FirstName)
                    .HasName("FirstName");

                entity.HasIndex(e => e.LastName)
                    .HasName("AcctNo");

                entity.HasIndex(e => e.Phone)
                    .HasName("ContactNm");

                entity.HasIndex(e => e.Qblink)
                    .HasName("QBLink");

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.AcctNo).HasMaxLength(30);

                entity.Property(e => e.Addr1).HasMaxLength(30);

                entity.Property(e => e.Addr2).HasMaxLength(30);

                entity.Property(e => e.AltPhone).HasMaxLength(20);

                entity.Property(e => e.City).HasMaxLength(30);

                entity.Property(e => e.Coname).HasMaxLength(50);

                entity.Property(e => e.Customer).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(31);

                entity.Property(e => e.LastEditDtTm).HasColumnName("LastEditDtTM");

                entity.Property(e => e.LastName).HasMaxLength(31);

                entity.Property(e => e.MiddleI).HasMaxLength(2);

                entity.Property(e => e.MyobcustId).HasColumnName("MYOBCustID");

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Qbdeleted)
                    .HasColumnName("QBDeleted")
                    .HasColumnType("bit");

                entity.Property(e => e.Qblink)
                    .HasColumnName("QBLink")
                    .HasMaxLength(99);

                entity.Property(e => e.State).HasMaxLength(3);

                entity.Property(e => e.Zip).HasMaxLength(14);

                entity.HasMany(m => m.Transactions)
                      .WithOne(m => m.Customer);
            });

            modelBuilder.Entity<CustomFlds>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Dtype)
                    .HasColumnName("DType")
                    .HasMaxLength(30);

                entity.Property(e => e.FldName).HasMaxLength(30);

                entity.Property(e => e.Flength).HasColumnName("FLength");

                entity.Property(e => e.Frm).HasMaxLength(30);

                entity.Property(e => e.Ftitle)
                    .HasColumnName("FTitle")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<CustRpt1>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.CustName).HasMaxLength(150);
            });

            modelBuilder.Entity<CustRpt2>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.CustName).HasMaxLength(150);
            });

            modelBuilder.Entity<CustRpt3>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.CustName).HasMaxLength(150);
            });

            modelBuilder.Entity<CustRpt4>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.CustName).HasMaxLength(150);
            });

            modelBuilder.Entity<DaysClosed>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(128);

                entity.Property(e => e.OffThisDay).HasColumnType("bit");
            });

            modelBuilder.Entity<EmpHours>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.SchedDesc).HasMaxLength(30);
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.HasIndex(e => e.EmpName)
                    .HasName("Department");

                entity.HasIndex(e => e.FirstName)
                    .HasName("Number");

                entity.HasIndex(e => e.GenList)
                    .HasName("Location");

                entity.HasIndex(e => e.LastName)
                    .HasName("Names");

                entity.HasIndex(e => e.MiddleI)
                    .HasName("Position");

                entity.HasIndex(e => e.Phone)
                    .HasName("Phone");

                entity.HasIndex(e => e.Position)
                    .HasName("Positiion");

                entity.HasIndex(e => e.Qblink)
                    .HasName("QBLink");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.AltPhone).HasMaxLength(20);

                entity.Property(e => e.Email)
                    .HasColumnName("EMail")
                    .HasMaxLength(99);

                entity.Property(e => e.EmpName).HasMaxLength(65);

                entity.Property(e => e.EmpNo).HasMaxLength(15);

                entity.Property(e => e.FirstName).HasMaxLength(31);

                entity.Property(e => e.GenList).HasMaxLength(20);

                entity.Property(e => e.GoogleCalEmail).HasMaxLength(200);

                entity.Property(e => e.GoogleCalPassword).HasMaxLength(200);

                entity.Property(e => e.Hide).HasColumnType("bit");

                entity.Property(e => e.LastEditDtTm).HasColumnName("LastEditDtTM");

                entity.Property(e => e.LastName).HasMaxLength(31);

                entity.Property(e => e.MiddleI).HasMaxLength(2);

                entity.Property(e => e.MyobemployeeId).HasColumnName("MYOBEmployeeID");

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Position).HasMaxLength(50);

                entity.Property(e => e.Qbdeleted)
                    .HasColumnName("QBDeleted")
                    .HasColumnType("bit");

                entity.Property(e => e.Qblink)
                    .HasColumnName("QBLink")
                    .HasMaxLength(99);
            });

            modelBuilder.Entity<EmpNotes>(entity =>
            {
                entity.HasIndex(e => e.EmpId)
                    .HasName("EmpID");

                entity.HasIndex(e => e.Ndt)
                    .HasName("NDt");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.Ndt).HasColumnName("NDt");

                entity.Property(e => e.Note).HasMaxLength(75);
            });

            modelBuilder.Entity<EmpProdRpt1>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.EmpName).HasMaxLength(65);

                entity.Property(e => e.Position).HasMaxLength(65);

                entity.Property(e => e.Services).HasMaxLength(255);
            });

            modelBuilder.Entity<EmpProdRpt2>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.EmpName).HasMaxLength(65);

                entity.Property(e => e.Position).HasMaxLength(65);

                entity.Property(e => e.Services).HasMaxLength(255);
            });

            modelBuilder.Entity<EmpSort>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.UserIdno).HasColumnName("UserIDNo");
            });

            modelBuilder.Entity<GlobalParms>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddCustToQb)
                    .HasColumnName("AddCustToQB")
                    .HasColumnType("bit");

                entity.Property(e => e.AddEmpsToQb)
                    .HasColumnName("AddEmpsToQB")
                    .HasColumnType("bit");

                entity.Property(e => e.ApptsInitialized).HasColumnType("bit");

                entity.Property(e => e.BackgroundColor).HasDefaultValueSql("0");

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.CustsAreCalled).HasMaxLength(50);

                entity.Property(e => e.DayFont).HasMaxLength(50);

                entity.Property(e => e.DayFontSz).HasDefaultValueSql("0");

                entity.Property(e => e.DisplayAddress).HasColumnType("bit");

                entity.Property(e => e.DisplayAddressWk)
                    .HasColumnName("DisplayAddressWK")
                    .HasColumnType("bit");

                entity.Property(e => e.DisplayAltPhone).HasColumnType("bit");

                entity.Property(e => e.DisplayAltPhoneWk)
                    .HasColumnName("DisplayAltPhoneWK")
                    .HasColumnType("bit");

                entity.Property(e => e.DisplayCoName).HasColumnType("bit");

                entity.Property(e => e.DisplayCoNameWk)
                    .HasColumnName("DisplayCoNameWK")
                    .HasColumnType("bit");

                entity.Property(e => e.DisplayCuName).HasColumnType("bit");

                entity.Property(e => e.DisplayCuNameWk)
                    .HasColumnName("DisplayCuNameWK")
                    .HasColumnType("bit");

                entity.Property(e => e.DisplayEmail).HasColumnType("bit");

                entity.Property(e => e.DisplayEmailWk)
                    .HasColumnName("DisplayEmailWK")
                    .HasColumnType("bit");

                entity.Property(e => e.DisplayPhone).HasColumnType("bit");

                entity.Property(e => e.DisplayPhoneWk)
                    .HasColumnName("DisplayPhoneWK")
                    .HasColumnType("bit");

                entity.Property(e => e.EmpSortOrder).HasMaxLength(20);

                entity.Property(e => e.EmpsLinkedToQb)
                    .HasColumnName("EmpsLinkedToQB")
                    .HasColumnType("bit");

                entity.Property(e => e.FirstDayOfWeek).HasDefaultValueSql("0");

                entity.Property(e => e.MoFont).HasMaxLength(50);

                entity.Property(e => e.MoFontSz).HasDefaultValueSql("0");

                entity.Property(e => e.RefreshTime).HasDefaultValueSql("0");

                entity.Property(e => e.ShowMinutes).HasColumnType("bit");

                entity.Property(e => e.TimeInterval).HasDefaultValueSql("0");

                entity.Property(e => e.TmFmat).HasDefaultValueSql("0");

                entity.Property(e => e.VinType).HasMaxLength(30);
            });

            modelBuilder.Entity<LastUpdate>(entity =>
            {
                entity.Property(e => e.LastUpdateId).HasColumnName("LastUpdateID");
            });

            modelBuilder.Entity<MergeFiles>(entity =>
            {
                entity.HasKey(e => e.DocId);

                entity.Property(e => e.DocId).HasColumnName("DocID");

                entity.Property(e => e.OldStyle).HasColumnType("bit");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Mlinks>(entity =>
            {
                entity.HasKey(e => e.MlinkId);

                entity.ToTable("MLinks");

                entity.HasIndex(e => e.CustId)
                    .HasName("CustID");

                entity.HasIndex(e => e.EmpId)
                    .HasName("EmpID");

                entity.HasIndex(e => e.Recur1Meeting2)
                    .HasName("Recur1Meeting2");

                entity.HasIndex(e => e.RecurId)
                    .HasName("RecurID");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("ServiceID");

                entity.Property(e => e.MlinkId).HasColumnName("MlinkID");

                entity.Property(e => e.AllDayTask).HasColumnType("bit");

                entity.Property(e => e.ApptClr).HasDefaultValueSql("0");

                entity.Property(e => e.Bclr).HasColumnName("BClr");

                entity.Property(e => e.Clr).HasDefaultValueSql("0");

                entity.Property(e => e.CustId)
                    .HasColumnName("CustID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.EmpId)
                    .HasColumnName("EmpID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Off).HasColumnType("bit");

                entity.Property(e => e.Recur1Meeting2).HasDefaultValueSql("0");

                entity.Property(e => e.RecurId)
                    .HasColumnName("RecurID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("ServiceID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Services).HasMaxLength(254);

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Mlinks)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("Reference10");
            });

            modelBuilder.Entity<PasteBuffer1>(entity =>
            {
                entity.HasKey(e => e.TranId);

                entity.Property(e => e.TranId).HasColumnName("TranID");

                entity.Property(e => e.AllDayTask).HasColumnType("bit");

                entity.Property(e => e.Bclr).HasColumnName("BClr");

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.MlinkId).HasColumnName("MlinkID");

                entity.Property(e => e.Off)
                    .HasColumnName("OFF")
                    .HasColumnType("bit");

                entity.Property(e => e.RecurId).HasColumnName("RecurID");

                entity.Property(e => e.Services).HasMaxLength(254);
            });

            modelBuilder.Entity<PasteBuffer2>(entity =>
            {
                entity.HasKey(e => e.TranId);

                entity.Property(e => e.TranId).HasColumnName("TranID");

                entity.Property(e => e.AllDayTask).HasColumnType("bit");

                entity.Property(e => e.Bclr).HasColumnName("BClr");

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.MlinkId).HasColumnName("MlinkID");

                entity.Property(e => e.Off)
                    .HasColumnName("OFF")
                    .HasColumnType("bit");

                entity.Property(e => e.RecurId).HasColumnName("RecurID");

                entity.Property(e => e.Services).HasMaxLength(254);
            });

            modelBuilder.Entity<PasteBuffer3>(entity =>
            {
                entity.HasKey(e => e.TranId);

                entity.Property(e => e.TranId).HasColumnName("TranID");

                entity.Property(e => e.AllDayTask).HasColumnType("bit");

                entity.Property(e => e.Bclr).HasColumnName("BClr");

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.MlinkId).HasColumnName("MlinkID");

                entity.Property(e => e.Off)
                    .HasColumnName("OFF")
                    .HasColumnType("bit");

                entity.Property(e => e.RecurId).HasColumnName("RecurID");

                entity.Property(e => e.Services).HasMaxLength(254);
            });

            modelBuilder.Entity<PasteBuffer4>(entity =>
            {
                entity.HasKey(e => e.TranId);

                entity.Property(e => e.TranId).HasColumnName("TranID");

                entity.Property(e => e.AllDayTask).HasColumnType("bit");

                entity.Property(e => e.Bclr).HasColumnName("BClr");

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.MlinkId).HasColumnName("MlinkID");

                entity.Property(e => e.Off)
                    .HasColumnName("OFF")
                    .HasColumnType("bit");

                entity.Property(e => e.RecurId).HasColumnName("RecurID");

                entity.Property(e => e.Services).HasMaxLength(254);
            });

            modelBuilder.Entity<Qbparms>(entity =>
            {
                entity.ToTable("QBParms");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AutoSync).HasColumnType("bit");

                entity.Property(e => e.IncludeInactiveCusts).HasColumnType("bit");

                entity.Property(e => e.TimeOfLastQbquery).HasColumnName("TimeOfLastQBQuery");

                entity.Property(e => e.UseBillToAddress).HasColumnType("bit");
            });

            modelBuilder.Entity<Recurs>(entity =>
            {
                entity.HasKey(e => e.RecurId);

                entity.Property(e => e.RecurId).HasColumnName("RecurID");

                entity.Property(e => e.MdayNo1to7).HasColumnName("MDayNo1to7");

                entity.Property(e => e.Repeat).HasColumnType("bit");
            });

            modelBuilder.Entity<RepeaterStatusTable1>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");
            });

            modelBuilder.Entity<RepeaterStatusTable2>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");
            });

            modelBuilder.Entity<RepeaterStatusTable3>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");
            });

            modelBuilder.Entity<RepeaterStatusTable4>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");
            });

            modelBuilder.Entity<Rlmsort>(entity =>
            {
                entity.ToTable("RLMSort");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ListId).HasColumnName("ListID");

                entity.Property(e => e.SortOrder).HasMaxLength(255);

                entity.Property(e => e.UserIdno).HasColumnName("UserIDNo");
            });

            modelBuilder.Entity<RlmviewCols>(entity =>
            {
                entity.ToTable("RLMViewCols");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ColTitle).HasMaxLength(30);

                entity.Property(e => e.ColVisible).HasColumnType("bit");

                entity.Property(e => e.FldNm).HasMaxLength(30);

                entity.Property(e => e.ListId).HasColumnName("ListID");

                entity.Property(e => e.UserIdno).HasColumnName("UserIDNo");
            });

            modelBuilder.Entity<SchedulableWls1>(entity =>
            {
                entity.ToTable("SchedulableWLs1");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Wlid).HasColumnName("WLID");
            });

            modelBuilder.Entity<SchedulableWls2>(entity =>
            {
                entity.ToTable("SchedulableWLs2");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Wlid).HasColumnName("WLID");
            });

            modelBuilder.Entity<SchedulableWls3>(entity =>
            {
                entity.ToTable("SchedulableWLs3");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Wlid).HasColumnName("WLID");
            });

            modelBuilder.Entity<SchedulableWls4>(entity =>
            {
                entity.ToTable("SchedulableWLs4");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Wlid).HasColumnName("WLID");
            });

            modelBuilder.Entity<SearchList>(entity =>
            {
                entity.HasKey(e => e.SlId);

                entity.Property(e => e.SlId).HasColumnName("SlID");

                entity.Property(e => e.SearchText).HasMaxLength(255);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasIndex(e => e.Service1)
                    .HasName("Service");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("SeviceID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.Bclr).HasColumnName("BClr");

                entity.Property(e => e.Fclr).HasColumnName("FClr");

                entity.Property(e => e.LastEditDtTm).HasColumnName("LastEditDtTM");

                entity.Property(e => e.Qbdeleted)
                    .HasColumnName("QBDeleted")
                    .HasColumnType("bit");

                entity.Property(e => e.Qblink)
                    .HasColumnName("QBLink")
                    .HasMaxLength(99);

                entity.Property(e => e.Service1)
                    .HasColumnName("Service")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<ServiceSort>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.UserIdno).HasColumnName("UserIDNo");
            });

            modelBuilder.Entity<TablesDateRange>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<TempCheckBoxTable1>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CheckFlag).HasColumnType("bit");

                entity.Property(e => e.OtherTableId).HasColumnName("OtherTableID");
            });

            modelBuilder.Entity<TempCheckBoxTable2>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CheckFlag).HasColumnType("bit");

                entity.Property(e => e.OtherTableId).HasColumnName("OtherTableID");
            });

            modelBuilder.Entity<TempCheckBoxTable3>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CheckFlag).HasColumnType("bit");

                entity.Property(e => e.OtherTableId).HasColumnName("OtherTableID");
            });

            modelBuilder.Entity<TempCheckBoxTable4>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CheckFlag).HasColumnType("bit");

                entity.Property(e => e.OtherTableId).HasColumnName("OtherTableID");
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(e => e.TranId);

                entity.HasIndex(e => e.CustId)
                    .HasName("Client");

                entity.HasIndex(e => e.EmpId)
                    .HasName("EmpID");

                entity.HasIndex(e => e.MlinkId)
                    .HasName("MLink");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("ServiceTypeID");

                entity.Property(e => e.TranId).HasColumnName("TranID");

                entity.Property(e => e.AllDayTask).HasColumnType("bit");

                entity.Property(e => e.ApptClr).HasDefaultValueSql("0");

                entity.Property(e => e.Bclr).HasColumnName("BClr");

                entity.Property(e => e.Clr).HasDefaultValueSql("0");

                entity.Property(e => e.CustId)
                    .HasColumnName("CustID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.EmpId)
                    .HasColumnName("EmpID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.MlinkId)
                    .HasColumnName("MlinkID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.NoShow).HasColumnType("bit");

                entity.Property(e => e.Off).HasColumnType("bit");

                entity.Property(e => e.RecurId)
                    .HasColumnName("RecurID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("ServiceID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Services).HasMaxLength(254);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("Reference");

                entity.HasOne(d => d.Mlink)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.MlinkId)
                    .HasConstraintName("Reference12");

                entity.HasOne(d => d.Recur)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.RecurId)
                    .HasConstraintName("Reference13");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("Reference6");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CustId);
            });

            modelBuilder.Entity<UserEmpInfo>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.UserParmsId).HasColumnName("UserParmsID");
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.Property(e => e.VideoId).HasColumnName("VideoID");

                entity.Property(e => e.Interlaced).HasColumnType("bit");

                entity.Property(e => e.Lcd)
                    .HasColumnName("LCD")
                    .HasColumnType("bit");

                entity.Property(e => e.Pattern1X).HasDefaultValueSql("0");

                entity.Property(e => e.Pattern1Y).HasDefaultValueSql("0");

                entity.Property(e => e.Pattern2X).HasDefaultValueSql("0");

                entity.Property(e => e.Pattern2Y).HasDefaultValueSql("0");

                entity.Property(e => e.Pattern3X).HasDefaultValueSql("0");

                entity.Property(e => e.Pattern3Y).HasDefaultValueSql("0");

                entity.Property(e => e.WideScreen).HasColumnType("bit");
            });

            modelBuilder.Entity<ViewFields>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DayView).HasColumnType("bit");

                entity.Property(e => e.Nm).HasMaxLength(254);

                entity.Property(e => e.UserParmsId).HasColumnName("UserParmsID");
            });

            modelBuilder.Entity<WaitingList>(entity =>
            {
                entity.HasKey(e => e.Wlid);

                entity.HasIndex(e => e.CustId)
                    .HasName("CustID");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("ServiceID");

                entity.HasIndex(e => e.StartDate)
                    .HasName("StartDate");

                entity.HasIndex(e => e.StartTm)
                    .HasName("StartTm");

                entity.Property(e => e.Wlid).HasColumnName("WLID");

                entity.Property(e => e.Adays).HasColumnName("ADays");

                entity.Property(e => e.AllDayTask).HasColumnType("bit");

                entity.Property(e => e.Bclr).HasColumnName("BClr");

                entity.Property(e => e.CustId)
                    .HasColumnName("CustID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Duration).HasDefaultValueSql("0");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("ServiceID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Services).HasMaxLength(254);

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.WaitingList)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("Reference17");
            });

            modelBuilder.Entity<WpasteBuffer>(entity =>
            {
                entity.HasKey(e => e.TranId);

                entity.ToTable("WPasteBuffer");

                entity.Property(e => e.TranId).HasColumnName("TranID");

                entity.Property(e => e.AllDayTask).HasColumnType("bit");

                entity.Property(e => e.Bclr).HasColumnName("BClr");

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.MlinkId).HasColumnName("MlinkID");

                entity.Property(e => e.Off)
                    .HasColumnName("OFF")
                    .HasColumnType("bit");

                entity.Property(e => e.RecurId).HasColumnName("RecurID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            });
        }
    }
}
