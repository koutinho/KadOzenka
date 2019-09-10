using Core.Shared.Extensions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Platform.Configurator;
using Platform.Configurator.ExportProfile;
using System;
using System.Configuration;
using System.IO;

namespace GenerateDbScripts
{
    class Program
    {
        static void Main(string[] args)
        {
			ExportDb();
		}

		private static void ExportDb()
		{
			string exportProfileFilename = ConfigurationManager.AppSettings["DbExporterProfile"];
			string connectionString = ConfigurationManager.AppSettings["DbExporterConnectionString"];
			string providerName = ConfigurationManager.AppSettings["DbExporterProviderName"];

			string baseFolder = ConfigurationManager.AppSettings["DbExporterBaseFolder"];


            //var command = DBMngr.Realty.GetStoredProcCommand("Core_Register_PKG.CorrectSystemLayoutsAndFilters");
            //DBMngr.Realty.ExecuteNonQuery(command);


            ExportProfile exportProfile = File.ReadAllText(exportProfileFilename).DeserializeFromXml<ExportProfile>();

			DbExporter dbExporter = new DbExporter(connectionString, providerName);
			dbExporter.GenerateExportScripts(baseFolder, exportProfile);

			Console.WriteLine("Done");
			Console.ReadLine();
		}
    }
}
