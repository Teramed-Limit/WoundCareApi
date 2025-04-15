
using WoundCareApi.Core.Domain.Interfaces;
using WoundCareApi.Infrastructure.Persistence;

namespace WoundCareApi.Application.Services;

public class UnitShiftService
{
    private readonly IRepository<SysClinicalUnit, CRSDbContext> _unitRepository;
    private readonly IRepository<SysClinicalUnitShift, CRSDbContext> _unitShiftRepository;

    public UnitShiftService(
        IRepository<SysClinicalUnit, CRSDbContext> unitRepository,
        IRepository<SysClinicalUnitShift, CRSDbContext> unitShiftRepository
    )
    {
        _unitRepository = unitRepository;
        _unitShiftRepository = unitShiftRepository;
    }

    public async Task<IEnumerable<SysClinicalUnit>> GetAllUnits()
    {
        return await _unitRepository.GetAllAsync();
    }

    public async Task<IEnumerable<SysClinicalUnit>> GetUnitByPuid(Guid unitPuid)
    {
        var unit = await _unitRepository.GetByConditionAsync(x => x.Puid == unitPuid);
        if (unit == null)
        {
            throw new Exception("Unit not found");
        }

        return unit;
    }

    public async Task<IEnumerable<SysClinicalUnitShift>> GetUnitShiftsByPuid(Guid unitPuid)
    {
        var unit = await _unitRepository.GetByConditionAsync(x => x.Puid == unitPuid);
        if (unit == null)
        {
            throw new Exception("Unit not found");
        }

        return await _unitShiftRepository.GetByConditionAsync(x => x.ClinicalUnitPuid == unitPuid);
    }
}