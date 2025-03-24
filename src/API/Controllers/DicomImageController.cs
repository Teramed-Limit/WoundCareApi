using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoundCareApi.Persistence.Repository;
using WoundCareApi.Persistence.UnitOfWork;
using WoundCareApi.src.Core.Domain.CRS;
using WoundCareApi.src.Infrastructure.Persistence;
using System.ComponentModel.DataAnnotations;

namespace WoundCareApi.API.Controllers;

/// <summary>
/// DICOM 影像控制器，處理與 DICOM 影像相關的操作
/// </summary>
[ApiController]
[Authorize]
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

    /// <summary>
    /// 更新 DICOM 影像的標記
    /// </summary>
    /// <param name="sopInstanceUid">DICOM 影像的 SOP Instance UID</param>
    /// <param name="imageMarker">影像標記資訊</param>
    /// <returns>更新後的 DICOM 影像資訊</returns>
    [HttpPost("sopInstanceUid/{sopInstanceUid}/imageMarker")]
    public async Task<ActionResult<string>> PostImageMarker(
        [Required(ErrorMessage = "SOP Instance UID 為必填項")] string sopInstanceUid,
        [FromBody] ImageMarkerDto imageMarker
    )
    {
        try
        {
            _logger.LogInformation(
                "開始更新 DICOM 影像標記，SOP Instance UID: {SopInstanceUid}",
                sopInstanceUid
            );

            // 驗證輸入
            if (imageMarker == null)
            {
                _logger.LogWarning("請求內容為空");
                return BadRequest("請求內容不能為空");
            }

            var dicomImage = new DicomImage
            {
                SOPInstanceUID = sopInstanceUid,
                ImageMarker = imageMarker.ImageMarker
            };

            await _repository.UpdatePartialAsync(dicomImage, "SOPInstanceUID", "ImageMarker");
            await _unitOfWork.SaveAsync();

            _logger.LogInformation(
                "成功更新 DICOM 影像標記，SOP Instance UID: {SopInstanceUid}",
                sopInstanceUid
            );
            return Ok("更新成功");
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "更新 DICOM 影像標記時發生錯誤，SOP Instance UID: {SopInstanceUid}",
                sopInstanceUid
            );
            return StatusCode(500, "更新 DICOM 影像標記時發生錯誤");
        }
    }
}
