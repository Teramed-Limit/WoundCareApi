using Microsoft.AspNetCore.Mvc;
using WoundCareApi.Persistence.Repository;
using WoundCareApi.Persistence.UnitOfWork;
using WoundCareApi.src.Core.Domain.CRS;
using WoundCareApi.src.Infrastructure.Persistence;

namespace WoundCareApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WoundTypeController : BaseApiController<CRS_CfgCaseType, CRSDbContext>
{
    private readonly ILogger<WoundTypeController> _logger;

    public WoundTypeController(
        ILogger<WoundTypeController> logger,
        IRepository<CRS_CfgCaseType, CRSDbContext> repository,
        IUnitOfWork unitOfWork
    )
        : base(repository, unitOfWork)
    {
        _logger = logger;
    }
}
