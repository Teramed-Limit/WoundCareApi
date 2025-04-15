using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public static class DirectoryHelper
{
    /// <summary>
    /// 取得資料夾中符合指定副檔名的所有檔案
    /// </summary>
    public static IEnumerable<string> GetFilesInFolder(string targetDirectory, string extension)
    {
        ArgumentException.ThrowIfNullOrEmpty(targetDirectory);
        ArgumentException.ThrowIfNullOrEmpty(extension);

        if (!Directory.Exists(targetDirectory))
            yield break;

        // 標準化副檔名格式
        var normalizedExtension = extension.StartsWith(".")
            ? extension.ToLowerInvariant()
            : $".{extension.ToLowerInvariant()}";

        // 使用 EnumerateFiles 提升大型目錄的效能
        foreach (
            var file in Directory.EnumerateFiles(
                targetDirectory,
                "*.*",
                SearchOption.AllDirectories
            )
        )
        {
            if (Path.GetExtension(file).ToLowerInvariant() == normalizedExtension)
                yield return file;
        }
    }

    /// <summary>
    /// 取得資料夾中符合指定副檔名的第一個檔案
    /// </summary>
    public static string GetFirstFileInFolder(string targetDirectory, string extension)
    {
        ArgumentException.ThrowIfNullOrEmpty(targetDirectory);
        ArgumentException.ThrowIfNullOrEmpty(extension);

        if (!Directory.Exists(targetDirectory))
            return null;

        // 標準化副檔名格式
        var normalizedExtension = extension.StartsWith(".") ? extension : $".{extension}";

        // 直接使用 LINQ 的 FirstOrDefault 避免多餘的迴圈
        return Directory
            .EnumerateFiles(targetDirectory, $"*{normalizedExtension}", SearchOption.AllDirectories)
            .FirstOrDefault();
    }

    /// <summary>
    /// 複製來源資料夾的所有檔案到目標資料夾
    /// </summary>
    public static void CopyAllFileInFolder(string sourcePath, string targetPath)
    {
        ArgumentException.ThrowIfNullOrEmpty(sourcePath);
        ArgumentException.ThrowIfNullOrEmpty(targetPath);

        if (!Directory.Exists(sourcePath))
            throw new DirectoryNotFoundException($"來源資料夾不存在: {sourcePath}");

        Directory.CreateDirectory(targetPath);

        try
        {
            foreach (var directory in Directory.GetDirectories(sourcePath))
            {
                CopyAllFileInFolder(
                    directory,
                    Path.Combine(targetPath, Path.GetFileName(directory))
                );
            }

            foreach (var srcPath in Directory.GetFiles(sourcePath))
            {
                File.Copy(srcPath, srcPath.Replace(sourcePath, targetPath), true);
            }
        }
        catch (Exception e)
        {
            throw new IOException($"複製檔案時發生錯誤: {e.Message}", e);
        }
    }

    public static void Copy(string sourcePath, string targetPath)
    {
        try
        {
            File.Copy(sourcePath, targetPath, true);
        }
        catch (Exception e)
        {
            throw new IOException($"複製檔案時發生錯誤: {e.Message}", e);
        }
    }

    public static void Move(string sourceFile, string targetFile)
    {
        try
        {
            var currentDirectory = Path.GetDirectoryName(sourceFile);
            CreateDirectory(currentDirectory);
            File.Copy(sourceFile, targetFile, true);
            File.Delete(sourceFile);
        }
        catch (Exception e)
        {
            throw new IOException($"移動檔案時發生錯誤: {e.Message}", e);
        }
    }

    public static void MoveDir(string source, string target)
    {
        try
        {
            CopyAllFileInFolder(source, target);
            Directory.Delete(source, true);
        }
        catch (Exception e)
        {
            throw new IOException($"複製資料夾時發生錯誤: {e.Message}", e);
        }
    }

    public static void DeleteDir(string dir)
    {
        try
        {
            Directory.Delete(dir, true);
        }
        catch (Exception e)
        {
            throw new IOException($"刪除資料夾時發生錯誤: {e.Message}", e);
        }
    }

    public static void CreateDirectory(string targetPath)
    {
        if (!Directory.Exists(targetPath))
            Directory.CreateDirectory(targetPath);
    }

    /// <summary>
    /// 非同步計算目錄大小
    /// </summary>
    public static async Task<long> DirSizeAsync(string path)
    {
        ArgumentException.ThrowIfNullOrEmpty(path);

        if (!Directory.Exists(path))
            return 0;

        var dirInfo = new DirectoryInfo(path);
        return await Task.Run(
            () => dirInfo.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length)
        );
    }

    /// <summary>
    /// 刪除所有空目錄
    /// </summary>
    public static void RemoveEmptyDirectories(string startLocation, bool removeRoot = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(startLocation);

        if (!Directory.Exists(startLocation))
            return;

        foreach (var directory in Directory.GetDirectories(startLocation))
        {
            RemoveEmptyDirectories(directory, true);
        }

        // 重新檢查目錄是否為空，因為子目錄可能已被移除
        if (
            Directory.GetFileSystemEntries(startLocation).Length == 0
            && (removeRoot || Path.GetFullPath(startLocation) != Path.GetFullPath(startLocation))
        )
        {
            try
            {
                Directory.Delete(startLocation, false);
            }
            catch (IOException)
            {
                // 忽略無法刪除的目錄
            }
        }
    }
}
