using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace KadOzenka.BlFrontEnd.GbuTest
{
	public static class TriggerCreationForDataCompositionReports
	{
		public static void Start()
		{
			CreateTriggerForGbuMainObject();

			DataCompositionByCharacteristicsService.CachedRegisters.ForEach(register =>
			{
				DataCompositionByCharacteristicsService.CreateTriggerForRegister(register.Id);
			});
		}

		//реализовано в коде при создании нового ГБУ-источника (ObjectsCharacteristicsService)
		//private static void CreateTriggerForNewRegistersWithPartitionByDataType(string triggerName, string gbuTableName, long? functionInputParameter)
		//{
		//	var triggerCreationSql = @"DROP TRIGGER IF EXISTS test_trigger ON core_register;
		//					CREATE TRIGGER core_register_trigger_for_data_composition_report
		//					AFTER INSERT 
		//					ON core_register
		//					FOR EACH ROW
		//					WHEN (NEW.quant_table = 'GBU_MAIN_OBJECT')
		//					EXECUTE FUNCTION create_trigger_for_new_gbu_source_table_with_partition_by_data();";

		//	var createTriggerCommand = DBMngr.Main.GetSqlStringCommand(triggerCreationSql);
		//	DBMngr.Main.ExecuteNonQuery(createTriggerCommand);
		//}

		private static void CreateTriggerForGbuMainObject()
		{
			var triggerCreationSql = DataCompositionByCharacteristicsService.GetTriggerCreationSql("gbu_main_object",
				"update_cache_table_for_data_composition_reports_for_main_object()");

			var createTriggerCommand = DBMngr.Main.GetSqlStringCommand(triggerCreationSql);
			DBMngr.Main.ExecuteNonQuery(createTriggerCommand);
		}
	}
}
