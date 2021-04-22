using System;
using KadOzenka.Dal.ConfigurationManagers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Microsoft.Extensions.Configuration;
using KadOzenka.WebClients.ConfigurationManagers;
using Platform.Main.ConfigurationManagers.CoreConfigurationManager;

namespace CIPJS
{
    public class Program
    {
        static string ASPNETCORE_ENVIRONMENT = "Development";
        public static void Main(string[] args)
        {
            
    //        #if DEBUG
    //            ASPNETCORE_ENVIRONMENT = "Development";
    //        #elif QA
    //            ASPNETCORE_ENVIRONMENT = "QA";
    //        #elif DEMO
    //            ASPNETCORE_ENVIRONMENT = "Demo";
    //        #elif RELEASE
    //            ASPNETCORE_ENVIRONMENT = "Production";
    //         #elif LOCAL
    //            ASPNETCORE_ENVIRONMENT = "Local";
    //        #elif UAT
				//ASPNETCORE_ENVIRONMENT = "UAT";
    //        #endif

            if (Environment.GetEnvironmentVariables().Contains("ASPNETCORE_ENVIRONMENT"))
            {
                ASPNETCORE_ENVIRONMENT = Environment.GetEnvironmentVariables()["ASPNETCORE_ENVIRONMENT"].ToString();
            }
            var envConfigFile = $"appsettings.{ASPNETCORE_ENVIRONMENT}.json";
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                //todo вынести во внедренные ресурсы
                .AddJsonFile(path: "gknImportSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(path: envConfigFile, optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
           
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
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
                //.StartFeatureSubscribe(config)
                .UseKoConfigManager(config)
                .UseReonConfigManager(config)
                .UseCoreConfigManager(config)
                .Build();

    }
}
