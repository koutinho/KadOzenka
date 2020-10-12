using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using KadOzenka.WebServices.Domain.Context;
using KadOzenka.WebServices.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using Core.ErrorManagment;
using KadOzenka.WebServices.Services.Rsm;

namespace KadOzenka.WebServices
{
	public class Startup
	{
		public IConfiguration AppConfiguration { get; set; }

		/// <summary>
		/// Startup
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration)
		{
			AppConfiguration = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
		}
		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		/// </summary>
		/// <param name="services"></param>
		public void ConfigureServices(IServiceCollection services)
		{
			//если подключим платформу, нужно раскомментить, чтобы сваггер сгененрировал файл для контроллеров только из этого проекта
			services.AddMvc()//(c => c.Conventions.Add(new ApiExplorerSpecialConvention()))
				.AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "Web Service",
					Description = "Web Service (ASP.NET Core 2.1)",
				});
				c.CustomSchemaIds(type => type.FullName);
				c.IncludeXmlComments(GetXmlCommentsPath());
			});

			services.AddDbContext<ApplicationContext>(o =>
			{
				o.UseNpgsql(AppConfiguration.GetConnectionString("dev"));
			}, ServiceLifetime.Singleton);

			services.AddTransient<JournalService>();
			services.AddTransient<RsmService>();
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

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
		}

		private string GetXmlCommentsPath()
		{
			return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CheckoutAd.Api.xml");
		}


		////Нужен, чтобы сваггер генерировал определения только для методов с аттрибутом ShowInSwagger
		//public class ApiExplorerSpecialConvention : IActionModelConvention
		//{
		//	public void Apply(ActionModel action)
		//	{
		//		action.ApiExplorer.IsVisible = action.Attributes.OfType<ShowInSwaggerAttribute>().Any();
		//	}
		//}
	}
}
