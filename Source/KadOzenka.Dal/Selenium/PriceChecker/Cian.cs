using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Configuration;
using System.Collections.Generic;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ObjectModel.Market;
using ObjectModel.Directory;
using KadOzenka.Dal.Logger;
using KadOzenka.Dal.Selenium.ScreenShots;
using Core.Main.FileStorages;

namespace KadOzenka.Dal.Selenium.PriceChecker
{
    public class Cian
    {

        public List<OMPriceHistory> resultList = new List<OMPriceHistory>();
        public List<OMCoreObject> AllObjects = OMCoreObject
            .Where(x => 
                x.Market_Code == MarketTypes.Cian && 
                x.LastDateUpdate == null && (
                    x.PropertyMarketSegment_Code == MarketSegment.Parking || 
                    x.PropertyMarketSegment_Code == MarketSegment.Trading ||
                    x.PropertyMarketSegment_Code == MarketSegment.Office ||
                    x.PropertyMarketSegment_Code == MarketSegment.Factory
                )
            )
            .Select(x => new { x.Url, x.DealType_Code, x.Price, x.LastDateUpdate }).Execute();
        public List<OMScreenshots> AllScreens = OMScreenshots.Where(x => true).SelectAll().Execute();

        public void Test()
        {
            ChromeOptions options = new ChromeOptions();
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            using (IWebDriver driver = new ChromeDriver(service, options))
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://www.cian.ru/sale/flat/221625776/");
                File.WriteAllBytes(@"C:\Users\silanov\Desktop\1.png", new FullScreen().TakeScreenShot((ChromeDriver)driver));
            }
        }

        public void RefreshAllData()
        {
            ChromeOptions options = new ChromeOptions();
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            using (ChromeDriver driver = new ChromeDriver(service, options)) 
            {
                driver.Manage().Window.Maximize();
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                int OCur = 0, OCor = 0, OErr = 0, NErr = 0, NPub = 0, OCtr = AllObjects.Count;
                foreach (OMCoreObject initialObject in AllObjects)
                {
                    DateTime currentTime = DateTime.Now;
                    try
                    {
                        driver.Navigate().GoToUrl(initialObject.Url);
                        if (bool.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["checkCIANUnpublished"]).ToString()))
                        {
                            //Тут обрабатываются данные снятых с публикации объектов
                            initialObject.LastDateUpdate = currentTime;
                            initialObject.ExclusionStatus_Code = ExclusionStatus.Unpublished;
                            initialObject.ProcessType_Code = ProcessStep.Excluded;
                            NPub++;
                            initialObject.Save();
                        }
                        else if (!bool.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["checkCIANError"]).ToString()) && 
                                 !bool.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["checkCIAN505Page"]).ToString()))
                        {
                            //Тут обрабатываются данные неудалённых и не снятых с публикации объектов
                            if (bool.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["isCorridor"]).ToString()))
                            {
                                //Тут обрабатываются данные объектов с ценником-коридором
                                if (executor.ExecuteScript(ConfigurationManager.AppSettings["getCIANCurrency"]).ToString() == "RUB") 
                                {
                                    long lowPrice = long.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["getCIANLowPrice"]).ToString()), 
                                         highPrice = long.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["getCIANHighPrice"]).ToString());
                                    OMPriceHistory lastPrice = OMPriceHistory.Where(x => x.InitialId == initialObject.Id).SelectAll().OrderByDescending(x => x.ChangingDate).ExecuteFirstOrDefault();
                                    if (lastPrice == null || highPrice != lastPrice.PriceValueTo)
                                    {
                                        new OMPriceHistory { InitialId = initialObject.Id, ChangingDate = currentTime, PriceValueFrom = lowPrice, PriceValueTo = highPrice }.Save();
                                        SaveScreenShot(driver, new OMScreenshots { InitialId = initialObject.Id, CreationDate = currentTime, Type = "image/png" }, currentTime);
                                        initialObject.Price = highPrice;
                                        initialObject.LastDateUpdate = currentTime;
                                        initialObject.ExclusionStatus_Code = ExclusionStatus.IncorrectPrice;
                                        initialObject.ProcessType_Code = ProcessStep.Excluded;
                                        initialObject.Save();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(initialObject.Url);
                                    Console.WriteLine(executor.ExecuteScript(ConfigurationManager.AppSettings["getCIANCurrency"]).ToString());
                                    Console.ReadKey();
                                }
                            }
                            else
                            {
                                if (executor.ExecuteScript(ConfigurationManager.AppSettings["getCIANCurrency"]).ToString() == "RUB")
                                {
                                    long price = long.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["getCIANPrice"]).ToString());
                                    OMPriceHistory lastPrice = OMPriceHistory.Where(x => x.InitialId == initialObject.Id).SelectAll().OrderByDescending(x => x.ChangingDate).ExecuteFirstOrDefault();
                                    if (lastPrice == null || price != lastPrice.PriceValueTo)
                                    {
                                        new OMPriceHistory { InitialId = initialObject.Id, ChangingDate = currentTime, PriceValueTo = price }.Save();
                                        SaveScreenShot(driver, new OMScreenshots { InitialId = initialObject.Id, CreationDate = currentTime, Type = "image/png" }, currentTime);
                                        initialObject.Price = price;
                                        initialObject.LastDateUpdate = currentTime;
                                        initialObject.Save();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(initialObject.Url);
                                    Console.WriteLine(executor.ExecuteScript(ConfigurationManager.AppSettings["getCIANCurrency"]).ToString());
                                    Console.ReadKey();
                                }
                            }
                        }
                        else
                        {
                            //Тут обрабатываются данные удалённых объектов
                            initialObject.LastDateUpdate = currentTime;
                            initialObject.ExclusionStatus_Code = ExclusionStatus.Deleted;
                            initialObject.ProcessType_Code = ProcessStep.Excluded;
                            NErr++;
                            initialObject.Save();
                        }
                        OCor++;
                    }
                    catch (Exception) {  OErr++;  }
                    OCur++;
                    ConsoleLog.WriteData("Обновление цен", OCtr, OCur, OCor, OErr, nspErr: NErr, unpub: NPub);
                }
            }
            ConsoleLog.WriteFotter("Обновление цен завершено");
        }

        private void SaveScreenShot(ChromeDriver driver, OMScreenshots screenshot, DateTime screenShotData)
        {
            try
            {
				var screenShot = new FullScreen().TakeScreenShot(driver);
				if (screenShot != null) FileStorageManager.Save(new MemoryStream(screenShot), ConfigurationManager.AppSettings["screenShotFolder"], screenShotData, screenshot.Save().ToString());
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
        }

    }

}