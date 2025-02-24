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
public class ClinicalUnitShiftController : BaseApiController<CRS_SysClinicalUnitShift, CRSDbContext>
{
    private readonly ILogger<ClinicalUnitShiftController> _logger;
    private readonly IRepository<CRS_SysClinicalUnit, CRSDbContext> _clinicalUnitRepository;

    public ClinicalUnitShiftController(
        ILogger<ClinicalUnitShiftController> logger,
        IRepository<CRS_SysClinicalUnitShift, CRSDbContext> repository,
        IRepository<CRS_SysClinicalUnit, CRSDbContext> clinicalUnitRepository,
        IUnitOfWork unitOfWork
    )
        : base(repository, unitOfWork)
    {
        _logger = logger;
        _clinicalUnitRepository = clinicalUnitRepository;
    }

    // 獲取所有輪班資訊
    [HttpGet]
    public override async Task<ActionResult<IEnumerable<CRS_SysClinicalUnitShift>>> GetAll(
        string? orderBy = null
    )
    {
        return await base.GetAll(orderBy);
    }

    // 獲取特定ID的輪班資訊
    [HttpGet("{id}")]
    public override async Task<ActionResult<CRS_SysClinicalUnitShift>> GetById(object id)
    {
        return await base.GetById(id);
    }

    // 根據查詢條件獲取輪班資訊
    [HttpGet("query")]
    public override async Task<ActionResult<IEnumerable<CRS_SysClinicalUnitShift>>> GetFromQuery(
        [FromQuery] string? filter
    )
    {
        return await base.GetFromQuery(filter);
    }

    // 獲取特定輪班對應的臨床單位資訊
    [HttpGet("{id}/clinicalunit")]
    public async Task<ActionResult<CRS_SysClinicalUnit>> GetClinicalUnitByShiftId(Guid id)
    {
        var shift = await _repository.GetByIdAsync(id);
        if (shift == null)
        {
            return NotFound($"未找到ID為 {id} 的輪班資訊");
        }

        var clinicalUnit = await _clinicalUnitRepository.GetByIdAsync(shift.ClinicalUnitPuid);
        if (clinicalUnit == null)
        {
            return NotFound($"未找到與輪班ID {id} 關聯的臨床單位");
        }

        return Ok(clinicalUnit);
    }

    // 根據時間範圍獲取輪班資訊
    [HttpGet("bytime")]
    public async Task<ActionResult<IEnumerable<CRS_SysClinicalUnitShift>>> GetShiftsByTimeRange(
        [FromQuery] int? beginHour,
        [FromQuery] int? endHour
    )
    {
        Expression<Func<CRS_SysClinicalUnitShift, bool>> condition = s => true;

        if (beginHour.HasValue)
        {
            condition = s => s.ShiftBeginHour >= beginHour.Value;
        }

        if (endHour.HasValue)
        {
            Expression<Func<CRS_SysClinicalUnitShift, bool>> endHourCondition = s =>
                s.ShiftBeginHour + s.ShiftDuration <= endHour.Value;

            // 由於無法直接組合Lambda表達式，這裡採用簡化的方法
            if (beginHour.HasValue)
            {
                // 如果有開始時間，則需要分開獲取並在Controller中過濾
                var shifts = await _repository.GetAllAsync();
                var filteredShifts = shifts
                    .Where(
                        s =>
                            s.ShiftBeginHour >= beginHour.Value
                            && (s.ShiftBeginHour + s.ShiftDuration) <= endHour.Value
                    )
                    .ToList();

                if (!filteredShifts.Any())
                {
                    return NotFound("未找到符合時間範圍的輪班資訊");
                }

                return Ok(filteredShifts);
            }
            else
            {
                // 僅有結束時間的條件
                condition = endHourCondition;
            }
        }

        var results = await _repository.GetByConditionAsync(condition);
        var resultList = results.ToList();

        if (!resultList.Any())
        {
            return NotFound("未找到符合時間範圍的輪班資訊");
        }

        return Ok(resultList);
    }
}
