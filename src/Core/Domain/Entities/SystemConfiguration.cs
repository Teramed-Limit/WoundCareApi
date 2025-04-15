using System;
using System.Collections.Generic;

namespace TeraLinkaCareApi.Core.Domain.Entities;

public partial class SystemConfiguration
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? CStoreBackupFilePath { get; set; }

    public string? CStroeTmpFilesPath { get; set; }

    public string? LogRootPath { get; set; }

    public int? LogLevel { get; set; }

    public string? ErrorImagesPath { get; set; }

    public string? PACSMessageWriteToLog { get; set; }

    public string? WorklistMessageWriteToLog { get; set; }

    public string? ScheduleMessageWriteToLog { get; set; }

    public int? SystemConfigureListenPort { get; set; }

    public int? JobProcessTimerInterval { get; set; }

    public int? DailyTimerInterval { get; set; }

    public string CreateDateTime { get; set; } = null!;

    public string CreateUser { get; set; } = null!;

    public string? ModifiedDateTime { get; set; }

    public string? ModifiedUser { get; set; }
}
