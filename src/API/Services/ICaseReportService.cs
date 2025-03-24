using WoundCareApi.API.DTOs;
using WoundCareApi.src.Core.Domain.CRS;

namespace WoundCareApi.API.Services;

public interface ICaseReportService
{
    Task<CaseRecordDto> GetReportByIdAsync(Guid reportId);
    Task<CaseRecordDto> InsertReportAsync(CaseReportDTO reportDto, string userId);
    Task UpdateReportAsync(Guid reportId, CaseReportDTO reportDto, string userId);
}
