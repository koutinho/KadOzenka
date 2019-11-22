using System.IO;

using System.Drawing;
using System.Drawing.Imaging;

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

		public Size IMGSize(int x, int y)
        {
            return new Size(x, y);
        }
    }
}
