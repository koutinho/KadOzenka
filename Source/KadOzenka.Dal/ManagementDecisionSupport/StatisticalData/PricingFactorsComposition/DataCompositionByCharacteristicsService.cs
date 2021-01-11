using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Register.RegisterEntities;
using Core.Shared.Exceptions;
using Core.Shared.Extensions;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.Register;
using Platform.Register;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition
{
	public class DataCompositionByCharacteristicsService
	{
		public static string TableName => "data_composition_by_characteristics_by_tables";

		private static List<RegisterData> _cachedRegisters;

		public readonly CancellationManager CancellationManager = new CancellationManager();

		public static List<RegisterData> CachedRegisters
		{
			get
			{
				if (_cachedRegisters == null)
				{
					var mainRegister = RegisterCache.GetRegisterData(ObjectModel.Gbu.OMMainObject.GetRegisterId());
					_cachedRegisters = RegisterCache.Registers.Values
						.Where(x => x.QuantTable == mainRegister.QuantTable && x.Id != mainRegister.Id).ToList();
				}

				return _cachedRegisters;
			}
		}

		private static List<RegisterAttribute> _cachedAttributes;
		public static List<RegisterAttribute> CachedAttributes
		{
			get
			{
				if (_cachedAttributes == null)
				{
					var registerIds = CachedRegisters.Select(x => x.Id).ToList();
					_cachedAttributes = RegisterCache.RegisterAttributes.Values.Where(x => registerIds.Contains(x.RegisterId)).ToList();
				}

				return _cachedAttributes;
			}
		}

		protected static long? _rosreestrRegisterId;
		public static long RosreestrRegisterId
		{
			get
			{
				if (_rosreestrRegisterId == null)
				{
					var rosreestrRegisterService = new RosreestrRegisterService();
					_rosreestrRegisterId = rosreestrRegisterService.RegisterId;
				}

				return _rosreestrRegisterId.Value;
			}
		}

		
		public bool IsCacheTableExists()
		{
			var isExists = false;

			var sql = $@"SELECT EXISTS (SELECT FROM information_schema.tables WHERE table_name = '{TableName}') as {nameof(isExists)}";
			var dataTable = CancellationManager.ExecuteSqlStringToDataSet(sql).Tables[0];

			if (dataTable.Rows.Count > 0)
			{
				isExists = dataTable.Rows[0][nameof(isExists)].ParseToBooleanNullable().GetValueOrDefault();
			}

			return isExists;
		}

		public void CreteCacheTableViaObjectId()
		{
			var sql = $@"DROP TABLE IF EXISTS {TableName};

				CREATE TABLE {TableName} (
				    object_id			bigint NOT NULL,
					cadastral_number	varchar(20) NOT NULL,
					object_type_code	integer,
				    attributes			bigint[]
				);";

			var command = DBMngr.Main.GetSqlStringCommand(sql);
			DBMngr.Main.ExecuteNonQuery(command);
		}

		public void CreteIndexOnCacheTable()
		{
			var sql = $@"CREATE UNIQUE INDEX ON {TableName} (object_id);";

			var command = DBMngr.Main.GetSqlStringCommand(sql);
			DBMngr.Main.ExecuteNonQuery(command);
		}

		public static void CreateTriggerForRegister(long registerId, long? attributeId = null)
		{
			//сделано через БД, чтобы не натыкаться на ошибки кеша и не ждать его обновления
			var register = OMRegister.Where(x => x.RegisterId == registerId)
				.Select(x => new
				{
					x.AllpriPartitioning,
					x.AllpriTable
				})
				.ExecuteFirstOrDefault();

			if (register.AllpriPartitioning == (int) AllpriPartitioningType.DataType)
			{
				var postfixes = new List<string> { "TXT", "NUM", "DT" };
				foreach (var postfix in postfixes)
				{
					var gbuTableName = $"{register.AllpriTable}_{postfix}";
					CreateTriggerForExistedRegisters(gbuTableName, null);
				}
			}
			else
			{
				if (attributeId == null)
				{
					var attributes = CachedAttributes.Where(x => x.RegisterId == register.RegisterId).ToList();
					foreach (var attribute in attributes)
					{
						if (attribute.IsPrimaryKey)
							continue;

						var gbuTableName = $"{register.AllpriTable}_{attribute.Id}";
						CreateTriggerForExistedRegisters(gbuTableName, attribute.Id);
					}
				}
				else
				{
					var gbuTableName = $"{register.AllpriTable}_{attributeId}";
					CreateTriggerForExistedRegisters(gbuTableName, attributeId);
				}
			}
		}

		private static void CreateTriggerForExistedRegisters(string gbuTableName, long? functionInputParameter)
		{
			var triggerCreationSql = GetTriggerCreationSql(gbuTableName,
				$"update_cache_table_for_data_composition_reports_for_registers({functionInputParameter})");

			var createTriggerCommand = DBMngr.Main.GetSqlStringCommand(triggerCreationSql);
			DBMngr.Main.ExecuteNonQuery(createTriggerCommand);
		}

		public static string GetTriggerCreationSql(string gbuTableName, string functionName)
		{
			var triggerName = $"trigger_for_{gbuTableName}";

			return $@"DROP TRIGGER IF EXISTS {triggerName} ON {gbuTableName};
							
						CREATE TRIGGER {triggerName}
						AFTER INSERT 
						ON {gbuTableName}
						FOR EACH ROW
						EXECUTE FUNCTION {functionName};";
		}
	}
}
