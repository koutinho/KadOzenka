using System;
using System.IO;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace KadOzenka.Dal.WebRequest
{

    public class RestApp
    {

        private string login = ConfigurationManager.AppSettings["restAppLogin010"];
        private string token = ConfigurationManager.AppSettings["restAppToken010"];
        private string metaInfoLink = ConfigurationManager.AppSettings["restAppMetaLink"];
        private string restAppCIANLink = ConfigurationManager.AppSettings["restAppCIANLink"];
        private string restAppAvitoLink = ConfigurationManager.AppSettings["restAppAvitoLink"];
        private string restAppTimeTemplate = ConfigurationManager.AppSettings["restAppTimeTmp"];

        public string GetMetaInfoDataValues() => 
            new StreamReader(System.Net.WebRequest.Create(string.Format(metaInfoLink, login, token)).GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd();

        public string GetCIANDataByMultipleValues(string regionId, string dealId, DateTime date1, DateTime date2) => 
            new StreamReader(
                System.Net.WebRequest.Create(string.Format(restAppCIANLink, login, token, dealId.ToString(), regionId.ToString(), date1.ToString(restAppTimeTemplate), date2.ToString(restAppTimeTemplate)))
                .GetResponse()
                .GetResponseStream(),Encoding.UTF8)
                .ReadToEnd();

        public string GetAvitoDataByMultipleValues(string regionId, string categoryId, DateTime date1, DateTime date2) => 
            new StreamReader(
                System.Net.WebRequest.Create(string.Format(restAppAvitoLink, login, token, categoryId, regionId, date1.ToString(restAppTimeTemplate), date2.ToString(restAppTimeTemplate)))
                .GetResponse()
                .GetResponseStream(), Encoding.UTF8)
                .ReadToEnd();

    }

}
