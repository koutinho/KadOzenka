using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Entities;
using Newtonsoft.Json;

namespace KadOzenka.Dal.LongProcess
{
    public class ModelingProcess : LongProcess
    {
        public const string LongProcessName = nameof(ModelingProcess);
        private static HttpClient _httpClient;

        public static void AddProcessToQueue(ModelingInputParameters inputParameters)
        {
            LongProcessManager.AddTaskToQueue(LongProcessName, parameters: inputParameters.SerializeToXml());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue,
            CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 0);
            if (_httpClient == null)
                _httpClient = new HttpClient();

            ModelingInputParameters inputParameters = null;
            if (!string.IsNullOrWhiteSpace(processQueue.Parameters))
            {
                inputParameters = processQueue.Parameters.DeserializeFromXml<ModelingInputParameters>();
            }
            if (string.IsNullOrWhiteSpace(inputParameters?.InputParametersXml))
            {
                WorkerCommon.SetMessage(processQueue, Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
                WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
                NotificationSender.SendNotification(processQueue, "Моделирование",
                    "Операция завершена с ошибкой, т.к. нет входных данных. Подробнее в списке процессов");
                return;
            }

            var strategy = GetModelingStrategy(inputParameters);
            try
            {
                strategy.PrepareData();
                WorkerCommon.SetProgress(processQueue, 50);

                var requestForService = strategy.GetRequestForService();

                var response = SendDataToService(_httpClient, strategy.GetUrl(), requestForService).GetAwaiter().GetResult();
                response = PreProcessServiceResponse(response);
                WorkerCommon.SetProgress(processQueue, 80);

                strategy.ProcessServiceAnswer(response);
                WorkerCommon.SetProgress(processQueue, 100);

                strategy.SendSuccessNotification(processQueue);
            }
            catch (Exception)
            {
                strategy.RollBackResult();
                strategy.SendFailNotification(processQueue);
                throw;
            }
        }


        #region Support Methods

        private AModelingStrategy GetModelingStrategy(ModelingInputParameters inputParameters)
        {
            switch (inputParameters.Mode)
            {
                case ModelingMode.Training:
                    return new TrainingStrategy(inputParameters.InputParametersXml);
                case ModelingMode.Prediction:
                    return new PredictionStrategy(inputParameters.InputParametersXml);
                case ModelingMode.Correlation:
                    return new CorrelationStrategy(inputParameters.InputParametersXml);
                default:
                    throw new Exception("Не определен тип моделирования");
            }
        }

        protected async Task<string> SendDataToService(HttpClient httpClient, string url, object data)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new Exception("Не найден URL для сервиса");

            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(data));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, httpContent);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        protected string PreProcessServiceResponse(string responseContentStr)
        {
            //обрабатываем кириллицу
            responseContentStr = Regex.Replace(responseContentStr, @"\\u([0-9A-Fa-f]{4})", m => ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString());
            if (string.IsNullOrWhiteSpace(responseContentStr))
                throw new Exception("Сервис для моделирования вернул пустой ответ");

            //TODO переделаем на обработку json-объекта ошибки, после реализации в сервисе
            if (responseContentStr.ToLower().Contains("message"))
                throw new Exception("Сервис для моделирования вернул ошибку: " + responseContentStr);

            return responseContentStr;
        }

        #endregion
    }
}
