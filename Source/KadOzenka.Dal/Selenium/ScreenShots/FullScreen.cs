using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;
using System.Collections.Generic;
using System.Windows.Forms;

using ObjectModel.Market;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Drawing.Imaging;
using OpenQA.Selenium.Support.UI;
using Core.Shared.Extensions;
using ImageProccessor;
using System.Threading;

namespace KadOzenka.Dal.Selenium.ScreenShots
{

    public class FullScreen
    {
        public byte[] TakeScreenShot(ChromeDriver driver)
        {
            driver.ExecuteScript(ConfigurationManager.AppSettings["showCIANHiddenInfo"]);
            driver.ExecuteScript(ConfigurationManager.AppSettings["removeCIANEnterSiteScript"]);
            driver.ExecuteScript(ConfigurationManager.AppSettings["removeCIANAboutSiteScript"]);
            driver.ExecuteScript(ConfigurationManager.AppSettings["removeCIANRightBanerScript"]);
            driver.ExecuteScript(ConfigurationManager.AppSettings["removeCIANBanerScript"]);
            driver.ExecuteScript(ConfigurationManager.AppSettings["hidePageScroll"]);

            var metrics = new Dictionary<string, object>();
            metrics["width"] = driver.ExecuteScript(ConfigurationManager.AppSettings["getPageMaxWidth"]);
	        metrics["height"] = driver.ExecuteScript(ConfigurationManager.AppSettings["getPageMaxHeight"]);
	        metrics["deviceScaleFactor"] = 1;
	        metrics["mobile"] = false;
	        driver.ExecuteChromeCommand("Emulation.setDeviceMetricsOverride", metrics);

            Thread.Sleep(500);
            var screenShot = ((ITakesScreenshot)driver).GetScreenshot();

			driver.ExecuteChromeCommand("Emulation.clearDeviceMetricsOverride", new Dictionary<string, object>());

			var xBottomPoint = ConvertObjPointToInt(driver.ExecuteScript(ConfigurationManager.AppSettings["getWindowOuterWidth"]));
			var yBottomPoint = ConvertObjPointToInt(driver.ExecuteScript(ConfigurationManager.AppSettings["getWindowOuterHeight"]));

            var choper = new Choper();
            var panelScreenShot = choper.MakeScreenShot(0, (int)yBottomPoint, (int)xBottomPoint, 40);
	        return choper.AppendImage(screenShot.AsByteArray, panelScreenShot);
        }

        private int ConvertObjPointToInt(object point) => (int)Math.Round(double.Parse(point.ToString().Replace('.', ',')));

    }

}