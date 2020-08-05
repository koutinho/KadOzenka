using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using KadOzenka.Dal.Logger;
using KadOzenka.Dal.YandexParsing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObjectModel.Directory;
using ObjectModel.Market;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace KadOzenka.Dal.Selenium.FillingAdditionalFields
{
	public class YandexFilling
	{
		public void Test()
		{
			ChromeOptions options = new ChromeOptions();
			ChromeDriverService service = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
			using (ChromeDriver driver = new ChromeDriver(service, options))
			{
				driver.Manage().Window.Maximize();
				try
				{
					driver.Navigate().GoToUrl("https://realty.yandex.ru/offer/5516079589983765680/");
					YandexParserUtils.CheckCapcha(driver);

					((IJavaScriptExecutor)driver).ExecuteScript(File.ReadAllText(ConfigurationManager.AppSettings["YandexGetInitialStateJsPath"]), "https://realty.yandex.ru/offer/5516079589983765680/");
					var jsObjectData = new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(_ => ((IJavaScriptExecutor)_).ExecuteScript("return window._result;"));
					var deserializedObject = (JObject)JsonConvert.DeserializeObject(jsObjectData.ToString());
					var initialObject = new OMCoreObject();
					YandexParserUtils.FillAdditionalData(deserializedObject, initialObject);
					Console.WriteLine($"Тип помещения:'{initialObject.PlacementType}', "
					+ $"Тип входа:'{initialObject.EntranceType}', "
					+ $"Состояние:'{initialObject.Quality}', "
					+ $"Коммунальные платежи включены:'{initialObject.IsUtilitiesIncluded}', "
					+ $"Эксплуатационные расходы включены:'{initialObject.IsOperatingCostsIncluded}', "
					+ $"НДС:'{initialObject.Vat}'; ");

				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}

		private List<OMCoreObject> GetInitialObjects()
		{
			return OMCoreObject
				.Where(x => x.Market_Code == MarketTypes.YandexProterty && x.LastDateUpdate == null && x.Url != null)
				.Select(x => new
				{
					x.Url,
					x.LastDateUpdate,
					x.PlacementType,
					x.EntranceType,
					x.Quality,
					x.IsUtilitiesIncluded,
					x.IsOperatingCostsIncluded,
					x.Vat,
				})
				.Execute();
		}

		private List<OMCoreObject> GetExistingObjects()
		{
			return OMCoreObject
				.Where(x => x.Market_Code == MarketTypes.YandexProterty && x.Url != null &&
				            (x.ProcessType_Code == ProcessStep.InProcess || x.ProcessType_Code == ProcessStep.Dealed))
				.Select(x => new
				{
					x.Url,
					x.LastDateUpdate,
					x.PlacementType,
					x.EntranceType,
					x.Quality,
					x.IsUtilitiesIncluded,
					x.IsOperatingCostsIncluded,
					x.Vat,
				})
				.Execute();
		}

		public void FillAdditionalData(bool fillInitialObjects = true)
		{
			ConsoleLog.WriteFotter("Парсинг дополнительных данных для Яндекс недвижимость");
			var objects = fillInitialObjects ? GetInitialObjects() : GetExistingObjects();
			
			ChromeOptions options = new ChromeOptions();
			ChromeDriverService service = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
			using (ChromeDriver driver = new ChromeDriver(service, options))
			{
				driver.Manage().Window.Maximize();
				Console.WriteLine($"Получено объектов: {objects.Count}");
				int totalCount = objects.Count, currentCount = 0, correctCount = 0, updateCount = 0, errorCount = 0;
				foreach (OMCoreObject marketObject in objects)
				{
					DateTime currentTime = DateTime.Now;
					try
					{
						driver.Navigate().GoToUrl(marketObject.Url);
						YandexParserUtils.CheckCapcha(driver);

						((IJavaScriptExecutor)driver).ExecuteScript(File.ReadAllText(ConfigurationManager.AppSettings["YandexGetInitialStateJsPath"]), marketObject.Url);
						var jsObjectData = new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(_ => ((IJavaScriptExecutor)_).ExecuteScript("return window._result;"));
						
						if (jsObjectData.ToString().StartsWith("error!"))
						{
							Console.WriteLine($"\nНе удалось обработать объект по ссылке '{marketObject.Url}':{jsObjectData}");
							errorCount++;
						}
						else
						{
							var deserializedObject = (JObject)JsonConvert.DeserializeObject(jsObjectData.ToString());
							if (YandexParserUtils.FillAdditionalData(deserializedObject, marketObject))
							{
								marketObject.LastDateUpdate = currentTime;
								marketObject.Save();
								updateCount++;
							}
						}

						correctCount++;
					}
					catch (Exception ex)
					{
						Console.WriteLine($"\nНе удалось обработать объект по ссылке '{marketObject.Url}':{ex.Message}, {ex.StackTrace}");
						errorCount++;
					}
					currentCount++;
					ConsoleLog.WriteData("Обработка объектов", totalCount, currentCount, correctCount, errorCount, updated: updateCount);
				}

				ConsoleLog.WriteFotter("Парсинг дополнительных данных для Яндекс недвижимость завершен");
			}
		}
	}
}
