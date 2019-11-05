using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

using ObjectModel.Market;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace KadOzenka.Dal.Selenium.ScreenShots
{

    public class Ordinar
    {

        public void TakeScreenShot()
        {
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.cian.ru/sale/commercial/215099657/");
            File.WriteAllBytes(@"C:\Users\silanov\Desktop\1.png", new MemoryStream(((ITakesScreenshot)driver).GetScreenshot().AsByteArray).ToArray());
            driver.Close();
        }

        public void TakeScreenShot(IWebDriver driver, string path) => File.WriteAllBytes(path, new MemoryStream(((ITakesScreenshot)driver).GetScreenshot().AsByteArray).ToArray());

        public MemoryStream GetScreenShot(IWebDriver driver) => new MemoryStream(((ITakesScreenshot)driver).GetScreenshot().AsByteArray);

    }

}
