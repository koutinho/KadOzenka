using System;
using System.Collections.Generic;
using System.Linq;
using CommonSdks.ConfigurationManagers;
using CommonSdks.ConfigurationManagers.WebClients;
using Core.Register.RegisterEntities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using GemBox.Spreadsheet;
using KadOzenka.Dal.Integration._Builders;
using KadOzenka.Dal.Integration._Builders.Task;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
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

		private OMGroup _oksParentGroup;
		protected OMGroup Oks2018ParentGroup
		{
			get
			{
				if (_oksParentGroup == null)
				{
					_oksParentGroup = new GroupBuilder().Algorithm(KoGroupAlgoritm.MainOKS).Parent(-1).Build();
					new TourGroupBuilder().Tour(2018).Group(_oksParentGroup.Id).Build();
				}

				return _oksParentGroup;
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

		protected void AddUnitFactor(RegisterData tourRegister, long unitId, RegisterAttribute tourFactor, object value)
		{
			AddUnitFactor(tourRegister, unitId, new List<RegisterAttribute> {tourFactor}, new List<object> {value});
		}

		protected void AddUnitFactor(RegisterData tourRegister, long unitId, List<RegisterAttribute> tourFactors, List<object> values)
		{
			var sql = @$"insert into {tourRegister.QuantTable} 
								(id, {string.Join(',', tourFactors.Select(x => x.ValueField))}) 
								values ({unitId}, {string.Join(',', values)})";

			var command = DBMngr.Main.GetSqlStringCommand(sql);
			DBMngr.Main.ExecuteNonQuery(command);
		}


		#region Support Methods

		private void InitConfig()
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: false)
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
				.UseWebClientsConfigManager(config)
				.UseCoreConfigManager(config)
				.Build();

		#endregion
	}
}
