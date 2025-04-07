using Microsoft.EntityFrameworkCore;
using WoundCareApi.Core.Domain.Entities;
using WoundCareApi.Infrastructure.Persistence;
using WoundCareApi.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace WoundCareApi.Core.Repository;

// 存儲庫的泛型實現
public class CodeListRepository : GenericRepository<CodeList, CRSDbContext>
{
    private readonly IUnitOfWork _unitOfWork;

    public CodeListRepository(CRSDbContext context, IUnitOfWork unitOfWork)
        : base(context)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<string>> GetCodeNameList()
    {
        var codeListItems = await _dbSet.GroupBy(c => c.CodeName).ToListAsync();
        return codeListItems.Select(c => c.Key).ToList();
    }

    public async Task<Dictionary<string, List<CodeList>>> GetCodeListOfEachCodeName()
    {
        var codeListItems = await _dbSet.GroupBy(c => c.CodeName).ToListAsync();
        return codeListItems.ToDictionary(c => c.Key, c => c.ToList());
    }

    public async Task<IEnumerable<CodeList>> GetCodeListByCodeName(string codeName)
    {
        return await _dbSet.Where(c => c.CodeName == codeName).ToListAsync();
    }

    public async Task AddCodeName(string codeName)
    {
        var codeList = new CodeList
        {
            CodeName = codeName,
            ParentCodeValue = null,
            OrderingIndex = 0,
            Label = "Option",
            Value = "Option",
        };

        await _dbSet.AddAsync(codeList);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteCodeName(string codeName)
    {
        var codes = await _dbSet.Where(c => c.CodeName == codeName).ToListAsync();
        _dbSet.RemoveRange(codes);
        await _unitOfWork.SaveAsync();
    }
}
