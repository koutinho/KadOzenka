using System;
using KadOzenka.Dal.ConfigurationManagers;
using KadOzenka.WebClients.ConfigurationManagers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using GemBox.Spreadsheet;
using ObjectModel.Core.TD;
using Platform.Main.ConfigurationManagers.CoreConfigurationManager;

namespace KadOzenka.Dal.IntegrationTests
{
	public class BaseTests
	{
		public static OMInstance Document { get; private set; }
		private static bool _initialized;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			//костыль для вызова метода один раз перед запуском любых тестов
			//nUnit позволяет определить OneTimeSetUp только для классов в одном namespace
			if (_initialized) 
				return;
			_initialized = true;

			//TODO создание БД

			InitConfig();

			SpreadsheetInfo.SetLicense("ERDD-TNCL-YKZ5-3ZTU");

			Document = new Task._Builders.DocumentBuilder().Build();
		}


		#region Support Methods

		private void InitConfig()
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.test.json", optional: false)
				.AddEnvironmentVariables()
				.Build();

			try
			{
				BuildWebHost(null, configuration).Run();
			}
			//TODO падает ошибка из-за отсутствия Startup, но если его добавить все зависает
			catch (Exception ex)
			{
				//Console.WriteLine($"Error: {ex.Message}");
				//throw;
			}
		}

		private IWebHost BuildWebHost(string[] args, IConfigurationRoot config) =>
			WebHost.CreateDefaultBuilder(args)
				.UseKoConfigManager(config)
				.UseReonConfigManager(config)
				.UseCoreConfigManager(config)
				.Build();

		#endregion
	}
}
