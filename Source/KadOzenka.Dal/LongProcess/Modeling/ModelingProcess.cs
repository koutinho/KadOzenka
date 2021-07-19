using System;
using System.Threading;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.LongProcess.Modeling.InputParameters;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Modeling;
using ModelingBusiness.Modeling.Entities;
using ObjectModel.Core.LongProcess;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Modeling
{
    public class ModelingProcess : LongProcess
    {
	    private readonly ILogger _log = Log.ForContext<ModelingProcess>();

        public static void AddProcessToQueue(ModelingInputParameters inputParameters)
        {
            LongProcessManager.AddTaskToQueue(nameof(ModelingProcess), parameters: inputParameters.SerializeToXml());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue,
            CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 0);
            _log.Information("Старт фонового процесса для Моделирования {InputParameters}", processQueue.Parameters);

            ModelingInputParameters inputParameters = null;
            if (!string.IsNullOrWhiteSpace(processQueue.Parameters))
            {
                inputParameters = processQueue.Parameters.DeserializeFromXml<ModelingInputParameters>();
            }
            if (string.IsNullOrWhiteSpace(inputParameters?.InputParametersXml))
            {
                WorkerCommon.SetMessage(processQueue, Common.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
                WorkerCommon.SetProgress(processQueue, Common.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
                NotificationSender.SendNotification(processQueue, "Моделирование",
                    "Операция завершена с ошибкой, т.к. нет входных данных. Подробнее в списке процессов");
                return;
            }

            var strategy = GetModelingStrategy(inputParameters, processQueue);
            AddLog(processQueue, $"Запущено моделирование вида '{inputParameters.Mode.GetEnumDescription()}'", logger: _log);
            strategy.Process();

            _log.Information("Закончен фоновый процесс Моделирования");
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
