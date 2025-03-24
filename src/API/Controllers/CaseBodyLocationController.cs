using Microsoft.AspNetCore.Mvc;
using WoundCareApi.Core.Domain.CRS;
using WoundCareApi.Persistence.Repository;
using WoundCareApi.Persistence.UnitOfWork;
using WoundCareApi.src.Core.Domain.CRS;
using WoundCareApi.src.Infrastructure.Persistence;

namespace WoundCareApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CaseBodyLocationController : BaseApiController<CRS_CfgBodyLocation, CRSDbContext>
{
    private readonly ILogger<CaseBodyLocationController> _logger;

    public CaseBodyLocationController(
        ILogger<CaseBodyLocationController> logger,
        IRepository<CRS_CfgBodyLocation, CRSDbContext> repository,
        IUnitOfWork unitOfWork
    )
        : base(repository, unitOfWork)
    {
        _logger = logger;
    }
}
