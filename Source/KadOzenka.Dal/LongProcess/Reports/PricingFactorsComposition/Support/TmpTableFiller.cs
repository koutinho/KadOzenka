using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;
using Serilog;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Support
{
	/// <summary>
	/// Сбор данных об объектах, которые нужно будет обновить в таблице с отчетами
	/// </summary>
	public class TmpTableFiller : ABaseReportTableLongProcess
	{
		public TmpTableFiller() : base(Log.ForContext<TmpTableFiller>())
		{
		}

		public static long AddProcessToQueue(Dictionary<long, List<long>> updatedObjectsAttributes)
		{
			var parameters = MapDictionary(updatedObjectsAttributes);

			return LongProcessManager.AddTaskToQueue(nameof(TmpTableFiller), parameters: parameters);
		}

		
		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			Logger.Debug("Старт фонового процесса: {Description}.", processType.Description);

			try
			{
				var updatedObjectsAttributes = GetInputParameters(processQueue);
				Logger.Debug($"Получено {updatedObjectsAttributes?.Count} для внесения во временную таблицу.", processType.Description);

				if (updatedObjectsAttributes?.Count != 0)
				{
					using (Logger.TimeOperation("Полное время работы процесса"))
					{
						var jobInfo = GetJobsInfo();
					
						var nexJobNumber = jobInfo.Max + 1;
						FillTmpTable(updatedObjectsAttributes, nexJobNumber);
					}
				}
			}
			catch (Exception e)
			{
				Logger.Fatal(e, "Не удалось добавить обновить таблицу");
				throw;
			}

			Logger.Debug("Финиш фонового процесса: {Description}.", processType.Description);
		}

		public static string MapDictionary(Dictionary<long, List<long>> updatedObjectsAttributes)
		{
			if (updatedObjectsAttributes == null)
				return string.Empty;

			var items = updatedObjectsAttributes.Select(x => new ObjectsToUpdate { ObjectId = x.Key, AttributeIds = x.Value }).ToList();
			
			return items.SerializeToXml();
		}


		#region Support Methods

		private List<ObjectsToUpdate> GetInputParameters(OMQueue processQueue)
		{
			List<ObjectsToUpdate> updatedObjectsAttributes = null;
			if (!string.IsNullOrWhiteSpace(processQueue.Parameters))
			{
				updatedObjectsAttributes = processQueue.Parameters.DeserializeFromXml<List<ObjectsToUpdate>>();
			}

			if (updatedObjectsAttributes == null)
				throw new Exception("Не удалось добавить обновить таблицу, т.к. нет входных данных для процесса");

			return updatedObjectsAttributes;
		}

		private void FillTmpTable(List<ObjectsToUpdate> updatedObjectsAttributes, long nexJobNumber)
		{
			var sb = new StringBuilder();
			updatedObjectsAttributes.ForEach(x => { sb.Append($"({x.ObjectId}, ARRAY[{string.Join(',', x.AttributeIds)}]::bigint[], {nexJobNumber}),"); });
			sb.Length--;

			var sql = $@"insert into {TmpTableName}
							(object_id, attributes, job_number)
							values {sb}";

			Logger.Debug(new Exception(sql), "Sql-запрос");

			var command = DBMngr.Main.GetSqlStringCommand(sql);
			var affectedRows = DBMngr.Main.ExecuteNonQuery(command);

			Logger.Debug($"В таблицу добавлено {affectedRows} строк");
		}

		#endregion


		#region Entities

		public class ObjectsToUpdate
		{
			public long ObjectId;
			public List<long> AttributeIds;
		}

		#endregion
	}
}
