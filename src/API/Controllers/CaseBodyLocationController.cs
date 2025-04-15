using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeraLinkaCareApi.API.Controllers.Base;
using TeraLinkaCareApi.Core.Domain.Entities;
using TeraLinkaCareApi.Core.Domain.Interfaces;
using TeraLinkaCareApi.Infrastructure.Persistence;
using TeraLinkaCareApi.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace TeraLinkaCareApi.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CaseBodyLocationController : BaseApiController<CfgBodyLocation, CRSDbContext>
{
    private readonly ILogger<CaseBodyLocationController> _logger;

    public CaseBodyLocationController(
        ILogger<CaseBodyLocationController> logger,
        IRepository<CfgBodyLocation, CRSDbContext> repository,
        IUnitOfWork unitOfWork
    )
        : base(repository, unitOfWork, logger)
    {
        _logger = logger;
    }
}
