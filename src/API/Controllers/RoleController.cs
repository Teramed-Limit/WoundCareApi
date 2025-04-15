using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeraLinkaCareApi.API.Controllers.Base;
using TeraLinkaCareApi.Application.Services;
using TeraLinkaCareApi.Core.Domain.Entities;
using TeraLinkaCareApi.Core.Domain.Interfaces;
using TeraLinkaCareApi.Infrastructure.Persistence;
using TeraLinkaCareApi.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace TeraLinkaCareApi.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class RoleController : BaseApiController<RoleGroup, CRSDbContext>
{
    private readonly RoleService _roleService;

    public RoleController(
        ILogger<RoleController> logger,
        IRepository<RoleGroup, CRSDbContext> repository,
        IUnitOfWork unitOfWork,
        RoleService roleService
    )
        : base(repository, unitOfWork, logger)
    {
        _roleService = roleService;
    }

    [HttpGet("{roleName}/functionList")]
    public async Task<IActionResult> GetFunctionListByRoleName(string roleName)
    {
        var result = await _roleService.GetFunctionListByRoleName(roleName);
        return Ok(result);
    }

    [HttpPost("{roleName}/function/{functionName}")]
    public async Task<IActionResult> SetFunctionByRoleName(string roleName, string functionName)
    {
        var result = await _roleService.SetFunctionByRoleName(roleName, functionName);
        if (!result)
            return BadRequest("無法設定功能到角色，可能是角色或功能不存在");

        return Ok(new { success = true });
    }

    [HttpDelete("{roleName}/function/{functionName}")]
    public async Task<IActionResult> DeleteFunctionByRoleName(string roleName, string functionName)
    {
        var result = await _roleService.DeleteFunctionByRoleName(roleName, functionName);
        if (!result)
            return BadRequest("無法從角色中刪除功能，可能是關聯不存在");

        return Ok(new { success = true });
    }
}
