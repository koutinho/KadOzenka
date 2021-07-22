using System.Collections.Generic;
using System.IO;
using CommonSdks;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.Registers.GbuRegistersServices;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class SubjectsUPKSService
	{
		public QueryManager QueryManager;
		private readonly StatisticalDataService _statisticalDataService;
		private readonly RosreestrRegisterService RosreestrRegisterService;
		private readonly string _reportByTypeSqlFileName = "SubjectsUPKS_ByType";
		private readonly string _reportByTypeAndPurposeSqlFileName = "SubjectsUPKS_ByTypeAndPurpose";

		public SubjectsUPKSService(StatisticalDataService statisticalDataService)
		{
			QueryManager = new QueryManager();
			_statisticalDataService = statisticalDataService;
            RosreestrRegisterService = new RosreestrRegisterService();

        }

		public List<SubjectsUPKSByTypeDto> GetSubjectsUPKSByTypeData(long[] taskIdList)
		{
			string contents;
			using (var sr = new StreamReader(_statisticalDataService.GetSqlQueryFileStream(_reportByTypeSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var result = QueryManager.ExecuteSql<SubjectsUPKSByTypeDto>(string.Format(contents, string.Join(", ", taskIdList)));

			return result;
		}

		public List<SubjectsUPKSByTypeAndPurposeDto> GetSubjectsUPKSByTypeAndPurposeData(long[] taskIdList)
		{
			var buildingPurposeAttr = RosreestrRegisterService.GetBuildingPurposeAttribute();
			var placementPurposeAttr = RosreestrRegisterService.GetPlacementPurposeAttribute();

			string contents;
			using (var sr = new StreamReader(_statisticalDataService.GetSqlQueryFileStream(_reportByTypeAndPurposeSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var result = QueryManager.ExecuteSql<SubjectsUPKSByTypeAndPurposeDto>(string.Format(contents, string.Join(", ", taskIdList), buildingPurposeAttr.Id, placementPurposeAttr.Id));

			return result;
		}
	}
}
