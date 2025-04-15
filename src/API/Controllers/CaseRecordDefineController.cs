using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeraLinkaCareApi.Application.DTOs;
using TeraLinkaCareApi.Core.Domain.Entities;
using TeraLinkaCareApi.Core.Domain.Interfaces;
using TeraLinkaCareApi.Infrastructure.Persistence;
using TeraLinkaCareApi.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace TeraLinkaCareApi.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CaseRecordDefineController : ControllerBase
{
    private readonly ILogger<CaseRecordDefineController> _logger;
    private readonly IRepository<ReportDefine, CRSDbContext> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CaseRecordDefineController(
        ILogger<CaseRecordDefineController> logger,
        IRepository<ReportDefine, CRSDbContext> repository,
        IUnitOfWork unitOfWork
    )
    {
        _logger = logger;
        _repository = repository;
        _unitOfWork = unitOfWork;
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
            var report = await _repository.GetByIdAsync(Guid.Parse(reportId));
            if (report == null)
            {
                return BadRequest("找不到最新的報告定義");
            }

            var reportDto = ToReportDefineDto(report);

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
    [HttpGet("report/{report}/latest")]
    public async Task<ActionResult<ReportDefineDto>> GetReportDefineLatest(string report)
    {
        try
        {
            var reportDefine = await _repository.GetOneByConditionAsync(
                x => x.ReportName == report,
                r => r.CreateDateTime,
                true
            );
            var reportDto = ToReportDefineDto(reportDefine);

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
    /// 獲取所有的報告定義
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet("all/latest")]
    public async Task<ActionResult<Dictionary<string, ReportDefineDto>>> GetReportDefineList()
    {
        try
        {
            var reportLatestListDto = _repository
                .GetDBSet()
                .AsNoTracking()
                .OrderByDescending(x => x.CreateDateTime)
                .Select(ToReportDefineDto)
                .GroupBy(x => x.ReportName)
                .ToDictionary(g => g.Key, g => g.First());

            return Ok(reportLatestListDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all report define");
            return StatusCode(500, "獲取所有報告定義時發生錯誤");
        }
    }

    /// <summary>
    /// 儲存報告定義
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost("newReport")]
    public async Task<ActionResult<Dictionary<string, ReportDefineDto>>> NewReportDefine(
        [FromBody] ReportDefineDto reportDefineDto
    )
    {
        try
        {
            await _repository.AddAsync(
                new ReportDefine()
                {
                    Puid = Guid.NewGuid(),
                    ReportName = reportDefineDto.ReportName,
                    FormDefine = JsonSerializer.Serialize(
                        new
                        {
                            headerDefine = reportDefineDto.HeaderDefine,
                            formDefine = reportDefineDto.FormDefine,
                            footerDefine = reportDefineDto.FooterDefine
                        }
                    ),
                    CreateDateTime = DateTime.Now
                }
            );
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        catch (FormatException ex)
        {
            _logger.LogError(ex, "Error occurred while saving report define");
            return StatusCode(500, "儲存報告定義時發生錯誤");
        }
    }

    /// <summary>
    /// 將 ReportDefine 轉換為 ReportDefineDTO
    /// </summary>
    private static ReportDefineDto ToReportDefineDto(ReportDefine reportDefine)
    {
        Dictionary<string, object> formDefine;
        Dictionary<string, object> headerDefine = new Dictionary<string, object>();
        Dictionary<string, object> footerDefine = new Dictionary<string, object>();

        try
        {
            var fullDefine = JsonSerializer.Deserialize<
                Dictionary<string, Dictionary<string, object>>
            >(reportDefine.FormDefine);
            if (fullDefine != null)
            {
                fullDefine.TryGetValue("headerDefine", out headerDefine);
                fullDefine.TryGetValue("formDefine", out formDefine);
                fullDefine.TryGetValue("footerDefine", out footerDefine);
            }
            else
            {
                formDefine = new Dictionary<string, object>();
            }
        }
        catch (JsonException)
        {
            formDefine = new Dictionary<string, object>();
        }

        return new ReportDefineDto
        {
            Puid = reportDefine.Puid,
            FormDefine = formDefine,
            HeaderDefine = headerDefine,
            FooterDefine = footerDefine,
            ReportName = reportDefine.ReportName,
            CreateDateTime = reportDefine.CreateDateTime
        };
    }
}
