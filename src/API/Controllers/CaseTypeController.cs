using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoundCareApi.Core.Domain.Entities;
using WoundCareApi.Core.Domain.Interfaces;
using WoundCareApi.Core.Repository;
using WoundCareApi.Infrastructure.Persistence;
using WoundCareApi.Infrastructure.Persistence.UnitOfWork;
using WoundCareApi.Infrastructure.Persistence.UnitOfWork.Interfaces;

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
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
}
