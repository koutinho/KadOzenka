using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KadOzenka.Dal.WorkerCheckerDataBase;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace KadOzenka.LongProcessService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            try
            {
                Log.Information("Starting web host KadOzenka.LongProcessService");
                CreateWebHostBuilder(args, configuration).Build().Run();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host KadOzenka.LongProcessService terminated unexpectedly");

            }
            finally
            {
                Log.CloseAndFlush();
            }
           
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, IConfigurationRoot config) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .StartWorkerChecker(config)
                .UseStartup<Startup>();
    }
}
