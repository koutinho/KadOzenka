using System;
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
        public static void AddProcessToQueue(ModelingInputParameters inputParameters)
        {
            LongProcessManager.AddTaskToQueue(nameof(ModelingProcess), parameters: inputParameters.SerializeToXml());
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
                NotificationSender.SendNotification(processQueue, "Моделирование",
                    "Операция завершена с ошибкой, т.к. нет входных данных. Подробнее в списке процессов");
                return;
            }

            var strategy = GetModelingStrategy(inputParameters, processQueue);
            AddLog(processQueue, $"Запущено моделирование вида '{inputParameters.Mode.GetEnumDescription()}'");
            strategy.Process();
        }


        #region Support Methods

        private AModelingTemplate GetModelingStrategy(ModelingInputParameters inputParameters, OMQueue processQueue)
        {
            switch (inputParameters.Mode)
            {
                case ModelingMode.Training:
                    return new Training(inputParameters.InputParametersXml, processQueue);
                case ModelingMode.Prediction:
                    return new Prediction(inputParameters.InputParametersXml, processQueue);
                case ModelingMode.Correlation:
                    return new Correlation(inputParameters.InputParametersXml, processQueue);
                default:
                    throw new Exception("Не определен тип моделирования");
            }
        }

        #endregion
    }
}
