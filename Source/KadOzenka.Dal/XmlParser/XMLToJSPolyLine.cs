using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace KadOzenka.Dal.XmlParser
{

    public class XMLToJSPolyLine
    {
        public static void parseDistricts()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(@"C:\Users\silanov\Documents\Дженикс\ЦИПЖС деление по кварталам, районам, округам\Геодезические координаты в XML\Данные\Кварталы.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            List<List<string>> allPolylines = new List<List<string>>();
            string result = string.Empty;
            foreach (XmlNode xnode in xRoot.SelectNodes("Objects/Object/Conturs/Contur/Points"))
            {
                allPolylines.Add(new List<string>());
                foreach (XmlNode point in xnode.SelectNodes("Point")) allPolylines.LastOrDefault().Add($"[{point.SelectNodes("Y")[0].InnerText}, {point.SelectNodes("X")[0].InnerText}]");
            }
            for(int i = 0; i < allPolylines.Count; i++)
                result += $"CLD.push(new ymaps.Polygon([[{string.Join(", ", allPolylines.ElementAt(i))}]], {{hintContent: \"Многоугольник\"}}, {{ fillColor: '#00FF0040', strokeColor: '#fe1616', strokeWidth: 1 }}));\n";
            File.WriteAllText(@"C:\Users\silanov\Documents\Дженикс\ЦИПЖС деление по кварталам, районам, округам\Геодезические координаты в XML\В текстовом виде\Кварталы.txt", result);
        }

    }

}
