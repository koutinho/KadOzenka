using System.Threading;
using System.Threading.Tasks;
using KadOzenka.Dal.DataImport;
using ObjectModel.Common;

namespace KadOzenka.Dal.Logger
{
	public class DataImporterGknLongProcessProgressLogger
	{
		private Task _taskLogProgress;
		private CancellationTokenSource _cancelSourceLogProcess;

		public void StartLogProgress(OMImportDataLog dataLog, DataImporterGkn dataImporterGkn)
		{
			if (_taskLogProgress != null)
				StopLogProgress();

			_cancelSourceLogProcess = new CancellationTokenSource();
			var token = _cancelSourceLogProcess.Token;
			_taskLogProgress = Task.Run(() => {
				while (true)
				{
					if (token.IsCancellationRequested)
					{
						if(dataImporterGkn.AreCountersInitialized)
							CollectStatistic(dataLog, GetFileTotalNumberOfObjects(dataImporterGkn), GetFileNumberOfImportedObjects(dataImporterGkn));

						break;
					}

					CollectStatistic(dataLog, dataImporterGkn);
					Thread.Sleep(1000);
				}
			}, token);
		}

		public void StopLogProgress()
		{
			if (_taskLogProgress == null)
				return;

			_cancelSourceLogProcess.Cancel();

			try
			{
				_taskLogProgress.Wait();
			}
			finally
			{
				_cancelSourceLogProcess.Dispose();
			}

			_taskLogProgress = null;
		}

		private void CollectStatistic(OMImportDataLog dataLog, DataImporterGkn dataImporterGkn)
		{
			var totalNumberOfObjects = GetFileTotalNumberOfObjects(dataImporterGkn);
			var numberOfImportedObjects = GetFileNumberOfImportedObjects(dataImporterGkn);
			if (dataImporterGkn.AreCountersInitialized && (dataLog.TotalNumberOfObjects != totalNumberOfObjects ||
				dataLog.NumberOfImportedObjects != numberOfImportedObjects))
			{
				CollectStatistic(dataLog, totalNumberOfObjects, numberOfImportedObjects);
			}
		}

		private void CollectStatistic(OMImportDataLog dataLog, int totalNumberOfObjects, int totalNumberOfImportedObjects)
		{
			dataLog.TotalNumberOfObjects = totalNumberOfObjects;
			dataLog.NumberOfImportedObjects = totalNumberOfImportedObjects;
			dataLog.Save();
		}

		private int GetFileNumberOfImportedObjects(DataImporterGkn dataImporterGkn)
		{
			return dataImporterGkn.CountImportBuildings + dataImporterGkn.CountImportParcels +
				   dataImporterGkn.CountImportConstructions + dataImporterGkn.CountImportUncompliteds +
				   dataImporterGkn.CountImportFlats + dataImporterGkn.CountImportCarPlaces;
		}

		private int GetFileTotalNumberOfObjects(DataImporterGkn dataImporterGkn)
		{
			return dataImporterGkn.CountXmlBuildings + dataImporterGkn.CountXmlParcels +
				   dataImporterGkn.CountXmlConstructions + dataImporterGkn.CountXmlUncompliteds +
				   dataImporterGkn.CountXmlFlats + dataImporterGkn.CountXmlCarPlaces;
		}
	}
}
