using System.Globalization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeraLinkaCareApi.Application.Common.Results;
using TeraLinkaCareApi.Application.DTOs;
using TeraLinkaCareApi.Infrastructure.Persistence;

namespace TeraLinkaCareApi.Application.UseCases.Cases.Queries.GetCaseRecordDate;

public record GetCaseRecordDateQuery(string CaseId, string Date, bool IsUsingShiftDate)
    : IRequest<Result<IEnumerable<CaseRecordDateDto>>>;

public class GetCaseRecordQueryHandler
    : IRequestHandler<GetCaseRecordDateQuery, Result<IEnumerable<CaseRecordDateDto>>>
{
    private readonly CRSDbContext _context;

    public GetCaseRecordQueryHandler(CRSDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<CaseRecordDateDto>>> Handle(
        GetCaseRecordDateQuery request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            if (!Guid.TryParse(request.CaseId, out Guid caseGuid))
            {
                return Result<IEnumerable<CaseRecordDateDto>>.Failure("無效的案例ID格式");
            }

            if (
                !DateTime.TryParseExact(
                    request.Date,
                    "yyyyMMdd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime date
                )
            )
            {
                return Result<IEnumerable<CaseRecordDateDto>>.Failure("無效的日期格式");
            }

            var nextDate = date.AddDays(1);

            var query =
                from caseRecord in _context.PtCaseRecords
                join clinicalUnitShift in _context.SysClinicalUnitShifts
                    on caseRecord.ClinicalUnitShiftPuid equals clinicalUnitShift.Puid
                    into clinicalShiftGroup
                from clinicalUnitShift in clinicalShiftGroup.DefaultIfEmpty()
                where
                    caseRecord.PtCasePuid == Guid.Parse(request.CaseId)
                    && (
                        !request.IsUsingShiftDate
                            ? (
                                caseRecord.ObservationDateTime >= date
                                && caseRecord.ObservationDateTime < nextDate
                            )
                            : (
                                caseRecord.ObservationShiftDate >= date
                                && caseRecord.ObservationShiftDate < nextDate
                            )
                    )
                select new CaseRecordDateDto()
                {
                    Puid = caseRecord.Puid,
                    ObservationDateTime = caseRecord.ObservationDateTime,
                    ObservationShiftDate = caseRecord.ObservationShiftDate,
                    ShiftLongLabel = clinicalUnitShift.ShiftLongLabel,
                    ShiftShortLabel = clinicalUnitShift.ShiftShortLabel,
                };

            var results = await query.OrderBy(x => x.ObservationDateTime).ToListAsync();
            
            return Result<IEnumerable<CaseRecordDateDto>>.Success(results);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<CaseRecordDateDto>>.Failure($"獲取案例記錄失敗: {ex.Message}");
        }
    }
}
