using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FellowOakDicom.Network.Client;
using FellowOakDicom;
using FellowOakDicom.Network;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;
using TeraLinkaCareApi.Core.Domain.Entities;
using TeraLinkaCareApi.Infrastructure.Persistence;
using DicomTag = FellowOakDicom.DicomTag;

namespace TeraLinkaCareApi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DicomController : ControllerBase
{
    private readonly ILogger<DicomController> _logger;
    private readonly IConfiguration _configuration;
    private readonly CRSDbContext _dbContext;

    public DicomController(
        ILogger<DicomController> logger,
        IConfiguration configuration,
        CRSDbContext dbContext
    )
    {
        _dbContext = dbContext;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost("echo")]
    public async Task<IActionResult> EchoPacs(
        [FromForm] string pacsServerAddress,
        [FromForm] int pacsServerPort,
        [FromForm] string pacsServerAETitle,
        [FromForm] string localAETitle
    )
    {
        if (
            string.IsNullOrEmpty(pacsServerAddress)
            || string.IsNullOrEmpty(pacsServerAETitle)
            || string.IsNullOrEmpty(localAETitle)
        )
        {
            return BadRequest("PACS 服務器參數不能為空");
        }

        try
        {
            // 創建 DICOM 客戶端 (使用 DicomClientFactory 是推薦的方法)
            var client = DicomClientFactory.Create(
                pacsServerAddress,
                pacsServerPort,
                false,
                localAETitle,
                pacsServerAETitle
            );

            // 創建 C-ECHO 請求
            var echoRequest = new DicomCEchoRequest();
            var responseReceived = false;
            var status = DicomStatus.Pending;

            echoRequest.OnResponseReceived = (request, response) =>
            {
                responseReceived = true;
                status = response.Status;
            };

            // 發送 C-ECHO 請求
            await client.AddRequestAsync(echoRequest);
            await client.SendAsync();

            if (!responseReceived)
            {
                return StatusCode(500, "未收到 PACS 服務器的回應");
            }

            if (status == DicomStatus.Success)
            {
                return Ok(
                    new
                    {
                        success = true,
                        message = "成功連接到 PACS 服務器",
                        status = status.ToString()
                    }
                );
            }
            else
            {
                return StatusCode(
                    500,
                    new
                    {
                        success = false,
                        message = "PACS 服務器回應錯誤",
                        status = status.ToString()
                    }
                );
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "C-ECHO 測試時發生錯誤");
            return StatusCode(500, new { success = false, message = $"測試連接時發生錯誤: {ex.Message}" });
        }
    }

    [HttpPost("upload-pdf")]
    public async Task<IActionResult> UploadPdfToPacs(
        IFormFile pdfFile,
        [FromForm] string pacsServerAddress,
        [FromForm] int pacsServerPort,
        [FromForm] string pacsServerAETitle,
        [FromForm] string localAETitle,
        [FromForm] string caseRecordId,
        [FromForm] string patientID,
        [FromForm] string encounterId
    )
    {
        try
        {
            // 參數驗證
            if (pdfFile == null || pdfFile.Length == 0)
                return BadRequest("PDF文件不能為空");

            if (
                string.IsNullOrEmpty(patientID)
                || string.IsNullOrEmpty(encounterId)
                || string.IsNullOrEmpty(caseRecordId)
            )
            {
                _logger.LogError("病患ID、就診編號和案例ID不能為空");
                return BadRequest("病患ID、就診編號和案例ID不能為空");
            }

            var dicomPdfDirPath = _configuration.GetSection("DicomPdfDirPath").Value;
            if (string.IsNullOrEmpty(dicomPdfDirPath))
            {
                _logger.LogError("找不到指定的PDF資料夾路徑配置");
                return BadRequest("找不到指定的PDF資料夾路徑配置");
            }

            var patientInfo = await _dbContext
                .Set<A_PtEncounter>()
                .FirstOrDefaultAsync(x => x.LifeTimeNumber == patientID);

            if (patientInfo == null)
            {
                _logger.LogError("找不到病患資料: {PatientID}", patientID);
                return BadRequest("找不到指定的病患資料");
            }

            // 確保資料夾路徑存在
            var patientDirPath = Path.Combine(dicomPdfDirPath, patientID);
            var encounterDirPath = Path.Combine(patientDirPath, encounterId);

            if (!Directory.Exists(encounterDirPath))
                Directory.CreateDirectory(encounterDirPath);

            // 讀取PDF內容到記憶體
            using var memoryStream = new MemoryStream();
            await pdfFile.CopyToAsync(memoryStream);
            var pdfBytes = memoryStream.ToArray();

            // 要查找的目標DCM文件路徑
            var dcmPath = Path.Combine(encounterDirPath, $"{caseRecordId}.dcm");

            // DICOM UID變數
            DicomUID studyInstanceUid = null;
            DicomUID seriesInstanceUid = null;
            DicomUID sopInstanceUid = null;

            // 情況1: 檢查目標.dcm文件是否存在
            if (System.IO.File.Exists(dcmPath))
            {
                // 從現有DCM文件中提取UID
                var uids = DicomHelper.GetDicomUids(dcmPath);
                studyInstanceUid = uids.StudyUID;
                seriesInstanceUid = uids.SeriesUID;
                sopInstanceUid = uids.SopUID;
            }
            else
            {
                // 情況2.1: 如果找不到目標DCM，查找同一資料夾中的其他DCM文件
                var siblingDcm = DirectoryHelper.GetFirstFileInFolder(encounterDirPath, ".dcm");

                if (!string.IsNullOrEmpty(siblingDcm))
                {
                    // 使用兄弟DCM文件的StudyInstanceUID，但生成新的SeriesInstanceUID和SOPInstanceUID
                    var uids = DicomHelper.GetDicomUids(siblingDcm);
                    studyInstanceUid = uids.StudyUID;
                    seriesInstanceUid = DicomUID.Generate();
                    sopInstanceUid = DicomUID.Generate();
                }
                else
                {
                    // 情況2.2: 如果沒有任何相關DCM文件，生成全新的UID
                    studyInstanceUid = DicomUID.Generate();
                    seriesInstanceUid = DicomUID.Generate();
                    sopInstanceUid = DicomUID.Generate();
                }
            }

            // 創建DICOM資料集
            var dicomDataset = new DicomDataset
            {
                { DicomTag.SOPClassUID, DicomUID.EncapsulatedPDFStorage },
                { DicomTag.SOPInstanceUID, sopInstanceUid },
                { DicomTag.PatientID, patientID },
                { DicomTag.StudyInstanceUID, studyInstanceUid },
                { DicomTag.SeriesInstanceUID, seriesInstanceUid },
                { DicomTag.Modality, "DOC" },
                { DicomTag.StudyDate, DateTime.Now.ToString("yyyyMMdd") },
                { DicomTag.StudyTime, DateTime.Now.ToString("HHmmss") },
                { DicomTag.PatientName, patientInfo.FirstName + patientInfo.LastName },
                { DicomTag.OtherPatientIDsRETIRED, patientInfo.NationalId },
                {
                    DicomTag.PatientBirthDate,
                    patientInfo.DateOfBirth?.ToString("yyyyMMdd") ?? string.Empty
                },
                // { DicomTag.PatientSex, patientInfo.Gender },
                { DicomTag.ContentDate, DateTime.Now.ToString("yyyyMMdd") },
                { DicomTag.ContentTime, DateTime.Now.ToString("HHmmss") },
                { DicomTag.AccessionNumber, encounterId },
                { DicomTag.DocumentTitle, $"{caseRecordId}" },
                { DicomTag.MIMETypeOfEncapsulatedDocument, "application/pdf" },
                { DicomTag.EncapsulatedDocument, pdfBytes }
            };

            // 創建序列項目的子數據集
            var otherPatientIdItem = new DicomDataset
            {
                { DicomTag.PatientID, patientInfo.NationalId },
                // { DicomTag.IssuerOfPatientID, "國民身分證" }
            };

            // 將序列項目添加到主數據集
            dicomDataset.Add(
                new DicomSequence(DicomTag.OtherPatientIDsSequence, otherPatientIdItem)
            );

            // 創建DICOM文件
            var dicomFile = new DicomFile(dicomDataset);

            // 保存DICOM文件到本地
            var outputDcmPath = Path.Combine(encounterDirPath, $"{caseRecordId}.dcm");
            dicomFile.Save(outputDcmPath);

            // 創建C-STORE請求並上傳到PACS
            var client = DicomClientFactory.Create(
                pacsServerAddress,
                pacsServerPort,
                false,
                localAETitle,
                pacsServerAETitle
            );

            var cStoreRequest = new DicomCStoreRequest(dicomFile);
            var success = false;
            var responseStatus = DicomStatus.Pending;

            cStoreRequest.OnResponseReceived = (request, response) =>
            {
                success = response.Status == DicomStatus.Success;
                responseStatus = response.Status;
            };

            // 連接到PACS服務器並發送文件
            await client.AddRequestAsync(cStoreRequest);
            await client.SendAsync();

            if (success)
            {
                return Ok(
                    new
                    {
                        success = true,
                        message = "PDF文件已成功轉換為DICOM並上傳到PACS",
                        studyInstanceUid = studyInstanceUid.UID,
                        seriesInstanceUid = seriesInstanceUid.UID,
                        sopInstanceUid = sopInstanceUid.UID,
                        outputPath = outputDcmPath
                    }
                );
            }
            else
            {
                return StatusCode(
                    500,
                    new
                    {
                        success = false,
                        message = $"PACS服務器回應錯誤: {responseStatus}",
                        status = responseStatus.ToString()
                    }
                );
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "處理PDF轉換為DICOM時發生錯誤");
            return StatusCode(
                500,
                new
                {
                    success = false,
                    message = $"處理過程中發生錯誤: {ex.Message}",
                    stackTrace = ex.StackTrace
                }
            );
        }
    }
}
