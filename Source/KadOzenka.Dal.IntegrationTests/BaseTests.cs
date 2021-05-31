using System;
using KadOzenka.Dal.ConfigurationManagers;
using KadOzenka.WebClients.ConfigurationManagers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using GemBox.Spreadsheet;
using KadOzenka.Dal.Integration._Builders;
using KadOzenka.Dal.Integration._Builders.Task;
using ObjectModel.Core.TD;
using ObjectModel.KO;
using Platform.Main.ConfigurationManagers.CoreConfigurationManager;

namespace KadOzenka.Dal.IntegrationTests
{
	public class BaseTests
	{
		private OMInstance _document;
		protected OMInstance Document
		{
			get
			{
				if (_document == null)
					_document = new DocumentBuilder().Build();

				return _document;
			}
		}

		private OMTour _tour;
		protected OMTour Tour
		{
			get
			{
				if (_tour == null)
					_tour = new TourBuilder().Build();

				return _tour;
			}
		}

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
