using System;
using System.Linq;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using KadOzenka.Dal.Correction;
using KadOzenka.Dal.Modeling;
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

			var service = new ModelingService();
			var model = service.GetModelById(processQueue.ObjectId.Value);

			var groupedObjects = OMCoreObject.Where(x =>
					x.PropertyMarketSegment_Code == model.MarketSegment &&
					x.CadastralNumber != null && x.CadastralNumber != string.Empty)
				.Select(x => x.CadastralNumber)
				.Select(x => x.Price)
				.Execute()
				.GroupBy(x => new
				{
					x.CadastralNumber,
					x.Price
				}).ToList();

			var objectsFromPreviousCalculation = OMModelToMarketObjects.Where(x => x.ModelId == model.ModelId).SelectAll().Execute();
			groupedObjects.ForEach(groupedObj =>
			{
				var existedObject = objectsFromPreviousCalculation.FirstOrDefault(x =>
					x.CadastralNumber == groupedObj.Key.CadastralNumber && x.Price == groupedObj.Key.Price);
				if (existedObject == null)
				{
					new OMModelToMarketObjects
					{
						ModelId = model.ModelId,
						CadastralNumber = groupedObj.Key.CadastralNumber,
						Price = groupedObj.Key.Price ?? 0
					}.Save();
				}
			});

			//TODO send objects {CN + Price} to API-service

			//WorkerCommon.SetProgress(processQueue, 100);
		}
	}
}
