using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.KO;
using Core.Register;
using Core.Register.RegisterEntities;
using KadOzenka.Dal.Tours;
using ObjectModel.Core.Register;
using ObjectModel.Directory;
using ObjectModel.Gbu;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
    public class StatisticalDataService
    {
        private TourFactorService TourFactorService { get; set; }

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

        public StatisticalDataService()
        {
            TourFactorService = new TourFactorService();
        }

        public string SqlQueriesFolder => "SqlQueries";

        public FileStream GetSqlQueryFileStream(string fileName)
        {
	        return Core.ConfigParam.Configuration.GetFileStream(fileName, "sql", SqlQueriesFolder);
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

        public string GetRegionNumberByCadastralQuarter(string cadastralQuartal)
        {
	        var delimeterIndex = cadastralQuartal.IndexOf(':', cadastralQuartal.IndexOf(':') + 1);
	        return cadastralQuartal.Substring(0, delimeterIndex);
        }

        public decimal? GetCalcValue<T>(UpksCalcType upksCalcType, List<T> dtoList) where T : CalcDto
        {
	        decimal? result = null;
	        switch (upksCalcType)
	        {
		        case UpksCalcType.Min:
			        result = dtoList.Min(x => x.ObjectValue);
			        break;
		        case UpksCalcType.Max:
			        result = dtoList.Max(x => x.ObjectValue);
			        break;
		        case UpksCalcType.Average:
			        result = dtoList.Average(x => x.ObjectValue);
			        break;
		        case UpksCalcType.AverageWeight:
                    var totalCost = dtoList.Sum(x => x.ObjectCost);
                    var totalSquare = dtoList.Sum(x => x.ObjectSquare);
                    result = totalSquare != 0 ? totalCost / totalSquare : null;
			        break;
	        }

	        return result;
        }


        #region Tour Settings

        public RegisterAttribute GetGroupAttributeFromTourSettings(long tourId)
        {
            return TourFactorService.GetTourAttributeFromSettings(tourId, KoAttributeUsingType.CodeGroupAttribute);
        }

        public RegisterAttribute GetObjectTypeAttributeFromTourSettings(long tourId)
        {
            return TourFactorService.GetTourAttributeFromSettings(tourId, KoAttributeUsingType.TypeRoomAttribute);
        }

        public RegisterAttribute GetCadastralQuartalAttributeFromTourSettings(long tourId)
        {
            return TourFactorService.GetTourAttributeFromSettings(tourId, KoAttributeUsingType.CodeQuarterAttribute);
        }

        #endregion


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
        /// Аттрибут "Вид использования по классификатору"
        /// </summary>
        public RegisterAttribute GetRosreestrTypeOfUseByClassifierAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Вид использования по классификатору");
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

        /// <summary>
        /// Аттрибут "Назначение помещения"
        /// </summary>
        public RegisterAttribute GetRosreestrPlacementPurposeAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Назначение помещения");
        }

        /// <summary>
        /// Аттрибут "Кадастровый номер здания или сооружения, в котором расположено помещение"
        /// </summary>
        public RegisterAttribute GetRosreestrParentCadastralNumberAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Кадастровый номер здания или сооружения, в котором расположено помещение");
        }

        /// <summary>
        /// Аттрибут "Наименование объекта"
        /// </summary>
        public RegisterAttribute GetRosreestrObjectNameNumberAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Наименование объекта");
        }

        #endregion


        #region Support Methods

        private RegisterAttribute GetRegisterAttributeByName(long registerId, string name)
        {
            var attribute = RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.RegisterId == registerId && x.Name == name);
            if (attribute == null)
                throw new Exception($"Не найден аттрибут Росреестра с именем '{name}'");

            return attribute;
        }

        #endregion Helpers
    }
}
