using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace ImageProccessor
{

    public class MapDrawer
    {

        private SmoothingMode _smoothing = SmoothingMode.HighQuality;
        private SolidBrush _brush = new SolidBrush(Color.FromArgb(102, Color.FromArgb(0, 255, 0)));
        private Pen _penBolt = new Pen(Color.FromArgb(229, Color.FromArgb(254, 22, 22)), 2);
        private Pen _penOrdinar = new Pen(Color.FromArgb(229, Color.FromArgb(254, 22, 22)), 1);
        private ImageFormat _format = ImageFormat.Png;
        private GraphicsUnit _unit = GraphicsUnit.Pixel;

        public void DrawMap(int xExt, int yExt, List<dynamic> coords, string name, bool bolt)
        {
            Bitmap result = new Bitmap(xExt, yExt);
            Graphics drawing = Graphics.FromImage(result);
            drawing.SmoothingMode = _smoothing;
            coords.ForEach(x =>
            {
                var Coordinates = x.Coordinates;
                foreach (var points in Coordinates)
                {
                    List<Point> pointsLst = new List<Point>();
                    foreach (var point in points)
                    {
                        int X = point[0], Y = point[1];
                        pointsLst.Add(new Point(X, Y));
                    }
                    drawing.FillPolygon(_brush, pointsLst.ToArray());
                    drawing.DrawPolygon(bolt ? _penBolt : _penOrdinar, pointsLst.ToArray());
                }
            });
            result.Save(name, _format);
        }

        public void ChopData(string fileName, int hStartTile, int yStartTile, int zStart, int z, int tileSize, string reqFolder)
        {
            Image image = Image.FromFile(fileName);
            Bitmap bmpImage = new Bitmap(image);
            int width = image.Width, height = image.Height;
            Image[,] imgarray = new Image[(height / tileSize), (width / tileSize)];
            int currentStartX = (int)(hStartTile * Math.Pow(2, z - zStart)), currentStartY = (int)(yStartTile * Math.Pow(2, z - zStart));
            if (!Directory.Exists(reqFolder)) Directory.CreateDirectory(reqFolder);
            for (int i = 0; i < height / tileSize; i++)
            {
                for (int j = 0; j < width / tileSize; j++)
                {
                    imgarray[i, j] = new Bitmap(tileSize, tileSize);
                    Graphics graphics = Graphics.FromImage(imgarray[i, j]);
                    graphics.DrawImage(image, new Rectangle(0, 0, tileSize, tileSize), new Rectangle(j * tileSize, i * tileSize, tileSize, tileSize), _unit);
                    graphics.Save();
                    graphics.Dispose();
                    new Bitmap(imgarray[i, j], new Size(tileSize, tileSize)).Save(reqFolder + $@"\{currentStartX + j}_{currentStartY + i}.png", _format);
                }
            }
        }

    }

}