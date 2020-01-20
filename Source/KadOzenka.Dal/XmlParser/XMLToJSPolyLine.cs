using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace KadOzenka.Dal.XmlParser
{

    public class XMLToJSPolyLine
    {

        private class Properties
        {
            public string description { get; set; } = "Многоугольник";
        }

        private class ParsedData
        {
            public string Number { get; set; }
            public List<List<List<int>>> Coordinates { get; set; }
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

        private class Feature 
        {
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
            xDoc.Load($@"{ConfigurationManager.AppSettings["InitialXMLCoordinatesFolder"]}{ConfigurationManager.AppSettings["InitialXMLCoordinatesFile"]}");
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
            File.WriteAllText($@"{ConfigurationManager.AppSettings["DestinyJSONCoordinatesFolder"]}{ConfigurationManager.AppSettings["DestinyJSONCoordinatesFile"]}",
                              JsonConvert.SerializeObject(geoObjects, Newtonsoft.Json.Formatting.Indented));
            File.WriteAllText($@"{ConfigurationManager.AppSettings["DestinyJSONCoordinatesFolder"]}{ConfigurationManager.AppSettings["DestinyJSONMINCoordinatesFile"]}",
                              JsonConvert.SerializeObject(geoObjects, Newtonsoft.Json.Formatting.None));
        }

        public static string parseToJson(XmlDocument xDoc, int zoom, int deltaX, int deltaY)
        {
            Console.WriteLine($"{zoom}\t{deltaX}\t{deltaY}");
            XmlElement xRoot = xDoc.DocumentElement;
            List<ParsedData> result = new List<ParsedData>();
            int i = 0;
            CoordinatesConverter converter = new CoordinatesConverter();
            foreach (XmlNode xnode in xRoot.SelectNodes("Objects/Object"))
            {
                result.Add(new ParsedData());
                result.ElementAt(i).Number = xnode.SelectNodes("Number")[0].InnerText;
                result.ElementAt(i).Coordinates = new List<List<List<int>>>();
                XmlNodeList nodeList = xnode.SelectNodes("Conturs");
                foreach(XmlNode conturs in nodeList)
                {
                    XmlNodeList contursList = conturs.SelectNodes("Contur");
                    int j = 0;
                    foreach(XmlNode contur in contursList)
                    {
                        result.ElementAt(i).Coordinates.Add(new List<List<int>>());
                        XmlNodeList points = contur.SelectNodes("Points/Point");
                        int k = 0;
                        foreach (XmlNode point in points)
                        {
                            double x = double.Parse(point.SelectNodes("X")[0].InnerText.Replace('.', ',')), y = double.Parse(point.SelectNodes("Y")[0].InnerText.Replace('.', ','));
                            result.ElementAt(i).Coordinates.ElementAt(j).Add(new List<int>());
                            result.ElementAt(i).Coordinates.ElementAt(j).ElementAt(k).Add(converter.getGlobalPixelsForLatitude(x, zoom) - deltaX);
                            result.ElementAt(i).Coordinates.ElementAt(j).ElementAt(k).Add(converter.getGlobalPixelsForLongitude(y, zoom) - deltaY);
                            k++;
                        }
                        j++;
                    }
                }
                i++;
            }
            return JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
        }

    }

}
