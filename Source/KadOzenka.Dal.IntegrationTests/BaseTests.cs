using System;
using Core.Register.RegisterEntities;
using KadOzenka.Dal.ConfigurationManagers;
using KadOzenka.WebClients.ConfigurationManagers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using GemBox.Spreadsheet;
using KadOzenka.Dal.Integration._Builders;
using KadOzenka.Dal.Integration._Builders.Task;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.TD;
using ObjectModel.KO;
using Platform.Main.ConfigurationManagers.CoreConfigurationManager;

namespace KadOzenka.Dal.IntegrationTests
{
	public class BaseTests
	{
		private OMInstance _firstDocument;
		protected OMInstance FirstDocument
		{
			get
			{
				if (_firstDocument == null)
					_firstDocument = new DocumentBuilder().Build();

				return _firstDocument;
			}
		}

		private OMInstance _secondDocument;
		protected OMInstance SecondDocument
		{
			get
			{
				if (_secondDocument == null)
					_secondDocument = new DocumentBuilder().Build();

				return _secondDocument;
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
		public void BaseOneTimeSetUp()
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

		protected void AddUnitFactor(RegisterData tourRegister, RegisterAttribute tourFactor, long unitId, object value)
		{
			var sql = $"insert into {tourRegister.QuantTable} (id, {tourFactor.ValueField}) values ({unitId}, {value})";

			var command = DBMngr.Main.GetSqlStringCommand(sql);
			DBMngr.Main.ExecuteNonQuery(command);
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
