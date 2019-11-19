using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using OpenQA.Selenium;
using ObjectModel.Market;
using Core.Main.FileStorages;
using OpenQA.Selenium.Chrome;

namespace KadOzenka.BlFrontEnd.GetSeleniumScreens
{

    class Selenium
    {
        readonly List<OMCoreObject> AllObjects =
			OMCoreObject.Where(x => x.Market_Code != ObjectModel.Directory.MarketTypes.Rosreestr &&
									x.ProcessType_Code == ObjectModel.Directory.ProcessStep.CadastralNumberStep &&
									x.LastDateUpdate == null)
						.Select(x => new { x.Url, x.Price, x.LastDateUpdate })
                        .Execute()
                        .ToList();

        public void MakeScreenshot()
        {
			IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            driver.Manage().Window.Maximize();
			int breakCtr = 0;
            foreach (OMCoreObject obj in AllObjects)
            {
                try
                {
					OMScreenshots screenshot = new OMScreenshots { InitialId = obj.Id, Type = "image/png", CreationDate = DateTime.Now };
                    driver.Navigate().GoToUrl(obj.Url);
                    bool IsDenied = driver.FindElements(By.ClassName("a10a3f92e9--container--1In69")).Count == 0;
                    long price = long.Parse(
                        Regex.Replace(
                            driver.FindElements(By.ClassName("a10a3f92e9--price--1HD9F"))
                                  .FirstOrDefault()
                                  .FindElements(By.CssSelector("span[content]"))
                                  .FirstOrDefault()
                                  .GetAttribute("content"),
                            "[^0-9]",
                            string.Empty
                        )
                    );
                    bool priceIsEquals = obj.Price.Equals(price);
                    if (priceIsEquals)
                    {
	                    //obj.ScreenShotExists = true;
						var description = ((ChromeDriver) driver).FindElementsById("description").FirstOrDefault();
	                    if (description != null)
	                    {
		                    var parent = ((ChromeDriver)driver).ExecuteScript(ConfigurationManager.AppSettings["getParentElement"], description);
		                    ((IWebElement)parent)?.FindElements(By.TagName("button")).FirstOrDefault()?.Click();
						}

						var metrics = new Dictionary<string, object>();
						metrics["width"] = ((ChromeDriver)driver).ExecuteScript(ConfigurationManager.AppSettings["getPageMaxWidth"]);
						metrics["height"] = ((ChromeDriver)driver).ExecuteScript(ConfigurationManager.AppSettings["getPageMaxHeight"]);
						metrics["deviceScaleFactor"] = 1;
						metrics["mobile"] = false;

						//Execute the emulation Chrome Command to change browser to a custom device that is the size of the entire page
						((ChromeDriver)driver).ExecuteChromeCommand("Emulation.setDeviceMetricsOverride", metrics);
						((ChromeDriver)driver).ExecuteScript(ConfigurationManager.AppSettings["removeCIANBanerScript"]);
						((ChromeDriver)driver).ExecuteScript(ConfigurationManager.AppSettings["hidePageScroll"]);

						var screen = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;

						((ChromeDriver)driver).ExecuteChromeCommand("Emulation.clearDeviceMetricsOverride", new Dictionary<string, object>());

						obj.Save();
						screenshot.Save();
						FileStorageManager.Save(
							new MemoryStream(((ITakesScreenshot)driver).GetScreenshot().AsByteArray),
							ConfigurationManager.AppSettings["screenShotFolder"],
							DateTime.Now,
							$"{screenshot.Id}");
						breakCtr++;
                        Console.WriteLine($"{breakCtr}: {obj.Url}");
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }
                if (breakCtr >= 100) break;
            }
            driver.Close();
        }
    }

}
