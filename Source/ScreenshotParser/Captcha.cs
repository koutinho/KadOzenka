using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using OpenQA.Selenium;

namespace ScreenshotParser
{

    class Captcha
    {

        private string Url { get; set; }
        private IJavaScriptExecutor JavaScript { get; set; }
        private IWebDriver Driver { get; set; }
        private string CapchaKey { get; set; }
        private string CapchaType { get; set; }
        private string CapchaMethodType { get; set; }
        private string ScriptCapchaKey { get; set; }
        private string ScriptSubmitCapcha { get; set; }

        public Captcha(string url, IJavaScriptExecutor javaScript, IWebDriver driver)
        {
            Url = url;
            JavaScript = javaScript;
            Driver = driver;
            CapchaKey = "8023344f5b13f2429d0679e5623f4d26";
            CapchaType = "userrecaptcha";
            ScriptCapchaKey = "return new URLSearchParams(document.querySelectorAll('#captcha div iframe')[0].getAttribute('src')).get('k');";
            ScriptSubmitCapcha = "document.querySelector('textarea#g-recaptcha-response').removeAttribute('style');document.querySelector('textarea#g-recaptcha-response').value = '{0}';document.getElementById('form_captcha').submit();";
            CapchaMethodType = "get";
        }

        public void SolveCapcha()
        {
            string capchaKey = SendCapcha(JavaScript.ExecuteScript(ScriptCapchaKey).ToString());
            Thread.Sleep(25000);
            string capchaToken = GetCapchaToken(capchaKey);
            JavaScript.ExecuteScript(string.Format(ScriptSubmitCapcha, capchaToken));
            Driver.Navigate().Refresh();
        }

        private string SendCapcha(string googlekey)
        {
            string url = $"https://rucaptcha.com/in.php?key={CapchaKey}&method={CapchaType}&googlekey={googlekey}&pageurl={Url}";
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
            string url = $"https://rucaptcha.com/res.php?key={CapchaKey}&action={CapchaMethodType}&id={id}";
            string capchaResponse = "CAPCHA_NOT_READY";
            while (capchaResponse == "CAPCHA_NOT_READY")
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Proxy = null;
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    using Stream stream = response.GetResponseStream();
                        using StreamReader reader = new StreamReader(stream);
                capchaResponse = reader.ReadToEnd();
                if (capchaResponse == "CAPCHA_NOT_READY")
                {
                    Console.WriteLine(url);
                    Thread.Sleep(10000);
                }
            }
            return capchaResponse.Split('|')[1];
        }

    }

}
