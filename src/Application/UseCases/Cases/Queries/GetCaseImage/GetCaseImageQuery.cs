using System.Globalization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WoundCareApi.Application.Common.Results;
using WoundCareApi.Application.DTOs;
using WoundCareApi.Infrastructure.Persistence;

namespace WoundCareApi.Application.UseCases.Cases.Queries.GetCaseImage;

public record GetCaseImageQuery(string CaseId, string Date, bool IsUsingShiftDate)
    : IRequest<Result<IEnumerable<CaseImageDto>>>;

public class GetCaseImageQueryHandler
    : IRequestHandler<GetCaseImageQuery, Result<IEnumerable<CaseImageDto>>>
{
    private readonly CRSDbContext _context;
    private readonly IConfiguration _configuration;

    public GetCaseImageQueryHandler(CRSDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<Result<IEnumerable<CaseImageDto>>> Handle(
        GetCaseImageQuery request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var date = DateTime.ParseExact(request.Date, "yyyyMMdd", CultureInfo.InvariantCulture);
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
                    caseSeriesMap.PtCasePuid == Guid.Parse(request.CaseId)
                    && (
                        !request.IsUsingShiftDate
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

            var images = await query.ToListAsync(cancellationToken);

            return Result<IEnumerable<CaseImageDto>>.Success(images);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<CaseImageDto>>.Failure($"獲取案例圖片失敗: {ex.Message}");
        }
    }
}
