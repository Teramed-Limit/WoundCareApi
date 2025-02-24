using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSPatientDataDB;

public partial class PtMedicationOrder
{
    public int poid { get; set; }

    public Guid PtEncounterPuid { get; set; }

    public string LifeTimeNumber { get; set; } = null!;

    public string? EncounterNumber { get; set; }

    public string? OrderCode { get; set; }

    public string? OrderName { get; set; }

    public DateTime? OrderBeginTime { get; set; }

    public DateTime? OrderDiscontinueTime { get; set; }

    public string? OrderDescription { get; set; }

    public string? OrderStatus { get; set; }

    public string? DrugGenericName { get; set; }

    public string? DrugBrandName { get; set; }

    public string? DrugATCCode { get; set; }

    public string? DrugCategory { get; set; }

    public decimal? Dose { get; set; }

    public string? DoseUnit { get; set; }

    public decimal? Rate { get; set; }

    public string? AdmitRoute { get; set; }

    public string? Frequency { get; set; }

    public string? SolutionName { get; set; }

    public decimal? TotalVolume { get; set; }

    public string? TotalUnit { get; set; }

    public string? OrderPhysicianId { get; set; }

    public string? OrderPhysicianName { get; set; }

    public string? OrderAcceptStatus { get; set; }

    public DateTime? OrderAcceptTime { get; set; }

    public string? OrderAcceptCareProviderId { get; set; }

    public string? OrderAcceptCareProviderName { get; set; }

    public DateTime? StoreTime { get; set; }

    public DateTime? LoadTime { get; set; }
}
