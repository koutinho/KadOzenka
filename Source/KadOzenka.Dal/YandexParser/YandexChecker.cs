using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Reflection;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using ObjectModel.Market;
using ObjectModel.Directory;
using KadOzenka.Dal.Logger;

using OpenQA.Selenium;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace KadOzenka.Dal.YandexParser
{

	public class YandexChecker
	{

		protected List<FormMarketObjectsRequest> FormMarketObjectsRequests { get; set; }
        protected List<OMCoreObject> initialList = OMCoreObject.Where(x => x.Market_Code == MarketTypes.YandexProterty).Select(x => x.Url).Execute();

		public YandexChecker() => FormMarketObjectsRequests = new List<FormMarketObjectsRequest>();

		public void Test(FormMarketObjectsRequest testRequest)
		{
			if (testRequest != null)
			{
				FormMarketObjectsRequests.Add(testRequest);
				DoFormMarketObjects();
			}
		}

		public void FormMarketObjects()
		{
			FormMarketObjectsRequests.Add(new FormMarketObjectsRequest
			{
				ObjectsListUrl = "https://realty.yandex.ru/moskva/kupit/kommercheskaya-nedvizhimost/ofis/?hasFurniture=NO",
				DealType = DealType.SaleSuggestion,
				MarketSegment = MarketSegment.Office,
				PropertyTypeCIPJS = PropertyTypesCIPJS.Placements,
				PropertyType = PropertyTypes.Pllacement,
				Subcategory = "Офисная"
			});
			//FormMarketObjectsRequests.Add(new FormMarketObjectsRequest
			//{
			//	ObjectsListUrl = "https://realty.yandex.ru/moskva/snyat/kommercheskaya-nedvizhimost/ofis/?hasFurniture=NO",
			//	DealType = DealType.RentSuggestion,
			//	MarketSegment = MarketSegment.Office,
			//	PropertyTypeCIPJS = PropertyTypesCIPJS.Placements,
			//	PropertyType = PropertyTypes.Pllacement,
			//	Subcategory = "Офисная"
			//});
			DoFormMarketObjects();
		}

		public void DoFormMarketObjects()
		{
            initialList.ForEach(x => Console.WriteLine(x.Url));
            ChromeOptions options = new ChromeOptions();
			ChromeDriverService service = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
			using (IWebDriver driver = new ChromeDriver(service, options))
			{
				driver.Manage().Window.Maximize();
				foreach (var formMarketObjectsRequest in FormMarketObjectsRequests)
				{
					try
					{
						driver.Navigate().GoToUrl(formMarketObjectsRequest.ObjectsListUrl);
						CheckCapcha((ChromeDriver)driver);
						Console.WriteLine($"Начат парсинг для {formMarketObjectsRequest.ObjectsListUrl}...");
						var objectsUrls = GetObjectsUrlList((IJavaScriptExecutor)driver);
						var pagerButton = ((ChromeDriver)driver).ExecuteScript(ConfigurationManager.AppSettings["getYandexPagerNextButton"]) as IWebElement;
						var nextPage = 1;
						var uriBuilder = new UriBuilder(formMarketObjectsRequest.ObjectsListUrl);
						while (pagerButton != null)
						{
							var query = HttpUtility.ParseQueryString(uriBuilder.Query);
							query["page"] = nextPage.ToString();
							uriBuilder.Query = query.ToString();
							driver.Navigate().GoToUrl(uriBuilder.ToString());
							CheckCapcha((ChromeDriver)driver);
							objectsUrls.AddRange(GetObjectsUrlList((IJavaScriptExecutor)driver));
							pagerButton = ((ChromeDriver)driver).ExecuteScript(ConfigurationManager.AppSettings["getYandexPagerNextButton"]) as IWebElement;
							nextPage++;
						}
						var createdObjects = new List<OMCoreObject>();
						var uniqueObjectsUrls = objectsUrls.Distinct().ToList();
						int KCtr = uniqueObjectsUrls.Count, KCur = 0, KKad = 0, KErr = 0;
						foreach (var objectUrl in uniqueObjectsUrls)
						{
                            Console.WriteLine(objectUrl);
                            try
                            {
                                driver.Navigate().GoToUrl(objectUrl);
                                CheckCapcha((ChromeDriver)driver);
                                ((IJavaScriptExecutor)driver).ExecuteScript(File.ReadAllText(ConfigurationManager.AppSettings["YandexGetInitialStateJsPath"]), objectUrl);
                                var val = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(_ => ((IJavaScriptExecutor)_).ExecuteScript("return window._result;"));
                                var obj = (JObject)JsonConvert.DeserializeObject(val.ToString());
                                createdObjects.Add(FillMarketObject(formMarketObjectsRequest, obj, objectUrl));
                                KKad++;
                            }
                            catch (Exception e) { KErr++; }
                            KCur++;
                            ConsoleLog.WriteData("Обработка объектов", KCtr, KCur, KKad, KErr);
						}
						foreach (var createdObject in createdObjects) createdObject.Save();
						Console.WriteLine($"Парсинг для категории {formMarketObjectsRequest.ObjectsListUrl} завершен. Успешно добавлено {createdObjects.Count} объектов.");
					}
					catch (Exception e) { Console.WriteLine($"Произошла ошибка во время парсинга категории {formMarketObjectsRequest.ObjectsListUrl}:{e.Message}"); }
				}
			}
		}

		private void CheckCapcha(ChromeDriver driver)
		{
			var isCapcha = driver.ExecuteScript(ConfigurationManager.AppSettings["isYandexCapchaScreen"]).ToString();
			if (bool.Parse(isCapcha))
			{
				Console.WriteLine($"Обнаружена капча! Обработайте запрос со страницы браузера и нажмите любую клавишу для продолжения парсинга...");
				Console.ReadKey();
				Console.WriteLine($"Парсинг возобновлен...");
			}
		}

		private List<string> GetObjectsUrlList(IJavaScriptExecutor javaScriptExecutor) => 
            ((ReadOnlyCollection<object>)javaScriptExecutor.ExecuteScript(ConfigurationManager.AppSettings["getYandexUrlList"])).Select(x => (string)x).ToList();

		private OMCoreObject FillMarketObject(FormMarketObjectsRequest formMarketObjectsRequest, JObject obj, string objectUrl)
		{
			var marketObject = new OMCoreObject();
			marketObject.Market_Code = MarketTypes.YandexProterty;
			marketObject.ProcessType_Code = ProcessStep.DoNotProcessed;
			marketObject.CategoryId = 3;
			marketObject.Category = "Коммерческая недвижимость";
			marketObject.Subcategory = formMarketObjectsRequest.Subcategory;
			marketObject.DealType_Code = formMarketObjectsRequest.DealType;
			marketObject.PropertyMarketSegment_Code = formMarketObjectsRequest.MarketSegment;
			marketObject.PropertyType_Code = formMarketObjectsRequest.PropertyType;
			marketObject.PropertyTypesCIPJS_Code = formMarketObjectsRequest.PropertyTypeCIPJS;
			marketObject.MarketId = obj.SelectToken("cards.offers.offerId").Value<long>();
			marketObject.Url = objectUrl;
			marketObject.Price = obj.SelectToken("cards.offers.price").HasValues ? obj.SelectToken("cards.offers.price.value")?.Value<long>() : (long?)null;
			marketObject.ParserTime = DateTime.Now;
			marketObject.Region = null;
			marketObject.City = "Москва";
			marketObject.Address = obj.SelectToken("cards.offers.location").HasValues ? obj.SelectToken("cards.offers.location.address")?.Value<string>() : null;
			if (obj.SelectToken("cards.offers.location.metroList").HasValues)
			{
				var metroList = new List<string>();
				for (var i = 0; i < obj.SelectToken("cards.offers.location.metroList").Count(); i++) metroList.Add(obj.SelectToken($"cards.offers.location.metroList[{i}].name")?.Value<string>());
				marketObject.Metro = string.Join(',', metroList.Where(x => !string.IsNullOrWhiteSpace(x)));
			}
			if (obj.SelectToken("cards.offers.fullImages").HasValues)
			{
				var imagesList = new List<string>();
				for (var i = 0; i < obj.SelectToken("cards.offers.fullImages").Count(); i++) imagesList.Add(obj.SelectToken($"cards.offers.fullImages[{i}]")?.Value<string>());
				var uri = new Uri(objectUrl);
				marketObject.Images = string.Join(',', imagesList.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => uri.Scheme + ":" + x));
			}
			marketObject.Description = obj.SelectToken("cards.offers.description")?.Value<string>();
			marketObject.Lat = obj.SelectToken("cards.offers.location.point").HasValues ? obj.SelectToken("cards.offers.location.point.latitude")?.Value<decimal>() : (decimal?)null;
			marketObject.Lng = obj.SelectToken("cards.offers.location.point").HasValues ? obj.SelectToken("cards.offers.location.point.longitude")?.Value<decimal>() : (decimal?)null;
			marketObject.FloorNumber = obj.SelectToken("cards.offers.floorsOffered[0]")?.Value<long>();
			marketObject.FloorsCount = obj.SelectToken("cards.offers.floorsTotal")?.Value<long>();
			if (formMarketObjectsRequest.PropertyType != PropertyTypes.Stead) marketObject.Area = GetObjectArea(obj);
			marketObject.AreaLand = GetObjectLandArea(obj);
			marketObject.BuildingYear = obj.SelectToken("cards.offers.building.builtYear")?.Value<long>();
			var wallMaterial = DefineWallMaterial(obj);
			if (wallMaterial.HasValue) marketObject.WallMaterial_Code = wallMaterial.Value;
			return marketObject;
		}

		private decimal? GetObjectArea(JObject obj)
		{
			var area = obj.SelectToken("cards.offers.area.value")?.Value<decimal>();
			var unit = obj.SelectToken("cards.offers.area.unit")?.Value<string>();
			var coef = 1;
			switch (unit)
			{
				case "SQUARE_METER":
					coef = 1;
					break;
				case "ARE":
					coef = 100;
					break;
				case "HECTARE":
					coef = 10000;
					break;
			}
			return area * coef;
		}

		private static decimal? GetObjectLandArea(JObject obj)
		{
			var landArea = obj.SelectToken("cards.offers.lot.lotArea.value")?.Value<decimal>();
			var landUnit = obj.SelectToken("cards.offers.lot.lotArea.unit")?.Value<string>();
			var landCoef = 1.0m;
			switch (landUnit)
			{
				case "SQUARE_METER":
					landCoef = 0.01m;
					break;
				case "ARE":
					landCoef = 1;
					break;
				case "HECTARE":
					landCoef = 100;
					break;
			}
			return landArea * landCoef;
		}

		private WallMaterial? DefineWallMaterial(JObject obj)
		{
			WallMaterial? result = null;
			var val = obj.SelectToken("cards.offers.building.buildingType")?.Value<string>();
			if (!string.IsNullOrWhiteSpace(val))
			{
				switch (val)
				{
					case "BRICK":
						result = WallMaterial.Brick;
						break;
					case "MONOLIT":
					case "MONOLIT_BRICK":
						result = WallMaterial.Monolit;
						break;
					case "PANEL":
					case "BLOCK":
						result = WallMaterial.Panel;
						break;
					default:
						result = WallMaterial.Other;
						break;
				}
			}
			return result;
		}

	}

}