using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;

namespace KadOzenka.BlFrontEnd.Logger
{
    class LogData
    {

        public static void WriteData(string data, int length, int current, int correct, int error)
        {
            Console.Write($"\r{data}: {current}/{length} {((double)current / length * 100).ToString("0.00")}% " + 
                          $"(Корректно: {correct} {((double)correct / length * 100).ToString("0.00")}%; " + 
                          $"С ошибкой: {error} {((double)error / length * 100).ToString("0.00")}%)");
        }

        public static void WriteFotter(string fotter) => Console.WriteLine($"\n{fotter}");

        public static void StartLogger() => File.AppendAllText(ConfigurationManager.AppSettings["LogerPath"], $"\n\n[{DateTime.Now}]:");

        public static void LogError(string info, string error) => File.AppendAllText(ConfigurationManager.AppSettings["LogerPath"], $"\n{info}:{error}");

    }
}
