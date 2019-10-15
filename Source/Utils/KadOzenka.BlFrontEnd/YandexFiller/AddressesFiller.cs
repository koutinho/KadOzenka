using ObjectModel.Market;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace KadOzenka.BlFrontEnd.YandexFiller
{
    class AddressesFiller
    {

        public void GetAddresses()
        {
            int count = 10000, current_element = 0;
            List<OMCoreObject> coreObject = OMCoreObject.Where(x => true)
                                                        .Select(x => new { x.Market, x.Url, x.Address, x.Lng, x.Lat, x.Market_Code, x.ProcessType_Code/*, x.FormalizedAddress*/ })
                                                        .OrderBy(x => x.Address).Execute().Where(x => x.ProcessType_Code == ObjectModel.Directory.ProcessStep.DoNotProcessed &&
                                                                                                      x.Market_Code == ObjectModel.Directory.MarketTypes.Rosreestr).Take(count).ToList();
            List<string> addresses = new List<string>();
            Console.WriteLine(coreObject.Count);
            for (int j = 0; j < coreObject.Count; j++)
            {
                decimal lng = 0, lat = 0;
                decimal.TryParse(coreObject.ElementAt(j).Lng.ToString(), out lng);
                decimal.TryParse(coreObject.ElementAt(j).Lat.ToString(), out lat);
                string link = "https://geocode-maps.yandex.ru/1.x/?apikey={0}&geocode={1}&format={2}&results={3}&kind={4}",
                       apiKey = "723085f0-a259-4734-8105-02fbf3729f7d", geoCodeAddress = coreObject.ElementAt(j).Address, format = "json", result = "1", kind = "house",
                       market = coreObject.ElementAt(j).Market, url = coreObject.ElementAt(j).Url, address = coreObject.ElementAt(j).Address,
                       latitude = lat.ToString(CultureInfo.InvariantCulture), longitude = lng.ToString(CultureInfo.InvariantCulture),
                       //$"{longitude},{latitude}"
                       addressResult = address,
                       addressLink = string.IsNullOrEmpty(address) ? null : string.Format(link, apiKey, address, format, result, kind);
                try
                {
                    address = address.Contains(", м/м ") ? address.Substring(0, address.LastIndexOf(", м/м ")) : address;
                    address = address.Contains(", кв.") ? address.Substring(0, address.LastIndexOf(", кв.")) : address;
                    address = address.Contains(", кв ") ? address.Substring(0, address.LastIndexOf(", кв ")) : address;
                    address = address.Contains(" кв ") ? address.Substring(0, address.LastIndexOf(" кв ")) : address;
                    address = address.Contains(" кв.") ? address.Substring(0, address.LastIndexOf(" кв.")) : address;
                    address = address.Contains(", уч.") ? address.Substring(0, address.LastIndexOf(", уч.")) : address;
                    address = address.Contains(" уч.") ? address.Substring(0, address.LastIndexOf(" уч.")) : address;
                    address = address.Contains(", пом.") ? address.Substring(0, address.LastIndexOf(", пом.")) : address;
                    address = address.Contains(" пом.") ? address.Substring(0, address.LastIndexOf(" пом.")) : address;
                    address = address.Contains(", бокс") ? address.Substring(0, address.LastIndexOf(", бокс")) : address;
                    address = address.Contains(" бокс") ? address.Substring(0, address.LastIndexOf(" бокс")) : address;
                    if (!addresses.Contains(address)) addresses.Add(address);
                    //addressLink = string.IsNullOrEmpty(address) ? null : string.Format(link, apiKey, address, format, result, kind);
                    //string dataAddress = string.IsNullOrEmpty(addressLink) ? null : new StreamReader(WebRequest.Create(addressLink).GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd();
                    //Console.WriteLine(j + ": " + address);
                    //coreObject.ElementAt(j).FormalizedAddress = (string)JObject.Parse(dataAddress)
                    //    ["response"]["GeoObjectCollection"]["featureMember"][0]["GeoObject"]["metaDataProperty"]["GeocoderMetaData"]["Address"]["formatted"];
                }
                catch (Exception)
                {
                    Console.WriteLine($"Error: {addressLink}");
                }
                coreObject.ElementAt(j).ProcessType_Code = ObjectModel.Directory.ProcessStep.AddressStep;
            }

            Console.WriteLine(string.Join("\n", addresses));
            Console.WriteLine(addresses.Count);

            //coreObject.ForEach(x =>
            //{
            //    current_element++;
            //    Console.WriteLine(current_element + ":" + x.Id);
            //    x.Save();
            //});

            //dataCoordinates = string.IsNullOrEmpty(coordLink) ? null : new StreamReader(WebRequest.Create(coordLink).GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd(),
            //coordResult = "—";
            //Console.WriteLine($"Выполняется замена адресов: {(double)(j + 1) / count * 100}%");
            //Console.SetCursorPosition(0, Console.CursorTop - 1);
            //try
            //{
            //    coordResult = string.IsNullOrEmpty(dataCoordinates) ? "—" : (string)JObject.Parse(dataCoordinates)["response"]
            //                                                                                                      ["GeoObjectCollection"]
            //                                                                                                      ["featureMember"][0]
            //                                                                                                      ["GeoObject"]
            //                                                                                                      ["metaDataProperty"]
            //                                                                                                      ["GeocoderMetaData"]
            //                                                                                                      ["Address"]
            //                                                                                                      ["Components"].Count().ToString() + "<===" + coordLink;
            //                                                                                                      //["formatted"];
            //}
            //catch (Exception){}
            //}
        }

    }
}
