using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Platform.Register;

namespace KadOzenka.BlFrontEnd.GbuTest
{
	public static class TriggerCreationForDataCompositionReports
	{
		public static void Start()
		{
			CreateTriggerForGbuMainObject();

			var postfixes = new List<string> { "TXT", "NUM", "DT" };
			DataCompositionByCharacteristicsService.CachedRegisters.ForEach(register =>
			{
				if (register.AllpriPartitioning == AllpriPartitioningType.DataType)
				{
					foreach (var postfix in postfixes)
					{
						var gbuTableName = $"{register.AllpriTable}_{postfix}";
						var triggerName = $"trigger_for_{gbuTableName}";

						CreateTriggerForRegistersWithAttributes(triggerName, gbuTableName, null);
					}
				}
				else
				{
					var attributes = DataCompositionByCharacteristicsService.CachedAttributes.Where(x => x.RegisterId == register.Id).ToList();
					foreach (var attribute in attributes)
					{
						if (attribute.IsPrimaryKey)
							continue;

						var gbuTableName = $"{register.AllpriTable}_{attribute.Id}";
						var triggerName = $"trigger_for_{gbuTableName}";

						CreateTriggerForRegistersWithAttributes(triggerName, gbuTableName, attribute.Id);
					}
				}
			});
		}

		private static void CreateTriggerForRegistersWithAttributes(string triggerName, string gbuTableName, long? functionInputParameter)
		{
			var triggerCreationSql = GetTriggerCreationSql(triggerName, gbuTableName,
				$"update_cache_table_for_data_composition_reports_for_registers({functionInputParameter})");

			var createTriggerCommand = DBMngr.Main.GetSqlStringCommand(triggerCreationSql);
			DBMngr.Main.ExecuteNonQuery(createTriggerCommand);
		}

		private static void CreateTriggerForGbuMainObject()
		{
			var triggerCreationSql = GetTriggerCreationSql("trigger_for_gbu_main_object", "gbu_main_object",
				"update_cache_table_for_data_composition_reports_for_main_object()");

			var createTriggerCommand = DBMngr.Main.GetSqlStringCommand(triggerCreationSql);
			DBMngr.Main.ExecuteNonQuery(createTriggerCommand);
		}

		private static string GetTriggerCreationSql(string triggerName, string gbuTableName, string functionName)
		{
			return $@"DROP TRIGGER IF EXISTS {triggerName} ON {gbuTableName};
							
						CREATE TRIGGER {triggerName}
						AFTER INSERT 
						ON {gbuTableName}
						FOR EACH ROW
						EXECUTE FUNCTION {functionName};";
		}
	}
}
