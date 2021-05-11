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
    /// 107 Таблица, содержащая информацию о соответствии кад кварталов районам, округам и зонам 
    /// </summary>
    public partial class OMQuartalDictionary
    {

        public OMQuartalDictionary()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMQuartalDictionary(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 108 Индексы для корректировки на дату
    /// </summary>
    public partial class OMIndexesForDateCorrection
    {

        public OMIndexesForDateCorrection()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMIndexesForDateCorrection(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 110 Временная таблица для проведения проверки механизма отбора дублей
    /// </summary>
    public partial class OMCoreObjectTest
    {

        public OMCoreObjectTest()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMCoreObjectTest(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 111 Таблица, содержащая коэффициенты на квартиры по зданиям для корректировки на комнатность
    /// </summary>
    public partial class OMCoefficientsForCorrectionByRooms
    {

        public OMCoefficientsForCorrectionByRooms()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMCoefficientsForCorrectionByRooms(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 112 Таблица, содержащая историю изменения цены после корректировки на этажность
    /// </summary>
    public partial class OMPriceCorrectionByStageHistory
    {

        public OMPriceCorrectionByStageHistory()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMPriceCorrectionByStageHistory(bool trackPropertyChanging) : this()
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

namespace ObjectModel.Market
{
    /// <summary>
    /// 114 Таблица, хранящая отношения цен первого этажа к верхним этажам
    /// </summary>
    public partial class OMCoefficientsForFirstFloorCorr
    {

        public OMCoefficientsForFirstFloorCorr()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMCoefficientsForFirstFloorCorr(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 115 Таблица, хранящая историю цен первых этажей
    /// </summary>
    public partial class OMPriceForFirstFloorHistory
    {

        public OMPriceForFirstFloorHistory()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMPriceForFirstFloorHistory(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 116 Таблица, содержащая ретроспективу цен по объектам с учетом корректировки на дату
    /// </summary>
    public partial class OMPriceAfterCorrectionByDateHistory
    {

        public OMPriceAfterCorrectionByDateHistory()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMPriceAfterCorrectionByDateHistory(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 117 Таблица, содержащая настройки для коэффициентов корректировок
    /// </summary>
    public partial class OMCorrectionSettings
    {

        public OMCorrectionSettings()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMCorrectionSettings(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 118 Таблица, содержащая коэффициенты для проверки на выбросы
    /// </summary>
    public partial class OMCoefficientsOutliersChecking
    {

        public OMCoefficientsOutliersChecking()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMCoefficientsOutliersChecking(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 119 Таблица, содержащая информацию о проведённых проверках на выбросы
    /// </summary>
    public partial class OMOutliersCheckingHistory
    {

        public OMOutliersCheckingHistory()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMOutliersCheckingHistory(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}