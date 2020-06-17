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

        public List<NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesDto> GetNumberOfObjectsByAdministrativeDistrictsByGroupsAndTypes(long[] taskList)
        {
            var query = new QSQuery
            {
                MainRegisterID = OMUnit.GetRegisterId(),
                Condition = new QSConditionGroup
                {
                    Type = QSConditionGroupType.And,
                    Conditions = new List<QSCondition>
                    {
                        new QSConditionSimple(OMTask.GetColumn(x => x.Id), QSConditionType.In, taskList.Select(x => (double)x).ToList())
                    }
                },
                Joins = new List<QSJoin>
                {
                    new QSJoin
                    {
                        RegisterId = OMTask.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMUnit.GetColumn(x => x.TaskId),
                            RightOperand = OMTask.GetColumn(x => x.Id)
                        },
                        JoinType = QSJoinType.Inner
                    },
                    new QSJoin
                    {
                        RegisterId = OMQuartalDictionary.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMUnit.GetColumn(x => x.CadastralBlock),
                            RightOperand = OMQuartalDictionary.GetColumn(x => x.CadastralQuartal)
                        },
                        JoinType = QSJoinType.Inner
                    },
                    new QSJoin
                    {
                        RegisterId = OMGroup.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMUnit.GetColumn(x => x.GroupId),
                            RightOperand = OMGroup.GetColumn(x => x.Id)
                        },
                        JoinType = QSJoinType.Left
                    },
                }
            };

            query.AddColumn(OMQuartalDictionary.GetColumn(x => x.CadastralQuartal, "CadastralQuartal"));
            query.AddColumn(OMQuartalDictionary.GetColumn(x => x.District_Code, "District_Code"));
            query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));

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

            var result = new List<NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesDto>();
            if (table.Rows.Count != 0)
            {
                for (var i = 0; i < table.Rows.Count; i++)
                {
                    var group = table.Rows[i]["ParentGroup"]
                        .ParseToStringNullable();
                    var dto = new NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesDto
                    {
                        Distrinct = ((Hunteds)table.Rows[i]["District_Code"].ParseToLong()).GetShortTitle(),
                        RegionNumber = GetRegionNumberByCadastralQuarter(table.Rows[i]["CadastralQuartal"].ParseToString()),
                        PropertyType = table.Rows[i]["PropertyType"].ParseToString(),
                        Group = string.IsNullOrEmpty(group) ? "Без группы" : group,
                        HasGroup = !string.IsNullOrEmpty(@group),
                        Count = 1
                    };
                    result.Add(dto);
                }
            }

            result =
                result.GroupBy(x => new { x.Distrinct, x.RegionNumber, x.PropertyType, x.Group, x.HasGroup }).Select(
                group => new NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesDto
                {
                    Distrinct = group.Key.Distrinct,
                    RegionNumber = group.Key.RegionNumber,
                    PropertyType = group.Key.PropertyType,
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
            var query = _statisticalDataService.GetQueryForUnitsByTasks(taskList, additionalJoins: new List<QSJoin>() { groupJoin });

            query.AddColumn(OMUnit.GetColumn(x => x.ObjectId, "ObjectId"));
            query.AddColumn(OMUnit.GetColumn(x => x.PropertyType_Code, nameof(NumberOfObjectsByAdministrativeDistrictsBySubjectDto.PropertyTypeCode)));

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

            var result = new List<NumberOfObjectsByAdministrativeDistrictsBySubjectDto>();
            if (table.Rows.Count != 0)
            {
	            var objectIds = new List<long>();
	            for (var i = 0; i < table.Rows.Count; i++)
	            {
		            objectIds.Add(table.Rows[i]["ObjectId"].ParseToLong());
	            }

	            var gbuAttributes = _gbuObjectService.GetAllAttributes(objectIds,
		            new List<long> { buildingPurposeAttr.RegisterId, placementPurposeAttr.RegisterId },
		            new List<long> { buildingPurposeAttr.Id, placementPurposeAttr.Id },
		            DateTime.Now.GetEndOfTheDay());

                for (var i = 0; i < table.Rows.Count; i++)
                {
                    var group = table.Rows[i]["ParentGroup"]
                        .ParseToStringNullable();
                    var dto = new NumberOfObjectsByAdministrativeDistrictsBySubjectDto
                    {
                        PropertyTypeCode = (PropertyTypes)table.Rows[i][nameof(NumberOfObjectsByAdministrativeDistrictsBySubjectDto.PropertyTypeCode)].ParseToLong(),
                        PropertyType = ((PropertyTypes)table.Rows[i][nameof(NumberOfObjectsByAdministrativeDistrictsBySubjectDto.PropertyTypeCode)].ParseToLong()).GetEnumDescription(),
                        GbuObjectId = table.Rows[i]["ObjectId"].ParseToLongNullable(),
                        Group = string.IsNullOrEmpty(group) ? "Без группы" : group,
                        HasGroup = !string.IsNullOrEmpty(@group),
                        Count = 1
                    };

                    FillPurposeData(dto, gbuAttributes, buildingPurposeAttr, placementPurposeAttr);

                    if (!dto.HasPurpose || dto.HasPurpose && dto.Purpose != null)
                    {
	                    result.Add(dto);
                    }
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
            var query = new QSQuery
            {
                MainRegisterID = OMUnit.GetRegisterId(),
                Condition = new QSConditionGroup
                {
                    Type = QSConditionGroupType.And,
                    Conditions = new List<QSCondition>
                    {
                        new QSConditionSimple(OMTask.GetColumn(x => x.Id), QSConditionType.In, taskList.Select(x => (double)x).ToList())
                    }
                },
                Joins = new List<QSJoin>
                {
                    new QSJoin
                    {
                        RegisterId = OMTask.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMUnit.GetColumn(x => x.TaskId),
                            RightOperand = OMTask.GetColumn(x => x.Id)
                        },
                        JoinType = QSJoinType.Inner
                    },
                    new QSJoin
                    {
                        RegisterId = OMQuartalDictionary.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMUnit.GetColumn(x => x.CadastralBlock),
                            RightOperand = OMQuartalDictionary.GetColumn(x => x.CadastralQuartal)
                        },
                        JoinType = QSJoinType.Inner
                    },
                    new QSJoin
                    {
	                    RegisterId = OMGroup.GetRegisterId(),
	                    JoinCondition = new QSConditionSimple
	                    {
		                    ConditionType = QSConditionType.Equal,
		                    LeftOperand = OMUnit.GetColumn(x => x.GroupId),
		                    RightOperand = OMGroup.GetColumn(x => x.Id)
	                    },
	                    JoinType = QSJoinType.Left
                    },
                }
            };

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
