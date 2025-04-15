using Microsoft.EntityFrameworkCore;
using TeraLinkaCareApi.Application.DTOs;
using TeraLinkaCareApi.Core.Domain.Entities;
using TeraLinkaCareApi.Infrastructure.Persistence;

namespace TeraLinkaCareApi.Application.Services;

public class RoleService
{
    private readonly CRSDbContext _context;

    public RoleService(CRSDbContext context)
    {
        _context = context;
    }

    public async Task<List<RoleGroup>> GetRoleList()
    {
        var roleList = await _context.RoleGroups.ToListAsync();
        return roleList;
    }

    public async Task<List<RoleFunction>> GetFunctionList()
    {
        var functionList = await _context.RoleFunctions.ToListAsync();
        return functionList;
    }

    public async Task<List<RoleFunctionWithAssignmentDto>> GetFunctionListByRoleName(
        string roleName
    )
    {
        // 獲取所有功能
        var allFunctions = await _context.RoleFunctions.ToListAsync();

        // 獲取該角色已分配的功能名稱
        var assignedFunctionNames = await _context.FunctionRoleGroups
            .Where(x => x.RoleName == roleName)
            .Select(x => x.FunctionName)
            .ToListAsync();

        // 組合結果
        var result = allFunctions
            .Select(
                f =>
                    new RoleFunctionWithAssignmentDto
                    {
                        FunctionName = f.FunctionName,
                        Description = f.Description,
                        CorrespondElementId = f.CorrespondElementId,
                        IsAssigned = assignedFunctionNames.Contains(f.FunctionName)
                    }
            )
            .ToList();

        return result;
    }

    public async Task<List<RoleFunction>> GetUserRoleList(string userId)
    {
        // 檢索用戶角色列表
        var roleListString = await _context.LoginUserData
            .Where(x => x.UserID == userId)
            .Select(x => x.RoleList)
            .FirstOrDefaultAsync();

        if (roleListString == null)
            return new List<RoleFunction>();

        // 避免在表達式樹中調用帶有可選參數的方法
        var roleList = roleListString.Split(new[] { ',' }).ToList();

        // 通過與 FunctionRoleGroups 進行連接，直接查詢 RoleFunctions
        var functionList = await (
            from frg in _context.FunctionRoleGroups
            join rf in _context.RoleFunctions on frg.FunctionName equals rf.FunctionName
            where roleList.Contains(frg.RoleName)
            select rf
        ).ToListAsync();

        return functionList;
    }

    public async Task<bool> SetFunctionByRoleName(string roleName, string functionName)
    {
        // 檢查角色是否存在
        var roleExists = await _context.RoleGroups.AnyAsync(r => r.RoleName == roleName);
        if (!roleExists)
            return false;

        // 檢查功能是否存在
        var functionExists = await _context.RoleFunctions.AnyAsync(
            f => f.FunctionName == functionName
        );
        if (!functionExists)
            return false;

        // 檢查是否已經存在相同的關聯
        var existingRelation = await _context.FunctionRoleGroups.AnyAsync(
            frg => frg.RoleName == roleName && frg.FunctionName == functionName
        );

        if (existingRelation)
            return true; // 已經存在，視為成功

        // 創建新的關聯
        var functionRoleGroup = new FunctionRoleGroup
        {
            RoleName = roleName,
            FunctionName = functionName
        };

        _context.FunctionRoleGroups.Add(functionRoleGroup);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteFunctionByRoleName(string roleName, string functionName)
    {
        // 查找要刪除的關聯
        var functionRoleGroup = await _context.FunctionRoleGroups.FirstOrDefaultAsync(
            frg => frg.RoleName == roleName && frg.FunctionName == functionName
        );

        if (functionRoleGroup == null)
            return false; // 關聯不存在

        // 刪除關聯
        _context.FunctionRoleGroups.Remove(functionRoleGroup);
        await _context.SaveChangesAsync();

        return true;
    }
}
