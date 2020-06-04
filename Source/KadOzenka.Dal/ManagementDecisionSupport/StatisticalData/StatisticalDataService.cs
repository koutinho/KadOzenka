using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Market;
using Core.Register;
using Core.Register.RegisterEntities;
using DevExpress.DataProcessing;
using ObjectModel.Core.Register;
using ObjectModel.Gbu;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
    public class StatisticalDataService
    {
        private long? _rosreestrRegisterId;

        public long RosreestrRegisterId
        {
            get
            {
                if (_rosreestrRegisterId != null)
                    return _rosreestrRegisterId.Value;

                var omMainObjectRegisterId = OMMainObject.GetRegisterId();
                _rosreestrRegisterId = OMRegister
                    .Where(x => x.MainRegister == omMainObjectRegisterId &&
                                x.RegisterDescription == "Источник: Росреестр").ExecuteFirstOrDefault().RegisterId;

                return _rosreestrRegisterId.Value;
            }
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

        public QSQuery GetQueryForUnitsByTasks(long[] taskIdList, params QSCondition[] additionalConditions)
        {
            var conditions = new List<QSCondition>
            {
                new QSConditionSimple(OMTask.GetColumn(x => x.Id), QSConditionType.In, taskIdList.Select(x => (double) x).ToList())
            };
            additionalConditions?.ForEach(x => conditions.Add(x));

            var query = new QSQuery
            {
                MainRegisterID = OMUnit.GetRegisterId(),
                Condition = new QSConditionGroup
                {
                    Type = QSConditionGroupType.And,
                    Conditions = conditions
                }
            };

            return query;
        }

        #region Rosreestr Attributes

        /// <summary>
        /// Аттрибут "Наименование объекта"
        /// </summary>
        public RegisterAttribute GetRosreestrObjectNameAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Наименование объекта");
        }

        /// <summary>
        /// Аттрибут "Наименование земельного участка"
        /// </summary>
        public RegisterAttribute GetRosreestrParcelNameAttribute()
        {
	        return GetRegisterAttributeByName(RosreestrRegisterId, "Наименование земельного участка");
        }

        /// <summary>
        /// Аттрибут "Назначение сооружения"
        /// </summary>
        public RegisterAttribute GetRosreestrConstructionPurposeAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Назначение сооружения");
        }

        /// <summary>
        /// Аттрибут "Адрес"
        /// </summary>
        public RegisterAttribute GetRosreestrAddressAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Адрес");
        }

        /// <summary>
        /// Аттрибут "Местоположение"
        /// </summary>
        public RegisterAttribute GetRosreestrLocationAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Местоположение");
        }

        /// <summary>
        /// Аттрибут "Земельный участок"
        /// </summary>
        public RegisterAttribute GetRosreestrParcelAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Земельный участок");
        }

        /// <summary>
        /// Аттрибут "Год постройки"
        /// </summary>
        public RegisterAttribute GetRosreestrBuildYearAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Год постройки");
        }

        /// <summary>
        /// Аттрибут "Год ввода в эксплуатацию"
        /// </summary>
        public RegisterAttribute GetRosreestrCommissioningYearAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Год ввода в эксплуатацию");
        }

        /// <summary>
        /// Аттрибут "Количество этажей"
        /// </summary>
        public RegisterAttribute GetRosreestrFloorsNumberAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Количество этажей");
        }

        /// <summary>
        /// Аттрибут "Количество подземных этажей"
        /// </summary>
        public RegisterAttribute GetRosreestrUndergroundFloorsNumberAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Количество подземных этажей");
        }

        /// <summary>
        /// Аттрибут "Этаж"
        /// </summary>
        public RegisterAttribute GetRosreestrFloorAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Этаж");
        }

        /// <summary>
        /// Аттрибут "Материал стен"
        /// </summary>
        public RegisterAttribute GetRosreestrWallMaterialAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Материал стен");
        }

        /// <summary>
        /// Аттрибут "Вид использования по документам"
        /// </summary>
        public RegisterAttribute GetRosreestrTypeOfUseByDocumentsAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Вид использования по документам");
        }

        /// <summary>
        /// Аттрибут "Категория земель"
        /// </summary>
        public RegisterAttribute GetRosreestrParcelCategoryAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Категория земель");
        }

        /// <summary>
        /// Аттрибут "Площадь"
        /// </summary>
        public RegisterAttribute GetRosreestrSquareAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Площадь");
        }

        /// <summary>
        /// Аттрибут "Дата образования"
        /// </summary>
        public RegisterAttribute GetRosreestrFormationDateAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Дата образования");
        }

        /// <summary>
        /// Аттрибут "Назначение здания"
        /// </summary>
        public RegisterAttribute GetRosreestrBuildingPurposeAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Назначение здания");
        }

        #endregion


        #region Helpers

        protected string GetRegionNumberByCadastralQuarter(string cadastralQuartal)
        {
	        var delimeterIndex = cadastralQuartal.IndexOf(':', cadastralQuartal.IndexOf(':') + 1);
	        return cadastralQuartal.Substring(0, delimeterIndex);
        }

        protected decimal? GetUpksCalcValue<T>(UpksCalcType upksCalcType, List<T> dtoList) where T: UpksCalcDto
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

        private RegisterAttribute GetRegisterAttributeByName(long registerId, string name)
        {
            return RegisterCache.RegisterAttributes.Values.First(x => x.RegisterId == registerId && x.Name.Equals(name));
        }

        #endregion Helpers
    }
}
