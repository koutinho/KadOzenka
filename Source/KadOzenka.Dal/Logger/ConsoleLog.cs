using System;
using System.IO;
using System.Text;
using System.Configuration;

namespace KadOzenka.Dal.Logger
{

    public class ConsoleLog
    {

        public static void WriteData(string data, int length, int current, int correct, int error, 
                                     int? duplicate = null, int? nspErr = null, int? unpub = null, 
                                     int? screen = null, int? auc = null)
        {
            Console.Write($"\r{data}: {current}/{length} {((double)current / length * 100).ToString("0.00")}% " +
                          $"(Корректно: {correct} {((double)correct / length * 100).ToString("0.00")}%; " +
                          (screen != null ? $"Со скриншотами: {screen} {((double)screen / length * 100).ToString("0.00")}%; " : string.Empty) +
                          (duplicate != null ? $"Дубликаты: {duplicate} {((double)duplicate / length * 100).ToString("0.00")}%; " : string.Empty) +
                          (nspErr != null ? $"Удалённых страниц: {nspErr} {((double)nspErr / length * 100).ToString("0.00")}%; " : string.Empty) +
                          (unpub != null ? $"Снятых с публикации страниц: {unpub} {((double)unpub / length * 100).ToString("0.00")}%; " : string.Empty) +
                          (auc != null ? $"Аукцион: {auc} {((double)auc / length * 100).ToString("0.00")}%; " : string.Empty) +
                          $"С ошибкой: {error} {((double)error / length * 100).ToString("0.00")}%)");
        }

        public static string GetResultData(int length, int current, int correct, int error, int? nspErr = null, int? unpub = null, int? screen = null, int? auc = null)
        {
            return $"Всего: {current}/{length} {((double)current / length * 100).ToString("0.00")}% " + 
                   $"Корректно: {correct} {((double)correct / length * 100).ToString("0.00")}%; " +
                   (screen != null ? $"Со скриншотами: {screen} {((double)screen / length * 100).ToString("0.00")}%; " : string.Empty) +
                   (nspErr != null ? $"Удалённых страниц: {nspErr} {((double)nspErr / length * 100).ToString("0.00")}%; " : string.Empty) +
                   (unpub != null ? $"Снятых с публикации страниц: {unpub} {((double)unpub / length * 100).ToString("0.00")}%; " : string.Empty) +
                   (auc != null ? $"Аукцион: {auc} {((double)auc / length * 100).ToString("0.00")}%; " : string.Empty) +
                   $"С ошибкой: {error} {((double)error / length * 100).ToString("0.00")}%)";
        }

        public static void WriteFotter(string fotter) => Console.WriteLine($"\n{fotter}");

        public static void LogError(string[] errorList, string procedureName)
        {
            string fileName = string.Format(ConfigurationManager.AppSettings["ErrorLogFile"], DateTime.Now.Date.ToShortDateString()), startFile = File.Exists(fileName) ? "\n\n" : string.Empty;
            if (errorList.Length != 0) File.AppendAllText(fileName, $"{startFile}========{DateTime.Now.ToString()} ({procedureName})========\n{string.Join("\n", errorList)}");
        }

        public static void WriteGbuCadastralFillingData(int length, int current, int fromMainObjectCount, int fromAllpriRosreestrSourceCount, int fromCadastralNumber)
        {
            Console.Write($"\rОбработка объектов: {current}/{length} {((double)current / length * 100).ToString("0.00")}% " +
                          $"(Из gbu_main_object: {fromMainObjectCount} {((double)fromMainObjectCount / length * 100).ToString("0.00")}%; " +
                          $"Из source2_a_txt: {fromAllpriRosreestrSourceCount} {((double)fromAllpriRosreestrSourceCount / length * 100).ToString("0.00")}% " +
                          $"по кадастровому номеру: {fromCadastralNumber} {((double)fromCadastralNumber / length * 100).ToString("0.00")}%;)");
        }
    }

}
