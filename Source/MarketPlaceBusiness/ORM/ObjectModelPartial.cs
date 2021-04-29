using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.ObjectModel.CustomAttribute;

namespace ObjectModel.Market
{
    /// <summary>
    /// 100 Аналоги
    /// </summary>
    public partial class OMCoreObject
    {


        /// <summary>
        /// Ссылка на (101 Адреса в яндек-формате)
        /// </summary>
        [Reference]
        public List<ObjectModel.Market.OMYandexAddress> YandexAddress { get; set; }

        /// <summary>
        /// Ссылка на (105 Таблица, содержащая ретроспективу цен по объектам)
        /// </summary>
        [Reference]
        public List<ObjectModel.Market.OMPriceHistory> PriceHistory { get; set; }

        /// <summary>
        /// Ссылка на (113 Таблица, содержащая ретроспективу цен по объектам с учетом корректировки на комнатность)
        /// </summary>
        [Reference]
        public List<ObjectModel.Market.OMPriceAfterCorrectionByRoomsHistory> PriceAfterCorrectionByRoomsHistory { get; set; }
        public OMCoreObject()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            YandexAddress = new List<ObjectModel.Market.OMYandexAddress>();

            PriceHistory = new List<ObjectModel.Market.OMPriceHistory>();

            PriceAfterCorrectionByRoomsHistory = new List<ObjectModel.Market.OMPriceAfterCorrectionByRoomsHistory>();

        }
        public OMCoreObject(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 101 Адреса в яндек-формате
    /// </summary>
    public partial class OMYandexAddress
    {

        public OMYandexAddress()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMYandexAddress(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 105 Таблица, содержащая ретроспективу цен по объектам
    /// </summary>
    public partial class OMPriceHistory
    {

        public OMPriceHistory()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMPriceHistory(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 113 Таблица, содержащая ретроспективу цен по объектам с учетом корректировки на комнатность
    /// </summary>
    public partial class OMPriceAfterCorrectionByRoomsHistory
    {

        public OMPriceAfterCorrectionByRoomsHistory()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMPriceAfterCorrectionByRoomsHistory(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}