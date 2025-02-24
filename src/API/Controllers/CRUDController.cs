using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoundCareApi.Persistence.Repository;
using WoundCareApi.Persistence.UnitOfWork;

namespace WoundCareApi.API.Controllers;

// 基礎API控制器提供CRUD操作
// [Authorize]
[Route("api/[controller]")]
[ApiController]
public class BaseApiController<T, C> : ControllerBase
    where T : class
    where C : DbContext
{
    protected readonly IRepository<T, C> _repository;
    protected readonly IUnitOfWork _unitOfWork;

    public BaseApiController(IRepository<T, C> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<T>>> GetAll(string? orderBy = null)
    {
        var items = await _repository.GetAllAsync(orderBy);
        return Ok(items);
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<T>> GetById(object id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpGet("query")]
    public virtual async Task<ActionResult<IEnumerable<T>>> GetFromQuery([FromQuery] string? filter)
    {
        var filteredData = await _repository.GetFromQueryStr(filter);
        return Ok(filteredData);
    }

    [HttpPost]
    public virtual async Task<ActionResult> Create([FromBody] T entity)
    {
        await _repository.AddAsync(entity);
        _unitOfWork.Save();
        return NoContent();
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update([FromBody] T entity)
    {
        await _repository.UpdateAsync(entity);
        _unitOfWork.Save();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(string id)
    {
        // Attempt to parse the ID as an integer
        if (int.TryParse(id, out int intId))
        {
            // If parsing is successful, call the delete method with the integer ID
            await _repository.DeleteByIdAsync(intId);
        }
        else if (Guid.TryParse(id, out var guidOutput))
        {
            await _repository.DeleteByIdAsync(guidOutput);
        }
        else
        {
            // If parsing fails, proceed with the ID as a string
            await _repository.DeleteByIdAsync(id);
        }

        _unitOfWork.Save();
        return NoContent();
    }
}
