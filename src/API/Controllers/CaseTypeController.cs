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
public class CaseTypeController : BaseApiController<CfgCaseType, CRSDbContext>
{
    private readonly ILogger<CaseTypeController> _logger;

    public CaseTypeController(
        ILogger<CaseTypeController> logger,
        IRepository<CfgCaseType, CRSDbContext> repository,
        IUnitOfWork unitOfWork
    )
        : base(repository, unitOfWork, logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
}
