using Core.ObjectModelBuilder;
using Core.Shared.Extensions;
using Platform.Configurator;
using System;
using System.Configuration;
using System.IO;

namespace GenerateObjectModel
{
    class Program
    {
		static int Main(string[] args)
		{
			if (ConfigurationManager.AppSettings["CipjsGenerate"].ParseToBoolean())
			{
				string providerName = ConfigurationManager.AppSettings["ProviderName"];
				string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
				string filter = ConfigurationManager.AppSettings["Filter"];


				string path = ConfigurationManager.AppSettings["Path"];

				string objectModel = ObjectModelBuilder.BuildObjectModel(providerName, connectionString, filter);
				File.WriteAllText(path + "ObjectModel.cs", objectModel);

				string objectModelEnum = ObjectModelBuilder.BuildObjectModelEnum(providerName, connectionString);
				File.WriteAllText(path + "ObjectModelEnum.cs", objectModelEnum);

				string objectModelPartial = ObjectModelBuilder.BuildObjectModelPartial(providerName, connectionString, filter);
				File.WriteAllText(path + "ObjectModelPartial.cs", objectModelPartial);

				string objectModelPartial2 = ObjectModelBuilder.BuildObjectModelPartial2(providerName, connectionString, filter);
				File.WriteAllText(path + "ObjectModelPartial2.cs", objectModelPartial2);

				string objectModelSRDFunction = ObjectModelBuilder.BuildObjectModelSRDFunction(providerName, connectionString);
				File.WriteAllText(path + "ObjectModelSRDFunction.cs", objectModelSRDFunction);
			}
			
			Console.WriteLine("Завершено. Нажмите любую клавишу для завершения.");
			Console.ReadLine();
            return 0;
        }
    }
}
