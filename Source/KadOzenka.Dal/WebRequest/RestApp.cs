using System;
using System.IO;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace KadOzenka.Dal.WebRequest
{

    public class RestApp
    {

        private string login = ConfigurationManager.AppSettings["restAppLogin002"];
        private string token = ConfigurationManager.AppSettings["restAppToken002"];
        private string metaInfoLink = ConfigurationManager.AppSettings["restAppMetaLink"];
        private string restAppLink = ConfigurationManager.AppSettings["restAppLink"];
        private string restAppTimeTemplate = ConfigurationManager.AppSettings["restAppTimeTmp"];

        public string GetMetaInfoDataValues() => 
            new StreamReader(System.Net.WebRequest.Create(string.Format(metaInfoLink, login, token)).GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd();

        public string GetDataByMultipleValues(string regionId, string dealId, DateTime date1, DateTime date2) => 
            new StreamReader(
                System.Net.WebRequest.Create(string.Format(restAppLink, login, token, dealId.ToString(), regionId.ToString(), date1.ToString(restAppTimeTemplate), date2.ToString(restAppTimeTemplate)))
                .GetResponse()
                .GetResponseStream(),Encoding.UTF8)
                .ReadToEnd();

        public string FormLink(string regionId, string dealId, DateTime date1, DateTime date2) =>
            string.Format(restAppLink, login, token, dealId.ToString(), regionId.ToString(), date1.ToString(restAppTimeTemplate), date2.ToString(restAppTimeTemplate));

        public string GetData(string link) => new StreamReader(System.Net.WebRequest.Create(link).GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd();

    }

}
