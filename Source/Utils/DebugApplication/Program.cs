using System;
using System.Collections.Generic;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using DebugApplication.RosreestrParser;
using DebugApplication.TestsAndExamples;
using DebugApplication.YandexFiller;

namespace DebugApplication
{
    class Program
    {
		private static Dictionary<string, Tuple<string, Action>> _commands = new Dictionary<string, Tuple<string, Action>>();

		static void Main(string[] args)
        {
			Console.WriteLine("Введите команду:");
			
			try
			{
				InitCommands();

				var command = Console.ReadLine();
				
				ExecuteCommand(command);
			}
			catch(Exception ex)
			{
				var errorId = ErrorManager.LogError(ex);

				Console.WriteLine($"Возникла ошибка {ex.Message} (журнал №{errorId})");
			}
			
			Console.WriteLine("Done");
			Console.ReadLine();
			return;
        }

		public delegate void Action();

		private static void InitCommands()
		{
			AddCommand("help", "Помощь", PrintHelp);
			AddCommand("h", "Помощь", PrintHelp);
			AddCommand("0", "Помощь", PrintHelp);

			AddCommand("1", "Tests.TestGetAllAttributes", Tests.TestGetAllAttributes);
			
			AddCommand("2", "RosreestrParser.ExcelParser().LoadRosreestrDeals", () => { new RosreestrParser.ExcelParser().LoadRosreestrDeals(); });
			
			AddCommand("3", "Запуск службы выполнения фоновых процессов", () => {
				LongProcessManagementService service = new LongProcessManagementService();
				service.Start();
			});
			
			AddCommand("4", "Запуск выгрузки объявлений объектов-аналогов из сторонних источников", () => { new OuterMarketParser.Launcher.OuterMarketParser().StartProcess(); });
			
			AddCommand("5", "Запуск выгрузки адресов из geocode-maps.yandex.ru", () => { new YandexFiller.AddressesFiller().GetAddresses(); });
			
			AddCommand("6", "Запуск парсинга excele файла с объектами-аналогами из росреестра", () => {
				//new OuterMarketParser.Launcher.OuterMarketParser().ParseExcele();
			});
		}

		private static void AddCommand(string id, string description, Action action)
		{
			_commands.Add(id, new Tuple<string, Action>(description, action));
		}

		private static void ExecuteCommand(string command)
		{
			var commandData = _commands[command];

			Console.WriteLine($"Запустить процедуру {commandData.Item1}? (0 - отмена; 1 - запуск)");

			bool confirmation = Console.ReadLine().ParseToBoolean();

			if(confirmation)
			{
				Console.WriteLine($"Запуск процедуры: {commandData.Item1}");
				commandData.Item2();
			}
			else
			{
				Console.WriteLine($"Запуск отменен");
			}
		}

		private static void PrintHelp()
		{
			foreach(var keyVal in _commands)
			{
				Console.WriteLine($" {keyVal.Key} - {keyVal.Value.Item1};");
				Console.WriteLine();
			}
		}
	}
}
