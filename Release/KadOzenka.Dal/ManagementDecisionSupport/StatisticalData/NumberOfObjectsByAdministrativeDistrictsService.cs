using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Market;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class NumberOfObjectsByAdministrativeDistrictsService : StatisticalDataService
	{
		private readonly StatisticalDataService _statisticalDataService;
		private readonly GbuObjectService _gbuObjectService;

		public NumberOfObjectsByAdministrativeDistrictsService(StatisticalDataService statisticalDataService, GbuObjectService gbuObjectService)
		{
			_statisticalDataService = statisticalDataService;
			_gbuObjectService = gbuObjectService;
		}

        public List<NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesDto>
	        GetNumberOfObjectsByAdministrativeDistrictsByGroupsAndTypes(long[] taskList,
		        StatisticDataAreaDivisionType divisionType)
        {
	        var buildingPurposeAttr = _statisticalDataService.GetRosreestrBuildingPurposeAttribute();
	        var placementPurposeAttr = _statisticalDataService.GetRosreestrPlacementPurposeAttribute();

	        var groupsTable = OMGroup.Where(x => true)
		        .Select(x => x.Id)
		        .Select(x => x.ParentId)
		        .Select(x => x.GroupName)
		        .Execute();
	        var quartalDictionaryTable = OMQuartalDictionary.Where(x => true)
		        .Select(x => x.CadastralQuartal)
		        .Select(x => x.Region_Code)
		        .Select(x => x.District_Code)
		        .Execute();

			var units = OMUnit.Where(x => taskList.Contains((long)x.TaskId) && x.ObjectId != null)
				.Select(x => x.Id)
				.Select(x => x.CadastralBlock)
				.Select(x => x.GroupId)
				.Select(x => x.PropertyType_Code)
				.Select(x => x.ObjectId)
				.Execute();

			var gbuObjectIds = units.Select(x => x.ObjectId.Value).ToList();
			var gbuAttributes = _gbuObjectService.GetAllAttributes(gbuObjectIds,
				new List<long> { buildingPurposeAttr.RegisterId, placementPurposeAttr.RegisterId },
				new List<long> { buildingPurposeAttr.Id, placementPurposeAttr.Id },
				DateTime.Now.GetEndOfTheDay());

			var result = new List<NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesDto>();
			foreach (var unit in units)
			{
				var quartalDictionaryInfo =
					quartalDictionaryTable.FirstOrDefault(x => x.CadastralQuartal == unit.CadastralBlock);
				if (quartalDictionaryInfo != null)
				{
					string groupName = null;
					var subgroup = groupsTable.FirstOrDefault(x => x.Id == unit.GroupId);
					if (subgroup != null)
					{
						var group = groupsTable.FirstOrDefault(x => x.Id == subgroup.ParentId);
						groupName = group?.GroupName;
					}

					var dto = new NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesDto
					{
						PropertyTypeCode = unit.PropertyType_Code,
						PropertyType = unit.PropertyType_Code.GetEnumDescription(),
						GbuObjectId = unit.ObjectId,
						Group = string.IsNullOrEmpty(groupName) ? "Без группы" : groupName,
						HasGroup = !string.IsNullOrEmpty(groupName),
						Count = 1
					};

					switch (divisionType)
					{
						case StatisticDataAreaDivisionType.RegionNumbers:
							dto.Name = GetRegionNumberByCadastralQuarter(quartalDictionaryInfo.CadastralQuartal);
							dto.ParentName = quartalDictionaryInfo.District_Code.GetShortTitle();
							break;
						case StatisticDataAreaDivisionType.Districts:
							dto.Name = quartalDictionaryInfo.District_Code.GetShortTitle();
							dto.ParentName = GetRegionNumberByCadastralQuarter(quartalDictionaryInfo.CadastralQuartal);
							break;
						case StatisticDataAreaDivisionType.Regions:
							dto.Name = quartalDictionaryInfo.Region_Code.GetEnumDescription();
							dto.ParentName = quartalDictionaryInfo.District_Code.GetShortTitle();
							break;
					}

					FillPurposeData(dto, gbuAttributes, buildingPurposeAttr, placementPurposeAttr);
					if (!dto.HasPurpose || dto.HasPurpose && dto.Purpose != null)
					{
						result.Add(dto);
					}
				}
			}

			result =
                result.GroupBy(x => new { x.ParentName, x.Name, x.PropertyType, x.Purpose, x.HasPurpose, x.Group, x.HasGroup }).Select(
                group => new NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesDto
                {
                    ParentName = group.Key.ParentName,
                    Name = group.Key.Name,
                    PropertyType = group.Key.PropertyType,
                    Purpose = group.Key.Purpose,
                    HasPurpose = group.Key.HasPurpose,
                    Group = group.Key.Group,
                    HasGroup = group.Key.HasGroup,
                    Count = group.ToList().DefaultIfEmpty().Sum(x => x.Count)
                }).ToList().OrderBy(x => x.HasGroup).ToList();

            return result;
        }

        public List<NumberOfObjectsByAdministrativeDistrictsBySubjectDto> GetNumberOfObjectsByAdministrativeDistrictsBySubject(long[] taskList)
        {
	        var buildingPurposeAttr = _statisticalDataService.GetRosreestrBuildingPurposeAttribute();
	        var placementPurposeAttr = _statisticalDataService.GetRosreestrPlacementPurposeAttribute();

	        var groupsTable = OMGroup.Where(x => true)
		        .Select(x => x.Id)
		        .Select(x => x.ParentId)
		        .Select(x => x.GroupName)
		        .Execute();

	        var units = OMUnit.Where(x => taskList.Contains((long)x.TaskId) && x.ObjectId != null)
		        .Select(x => x.Id)
		        .Select(x => x.GroupId)
		        .Select(x => x.PropertyType_Code)
		        .Select(x => x.ObjectId)
		        .Execute();

	        var gbuObjectIds = units.Select(x => x.ObjectId.Value).ToList();
	        var gbuAttributes = _gbuObjectService.GetAllAttributes(gbuObjectIds,
		        new List<long> { buildingPurposeAttr.RegisterId, placementPurposeAttr.RegisterId },
		        new List<long> { buildingPurposeAttr.Id, placementPurposeAttr.Id },
		        DateTime.Now.GetEndOfTheDay());

	        var result = new List<NumberOfObjectsByAdministrativeDistrictsBySubjectDto>();
	        foreach (var unit in units)
	        {
		        string groupName = null;
		        var subgroup = groupsTable.FirstOrDefault(x => x.Id == unit.GroupId);
		        if (subgroup != null)
		        {
			        var group = groupsTable.FirstOrDefault(x => x.Id == subgroup.ParentId);
			        groupName = group?.GroupName;
		        }

		        var dto = new NumberOfObjectsByAdministrativeDistrictsBySubjectDto
		        {
			        PropertyTypeCode = unit.PropertyType_Code,
			        PropertyType = unit.PropertyType_Code.GetEnumDescription(),
			        GbuObjectId = unit.ObjectId,
			        Group = string.IsNullOrEmpty(groupName) ? "Без группы" : groupName,
			        HasGroup = !string.IsNullOrEmpty(groupName),
			        Count = 1
		        };

		        FillPurposeData(dto, gbuAttributes, buildingPurposeAttr, placementPurposeAttr);
		        if (!dto.HasPurpose || dto.HasPurpose && dto.Purpose != null)
		        {
			        result.Add(dto);
		        }
	        }

            result =
                result.GroupBy(x => new { x.PropertyType, x.Purpose, x.HasPurpose, x.Group, x.HasGroup }).Select(
                group => new NumberOfObjectsByAdministrativeDistrictsBySubjectDto
                {
                    PropertyType = group.Key.PropertyType,
                    Purpose = group.Key.Purpose,
                    HasPurpose = group.Key.HasPurpose,
                    Group = group.Key.Group,
                    HasGroup = group.Key.HasGroup,
                    Count = group.ToList().DefaultIfEmpty().Sum(x => x.Count)
                }).ToList().OrderBy(x => x.HasGroup).ToList();

            return result;
        }

        public List<NumberOfObjectsByAdministrativeDistrictsByGroupsDto> GetNumberOfObjectsByAdministrativeDistrictsByGroups(long[] taskList, StatisticDataAreaDivisionType areaDivisionType)
        {
			var groupJoin = new QSJoin
			{
				RegisterId = OMGroup.GetRegisterId(),
				JoinCondition = new QSConditionSimple
				{
					ConditionType = QSConditionType.Equal,
					LeftOperand = OMUnit.GetColumn(x => x.GroupId),
					RightOperand = OMGroup.GetColumn(x => x.Id)
				},
				JoinType = QSJoinType.Left
			};
			var quartalDictionaryJoin = new QSJoin
			{
				RegisterId = OMQuartalDictionary.GetRegisterId(),
				JoinCondition = new QSConditionSimple
				{
					ConditionType = QSConditionType.Equal,
					LeftOperand = OMUnit.GetColumn(x => x.CadastralBlock),
					RightOperand = OMQuartalDictionary.GetColumn(x => x.CadastralQuartal)
				},
				JoinType = QSJoinType.Inner
			};
			var query = _statisticalDataService.GetQueryForUnitsByTasks(taskList, additionalJoins: new List<QSJoin>() { quartalDictionaryJoin, groupJoin });

			query.AddColumn(OMQuartalDictionary.GetColumn(x => x.CadastralQuartal, "CadastralQuartal"));
            query.AddColumn(OMQuartalDictionary.GetColumn(x => x.Region_Code, "Region_Code"));
            query.AddColumn(OMQuartalDictionary.GetColumn(x => x.District_Code, "District_Code"));

            var subQuery = new QSQuery(OMGroup.GetRegisterId())
            {
	            Columns = new List<QSColumn>
	            {
		            OMGroup.GetColumn(x => x.GroupName)
	            },
	            Condition = new QSConditionGroup(QSConditionGroupType.And)
	            {
		            Conditions = new List<QSCondition>
		            {
			            new QSConditionSimple(
				            OMGroup.GetColumn(x => x.Id),
				            QSConditionType.Equal,
				            OMGroup.GetColumn(x => x.ParentId))
			            {
				            RightOperandLevel = 1
			            }
		            }
	            }
            };
            query.AddColumn(subQuery, "ParentGroup");


            var table = query.ExecuteQuery();

            var data = new List<NumberOfObjectsByAdministrativeDistrictsByGroupsDto>();
            if (table.Rows.Count != 0)
            {
                for (var i = 0; i < table.Rows.Count; i++)
                {
	                var dto = new NumberOfObjectsByAdministrativeDistrictsByGroupsDto();

                    var group = table.Rows[i]["ParentGroup"].ParseToStringNullable();
                    dto.Group = string.IsNullOrEmpty(group) ? "Без группы" : group;
                    dto.HasGroup = !string.IsNullOrEmpty(group);

                    switch (areaDivisionType)
                    {
                        case StatisticDataAreaDivisionType.RegionNumbers:
	                        dto.Name = GetRegionNumberByCadastralQuarter(table.Rows[i]["CadastralQuartal"].ParseToString());
	                        break;
                        case StatisticDataAreaDivisionType.Districts:
	                        dto.Name = ((Hunteds)table.Rows[i]["District_Code"].ParseToLong()).GetShortTitle();
	                        break;
                        case StatisticDataAreaDivisionType.Regions:
	                        dto.Name = ((Districts)table.Rows[i]["Region_Code"].ParseToLong()).GetEnumDescription();
                            dto.FirstParentName = ((Hunteds)table.Rows[i]["District_Code"].ParseToLong()).GetShortTitle();
                            break;
                        case StatisticDataAreaDivisionType.Quarters:
	                        dto.Name = table.Rows[i]["CadastralQuartal"].ParseToString();
	                        dto.FirstParentName = ((Hunteds)table.Rows[i]["District_Code"].ParseToLong()).GetShortTitle();
	                        dto.SecondParentName = ((Districts)table.Rows[i]["Region_Code"].ParseToLong()).GetEnumDescription();
                            dto.ThirdParentName = GetRegionNumberByCadastralQuarter(dto.Name);
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
		            ObjectsCount = group.ToList().Count,
	            }).OrderBy(x => x.HasGroup).ToList();

            return result;
        }

        private void FillPurposeData<T>(T dto, List<GbuObjectAttribute> gbuAttributes,
	        RegisterAttribute buildingPurposeAttr, RegisterAttribute placementPurposeAttr) where  T : PropertyTypeWithPurposeDto
        {
	        if (dto.PropertyTypeCode == PropertyTypes.Building)
	        {
		        dto.HasPurpose = true;
		        var purpose = gbuAttributes
			        .FirstOrDefault(x => x.ObjectId == dto.GbuObjectId && x.AttributeId == buildingPurposeAttr.Id);
		        if (purpose != null)
		        {
			        dto.Purpose = purpose.GetValueInString();
		        }
	        }
	        else if (dto.PropertyTypeCode == PropertyTypes.Pllacement)
	        {
		        dto.HasPurpose = true;
		        var purpose = gbuAttributes
			        .FirstOrDefault(x => x.ObjectId == dto.GbuObjectId && x.AttributeId == placementPurposeAttr.Id);
		        if (purpose != null)
		        {
			        dto.Purpose = purpose.GetValueInString();
		        }
	        }
        }
    }
}
