using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using WoundCareApi.API.DTOs;
using WoundCareApi.API.Services;
using WoundCareApi.Persistence.Repository;
using WoundCareApi.Persistence.UnitOfWork;
using WoundCareApi.src.Core.Domain.CRS;
using WoundCareApi.src.Infrastructure.Persistence;

namespace WoundCareApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CaseReportController : ControllerBase
{
    private readonly ILogger<CaseReportController> _logger;
    private readonly ICaseReportService _caseReportService;

    public CaseReportController(
        ILogger<CaseReportController> logger,
        ICaseReportService caseReportService
    )
    {
        _logger = logger;
        _caseReportService = caseReportService;
    }

    [HttpGet("reportId/{reportId}")]
    public async Task<ActionResult<CRS_CaseRecord>> GetReport(string reportId)
    {
        try
        {
            if (!Guid.TryParse(reportId, out Guid reportGuid))
            {
                return BadRequest("Invalid report ID format");
            }

            var result = await _caseReportService.GetReportByIdAsync(reportGuid);

            if (result == null)
            {
                return NotFound($"Report with ID {reportId} not found");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting report {ReportId}", reportId);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpPost]
    public async Task<ActionResult> InsertReport([FromBody] CaseReportDTO reportDto)
    {
        try
        {
            var newReport = await _caseReportService.InsertReportAsync(reportDto);
            return CreatedAtAction(nameof(GetReport), new { reportId = newReport.Puid }, newReport);
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
            _logger.LogError(ex, "Error occurred while inserting new report");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpPut("reportId/{reportId}")]
    public async Task<ActionResult> UpdateReport(
        string reportId,
        [FromBody] CaseReportDTO reportDto
    )
    {
        try
        {
            if (!Guid.TryParse(reportId, out Guid reportGuid))
            {
                return BadRequest("Invalid report ID format");
            }

            if (reportDto == null)
            {
                return BadRequest("Report data is required");
            }

            await _caseReportService.UpdateReportAsync(reportGuid, reportDto);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating report {ReportId}", reportId);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
}
