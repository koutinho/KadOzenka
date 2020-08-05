using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using ObjectModel.Market;
using ObjectModel.Directory;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using KadOzenka.Dal.Logger;
using KadOzenka.Dal.YandexParsing;

namespace KadOzenka.Dal.Selenium.PriceChecker
{

    public class Yandex
    {

        public List<OMScreenshots> AllScreens = OMScreenshots.Where(x => true).SelectAll().Execute();
        public List<OMCoreObject> AllObjects = OMCoreObject
            .Where(x => x.Market_Code == MarketTypes.YandexProterty && x.LastDateUpdate == null)
            .Select(x => new { x.Url, x.DealType_Code, x.PropertyMarketSegment_Code, x.Price, x.LastDateUpdate }).Execute();

        public void RefreshAllData(int maxCounter = 0, bool testBoot = false)
        {
            if (maxCounter != 0) AllObjects = AllObjects.Take(maxCounter).ToList();
            ChromeOptions options = new ChromeOptions();
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            using (ChromeDriver driver = new ChromeDriver(service, options))
            {
                driver.Manage().Window.Maximize();
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                int OCur = 0, OCor = 0, OErr = 0, NPub = 0, CScr = 0, OCtr = AllObjects.Count;
                List<string> errorLog = new List<string>();
                foreach (OMCoreObject initialObject in AllObjects)
                {
                    DateTime currentTime = DateTime.Now;
                    try
                    {
                        driver.Navigate().GoToUrl(initialObject.Url);
                        YandexParserUtils.CheckCapcha(driver);
                        if (bool.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["checkYandexUnpublished"]).ToString()))
                        {
                            //Тут обрабатываются данные снятых с публикации объектов
                            initialObject.LastDateUpdate = currentTime;
                            initialObject.ExclusionStatus_Code = ExclusionStatus.Unpublished;
                            initialObject.ProcessType_Code = ProcessStep.Excluded;
                            NPub++;
                            initialObject.Save();
                            errorLog.Add($"[{DateTime.Now}]({initialObject.Id}): {initialObject.Url}\nОбъявление снято с публикации\n");
                        }
                        else
                        {
                            //Тут обрабатываются данные не снятых с публикации объектов с обычным ценником
                            if (bool.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["getYandexCurrency"]).ToString()))
                            {
                                long price = long.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["getYandexPrice"]).ToString());
                                //Подбор типа ОН по этажам (если 1 показатель - здание, иначе - помещение)
                                if (bool.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["getYandexContainsFloorsInfo"]).ToString()) && 
                                    initialObject.PropertyMarketSegment_Code != MarketSegment.PublicCatering &&
                                    initialObject.PropertyMarketSegment_Code != MarketSegment.Hotel)
                                {
                                   if(executor.ExecuteScript(ConfigurationManager.AppSettings["getYandexFloors"]).ToString().Split(",").Select(x => Int32.Parse(x)).Count() == 1 || 
                                      bool.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["getYandexBuildingInfo"]).ToString())) 
                                        initialObject.PropertyTypesCIPJS_Code = PropertyTypesCIPJS.Buildings;
                                   else initialObject.PropertyTypesCIPJS_Code = PropertyTypesCIPJS.Placements;
                                }
                                //Получение актуальной цены
                                OMPriceHistory lastPrice = OMPriceHistory.Where(x => x.InitialId == initialObject.Id).SelectAll().OrderByDescending(x => x.ChangingDate).ExecuteFirstOrDefault();
                                //Обновление информации и снятие скриншотов
                                if (lastPrice == null || price != lastPrice.PriceValueTo)
                                {
                                    new OMPriceHistory { InitialId = initialObject.Id, ChangingDate = currentTime, PriceValueTo = price }.Save();
                                    new Cian().SaveScreenShot(driver, new OMScreenshots { InitialId = initialObject.Id, CreationDate = currentTime, Type = "image/png" }, currentTime, MarketTypes.YandexProterty, initialObject.Id, testBoot);
                                    initialObject.Price = price;
                                    initialObject.LastDateUpdate = currentTime;
                                    CScr++;
                                    initialObject.Save();
                                    errorLog.Add($"[{DateTime.Now}]({initialObject.Id}): {initialObject.Url}\nСделан скриншот и обновлена цена: {price}\n");
                                }
                            }
                            else errorLog.Add($"[{DateTime.Now}]({initialObject.Id}): {initialObject.Url}\nЦена указана не в рублях! (Обычный ценник)\n");
                        }
                        OCor++;
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine($"\n{initialObject.Url}\n{ex.Message}");
                        OErr++; 
                    }
                    OCur++;
                    ConsoleLog.WriteData("Обновление цен", OCtr, OCur, OCor, OErr, unpub: NPub, screen: CScr);
                }
                errorLog.Add($"========> Обновление цен завершено ({ConsoleLog.GetResultData(OCtr, OCur, OCor, OErr, unpub: NPub, screen: CScr)})\n");
                ConsoleLog.LogError(errorLog.ToArray(), "Присвоение координат объектам из исходного файла");
            }
            ConsoleLog.WriteFotter("Обновление цен завершено");
        }

    }

}
