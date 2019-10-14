﻿using AutoMapper;
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
using System.IO;
using System.Text;
using System.Threading.Tasks;

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
            //Добавляет поддержку кодировок 1251, 866
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            services.AddTransient<CoreUiService>();
            services.AddTransient<RegistersService>();
			services.AddTransient<DashboardService>();

			services.AddTransient<GbuObjectService>();


			services.AddHttpContextAccessor();
            services.AddSession(options =>
            {
                options.Cookie.Name = "CIPJS.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(60);
            });
            services.AddKendo();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                });
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver()); ;
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
            });
           
            //init AutoMapping
            Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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

			if(ConfigurationManager.AppSettings["UsePlatformWwwroot"].ParseToBoolean())
			{
				app.UseStaticFiles(new StaticFileOptions
				{
					FileProvider = new PhysicalFileProvider("\\\\192.168.3.151\\версии систем\\Platform.wwwroot\\Platform.Web.1.0.68.wwwroot"),
					RequestPath = ""
				});
			}

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

            HttpContextHelper.HttpContextAccessor = app.ApplicationServices.GetService<IHttpContextAccessor>();
            HttpContextHelper.WebRootPath = env.ContentRootPath;
        }
    }
}