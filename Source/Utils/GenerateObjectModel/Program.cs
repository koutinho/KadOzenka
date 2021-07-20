using Core.ObjectModelBuilder;
using Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using GenerateObjectModel.JsonParsingSupport;
using Newtonsoft.Json;

namespace GenerateObjectModel
{
	class Program
    {
	    static int Main(string[] args)
		{
			Console.WriteLine("Запуск приложения.");

			var mode = GetModeInfo();
			Console.WriteLine($"Подробности конфигурации '{mode}'");

			var pathForEnumFile = ConfigurationManager.AppSettings["EnumFilePath"].ParseToStringNullable();
			var referenceFilter = ConfigurationManager.AppSettings["ReferenceFilter"].ParseToStringNullable();


			Console.WriteLine("\n\nНачата генерация ORM");

			var objectModel = ObjectModelBuilder.BuildObjectModel(mode.RegisterFilter);
			File.WriteAllText(mode.Path + $"{mode.FileNameStarting}.cs", objectModel);
			Console.WriteLine("Закончена работа с ObjectModel.");

			var objectModelEnum = ObjectModelBuilder.BuildObjectModelEnum(referenceFilter);
			File.WriteAllText(pathForEnumFile + "ObjectModelEnum.cs", objectModelEnum);
			Console.WriteLine("Закончена работа с ObjectModelEnum.");

			var objectModelPartial = ObjectModelBuilder.BuildObjectModelPartial(mode.RegisterFilter);
			File.WriteAllText(mode.Path + $"{mode.FileNameStarting}Partial.cs", objectModelPartial);
			Console.WriteLine("Закончена работа с ObjectModelPartial.");

			var objectModelPartial2 = ObjectModelBuilder.BuildObjectModelPartial2(mode.RegisterFilter);
			File.WriteAllText(mode.Path + $"{mode.FileNameStarting}Partial2.cs", objectModelPartial2);
			Console.WriteLine("Закончена работа с ObjectModelPartial2.");

			//TODO KOMO-33 добавить в платформу фильтры для генерации СРД
			if (mode.Type == "General")
			{
				var objectModelSRDFunction = ObjectModelBuilder.BuildObjectModelSRDFunction();
				File.WriteAllText(mode.Path + $"{mode.FileNameStarting}SRDFunction.cs", objectModelSRDFunction);
				Console.WriteLine("Закончена работа с ObjectModelSRDFunction.");
			}

			Console.WriteLine("Завершено. Нажмите любую клавишу для завершения.");
			Console.ReadLine();
            return 0;
        }

		
		#region Support Methods

		private static Mode GetModeInfo()
		{
			var modeStr = ConfigurationManager.AppSettings["Mode"].ParseToStringNullable();
			Console.WriteLine($"Выбранная конфигурация '{modeStr}'");

			var fileContent = File.ReadAllText("appsettings.json");
			var allModes = JsonConvert.DeserializeObject<List<Mode>>(fileContent);

			var mode = allModes?.FirstOrDefault(item => item.Type == modeStr);
			if (mode == null)
				throw new Exception($"Не найден параметр запуска '{modeStr}'");

			mode.RegisterFilter = string.Format(mode.RegisterFilter, RegisterIdsForAnalogs);

			return mode;
		}

		public const string RegisterIdsForAnalogs = "100, 101, 105, 107, 110, 118, 119";

		#endregion
	}
}
