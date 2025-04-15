namespace TeraLinkaCareApi.Common.Utils;

public static class DateUtils
{
    /// <summary>
    /// 格式化日期時間顯示
    /// </summary>
    /// <param name="date">日期時間</param>
    /// <returns>格式化的日期時間字符串</returns>
    public static string FormatDateTime(DateTime date)
    {
        return date.ToString("yyyy-MM-dd HH:mm");
    }

    /// <summary>
    /// 格式化日期顯示
    /// </summary>
    /// <param name="date">日期</param>
    /// <returns>格式化的日期字符串</returns>
    public static string FormatDate(DateTime date)
    {
        return date.ToString("yyyy-MM-dd");
    }
}
