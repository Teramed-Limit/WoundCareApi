using Microsoft.AspNetCore.Mvc;
using WoundCareApi.Persistence.Repository;
using WoundCareApi.Persistence.UnitOfWork;
using WoundCareApi.src.Core.Domain.CRS;
using WoundCareApi.src.Infrastructure.Persistence;

namespace WoundCareApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DicomImageController : ControllerBase
{
    private readonly ILogger<DicomImageController> _logger;
    private readonly IRepository<DicomImage, CRSDbContext> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DicomImageController(
        ILogger<DicomImageController> logger,
        IRepository<DicomImage, CRSDbContext> repository,
        IUnitOfWork unitOfWork
    )
    {
        _logger = logger;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("sopInstanceUid/{sopInstanceUid}/imageMarker")]
    public async Task<ActionResult<DicomImage>> PostImageMarker(
        string sopInstanceUid,
        [FromBody] ImageMarkerDto imageMarker
    )
    {
        var dicomImage = new DicomImage
        {
            SOPInstanceUID = sopInstanceUid,
            ImageMarker = imageMarker.ImageMarker
        };

        await _repository.UpdatePartialAsync(dicomImage, "SOPInstanceUID", "ImageMarker");
        await _unitOfWork.SaveAsync();

        return Ok();
    }
}
