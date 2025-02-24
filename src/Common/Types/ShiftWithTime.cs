namespace WoundCareApi.Common.Types;

/// <summary>
/// 包含時間的班別資訊
/// </summary>
public class ShiftWithTime
{
    public Guid PuId { get; set; }
    public int ShiftBeginHour { get; set; }
    public int ShiftOffsetMinutes { get; set; }
    public int ShiftDuration { get; set; }
    public string? ShiftShortLabel { get; set; }
    public string? ShiftLongLabel { get; set; }

    public DateTime ShiftStartDate { get; set; }
    public DateTime ShiftEndDate { get; set; }
}