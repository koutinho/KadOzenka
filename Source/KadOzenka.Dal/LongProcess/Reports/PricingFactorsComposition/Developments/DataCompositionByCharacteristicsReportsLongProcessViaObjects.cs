//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using Core.Register;
//using Core.Register.LongProcessManagment;
//using Core.Register.RegisterEntities;
//using KadOzenka.Dal.Registers.GbuRegistersServices;
//using Microsoft.Practices.EnterpriseLibrary.Data;
//using ObjectModel.Core.LongProcess;
//using ObjectModel.Directory;
//using ObjectModel.Gbu;
//using Platform.Register;
//using Serilog;

//namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Developments
//{
//	public class DataCompositionByCharacteristicsReportsLongProcessViaObjects : LongProcess
//	{
//		private const int PackageSize = 5000;
//		public string TableName => "data_composition_by_characteristics_by_objects";
//		public static List<RegisterData> CachedRegisters { get; private set; }
//		public static List<RegisterAttribute> CachedAttributes { get; private set; }
//		public static long RosreestrRegisterId { get; private set; }

//		private static readonly ILogger Log = Serilog.Log.ForContext<DataCompositionByCharacteristicsReportsLongProcessViaObjects>();
//		public const string LongProcessName = "DataCompositionByCharacteristicsReportsLongProcess";


//		public static long AddProcessToQueue()
//		{
//			return LongProcessManager.AddTaskToQueue(LongProcessName);
//		}

//		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
//		{
//			Log.Debug("Старт фонового процесса: {Description}.", processType.Description);

//			FillInfoAboutGbuRegisters();

//			CreteCacheTableViaCadastralNumber();

//			var objectsCount = OMMainObject.Where(x => x.ObjectType_Code != PropertyTypes.CadastralQuartal).ExecuteCount();
//			Log.Debug($"Найдено {objectsCount} ОН.");

//			var sql = GetBasicSql().ToString();
//			Log.Debug(new Exception(sql), "Сгенерирован базовый sql-запрос.");

//			var packageIndex = 0;
//			for (var i = packageIndex * PackageSize; i < (packageIndex + 1) * PackageSize; i++)
//			{
//				if (cancellationToken.IsCancellationRequested)
//				{
//					Log.Error("Формирование кеш-таблицы было отменено пользователем");
//					return;
//				}

//				var offset = packageIndex * PackageSize;
//				if (offset >= objectsCount)
//					break;

//				var fullSql = $@"{sql}
//					limit {PackageSize} offset {offset})
//					INSERT INTO {TableName} 
//					select cadastralNumber, array_remove(attributes, NULL) as attributes from data ";

//				Log.Debug(new Exception(fullSql), $"Начата обработка пакета с индексом {i}, до этого было выгружено {offset} записей");

//				var command = DBMngr.Main.GetSqlStringCommand(fullSql);
//				DBMngr.Main.ExecuteNonQuery(command);

//				packageIndex++;
//			}
//		}


//		#region Support Methods

//		private void FillInfoAboutGbuRegisters()
//		{
//			Log.Debug("Начат сбор информации о реестрах ГБУ.");

//			var mainRegister = RegisterCache.GetRegisterData(ObjectModel.Gbu.OMMainObject.GetRegisterId());

//			//для тестирования
//			//var test = new List<long> {2, 4, 5, 14, 42430534 };
//			//--and unit.object_id in (10743778)--(549616)
//			CachedRegisters = RegisterCache.Registers.Values.Where(x => x.QuantTable == mainRegister.QuantTable &&
//			                                                            x.Id != mainRegister.Id)
//				//.Where(x => test.Contains(x.Id))
//				.ToList();

//			var registerIds = CachedRegisters.Select(x => x.Id).ToList();
//			CachedAttributes = RegisterCache.RegisterAttributes.Values.Where(x => registerIds.Contains(x.RegisterId)).ToList();

//			var rosreestrRegisterService = new RosreestrRegisterService();
//			RosreestrRegisterId = rosreestrRegisterService.RegisterId;

//			Log.Debug("Зкончен сбор информации о реестрах ГБУ.");
//		}

//		public void CreteCacheTableViaCadastralNumber()
//		{
//			Log.Debug("Начато создание таблицы-кеша для данных отчета.");

//			var sql = $@"DROP TABLE IF EXISTS {TableName};

//				CREATE TABLE {TableName} (
//				    cadastral_number	varchar(20) NOT NULL,
//				    attributes			text[]
//				);

//				CREATE INDEX ON {TableName} (cadastral_number);";

//			var command = DBMngr.Main.GetSqlStringCommand(sql);
//			DBMngr.Main.ExecuteNonQuery(command);

//			Log.Debug("Закончено создание таблицы-кеша для данных отчета.");
//		}

//		private StringBuilder GetBasicSql()
//		{
//			var counter = 0;
//			var sql = new StringBuilder(@"with data as(
//				select main_object.cadastral_number as CadastralNumber,
//				ARRAY[");

//			var postfixes = new List<string> { "TXT", "NUM", "DT" };
//			foreach (var register in CachedRegisters)
//			{
//				if (register.AllpriPartitioning == AllpriPartitioningType.DataType)
//				{
//					foreach (var postfix in postfixes)
//					{
//						var tableName = $"{register.AllpriTable}_{postfix}";
//						var tableAlias = $"source_{++counter}";

//						sql.Append($@" 
//						(select string_agg(distinct cast ({tableAlias}.attribute_id as text), ',') 
//						from {tableName} {tableAlias} where main_object.id = {tableAlias}.object_id),");
//					}
//				}
//				else if (register.AllpriPartitioning == AllpriPartitioningType.AttributeId)
//				{
//					var attributes = CachedAttributes.Where(x => x.RegisterId == register.Id).ToList();
//					foreach (var attribute in attributes)
//					{
//						if (attribute.IsPrimaryKey)
//							continue;

//						var tableName = $"{register.AllpriTable}_{attribute.Id}";
//						var tableAlias = $"gbu_source_{++counter}";

//						sql.Append($@"
//							(select (case when {tableAlias}.id is null then '' else '{attribute.Id}' end)
//							from {tableName} {tableAlias} 
//							where main_object.id = {tableAlias}.object_id limit 1),");
//					}
//				}
//			}
//			//удаляем ',' после последнего столбца
//			sql.Length--;

//			sql.Append(@"] as attributes
//			from gbu_main_object main_object
//				where  main_object.object_type_code<>2190
//				group by main_object.cadastral_number, main_object.id 
//				order by main_object.cadastral_number");

//			return sql;
//		}

//		#endregion
//	}
//}
