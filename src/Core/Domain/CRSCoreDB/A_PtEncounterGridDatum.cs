using System;
using System.Collections.Generic;

namespace WoundCareApi.src.Core.Domain.CRSCoreDB;

public partial class A_PtEncounterGridDatum
{
    public Guid Puid { get; set; }

    public Guid? PatientPuid { get; set; }

    public Guid? PtEncounterPuid { get; set; }

    public string? LifeTimeNumber { get; set; }

    public string? EncounterNumber { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? NationalId { get; set; }

    public string? ClinicalService { get; set; }

    public string? AttendingPhysician { get; set; }

    public string? Diagnosis { get; set; }

    public decimal? GCS { get; set; }

    public decimal? APACHEII { get; set; }

    public decimal? LOS { get; set; }

    public decimal? BT { get; set; }

    public decimal? HR { get; set; }

    public decimal? RR { get; set; }

    public decimal? ABPs { get; set; }

    public decimal? ABPd { get; set; }

    public decimal? ABPm { get; set; }

    public decimal? NBPs { get; set; }

    public decimal? NBPd { get; set; }

    public decimal? NBPm { get; set; }

    public decimal? SpO2 { get; set; }

    public decimal? Si { get; set; }

    public string? VentilatorMode { get; set; }

    public int? DurationEndo { get; set; }

    public int? DurationFoley { get; set; }

    public int? DurationCVC { get; set; }

    public int? Isolation { get; set; }

    public int? CPR { get; set; }

    public int? OP { get; set; }

    public int? WithDraw { get; set; }

    public int? WaitingICU { get; set; }

    public int? WaitingGW { get; set; }

    public string? CNSState { get; set; }

    public string? RespState { get; set; }

    public string? CardioState { get; set; }

    public string? RenalState { get; set; }

    public string? HemoState { get; set; }

    public string? GIState { get; set; }

    public string? OtherNotification { get; set; }

    public DateTime? UpdateTime { get; set; }
}
