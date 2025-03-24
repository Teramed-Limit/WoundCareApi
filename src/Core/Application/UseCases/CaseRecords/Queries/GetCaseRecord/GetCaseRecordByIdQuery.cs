using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WoundCareApi.Application.DTOs;
using WoundCareApi.Core.Application.Common.Models;
using WoundCareApi.Infrastructure.Persistence;

namespace WoundCareApi.Core.Application.UseCases.CaseRecords.Queries.GetCaseRecord;

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
                from caseRecord in _context.CRS_CaseRecords
                join caseRecordFormDefine in _context.ReportDefines
                    on caseRecord.FormDefinePuid equals caseRecordFormDefine.Puid
                join clinicalUnitShift in _context.CRS_SysClinicalUnitShifts
                    on caseRecord.ClinicalUnitShiftPuid equals clinicalUnitShift.Puid
                    into clinicalShiftGroup
                from clinicalUnitShift in clinicalShiftGroup.DefaultIfEmpty()
                where caseRecord.Puid == request.ReportId
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
                    RawFormData = caseRecord.FormData,
                };

            var results = await query
                .OrderBy(x => x.ObservationDateTime)
                .ToListAsync(cancellationToken);

            if (results.Count == 0)
            {
                return Result<CaseRecordDto>.Failure($"找不到 ID 為 {request.ReportId} 的報告");
            }

            var record = results.First();

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

            return Result<CaseRecordDto>.Success(record);
        }
        catch (Exception ex)
        {
            return Result<CaseRecordDto>.Failure(ex.Message);
        }
    }
}
