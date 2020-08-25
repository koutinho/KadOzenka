﻿using System.Collections.Generic;
using System.IO;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class SubjectsUPKSService
	{
		private readonly StatisticalDataService _statisticalDataService;
		private readonly string _reportByTypeSqlFileName = "SubjectsUPKS_ByType";
		private readonly string _reportByTypeAndPurposeSqlFileName = "SubjectsUPKS_ByTypeAndPurpose";

		public SubjectsUPKSService(StatisticalDataService statisticalDataService)
		{
			_statisticalDataService = statisticalDataService;
		}

		public List<SubjectsUPKSByTypeDto> GetSubjectsUPKSByTypeData(long[] taskIdList)
		{
			string contents;
			using (var sr = new StreamReader(_statisticalDataService.GetSqlQueryFileStream(_reportByTypeSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var result = QSQuery.ExecuteSql<SubjectsUPKSByTypeDto>(string.Format(contents, string.Join(", ", taskIdList)));

			return result;
		}

		public List<SubjectsUPKSByTypeAndPurposeDto> GetSubjectsUPKSByTypeAndPurposeData(long[] taskIdList)
		{
			var buildingPurposeAttr = _statisticalDataService.GetRosreestrBuildingPurposeAttribute();
			var placementPurposeAttr = _statisticalDataService.GetRosreestrPlacementPurposeAttribute();

			string contents;
			using (var sr = new StreamReader(_statisticalDataService.GetSqlQueryFileStream(_reportByTypeAndPurposeSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var result = QSQuery.ExecuteSql<SubjectsUPKSByTypeAndPurposeDto>(string.Format(contents, string.Join(", ", taskIdList), buildingPurposeAttr.Id, placementPurposeAttr.Id));

			return result;
		}
	}
}
