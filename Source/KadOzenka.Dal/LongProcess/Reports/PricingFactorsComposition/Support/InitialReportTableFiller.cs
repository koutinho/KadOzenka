using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Register.LongProcessManagment;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using Platform.Register;
using Serilog;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Support
{
	/// <summary>
	/// Начальный сбор данных. Идет 3 суток, заполняет таблицу для отчета по всем ОН в системе.
	/// </summary>
	public class InitialReportTableFiller : LongProcess
	{
		public static int ProcessId => 53;
		public static string ProcessName => "DataCompositionByCharacteristics_InitialReportTableFiller";

		private const int GbuMainObjectPackageSize = 150000;

		private static readonly ILogger Logger = Log.ForContext<InitialReportTableFiller>();
		private DataCompositionByCharacteristicsService DataCompositionByCharacteristicsService { get; }

		public InitialReportTableFiller()
		{
			DataCompositionByCharacteristicsService = new DataCompositionByCharacteristicsService();
		}



		public static long AddProcessToQueue()
		{
			return LongProcessManager.AddTaskToQueue(ProcessName);
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			DataCompositionByCharacteristicsService.CancellationManager.BaseCancellationToken = cancellationToken;

			Logger.Debug("Старт фонового процесса: {Description}.", processType.Description);

			using (Logger.TimeOperation("Полное время работы процесса"))
			{
				using (Logger.TimeOperation("Создание таблицы-кеша для данных отчета"))
				{
					DataCompositionByCharacteristicsService.CreteCacheTableViaObjectId();
				}

				CopyObjectIdsToCacheTable(cancellationToken);

				CopyAttributeIds(cancellationToken);

				using (Logger.TimeOperation("Создание индекса для таблицы-кеша"))
				{
					DataCompositionByCharacteristicsService.CreteIndexOnCacheTable();
				}
			}

			Logger.Debug("Финиш фонового процесса: {Description}.", processType.Description);
		}


		#region Support Methods

		private void CopyObjectIdsToCacheTable(CancellationToken cancellationToken)
		{
			var objectsCount = OMMainObject.Where(x => x.ObjectType_Code != PropertyTypes.CadastralQuartal).ExecuteCount();
			Logger.Debug($"Всего в БД {objectsCount} ОН.");

			var copiedObjectsCount = 0;
			var packageIndex = 0;
			for (var i = packageIndex * GbuMainObjectPackageSize; i < (packageIndex + 1) * GbuMainObjectPackageSize; i++)
			{
				if (copiedObjectsCount >= objectsCount)
					break;

				CheckCancellationToken(cancellationToken);

				var copiedObjectIdsSql = $@"INSERT INTO {DataCompositionByCharacteristicsService.TableName} (object_id, cadastral_number, object_type_code) 
							(
								select id, cadastral_number, object_type_code from gbu_main_object where OBJECT_TYPE_CODE <> 2190 order by id limit {GbuMainObjectPackageSize} offset {packageIndex * GbuMainObjectPackageSize} 
							)";

				Logger.Debug(new Exception(copiedObjectIdsSql), $"Начато копирование пакета с ОН, индекс - {i}. До этого было выгружено {copiedObjectsCount} записей");
				var insertObjectIdsCommand = DBMngr.Main.GetSqlStringCommand(copiedObjectIdsSql);
				copiedObjectsCount += DBMngr.Main.ExecuteNonQuery(insertObjectIdsCommand);
				Logger.Debug($"Закончено копирование пакета {i}");

				packageIndex++;
			}
		}

		private void CopyAttributeIds(CancellationToken cancellationToken)
		{
			var postfixes = new List<string> { "TXT", "NUM", "DT" };
			var maxNumberOfRegisters = DataCompositionByCharacteristicsService.CachedRegisters.Count;
			CheckCancellationToken(cancellationToken);

			var i = 0;
			DataCompositionByCharacteristicsService.CachedRegisters.ForEach(register =>
			{
				using (Logger.TimeOperation($"Работа с реестром '{register.Description}' (ИД {register.Id})"))
				{
					Logger.ForContext("RegisterId", register.Id)
					.Debug($"Начата работа с реестром '{register.Description}'. №{i++} из {maxNumberOfRegisters}");

					if (register.AllpriPartitioning == AllpriPartitioningType.DataType)
					{
						foreach (var postfix in postfixes)
						{
							CheckCancellationToken(cancellationToken);

							var gbuTableName = $"{register.AllpriTable}_{postfix}";

							var subQuery = $@" select object_id, array_agg(distinct(attribute_id)) as newAttributes from {gbuTableName}
									group by object_id";

							var sql = $@"update {DataCompositionByCharacteristicsService.TableName} cache_table
									set attributes = array_cat(attributes, source.newAttributes)
									from ({subQuery}) as source
									where cache_table.object_id = source.object_id";

							Logger.ForContext("RegisterId", register.Id).Debug(new Exception(sql), $"Запрос для таблицы - {gbuTableName}");

							var insetAttributesCommand = DBMngr.Main.GetSqlStringCommand(sql);
							DBMngr.Main.ExecuteNonQuery(insetAttributesCommand);
						}
					}
					else if (register.AllpriPartitioning == AllpriPartitioningType.AttributeId)
					{
						var attributesCounter = 0;
						var attributes = DataCompositionByCharacteristicsService.CachedAttributes.Where(x => x.RegisterId == register.Id).ToList();
						foreach (var attribute in attributes)
						{
							CheckCancellationToken(cancellationToken);

							if (attribute.IsPrimaryKey)
								continue;

							var gbuTableName = $"{register.AllpriTable}_{attribute.Id}";

							var subQuery = $@"select object_id from {gbuTableName} group by object_id";

							var sql = $@"update {DataCompositionByCharacteristicsService.TableName} cache_table 
									set attributes = array_append(attributes, CAST ({attribute.Id} AS bigint))
									from ({subQuery}) as source
									where cache_table.object_id = source.object_id";

							Logger.ForContext("RegisterId", register.Id)
								.Debug(new Exception(sql), $"Запрос для таблицы - {gbuTableName}. №{++attributesCounter} из {attributes.Count}");

							var insetAttributesCommand = DBMngr.Main.GetSqlStringCommand(sql);
							DBMngr.Main.ExecuteNonQuery(insetAttributesCommand);
						}
					}
				}
			});
		}

		private void CheckCancellationToken(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
				return;

			var message = "Формирование кеш-таблицы было отменено пользователем";
			Logger.Error(message);
			throw new Exception(message);
		}

		#endregion
	}
}
