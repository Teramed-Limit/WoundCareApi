using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoundCareApi.API.Controllers.Base;
using WoundCareApi.API.DTOs;
using WoundCareApi.API.Services;

namespace WoundCareApi.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CaseRecordController : BaseController
{
    private readonly ICaseReportService _caseReportService;

    public CaseRecordController(
        ILogger<CaseRecordController> logger,
        ICaseReportService caseReportService
    )
        : base(logger)
    {
        _caseReportService = caseReportService;
    }

    [Authorize(AuthenticationSchemes = "Bearer,ApiKey")]
    [HttpGet("reportId/{reportId}")]
    public async Task<ActionResult<CaseRecordDto>> GetReport(string reportId)
    {
        if (!Guid.TryParse(reportId, out Guid reportGuid))
        {
            return BadRequest("無效的報告 ID 格式");
        }

        try
        {
            var result = await _caseReportService.GetReportByIdAsync(reportGuid);
            if (result == null)
            {
                return BadRequest($"找不到 ID 為 {reportId} 的報告");
            }
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    [HttpPost]
    public async Task<ActionResult<CaseRecordDto>> InsertReport([FromBody] CaseReportDTO reportDto)
    {
        try
        {
            var userId = GetUserId();
            var newReportId = await _caseReportService.InsertReportAsync(reportDto, userId);
            var newReport = await _caseReportService.GetReportByIdAsync(newReportId.Puid);
            return CreatedAtAction(
                nameof(GetReport),
                new { reportId = newReportId.ToString() },
                newReport
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during InsertReport");
            return InternalServerError(ex);
        }
    }

    [HttpPut("reportId/{reportId}")]
    public async Task<ActionResult> UpdateReport(
        string reportId,
        [FromBody] CaseReportDTO reportDto
    )
    {
        if (!Guid.TryParse(reportId, out Guid reportGuid))
        {
            return BadRequest("無效的報告 ID 格式");
        }

        if (reportDto == null)
        {
            return BadRequest("報告資料為必填");
        }

        try
        {
            var userId = GetUserId();
            await _caseReportService.UpdateReportAsync(reportGuid, reportDto, userId);
            return Success("報告更新成功");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during UpdateReport");
            return InternalServerError(ex);
        }
    }
}
