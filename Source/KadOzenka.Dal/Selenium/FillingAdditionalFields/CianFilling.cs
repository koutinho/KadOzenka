using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using Core.Shared.Extensions;
using KadOzenka.Dal.Extentions;
using KadOzenka.Dal.Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObjectModel.Directory;
using ObjectModel.Market;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace KadOzenka.Dal.Selenium.FillingAdditionalFields
{
	public class CianFilling
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
					driver.Navigate().GoToUrl("https://www.cian.ru/rent/commercial/233329675/");
					((IJavaScriptExecutor)driver).ExecuteScript(File.ReadAllText(ConfigurationManager.AppSettings["CIANGetAdditionalDataJsPath"]));
					var jsObjectData = new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(_ => ((IJavaScriptExecutor)_).ExecuteScript("return window._result;"));
					var deserializedObject = (JObject)JsonConvert.DeserializeObject(jsObjectData.ToString());
					var initialObject = new OMCoreObject();
					UpdateObject(deserializedObject, initialObject);
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
				.Where(x => x.Market_Code == MarketTypes.Cian && x.LastDateUpdate == null && x.Url != null)
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
				.Where(x => x.Market_Code == MarketTypes.Cian && x.Url != null &&
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
			ConsoleLog.WriteFotter("Парсинг дополнительных данных для Циан");
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
					try
					{
						driver.Navigate().GoToUrl(marketObject.Url);

						((IJavaScriptExecutor)driver).ExecuteScript(File.ReadAllText(ConfigurationManager.AppSettings["CIANGetAdditionalDataJsPath"]));
						var jsObjectData = new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(_ => ((IJavaScriptExecutor)_).ExecuteScript("return window._result;"));
						var deserializedObject = (JObject)JsonConvert.DeserializeObject(jsObjectData.ToString());
						if (UpdateObject(deserializedObject, marketObject))
						{
							marketObject.Save();
							updateCount++;
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

				ConsoleLog.WriteFotter("Парсинг дополнительных данных для Циан завершен");
			}
		}

		public bool UpdateObject(JObject deserializedObject, OMCoreObject initialObject)
		{
			var isObjectUpdated = false;
			if (!deserializedObject.SelectToken("placementType").IsNullOrEmpty())
			{
				initialObject.PlacementType =
					deserializedObject.SelectToken("placementType").Value<string>();
				isObjectUpdated = true;
			}

			if (!deserializedObject.SelectToken("entranceType").IsNullOrEmpty())
			{
				initialObject.EntranceType =
					deserializedObject.SelectToken("entranceType").Value<string>();
				isObjectUpdated = true;
			}

			if (!deserializedObject.SelectToken("quality").IsNullOrEmpty())
			{
				initialObject.Quality =
					deserializedObject.SelectToken("quality").Value<string>();
				isObjectUpdated = true;
			}

			if (!deserializedObject.SelectToken("isUtilitiesIncluded").IsNullOrEmpty())
			{
				initialObject.IsUtilitiesIncluded =
					deserializedObject.SelectToken("isUtilitiesIncluded").Value<bool>();
				isObjectUpdated = true;
			}

			if (!deserializedObject.SelectToken("isOperatingCostsIncluded").IsNullOrEmpty())
			{
				initialObject.IsOperatingCostsIncluded =
					deserializedObject.SelectToken("isOperatingCostsIncluded").Value<bool>();
				isObjectUpdated = true;
			}

			if (!deserializedObject.SelectToken("vat").IsNullOrEmpty())
			{
				var vatValueString = deserializedObject.SelectToken("vat").Value<string>();
				if (vatValueString == VatType.NDS.GetEnumDescription())
				{
					initialObject.Vat_Code = VatType.NDS;
				}
				else if (vatValueString == VatType.USN.GetEnumDescription())
				{
					initialObject.Vat_Code = VatType.USN;
				}
				else
				{
					initialObject.Vat_Code = VatType.None;
				}
				isObjectUpdated = true;
			}
			else
			{
				initialObject.Vat_Code = VatType.None;
				isObjectUpdated = true;
			}

			return isObjectUpdated;
		}
	}
}
