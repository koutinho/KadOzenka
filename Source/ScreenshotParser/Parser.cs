using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ObjectModel.Directory;
using ObjectModel.Market;
using Core.Main.FileStorages;
using System.Configuration;

namespace ScreenshotParser
{

    class Parser
    {

        private IJavaScriptExecutor JavaScript { get; set; }
        private IWebDriver Driver { get; set; }

        public Parser()
        {
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--window-size=1920,1040");
            options.AddArgument("--headless");
            options.AddArgument("--disable-application-cache");
            options.AddArgument("--silent");
            options.AddArgument("log-level=3");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-infobars");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-browser-side-navigation");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--ignore-certificate-errors");
            options.PageLoadStrategy = PageLoadStrategy.Eager;
            Driver = new ChromeDriver(chromeDriverService, options);
            Driver.Manage().Window.Maximize();
            JavaScript = (IJavaScriptExecutor)Driver;
        }

        public void ParseUrl(OMCoreObject initialObject, int incommon, int currentCount, string argegatorName)
        {
            Stopwatch sw = new Stopwatch();
            DateTime currentDate = DateTime.Now;
            sw.Start();
            Driver.Navigate().GoToUrl(initialObject.Url);
            bool isCapcha = bool.Parse(JavaScript.ExecuteScript(ConfigurationManager.AppSettings[$"isCapcha_{argegatorName}"]).ToString());
            try
            {
                if (isCapcha)
                    if (argegatorName == "Cian") new Captcha(initialObject.Url, JavaScript, Driver).SolveCapcha(); 
                    else Console.WriteLine("==============>Капча!");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            isCapcha = bool.Parse(JavaScript.ExecuteScript(ConfigurationManager.AppSettings[$"isCapcha_{argegatorName}"]).ToString());
            bool
                priceExists = bool.Parse(JavaScript.ExecuteScript(ConfigurationManager.AppSettings[$"priceExists_{argegatorName}"]).ToString()),
                hightPriceExists = bool.Parse(JavaScript.ExecuteScript(ConfigurationManager.AppSettings[$"hightPriceExists_{argegatorName}"]).ToString()),
                tradePrice = bool.Parse(JavaScript.ExecuteScript(ConfigurationManager.AppSettings[$"tradePrice_{argegatorName}"]).ToString()),
                isDeleted = bool.Parse(JavaScript.ExecuteScript(ConfigurationManager.AppSettings[$"isDeleted_{argegatorName}"]).ToString()),
                isUnPublished = bool.Parse(JavaScript.ExecuteScript(ConfigurationManager.AppSettings[$"isUnPublished_{argegatorName}"]).ToString());
            string log = string.Empty;
            try
            {
                if (isUnPublished)
                {
                    initialObject.ExclusionStatus_Code = ExclusionStatus.Unpublished;
                    initialObject.ProcessType_Code = ProcessStep.Excluded;
                    if (hightPriceExists) initialObject.Price = Decimal.Parse(JavaScript.ExecuteScript(ConfigurationManager.AppSettings[$"getHightPrice_{argegatorName}"]).ToString());
                    else if (priceExists) initialObject.Price = Decimal.Parse(JavaScript.ExecuteScript(ConfigurationManager.AppSettings[$"getPrice_{argegatorName}"]).ToString());
                    log = $"Снято с публикации. Цена {initialObject.Price}";
                }    
                else if (priceExists)
                {
                    initialObject.Price = Decimal.Parse(JavaScript.ExecuteScript(ConfigurationManager.AppSettings[$"getPrice_{argegatorName}"]).ToString());
                    log = $"Актуально. Цена {initialObject.Price}";
                    OMScreenshots screenshot = new OMScreenshots { InitialId = initialObject.Id, CreationDate = currentDate, Type = "image/png" };
                    FileStorageManager.Save(new MemoryStream(TakeScreenShot()), "MarketObjectScreenShot", currentDate, screenshot.Save().ToString());
                }
                else if (hightPriceExists)
                {
                    initialObject.Price = Decimal.Parse(JavaScript.ExecuteScript("return document.querySelector('span[itemprop=highPrice]').innerText.match(/\\d/g).join('');").ToString());
                    initialObject.ExclusionStatus_Code = ExclusionStatus.IncorrectPrice;
                    initialObject.ProcessType_Code = ProcessStep.Excluded;
                    log = $"Коридор. Максимальная цена: {initialObject.Price}";
                }
                else if (tradePrice)
                {
                    initialObject.ExclusionStatus_Code = ExclusionStatus.Auction;
                    initialObject.ProcessType_Code = ProcessStep.Excluded;
                    log = $"Аукцион.";
                }
                else if (isDeleted)
                {
                    initialObject.ExclusionStatus_Code = ExclusionStatus.Deleted;
                    initialObject.ProcessType_Code = ProcessStep.Excluded;
                    log = "Объявление удалено.";
                }
                if(!isCapcha)
                {
                    initialObject.CCT = null;
                    initialObject.ParserTime = currentDate;
                }
                initialObject.Save();
            }
            catch (Exception ex)
            {
                log = $"{ex.Message}\n{ex.StackTrace}";
            }
            Console.WriteLine($"{initialObject.Id} ({currentCount} из {incommon}) {initialObject.Url}: {log} {((decimal)sw.ElapsedMilliseconds)/1000}");
            sw.Stop();
        }

        public void QuitDriver() => Driver.Quit();

        public byte[] TakeScreenShot()
        {
            FullScreen screen = new FullScreen();
            screen.ExecuteScreenScripts(JavaScript);
            var metrics = new Dictionary<string, object>();
            metrics["width"] = JavaScript.ExecuteScript("return Math.max(window.innerWidth,document.body.scrollWidth,document.documentElement.scrollWidth);");
            metrics["height"] = JavaScript.ExecuteScript("return Math.max(window.innerHeight,document.body.scrollHeight,document.documentElement.scrollHeight);");
            metrics["deviceScaleFactor"] = 1;
            metrics["mobile"] = false;
            ((ChromeDriver)Driver).ExecuteChromeCommand("Emulation.setDeviceMetricsOverride", metrics);
            Thread.Sleep(1500);
            return screen.AppendImage(((ITakesScreenshot)Driver).GetScreenshot().AsByteArray, screen.MakeScreenShot(0, 1040, 1920, 40));
        }

    }

}
