using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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
    private readonly IRepository<CRS_SysClinicalUnitShift, CRSDbContext> _shiftRepository;

    public ClinicalUnitController(
        ILogger<ClinicalUnitController> logger,
        IRepository<CRS_SysClinicalUnit, CRSDbContext> repository,
        IRepository<CRS_SysClinicalUnitShift, CRSDbContext> shiftRepository,
        IUnitOfWork unitOfWork
    )
        : base(repository, unitOfWork)
    {
        _logger = logger;
        _shiftRepository = shiftRepository;
    }

    // 獲取所有臨床單位
    [HttpGet]
    public override async Task<ActionResult<IEnumerable<CRS_SysClinicalUnit>>> GetAll(
        string? orderBy = null
    )
    {
        return await base.GetAll(orderBy);
    }

    // 獲取特定ID的臨床單位
    [HttpGet("{id}")]
    public override async Task<ActionResult<CRS_SysClinicalUnit>> GetById(object id)
    {
        return await base.GetById(id);
    }

    // 根據查詢條件獲取臨床單位
    [HttpGet("query")]
    public override async Task<ActionResult<IEnumerable<CRS_SysClinicalUnit>>> GetFromQuery(
        [FromQuery] string? filter
    )
    {
        return await base.GetFromQuery(filter);
    }

    // 獲取特定臨床單位的所有輪班
    [HttpGet("{id}/shifts")]
    public async Task<
        ActionResult<IEnumerable<CRS_SysClinicalUnitShift>>
    > GetShiftsByClinicalUnitId(Guid id)
    {
        Expression<Func<CRS_SysClinicalUnitShift, bool>> condition = s => s.ClinicalUnitPuid == id;

        var shifts = await _shiftRepository.GetByConditionAsync(condition);
        var shiftsList = shifts.ToList();

        if (!shiftsList.Any())
        {
            return NotFound($"未找到臨床單位ID {id} 的任何輪班資訊");
        }

        return Ok(shiftsList);
    }
}
