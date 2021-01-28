using System;
using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.LongProcess.BackgroundExporter;
using KadOzenka.Dal.WorkerCheckerDataBase.Interface;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Core.LongProcess;

namespace KadOzenka.Dal.WorkerCheckerDataBase.BackgroundCheckerDb
{
	public class BackgroundChecker: IWorkerChecker
	{

		public Dictionary<long, List<CheckerModel>> ExecutedList = new Dictionary<long, List<CheckerModel>>();
		public void Check()
		{
			CheckExecuteListOnRemoveByQueueIds();
			CheckExecuteListByFailUnloading();
			var exportsToRun = OMBackgroundExport.Where(x => x.NextRunDate <= DateTime.Now).SelectAll().Execute();

			List<long> executedListIds = new List<long>();
			ExecutedList.Values.ForEach(x => executedListIds.AddRange(x.Select(y => y.Id).ToList()));

			//Оставляем только те которые действительно надо выполнить
			var forRun = exportsToRun.Where(x => !executedListIds.Contains(x.Id)).ToList();

			List<long> idsToRun = forRun.Select(x => x.Id).ToList();

			long idQueue = 0;
			if(idsToRun.Count > 0) BackgroundExporterLongProcess.AddProcessToQueue(idsToRun);

			// Добовляем в список выполняемых
			if (idQueue != 0)
			{
				ExecutedList.Add(idQueue, forRun.Select(x => new CheckerModel
				{
					Id = x.Id,
					DateStart = x.NextRunDate.GetValueOrDefault()
				}).ToList());
			}
			
		}

		/// <summary>
		/// метод проверяет очереди долгих процессов и удаляет из списка ExecutedList те выгрузки процессы которых завершены успешно или с ошибкой
		/// </summary>
		public void CheckExecuteListOnRemoveByQueueIds()
		{
			List<long> queueIds = ExecutedList.Keys.Select(x => x).ToList();

			List<OMQueue> queuesToRemove = OMQueue.Where(x => queueIds.Contains(x.Id) && x.Status_Code == Status.Completed || x.Status_Code == Status.Faulted).Select(x => x.Id).Execute();

			queuesToRemove.ForEach(queue => { ExecutedList.Remove(queue.Id); });

		}

		public void CheckExecuteListByFailUnloading()
		{
			List<long> executedListIds = new List<long>();
			ExecutedList.Values.ForEach(x => executedListIds.AddRange(x.Select(y => y.Id).ToList()));

			List<OMBackgroundExport> exportsToRemove = OMBackgroundExport
				.Where(x => executedListIds.Contains(x.Id) && x.NextRunDate == null).Select(x => x.Id).Execute();

			List<long> exportsToRemoveIds = exportsToRemove.Select(x => x.Id).ToList();

			foreach (var item in ExecutedList)
			{
				var checkerModels = item.Value.FindAll(x => exportsToRemoveIds.Contains(x.Id));
				if (checkerModels.Count > 0)
				{
					checkerModels.ForEach(checkerModel => item.Value.Remove(checkerModel));
				}
			}
		}
	}

	public class CheckerModel
	{
		public long Id { get; set; }

		public DateTime DateStart { get; set; }
	}
}