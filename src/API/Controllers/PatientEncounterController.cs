using Microsoft.AspNetCore.Mvc;
using WoundCareApi.Persistence.Repository;
using WoundCareApi.src.Core.Domain.CRS;
using WoundCareApi.src.Infrastructure.Persistence;

namespace WoundCareApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientEncounterController : ControllerBase
{
    private readonly ILogger<PatientEncounterController> _logger;
    private readonly IRepository<CRS_A_PtEncounter, CRSDbContext> _repository;

    public PatientEncounterController(
        ILogger<PatientEncounterController> logger,
        IRepository<CRS_A_PtEncounter, CRSDbContext> repository
    )
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("clinicalUnitId/{clinicalUnitId}")]
    public async Task<ActionResult<IEnumerable<CRS_A_PtEncounter>>> Get(
        string clinicalUnitId
    )
    {
        var result = await _repository.GetByConditionAsync(
            (x) => x.ClinicalUnitPuid == Guid.Parse(clinicalUnitId),
            orderBy: x => x.BedLabel
        );

        return Ok(result);
    }
}
