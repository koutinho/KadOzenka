using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using KadOzenka.Dal.Api.Models;
using KadOzenka.Dal.Api.Service;
using KadOzenka.Web.Api.Dto;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace KadOzenka.Web.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class ConfigController : ControllerBase
	{
		private readonly ILogger _log = Log.ForContext<ConfigController>();
		private readonly ConfigService _configService;

		public ConfigController()
		{
			string env = "";
			if (Environment.GetEnvironmentVariables().Contains("ASPNETCORE_ENVIRONMENT"))
			{
				env = Environment.GetEnvironmentVariables()["ASPNETCORE_ENVIRONMENT"].ToString();
			}

			_configService = new ConfigService(_log, env);
		}

		[SwaggerResponse(statusCode: 200, type: typeof(string), description: "OK")]
		[SwaggerResponse(statusCode: (int)HttpStatusCode.BadRequest, type: typeof(string), description: "Не удалось получить настройки конфигурации")]
		[Route("GetConfigurations")]
		[HttpGet]
		public IActionResult GetConfigurations()
		{

			ConfigDto config;
			try
			{
				config = _configService.GetSerilogConfig();
			
			}
			catch (Exception e)
			{
				_log.Error(e, e.Message);
				return BadRequest(e.Message);
			}
			return Ok(config);
		}

		[SwaggerResponse(statusCode: 200, type: typeof(string), description: "OK")]
		[SwaggerResponse(statusCode: (int)HttpStatusCode.BadRequest, type: typeof(string), description: "Не удалось установить настройки конфигурации")]
		[Route("SetConfigurations")]
		[HttpPost]
		public IActionResult SetConfigurations(ConfigDto config)
		{
			try
			{
				_configService.SetSerilogConfig(config);

			}
			catch (Exception e)
			{
				_log.Error(e, e.Message);
				return BadRequest(e.Message);
			}
			return Ok();
		}

	}
}
