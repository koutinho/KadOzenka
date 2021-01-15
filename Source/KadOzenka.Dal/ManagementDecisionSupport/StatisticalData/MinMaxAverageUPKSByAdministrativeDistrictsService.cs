using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Market;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{

	public class MinMaxAverageUPKSByAdministrativeDistrictsService
	{
		private class InitialData
		{
			public long id { get; set; }
			public long districtCode { get; set; }
			public long regionCode { get; set; }
			public string cadastralDistrict { get; set; }
			public long propertyTypeCode { get; set; }
			public long objectsCount { get; set; }
			public decimal min { get; set; }
			public decimal average { get; set; }
			public decimal averageWeight { get; set; }
			public decimal max { get; set; }
		}

		private readonly GbuCodRegisterService _gbuCodRegisterService;

		public readonly QueryManager QueryManager;
		public MinMaxAverageUPKSByAdministrativeDistrictsService(GbuCodRegisterService gbuCodRegisterService)
		{
			QueryManager =  new QueryManager();
			_gbuCodRegisterService = gbuCodRegisterService;
		}

		public List<MinMaxAverageUPKSByAdministrativeDistrictsDto> GetMinMaxAverageUPKSByAdministrativeDistricts(long[] taskList, MinMaxAverageUPKSByAdministrativeDistrictsType reportType)
		{
			string contents, fileName = string.Empty;
			switch (reportType)
			{
				case MinMaxAverageUPKSByAdministrativeDistrictsType.ByDistricts:
					fileName = "GetMinMaxAverageUPKSByAdministrativeDistricts_Districts";
					break;
				case MinMaxAverageUPKSByAdministrativeDistrictsType.ByCarastralRegions:
					fileName = "GetMinMaxAverageUPKSByAdministrativeDistricts_CadastralRegions";
					break;
				case MinMaxAverageUPKSByAdministrativeDistrictsType.ByRegions:
					fileName = "GetMinMaxAverageUPKSByAdministrativeDistricts_Regions";
					break;
			}

			using (var sr = new StreamReader(Core.ConfigParam.Configuration.GetFileStream(fileName, "sql", "SqlQueries"))) contents = sr.ReadToEnd();
			var table = QueryManager.ExecuteSql<InitialData>(string.Format(contents, string.Join(", ", taskList), _gbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id));
			var data = new List<MinMaxAverageUPKSByAdministrativeDistrictsDto>();

			if (table.Count != 0)
			{
				foreach (InitialData initial in table)
				{
					var dtoMin = new MinMaxAverageUPKSByAdministrativeDistrictsDto
					{
						ObjectsCount = initial.objectsCount,
						PropertyType = ((PropertyTypes)initial.propertyTypeCode).GetEnumDescription(),
						UpksCalcType = UpksCalcType.Min,
						UpksCalcValue = initial.min
					};
					var dtoAvg = new MinMaxAverageUPKSByAdministrativeDistrictsDto
					{
						ObjectsCount = initial.objectsCount,
						PropertyType = ((PropertyTypes)initial.propertyTypeCode).GetEnumDescription(),
						UpksCalcType = UpksCalcType.Average,
						UpksCalcValue = initial.average
					};
					var dtoAvgWeight = new MinMaxAverageUPKSByAdministrativeDistrictsDto
					{
						ObjectsCount = initial.objectsCount,
						PropertyType = ((PropertyTypes)initial.propertyTypeCode).GetEnumDescription(),
						UpksCalcType = UpksCalcType.AverageWeight,
						UpksCalcValue = initial.averageWeight
					};
					var dtoMax = new MinMaxAverageUPKSByAdministrativeDistrictsDto
					{
						ObjectsCount = initial.objectsCount,
						PropertyType = ((PropertyTypes)initial.propertyTypeCode).GetEnumDescription(),
						UpksCalcType = UpksCalcType.Max,
						UpksCalcValue = initial.max
					};

                    switch (reportType)
                    {
						case MinMaxAverageUPKSByAdministrativeDistrictsType.ByDistricts:
							dtoMin.Name = ((Hunteds)initial.districtCode).GetShortTitle();
							dtoAvg.Name = ((Hunteds)initial.districtCode).GetShortTitle();
							dtoAvgWeight.Name = ((Hunteds)initial.districtCode).GetShortTitle();
							dtoMax.Name = ((Hunteds)initial.districtCode).GetShortTitle();
							break;
						case MinMaxAverageUPKSByAdministrativeDistrictsType.ByCarastralRegions:
							dtoMin.Name = initial.cadastralDistrict;
							dtoAvg.Name = initial.cadastralDistrict;
							dtoAvgWeight.Name = initial.cadastralDistrict;
							dtoMax.Name = initial.cadastralDistrict;
							break;
						case MinMaxAverageUPKSByAdministrativeDistrictsType.ByRegions:
							dtoMin.Name = ((Districts)initial.regionCode).GetEnumDescription();
							dtoMin.AdditionalName = ((Hunteds)initial.districtCode).GetShortTitle();
							dtoAvg.Name = ((Districts)initial.regionCode).GetEnumDescription();
							dtoAvg.AdditionalName = ((Hunteds)initial.districtCode).GetShortTitle();
							dtoAvgWeight.Name = ((Districts)initial.regionCode).GetEnumDescription();
							dtoAvgWeight.AdditionalName = ((Hunteds)initial.districtCode).GetShortTitle();
							dtoMax.Name = ((Districts)initial.regionCode).GetEnumDescription();
							dtoMax.AdditionalName = ((Hunteds)initial.districtCode).GetShortTitle();
							break;
					}
					data.Add(dtoMin);
					data.Add(dtoAvg);
					data.Add(dtoAvgWeight);
					data.Add(dtoMax);
				}
			}
			return data;
		}

	}
}
