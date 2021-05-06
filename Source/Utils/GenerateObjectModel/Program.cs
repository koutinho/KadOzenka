using Core.ObjectModelBuilder;
using Core.Shared.Extensions;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using GenerateObjectModel.XmlParsingSupport;

namespace GenerateObjectModel
{
	class Program
    {
	    static int Main(string[] args)
		{
			Console.WriteLine("Запуск приложения.");

			var mode = GetModeInfo();
			Console.WriteLine($"Подробности конфигурации '{mode}'");

			var filter = mode.RegisterFilter;
			var filterReference = mode.ReferenceFilter;
			var path = mode.Path;

			Console.WriteLine("\n\nНачата генерация ОРМ");
			string objectModel = ObjectModelBuilder.BuildObjectModel(filter);
			File.WriteAllText(path + "ObjectModel.cs", objectModel);
			Console.WriteLine("Закончена работа с ObjectModel.");

			string objectModelEnum = ObjectModelBuilder.BuildObjectModelEnum(filterReference);
			File.WriteAllText(path + "ObjectModelEnum.cs", objectModelEnum);
			Console.WriteLine("Закончена работа с ObjectModelEnum.");

			string objectModelPartial = ObjectModelBuilder.BuildObjectModelPartial(filter);
			File.WriteAllText(path + "ObjectModelPartial.cs", objectModelPartial);
			Console.WriteLine("Закончена работа с ObjectModelPartial.");

			string objectModelPartial2 = ObjectModelBuilder.BuildObjectModelPartial2(filter);
			File.WriteAllText(path + "ObjectModelPartial2.cs", objectModelPartial2);
			Console.WriteLine("Закончена работа с ObjectModelPartial2.");

			string objectModelSRDFunction = ObjectModelBuilder.BuildObjectModelSRDFunction();
			File.WriteAllText(path + "ObjectModelSRDFunction.cs", objectModelSRDFunction);
			Console.WriteLine("Закончена работа с ObjectModelSRDFunction.");


			Console.WriteLine("Завершено. Нажмите любую клавишу для завершения.");
			Console.ReadLine();
            return 0;
        }

		
		#region Support Methods

		private static ModeElement GetModeInfo()
		{
			var modeStr = ConfigurationManager.AppSettings["Mode"].ParseToStringNullable();
			Console.WriteLine($"Выбранная конфигурация '{modeStr}'");

			var config = RegisterModesConfig.GetConfig();

			var mode = config.ModesCollection.Cast<ModeElement>().FirstOrDefault(item => item.Type == modeStr);
			if (mode == null)
				throw new Exception($"Не найден параметр запуска '{modeStr}'");

			return mode;
		}

		#endregion
	}
}
