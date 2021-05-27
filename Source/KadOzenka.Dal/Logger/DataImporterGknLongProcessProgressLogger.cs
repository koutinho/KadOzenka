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
			Serilog.Log.ForContext<DataImporterGknLongProcessProgressLogger>().Debug("Начало логирования прогресса");

			if (_taskLogProgress != null)
				StopLogProgress();

			_cancelSourceLogProcess = new CancellationTokenSource();
			var token = _cancelSourceLogProcess.Token;
			_taskLogProgress = Task.Run(() => {
				while (true)
				{
					if (token.IsCancellationRequested)
					{
						if (dataImporterGkn.AreCountersInitialized)
						{
							var totalNumberOfObjects = dataImporterGkn.GetTotalObjectCountInFile();
							CollectStatistic(dataLog, totalNumberOfObjects, GetFileNumberOfImportedObjects(dataImporterGkn));
						}

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

			if(!_cancelSourceLogProcess.IsCancellationRequested)
				_cancelSourceLogProcess.Cancel();

			try
			{
				_taskLogProgress.Wait();
			}
			//падает, если задача была отменена раньше, чем стартовала
			//такое может быть, если в файле мало данных
			catch (TaskCanceledException exception)
			{
				Serilog.Log.ForContext<DataImporterGknLongProcessProgressLogger>()
					.Error(exception, "Ошибка при отмене потока для логирования прогресса");
			}
			finally
			{
				_cancelSourceLogProcess.Dispose();
			}

			_taskLogProgress = null;
		}

		private void CollectStatistic(OMImportDataLog dataLog, DataImporterGkn dataImporterGkn)
		{
			var totalNumberOfObjects = dataImporterGkn.GetTotalObjectCountInFile();
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
	}
}
