using System;
using System.Configuration;
using System.IO;
using Core.ObjectModelBuilder;
using Core.Shared.Extensions;

namespace MarketPlaceBusiness
{
    class Program
    {
		static int Main(string[] args)
		{
			Console.WriteLine("Запуск приложения.");

			try
			{
				if (ConfigurationManager.AppSettings["CipjsGenerate"].ParseToBoolean())
				{
					//string providerName = ConfigurationManager.AppSettings["ProviderName"];
					//string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
					string filter = ConfigurationManager.AppSettings["Filter"];
					string filterReference = ConfigurationManager.AppSettings["FilterReference"];


					string path = ConfigurationManager.AppSettings["Path"];
					Console.WriteLine($"Путь: {path}.");

					string objectModel = ObjectModelBuilder.BuildObjectModel(filter);
					File.WriteAllText(path + "ObjectModel.cs", objectModel);
					Console.WriteLine("Закончена работа с ObjectModel.");

					//todo не все enum
					string objectModelEnum = ObjectModelBuilder.BuildObjectModelEnum(filterReference);
					File.WriteAllText(path + "ObjectModelEnum.cs", objectModelEnum);
					Console.WriteLine("Закончена работа с ObjectModelEnum.");

					string objectModelPartial = ObjectModelBuilder.BuildObjectModelPartial(filter);
					File.WriteAllText(path + "ObjectModelPartial.cs", objectModelPartial);
					Console.WriteLine("Закончена работа с ObjectModelPartial.");

					string objectModelPartial2 = ObjectModelBuilder.BuildObjectModelPartial2(filter);
					File.WriteAllText(path + "ObjectModelPartial2.cs", objectModelPartial2);
					Console.WriteLine("Закончена работа с ObjectModelPartial2.");

					//string objectModelSRDFunction = ObjectModelBuilder.BuildObjectModelSRDFunction();
					//File.WriteAllText(path + "ObjectModelSRDFunction.cs", objectModelSRDFunction);
					//Console.WriteLine("Закончена работа с ObjectModelSRDFunction.");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			
			Console.WriteLine("Завершено. Нажмите любую клавишу для завершения.");
			Console.ReadLine();
            return 0;
        }
    }
}
