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
        public OMCoreObject()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            YandexAddress = new List<ObjectModel.Market.OMYandexAddress>();

            PriceHistory = new List<ObjectModel.Market.OMPriceHistory>();

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