using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace KadOzenka.Dal.XmlParser
{

    public class CoordinatesConverter
    {

        private string coordFolderPxl = ConfigurationManager.AppSettings["DestinyPixelsCoordinatesFolder"];
        private string coordFileNamePxl = ConfigurationManager.AppSettings["DestinyJSONCoordinatesFile"];
        private string coordFolderGeo = ConfigurationManager.AppSettings["InitialXMLCoordinatesFolder"];
        private string coordFileNameGeo = ConfigurationManager.AppSettings["InitialXMLCoordinatesFile"];
        private string initialImagesFolder = ConfigurationManager.AppSettings["InitialImagesFolder"];
        private string chopedImagesFolder = ConfigurationManager.AppSettings["ChopedImagesFolder"];

        private int MCKa = Int32.Parse(ConfigurationManager.AppSettings["MCKa"]);
        private double MCKk = double.Parse(ConfigurationManager.AppSettings["MCKk"].Replace('.', ','));
        private double MCKb = double.Parse(ConfigurationManager.AppSettings["MCKb"].Replace('.', ','));
        private double MCKc = double.Parse(ConfigurationManager.AppSettings["MCKc"].Replace('.', ','));
        private int MCKd = Int32.Parse(ConfigurationManager.AppSettings["MCKd"]);
        private int MCMinZoom = Int32.Parse(ConfigurationManager.AppSettings["MCMinZoom"]);
        private int MCMaxZoom = Int32.Parse(ConfigurationManager.AppSettings["MCMaxZoom"]);
        private double MCLeftLat = double.Parse(ConfigurationManager.AppSettings["MCLeftLat"].Replace('.', ','));
        private double MCRightLat = double.Parse(ConfigurationManager.AppSettings["MCRightLat"].Replace('.', ','));
        private double MCTopLng = double.Parse(ConfigurationManager.AppSettings["MCTopLng"].Replace('.', ','));
        private double MCBottomLng = double.Parse(ConfigurationManager.AppSettings["MCBottomLng"].Replace('.', ','));
        private double MCDegrees = double.Parse(ConfigurationManager.AppSettings["MCDegrees"].Replace('.', ','));
        private int MCInitHPix = Int32.Parse(ConfigurationManager.AppSettings["MCInitHPix"]);
        private int MCInitVPix = Int32.Parse(ConfigurationManager.AppSettings["MCInitVPix"]);
        private int MCImgWidth = Int32.Parse(ConfigurationManager.AppSettings["MCImgWidth"]);
        private int MCImgHeight = Int32.Parse(ConfigurationManager.AppSettings["MCImgHeight"]);
        private int MCHorizontalStartTile = Int32.Parse(ConfigurationManager.AppSettings["MCHorizontalStartTile"]);
        private int MCVerticalStartTile = Int32.Parse(ConfigurationManager.AppSettings["MCVerticalStartTile"]);
        private int MCTileSize = Int32.Parse(ConfigurationManager.AppSettings["MCTileSize"]);

        public void GenerateInitialImages() 
        {
            for (int i = MCMinZoom, mult = 1; i <= MCMaxZoom; i++, mult *= 2)
            {
                List<dynamic> coords = JsonConvert.DeserializeObject<List<dynamic>>(File.ReadAllText($"{coordFolderPxl}{coordFileNamePxl}_{i}.json"));
                new ImageProccessor.MapDrawer().DrawMap(MCImgWidth * mult, MCImgHeight * mult, coords, $"{initialImagesFolder}{i}.png", i > 12);
                new ImageProccessor.MapDrawer().ChopData($"{initialImagesFolder}{i}.png", MCHorizontalStartTile, MCVerticalStartTile, MCMinZoom, i, MCTileSize, $"{chopedImagesFolder}{i}");
            }
        }

        public void GenerateInitialCoordinates()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load($@"{coordFolderGeo}{coordFileNameGeo}");
            for (int i = MCMinZoom, mult = 1; i <= MCMaxZoom; i++, mult *= 2) 
                File.WriteAllText($"{coordFolderPxl}{coordFileNamePxl}_{i}.json", XMLToJSPolyLine.parseToJson(xDoc, i, MCInitHPix * mult, MCInitVPix * mult));
        }

        public int getGlobalPixelsForLongitude(double lng, int z)
        {
            lng *= Math.PI / MCDegrees;
            double result = (MCKb - MCKa * Math.Log(Math.Tan(Math.PI / 4d + lng / 2) / Math.Pow(Math.Tan(Math.PI / 4 + Math.Asin(MCKk * Math.Sin(lng)) / 2), MCKk))) * MCKc / Math.Pow(2, MCKd - z);
            return (int) Math.Round(result);
        }

        public int getGlobalPixelsForLatitude(double lat, int z)
        {
            lat *= Math.PI / MCDegrees;
            double result = (MCKb + MCKa * lat) * MCKc / Math.Pow(2, MCKd - z);
            return (int) Math.Round(result);
        }

    }

}