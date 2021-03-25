﻿using System;
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
            
            if (Environment.GetEnvironmentVariables().Contains("ASPNETCORE_ENVIRONMENT"))
            {
                ASPNETCORE_ENVIRONMENT = Environment.GetEnvironmentVariables()["ASPNETCORE_ENVIRONMENT"].ToString();
            }

            var envConfigFile = $"appsettings.{ASPNETCORE_ENVIRONMENT}.json";
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(path: envConfigFile, optional: true, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            try
            {
                Log.ForContext("envConfigFile", envConfigFile).Warning("Starting web host MiomoKadOzenka.LongProcessService");
                CreateWebHostBuilder(args, configuration).Build().Run();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host MiomoKadOzenka.LongProcessService terminated unexpectedly");

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
