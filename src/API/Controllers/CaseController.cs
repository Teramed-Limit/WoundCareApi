using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoundCareApi.API.DTOs;
using WoundCareApi.API.Services;
using WoundCareApi.src.Core.Domain.CRS;
using System.Globalization;

namespace WoundCareApi.API.Controllers;

/// <summary>
/// 傷口案例管理控制器
/// </summary>
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CaseController : ControllerBase
{
    private readonly ILogger<CaseController> _logger;
    private readonly CaseService _caseService;

    public CaseController(ILogger<CaseController> logger, CaseService caseService)
    {
        _logger = logger;
        _caseService = caseService;
    }

    /// <summary>
    /// 根據病人ID和就醫ID獲取案例列表
    /// </summary>
    /// <param name="patientId">病人ID</param>
    /// <param name="encounterId">就醫ID</param>
    /// <returns>按身體部位分類的案例字典</returns>
    /// <response code="200">成功返回案例列表</response>
    /// <response code="400">請求參數無效</response>
    /// <response code="401">未授權</response>
    /// <response code="500">伺服器內部錯誤</response>
    [Authorize(AuthenticationSchemes = "Bearer,ApiKey")]
    [HttpGet("patientId/{patientId}/encounterId/{encounterId}/cases")]
    public async Task<ActionResult<Dictionary<string, CRS_Case>>> GetByPatientIdAndEncounterId(
        string patientId,
        string encounterId
    )
    {
        try
        {
            if (string.IsNullOrEmpty(patientId) || string.IsNullOrEmpty(encounterId))
            {
                return BadRequest("病人ID和就醫ID不能為空");
            }

            var res = await _caseService.GetCasesByBodyPart(patientId, encounterId);
            return Ok(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "獲取案例列表時發生錯誤: PatientId={PatientId}, EncounterId={EncounterId}",
                patientId,
                encounterId
            );
            return StatusCode(500, "獲取案例列表時發生錯誤");
        }
    }

    /// <summary>
    /// 獲取案例歷史記錄
    /// </summary>
    /// <param name="caseId">案例ID</param>
    /// <returns>案例歷史記錄</returns>
    /// <response code="200">成功返回案例歷史記錄</response>
    /// <response code="400">請求參數無效</response>
    /// <response code="401">未授權</response>
    /// <response code="404">找不到案例</response>
    /// <response code="500">伺服器內部錯誤</response>
    [Authorize(AuthenticationSchemes = "Bearer,ApiKey")]
    [HttpGet("caseId/{caseId}/caseHistory")]
    public async Task<ActionResult<CaseHistoryDto>> GetCaseHistory(string caseId)
    {
        try
        {
            if (!Guid.TryParse(caseId, out _))
            {
                return BadRequest("無效的案例ID格式");
            }

            var res = await _caseService.GetCaseHistory(caseId);
            return Ok(res);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "找不到案例: CaseId={CaseId}", caseId);
            return NotFound($"找不到案例: {caseId}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "獲取案例歷史記錄時發生錯誤: CaseId={CaseId}", caseId);
            return StatusCode(500, "獲取案例歷史記錄時發生錯誤");
        }
    }

    /// <summary>
    /// 獲取案例記錄
    /// </summary>
    /// <param name="caseId">案例ID</param>
    /// <param name="date">日期 (格式: yyyyMMdd)</param>
    /// <param name="isUsingShiftDate">是否使用班別日期</param>
    /// <returns>案例記錄列表</returns>
    /// <response code="200">成功返回案例記錄</response>
    /// <response code="400">請求參數無效</response>
    /// <response code="401">未授權</response>
    /// <response code="404">找不到案例</response>
    /// <response code="500">伺服器內部錯誤</response>
    [Authorize(AuthenticationSchemes = "Bearer,ApiKey")]
    [HttpGet("caseId/{caseId}/date/{date}/caseRecords")]
    public async Task<ActionResult<IEnumerable<CaseRecordDto>>> GetCaseRecord(
        string caseId,
        string date,
        [FromQuery] bool isUsingShiftDate = false
    )
    {
        try
        {
            if (!Guid.TryParse(caseId, out _))
            {
                return BadRequest("無效的案例ID格式");
            }

            if (
                !DateTime.TryParseExact(
                    date,
                    "yyyyMMdd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out _
                )
            )
            {
                return BadRequest("無效的日期格式，請使用yyyyMMdd格式");
            }

            var res = await _caseService.GetCaseRecord(caseId, date, isUsingShiftDate);
            return Ok(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "獲取案例記錄時發生錯誤: CaseId={CaseId}, Date={Date}", caseId, date);
            return StatusCode(500, "獲取案例記錄時發生錯誤");
        }
    }

    /// <summary>
    /// 獲取案例圖片
    /// </summary>
    /// <param name="caseId">案例ID</param>
    /// <param name="date">日期 (格式: yyyyMMdd)</param>
    /// <param name="isUsingShiftDate">是否使用班別日期</param>
    /// <returns>案例圖片列表</returns>
    /// <response code="200">成功返回案例圖片</response>
    /// <response code="400">請求參數無效</response>
    /// <response code="401">未授權</response>
    /// <response code="404">找不到案例</response>
    /// <response code="500">伺服器內部錯誤</response>
    [Authorize(AuthenticationSchemes = "Bearer,ApiKey")]
    [HttpGet("caseId/{caseId}/date/{date}/images")]
    public async Task<ActionResult<IEnumerable<CaseImageDto>>> GetCaseImage(
        string caseId,
        string date,
        [FromQuery] bool isUsingShiftDate = false
    )
    {
        try
        {
            if (!Guid.TryParse(caseId, out _))
            {
                return BadRequest("無效的案例ID格式");
            }

            if (
                !DateTime.TryParseExact(
                    date,
                    "yyyyMMdd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out _
                )
            )
            {
                return BadRequest("無效的日期格式，請使用yyyyMMdd格式");
            }

            var res = await _caseService.GetCaseImage(caseId, date, isUsingShiftDate);
            return Ok(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "獲取案例圖片時發生錯誤: CaseId={CaseId}, Date={Date}", caseId, date);
            return StatusCode(500, "獲取案例圖片時發生錯誤");
        }
    }

    /// <summary>
    /// 將案例標記為已關閉
    /// </summary>
    /// <param name="caseId">案例ID</param>
    /// <returns>操作結果</returns>
    /// <response code="200">成功關閉案例</response>
    /// <response code="400">請求參數無效</response>
    /// <response code="401">未授權</response>
    /// <response code="404">找不到案例</response>
    /// <response code="500">伺服器內部錯誤</response>
    [HttpPost("caseId/{caseId}/setAsClosed")]
    public async Task<ActionResult> SetCaseAsClosed(string caseId)
    {
        try
        {
            var userId = User.Identity?.Name;
            if (userId == null)
            {
                _logger.LogError("用戶ID為空");
                return Unauthorized("未授權的用戶");
            }

            if (!Guid.TryParse(caseId, out Guid caseGuid))
            {
                return BadRequest("無效的案例ID格式");
            }

            await _caseService.SetCaseAsClosedAsync(caseGuid, userId);
            return Ok(new { message = "案例已成功關閉" });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "找不到案例: CaseId={CaseId}", caseId);
            return NotFound($"找不到案例: {caseId}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "關閉案例時發生錯誤: CaseId={CaseId}", caseId);
            return StatusCode(500, "關閉案例時發生錯誤");
        }
    }

    /// <summary>
    /// 將案例標記為開啟
    /// </summary>
    /// <param name="caseId">案例ID</param>
    /// <returns>操作結果</returns>
    /// <response code="200">成功開啟案例</response>
    /// <response code="400">請求參數無效</response>
    /// <response code="401">未授權</response>
    /// <response code="404">找不到案例</response>
    /// <response code="500">伺服器內部錯誤</response>
    [HttpPost("caseId/{caseId}/setAsOpen")]
    public async Task<ActionResult> SetCaseAsOpen(string caseId)
    {
        try
        {
            var userId = User.Identity?.Name;
            if (userId == null)
            {
                _logger.LogError("用戶ID為空");
                return Unauthorized("未授權的用戶");
            }

            if (!Guid.TryParse(caseId, out Guid caseGuid))
            {
                return BadRequest("無效的案例ID格式");
            }

            await _caseService.SetCaseAsOpenAsync(caseGuid, userId);
            return Ok(new { message = "案例已成功開啟" });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "找不到案例: CaseId={CaseId}", caseId);
            return NotFound($"找不到案例: {caseId}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "開啟案例時發生錯誤: CaseId={CaseId}", caseId);
            return StatusCode(500, "開啟案例時發生錯誤");
        }
    }
}
