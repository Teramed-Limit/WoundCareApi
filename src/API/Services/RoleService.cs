using Microsoft.EntityFrameworkCore;
using WoundCareApi.src.Core.Domain.CRS;
using WoundCareApi.src.Infrastructure.Persistence;

namespace WoundCareApi.API.Services;

public class RoleService
{
    private readonly CRSDbContext _context;

    public RoleService(CRSDbContext context)
    {
        _context = context;
    }


    public async Task<List<RoleFunction>> GetUserRoleList(string userId)
    {
        // 檢索用戶角色列表
        var roleListString = await _context.LoginUserData
            .Where(x => x.UserID == userId)
            .Select(x => x.RoleList)
            .FirstOrDefaultAsync();

        if (roleListString == null) return new List<RoleFunction>();

        // 避免在表達式樹中調用帶有可選參數的方法
        var roleList = roleListString.Split(new[] { ',' }).ToList();

        // 通過與 FunctionRoleGroups 進行連接，直接查詢 RoleFunctions
        var functionList = await (from frg in _context.FunctionRoleGroups
            join rf in _context.RoleFunctions on frg.FunctionName equals rf.FunctionName
            where roleList.Contains(frg.RoleName)
            select rf).ToListAsync();


        return functionList;
    }
}