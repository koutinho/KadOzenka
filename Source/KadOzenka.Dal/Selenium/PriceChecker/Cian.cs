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
            .Where(x => x.Market_Code == MarketTypes.Cian && x.LastDateUpdate == null && x.Category == "Коммерческая недвижимость")
            .Select(x => new { x.Url, x.DealType_Code, x.Price, x.LastDateUpdate }).Execute();
        public List<OMScreenshots> AllScreens = OMScreenshots.Where(x => true).SelectAll().Execute();

        public void Test(int count)
        {
            List<OMCoreObject> objects = OMCoreObject
                .Where(x => x.Market_Code == MarketTypes.Cian && x.LastDateUpdate != null)
                .Select(x => new { x.Url })
                .SetPackageSize(count).Execute();
            ChromeOptions options = new ChromeOptions();
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            using (IWebDriver driver = new ChromeDriver(service, options))
            {
                int cntr = 1;
                driver.Manage().Window.Maximize();
                objects.ForEach(x => 
                {
                    driver.Navigate().GoToUrl(x.Url);
                    File.WriteAllBytes($@"C:\Users\silanov\Desktop\Screens\{cntr}.png", new FullScreen().TakeScreenShot((ChromeDriver)driver));
                    cntr++;
                });
            }
        }

        public void RefreshAllData(int maxCounter = 0, bool testBoot = false)
        {
            if (maxCounter != 0) AllObjects = AllObjects.Take(maxCounter).ToList();
            ChromeOptions options = new ChromeOptions();
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            Console.WriteLine(AllObjects.Count);
            using (ChromeDriver driver = new ChromeDriver(service, options)) 
            {
                driver.Manage().Window.Maximize();
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                int OCur = 0, OCor = 0, OErr = 0, NErr = 0, NPub = 0, CScr = 0, OCtr = AllObjects.Count;
                List<string> errorLog = new List<string>();
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
                            errorLog.Add($"[{DateTime.Now}]({initialObject.Id}): {initialObject.Url}\nОбъявление снято с публикации\n");
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
                                        SaveScreenShot(driver, new OMScreenshots { InitialId = initialObject.Id, CreationDate = currentTime, Type = "image/png" }, currentTime, initialObject.Id, testBoot);
                                        initialObject.Price = highPrice;
                                        initialObject.LastDateUpdate = currentTime;
                                        initialObject.ExclusionStatus_Code = ExclusionStatus.IncorrectPrice;
                                        initialObject.ProcessType_Code = ProcessStep.Excluded;
                                        CScr++;
                                        initialObject.Save();
                                        errorLog.Add($"[{DateTime.Now}]({initialObject.Id}): {initialObject.Url}\nСделан скриншот и обновлена цена: {lowPrice} - {highPrice}\n");
                                    }
                                }
                                else errorLog.Add($"[{DateTime.Now}]({initialObject.Id}): {initialObject.Url}\nЦена указана не в рублях! (Ценник-коридор)\n");
                            }
                            else
                            {
                                //Тут обрабатываются данные объектов с обычным ценником
                                if (executor.ExecuteScript(ConfigurationManager.AppSettings["getCIANCurrency"]).ToString() == "RUB")
                                {
                                    long price = long.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["getCIANPrice"]).ToString());
                                    OMPriceHistory lastPrice = OMPriceHistory.Where(x => x.InitialId == initialObject.Id).SelectAll().OrderByDescending(x => x.ChangingDate).ExecuteFirstOrDefault();
                                    if (lastPrice == null || price != lastPrice.PriceValueTo)
                                    {
                                        new OMPriceHistory { InitialId = initialObject.Id, ChangingDate = currentTime, PriceValueTo = price }.Save();
                                        SaveScreenShot(driver, new OMScreenshots { InitialId = initialObject.Id, CreationDate = currentTime, Type = "image/png" }, currentTime, initialObject.Id, testBoot);
                                        initialObject.Price = price;
                                        initialObject.LastDateUpdate = currentTime;
                                        CScr++;
                                        initialObject.Save();
                                        errorLog.Add($"[{DateTime.Now}]({initialObject.Id}): {initialObject.Url}\nСделан скриншот и обновлена цена: {price}\n");
                                    }
                                }
                                else errorLog.Add($"[{DateTime.Now}]({initialObject.Id}): {initialObject.Url}\nЦена указана не в рублях! (Обычный ценник)\n");
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
                            errorLog.Add($"[{DateTime.Now}]({initialObject.Id}): {initialObject.Url}\nОбъявление удалено\n");
                        }
                        OCor++;
                    }
                    catch (Exception) {  OErr++;  }
                    OCur++;
                    ConsoleLog.WriteData("Обновление цен", OCtr, OCur, OCor, OErr, nspErr: NErr, unpub: NPub, screen: CScr);
                }
                errorLog.Add($"========> Обновление цен завершено ({ConsoleLog.GetResultData(OCtr, OCur, OCor, OErr, nspErr: NErr, unpub: NPub, screen: CScr)})\n");
                ConsoleLog.LogError(errorLog.ToArray(), "Присвоение координат объектам из исходного файла");
            }
            ConsoleLog.WriteFotter("Обновление цен завершено");
        }

        private void SaveScreenShot(ChromeDriver driver, OMScreenshots screenshot, DateTime screenShotData, long objectId = 0, bool testBoot = false)
        {
            try
            {
				var screenShot = new FullScreen().TakeScreenShot(driver);
				if (screenShot != null) FileStorageManager.Save(new MemoryStream(screenShot), ConfigurationManager.AppSettings["screenShotFolder"], screenShotData, screenshot.Save().ToString());
                if (testBoot) File.WriteAllBytes($@"C:\Users\silanov\Desktop\Screens\{objectId}.png", screenShot);
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
        }

    }

}