using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeraLinkaCareApi.Application.Common.Results;
using TeraLinkaCareApi.Application.DTOs;
using TeraLinkaCareApi.Infrastructure.Persistence;

namespace TeraLinkaCareApi.Application.UseCases.Cases.Queries.GetCasesByPatient;

public record GetCasesByPatientQuery(string PatientId, string EncounterId)
    : IRequest<Result<Dictionary<string, List<CaseDto>>>>;

public class GetCasesByPatientQueryHandler
    : IRequestHandler<GetCasesByPatientQuery, Result<Dictionary<string, List<CaseDto>>>>
{
    private readonly CRSDbContext _context;
    private readonly IMapper _mapper;

    public GetCasesByPatientQueryHandler(CRSDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<Dictionary<string, List<CaseDto>>>> Handle(
        GetCasesByPatientQuery request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var query =
                from caseItem in _context.PtCases
                join caseType in _context.CfgCaseTypes on caseItem.CaseTypePuid equals caseType.Puid
                join bodyLocation in _context.CfgBodyLocations
                    on caseItem.CaseLocation equals bodyLocation.NISLocationLabel
                // join caseMap in _context.DicomSeriesMaps on caseItem.Puid equals caseMap.PtCasePuid
                // join dcmSeries in _context.DicomSeries
                //     on caseMap.DicomSeriesUid equals dcmSeries.SeriesInstanceUID
                where
                    caseItem.LIfeTimeNumber == request.PatientId
                    && caseItem.EncounterNumber == request.EncounterId
                select new
                {
                    Case = caseItem,
                    LocationLabel = bodyLocation.NISLocationLabel,
                    LocationSVGId = bodyLocation.SVGGraphicId,
                    CasetypeShortLabel = caseType.CaseTypeShortLabel,
                    // SeriesInsUid = caseMap.DicomSeriesUid,
                    // StudyInsUid = dcmSeries.StudyInstanceUID
                };

            var results = await query.ToListAsync();

            var caseDtos = results.Select(r =>
            {
                var dto = _mapper.Map<CaseDto>(r.Case);
                dto.LocationLabel = r.LocationLabel;
                dto.LocationSVGId = r.LocationSVGId;
                dto.CaseTypeShortLabel = r.CasetypeShortLabel;
                dto.IsCaseClosed = r.Case.CaseCloseTime.HasValue;
                return dto;
            });

            var groupedCases = caseDtos
                .GroupBy(c => c.LocationSVGId ?? "Unknown")
                .ToDictionary(g => g.Key, g => g.ToList());

            return Result<Dictionary<string, List<CaseDto>>>.Success(groupedCases);
        }
        catch (Exception ex)
        {
            return Result<Dictionary<string, List<CaseDto>>>.Failure($"獲取案例失敗: {ex.Message}");
        }
    }
}
