using System;
using System.Text;

namespace KadOzenka.Dal.Logger
{

    class ConsoleLog
    {

        public static void WriteData(string data, int length, int current, int correct, int error, int? duplicate = null, int? nspErr = null, int? unpub = null)
        {
            Console.Write($"\r{data}: {current}/{length} {((double)current / length * 100).ToString("0.00")}% " +
                          $"(Корректно: {correct} {((double)correct / length * 100).ToString("0.00")}%; " +
                          (duplicate != null ? $"Дубликаты: {duplicate} {((double)duplicate / length * 100).ToString("0.00")}%; " : string.Empty) +
                          (nspErr != null ? $"Удалённых страниц: {nspErr} {((double)nspErr / length * 100).ToString("0.00")}%; " : string.Empty) +
                          (unpub != null ? $"Снятых с публикации страниц: {unpub} {((double)unpub / length * 100).ToString("0.00")}%; " : string.Empty) +
                          $"С ошибкой: {error} {((double)error / length * 100).ToString("0.00")}%)");
        }

        public static void WriteFotter(string fotter) => Console.WriteLine($"\n{fotter}");

    }

}
