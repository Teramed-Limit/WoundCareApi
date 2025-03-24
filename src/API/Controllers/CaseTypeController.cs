using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoundCareApi.Persistence.Repository;
using WoundCareApi.Persistence.UnitOfWork;
using WoundCareApi.src.Core.Domain.CRS;
using WoundCareApi.src.Infrastructure.Persistence;

namespace WoundCareApi.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CaseTypeController : BaseApiController<CRS_CfgCaseType, CRSDbContext>
{
    private readonly ILogger<CaseTypeController> _logger;

    public CaseTypeController(
        ILogger<CaseTypeController> logger,
        IRepository<CRS_CfgCaseType, CRSDbContext> repository,
        IUnitOfWork unitOfWork
    )
        : base(repository, unitOfWork)
    {
        _logger = logger;
    }
}
