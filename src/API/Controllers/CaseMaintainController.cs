using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoundCareApi.API.Services;
using WoundCareApi.API.Controllers.Base;

namespace WoundCareApi.API.Controllers;

/// <summary>
/// 案例維護控制器，用於處理案例相關的維護操作
/// </summary>
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CaseMaintainController : BaseController
{
    private readonly CaseMaintainService _caseMaintainService;

    public CaseMaintainController(
        ILogger<CaseMaintainController> logger,
        CaseMaintainService caseMaintainService
    )
        : base(logger)
    {
        _caseMaintainService = caseMaintainService;
    }

    /// <summary>
    /// 將一個案例移動到另一個案例
    /// </summary>
    /// <param name="fromCaseId">來源案例ID</param>
    /// <param name="toCaseId">目標案例ID</param>
    /// <returns>操作結果</returns>
    [HttpPost("fromCaseId/{fromCaseId}/toCaseId/{toCaseId}")]
    public async Task<ActionResult> MoveCase(string fromCaseId, string toCaseId)
    {
        try
        {
            var userId = GetUserId();
            await _caseMaintainService.MoveCase(fromCaseId, toCaseId, userId);
            return Success("案例移動成功");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    /// <summary>
    /// 將一個系列移動到指定案例
    /// </summary>
    /// <param name="seriesInstanceUid">系列實例UID</param>
    /// <param name="toCaseId">目標案例ID</param>
    /// <returns>操作結果</returns>
    [HttpPost("seriesInstanceUid/{seriesInstanceUid}/toCaseId/{toCaseId}")]
    public async Task<ActionResult> MoveSeriesToCase(string seriesInstanceUid, string toCaseId)
    {
        try
        {
            var userId = GetUserId();
            await _caseMaintainService.MoveSeriesToCase(seriesInstanceUid, toCaseId, userId);
            return Success("系列移動成功");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    /// <summary>
    /// 將一個記錄移動到指定案例
    /// </summary>
    /// <param name="recordId">記錄ID</param>
    /// <param name="toCaseId">目標案例ID</param>
    /// <returns>操作結果</returns>
    [HttpPost("recordId/{recordId}/toCaseId/{toCaseId}")]
    public async Task<ActionResult> MoveRecordCase(string recordId, string toCaseId)
    {
        try
        {
            var userId = GetUserId();
            await _caseMaintainService.MoveRecordCase(recordId, toCaseId, userId);
            return Success("記錄移動成功");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }
}
