using Microsoft.AspNetCore.Mvc;
using WoundCareApi.Persistence.Repository;
using WoundCareApi.Persistence.UnitOfWork;
using WoundCareApi.src.Core.Domain.CRS;
using WoundCareApi.src.Infrastructure.Persistence;

namespace WoundCareApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClinicalUnitController : BaseApiController<CRS_SysClinicalUnit, CRSDbContext>
{
    private readonly ILogger<ClinicalUnitController> _logger;

    public ClinicalUnitController(
        ILogger<ClinicalUnitController> logger,
        IRepository<CRS_SysClinicalUnit, CRSDbContext> repository,
        IUnitOfWork unitOfWork
    )
        : base(repository, unitOfWork)
    {
        _logger = logger;
    }
}
