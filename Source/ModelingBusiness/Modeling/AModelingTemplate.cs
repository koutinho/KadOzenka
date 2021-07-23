using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommonSdks;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using ModelingBusiness.Factors;
using ModelingBusiness.Model;
using ModelingBusiness.Modeling.InputParameters;
using ModelingBusiness.Modeling.Responses;
using ModelingBusiness.Objects.Repositories;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using Serilog;

namespace ModelingBusiness.Modeling
{
    public abstract class AModelingTemplate
    {
        private static HttpClient _httpClient;
        protected ModelService ModelService { get; set; }
        protected IModelingService ModelingService { get; set; }
        protected IModelObjectsRepository ModelObjectsRepository { get; set; }
        protected ModelFactorsService ModelFactorsService { get; set; }
        protected OMQueue ProcessQueue { get; set; }
        protected ILogger Logger { get; set; }


        protected AModelingTemplate(OMQueue processQueue, ILogger logger)
        {
	        ModelObjectsRepository = new ModelObjectsRepository();
            ModelService = new ModelService();
            ModelingService = new ModelingService();
            ModelFactorsService = new ModelFactorsService();
            ProcessQueue = processQueue;
            Logger = logger;
        }

        public void Process()
        {
            try
            {
                if (_httpClient == null)
                    _httpClient = new HttpClient();

                AddLog("Начат сбор данных");
                PrepareData();
                AddLog("Закончен сбор данных");
                WorkerCommon.SetProgress(ProcessQueue, 50);

                AddLog("\nНачато формирование запроса на сервис");
                var requestForService = GetRequestForService();
                AddLog("Закончено формирование запроса на сервис");

                var responseStr = SendDataToService(requestForService).GetAwaiter().GetResult();
                var generalResponse = PreProcessServiceResponse(responseStr);
                WorkerCommon.SetProgress(ProcessQueue, 80);

                AddLog("\nНачата обработка ответа сервиса");
                ProcessServiceResponse(generalResponse);
                AddLog("Закончена обработка ответа сервиса");
                WorkerCommon.SetProgress(ProcessQueue, 100);

                SendSuccessNotification(ProcessQueue);
            }
            catch (Exception exception)
            {
	            var errorId = ErrorManager.LogError(exception);
                RollBackResult();
                SendFailNotification(ProcessQueue, exception, errorId);
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
            new NotificationSender().SendNotification(processQueue, SubjectForMessageInNotification, message);
        }

        protected virtual void SendFailNotification(OMQueue processQueue, Exception exception, long errorId)
        {
            var message = $"Операция завершена с ошибкой.\n{exception.Message}\n\nПодробнее в списке процессов.\nЖурнал: {errorId}";
            new NotificationSender().SendNotification(processQueue, SubjectForMessageInNotification, message);
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

            Logger.Information(message);
        }

        public static GeneralModelingInputParameters DeserializeInputParameters(string inputParametersXml)
        {
	        return inputParametersXml.DeserializeFromXml<GeneralModelingInputParameters>();
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
