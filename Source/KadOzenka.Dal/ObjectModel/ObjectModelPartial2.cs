using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.ObjectModel.CustomAttribute;
using System.Xml.Serialization;


namespace ObjectModel.Market
{
    /// <summary>
    /// 101 Таблица, содержащая объекты полученные с ЦИАНа
    /// </summary>
    public partial class OMCianObject
    {
        /// <summary>
        /// Ссылка на (100 Таблица, содержащая объекты аналоги)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Market.OMCoreObject ParentCoreObject { get; set; }

    }
}


namespace ObjectModel.Market
{
    /// <summary>
    /// 102 Таблица, содержащая объекты полученные с авито
    /// </summary>
    public partial class OMAvitoObject
    {
        /// <summary>
        /// Ссылка на (100 Таблица, содержащая объекты аналоги)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Market.OMCoreObject ParentCoreObject { get; set; }

    }
}
