using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using ObjectModel.Directory;


namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{

	public class NumberOfObjectsByAdministrativeDistrictsService : StatisticalDataService
	{

		private class InitialData
		{
			public long id { get; set; }
			public string cadastralQuartal { get; set; }
			public long regionCode { get; set; }
			public long districtCode { get; set; }
			public string ParentGroup { get; set; }
			public long objectsCount { get; set; }
			public string cadastralDistrict { get; set; }
			public long propertyTypeCode { get; set; }
		}

		private readonly GbuCodRegisterService _gbuCodRegisterService;

		public NumberOfObjectsByAdministrativeDistrictsService(GbuCodRegisterService gbuCodRegisterService)
		{
			_gbuCodRegisterService = gbuCodRegisterService;
		}

		public List<NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesDto> GetNumberOfObjectsByAdministrativeDistrictsByGroupsAndTypes(long[] taskList, StatisticDataAreaDivisionType divisionType, bool isOks)
		{
			string contents, fileName = string.Empty;
			switch (divisionType)
			{
				case StatisticDataAreaDivisionType.RegionNumbers:
				case StatisticDataAreaDivisionType.Districts:
					fileName = "NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypes_DistrictsRegionNumbers";
					break;
				case StatisticDataAreaDivisionType.Regions:
					fileName = "NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypes_Regions";
					break;
			}
			using (var sr = new StreamReader(Core.ConfigParam.Configuration.GetFileStream(fileName, "sql", "SqlQueries"))) contents = sr.ReadToEnd();
			
			var table = CancellationManager.ExecuteSql<InitialData>(string.Format(contents, string.Join(", ", taskList), isOks,
				_gbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id));

			var data = new List<NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesDto>();

			if (table.Count != 0)
			{
				foreach (InitialData initial in table)
				{

					var dto = new NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesDto
                    {
                        Group = string.IsNullOrEmpty(initial.ParentGroup) ? "Без группы" : initial.ParentGroup,
						HasGroup = !string.IsNullOrEmpty(initial.ParentGroup),
						PropertyTypeCode = (PropertyTypes)initial.propertyTypeCode,
						PropertyType = ((PropertyTypes)initial.propertyTypeCode).GetEnumDescription(),
						Count = initial.objectsCount
					};

					switch (divisionType)
					{
						case StatisticDataAreaDivisionType.RegionNumbers:
							dto.Name = initial.cadastralQuartal;
							dto.ParentName = ((Hunteds)initial.districtCode).GetShortTitle();
							break;
						case StatisticDataAreaDivisionType.Districts:
							dto.Name = ((Hunteds)initial.districtCode).GetShortTitle();
							dto.ParentName = initial.cadastralQuartal;
							break;
						case StatisticDataAreaDivisionType.Regions:
							dto.Name = ((Districts)initial.regionCode).GetEnumDescription();
							dto.ParentName = ((Hunteds)initial.districtCode).GetShortTitle();
							break;
					}
					//FillPurposeData(dto, gbuAttributes, buildingPurposeAttr, placementPurposeAttr);
					if (!dto.HasPurpose || dto.HasPurpose && dto.Purpose != null) data.Add(dto);
				}
			}

			var result = data.GroupBy(x => new { x.ParentName, x.Name, x.PropertyType, x.Purpose, x.HasPurpose, x.Group, x.HasGroup }).Select(
			group => new NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesDto
			{
				ParentName = group.ToList().FirstOrDefault()?.ParentName,
				Name = group.Key.Name,
				PropertyType = group.ToList().FirstOrDefault()?.PropertyType,
				Purpose = group.Key.Purpose,
				HasPurpose = group.Key.HasPurpose,
				Group = group.Key.Group,
				HasGroup = group.Key.HasGroup,
				Count = group.ToList().FirstOrDefault().Count,
			}).OrderBy(x => x.HasGroup).ToList();

            return result;
		}

		public List<NumberOfObjectsByAdministrativeDistrictsBySubjectDto> GetNumberOfObjectsByAdministrativeDistrictsBySubject(long[] taskList, bool isOks)
		{
			string contents;
			using (var sr = new StreamReader(Core.ConfigParam.Configuration.GetFileStream("NumberOfObjectsByAdministrativeDistrictsBySubject", "sql", "SqlQueries"))) contents = sr.ReadToEnd();
			var table = CancellationManager.ExecuteSql<InitialData>(string.Format(contents, string.Join(", ", taskList),
				isOks));
			var data = new List<NumberOfObjectsByAdministrativeDistrictsBySubjectDto>();
			if (table.Count != 0)
			{
				foreach (InitialData initial in table)
				{
					var dto = new NumberOfObjectsByAdministrativeDistrictsBySubjectDto 
					{
						Group = string.IsNullOrEmpty(initial.ParentGroup) ? "Без группы" : initial.ParentGroup,
						HasGroup = !string.IsNullOrEmpty(initial.ParentGroup),
						PropertyTypeCode = (PropertyTypes)initial.propertyTypeCode,
						PropertyType = ((PropertyTypes)initial.propertyTypeCode).GetEnumDescription(),
						Count = initial.objectsCount
					};
					//FillPurposeData(dto, gbuAttributes, buildingPurposeAttr, placementPurposeAttr);
					if (!dto.HasPurpose || dto.HasPurpose && dto.Purpose != null) data.Add(dto);
				}
			}

			var result = data.GroupBy(x => new { x.PropertyType, x.Purpose, x.HasPurpose, x.Group, x.HasGroup }).Select(
			group => new NumberOfObjectsByAdministrativeDistrictsBySubjectDto
			{
				PropertyType = group.Key.PropertyType,
				Purpose = group.Key.Purpose,
				HasPurpose = group.Key.HasPurpose,
				Group = group.Key.Group,
				HasGroup = group.Key.HasGroup,
				Count = group.ToList().FirstOrDefault().Count,
			}).OrderBy(x => x.HasGroup).ToList();

			return result;
			/*
	        var buildingPurposeAttr = _statisticalDataService.GetRosreestrBuildingPurposeAttribute();
	        var placementPurposeAttr = _statisticalDataService.GetRosreestrPlacementPurposeAttribute();
	        var gbuObjectIds = units.Select(x => x.ObjectId.Value).ToList();
	        var gbuAttributes = _gbuObjectService.GetAllAttributes(gbuObjectIds,
		        new List<long> { buildingPurposeAttr.RegisterId, placementPurposeAttr.RegisterId },
		        new List<long> { buildingPurposeAttr.Id, placementPurposeAttr.Id },
		        DateTime.Now.GetEndOfTheDay());
			*/
		}

		public List<NumberOfObjectsByAdministrativeDistrictsByGroupsDto> GetNumberOfObjectsByAdministrativeDistrictsByGroups(long[] taskList, StatisticDataAreaDivisionType areaDivisionType, bool isOks)
		{

			string contents, fileName = string.Empty;
			switch (areaDivisionType)
			{
				case StatisticDataAreaDivisionType.RegionNumbers:
					fileName = "NumberOfObjectsByAdministrativeDistrictsByGroups_RegionNumbers";
					break;
				case StatisticDataAreaDivisionType.Districts:
					fileName = "NumberOfObjectsByAdministrativeDistrictsByGroups_Districts";
					break;
				case StatisticDataAreaDivisionType.Regions:
					fileName = "NumberOfObjectsByAdministrativeDistrictsByGroups_Regions";
					break;
				case StatisticDataAreaDivisionType.Quarters:
					fileName = "NumberOfObjectsByAdministrativeDistrictsByGroups_Quarters";
					break;
			}
			using (var sr = new StreamReader(Core.ConfigParam.Configuration.GetFileStream(fileName, "sql", "SqlQueries"))) contents = sr.ReadToEnd();
			var table = QSQuery.ExecuteSql<InitialData>(string.Format(contents, string.Join(", ", taskList), isOks, _gbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id));
			var data = new List<NumberOfObjectsByAdministrativeDistrictsByGroupsDto>();
			if(table.Count != 0)
            {
                foreach (InitialData initial in table)
                {
					var dto = new NumberOfObjectsByAdministrativeDistrictsByGroupsDto();
					var group = initial.ParentGroup;
					dto.Group = string.IsNullOrEmpty(group) ? "Без группы" : group;
					dto.HasGroup = !string.IsNullOrEmpty(group);
					dto.ObjectsCount = initial.objectsCount;
					switch (areaDivisionType)
					{
						case StatisticDataAreaDivisionType.RegionNumbers:
							dto.Name = initial.cadastralQuartal;
							break;
						case StatisticDataAreaDivisionType.Districts:
							dto.Name = ((Hunteds)initial.districtCode).GetShortTitle();
							break;
						case StatisticDataAreaDivisionType.Regions:
							dto.Name = ((Districts)initial.regionCode).GetEnumDescription();
							dto.FirstParentName = ((Hunteds)initial.districtCode).GetShortTitle();
							break;
						case StatisticDataAreaDivisionType.Quarters:
							dto.Name = initial.cadastralQuartal;
							dto.FirstParentName = ((Hunteds)initial.districtCode).GetShortTitle();
							dto.SecondParentName = ((Districts)initial.regionCode).GetEnumDescription();
							dto.ThirdParentName = initial.cadastralDistrict;
							break;
					}
					data.Add(dto);
				}
            }

			var result = data.GroupBy(x => new {x.Name, x.Group, x.HasGroup}).Select(
	            group => new NumberOfObjectsByAdministrativeDistrictsByGroupsDto
	            {
		            Name = group.Key.Name,
		            FirstParentName = group.ToList().FirstOrDefault()?.FirstParentName,
		            SecondParentName = group.ToList().FirstOrDefault()?.SecondParentName,
		            ThirdParentName = group.ToList().FirstOrDefault()?.ThirdParentName,
		            Group = group.Key.Group,
		            HasGroup = group.Key.HasGroup,
		            ObjectsCount = group.ToList().FirstOrDefault().ObjectsCount,
	            }).OrderBy(x => x.HasGroup).ToList();

            return result;
        }

        private void FillPurposeData<T>(T dto, List<GbuObjectAttribute> gbuAttributes, RegisterAttribute buildingPurposeAttr, RegisterAttribute placementPurposeAttr) where  T : PropertyTypeWithPurposeDto
        {
	        if (dto.PropertyTypeCode == PropertyTypes.Building)
	        {
		        dto.HasPurpose = true;
		        var purpose = gbuAttributes.FirstOrDefault(x => x.ObjectId == dto.GbuObjectId && x.AttributeId == buildingPurposeAttr.Id);
		        if (purpose != null) dto.Purpose = purpose.GetValueInString();
	        }
	        else if (dto.PropertyTypeCode == PropertyTypes.Pllacement)
	        {
		        dto.HasPurpose = true;
		        var purpose = gbuAttributes.FirstOrDefault(x => x.ObjectId == dto.GbuObjectId && x.AttributeId == placementPurposeAttr.Id);
		        if (purpose != null) dto.Purpose = purpose.GetValueInString();
	        }
        }
    }
}
