using System;
using System.Linq;
using Core.Register;
using Core.Shared.Extensions;
using ObjectModel.Core.Register;
using ObjectModel.Gbu;
using Core.Register.RegisterEntities;

namespace KadOzenka.Dal.Registers
{
    public class RosreestrRegisterService
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

        #region Rosreestr Attributes

        /// <summary>
        /// Аттрибут "Наименование объекта"
        /// </summary>
        public RegisterAttribute GetObjectNameAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Наименование объекта");
        }

        /// <summary>
        /// Аттрибут "Наименование земельного участка"
        /// </summary>
        public RegisterAttribute GetParcelNameAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Наименование земельного участка");
        }

        /// <summary>
        /// Аттрибут "Назначение сооружения"
        /// </summary>
        public RegisterAttribute GetConstructionPurposeAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Назначение сооружения");
        }

        /// <summary>
        /// Аттрибут "Адрес"
        /// </summary>
        public RegisterAttribute GetAddressAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Адрес");
        }

        /// <summary>
        /// Аттрибут "Местоположение"
        /// </summary>
        public RegisterAttribute GetLocationAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Местоположение");
        }

        /// <summary>
        /// Аттрибут "Земельный участок"
        /// </summary>
        public RegisterAttribute GetParcelAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Земельный участок");
        }

        /// <summary>
        /// Аттрибут "Год постройки"
        /// </summary>
        public RegisterAttribute GetBuildYearAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Год постройки");
        }

        /// <summary>
        /// Аттрибут "Год ввода в эксплуатацию"
        /// </summary>
        public RegisterAttribute GetCommissioningYearAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Год ввода в эксплуатацию");
        }

        /// <summary>
        /// Аттрибут "Количество этажей"
        /// </summary>
        public RegisterAttribute GetFloorsNumberAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Количество этажей");
        }

        /// <summary>
        /// Аттрибут "Количество подземных этажей"
        /// </summary>
        public RegisterAttribute GetUndergroundFloorsNumberAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Количество подземных этажей");
        }

        /// <summary>
        /// Аттрибут "Этаж"
        /// </summary>
        public RegisterAttribute GetFloorAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Этаж");
        }

        /// <summary>
        /// Аттрибут "Материал стен"
        /// </summary>
        public RegisterAttribute GetWallMaterialAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Материал стен");
        }

        /// <summary>
        /// Аттрибут "Вид использования по документам"
        /// </summary>
        public RegisterAttribute GetTypeOfUseByDocumentsAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Вид использования по документам");
        }

        /// <summary>
        /// Аттрибут "Вид использования по классификатору"
        /// </summary>
        public RegisterAttribute GetTypeOfUseByClassifierAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Вид использования по классификатору");
        }

        /// <summary>
        /// Аттрибут "Категория земель"
        /// </summary>
        public RegisterAttribute GetParcelCategoryAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Категория земель");
        }

        /// <summary>
        /// Аттрибут "Площадь"
        /// </summary>
        public RegisterAttribute GetSquareAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Площадь");
        }

        /// <summary>
        /// Аттрибут "Дата образования"
        /// </summary>
        public RegisterAttribute GetFormationDateAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Дата образования");
        }

        /// <summary>
        /// Аттрибут "Назначение здания"
        /// </summary>
        public RegisterAttribute GetBuildingPurposeAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Назначение здания");
        }

        /// <summary>
        /// Аттрибут "Процент готовности"
        /// </summary>
        public RegisterAttribute GetReadinessPercentageAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Процент готовности");
        }

        /// <summary>
        /// Аттрибут "Назначение помещения"
        /// </summary>
        public RegisterAttribute GetPlacementPurposeAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Назначение помещения");
        }

        /// <summary>
        /// Аттрибут "Кадастровый номер здания или сооружения, в котором расположено помещение"
        /// </summary>
        public RegisterAttribute GetParentCadastralNumberAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Кадастровый номер здания или сооружения, в котором расположено помещение");
        }

        /// <summary>
        /// Аттрибут "Наименование объекта"
        /// </summary>
        public RegisterAttribute GetObjectNameNumberAttribute()
        {
            return GetRegisterAttributeByName(RosreestrRegisterId, "Наименование объекта");
        }

        #endregion

        #region Support Methods

        private long GetFirstAttributeId(long registerId)
        {
            return registerId * Math.Pow(10, (8 - Math.Floor(Math.Log10(registerId) + 1))).ParseToLong() + 100;
        }

        private RegisterAttribute GetRegisterAttributeByName(long registerId, string name)
        {
            var attribute = RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.RegisterId == registerId && x.Name == name);
            if (attribute == null)
                throw new Exception($"Не найден аттрибут Росреестра с именем '{name}'");

            return attribute;
        }

        #endregion
    }
}
