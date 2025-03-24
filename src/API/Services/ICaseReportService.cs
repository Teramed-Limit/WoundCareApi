using WoundCareApi.API.DTOs;
using WoundCareApi.src.Core.Domain.CRS;

namespace WoundCareApi.API.Services;

public interface ICaseReportService
{
    Task<CaseRecordDto> GetReportByIdAsync(Guid reportId);
    Task<CaseRecordDto> InsertReportAsync(CaseReportDTO reportDto);
    Task UpdateReportAsync(Guid reportId, CaseReportDTO reportDto);
}
