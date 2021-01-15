//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Threading;
//using Core.Register.LongProcessManagment;
//using Core.Shared.Extensions;
//using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
//using Microsoft.Practices.EnterpriseLibrary.Data;
//using ObjectModel.Core.LongProcess;
//using Platform.Register;
//using Serilog;
//using SerilogTimings.Extensions;

//namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Developments
//{
//	//25 min
//	public class DataCompositionByCharacteristicsSingleFull : LongProcess
//	{
//		private static readonly ILogger Logger = Log.ForContext<DataCompositionByCharacteristicsSingleFull>();
//		private DataCompositionByCharacteristicsService DataCompositionByCharacteristicsService { get; }

//		public DataCompositionByCharacteristicsSingleFull()
//		{
//			DataCompositionByCharacteristicsService = new DataCompositionByCharacteristicsService();
//		}



//		public static long AddProcessToQueue()
//		{
//			return LongProcessManager.AddTaskToQueue(nameof(DataCompositionByCharacteristicsSingleFull));
//		}

//		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
//		{
//			DataCompositionByCharacteristicsService.CancellationManager.BaseCancellationToken = cancellationToken;

//			Logger.Debug("Старт фонового процесса: {Description}.", processType.Description);

//			using (Logger.TimeOperation("Полное время работы процесса"))
//			{
//				List<long> objectsIds;
//				using (Logger.TimeOperation("Получение ИД обновленных объектов"))
//				{
//					objectsIds = GetObjectIds();
//					Logger.Debug($"Найдено {objectsIds.Count} объектов для пересбора данных");
//				}
				
//				CheckCancellationToken(cancellationToken);
//				using (Logger.TimeOperation("Сброс атрибутов"))
//				{
//					ResetAttributes(objectsIds);
//				}

//				using (Logger.TimeOperation("Пересбор данных"))
//				{
//					CopyAttributeIds(objectsIds, cancellationToken);
//				}
//			}

//			Logger.Debug("Финиш фонового процесса: {Description}.", processType.Description);
//		}


//		#region Support Methods

//		private List<long> GetObjectIds()
//		{
//			var sql = "select object_id from data_composition_by_characteristics_tmp";
//			var command = DBMngr.Main.GetSqlStringCommand(sql);
//			var dataTable = DBMngr.Main.ExecuteDataSet(command).Tables[0];

//			var objectIds = new List<long>();
//			foreach (DataRow row in dataTable.Rows)
//			{
//				objectIds.Add(row["object_id"].ParseToLong());
//			}

//			return objectIds;
//		}

//		private void ResetAttributes(List<long> objectIds)
//		{
//			var sql = $"update data_composition_by_characteristics_by_tables set attributes = null where object_id in ({string.Join(',', objectIds)})";
			
//			var resetAttributesCommand = DBMngr.Main.GetSqlStringCommand(sql);
//			DBMngr.Main.ExecuteNonQuery(resetAttributesCommand);
//		}

//		private void CopyAttributeIds(List<long> objectsIds, CancellationToken cancellationToken)
//		{
//			var postfixes = new List<string> { "TXT", "NUM", "DT" };
//			var maxNumberOfRegisters = DataCompositionByCharacteristicsService.CachedRegisters.Count;
//			CheckCancellationToken(cancellationToken);

//			var objectIdsSqlCondition = $"where object_id in ({string.Join(',', objectsIds)})";
//			var i = 0;
//			DataCompositionByCharacteristicsService.CachedRegisters.ForEach(register =>
//			{
//				using (Logger.TimeOperation($"Работа с реестром '{register.Description}' (ИД {register.Id})"))
//				{
//					Logger.ForContext("RegisterId", register.Id)
//					.Debug($"Начата работа с реестром '{register.Description}'. №{i++} из {maxNumberOfRegisters}");

//					if (register.AllpriPartitioning == AllpriPartitioningType.DataType)
//					{
//						foreach (var postfix in postfixes)
//						{
//							CheckCancellationToken(cancellationToken);

//							var gbuTableName = $"{register.AllpriTable}_{postfix}";

//							var subQuery = $@" select object_id, array_agg(distinct(attribute_id)) as newAttributes from {gbuTableName}
//									{objectIdsSqlCondition}
//									group by object_id";

//							var sql = $@"update {DataCompositionByCharacteristicsService.TableName} cache_table
//									set attributes = array_cat(attributes, source.newAttributes)
//									from ({subQuery}) as source
//									where cache_table.object_id = source.object_id";

//							Logger.ForContext("RegisterId", register.Id).Debug(new Exception(sql), $"Запрос для таблицы - {gbuTableName}");

//							var insetAttributesCommand = DBMngr.Main.GetSqlStringCommand(sql);
//							DBMngr.Main.ExecuteNonQuery(insetAttributesCommand);
//						}
//					}
//					else if (register.AllpriPartitioning == AllpriPartitioningType.AttributeId)
//					{
//						var attributesCounter = 0;
//						var attributes = DataCompositionByCharacteristicsService.CachedAttributes.Where(x => x.RegisterId == register.Id).ToList();
//						foreach (var attribute in attributes)
//						{
//							CheckCancellationToken(cancellationToken);

//							if (attribute.IsPrimaryKey)
//								continue;

//							var gbuTableName = $"{register.AllpriTable}_{attribute.Id}";

//							var subQuery = $@"select object_id from {gbuTableName} {objectIdsSqlCondition} group by object_id";

//							var sql = $@"update {DataCompositionByCharacteristicsService.TableName} cache_table 
//									set attributes = array_append(attributes, CAST ({attribute.Id} AS bigint))
//									from ({subQuery}) as source
//									where cache_table.object_id = source.object_id";

//							Logger.ForContext("RegisterId", register.Id)
//								.Debug(new Exception(sql), $"Запрос для таблицы - {gbuTableName}. №{++attributesCounter} из {attributes.Count}");

//							var insetAttributesCommand = DBMngr.Main.GetSqlStringCommand(sql);
//							DBMngr.Main.ExecuteNonQuery(insetAttributesCommand);
//						}
//					}
//				}
//			});
//		}

//		private void CheckCancellationToken(CancellationToken cancellationToken)
//		{
//			if (!cancellationToken.IsCancellationRequested) 
//				return;

//			var message = "Формирование кеш-таблицы было отменено пользователем";
//			Logger.Error(message);
//			throw new Exception(message);
//		}

//		#endregion
//	}
//}
