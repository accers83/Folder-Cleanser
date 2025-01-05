using FolderCleanserFrontEndLibrary.DataAccess;
using FolderCleanserFrontEndLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace FolderCleanserConsole.Processors;

public class FolderCleanserProcessor : IFolderCleanserProcessor
{
    private readonly IFileSystemRepository _fileSystemRepository;
    private readonly ILogger<FolderCleanserProcessor> _logger;
    private readonly IConfiguration _config;

    public FolderCleanserProcessor(IFileSystemRepository fileSystemRepository, ILogger<FolderCleanserProcessor> logger, IConfiguration config)
    {
        _fileSystemRepository = fileSystemRepository;
        _logger = logger;
        _config = config;
    }

    public SummaryHistoryModel ProcessPath(PathModel path)
    {
        _logger.LogInformation("Processing path: {path}", path.Path);

        List<string> files = new();
        SummaryHistoryModel summaryHistory = new() { ProcessingStartDateTime = DateTime.Now };
        var startTime = Stopwatch.GetTimestamp();

        try
        {
            files = _fileSystemRepository.GetAllFiles(path.Path);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Exception retrieving files from {path.Path}");
        }

        foreach (var file in files)
        {
            var fileLastWriteTime = _fileSystemRepository.GetFileLastWriteTime(file);
            var fileRetentionThreshold = DateTime.Now.AddDays(-path.RetentionDays);

            if (fileLastWriteTime < fileRetentionThreshold)
            {
                _logger.LogInformation("Deleting file: {file}", file);
                _logger.LogInformation($"fileLastWriteTime ({fileLastWriteTime}) < fileRetentionThreshold ({fileRetentionThreshold})");

                summaryHistory.FilesDeletedCount++;
                summaryHistory.FileSizeDeletedMB += _fileSystemRepository.GetFileSizeMB(file);

                if (_config.GetValue<bool>("EnableFileDeletes") == true)
                {
                    try
                    {
                        _fileSystemRepository.DeleteFile(file);
                        _logger.LogInformation("File deleted");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Exception deleting file from {file}");
                    }
                }
            }
        }

        var endTime = Stopwatch.GetTimestamp();
        summaryHistory.ProcessingEndDateTime = DateTime.Now;
        summaryHistory.ProcessingDurationMins = Stopwatch.GetElapsedTime(startTime, endTime).TotalMinutes;
        summaryHistory.PathId = path.Id;

        return summaryHistory;
    }
}
