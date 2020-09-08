using System;
using System.Collections.Generic;
using ObjectModel.Directory;

namespace KadOzenka.Dal.GbuObject.Dto
{
    /// <summary>
    /// Настройки простой гармонизации
    /// </summary>
    public abstract class ABaseHarmonizationSettings
    {
        /// <summary>
        /// Идентификатор атрибута, куда будет записан результат 
        /// </summary>
        public long IdAttributeResult;
        /// <summary>
        /// Тип объекта 
        /// </summary>
        public PropertyTypes PropertyType;
        ///// <summary>
        ///// Назначение здания для типа объекта "Здание"
        ///// </summary>
        //public BuildingPurpose BuildingPurpose;
        /// <summary>
        /// Выборка по всем объектам
        /// </summary>
        public bool SelectAllObject;
        /// <summary>
        /// Идентификатор аттрибута - фильтра
        /// </summary>
        public long? IdAttributeFilter;
        /// <summary>
        /// Список значений фильтра
        /// </summary>
        public List<string> ValuesFilter;
        /// <summary>
        /// Список заданий на оценку
        /// </summary>
        public List<long> TaskFilter;

        /// <summary>
        /// Фактор 1 уровня 
        /// </summary>
        public long? Level1Attribute;
        /// <summary>
        /// Фактор 2 уровня 
        /// </summary>
        public long? Level2Attribute;
        /// <summary>
        /// Фактор 3 уровня 
        /// </summary>
        public long? Level3Attribute;
        /// <summary>
        /// Фактор 4 уровня 
        /// </summary>
        public long? Level4Attribute;
        /// <summary>
        /// Фактор 5 уровня 
        /// </summary>
        public long? Level5Attribute;
        /// <summary>
        /// Фактор 6 уровня 
        /// </summary>
        public long? Level6Attribute;
        /// <summary>
        /// Фактор 7 уровня 
        /// </summary>
        public long? Level7Attribute;
        /// <summary>
        /// Фактор 8 уровня 
        /// </summary>
        public long? Level8Attribute;
        /// <summary>
        /// Фактор 9 уровня 
        /// </summary>
        public long? Level9Attribute;
        /// <summary>
        /// Фактор 10 уровня 
        /// </summary>
        public long? Level10Attribute;
        /// <summary>
        /// Факторы, добавленные юзером
        /// </summary>
        public List<AdditionalLevelsForHarmonization> AdditionalLevels;
        /// <summary>
        /// Дата на которую делается гармонизация 
        /// </summary>
        public DateTime? DateActual;
    }

    public struct AdditionalLevelsForHarmonization
    {
        public int LevelNumber { get; set; }
        public long? AttributeId { get; set; }
    }

    /// <summary>
    /// Настройки простой гармонизации
    /// </summary>
    public class HarmonizationSettings : ABaseHarmonizationSettings
    {

    }

    /// <summary>
    /// Настройки гармонизации с использованием справочника ЦОД
    /// </summary>
    public class HarmonizationCODSettings : ABaseHarmonizationSettings
    {
        /// <summary>
        /// Идентификатор задания ЦОД
        /// </summary>
        public long? IdCodJob;
        /// <summary>
        /// Значение по умолчанию 
        /// </summary>
        public string DefaultValue;
        /// <summary>
        /// Документ для значения по умолчанию 
        /// </summary>
        public long? IdDocument;
    }
}
