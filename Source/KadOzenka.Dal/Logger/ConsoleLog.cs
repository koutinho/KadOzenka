using System;
using System.Text;

namespace KadOzenka.Dal.Logger
{

    class ConsoleLog
    {

        public static void WriteData(string data, int length, int current, int correct, int error)
        {
            Console.Write($"\r{data}: {current}/{length} {((double)current / length * 100).ToString("0.00")}% " +
                          $"(Корректно: {correct} {((double)correct / length * 100).ToString("0.00")}%; " +
                          $"С ошибкой: {error} {((double)error / length * 100).ToString("0.00")}%)");
        }

        public static void WriteFotter(string fotter) => Console.WriteLine($"\n{fotter}");

    }

}
