using System.Collections.Generic;
using Core.SessionManagment;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports
{
	public class SessionVariablesStatisticsReports : SessionVariables
	{
		public static SessionVariable<List<UnitObjectDto>> ImportedObjectsDataReader = new SessionVariable<List<UnitObjectDto>>();
		public static SessionVariable<List<ExportedObjectDto>> ExportedObjectsDataReader = new SessionVariable<List<ExportedObjectDto>>();
		public static SessionVariable<List<ZoneStatisticDto>> ZoneStatisticsDataReader = new SessionVariable<List<ZoneStatisticDto>>();
		public static SessionVariable<List<FactorStatisticDto>> FactorStatisticsDataReader = new SessionVariable<List<FactorStatisticDto>>();
		public static SessionVariable<List<GroupStatisticDto>> GroupStatisticsDataReader = new SessionVariable<List<GroupStatisticDto>>();
	}
}
