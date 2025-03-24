using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoundCareApi.API.Controllers.Base;
using WoundCareApi.Application.DTOs;
using WoundCareApi.Core.Application.UseCases.CaseRecords.Commands.InsertCaseRecord;
using WoundCareApi.Core.Application.UseCases.CaseRecords.Commands.UpdateCaseRecord;
using WoundCareApi.Core.Application.UseCases.CaseRecords.Queries.GetCaseRecord;
using MediatR;

namespace WoundCareApi.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CaseRecordController : BaseController
{
    private readonly IMediator _mediator;

    public CaseRecordController(ILogger<CaseRecordController> logger, IMediator mediator)
        : base(logger)
    {
        _mediator = mediator;
    }

    [Authorize(AuthenticationSchemes = "Bearer,ApiKey")]
    [HttpGet("reportId/{reportId}")]
    public async Task<ActionResult<CaseRecordDto>> GetReport(string reportId)
    {
        if (!Guid.TryParse(reportId, out Guid reportGuid))
        {
            return BadRequest("無效的報告 ID 格式");
        }

        var query = new GetCaseRecordByIdQuery(reportGuid);
        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<ActionResult<CaseRecordDto>> InsertReport(
        [FromBody] CaseFormDataDto formDataDto
    )
    {
        var command = new InsertCaseRecordCommand(formDataDto, GetUserId());
        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(
            nameof(GetReport),
            new { reportId = result.Data.Puid.ToString() },
            result.Data
        );
    }

    [HttpPut("reportId/{reportId}")]
    public async Task<ActionResult> UpdateReport(
        string reportId,
        [FromBody] CaseFormDataDto formDataDto
    )
    {
        if (!Guid.TryParse(reportId, out Guid reportGuid))
        {
            return BadRequest("無效的報告 ID 格式");
        }

        if (formDataDto == null)
        {
            return BadRequest("報告資料為必填");
        }

        var command = new UpdateCaseRecordCommand(reportGuid, formDataDto, GetUserId());
        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(result.Error);
        }

        return Success("報告更新成功");
    }
}
