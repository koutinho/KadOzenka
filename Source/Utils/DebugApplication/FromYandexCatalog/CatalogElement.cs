using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObjectModel.Market;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace DebugApplication.FromYandexCatalog
{
    class CatalogElement
    {
        public string cadastralNumber;
        public string address;
        public string link;
        public OMYandexAddress yandexAddress;

        private string linkTemplate = "https://geocode-maps.yandex.ru/1.x/?apikey={0}&geocode={1}&format={2}&results={3}&kind={4}";
        private string format = "json";
        private string result = "1";
        private string kind = "house";
        //private string apiKey = "b621eeb4-7f0b-45c0-84a9-7f62d6db8a1f"; //GeocodeTest00
        //private string apiKey = "ae344970-4b37-4c48-95d6-873dabf6c900"; //GeocodeTest01
        //private string apiKey = "bc933605-bb28-452e-82a7-cb61bb506cb3"; //GeocodeTest02
        //private string apiKey = "dfe89520-4d19-40c9-84f7-8554c238fdba"; //GeocodeTest03
        //private string apiKey = "295ca538-9978-4e0c-9901-1b1b254ae2ec"; //GeocodeTest04
        //private string apiKey = "9b3fb691-b0b0-4684-ba01-dfd3a3e994ca"; //GeocodeTest05
        //private string apiKey = "28ae496e-350b-4856-abb5-f09e31cc81ec"; //GeocodeTest06
        //private string apiKey = "770affe1-de6b-4d4b-89c1-19228db21860"; //GeocodeTest07
        //private string apiKey = "aa7ca4d5-9049-4336-a741-286db75c6629"; //GeocodeTest08
        //private string apiKey = "54cc5da5-7354-417c-ab19-9b31d9b5f407"; //GeocodeTest09
        //private string apiKey = "23236621-9c12-48f4-bd15-39bed8cd9866"; //GeocodeTest10
        //private string apiKey = "dc262fd1-a5d4-4795-a4dd-2a7c38e270e2"; //GeocodeTest11
        //private string apiKey = "a5634cb4-dfa1-46f5-8a9a-1f9bf289708c"; //GeocodeTest12
        //private string apiKey = "3a018850-c518-4da4-a57e-a7e7f895673c"; //GeocodeTest13
        //private string apiKey = "878a0296-414c-44a1-8225-633129abc884"; //GeocodeTest14
        private string apiKey = "b2af6865-44b7-4bde-b357-ef9e1979e924"; //GeocodeTest15
        //private string apiKey = "237b5425-33c5-46ba-862b-216d82205f4a"; //GeocodeTest16

        public CatalogElement(string cadastralNumber, string address)
        {
            this.cadastralNumber = cadastralNumber;
            this.address = address;
            this.link = string.Format(linkTemplate, apiKey, this.address, format, result, kind);
        }

        public void GetYandexAddress() => yandexAddress = ParseGeoDataFromJSON(SendRequest());

        public void SaveDataToPostgres()
        {
            List<OMYandexAddress> addresses = OMYandexAddress.Where(x => x.FormalizedAddress == yandexAddress.FormalizedAddress && x.CadastralNumber == cadastralNumber).Select(x => x.Id).Execute();
            if (addresses.Count == 0) yandexAddress.Save();
        }

        private string SendRequest() => new StreamReader(WebRequest.Create(link).GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd();

        private OMYandexAddress ParseGeoDataFromJSON(string geoData)
        {
            OMYandexAddress result = new OMYandexAddress();
            List<string> addressComponentsData = new List<string>(), otherData = new List<string>();
            string formalizedAddress = 
                (string)JObject.Parse(geoData)["response"]["GeoObjectCollection"]["featureMember"][0]["GeoObject"]["metaDataProperty"]["GeocoderMetaData"]["Address"]["formatted"];
            JArray AddressComponents =
                JArray.Parse(
                    JsonConvert.SerializeObject(
                        JObject.Parse(geoData)["response"]["GeoObjectCollection"]["featureMember"][0]["GeoObject"]["metaDataProperty"]["GeocoderMetaData"]["Address"]["Components"]
                    )
                );
            foreach (JToken addressElement in AddressComponents)
            {
                string value = addressElement["kind"].ToString();
                string element_value = addressComponentsData.Contains(value) ? $"{value}_{(addressComponentsData.Count(x => x == value) + 1)}" : value;
                switch (element_value)
                {
                    case "country":
                        result.Country = addressElement["name"].ToString();
                        break;
                    case "province":
                        result.Province = addressElement["name"].ToString();
                        break;
                    case "province_2":
                        result.Province2 = addressElement["name"].ToString();
                        break;
                    case "area":
                        result.Area = addressElement["name"].ToString();
                        break;
                    case "area_2":
                        result.Area2 = addressElement["name"].ToString();
                        break;
                    case "locality":
                        result.Locality = addressElement["name"].ToString();
                        break;
                    case "locality_2":
                        result.Locality2 = addressElement["name"].ToString();
                        break;
                    case "district":
                        result.District = addressElement["name"].ToString();
                        break;
                    case "district_2":
                        result.District2 = addressElement["name"].ToString();
                        break;
                    case "district_3":
                        result.District3 = addressElement["name"].ToString();
                        break;
                    case "airport":
                        result.Airport = addressElement["name"].ToString();
                        break;
                    case "vegetation":
                        result.Vegetation = addressElement["name"].ToString();
                        break;
                    case "route":
                        result.Route = addressElement["name"].ToString();
                        break;
                    case "railway_station":
                        result.RailwayStation = addressElement["name"].ToString();
                        break;
                    case "street":
                        result.Street = addressElement["name"].ToString();
                        break;
                    case "house":
                        result.House = addressElement["name"].ToString();
                        break;
                    default:
                        otherData.Add(addressElement["name"].ToString());
                        break;
                }
                addressComponentsData.Add(value);
            }
            result.FormalizedAddress = formalizedAddress;
            result.CadastralNumber = cadastralNumber;
            result.Other = string.Join(", ", otherData);
            return result;
        }

    }
}