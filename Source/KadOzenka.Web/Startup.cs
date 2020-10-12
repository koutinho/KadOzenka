using AutoMapper;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.UI.Registers.Services;
using GemBox.Spreadsheet;
using KadOzenka.Dal.GbuObject;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
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
using Microsoft.Extensions.Logging;
using Serilog;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.Data.Helpers;
using Serilog.Context;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.Documents;
using KadOzenka.Dal.Groups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

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
            Log.Information("ConfigureServices called");
      
            //Добавляет поддержку кодировок 1251, 866
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            services.AddTransient<CoreUiService>();
            services.AddTransient<RegistersService>();
			services.AddTransient<DashboardService>();
			services.AddTransient<GbuObjectService>();
			services.AddTransient<TaskService>();
            services.AddTransient<TourFactorService>();
	        services.AddTransient<GbuLongProcessesService>();
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
	        services.AddTransient<UpdateCadastralDataService>();
	        services.AddTransient<TemplateService>();
	        services.AddTransient<GroupService>();
	        services.AddTransient<DocumentService>();
	        services.AddSingleton<KoUnloadResultsListenerService>();
	        services.AddSingleton<DictionaryService>();

            services.AddHttpContextAccessor();
            services.AddSession(options =>
            {
                options.Cookie.Name = "CIPJS.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
            services.AddKendo();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                });
            services.AddMvc(opts =>
	            {
		            opts.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
	            })
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver()); ;
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
            });
           
            //init AutoMapping
            Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
            services.AddAutoMapper();
	        services.AddSignalR(hubOptions =>
	        {
		        hubOptions.EnableDetailedErrors = true;
	        });
	        services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Log
                .ForContext("EnvironmentName", env.EnvironmentName)
                .ForContext("WebRootPath", env.WebRootPath)
                .Information("Configure called");

            // loggerFactory.AddSerilog();
            //if (env.IsDevelopment())
            //{
            //    app.UseBrowserLink();
            //    app.UseDeveloperExceptionPage();
            //    app.UseDatabaseErrorPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}
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
            app.UseMvc(routes =>
            {
				routes.MapRoute(
                    name: "Register",
                    template: "RegistersView/{registerId}", 
                    defaults: new { controller = "RegistersView", action = "Index" });
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
                            System.Net.WebSockets.WebSocket socket = await context.WebSockets.AcceptWebSocketAsync();
                            await new SocketPool().SendMessage(socket, Duplicates.GetCurrentProgress());
                            await new SocketPool().AddSocket(socket);
                            break;
                    }
                }
                else await next();
            });

            app.UseSignalR(routes =>
	        {
		        routes.MapHub<GbuLongProcessesProgressBarHub>("/gbuLongProcessesProgressBar");
		        routes.MapHub<KoUnloadResultsProgressHub>("/koUnloadResultsProgress");
	        });

			HttpContextHelper.HttpContextAccessor = app.ApplicationServices.GetService<IHttpContextAccessor>();
            HttpContextHelper.WebRootPath = env.ContentRootPath;
        }
    }
}