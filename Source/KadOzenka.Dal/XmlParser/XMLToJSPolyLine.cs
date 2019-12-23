using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace KadOzenka.Dal.XmlParser
{

    public class XMLToJSPolyLine
    {

        private class Properties
        {
            public string description { get; set; } = "Многоугольник";
        }

        private class Options
        {
            public bool fill { get; set; } = true;
            public string fillColor { get; set; } = "#00FF00";
            public double fillOpacity { get; set; } = 0.4;
            public string strokeColor { get; set; } = "#FE1616";
            public string strokeWidth { get; set; } = "1";
            public double strokeOpacity { get; set; } = 0.9;
        }

        private class Geometry
        {
            public Geometry() => coordinates.Add(new List<List<double>>());
            public string type { get; set; } = "Polygon";
            public List<List<List<double>>> coordinates { get; set; } = new List<List<List<double>>>();
        }

        private class Feature {
            public string type { get; set; } = "Feature";
            public int id { get; set; }
            public Geometry geometry { get; set; } = new Geometry();
            public Properties properties { get; set; } = new Properties();
            public Options options { get; set; } = new Options();
        }

        private class GeoObjects
        {
            public string type { get; set; } = "FeatureCollection";
            public List<Feature> features { get; set; } = new List<Feature>();
        }

        public static void parseDistricts()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(@"C:\Users\silanov\Documents\Дженикс\ЦИПЖС деление по кварталам, районам, округам\Геодезические координаты в XML\Данные\Кварталы.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            GeoObjects geoObjects = new GeoObjects();
            int i = 0;
            foreach (XmlNode xnode in xRoot.SelectNodes("Objects/Object/Conturs/Contur/Points"))
            {
                Feature feature = new Feature();
                feature.id = i;
                geoObjects.features.Add(feature);
                foreach (XmlNode point in xnode.SelectNodes("Point"))
                {
                    List<double> data = new List<double>();
                    data.Add(double.Parse(point.SelectNodes("Y")[0].InnerText.Replace('.', ',')));
                    data.Add(double.Parse(point.SelectNodes("X")[0].InnerText.Replace('.', ',')));
                    feature.geometry.coordinates.FirstOrDefault().Add(data);
                }
                i++;
            }
            File.WriteAllText(@"C:\Users\silanov\Documents\Дженикс\ЦИПЖС деление по кварталам, районам, округам\Геодезические координаты в XML\JSON\quartal.json", JsonConvert.SerializeObject(geoObjects, Newtonsoft.Json.Formatting.Indented));
            File.WriteAllText(@"C:\Users\silanov\Documents\Дженикс\ЦИПЖС деление по кварталам, районам, округам\Геодезические координаты в XML\JSON\quartal.min.json", JsonConvert.SerializeObject(geoObjects, Newtonsoft.Json.Formatting.None));
            //List<List<string>> allPolylines = new List<List<string>>();
            //string result = string.Empty;
            //foreach (XmlNode xnode in xRoot.SelectNodes("Objects/Object/Conturs/Contur/Points"))
            //{
            //    allPolylines.Add(new List<string>());
            //    foreach (XmlNode point in xnode.SelectNodes("Point")) allPolylines.LastOrDefault().Add($"[{point.SelectNodes("Y")[0].InnerText}, {point.SelectNodes("X")[0].InnerText}]");
            //}
            //for(int i = 0; i < allPolylines.Count; i++)
            //    result += $"CLD.push(new ymaps.Polygon([[{string.Join(", ", allPolylines.ElementAt(i))}]], {{hintContent: \"Многоугольник\"}}, {{ fillColor: '#00FF0040', strokeColor: '#fe1616', strokeWidth: 1 }}));\n";
            //File.WriteAllText(@"C:\Users\silanov\Documents\Дженикс\ЦИПЖС деление по кварталам, районам, округам\Геодезические координаты в XML\В текстовом виде\Кварталы.txt", result);
        }

    }

}
