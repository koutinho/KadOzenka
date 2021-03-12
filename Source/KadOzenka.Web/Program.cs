using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Microsoft.Extensions.Configuration;
using KadOzenka.Dal.WorkerCheckerDataBase;
using Platform.Web.Configurator.FeatureToggleConfigManager;

namespace CIPJS
{
    public class Program
    {
        static string ASPNETCORE_ENVIRONMENT;
        public static void Main(string[] args)
        {
            
            #if DEBUG
                ASPNETCORE_ENVIRONMENT = "Development";
            #elif QA
                ASPNETCORE_ENVIRONMENT = "QA";
            #elif DEMO
                ASPNETCORE_ENVIRONMENT = "Demo";
            #elif RELEASE
                ASPNETCORE_ENVIRONMENT = "Production";
            #endif

           var configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(path: $"appsettings.{ASPNETCORE_ENVIRONMENT}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
           
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                //.Enrich.WithProperty("Version", typeof(Program).Assembly.Version)
                .CreateLogger();


            try
            {
                Log.Warning("Application KadOzenka.Web starting up");
                BuildWebHost(args, configuration).Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application KadOzenka.Web start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args, IConfigurationRoot config) =>
            WebHost.CreateDefaultBuilder(args)
                //.ConfigureAppConfiguration((a, configuration) =>
                //{
                //    configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                //    configuration.AddJsonFile($"appsettings.{ASPNETCORE_ENVIRONMENT}.json", optional: true, reloadOnChange: true);
                //})
                .UseSerilog()
                .UseStartup<Startup>()
                .StartFeatureSubscribe(config)
                .Build();

    }
}
