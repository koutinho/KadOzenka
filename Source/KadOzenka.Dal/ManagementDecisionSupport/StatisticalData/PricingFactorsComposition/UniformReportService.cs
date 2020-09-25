using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Core.Register;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.Registers;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition
{
	public class UniformReportService
	{
		private const int MaxNumberOfUnits = 200000;
		public string TableName => "uniformreport";
		public static List<RegisterData> CachedRegisters { get; private set; }
		public static List<RegisterAttribute> CachedAttributes { get; private set; }
		public static long RosreestrRegisterId { get; private set; }


		public UniformReportService()
		{
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
			RosreestrRegisterId = rosreestrRegisterService.RosreestrRegisterId;
		}


		public List<long> GetLongPerformanceTasks()
		{
			return new List<long> { 15534573 };
			var sql = $"select task_id as id from ko_unit group by task_id having count(*) > {MaxNumberOfUnits}";
			var command = DBMngr.Main.GetSqlStringCommand(sql);
			var dataTable = DBMngr.Main.ExecuteDataSet(command).Tables[0];

			var tasIds = new List<long>();
			foreach (DataRow row in dataTable.Rows)
			{
				tasIds.Add(row["id"].ParseToLong());
			}

			return tasIds;
		}

		public StringBuilder GetBasicSql(long taskId)
		{
			return GetBasicSql(new List<long> {taskId});
		}

		public StringBuilder GetBasicSql(List<long> taskIds)
		{
			var counter = 0;
			var sql = new StringBuilder(@"with data as(
				select unit.cadastral_number as CadastralNumber,
				ARRAY[");

			var postfixes = new List<string> { "TXT", "NUM", "DT" };
			foreach (var register in CachedRegisters)
			{
				if (register.AllpriPartitioning == Platform.Register.AllpriPartitioningType.DataType)
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
				else if (register.AllpriPartitioning == Platform.Register.AllpriPartitioningType.AttributeId)
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
				where unit.task_id in ({ string.Join(',', taskIds)}) 
				group by unit.cadastral_number, unit.object_id
				order by unit.cadastral_number)");

			return sql;
		}
	}
}
