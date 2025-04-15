using MediatR;
using Microsoft.EntityFrameworkCore;
using TeraLinkaCareApi.Application.Common.Results;
using TeraLinkaCareApi.Application.DTOs;
using TeraLinkaCareApi.Infrastructure.Persistence;

namespace TeraLinkaCareApi.Application.UseCases.Cases.Queries.GetCaseHistory;

public record GetCaseHistoryQuery(string CaseId) : IRequest<Result<CaseHistoryDto>>;

public class GetCaseHistoryQueryHandler
    : IRequestHandler<GetCaseHistoryQuery, Result<CaseHistoryDto>>
{
    private readonly CRSDbContext _context;

    public GetCaseHistoryQueryHandler(CRSDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CaseHistoryDto>> Handle(
        GetCaseHistoryQuery request,
        CancellationToken cancellationToken
    )
    {
        if (!Guid.TryParse(request.CaseId, out Guid caseGuid))
        {
            return Result<CaseHistoryDto>.Failure("無效的案例ID格式");
        }

        var parsedCaseId = Guid.Parse(request.CaseId);

        // 分別查詢記錄日期和系列日期
        var recordDates = await _context.PtCaseRecords
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
                            : null,
                    }
            )
            .ToListAsync();

        var seriesDates = await _context.DicomSeriesMaps
            .Where(s => s.PtCasePuid == parsedCaseId)
            .Select(
                s =>
                    new
                    {
                        SeriesUid = s.DicomSeriesUid,
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
        var result = new CaseHistoryDto
        {
            CaseId = parsedCaseId,
            CaseDate = allDates.OrderBy(x => x).ToArray(),
            CaseShiftDate = allShiftDates.OrderBy(x => x).ToArray(),
            IsCaseClosed = false
        };

        return Result<CaseHistoryDto>.Success(result);
    }
}
