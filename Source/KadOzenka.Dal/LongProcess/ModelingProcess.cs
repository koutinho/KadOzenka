using System;
using System.Linq;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using KadOzenka.Dal.Correction;
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

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			//WorkerCommon.SetProgress(processQueue, 0);
			//if (!processQueue.ObjectId.HasValue)
			//{
			//	WorkerCommon.SetMessage(processQueue, Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
			//	WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
			//	return;
			//}

			var modelingService = new ModelingService(new ScoreCommonService());
			var model = modelingService.GetModelById(processQueue.ObjectId.Value);

			modelingService.CreateObjectsForModel(model);

			//загружаем второй раз, т.к. некоторые объекты могут быть исключены вручную
			modelingService.CreateCoefficientsForObjects(model.ModelId);

            //TODO send coefficients to API-service

			//WorkerCommon.SetProgress(processQueue, 100);
		}
	}
}
