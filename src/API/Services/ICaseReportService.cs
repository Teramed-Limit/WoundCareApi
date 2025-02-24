using WoundCareApi.API.DTOs;
using WoundCareApi.src.Core.Domain.CRS;

namespace WoundCareApi.API.Services;

public interface ICaseReportService
{
    Task<CRS_CaseRecord> GetReportByIdAsync(Guid reportId);
    Task<CRS_CaseRecord> InsertReportAsync(CaseReportDTO reportDto);
    Task UpdateReportAsync(Guid reportId, CaseReportDTO reportDto);
}
