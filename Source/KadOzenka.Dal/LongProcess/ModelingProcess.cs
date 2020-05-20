using System;
using System.Net.Http;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Entities;

namespace KadOzenka.Dal.LongProcess
{
    public class ModelingProcess : LongProcess
    {
        public const string LongProcessName = nameof(ModelingProcess);
        private static HttpClient _httpClient = new HttpClient();

        public static void AddProcessToQueue(ModelingInputParameters inputParameters)
        {
            LongProcessManager.AddTaskToQueue(LongProcessName, parameters: inputParameters.SerializeToXml());
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
            if (string.IsNullOrWhiteSpace(inputParameters?.InputParametersXml))
            {
                WorkerCommon.SetMessage(processQueue, Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
                WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
                NotificationSender.SendNotification(processQueue, "Моделирование", "Операция завершена с ошибкой. Подробнее в списке процессов");
                return;
            }

            var strategy = GetModelingStrategy(inputParameters);
            try
            {
                strategy.PrepareData();
                WorkerCommon.SetProgress(processQueue, 50);

                var requestForService = strategy.GetRequestForService();

                var response = SendDataToService(_httpClient, strategy.Url, requestForService).GetAwaiter().GetResult();
                response = PreProcessServiceResponse(response);
                WorkerCommon.SetProgress(processQueue, 80);

                strategy.SaveResult(response);

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
            switch (inputParameters.ModelingType)
            {
                case ModelingType.Training:
                    return new TrainingStrategy(inputParameters.InputParametersXml);
                case ModelingType.Prediction:
                    return new PredictionStrategy(inputParameters.InputParametersXml);
                default:
                    throw new Exception("Не определен тип моделирования");
            }
        }

        #endregion
    }
}
