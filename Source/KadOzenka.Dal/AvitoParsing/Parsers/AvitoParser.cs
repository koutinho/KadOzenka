﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
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

namespace KadOzenka.Dal.AvitoParsing.Parsers
{
    public abstract class AvitoParser
    {
        protected List<OMCoreObject> ExistedAvitoObjects = OMCoreObject
            .Where(x => x.Market_Code == MarketTypes.Avito).Select(x => new { x.Url, x.MarketId }).Execute();

        protected string Url;
        //TODO: список будет заполнен при уточнении сооотношений объектов с авито и нашей бд
        public List<ObjectCategoryCorrelation> ObjectTypeList { get; protected set; }

        public void HandleObjects()
        {
            HandleObjects(DealType.SaleSuggestion);
            HandleObjects(DealType.RentSuggestion);
        }

        protected void HandleObjects(DealType dealType)
        {
            ChromeOptions options = new ChromeOptions();
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            using (IWebDriver driver = new ChromeDriver(service, options))
            {
                driver.Manage().Window.Maximize();

                foreach (var objectType in ObjectTypeList)
                {
                    try
                    {
                        Console.WriteLine($"Получение URL адресов объектов для категории '{objectType}'...");
                        driver.Navigate().GoToUrl(Url);
                        ChooseDealType(dealType, driver);
                        SelectObjectTypeCategory(driver, objectType.AvitoName);
                        ApplyFilter(driver);

                        var objectsUrls = GetObjectsUrlList((IJavaScriptExecutor)driver);
                        //var pagerButton = GetPagerNextButton(driver);
                        //while (pagerButton != null)
                        //{
                        //    pagerButton.Click();
                        //    WaitUntilPageLoad(driver);
                        //    objectsUrls.AddRange(GetObjectsUrlList((IJavaScriptExecutor)driver));
                        //    pagerButton = GetPagerNextButton(driver);
                        //}

                        var uniqueObjectsUrls = objectsUrls.Distinct().ToList();
                        Console.WriteLine($"Найдено {uniqueObjectsUrls.Count} объектов для категории '{objectType}'");

                        int totalCount = uniqueObjectsUrls.Count, currentCount = 0, correctCount = 0, errorCount = 0;
                        var createdObjects = new List<OMCoreObject>();
                        foreach (var objectUrl in uniqueObjectsUrls)
                        {
                            try
                            {
                                driver.Navigate().GoToUrl(objectUrl);
                                ((IJavaScriptExecutor)driver).ExecuteScript(
                                    File.ReadAllText(ConfigurationManager.AppSettings["AvitoGetObjectInfo"]),
                                    objectUrl);
                                var val = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(_ =>
                                    ((IJavaScriptExecutor)_).ExecuteScript("return window._result;"));
                                var deserializedObject = (JObject)JsonConvert.DeserializeObject(val.ToString());
                                var value = FillMarketObject(dealType, objectType, deserializedObject, objectUrl);
                                if (ExistedAvitoObjects.Count(x => x.MarketId == value.MarketId) == 0)
                                    createdObjects.Add(value);
                                correctCount++;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"\nНе удалось обработать объект по ссылке '{objectUrl}':{ex.Message}, {ex.StackTrace}");
                                errorCount++;
                            }

                            currentCount++;
                            ConsoleLog.WriteData("Обработка объектов", totalCount, currentCount, correctCount,
                                errorCount);
                        }

                        foreach (var createdObject in createdObjects) createdObject.Save();
                        Console.WriteLine(
                            $"\nПарсинг для категории '{objectType}' типа сделки '{dealType.GetEnumDescription()}' завершен. Добавлено объектов: {createdObjects.Count}.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nПроизошла ошибка во время парсинга категории '{objectType}' типа сделки '{dealType.GetEnumDescription()}':{ex.Message}, {ex.StackTrace}");
                    }
                }
            }
        }

        private static void ChooseDealType(DealType dealType, IWebDriver driver)
        {
            IWebElement button;
            if (dealType == DealType.SaleSuggestion)
            {
                button = ((ChromeDriver) driver).ExecuteScript(
                    ConfigurationManager.AppSettings["getAvitoBuyButton"]) as IWebElement;
            }
            else if (dealType == DealType.RentSuggestion)
            {
                button = ((ChromeDriver) driver).ExecuteScript(
                    ConfigurationManager.AppSettings["getAvitoRentButton"]) as IWebElement;
            }
            else
            {
                throw new Exception($"Передан неподдерживаемый тип сделки: {dealType.GetEnumDescription()}");
            }

            button.Click();
            WaitUntilPageLoad(driver);
        }

        private OMCoreObject FillMarketObject(DealType dealType, ObjectCategoryCorrelation objectType, JObject jObject, string objectUrl)
        {
            var marketObject = new OMCoreObject();
            try
            {
                marketObject.Market_Code = MarketTypes.Avito;
                marketObject.ProcessType_Code = ProcessStep.DoNotProcessed;
                marketObject.DealType_Code = dealType;
                marketObject.PropertyMarketSegment_Code = objectType.MarketSegment;
                marketObject.PropertyTypesCIPJS_Code = objectType.PropertyType;

                marketObject.Url = objectUrl;
                marketObject.ParserTime = DateTime.Now;
                marketObject.Region = null;
                marketObject.City = "Москва";
                marketObject.MarketId = jObject.SelectToken("marketId").Value<long>();
                marketObject.Description = jObject.SelectToken("description")?.Value<string>();
                marketObject.Lat = !jObject.SelectToken("lat").IsNullOrEmpty()
                    ? jObject.SelectToken("lat").Value<decimal>()
                    : (decimal?)null;
                marketObject.Lng = !jObject.SelectToken("lon").IsNullOrEmpty()
                    ? jObject.SelectToken("lon").Value<decimal>()
                    : (decimal?)null;
                marketObject.Address = !jObject.SelectToken("address").IsNullOrEmpty()
                    ? jObject.SelectToken("address").Value<string>()
                    : null;
                marketObject.Price = GetPrice(jObject, dealType); //obj.SelectToken("cards.offers.price").HasValues ? obj.SelectToken("cards.offers.price.value")?.Value<long>() : (long?)null;

                if (!jObject.SelectToken("metroList").IsNullOrEmpty())
                {
                    var metroList = new List<string>();
                    for (var i = 0; i < jObject.SelectToken("metroList").Count(); i++)
                    {
                        metroList.Add(jObject.SelectToken($"metroList[{i}]")?.Value<string>());
                    }
                    marketObject.Metro = string.Join(',', metroList.Where(x => !string.IsNullOrWhiteSpace(x)));
                }
                if (!jObject.SelectToken("imageUrls").IsNullOrEmpty())
                {
                    var imageUrls = new List<string>();
                    for (var i = 0; i < jObject.SelectToken("imageUrls").Count(); i++)
                    {
                        imageUrls.Add(jObject.SelectToken($"imageUrls[{i}]")?.Value<string>());
                    }
                    var uri = new Uri(objectUrl);
                    marketObject.Images = string.Join(',', imageUrls.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => uri.Scheme + ":" + x));
                }


                marketObject.FloorNumber = !jObject.SelectToken("floorNumber").IsNullOrEmpty()
                    ? jObject.SelectToken("floorNumber").Value<long>()
                    : (long?) null;
                marketObject.FloorsCount = !jObject.SelectToken("floorCount").IsNullOrEmpty()
                    ? jObject.SelectToken("floorCount").Value<long>()
                    : (long?)null;

                //marketObject.BuildingYear = obj.SelectToken("cards.offers.building.builtYear")?.Value<long>();
                FillObjectLandInfo(marketObject, jObject);
                FillObjectWallMaterialInfo(marketObject, jObject);
                FillObjectClassType(marketObject, jObject);

            }
            catch (Exception e)
            {
                Console.WriteLine($"\n{e.StackTrace}");
                Console.ReadLine();
            }
            return marketObject;
        }

        private void FillObjectClassType(OMCoreObject marketObject, JObject jObject)
        {
            var buildingClass = !jObject.SelectToken("buildingClass").IsNullOrEmpty()
                    ? jObject.SelectToken("buildingClass").Value<string>()
                    : null;
            if (!string.IsNullOrWhiteSpace(buildingClass))
            {
                QualityClass? result = null;
                switch (buildingClass.ToUpper().Trim())
                {
                    case "A":
                        result = QualityClass.A;
                        break;
                    case "B":
                        result = QualityClass.B;
                        break;
                    case "C":
                        result = QualityClass.B;
                        break;
                }

                marketObject.QualityClass_Code = result.GetValueOrDefault();
            }
        }

        protected virtual void FillObjectWallMaterialInfo(OMCoreObject marketObject, JObject jObject)
        {
            var val = !jObject.SelectToken("wallMaterial").IsNullOrEmpty()
                ? jObject.SelectToken("wallMaterial").Value<string>()
                : !jObject.SelectToken("houseType").IsNullOrEmpty()
                    ? jObject.SelectToken("houseType").Value<string>()
                    : null;
            if (!string.IsNullOrWhiteSpace(val))
            {
                WallMaterial? result = null;
                switch (val)
                {
                    case "Кирпич":
                        result = WallMaterial.Brick;
                        break;
                    case "Монолитный":
                        result = WallMaterial.Monolit;
                        break;
                    case "Панельный":
                    case "Блочный":
                        result = WallMaterial.Panel;
                        break;
                    default:
                        result = WallMaterial.Other;
                        break;
                }

                marketObject.WallMaterial_Code = result.Value;
            }
        }

        protected virtual void FillObjectLandInfo(OMCoreObject marketObject, JObject jObject)
        {
            var areaString = !jObject.SelectToken("area").IsNullOrEmpty()
                ? jObject.SelectToken("area").Value<string>()
                : !jObject.SelectToken("houseArea").IsNullOrEmpty()
                    ? jObject.SelectToken("houseArea").Value<string>()
                    : null;

            if (!string.IsNullOrEmpty(areaString))
            {
                var areaStringParts = areaString.Split(' ');
                if (!decimal.TryParse(areaStringParts[0], out var area))
                {
                    area = decimal.Parse(areaStringParts[1]);
                }
                var unit = areaStringParts.Last().ToLower().Trim();

                var coef = 1.0m;
                switch (unit)
                {
                    case "м²":
                        coef = 1;
                        break;
                    case "сот.":
                        coef = 100;
                        break;
                    case "га":
                        coef = 10000;
                        break;
                }

                marketObject.Area = area * coef;
            }

            var areaLandString = !jObject.SelectToken("site_area").IsNullOrEmpty()
                ? jObject.SelectToken("site_area").Value<string>()
                : null;
            if (!string.IsNullOrEmpty(areaLandString))
            {
                var areaLandStringParts = areaLandString.Split(' ');
                if (!decimal.TryParse(areaLandStringParts[0], out var areaLand))
                {
                    areaLand = decimal.Parse(areaLandStringParts[1]);
                }
                var unitLand = areaLandStringParts.Last().ToLower().Trim();

                var landCoef = 1.0m;
                switch (unitLand)
                {
                    case "м²":
                        landCoef = 0.01m;
                        break;
                    case "сот.":
                        landCoef = 1;
                        break;
                    case "га":
                        landCoef = 100;
                        break;
                }

                marketObject.AreaLand = areaLand * landCoef;
            }
        }

        private decimal? GetPrice(JObject jObject, DealType dealType)
        {
            decimal? price = null;
            if (dealType == DealType.RentSuggestion)
            {
                var priceValue = !jObject.SelectToken("price").IsNullOrEmpty()
                    ? jObject.SelectToken("price").Value<decimal>()
                    : (decimal?) null;
                var priceString = !jObject.SelectToken("priceFormatted").IsNullOrEmpty()
                    ? jObject.SelectToken("priceFormatted").Value<string>().Trim()
                    : null;
                if (priceString != null && priceString.EndsWith("в месяц"))
                {
                    price = priceValue;
                }
                else if (priceString != null && priceString.EndsWith("в год"))
                {
                    price = priceValue / 12;
                }
                else
                {
                    price = priceValue;
                }
            }
            else if (dealType == DealType.SaleSuggestion)
            {
                price = !jObject.SelectToken("price").IsNullOrEmpty()
                    ? jObject.SelectToken("price").Value<decimal>()
                    : (decimal?)null;
            }
            else
            {
                throw new Exception($"Передан неподдерживаемый тип сделки: {dealType.GetEnumDescription()}");
            }
            return price;
        }

        protected virtual void SelectObjectTypeCategory(IWebDriver driver, string objectTypeName)
        {
            if (((ChromeDriver)driver).ExecuteScript(ConfigurationManager.AppSettings["getAvitoObjectTypeListItems"]) is IReadOnlyCollection<IWebElement> items)
            {
                foreach (var item in items)
                {
                    var checkedElement = item.FindElements(By.CssSelector("label[class*='checkbox-checked']")).FirstOrDefault();
                    checkedElement?.Click();
                }
                var currentItem = items.FirstOrDefault(x => x.Text.Contains(objectTypeName));
                currentItem?.FindElement(By.CssSelector("label[class*='checkbox']")).Click();
            }
        }

        protected List<string> GetObjectsUrlList(IJavaScriptExecutor javaScriptExecutor) =>
            ((ReadOnlyCollection<object>)javaScriptExecutor.ExecuteScript(ConfigurationManager.AppSettings["getAvitoUrlList"])).Select(x => (string)x).ToList();


        private static void WaitUntilPageLoad(IWebDriver driver)
        {
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30.00));
            wait.Until(driver1 =>
                ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        private static IWebElement GetPagerNextButton(IWebDriver driver)
        {
            return ((ChromeDriver)driver).ExecuteScript(ConfigurationManager.AppSettings["getAvitoPaginationNextButton"]) as IWebElement;
        }

        private static void ApplyFilter(IWebDriver driver)
        {
            var applyFilterButton =
                (((ChromeDriver)driver).ExecuteScript(ConfigurationManager.AppSettings["getAvitoApplyFilterButton"]) as
                    IWebElement);
            applyFilterButton.Click();
            WaitUntilPageLoad(driver);
        }
    }
}