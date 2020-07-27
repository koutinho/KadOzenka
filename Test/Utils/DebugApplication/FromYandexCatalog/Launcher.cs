using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronXL;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ObjectModel.Market;

namespace DebugApplication.FromYandexCatalog
{
    class Launcher
    {

        private static string defaultPath = @"C:\Users\silanov\Desktop";

        private List<string> error = new List<string>();
        private List<CatalogElement> initialList;
        private int counter = 24000;
        private int startValue = Convert.ToInt32(File.ReadAllText($@"{defaultPath}\settings.data"));
        private int chunkSize = 1000;
        private int yandexCurrentCounter = 0;
        private int yandexCorrectCounter = 0;
        private int yandexErrorCounter = 0;
        private int postgresCurrentCounter = 0;
        private int postgresCorrectCounter = 0;
        private int postgresErrorCounter = 0;

        object __lockObjYandexCurrentCounter = new object();
        object __lockObjYandexCorrectCounter = new object();
        object __lockObjYandexErrorCounter = new object();

        public void Launch()
        {
            initialList = new List<CatalogElement>();
            ParseExcele();
            List<List<CatalogElement>> chunks = SplitList(initialList, chunkSize).ToList();
            int yandexAllCounter = initialList.Count;
            List<Task> CTF = new List<Task>();
            chunks.ForEach(
                x => CTF.Add(new Task(() => 
                    x.ForEach(y => 
                    {
                        try
                        {
                            y.GetYandexAddress();
                            lock(__lockObjYandexCorrectCounter) yandexCorrectCounter++;
                        }
                        catch (Exception ex)
                        {
                            lock(__lockObjYandexErrorCounter)
                            {
                                error.Add($"({y.link}):{ex.Message}");
                                y.yandexAddress = null;
                                yandexErrorCounter++;
                            }
                        }
                        lock(__lockObjYandexCurrentCounter)
                        {
                            yandexCurrentCounter++;
                            Logger("Получение адресов при помощи yandex API", yandexAllCounter, yandexCurrentCounter, yandexCorrectCounter, yandexErrorCounter);
                        }
                    }))
                )
            );
            CTF.ForEach(x => x.Start());
            Task.WaitAll(CTF.ToArray());
            LogFotter("Получение адресов при помощи yandex API Завершено");
            initialList.RemoveAll(x => x.yandexAddress == null);
            LogFotter("Удаление NULL-значений завершено");
            SaveDataToPostgres();
            LogError(string.Join("\n", error));
            SaveLast();
        }

        private void ParseExcele()
        {
            List<RangeRow> rows = WorkBook.Load($@"{defaultPath}\Здание_КН и Адрес.xlsx").WorkSheets.First().Rows.Skip(startValue).Take(counter).ToList();
            int rowCount = rows.Count, correctCount = 0, errorCount = 0;
            for(int i = 0; i < rowCount; i++)
            {
                string cadastralNumber = string.Empty;
                string placement = string.Empty;
                string initialAddress = string.Empty;
                string address = string.Empty;
                try
                {
                    RangeRow row = rows[i];
                    cadastralNumber = row.ElementAt(1).ToString();
                    placement = row.ElementAt(2).ToString();
                    initialAddress = row.ElementAt(3).ToString();
                    address = initialAddress.Equals("77") || initialAddress.Equals("101000, 77, аал Кремль, д б/н") ? placement : initialAddress.ToString();
                    initialList.Add(new CatalogElement(cadastralNumber, address));
                    correctCount++;
                }
                catch (Exception ex)
                {
                    error.Add($"({i}, {cadastralNumber}, {address}):{ex.Message}");
                    errorCount++;
                }
                Logger("Создано исходных объектов", rowCount, i+1, correctCount, errorCount);
            }
            LogFotter("Создано исходных объектов завершено");
        }

        private void SaveLast() => File.WriteAllText($@"{defaultPath}\settings.data", (startValue + initialList.Count).ToString());

        private void Logger(string header, int all, int current, int correct, int error)
        {
            Console.Write($"\r{header}: {current}/{all} ({(((double)current) / all * 100).ToString("0.00")}%) (" + 
                          $"Корректно: {correct} ({(((double)correct) / all * 100).ToString("0.00")}%); " + 
                          $"С ошибкой: {error} ({(((double)error) / all * 100).ToString("0.00")}%))");
        }

        private void LogFotter(string footer) => Console.WriteLine($"\n{footer}");

        private void LogError(string error) => File.AppendAllText($@"{defaultPath}\Error.log", $"[{DateTime.Now}]:\n{error}\n\n");

        private static IEnumerable<List<CatalogElement>> SplitList(List<CatalogElement> data, int nSize)
        {
            for (int i = 0; i < data.Count; i += nSize)  yield return data.GetRange(i, Math.Min(nSize, data.Count - i));
        }

        private void SaveDataToPostgres()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions{ CancellationToken = cancelTokenSource.Token, MaxDegreeOfParallelism = 5 };
            List<OMYandexAddress> addresses = OMYandexAddress.Where(y => true).SelectAll().Execute();
            int allCounter = initialList.Count;
            Parallel.ForEach(initialList, options, x =>
            {
                try
                {
                    List<OMYandexAddress> data = addresses.Where(y => x.yandexAddress.FormalizedAddress.Equals(y.FormalizedAddress) &&
                                                                      x.yandexAddress.CadastralNumber.Equals(y.CadastralNumber)).ToList();
                    if (data.Count == 0) x.yandexAddress.Save();
                    postgresCorrectCounter++;
                }
                catch (Exception ex)
                {
                    error.Add($"({x.yandexAddress}):{ex.Message}");
                    postgresErrorCounter++;
                }
                postgresCurrentCounter++;
                Logger("Запись данных в postgres", allCounter, postgresCurrentCounter, postgresCorrectCounter, postgresErrorCounter);
            });
            LogFotter("Запись данных в postgres завершена");
        }

    }
}
