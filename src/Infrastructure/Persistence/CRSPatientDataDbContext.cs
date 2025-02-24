using Microsoft.EntityFrameworkCore;
using WoundCareApi.src.Core.Domain.CRSPatientDataDB;

namespace WoundCareApi.Infrastructure.Persistence;

public partial class CRSPatientDataDbContext : DbContext
{
    public CRSPatientDataDbContext()
    {
    }

    public CRSPatientDataDbContext(DbContextOptions<CRSPatientDataDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<APtTotalIOBalance> APtTotalIOBalances { get; set; }

    public virtual DbSet<DicomSeriesMap> DicomSeriesMaps { get; set; }

    public virtual DbSet<PtCase> PtCases { get; set; }

    public virtual DbSet<PtCaseRecord> PtCaseRecords { get; set; }

    public virtual DbSet<PtClinicalImage> PtClinicalImages { get; set; }

    public virtual DbSet<PtClinicalNote> PtClinicalNotes { get; set; }

    public virtual DbSet<PtCultureResult> PtCultureResults { get; set; }

    public virtual DbSet<PtDeviceDatum> PtDeviceData { get; set; }

    public virtual DbSet<PtGeneralIntake> PtGeneralIntakes { get; set; }

    public virtual DbSet<PtLabResult> PtLabResults { get; set; }

    public virtual DbSet<PtMedicationIntake> PtMedicationIntakes { get; set; }

    public virtual DbSet<PtMedicationOrder> PtMedicationOrders { get; set; }

    public virtual DbSet<PtPhysicialAssessment> PtPhysicialAssessments { get; set; }

    public virtual DbSet<PtScoring> PtScorings { get; set; }

    public virtual DbSet<PtSiteCare> PtSiteCares { get; set; }

    public virtual DbSet<PtSiteOutput> PtSiteOutputs { get; set; }

    public virtual DbSet<PtVentilation> PtVentilations { get; set; }

    public virtual DbSet<PtVitalsign> PtVitalsigns { get; set; }

    public virtual DbSet<PtWoundCare> PtWoundCares { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=CRSPatientDataDB1;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<APtTotalIOBalance>(entity =>
        {
            entity.HasKey(e => e.poid);

            entity.ToTable("APtTotalIOBalance");

            entity.Property(e => e.CareProviderId).HasMaxLength(50);
            entity.Property(e => e.CareProviderName).HasMaxLength(50);
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.IntakeDailyHourTotal).HasColumnType("numeric(28, 14)");
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(32);
            entity.Property(e => e.LoadTime).HasColumnType("datetime");
            entity.Property(e => e.NetBalance).HasColumnType("numeric(28, 14)");
            entity.Property(e => e.ObservationDateTime).HasColumnType("datetime");
            entity.Property(e => e.OutputDailyHourTotal).HasColumnType("numeric(28, 14)");
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<DicomSeriesMap>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("DicomSeriesMap");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.DicomSeriesDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DicomSeriesShiftDate).HasColumnType("datetime");
            entity.Property(e => e.DicomSeriesTime)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<PtCase>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("PtCase");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.CareProviderId).HasMaxLength(32);
            entity.Property(e => e.CaseBeginTime).HasColumnType("datetime");
            entity.Property(e => e.CaseCloseCareProviderName).HasMaxLength(32);
            entity.Property(e => e.CaseCloseStatus).HasMaxLength(64);
            entity.Property(e => e.CaseCloseTime).HasColumnType("datetime");
            entity.Property(e => e.CaseEntityId).HasMaxLength(32);
            entity.Property(e => e.CaseLocation).HasMaxLength(64);
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.LIfeTimeNumber).HasMaxLength(32);
            entity.Property(e => e.LoadTime).HasColumnType("datetime");
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<PtCaseRecord>(entity =>
        {
            entity.HasKey(e => e.Puid);

            entity.ToTable("PtCaseRecord");

            entity.Property(e => e.Puid).ValueGeneratedNever();
            entity.Property(e => e.CareProviderId).HasMaxLength(32);
            entity.Property(e => e.Comment).HasMaxLength(2000);
            entity.Property(e => e.FormData).HasMaxLength(4000);
            entity.Property(e => e.LoadTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationDateTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationShiftDate).HasColumnType("datetime");
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<PtClinicalImage>(entity =>
        {
            entity.HasKey(e => e.poid);

            entity.ToTable("PtClinicalImage");

            entity.Property(e => e.CareProviderId).HasMaxLength(50);
            entity.Property(e => e.CareProviderName).HasMaxLength(50);
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.ImageDetail).HasMaxLength(100);
            entity.Property(e => e.ImageTitle).HasMaxLength(100);
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(32);
            entity.Property(e => e.LoadTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationDateTime).HasColumnType("datetime");
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<PtClinicalNote>(entity =>
        {
            entity.HasKey(e => e.poid);

            entity.Property(e => e.AbnormalFlags).HasMaxLength(32);
            entity.Property(e => e.CareProviderId).HasMaxLength(50);
            entity.Property(e => e.CareProviderName).HasMaxLength(50);
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(32);
            entity.Property(e => e.LoadTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationCode).HasMaxLength(32);
            entity.Property(e => e.ObservationDateTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationLabel).HasMaxLength(32);
            entity.Property(e => e.ObservationSource).HasMaxLength(32);
            entity.Property(e => e.OrderCode).HasMaxLength(32);
            entity.Property(e => e.ResultStatus).HasMaxLength(32);
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
            entity.Property(e => e.ValueComment).HasMaxLength(64);
            entity.Property(e => e.ValueString).HasMaxLength(4000);
        });

        modelBuilder.Entity<PtCultureResult>(entity =>
        {
            entity.HasKey(e => e.poid);

            entity.ToTable("PtCultureResult");

            entity.Property(e => e.CareProviderId).HasMaxLength(50);
            entity.Property(e => e.CareProviderName).HasMaxLength(50);
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(32);
            entity.Property(e => e.LoadTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationCode).HasMaxLength(32);
            entity.Property(e => e.ObservationDateTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationLabel).HasMaxLength(32);
            entity.Property(e => e.ObservationSource).HasMaxLength(32);
            entity.Property(e => e.OrderCode).HasMaxLength(32);
            entity.Property(e => e.OrderDateTime).HasColumnType("datetime");
            entity.Property(e => e.ReceiptDateTime).HasColumnType("datetime");
            entity.Property(e => e.ResultStatus).HasMaxLength(32);
            entity.Property(e => e.Specimen).HasMaxLength(32);
            entity.Property(e => e.StainDescription).HasMaxLength(256);
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
            entity.Property(e => e.ValueString1).HasMaxLength(128);
            entity.Property(e => e.ValueString2).HasMaxLength(128);
            entity.Property(e => e.ValueString3).HasMaxLength(128);
        });

        modelBuilder.Entity<PtDeviceDatum>(entity =>
        {
            entity.HasKey(e => e.poid);

            entity.Property(e => e.AbnormalFlags).HasMaxLength(32);
            entity.Property(e => e.CareProviderId).HasMaxLength(50);
            entity.Property(e => e.CareProviderName).HasMaxLength(50);
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(32);
            entity.Property(e => e.LoadTime).HasColumnType("datetime");
            entity.Property(e => e.NormalRangeDescription).HasMaxLength(64);
            entity.Property(e => e.NormalRangeHigh).HasMaxLength(32);
            entity.Property(e => e.NormalRangeLow).HasMaxLength(32);
            entity.Property(e => e.ObservationCode).HasMaxLength(32);
            entity.Property(e => e.ObservationDateTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationLabel).HasMaxLength(32);
            entity.Property(e => e.ObservationSource).HasMaxLength(32);
            entity.Property(e => e.OrderCode).HasMaxLength(32);
            entity.Property(e => e.ResultStatus).HasMaxLength(32);
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
            entity.Property(e => e.ValueComment).HasMaxLength(64);
            entity.Property(e => e.ValueDateTime).HasColumnType("datetime");
            entity.Property(e => e.ValueMedCode).HasMaxLength(32);
            entity.Property(e => e.ValueNumber).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.ValueString).HasMaxLength(128);
            entity.Property(e => e.ValueUnitCode).HasMaxLength(32);
            entity.Property(e => e.ValueUnitLabel).HasMaxLength(32);
        });

        modelBuilder.Entity<PtGeneralIntake>(entity =>
        {
            entity.HasKey(e => e.poid);

            entity.ToTable("PtGeneralIntake");

            entity.Property(e => e.CareProviderId).HasMaxLength(50);
            entity.Property(e => e.CareProviderName).HasMaxLength(50);
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(32);
            entity.Property(e => e.LoadTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationDateTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationLabel).HasMaxLength(32);
            entity.Property(e => e.Rate).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
            entity.Property(e => e.ValueComment).HasMaxLength(64);
            entity.Property(e => e.VolumeIn).HasColumnType("decimal(28, 14)");
        });

        modelBuilder.Entity<PtLabResult>(entity =>
        {
            entity.HasKey(e => e.poid);

            entity.ToTable("PtLabResult");

            entity.Property(e => e.AbnormalFlags).HasMaxLength(32);
            entity.Property(e => e.CareProviderId).HasMaxLength(50);
            entity.Property(e => e.CareProviderName).HasMaxLength(50);
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(32);
            entity.Property(e => e.LoadTime).HasColumnType("datetime");
            entity.Property(e => e.NormalRangeDescription).HasMaxLength(64);
            entity.Property(e => e.NormalRangeHigh).HasMaxLength(32);
            entity.Property(e => e.NormalRangeLow).HasMaxLength(32);
            entity.Property(e => e.ObservationCode).HasMaxLength(32);
            entity.Property(e => e.ObservationDateTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationLabel).HasMaxLength(32);
            entity.Property(e => e.ObservationSource).HasMaxLength(32);
            entity.Property(e => e.OrderCode).HasMaxLength(32);
            entity.Property(e => e.ResultStatus).HasMaxLength(32);
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
            entity.Property(e => e.ValueComment).HasMaxLength(64);
            entity.Property(e => e.ValueDateTime).HasColumnType("datetime");
            entity.Property(e => e.ValueMedCode).HasMaxLength(32);
            entity.Property(e => e.ValueNumber).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.ValueString).HasMaxLength(128);
            entity.Property(e => e.ValueUnitCode).HasMaxLength(32);
            entity.Property(e => e.ValueUnitLabel).HasMaxLength(32);
        });

        modelBuilder.Entity<PtMedicationIntake>(entity =>
        {
            entity.HasKey(e => e.poid);

            entity.ToTable("PtMedicationIntake");

            entity.Property(e => e.CareProviderId).HasMaxLength(50);
            entity.Property(e => e.CareProviderName).HasMaxLength(50);
            entity.Property(e => e.Dose).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.DoseUnitLabel).HasMaxLength(32);
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(32);
            entity.Property(e => e.LoadTime).HasColumnType("datetime");
            entity.Property(e => e.MaterialLabel).HasMaxLength(64);
            entity.Property(e => e.ObservationDateTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationLabel).HasMaxLength(32);
            entity.Property(e => e.Rate).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
            entity.Property(e => e.ValueComment).HasMaxLength(64);
            entity.Property(e => e.VolumeIn).HasColumnType("decimal(28, 14)");
        });

        modelBuilder.Entity<PtMedicationOrder>(entity =>
        {
            entity.HasKey(e => e.poid);

            entity.ToTable("PtMedicationOrder");

            entity.Property(e => e.AdmitRoute).HasMaxLength(64);
            entity.Property(e => e.Dose).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.DoseUnit).HasMaxLength(64);
            entity.Property(e => e.DrugATCCode).HasMaxLength(64);
            entity.Property(e => e.DrugBrandName).HasMaxLength(64);
            entity.Property(e => e.DrugCategory).HasMaxLength(64);
            entity.Property(e => e.DrugGenericName).HasMaxLength(64);
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.Frequency).HasMaxLength(64);
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(32);
            entity.Property(e => e.LoadTime).HasColumnType("datetime");
            entity.Property(e => e.OrderAcceptCareProviderId).HasMaxLength(32);
            entity.Property(e => e.OrderAcceptCareProviderName).HasMaxLength(32);
            entity.Property(e => e.OrderAcceptStatus).HasMaxLength(32);
            entity.Property(e => e.OrderAcceptTime).HasColumnType("datetime");
            entity.Property(e => e.OrderBeginTime).HasColumnType("datetime");
            entity.Property(e => e.OrderCode).HasMaxLength(32);
            entity.Property(e => e.OrderDescription).HasMaxLength(128);
            entity.Property(e => e.OrderDiscontinueTime).HasColumnType("datetime");
            entity.Property(e => e.OrderName).HasMaxLength(64);
            entity.Property(e => e.OrderPhysicianId).HasMaxLength(32);
            entity.Property(e => e.OrderPhysicianName).HasMaxLength(32);
            entity.Property(e => e.OrderStatus).HasMaxLength(32);
            entity.Property(e => e.Rate).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.SolutionName).HasMaxLength(64);
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
            entity.Property(e => e.TotalUnit).HasMaxLength(32);
            entity.Property(e => e.TotalVolume).HasColumnType("decimal(28, 14)");
        });

        modelBuilder.Entity<PtPhysicialAssessment>(entity =>
        {
            entity.HasKey(e => e.poid);

            entity.ToTable("PtPhysicialAssessment");

            entity.Property(e => e.AbnormalFlags).HasMaxLength(32);
            entity.Property(e => e.CareProviderId).HasMaxLength(50);
            entity.Property(e => e.CareProviderName).HasMaxLength(50);
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(32);
            entity.Property(e => e.LoadTime).HasColumnType("datetime");
            entity.Property(e => e.NormalRangeDescription).HasMaxLength(64);
            entity.Property(e => e.NormalRangeHigh).HasMaxLength(32);
            entity.Property(e => e.NormalRangeLow).HasMaxLength(32);
            entity.Property(e => e.ObservationCode).HasMaxLength(32);
            entity.Property(e => e.ObservationDateTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationLabel).HasMaxLength(32);
            entity.Property(e => e.ObservationSource).HasMaxLength(32);
            entity.Property(e => e.OrderCode).HasMaxLength(32);
            entity.Property(e => e.ResultStatus).HasMaxLength(32);
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
            entity.Property(e => e.ValueComment).HasMaxLength(64);
            entity.Property(e => e.ValueDateTime).HasColumnType("datetime");
            entity.Property(e => e.ValueMedCode).HasMaxLength(32);
            entity.Property(e => e.ValueNumber).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.ValueString).HasMaxLength(128);
            entity.Property(e => e.ValueUnitCode).HasMaxLength(32);
            entity.Property(e => e.ValueUnitLabel).HasMaxLength(32);
        });

        modelBuilder.Entity<PtScoring>(entity =>
        {
            entity.HasKey(e => e.poid);

            entity.ToTable("PtScoring");

            entity.Property(e => e.AbnormalFlags).HasMaxLength(32);
            entity.Property(e => e.CareProviderId).HasMaxLength(50);
            entity.Property(e => e.CareProviderName).HasMaxLength(50);
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(32);
            entity.Property(e => e.LoadTime).HasColumnType("datetime");
            entity.Property(e => e.NormalRangeDescription).HasMaxLength(64);
            entity.Property(e => e.NormalRangeHigh).HasMaxLength(32);
            entity.Property(e => e.NormalRangeLow).HasMaxLength(32);
            entity.Property(e => e.ObservationCode).HasMaxLength(32);
            entity.Property(e => e.ObservationDateTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationLabel).HasMaxLength(32);
            entity.Property(e => e.ObservationSource).HasMaxLength(32);
            entity.Property(e => e.OrderCode).HasMaxLength(32);
            entity.Property(e => e.ResultStatus).HasMaxLength(32);
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
            entity.Property(e => e.ValueComment).HasMaxLength(64);
            entity.Property(e => e.ValueDateTime).HasColumnType("datetime");
            entity.Property(e => e.ValueMedCode).HasMaxLength(32);
            entity.Property(e => e.ValueNumber).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.ValueString).HasMaxLength(128);
            entity.Property(e => e.ValueUnitCode).HasMaxLength(32);
            entity.Property(e => e.ValueUnitLabel).HasMaxLength(32);
        });

        modelBuilder.Entity<PtSiteCare>(entity =>
        {
            entity.HasKey(e => e.poid);

            entity.ToTable("PtSiteCare");

            entity.Property(e => e.BundleComplianceComment).HasMaxLength(64);
            entity.Property(e => e.CareAction).HasMaxLength(32);
            entity.Property(e => e.CareProviderId).HasMaxLength(50);
            entity.Property(e => e.CareProviderName).HasMaxLength(50);
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.FixDeepth).HasMaxLength(32);
            entity.Property(e => e.InsertDateTime).HasColumnType("datetime");
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(32);
            entity.Property(e => e.LineSize).HasMaxLength(32);
            entity.Property(e => e.LoadTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationDateTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationLabel).HasMaxLength(32);
            entity.Property(e => e.RemoveDateTime).HasColumnType("datetime");
            entity.Property(e => e.SkinStatus).HasMaxLength(32);
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
            entity.Property(e => e.ValueComment).HasMaxLength(64);
            entity.Property(e => e.VolumeOutput).HasColumnType("decimal(28, 14)");
        });

        modelBuilder.Entity<PtSiteOutput>(entity =>
        {
            entity.HasKey(e => e.poid);

            entity.ToTable("PtSiteOutput");

            entity.Property(e => e.CareProviderId).HasMaxLength(50);
            entity.Property(e => e.CareProviderName).HasMaxLength(50);
            entity.Property(e => e.CountOfOutput).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(32);
            entity.Property(e => e.LoadTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationDateTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationLabel).HasMaxLength(32);
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
            entity.Property(e => e.ValueComment).HasMaxLength(64);
            entity.Property(e => e.ValueUnitCode).HasMaxLength(32);
            entity.Property(e => e.ValueUnitLabel).HasMaxLength(32);
            entity.Property(e => e.VolumeOutput).HasColumnType("decimal(28, 14)");
        });

        modelBuilder.Entity<PtVentilation>(entity =>
        {
            entity.HasKey(e => e.poid);

            entity.ToTable("PtVentilation");

            entity.Property(e => e.AbnormalFlags).HasMaxLength(32);
            entity.Property(e => e.CareProviderId).HasMaxLength(50);
            entity.Property(e => e.CareProviderName).HasMaxLength(50);
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(32);
            entity.Property(e => e.LoadTime).HasColumnType("datetime");
            entity.Property(e => e.NormalRangeDescription).HasMaxLength(64);
            entity.Property(e => e.NormalRangeHigh).HasMaxLength(32);
            entity.Property(e => e.NormalRangeLow).HasMaxLength(32);
            entity.Property(e => e.ObservationCode).HasMaxLength(32);
            entity.Property(e => e.ObservationDateTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationLabel).HasMaxLength(32);
            entity.Property(e => e.ObservationSource).HasMaxLength(32);
            entity.Property(e => e.OrderCode).HasMaxLength(32);
            entity.Property(e => e.ResultStatus).HasMaxLength(32);
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
            entity.Property(e => e.ValueComment).HasMaxLength(64);
            entity.Property(e => e.ValueDateTime).HasColumnType("datetime");
            entity.Property(e => e.ValueMedCode).HasMaxLength(32);
            entity.Property(e => e.ValueNumber).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.ValueString).HasMaxLength(128);
            entity.Property(e => e.ValueUnitCode).HasMaxLength(32);
            entity.Property(e => e.ValueUnitLabel).HasMaxLength(32);
        });

        modelBuilder.Entity<PtVitalsign>(entity =>
        {
            entity.HasKey(e => e.poid);

            entity.Property(e => e.ABPd).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.ABPm).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.ABPs).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.BodyTemp).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.CPP).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.CVPm).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.CareProviderId).HasMaxLength(50);
            entity.Property(e => e.CareProviderName).HasMaxLength(50);
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.GCS).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.HR).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.ICP).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(32);
            entity.Property(e => e.LoadTime).HasColumnType("datetime");
            entity.Property(e => e.NBPd).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.NBPm).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.NBPs).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.ObservationDateTime).HasColumnType("datetime");
            entity.Property(e => e.PVC).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.RR).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.SpO2).HasColumnType("decimal(28, 14)");
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<PtWoundCare>(entity =>
        {
            entity.HasKey(e => e.poid);

            entity.ToTable("PtWoundCare");

            entity.Property(e => e.BodyLocationDescription).HasMaxLength(64);
            entity.Property(e => e.CareAction).HasMaxLength(32);
            entity.Property(e => e.CareProviderId).HasMaxLength(50);
            entity.Property(e => e.CareProviderName).HasMaxLength(50);
            entity.Property(e => e.CaseBeginDateTime).HasColumnType("datetime");
            entity.Property(e => e.CaseCloseCareProviderName).HasMaxLength(64);
            entity.Property(e => e.CaseCloseDateTime).HasColumnType("datetime");
            entity.Property(e => e.CaseCloseStatus).HasMaxLength(64);
            entity.Property(e => e.CaseId).HasMaxLength(32);
            entity.Property(e => e.Comment).HasMaxLength(64);
            entity.Property(e => e.DressingChange).HasMaxLength(32);
            entity.Property(e => e.EncounterNumber).HasMaxLength(32);
            entity.Property(e => e.LifeTimeNumber).HasMaxLength(32);
            entity.Property(e => e.LoadTime).HasColumnType("datetime");
            entity.Property(e => e.ObservationDateTime).HasColumnType("datetime");
            entity.Property(e => e.QuantitySecretions).HasMaxLength(32);
            entity.Property(e => e.SmellSecretions).HasMaxLength(32);
            entity.Property(e => e.StoreTime).HasColumnType("datetime");
            entity.Property(e => e.WoundColor).HasMaxLength(32);
            entity.Property(e => e.WoundDeepth).HasMaxLength(32);
            entity.Property(e => e.WoundLevel).HasMaxLength(32);
            entity.Property(e => e.WoundSize).HasMaxLength(32);
            entity.Property(e => e.WoundSource).HasMaxLength(32);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
