//doc:MapConfiguration.xml
using Core.Shared.Extensions;
using System;
using System.Xml.Serialization;

namespace CIPJS.DAL.Building
{
    /// <summary>Карта конфигурации</summary>
    /// <include>Включает класс конфигурационного атрибута EntityAttribute</include>
    /// <exception>Исключений не вызывает</exception>
    /// <see>Используется в CIPJS.DAL.Building.BuildingService. Пример использования см. там же </see>
    [Serializable()]
    [XmlRoot("MapConfiguration", Namespace = "http://genix.pro", IsNullable = false)]
    public class MapConfiguration
    {
        /// <value>Перечень конфигурируемых атрибутов</value>
        [XmlArray("EntityAttributes")]
        public EntityAttribute[] EntityAttributes;
    }

    /// <summary>Конфигурационный атрибут объекта InsurBuilding</summary>
    [Serializable()]
    public class EntityAttribute
    {
        /// <value>ID атрибута сущности модели</value>
        public string OmEntityAttrId;

        /// <value>Описание атрибута</value>
        public string Description;

        /// <value>Тип значения. Возможные варианты: A, C, P (атрибут, константа, процедура)</value>
        public string Type;

        /// <value>Значение константы</value>
        public string Const;

		/// <value>Значение константы - справочный код</value>
		public long? ConstCode;

		/// <value>Наименование процедуры</value>
		public string Proc;

        /// <value>Id атрибута Источника 1</value>
        public string Source1AttrId;

        /// <value>Id атрибута Источника 2</value>
        public string Source2AttrId;

        /// <value>Id атрибута Источника 3</value>
        public string Source3AttrId;

        /// <value>Признак заполнения значения атрибута модели атрибутом Источника 2, если Источник 1 не задан</value>
        public bool UseSource2Sign;

        /// <value>Признак заполнения значения атрибута модели атрибутом Источника 3, если Источник 2 не задан</value>
        public bool UseSource3Sign;

        /// <value>Признак обновления значения атрибута модели при обновлении значения атрибута в соответствующм Источнике данных</value>
        public bool EnableUpdateSign;
    }
}