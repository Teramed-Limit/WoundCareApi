using System.Globalization;
using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WoundCareApi.API.DTOs;
using WoundCareApi.src.Infrastructure.Persistence;

namespace WoundCareApi.API.Services;

public class CaseService
{
    private readonly CRSDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public CaseService(CRSDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    // 將Cases依照bodyPart分類
    public async Task<Dictionary<string, List<CaseDto>>> GetCasesByBodyPart(
        string patientId,
        string encounterId
    )
    {
        var query =
            from caseItem in _context.CRS_Cases
            join caseType in _context.CRS_CfgCaseTypes on caseItem.CaseTypePuid equals caseType.Puid
            join bodyLocation in _context.CRS_CfgBodyLocations
                on caseItem.CaseLocation equals bodyLocation.NISLocationLabel
            where caseItem.LIfeTimeNumber == patientId && caseItem.EncounterNumber == encounterId
            select new
            {
                Case = caseItem,
                LocationLabel = bodyLocation.NISLocationLabel,
                LocationSVGId = bodyLocation.SVGGraphicId,
                CasetypeShortLabel = caseType.CaseTypeShortLabel
            };

        var results = await query.ToListAsync();

        var caseDtos = results.Select(r =>
        {
            var dto = _mapper.Map<CaseDto>(r.Case);
            dto.LocationLabel = r.LocationLabel;
            dto.LocationSVGId = r.LocationSVGId;
            dto.CaseTypeShortLabel = r.CasetypeShortLabel;
            return dto;
        });

        return caseDtos
            .GroupBy(c => c.LocationSVGId ?? "Unknown")
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    public async Task<CaseHistoryDto> GetCaseHistory(string caseId)
    {
        var parsedCaseId = Guid.Parse(caseId);

        // 分別查詢記錄日期和系列日期
        var recordDates = await _context.CRS_CaseRecords
            .Where(r => r.PtCasePuid == parsedCaseId)
            .Select(
                r =>
                    new
                    {
                        ObservationDate = r.ObservationDateTime.HasValue
                            ? r.ObservationDateTime.Value.ToString("yyyyMMdd")
                            : null,
                        ShiftDate = r.ObservationShiftDate.HasValue
                            ? r.ObservationShiftDate.Value.ToString("yyyyMMdd")
                            : null
                    }
            )
            .ToListAsync();

        var seriesDates = await _context.CRS_CareSeriesMaps
            .Where(s => s.PtCasePuid == parsedCaseId)
            .Select(
                s =>
                    new
                    {
                        SeriesDate = !string.IsNullOrEmpty(s.DicomSeriesDate)
                            ? s.DicomSeriesDate.Trim()
                            : null,
                        ShiftDate = s.DicomSeriesShiftDate.HasValue
                            ? s.DicomSeriesShiftDate.Value.ToString("yyyyMMdd")
                            : null
                    }
            )
            .ToListAsync();

        // 使用 HashSet 來收集並去重日期
        var allDates = new HashSet<string>();
        var allShiftDates = new HashSet<string>();

        // 處理記錄日期
        foreach (var record in recordDates)
        {
            if (!string.IsNullOrEmpty(record.ObservationDate))
            {
                allDates.Add(record.ObservationDate);
            }
            if (!string.IsNullOrEmpty(record.ShiftDate))
            {
                allShiftDates.Add(record.ShiftDate);
            }
        }

        // 處理系列日期
        foreach (var series in seriesDates)
        {
            if (!string.IsNullOrEmpty(series.SeriesDate))
            {
                allDates.Add(series.SeriesDate);
            }
            if (!string.IsNullOrEmpty(series.ShiftDate))
            {
                allShiftDates.Add(series.ShiftDate);
            }
        }

        // 返回合併後的結果
        return new CaseHistoryDto
        {
            CaseId = parsedCaseId,
            CaseDate = allDates.OrderBy(x => x).ToArray(),
            CaseShiftDate = allShiftDates.OrderBy(x => x).ToArray()
        };
    }

    public async Task<IEnumerable<CaseRecordDto>> GetCaseRecord(
        string caseId,
        string dateStr,
        bool isUsingShiftDate = false
    )
    {
        var date = DateTime.ParseExact(dateStr, "yyyyMMdd", CultureInfo.InvariantCulture);
        var nextDate = date.AddDays(1);

        var query =
            from caseRecord in _context.CRS_CaseRecords
            join caseRecordFormDefine in _context.ReportDefines
                on caseRecord.FormDefinePuid equals caseRecordFormDefine.Puid
            join clinicalUnitShift in _context.CRS_SysClinicalUnitShifts
                on caseRecord.ClinicalUnitShiftPuid equals clinicalUnitShift.Puid
                into clinicalShiftGroup
            from clinicalUnitShift in clinicalShiftGroup.DefaultIfEmpty()
            where
                caseRecord.PtCasePuid == Guid.Parse(caseId)
                && (
                    !isUsingShiftDate
                        ? (
                            caseRecord.ObservationDateTime >= date
                            && caseRecord.ObservationDateTime < nextDate
                        )
                        : (
                            caseRecord.ObservationShiftDate >= date
                            && caseRecord.ObservationShiftDate < nextDate
                        )
                )
            select new CaseRecordDto()
            {
                Puid = caseRecord.Puid,
                ObservationDateTime = caseRecord.ObservationDateTime,
                ObservationShiftDate = caseRecord.ObservationShiftDate,
                ShiftLongLabel = clinicalUnitShift.ShiftLongLabel,
                ShiftShortLabel = clinicalUnitShift.ShiftShortLabel,
                FormDefinePuid = caseRecord.FormDefinePuid,
                FormDefine = null,
                FormData = null,
                RawFormDefine = caseRecordFormDefine.FormDefine,
                RawFormData = caseRecord.FormData
            };

        var results = await query.OrderBy(x => x.ObservationDateTime).ToListAsync();

        foreach (var record in results)
        {
            if (!string.IsNullOrEmpty(record.RawFormDefine))
            {
                record.FormDefine = JsonSerializer.Deserialize<Dictionary<string, object>>(
                    record.RawFormDefine
                );
            }

            if (!string.IsNullOrEmpty(record.RawFormData))
            {
                record.FormData = JsonSerializer.Deserialize<Dictionary<string, object>>(
                    record.RawFormData
                );
            }
        }

        return results;
    }

    public async Task<IEnumerable<CaseImageDto>> GetCaseImage(
        string caseId,
        string dateStr,
        bool isUsingShiftDate = false
    )
    {
        var date = DateTime.ParseExact(dateStr, "yyyyMMdd", CultureInfo.InvariantCulture);
        var nextDate = date.AddDays(1);
        var dateString = date.ToString("yyyyMMdd");
        var nextDateString = nextDate.ToString("yyyyMMdd");

        var imagePath = _configuration.GetSection("ImageVirtualPath").Value;
        var query =
            from image in _context.DicomImages
            join caseSeriesMap in _context.CRS_CareSeriesMaps
                on image.SeriesInstanceUID equals caseSeriesMap.DicomSeriesUid
            join clinicalUnitShift in _context.CRS_SysClinicalUnitShifts
                on caseSeriesMap.DicomSeriesClinicalUnitShiftPuid equals clinicalUnitShift.Puid
                into clinicalShiftGroup
            from clinicalUnitShift in clinicalShiftGroup.DefaultIfEmpty()
            where
                caseSeriesMap.PtCasePuid == Guid.Parse(caseId)
                && (
                    !isUsingShiftDate
                        ? (
                            caseSeriesMap.DicomSeriesDate.CompareTo(dateString) >= 0
                            && caseSeriesMap.DicomSeriesDate.CompareTo(nextDateString) < 0
                        )
                        : (
                            caseSeriesMap.DicomSeriesShiftDate >= date
                            && caseSeriesMap.DicomSeriesShiftDate < nextDate
                        )
                )
            select new CaseImageDto()
            {
                CaseId = caseSeriesMap.PtCasePuid.ToString(),
                SopInstanceUID = image.SOPInstanceUID.Trim(),
                ImageNumber = image.ImageNumber.Trim(),
                ImageDate = image.ImageDate.Trim(),
                ImageTime = image.ImageTime.Trim(),
                FilePath = imagePath + Path.ChangeExtension(image.FilePath.Trim(), ".jpg"),
                SeriesInstanceUID = image.SeriesInstanceUID.Trim(),
                ImageMarker = image.ImageMarker ?? "",
                ShiftDate = caseSeriesMap.DicomSeriesShiftDate,
                ShiftLongLabel = clinicalUnitShift.ShiftLongLabel,
                ShiftShortLabel = clinicalUnitShift.ShiftShortLabel,
            };

        return await query.ToListAsync();
    }
}
