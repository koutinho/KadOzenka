using System;
using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.LongProcess.BackgroundExporter;
using KadOzenka.Dal.WorkerCheckerDataBase.Interface;
using ObjectModel.Core.LongProcess;

namespace KadOzenka.Dal.WorkerCheckerDataBase.BackgroundCheckerDb
{
	public class BackgroundChecker: IWorkerChecker
	{

		public List<CheckerModel> ExecutedList = new List<CheckerModel>();
		public void Check()
		{
			var exportsToRun = OMBackgroundExport.Where(x => x.NextRunDate <= DateTime.Now).SelectAll().Execute();

			//Проверяем и удаляем те которые уже выполнились и у них изменилась дата запуска, остаются только те что выполняются в предыдущем цикле 
			foreach (var export in exportsToRun)
			{
				if (!ExecutedList.Exists( x => x.Id == export.Id && x.DateStart.Equals(export.NextRunDate)))
				{
					var executedEl =  ExecutedList.FirstOrDefault(x => x.Id == export.Id);
					if (executedEl != null)
					{
						ExecutedList.Remove(executedEl);
					}
				}
			}

			List<long> executedListIds = ExecutedList.Select(x => x.Id).ToList();

			//Оставляем только те которые действительно надо выполнить
			var forRun = exportsToRun.Where(x => !executedListIds.Contains(x.Id)).ToList();

			// Добовляем в список выполняемых
			ExecutedList.AddRange(forRun.Select(x => new CheckerModel
			{
				Id = x.Id,
				DateStart = x.NextRunDate
			}));

			List<long> idsToRun = forRun.Select(x => x.Id).ToList();

			if(idsToRun.Count > 0) BackgroundExporterLongProcess.AddProcessToQueue(idsToRun);
		}
	}

	public class CheckerModel
	{
		public long Id { get; set; }

		public DateTime DateStart { get; set; }
	}
}