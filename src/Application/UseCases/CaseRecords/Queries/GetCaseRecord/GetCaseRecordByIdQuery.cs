using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeraLinkaCareApi.Application.Common.Results;
using TeraLinkaCareApi.Application.DTOs;
using TeraLinkaCareApi.Infrastructure.Persistence;

namespace TeraLinkaCareApi.Application.UseCases.CaseRecords.Queries.GetCaseRecord;

public record GetCaseRecordByIdQuery(Guid ReportId) : IRequest<Result<CaseRecordDto>>;

public class GetCaseRecordByIdQueryHandler
    : IRequestHandler<GetCaseRecordByIdQuery, Result<CaseRecordDto>>
{
    private readonly CRSDbContext _context;

    public GetCaseRecordByIdQueryHandler(CRSDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CaseRecordDto>> Handle(
        GetCaseRecordByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var query =
                from caseRecord in _context.PtCaseRecords
                join caseRecordFormDefine in _context.ReportDefines
                    on caseRecord.FormDefinePuid equals caseRecordFormDefine.Puid
                where caseRecord.Puid == request.ReportId
                select new CaseRecordDto()
                {
                    Puid = caseRecord.Puid,
                    FormDefinePuid = caseRecord.FormDefinePuid,
                    ObservationDateTime = caseRecord.ObservationDateTime,
                    FormDefine = null,
                    HeaderDefine = null,
                    FooterDefine = null,
                    FormData = null,
                    RawFormDefine = caseRecordFormDefine.FormDefine,
                    RawFormData = caseRecord.FormData,
                    ObservationDateTimeString =
                        caseRecord.ObservationDateTime != null
                            ? caseRecord.ObservationDateTime.Value.ToString("yyyy/MM/dd HH:mm:ss")
                            : null,
                    ObservationShiftDateString =
                        caseRecord.ObservationShiftDate != null
                            ? caseRecord.ObservationShiftDate.Value.ToString("yyyy/MM/dd")
                            : null,
                };

            var results = await query
                .OrderBy(x => x.ObservationDateTime)
                .ToListAsync(cancellationToken);

            if (results.Count == 0)
            {
                return Result<CaseRecordDto>.Failure($"找不到 ID 為 {request.ReportId} 的報告");
            }

            var record = results.First();

            Dictionary<string, object> formDefine = new Dictionary<string, object>();
            Dictionary<string, object> headerDefine = new Dictionary<string, object>();
            Dictionary<string, object> footerDefine = new Dictionary<string, object>();

            try
            {
                var fullDefine = JsonSerializer.Deserialize<
                    Dictionary<string, Dictionary<string, object>>
                >(record.RawFormDefine);
                if (fullDefine != null)
                {
                    fullDefine.TryGetValue("headerDefine", out headerDefine);
                    fullDefine.TryGetValue("formDefine", out formDefine);
                    fullDefine.TryGetValue("footerDefine", out footerDefine);
                }
            }
            catch (JsonException)
            {
                formDefine = new Dictionary<string, object>();
                headerDefine = new Dictionary<string, object>();
                footerDefine = new Dictionary<string, object>();
            }

            if (!string.IsNullOrEmpty(record.RawFormDefine))
            {
                record.FormDefine = formDefine;
                record.HeaderDefine = headerDefine;
                record.FooterDefine = footerDefine;
            }

            if (!string.IsNullOrEmpty(record.RawFormData))
            {
                record.FormData = JsonSerializer.Deserialize<Dictionary<string, object>>(
                    record.RawFormData
                );
            }

            return Result<CaseRecordDto>.Success(record);
        }
        catch (Exception ex)
        {
            return Result<CaseRecordDto>.Failure(ex.Message);
        }
    }
}
