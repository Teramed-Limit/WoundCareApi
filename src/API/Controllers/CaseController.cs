using Microsoft.AspNetCore.Mvc;
using WoundCareApi.Persistence.Repository;
using WoundCareApi.Persistence.UnitOfWork;
using WoundCareApi.src.Core.Domain.CRS;
using WoundCareApi.src.Infrastructure.Persistence;

namespace WoundCareApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CaseController : BaseApiController<CRS_Case, CRSDbContext>
{
    private readonly ILogger<CaseController> _logger;

    public CaseController(
        ILogger<CaseController> logger,
        IRepository<CRS_Case, CRSDbContext> repository,
        IUnitOfWork unitOfWork
    )
        : base(repository, unitOfWork)
    {
        _logger = logger;
    }

    [HttpGet("patientId/{patientId}/encounterId/{encounterId}/cases")]
    public async Task<ActionResult<IEnumerable<CRS_Case>>> GetByPatientIdAndEncounterId(
        string patientId,
        string encounterId
    )
    {
        var res = await _repository.GetAllAsync();

        var cases = await _repository.GetByConditionAsync(
            x => x.LIfeTimeNumber == patientId && x.EncounterNumber == encounterId
        );

        var groupedBodyPartCases = cases
            .OrderBy(x => x.CaseLocation ?? "")
            .GroupBy(x => x.CaseLocation)
            .ToDictionary(group => group.Key ?? "undefined", group => group.AsEnumerable());

        return Ok(groupedBodyPartCases);
    }
}
