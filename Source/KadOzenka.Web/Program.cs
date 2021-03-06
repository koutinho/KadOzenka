using System;
using CommonSdks.ConfigurationManagers;
using CommonSdks.ConfigurationManagers.WebClients;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Microsoft.Extensions.Configuration;
using Platform.Main.ConfigurationManagers.CoreConfigurationManager;

namespace CIPJS
{
    public class Program
    {
        static string ASPNETCORE_ENVIRONMENT = "Development";
        public static void Main(string[] args)
        {

	        if (Environment.GetEnvironmentVariables().Contains("ASPNETCORE_ENVIRONMENT"))
            {
                ASPNETCORE_ENVIRONMENT = Environment.GetEnvironmentVariables()["ASPNETCORE_ENVIRONMENT"]?.ToString();
            }
	        else
	        {
		        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", ASPNETCORE_ENVIRONMENT);
            }
            var envConfigFile = $"appsettings.{ASPNETCORE_ENVIRONMENT}.json";
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(path: envConfigFile, optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
           
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("Environment", ASPNETCORE_ENVIRONMENT)
                //.Enrich.WithProperty("Version", typeof(Program).Assembly.Version)
                .CreateLogger();

            try
            {
                Log.ForContext("envConfigFile", envConfigFile).Warning("Application MiomoKadOzenka.Web starting up");
                BuildWebHost(args, configuration).Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application MiomoKadOzenka.Web start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args, IConfigurationRoot config) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>()
                .UseKoConfigManager(config)
                .UseWebClientsConfigManager(config)
                .UseCoreConfigManager(config)
                .Build();

    }
}
