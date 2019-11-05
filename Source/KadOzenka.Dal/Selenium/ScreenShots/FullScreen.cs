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

namespace KadOzenka.Dal.Selenium.ScreenShots
{

    public class FullScreen
    {

        public void PreperateHTML2Canvas(IJavaScriptExecutor executor) => executor.ExecuteScript(File.ReadAllText(ConfigurationManager.AppSettings["HTML2CanvasJsPath"]));

        public byte[] TakeScreenShot(IJavaScriptExecutor executor, WebDriverWait wait)
        {
            PreperateHTML2Canvas(executor);
            executor.ExecuteScript(File.ReadAllText(@"Resources\screenshotScript.js"));
            wait.Until(wd => !string.IsNullOrEmpty((string)executor.ExecuteScript("return window.ScreenShot;")));
            string screenShot = executor.ExecuteScript("return window.ScreenShot;").ToString();
            return screenShot != "error" ? Convert.FromBase64String(screenShot.Replace("data:image/png;base64,", string.Empty)) : null;
        }

    }

}
