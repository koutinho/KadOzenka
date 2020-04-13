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
using KadOzenka.Dal.YandexParser;
using System.Configuration;
using System.Transactions;
using Core.Main.FileStorages;
using KadOzenka.Dal.AvitoParsing.Parsers;
using KadOzenka.Dal.Logger;
using KadOzenka.Dal.Selenium.ScreenShots;

namespace KadOzenka.Dal.Selenium.PriceChecker
{
    public class Avito
    {
        public List<OMCoreObject> AllObjects { get; private set; }

        public Avito()
        {
            AllObjects = OMCoreObject
                .Where(x => x.Market_Code == MarketTypes.Avito && x.LastDateUpdate == null)
                .Select(x => new { x.Url, x.DealType_Code, x.PropertyMarketSegment_Code, x.Price, x.LastDateUpdate }).Execute();
        }

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
                int OCur = 0, OCor = 0, OErr = 0, NDel = 0, NPub = 0, CScr = 0, OCtr = AllObjects.Count;
                List<string> errorLog = new List<string>();
                foreach (OMCoreObject initialObject in AllObjects)
                {
                    DateTime currentTime = DateTime.Now;
                    try
                    {
                        driver.Navigate().GoToUrl(initialObject.Url);
                        AvitoParser.CheckCapcha(driver, initialObject.Url);
                        if (driver.Url != initialObject.Url)
                        {
                            initialObject.LastDateUpdate = currentTime;
                            initialObject.ExclusionStatus_Code = ExclusionStatus.Deleted;
                            initialObject.ProcessType_Code = ProcessStep.Excluded;
                            NDel++;
                            initialObject.Save();
                            errorLog.Add($"[{DateTime.Now}]({initialObject.Id}): {initialObject.Url}\nОбъявление удалено\n");
                        }
                        else if (bool.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["checkAvitoUnpublished"]).ToString()))
                        {
                            initialObject.LastDateUpdate = currentTime;
                            initialObject.ExclusionStatus_Code = ExclusionStatus.Unpublished;
                            initialObject.ProcessType_Code = ProcessStep.Excluded;
                            NPub++;
                            initialObject.Save();
                            errorLog.Add($"[{DateTime.Now}]({initialObject.Id}): {initialObject.Url}\nОбъявление снято с публикации\n");
                        }
                        else
                        {
                            var price = long.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["getAvitoPrice"]).ToString());
                            OMPriceHistory lastPrice = OMPriceHistory.Where(x => x.InitialId == initialObject.Id).SelectAll().OrderByDescending(x => x.ChangingDate).ExecuteFirstOrDefault();
                            if (lastPrice == null || price != lastPrice.PriceValueTo)
                            {
                                using (var ts = new TransactionScope(TransactionScopeOption.RequiresNew))
                                {
                                    new OMPriceHistory { InitialId = initialObject.Id, ChangingDate = currentTime, PriceValueTo = price }.Save();
                                    AvitoParser.SaveScreenShot(driver, new OMScreenshots { InitialId = initialObject.Id, CreationDate = currentTime, Type = "image/png" }, currentTime, MarketTypes.Avito, initialObject.Id, testBoot);
                                    initialObject.Price = price;
                                    initialObject.LastDateUpdate = currentTime;
                                    CScr++;
                                    initialObject.Save();
                                    errorLog.Add($"[{DateTime.Now}]({initialObject.Id}): {initialObject.Url}\nСделан скриншот и обновлена цена: {price}\n");
                                    ts.Complete();
                                }
                            }
                        }
                        OCor++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n{initialObject.Url}\n{ex.Message}");
                        OErr++;
                    }
                    OCur++;
                    ConsoleLog.WriteData("Обновление цен", OCtr, OCur, OCor, OErr, nspErr: NDel, unpub: NPub, screen: CScr);
                }
                errorLog.Add($"========> Обновление цен завершено ({ConsoleLog.GetResultData(OCtr, OCur, OCor, OErr, nspErr: NDel, unpub: NPub, screen: CScr)})\n");
                ConsoleLog.LogError(errorLog.ToArray(), "Присвоение координат объектам из исходного файла");
            }
            ConsoleLog.WriteFotter("Обновление цен завершено");
        }
    }
}
