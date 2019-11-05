using System;
using System.Text;
using System.Collections.Generic;

using ImageProccessor;
using OpenQA.Selenium;
using System.Configuration;

namespace KadOzenka.Dal.Selenium.ScreenShots
{

    class OnlyPriceByExecutor
    {

        public void TakeScreenShot(IJavaScriptExecutor executor, IWebDriver driver, string screenShotName)
        {
            executor.ExecuteScript(string.Format(ConfigurationManager.AppSettings["readCIANPriceBlockScript"], ConfigurationManager.AppSettings["readCIANPrVar"]));
            new Choper().ChopImage(
                new Ordinar().GetScreenShot(driver),
                GPV(executor, $"{ConfigurationManager.AppSettings["readCIANPrVar"]}.x"),
                GPV(executor, $"{ConfigurationManager.AppSettings["readCIANPrVar"]}.y"),
                GPV(executor, $"{ConfigurationManager.AppSettings["readCIANPrVar"]}.height"),
                GPV(executor, $"{ConfigurationManager.AppSettings["readCIANPrVar"]}.width"),
                string.Format(ConfigurationManager.AppSettings["screenShotFolder"], screenShotName)
            );
        }

        private int PxToInt(string initialValue) => (int)Math.Round(double.Parse(initialValue.Replace('.', ',')));

        private int GPV(IJavaScriptExecutor executor, string variable) => PxToInt(executor.ExecuteScript($"return {variable};").ToString());

    }

}
