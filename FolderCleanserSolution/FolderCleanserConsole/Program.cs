using FolderCleanserConsole;
using Microsoft.Extensions.DependencyInjection;
using Serilog;


ConsoleConfigHelper.ConfigureLogger();
Log.Information("Application Started.");
Log.Information("Pre-config logging commenced.");

var environment = ConsoleConfigHelper.IdentifyEnvironment(args);
var developmentTool = ConsoleConfigHelper.IsDevelopmentTool();

if (environment == "prod" && developmentTool == true)
{
    ConsoleConfigHelper.IsProductionConfiguration();
}


try
{
    var host = ConsoleConfigHelper.CreateHostBuilder(environment).Build();
    Log.Information("Application running and configured.");

    var applicationMain = ActivatorUtilities.GetServiceOrCreateInstance<IApplicationMain>(host.Services);
    var option = ConsoleConfigHelper.GetOption(args);

    applicationMain.Run(option);

    Log.Information("Application completed cleanly.");
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occurred.");
    return 1;
}
finally
{
    Log.Information("");
    Log.CloseAndFlush();
}

