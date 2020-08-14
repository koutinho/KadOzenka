using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog.Extensions.Logging;
using Serilog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Serilog.Configuration;
using Serilog.Events;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace CIPJS
{
    public class Program
    {

        public static void Main(string[] args)
        {

            //// Creating a `LoggerProviderCollection` lets Serilog optionally write
            //// events through other dynamically-added MEL ILoggerProviders.
            var providers = new LoggerProviderCollection();

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
                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application KadOzenka.Web start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }

            //var services = new ServiceCollection();

            //services.AddSingleton(providers);
            //services.AddSingleton<ILoggerFactory>(sc =>
            //{
            //    var providerCollection = sc.GetService<LoggerProviderCollection>();
            //    var factory = new SerilogLoggerFactory(null, true, providerCollection);

            //    foreach (var provider in sc.GetServices<ILoggerProvider>())
            //        factory.AddProvider(provider);

            //    return factory;
            //});

            //var serviceProvider = services.BuildServiceProvider();
            //var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            //serviceProvider.Dispose();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>()
                .Build();
    }
}
