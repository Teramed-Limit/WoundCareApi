using System.Globalization;
using WoundCareApi.API.DTOs;
using WoundCareApi.Common.Types;
using WoundCareApi.Persistence.Repository;
using WoundCareApi.Persistence.UnitOfWork;
using WoundCareApi.src.Core.Domain.CRS;
using WoundCareApi.src.Infrastructure.Persistence;

namespace WoundCareApi.API.Services;

public class CaseReportService : ICaseReportService
{
    private readonly ILogger<CaseReportService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ShiftTimeService _shiftTimeService;
    private readonly IRepository<CRS_CaseRecord, CRSDbContext> _repository;
    private readonly UnitShiftService _unitShiftService;

    public CaseReportService(
        ILogger<CaseReportService> logger,
        IUnitOfWork unitOfWork,
        ShiftTimeService shiftTimeService,
        UnitShiftService unitShiftService,
        IRepository<CRS_CaseRecord, CRSDbContext> repository
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _shiftTimeService = shiftTimeService;
        _unitShiftService = unitShiftService;
        _repository = repository;
    }

    public async Task<CRS_CaseRecord> GetReportByIdAsync(Guid reportId)
    {
        var report = await _repository.GetByConditionAsync(x => x.Puid == reportId);

        if (report == null)
            throw new InvalidOperationException("無法找到報告");

        return report.FirstOrDefault();
    }

    public async Task<CRS_CaseRecord> InsertReportAsync(CaseReportDTO reportDto)
    {
        // 解析日期時間並獲取觀察時間
        var (observationDateTime, observationShiftDate, clinicalUnitShiftPuid) =
            await ProcessDateTimeAndShiftInfoAsync(reportDto);

        // 創建報告記錄
        var newReport = CreateReportRecord(
            reportDto,
            observationDateTime,
            observationShiftDate,
            clinicalUnitShiftPuid
        );

        // 保存報告
        await SaveReportAsync(newReport);

        return newReport;
    }

    public async Task UpdateReportAsync(Guid reportId, CaseReportDTO reportDto)
    {
        var updateReport = new CRS_CaseRecord
        {
            Puid = reportId,
            FormData = SerializeFormData(reportDto.FormData),
            FormDefinePuid = reportDto.FormDefinePuid
        };

        await _repository.UpdatePartialAsync(updateReport, "Puid", "FormData", "FormDefinePuid");
        await _unitOfWork.SaveAsync();
    }

    private async Task<(
        DateTime ObservationDateTime,
        DateTime? ObservationShiftDate,
        Guid? ClinicalUnitShiftPuid
    )> ProcessDateTimeAndShiftInfoAsync(CaseReportDTO reportDto)
    {
        // 解析案例日期時間
        DateTime caseDateTime = ParseCaseDateTime(reportDto.CaseDateTime);

        // 默認使用CaseDateTime作為observationDateTime
        DateTime observationDateTime = caseDateTime;
        DateTime? observationShiftDate = null;
        Guid? clinicalUnitShiftPuid = null;

        // 獲取班別資訊
        var shiftInfo = await GetShiftInfoAsync(reportDto.ClinicalUnitPuid, observationDateTime);

        // 設置班別相關資訊
        if (shiftInfo != null)
        {
            clinicalUnitShiftPuid = shiftInfo.ClinicalUnitShiftPuid;

            // 設置臨床日期作為ObservationShiftDate
            if (!string.IsNullOrEmpty(shiftInfo.ClinicalDate))
            {
                observationShiftDate = DateTime.Parse(shiftInfo.ClinicalDate);
            }
        }
        else
        {
            throw new InvalidOperationException("無法根據提供的時間和臨床單位計算班別資訊");
        }

        return (observationDateTime, observationShiftDate, clinicalUnitShiftPuid);
    }

    private DateTime ParseCaseDateTime(string dateTimeString)
    {
        if (
            !DateTime.TryParseExact(
                dateTimeString,
                "yyyyMMddHHmmss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime caseDateTime
            )
        )
        {
            throw new ArgumentException("Invalid CaseDateTime format");
        }

        return caseDateTime;
    }

    private async Task<ShiftTimeResult?> GetShiftInfoAsync(
        Guid clinicalUnitPuid,
        DateTime observationDateTime
    )
    {
        // 獲取指定的臨床單位
        var clinicalUnits = await _unitShiftService.GetUnitByPuid(clinicalUnitPuid);

        // 獲取與該臨床單位相關的班別
        var shifts = await _unitShiftService.GetUnitShiftsByPuid(clinicalUnitPuid);

        // 計算班別時間資訊，指定臨床單位PUID
        var shiftInfo = _shiftTimeService.DetermineShiftAndTime(
            observationDateTime,
            clinicalUnits.ToList(),
            shifts.ToList(),
            clinicalUnitPuid
        );

        return shiftInfo;
    }

    private CRS_CaseRecord CreateReportRecord(
        CaseReportDTO reportDto,
        DateTime observationDateTime,
        DateTime? observationShiftDate,
        Guid? clinicalUnitShiftPuid
    )
    {
        return new CRS_CaseRecord
        {
            Puid = Guid.NewGuid(),
            PtCasePuid = reportDto.CaseId,
            FormData = SerializeFormData(reportDto.FormData),
            FormDefinePuid = reportDto.FormDefinePuid,
            ObservationDateTime = observationDateTime,
            ObservationShiftDate = observationShiftDate,
            ClinicalUnitShiftPuid = clinicalUnitShiftPuid,
            StoreTime = DateTime.Now
        };
    }

    private string SerializeFormData(object formData)
    {
        return System.Text.Json.JsonSerializer.Serialize(formData);
    }

    private async Task SaveReportAsync(CRS_CaseRecord report)
    {
        await _repository.AddAsync(report);
        await _unitOfWork.SaveAsync();
    }
}
