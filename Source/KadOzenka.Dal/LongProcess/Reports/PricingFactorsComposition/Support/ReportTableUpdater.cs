using System;
using System.Linq;
using System.Threading;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;
using Serilog;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Support
{
	/// <summary>
	/// Обновляет таблицу с отчетами
	/// </summary>
	public class ReportTableUpdater : ABaseReportTableLongProcess
	{
		public static int ProcessId => 70;
		public static string ProcessName => "DataCompositionByCharacteristics_ReportTableUpdater";

		public ReportTableUpdater() : base(Log.ForContext<ReportTableUpdater>())
		{
		}


		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			Logger.Debug("Старт фонового процесса: {Description}.", processType.Description);

			using (Logger.TimeOperation("Полное время работы процесса"))
			{
				if(!IsNotDoneJobExists())
					return;

				var jobInfo = GetJobsInfo();

				for(var i = jobInfo.Min; i <= jobInfo.Max; i++)
				{
					using (Logger.TimeOperation($"Полное время работы задания №{i}"))
					{
						CheckCancellationToken(cancellationToken);

						UpdateAttributesInCacheTable(i);

						MakeJobDone(i);
					}
				}
			}

			Logger.Debug("Финиш фонового процесса: {Description}.", processType.Description);
		}


		#region Support Methods

		private bool IsNotDoneJobExists()
		{
			var isJobExists = false;
			using (Logger.TimeOperation("Проверка наличия невыполненных заданий"))
			{
				var sql = @$"select exists (select 1 from {TmpTableName} where {GetConditionForNotDoneJob()}) as is_exists";

				var command = DBMngr.Main.GetSqlStringCommand(sql);
				var row = DBMngr.Main.ExecuteDataSet(command).Tables[0]?.Rows[0];
				if (row != null)
				{
					isJobExists = row["is_exists"].ParseToBoolean();
				}

				Logger.Debug($"Задание найдено - {isJobExists}");
			}

			return isJobExists;
		}

		private JobInfo GetJobsInfo()
		{
			using (Logger.TimeOperation("Получение информации о невыполненных заданиях"))
			{
				var sql = @$"select min(job_number) as {nameof(JobInfo.Min)}, max(job_number) as {nameof(JobInfo.Max)}
							from {TmpTableName} where {GetConditionForNotDoneJob()}";

				var jobsInfo = QSQuery.ExecuteSql<JobInfo>(sql).First();
				Logger.Debug($"Минимальный номер невыполненной работы - {jobsInfo.Min}, максимальный - {jobsInfo.Max}");

				return jobsInfo;
			}
		}

		private string GetConditionForNotDoneJob()
		{
			return "COALESCE(is_done, 0) = 0";
		}

		private void UpdateAttributesInCacheTable(long jobNumber)
		{
			var sql = $@"update data_composition_by_characteristics_by_tables cache_table
							set attributes = ARRAY(SELECT DISTINCT UNNEST(attributes || newAttributes))
							from (
									select object_id, attributes as newAttributes from {TmpTableName} where job_number = {jobNumber}
								 ) as source
							where cache_table.object_id = source.object_id";

			Logger.Debug(new Exception(sql), $"Sql-запрос для задания на обновление №{jobNumber}");

			using (Logger.TimeOperation($"Время работы задания на обновление №{jobNumber}"))
			{
				var command = DBMngr.Main.GetSqlStringCommand(sql);
				var affectedRowsCount = DBMngr.Main.ExecuteNonQuery(command);
				Logger.Debug(new Exception(sql), $"Обновлено {affectedRowsCount} строк");
			}
		}

		private void MakeJobDone(long jobNumber)
		{
			var sql = $@"update {TmpTableName} set is_done = 1 where job_number = {jobNumber}";
			Logger.Debug(new Exception(sql), $"Sql-запрос для проставления статуса заданию №{jobNumber}");

			using (Logger.TimeOperation($"Время работы для проставления статуса заданию №{jobNumber}"))
			{
				var command = DBMngr.Main.GetSqlStringCommand(sql);
				var affectedRowsCount = DBMngr.Main.ExecuteNonQuery(command);
				Logger.Debug(new Exception(sql), $"Обновлено {affectedRowsCount} строк");
			}
		}

		#endregion

		#region Entities

		public class JobInfo
		{
			public long Max { get; set; }
			public long Min { get; set; }
		}

		#endregion
	}
}
