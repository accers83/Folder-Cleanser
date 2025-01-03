using FolderCleanserConsole.Services;
using FolderCleanserFrontEndLibrary.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderCleanserConsole;

internal static class ConsoleConfigHelper
{
    public static string IdentifyEnvironment(string[] arguments)
    {
        foreach (var argument in arguments)
        {
            if (argument.ToLower().Equals("prod") == true)
            {
                return "prod";
            }
        }

        return "dev";
    }

    public static bool IsDevelopmentTool()
    {
        var directory = Environment.CurrentDirectory;
        return directory.ToLower().Contains(@"bin\debug") || directory.ToLower().Contains(@"bin\release") ? true : false;
    }

    public static void ConfigureLogger()
    {
        Log.Logger = new LoggerConfiguration()
                        .WriteTo.Console()
                        //.WriteTo.File(@"C:\Temp\DiConsoleTemplate2-production-Log.txt", rollingInterval: RollingInterval.Day) // I could pull from args if I really wanted to
                        .CreateBootstrapLogger();
    }

    public static void IsProductionConfiguration()
    {
        Log.Information($"WARNING! You are attempting to use production configuration in a development environment!");
        Log.Information("WARNING! Are you sure you want to continue, this may impact production systems and data (yes / no)");
        var confirmationInput = Console.ReadLine();
        if (confirmationInput.ToLower() == "yes")
        {
            Log.Information("Thank you for confirming, application continuing.. ");
            Log.Information("");
        }
        else
        {
            Log.Information("Exiting application");
            Log.Information("");
            Environment.Exit(0);
        }
    }

    public static IHostBuilder CreateHostBuilder(string environment)
    {
        return Host.CreateDefaultBuilder()
        .UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Console())

        .ConfigureServices(services => services
            .AddTransient<IApplicationMain, ApplicationMain>()
            .AddTransient<IFolderCleanserService, FolderCleanserService>()
            .AddTransient<IFolderCleanserApiRepository, FolderCleanserApiRepository>()
            .AddTransient<IFileSystemRepository, FileSystemRepository>()
            .AddHttpClient())

        .ConfigureHostConfiguration(config => config
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true));
    }

    public static int GetOption(string[] args)
    {
        int option = 0;

        if (args.Length > 0 && args[0].Length == 1)
        {
            option = ParseOptionArgument(args[0]);
        }

        if (option == 0)
        {
            option = RequestOptionFromUser();
        }

        return option;
    }

    private static int ParseOptionArgument(string arg)
    {
        bool isValidNumber = int.TryParse(arg, out int serviceId);

        if (isValidNumber == false)
        {
            serviceId = 0;
        }

        return serviceId;
    }


    private static int RequestOptionFromUser()
    {
        int option = 0;
        bool isValidNumber = false;

        DisplayOptions();

        do
        {
            Console.Write("Please enter an option from above: ");
            string optionText = Console.ReadLine();
            isValidNumber = int.TryParse(optionText, out option);

            if (isValidNumber == false)
            {
                Console.WriteLine("Please enter a valid option (e.g. 1).");
            }

            Console.WriteLine();

        } while (isValidNumber == false);

        return option;
    }

    private static void DisplayOptions()
    {
        Console.WriteLine();
        Console.WriteLine("0) Exit application.");
        Console.WriteLine("1) Run some service.");

        Console.WriteLine();
    }
}
