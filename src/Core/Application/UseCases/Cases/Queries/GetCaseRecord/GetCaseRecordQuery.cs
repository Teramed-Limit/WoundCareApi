using System.Globalization;
using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WoundCareApi.Application.DTOs;
using WoundCareApi.Core.Application.Common.Models;
using WoundCareApi.Infrastructure.Persistence;

namespace WoundCareApi.Core.Application.UseCases.Cases.Queries.GetCaseRecord;

public record GetCaseRecordQuery(string CaseId, string Date, bool IsUsingShiftDate)
    : IRequest<Result<IEnumerable<CaseRecordDto>>>;

public class GetCaseRecordQueryHandler
    : IRequestHandler<GetCaseRecordQuery, Result<IEnumerable<CaseRecordDto>>>
{
    private readonly CRSDbContext _context;

    public GetCaseRecordQueryHandler(CRSDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<CaseRecordDto>>> Handle(
        GetCaseRecordQuery request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            if (!Guid.TryParse(request.CaseId, out Guid caseGuid))
            {
                return Result<IEnumerable<CaseRecordDto>>.Failure("無效的案例ID格式");
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
                return Result<IEnumerable<CaseRecordDto>>.Failure("無效的日期格式");
            }

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

            return Result<IEnumerable<CaseRecordDto>>.Success(results);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<CaseRecordDto>>.Failure($"獲取案例記錄失敗: {ex.Message}");
        }
    }
}
