using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoundCareApi.Core.Domain.Entities;
using WoundCareApi.Core.Domain.Interfaces;
using WoundCareApi.Core.Repository;
using WoundCareApi.Infrastructure.Persistence;

namespace WoundCareApi.API.Controllers;

/// <summary>
/// 病患就醫紀錄控制器
/// </summary>
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PatientEncounterController : ControllerBase
{
    private readonly ILogger<PatientEncounterController> _logger;
    private readonly IRepository<CRS_A_PtEncounter, CRSDbContext> _repository;

    public PatientEncounterController(
        ILogger<PatientEncounterController> logger,
        IRepository<CRS_A_PtEncounter, CRSDbContext> repository
    )
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 根據臨床單位 ID 取得病患就醫紀錄列表
    /// </summary>
    /// <param name="clinicalUnitId">臨床單位 ID (GUID 格式)</param>
    /// <returns>病患就醫紀錄列表</returns>
    [HttpGet("clinicalUnitId/{clinicalUnitId}")]
    public async Task<ActionResult<IEnumerable<CRS_A_PtEncounter>>> GetByClinicalUnitId(
        string clinicalUnitId
    )
    {
        try
        {
            if (!Guid.TryParse(clinicalUnitId, out Guid clinicalUnitGuid))
            {
                _logger.LogWarning("無效的臨床單位 ID 格式: {ClinicalUnitId}", clinicalUnitId);
                return BadRequest("臨床單位 ID 格式不正確");
            }

            var encounters = await _repository.GetByConditionAsync(
                x => x.ClinicalUnitPuid == clinicalUnitGuid,
                orderBy: x => x.BedLabel
            );

            if (!encounters.Any())
            {
                return NotFound($"找不到臨床單位 ID {clinicalUnitId} 的病患就醫紀錄");
            }
            
            return Ok(encounters);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "取得臨床單位 ID {ClinicalUnitId} 的病患就醫紀錄時發生錯誤", clinicalUnitId);
            return StatusCode(500, "取得病患就醫紀錄時發生錯誤");
        }
    }
}
