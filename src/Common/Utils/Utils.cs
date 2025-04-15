namespace TeraLinkaCareApi.Common.Utils;

public static class Utils
{
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
            + "."
            + "1."
            + Convert.ToString(randomValue)
            + ".1"
            + timespan;
    }

    public static string HandleVirtualPath(string virtualPath, string filePath)
    {
        // FilePath要做處理改虛擬目錄了URL
        // 取出檔名和附檔名
        var fileName = Path.GetFileName(filePath);
        // 處理虛擬目錄
        return $"{virtualPath}/{fileName}";
    }
}
