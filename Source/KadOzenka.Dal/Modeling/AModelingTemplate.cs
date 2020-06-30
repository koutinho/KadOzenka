using System;
using System.Text.RegularExpressions;
using Core.Register.LongProcessManagment;
using KadOzenka.Dal.Modeling.Entities;
using KadOzenka.Dal.ScoreCommon;
using ObjectModel.Core.LongProcess;
using ObjectModel.Modeling;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KadOzenka.Dal.LongProcess;
using Newtonsoft.Json;

namespace KadOzenka.Dal.Modeling
{
    public abstract class AModelingTemplate
    {
        private static HttpClient _httpClient;
        protected ModelingService ModelingService { get; set; }
        protected OMQueue ProcessQueue { get; set; }


        protected AModelingTemplate(OMQueue processQueue)
        {
            ModelingService = new ModelingService(new ScoreCommonService());
            ProcessQueue = processQueue;
        }

        public void Process()
        {
            try
            {
                if (_httpClient == null)
                    _httpClient = new HttpClient();

                PrepareData();
                WorkerCommon.SetProgress(ProcessQueue, 50);

                var requestForService = GetRequestForService();

                var responseStr = SendDataToService(requestForService).GetAwaiter().GetResult();

                var generalResponse = PreProcessServiceResponse(responseStr);
                WorkerCommon.SetProgress(ProcessQueue, 80);

                ProcessServiceResponse(generalResponse);
                WorkerCommon.SetProgress(ProcessQueue, 100);

                SendSuccessNotification(ProcessQueue);
            }
            catch (Exception exception)
            {
                RollBackResult();
                SendFailNotification(ProcessQueue, exception);
                throw;
            }
        }


        protected abstract string SubjectForMessageInNotification { get; }
        protected abstract string GetUrl();

        protected abstract void PrepareData();

        protected abstract object GetRequestForService();

        protected abstract void ProcessServiceResponse(GeneralResponse responseFromService);

        protected virtual void RollBackResult()
        {

        }

        protected virtual void SendSuccessNotification(OMQueue processQueue)
        {
            var message = "Операция успешно завершена";
            NotificationSender.SendNotification(processQueue, SubjectForMessageInNotification, message);
        }

        protected virtual void SendFailNotification(OMQueue processQueue, Exception exception)
        {
            var message = $"Операция завершена с ошибкой: {exception.Message}. \nПодробнее в списке процессов.";
            NotificationSender.SendNotification(processQueue, SubjectForMessageInNotification, message);
        }


        protected OMModelingModel GetModel(long modelId)
        {
            var model = OMModelingModel.Where(x => x.Id == modelId).SelectAll().ExecuteFirstOrDefault();
            if (model == null)
                throw new Exception($"Не найдена модель с Id='{modelId}'");

            return model;
        }

        protected string PreProcessAttributeName(string name)
        {
            var pattern = new Regex("[() ]");
            return pattern.Replace(name, string.Empty);
        }

        protected void AddLog(string message, bool withNewLine = true)
        {
            var previousLog = string.IsNullOrWhiteSpace(ProcessQueue.Log) ? string.Empty : ProcessQueue.Log;

            var newLog = withNewLine && !string.IsNullOrWhiteSpace(previousLog)
                ? previousLog + Environment.NewLine + message
                : previousLog + message;

            ProcessQueue.Log = newLog;
            ProcessQueue.Save();
        }


        #region Support Methods

        private async Task<string> SendDataToService(object data)
        {
            var url = GetUrl();

            if (string.IsNullOrWhiteSpace(url))
                throw new Exception("Не найден URL для сервиса");

            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(data));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, httpContent);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        private GeneralResponse PreProcessServiceResponse(string responseContentStr)
        {
            //обрабатываем кириллицу
            responseContentStr = Regex.Replace(responseContentStr, @"\\u([0-9A-Fa-f]{4})", m => ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString());
            if (string.IsNullOrWhiteSpace(responseContentStr))
                throw new Exception("Сервис для моделирования вернул пустой ответ");

            var json = JsonConvert.DeserializeObject<GeneralResponse>(responseContentStr);
            if (!string.IsNullOrWhiteSpace(json.ErrorMessage))
                throw new Exception($"Сервис для моделирования вернул ошибку: {json.ErrorMessage}.\n{json.InnerError}");

            return json;
        }

        #endregion
    }
}
