using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Market;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
    public class StatisticalDataService
    {
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
            query.AddColumn(OMGroup.GetColumn(x => x.GroupName, "Group"));

            var table = query.ExecuteQuery();

            var result = new List<NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesDto>();
            if (table.Rows.Count != 0)
            {
                for (var i = 0; i < table.Rows.Count; i++)
                {
	                var group = table.Rows[i][nameof(NumberOfObjectsByAdministrativeDistrictsBySubjectDto.Group)]
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

            query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, nameof(NumberOfObjectsByAdministrativeDistrictsBySubjectDto.PropertyType)));
            query.AddColumn(OMGroup.GetColumn(x => x.GroupName, nameof(NumberOfObjectsByAdministrativeDistrictsBySubjectDto.Group)));

            var table = query.ExecuteQuery();

            var result = new List<NumberOfObjectsByAdministrativeDistrictsBySubjectDto>();
            if (table.Rows.Count != 0)
            {
                for (var i = 0; i < table.Rows.Count; i++)
                {
	                var group = table.Rows[i][nameof(NumberOfObjectsByAdministrativeDistrictsBySubjectDto.Group)]
		                .ParseToStringNullable();
                    var dto = new NumberOfObjectsByAdministrativeDistrictsBySubjectDto
                    {
                        PropertyType = table.Rows[i][nameof(NumberOfObjectsByAdministrativeDistrictsBySubjectDto.PropertyType)].ParseToString(),
                        Group = string.IsNullOrEmpty(group) ? "Без группы" : group,
                        HasGroup = !string.IsNullOrEmpty(@group),
                        Count = 1
                    };
                    result.Add(dto);
                }
            }

            result =
                result.GroupBy(x => new { x.PropertyType, x.Group, x.HasGroup }).Select(
                group => new NumberOfObjectsByAdministrativeDistrictsBySubjectDto
                {
                    PropertyType = group.Key.PropertyType,
                    Group = group.Key.Group,
                    HasGroup = group.Key.HasGroup,
                    Count = group.ToList().DefaultIfEmpty().Sum(x => x.Count)
                }).ToList().OrderBy(x => x.HasGroup).ToList();

            return result;
        }

        public List<NumberOfObjectsByAdministrativeDistrictsByGroupsDto> GetNumberOfObjectsByAdministrativeDistrictsByGroups(long[] taskList)
        {
            throw new NotImplementedException();
        }

        public List<NumberOfObjectsByGroupsDto> GetNumberOfObjectsByGroups(long[] taskList, bool isOksReportType)
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

            QSCondition propertyTypeCondition;
            if (isOksReportType)
            {
	            propertyTypeCondition = new QSConditionGroup()
	            {
		            Conditions = new List<QSCondition>
		            {
			            new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code),
				            QSConditionType.NotEqual, (long)PropertyTypes.Stead),
			            new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code),
				            QSConditionType.NotEqual, (long)PropertyTypes.None),
                    },
		            Type = QSConditionGroupType.And
                };
            }
            else
            {
	            propertyTypeCondition = new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code),
		            QSConditionType.Equal, (long)PropertyTypes.Stead);
            }
            var conditionGroup = new QSConditionGroup(QSConditionGroupType.And);
            conditionGroup.Conditions = new List<QSCondition> { propertyTypeCondition, query.Condition };
            query.Condition = conditionGroup;

            query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
            query.AddColumn(OMGroup.GetColumn(x => x.GroupName, "Group"));

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
				            OMGroup.GetColumn(x => x.ParentId)){
				            RightOperandLevel = 1
			            }
		            }
	            }
            };
            query.AddColumn(subQuery, "ParentGroup");

            var table = query.ExecuteQuery();

            var result = new List<NumberOfObjectsByGroupsDto>();
            if (table.Rows.Count != 0)
            {
                for (var i = 0; i < table.Rows.Count; i++)
                {
                    var dto = new NumberOfObjectsByGroupsDto
                    {
	                    PropertyType = table.Rows[i]["PropertyType"].ParseToString(),
                        Group = table.Rows[i]["Group"].ParseToStringNullable(),
                        ParentGroup = table.Rows[i]["ParentGroup"].ParseToStringNullable(),
                        Count = 1
                    };
                    result.Add(dto);
                }
            }

            foreach (var dto in result.Where(x => string.IsNullOrEmpty(x.Group) || string.IsNullOrEmpty(x.ParentGroup)))
            {
	            if (string.IsNullOrEmpty(dto.Group))
	            {
		            dto.Group = "Без группы";
		            dto.HasGroup = false;
	            }
	            if (string.IsNullOrEmpty(dto.ParentGroup))
	            {
		            dto.ParentGroup = "Без группы";
		            dto.HasParentGroup = false;
	            }
            }

            result =
                result.GroupBy(x => new { x.PropertyType, x.Group, x.ParentGroup }).Select(
                group => new NumberOfObjectsByGroupsDto
                {
	                PropertyType = group.Key.PropertyType,
                    Group = group.Key.Group,
                    ParentGroup = group.Key.ParentGroup,
                    Count = group.ToList().DefaultIfEmpty().Sum(x => x.Count)
                }).ToList().OrderBy(x => x.HasParentGroup).ThenBy(x => x.HasGroup).ToList();

            return result;
        }

        public List<MinMaxAverageUPKSByAdministrativeDistrictsDto> GetMinMaxAverageUPKSByAdministrativeDistricts(long[] taskList, MinMaxAverageUPKSByAdministrativeDistrictsType reportType)
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
                    }
                }
            };

            query.AddColumn(OMQuartalDictionary.GetColumn(x => x.CadastralQuartal, "CadastralQuartal"));
            query.AddColumn(OMQuartalDictionary.GetColumn(x => x.Region_Code, "Region_Code"));
            query.AddColumn(OMQuartalDictionary.GetColumn(x => x.District_Code, "District_Code"));
            query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
            query.AddColumn(OMUnit.GetColumn(x => x.Upks, "ObjectUpks"));

            var table = query.ExecuteQuery();

            var data = new List<MinMaxAverageUPKSByAdministrativeDistrictsObjectDto>();
            if (table.Rows.Count != 0)
            {
                for (var i = 0; i < table.Rows.Count; i++)
                {
	                var dto = new MinMaxAverageUPKSByAdministrativeDistrictsObjectDto
                    {
	                    PropertyType = table.Rows[i]["PropertyType"].ParseToString(),
                        ObjectUpks = table.Rows[i]["ObjectUpks"].ParseToDecimalNullable(),
                        //TODO: ObjectWeigth MUST BE CLARIFIED
                        ObjectWeigth = 1
                    };

	                switch (reportType)
	                {
                        case MinMaxAverageUPKSByAdministrativeDistrictsType.ByDistricts:
	                        dto.Name = ((Hunteds) table.Rows[i]["District_Code"].ParseToLong()).GetShortTitle();
                            break;
                        case MinMaxAverageUPKSByAdministrativeDistrictsType.ByCarastralRegions:
	                        dto.Name = GetRegionNumberByCadastralQuarter(table.Rows[i]["CadastralQuartal"].ParseToString());
                            break;
                        case MinMaxAverageUPKSByAdministrativeDistrictsType.ByRegions:
	                        dto.Name = ((Districts) table.Rows[i]["Region_Code"].ParseToLong()).GetEnumDescription();
                            dto.AdditionalName = ((Hunteds)table.Rows[i]["District_Code"].ParseToLong()).GetShortTitle();
                            break;
	                }

                    data.Add(dto);
                }
            }

            var dataGrouped =
	            data.GroupBy(x => new {x.Name, x.PropertyType});

            var result = new List<MinMaxAverageUPKSByAdministrativeDistrictsDto>();
            foreach (var @group in dataGrouped)
            {
	            var groupValues = @group.ToList();
	            var upksCalcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>();
                foreach (var upksCalcType in upksCalcTypes)
                {
	                var dto = new MinMaxAverageUPKSByAdministrativeDistrictsDto
	                {
		                AdditionalName = groupValues.First().AdditionalName,
		                Name = @group.Key.Name,
		                ObjectsCount = groupValues.Count,
		                PropertyType = @group.Key.PropertyType,
                        UpksCalcType = upksCalcType,
                        UpksCalcValue = GetUpksCalcValue(upksCalcType, groupValues)
                    };

                    result.Add(dto);
                }
            }

            return result;
        }

        #region Helpers

        private string GetRegionNumberByCadastralQuarter(string cadastralQuartal)
        {
	        var delimeterIndex = cadastralQuartal.IndexOf(':', cadastralQuartal.IndexOf(':') + 1);
	        return cadastralQuartal.Substring(0, delimeterIndex);
        }

        private decimal? GetUpksCalcValue(UpksCalcType upksCalcType, List<MinMaxAverageUPKSByAdministrativeDistrictsObjectDto> dtoList)
        {
	        decimal? result = null;
	        switch (upksCalcType)
	        {
		        case UpksCalcType.Min:
			        result = dtoList.Min(x => x.ObjectUpks);
			        break;
		        case UpksCalcType.Max:
			        result = dtoList.Max(x => x.ObjectUpks);
			        break;
		        case UpksCalcType.Average:
			        result = dtoList.Average(x => x.ObjectUpks);
			        break;
		        case UpksCalcType.AverageWeight:
			        var sum = dtoList.Sum(x => x.ObjectUpks * x.ObjectWeigth);
			        var weightSum = dtoList.Sum(x => x.ObjectWeigth);
                    result = weightSum != 0 ? sum / weightSum : null;
			        break;
	        }

	        return result;
        }

        #endregion Helpers
    }
}
