using Core.Register;
using Core.Register.LongProcessManagment;
using Core.Register.RegisterEntities;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using Platform.Register;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition
{
	public class DataCompositionByCharacteristicsReportsLongProcessViaTables : LongProcess
	{
		private const int GbuMainObjectPackageSize = 50000;
		public string TableName => "data_composition_by_characteristics_by_tables";
		public static List<RegisterData> CachedRegisters { get; private set; }
		public static List<RegisterAttribute> CachedAttributes { get; private set; }
		public static long RosreestrRegisterId { get; private set; }

		private static readonly ILogger Log = Serilog.Log.ForContext<DataCompositionByCharacteristicsReportsLongProcessViaTables>();
		public const string LongProcessName = "DataCompositionByCharacteristicsReportsLongProcess";


		public static long AddProcessToQueue()
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName);
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			Log.Debug("Старт фонового процесса: {Description}.", processType.Description);

			FillInfoAboutGbuRegisters();
			AddLog(processQueue, "Заполнена информация по реестрам", logger: Log);

			CreteCacheTableViaObjectId();
			AddLog(processQueue, "Создана таблица", logger: Log);

			CopyObjectIdsToCacheTable(cancellationToken);
			AddLog(processQueue, "Скопированы ИД ОН", logger: Log);

			CopyAttributeIds(cancellationToken);

			Log.Debug("Финиш фонового процесса: {Description}.", processType.Description);
		}


		#region Support Methods

		private void FillInfoAboutGbuRegisters()
		{
			Log.Debug("Начат сбор информации о реестрах ГБУ.");

			var mainRegister = RegisterCache.GetRegisterData(OMMainObject.GetRegisterId());

			//для тестирования
			//var test = new List<long> {2, 4, 5, 14, 42430534 };
			//--and unit.object_id in (10743778)--(549616)
			CachedRegisters = RegisterCache.Registers.Values.Where(x => x.QuantTable == mainRegister.QuantTable &&
			                                                            x.Id != mainRegister.Id)
				//.Where(x => test.Contains(x.Id))
				.ToList();

			var registerIds = CachedRegisters.Select(x => x.Id).ToList();
			CachedAttributes = RegisterCache.RegisterAttributes.Values.Where(x => registerIds.Contains(x.RegisterId)).ToList();

			var rosreestrRegisterService = new RosreestrRegisterService();
			RosreestrRegisterId = rosreestrRegisterService.RegisterId;

			Log.Debug("Зкончен сбор информации о реестрах ГБУ.");
		}

		public void CreteCacheTableViaObjectId()
		{
			Log.Debug("Начато создание таблицы-кеша для данных отчета.");

			var sql = $@"DROP TABLE IF EXISTS {TableName};

				CREATE TABLE {TableName} (
				    object_id	bigint NOT NULL,
				    attributes			bigint[]
				);

				CREATE INDEX ON {TableName} (object_id);";

			var command = DBMngr.Main.GetSqlStringCommand(sql);
			DBMngr.Main.ExecuteNonQuery(command);

			Log.Debug("Закончено создание таблицы-кеша для данных отчета.");
		}

		private void CopyObjectIdsToCacheTable(CancellationToken cancellationToken)
		{
			var objectsCount = OMMainObject.Where(x => x.ObjectType_Code != PropertyTypes.CadastralQuartal).ExecuteCount();
			Log.Debug($"Всего в БД {objectsCount} ОН.");

			var packageIndex = 0;
			for (var i = packageIndex * GbuMainObjectPackageSize; i < (packageIndex + 1) * GbuMainObjectPackageSize; i++)
			{
				CheckCancellationToken(cancellationToken);

				Log.Debug($"Копирование пакета с ОН, индекс - {i}. До этого было выгружено {packageIndex * GbuMainObjectPackageSize} записей");

				var objects = OMMainObject.Where(x => x.ObjectType_Code != PropertyTypes.CadastralQuartal)
					.SetPackageSize(GbuMainObjectPackageSize)
					.SetPackageIndex(i)
					.Execute();
				Log.Debug($"Выкачено {objects.Count} ОН.");
				if (objects.Count == 0)
					break;

				var sqlPartWithValues = new StringBuilder();
				objects.ForEach(x => { sqlPartWithValues.Append($"({x.Id}),"); });
				sqlPartWithValues.Length--;

				var copiedObjectIdsSql = $"INSERT INTO {TableName} (object_id) values {sqlPartWithValues}";

				var insertObjectIdsCommand = DBMngr.Main.GetSqlStringCommand(copiedObjectIdsSql);
				DBMngr.Main.ExecuteNonQuery(insertObjectIdsCommand);

				packageIndex++;
			}
		}

		private void CopyAttributeIds(CancellationToken cancellationToken)
		{
			var postfixes = new List<string> { "TXT", "NUM", "DT" };
			var registersCount = 0;
			foreach (var register in CachedRegisters)
			{
				CheckCancellationToken(cancellationToken);

				Log.ForContext("RegisterId", register.Id)
					.Debug($"Начата работа с реестром '{register.Description}'. №{++registersCount} из {CachedRegisters.Count}");

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
					var attributes = CachedAttributes.Where(x => x.RegisterId == register.Id).ToList();
					foreach (var attribute in attributes)
					{
						CheckCancellationToken(cancellationToken);

						if (attribute.IsPrimaryKey)
							continue;

						var gbuTableName = $"{register.AllpriTable}_{attribute.Id}";

						var subQuery = $@"select object_id, CAST ({attribute.Id} AS bigint) as newAttribute from {gbuTableName} 
									--where object_id = 15880792
									group by object_id";

						var sql = $@"update {TableName} cache_table 
									set attributes = array_append(attributes, source.newAttribute)
									from ({subQuery}) as source
									where cache_table.object_id = source.object_id";

						Log.ForContext("RegisterId", register.Id).Debug(new Exception(sql), $"Запрос для таблицы - {gbuTableName}");

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
