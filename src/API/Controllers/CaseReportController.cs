using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using WoundCareApi.API.DTOs;
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
    private readonly IRepository<CRS_CaseRecord, CRSDbContext> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CaseReportController(
        ILogger<CaseReportController> logger,
        IRepository<CRS_CaseRecord, CRSDbContext> repository,
        IUnitOfWork unitOfWork
    )
    {
        _logger = logger;
        _repository = repository;
        _unitOfWork = unitOfWork;
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

            var report = await _repository.GetByConditionAsync(x => x.Puid == reportGuid);
            var result = report.FirstOrDefault();

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
            if (reportDto == null)
            {
                return BadRequest("Report data is required");
            }

            var newReport = new CRS_CaseRecord
            {
                Puid = Guid.NewGuid(),
                PtCasePuid = reportDto.CaseId,
                FormData = System.Text.Json.JsonSerializer.Serialize(reportDto.FormData),
                FormDefinePuid = reportDto.FormDefinePuid,
                ObservationDateTime = DateTime.ParseExact(
                    reportDto.CaseDate,
                    "yyyyMMdd",
                    CultureInfo.InvariantCulture
                ),
                ObservationShiftDate = DateTime.ParseExact(
                    reportDto.CaseDate,
                    "yyyyMMdd",
                    CultureInfo.InvariantCulture
                ),
                StoreTime = DateTime.Now
            };

            await _repository.AddAsync(newReport);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(GetReport), new { reportId = newReport.Puid }, newReport);
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

            var updateReport = new CRS_CaseRecord
            {
                Puid = reportGuid,
                FormData = System.Text.Json.JsonSerializer.Serialize(reportDto.FormData),
                FormDefinePuid = reportDto.FormDefinePuid
            };

            await _repository.UpdatePartialAsync(
                updateReport,
                "Puid",
                "FormData",
                "FormDefinePuid"
            );
            await _unitOfWork.SaveAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating report {ReportId}", reportId);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
}
