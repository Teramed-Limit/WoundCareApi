using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoundCareApi.API.Controllers.Base;
using WoundCareApi.Application.Services;
using WoundCareApi.Common.Types;
using WoundCareApi.Core.Domain.Entities;
using WoundCareApi.Core.Domain.Interfaces;
using WoundCareApi.Core.Repository;
using WoundCareApi.Infrastructure.Persistence;
using WoundCareApi.Infrastructure.Persistence.UnitOfWork;
using WoundCareApi.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace WoundCareApi.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ClinicalUnitShiftController : BaseApiController<CRS_SysClinicalUnitShift, CRSDbContext>
{
    private readonly ILogger<ClinicalUnitShiftController> _logger;
    private readonly IRepository<CRS_SysClinicalUnit, CRSDbContext> _clinicalUnitRepository;
    private readonly ShiftTimeService _shiftTimeService;

    public ClinicalUnitShiftController(
        ILogger<ClinicalUnitShiftController> logger,
        IRepository<CRS_SysClinicalUnitShift, CRSDbContext> repository,
        IRepository<CRS_SysClinicalUnit, CRSDbContext> clinicalUnitRepository,
        IUnitOfWork unitOfWork,
        ShiftTimeService shiftTimeService
    )
        : base(repository, unitOfWork, logger)
    {
        _logger = logger;
        _clinicalUnitRepository = clinicalUnitRepository;
        _shiftTimeService = shiftTimeService;
    }

    // 獲取特定輪班對應的臨床單位資訊
    [HttpGet("{id}/clinicalunit")]
    public async Task<ActionResult<CRS_SysClinicalUnit>> GetClinicalUnitByShiftId(Guid id)
    {
        var shift = await Repository.GetByIdAsync(id);
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
    [HttpGet("TestShiftsByTime/{clinicalUnitPuid}")]
    public async Task<ActionResult> TestShiftsByTime(Guid clinicalUnitPuid)
    {
        // 獲取指定的臨床單位
        var clinicalUnits = (
            await _clinicalUnitRepository.GetByConditionAsync(cu => cu.Puid == clinicalUnitPuid)
        ).ToList();

        if (!clinicalUnits.Any())
        {
            return BadRequest($"找不到PUID為 {clinicalUnitPuid} 的臨床單位");
        }

        // 獲取與該臨床單位相關的班別
        var shifts = (
            await Repository.GetByConditionAsync(s => s.ClinicalUnitPuid == clinicalUnitPuid)
        ).ToList();

        if (!shifts.Any())
        {
            return BadRequest($"找不到與臨床單位 {clinicalUnitPuid} 相關的班別資料");
        }

        var testTimes = new List<DateTime>
        {
            new DateTime(2025, 2, 17, 15, 0, 0),
            new DateTime(2025, 2, 17, 15, 1, 0),
            new DateTime(2025, 2, 17, 7, 0, 0),
            new DateTime(2025, 2, 17, 7, 1, 0),
            new DateTime(2025, 2, 17, 23, 0, 0),
            new DateTime(2025, 2, 17, 23, 1, 0)
        };

        var shiftTimeResults = new List<ShiftTimeResult>();

        testTimes.ForEach(async testTime =>
        {
            // 默認使用當前時間作為observationDateTime
            DateTime observationDateTime = testTime;
            DateTime? observationShiftDate = null;

            // 計算班別時間資訊，指定臨床單位PUID
            var shiftTimeResult = _shiftTimeService.DetermineShiftAndTime(
                observationDateTime,
                clinicalUnits,
                shifts,
                clinicalUnitPuid
            );

            Guid? clinicalUnitShiftPuid = null;
            if (shiftTimeResult != null)
            {
                // 設置班別相關資訊
                clinicalUnitShiftPuid = shiftTimeResult.ClinicalUnitShiftPuid;

                // 設置臨床日期作為ObservationShiftDate
                if (!string.IsNullOrEmpty(shiftTimeResult.ClinicalDate))
                {
                    observationShiftDate = DateTime.Parse(shiftTimeResult.ClinicalDate);
                }
                shiftTimeResults.Add(shiftTimeResult);
            }
            else
            {
                shiftTimeResults.Add(null);
            }
        });

        return Ok(shiftTimeResults);
    }
}
