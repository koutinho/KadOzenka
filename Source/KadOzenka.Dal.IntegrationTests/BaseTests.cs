using System;
using KadOzenka.Dal.ConfigurationManagers;
using KadOzenka.WebClients.ConfigurationManagers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using GemBox.Spreadsheet;
using Platform.Main.ConfigurationManagers.CoreConfigurationManager;

namespace KadOzenka.Dal.IntegrationTests
{
	public class BaseTests
	{
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
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
