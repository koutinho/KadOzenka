using System;
using System.Collections.Generic;
using KadOzenka.Dal.Enum;
using ObjectModel.Gbu.GroupingAlgoritm;

namespace KadOzenka.Dal.GbuObject.Dto
{
    /// <summary>
    /// Настройки группировки
    /// </summary>
    public struct GroupingSettings
    {
        /// <summary>
        /// Идентификатор задания ЦОД
        /// </summary>
        public long? IdCodJob;
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
        public List<UnitChangeStatus> UnitChangeStatus;
        /// <summary>
        /// Настройки 1 уровня группировки
        /// </summary>
        public LevelItem Level1;
        /// <summary>
        /// Настройки 2 уровня группировки
        /// </summary>
        public LevelItem Level2;
        /// <summary>
        /// Настройки 3 уровня группировки
        /// </summary>
        public LevelItem Level3;
        /// <summary>
        /// Настройки 4 уровня группировки
        /// </summary>
        public LevelItem Level4;
        /// <summary>
        /// Настройки 5 уровня группировки
        /// </summary>
        public LevelItem Level5;
        /// <summary>
        /// Настройки 6 уровня группировки
        /// </summary>
        public LevelItem Level6;
        /// <summary>
        /// Настройки 7 уровня группировки
        /// </summary>
        public LevelItem Level7;
        /// <summary>
        /// Настройки 8 уровня группировки
        /// </summary>
        public LevelItem Level8;
        /// <summary>
        /// Настройки 9 уровня группировки
        /// </summary>
        public LevelItem Level9;
        /// <summary>
        /// Настройки 10 уровня группировки
        /// </summary>
        public LevelItem Level10;
        /// <summary>
        /// Настройки 11 уровня группировки
        /// </summary>
        public LevelItem Level11;

        /// <summary>
        /// Идентификатор атрибута, куда будет записан результат 
        /// </summary>
        public long? IdAttributeResult;
        /// <summary>
        /// Идентификатор атрибута, куда будут записаны источники 
        /// </summary>
        public long? IdAttributeSource;
        /// <summary>
        /// Идентификатор атрибута, куда будут записаны документы 
        /// </summary>
        public long? IdAttributeDocument;
        /// <summary>
        /// Дата на которую делается группировка 
        /// </summary>
        public DateTime? DateActual;
    }
}
