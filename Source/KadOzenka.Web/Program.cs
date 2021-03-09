﻿using System;
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
        public static void Main(string[] args)
        {
           var configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
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
                .UseSerilog()
                .UseStartup<Startup>()
                .StartFeatureSubscribe(config)
                .Build();

    }
}
