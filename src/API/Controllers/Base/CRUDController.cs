using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoundCareApi.Core.Domain.Interfaces;
using WoundCareApi.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace WoundCareApi.API.Controllers.Base;

// 基礎API控制器提供CRUD操作
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BaseApiController<T, C> : ControllerBase
    where T : class
    where C : DbContext
{
    protected readonly IRepository<T, C> Repository;
    protected readonly IUnitOfWork UnitOfWork;
    protected readonly ILogger _logger;

    public BaseApiController(IRepository<T, C> repository, IUnitOfWork unitOfWork, ILogger logger)
    {
        Repository = repository;
        UnitOfWork = unitOfWork;
        _logger = logger;
    }

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<T>>> GetAll(string? orderBy = null)
    {
        try
        {
            _logger.LogInformation(
                "獲取所有 {EntityType} 記錄，排序方式: {OrderBy}",
                typeof(T).Name,
                orderBy ?? "默認"
            );
            var items = await Repository.GetAllAsync(orderBy);
            _logger.LogInformation("成功獲取 {Count} 條 {EntityType} 記錄", items.Count(), typeof(T).Name);
            return Ok(items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "獲取 {EntityType} 記錄時發生錯誤", typeof(T).Name);
            return StatusCode(500, "獲取記錄時發生錯誤");
        }
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<T>> GetById(object id)
    {
        try
        {
            _logger.LogInformation("根據ID獲取 {EntityType} 記錄: {Id}", typeof(T).Name, id);
            var item = await Repository.GetByIdAsync(id);
            if (item == null)
            {
                _logger.LogWarning("未找到ID為 {Id} 的 {EntityType} 記錄", id, typeof(T).Name);
                return NotFound();
            }

            _logger.LogInformation("成功獲取ID為 {Id} 的 {EntityType} 記錄", id, typeof(T).Name);
            return Ok(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "獲取ID為 {Id} 的 {EntityType} 記錄時發生錯誤", id, typeof(T).Name);
            return StatusCode(500, "獲取記錄時發生錯誤");
        }
    }

    [HttpGet("query")]
    public virtual async Task<ActionResult<IEnumerable<T>>> GetFromQuery([FromQuery] string? filter)
    {
        try
        {
            _logger.LogInformation(
                "根據查詢條件獲取 {EntityType} 記錄: {Filter}",
                typeof(T).Name,
                filter ?? "無"
            );
            var filteredData = await Repository.GetFromQueryStr(filter);
            _logger.LogInformation(
                "成功獲取 {Count} 條符合條件的 {EntityType} 記錄",
                filteredData.Count(),
                typeof(T).Name
            );
            return Ok(filteredData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "根據查詢條件獲取 {EntityType} 記錄時發生錯誤: {Filter}", typeof(T).Name, filter);
            return StatusCode(500, "獲取記錄時發生錯誤");
        }
    }

    [HttpPost]
    public virtual async Task<ActionResult> Create([FromBody] T entity)
    {
        try
        {
            _logger.LogInformation("創建新的 {EntityType} 記錄", typeof(T).Name);

            // 檢查實體類型是否具有 CreateDateTime 和 CreateUser 屬性
            var entityType = typeof(T);
            var createDateTimeProperty = entityType.GetProperty("CreateDateTime");
            var createUserProperty = entityType.GetProperty("CreateUser");

            // 獲取當前用戶ID
            var userId = User.Identity?.Name;
            _logger.LogInformation("當前操作用戶: {UserId}", userId ?? "未知");

            // 如果實體具有 CreateDateTime 屬性，則設置當前日期時間
            if (createDateTimeProperty != null)
            {
                // 根據屬性類型設置適當的日期時間格式
                if (createDateTimeProperty.PropertyType == typeof(string))
                {
                    // 如果屬性類型是字符串，則設置為當前日期時間的字符串表示
                    createDateTimeProperty.SetValue(
                        entity,
                        DateTime.Now.ToString("yyyyMMddHHmmss")
                    );
                }
                else if (createDateTimeProperty.PropertyType == typeof(DateTime))
                {
                    // 如果屬性類型是 DateTime，則設置為當前日期時間
                    createDateTimeProperty.SetValue(entity, DateTime.Now);
                }
            }

            // 如果實體具有 CreateUser 屬性，則設置當前用戶ID
            if (createUserProperty != null && userId != null)
            {
                createUserProperty.SetValue(entity, userId);
            }

            await Repository.AddAsync(entity);
            await UnitOfWork.SaveAsync();
            _logger.LogInformation("成功創建 {EntityType} 記錄", typeof(T).Name);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "創建 {EntityType} 記錄時發生錯誤", typeof(T).Name);
            return StatusCode(500, "創建記錄時發生錯誤");
        }
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update([FromBody] T entity)
    {
        try
        {
            _logger.LogInformation("更新 {EntityType} 記錄", typeof(T).Name);

            // 檢查實體類型是否具有 ModifiedDateTime 和 ModifiedUser 屬性
            var entityType = typeof(T);
            var modifiedDateTimeProperty = entityType.GetProperty("ModifiedDateTime");
            var modifiedUserProperty = entityType.GetProperty("ModifiedUser");

            // 獲取當前用戶ID
            var userId = User.Identity?.Name;
            _logger.LogInformation("當前操作用戶: {UserId}", userId ?? "未知");

            // 如果實體具有 ModifiedDateTime 屬性，則設置當前日期時間
            if (modifiedDateTimeProperty != null)
            {
                // 根據屬性類型設置適當的日期時間格式
                if (modifiedDateTimeProperty.PropertyType == typeof(string))
                {
                    // 如果屬性類型是字符串，則設置為當前日期時間的字符串表示
                    modifiedDateTimeProperty.SetValue(
                        entity,
                        DateTime.Now.ToString("yyyyMMddHHmmss")
                    );
                }
                else if (modifiedDateTimeProperty.PropertyType == typeof(DateTime))
                {
                    // 如果屬性類型是 DateTime，則設置為當前日期時間
                    modifiedDateTimeProperty.SetValue(entity, DateTime.Now);
                }
            }

            // 如果實體具有 ModifiedUser 屬性，則設置當前用戶ID
            if (modifiedUserProperty != null && userId != null)
            {
                modifiedUserProperty.SetValue(entity, userId);
            }

            await Repository.UpdateAsync(entity);
            await UnitOfWork.SaveAsync();
            _logger.LogInformation("成功更新 {EntityType} 記錄", typeof(T).Name);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "更新 {EntityType} 記錄時發生錯誤", typeof(T).Name);
            return StatusCode(500, "更新記錄時發生錯誤");
        }
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(string id)
    {
        try
        {
            _logger.LogInformation("刪除 {EntityType} 記錄，ID: {Id}", typeof(T).Name, id);

            // Attempt to parse the ID as an integer
            if (int.TryParse(id, out int intId))
            {
                // If parsing is successful, call the delete method with the integer ID
                _logger.LogInformation("使用整數ID刪除 {EntityType} 記錄: {Id}", typeof(T).Name, intId);
                await Repository.DeleteByIdAsync(intId);
            }
            else if (Guid.TryParse(id, out var guidOutput))
            {
                _logger.LogInformation(
                    "使用GUID刪除 {EntityType} 記錄: {Id}",
                    typeof(T).Name,
                    guidOutput
                );
                await Repository.DeleteByIdAsync(guidOutput);
            }
            else
            {
                // If parsing fails, proceed with the ID as a string
                _logger.LogInformation("使用字符串ID刪除 {EntityType} 記錄: {Id}", typeof(T).Name, id);
                await Repository.DeleteByIdAsync(id);
            }

            await UnitOfWork.SaveAsync();
            _logger.LogInformation("成功刪除ID為 {Id} 的 {EntityType} 記錄", id, typeof(T).Name);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "刪除ID為 {Id} 的 {EntityType} 記錄時發生錯誤", id, typeof(T).Name);
            return StatusCode(500, "刪除記錄時發生錯誤");
        }
    }
}
