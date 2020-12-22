using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Core.Register;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Platform.Register;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition
{
	public class DataCompositionByCharacteristicsService
	{
		private const int MaxNumberOfUnits = 200000;
		public string TableName => "data_composition_by_characteristics";
		public List<RegisterData> CachedRegisters { get; private set; }
		public List<RegisterAttribute> CachedAttributes { get; private set; }
		public long RosreestrRegisterId { get; private set; }

		private readonly CancellationManager _cancellationManager;

		public DataCompositionByCharacteristicsService(CancellationManager cancellationManager)
		{
			_cancellationManager = cancellationManager;
			var mainRegister = RegisterCache.GetRegisterData(ObjectModel.Gbu.OMMainObject.GetRegisterId());

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
		}


		public List<long> GetLongPerformanceTasks()
		{
			//return new List<long> { 15534573 };

			var sql = $"select task_id as id from ko_unit group by task_id having count(*) > {MaxNumberOfUnits}";
			return GetTaskIds(sql);
		}

		public List<long> GetCachedTaskIds()
		{
			var sql = $"select distinct task_id as id from {TableName}";
			return GetTaskIds(sql);
		}

		public void CreteCacheTable()
		{
			var sql = $@"DROP TABLE IF EXISTS {TableName};

				CREATE TABLE {TableName} (
				    task_id				bigint NOT NULL,
				    cadastral_number	varchar(20) NOT NULL,
				    attributes			text[]
				);

				CREATE INDEX ON {TableName} (task_id);";

			var command = DBMngr.Main.GetSqlStringCommand(sql);
			DBMngr.Main.ExecuteNonQuery(command);
		}

		public bool IsCacheTableExists()
		{
			var isExists = false;

			var sql = $@"SELECT EXISTS (SELECT FROM information_schema.tables WHERE table_name = '{TableName}') as {nameof(isExists)}";
			var dataTable = _cancellationManager.ExecuteSqlStringToDataSet(sql).Tables[0];
			//var command = DBMngr.Main.GetSqlStringCommand(sql);
			//var dataTable = DBMngr.Main.ExecuteDataSet(command).Tables[0];

			if (dataTable.Rows.Count > 0)
			{
				isExists = dataTable.Rows[0][nameof(isExists)].ParseToBooleanNullable().GetValueOrDefault();
			}

			return isExists;
		}

		public void FillCache(long taskId)
		{
			var sql = GetBasicSql(taskId);
			sql.Append($@"
					INSERT INTO {TableName} 
					select {taskId}, cadastralNumber, array_remove(attributes, NULL) as attributes from data;");

			var command = DBMngr.Main.GetSqlStringCommand(sql.ToString());
			DBMngr.Main.ExecuteNonQuery(command);
		}

		public string GetSqlForRuntime(List<long> taskIds)
		{
			var runtimeSql = GetBasicSql(taskIds);

			runtimeSql.Append("\nselect cadastralNumber, array_remove(attributes, NULL) as attributes from data");

			return runtimeSql.ToString();
		}

		public string GetSqlForCache(List<long> taskIds)
		{
			return $"select cadastral_number as cadastralNumber, attributes from {TableName} where task_id in ({string.Join(',', taskIds)})";
		}

		public int GetUnitsCount(List<long> taskIds)
		{
			var columnName = "count";
			var sql = $"select count(*) as {columnName} from ko_unit unit where {GetUnitCondition(taskIds)}";

			var dataTable= _cancellationManager.ExecuteSqlStringToDataSet(sql)?.Tables[0];
			//var command = DBMngr.Main.GetSqlStringCommand(sql);
			//var dataTable = DBMngr.Main.ExecuteDataSet(command).Tables[0];

			if (dataTable.Rows.Count <= 0) 
				return 0;

			var row = dataTable.Rows[0];

			return row[columnName].ParseToInt();
		}


		#region Support Methods

		private List<long> GetTaskIds(string sql)
		{
			var command = DBMngr.Main.GetSqlStringCommand(sql);
			var dataTable = DBMngr.Main.ExecuteDataSet(command).Tables[0];

			var tasIds = new List<long>();
			foreach (DataRow row in dataTable.Rows)
			{
				tasIds.Add(row["id"].ParseToLong());
			}

			return tasIds;
		}

		private StringBuilder GetBasicSql(long taskId)
		{
			return GetBasicSql(new List<long> {taskId});
		}

		private StringBuilder GetBasicSql(List<long> taskIds)
		{
			var counter = 0;
			var sql = new StringBuilder(@"with data as(
				select unit.cadastral_number as CadastralNumber,
				ARRAY[");

			var postfixes = new List<string> { "TXT", "NUM", "DT" };
			foreach (var register in CachedRegisters)
			{
				if (register.AllpriPartitioning == AllpriPartitioningType.DataType)
				{
					foreach (var postfix in postfixes)
					{
						var tableName = $"{register.AllpriTable}_{postfix}";
						var tableAlias = $"source_{++counter}";

						sql.Append($@" 
						(select string_agg(distinct cast ({tableAlias}.attribute_id as text), ',') 
						from {tableName} {tableAlias} where unit.object_id = {tableAlias}.object_id),");
					}
				}
				else if (register.AllpriPartitioning == AllpriPartitioningType.AttributeId)
				{
					var attributes = CachedAttributes.Where(x => x.RegisterId == register.Id).ToList();
					foreach (var attribute in attributes)
					{
						if (attribute.IsPrimaryKey)
							continue;

						var tableName = $"{register.AllpriTable}_{attribute.Id}";
						var tableAlias = $"gbu_source_{++counter}";

						sql.Append($@"
							(select (case when {tableAlias}.id is null then '' else '{attribute.Id}' end)
							from {tableName} {tableAlias} 
							where unit.object_id = {tableAlias}.object_id limit 1),");
					}
				}
			}
			//удаляем ',' после последнего столбца
			sql.Length--;

			sql.Append($@"] as attributes
			from ko_unit unit
				where {GetUnitCondition(taskIds)}
				group by unit.cadastral_number, unit.object_id 
				order by unit.cadastral_number)");

			return sql;
		}

		private string GetUnitCondition(List<long> taskIds)
		{
			return $@" unit.task_id in ({string.Join(',', taskIds)}) and unit.PROPERTY_TYPE_CODE<>2190 ";
		}

		#endregion
	}
}
