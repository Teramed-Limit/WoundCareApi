using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeraLinkaCareApi.Application.DTOs;
using TeraLinkaCareApi.Core.Domain.Entities;
using TeraLinkaCareApi.Core.Domain.Interfaces;
using TeraLinkaCareApi.Infrastructure.Persistence;
using TeraLinkaCareApi.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace TeraLinkaCareApi.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IRepository<LoginUserDatum, CRSDbContext> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AccountController(
        ILogger<AccountController> logger,
        IRepository<LoginUserDatum, CRSDbContext> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _logger = logger;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserAccountDto>>> GetAll()
    {
        var items = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<UserAccountDto>>(items));
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] UserAccountDto entity)
    {
        try
        {
            entity.CreateUser = User.Identity?.Name;
            entity.CreateDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            var loginUserDatum = _mapper.Map<LoginUserDatum>(entity);
            await _repository.AddAsync(loginUserDatum);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "創建 {EntityType} 記錄時發生錯誤", typeof(LoginUserDatum).Name);
            return StatusCode(500, "創建記錄時發生錯誤");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] UserAccountDto entity)
    {
        try
        {
            var existingEntity = await _repository.GetByIdAsync(entity.UserID);
            if (existingEntity == null)
            {
                _logger.LogWarning(
                    "未找到ID為 {Id} 的 {EntityType} 記錄",
                    entity.UserID,
                    nameof(LoginUserDatum)
                );
                return NotFound();
            }

            entity.ModifiedUser = User.Identity?.Name;
            entity.ModifiedDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            _mapper.Map(entity, existingEntity);
            await _repository.UpdateAsync(existingEntity);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "更新 {EntityType} 記錄時發生錯誤", nameof(LoginUserDatum));
            return StatusCode(500, "更新記錄時發生錯誤");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _repository.DeleteByIdAsync(id);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
