using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.ObjectModel.CustomAttribute;
using System.Xml.Serialization;


namespace ObjectModel.KO
{
    /// <summary>
    /// 201 Единица кадастровой оценки
    /// </summary>
    public partial class OMUnit
    {
        /// <summary>
        /// Ссылка на (200 Объект кадастровой оценки)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.KO.OMMainObject ParentMainObject { get; set; }

        /// <summary>
        /// Ссылка на (202 Тур оценки)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.KO.OMTour ParentTour { get; set; }

        /// <summary>
        /// Ссылка на (203 Задание на оценку)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.KO.OMTask ParentTask { get; set; }

        /// <summary>
        /// Ссылка на (205 Группы/Подгруппы)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.KO.OMGroup ParentGroup { get; set; }

        /// <summary>
        /// Ссылка на (206 Модель)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.KO.OMModel ParentModel { get; set; }

    }
}


namespace ObjectModel.KO
{
    /// <summary>
    /// 203 Задание на оценку
    /// </summary>
    public partial class OMTask
    {
        /// <summary>
        /// Ссылка на (202 Тур оценки)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.KO.OMTour ParentTour { get; set; }

        /// <summary>
        /// Ссылка на (204 Документы)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.KO.OMDocument ParentDocument { get; set; }

    }
}


namespace ObjectModel.KO
{
    /// <summary>
    /// 206 Модель
    /// </summary>
    public partial class OMModel
    {
        /// <summary>
        /// Ссылка на (205 Группы/Подгруппы)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.KO.OMGroup ParentGroup { get; set; }

    }
}


namespace ObjectModel.KO
{
    /// <summary>
    /// 207 Модель тура
    /// </summary>
    public partial class OMTourModel
    {
        /// <summary>
        /// Ссылка на (202 Тур оценки)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.KO.OMTour ParentTour { get; set; }

        /// <summary>
        /// Ссылка на (206 Модель)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.KO.OMModel ParentModel { get; set; }

    }
}


namespace ObjectModel.KO
{
    /// <summary>
    /// 208 Факторы группы
    /// </summary>
    public partial class OMGroupFactor
    {
        /// <summary>
        /// Ссылка на (205 Группы/Подгруппы)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.KO.OMGroup ParentGroup { get; set; }

    }
}


namespace ObjectModel.KO
{
    /// <summary>
    /// 210 Факторы модели
    /// </summary>
    public partial class OMModelFactor
    {
        /// <summary>
        /// Ссылка на (206 Модель)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.KO.OMModel ParentModel { get; set; }

        /// <summary>
        /// Ссылка на (211 Справочник меток)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.KO.OMMarkCatalog ParentMarkCatalog { get; set; }

    }
}
