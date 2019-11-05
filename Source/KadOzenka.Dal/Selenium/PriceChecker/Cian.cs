using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using OpenQA.Selenium;
using ObjectModel.Market;
using ObjectModel.Directory;
using KadOzenka.Dal.Logger;
using Core.Main.FileStorages;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using KadOzenka.Dal.Selenium.ScreenShots;

namespace KadOzenka.Dal.Selenium.PriceChecker
{
    public class Cian
    {

        public List<OMPriceHistory> resultList = new List<OMPriceHistory>();
        public List<OMCoreObject> AllObjects = 
            OMCoreObject.Where(x => x.Market_Code == MarketTypes.Cian && x.LastDateUpdate == null).Select(x => new { x.Url, x.DealType_Code, x.Price, x.LastDateUpdate }).Execute();
        public List<OMScreenshots> AllScreens = OMScreenshots.Where(x => true).SelectAll().Execute();
        private DateTime currentDate = DateTime.Now;
        private DateTime lockDateTime = new DateTime(1970, 1, 1, 0, 0, 1);

        public void TakePrice() 
        {
            using (IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Manage().Window.Maximize();
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                int OCur = 0, OCor = 0, OErr = 0, NErr = 0, OCtr = AllObjects.Count;
                foreach (OMCoreObject initialObject in AllObjects)
                {
                    try
                    {
                        driver.Navigate().GoToUrl(initialObject.Url);
                        if (!bool.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["checkCIANError"]).ToString()) && !bool.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["checkCIAN505Page"]).ToString()))
                        {
                            executor.ExecuteScript(ConfigurationManager.AppSettings["removeCIANBanerScript"]);
                            RefreshObjectInfo(initialObject, GetData(executor, initialObject.DealType_Code, initialObject.Id), executor, wait);
                            OCor++;
                        }
                        else
                        {
                            LockObject(initialObject);
                            NErr++;
                        }
                    }
                    catch (Exception ex) 
                    { 
                        OErr++;
                        Console.WriteLine($"{ex.Message}\n");
                        //Console.ReadLine();
                    }
                    OCur++;
                    ConsoleLog.WriteData("Обновление цен", OCtr, OCur, OCor, OErr, nspErr:NErr);
                }
            }
            ConsoleLog.WriteFotter("Обновление цен завершено");
        }

        private void RefreshObjectInfo(OMCoreObject initialObject, List<OMPriceHistory> history, IJavaScriptExecutor executor, WebDriverWait wait)
        {
            OMPriceHistory currentPrice = history.First();
            if (initialObject.LastDateUpdate == null)
            {
                history.ForEach(x => x.Save());
                if (initialObject.Price != currentPrice.PriceValueTo) initialObject.Price = currentPrice.PriceValueTo;
                SaveScreenShot(executor, wait, new OMScreenshots { InitialId = initialObject.Id, CreationDate = currentDate, Type = "image/png" });
            }
            else
            {
                List<OMPriceHistory> OH = OMPriceHistory.Where(x => x.InitialId == initialObject.Id).SelectAll().OrderByDescending(x => x.ChangingDate).Execute();
                if(initialObject.Price != currentPrice.PriceValueTo)
                {
                    initialObject.Price = currentPrice.PriceValueTo;
                    SaveScreenShot(executor, wait, new OMScreenshots { InitialId = initialObject.Id, CreationDate = currentDate, Type = "image/png" });
                }
                else history = history.Skip(1).ToList();
                history.ForEach(x => { if (OH.Count(y => y.ChangingDate == x.ChangingDate && y.PriceValueTo == x.PriceValueTo) == 0) x.Save(); });
            }
            initialObject.LastDateUpdate = currentDate;
            initialObject.Save();
        }

        private void LockObject(OMCoreObject initialObject)
        {
            initialObject.LastDateUpdate = lockDateTime;
            initialObject.Save();
        }

        private void SaveScreenShot(IJavaScriptExecutor executor, WebDriverWait wait, OMScreenshots screenshot)
        {
            try
            {
                byte[] screenShot = new FullScreen().TakeScreenShot(executor, wait);
                if (screenShot != null) 
                    FileStorageManager.Save(
                        new MemoryStream(screenShot), 
                        ConfigurationManager.AppSettings["screenShotFolder"], 
                        currentDate, 
                        screenshot.Save().ToString()
                    );
            }
            catch(Exception){}
        }

        private List<OMPriceHistory> GetData(IJavaScriptExecutor executor, DealType dealType, long initialId)
        {
            List<OMPriceHistory> result = new List<OMPriceHistory>();
            bool containsHistory = bool.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["readCIANPrHCh"]).ToString());
            bool isRent = dealType == DealType.RentSuggestion || dealType == DealType.RentDeal;
            bool onePrice = bool.Parse(executor.ExecuteScript(ConfigurationManager.AppSettings["readCIANPrDCh"]).ToString());
            Regex NO = new Regex("[^0-9]");
            long meterPrice = 0, price = 0, lowPrice = 0, areaFrom = 0, areaTo = 0;
            DateTime refreshingDate = currentDate;
            if (containsHistory && isRent && onePrice)          //С историей    аренда      Одна цена 
            {
                executor.ExecuteScript(string.Format(ConfigurationManager.AppSettings["CIANHCCScript"], ConfigurationManager.AppSettings["CIANArrVar"]));
                executor.ExecuteScript(string.Format(ConfigurationManager.AppSettings["CIANHCFScript"], ConfigurationManager.AppSettings["CIANArrVar"], ConfigurationManager.AppSettings["CIANInnerSep"]));
                string[] history = executor.ExecuteScript(
                    string.Format(ConfigurationManager.AppSettings["CIANHCRScript"], ConfigurationManager.AppSettings["CIANArrVar"], ConfigurationManager.AppSettings["CIANOuterSep"]))
                    .ToString()
                    .Split(ConfigurationManager.AppSettings["CIANOuterSep"]);
                history.ToList().ForEach(x => 
                {
                    string[] data = x.Split(ConfigurationManager.AppSettings["CIANInnerSep"]);
                    refreshingDate = ParseDateTime(data[0]);
                    long.TryParse(NO.Replace(data[1], string.Empty), out price);
                    result.Add(new OMPriceHistory { InitialId = initialId, PriceValueTo = price, ChangingDate = refreshingDate });
                });
            }
            else if(containsHistory && isRent && !onePrice)     //С историей    аренда      Две цены
            {
                executor.ExecuteScript(string.Format(ConfigurationManager.AppSettings["CIANHCCScript"], ConfigurationManager.AppSettings["CIANArrVar"]));
                executor.ExecuteScript(string.Format(ConfigurationManager.AppSettings["CIANHCFScript"], ConfigurationManager.AppSettings["CIANArrVar"], ConfigurationManager.AppSettings["CIANInnerSep"]));
                string[] history = executor.ExecuteScript(
                    string.Format(ConfigurationManager.AppSettings["CIANHCRScript"], ConfigurationManager.AppSettings["CIANArrVar"], ConfigurationManager.AppSettings["CIANOuterSep"]))
                    .ToString()
                    .Split(ConfigurationManager.AppSettings["CIANOuterSep"]);
                string[] splitedArea = executor.ExecuteScript(ConfigurationManager.AppSettings["readCIANArea"]).ToString().Split(ConfigurationManager.AppSettings["CIANAreaSpliter"]);
                long.TryParse(NO.Replace(splitedArea[0], string.Empty), out areaFrom);
                long.TryParse(NO.Replace(splitedArea[1], string.Empty), out areaTo);
                history.ToList().ForEach(x =>
                {
                    string[] data = x.Split(ConfigurationManager.AppSettings["CIANInnerSep"]);
                    refreshingDate = ParseDateTime(data[0]);
                    long.TryParse(NO.Replace(data[1], string.Empty), out meterPrice);
                    lowPrice = (long)Math.Ceiling((decimal)meterPrice / 12 * areaFrom);
                    price = (long)Math.Ceiling((decimal)meterPrice / 12 * areaTo);
                    result.Add(new OMPriceHistory { InitialId = initialId, PriceValueFrom = lowPrice, PriceValueTo = price, ChangingDate = refreshingDate });
                });
            }
            else if (containsHistory && !isRent && onePrice)    //С историей    продажа     Одна цена 
            {
                executor.ExecuteScript(string.Format(ConfigurationManager.AppSettings["CIANHCCScript"], ConfigurationManager.AppSettings["CIANArrVar"]));
                executor.ExecuteScript(string.Format(ConfigurationManager.AppSettings["CIANHCFScript"], ConfigurationManager.AppSettings["CIANArrVar"], ConfigurationManager.AppSettings["CIANInnerSep"]));
                string[] history = executor.ExecuteScript(
                    string.Format(ConfigurationManager.AppSettings["CIANHCRScript"], ConfigurationManager.AppSettings["CIANArrVar"], ConfigurationManager.AppSettings["CIANOuterSep"]))
                    .ToString()
                    .Split(ConfigurationManager.AppSettings["CIANOuterSep"]);
                history.ToList().ForEach(x =>
                {
                    string[] data = x.Split(ConfigurationManager.AppSettings["CIANInnerSep"]);
                    refreshingDate = ParseDateTime(data[0]);
                    long.TryParse(NO.Replace(data[1], string.Empty), out price);
                    result.Add(new OMPriceHistory { InitialId = initialId, PriceValueTo = price, ChangingDate = refreshingDate });
                });
            }
            else if (containsHistory && !isRent && !onePrice)   //С историей    продажа     Две цены
            {
                executor.ExecuteScript(string.Format(ConfigurationManager.AppSettings["CIANHCCScript"], ConfigurationManager.AppSettings["CIANArrVar"]));
                executor.ExecuteScript(string.Format(ConfigurationManager.AppSettings["CIANHCFScript"], ConfigurationManager.AppSettings["CIANArrVar"], ConfigurationManager.AppSettings["CIANInnerSep"]));
                string[] history = executor.ExecuteScript(
                    string.Format(ConfigurationManager.AppSettings["CIANHCRScript"], ConfigurationManager.AppSettings["CIANArrVar"], ConfigurationManager.AppSettings["CIANOuterSep"]))
                    .ToString()
                    .Split(ConfigurationManager.AppSettings["CIANOuterSep"]);
                string[] splitedArea = executor.ExecuteScript(ConfigurationManager.AppSettings["readCIANArea"]).ToString().Split(ConfigurationManager.AppSettings["CIANAreaSpliter"]);
                long.TryParse(NO.Replace(splitedArea[0], string.Empty), out areaFrom);
                long.TryParse(NO.Replace(splitedArea[1], string.Empty), out areaTo);
                history.ToList().ForEach(x =>
                {
                    string[] data = x.Split(ConfigurationManager.AppSettings["CIANInnerSep"]);
                    refreshingDate = ParseDateTime(data[0]);
                    long.TryParse(NO.Replace(data[1], string.Empty), out meterPrice);
                    lowPrice = meterPrice  * areaFrom;
                    price = meterPrice * areaTo;
                    result.Add(new OMPriceHistory { InitialId = initialId, PriceValueFrom = lowPrice, PriceValueTo = price, ChangingDate = refreshingDate });
                });
            }
            else if (!containsHistory && isRent && onePrice)    //Без истории   аренда      Одна цены
            {
                long.TryParse(NO.Replace(executor.ExecuteScript(string.Format(ConfigurationManager.AppSettings["readCIANPrGTR"], ConfigurationManager.AppSettings["CIANPrVar"])).ToString(), string.Empty), out price);
                result.Add(new OMPriceHistory { InitialId = initialId, PriceValueTo = price, ChangingDate = refreshingDate });
            }
            else if (!containsHistory && isRent && !onePrice)   //Без истории   аренда      Две цены
            {
                long.TryParse(NO.Replace(executor.ExecuteScript(string.Format(ConfigurationManager.AppSettings["readCIANPrGTR"], ConfigurationManager.AppSettings["CIANHighPricePrVar"])).ToString(), string.Empty), out price);
                long.TryParse(NO.Replace(executor.ExecuteScript(string.Format(ConfigurationManager.AppSettings["readCIANPrGTR"], ConfigurationManager.AppSettings["CIANLowPrVar"])).ToString(), string.Empty), out lowPrice);
                result.Add(new OMPriceHistory { InitialId = initialId, PriceValueFrom = lowPrice, PriceValueTo = price, ChangingDate = refreshingDate });
            }
            else if (!containsHistory && !isRent && onePrice)   //Без истории   продажа     Одна цена 
            {
                long.TryParse(NO.Replace(executor.ExecuteScript(string.Format(ConfigurationManager.AppSettings["readCIANPrGTR"], ConfigurationManager.AppSettings["CIANPrVar"])).ToString(), string.Empty), out price);
                result.Add(new OMPriceHistory { InitialId = initialId, PriceValueTo = price, ChangingDate = refreshingDate });
            }
            else if (!containsHistory && !isRent && !onePrice)  //Без истории   продажа     Две цены
            {
                long.TryParse(NO.Replace(executor.ExecuteScript(string.Format(ConfigurationManager.AppSettings["readCIANPrGTR"], ConfigurationManager.AppSettings["CIANHighPricePrVar"])).ToString(), string.Empty), out price);
                long.TryParse(NO.Replace(executor.ExecuteScript(string.Format(ConfigurationManager.AppSettings["readCIANPrGTR"], ConfigurationManager.AppSettings["CIANLowPrVar"])).ToString(), string.Empty), out lowPrice);
                result.Add(new OMPriceHistory { InitialId = initialId, PriceValueFrom = lowPrice, PriceValueTo = price, ChangingDate = refreshingDate });
            }
            return result;
        }

        private DateTime ParseDateTime(string dateString)
        {
            string[] dateArr = dateString.Split(" ");
            DateTime result = currentDate;
            if(dateArr.Length == 2)
            {
                int[] time = dateArr[1].Split(":").ToList().Select(x => Int32.Parse(x)).ToArray();
                result = result.AddDays(GetDayDifference(dateArr[0]));
                result = new DateTime(result.Year, result.Month, result.Day, time[0], time[1], 0);
            }
            else
            {
                if(!dateArr[2].Contains(":")) result = new DateTime(Int32.Parse(dateArr[2]), GetMonthFromString(dateArr[1]), Int32.Parse(dateArr[0]), 0, 0, 0);
                else
                {
                    int[] time = dateArr[2].Split(":").ToList().Select(x => Int32.Parse(x)).ToArray();
                    result = new DateTime(result.Year, GetMonthFromString(dateArr[1]), Int32.Parse(dateArr[0]), time[0], time[1], 0);
                }
            }
            return result;
        }

        private int GetMonthFromString(string month)
        {
            switch (month)
            {
                case "января": return 1;
                case "февраля": return 2;
                case "марта": return 3;
                case "апреля": return 4;
                case "мая": return 5;
                case "июня": return 6;
                case "июля": return 7;
                case "августа": return 8;
                case "сентября": return 9;
                case "октября": return 10;
                case "ноября": return 11;
                case "декабря": return 12;
                default: return 0;
            }
        }

        private int GetDayDifference(string day)
        {
            switch(day)
            {
                case "вчера": return -1;
                case "позавчера": return -2;
                default: return 0;
            }
        }

    }

}