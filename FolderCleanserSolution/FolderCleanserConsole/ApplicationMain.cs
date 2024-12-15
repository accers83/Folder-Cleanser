using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FolderCleanserConsole;

internal class ApplicationMain : IApplicationMain
{
    private readonly ILogger<ApplicationMain> _logger;
    private readonly IConfiguration _config;

    public ApplicationMain(ILogger<ApplicationMain> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    public void Run(int option)
    {
        _logger.LogInformation($"Application started with option {option}.");

        switch (option)
        {
            case 0:
                _logger.LogInformation("The application will now exit.");
                break;

            case 1:
                _logger.LogInformation("Display value from appsettings, IsProduction: {IsProduction}", _config.GetValue<bool>("IsProduction"));
                _logger.LogInformation("Display value from appsettings, IsDevelopment: {IsDevelopment}", _config.GetValue<bool>("IsDevelopment"));
                _logger.LogInformation("Display value from appsettings, TestString: {TestString}", _config.GetValue<string>("TestString"));
                _logger.LogInformation("Display value from appsettings, TestInt: {TestInt}", _config.GetValue<int>("TestInt"));
                break;

            default:
                _logger.LogInformation("An invalid option has been entered.");
                _logger.LogInformation("The application will now exit.");
                break;

        }
    }
}
