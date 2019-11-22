using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;
using System.Collections.Generic;

using ObjectModel.Market;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Drawing.Imaging;
using OpenQA.Selenium.Support.UI;
using Core.Shared.Extensions;
using ImageProccessor;

namespace KadOzenka.Dal.Selenium.ScreenShots
{

    public class FullScreen
    {
        public byte[] TakeScreenShot(ChromeDriver driver)
        {
	        var description = driver.FindElementsById("description").FirstOrDefault();
	        if (description != null)
	        {
		        var parent = driver.ExecuteScript(ConfigurationManager.AppSettings["getParentElement"], description);
		        (parent as IWebElement)?.FindElements(By.TagName("button")).FirstOrDefault(x =>
			        x.Text.Contains("Показать информацию") ||
			        x.FindElements(By.PartialLinkText("Показать информацию")).Count > 0)?.Click();
	        }

	        var metrics = new Dictionary<string, object>();
            //Console.ReadLine();
            metrics["width"] = driver.ExecuteScript(ConfigurationManager.AppSettings["getPageMaxWidth"]);
	        metrics["height"] = driver.ExecuteScript(ConfigurationManager.AppSettings["getPageMaxHeight"]);
	        metrics["deviceScaleFactor"] = 1;
	        metrics["mobile"] = false;

	        //Execute the emulation Chrome Command to change browser to a custom device that is the size of the entire page
	        driver.ExecuteChromeCommand("Emulation.setDeviceMetricsOverride", metrics);

            driver.ExecuteScript(ConfigurationManager.AppSettings["removeCIANEnterSiteScript"]);
            driver.ExecuteScript(ConfigurationManager.AppSettings["removeCIANAboutSiteScript"]);
            driver.ExecuteScript(ConfigurationManager.AppSettings["removeCIANRightBanerScript"]);
            driver.ExecuteScript(ConfigurationManager.AppSettings["removeCIANBanerScript"]);
            driver.ExecuteScript(ConfigurationManager.AppSettings["hidePageScroll"]);

	        var screenShot = ((ITakesScreenshot)driver).GetScreenshot();
			driver.ExecuteChromeCommand("Emulation.clearDeviceMetricsOverride", new Dictionary<string, object>());

			var xBottomPoint = ConvertObjPointToInt(driver.ExecuteScript(ConfigurationManager.AppSettings["getWindowOuterWidth"]));
			var yBottomPoint = ConvertObjPointToInt(driver.ExecuteScript(ConfigurationManager.AppSettings["getWindowOuterHeight"]));
			driver.Manage().Window.FullScreen();
	        var yBottomNewPoint = driver.Manage().Window.Size.Height;
	        driver.Manage().Window.Minimize();

			var choper = new Choper();
	        var panelScreenShot = choper.MakeScreenShot(0,
		        (int)yBottomPoint, (int)xBottomPoint,
		        (int)yBottomNewPoint - (int)yBottomPoint);
	        var res = choper.AppendImage(screenShot.AsByteArray, panelScreenShot);

	        return res;
        }

	    private int ConvertObjPointToInt(object point)
	    {
		     return (int)Math.Round(double.Parse(point.ToString().Replace('.', ',')));
		}

    }

}
