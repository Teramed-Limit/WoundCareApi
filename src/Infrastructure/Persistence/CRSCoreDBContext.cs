using Microsoft.EntityFrameworkCore;
using WoundCareApi.Core.Domain.CRSCoreDB;
using WoundCareApi.src.Core.Domain.CRSCoreDB;

namespace WoundCareApi.Infrastructure.Persistence;

public partial class CRSCoreDBContext : DbContext
{
    public CRSCoreDBContext()
    {
    }

    public CRSCoreDBContext(DbContextOptions<CRSCoreDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<A_PtEncounter> A_PtEncounters { get; set; }

    public virtual DbSet<A_PtEncounterGridDatum> A_PtEncounterGridData { get; set; }

    public virtual DbSet<CfgAttribute> CfgAttributes { get; set; }

    public virtual DbSet<CfgBedGridField> CfgBedGridFields { get; set; }

    public virtual DbSet<CfgBodyLocation> CfgBodyLocations { get; set; }

    public virtual DbSet<CfgBodySystem> CfgBodySystems { get; set; }

    public virtual DbSet<CfgBodySystemObservation> CfgBodySystemObservations { get; set; }

    public virtual DbSet<CfgBodySystemSite> CfgBodySystemSites { get; set; }

    public virtual DbSet<CfgCaseType> CfgCaseTypes { get; set; }

    public virtual DbSet<CfgChartLimitRangeSetting> CfgChartLimitRangeSettings { get; set; }

    public virtual DbSet<CfgDataBlock> CfgDataBlocks { get; set; }

    public virtual DbSet<CfgFunctionBlock> CfgFunctionBlocks { get; set; }

    public virtual DbSet<CfgFunctionBlockDataBlock> CfgFunctionBlockDataBlocks { get; set; }

    public virtual DbSet<CfgFunctionBlockTabArea> CfgFunctionBlockTabAreas { get; set; }

    public virtual DbSet<CfgImageLibrary> CfgImageLibraries { get; set; }

    public virtual DbSet<CfgObservation> CfgObservations { get; set; }

    public virtual DbSet<CfgPatientView> CfgPatientViews { get; set; }

    public virtual DbSet<CfgPatientViewFunctionBlock> CfgPatientViewFunctionBlocks { get; set; }

    public virtual DbSet<CfgSite> CfgSites { get; set; }

    public virtual DbSet<CfgUnitCard> CfgUnitCards { get; set; }

    public virtual DbSet<CfgUnitCardField> CfgUnitCardFields { get; set; }

    public virtual DbSet<CfgUnitView> CfgUnitViews { get; set; }

    public virtual DbSet<CfgUnitViewBedCardField> CfgUnitViewBedCardFields { get; set; }

    public virtual DbSet<CfgUnitViewBedCardFieldDisplayRule> CfgUnitViewBedCardFieldDisplayRules { get; set; }

    public virtual DbSet<CfgUnitViewBedGridField> CfgUnitViewBedGridFields { get; set; }

    public virtual DbSet<CfgUnitViewBedGridFieldDisplayRule> CfgUnitViewBedGridFieldDisplayRules { get; set; }

    public virtual DbSet<CfgUnitViewUnitCardField> CfgUnitViewUnitCardFields { get; set; }

    public virtual DbSet<CfgWoundType> CfgWoundTypes { get; set; }

    public virtual DbSet<ClinicalUnitStatisticsDatum> ClinicalUnitStatisticsData { get; set; }

    public virtual DbSet<PtEncounter> PtEncounters { get; set; }

    public virtual DbSet<PtEncounterDetail> PtEncounterDetails { get; set; }

    public virtual DbSet<PtEncounterLocationStay> PtEncounterLocationStays { get; set; }

    public virtual DbSet<PtPatient> PtPatients { get; set; }

    public virtual DbSet<SysBed> SysBeds { get; set; }

    public virtual DbSet<SysClinicalUnit> SysClinicalUnits { get; set; }

    public virtual DbSet<SysClinicalUnitShift> SysClinicalUnitShifts { get; set; }

    public virtual DbSet<SysFunction> SysFunctions { get; set; }

    public virtual DbSet<SysHostDB> SysHostDBs { get; set; }

    public virtual DbSet<SysInstitution> SysInstitutions { get; set; }

    public virtual DbSet<SysRole> SysRoles { get; set; }

    public virtual DbSet<SysRoleFunction> SysRoleFunctions { get; set; }

    public virtual DbSet<SysUser> SysUsers { get; set; }

    public virtual DbSet<SysUserClinicalUnit> SysUserClinicalUnits { get; set; }

    public virtual DbSet<SysUserRole> SysUserRoles { get; set; }

    public virtual DbSet<vwPatientBedLocationCurrent> vwPatientBedLocationCurrents { get; set; }

    public virtual DbSet<xCfgPatientViewLayout> xCfgPatientViewLayouts { get; set; }

    public virtual DbSet<xCfgUnitViewBedArea> xCfgUnitViewBedAreas { get; set; }

    public virtual DbSet<xCfgViewerLayout> xCfgViewerLayouts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=CRSCoreDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<A_PtEncounter>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("A_PtEncounter");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.AttendingPhysician).HasMaxLength(64);
            entity.Property(e => e.BedLabel).HasMaxLength(32);
            entity.Property(e => e.ClinicalService).HasMaxLength(50);
            entity.Property(e => e.ClinicalUnitLabel).HasMaxLength(32);
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.EncounterNumber).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(50);
            entity.Property(e => e.NationalId).HasMaxLength(50);
            entity.Property(e => e.SysInTime).HasColumnType("datetime");
            entity.Property(e => e.SysOutTime).HasColumnType("datetime");
            entity.Property(e => e.TransferInTime).HasColumnType("datetime");
            entity.Property(e => e.TransferOutStatus).HasMaxLength(32);
            entity.Property(e => e.TransferOutTime).HasColumnType("datetime");
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<A_PtEncounterGridDatum>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.ABPd).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.ABPm).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.ABPs).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.APACHEII).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.AttendingPhysician).HasMaxLength(50);
            entity.Property(e => e.BT).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.CNSState).HasMaxLength(64);
            entity.Property(e => e.CardioState).HasMaxLength(64);
            entity.Property(e => e.ClinicalService).HasMaxLength(50);
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Diagnosis).HasMaxLength(2500);
            entity.Property(e => e.EncounterNumber).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.GCS).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.GIState).HasMaxLength(64);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.HR).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.HemoState).HasMaxLength(64);
            entity.Property(e => e.LOS).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(50);
            entity.Property(e => e.NBPd).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.NBPm).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.NBPs).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.NationalId).HasMaxLength(50);
            entity.Property(e => e.OtherNotification).HasMaxLength(128);
            entity.Property(e => e.RR).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.RenalState).HasMaxLength(64);
            entity.Property(e => e.RespState).HasMaxLength(64);
            entity.Property(e => e.Si).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.SpO2).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            entity.Property(e => e.VentilatorMode).HasMaxLength(50);
        });

        modelBuilder.Entity<CfgAttribute>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgAttribute");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.AttributeLongLabel).HasMaxLength(64);
            entity.Property(e => e.AttributeShortLabel).HasMaxLength(32);
            entity.Property(e => e.Description).HasMaxLength(64);
            entity.Property(e => e.GlobalMedCode).HasMaxLength(32);
            entity.Property(e => e.ValueType).HasMaxLength(32);
        });

        modelBuilder.Entity<CfgBedGridField>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgBedGridField");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.FieldLongLabel).HasMaxLength(64);
            entity.Property(e => e.FieldShortLabel).HasMaxLength(32);
            entity.Property(e => e.FieldValueType).HasMaxLength(32);
            entity.Property(e => e.SourceColumnName).HasMaxLength(32);
        });

        modelBuilder.Entity<CfgBodyLocation>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgBodyLocation");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.LongLabel).HasMaxLength(64);
            entity.Property(e => e.NISCategory).HasMaxLength(32);
            entity.Property(e => e.NISLocationId).HasMaxLength(32);
            entity.Property(e => e.NISLocationLabel).HasMaxLength(32);
            entity.Property(e => e.ShortLabel).HasMaxLength(32);
        });

        modelBuilder.Entity<CfgBodySystem>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgBodySystem");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.BodySystemLongLabel).HasMaxLength(64);
            entity.Property(e => e.BodySystemShortLabel).HasMaxLength(32);
            entity.Property(e => e.Description).HasMaxLength(64);
            entity.Property(e => e.GlobalMedCode).HasMaxLength(32);
        });

        modelBuilder.Entity<CfgBodySystemObservation>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgBodySystemObservation");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.AlternativeLabel).HasMaxLength(64);
        });

        modelBuilder.Entity<CfgBodySystemSite>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgBodySystemSite");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.AlternativeLabel).HasMaxLength(64);
        });

        modelBuilder.Entity<CfgCaseType>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgCaseType");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.CaseTypeCategory).HasMaxLength(32);
            entity.Property(e => e.CaseTypeLongLabel).HasMaxLength(32);
            entity.Property(e => e.CaseTypeShortLabel).HasMaxLength(32);
        });

        modelBuilder.Entity<CfgChartLimitRangeSetting>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgChartLimitRangeSetting");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.ChartSeriesName).HasMaxLength(50);
            entity.Property(e => e.CriticalHighLimit).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.CriticalLowLimit).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.HighLimit).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.LowLimit).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.SourceDataSetName).HasMaxLength(50);
            entity.Property(e => e.YScaleHigh).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.YScaleLow).HasColumnType("decimal(18, 5)");
        });

        modelBuilder.Entity<CfgDataBlock>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgDataBlock");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.DataBlockForm).HasMaxLength(32);
            entity.Property(e => e.DataBlockLabel).HasMaxLength(32);
            entity.Property(e => e.DataBlockTitle).HasMaxLength(32);
            entity.Property(e => e.Description).HasMaxLength(128);
        });

        modelBuilder.Entity<CfgFunctionBlock>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgFunctionBlock");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(128);
            entity.Property(e => e.FunctionBlockLabel).HasMaxLength(32);
            entity.Property(e => e.FunctionBlockTitle).HasMaxLength(32);
            entity.Property(e => e.TabTitleList).HasMaxLength(128);
        });

        modelBuilder.Entity<CfgFunctionBlockDataBlock>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgFunctionBlockDataBlock");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.DataBlockParameterList).HasMaxLength(256);
        });

        modelBuilder.Entity<CfgFunctionBlockTabArea>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgFunctionBlockTabArea");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(128);
            entity.Property(e => e.TabTitle).HasMaxLength(32);
        });

        modelBuilder.Entity<CfgImageLibrary>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgImageLibrary");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(128);
            entity.Property(e => e.ImageFullFileName).HasMaxLength(512);
            entity.Property(e => e.ImageLabel).HasMaxLength(64);
        });

        modelBuilder.Entity<CfgObservation>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgObservation");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.AttributeLongLabel).HasMaxLength(64);
            entity.Property(e => e.AttributeShortLabel).HasMaxLength(32);
            entity.Property(e => e.Description).HasMaxLength(64);
            entity.Property(e => e.GlobalMedCode).HasMaxLength(32);
            entity.Property(e => e.ObservationCategory).HasMaxLength(64);
            entity.Property(e => e.ObservationLongLabel).HasMaxLength(64);
            entity.Property(e => e.ObservationShortLabel).HasMaxLength(32);
            entity.Property(e => e.SourceDataSetName).HasMaxLength(64);
            entity.Property(e => e.ValueType).HasMaxLength(32);
        });

        modelBuilder.Entity<CfgPatientView>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CfgPatientView");

            entity.Property(e => e.Description).HasMaxLength(128);
            entity.Property(e => e.PatientViewForm).HasMaxLength(32);
            entity.Property(e => e.PatientViewLabel).HasMaxLength(32);
        });

        modelBuilder.Entity<CfgPatientViewFunctionBlock>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgPatientViewFunctionBlock");

            entity.Property(e => e.Puid).ValueGeneratedNever();
        });

        modelBuilder.Entity<CfgSite>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgSite");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.SiteCategory).HasMaxLength(64);
            entity.Property(e => e.SiteDisplayLabel).HasMaxLength(64);
            entity.Property(e => e.SiteMaterialTag).HasMaxLength(64);
        });

        modelBuilder.Entity<CfgUnitCard>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgUnitCard");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.CardCategory).HasMaxLength(32);
            entity.Property(e => e.CardDispalyThemeName).HasMaxLength(32);
            entity.Property(e => e.CardForm).HasMaxLength(32);
            entity.Property(e => e.ChartType).HasMaxLength(32);
            entity.Property(e => e.Description).HasMaxLength(128);
        });

        modelBuilder.Entity<CfgUnitCardField>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgUnitCardField");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.FieldLongLabel).HasMaxLength(64);
            entity.Property(e => e.FieldShortLabel).HasMaxLength(32);
            entity.Property(e => e.FieldValueType).HasMaxLength(32);
            entity.Property(e => e.SourceColumnName).HasMaxLength(32);
        });

        modelBuilder.Entity<CfgUnitView>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgUnitView");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(128);
            entity.Property(e => e.UnitViewForm).HasMaxLength(32);
            entity.Property(e => e.UnitViewLabel).HasMaxLength(32);
        });

        modelBuilder.Entity<CfgUnitViewBedCardField>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgUnitViewBedCardField");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.FieldLinkURL).HasMaxLength(256);
        });

        modelBuilder.Entity<CfgUnitViewBedCardFieldDisplayRule>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgUnitViewBedCardFieldDisplayRule");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.BackgroundColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BorderColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FontBold)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FontColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FontSize)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LightColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RuleExpression).HasMaxLength(256);
        });

        modelBuilder.Entity<CfgUnitViewBedGridField>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgUnitViewBedGridField");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.FieldLinkURL).HasMaxLength(256);
        });

        modelBuilder.Entity<CfgUnitViewBedGridFieldDisplayRule>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgUnitViewBedGridFieldDisplayRule");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.BackgroundColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BorderColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FontBold)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FontColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FontSize)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LightColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RuleExpression).HasMaxLength(256);
        });

        modelBuilder.Entity<CfgUnitViewUnitCardField>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgUnitViewUnitCardField");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.DefaultValueBackColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DefaultValueFontColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DisplayTitle).HasMaxLength(32);
        });

        modelBuilder.Entity<CfgWoundType>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("CfgWoundType");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.WoundTypeCategory).HasMaxLength(64);
            entity.Property(e => e.WoundTypeDisplayLabel).HasMaxLength(64);
        });

        modelBuilder.Entity<ClinicalUnitStatisticsDatum>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.DataTimeBegin).HasColumnType("datetime");
            entity.Property(e => e.DataTimeEnd).HasColumnType("datetime");
            entity.Property(e => e.SourceColumnName).HasMaxLength(32);
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            entity.Property(e => e.ValueDateTime).HasColumnType("datetime");
            entity.Property(e => e.ValueNumber).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.ValueString).HasMaxLength(64);
        });

        modelBuilder.Entity<PtEncounter>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("PtEncounter");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<PtEncounterDetail>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("PtEncounterDetail");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            entity.Property(e => e.ValueDateTime).HasColumnType("datetime");
            entity.Property(e => e.ValueNumber).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.ValueString).HasMaxLength(64);
        });

        modelBuilder.Entity<PtEncounterLocationStay>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("PtEncounterLocationStay");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.BedLabel).HasMaxLength(32);
            entity.Property(e => e.SysInTime).HasColumnType("datetime");
            entity.Property(e => e.SysOutTime).HasColumnType("datetime");
            entity.Property(e => e.TransferInTime).HasColumnType("datetime");
            entity.Property(e => e.TransferOutStatus).HasMaxLength(32);
            entity.Property(e => e.TransferOutTime).HasColumnType("datetime");
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            entity.Property(e => e.WardLabel).HasMaxLength(32);
        });

        modelBuilder.Entity<PtPatient>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("PtPatient");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(50);
            entity.Property(e => e.NationalId).HasMaxLength(50);
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<SysBed>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("SysBed");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.BedLabel).HasMaxLength(32);
            entity.Property(e => e.BedLabelHIS).HasMaxLength(32);
            entity.Property(e => e.BedType).HasMaxLength(32);
            entity.Property(e => e.RoomLabel).HasMaxLength(32);
        });

        modelBuilder.Entity<SysClinicalUnit>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("SysClinicalUnit");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.BedZoneList).HasMaxLength(128);
            entity.Property(e => e.DisplayLabel).HasMaxLength(32);
            entity.Property(e => e.HISCode).HasMaxLength(32);
            entity.Property(e => e.Location).HasMaxLength(32);
        });

        modelBuilder.Entity<SysClinicalUnitShift>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("SysClinicalUnitShift");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.ShiftLongLabel).HasMaxLength(32);
            entity.Property(e => e.ShiftShortLabel).HasMaxLength(32);
        });

        modelBuilder.Entity<SysFunction>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("SysFunction");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(128);
            entity.Property(e => e.FunctionCode).HasMaxLength(32);
            entity.Property(e => e.FunctionGroup).HasMaxLength(32);
            entity.Property(e => e.FunctionLabel).HasMaxLength(32);
            entity.Property(e => e.FunctionType).HasMaxLength(32);
        });

        modelBuilder.Entity<SysHostDB>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("SysHostDB");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");
            entity.Property(e => e.DBName).HasMaxLength(50);
            entity.Property(e => e.HostName).HasMaxLength(50);
        });

        modelBuilder.Entity<SysInstitution>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("SysInstitution");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(128);
            entity.Property(e => e.InstitutionAddress).HasMaxLength(64);
            entity.Property(e => e.InstitutionLongLabel).HasMaxLength(64);
            entity.Property(e => e.InstitutionPhoneNumber).HasMaxLength(64);
            entity.Property(e => e.InstitutionShortLabel).HasMaxLength(32);
            entity.Property(e => e.InstitutionType).HasMaxLength(64);
        });

        modelBuilder.Entity<SysRole>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("SysRole");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.RoleDescription).HasMaxLength(128);
            entity.Property(e => e.RoleLabel).HasMaxLength(64);
        });

        modelBuilder.Entity<SysRoleFunction>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("SysRoleFunction");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(50);
        });

        modelBuilder.Entity<SysUser>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("SysUser");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(128);
            entity.Property(e => e.Email).HasMaxLength(64);
            entity.Property(e => e.FirstName).HasMaxLength(64);
            entity.Property(e => e.JobTitle).HasMaxLength(64);
            entity.Property(e => e.LastName).HasMaxLength(64);
            entity.Property(e => e.NationalID).HasMaxLength(64);
            entity.Property(e => e.PhoneNumber1).HasMaxLength(64);
            entity.Property(e => e.PhoneNumber2).HasMaxLength(64);
            entity.Property(e => e.UserDomainName).HasMaxLength(64);
            entity.Property(e => e.UserEynPwd).HasMaxLength(64);
            entity.Property(e => e.UserId).HasMaxLength(64);
        });

        modelBuilder.Entity<SysUserClinicalUnit>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SysUserClinicalUnit");
        });

        modelBuilder.Entity<SysUserRole>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("SysUserRole");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(50);
        });

        modelBuilder.Entity<vwPatientBedLocationCurrent>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwPatientBedLocationCurrent");

            entity.Property(e => e.BedLabel).HasMaxLength(32);
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(50);
            entity.Property(e => e.NationalId).HasMaxLength(50);
            entity.Property(e => e.SysInTime).HasColumnType("datetime");
            entity.Property(e => e.SysOutTime).HasColumnType("datetime");
            entity.Property(e => e.TransferInTime).HasColumnType("datetime");
            entity.Property(e => e.TransferOutStatus).HasMaxLength(32);
            entity.Property(e => e.TransferOutTime).HasColumnType("datetime");
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            entity.Property(e => e.WardLabel).HasMaxLength(32);
        });

        modelBuilder.Entity<xCfgPatientViewLayout>(entity =>
        {
            entity.HasKey(e => e.Puid).HasName("PK_CfgPatientViewLayout");

            entity.ToTable("xCfgPatientViewLayout");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.LayoutLabel).HasMaxLength(32);
        });

        modelBuilder.Entity<xCfgUnitViewBedArea>(entity =>
        {
            entity.HasKey(e => e.Puid).HasName("PK_CfgUnitViewBedArea");

            entity.ToTable("xCfgUnitViewBedArea");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.BedAreaCode).HasMaxLength(32);
            entity.Property(e => e.BedAreaLabel).HasMaxLength(32);
            entity.Property(e => e.Description).HasMaxLength(128);
        });

        modelBuilder.Entity<xCfgViewerLayout>(entity =>
        {
            entity.HasKey(e => e.Puid).HasName("PK_CfgViewerLayout");

            entity.ToTable("xCfgViewerLayout");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.LayoutLabel).HasMaxLength(32);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
