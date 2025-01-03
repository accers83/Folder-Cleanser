using FolderCleanserConsole.Processors;
using FolderCleanserFrontEndLibrary.DataAccess;
using FolderCleanserFrontEndLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FolderCleanserConsole.Services;

public class FolderCleanserService : IFolderCleanserService
{
    private readonly IFolderCleanserApiRepository _folderCleanserApiRepository;
    private readonly IFolderCleanserProcessor _folderCleanserProcessor;
    private readonly ILogger<FolderCleanserService> _logger;
    private readonly IConfiguration _config;

    public FolderCleanserService(IFolderCleanserApiRepository folderCleanserApiRepository, IFolderCleanserProcessor folderCleanserProcessor,
        ILogger<FolderCleanserService> logger, IConfiguration config)
    {
        _folderCleanserApiRepository = folderCleanserApiRepository;
        _folderCleanserProcessor = folderCleanserProcessor;
        _logger = logger;
        _config = config;
    }

    public async Task InitiateAsync()
    {
        var paths = await _folderCleanserApiRepository.GetPathsAsync();

        foreach (var path in paths)
        {
            SummaryHistoryModel summaryHistory = new();
            summaryHistory = _folderCleanserProcessor.ProcessPath(path);
            await _folderCleanserApiRepository.AddSummaryHistoryAsync(summaryHistory);
        }
    }
}
