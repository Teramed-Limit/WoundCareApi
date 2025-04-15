using System;
using FellowOakDicom;
using Serilog;

/// <summary>
/// DICOM UID 類型枚舉
/// </summary>
public enum DicomUidType
{
    Study,
    Series,
    Instance
}

public static class DicomHelper
{
    public static bool IsValidDicomInstanceUid(string uid)
    {
        try
        {
            // 嘗試解析 UID
            var dicomUid = DicomUID.Parse(uid);

            // 檢查是否為 Instance UID
            // 這裡可以進一步檢查是否為特定的 SOP Instance UID（可選）
            return dicomUid != null && !string.IsNullOrEmpty(dicomUid.UID);
        }
        catch (DicomDataException e)
        {
            // 如果解析失敗，則表示無效
            Log.Error(e, uid);
            return false;
        }
    }

    /// <summary>
    /// 確保UID有效，如果無效或為空則生成新的
    /// </summary>
    /// <param name="uid">原始UID</param>
    /// <param name="uidType">UID類型</param>
    /// <param name="parentUid">父級UID（可選）</param>
    /// <returns>有效的DICOM UID</returns>
    public static DicomUID EnsureValidUid(string uid, string uidType, DicomUID parentUid = null)
    {
        // 如果提供了有效的UID，嘗試解析
        if (!string.IsNullOrWhiteSpace(uid))
        {
            try
            {
                var dicomUid = new DicomUID(
                    uid,
                    $"{uidType} UID",
                    (FellowOakDicom.DicomUidType)
                        Enum.Parse(typeof(FellowOakDicom.DicomUidType), uidType)
                );
                return dicomUid;
            }
            catch (Exception)
            {
                // 解析失敗，生成新的
            }
        }

        // 生成新的UID
        return DicomUID.Generate();
    }

    /// <summary>
    /// 從DICOM文件中提取特定標籤的值
    /// </summary>
    /// <param name="file">DICOM文件</param>
    /// <param name="tag">DICOM標籤</param>
    /// <returns>標籤值，如果不存在則返回null</returns>
    public static string GetTagValue(DicomFile file, DicomTag tag)
    {
        if (file?.Dataset != null && file.Dataset.Contains(tag))
        {
            return file.Dataset.GetString(tag);
        }
        return null;
    }

    /// <summary>
    /// 從DICOM文件中讀取關鍵UID
    /// </summary>
    /// <param name="filePath">DICOM文件路徑</param>
    /// <returns>包含UID的元組 (StudyInstanceUID, SeriesInstanceUID, SOPInstanceUID)</returns>
    public static (DicomUID StudyUID, DicomUID SeriesUID, DicomUID SopUID) GetDicomUids(
        string filePath
    )
    {
        try
        {
            var file = DicomFile.Open(filePath);

            var studyUid = file.Dataset.GetSingleValue<DicomUID>(DicomTag.StudyInstanceUID);
            var seriesUid = file.Dataset.GetSingleValue<DicomUID>(DicomTag.SeriesInstanceUID);
            var sopUid = file.Dataset.GetSingleValue<DicomUID>(DicomTag.SOPInstanceUID);

            return (studyUid, seriesUid, sopUid);
        }
        catch (Exception ex)
        {
            // 如果讀取失敗，返回null值
            return (null, null, null);
        }
    }

    /// <summary>
    /// 根據類型生成適當的UID，當傳入的UID為空時使用
    /// </summary>
    /// <param name="uid">原始UID，可能為空</param>
    /// <param name="uidType">UID類型</param>
    /// <param name="parentUid">用於Series和Instance UID生成的父級UID，Series需要Study UID，Instance需要Series UID</param>
    /// <param name="index">序列號或實例號，用於Series和Instance UID</param>
    /// <returns>有效的DICOM UID</returns>
    public static string EnsureValidUid(
        string uid,
        DicomUidType uidType,
        string parentUid = null,
        int index = 0
    )
    {
        // 如果UID不為空且有效，則直接返回
        if (!string.IsNullOrEmpty(uid) && IsValidDicomInstanceUid(uid))
        {
            return uid;
        }

        // 根據類型生成新的UID
        switch (uidType)
        {
            case DicomUidType.Study:
                return GenerateStudyInstanceUID();
            case DicomUidType.Series:
                if (string.IsNullOrEmpty(parentUid))
                {
                    throw new ArgumentException("生成Series UID時需要提供有效的Study UID", nameof(parentUid));
                }
                return GenerateSeriesInstanceUID(parentUid, index);
            case DicomUidType.Instance:
                if (string.IsNullOrEmpty(parentUid))
                {
                    throw new ArgumentException(
                        "生成Instance UID時需要提供有效的Series UID",
                        nameof(parentUid)
                    );
                }
                return GenerateSopInstanceUID(parentUid, index);
            default:
                throw new ArgumentOutOfRangeException(nameof(uidType), uidType, "不支援的UID類型");
        }
    }

    public static string GenerateStudyInstanceUID()
    {
        string timespan = DateTime.Now.ToString("fff");
        Random rnd = new Random();
        int randomValue = Enumerable
            .Range(1, 9999)
            .OrderBy(x => rnd.Next())
            .Take(1000)
            .ToList()
            .First();
        return "1.3.6.1.4.1.54514."
            + DateTime.Now.ToString("yyyyMMddhhmmss")
            + ".1."
            + Convert.ToString(randomValue)
            + ".1"
            + timespan;
    }

    public static string GenerateSeriesInstanceUID(string studyInstanceUid, int seriesIdx)
    {
        return studyInstanceUid + "." + Convert.ToString(seriesIdx + 1);
    }

    public static string GenerateSopInstanceUID(string seriesInstanceUid, int instanceNumber)
    {
        return seriesInstanceUid + "." + Convert.ToString(instanceNumber + 1);
    }
}
