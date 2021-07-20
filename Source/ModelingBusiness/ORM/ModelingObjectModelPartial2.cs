using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.ObjectModel.CustomAttribute;
using System.Xml.Serialization;


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
        /// Ссылка на (264 Моделирование. Справочники)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.KO.OMModelingDictionary ParentModelingDictionary { get; set; }

        /// <summary>
        /// Ссылка на (931 Список показателей реестра)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Core.Register.OMAttribute ParentAttribute { get; set; }

    }
}


namespace ObjectModel.KO
{
    /// <summary>
    /// 223 Картинки с результатами обучения модели
    /// </summary>
    public partial class OMModelTrainingResultImages
    {
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
    /// 265 Моделирование. Значения справочников
    /// </summary>
    public partial class OMModelingDictionariesValues
    {
        /// <summary>
        /// Ссылка на (264 Моделирование. Справочники)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.KO.OMModelingDictionary ParentModelingDictionary { get; set; }

    }
}
