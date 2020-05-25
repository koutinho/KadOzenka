using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using System.IO;
using KadOzenka.WebServices.Domain.Context;
using KadOzenka.WebServices.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;

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
			services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
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
		}

		private string GetXmlCommentsPath()
		{
			return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CheckoutAd.Api.xml");
		}
	}
}
