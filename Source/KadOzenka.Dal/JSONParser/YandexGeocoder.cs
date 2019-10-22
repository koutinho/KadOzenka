using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

using ObjectModel.Market;
using Newtonsoft.Json.Linq;

namespace KadOzenka.Dal.JSONParser
{

    public class YandexGeocoder
    {

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
            for (int i = 0; i < components.Count; i++)
            {
                string kind = components[i]["kind"].ToString();
                string name = components[i]["name"].ToString();
                kind = geoData.Count(x => x.Equals(kind)) == 0 ? kind : $"{kind}_{geoData.Count(x => x.Equals(kind)) + 1}";
                switch (kind)
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
        }

    }

}
