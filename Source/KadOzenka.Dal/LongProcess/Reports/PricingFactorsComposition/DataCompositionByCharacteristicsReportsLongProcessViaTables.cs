using Core.Register.LongProcessManagment;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using Platform.Register;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition
{
	public class DataCompositionByCharacteristicsReportsLongProcessViaTables : LongProcess
	{
		private const int GbuMainObjectPackageSize = 150000;
		public static string TableName => "data_composition_by_characteristics_by_tables";

		private static readonly ILogger Log = Serilog.Log.ForContext<DataCompositionByCharacteristicsReportsLongProcessViaTables>();
		private DataCompositionByCharacteristicsService DataCompositionByCharacteristicsService { get; set; }

		public DataCompositionByCharacteristicsReportsLongProcessViaTables()
		{
			DataCompositionByCharacteristicsService = new DataCompositionByCharacteristicsService();
		}



		public static long AddProcessToQueue()
		{
			return LongProcessManager.AddTaskToQueue(nameof(DataCompositionByCharacteristicsReportsLongProcessViaTables));
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			Log.Debug("Старт фонового процесса: {Description}.", processType.Description);

			CreteCacheTableViaObjectId();

			CopyObjectIdsToCacheTable(cancellationToken);

			CopyAttributeIds(cancellationToken);

			Log.Debug("Финиш фонового процесса: {Description}.", processType.Description);
		}


		#region Support Methods

		public void CreteCacheTableViaObjectId()
		{
			Log.Debug("Начато создание таблицы-кеша для данных отчета.");

			var sql = $@"DROP TABLE IF EXISTS {TableName};

				CREATE TABLE {TableName} (
				    object_id			bigint NOT NULL,
					cadastral_number	varchar(20) NOT NULL,
				    attributes			bigint[]
				);

				CREATE UNIQUE INDEX ON {TableName} (object_id);";

			var command = DBMngr.Main.GetSqlStringCommand(sql);
			DBMngr.Main.ExecuteNonQuery(command);

			Log.Debug("Закончено создание таблицы-кеша для данных отчета.");
		}

		private void CopyObjectIdsToCacheTable(CancellationToken cancellationToken)
		{
			var objectsCount = OMMainObject.Where(x => x.ObjectType_Code != PropertyTypes.CadastralQuartal).ExecuteCount();
			Log.Debug($"Всего в БД {objectsCount} ОН.");

			var copiedObjectsCount = 0;
			var packageIndex = 0;
			for (var i = packageIndex * GbuMainObjectPackageSize; i < (packageIndex + 1) * GbuMainObjectPackageSize; i++)
			{
				if(copiedObjectsCount >= objectsCount)
					break;

				CheckCancellationToken(cancellationToken);

				var copiedObjectIdsSql = $@"INSERT INTO {TableName} (object_id, cadastral_number) 
							(
								select id, cadastral_number from gbu_main_object where OBJECT_TYPE_CODE <> 2190 order by id limit {GbuMainObjectPackageSize} offset {packageIndex * GbuMainObjectPackageSize} 
							)";

				Log.Debug(new Exception(copiedObjectIdsSql), $"Начато копирование пакета с ОН, индекс - {i}. До этого было выгружено {copiedObjectsCount} записей");
				var insertObjectIdsCommand = DBMngr.Main.GetSqlStringCommand(copiedObjectIdsSql);
				copiedObjectsCount += DBMngr.Main.ExecuteNonQuery(insertObjectIdsCommand);
				Log.Debug($"Закончено копирование пакета {i}");

				packageIndex++;
			}
		}

		private void CopyAttributeIds(CancellationToken cancellationToken)
		{
			var postfixes = new List<string> { "TXT", "NUM", "DT" };
			var registersCount = 0;
			foreach (var register in DataCompositionByCharacteristicsService.CachedRegisters)
			{
				CheckCancellationToken(cancellationToken);

				Log.ForContext("RegisterId", register.Id)
					.Debug($"Начата работа с реестром '{register.Description}'. №{++registersCount} из {DataCompositionByCharacteristicsService.CachedRegisters.Count}");

				if (register.AllpriPartitioning == AllpriPartitioningType.DataType)
				{
					foreach (var postfix in postfixes)
					{
						CheckCancellationToken(cancellationToken);

						var gbuTableName = $"{register.AllpriTable}_{postfix}";

						var subQuery = $@" select object_id, array_agg(distinct(attribute_id)) as newAttributes from {gbuTableName}
									--where object_id = 15880792
									group by object_id";

						var sql = $@"update {TableName} cache_table
									set attributes = array_cat(attributes, source.newAttributes)
									from ({subQuery}) as source
									where cache_table.object_id = source.object_id";

						Log.ForContext("RegisterId", register.Id).Debug(new Exception(sql), $"Запрос для таблицы - {gbuTableName}");

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

						var subQuery = $@"select object_id from {gbuTableName} 
									--where object_id = 15880792
									group by object_id";

						var sql = $@"update {TableName} cache_table 
									set attributes = array_append(attributes, CAST ({attribute.Id} AS bigint))
									from ({subQuery}) as source
									where cache_table.object_id = source.object_id";

						Log.ForContext("RegisterId", register.Id).Debug(new Exception(sql), $"Запрос для таблицы - {gbuTableName}. №{++attributesCounter} из {attributes.Count}");

						var insetAttributesCommand = DBMngr.Main.GetSqlStringCommand(sql);
						DBMngr.Main.ExecuteNonQuery(insetAttributesCommand);
					}
				}
			}
		}


		private void CheckCancellationToken(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested) 
				return;

			var message = "Формирование кеш-таблицы было отменено пользователем";
			Log.Error(message);
			throw new Exception(message);
		}

		#endregion
	}
}
