using System.Collections.Generic;
using KadOzenka.Dal.Enum;

namespace KadOzenka.Dal.GbuObject.Dto
{
    /// <summary>
    /// Настройки наследования атрибутов
    /// </summary>
    public struct GbuInheritanceAttributeSettings
    {
        /// <summary>
        /// Список заданий на оценку
        /// </summary>
        public List<long> TaskFilter;

        /// <summary>
        /// Список статусов Единицы оценки после обновления
        /// </summary>
        public List<ObjectChangeStatus> ObjectChangeStatus;

        /// <summary>
        /// Тип наследования: Кадастровый квартал -> Земельный участок
        /// </summary>
        public bool CadastralBlockToParcel;

        /// <summary>
        /// Тип наследования: Кадастровый квартал -> Здание
        /// </summary>
        public bool CadastralBlockToBuilding;

        /// <summary>
        /// Тип наследования: Кадастровый квартал -> Сооружение
        /// </summary>
        public bool CadastralBlockToConstruction;

        /// <summary>
        /// Тип наследования: Кадастровый квартал -> Объект незавершенного строительства
        /// </summary>
        public bool CadastralBlockToUncomplited;

        /// <summary>
        /// Тип наследования: Земельный участок -> Здание
        /// </summary>
        public bool ParcelToBuilding;

        /// <summary>
        /// Тип наследования: Земельный участок -> Сооружение
        /// </summary>
        public bool ParcelToConstruction;

        /// <summary>
        /// Тип наследования: Земельный участок -> Объект незавершенного строительства
        /// </summary>
        public bool ParcelToUncomplited;

        /// <summary>
        /// Тип наследования: Здание -> Помещение
        /// </summary>
        public bool BuildToFlat;

        /// <summary>
        /// Фактор, содержащий родительский кадастровый номер, по которому осуществляется сопоставление с родительским объектом
        /// </summary>
        public long ParentCadastralNumberAttribute;

        /// <summary>
        /// Список выбранных атрибутов
        /// </summary>
        public List<long> Attributes;
    }
}
