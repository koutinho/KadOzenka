using System;
using System.Linq;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using Core.Shared.Extensions;
using KadOzenka.Dal.Correction;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.ScoreCommon;
using ObjectModel.Directory;
using ObjectModel.Directory.MarketObjects;
using ObjectModel.Market;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.LongProcess
{
	public class ModelingProcess : LongProcess
	{
		public const string LongProcessName = nameof(ModelingProcess);

		public static void AddProcessToQueue(long modelId)
		{
			LongProcessManager.AddTaskToQueue(LongProcessName, objectId: modelId, registerId: OMModelingModel.GetRegisterId());
		}

        //TODO
		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
            //WorkerCommon.SetProgress(processQueue, 0);
            //if (!processQueue.ObjectId.HasValue)
            //{
            //	WorkerCommon.SetMessage(processQueue, Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
            //	WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
            //	return;
            //}

            var isTrainingMode = false;
            if (!string.IsNullOrWhiteSpace(processQueue.Parameters))
            {
                var request = processQueue.Parameters.DeserializeFromXml<ModelingRequest>();
                isTrainingMode = request.IsTrainingMode;
            }

            var modelingService = new ModelingService(new ScoreCommonService());
            var modelId = processQueue.ObjectId.Value;

            if (isTrainingMode)
            {
                var coefficients = TrainModel(modelingService, modelId);
            }
            else
            {
                var coefficients = CalculateModel(modelingService, modelId);
            }

            //TODO send coefficients to API-service

            //WorkerCommon.SetProgress(processQueue, 100);
        }


        #region Support Methods

        public TrainingSet TrainModel(ModelingService modelingService, long modelId)
        {
            modelingService.CreateObjectsForModel(modelId);
            return modelingService.GetCoefficientsToTrainModel(modelId);
        }


        public CalculationSet CalculateModel(ModelingService modelingService, long modelId)
        {
           return modelingService.GetCoefficientsToCalculateModel(modelId);
        }

        #endregion
    }
}
