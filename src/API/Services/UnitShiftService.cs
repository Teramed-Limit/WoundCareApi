using AutoMapper;
using WoundCareApi.Persistence.Repository;
using WoundCareApi.src.Core.Domain.CRS;
using WoundCareApi.src.Infrastructure.Persistence;

namespace WoundCareApi.API.Services;

public class UnitShiftService
{
    private readonly IRepository<CRS_SysClinicalUnit, CRSDbContext> _unitRepository;
    private readonly IRepository<CRS_SysClinicalUnitShift, CRSDbContext> _unitShiftRepository;

    public UnitShiftService(
        IRepository<CRS_SysClinicalUnit, CRSDbContext> unitRepository,
        IRepository<CRS_SysClinicalUnitShift, CRSDbContext> unitShiftRepository
    )
    {
        _unitRepository = unitRepository;
        _unitShiftRepository = unitShiftRepository;
    }

    public async Task<IEnumerable<CRS_SysClinicalUnit>> GetAllUnits()
    {
        return await _unitRepository.GetAllAsync();
    }

    public async Task<IEnumerable<CRS_SysClinicalUnit>> GetUnitByPuid(Guid unitPuid)
    {
        var unit = await _unitRepository.GetByConditionAsync(x => x.Puid == unitPuid);
        if (unit == null)
        {
            throw new Exception("Unit not found");
        }
        return unit;
    }

    public async Task<IEnumerable<CRS_SysClinicalUnitShift>> GetUnitShiftsByPuid(Guid unitPuid)
    {
        var unit = await _unitRepository.GetByConditionAsync(x => x.Puid == unitPuid);
        if (unit == null)
        {
            throw new Exception("Unit not found");
        }
        return await _unitShiftRepository.GetByConditionAsync(x => x.ClinicalUnitPuid == unitPuid);
    }
}
