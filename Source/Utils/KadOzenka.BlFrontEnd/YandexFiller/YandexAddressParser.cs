using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;
using System.Net;
using System.Linq;

using Newtonsoft.Json.Linq;

using ObjectModel.Market;

namespace KadOzenka.BlFrontEnd.YandexFiller
{
    class YandexAddressParser
    {

        public string GetDataByGeocode(decimal? lng, decimal? lat) =>
            new StreamReader(
                WebRequest.Create(string.Format(ConfigurationManager.AppSettings["geocodeLink"],
                                                ConfigurationManager.AppSettings["GeocodeTest00"],
                                                lng.ToString().Replace(",", "."),
                                                lat.ToString().Replace(",", ".")))
                          .GetResponse()
                          .GetResponseStream(), 
                Encoding.UTF8)
                .ReadToEnd();

        public OMYandexAddress ParseYandexAddress(string JSON)
        {
            OMYandexAddress addressElement = new OMYandexAddress();
            List<string> geoData = new List<string>();
            List<string> other = new List<string>();
            addressElement.FormalizedAddress = 
                JObject
                    .Parse(JSON)
                        ["response"]
                        ["GeoObjectCollection"]
                        ["featureMember"][0]
                        ["GeoObject"]
                        ["metaDataProperty"]
                        ["GeocoderMetaData"]
                        ["Address"]
                        ["formatted"]
                    .ToString();
            JArray components =
                JArray.Parse(
                     JObject
                        .Parse(JSON)
                            ["response"]
                            ["GeoObjectCollection"]
                            ["featureMember"][0]
                            ["GeoObject"]
                            ["metaDataProperty"]
                            ["GeocoderMetaData"]
                            ["Address"]
                            ["Components"]
                        .ToString()
                    );
            for(int i = 0; i < components.Count; i++)
            {
                string kind = components[i]["kind"].ToString();
                string name = components[i]["name"].ToString();
                kind = geoData.Count(x => x.Equals(kind)) == 0 ? kind : $"{kind}_{geoData.Count(x => x.Equals(kind)) + 1}";
                switch(kind)
                {
                    case "country":
                        addressElement.Country = name;
                        break;
                    case "province":
                        addressElement.Province = name;
                        break;
                    case "province_2":
                        addressElement.Province2 = name;
                        break;
                    case "area":
                        addressElement.Area = name;
                        break;
                    case "area_2":
                        addressElement.Area2 = name;
                        break;
                    case "locality":
                        addressElement.Locality = name;
                        break;
                    case "locality_2":
                        addressElement.Locality2 = name;
                        break;
                    case "district":
                        addressElement.District = name;
                        break;
                    case "district_2":
                        addressElement.District2 = name;
                        break;
                    case "district_3":
                        addressElement.District3 = name;
                        break;
                    case "airport":
                        addressElement.Airport = name;
                        break;
                    case "vegetation":
                        addressElement.Vegetation = name;
                        break;
                    case "route":
                        addressElement.Route = name;
                        break;
                    case "railway":
                        addressElement.RailwayStation = name;
                        break;
                    case "street":
                        addressElement.Street = name;
                        break;
                    case "house":
                        addressElement.House = name;
                        break;
                    case "other":
                        other.Add(name);
                        break;
                }
                geoData.Add(kind);
            }
            addressElement.Other = string.Join(", ", other);
            return addressElement;
            //Console.WriteLine($"Формализованный адрес: {addressElement.FormalizedAddress}\n" + 
            //                  $"Страна: {addressElement.Country}\n" + 
            //                  $"Федеральный округ: {addressElement.Province}\n" +
            //                  $"Уточнение округа: {addressElement.Province2}\n" +
            //                  $"Область: {addressElement.Area}\n" +
            //                  $"Уточнение области: {addressElement.Area2}\n" +
            //                  $"Район: {addressElement.Locality}\n" +
            //                  $"Уточнение района: {addressElement.Locality2}\n" +
            //                  $"Округ: {addressElement.District}\n" +
            //                  $"Уточнение округа: {addressElement.District2}\n" +
            //                  $"Второе уточнение округа: {addressElement.District3}\n" +
            //                  $"Аэропорт: {addressElement.Airport}\n" +
            //                  $"Ориентир: {addressElement.Vegetation}\n" +
            //                  $"Путь: {addressElement.Route}\n" +
            //                  $"ЖД станция: {addressElement.RailwayStation}\n" +
            //                  $"Улица: {addressElement.Street}\n" +
            //                  $"Дом: {addressElement.House}\n" +
            //                  $"Другое: {addressElement.Other}");
            //Console.WriteLine();
        }

    }
}
