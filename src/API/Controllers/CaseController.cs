using Microsoft.AspNetCore.Mvc;
using WoundCareApi.API.DTOs;
using WoundCareApi.API.Services;
using WoundCareApi.src.Core.Domain.CRS;

namespace WoundCareApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CaseController : ControllerBase
{
    private readonly ILogger<CaseController> _logger;
    private readonly CaseService _caseService;

    public CaseController(ILogger<CaseController> logger, CaseService caseService)
    {
        _logger = logger;
        _caseService = caseService;
    }

    [HttpGet("patientId/{patientId}/encounterId/{encounterId}/cases")]
    public async Task<ActionResult<Dictionary<string, CRS_Case>>> GetByPatientIdAndEncounterId(
        string patientId,
        string encounterId
    )
    {
        var res = await _caseService.GetCasesByBodyPart(patientId, encounterId);
        return Ok(res);
    }

    [HttpGet("caseId/{caseId}/caseHistory")]
    public async Task<ActionResult<CaseHistoryDto>> GetCaseHistory(string caseId)
    {
        var res = await _caseService.GetCaseHistory(caseId);
        return Ok(res);
    }

    [HttpGet("caseId/{caseId}/date/{date}/caseRecords")]
    public async Task<ActionResult<IEnumerable<CaseRecordDto>>> GetCaseRecord(
        string caseId,
        string date,
        [FromQuery] bool isUsingShiftDate = false
    )
    {
        var res = await _caseService.GetCaseRecord(caseId, date, isUsingShiftDate);
        return Ok(res);
    }

    [HttpGet("caseId/{caseId}/date/{date}/images")]
    public async Task<ActionResult<IEnumerable<CaseImageDto>>> GetCaseImage(
        string caseId,
        string date,
        [FromQuery] bool isUsingShiftDate = false
    )
    {
        var res = await _caseService.GetCaseImage(caseId, date, isUsingShiftDate);
        return Ok(res);
    }
}
