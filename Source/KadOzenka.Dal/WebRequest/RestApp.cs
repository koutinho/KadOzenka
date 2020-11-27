using System;
using System.IO;
using System.Text;
using System.Configuration;
using System.Collections.Generic;
using Serilog;

namespace KadOzenka.Dal.WebRequest
{

    public class RestApp
    {

        private string _metaInfoLink = ConfigurationManager.AppSettings["restAppMetaLink"];
        private string _restAppCIANLink = ConfigurationManager.AppSettings["restAppCIANLink"];
        private string _restAppAvitoLink = ConfigurationManager.AppSettings["restAppAvitoLink"];
        private string _restAppTimeTemplate = ConfigurationManager.AppSettings["restAppTimeTmp"];
        static readonly ILogger _log = Log.ForContext<RestApp>();

        public string GetMetaInfoDataValues(string login, string token) => 
            new StreamReader(System.Net.WebRequest.Create(string.Format(_metaInfoLink, login, token)).GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd();

        public string GetCIANDataByMultipleValues(string link)
        {
            string result = string.Empty;
            try
            {
                _log
                    .ForContext("Method", "GetCIANDataByMultipleValues")
                    .Debug("Попытка получения данных от RestApp");
                result = new StreamReader(System.Net.WebRequest.Create(link).GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd();
            }
            catch (Exception ex) 
            {
                _log
                    .ForContext("Method", "GetCIANDataByMultipleValues")
                    .Error(ex, "Ошибка получения ответа от RestApp");
            }
            return result;
        }

        public string GetAvitoDataByMultipleValues(string regionId, string categoryId, DateTime date1, DateTime date2, string login, string token) => 
            new StreamReader(
                System.Net.WebRequest.Create(string.Format(_restAppAvitoLink, login, token, categoryId, regionId, date1.ToString(_restAppTimeTemplate), date2.ToString(_restAppTimeTemplate)))
                .GetResponse()
                .GetResponseStream(), Encoding.UTF8)
                .ReadToEnd();

        public string FormCianLink(string regionId, string dealId, DateTime dateStart, DateTime dateEnd, string login, string token) =>
            string.Format(_restAppCIANLink, login, token, dealId.ToString(), regionId.ToString(), dateStart.ToString(_restAppTimeTemplate), dateEnd.ToString(_restAppTimeTemplate));

    }

}
