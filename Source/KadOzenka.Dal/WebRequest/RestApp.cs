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

        private string metaInfoLink = ConfigurationManager.AppSettings["restAppMetaLink"];
        private string restAppCIANLink = ConfigurationManager.AppSettings["restAppCIANLink"];
        private string restAppAvitoLink = ConfigurationManager.AppSettings["restAppAvitoLink"];
        private string restAppTimeTemplate = ConfigurationManager.AppSettings["restAppTimeTmp"];
        static readonly ILogger _log = Log.ForContext<RestApp>();

        public string GetMetaInfoDataValues(string login, string token) => 
            new StreamReader(System.Net.WebRequest.Create(string.Format(metaInfoLink, login, token)).GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd();

        public string GetCIANDataByMultipleValues(string regionId, string dealId, DateTime date1, DateTime date2, string login, string token)
        {
            _log
                .ForContext("Method", "GetCIANDataByMultipleValues")
                .Debug("Попытка получения данных от RestApp");
            string result = string.Empty;
            try
            {
                result = new StreamReader(
                        System.Net.WebRequest.Create(string.Format(restAppCIANLink, login, token, dealId.ToString(), regionId.ToString(), date1.ToString(restAppTimeTemplate), date2.ToString(restAppTimeTemplate)))
                        .GetResponse()
                        .GetResponseStream(), Encoding.UTF8)
                    .ReadToEnd();
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
                System.Net.WebRequest.Create(string.Format(restAppAvitoLink, login, token, categoryId, regionId, date1.ToString(restAppTimeTemplate), date2.ToString(restAppTimeTemplate)))
                .GetResponse()
                .GetResponseStream(), Encoding.UTF8)
                .ReadToEnd();

    }

}
