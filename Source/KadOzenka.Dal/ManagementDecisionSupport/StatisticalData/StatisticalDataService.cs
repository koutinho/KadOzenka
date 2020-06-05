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

        public QSQuery GetQueryForUnitsByTasks(long[] taskIdList, List<QSCondition> additionalConditions = null, List<QSJoin> additionalJoins = null)
        {
            var conditions = new List<QSCondition>
            {
                new QSConditionSimple(OMUnit.GetColumn(x => x.TaskId), QSConditionType.In, taskIdList.Select(x => (double) x).ToList())
            };
            additionalConditions?.ForEach(x => conditions.Add(x));

            var query = new QSQuery
            {
                MainRegisterID = OMUnit.GetRegisterId(),
                Condition = new QSConditionGroup
                {
                    Type = QSConditionGroupType.And,
                    Conditions = conditions
                },
                Joins = additionalJoins
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

        /// <summary>
        /// Аттрибут "Процент готовности"
        /// </summary>
        public RegisterAttribute GetRosreestrReadinessPercentageAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Процент готовности");
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
