using System;
using System.IO;

using System.Drawing;

namespace ImageProccessor
{
    public class Choper
    {

        public void ChopImage(MemoryStream memoryStream, int left, int top, int height, int width, string path)
        {
            Image original = Image.FromStream(memoryStream, true);
            Rectangle rectangle = new Rectangle(new Point(left, top), new Size(width, height));
            Bitmap result = new Bitmap(rectangle.Width, rectangle.Height);
            Graphics graphics = Graphics.FromImage(result);
            graphics.DrawImage(original, 0, 0, rectangle, GraphicsUnit.Pixel);
            graphics.Dispose();
            result.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        public Size IMGSize(int x, int y)
        {
            return new Size(x, y);
        }

    }
}
