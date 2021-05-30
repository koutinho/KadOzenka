﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using KadOzenka.Dal.Api.Models;
using KadOzenka.Dal.Api.Service;
using KadOzenka.Web.Api.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace KadOzenka.Web.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class ConfigController : ControllerBase
	{
		private ConfigService _configService;

		public ConfigController()
		{
			_configService = new ConfigService();
		}

		[SwaggerResponse(statusCode: 200, type: typeof(string), description: "OK")]
		[SwaggerResponse(statusCode: (int)HttpStatusCode.BadRequest, type: typeof(string), description: "Не удалось получить настройки конфигурации")]
		[Route("GetConfigurations")]
		[HttpGet]
		public IActionResult GetConfigurations()
		{
			string env = "";
			if (Environment.GetEnvironmentVariables().Contains("ASPNETCORE_ENVIRONMENT"))
			{
				env = Environment.GetEnvironmentVariables()["ASPNETCORE_ENVIRONMENT"].ToString();
			}

			ConfigDto config;
			try
			{
				config = _configService.GetSerilogConfig(env);
			
			}
			catch (Exception e)
			{
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
			string env = "";
			if (Environment.GetEnvironmentVariables().Contains("ASPNETCORE_ENVIRONMENT"))
			{
				env = Environment.GetEnvironmentVariables()["ASPNETCORE_ENVIRONMENT"].ToString();
			}

	
			try
			{
				_configService.SetSerilogConfig(env, config);

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
			return Ok();
		}

	}
}
