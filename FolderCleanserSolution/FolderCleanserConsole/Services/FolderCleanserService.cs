using FolderCleanserFrontEndLibrary.DataAccess;
using FolderCleanserFrontEndLibrary.Models;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace FolderCleanserConsole.Services;

public class FolderCleanserService : IFolderCleanserService
{
    private readonly IFolderCleanserApiRepository _folderCleanserApiRepository;
    private readonly IFileSystemRepository _fileSystemRepository;
    private readonly ILogger<FolderCleanserService> _logger;

    public FolderCleanserService(IFolderCleanserApiRepository folderCleanserApiRepository, IFileSystemRepository fileSystemRepository,
        ILogger<FolderCleanserService> logger)
    {
        _folderCleanserApiRepository = folderCleanserApiRepository;
        _fileSystemRepository = fileSystemRepository;
        _logger = logger;
    }

    public async Task InitiateAsync()
    {
        var paths = await _folderCleanserApiRepository.GetPathsAsync();

        foreach (var path in paths)
        {
            SummaryHistoryModel summaryHistory = new();
            summaryHistory = ProcessPath(path);
            await _folderCleanserApiRepository.AddSummaryHistoryAsync(summaryHistory);
        }


    }

    private SummaryHistoryModel ProcessPath(PathModel path)
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
                // TODO: delete file
            }
        }

        var endTime = Stopwatch.GetTimestamp();
        summaryHistory.ProcessingEndDateTime = DateTime.Now;
        summaryHistory.ProcessingDurationMins = Stopwatch.GetElapsedTime(startTime, endTime).TotalMinutes;
        summaryHistory.PathId = path.Id;

        return summaryHistory;
    }
}
