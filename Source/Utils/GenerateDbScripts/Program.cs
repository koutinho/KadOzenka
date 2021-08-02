using Core.ErrorManagment;
using Core.Shared.Extensions;
using Npgsql;
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

            if (ConfigurationManager.AppSettings["CreateDbFunctions"].ParseToBoolean())
                new GenerateFunctions(
                        ConfigurationManager.AppSettings["DbExporterConnectionString"],
                        ConfigurationManager.AppSettings["DbExporterConnectionString"].Split("Username=")[1].Split(";")[0],
                        ConfigurationManager.AppSettings["GetFunctionsSQLReuqest"],
                        ConfigurationManager.AppSettings["FunctionsTemplate"],
                        ConfigurationManager.AppSettings["DbExporterBaseFolder"],
                        ConfigurationManager.AppSettings["FunctionsFileName"]
                    ).Generate();

            if (ConfigurationManager.AppSettings["CreateDbTriggers"].ParseToBoolean())
				new GenerateTriggers(
						ConfigurationManager.AppSettings["DbExporterConnectionString"],
						ConfigurationManager.AppSettings["GetTriggersSQLReuqest"],
						ConfigurationManager.AppSettings["TriggersTemplate"],
						ConfigurationManager.AppSettings["DbExporterBaseFolder"],
						ConfigurationManager.AppSettings["TriggersFileName"]
					).Generate();

			if (ConfigurationManager.AppSettings["CreateDbIndexes"].ParseToBoolean())
				new GenerateIndexes(
						ConfigurationManager.AppSettings["DbExporterConnectionString"],
						ConfigurationManager.AppSettings["GetIndexesSQLReuqest"],
						ConfigurationManager.AppSettings["IndexesTemplate"],
						ConfigurationManager.AppSettings["DbExporterBaseFolder"],
						ConfigurationManager.AppSettings["IndexesFileName"]
					).Generate();

			Console.WriteLine("Done");
			Console.ReadLine();
		}

		private static void ExportDb()
		{
			string exportProfileFilename = ConfigurationManager.AppSettings["DbExporterProfile"];
			string connectionString = ConfigurationManager.AppSettings["DbExporterConnectionString"];
			string providerName = ConfigurationManager.AppSettings["DbExporterProviderName"];

			string baseFolder = ConfigurationManager.AppSettings["DbExporterBaseFolder"];
			string providerNameDestination = ConfigurationManager.AppSettings["DbExporterProviderNameDestination"];

			DbExporter.CorrectSystemLayoutsAndFilters();
			
			ExportProfile exportProfile = File.ReadAllText(exportProfileFilename).DeserializeFromXml<ExportProfile>();

			try
			{
				DbExporter dbExporter = new DbExporter(connectionString, providerName);
				dbExporter.GenerateExportScripts(baseFolder, exportProfile, providerNameDestination);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
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

					Console.WriteLine($"При формировании физической таблицы в БД для реестра \"{registerId}\" возникла ошибка: {ex.Message} (подробно в журнале № {errorId})");
				}
			}
		}

	}

}
