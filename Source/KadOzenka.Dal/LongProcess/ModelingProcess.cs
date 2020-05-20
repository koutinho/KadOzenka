using System;
using System.Net.Http;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.LongProcess
{
    public class ModelingProcess : LongProcess
    {
        public const string LongProcessName = nameof(ModelingProcess);
        private static HttpClient _httpClient = new HttpClient();

        public static void AddProcessToQueue(long modelId, ModelingInputParameters inputParameters)
        {
            LongProcessManager.AddTaskToQueue(LongProcessName, objectId: modelId,
                registerId: OMModelingModel.GetRegisterId(), parameters: inputParameters.SerializeToXml());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue,
            CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 0);

            ModelingInputParameters inputParameters = null;
            if (!string.IsNullOrWhiteSpace(processQueue.Parameters))
            {
                inputParameters = processQueue.Parameters.DeserializeFromXml<ModelingInputParameters>();
            }
            if (inputParameters == null || inputParameters.ModelId == 0)
            {
                WorkerCommon.SetMessage(processQueue, Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
                WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);

                NotificationSender.SendNotification(processQueue, "Моделирование",
                    "Операция завершена с ошибкой. Подробнее в списке процессов");

                return;
            }

            var model = GetModel(inputParameters.ModelId);

            try
            {
                var strategy = inputParameters.IsTrainingMode
                    ? (AModelingStrategy)new TrainingStrategy(inputParameters, model)
                    : new PredictionStrategy(inputParameters, model);

                strategy.PrepareData();
                WorkerCommon.SetProgress(processQueue, 50);

                var requestForService = strategy.GetRequestForService();

                var response = SendDataToService(_httpClient, strategy.Url, requestForService).GetAwaiter().GetResult();
                response = PreProcessServiceResponse(response);

                strategy.SaveResult(response);

                WorkerCommon.SetProgress(processQueue, 100);

                SendNotification(inputParameters.IsTrainingMode, model.Name, "Операция успешно завершена", processQueue);
            }
            catch (Exception)
            {
                if (inputParameters.IsTrainingMode)
                {
                    model.WasTrained = false;
                    model.LinearTrainingResult = null;
                    model.ExponentialTrainingResult = null;
                    model.MultiplicativeTrainingResult = null;
                    model.Save();
                }

                SendNotification(inputParameters.IsTrainingMode, model.Name,
                    "Операция завершена с ошибкой. Подробнее в списке процессов", processQueue);

                throw;
            }
        }


        #region Support Methods

        private OMModelingModel GetModel(long modelId)
        {
            var model = OMModelingModel.Where(x => x.Id == modelId).SelectAll().ExecuteFirstOrDefault();
            if (model == null)
                throw new Exception($"Не найдена модель с Id='{modelId}'");

            return model;
        }

        private void SendNotification(bool isTrainingMode, string modelName, string message, OMQueue processQueue)
        {
            var subject = isTrainingMode
                ? $"Процесс обучения модели '{modelName}'"
                : $"Процесс прогнозирования цены для модели '{modelName}'";

            NotificationSender.SendNotification(processQueue, subject, message);
        }

        #endregion
    }
}
