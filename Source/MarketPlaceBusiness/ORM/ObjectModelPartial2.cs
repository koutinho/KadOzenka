using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.ObjectModel.CustomAttribute;
using System.Xml.Serialization;


namespace ObjectModel.Market
{
    /// <summary>
    /// 100 Аналоги
    /// </summary>
    public partial class OMCoreObject
    {
        /// <summary>
        /// Ссылка на (200 Объекты недвижимости)
        /// </summary>
        //[ParentRegister]
        //[XmlIgnore]
        //public ObjectModel.Gbu.OMMainObject ParentMainObject { get; set; }

    }
}


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


namespace ObjectModel.Market
{
    /// <summary>
    /// 113 Таблица, содержащая ретроспективу цен по объектам с учетом корректировки на комнатность
    /// </summary>
    public partial class OMPriceAfterCorrectionByRoomsHistory
    {
        /// <summary>
        /// Ссылка на (100 Аналоги)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Market.OMCoreObject ParentCoreObject { get; set; }

    }
}
