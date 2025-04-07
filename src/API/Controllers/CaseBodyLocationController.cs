using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoundCareApi.API.Controllers.Base;
using WoundCareApi.Core.Domain.Entities;
using WoundCareApi.Core.Domain.Interfaces;
using WoundCareApi.Infrastructure.Persistence;
using WoundCareApi.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace WoundCareApi.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CaseBodyLocationController : BaseApiController<CRS_CfgBodyLocation, CRSDbContext>
{
    private readonly ILogger<CaseBodyLocationController> _logger;

    public CaseBodyLocationController(
        ILogger<CaseBodyLocationController> logger,
        IRepository<CRS_CfgBodyLocation, CRSDbContext> repository,
        IUnitOfWork unitOfWork
    )
        : base(repository, unitOfWork, logger)
    {
        _logger = logger;
    }
}
