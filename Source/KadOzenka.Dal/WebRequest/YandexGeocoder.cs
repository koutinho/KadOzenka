using System;
using System.IO;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace KadOzenka.Dal.WebRequest
{
    public class YandexGeocoder
    {

        public string GetDataByGeocode(decimal? lng, decimal? lat) =>
            new StreamReader(
                System.Net.WebRequest.Create(string.Format(ConfigurationManager.AppSettings["geocodeLink"],
                                                           ConfigurationManager.AppSettings["GeocodeTestSpecial"],
                                                           lng.ToString().Replace(",", "."),
                                                           lat.ToString().Replace(",", ".")))
                                     .GetResponse()
                                     .GetResponseStream(),
                Encoding.UTF8
            ).ReadToEnd();

        //public string GetDataByGeocode(decimal? lng, decimal? lat)
        //{
        //    Console.WriteLine(string.Format(ConfigurationManager.AppSettings["geocodeLink"],
        //                                                   ConfigurationManager.AppSettings["GeocodeTestSpecial"],
        //                                                   lng.ToString().Replace(",", "."),
        //                                                   lat.ToString().Replace(",", ".")));
        //    return new StreamReader(
        //        System.Net.WebRequest.Create(string.Format(ConfigurationManager.AppSettings["geocodeLink"],
        //                                                   ConfigurationManager.AppSettings["GeocodeTestSpecial"],
        //                                                   lng.ToString().Replace(",", "."),
        //                                                   lat.ToString().Replace(",", ".")))
        //                             .GetResponse()
        //                             .GetResponseStream(),
        //        Encoding.UTF8
        //    ).ReadToEnd();
        //}

        public string GetDataByAddress(string address) =>
            new StreamReader(System.Net.WebRequest.Create(string.Format(ConfigurationManager.AppSettings["geocodeLinkAddress"], ConfigurationManager.AppSettings["GeocodeTest0001"], address))
                                     .GetResponse()
                                     .GetResponseStream(),
                Encoding.UTF8).ReadToEnd();
    }
}
