using FolderCleanserConsole.Services;
using Microsoft.Extensions.Logging;

namespace FolderCleanserConsole;

internal class ApplicationMain : IApplicationMain
{
    private readonly ILogger<ApplicationMain> _logger;
    private readonly IFolderCleanserService _folderCleanserService;

    public ApplicationMain(ILogger<ApplicationMain> logger, IFolderCleanserService folderCleanserService)
    {
        _logger = logger;
        _folderCleanserService = folderCleanserService;
    }

    public async Task RunAsync(int option)
    {
        _logger.LogInformation($"Application started with option {option}.");

        switch (option)
        {
            case 0:
                _logger.LogInformation("The application will now exit.");
                break;

            case 1:
                await _folderCleanserService.InitiateAsync();
                break;

            default:
                _logger.LogInformation("An invalid option has been entered.");
                _logger.LogInformation("The application will now exit.");
                break;

        }
    }
}
