using System;
using System.Collections.Generic;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.Models.Filters;
using ObjectModel.Gbu.GroupingAlgoritm;

namespace KadOzenka.Dal.GbuObject.Dto
{
    /// <summary>
    /// Настройки группировки
    /// </summary>
    public struct GroupingSettingsFinal
    {
        /// <summary>
        /// Выборка по всем объектам
        /// </summary>
        public bool SelectAllObject;
        /// <summary>
        /// Список заданий на оценку
        /// </summary>
        public List<long> TaskFilter;
        /// <summary>
        /// Список статусов Единиц Оценки
        /// </summary>
        public List<ObjectChangeStatus> ObjectChangeStatus;
        /// <summary>
        /// Идентификатор атрибута-источника
        /// </summary>
        public long? IdAttributeSource;

        /// <summary>
        /// Идентификатор атрибута для разделения между 2 кодами
        /// </summary>
        public long? IdAttributeFor2Selections;

        /// <summary>
        /// Фильтр 1 для разделения между 2 кодами
        /// </summary>
        public Filters Filter1ForSelectionBetween2;

        /// <summary>
        /// Фильтр 2 для разделения между 2 кодами
        /// </summary>
        public Filters Filter2ForSelectionBetween2;

        /// <summary>
        /// Идентификатор атрибута для разделения между 3 кодами
        /// </summary>
        public long? IdAttributeFor3Selections;

        /// <summary>
        /// Фильтр 1 для разделения между 3 кодами
        /// </summary>
        public Filters Filter1ForSelectionBetween3;

        /// <summary>
        /// Фильтр 2 для разделения между 3 кодами
        /// </summary>
        public Filters Filter2ForSelectionBetween3;

        /// <summary>
        /// Фильтр 3 для разделения между 3 кодами
        /// </summary>
        public Filters Filter3ForSelectionBetween3;


        /// <summary>
        /// Идентификатор атрибута, куда будет записан результат 
        /// </summary>
        public long? IdAttributeResult;
        /// <summary>
        /// Дата на которую делается группировка 
        /// </summary>
        public DateTime? DateActual;
    }
}
