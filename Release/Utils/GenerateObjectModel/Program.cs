using Core.ObjectModelBuilder;
using Core.Shared.Extensions;
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
				string filterReference = ConfigurationManager.AppSettings["FilterReference"];


				string path = ConfigurationManager.AppSettings["Path"];

				string objectModel = ObjectModelBuilder.BuildObjectModel(filter);
				File.WriteAllText(path + "ObjectModel.cs", objectModel);

				string objectModelEnum = ObjectModelBuilder.BuildObjectModelEnum(filterReference);
				File.WriteAllText(path + "ObjectModelEnum.cs", objectModelEnum);

				string objectModelPartial = ObjectModelBuilder.BuildObjectModelPartial(filter);
				File.WriteAllText(path + "ObjectModelPartial.cs", objectModelPartial);

				string objectModelPartial2 = ObjectModelBuilder.BuildObjectModelPartial2(filter);
				File.WriteAllText(path + "ObjectModelPartial2.cs", objectModelPartial2);

				string objectModelSRDFunction = ObjectModelBuilder.BuildObjectModelSRDFunction();
				File.WriteAllText(path + "ObjectModelSRDFunction.cs", objectModelSRDFunction);
			}
			
			Console.WriteLine("Завершено. Нажмите любую клавишу для завершения.");
			Console.ReadLine();
            return 0;
        }
    }
}
