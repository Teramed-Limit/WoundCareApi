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

    public async Task<Dictionary<string, List<CaseHistoryDto>>> GetCaseHistory(string caseId)
    {
        var query =
            from caseItem in _context.CRS_Cases
            join caseSeriesMap in _context.CRS_CareSeriesMaps
                on caseItem.Puid equals caseSeriesMap.PtCasePuid
            join series in _context.DicomSeries
                on caseSeriesMap.DicomSeriesUid equals series.SeriesInstanceUID
            where caseItem.Puid == Guid.Parse(caseId)
            select new CaseHistoryDto()
            {
                CaseId = caseItem.Puid,
                SeriesInstanceUid = series.SeriesInstanceUID.Trim(),
                SeriesDate = series.SeriesDate.Trim(),
                SeriesTime = series.SeriesTime.Trim()
            };

        return await query
            .GroupBy(c => c.SeriesDate.Trim())
            .ToDictionaryAsync(
                g => g.Key.Trim(),
                g => g.OrderBy(x => x.SeriesDate.Trim()).ThenBy(x => x.SeriesTime).ToList()
            );
    }

    public async Task<IEnumerable<CaseRecordDto>> GetCaseRecord(string caseId, string dateStr)
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
                && caseRecord.ObservationDateTime >= date
                && caseRecord.ObservationDateTime < nextDate
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

    public async Task<IEnumerable<CaseImageDto>> GetCaseImage(string caseId, string dateStr)
    {
        var date = DateTime.ParseExact(dateStr, "yyyyMMdd", CultureInfo.InvariantCulture);
        var nextDate = date.AddDays(1);

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
                && caseSeriesMap.DicomSeriesShiftDate >= date
                && caseSeriesMap.DicomSeriesShiftDate < nextDate
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