using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;

namespace ScreenshotParser
{

    class FullScreen
    {

        public byte[] MakeScreenShot(int left, int top, int width, int height)
        {
            var printScreen = new Bitmap(width, height);
            var graphics = Graphics.FromImage(printScreen);
            graphics.CopyFromScreen(left, top, 0, 0, printScreen.Size);
            graphics.Dispose();
            var stream = new MemoryStream();
            printScreen.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }

        public byte[] AppendImage(byte[] imageMemoryStream, byte[] appendImageMemoryStream)
        {
            var image = Image.FromStream(new MemoryStream(imageMemoryStream), true);
            var appendImage = Image.FromStream(new MemoryStream(appendImageMemoryStream), true);
            var result = new Bitmap(image.Width, image.Height + appendImage.Height);
            var graphics = Graphics.FromImage(result);
            graphics.DrawImage(image, 0, 0, image.Width, image.Height);
            graphics.DrawImage(appendImage, 0, image.Height, appendImage.Width, appendImage.Height);
            graphics.Dispose();
            var stream = new MemoryStream();
            result.Save(stream, ImageFormat.Jpeg);
            return stream.ToArray();
        }

        public void ExecuteScreenScripts(IJavaScriptExecutor JavaScript)
        {
            JavaScript.ExecuteScript("var showInfo=document.querySelector(\"div[class*='offer_card_page-main'] button[class*=button_collapsed]\"); if(showInfo) showInfo.click();");
            JavaScript.ExecuteScript("try{document.querySelector('div[class=\"uxs-wrapper\"]').remove();}catch{}");
            JavaScript.ExecuteScript("try{document.querySelector('div[id=\"stretch-banner\"]').remove();}catch{}");
            JavaScript.ExecuteScript("try{document.querySelector('div[data-name=\"AsideBanners\"]').remove();}catch{}");
            JavaScript.ExecuteScript("try{document.querySelector('div[class*=\"bottom_banners\"]').remove();}catch{}");
            JavaScript.ExecuteScript("try{document.querySelector('div[data-name=\"CookieAgreementBar\"]').remove();}catch{}");
            JavaScript.ExecuteScript("try{document.querySelector('div[class*=\"offer_card_page-footer\"]').remove();}catch{}");
        }

    }

}
