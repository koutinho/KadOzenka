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
        protected List<OMCoreObject> initialList = OMCoreObject.Where(x => x.Market_Code == MarketTypes.YandexProterty).Select(x => new { x.Url, x.MarketId}).Execute();

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
            //Офисы
            FormMarketObjectsRequests.Add(new FormMarketObjectsRequest
            {
                ObjectsListUrl = "https://realty.yandex.ru/moskva/kupit/kommercheskaya-nedvizhimost/ofis/?hasFurniture=NO",
                DealType = DealType.SaleSuggestion,
                MarketSegment = MarketSegment.Office,
                PropertyTypeCIPJS = PropertyTypesCIPJS.Placements
            });
            //Торговые помещения
            FormMarketObjectsRequests.Add(new FormMarketObjectsRequest
            {
                ObjectsListUrl = "https://realty.yandex.ru/moskva/kupit/kommercheskaya-nedvizhimost/torgovoe-pomeshchenie/?hasPhoto=YES&showSimilar=NO&hasFurniture=NO",
                DealType = DealType.SaleSuggestion,
                MarketSegment = MarketSegment.Trading,
                PropertyTypeCIPJS = PropertyTypesCIPJS.Placements
            });
            //Склады
            FormMarketObjectsRequests.Add(new FormMarketObjectsRequest
            {
                ObjectsListUrl = "https://realty.yandex.ru/moskva/kupit/kommercheskaya-nedvizhimost/sklad/",
                DealType = DealType.SaleSuggestion,
                MarketSegment = MarketSegment.Factory,
                PropertyTypeCIPJS = PropertyTypesCIPJS.Buildings
            });
            //Производственное помещение
            FormMarketObjectsRequests.Add(new FormMarketObjectsRequest
            {
                ObjectsListUrl = "https://realty.yandex.ru/moskva/kupit/kommercheskaya-nedvizhimost/proizvodstvennoe-pomeshchenie/?hasPhoto=YES&showSimilar=NO",
                DealType = DealType.SaleSuggestion,
                MarketSegment = MarketSegment.Factory,
                PropertyTypeCIPJS = PropertyTypesCIPJS.Placements
            });
            //Общепит
            FormMarketObjectsRequests.Add(new FormMarketObjectsRequest
            {
                ObjectsListUrl = "https://realty.yandex.ru/moskva/kupit/kommercheskaya-nedvizhimost/obshchepit/?hasPhoto=YES&showSimilar=NO",
                DealType = DealType.SaleSuggestion,
                MarketSegment = MarketSegment.PublicCatering,
                PropertyTypeCIPJS = PropertyTypesCIPJS.Placements
            });
            //Гостиницы
            FormMarketObjectsRequests.Add(new FormMarketObjectsRequest
            {
                ObjectsListUrl = "https://realty.yandex.ru/moskva/kupit/kommercheskaya-nedvizhimost/gostinica/?hasPhoto=YES&showSimilar=NO&commercialBuildingType=DETACHED_BUILDING",
                DealType = DealType.SaleSuggestion,
                MarketSegment = MarketSegment.Hotel,
                PropertyTypeCIPJS = PropertyTypesCIPJS.Buildings
            });
            //Готовый бизнес
            FormMarketObjectsRequests.Add(new FormMarketObjectsRequest
            {
                ObjectsListUrl = "https://realty.yandex.ru/moskva/kupit/kommercheskaya-nedvizhimost/gotovyj-biznes/?hasPhoto=YES&showSimilar=NO",
                DealType = DealType.SaleSuggestion,
                MarketSegment = MarketSegment.Trading,
                PropertyTypeCIPJS = PropertyTypesCIPJS.Buildings
            });
            //Бокс
            FormMarketObjectsRequests.Add(new FormMarketObjectsRequest
            {
                ObjectsListUrl = "https://realty.yandex.ru/moskva/kupit/garazh/?hasPhoto=YES&showSimilar=NO&garageType=BOX",
                DealType = DealType.SaleSuggestion,
                MarketSegment = MarketSegment.Parking,
                PropertyTypeCIPJS = PropertyTypesCIPJS.Placements
            });
            //Гараж
            FormMarketObjectsRequests.Add(new FormMarketObjectsRequest
            {
                ObjectsListUrl = "https://realty.yandex.ru/moskva/kupit/garazh/?garageType=GARAGE&hasPhoto=YES&showSimilar=NO",
                DealType = DealType.SaleSuggestion,
                MarketSegment = MarketSegment.Parking,
                PropertyTypeCIPJS = PropertyTypesCIPJS.Placements
            });
            //Машиноместо
            FormMarketObjectsRequests.Add(new FormMarketObjectsRequest
            {
                ObjectsListUrl = "https://realty.yandex.ru/moskva/kupit/garazh/?garageType=PARKING_PLACE&hasPhoto=YES&showSimilar=NO",
                DealType = DealType.SaleSuggestion,
                MarketSegment = MarketSegment.CarParking,
                PropertyTypeCIPJS = PropertyTypesCIPJS.Placements
            });
            DoFormMarketObjects();
		}

		public void DoFormMarketObjects()
		{
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
                            try
                            {
                                driver.Navigate().GoToUrl(objectUrl);
                                CheckCapcha((ChromeDriver)driver);
                                ((IJavaScriptExecutor)driver).ExecuteScript(File.ReadAllText(ConfigurationManager.AppSettings["YandexGetInitialStateJsPath"]), objectUrl);
                                var val = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(_ => ((IJavaScriptExecutor)_).ExecuteScript("return window._result;"));
                                var obj = (JObject)JsonConvert.DeserializeObject(val.ToString());
                                OMCoreObject value = FillMarketObject(formMarketObjectsRequest, obj, objectUrl);
                                if (initialList.Where(x => x.MarketId == value.MarketId).Count() == 0) createdObjects.Add(value);
                                KKad++;
                            }
                            catch (Exception ex) 
                            {
                                Console.WriteLine($"\n{ex.Message}");
                                KErr++; 
                            }
                            KCur++;
                            ConsoleLog.WriteData("Обработка объектов", KCtr, KCur, KKad, KErr);
						}
						foreach (var createdObject in createdObjects) createdObject.Save();
						Console.WriteLine($"\nПарсинг для категории {formMarketObjectsRequest.ObjectsListUrl} завершен. Добавлено объектов: {createdObjects.Count}.");
					}
					catch (Exception e) { Console.WriteLine($"\nПроизошла ошибка во время парсинга категории {formMarketObjectsRequest.ObjectsListUrl}:{e.Message}"); }
				}
			}
		}

		public void CheckCapcha(ChromeDriver driver)
		{
			var isCapcha = driver.ExecuteScript(ConfigurationManager.AppSettings["isYandexCapchaScreen"]).ToString();
			if (bool.Parse(isCapcha))
			{
				Console.WriteLine($"\nОбнаружена капча! Обработайте запрос со страницы браузера и нажмите любую клавишу для продолжения парсинга...");
				Console.ReadKey();
				Console.WriteLine($"Парсинг возобновлен...");
			}
		}

		private List<string> GetObjectsUrlList(IJavaScriptExecutor javaScriptExecutor) => 
            ((ReadOnlyCollection<object>)javaScriptExecutor.ExecuteScript(ConfigurationManager.AppSettings["getYandexUrlList"])).Select(x => (string)x).ToList();

		private OMCoreObject FillMarketObject(FormMarketObjectsRequest formMarketObjectsRequest, JObject obj, string objectUrl)
		{
            var marketObject = new OMCoreObject();
            try
            {
                marketObject.Market_Code = MarketTypes.YandexProterty;
                marketObject.ProcessType_Code = ProcessStep.DoNotProcessed;
                marketObject.DealType_Code = formMarketObjectsRequest.DealType;
                marketObject.PropertyMarketSegment_Code = formMarketObjectsRequest.MarketSegment;
                marketObject.PropertyTypesCIPJS_Code = formMarketObjectsRequest.PropertyTypeCIPJS;
                marketObject.MarketId = obj.SelectToken("cards.offers.offerId").Value<long>();
                marketObject.Url = objectUrl;
                marketObject.Price = obj.SelectToken("cards.offers.price").HasValues ? obj.SelectToken("cards.offers.price.value")?.Value<long>() : (long?)null;
                marketObject.ParserTime = DateTime.Now;
                marketObject.Region = null;
                marketObject.City = "Москва";
                marketObject.Address = obj.SelectToken("cards.offers.location").HasValues ? obj.SelectToken("cards.offers.location.address")?.Value<string>() : null;
                try
                {
                    if (obj.SelectToken("cards.offers.location").HasValues)
                    {
                        var metroList = new List<string>();
                        for (var i = 0; i < obj.SelectToken("cards.offers.location.metroList").Count(); i++) metroList.Add(obj.SelectToken($"cards.offers.location.metroList[{i}].name")?.Value<string>());
                        marketObject.Metro = string.Join(',', metroList.Where(x => !string.IsNullOrWhiteSpace(x)));
                    }
                }
                catch (Exception) { }
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
            }
            catch (Exception e) 
            { 
                Console.WriteLine($"\n{e.StackTrace}");
                Console.ReadLine();
            }
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