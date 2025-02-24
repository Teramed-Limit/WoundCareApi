using Microsoft.AspNetCore.Mvc;
using WoundCareApi.Persistence.Repository;
using WoundCareApi.Persistence.UnitOfWork;
using WoundCareApi.src.Core.Domain.CRS;
using WoundCareApi.src.Infrastructure.Persistence;

namespace WoundCareApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : BaseApiController<CRS_A_PtEncounter, CRSDbContext>
{
    private readonly ILogger<PatientController> _logger;

    public PatientController(
        ILogger<PatientController> logger,
        IRepository<CRS_A_PtEncounter, CRSDbContext> repository,
        IUnitOfWork unitOfWork
    )
        : base(repository, unitOfWork)
    {
        _logger = logger;
    }

    [HttpGet("patientId/{patientId}")]
    public async Task<ActionResult<CRS_PtPatient>> GetByPatientId(string patientId)
    {
        var patient = await _repository.GetByConditionAsync(x => x.LifeTimeNumber == patientId);
        var result = patient.First();
        return Ok(result);
    }
}
