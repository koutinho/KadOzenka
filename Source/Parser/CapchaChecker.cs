using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Parser
{

    class CapchaChecker
    {

        private string Url { get; set; }
        private CapchaSettings Settings { get; set; }

        public CapchaChecker(string url, CapchaSettings settings)
        {
            Settings = settings;
            Url = url;
        }

        public string GetCapcha()
        {
            if (Settings.Agregator_id == 1)
            {
                ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;
                ChromeOptions options = new ChromeOptions();
                Settings.Driver_options.Split(", ").Where(x => !x.StartsWith("\\\\")).ToList().ForEach(x => options.AddArgument(x));
                IWebDriver driver = new ChromeDriver(chromeDriverService, options);
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                driver.Navigate().GoToUrl(Url);
                string capchaKey = SendCapcha(js.ExecuteScript(Settings.Script_capcha_key).ToString());
                Thread.Sleep((int)Settings.Capcha_timeout);
                string capchaToken = GetCapchaToken(capchaKey);
                js.ExecuteScript(string.Format(Settings.Script_submit_capcha, capchaToken));
                driver.Navigate().Refresh();
                string cookies = string.Join("; ", driver.Manage().Cookies.AllCookies.ToList().Select(x => $"{x.Name}={x.Value}"));
                driver.Quit();
                return cookies;
            } 
            else
            {

                return null;
            }
        }

        private string SendCapcha(string googlekey)
        {
            string url = string.Format(Settings.Capcha_send_template, Settings.Capcha_key, Settings.Capcha_type, googlekey, Url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Proxy = null;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using Stream stream = response.GetResponseStream();
                    using StreamReader reader = new StreamReader(stream);
                        return string.Concat(reader.ReadToEnd().Where(char.IsDigit));
        }

        private string GetCapchaToken(string id)
        {
            string url = string.Format(Settings.Capcha_get_template, Settings.Capcha_key, Settings.Capcha_method_type, id);
            string capchaResponse = Settings.Capcha_retry_parameter;
            while (capchaResponse == Settings.Capcha_retry_parameter)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Proxy = null;
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    using Stream stream = response.GetResponseStream();
                        using StreamReader reader = new StreamReader(stream);
                            capchaResponse = reader.ReadToEnd();
                if (capchaResponse == Settings.Capcha_retry_parameter)
                {
                    Console.WriteLine(url);
                    Thread.Sleep(10000);
                }
            }
            return capchaResponse.Split('|')[1];
        }

    }

}
