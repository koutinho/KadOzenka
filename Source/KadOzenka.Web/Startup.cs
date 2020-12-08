using AutoMapper;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.UI.Registers.Services;
using GemBox.Spreadsheet;
using KadOzenka.Dal.GbuObject;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;
using Platform.Web;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Core.ErrorManagment;
using DocumentFormat.OpenXml.InkML;
using KadOzenka.Dal.WebSocket;
using KadOzenka.Dal.DuplicateCleaner;
using KadOzenka.Dal.ExpressScore;
using KadOzenka.Dal.ManagementDecisionSupport;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.ScoreCommon;
using KadOzenka.Dal.Tasks;
using KadOzenka.Dal.Tours;
using KadOzenka.Web.Helpers;
using KadOzenka.Web.SignalR;
using Serilog;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.Documents;
using KadOzenka.Dal.Groups;
using KadOzenka.Web.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using KadOzenka.Web.SignalR.AnalogCheck;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics;
using Platform.Web.SignalR.Messages;
using Serilog.Context;
using SerilogTimings;

namespace CIPJS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            SpreadsheetInfo.SetLicense("ERDD-TNCL-YKZ5-3ZTU");

            Configuration = configuration;

            // Запуск службы фоновых процессов (для отладки)
            if (ConfigurationManager.AppSettings["StartLongProcessService"].ParseToBoolean())
            {
                LongProcessManagementService service = new LongProcessManagementService();
                service.Start();
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            using (Operation.Time("Конфигурация сервисов"))
            {

                //Добавляет поддержку кодировок 1251, 866
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            services.AddTransient<CoreUiService>();
            services.AddTransient<RegistersService>();
            services.AddTransient<DashboardService>();
            
            services.AddTransient<GbuObjectService>();
            services.AddTransient<TaskService>();
            services.AddTransient<TourFactorService>();
	        services.AddTransient<GbuLongProcessesService>();
	        services.AddSingleton<GbuCurrentLongProcessesListenerService>();
	        services.AddTransient<ScoreCommonService>();
			services.AddTransient<ExpressScoreService>();
	        services.AddTransient<ExpressScoreReferenceService>();
	        services.AddTransient<ViewRenderService>();
	        services.AddTransient<ModelingService>();
	        services.AddTransient<MapBuildingService>();
	        services.AddTransient<DashboardWidgetService>();
	        services.AddTransient<StatisticsReportsService>();
	        services.AddTransient<StatisticsReportsExportService>();
	        services.AddTransient<TourService>();
	        services.AddTransient<RegisterAttributeService>();
	        services.AddTransient<SystemAttributeSettingsService>();
	        services.AddTransient<TemplateService>();
	        services.AddTransient<GroupService>();
	        services.AddTransient<DocumentService>();
	        services.AddTransient<ModelFactorsService>();
            services.AddSingleton<KoUnloadResultsListenerService>();
            services.AddSingleton<OutliersCheckingListenerService>();
            services.AddSingleton<DictionaryService>();
            services.AddSingleton<EsHubService>();
            services.AddSingleton<SignalRMessageService>();

                services.AddTransient<GbuObjectService>();
                services.AddTransient<TaskService>();
                services.AddTransient<TourFactorService>();
                services.AddTransient<GbuLongProcessesService>();
                services.AddSingleton<GbuCurrentLongProcessesListenerService>();
                services.AddTransient<ScoreCommonService>();
                services.AddTransient<ExpressScoreService>();
                services.AddTransient<ExpressScoreReferenceService>();
                services.AddTransient<ViewRenderService>();
                services.AddTransient<ModelingService>();
                services.AddTransient<MapBuildingService>();
                services.AddTransient<DashboardWidgetService>();
                services.AddTransient<StatisticsReportsService>();
                services.AddTransient<StatisticsReportsExportService>();
                services.AddTransient<TourService>();
                services.AddTransient<RegisterAttributeService>();
                services.AddTransient<SystemAttributeSettingsService>();
                services.AddTransient<TemplateService>();
                services.AddTransient<GroupService>();
                services.AddTransient<DocumentService>();
                services.AddTransient<ModelFactorsService>();
                services.AddSingleton<KoUnloadResultsListenerService>();
                services.AddSingleton<OutliersCheckingListenerService>();
                services.AddSingleton<DictionaryService>();
                services.AddSingleton<EsHubService>();
                services.AddSingleton<UrgentMessageService>();

                services.AddHttpContextAccessor();
                services.AddSession(options =>
                {
                    options.Cookie.Name = "CIPJS.Session";
                    options.IdleTimeout = TimeSpan.FromMinutes(60);
                });
                services.AddKendo();
                services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options => { options.LoginPath = new PathString("/Account/Login"); });
                services.AddMvc(opts =>
                    {
                        opts.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                            .Build()));
                        opts.Filters.Add(new MasterPageHeaderAttribute());
                    })
                    .AddJsonOptions(options =>
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver());
                services.Configure<FormOptions>(x =>
                {
                    x.ValueLengthLimit = int.MaxValue;
                    x.MultipartBodyLengthLimit = int.MaxValue;
                });

                //init AutoMapping
                Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
                services.AddAutoMapper();
                services.AddSignalR(hubOptions => { hubOptions.EnableDetailedErrors = true; });

                services.AddMemoryCache();

                string keysFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "temp-keys");
                services.AddDataProtection()
                    .PersistKeysToFileSystem(new DirectoryInfo(keysFolder))
                    .SetDefaultKeyLifetime(TimeSpan.FromDays(14));

                var cultureInfo = new CultureInfo("ru-RU");
                CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (Operation.Time("Конфигурация pipeline http-запросов"))
            {
                app.UseMiddleware<SerilogMiddleware>();
                app.UseFastReport();
                app.UseExceptionHandler(
                    builder =>
                    {
                        builder.Run(
                            async context =>
                            {
                                IExceptionHandlerFeature error = context.Features.Get<IExceptionHandlerFeature>();
                                if (error != null)
                                {
                                    await Task.Factory.StartNew(() => ErrorManager.LogServerError(error.Error));
                                }
                            });
                    });

                app.UseStaticFiles();

                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), "Config", "Reports")),
                    RequestPath = "/ConfigReports"
                });

                app.UseSession();
                app.UseAuthentication();

            app.UseSignalR(routes =>
            {
                routes.MapHub<GbuLongProcessesProgressBarHub>("/gbuLongProcessesProgressBar");
                routes.MapHub<KoUnloadResultsProgressHub>("/koUnloadResultsProgress");
                routes.MapHub<OutliersCheckingHub>("/marketOutliersCheckingProgress");
                routes.MapHub<EsHub>("/esCheckProgress");
                routes.MapHub<ActivateCoordinates>("/ActivateCoordinates");
                routes.MapHub<ActivateDistrictsRegionsZones>("/ActivateDistrictsRegionsZones");
                routes.MapHub<UrgentMessageHub>("/coreMessageData");
                routes.MapHub<NotificationMessageHub>("/coreMessagesList");
            });

                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "Register",
                        template: "RegistersView/{registerId}",
                        defaults: new {controller = "RegistersView", action = "Index"});
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });
                app.UseSerilogRequestLogging();
                app.UseWebSockets();

                app.Use(async (context, next) =>
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        switch (context.Request.Path)
                        {
                            case "/DuplicateProgress":
                                System.Net.WebSockets.WebSocket
                                    socket = await context.WebSockets.AcceptWebSocketAsync();
                                await new SocketPool().SendMessage(socket, Duplicates.GetCurrentProgress());
                                await new SocketPool().SendMessage(socket, Duplicates.GetListOfMarkets());
                                await new SocketPool().AddSocket(socket);
                                break;
                        }
                    }
                    else await next();
                });

                HttpContextHelper.HttpContextAccessor = app.ApplicationServices.GetService<IHttpContextAccessor>();
                HttpContextHelper.WebRootPath = env.ContentRootPath;
                LogContext.PushProperty("EnvironmentName", env.EnvironmentName);
                LogContext.PushProperty("WebRootPath", env.WebRootPath);
            }
        }
    }
}