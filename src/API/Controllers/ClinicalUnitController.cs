using Microsoft.AspNetCore.Mvc;
using WoundCareApi.Infrastructure.Persistence;
using WoundCareApi.Persistence.Repository;
using WoundCareApi.Persistence.UnitOfWork;
using WoundCareApi.src.Core.Domain.CRSCoreDB;

namespace WoundCareApi.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClinicalUnitController : BaseApiController<SysClinicalUnit, CRSCoreDBContext>
{
    private readonly ILogger<ClinicalUnitController> _logger;

    public ClinicalUnitController(
        ILogger<ClinicalUnitController> logger,
        IRepository<SysClinicalUnit, CRSCoreDBContext> repository,
        IUnitOfWork unitOfWork
    )
        : base(repository, unitOfWork)
    {
        _logger = logger;
    }
}
