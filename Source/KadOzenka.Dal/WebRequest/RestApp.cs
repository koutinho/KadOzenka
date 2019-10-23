using System;
using System.IO;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace KadOzenka.Dal.WebRequest
{

    public class RestApp
    {

        public string GetMetaInfoDataValues() =>
            new StreamReader(
                System.Net.WebRequest.Create(
                    string.Format(
                        ConfigurationManager.AppSettings["restAppMetaLink"],
                        ConfigurationManager.AppSettings["restAppLogin"],
                        ConfigurationManager.AppSettings["restAppToken"]
                    ))
                .GetResponse()
                .GetResponseStream(), Encoding.UTF8)
                .ReadToEnd();

        public string GetDataByMultipleValues(string regionId, string dealId, DateTime date1, DateTime date2) => 
            new StreamReader(
                System.Net.WebRequest.Create(
                    string.Format(
                        ConfigurationManager.AppSettings["restAppLink"],
                        ConfigurationManager.AppSettings["restAppLogin"],
                        ConfigurationManager.AppSettings["restAppToken"],
                        dealId.ToString(),
                        regionId.ToString(),
                        date1.ToString(ConfigurationManager.AppSettings["restAppTimeTmp"]),
                        date2.ToString(ConfigurationManager.AppSettings["restAppTimeTmp"])
                    ))
                .GetResponse()
                .GetResponseStream(),Encoding.UTF8)
                .ReadToEnd();

        public string FormLink(string regionId, string dealId, DateTime date1, DateTime date2) =>
            string.Format(
                ConfigurationManager.AppSettings["restAppLink"],
                ConfigurationManager.AppSettings["restAppLogin"],
                ConfigurationManager.AppSettings["restAppToken"],
                dealId.ToString(),
                regionId.ToString(),
                date1.ToString(ConfigurationManager.AppSettings["restAppTimeTmp"]),
                date2.ToString(ConfigurationManager.AppSettings["restAppTimeTmp"]));

        public string GetData(string link) => new StreamReader(System.Net.WebRequest.Create(link).GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd();

    }

}
