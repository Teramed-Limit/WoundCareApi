using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoundCareApi.Application.DTOs;
using WoundCareApi.Core.Domain.Entities;
using WoundCareApi.Core.Domain.Interfaces;
using WoundCareApi.Infrastructure.Persistence;

namespace WoundCareApi.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CaseRecordDefineController : ControllerBase
{
    private readonly ILogger<CaseRecordDefineController> _logger;
    private readonly IRepository<ReportDefine, CRSDbContext> _repository;

    public CaseRecordDefineController(
        ILogger<CaseRecordDefineController> logger,
        IRepository<ReportDefine, CRSDbContext> repository
    )
    {
        _logger = logger;
        _repository = repository;
    }

    /// <summary>
    /// 根據報告ID獲取報告定義
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer,ApiKey")]
    [HttpGet("reportId/{reportId}")]
    public async Task<ActionResult<ReportDefineDto>> GetReportDefine(string reportId)
    {
        try
        {
            var report = await _repository.GetByConditionAsync(x => x.Puid == Guid.Parse(reportId));
            var reportDto = report.Select(ToReportDefineDto).FirstOrDefault();

            if (reportDto == null)
            {
                _logger.LogWarning("Report not found with ID: {ReportId}", reportId);
                return NotFound($"找不到ID為 {reportId} 的報告定義");
            }

            return Ok(reportDto);
        }
        catch (FormatException ex)
        {
            _logger.LogError(ex, "Invalid GUID format for reportId: {ReportId}", reportId);
            return BadRequest("無效的報告ID格式");
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error occurred while getting report define for ID: {ReportId}",
                reportId
            );
            return StatusCode(500, "獲取報告定義時發生錯誤");
        }
    }

    /// <summary>
    /// 獲取最新的報告定義
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer,ApiKey")]
    [HttpGet("latest")]
    public async Task<ActionResult<ReportDefineDto>> GetReportDefineLatest()
    {
        try
        {
            var reportDefine = await _repository.GetByConditionAsync(x => x.isLatest == true);
            var reportDto = reportDefine.Select(ToReportDefineDto).FirstOrDefault();

            if (reportDto == null)
            {
                _logger.LogWarning("No latest report define found");
                return NotFound("找不到最新的報告定義");
            }

            return Ok(reportDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting latest report define");
            return StatusCode(500, "獲取最新報告定義時發生錯誤");
        }
    }

    /// <summary>
    /// 將 ReportDefine 轉換為 ReportDefineDTO
    /// </summary>
    private static ReportDefineDto ToReportDefineDto(ReportDefine reportDefine)
    {
        Dictionary<string, object> formDefine;
        try
        {
            formDefine =
                JsonSerializer.Deserialize<Dictionary<string, object>>(reportDefine.FormDefine)
                ?? new Dictionary<string, object>();
        }
        catch (JsonException)
        {
            formDefine = new Dictionary<string, object>();
        }

        return new ReportDefineDto { Puid = reportDefine.Puid, FormDefine = formDefine };
    }
}
