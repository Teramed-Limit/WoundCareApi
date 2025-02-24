using Microsoft.AspNetCore.Mvc;
using WoundCareApi.Core.Domain.CRSCoreDB;
using WoundCareApi.Infrastructure.Persistence;
using WoundCareApi.Persistence.Repository;
using WoundCareApi.src.Core.Domain.CRSCoreDB;

namespace WoundCareApi.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientEncounterController : ControllerBase
{
    private readonly ILogger<PatientEncounterController> _logger;
    private readonly IRepository<vwPatientBedLocationCurrent, CRSCoreDBContext> _repository;

    public PatientEncounterController(
        ILogger<PatientEncounterController> logger,
        IRepository<vwPatientBedLocationCurrent, CRSCoreDBContext> repository
    )
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("clinicalUnit/{clinicalUnit}")]
    public async Task<ActionResult<IEnumerable<vwPatientBedLocationCurrent>>> Get(
        string clinicalUnit
    )
    {
        return Ok(await _repository.GetByConditionAsync((x) => x.WardLabel == clinicalUnit));
    }
}
