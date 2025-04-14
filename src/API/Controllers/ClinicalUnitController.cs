using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WoundCareApi.API.Controllers.Base;
using WoundCareApi.Core.Domain.Entities;
using WoundCareApi.Core.Domain.Interfaces;
using WoundCareApi.Core.Repository;
using WoundCareApi.Infrastructure.Persistence;
using WoundCareApi.Infrastructure.Persistence.UnitOfWork;
using WoundCareApi.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace WoundCareApi.API.Controllers;

/// <summary>
/// 臨床單位管理控制器
/// </summary>
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ClinicalUnitController : BaseApiController<CRS_SysClinicalUnit, CRSDbContext>
{
    private readonly ILogger<ClinicalUnitController> _logger;
    private readonly IRepository<CRS_SysClinicalUnitShift, CRSDbContext> _shiftRepository;
    private readonly IRepository<CRS_SysClinicalUnit, CRSDbContext> _clinicalUnitRepository;

    public ClinicalUnitController(
        ILogger<ClinicalUnitController> logger,
        IRepository<CRS_SysClinicalUnit, CRSDbContext> repository,
        IRepository<CRS_SysClinicalUnitShift, CRSDbContext> shiftRepository,
        IUnitOfWork unitOfWork
    )
        : base(repository, unitOfWork, logger)
    {
        _logger = logger;
        _shiftRepository = shiftRepository;
        _clinicalUnitRepository = repository;
    }

    /// <summary>
    /// 獲取特定臨床單位的所有輪班資訊
    /// </summary>
    /// <param name="id">臨床單位 ID</param>
    /// <returns>輪班資訊列表</returns>
    [HttpGet("{id}/shifts")]
    public async Task<
        ActionResult<IEnumerable<CRS_SysClinicalUnitShift>>
    > GetShiftsByClinicalUnitId(Guid id)
    {
        try
        {
            // 驗證臨床單位是否存在
            var clinicalUnit = await _clinicalUnitRepository.GetByIdAsync(id);
            if (clinicalUnit == null)
            {
                _logger.LogWarning("未找到臨床單位 ID: {ClinicalUnitId}", id);
                return NotFound($"未找到臨床單位 ID {id}");
            }

            // 獲取輪班資訊
            Expression<Func<CRS_SysClinicalUnitShift, bool>> condition = s =>
                s.ClinicalUnitPuid == id;
            var shifts = await _shiftRepository.GetByConditionAsync(condition);
            var shiftsList = shifts.ToList();

            if (!shiftsList.Any())
            {
                return Ok(Array.Empty<CRS_SysClinicalUnitShift>());
            }
            
            return Ok(shiftsList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "獲取臨床單位 ID: {ClinicalUnitId} 的輪班資訊時發生錯誤", id);
            return StatusCode(500, "獲取輪班資訊時發生錯誤，請稍後再試");
        }
    }
}
