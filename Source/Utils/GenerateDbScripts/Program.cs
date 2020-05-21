using Core.ErrorManagment;
using Core.Shared.Extensions;
using Platform.Configurator;
using Platform.Configurator.ExportProfile;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace GenerateDbScripts
{
    class Program
    {
        static void Main(string[] args)
        {
			if (ConfigurationManager.AppSettings["DbExporterEnabled"].ParseToBoolean())
				ExportDb();

			if (ConfigurationManager.AppSettings["CreateDbTableForRegister"].ParseToBoolean())
				CreateDbTableForRegister();
		}

		private static void ExportDb()
		{
			string exportProfileFilename = ConfigurationManager.AppSettings["DbExporterProfile"];
			string connectionString = ConfigurationManager.AppSettings["DbExporterConnectionString"];
			string providerName = ConfigurationManager.AppSettings["DbExporterProviderName"];

			string baseFolder = ConfigurationManager.AppSettings["DbExporterBaseFolder"];
			
			DbExporter.CorrectSystemLayoutsAndFilters();
			
			ExportProfile exportProfile = File.ReadAllText(exportProfileFilename).DeserializeFromXml<ExportProfile>();

			try
			{
				DbExporter dbExporter = new DbExporter(connectionString, providerName);
				dbExporter.GenerateExportScripts(baseFolder, exportProfile);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			

			Console.WriteLine("Done");
			Console.ReadLine();
		}

		private static void CreateDbTableForRegister()
		{
			List<long> registerIds = ConfigurationManager.AppSettings["CreateDbTableForRegisterIds"].Split(",").Select(x => x.ParseToLong()).ToList();

			foreach (var registerId in registerIds)
			{
				try
				{
					RegisterConfigurator.CreateDbTableForRegister(registerId);

					Console.WriteLine($"Физическая таблица сформирована в БД для реестра \"{registerId}\"");
				}
				catch (Exception ex)
				{
					int errorId = ErrorManager.LogError(ex);

					Console.WriteLine($"При формировании физической таблицы в БД для реестра \"{registerId}\" взниклаошбка: {ex.Message} (подробно в журнале № {errorId})");
				}
			}

			Console.WriteLine("Done");
			Console.ReadLine();
		}
	}
}
