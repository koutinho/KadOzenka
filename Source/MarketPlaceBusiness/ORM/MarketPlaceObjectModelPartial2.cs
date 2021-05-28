using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.ObjectModel.CustomAttribute;
using System.Xml.Serialization;


namespace ObjectModel.Market
{
    /// <summary>
    /// 101 Адреса в яндек-формате
    /// </summary>
    public partial class OMYandexAddress
    {
        /// <summary>
        /// Ссылка на (100 Аналоги)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Market.OMCoreObject ParentCoreObject { get; set; }

    }
}


namespace ObjectModel.Market
{
    /// <summary>
    /// 105 Таблица, содержащая ретроспективу цен по объектам
    /// </summary>
    public partial class OMPriceHistory
    {
        /// <summary>
        /// Ссылка на (100 Аналоги)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Market.OMCoreObject ParentCoreObject { get; set; }

    }
}
