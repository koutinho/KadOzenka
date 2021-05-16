﻿using AutoMapper;
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
using AutoMapper.Configuration;
using Core.ErrorManagment;
using KadOzenka.Dal.CodDictionary;
using KadOzenka.Dal.WebSocket;
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
using KadOzenka.Dal.CommonFunctions.ExistFolderChecker;
using KadOzenka.Dal.CommonFunctions.Repositories;
using KadOzenka.Dal.Documents;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.LongProcess.Common;
using KadOzenka.Dal.LongProcess.Reports;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using KadOzenka.Dal.Modeling.Repositories;
using KadOzenka.Dal.ObjectsCharacteristics;
using KadOzenka.Dal.ObjectsCharacteristics.Repositories;
using KadOzenka.Dal.RecycleBin;
using KadOzenka.Dal.Tours.Repositories;
using KadOzenka.Web.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using KadOzenka.Web.SignalR.AnalogCheck;
using MarketPlaceBusiness;
using MarketPlaceBusiness.Common;
using MarketPlaceBusiness.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics;
using Platform.Web.SignalR.BackgroundProcessWidget;
using Platform.Web.SignalR.Messages;
using Serilog.Context;
using SerilogTimings;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using MappingProfile = Platform.Web.MappingProfile;

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
            ExistFolderCheckerService.Run();
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
            services.AddTransient<CodDictionaryService>();
            services.AddTransient<TaskService>();
            services.AddTransient<TourFactorService>();
	        services.AddTransient<GbuLongProcessesService>();
	        services.AddSingleton<GbuCurrentLongProcessesListenerService>();
	        //services.AddTransient<ScoreCommonService>();
			//services.AddTransient<ExpressScoreService>();
			//services.AddTransient<ExpressScoreReferenceService>();
	        services.AddTransient<ViewRenderService>();
	        services.AddTransient<ModelingService>();
	        services.AddTransient<MapBuildingService>();
	        services.AddTransient<DashboardWidgetService>();
	        services.AddTransient<StatisticsReportsWidgetService>();
	        services.AddTransient<StatisticsReportsWidgetExportService>();
	        services.AddTransient<TourService>();
	        services.AddTransient<SystemAttributeSettingsService>();
	        services.AddTransient<TemplateService>();
	        services.AddTransient<GroupService>();
	        services.AddTransient<DocumentService>();
	        services.AddTransient<ModelFactorsService>();
            services.AddSingleton<KoUnloadResultsListenerService>();
            //services.AddSingleton<OutliersCheckingListenerService>();
            services.AddSingleton<DictionaryService>();
            services.AddSingleton<EsHubService>();
            services.AddSingleton<SignalRMessageService>();
            services.AddSingleton<StatisticalDataService>();
            services.AddSingleton<CustomReportsService>();
            services.AddTransient(typeof(IModelingRepository), typeof(ModelingRepository));
            services.AddTransient(typeof(IModelingService), typeof(ModelingService));
            services.AddTransient(typeof(IModelObjectsService), typeof(ModelObjectsService));
            services.AddTransient(typeof(ITourRepository), typeof(TourRepository));
            services.AddTransient(typeof(ITourService), typeof(TourService));
            services.AddTransient(typeof(IImportDataLogRepository), typeof(ImportDataLogRepository));
            services.AddTransient(typeof(IModelObjectsRepository), typeof(ModelObjectsRepository));
            services.AddTransient(typeof(IGbuObjectService), typeof(GbuObjectService));
            services.AddTransient(typeof(ICodDictionaryService), typeof(CodDictionaryService));
            services.AddTransient(typeof(IGbuReportService), typeof(GbuReportService));
            services.AddTransient(typeof(ILongProcessService), typeof(LongProcessService));
            services.AddTransient(typeof(IRecycleBinService), typeof(RecycleBinService));
            services.AddTransient(typeof(IRegisterAttributeService), typeof(RegisterAttributeService));
            services.AddTransient(typeof(IRegisterService), typeof(RegisterService));
            services.AddTransient(typeof(IObjectsCharacteristicsService), typeof(ObjectsCharacteristicsService));
            services.AddTransient(typeof(IObjectsCharacteristicsSourceService), typeof(ObjectsCharacteristicsSourceService));
            services.AddTransient(typeof(IObjectCharacteristicsRepository), typeof(ObjectsCharacteristicsRepository));
            services.AddTransient(typeof(ISRDSessionWrapper), typeof(SRDSessionWrapper));
            services.AddTransient(typeof(IRegisterConfiguratorWrapper), typeof(RegisterConfiguratorWrapper));
            services.AddTransient(typeof(IRegisterCacheWrapper), typeof(RegisterCacheWrapper));
            services.AddTransient(typeof(ICodDictionaryService), typeof(CodDictionaryService));
            services.AddTransient(typeof(ICodDictionaryRepository), typeof(CodDictionaryRepository));
            services.AddTransient(typeof(IRegisterObjectWrapper), typeof(RegisterObjectWrapper));
            services.AddTransient(typeof(IRegisterRepository), typeof(RegisterRepository));
            services.AddTransient(typeof(IRegisterAttributeRepository), typeof(RegisterAttributeRepository));
            services.AddTransient(typeof(IRecycleBinRepository), typeof(RecycleBinRepository));
            services.AddTransient(typeof(IMarketObjectsRepository), typeof(MarketObjectsRepository));
            services.AddTransient(typeof(IMarketObjectService), typeof(MarketObjectService));
            services.AddTransient(typeof(IMarketObjectsForMapService), typeof(MarketObjectsForMapService));
            //services.AddTransient(typeof(IMarketObjectsForExpressScoreService), typeof(MarketObjectsForExpressScoreService));

            services.AddSingleton<BackgroundProcessWidgetService>();
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
                    .AddNewtonsoftJson(options =>
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver());
                services.Configure<FormOptions>(x =>
                {
                    x.ValueLengthLimit = int.MaxValue;
                    x.MultipartBodyLengthLimit = int.MaxValue;
                });

                //init AutoMapping
                //Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
                services.AddSingleton(provider => new MapperConfiguration(cfg =>
                {
	                cfg.AddProfile(new MappingProfile());
	                cfg.AddProfile(new MarketPlaceBusiness.Dto.AutoMapper.MappingProfile());
	                cfg.AddProfile(new KadOzenka.Web.Helpers.MappingProfile());
                }).CreateMapper());

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

                //app.UseSignalR(routes =>
                //{
                //    routes.MapHub<GbuLongProcessesProgressBarHub>("/gbuLongProcessesProgressBar");
                //    routes.MapHub<KoUnloadResultsProgressHub>("/koUnloadResultsProgress");
                //    //routes.MapHub<OutliersCheckingHub>("/marketOutliersCheckingProgress");
                //    routes.MapHub<EsHub>("/esCheckProgress");
                //    routes.MapHub<ActivateCoordinates>("/ActivateCoordinates");
                //    routes.MapHub<ActivateDistrictsRegionsZones>("/ActivateDistrictsRegionsZones");
                //    routes.MapHub<UrgentMessageHub>("/coreMessageData");
                //    routes.MapHub<NotificationMessageHub>("/coreMessagesList");
                //    routes.MapHub<BackgroundProcessWidgetHub>("/backgroundUserProcess");
                //});

                //app.UseMvc(routes =>
                //{
                //    routes.MapRoute(
                //        name: "Register",
                //        template: "RegistersView/{registerId}",
                //        defaults: new {controller = "RegistersView", action = "Index"});
                //    routes.MapRoute(
                //        name: "default",
                //        template: "{controller=Home}/{action=Index}/{id?}");
                //});

                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapHub<GbuLongProcessesProgressBarHub>("/gbuLongProcessesProgressBar");
                    endpoints.MapHub<KoUnloadResultsProgressHub>("/koUnloadResultsProgress");
                    endpoints.MapHub<EsHub>("/esCheckProgress");
                    endpoints.MapHub<ActivateCoordinates>("/ActivateCoordinates");
                    endpoints.MapHub<ActivateDistrictsRegionsZones>("/ActivateDistrictsRegionsZones");
                    endpoints.MapHub<UrgentMessageHub>("/coreMessageData");
                    endpoints.MapHub<NotificationMessageHub>("/coreMessagesList");
                    endpoints.MapHub<BackgroundProcessWidgetHub>("/backgroundUserProcess");
                    endpoints.MapControllerRoute(
                        name: "Register",
                        pattern: "RegistersView/{registerId}",
                        defaults: new { controller = "RegistersView", action = "Index" });
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                });

                app.UseSerilogRequestLogging();
                app.UseWebSockets();

                app.Use(async (context, next) =>
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        switch (context.Request.Path)
                        {
                            //case "/DuplicateProgress":
                            //    System.Net.WebSockets.WebSocket
                            //        socket = await context.WebSockets.AcceptWebSocketAsync();
                            //    await new SocketPool().SendMessage(socket, Duplicates.GetCurrentProgress());
                            //    await new SocketPool().SendMessage(socket, Duplicates.GetListOfMarkets());
                            //    await new SocketPool().AddSocket(socket);
                            //    break;
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