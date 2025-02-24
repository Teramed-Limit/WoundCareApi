using Microsoft.AspNetCore.Mvc;
using WoundCareApi.Infrastructure.Persistence;
using WoundCareApi.Persistence.Repository;
using WoundCareApi.Persistence.UnitOfWork;
using WoundCareApi.src.Core.Domain.CRSCoreDB;

namespace WoundCareApi.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WoundTypeController : BaseApiController<CfgWoundType, CRSCoreDBContext>
{
    private readonly ILogger<WoundTypeController> _logger;

    public WoundTypeController(
        ILogger<WoundTypeController> logger,
        IRepository<CfgWoundType, CRSCoreDBContext> repository,
        IUnitOfWork unitOfWork
    )
        : base(repository, unitOfWork)
    {
        _logger = logger;
    }
}
