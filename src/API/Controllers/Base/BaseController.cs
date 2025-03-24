using Microsoft.AspNetCore.Mvc;

namespace WoundCareApi.API.Controllers.Base;

/// <summary>
/// 基礎控制器，提供通用的控制器功能
/// </summary>
public abstract class BaseController : ControllerBase
{
    protected readonly ILogger _logger;

    protected BaseController(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 獲取當前用戶ID
    /// </summary>
    protected string GetUserId()
    {
        var userId = User.Identity?.Name;
        if (userId == null)
        {
            _logger.LogError("User ID is null");
            throw new UnauthorizedAccessException("未授權的訪問");
        }
        return userId;
    }

    /// <summary>
    /// 返回成功響應
    /// </summary>
    protected ActionResult Success(string message)
    {
        return Ok(new { message });
    }

    /// <summary>
    /// 返回錯誤響應
    /// </summary>
    protected ActionResult BadRequest(string message)
    {
        return base.BadRequest(new { message });
    }

    /// <summary>
    /// 返回內部服務器錯誤響應
    /// </summary>
    protected ActionResult InternalServerError(Exception ex)
    {
        _logger.LogError(ex, "發生內部服務器錯誤");
        return StatusCode(500, new { message = "發生內部服務器錯誤" });
    }
}
