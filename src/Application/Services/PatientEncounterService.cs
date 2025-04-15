using Microsoft.EntityFrameworkCore;
using TeraLinkaCareApi.Core.Domain.Entities;
using TeraLinkaCareApi.Infrastructure.Persistence;

namespace TeraLinkaCareApi.Application.Services;

public class PatientEncounterService
{
    private readonly CRSDbContext _context;

    public PatientEncounterService(CRSDbContext context)
    {
        _context = context;
    }

    public async Task<List<A_PtEncounter>> GetPatientEncounterList(Guid clinicalUnitId)
    {
        var patientEncounterList =
            from pe in _context.A_PtEncounters
            join bed in _context.SysBeds on pe.BedLabel equals bed.BedLabel
            where pe.ClinicalUnitPuid == clinicalUnitId
            orderby bed.Sequence
            select pe;

        return await patientEncounterList.ToListAsync();
    }
}
