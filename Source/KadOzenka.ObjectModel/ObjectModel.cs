using System;
using Core.ObjectModel;
using Core.ObjectModel.CustomAttribute;
using Core.Shared.Extensions;
using ObjectModel.Directory;
namespace ObjectModel.Gbu
{
    /// <summary>
    /// 1 Источник: Неопределено (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 1)]
    [Serializable]
    public partial class OMSource1 : OMBaseClass<OMSource1>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 2 Источник: Росреестр (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 2)]
    [Serializable]
    public partial class OMSource2 : OMBaseClass<OMSource2>
    {

        private long _id;
        /// <summary>
        /// 200100 ИД (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 200100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 3 Источник: Департамент экономической политики и развития города Москвы (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 3)]
    [Serializable]
    public partial class OMSource3 : OMBaseClass<OMSource3>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 4 Источник: МосгорБТИ (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 4)]
    [Serializable]
    public partial class OMSource4 : OMBaseClass<OMSource4>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 5 Источник: Департамент городского имущества города Москвы (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 5)]
    [Serializable]
    public partial class OMSource5 : OMBaseClass<OMSource5>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 6 Источник: Департамент жилищно-коммунального хозяйства города Москвы (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 6)]
    [Serializable]
    public partial class OMSource6 : OMBaseClass<OMSource6>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 7 Источник: Департамент культурного наследия города Москвы (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 7)]
    [Serializable]
    public partial class OMSource7 : OMBaseClass<OMSource7>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 8 Источник: Департамент спорта и туризма города Москвы (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 8)]
    [Serializable]
    public partial class OMSource8 : OMBaseClass<OMSource8>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 9 Источник: Департамент природопользования и охраны окружающей среды города Москвы (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 9)]
    [Serializable]
    public partial class OMSource9 : OMBaseClass<OMSource9>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 10 Источник: Росавтодор (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 10)]
    [Serializable]
    public partial class OMSource10 : OMBaseClass<OMSource10>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 11 Источник: Департамент капитального ремонта города Москвы (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 11)]
    [Serializable]
    public partial class OMSource11 : OMBaseClass<OMSource11>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 12 Источник: Комитет по архитектуре и градостроительству города Москвы (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 12)]
    [Serializable]
    public partial class OMSource12 : OMBaseClass<OMSource12>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 13 Источник: Управление Росреестра по Москве (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 13)]
    [Serializable]
    public partial class OMSource13 : OMBaseClass<OMSource13>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 14 Источник: ГБУ (ЦОД) (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 14)]
    [Serializable]
    public partial class OMSource14 : OMBaseClass<OMSource14>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 15 Источник: ГКО 2014 (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 15)]
    [Serializable]
    public partial class OMSource15 : OMBaseClass<OMSource15>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 16 Источник: ГКО 2016 (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 16)]
    [Serializable]
    public partial class OMSource16 : OMBaseClass<OMSource16>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 17 Источник: Правительство города Москвы (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 17)]
    [Serializable]
    public partial class OMSource17 : OMBaseClass<OMSource17>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 18 Источник: Сведения о судебных разбирательствах (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 18)]
    [Serializable]
    public partial class OMSource18 : OMBaseClass<OMSource18>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 19 Источник: Комиссии по расмотрению споров (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 19)]
    [Serializable]
    public partial class OMSource19 : OMBaseClass<OMSource19>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 20 Источник: Наименование объектов по предыдущим турам (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 20)]
    [Serializable]
    public partial class OMSource20 : OMBaseClass<OMSource20>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 21 Источник: Расчет ЦФ (ГБУ) (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 21)]
    [Serializable]
    public partial class OMSource21 : OMBaseClass<OMSource21>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 22 Источник: Наименования адресов и гаражей (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 22)]
    [Serializable]
    public partial class OMSource22 : OMBaseClass<OMSource22>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 23 Источник: ГКО 2018 (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 23)]
    [Serializable]
    public partial class OMSource23 : OMBaseClass<OMSource23>
    {
    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 24 Реестр хранения реестров с характеристиками объекта (KO_OBJECTS_CHARACTERISTICS_REGISTER)
    /// </summary>
    [RegisterInfo(RegisterID = 24)]
    [Serializable]
    public partial class OMObjectsCharacteristicsRegister : OMBaseClass<OMObjectsCharacteristicsRegister>
    {

        private long _id;
        /// <summary>
        /// 2400100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 2400100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _registerid;
        /// <summary>
        /// 2400200 Идентификатор регистра (REGISTER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 2400200)]
        public long? RegisterId
        {
            get
            {
                CheckPropertyInited("RegisterId");
                return _registerid;
            }
            set
            {
                _registerid = value;
                NotifyPropertyChanged("RegisterId");
            }
        }

    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 25 Источник 25 (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 25)]
    [Serializable]
    public partial class OMSource25 : OMBaseClass<OMSource25>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 28 Источник 28 (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 28)]
    [Serializable]
    public partial class OMSource28 : OMBaseClass<OMSource28>
    {

        private long _id;
        /// <summary>
        /// 1717 ИД (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 1717)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 29 Источник 29 (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 29)]
    [Serializable]
    public partial class OMSource29 : OMBaseClass<OMSource29>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 30 Источник 30 (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 30)]
    [Serializable]
    public partial class OMSource30 : OMBaseClass<OMSource30>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 44 Источник 44 (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 44)]
    [Serializable]
    public partial class OMSource44 : OMBaseClass<OMSource44>
    {
    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 80 Справочник кадастровых кварталов (GBU_KADASTR_KVARTAL)
    /// </summary>
    [RegisterInfo(RegisterID = 80)]
    [Serializable]
    public partial class OMKadastrKvartal : OMBaseClass<OMKadastrKvartal>
    {

        private long _id;
        /// <summary>
        /// 8000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 8000100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _kadastrkvartal;
        /// <summary>
        /// 8000200 Кадастровый квартал (KADASTR_KVARTAL)
        /// </summary>
        [RegisterAttribute(AttributeID = 8000200)]
        public string KadastrKvartal
        {
            get
            {
                CheckPropertyInited("KadastrKvartal");
                return _kadastrkvartal;
            }
            set
            {
                _kadastrkvartal = value;
                NotifyPropertyChanged("KadastrKvartal");
            }
        }


        private long _parentid;
        /// <summary>
        /// 8000300 ИД родителя (PARENT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 8000300)]
        public long ParentId
        {
            get
            {
                CheckPropertyInited("ParentId");
                return _parentid;
            }
            set
            {
                _parentid = value;
                NotifyPropertyChanged("ParentId");
            }
        }


        private long? _typeterritory2017;
        /// <summary>
        /// 8000400 Показатель за 2017 год (TYPE_TERRITORY_2017)
        /// </summary>
        [RegisterAttribute(AttributeID = 8000400)]
        public long? TypeTerritory2017
        {
            get
            {
                CheckPropertyInited("TypeTerritory2017");
                return _typeterritory2017;
            }
            set
            {
                _typeterritory2017 = value;
                NotifyPropertyChanged("TypeTerritory2017");
            }
        }


        private long? _typeterritory2020;
        /// <summary>
        /// 8000500 Показатель за 2020 год (TYPE_TERRITORY_2020)
        /// </summary>
        [RegisterAttribute(AttributeID = 8000500)]
        public long? TypeTerritory2020
        {
            get
            {
                CheckPropertyInited("TypeTerritory2020");
                return _typeterritory2020;
            }
            set
            {
                _typeterritory2020 = value;
                NotifyPropertyChanged("TypeTerritory2020");
            }
        }

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 100 Аналоги (MARKET_CORE_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 100)]
    [Serializable]
    public partial class OMCoreObject : OMBaseClass<OMCoreObject>
    {

        private long _id;
        /// <summary>
        /// 10002000 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 10002000)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _url;
        /// <summary>
        /// 10002100 URL-адрес объявления (URL)
        /// </summary>
        [RegisterAttribute(AttributeID = 10002100)]
        public string Url
        {
            get
            {
                CheckPropertyInited("Url");
                return _url;
            }
            set
            {
                _url = value;
                NotifyPropertyChanged("Url");
            }
        }


        private string _market;
        /// <summary>
        /// 10002300 Источник информации (MARKET)
        /// </summary>
        [RegisterAttribute(AttributeID = 10002300)]
        public string Market
        {
            get
            {
                CheckPropertyInited("Market");
                return _market;
            }
            set
            {
                _market = value;
                NotifyPropertyChanged("Market");
            }
        }


        private MarketTypes _market_Code;
        /// <summary>
        /// 10002300 Источник информации (справочный код) (MARKET_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10002300)]
        public MarketTypes Market_Code
        {
            get
            {
                CheckPropertyInited("Market_Code");
                return this._market_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_market))
                    {
                         _market = descr;
                    }
                }
                else
                {
                     _market = descr;
                }

                this._market_Code = value;
                NotifyPropertyChanged("Market");
                NotifyPropertyChanged("Market_Code");
            }
        }


        private long? _marketid;
        /// <summary>
        /// 10002600 Идентификатор в Источнике данных (MARKET_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 10002600)]
        public long? MarketId
        {
            get
            {
                CheckPropertyInited("MarketId");
                return _marketid;
            }
            set
            {
                _marketid = value;
                NotifyPropertyChanged("MarketId");
            }
        }


        private decimal? _price;
        /// <summary>
        /// 10002700 Цена сделки/предложения (PRICE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10002700)]
        public decimal? Price
        {
            get
            {
                CheckPropertyInited("Price");
                return _price;
            }
            set
            {
                _price = value;
                NotifyPropertyChanged("Price");
            }
        }


        private decimal? _pricepermeter;
        /// <summary>
        /// 10002701 Цена за кв. м ()
        /// </summary>
        [RegisterAttribute(AttributeID = 10002701)]
        public decimal? PricePerMeter
        {
            get
            {
                CheckPropertyInited("PricePerMeter");
                return _pricepermeter;
            }
            set
            {
                _pricepermeter = value;
                NotifyPropertyChanged("PricePerMeter");
            }
        }


        private DateTime? _parsertime;
        /// <summary>
        /// 10002800 Дата предложения (сделки) (PARSER_TIME)
        /// </summary>
        [RegisterAttribute(AttributeID = 10002800)]
        public DateTime? ParserTime
        {
            get
            {
                CheckPropertyInited("ParserTime");
                return _parsertime;
            }
            set
            {
                _parsertime = value;
                NotifyPropertyChanged("ParserTime");
            }
        }


        private string _region;
        /// <summary>
        /// 10002900 Регион (REGION)
        /// </summary>
        [RegisterAttribute(AttributeID = 10002900)]
        public string Region
        {
            get
            {
                CheckPropertyInited("Region");
                return _region;
            }
            set
            {
                _region = value;
                NotifyPropertyChanged("Region");
            }
        }


        private string _city;
        /// <summary>
        /// 10003000 Город (CITY)
        /// </summary>
        [RegisterAttribute(AttributeID = 10003000)]
        public string City
        {
            get
            {
                CheckPropertyInited("City");
                return _city;
            }
            set
            {
                _city = value;
                NotifyPropertyChanged("City");
            }
        }


        private string _address;
        /// <summary>
        /// 10003100 Адресный ориентир (ADDRESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 10003100)]
        public string Address
        {
            get
            {
                CheckPropertyInited("Address");
                return _address;
            }
            set
            {
                _address = value;
                NotifyPropertyChanged("Address");
            }
        }


        private string _metro;
        /// <summary>
        /// 10003200 Наименование метро (METRO)
        /// </summary>
        [RegisterAttribute(AttributeID = 10003200)]
        public string Metro
        {
            get
            {
                CheckPropertyInited("Metro");
                return _metro;
            }
            set
            {
                _metro = value;
                NotifyPropertyChanged("Metro");
            }
        }


        private string _images;
        /// <summary>
        /// 10003300 URL-адреса изображений (IMAGES)
        /// </summary>
        [RegisterAttribute(AttributeID = 10003300)]
        public string Images
        {
            get
            {
                CheckPropertyInited("Images");
                return _images;
            }
            set
            {
                _images = value;
                NotifyPropertyChanged("Images");
            }
        }


        private string _description;
        /// <summary>
        /// 10003400 Текст объявления (DESCRIPTION)
        /// </summary>
        [RegisterAttribute(AttributeID = 10003400)]
        public string Description
        {
            get
            {
                CheckPropertyInited("Description");
                return _description;
            }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }


        private decimal? _lat;
        /// <summary>
        /// 10003500 Широта (LAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10003500)]
        public decimal? Lat
        {
            get
            {
                CheckPropertyInited("Lat");
                return _lat;
            }
            set
            {
                _lat = value;
                NotifyPropertyChanged("Lat");
            }
        }


        private string _dealtype;
        /// <summary>
        /// 10003600 Тип сделки (DEAL_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10003600)]
        public string DealType
        {
            get
            {
                CheckPropertyInited("DealType");
                return _dealtype;
            }
            set
            {
                _dealtype = value;
                NotifyPropertyChanged("DealType");
            }
        }


        private DealType _dealtype_Code;
        /// <summary>
        /// 10003600 Тип сделки (справочный код) (DEAL_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10003600)]
        public DealType DealType_Code
        {
            get
            {
                CheckPropertyInited("DealType_Code");
                return this._dealtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_dealtype))
                    {
                         _dealtype = descr;
                    }
                }
                else
                {
                     _dealtype = descr;
                }

                this._dealtype_Code = value;
                NotifyPropertyChanged("DealType");
                NotifyPropertyChanged("DealType_Code");
            }
        }


        private long? _roomscount;
        /// <summary>
        /// 10003900 Количество комнат (ROOMS_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10003900)]
        public long? RoomsCount
        {
            get
            {
                CheckPropertyInited("RoomsCount");
                return _roomscount;
            }
            set
            {
                _roomscount = value;
                NotifyPropertyChanged("RoomsCount");
            }
        }


        private decimal? _lng;
        /// <summary>
        /// 10004000 Долгота (LNG)
        /// </summary>
        [RegisterAttribute(AttributeID = 10004000)]
        public decimal? Lng
        {
            get
            {
                CheckPropertyInited("Lng");
                return _lng;
            }
            set
            {
                _lng = value;
                NotifyPropertyChanged("Lng");
            }
        }


        private long? _floornumber;
        /// <summary>
        /// 10004100 Номер этажа (FLOOR_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 10004100)]
        public long? FloorNumber
        {
            get
            {
                CheckPropertyInited("FloorNumber");
                return _floornumber;
            }
            set
            {
                _floornumber = value;
                NotifyPropertyChanged("FloorNumber");
            }
        }


        private long? _floorscount;
        /// <summary>
        /// 10004200 Этажность (FLOORS_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10004200)]
        public long? FloorsCount
        {
            get
            {
                CheckPropertyInited("FloorsCount");
                return _floorscount;
            }
            set
            {
                _floorscount = value;
                NotifyPropertyChanged("FloorsCount");
            }
        }


        private decimal? _area;
        /// <summary>
        /// 10004300 Общая площадь (AREA)
        /// </summary>
        [RegisterAttribute(AttributeID = 10004300)]
        public decimal? Area
        {
            get
            {
                CheckPropertyInited("Area");
                return _area;
            }
            set
            {
                _area = value;
                NotifyPropertyChanged("Area");
            }
        }


        private decimal? _areakitchen;
        /// <summary>
        /// 10004400 Площадь кухни (AREA_KITCHEN)
        /// </summary>
        [RegisterAttribute(AttributeID = 10004400)]
        public decimal? AreaKitchen
        {
            get
            {
                CheckPropertyInited("AreaKitchen");
                return _areakitchen;
            }
            set
            {
                _areakitchen = value;
                NotifyPropertyChanged("AreaKitchen");
            }
        }


        private decimal? _arealiving;
        /// <summary>
        /// 10004500 Жилая площадь (AREA_LIVING)
        /// </summary>
        [RegisterAttribute(AttributeID = 10004500)]
        public decimal? AreaLiving
        {
            get
            {
                CheckPropertyInited("AreaLiving");
                return _arealiving;
            }
            set
            {
                _arealiving = value;
                NotifyPropertyChanged("AreaLiving");
            }
        }


        private decimal? _arealand;
        /// <summary>
        /// 10004600 Площадь ЗУ (AREA_LAND)
        /// </summary>
        [RegisterAttribute(AttributeID = 10004600)]
        public decimal? AreaLand
        {
            get
            {
                CheckPropertyInited("AreaLand");
                return _arealand;
            }
            set
            {
                _arealand = value;
                NotifyPropertyChanged("AreaLand");
            }
        }


        private long? _buildingyear;
        /// <summary>
        /// 10004700 Год постройки (BUILDING_YEAR)
        /// </summary>
        [RegisterAttribute(AttributeID = 10004700)]
        public long? BuildingYear
        {
            get
            {
                CheckPropertyInited("BuildingYear");
                return _buildingyear;
            }
            set
            {
                _buildingyear = value;
                NotifyPropertyChanged("BuildingYear");
            }
        }


        private string _zoneregion;
        /// <summary>
        /// 10005100 Зона_Округ (ZONE_REGION)
        /// </summary>
        [RegisterAttribute(AttributeID = 10005100)]
        public string ZoneRegion
        {
            get
            {
                CheckPropertyInited("ZoneRegion");
                return _zoneregion;
            }
            set
            {
                _zoneregion = value;
                NotifyPropertyChanged("ZoneRegion");
            }
        }


        private string _customzone;
        /// <summary>
        /// 10005101 Настраиваемая зона (CUSTOM_ZONE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10005101)]
        public string CustomZone
        {
            get
            {
                CheckPropertyInited("CustomZone");
                return _customzone;
            }
            set
            {
                _customzone = value;
                NotifyPropertyChanged("CustomZone");
            }
        }


        private string _district;
        /// <summary>
        /// 10005200 Административный округ (DISTRICT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10005200)]
        public string District
        {
            get
            {
                CheckPropertyInited("District");
                return _district;
            }
            set
            {
                _district = value;
                NotifyPropertyChanged("District");
            }
        }


        private Hunteds _district_Code;
        /// <summary>
        /// 10005200 Административный округ (справочный код) (DISTRICT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10005200)]
        public Hunteds District_Code
        {
            get
            {
                CheckPropertyInited("District_Code");
                return this._district_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_district))
                    {
                         _district = descr;
                    }
                }
                else
                {
                     _district = descr;
                }

                this._district_Code = value;
                NotifyPropertyChanged("District");
                NotifyPropertyChanged("District_Code");
            }
        }


        private string _neighborhood;
        /// <summary>
        /// 10005300 Район (NEIGHBORHOOD)
        /// </summary>
        [RegisterAttribute(AttributeID = 10005300)]
        public string Neighborhood
        {
            get
            {
                CheckPropertyInited("Neighborhood");
                return _neighborhood;
            }
            set
            {
                _neighborhood = value;
                NotifyPropertyChanged("Neighborhood");
            }
        }


        private Districts _neighborhood_Code;
        /// <summary>
        /// 10005300 Район (справочный код) (NEIGHBORHOOD_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10005300)]
        public Districts Neighborhood_Code
        {
            get
            {
                CheckPropertyInited("Neighborhood_Code");
                return this._neighborhood_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_neighborhood))
                    {
                         _neighborhood = descr;
                    }
                }
                else
                {
                     _neighborhood = descr;
                }

                this._neighborhood_Code = value;
                NotifyPropertyChanged("Neighborhood");
                NotifyPropertyChanged("Neighborhood_Code");
            }
        }


        private string _cadastralnumber;
        /// <summary>
        /// 10005400 Кадастровый номер (CADASTRAL_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 10005400)]
        public string CadastralNumber
        {
            get
            {
                CheckPropertyInited("CadastralNumber");
                return _cadastralnumber;
            }
            set
            {
                _cadastralnumber = value;
                NotifyPropertyChanged("CadastralNumber");
            }
        }


        private string _buildingcadastralnumber;
        /// <summary>
        /// 10005500 Кадастровый номер здания (BUILDING_CADASTRAL_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 10005500)]
        public string BuildingCadastralNumber
        {
            get
            {
                CheckPropertyInited("BuildingCadastralNumber");
                return _buildingcadastralnumber;
            }
            set
            {
                _buildingcadastralnumber = value;
                NotifyPropertyChanged("BuildingCadastralNumber");
            }
        }


        private string _cadastralquartal;
        /// <summary>
        /// 10005600 Кадастровый квартал (CADASTRAL_QUARTAL)
        /// </summary>
        [RegisterAttribute(AttributeID = 10005600)]
        public string CadastralQuartal
        {
            get
            {
                CheckPropertyInited("CadastralQuartal");
                return _cadastralquartal;
            }
            set
            {
                _cadastralquartal = value;
                NotifyPropertyChanged("CadastralQuartal");
            }
        }


        private string _group;
        /// <summary>
        /// 10005700 Группа сегмента рынка (KO_GROUP)
        /// </summary>
        [RegisterAttribute(AttributeID = 10005700)]
        public string Group
        {
            get
            {
                CheckPropertyInited("Group");
                return _group;
            }
            set
            {
                _group = value;
                NotifyPropertyChanged("Group");
            }
        }


        private long? _group_Code;
        /// <summary>
        /// 10005700 Группа сегмента рынка (справочный код) (KO_GROUP_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10005700)]
        public long? Group_Code
        {
            get
            {
                CheckPropertyInited("Group_Code");
                return _group_Code;
            }
            set
            {
                _group_Code = value;
                NotifyPropertyChanged("Group_Code");
            }
        }


        private string _subgroup;
        /// <summary>
        /// 10005800 Подгруппа сегмента рынка (KO_SUBGROUP)
        /// </summary>
        [RegisterAttribute(AttributeID = 10005800)]
        public string Subgroup
        {
            get
            {
                CheckPropertyInited("Subgroup");
                return _subgroup;
            }
            set
            {
                _subgroup = value;
                NotifyPropertyChanged("Subgroup");
            }
        }


        private long? _subgroup_Code;
        /// <summary>
        /// 10005800 Подгруппа сегмента рынка (справочный код) (KO_SUBGROUP_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10005800)]
        public long? Subgroup_Code
        {
            get
            {
                CheckPropertyInited("Subgroup_Code");
                return _subgroup_Code;
            }
            set
            {
                _subgroup_Code = value;
                NotifyPropertyChanged("Subgroup_Code");
            }
        }


        private long? _zone;
        /// <summary>
        /// 10005900 Зона (ZONE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10005900)]
        public long? Zone
        {
            get
            {
                CheckPropertyInited("Zone");
                return _zone;
            }
            set
            {
                _zone = value;
                NotifyPropertyChanged("Zone");
            }
        }


        private string _processtype;
        /// <summary>
        /// 10006000 Статус (PROCESS_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10006000)]
        public string ProcessType
        {
            get
            {
                CheckPropertyInited("ProcessType");
                return _processtype;
            }
            set
            {
                _processtype = value;
                NotifyPropertyChanged("ProcessType");
            }
        }


        private ProcessStep _processtype_Code;
        /// <summary>
        /// 10006000 Статус (справочный код) (PROCESS_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10006000)]
        public ProcessStep ProcessType_Code
        {
            get
            {
                CheckPropertyInited("ProcessType_Code");
                return this._processtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_processtype))
                    {
                         _processtype = descr;
                    }
                }
                else
                {
                     _processtype = descr;
                }

                this._processtype_Code = value;
                NotifyPropertyChanged("ProcessType");
                NotifyPropertyChanged("ProcessType_Code");
            }
        }


        private string _exclusionstatus;
        /// <summary>
        /// 10006001 Причина исключения (EXCLUSION_STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 10006001)]
        public string ExclusionStatus
        {
            get
            {
                CheckPropertyInited("ExclusionStatus");
                return _exclusionstatus;
            }
            set
            {
                _exclusionstatus = value;
                NotifyPropertyChanged("ExclusionStatus");
            }
        }


        private ExclusionStatus _exclusionstatus_Code;
        /// <summary>
        /// 10006001 Причина исключения (справочный код) (EXCLUSION_STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10006001)]
        public ExclusionStatus ExclusionStatus_Code
        {
            get
            {
                CheckPropertyInited("ExclusionStatus_Code");
                return this._exclusionstatus_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_exclusionstatus))
                    {
                         _exclusionstatus = descr;
                    }
                }
                else
                {
                     _exclusionstatus = descr;
                }

                this._exclusionstatus_Code = value;
                NotifyPropertyChanged("ExclusionStatus");
                NotifyPropertyChanged("ExclusionStatus_Code");
            }
        }


        private string _phonenumber;
        /// <summary>
        /// 10006500 Телефонный номер (PHONE_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 10006500)]
        public string PhoneNumber
        {
            get
            {
                CheckPropertyInited("PhoneNumber");
                return _phonenumber;
            }
            set
            {
                _phonenumber = value;
                NotifyPropertyChanged("PhoneNumber");
            }
        }


        private string _propertymarketsegment;
        /// <summary>
        /// 10007000 Сегмент рынка (PROPERTY_MARKET_SEGMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10007000)]
        public string PropertyMarketSegment
        {
            get
            {
                CheckPropertyInited("PropertyMarketSegment");
                return _propertymarketsegment;
            }
            set
            {
                _propertymarketsegment = value;
                NotifyPropertyChanged("PropertyMarketSegment");
            }
        }


        private MarketSegment _propertymarketsegment_Code;
        /// <summary>
        /// 10007000 Сегмент рынка (справочный код) (PROPERTY_MARKET_SEGMENT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10007000)]
        public MarketSegment PropertyMarketSegment_Code
        {
            get
            {
                CheckPropertyInited("PropertyMarketSegment_Code");
                return this._propertymarketsegment_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_propertymarketsegment))
                    {
                         _propertymarketsegment = descr;
                    }
                }
                else
                {
                     _propertymarketsegment = descr;
                }

                this._propertymarketsegment_Code = value;
                NotifyPropertyChanged("PropertyMarketSegment");
                NotifyPropertyChanged("PropertyMarketSegment_Code");
            }
        }


        private string _wallmaterial;
        /// <summary>
        /// 10007100 Материал стен (WALL_MATERIAL)
        /// </summary>
        [RegisterAttribute(AttributeID = 10007100)]
        public string WallMaterial
        {
            get
            {
                CheckPropertyInited("WallMaterial");
                return _wallmaterial;
            }
            set
            {
                _wallmaterial = value;
                NotifyPropertyChanged("WallMaterial");
            }
        }


        private WallMaterial _wallmaterial_Code;
        /// <summary>
        /// 10007100 Материал стен (справочный код) (WALL_MATERIAL_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10007100)]
        public WallMaterial WallMaterial_Code
        {
            get
            {
                CheckPropertyInited("WallMaterial_Code");
                return this._wallmaterial_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_wallmaterial))
                    {
                         _wallmaterial = descr;
                    }
                }
                else
                {
                     _wallmaterial = descr;
                }

                this._wallmaterial_Code = value;
                NotifyPropertyChanged("WallMaterial");
                NotifyPropertyChanged("WallMaterial_Code");
            }
        }


        private string _qualityclass;
        /// <summary>
        /// 10007200 Класс качества (QUALITY_CLASS)
        /// </summary>
        [RegisterAttribute(AttributeID = 10007200)]
        public string QualityClass
        {
            get
            {
                CheckPropertyInited("QualityClass");
                return _qualityclass;
            }
            set
            {
                _qualityclass = value;
                NotifyPropertyChanged("QualityClass");
            }
        }


        private QualityClass _qualityclass_Code;
        /// <summary>
        /// 10007200 Класс качества (справочный код) (QUALITY_CLASS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10007200)]
        public QualityClass QualityClass_Code
        {
            get
            {
                CheckPropertyInited("QualityClass_Code");
                return this._qualityclass_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_qualityclass))
                    {
                         _qualityclass = descr;
                    }
                }
                else
                {
                     _qualityclass = descr;
                }

                this._qualityclass_Code = value;
                NotifyPropertyChanged("QualityClass");
                NotifyPropertyChanged("QualityClass_Code");
            }
        }


        private decimal? _subwayspace;
        /// <summary>
        /// 10007300 Расстояние до ближайшей станции метро (SUBWAY_SPACE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10007300)]
        public decimal? SubwaySpace
        {
            get
            {
                CheckPropertyInited("SubwaySpace");
                return _subwayspace;
            }
            set
            {
                _subwayspace = value;
                NotifyPropertyChanged("SubwaySpace");
            }
        }


        private long? _regionid;
        /// <summary>
        /// 10007400 Код региона (REGION_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 10007400)]
        public long? RegionId
        {
            get
            {
                CheckPropertyInited("RegionId");
                return _regionid;
            }
            set
            {
                _regionid = value;
                NotifyPropertyChanged("RegionId");
            }
        }


        private long? _cityid;
        /// <summary>
        /// 10007500 ID города (CITY_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 10007500)]
        public long? CityId
        {
            get
            {
                CheckPropertyInited("CityId");
                return _cityid;
            }
            set
            {
                _cityid = value;
                NotifyPropertyChanged("CityId");
            }
        }


        private DateTime? _lastdateupdate;
        /// <summary>
        /// 10007600 Дата последнего обновления цены (LAST_DATE_UPDATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10007600)]
        public DateTime? LastDateUpdate
        {
            get
            {
                CheckPropertyInited("LastDateUpdate");
                return _lastdateupdate;
            }
            set
            {
                _lastdateupdate = value;
                NotifyPropertyChanged("LastDateUpdate");
            }
        }


        private string _propertytypescipjs;
        /// <summary>
        /// 10007700 Вид объекта недвижимости (PROPERTY_TYPETS_CIPJS)
        /// </summary>
        [RegisterAttribute(AttributeID = 10007700)]
        public string PropertyTypesCIPJS
        {
            get
            {
                CheckPropertyInited("PropertyTypesCIPJS");
                return _propertytypescipjs;
            }
            set
            {
                _propertytypescipjs = value;
                NotifyPropertyChanged("PropertyTypesCIPJS");
            }
        }


        private PropertyTypesCIPJS _propertytypescipjs_Code;
        /// <summary>
        /// 10007700 Вид объекта недвижимости (справочный код) (PROPERTY_TYPETS_CIPJS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10007700)]
        public PropertyTypesCIPJS PropertyTypesCIPJS_Code
        {
            get
            {
                CheckPropertyInited("PropertyTypesCIPJS_Code");
                return this._propertytypescipjs_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_propertytypescipjs))
                    {
                         _propertytypescipjs = descr;
                    }
                }
                else
                {
                     _propertytypescipjs = descr;
                }

                this._propertytypescipjs_Code = value;
                NotifyPropertyChanged("PropertyTypesCIPJS");
                NotifyPropertyChanged("PropertyTypesCIPJS_Code");
            }
        }


        private string _propertylawtype;
        /// <summary>
        /// 10007800 Вид передаваемых прав (PROPERTY_LAW_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10007800)]
        public string PropertyLawType
        {
            get
            {
                CheckPropertyInited("PropertyLawType");
                return _propertylawtype;
            }
            set
            {
                _propertylawtype = value;
                NotifyPropertyChanged("PropertyLawType");
            }
        }


        private string _propertypartsize;
        /// <summary>
        /// 10007900 Размер доли (PROPERTY_PART_SIZE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10007900)]
        public string PropertyPartSize
        {
            get
            {
                CheckPropertyInited("PropertyPartSize");
                return _propertypartsize;
            }
            set
            {
                _propertypartsize = value;
                NotifyPropertyChanged("PropertyPartSize");
            }
        }


        private decimal? _priceaftercorrectionbydate;
        /// <summary>
        /// 10008100 Цена после корректировки на дату (PRICE_AFTER_CORRECTION_BY_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008100)]
        public decimal? PriceAfterCorrectionByDate
        {
            get
            {
                CheckPropertyInited("PriceAfterCorrectionByDate");
                return _priceaftercorrectionbydate;
            }
            set
            {
                _priceaftercorrectionbydate = value;
                NotifyPropertyChanged("PriceAfterCorrectionByDate");
            }
        }


        private decimal? _priceaftercorrectionbybargain;
        /// <summary>
        /// 10008200 Цена после корректировки на торг (PRICE_AFTER_CORRECTION_BY_BARGAIN)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008200)]
        public decimal? PriceAfterCorrectionByBargain
        {
            get
            {
                CheckPropertyInited("PriceAfterCorrectionByBargain");
                return _priceaftercorrectionbybargain;
            }
            set
            {
                _priceaftercorrectionbybargain = value;
                NotifyPropertyChanged("PriceAfterCorrectionByBargain");
            }
        }


        private decimal? _priceaftercorrectionbyrooms;
        /// <summary>
        /// 10008400 Цена после корректировки на комнатность (PRICE_AFTER_CORRECTION_BY_ROOMS)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008400)]
        public decimal? PriceAfterCorrectionByRooms
        {
            get
            {
                CheckPropertyInited("PriceAfterCorrectionByRooms");
                return _priceaftercorrectionbyrooms;
            }
            set
            {
                _priceaftercorrectionbyrooms = value;
                NotifyPropertyChanged("PriceAfterCorrectionByRooms");
            }
        }


        private long? _formalizedaddressid;
        /// <summary>
        /// 10008600 Идентификатор формализованного адреса (FORMALIZED_ADDRESS_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008600)]
        public long? FormalizedAddressId
        {
            get
            {
                CheckPropertyInited("FormalizedAddressId");
                return _formalizedaddressid;
            }
            set
            {
                _formalizedaddressid = value;
                NotifyPropertyChanged("FormalizedAddressId");
            }
        }


        private decimal? _priceaftercorrectionforfirstfloor;
        /// <summary>
        /// 10008800 Цена после корректировки на первый этаж (PRICE_AFTER_CORRECTION_FOR_FIRST_FLOOR)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008800)]
        public decimal? PriceAfterCorrectionForFirstFloor
        {
            get
            {
                CheckPropertyInited("PriceAfterCorrectionForFirstFloor");
                return _priceaftercorrectionforfirstfloor;
            }
            set
            {
                _priceaftercorrectionforfirstfloor = value;
                NotifyPropertyChanged("PriceAfterCorrectionForFirstFloor");
            }
        }


        private decimal? _priceaftercorrectionbystage;
        /// <summary>
        /// 10008900 Цена после корректировки на цоколь/подвал (PRICE_AFTER_CORRECTION_BY_STAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008900)]
        public decimal? PriceAfterCorrectionByStage
        {
            get
            {
                CheckPropertyInited("PriceAfterCorrectionByStage");
                return _priceaftercorrectionbystage;
            }
            set
            {
                _priceaftercorrectionbystage = value;
                NotifyPropertyChanged("PriceAfterCorrectionByStage");
            }
        }


        private string _ownershiptype;
        /// <summary>
        /// 10009002 Вид права (OWNERSHIP_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009002)]
        public string OwnershipType
        {
            get
            {
                CheckPropertyInited("OwnershipType");
                return _ownershiptype;
            }
            set
            {
                _ownershiptype = value;
                NotifyPropertyChanged("OwnershipType");
            }
        }


        private string _placementtype;
        /// <summary>
        /// 10009003 Тип помещения (PLACEMENT_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009003)]
        public string PlacementType
        {
            get
            {
                CheckPropertyInited("PlacementType");
                return _placementtype;
            }
            set
            {
                _placementtype = value;
                NotifyPropertyChanged("PlacementType");
            }
        }


        private string _quality;
        /// <summary>
        /// 10009004 Состояние (QUALITY)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009004)]
        public string Quality
        {
            get
            {
                CheckPropertyInited("Quality");
                return _quality;
            }
            set
            {
                _quality = value;
                NotifyPropertyChanged("Quality");
            }
        }


        private bool? _isoperatingcostsincluded;
        /// <summary>
        /// 10009005 Эксплуатационные расходы включены (IS_OPERATING_COSTS_INCLUDED)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009005)]
        public bool? IsOperatingCostsIncluded
        {
            get
            {
                CheckPropertyInited("IsOperatingCostsIncluded");
                return _isoperatingcostsincluded;
            }
            set
            {
                _isoperatingcostsincluded = value;
                NotifyPropertyChanged("IsOperatingCostsIncluded");
            }
        }


        private bool? _isutilitiesincluded;
        /// <summary>
        /// 10009006 Коммунальные платежи включены (IS_UTILITIES_INCLUDED)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009006)]
        public bool? IsUtilitiesIncluded
        {
            get
            {
                CheckPropertyInited("IsUtilitiesIncluded");
                return _isutilitiesincluded;
            }
            set
            {
                _isutilitiesincluded = value;
                NotifyPropertyChanged("IsUtilitiesIncluded");
            }
        }


        private string _vat;
        /// <summary>
        /// 10009007 НДС (VAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009007)]
        public string Vat
        {
            get
            {
                CheckPropertyInited("Vat");
                return _vat;
            }
            set
            {
                _vat = value;
                NotifyPropertyChanged("Vat");
            }
        }


        private VatType _vat_Code;
        /// <summary>
        /// 10009007 НДС (справочный код) (VAT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009007)]
        public VatType Vat_Code
        {
            get
            {
                CheckPropertyInited("Vat_Code");
                return this._vat_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_vat))
                    {
                         _vat = descr;
                    }
                }
                else
                {
                     _vat = descr;
                }

                this._vat_Code = value;
                NotifyPropertyChanged("Vat");
                NotifyPropertyChanged("Vat_Code");
            }
        }


        private string _entrancetype;
        /// <summary>
        /// 10009008 Тип входа (ENTRANCE_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009008)]
        public string EntranceType
        {
            get
            {
                CheckPropertyInited("EntranceType");
                return _entrancetype;
            }
            set
            {
                _entrancetype = value;
                NotifyPropertyChanged("EntranceType");
            }
        }


        private string _renovation;
        /// <summary>
        /// 10009009 Состояние отделки (RENOVATION)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009009)]
        public string Renovation
        {
            get
            {
                CheckPropertyInited("Renovation");
                return _renovation;
            }
            set
            {
                _renovation = value;
                NotifyPropertyChanged("Renovation");
            }
        }


        private string _buildingline;
        /// <summary>
        /// 10009010 Линия застройки (BUILDING_LINE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009010)]
        public string BuildingLine
        {
            get
            {
                CheckPropertyInited("BuildingLine");
                return _buildingline;
            }
            set
            {
                _buildingline = value;
                NotifyPropertyChanged("BuildingLine");
            }
        }


        private decimal? _cct;
        /// <summary>
        /// 10009011 Коэффициент ценности территории (КЦТ) (CCT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009011)]
        public decimal? CCT
        {
            get
            {
                CheckPropertyInited("CCT");
                return _cct;
            }
            set
            {
                _cct = value;
                NotifyPropertyChanged("CCT");
            }
        }

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 101 Адреса в яндек-формате (MARKET_ADDRESS_YANDEX)
    /// </summary>
    [RegisterInfo(RegisterID = 101)]
    [Serializable]
    public partial class OMYandexAddress : OMBaseClass<OMYandexAddress>
    {

        private long _id;
        /// <summary>
        /// 10100100 Идентификатор адреса (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 10100100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _formalizedaddress;
        /// <summary>
        /// 10100200 Формализованный адрес (FORMALIZED_ADDRESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 10100200)]
        public string FormalizedAddress
        {
            get
            {
                CheckPropertyInited("FormalizedAddress");
                return _formalizedaddress;
            }
            set
            {
                _formalizedaddress = value;
                NotifyPropertyChanged("FormalizedAddress");
            }
        }


        private string _cadastralnumber;
        /// <summary>
        /// 10100300 Кадастровый номер (CADASTRAL_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 10100300)]
        public string CadastralNumber
        {
            get
            {
                CheckPropertyInited("CadastralNumber");
                return _cadastralnumber;
            }
            set
            {
                _cadastralnumber = value;
                NotifyPropertyChanged("CadastralNumber");
            }
        }


        private decimal? _lat;
        /// <summary>
        /// 10100400 Широта (LAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10100400)]
        public decimal? Lat
        {
            get
            {
                CheckPropertyInited("Lat");
                return _lat;
            }
            set
            {
                _lat = value;
                NotifyPropertyChanged("Lat");
            }
        }


        private decimal? _lng;
        /// <summary>
        /// 10100500 Долгота (LNG)
        /// </summary>
        [RegisterAttribute(AttributeID = 10100500)]
        public decimal? Lng
        {
            get
            {
                CheckPropertyInited("Lng");
                return _lng;
            }
            set
            {
                _lng = value;
                NotifyPropertyChanged("Lng");
            }
        }


        private string _country;
        /// <summary>
        /// 10100600 Страна (COUNTRY)
        /// </summary>
        [RegisterAttribute(AttributeID = 10100600)]
        public string Country
        {
            get
            {
                CheckPropertyInited("Country");
                return _country;
            }
            set
            {
                _country = value;
                NotifyPropertyChanged("Country");
            }
        }


        private string _province;
        /// <summary>
        /// 10100700 Федеральный округ (PROVINCE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10100700)]
        public string Province
        {
            get
            {
                CheckPropertyInited("Province");
                return _province;
            }
            set
            {
                _province = value;
                NotifyPropertyChanged("Province");
            }
        }


        private string _province2;
        /// <summary>
        /// 10100800 Уточнение округа (PROVINCE_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 10100800)]
        public string Province2
        {
            get
            {
                CheckPropertyInited("Province2");
                return _province2;
            }
            set
            {
                _province2 = value;
                NotifyPropertyChanged("Province2");
            }
        }


        private string _area;
        /// <summary>
        /// 10100900 Область (AREA)
        /// </summary>
        [RegisterAttribute(AttributeID = 10100900)]
        public string Area
        {
            get
            {
                CheckPropertyInited("Area");
                return _area;
            }
            set
            {
                _area = value;
                NotifyPropertyChanged("Area");
            }
        }


        private string _area2;
        /// <summary>
        /// 10101000 Уточнение области (AREA_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 10101000)]
        public string Area2
        {
            get
            {
                CheckPropertyInited("Area2");
                return _area2;
            }
            set
            {
                _area2 = value;
                NotifyPropertyChanged("Area2");
            }
        }


        private string _locality;
        /// <summary>
        /// 10101100 Район (LOCALITY)
        /// </summary>
        [RegisterAttribute(AttributeID = 10101100)]
        public string Locality
        {
            get
            {
                CheckPropertyInited("Locality");
                return _locality;
            }
            set
            {
                _locality = value;
                NotifyPropertyChanged("Locality");
            }
        }


        private string _locality2;
        /// <summary>
        /// 10101200 Уточнение района (LOCALITY_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 10101200)]
        public string Locality2
        {
            get
            {
                CheckPropertyInited("Locality2");
                return _locality2;
            }
            set
            {
                _locality2 = value;
                NotifyPropertyChanged("Locality2");
            }
        }


        private string _district;
        /// <summary>
        /// 10101300 Округ (DISTRICT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10101300)]
        public string District
        {
            get
            {
                CheckPropertyInited("District");
                return _district;
            }
            set
            {
                _district = value;
                NotifyPropertyChanged("District");
            }
        }


        private string _district2;
        /// <summary>
        /// 10101400 Уточнение округа (DISTRICT_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 10101400)]
        public string District2
        {
            get
            {
                CheckPropertyInited("District2");
                return _district2;
            }
            set
            {
                _district2 = value;
                NotifyPropertyChanged("District2");
            }
        }


        private string _district3;
        /// <summary>
        /// 10101500 Второе уточнение округа (DISTRICT_3)
        /// </summary>
        [RegisterAttribute(AttributeID = 10101500)]
        public string District3
        {
            get
            {
                CheckPropertyInited("District3");
                return _district3;
            }
            set
            {
                _district3 = value;
                NotifyPropertyChanged("District3");
            }
        }


        private string _airport;
        /// <summary>
        /// 10101600 Аэропорт (AIRPORT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10101600)]
        public string Airport
        {
            get
            {
                CheckPropertyInited("Airport");
                return _airport;
            }
            set
            {
                _airport = value;
                NotifyPropertyChanged("Airport");
            }
        }


        private string _vegetation;
        /// <summary>
        /// 10101700 Ориентир (VEGETATION)
        /// </summary>
        [RegisterAttribute(AttributeID = 10101700)]
        public string Vegetation
        {
            get
            {
                CheckPropertyInited("Vegetation");
                return _vegetation;
            }
            set
            {
                _vegetation = value;
                NotifyPropertyChanged("Vegetation");
            }
        }


        private string _route;
        /// <summary>
        /// 10101800 Путь (ROUTE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10101800)]
        public string Route
        {
            get
            {
                CheckPropertyInited("Route");
                return _route;
            }
            set
            {
                _route = value;
                NotifyPropertyChanged("Route");
            }
        }


        private string _railwaystation;
        /// <summary>
        /// 10101900 ЖД станция (RAILWAY_STATION)
        /// </summary>
        [RegisterAttribute(AttributeID = 10101900)]
        public string RailwayStation
        {
            get
            {
                CheckPropertyInited("RailwayStation");
                return _railwaystation;
            }
            set
            {
                _railwaystation = value;
                NotifyPropertyChanged("RailwayStation");
            }
        }


        private string _street;
        /// <summary>
        /// 10102000 Улица (STREET)
        /// </summary>
        [RegisterAttribute(AttributeID = 10102000)]
        public string Street
        {
            get
            {
                CheckPropertyInited("Street");
                return _street;
            }
            set
            {
                _street = value;
                NotifyPropertyChanged("Street");
            }
        }


        private string _house;
        /// <summary>
        /// 10102100 Дом (HOUSE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10102100)]
        public string House
        {
            get
            {
                CheckPropertyInited("House");
                return _house;
            }
            set
            {
                _house = value;
                NotifyPropertyChanged("House");
            }
        }


        private string _other;
        /// <summary>
        /// 10102200 Другое (OTHER)
        /// </summary>
        [RegisterAttribute(AttributeID = 10102200)]
        public string Other
        {
            get
            {
                CheckPropertyInited("Other");
                return _other;
            }
            set
            {
                _other = value;
                NotifyPropertyChanged("Other");
            }
        }


        private long? _initialid;
        /// <summary>
        /// 10102300 Идентификатор исходного объекта (INITIAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 10102300)]
        public long? InitialId
        {
            get
            {
                CheckPropertyInited("InitialId");
                return _initialid;
            }
            set
            {
                _initialid = value;
                NotifyPropertyChanged("InitialId");
            }
        }

    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 102 Адреса в формате Росреестра (GBU_ADDRESS_ROSREESTR)
    /// </summary>
    [RegisterInfo(RegisterID = 102)]
    [Serializable]
    public partial class OMAddressRosreestr : OMBaseClass<OMAddressRosreestr>
    {

        private long _id;
        /// <summary>
        /// 10200100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 10200100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _idfactor;
        /// <summary>
        /// 10200200 Идентификатор фактора (ID_FACTOR)
        /// </summary>
        [RegisterAttribute(AttributeID = 10200200)]
        public long? IdFactor
        {
            get
            {
                CheckPropertyInited("IdFactor");
                return _idfactor;
            }
            set
            {
                _idfactor = value;
                NotifyPropertyChanged("IdFactor");
            }
        }


        private long? _idobject;
        /// <summary>
        /// 10200300 Идентификатор объекта (ID_OBJECT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10200300)]
        public long? IdObject
        {
            get
            {
                CheckPropertyInited("IdObject");
                return _idobject;
            }
            set
            {
                _idobject = value;
                NotifyPropertyChanged("IdObject");
            }
        }


        private long? _iddocument;
        /// <summary>
        /// 10200400 Идентификатор документа (ID_DOCUMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10200400)]
        public long? IdDocument
        {
            get
            {
                CheckPropertyInited("IdDocument");
                return _iddocument;
            }
            set
            {
                _iddocument = value;
                NotifyPropertyChanged("IdDocument");
            }
        }


        private string _okato;
        /// <summary>
        /// 10200500 ОКАТО (OKATO)
        /// </summary>
        [RegisterAttribute(AttributeID = 10200500)]
        public string OKATO
        {
            get
            {
                CheckPropertyInited("OKATO");
                return _okato;
            }
            set
            {
                _okato = value;
                NotifyPropertyChanged("OKATO");
            }
        }


        private string _kladr;
        /// <summary>
        /// 10200600 КЛАДР (KLADR)
        /// </summary>
        [RegisterAttribute(AttributeID = 10200600)]
        public string KLADR
        {
            get
            {
                CheckPropertyInited("KLADR");
                return _kladr;
            }
            set
            {
                _kladr = value;
                NotifyPropertyChanged("KLADR");
            }
        }


        private string _postalcode;
        /// <summary>
        /// 10200700 Почтовый индекс (POSTAL_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10200700)]
        public string PostalCode
        {
            get
            {
                CheckPropertyInited("PostalCode");
                return _postalcode;
            }
            set
            {
                _postalcode = value;
                NotifyPropertyChanged("PostalCode");
            }
        }


        private string _region;
        /// <summary>
        /// 10200800 Регион (REGION)
        /// </summary>
        [RegisterAttribute(AttributeID = 10200800)]
        public string Region
        {
            get
            {
                CheckPropertyInited("Region");
                return _region;
            }
            set
            {
                _region = value;
                NotifyPropertyChanged("Region");
            }
        }


        private string _districtname;
        /// <summary>
        /// 10200900 Название подрегиона (DISTRICT_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 10200900)]
        public string DistrictName
        {
            get
            {
                CheckPropertyInited("DistrictName");
                return _districtname;
            }
            set
            {
                _districtname = value;
                NotifyPropertyChanged("DistrictName");
            }
        }


        private string _districttype;
        /// <summary>
        /// 10201000 Тип подрегиона (DISTRICT_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10201000)]
        public string DistrictType
        {
            get
            {
                CheckPropertyInited("DistrictType");
                return _districttype;
            }
            set
            {
                _districttype = value;
                NotifyPropertyChanged("DistrictType");
            }
        }


        private string _cityname;
        /// <summary>
        /// 10201100 Название города (CITY_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 10201100)]
        public string CityName
        {
            get
            {
                CheckPropertyInited("CityName");
                return _cityname;
            }
            set
            {
                _cityname = value;
                NotifyPropertyChanged("CityName");
            }
        }


        private string _citytype;
        /// <summary>
        /// 10201200 Тип города (CITY_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10201200)]
        public string CityType
        {
            get
            {
                CheckPropertyInited("CityType");
                return _citytype;
            }
            set
            {
                _citytype = value;
                NotifyPropertyChanged("CityType");
            }
        }


        private string _urbanname;
        /// <summary>
        /// 10201300 Название области (URBAN_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 10201300)]
        public string UrbanName
        {
            get
            {
                CheckPropertyInited("UrbanName");
                return _urbanname;
            }
            set
            {
                _urbanname = value;
                NotifyPropertyChanged("UrbanName");
            }
        }


        private string _urbantype;
        /// <summary>
        /// 10201400 Тип области (URBAN_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10201400)]
        public string UrbanType
        {
            get
            {
                CheckPropertyInited("UrbanType");
                return _urbantype;
            }
            set
            {
                _urbantype = value;
                NotifyPropertyChanged("UrbanType");
            }
        }


        private string _sovietname;
        /// <summary>
        /// 10201500 Поселение (SOVIET_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 10201500)]
        public string SovietName
        {
            get
            {
                CheckPropertyInited("SovietName");
                return _sovietname;
            }
            set
            {
                _sovietname = value;
                NotifyPropertyChanged("SovietName");
            }
        }


        private string _soviettype;
        /// <summary>
        /// 10201600 Тип поселения (SOVIET_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10201600)]
        public string SovietType
        {
            get
            {
                CheckPropertyInited("SovietType");
                return _soviettype;
            }
            set
            {
                _soviettype = value;
                NotifyPropertyChanged("SovietType");
            }
        }


        private string _localityname;
        /// <summary>
        /// 10201700 Район (LOCALITY_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 10201700)]
        public string LocalityName
        {
            get
            {
                CheckPropertyInited("LocalityName");
                return _localityname;
            }
            set
            {
                _localityname = value;
                NotifyPropertyChanged("LocalityName");
            }
        }


        private string _localitytype;
        /// <summary>
        /// 10201800 Тип рaйона (LOCALITY_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10201800)]
        public string LocalityType
        {
            get
            {
                CheckPropertyInited("LocalityType");
                return _localitytype;
            }
            set
            {
                _localitytype = value;
                NotifyPropertyChanged("LocalityType");
            }
        }


        private string _streetname;
        /// <summary>
        /// 10201900 Улица (STREET_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 10201900)]
        public string StreetName
        {
            get
            {
                CheckPropertyInited("StreetName");
                return _streetname;
            }
            set
            {
                _streetname = value;
                NotifyPropertyChanged("StreetName");
            }
        }


        private string _streettype;
        /// <summary>
        /// 10202000 Тип улицы (STREET_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10202000)]
        public string StreetType
        {
            get
            {
                CheckPropertyInited("StreetType");
                return _streettype;
            }
            set
            {
                _streettype = value;
                NotifyPropertyChanged("StreetType");
            }
        }


        private string _level1name;
        /// <summary>
        /// 10202100 Первый ориентир (LEVEL1_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 10202100)]
        public string Level1Name
        {
            get
            {
                CheckPropertyInited("Level1Name");
                return _level1name;
            }
            set
            {
                _level1name = value;
                NotifyPropertyChanged("Level1Name");
            }
        }


        private string _level1type;
        /// <summary>
        /// 10202200 Тип первого ориентира (LEVEL1_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10202200)]
        public string Level1Type
        {
            get
            {
                CheckPropertyInited("Level1Type");
                return _level1type;
            }
            set
            {
                _level1type = value;
                NotifyPropertyChanged("Level1Type");
            }
        }


        private string _level2name;
        /// <summary>
        /// 10202300 Второй ориентир	 (LEVEL2_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 10202300)]
        public string Level2Name
        {
            get
            {
                CheckPropertyInited("Level2Name");
                return _level2name;
            }
            set
            {
                _level2name = value;
                NotifyPropertyChanged("Level2Name");
            }
        }


        private string _level2type;
        /// <summary>
        /// 10202400 Тип второго ориентира (LEVEL2_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10202400)]
        public string Level2Type
        {
            get
            {
                CheckPropertyInited("Level2Type");
                return _level2type;
            }
            set
            {
                _level2type = value;
                NotifyPropertyChanged("Level2Type");
            }
        }


        private string _level3name;
        /// <summary>
        /// 10202500 Третий ориентир (LEVEL3_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 10202500)]
        public string Level3Name
        {
            get
            {
                CheckPropertyInited("Level3Name");
                return _level3name;
            }
            set
            {
                _level3name = value;
                NotifyPropertyChanged("Level3Name");
            }
        }


        private string _level3type;
        /// <summary>
        /// 10202600 Тип третьего ориентира (LEVEL3_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10202600)]
        public string Level3Type
        {
            get
            {
                CheckPropertyInited("Level3Type");
                return _level3type;
            }
            set
            {
                _level3type = value;
                NotifyPropertyChanged("Level3Type");
            }
        }


        private string _appartmentname;
        /// <summary>
        /// 10202700 Квартира (APARTMENT_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 10202700)]
        public string AppartmentName
        {
            get
            {
                CheckPropertyInited("AppartmentName");
                return _appartmentname;
            }
            set
            {
                _appartmentname = value;
                NotifyPropertyChanged("AppartmentName");
            }
        }


        private string _appartmenttype;
        /// <summary>
        /// 10202800 Тип квартиры (APARTMENT_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10202800)]
        public string AppartmentType
        {
            get
            {
                CheckPropertyInited("AppartmentType");
                return _appartmenttype;
            }
            set
            {
                _appartmenttype = value;
                NotifyPropertyChanged("AppartmentType");
            }
        }


        private string _other;
        /// <summary>
        /// 10202900 Другое (OTHER)
        /// </summary>
        [RegisterAttribute(AttributeID = 10202900)]
        public string Other
        {
            get
            {
                CheckPropertyInited("Other");
                return _other;
            }
            set
            {
                _other = value;
                NotifyPropertyChanged("Other");
            }
        }


        private string _note;
        /// <summary>
        /// 10203000 Примечание (NOTE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10203000)]
        public string Note
        {
            get
            {
                CheckPropertyInited("Note");
                return _note;
            }
            set
            {
                _note = value;
                NotifyPropertyChanged("Note");
            }
        }


        private DateTime? _datevalue;
        /// <summary>
        /// 10203100 Дата заполнения (DATE_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10203100)]
        public DateTime? DateValue
        {
            get
            {
                CheckPropertyInited("DateValue");
                return _datevalue;
            }
            set
            {
                _datevalue = value;
                NotifyPropertyChanged("DateValue");
            }
        }


        private long? _statusvalue;
        /// <summary>
        /// 10203200 Статус (STATUS_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10203200)]
        public long? StatusValue
        {
            get
            {
                CheckPropertyInited("StatusValue");
                return _statusvalue;
            }
            set
            {
                _statusvalue = value;
                NotifyPropertyChanged("StatusValue");
            }
        }


        private long? _iduser;
        /// <summary>
        /// 10203300 Идентификатор пользователя (ID_USER)
        /// </summary>
        [RegisterAttribute(AttributeID = 10203300)]
        public long? IdUser
        {
            get
            {
                CheckPropertyInited("IdUser");
                return _iduser;
            }
            set
            {
                _iduser = value;
                NotifyPropertyChanged("IdUser");
            }
        }


        private DateTime? _dateuser;
        /// <summary>
        /// 10203400 Дата изменения	 (DATE_USER)
        /// </summary>
        [RegisterAttribute(AttributeID = 10203400)]
        public DateTime? DateUser
        {
            get
            {
                CheckPropertyInited("DateUser");
                return _dateuser;
            }
            set
            {
                _dateuser = value;
                NotifyPropertyChanged("DateUser");
            }
        }

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 103 Таблица, содержащая настройки модуля (MARKET_SETTINGS)
    /// </summary>
    [RegisterInfo(RegisterID = 103)]
    [Serializable]
    public partial class OMSettings : OMBaseClass<OMSettings>
    {

        private long _id;
        /// <summary>
        /// 10300100 Идентификатор настройки (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 10300100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _settingvalue;
        /// <summary>
        /// 10300200 Значение настройки (SETTING_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10300200)]
        public string SettingValue
        {
            get
            {
                CheckPropertyInited("SettingValue");
                return _settingvalue;
            }
            set
            {
                _settingvalue = value;
                NotifyPropertyChanged("SettingValue");
            }
        }

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 104 Таблица, содержащая информацию о скриншотах (MARKET_SCREENSHOTS)
    /// </summary>
    [RegisterInfo(RegisterID = 104)]
    [Serializable]
    public partial class OMScreenshots : OMBaseClass<OMScreenshots>
    {

        private long _id;
        /// <summary>
        /// 10400100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 10400100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _initialid;
        /// <summary>
        /// 10400200 Идентификатор исходного объекта (INITIAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 10400200)]
        public long? InitialId
        {
            get
            {
                CheckPropertyInited("InitialId");
                return _initialid;
            }
            set
            {
                _initialid = value;
                NotifyPropertyChanged("InitialId");
            }
        }


        private DateTime? _creationdate;
        /// <summary>
        /// 10400300 Дата записи (CREATION_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10400300)]
        public DateTime? CreationDate
        {
            get
            {
                CheckPropertyInited("CreationDate");
                return _creationdate;
            }
            set
            {
                _creationdate = value;
                NotifyPropertyChanged("CreationDate");
            }
        }


        private string _type;
        /// <summary>
        /// 10400400 Тип записи (TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10400400)]
        public string Type
        {
            get
            {
                CheckPropertyInited("Type");
                return _type;
            }
            set
            {
                _type = value;
                NotifyPropertyChanged("Type");
            }
        }

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 105 Таблица, содержащая ретроспективу цен по объектам (MARKET_PRICE_HISTORY)
    /// </summary>
    [RegisterInfo(RegisterID = 105)]
    [Serializable]
    public partial class OMPriceHistory : OMBaseClass<OMPriceHistory>
    {

        private long _id;
        /// <summary>
        /// 10500100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 10500100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _initialid;
        /// <summary>
        /// 10500200 Идентификатор объекта (INITIAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 10500200)]
        public long InitialId
        {
            get
            {
                CheckPropertyInited("InitialId");
                return _initialid;
            }
            set
            {
                _initialid = value;
                NotifyPropertyChanged("InitialId");
            }
        }


        private DateTime _changingdate;
        /// <summary>
        /// 10500300 Время изменения цены (CHANGING_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10500300)]
        public DateTime ChangingDate
        {
            get
            {
                CheckPropertyInited("ChangingDate");
                return _changingdate;
            }
            set
            {
                _changingdate = value;
                NotifyPropertyChanged("ChangingDate");
            }
        }


        private long? _pricevaluefrom;
        /// <summary>
        /// 10500400 Значение стоимости от (PRICE_VALUE_FROM)
        /// </summary>
        [RegisterAttribute(AttributeID = 10500400)]
        public long? PriceValueFrom
        {
            get
            {
                CheckPropertyInited("PriceValueFrom");
                return _pricevaluefrom;
            }
            set
            {
                _pricevaluefrom = value;
                NotifyPropertyChanged("PriceValueFrom");
            }
        }


        private long _pricevalueto;
        /// <summary>
        /// 10500500 Значение стоимости до (PRICE_VALUE_TO)
        /// </summary>
        [RegisterAttribute(AttributeID = 10500500)]
        public long PriceValueTo
        {
            get
            {
                CheckPropertyInited("PriceValueTo");
                return _pricevalueto;
            }
            set
            {
                _pricevalueto = value;
                NotifyPropertyChanged("PriceValueTo");
            }
        }

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 106 Таблица, содержащая информацию о проведённых проверках на дублирование (MARKET_DUPLICATES_HISTORY)
    /// </summary>
    [RegisterInfo(RegisterID = 106)]
    [Serializable]
    public partial class OMDuplicatesHistory : OMBaseClass<OMDuplicatesHistory>
    {

        private long _id;
        /// <summary>
        /// 10600100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 10600100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private DateTime? _checkdate;
        /// <summary>
        /// 10600200 Дата проверки (CHECK_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10600200)]
        public DateTime? CheckDate
        {
            get
            {
                CheckPropertyInited("CheckDate");
                return _checkdate;
            }
            set
            {
                _checkdate = value;
                NotifyPropertyChanged("CheckDate");
            }
        }


        private string _marketsegment;
        /// <summary>
        /// 10600300 Сегмент рынка (MARKET_SEGMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10600300)]
        public string MarketSegment
        {
            get
            {
                CheckPropertyInited("MarketSegment");
                return _marketsegment;
            }
            set
            {
                _marketsegment = value;
                NotifyPropertyChanged("MarketSegment");
            }
        }


        private decimal? _areadelta;
        /// <summary>
        /// 10600400 Отклонение по площади (AREA_DELTA)
        /// </summary>
        [RegisterAttribute(AttributeID = 10600400)]
        public decimal? AreaDelta
        {
            get
            {
                CheckPropertyInited("AreaDelta");
                return _areadelta;
            }
            set
            {
                _areadelta = value;
                NotifyPropertyChanged("AreaDelta");
            }
        }


        private decimal? _pricedelta;
        /// <summary>
        /// 10600500 Отклонение по цене (PRICE_DELTA)
        /// </summary>
        [RegisterAttribute(AttributeID = 10600500)]
        public decimal? PriceDelta
        {
            get
            {
                CheckPropertyInited("PriceDelta");
                return _pricedelta;
            }
            set
            {
                _pricedelta = value;
                NotifyPropertyChanged("PriceDelta");
            }
        }


        private long? _commoncount;
        /// <summary>
        /// 10600600 Всего объектов (COMMON_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10600600)]
        public long? CommonCount
        {
            get
            {
                CheckPropertyInited("CommonCount");
                return _commoncount;
            }
            set
            {
                _commoncount = value;
                NotifyPropertyChanged("CommonCount");
            }
        }


        private long? _inprogresscount;
        /// <summary>
        /// 10600700 Объектов в работе (IN_PROGRESS_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10600700)]
        public long? InProgressCount
        {
            get
            {
                CheckPropertyInited("InProgressCount");
                return _inprogresscount;
            }
            set
            {
                _inprogresscount = value;
                NotifyPropertyChanged("InProgressCount");
            }
        }


        private long? _duplicateobjects;
        /// <summary>
        /// 10600800 Объектов дубликатов (DUPLICATE_OBJECTS)
        /// </summary>
        [RegisterAttribute(AttributeID = 10600800)]
        public long? DuplicateObjects
        {
            get
            {
                CheckPropertyInited("DuplicateObjects");
                return _duplicateobjects;
            }
            set
            {
                _duplicateobjects = value;
                NotifyPropertyChanged("DuplicateObjects");
            }
        }

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 107 Таблица, содержащая информацию о соответствии кад кварталов районам, округам и зонам  (MARKET_REGION_DICTIONATY)
    /// </summary>
    [RegisterInfo(RegisterID = 107)]
    [Serializable]
    public partial class OMQuartalDictionary : OMBaseClass<OMQuartalDictionary>
    {

        private long _id;
        /// <summary>
        /// 10700100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 10700100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _cadastralquartal;
        /// <summary>
        /// 10700200 Кадастровый квартал (CADASTRAL_QUARTAL)
        /// </summary>
        [RegisterAttribute(AttributeID = 10700200)]
        public string CadastralQuartal
        {
            get
            {
                CheckPropertyInited("CadastralQuartal");
                return _cadastralquartal;
            }
            set
            {
                _cadastralquartal = value;
                NotifyPropertyChanged("CadastralQuartal");
            }
        }


        private string _district;
        /// <summary>
        /// 10700300 Округ (DISTRICT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10700300)]
        public string District
        {
            get
            {
                CheckPropertyInited("District");
                return _district;
            }
            set
            {
                _district = value;
                NotifyPropertyChanged("District");
            }
        }


        private Hunteds _district_Code;
        /// <summary>
        /// 10700300 Округ (справочный код) (DISTRICT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10700300)]
        public Hunteds District_Code
        {
            get
            {
                CheckPropertyInited("District_Code");
                return this._district_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_district))
                    {
                         _district = descr;
                    }
                }
                else
                {
                     _district = descr;
                }

                this._district_Code = value;
                NotifyPropertyChanged("District");
                NotifyPropertyChanged("District_Code");
            }
        }


        private string _region;
        /// <summary>
        /// 10700400 Район (REGION)
        /// </summary>
        [RegisterAttribute(AttributeID = 10700400)]
        public string Region
        {
            get
            {
                CheckPropertyInited("Region");
                return _region;
            }
            set
            {
                _region = value;
                NotifyPropertyChanged("Region");
            }
        }


        private Districts _region_Code;
        /// <summary>
        /// 10700400 Район (справочный код) (REGION_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10700400)]
        public Districts Region_Code
        {
            get
            {
                CheckPropertyInited("Region_Code");
                return this._region_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_region))
                    {
                         _region = descr;
                    }
                }
                else
                {
                     _region = descr;
                }

                this._region_Code = value;
                NotifyPropertyChanged("Region");
                NotifyPropertyChanged("Region_Code");
            }
        }


        private long? _zone;
        /// <summary>
        /// 10700500 Зона (ZONE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10700500)]
        public long? Zone
        {
            get
            {
                CheckPropertyInited("Zone");
                return _zone;
            }
            set
            {
                _zone = value;
                NotifyPropertyChanged("Zone");
            }
        }


        private string _zoneregion;
        /// <summary>
        /// 10700600 Зона_район (ZONE_REGION)
        /// </summary>
        [RegisterAttribute(AttributeID = 10700600)]
        public string ZoneRegion
        {
            get
            {
                CheckPropertyInited("ZoneRegion");
                return _zoneregion;
            }
            set
            {
                _zoneregion = value;
                NotifyPropertyChanged("ZoneRegion");
            }
        }


        private string _customzone;
        /// <summary>
        /// 10700700 Настраиваемая зона (CUSTOM_ZONE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10700700)]
        public string CustomZone
        {
            get
            {
                CheckPropertyInited("CustomZone");
                return _customzone;
            }
            set
            {
                _customzone = value;
                NotifyPropertyChanged("CustomZone");
            }
        }


        private string _zonenamebycircles;
        /// <summary>
        /// 10700800 Название зоны по кольцам (ZONE_NAME_BY_CIRCLES)
        /// </summary>
        [RegisterAttribute(AttributeID = 10700800)]
        public string ZoneNameByCircles
        {
            get
            {
                CheckPropertyInited("ZoneNameByCircles");
                return _zonenamebycircles;
            }
            set
            {
                _zonenamebycircles = value;
                NotifyPropertyChanged("ZoneNameByCircles");
            }
        }

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 108 Индексы для корректировки на дату (MARKET_INDEXES_FOR_DATE_CORRECTION)
    /// </summary>
    [RegisterInfo(RegisterID = 108)]
    [Serializable]
    public partial class OMIndexesForDateCorrection : OMBaseClass<OMIndexesForDateCorrection>
    {

        private long _id;
        /// <summary>
        /// 10800100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 10800100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private DateTime _date;
        /// <summary>
        /// 10800200 Дата (DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10800200)]
        public DateTime Date
        {
            get
            {
                CheckPropertyInited("Date");
                return _date;
            }
            set
            {
                _date = value;
                NotifyPropertyChanged("Date");
            }
        }


        private decimal _coefficient;
        /// <summary>
        /// 10800400 Коэффициент (COEFFICIENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10800400)]
        public decimal Coefficient
        {
            get
            {
                CheckPropertyInited("Coefficient");
                return _coefficient;
            }
            set
            {
                _coefficient = value;
                NotifyPropertyChanged("Coefficient");
            }
        }


        private string _buildingcadastralnumber;
        /// <summary>
        /// 10800600 Кадастровый номер здания (BUILDING_CADASTRAL_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 10800600)]
        public string BuildingCadastralNumber
        {
            get
            {
                CheckPropertyInited("BuildingCadastralNumber");
                return _buildingcadastralnumber;
            }
            set
            {
                _buildingcadastralnumber = value;
                NotifyPropertyChanged("BuildingCadastralNumber");
            }
        }


        private string _marketsegment;
        /// <summary>
        /// 10800900 Сегмент рынка (MARKET_SEGMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10800900)]
        public string MarketSegment
        {
            get
            {
                CheckPropertyInited("MarketSegment");
                return _marketsegment;
            }
            set
            {
                _marketsegment = value;
                NotifyPropertyChanged("MarketSegment");
            }
        }


        private MarketSegment _marketsegment_Code;
        /// <summary>
        /// 10800900 Сегмент рынка (справочный код) (MARKET_SEGMENT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10800900)]
        public MarketSegment MarketSegment_Code
        {
            get
            {
                CheckPropertyInited("MarketSegment_Code");
                return this._marketsegment_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_marketsegment))
                    {
                         _marketsegment = descr;
                    }
                }
                else
                {
                     _marketsegment = descr;
                }

                this._marketsegment_Code = value;
                NotifyPropertyChanged("MarketSegment");
                NotifyPropertyChanged("MarketSegment_Code");
            }
        }


        private bool? _isexcluded;
        /// <summary>
        /// 10801000 Исключение здания из рассчета (IS_EXCLUDED)
        /// </summary>
        [RegisterAttribute(AttributeID = 10801000)]
        public bool? IsExcluded
        {
            get
            {
                CheckPropertyInited("IsExcluded");
                return _isexcluded;
            }
            set
            {
                _isexcluded = value;
                NotifyPropertyChanged("IsExcluded");
            }
        }

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 110 Временная таблица для проведения проверки механизма отбора дублей (MARKET_CORE_OBJECT_TEST)
    /// </summary>
    [RegisterInfo(RegisterID = 110)]
    [Serializable]
    public partial class OMCoreObjectTest : OMBaseClass<OMCoreObjectTest>
    {

        private long _id;
        /// <summary>
        /// 11000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 11000100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _cadastralnumber;
        /// <summary>
        /// 11000200 Кадастровый номер (CADASTRAL_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 11000200)]
        public string CadastralNumber
        {
            get
            {
                CheckPropertyInited("CadastralNumber");
                return _cadastralnumber;
            }
            set
            {
                _cadastralnumber = value;
                NotifyPropertyChanged("CadastralNumber");
            }
        }


        private string _address;
        /// <summary>
        /// 11000300 Адрес (ADDRESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 11000300)]
        public string Address
        {
            get
            {
                CheckPropertyInited("Address");
                return _address;
            }
            set
            {
                _address = value;
                NotifyPropertyChanged("Address");
            }
        }


        private DateTime? _parsertime;
        /// <summary>
        /// 11000400 Дата сделки (PARSER_TIME)
        /// </summary>
        [RegisterAttribute(AttributeID = 11000400)]
        public DateTime? ParserTime
        {
            get
            {
                CheckPropertyInited("ParserTime");
                return _parsertime;
            }
            set
            {
                _parsertime = value;
                NotifyPropertyChanged("ParserTime");
            }
        }


        private string _description;
        /// <summary>
        /// 11000500 Описание (DESCRIPTION)
        /// </summary>
        [RegisterAttribute(AttributeID = 11000500)]
        public string Description
        {
            get
            {
                CheckPropertyInited("Description");
                return _description;
            }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }


        private decimal? _area;
        /// <summary>
        /// 11000600 Общая площадь (AREA)
        /// </summary>
        [RegisterAttribute(AttributeID = 11000600)]
        public decimal? Area
        {
            get
            {
                CheckPropertyInited("Area");
                return _area;
            }
            set
            {
                _area = value;
                NotifyPropertyChanged("Area");
            }
        }


        private long? _price;
        /// <summary>
        /// 11000700 Цена (PRICE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11000700)]
        public long? Price
        {
            get
            {
                CheckPropertyInited("Price");
                return _price;
            }
            set
            {
                _price = value;
                NotifyPropertyChanged("Price");
            }
        }


        private decimal? _pricepermeter;
        /// <summary>
        /// 11000800 Цена за кв. м (PRICE_PER_METER)
        /// </summary>
        [RegisterAttribute(AttributeID = 11000800)]
        public decimal? PricePerMeter
        {
            get
            {
                CheckPropertyInited("PricePerMeter");
                return _pricepermeter;
            }
            set
            {
                _pricepermeter = value;
                NotifyPropertyChanged("PricePerMeter");
            }
        }


        private string _processtype;
        /// <summary>
        /// 11000900 Статус обработки (PROCESS_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11000900)]
        public string ProcessType
        {
            get
            {
                CheckPropertyInited("ProcessType");
                return _processtype;
            }
            set
            {
                _processtype = value;
                NotifyPropertyChanged("ProcessType");
            }
        }


        private ProcessStep _processtype_Code;
        /// <summary>
        /// 11000900 Статус обработки (справочный код) (PROCESS_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11000900)]
        public ProcessStep ProcessType_Code
        {
            get
            {
                CheckPropertyInited("ProcessType_Code");
                return this._processtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_processtype))
                    {
                         _processtype = descr;
                    }
                }
                else
                {
                     _processtype = descr;
                }

                this._processtype_Code = value;
                NotifyPropertyChanged("ProcessType");
                NotifyPropertyChanged("ProcessType_Code");
            }
        }


        private string _exclusionstatus;
        /// <summary>
        /// 11001000 Причина исключения (EXCLUSION_STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 11001000)]
        public string ExclusionStatus
        {
            get
            {
                CheckPropertyInited("ExclusionStatus");
                return _exclusionstatus;
            }
            set
            {
                _exclusionstatus = value;
                NotifyPropertyChanged("ExclusionStatus");
            }
        }


        private ExclusionStatus _exclusionstatus_Code;
        /// <summary>
        /// 11001000 Причина исключения (справочный код) (EXCLUSION_STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11001000)]
        public ExclusionStatus ExclusionStatus_Code
        {
            get
            {
                CheckPropertyInited("ExclusionStatus_Code");
                return this._exclusionstatus_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_exclusionstatus))
                    {
                         _exclusionstatus = descr;
                    }
                }
                else
                {
                     _exclusionstatus = descr;
                }

                this._exclusionstatus_Code = value;
                NotifyPropertyChanged("ExclusionStatus");
                NotifyPropertyChanged("ExclusionStatus_Code");
            }
        }


        private string _dealtype;
        /// <summary>
        /// 11001100 Тип сделки (DEAL_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11001100)]
        public string DealType
        {
            get
            {
                CheckPropertyInited("DealType");
                return _dealtype;
            }
            set
            {
                _dealtype = value;
                NotifyPropertyChanged("DealType");
            }
        }


        private DealType _dealtype_Code;
        /// <summary>
        /// 11001100 Тип сделки (справочный код) (DEAL_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11001100)]
        public DealType DealType_Code
        {
            get
            {
                CheckPropertyInited("DealType_Code");
                return this._dealtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_dealtype))
                    {
                         _dealtype = descr;
                    }
                }
                else
                {
                     _dealtype = descr;
                }

                this._dealtype_Code = value;
                NotifyPropertyChanged("DealType");
                NotifyPropertyChanged("DealType_Code");
            }
        }


        private string _propertytype;
        /// <summary>
        /// 11001200 Тип объекта недвижимости (PROPERTY_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11001200)]
        public string PropertyType
        {
            get
            {
                CheckPropertyInited("PropertyType");
                return _propertytype;
            }
            set
            {
                _propertytype = value;
                NotifyPropertyChanged("PropertyType");
            }
        }


        private PropertyTypes _propertytype_Code;
        /// <summary>
        /// 11001200 Тип объекта недвижимости (справочный код) (PROPERTY_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11001200)]
        public PropertyTypes PropertyType_Code
        {
            get
            {
                CheckPropertyInited("PropertyType_Code");
                return this._propertytype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_propertytype))
                    {
                         _propertytype = descr;
                    }
                }
                else
                {
                     _propertytype = descr;
                }

                this._propertytype_Code = value;
                NotifyPropertyChanged("PropertyType");
                NotifyPropertyChanged("PropertyType_Code");
            }
        }


        private string _subcategory;
        /// <summary>
        /// 11001300 Подкатегория (SUBCATEGORY)
        /// </summary>
        [RegisterAttribute(AttributeID = 11001300)]
        public string Subcategory
        {
            get
            {
                CheckPropertyInited("Subcategory");
                return _subcategory;
            }
            set
            {
                _subcategory = value;
                NotifyPropertyChanged("Subcategory");
            }
        }


        private string _resultfromsourcefile;
        /// <summary>
        /// 11001400 Результат из исходного файла (RESULT_FROM_SOURCE_FILE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11001400)]
        public string ResultFromSourceFile
        {
            get
            {
                CheckPropertyInited("ResultFromSourceFile");
                return _resultfromsourcefile;
            }
            set
            {
                _resultfromsourcefile = value;
                NotifyPropertyChanged("ResultFromSourceFile");
            }
        }

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 111 Таблица, содержащая коэффициенты на квартиры по зданиям для корректировки на комнатность (MARKET_COEFFICIENT_FOR_ROOMS_CORRECTION)
    /// </summary>
    [RegisterInfo(RegisterID = 111)]
    [Serializable]
    public partial class OMCoefficientsForCorrectionByRooms : OMBaseClass<OMCoefficientsForCorrectionByRooms>
    {

        private long _id;
        /// <summary>
        /// 11100100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 11100100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _buildingcadastralnumber;
        /// <summary>
        /// 11100200 Кадастровый номер здания (BUILDING_CADASTRAL_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 11100200)]
        public string BuildingCadastralNumber
        {
            get
            {
                CheckPropertyInited("BuildingCadastralNumber");
                return _buildingcadastralnumber;
            }
            set
            {
                _buildingcadastralnumber = value;
                NotifyPropertyChanged("BuildingCadastralNumber");
            }
        }


        private DateTime _changingdate;
        /// <summary>
        /// 11100300 Время изменения цены (CHANGING_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11100300)]
        public DateTime ChangingDate
        {
            get
            {
                CheckPropertyInited("ChangingDate");
                return _changingdate;
            }
            set
            {
                _changingdate = value;
                NotifyPropertyChanged("ChangingDate");
            }
        }


        private decimal _oneroomcoefficient;
        /// <summary>
        /// 11100400 Коэффициент для 1-комнатной квартиры (ONE_ROOM_COEFFICIENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 11100400)]
        public decimal OneRoomCoefficient
        {
            get
            {
                CheckPropertyInited("OneRoomCoefficient");
                return _oneroomcoefficient;
            }
            set
            {
                _oneroomcoefficient = value;
                NotifyPropertyChanged("OneRoomCoefficient");
            }
        }


        private decimal _threeroomscoefficient;
        /// <summary>
        /// 11100500 Коэффициент для 3-комнатной квартиры (THREE_ROOMS_COEFFICIENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 11100500)]
        public decimal ThreeRoomsCoefficient
        {
            get
            {
                CheckPropertyInited("ThreeRoomsCoefficient");
                return _threeroomscoefficient;
            }
            set
            {
                _threeroomscoefficient = value;
                NotifyPropertyChanged("ThreeRoomsCoefficient");
            }
        }


        private string _marketsegment;
        /// <summary>
        /// 11100600 Сегмент рынка (MARKET_SEGMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 11100600)]
        public string MarketSegment
        {
            get
            {
                CheckPropertyInited("MarketSegment");
                return _marketsegment;
            }
            set
            {
                _marketsegment = value;
                NotifyPropertyChanged("MarketSegment");
            }
        }


        private MarketSegment _marketsegment_Code;
        /// <summary>
        /// 11100600 Сегмент рынка (справочный код) (MARKET_SEGMENT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11100600)]
        public MarketSegment MarketSegment_Code
        {
            get
            {
                CheckPropertyInited("MarketSegment_Code");
                return this._marketsegment_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_marketsegment))
                    {
                         _marketsegment = descr;
                    }
                }
                else
                {
                     _marketsegment = descr;
                }

                this._marketsegment_Code = value;
                NotifyPropertyChanged("MarketSegment");
                NotifyPropertyChanged("MarketSegment_Code");
            }
        }


        private bool? _isexcluded;
        /// <summary>
        /// 11100700 Исключение здания из рассчета (IS_EXCLUDED)
        /// </summary>
        [RegisterAttribute(AttributeID = 11100700)]
        public bool? IsExcluded
        {
            get
            {
                CheckPropertyInited("IsExcluded");
                return _isexcluded;
            }
            set
            {
                _isexcluded = value;
                NotifyPropertyChanged("IsExcluded");
            }
        }

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 112 Таблица, содержащая историю изменения цены после корректировки на этажность (MARKET_PRICE_CORRECTION_BY_STAGE_HISTORY)
    /// </summary>
    [RegisterInfo(RegisterID = 112)]
    [Serializable]
    public partial class OMPriceCorrectionByStageHistory : OMBaseClass<OMPriceCorrectionByStageHistory>
    {

        private long _id;
        /// <summary>
        /// 11200100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 11200100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _buildingcadastralnumber;
        /// <summary>
        /// 11200200 Кадастровый номер здания (BUILDING_CADASTRAL_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 11200200)]
        public string BuildingCadastralNumber
        {
            get
            {
                CheckPropertyInited("BuildingCadastralNumber");
                return _buildingcadastralnumber;
            }
            set
            {
                _buildingcadastralnumber = value;
                NotifyPropertyChanged("BuildingCadastralNumber");
            }
        }


        private DateTime _changingdate;
        /// <summary>
        /// 11200300 Время изменения цены (CHANGING_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11200300)]
        public DateTime ChangingDate
        {
            get
            {
                CheckPropertyInited("ChangingDate");
                return _changingdate;
            }
            set
            {
                _changingdate = value;
                NotifyPropertyChanged("ChangingDate");
            }
        }


        private decimal _stagecoefficient;
        /// <summary>
        /// 11200400 Коэффициент (STAGE_COEFFICIENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 11200400)]
        public decimal StageCoefficient
        {
            get
            {
                CheckPropertyInited("StageCoefficient");
                return _stagecoefficient;
            }
            set
            {
                _stagecoefficient = value;
                NotifyPropertyChanged("StageCoefficient");
            }
        }


        private string _marketsegment;
        /// <summary>
        /// 11200500 Сегмент рынка (MARKET_SEGMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 11200500)]
        public string MarketSegment
        {
            get
            {
                CheckPropertyInited("MarketSegment");
                return _marketsegment;
            }
            set
            {
                _marketsegment = value;
                NotifyPropertyChanged("MarketSegment");
            }
        }


        private MarketSegment _marketsegment_Code;
        /// <summary>
        /// 11200500 Сегмент рынка (справочный код) (MARKET_SEGMENT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11200500)]
        public MarketSegment MarketSegment_Code
        {
            get
            {
                CheckPropertyInited("MarketSegment_Code");
                return this._marketsegment_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_marketsegment))
                    {
                         _marketsegment = descr;
                    }
                }
                else
                {
                     _marketsegment = descr;
                }

                this._marketsegment_Code = value;
                NotifyPropertyChanged("MarketSegment");
                NotifyPropertyChanged("MarketSegment_Code");
            }
        }


        private bool? _isexcluded;
        /// <summary>
        /// 11200600 Исключение здания из рассчета (IS_EXCLUDED)
        /// </summary>
        [RegisterAttribute(AttributeID = 11200600)]
        public bool? IsExcluded
        {
            get
            {
                CheckPropertyInited("IsExcluded");
                return _isexcluded;
            }
            set
            {
                _isexcluded = value;
                NotifyPropertyChanged("IsExcluded");
            }
        }

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 113 Таблица, содержащая ретроспективу цен по объектам с учетом корректировки на комнатность (MARKET_PRICE_AFTER_CORRECTION_BY_ROOMS_H)
    /// </summary>
    [RegisterInfo(RegisterID = 113)]
    [Serializable]
    public partial class OMPriceAfterCorrectionByRoomsHistory : OMBaseClass<OMPriceAfterCorrectionByRoomsHistory>
    {

        private long _id;
        /// <summary>
        /// 11300100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 11300100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _initialid;
        /// <summary>
        /// 11300200 Идентификатор объекта (INITIAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 11300200)]
        public long InitialId
        {
            get
            {
                CheckPropertyInited("InitialId");
                return _initialid;
            }
            set
            {
                _initialid = value;
                NotifyPropertyChanged("InitialId");
            }
        }


        private DateTime _changingdate;
        /// <summary>
        /// 11300300 Время изменения цены (CHANGING_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11300300)]
        public DateTime ChangingDate
        {
            get
            {
                CheckPropertyInited("ChangingDate");
                return _changingdate;
            }
            set
            {
                _changingdate = value;
                NotifyPropertyChanged("ChangingDate");
            }
        }


        private decimal? _pricevaluefrom;
        /// <summary>
        /// 11300400 Значение стоимости от (PRICE_VALUE_FROM)
        /// </summary>
        [RegisterAttribute(AttributeID = 11300400)]
        public decimal? PriceValueFrom
        {
            get
            {
                CheckPropertyInited("PriceValueFrom");
                return _pricevaluefrom;
            }
            set
            {
                _pricevaluefrom = value;
                NotifyPropertyChanged("PriceValueFrom");
            }
        }


        private decimal? _pricevalueto;
        /// <summary>
        /// 11300500 Значение стоимости до (PRICE_VALUE_TO)
        /// </summary>
        [RegisterAttribute(AttributeID = 11300500)]
        public decimal? PriceValueTo
        {
            get
            {
                CheckPropertyInited("PriceValueTo");
                return _pricevalueto;
            }
            set
            {
                _pricevalueto = value;
                NotifyPropertyChanged("PriceValueTo");
            }
        }

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 114 Таблица, хранящая отношения цен первого этажа к верхним этажам (MARKET_COEFFICIENTS_FOR_FIRST_FLOOR_CORR)
    /// </summary>
    [RegisterInfo(RegisterID = 114)]
    [Serializable]
    public partial class OMCoefficientsForFirstFloorCorr : OMBaseClass<OMCoefficientsForFirstFloorCorr>
    {

        private long _id;
        /// <summary>
        /// 11400100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 11400100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private DateTime _statsdate;
        /// <summary>
        /// 11400200 Дата сбора данных (STATS_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11400200)]
        public DateTime StatsDate
        {
            get
            {
                CheckPropertyInited("StatsDate");
                return _statsdate;
            }
            set
            {
                _statsdate = value;
                NotifyPropertyChanged("StatsDate");
            }
        }


        private string _buildingcadastralnumber;
        /// <summary>
        /// 11400300 Кадастровый номер здания (BUILDING_CADASTRAL_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 11400300)]
        public string BuildingCadastralNumber
        {
            get
            {
                CheckPropertyInited("BuildingCadastralNumber");
                return _buildingcadastralnumber;
            }
            set
            {
                _buildingcadastralnumber = value;
                NotifyPropertyChanged("BuildingCadastralNumber");
            }
        }


        private string _marketsegment;
        /// <summary>
        /// 11400400 Сегмент рынка (MARKET_SEGMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 11400400)]
        public string MarketSegment
        {
            get
            {
                CheckPropertyInited("MarketSegment");
                return _marketsegment;
            }
            set
            {
                _marketsegment = value;
                NotifyPropertyChanged("MarketSegment");
            }
        }


        private MarketSegment _marketsegment_Code;
        /// <summary>
        /// 11400400 Сегмент рынка (справочный код) (MARKET_SEGMENT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11400400)]
        public MarketSegment MarketSegment_Code
        {
            get
            {
                CheckPropertyInited("MarketSegment_Code");
                return this._marketsegment_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_marketsegment))
                    {
                         _marketsegment = descr;
                    }
                }
                else
                {
                     _marketsegment = descr;
                }

                this._marketsegment_Code = value;
                NotifyPropertyChanged("MarketSegment");
                NotifyPropertyChanged("MarketSegment_Code");
            }
        }


        private decimal _firsttoupperfloorrate;
        /// <summary>
        /// 11400500 Отношение цены первого этажа к верхним (FIRST_TO_UPPER_FLOOR_RATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11400500)]
        public decimal FirstToUpperFloorRate
        {
            get
            {
                CheckPropertyInited("FirstToUpperFloorRate");
                return _firsttoupperfloorrate;
            }
            set
            {
                _firsttoupperfloorrate = value;
                NotifyPropertyChanged("FirstToUpperFloorRate");
            }
        }


        private bool _isexcludedfromcalculation;
        /// <summary>
        /// 11400600 Исключение здания из рассчета (IS_EXCLUDED_FROM_CALCULATION)
        /// </summary>
        [RegisterAttribute(AttributeID = 11400600)]
        public bool IsExcludedFromCalculation
        {
            get
            {
                CheckPropertyInited("IsExcludedFromCalculation");
                return _isexcludedfromcalculation;
            }
            set
            {
                _isexcludedfromcalculation = value;
                NotifyPropertyChanged("IsExcludedFromCalculation");
            }
        }

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 115 Таблица, хранящая историю цен первых этажей (MARKET_PRICE_FOR_FIRST_FLOOR_HISTORY)
    /// </summary>
    [RegisterInfo(RegisterID = 115)]
    [Serializable]
    public partial class OMPriceForFirstFloorHistory : OMBaseClass<OMPriceForFirstFloorHistory>
    {

        private long _id;
        /// <summary>
        /// 11500100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 11500100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private DateTime _statsdate;
        /// <summary>
        /// 11500200 Дата сбора данных (STATS_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11500200)]
        public DateTime StatsDate
        {
            get
            {
                CheckPropertyInited("StatsDate");
                return _statsdate;
            }
            set
            {
                _statsdate = value;
                NotifyPropertyChanged("StatsDate");
            }
        }


        private decimal _pricewithcorrectionforfirstfloor;
        /// <summary>
        /// 11500300 Цена с поправкой на первый этаж (PRICE_WITH_CORRECTION_FOR_FIRST_FLOOR)
        /// </summary>
        [RegisterAttribute(AttributeID = 11500300)]
        public decimal PriceWithCorrectionForFirstFloor
        {
            get
            {
                CheckPropertyInited("PriceWithCorrectionForFirstFloor");
                return _pricewithcorrectionforfirstfloor;
            }
            set
            {
                _pricewithcorrectionforfirstfloor = value;
                NotifyPropertyChanged("PriceWithCorrectionForFirstFloor");
            }
        }


        private long _objectid;
        /// <summary>
        /// 11500400 Объект аналог (OBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 11500400)]
        public long ObjectId
        {
            get
            {
                CheckPropertyInited("ObjectId");
                return _objectid;
            }
            set
            {
                _objectid = value;
                NotifyPropertyChanged("ObjectId");
            }
        }

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 116 Таблица, содержащая ретроспективу цен по объектам с учетом корректировки на дату (MARKET_PRICE_AFTER_CORRECTION_BY_DATE_H)
    /// </summary>
    [RegisterInfo(RegisterID = 116)]
    [Serializable]
    public partial class OMPriceAfterCorrectionByDateHistory : OMBaseClass<OMPriceAfterCorrectionByDateHistory>
    {

        private long _id;
        /// <summary>
        /// 11600100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 11600100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private decimal? _pricevalueto;
        /// <summary>
        /// 11600200 Значение стоимости до (PRICE_VALUE_TO)
        /// </summary>
        [RegisterAttribute(AttributeID = 11600200)]
        public decimal? PriceValueTo
        {
            get
            {
                CheckPropertyInited("PriceValueTo");
                return _pricevalueto;
            }
            set
            {
                _pricevalueto = value;
                NotifyPropertyChanged("PriceValueTo");
            }
        }


        private long _initialid;
        /// <summary>
        /// 11600300 Идентификатор объекта (INITIAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 11600300)]
        public long InitialId
        {
            get
            {
                CheckPropertyInited("InitialId");
                return _initialid;
            }
            set
            {
                _initialid = value;
                NotifyPropertyChanged("InitialId");
            }
        }


        private DateTime _changingdate;
        /// <summary>
        /// 11600400 Время изменения цены (CHANGING_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11600400)]
        public DateTime ChangingDate
        {
            get
            {
                CheckPropertyInited("ChangingDate");
                return _changingdate;
            }
            set
            {
                _changingdate = value;
                NotifyPropertyChanged("ChangingDate");
            }
        }


        private decimal? _pricevaluefrom;
        /// <summary>
        /// 11600500 Значение стоимости от (PRICE_VALUE_FROM)
        /// </summary>
        [RegisterAttribute(AttributeID = 11600500)]
        public decimal? PriceValueFrom
        {
            get
            {
                CheckPropertyInited("PriceValueFrom");
                return _pricevaluefrom;
            }
            set
            {
                _pricevaluefrom = value;
                NotifyPropertyChanged("PriceValueFrom");
            }
        }

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 117 Таблица, содержащая настройки для коэффициентов корректировок (MARKET_CORRECTION_SETTINGS)
    /// </summary>
    [RegisterInfo(RegisterID = 117)]
    [Serializable]
    public partial class OMCorrectionSettings : OMBaseClass<OMCorrectionSettings>
    {

        private long _id;
        /// <summary>
        /// 11700100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 11700100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _correctiontype;
        /// <summary>
        /// 11700200 Тип корректировки (CORRECTION_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11700200)]
        public string CorrectionType
        {
            get
            {
                CheckPropertyInited("CorrectionType");
                return _correctiontype;
            }
            set
            {
                _correctiontype = value;
                NotifyPropertyChanged("CorrectionType");
            }
        }


        private ObjectModel.Directory.MarketObjects.CorrectionTypes _correctiontype_Code;
        /// <summary>
        /// 11700200 Тип корректировки (справочный код) (CORRECTION_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11700200)]
        public ObjectModel.Directory.MarketObjects.CorrectionTypes CorrectionType_Code
        {
            get
            {
                CheckPropertyInited("CorrectionType_Code");
                return this._correctiontype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_correctiontype))
                    {
                         _correctiontype = descr;
                    }
                }
                else
                {
                     _correctiontype = descr;
                }

                this._correctiontype_Code = value;
                NotifyPropertyChanged("CorrectionType");
                NotifyPropertyChanged("CorrectionType_Code");
            }
        }


        private string _settings;
        /// <summary>
        /// 11700300 Настройки корректировки (SETTINGS)
        /// </summary>
        [RegisterAttribute(AttributeID = 11700300)]
        public string Settings
        {
            get
            {
                CheckPropertyInited("Settings");
                return _settings;
            }
            set
            {
                _settings = value;
                NotifyPropertyChanged("Settings");
            }
        }

    }
}

namespace ObjectModel.Gbu
{
    /// <summary>
    /// 200 Объекты недвижимости (GBU_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 200)]
    [Serializable]
    public partial class OMMainObject : OMBaseClass<OMMainObject>
    {

        private long _id;
        /// <summary>
        /// 20000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 20000100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _cadastralnumber;
        /// <summary>
        /// 20000200 Кадастровый номер (CADASTRAL_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 20000200)]
        public string CadastralNumber
        {
            get
            {
                CheckPropertyInited("CadastralNumber");
                return _cadastralnumber;
            }
            set
            {
                _cadastralnumber = value;
                NotifyPropertyChanged("CadastralNumber");
            }
        }


        private string _objecttype;
        /// <summary>
        /// 20000300 Тип объекта (OBJECT_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20000300)]
        public string ObjectType
        {
            get
            {
                CheckPropertyInited("ObjectType");
                return _objecttype;
            }
            set
            {
                _objecttype = value;
                NotifyPropertyChanged("ObjectType");
            }
        }


        private PropertyTypes _objecttype_Code;
        /// <summary>
        /// 20000300 Тип объекта (справочный код) (OBJECT_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20000300)]
        public PropertyTypes ObjectType_Code
        {
            get
            {
                CheckPropertyInited("ObjectType_Code");
                return this._objecttype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_objecttype))
                    {
                         _objecttype = descr;
                    }
                }
                else
                {
                     _objecttype = descr;
                }

                this._objecttype_Code = value;
                NotifyPropertyChanged("ObjectType");
                NotifyPropertyChanged("ObjectType_Code");
            }
        }


        private bool? _isactive;
        /// <summary>
        /// 20000400 Признак активного (IS_ACTIVE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20000400)]
        public bool? IsActive
        {
            get
            {
                CheckPropertyInited("IsActive");
                return _isactive;
            }
            set
            {
                _isactive = value;
                NotifyPropertyChanged("IsActive");
            }
        }


        private string _kadastrkvartal;
        /// <summary>
        /// 20000500 Кадастровый квартал (KADASTR_KVARTAL)
        /// </summary>
        [RegisterAttribute(AttributeID = 20000500)]
        public string KadastrKvartal
        {
            get
            {
                CheckPropertyInited("KadastrKvartal");
                return _kadastrkvartal;
            }
            set
            {
                _kadastrkvartal = value;
                NotifyPropertyChanged("KadastrKvartal");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 201 Единица оценки (KO_UNIT)
    /// </summary>
    [RegisterInfo(RegisterID = 201)]
    [Serializable]
    public partial class OMUnit : OMBaseClass<OMUnit>
    {

        private long _id;
        /// <summary>
        /// 20100100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 20100100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _objectid;
        /// <summary>
        /// 20100200 Идентификатор объекта (OBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20100200)]
        public long? ObjectId
        {
            get
            {
                CheckPropertyInited("ObjectId");
                return _objectid;
            }
            set
            {
                _objectid = value;
                NotifyPropertyChanged("ObjectId");
            }
        }


        private long? _tourid;
        /// <summary>
        /// 20100300 Идентификатор тура (TOUR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20100300)]
        public long? TourId
        {
            get
            {
                CheckPropertyInited("TourId");
                return _tourid;
            }
            set
            {
                _tourid = value;
                NotifyPropertyChanged("TourId");
            }
        }


        private long? _taskid;
        /// <summary>
        /// 20100400 Идентификатор задания (TASK_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20100400)]
        public long? TaskId
        {
            get
            {
                CheckPropertyInited("TaskId");
                return _taskid;
            }
            set
            {
                _taskid = value;
                NotifyPropertyChanged("TaskId");
            }
        }


        private long? _groupid;
        /// <summary>
        /// 20100600 Идентификатор группы (GROUP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20100600)]
        public long? GroupId
        {
            get
            {
                CheckPropertyInited("GroupId");
                return _groupid;
            }
            set
            {
                _groupid = value;
                NotifyPropertyChanged("GroupId");
            }
        }


        private string _status;
        /// <summary>
        /// 20100700 Статус задания (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 20100700)]
        public string Status
        {
            get
            {
                CheckPropertyInited("Status");
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }


        private KoUnitStatus _status_Code;
        /// <summary>
        /// 20100700 Статус задания (справочный код) (STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20100700)]
        public KoUnitStatus Status_Code
        {
            get
            {
                CheckPropertyInited("Status_Code");
                return this._status_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_status))
                    {
                         _status = descr;
                    }
                }
                else
                {
                     _status = descr;
                }

                this._status_Code = value;
                NotifyPropertyChanged("Status");
                NotifyPropertyChanged("Status_Code");
            }
        }


        private DateTime? _creationdate;
        /// <summary>
        /// 20100800 Дата создания (CREATION_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20100800)]
        public DateTime? CreationDate
        {
            get
            {
                CheckPropertyInited("CreationDate");
                return _creationdate;
            }
            set
            {
                _creationdate = value;
                NotifyPropertyChanged("CreationDate");
            }
        }


        private decimal? _cadastralcost;
        /// <summary>
        /// 20100900 Кадастровая стоимость (CADASTRAL_COST)
        /// </summary>
        [RegisterAttribute(AttributeID = 20100900)]
        public decimal? CadastralCost
        {
            get
            {
                CheckPropertyInited("CadastralCost");
                return _cadastralcost;
            }
            set
            {
                _cadastralcost = value;
                NotifyPropertyChanged("CadastralCost");
            }
        }


        private decimal? _upks;
        /// <summary>
        /// 20101000 Удельная кадастровая стоимость (UPKS)
        /// </summary>
        [RegisterAttribute(AttributeID = 20101000)]
        public decimal? Upks
        {
            get
            {
                CheckPropertyInited("Upks");
                return _upks;
            }
            set
            {
                _upks = value;
                NotifyPropertyChanged("Upks");
            }
        }


        private decimal? _cadastralcostpre;
        /// <summary>
        /// 20101100 Предварительная кадастровая стоимость (CADASTRAL_COST_PRE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20101100)]
        public decimal? CadastralCostPre
        {
            get
            {
                CheckPropertyInited("CadastralCostPre");
                return _cadastralcostpre;
            }
            set
            {
                _cadastralcostpre = value;
                NotifyPropertyChanged("CadastralCostPre");
            }
        }


        private decimal? _upkspre;
        /// <summary>
        /// 20101200 Предварительная удельная кадастровая стоимость (UPKS_PRE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20101200)]
        public decimal? UpksPre
        {
            get
            {
                CheckPropertyInited("UpksPre");
                return _upkspre;
            }
            set
            {
                _upkspre = value;
                NotifyPropertyChanged("UpksPre");
            }
        }


        private string _statusresultcalc;
        /// <summary>
        /// 20101300 Результат анализа стоимости (STATUS_RESULT_CALC)
        /// </summary>
        [RegisterAttribute(AttributeID = 20101300)]
        public string StatusResultCalc
        {
            get
            {
                CheckPropertyInited("StatusResultCalc");
                return _statusresultcalc;
            }
            set
            {
                _statusresultcalc = value;
                NotifyPropertyChanged("StatusResultCalc");
            }
        }


        private KoStatusResultCalc _statusresultcalc_Code;
        /// <summary>
        /// 20101300 Результат анализа стоимости (справочный код) (STATUS_RESULT_CALC_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20101300)]
        public KoStatusResultCalc StatusResultCalc_Code
        {
            get
            {
                CheckPropertyInited("StatusResultCalc_Code");
                return this._statusresultcalc_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_statusresultcalc))
                    {
                         _statusresultcalc = descr;
                    }
                }
                else
                {
                     _statusresultcalc = descr;
                }

                this._statusresultcalc_Code = value;
                NotifyPropertyChanged("StatusResultCalc");
                NotifyPropertyChanged("StatusResultCalc_Code");
            }
        }


        private string _statusrepeatcalc;
        /// <summary>
        /// 20101400 Статус расчета (STATUS_REPEAT_CALC)
        /// </summary>
        [RegisterAttribute(AttributeID = 20101400)]
        public string StatusRepeatCalc
        {
            get
            {
                CheckPropertyInited("StatusRepeatCalc");
                return _statusrepeatcalc;
            }
            set
            {
                _statusrepeatcalc = value;
                NotifyPropertyChanged("StatusRepeatCalc");
            }
        }


        private KoStatusRepeatCalc _statusrepeatcalc_Code;
        /// <summary>
        /// 20101400 Статус расчета (справочный код) (STATUS_REPEAT_CALC_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20101400)]
        public KoStatusRepeatCalc StatusRepeatCalc_Code
        {
            get
            {
                CheckPropertyInited("StatusRepeatCalc_Code");
                return this._statusrepeatcalc_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_statusrepeatcalc))
                    {
                         _statusrepeatcalc = descr;
                    }
                }
                else
                {
                     _statusrepeatcalc = descr;
                }

                this._statusrepeatcalc_Code = value;
                NotifyPropertyChanged("StatusRepeatCalc");
                NotifyPropertyChanged("StatusRepeatCalc_Code");
            }
        }


        private decimal? _square;
        /// <summary>
        /// 20101500 Площадь (SQUARE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20101500)]
        public decimal? Square
        {
            get
            {
                CheckPropertyInited("Square");
                return _square;
            }
            set
            {
                _square = value;
                NotifyPropertyChanged("Square");
            }
        }


        private string _cadastralnumber;
        /// <summary>
        /// 20101600 Кадастровый номер (CADASTRAL_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 20101600)]
        public string CadastralNumber
        {
            get
            {
                CheckPropertyInited("CadastralNumber");
                return _cadastralnumber;
            }
            set
            {
                _cadastralnumber = value;
                NotifyPropertyChanged("CadastralNumber");
            }
        }


        private string _cadastralblock;
        /// <summary>
        /// 20101700 Кадастровый квартал (CADASTRAL_BLOCK)
        /// </summary>
        [RegisterAttribute(AttributeID = 20101700)]
        public string CadastralBlock
        {
            get
            {
                CheckPropertyInited("CadastralBlock");
                return _cadastralblock;
            }
            set
            {
                _cadastralblock = value;
                NotifyPropertyChanged("CadastralBlock");
            }
        }


        private string _propertytype;
        /// <summary>
        /// 20101800 Тип объекта (PROPERTY_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20101800)]
        public string PropertyType
        {
            get
            {
                CheckPropertyInited("PropertyType");
                return _propertytype;
            }
            set
            {
                _propertytype = value;
                NotifyPropertyChanged("PropertyType");
            }
        }


        private PropertyTypes _propertytype_Code;
        /// <summary>
        /// 20101800 Тип объекта (справочный код) (PROPERTY_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20101800)]
        public PropertyTypes PropertyType_Code
        {
            get
            {
                CheckPropertyInited("PropertyType_Code");
                return this._propertytype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_propertytype))
                    {
                         _propertytype = descr;
                    }
                }
                else
                {
                     _propertytype = descr;
                }

                this._propertytype_Code = value;
                NotifyPropertyChanged("PropertyType");
                NotifyPropertyChanged("PropertyType_Code");
            }
        }


        private long? _degreereadiness;
        /// <summary>
        /// 20101900 Степень готовности в процента (DEGREE_READINESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 20101900)]
        public long? DegreeReadiness
        {
            get
            {
                CheckPropertyInited("DegreeReadiness");
                return _degreereadiness;
            }
            set
            {
                _degreereadiness = value;
                NotifyPropertyChanged("DegreeReadiness");
            }
        }


        private string _parentcalcnumber;
        /// <summary>
        /// 20102000 Объект, по которому было рассчитано среднее/минимальное значение (PARENT_CALC_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 20102000)]
        public string ParentCalcNumber
        {
            get
            {
                CheckPropertyInited("ParentCalcNumber");
                return _parentcalcnumber;
            }
            set
            {
                _parentcalcnumber = value;
                NotifyPropertyChanged("ParentCalcNumber");
            }
        }


        private string _parentcalctype;
        /// <summary>
        /// 20102100 Тип объекта, по которому было рассчитано среднее/минимальное значение ()
        /// </summary>
        [RegisterAttribute(AttributeID = 20102100)]
        public string ParentCalcType
        {
            get
            {
                CheckPropertyInited("ParentCalcType");
                return _parentcalctype;
            }
            set
            {
                _parentcalctype = value;
                NotifyPropertyChanged("ParentCalcType");
            }
        }


        private KoParentCalcType _parentcalctype_Code;
        /// <summary>
        /// 20102100 Тип объекта, по которому было рассчитано среднее/минимальное значение (справочный код) (PARENT_CALC_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20102100)]
        public KoParentCalcType ParentCalcType_Code
        {
            get
            {
                CheckPropertyInited("ParentCalcType_Code");
                return this._parentcalctype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_parentcalctype))
                    {
                         _parentcalctype = descr;
                    }
                }
                else
                {
                     _parentcalctype = descr;
                }

                this._parentcalctype_Code = value;
                NotifyPropertyChanged("ParentCalcType");
                NotifyPropertyChanged("ParentCalcType_Code");
            }
        }


        private bool? _useasprototype;
        /// <summary>
        /// 20102200 Признак использования в качестве эталонного объекта (USE_AS_PROTOTYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20102200)]
        public bool? UseAsPrototype
        {
            get
            {
                CheckPropertyInited("UseAsPrototype");
                return _useasprototype;
            }
            set
            {
                _useasprototype = value;
                NotifyPropertyChanged("UseAsPrototype");
            }
        }


        private long? _responsedocid;
        /// <summary>
        /// 20102300 Идентификатор исходящего документа (RESPONSE_DOCUMENT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20102300)]
        public long? ResponseDocId
        {
            get
            {
                CheckPropertyInited("ResponseDocId");
                return _responsedocid;
            }
            set
            {
                _responsedocid = value;
                NotifyPropertyChanged("ResponseDocId");
            }
        }


        private string _buildingcadastralnumber;
        /// <summary>
        /// 20102400 Кадастровый номер здания (BUILDING_CADASTRAL_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 20102400)]
        public string BuildingCadastralNumber
        {
            get
            {
                CheckPropertyInited("BuildingCadastralNumber");
                return _buildingcadastralnumber;
            }
            set
            {
                _buildingcadastralnumber = value;
                NotifyPropertyChanged("BuildingCadastralNumber");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 202 Тур оценки (KO_TOUR)
    /// </summary>
    [RegisterInfo(RegisterID = 202)]
    [Serializable]
    public partial class OMTour : OMBaseClass<OMTour>
    {

        private long _id;
        /// <summary>
        /// 20200100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 20200100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _year;
        /// <summary>
        /// 20200200 Год проведения тура (YEAR)
        /// </summary>
        [RegisterAttribute(AttributeID = 20200200)]
        public long? Year
        {
            get
            {
                CheckPropertyInited("Year");
                return _year;
            }
            set
            {
                _year = value;
                NotifyPropertyChanged("Year");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 203 Задание на оценку (KO_TASK)
    /// </summary>
    [RegisterInfo(RegisterID = 203)]
    [Serializable]
    public partial class OMTask : OMBaseClass<OMTask>
    {

        private long _id;
        /// <summary>
        /// 20300100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 20300100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private DateTime? _creationdate;
        /// <summary>
        /// 20300200 Дата создания (CREATION_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20300200)]
        public DateTime? CreationDate
        {
            get
            {
                CheckPropertyInited("CreationDate");
                return _creationdate;
            }
            set
            {
                _creationdate = value;
                NotifyPropertyChanged("CreationDate");
            }
        }


        private long? _documentid;
        /// <summary>
        /// 20300300 Идентификатор входящего документа (DOCUMENT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20300300)]
        public long? DocumentId
        {
            get
            {
                CheckPropertyInited("DocumentId");
                return _documentid;
            }
            set
            {
                _documentid = value;
                NotifyPropertyChanged("DocumentId");
            }
        }


        private string _notetype;
        /// <summary>
        /// 20300400 Тип статьи (NOTE_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20300400)]
        public string NoteType
        {
            get
            {
                CheckPropertyInited("NoteType");
                return _notetype;
            }
            set
            {
                _notetype = value;
                NotifyPropertyChanged("NoteType");
            }
        }


        private KoNoteType _notetype_Code;
        /// <summary>
        /// 20300400 Тип статьи (справочный код) (NOTE_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20300400)]
        public KoNoteType NoteType_Code
        {
            get
            {
                CheckPropertyInited("NoteType_Code");
                return this._notetype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_notetype))
                    {
                         _notetype = descr;
                    }
                }
                else
                {
                     _notetype = descr;
                }

                this._notetype_Code = value;
                NotifyPropertyChanged("NoteType");
                NotifyPropertyChanged("NoteType_Code");
            }
        }


        private long? _tourid;
        /// <summary>
        /// 20300500 Идентификатор тура (TOUR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20300500)]
        public long? TourId
        {
            get
            {
                CheckPropertyInited("TourId");
                return _tourid;
            }
            set
            {
                _tourid = value;
                NotifyPropertyChanged("TourId");
            }
        }


        private long? _responsedocid;
        /// <summary>
        /// 20300600 Идентификатор исходящего документа (RESPONSE_DOCUMENT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20300600)]
        public long? ResponseDocId
        {
            get
            {
                CheckPropertyInited("ResponseDocId");
                return _responsedocid;
            }
            set
            {
                _responsedocid = value;
                NotifyPropertyChanged("ResponseDocId");
            }
        }


        private string _status;
        /// <summary>
        /// 20300700 Статус (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 20300700)]
        public string Status
        {
            get
            {
                CheckPropertyInited("Status");
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }


        private KoTaskStatus _status_Code;
        /// <summary>
        /// 20300700 Статус (справочный код) (STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20300700)]
        public KoTaskStatus Status_Code
        {
            get
            {
                CheckPropertyInited("Status_Code");
                return this._status_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_status))
                    {
                         _status = descr;
                    }
                }
                else
                {
                     _status = descr;
                }

                this._status_Code = value;
                NotifyPropertyChanged("Status");
                NotifyPropertyChanged("Status_Code");
            }
        }


        private DateTime? _estimationdate;
        /// <summary>
        /// 20300800 Дата оценки (ESTIMATION_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20300800)]
        public DateTime? EstimationDate
        {
            get
            {
                CheckPropertyInited("EstimationDate");
                return _estimationdate;
            }
            set
            {
                _estimationdate = value;
                NotifyPropertyChanged("EstimationDate");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 205 Группы/Подгруппы (KO_GROUP)
    /// </summary>
    [RegisterInfo(RegisterID = 205)]
    [Serializable]
    public partial class OMGroup : OMBaseClass<OMGroup>
    {

        private long _id;
        /// <summary>
        /// 20500100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 20500100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _parentid;
        /// <summary>
        /// 20500200 Идентификатор родительской группы (PARENT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20500200)]
        public long? ParentId
        {
            get
            {
                CheckPropertyInited("ParentId");
                return _parentid;
            }
            set
            {
                _parentid = value;
                NotifyPropertyChanged("ParentId");
            }
        }


        private string _groupname;
        /// <summary>
        /// 20500300 Наименование группы (GROUP_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 20500300)]
        public string GroupName
        {
            get
            {
                CheckPropertyInited("GroupName");
                return _groupname;
            }
            set
            {
                _groupname = value;
                NotifyPropertyChanged("GroupName");
            }
        }


        private string _groupalgoritm;
        /// <summary>
        /// 20500400 Механизм группировки ()
        /// </summary>
        [RegisterAttribute(AttributeID = 20500400)]
        public string GroupAlgoritm
        {
            get
            {
                CheckPropertyInited("GroupAlgoritm");
                return _groupalgoritm;
            }
            set
            {
                _groupalgoritm = value;
                NotifyPropertyChanged("GroupAlgoritm");
            }
        }


        private KoGroupAlgoritm _groupalgoritm_Code;
        /// <summary>
        /// 20500400 Механизм группировки (справочный код) (GROUP_ALGORITM)
        /// </summary>
        [RegisterAttribute(AttributeID = 20500400)]
        public KoGroupAlgoritm GroupAlgoritm_Code
        {
            get
            {
                CheckPropertyInited("GroupAlgoritm_Code");
                return this._groupalgoritm_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_groupalgoritm))
                    {
                         _groupalgoritm = descr;
                    }
                }
                else
                {
                     _groupalgoritm = descr;
                }

                this._groupalgoritm_Code = value;
                NotifyPropertyChanged("GroupAlgoritm");
                NotifyPropertyChanged("GroupAlgoritm_Code");
            }
        }


        private string _number;
        /// <summary>
        /// 20500500 Номер (NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 20500500)]
        public string Number
        {
            get
            {
                CheckPropertyInited("Number");
                return _number;
            }
            set
            {
                _number = value;
                NotifyPropertyChanged("Number");
            }
        }


        private string _appliedapproachesincadastralcost;
        /// <summary>
        /// 20500600 Настройки для разъяснений: Примененные подходы при определении КС объекта недвижимости (dop_podhod)
        /// </summary>
        [RegisterAttribute(AttributeID = 20500600)]
        public string AppliedApproachesInCadastralCost
        {
            get
            {
                CheckPropertyInited("AppliedApproachesInCadastralCost");
                return _appliedapproachesincadastralcost;
            }
            set
            {
                _appliedapproachesincadastralcost = value;
                NotifyPropertyChanged("AppliedApproachesInCadastralCost");
            }
        }


        private string _appliedevaluationmethodsincadastralcost;
        /// <summary>
        /// 20500700 Настройки для разъяснений: Примененные методы оценки при определении КС объекта недвижимости (dop_metod)
        /// </summary>
        [RegisterAttribute(AttributeID = 20500700)]
        public string AppliedEvaluationMethodsInCadastralCost
        {
            get
            {
                CheckPropertyInited("AppliedEvaluationMethodsInCadastralCost");
                return _appliedevaluationmethodsincadastralcost;
            }
            set
            {
                _appliedevaluationmethodsincadastralcost = value;
                NotifyPropertyChanged("AppliedEvaluationMethodsInCadastralCost");
            }
        }


        private string _cadastralcostdetermingmethod;
        /// <summary>
        /// 20500800 Настройки для разъяснений: Способ определения КС объекта недвижимости (dop_sposob)
        /// </summary>
        [RegisterAttribute(AttributeID = 20500800)]
        public string CadastralCostDetermingMethod
        {
            get
            {
                CheckPropertyInited("CadastralCostDetermingMethod");
                return _cadastralcostdetermingmethod;
            }
            set
            {
                _cadastralcostdetermingmethod = value;
                NotifyPropertyChanged("CadastralCostDetermingMethod");
            }
        }


        private string _modeljustification;
        /// <summary>
        /// 20500900 Настройки для разъяснений: Обоснование модели (dop_model)
        /// </summary>
        [RegisterAttribute(AttributeID = 20500900)]
        public string ModelJustification
        {
            get
            {
                CheckPropertyInited("ModelJustification");
                return _modeljustification;
            }
            set
            {
                _modeljustification = value;
                NotifyPropertyChanged("ModelJustification");
            }
        }


        private string _objectssegment;
        /// <summary>
        /// 20501000 Настройки для разъяснений: Сегмент объектов недвижимости (dop_segment)
        /// </summary>
        [RegisterAttribute(AttributeID = 20501000)]
        public string ObjectsSegment
        {
            get
            {
                CheckPropertyInited("ObjectsSegment");
                return _objectssegment;
            }
            set
            {
                _objectssegment = value;
                NotifyPropertyChanged("ObjectsSegment");
            }
        }


        private string _objectssubgroup;
        /// <summary>
        /// 20501100 Настройки для разъяснений: Группа (подгруппа) объектов недвижимости (dop_group)
        /// </summary>
        [RegisterAttribute(AttributeID = 20501100)]
        public string ObjectsSubgroup
        {
            get
            {
                CheckPropertyInited("ObjectsSubgroup");
                return _objectssubgroup;
            }
            set
            {
                _objectssubgroup = value;
                NotifyPropertyChanged("ObjectsSubgroup");
            }
        }


        private string _cadastralcostcalculationorderdescription;
        /// <summary>
        /// 20501200 Настройки для разъяснений: Краткое описание последовательности определения КС объекта недвижимости (dop_opisanie)
        /// </summary>
        [RegisterAttribute(AttributeID = 20501200)]
        public string CadastralCostCalculationOrderDescription
        {
            get
            {
                CheckPropertyInited("CadastralCostCalculationOrderDescription");
                return _cadastralcostcalculationorderdescription;
            }
            set
            {
                _cadastralcostcalculationorderdescription = value;
                NotifyPropertyChanged("CadastralCostCalculationOrderDescription");
            }
        }


        private string _pricezonecharacteristic;
        /// <summary>
        /// 20501300 Настройки для разъяснений: Характеристика ценовой зоны, в которой находится объект недвижимости (m_zone)
        /// </summary>
        [RegisterAttribute(AttributeID = 20501300)]
        public string PriceZoneCharacteristic
        {
            get
            {
                CheckPropertyInited("PriceZoneCharacteristic");
                return _pricezonecharacteristic;
            }
            set
            {
                _pricezonecharacteristic = value;
                NotifyPropertyChanged("PriceZoneCharacteristic");
            }
        }


        private string _marketsegment;
        /// <summary>
        /// 20501400 Настройки для разъяснений: Сегмент рынка недвижимости, к которому отнесен объект недвижимости (m_segment)
        /// </summary>
        [RegisterAttribute(AttributeID = 20501400)]
        public string MarketSegment
        {
            get
            {
                CheckPropertyInited("MarketSegment");
                return _marketsegment;
            }
            set
            {
                _marketsegment = value;
                NotifyPropertyChanged("MarketSegment");
            }
        }


        private string _marketsegmentfunctioningfeatures;
        /// <summary>
        /// 20501500 Настройки для разъяснений: Краткая характеристика особенностей функционирования сегмента рынка объектов недвижимости (m_har)
        /// </summary>
        [RegisterAttribute(AttributeID = 20501500)]
        public string MarketSegmentFunctioningFeatures
        {
            get
            {
                CheckPropertyInited("MarketSegmentFunctioningFeatures");
                return _marketsegmentfunctioningfeatures;
            }
            set
            {
                _marketsegmentfunctioningfeatures = value;
                NotifyPropertyChanged("MarketSegmentFunctioningFeatures");
            }
        }


        private string _cadastralcostestimationmodelsreferences;
        /// <summary>
        /// 20501600 Настройки для акта определения: Ссылки на модели оценки кадастровой стоимости (act_model)
        /// </summary>
        [RegisterAttribute(AttributeID = 20501600)]
        public string CadastralCostEstimationModelsReferences
        {
            get
            {
                CheckPropertyInited("CadastralCostEstimationModelsReferences");
                return _cadastralcostestimationmodelsreferences;
            }
            set
            {
                _cadastralcostestimationmodelsreferences = value;
                NotifyPropertyChanged("CadastralCostEstimationModelsReferences");
            }
        }


        private string _assumptionsreference;
        /// <summary>
        /// 20501700 Настройки для акта определения: Ссылка на допущения (act_dop)
        /// </summary>
        [RegisterAttribute(AttributeID = 20501700)]
        public string AssumptionsReference
        {
            get
            {
                CheckPropertyInited("AssumptionsReference");
                return _assumptionsreference;
            }
            set
            {
                _assumptionsreference = value;
                NotifyPropertyChanged("AssumptionsReference");
            }
        }


        private string _othercostrelatedinfo;
        /// <summary>
        /// 20501800 Настройки для акта определения: Иная отражающаяся на стоимости информация (act_other)
        /// </summary>
        [RegisterAttribute(AttributeID = 20501800)]
        public string OtherCostRelatedInfo
        {
            get
            {
                CheckPropertyInited("OtherCostRelatedInfo");
                return _othercostrelatedinfo;
            }
            set
            {
                _othercostrelatedinfo = value;
                NotifyPropertyChanged("OtherCostRelatedInfo");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 206 Модель (KO_MODEL)
    /// </summary>
    [RegisterInfo(RegisterID = 206)]
    [Serializable]
    public partial class OMModel : OMBaseClass<OMModel>
    {

        private long _id;
        /// <summary>
        /// 20600100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 20600100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _groupid;
        /// <summary>
        /// 20600200 Идентификатор группы (GROUP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20600200)]
        public long? GroupId
        {
            get
            {
                CheckPropertyInited("GroupId");
                return _groupid;
            }
            set
            {
                _groupid = value;
                NotifyPropertyChanged("GroupId");
            }
        }


        private string _name;
        /// <summary>
        /// 20600300 Наименование (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 20600300)]
        public string Name
        {
            get
            {
                CheckPropertyInited("Name");
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }


        private string _description;
        /// <summary>
        /// 20600400 Описание (DESCRIPTION)
        /// </summary>
        [RegisterAttribute(AttributeID = 20600400)]
        public string Description
        {
            get
            {
                CheckPropertyInited("Description");
                return _description;
            }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }


        private string _formula;
        /// <summary>
        /// 20600500 Формула (FORMULA)
        /// </summary>
        [RegisterAttribute(AttributeID = 20600500)]
        public string Formula
        {
            get
            {
                CheckPropertyInited("Formula");
                return _formula;
            }
            set
            {
                _formula = value;
                NotifyPropertyChanged("Formula");
            }
        }


        private string _algoritmtype;
        /// <summary>
        /// 20600600 Алгоритм расчета ()
        /// </summary>
        [RegisterAttribute(AttributeID = 20600600)]
        public string AlgoritmType
        {
            get
            {
                CheckPropertyInited("AlgoritmType");
                return _algoritmtype;
            }
            set
            {
                _algoritmtype = value;
                NotifyPropertyChanged("AlgoritmType");
            }
        }


        private KoAlgoritmType _algoritmtype_Code;
        /// <summary>
        /// 20600600 Алгоритм расчета (справочный код) (ALGORITM_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20600600)]
        public KoAlgoritmType AlgoritmType_Code
        {
            get
            {
                CheckPropertyInited("AlgoritmType_Code");
                return this._algoritmtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_algoritmtype))
                    {
                         _algoritmtype = descr;
                    }
                }
                else
                {
                     _algoritmtype = descr;
                }

                this._algoritmtype_Code = value;
                NotifyPropertyChanged("AlgoritmType");
                NotifyPropertyChanged("AlgoritmType_Code");
            }
        }


        private decimal? _a0;
        /// <summary>
        /// 20600700 Cвободный член в формуле (A0)
        /// </summary>
        [RegisterAttribute(AttributeID = 20600700)]
        public decimal? A0
        {
            get
            {
                CheckPropertyInited("A0");
                return _a0;
            }
            set
            {
                _a0 = value;
                NotifyPropertyChanged("A0");
            }
        }


        private string _calculationtype;
        /// <summary>
        /// 20600800 Тип расчета ()
        /// </summary>
        [RegisterAttribute(AttributeID = 20600800)]
        public string CalculationType
        {
            get
            {
                CheckPropertyInited("CalculationType");
                return _calculationtype;
            }
            set
            {
                _calculationtype = value;
                NotifyPropertyChanged("CalculationType");
            }
        }


        private KoCalculationType _calculationtype_Code;
        /// <summary>
        /// 20600800 Тип расчета (справочный код) (CALCULATION_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20600800)]
        public KoCalculationType CalculationType_Code
        {
            get
            {
                CheckPropertyInited("CalculationType_Code");
                return this._calculationtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_calculationtype))
                    {
                         _calculationtype = descr;
                    }
                }
                else
                {
                     _calculationtype = descr;
                }

                this._calculationtype_Code = value;
                NotifyPropertyChanged("CalculationType");
                NotifyPropertyChanged("CalculationType_Code");
            }
        }


        private string _calculationmethod;
        /// <summary>
        /// 20600900 Метод расчета ()
        /// </summary>
        [RegisterAttribute(AttributeID = 20600900)]
        public string CalculationMethod
        {
            get
            {
                CheckPropertyInited("CalculationMethod");
                return _calculationmethod;
            }
            set
            {
                _calculationmethod = value;
                NotifyPropertyChanged("CalculationMethod");
            }
        }


        private KoCalculationMethod _calculationmethod_Code;
        /// <summary>
        /// 20600900 Метод расчета (справочный код) (CALCULATION_METHOD)
        /// </summary>
        [RegisterAttribute(AttributeID = 20600900)]
        public KoCalculationMethod CalculationMethod_Code
        {
            get
            {
                CheckPropertyInited("CalculationMethod_Code");
                return this._calculationmethod_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_calculationmethod))
                    {
                         _calculationmethod = descr;
                    }
                }
                else
                {
                     _calculationmethod = descr;
                }

                this._calculationmethod_Code = value;
                NotifyPropertyChanged("CalculationMethod");
                NotifyPropertyChanged("CalculationMethod_Code");
            }
        }


        private string _lineartrainingresult;
        /// <summary>
        /// 20601100 Результат обучения по линейной формуле (LINEAR_TRAINING_RESULT)
        /// </summary>
        [RegisterAttribute(AttributeID = 20601100)]
        public string LinearTrainingResult
        {
            get
            {
                CheckPropertyInited("LinearTrainingResult");
                return _lineartrainingresult;
            }
            set
            {
                _lineartrainingresult = value;
                NotifyPropertyChanged("LinearTrainingResult");
            }
        }


        private string _exponentialtrainingresult;
        /// <summary>
        /// 20601200 Результат обучения по экспоненциальной формуле (EXPONENTIAL_TRAINING_RESULT)
        /// </summary>
        [RegisterAttribute(AttributeID = 20601200)]
        public string ExponentialTrainingResult
        {
            get
            {
                CheckPropertyInited("ExponentialTrainingResult");
                return _exponentialtrainingresult;
            }
            set
            {
                _exponentialtrainingresult = value;
                NotifyPropertyChanged("ExponentialTrainingResult");
            }
        }


        private string _multiplicativetrainingresult;
        /// <summary>
        /// 20601300 Результат обучения по мультипликативной формуле (MULTIPLICATIVE_TRAINING_RESULT)
        /// </summary>
        [RegisterAttribute(AttributeID = 20601300)]
        public string MultiplicativeTrainingResult
        {
            get
            {
                CheckPropertyInited("MultiplicativeTrainingResult");
                return _multiplicativetrainingresult;
            }
            set
            {
                _multiplicativetrainingresult = value;
                NotifyPropertyChanged("MultiplicativeTrainingResult");
            }
        }


        private bool? _isoksobjecttype;
        /// <summary>
        /// 20601400 Тип объекта (IS_OKS_OBJECT_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20601400)]
        public bool? IsOksObjectType
        {
            get
            {
                CheckPropertyInited("IsOksObjectType");
                return _isoksobjecttype;
            }
            set
            {
                _isoksobjecttype = value;
                NotifyPropertyChanged("IsOksObjectType");
            }
        }

    }
}

namespace ObjectModel.Ko
{
    /// <summary>
    /// 207 Модель типизированная (ko_model_typified)
    /// </summary>
    [RegisterInfo(RegisterID = 207)]
    [Serializable]
    public partial class OMModelTypified : OMBaseClass<OMModelTypified>
    {

        private long _id;
        /// <summary>
        /// 20700100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 20700100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _modelid;
        /// <summary>
        /// 20700200 ИД модели (model_id)
        /// </summary>
        [RegisterAttribute(AttributeID = 20700200)]
        public long ModelId
        {
            get
            {
                CheckPropertyInited("ModelId");
                return _modelid;
            }
            set
            {
                _modelid = value;
                NotifyPropertyChanged("ModelId");
            }
        }


        private string _algoritmtype;
        /// <summary>
        /// 20700300 Алгоритм расчета ()
        /// </summary>
        [RegisterAttribute(AttributeID = 20700300)]
        public string AlgoritmType
        {
            get
            {
                CheckPropertyInited("AlgoritmType");
                return _algoritmtype;
            }
            set
            {
                _algoritmtype = value;
                NotifyPropertyChanged("AlgoritmType");
            }
        }


        private KoAlgoritmType _algoritmtype_Code;
        /// <summary>
        /// 20700300 Алгоритм расчета (справочный код) (ALGORITM_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20700300)]
        public KoAlgoritmType AlgoritmType_Code
        {
            get
            {
                CheckPropertyInited("AlgoritmType_Code");
                return this._algoritmtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_algoritmtype))
                    {
                         _algoritmtype = descr;
                    }
                }
                else
                {
                     _algoritmtype = descr;
                }

                this._algoritmtype_Code = value;
                NotifyPropertyChanged("AlgoritmType");
                NotifyPropertyChanged("AlgoritmType_Code");
            }
        }


        private string _formula;
        /// <summary>
        /// 20700400 Формула ()
        /// </summary>
        [RegisterAttribute(AttributeID = 20700400)]
        public string Formula
        {
            get
            {
                CheckPropertyInited("Formula");
                return _formula;
            }
            set
            {
                _formula = value;
                NotifyPropertyChanged("Formula");
            }
        }


        private string _trainingresult;
        /// <summary>
        /// 20700500 Результаты обучения (коэффициенты от сервиса Моделирования) (training_result)
        /// </summary>
        [RegisterAttribute(AttributeID = 20700500)]
        public string TrainingResult
        {
            get
            {
                CheckPropertyInited("TrainingResult");
                return _trainingresult;
            }
            set
            {
                _trainingresult = value;
                NotifyPropertyChanged("TrainingResult");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 208 Факторы группы (KO_GROUP_FACTOR)
    /// </summary>
    [RegisterInfo(RegisterID = 208)]
    [Serializable]
    public partial class OMGroupFactor : OMBaseClass<OMGroupFactor>
    {

        private long _id;
        /// <summary>
        /// 20800100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 20800100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _groupid;
        /// <summary>
        /// 20800200 Идентификатор группы (GROUP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20800200)]
        public long? GroupId
        {
            get
            {
                CheckPropertyInited("GroupId");
                return _groupid;
            }
            set
            {
                _groupid = value;
                NotifyPropertyChanged("GroupId");
            }
        }


        private long? _factorid;
        /// <summary>
        /// 20800300 Идентификатор фактора (FACTOR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20800300)]
        public long? FactorId
        {
            get
            {
                CheckPropertyInited("FactorId");
                return _factorid;
            }
            set
            {
                _factorid = value;
                NotifyPropertyChanged("FactorId");
            }
        }


        private bool? _signmarket;
        /// <summary>
        /// 20800400 Признак использования метки (SIGN_NARKET)
        /// </summary>
        [RegisterAttribute(AttributeID = 20800400)]
        public bool? SignMarket
        {
            get
            {
                CheckPropertyInited("SignMarket");
                return _signmarket;
            }
            set
            {
                _signmarket = value;
                NotifyPropertyChanged("SignMarket");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 210 Факторы модели (KO_MODEL_FACTOR)
    /// </summary>
    [RegisterInfo(RegisterID = 210)]
    [Serializable]
    public partial class OMModelFactor : OMBaseClass<OMModelFactor>
    {

        private long _id;
        /// <summary>
        /// 21000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 21000100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

        //TODO CIPJSKO-526: удалить колонку и связь select * from core_register_relation where id=209
        private long? _modelid;
        /// <summary>
        /// 21000200 Идентификатор модели (MODEL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21000200)]
        public long? ModelId
        {
            get
            {
                CheckPropertyInited("ModelId");
                return _modelid;
            }
            set
            {
                _modelid = value;
                NotifyPropertyChanged("ModelId");
            }
        }


        private long? _factorid;
        /// <summary>
        /// 21000300 Идентификатор фактора (FACTOR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21000300)]
        public long? FactorId
        {
            get
            {
                CheckPropertyInited("FactorId");
                return _factorid;
            }
            set
            {
                _factorid = value;
                NotifyPropertyChanged("FactorId");
            }
        }


        private long? _markerid;
        /// <summary>
        /// 21000400 Идентификатор метки (MARKER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21000400)]
        public long? MarkerId
        {
            get
            {
                CheckPropertyInited("MarkerId");
                return _markerid;
            }
            set
            {
                _markerid = value;
                NotifyPropertyChanged("MarkerId");
            }
        }


        private decimal _weight;
        /// <summary>
        /// 21000500 Вес фактора (WEIGHT)
        /// </summary>
        [RegisterAttribute(AttributeID = 21000500)]
        public decimal Weight
        {
            get
            {
                CheckPropertyInited("Weight");
                return _weight;
            }
            set
            {
                _weight = value;
                NotifyPropertyChanged("Weight");
            }
        }


        private decimal _b0;
        /// <summary>
        /// 21000600 Добавочный коэффициент (B0)
        /// </summary>
        [RegisterAttribute(AttributeID = 21000600)]
        public decimal B0
        {
            get
            {
                CheckPropertyInited("B0");
                return _b0;
            }
            set
            {
                _b0 = value;
                NotifyPropertyChanged("B0");
            }
        }


        private bool _signdiv;
        /// <summary>
        /// 21000700 Признак деления на фактор (SIGN_DIV)
        /// </summary>
        [RegisterAttribute(AttributeID = 21000700)]
        public bool SignDiv
        {
            get
            {
                CheckPropertyInited("SignDiv");
                return _signdiv;
            }
            set
            {
                _signdiv = value;
                NotifyPropertyChanged("SignDiv");
            }
        }


        private bool _signadd;
        /// <summary>
        /// 21000800 Признак сложения (SIGN_ADD)
        /// </summary>
        [RegisterAttribute(AttributeID = 21000800)]
        public bool SignAdd
        {
            get
            {
                CheckPropertyInited("SignAdd");
                return _signadd;
            }
            set
            {
                _signadd = value;
                NotifyPropertyChanged("SignAdd");
            }
        }


        private bool _signmarket;
        /// <summary>
        /// 21000900 Признак использования метки (SIGN_MARKET)
        /// </summary>
        [RegisterAttribute(AttributeID = 21000900)]
        public bool SignMarket
        {
            get
            {
                CheckPropertyInited("SignMarket");
                return _signmarket;
            }
            set
            {
                _signmarket = value;
                NotifyPropertyChanged("SignMarket");
            }
        }


        private long? _dictionaryid;
        /// <summary>
        /// 21001000 Идентификатор словаря (DICTIONARY_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21001000)]
        public long? DictionaryId
        {
            get
            {
                CheckPropertyInited("DictionaryId");
                return _dictionaryid;
            }
            set
            {
                _dictionaryid = value;
                NotifyPropertyChanged("DictionaryId");
            }
        }


        private long? _typifiedmodelid;
        /// <summary>
        /// 21001100 Идентификатор типизированной модели (typified_model_id)
        /// </summary>
        [RegisterAttribute(AttributeID = 21001100)]
        public long? TypifiedModelId
        {
            get
            {
                CheckPropertyInited("TypifiedModelId");
                return _typifiedmodelid;
            }
            set
            {
                _typifiedmodelid = value;
                NotifyPropertyChanged("TypifiedModelId");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 211 Справочник меток (KO_MARK_CATALOG)
    /// </summary>
    [RegisterInfo(RegisterID = 211)]
    [Serializable]
    public partial class OMMarkCatalog : OMBaseClass<OMMarkCatalog>
    {

        private long _id;
        /// <summary>
        /// 21100100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 21100100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _groupid;
        /// <summary>
        /// 21100200 Идентификатор группы (GROUP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21100200)]
        public long? GroupId
        {
            get
            {
                CheckPropertyInited("GroupId");
                return _groupid;
            }
            set
            {
                _groupid = value;
                NotifyPropertyChanged("GroupId");
            }
        }


        private long? _factorid;
        /// <summary>
        /// 21100300 Идентификатор фактора (FACTOR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21100300)]
        public long? FactorId
        {
            get
            {
                CheckPropertyInited("FactorId");
                return _factorid;
            }
            set
            {
                _factorid = value;
                NotifyPropertyChanged("FactorId");
            }
        }


        private string _valuefactor;
        /// <summary>
        /// 21100400 Значение фактора (VALUE_FACTOR)
        /// </summary>
        [RegisterAttribute(AttributeID = 21100400)]
        public string ValueFactor
        {
            get
            {
                CheckPropertyInited("ValueFactor");
                return _valuefactor;
            }
            set
            {
                _valuefactor = value;
                NotifyPropertyChanged("ValueFactor");
            }
        }


        private decimal? _metkafactor;
        /// <summary>
        /// 21100500 Значение метки (METKA_FACTOR)
        /// </summary>
        [RegisterAttribute(AttributeID = 21100500)]
        public decimal? MetkaFactor
        {
            get
            {
                CheckPropertyInited("MetkaFactor");
                return _metkafactor;
            }
            set
            {
                _metkafactor = value;
                NotifyPropertyChanged("MetkaFactor");
            }
        }


        private long? _generalmodelid;
        /// <summary>
        /// 21100600 ИД основной модели (206 реестр) (general_model_id)
        /// </summary>
        [RegisterAttribute(AttributeID = 21100600)]
        public long? GeneralModelId
        {
            get
            {
                CheckPropertyInited("GeneralModelId");
                return _generalmodelid;
            }
            set
            {
                _generalmodelid = value;
                NotifyPropertyChanged("GeneralModelId");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 212 Группы тура (KO_TOUR_GROUPS)
    /// </summary>
    [RegisterInfo(RegisterID = 212)]
    [Serializable]
    public partial class OMTourGroup : OMBaseClass<OMTourGroup>
    {

        private long _id;
        /// <summary>
        /// 21200100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 21200100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _tourid;
        /// <summary>
        /// 21200200 Идентификатор тура (TOUR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21200200)]
        public long TourId
        {
            get
            {
                CheckPropertyInited("TourId");
                return _tourid;
            }
            set
            {
                _tourid = value;
                NotifyPropertyChanged("TourId");
            }
        }


        private long _groupid;
        /// <summary>
        /// 21200300 Идентификатор группы (GROUP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21200300)]
        public long GroupId
        {
            get
            {
                CheckPropertyInited("GroupId");
                return _groupid;
            }
            set
            {
                _groupid = value;
                NotifyPropertyChanged("GroupId");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 213 Соответствие факторов учетной и расчетной части (KO_ATTRIBUTE_MAP)
    /// </summary>
    [RegisterInfo(RegisterID = 213)]
    [Serializable]
    public partial class OMAttributeMap : OMBaseClass<OMAttributeMap>
    {

        private long _id;
        /// <summary>
        /// 21300100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 21300100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _gbuattributeid;
        /// <summary>
        /// 21300200 Идентификатор атрибута реестровой части (GBU_ATTRIBUTE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21300200)]
        public long GbuAttributeId
        {
            get
            {
                CheckPropertyInited("GbuAttributeId");
                return _gbuattributeid;
            }
            set
            {
                _gbuattributeid = value;
                NotifyPropertyChanged("GbuAttributeId");
            }
        }


        private long _koattributeid;
        /// <summary>
        /// 21300300 Идентификатор атрибута расчетной части (KO_ATTRIBUTE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21300300)]
        public long KoAttributeId
        {
            get
            {
                CheckPropertyInited("KoAttributeId");
                return _koattributeid;
            }
            set
            {
                _koattributeid = value;
                NotifyPropertyChanged("KoAttributeId");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 214 Справочник ЦОД (KO_COD_DICTIONARY)
    /// </summary>
    [RegisterInfo(RegisterID = 214)]
    [Serializable]
    public partial class OMCodDictionary : OMBaseClass<OMCodDictionary>
    {

        private long _id;
        /// <summary>
        /// 21400100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 21400100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _idcodjob;
        /// <summary>
        /// 21400200 Идентификатор задания ЦОД (ID_CODJOB)
        /// </summary>
        [RegisterAttribute(AttributeID = 21400200)]
        public long IdCodjob
        {
            get
            {
                CheckPropertyInited("IdCodjob");
                return _idcodjob;
            }
            set
            {
                _idcodjob = value;
                NotifyPropertyChanged("IdCodjob");
            }
        }


        private string _value;
        /// <summary>
        /// 21400300 Значение (VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 21400300)]
        public string Value
        {
            get
            {
                CheckPropertyInited("Value");
                return _value;
            }
            set
            {
                _value = value;
                NotifyPropertyChanged("Value");
            }
        }


        private string _code;
        /// <summary>
        /// 21400400 Код (CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 21400400)]
        public string Code
        {
            get
            {
                CheckPropertyInited("Code");
                return _code;
            }
            set
            {
                _code = value;
                NotifyPropertyChanged("Code");
            }
        }


        private string _source;
        /// <summary>
        /// 21400500  Источник (SOURCE)
        /// </summary>
        [RegisterAttribute(AttributeID = 21400500)]
        public string Source
        {
            get
            {
                CheckPropertyInited("Source");
                return _source;
            }
            set
            {
                _source = value;
                NotifyPropertyChanged("Source");
            }
        }


        private string _expert;
        /// <summary>
        /// 21400600 ФИО эксперта (EXPERT)
        /// </summary>
        [RegisterAttribute(AttributeID = 21400600)]
        public string Expert
        {
            get
            {
                CheckPropertyInited("Expert");
                return _expert;
            }
            set
            {
                _expert = value;
                NotifyPropertyChanged("Expert");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 215 Задания ЦОД (KO_COD_JOB)
    /// </summary>
    [RegisterInfo(RegisterID = 215)]
    [Serializable]
    public partial class OMCodJob : OMBaseClass<OMCodJob>
    {

        private long _id;
        /// <summary>
        /// 21500100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 21500100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _namejob;
        /// <summary>
        /// 21500200 Задание ЦОД (NAME_JOB)
        /// </summary>
        [RegisterAttribute(AttributeID = 21500200)]
        public string NameJob
        {
            get
            {
                CheckPropertyInited("NameJob");
                return _namejob;
            }
            set
            {
                _namejob = value;
                NotifyPropertyChanged("NameJob");
            }
        }


        private string _resultjob;
        /// <summary>
        /// 21500300 Результат (RESULT_JOB)
        /// </summary>
        [RegisterAttribute(AttributeID = 21500300)]
        public string ResultJob
        {
            get
            {
                CheckPropertyInited("ResultJob");
                return _resultjob;
            }
            set
            {
                _resultjob = value;
                NotifyPropertyChanged("ResultJob");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 216 Данные о кадастровой стоимости из Росреестра (KO_COST_ROSREESTR)
    /// </summary>
    [RegisterInfo(RegisterID = 216)]
    [Serializable]
    public partial class OMCostRosreestr : OMBaseClass<OMCostRosreestr>
    {

        private long _id;
        /// <summary>
        /// 21600100 ИД (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 21600100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _idobject;
        /// <summary>
        /// 21600200 ИД единицы оценки (ID_OBJECT)
        /// </summary>
        [RegisterAttribute(AttributeID = 21600200)]
        public long IdObject
        {
            get
            {
                CheckPropertyInited("IdObject");
                return _idobject;
            }
            set
            {
                _idobject = value;
                NotifyPropertyChanged("IdObject");
            }
        }


        private DateTime? _datevaluation;
        /// <summary>
        /// 21600300 Дата определения кадастровой стоимости (DATEVALUATION)
        /// </summary>
        [RegisterAttribute(AttributeID = 21600300)]
        public DateTime? Datevaluation
        {
            get
            {
                CheckPropertyInited("Datevaluation");
                return _datevaluation;
            }
            set
            {
                _datevaluation = value;
                NotifyPropertyChanged("Datevaluation");
            }
        }


        private DateTime? _dateentering;
        /// <summary>
        /// 21600400 Дата внесения сведений о кадастровой стоимости в ЕГРН (DATEENTERING)
        /// </summary>
        [RegisterAttribute(AttributeID = 21600400)]
        public DateTime? Dateentering
        {
            get
            {
                CheckPropertyInited("Dateentering");
                return _dateentering;
            }
            set
            {
                _dateentering = value;
                NotifyPropertyChanged("Dateentering");
            }
        }


        private string _docnumber;
        /// <summary>
        /// 21600500 Номер акта об утверждении кадастровой стоимости (DOCNUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 21600500)]
        public string Docnumber
        {
            get
            {
                CheckPropertyInited("Docnumber");
                return _docnumber;
            }
            set
            {
                _docnumber = value;
                NotifyPropertyChanged("Docnumber");
            }
        }


        private DateTime? _docdate;
        /// <summary>
        /// 21600600 Дата акта об утверждении кадастровой стоимости (DOCDATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 21600600)]
        public DateTime? Docdate
        {
            get
            {
                CheckPropertyInited("Docdate");
                return _docdate;
            }
            set
            {
                _docdate = value;
                NotifyPropertyChanged("Docdate");
            }
        }


        private DateTime? _applicationdate;
        /// <summary>
        /// 21600700 Дата начала применения кадастровой стоимости (APPLICATIONDATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 21600700)]
        public DateTime? Applicationdate
        {
            get
            {
                CheckPropertyInited("Applicationdate");
                return _applicationdate;
            }
            set
            {
                _applicationdate = value;
                NotifyPropertyChanged("Applicationdate");
            }
        }


        private string _docname;
        /// <summary>
        /// 21600800 Наименование документа об утверждении кадастровой стоимости (DOCNAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 21600800)]
        public string Docname
        {
            get
            {
                CheckPropertyInited("Docname");
                return _docname;
            }
            set
            {
                _docname = value;
                NotifyPropertyChanged("Docname");
            }
        }


        private decimal? _costvalue;
        /// <summary>
        /// 21600900 Сведения о кадастровой стоимости (COSTVALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 21600900)]
        public decimal? Costvalue
        {
            get
            {
                CheckPropertyInited("Costvalue");
                return _costvalue;
            }
            set
            {
                _costvalue = value;
                NotifyPropertyChanged("Costvalue");
            }
        }


        private DateTime? _dateapproval;
        /// <summary>
        /// 21601000 Дата утверждения кадастровой стоимости (DATEAPPROVAL)
        /// </summary>
        [RegisterAttribute(AttributeID = 21601000)]
        public DateTime? Dateapproval
        {
            get
            {
                CheckPropertyInited("Dateapproval");
                return _dateapproval;
            }
            set
            {
                _dateapproval = value;
                NotifyPropertyChanged("Dateapproval");
            }
        }


        private DateTime? _revisalstatementdate;
        /// <summary>
        /// 21601100 Дата подачи заявления о пересмотре кадастровой стоимости (REVISALSTATEMENTDATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 21601100)]
        public DateTime? Revisalstatementdate
        {
            get
            {
                CheckPropertyInited("Revisalstatementdate");
                return _revisalstatementdate;
            }
            set
            {
                _revisalstatementdate = value;
                NotifyPropertyChanged("Revisalstatementdate");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 217 Экспликации площадей (KO_EXPLICATION)
    /// </summary>
    [RegisterInfo(RegisterID = 217)]
    [Serializable]
    public partial class OMExplication : OMBaseClass<OMExplication>
    {

        private long _id;
        /// <summary>
        /// 21700100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 21700100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _objectid;
        /// <summary>
        /// 21700200 Идентификатор единицы кадастровой оценки (OBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21700200)]
        public long ObjectId
        {
            get
            {
                CheckPropertyInited("ObjectId");
                return _objectid;
            }
            set
            {
                _objectid = value;
                NotifyPropertyChanged("ObjectId");
            }
        }


        private long _groupid;
        /// <summary>
        /// 21700300 Идентификатор группы (GROUP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21700300)]
        public long GroupId
        {
            get
            {
                CheckPropertyInited("GroupId");
                return _groupid;
            }
            set
            {
                _groupid = value;
                NotifyPropertyChanged("GroupId");
            }
        }


        private decimal? _square;
        /// <summary>
        /// 21700400 Площадь экспликации (SQUARE)
        /// </summary>
        [RegisterAttribute(AttributeID = 21700400)]
        public decimal? Square
        {
            get
            {
                CheckPropertyInited("Square");
                return _square;
            }
            set
            {
                _square = value;
                NotifyPropertyChanged("Square");
            }
        }


        private decimal? _upks;
        /// <summary>
        /// 21700500 Удельный показатель для экспликации (UPKS)
        /// </summary>
        [RegisterAttribute(AttributeID = 21700500)]
        public decimal? Upks
        {
            get
            {
                CheckPropertyInited("Upks");
                return _upks;
            }
            set
            {
                _upks = value;
                NotifyPropertyChanged("Upks");
            }
        }


        private decimal? _kc;
        /// <summary>
        /// 21700600 Кадастровая стоимость экспликации (KC)
        /// </summary>
        [RegisterAttribute(AttributeID = 21700600)]
        public decimal? Kc
        {
            get
            {
                CheckPropertyInited("Kc");
                return _kc;
            }
            set
            {
                _kc = value;
                NotifyPropertyChanged("Kc");
            }
        }


        private decimal? _upksanalog;
        /// <summary>
        /// 21700700 Удельный показатель аналога (UPKS_ANALOG)
        /// </summary>
        [RegisterAttribute(AttributeID = 21700700)]
        public decimal? UpksAnalog
        {
            get
            {
                CheckPropertyInited("UpksAnalog");
                return _upksanalog;
            }
            set
            {
                _upksanalog = value;
                NotifyPropertyChanged("UpksAnalog");
            }
        }


        private string _nameanalog;
        /// <summary>
        /// 21700800 Наименование аналога (NAME_ANALOG)
        /// </summary>
        [RegisterAttribute(AttributeID = 21700800)]
        public string NameAnalog
        {
            get
            {
                CheckPropertyInited("NameAnalog");
                return _nameanalog;
            }
            set
            {
                _nameanalog = value;
                NotifyPropertyChanged("NameAnalog");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 218 Эталонные объекты (KO_ETALON)
    /// </summary>
    [RegisterInfo(RegisterID = 218)]
    [Serializable]
    public partial class OMEtalon : OMBaseClass<OMEtalon>
    {

        private long _id;
        /// <summary>
        /// 21800100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 21800100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _groupid;
        /// <summary>
        /// 21800200 Идентификатор группы (GROUP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21800200)]
        public long GroupId
        {
            get
            {
                CheckPropertyInited("GroupId");
                return _groupid;
            }
            set
            {
                _groupid = value;
                NotifyPropertyChanged("GroupId");
            }
        }


        private string _cadastraldistrict;
        /// <summary>
        /// 21800300 Кадастровый район (CADASTRALDISTRICT)
        /// </summary>
        [RegisterAttribute(AttributeID = 21800300)]
        public string Cadastraldistrict
        {
            get
            {
                CheckPropertyInited("Cadastraldistrict");
                return _cadastraldistrict;
            }
            set
            {
                _cadastraldistrict = value;
                NotifyPropertyChanged("Cadastraldistrict");
            }
        }


        private string _cadastralnumber;
        /// <summary>
        /// 21800400 Кадастровый номер эталонного объекта (CADASTRALNUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 21800400)]
        public string Cadastralnumber
        {
            get
            {
                CheckPropertyInited("Cadastralnumber");
                return _cadastralnumber;
            }
            set
            {
                _cadastralnumber = value;
                NotifyPropertyChanged("Cadastralnumber");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 219 Реестр хранения факторов тура (KO_TOUR_FACTOR_REGISTER)
    /// </summary>
    [RegisterInfo(RegisterID = 219)]
    [Serializable]
    public partial class OMTourFactorRegister : OMBaseClass<OMTourFactorRegister>
    {

        private long _id;
        /// <summary>
        /// 21900100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 21900100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _tourid;
        /// <summary>
        /// 21900200 Идентификатор тура (TOUR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21900200)]
        public long? TourId
        {
            get
            {
                CheckPropertyInited("TourId");
                return _tourid;
            }
            set
            {
                _tourid = value;
                NotifyPropertyChanged("TourId");
            }
        }


        private string _objecttype;
        /// <summary>
        /// 21900300 Тип объекта (OBJECT_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 21900300)]
        public string ObjectType
        {
            get
            {
                CheckPropertyInited("ObjectType");
                return _objecttype;
            }
            set
            {
                _objecttype = value;
                NotifyPropertyChanged("ObjectType");
            }
        }


        private PropertyTypes _objecttype_Code;
        /// <summary>
        /// 21900300 Тип объекта (справочный код) (OBJECT_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 21900300)]
        public PropertyTypes ObjectType_Code
        {
            get
            {
                CheckPropertyInited("ObjectType_Code");
                return this._objecttype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_objecttype))
                    {
                         _objecttype = descr;
                    }
                }
                else
                {
                     _objecttype = descr;
                }

                this._objecttype_Code = value;
                NotifyPropertyChanged("ObjectType");
                NotifyPropertyChanged("ObjectType_Code");
            }
        }


        private long? _registerid;
        /// <summary>
        /// 21900400 Идентификатор регистра (REGISTER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21900400)]
        public long? RegisterId
        {
            get
            {
                CheckPropertyInited("RegisterId");
                return _registerid;
            }
            set
            {
                _registerid = value;
                NotifyPropertyChanged("RegisterId");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 220 Связи документов (KO_DOCUMENT_LINK)
    /// </summary>
    [RegisterInfo(RegisterID = 220)]
    [Serializable]
    public partial class OMDocumentLink : OMBaseClass<OMDocumentLink>
    {

        private long _id;
        /// <summary>
        /// 22000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 22000100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _maindocid;
        /// <summary>
        /// 22000200 Идентификатор основного документа (MAIN_DOC_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 22000200)]
        public long MainDocId
        {
            get
            {
                CheckPropertyInited("MainDocId");
                return _maindocid;
            }
            set
            {
                _maindocid = value;
                NotifyPropertyChanged("MainDocId");
            }
        }


        private long _linkdocid;
        /// <summary>
        /// 22000300 Идентификатор связанного документа (LINK_DOC_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 22000300)]
        public long LinkDocId
        {
            get
            {
                CheckPropertyInited("LinkDocId");
                return _linkdocid;
            }
            set
            {
                _linkdocid = value;
                NotifyPropertyChanged("LinkDocId");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 221 Журнал отправки итогов расчета КО (KO_RESULT_SEND_JOURNAL)
    /// </summary>
    [RegisterInfo(RegisterID = 221)]
    [Serializable]
    public partial class OMKoResultSendJournal : OMBaseClass<OMKoResultSendJournal>
    {

        private long _id;
        /// <summary>
        /// 22100100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 22100100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _guid;
        /// <summary>
        /// 22100200 Глобальный идентификатор сообщения (GUID)
        /// </summary>
        [RegisterAttribute(AttributeID = 22100200)]
        public string Guid
        {
            get
            {
                CheckPropertyInited("Guid");
                return _guid;
            }
            set
            {
                _guid = value;
                NotifyPropertyChanged("Guid");
            }
        }


        private long _taskid;
        /// <summary>
        /// 22100300 ИД Задания на оценку (TASK_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 22100300)]
        public long TaskId
        {
            get
            {
                CheckPropertyInited("TaskId");
                return _taskid;
            }
            set
            {
                _taskid = value;
                NotifyPropertyChanged("TaskId");
            }
        }


        private DateTime _createdate;
        /// <summary>
        /// 22100400 Дата создания записи в журнале (CREATE_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 22100400)]
        public DateTime CreateDate
        {
            get
            {
                CheckPropertyInited("CreateDate");
                return _createdate;
            }
            set
            {
                _createdate = value;
                NotifyPropertyChanged("CreateDate");
            }
        }


        private DateTime? _senddate;
        /// <summary>
        /// 22100500 Дата прочтения сообщения ИС РЕОН (SEND_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 22100500)]
        public DateTime? SendDate
        {
            get
            {
                CheckPropertyInited("SendDate");
                return _senddate;
            }
            set
            {
                _senddate = value;
                NotifyPropertyChanged("SendDate");
            }
        }


        private DateTime? _confirmdate;
        /// <summary>
        /// 22100600 Дата подтверждения сообщения ИС РЕОН (CONFIRM_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 22100600)]
        public DateTime? ConfirmDate
        {
            get
            {
                CheckPropertyInited("ConfirmDate");
                return _confirmdate;
            }
            set
            {
                _confirmdate = value;
                NotifyPropertyChanged("ConfirmDate");
            }
        }


        private long _resultexportid;
        /// <summary>
        /// 22100700 Идентификатор результата выгрузки (RESULT_EXPORT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 22100700)]
        public long ResultExportId
        {
            get
            {
                CheckPropertyInited("ResultExportId");
                return _resultexportid;
            }
            set
            {
                _resultexportid = value;
                NotifyPropertyChanged("ResultExportId");
            }
        }

    }
}

namespace ObjectModel.Ko
{
    /// <summary>
    /// 222 Таблица для хранения отношений между группами и сегментами рынка (KO_GROUP_TO_MARKET_SEGMENT_RELATION)
    /// </summary>
    [RegisterInfo(RegisterID = 222)]
    [Serializable]
    public partial class OMGroupToMarketSegmentRelation : OMBaseClass<OMGroupToMarketSegmentRelation>
    {

        private long _id;
        /// <summary>
        /// 22200100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 22200100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _groupid;
        /// <summary>
        /// 22200101 ИД группы (GROUP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 22200101)]
        public long GroupId
        {
            get
            {
                CheckPropertyInited("GroupId");
                return _groupid;
            }
            set
            {
                _groupid = value;
                NotifyPropertyChanged("GroupId");
            }
        }


        private string _marketsegment;
        /// <summary>
        /// 22200102 Сегмент рынка недвижимости ()
        /// </summary>
        [RegisterAttribute(AttributeID = 22200102)]
        public string MarketSegment
        {
            get
            {
                CheckPropertyInited("MarketSegment");
                return _marketsegment;
            }
            set
            {
                _marketsegment = value;
                NotifyPropertyChanged("MarketSegment");
            }
        }


        private MarketSegment _marketsegment_Code;
        /// <summary>
        /// 22200102 Сегмент рынка недвижимости (справочный код) (MARKET_SEGMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 22200102)]
        public MarketSegment MarketSegment_Code
        {
            get
            {
                CheckPropertyInited("MarketSegment_Code");
                return this._marketsegment_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_marketsegment))
                    {
                         _marketsegment = descr;
                    }
                }
                else
                {
                     _marketsegment = descr;
                }

                this._marketsegment_Code = value;
                NotifyPropertyChanged("MarketSegment");
                NotifyPropertyChanged("MarketSegment_Code");
            }
        }


        private string _territorytype;
        /// <summary>
        /// 22200103 Тип территории ()
        /// </summary>
        [RegisterAttribute(AttributeID = 22200103)]
        public string TerritoryType
        {
            get
            {
                CheckPropertyInited("TerritoryType");
                return _territorytype;
            }
            set
            {
                _territorytype = value;
                NotifyPropertyChanged("TerritoryType");
            }
        }


        private TerritoryType _territorytype_Code;
        /// <summary>
        /// 22200103 Тип территории (справочный код) (TERRITORY_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 22200103)]
        public TerritoryType TerritoryType_Code
        {
            get
            {
                CheckPropertyInited("TerritoryType_Code");
                return this._territorytype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_territorytype))
                    {
                         _territorytype = descr;
                    }
                }
                else
                {
                     _territorytype = descr;
                }

                this._territorytype_Code = value;
                NotifyPropertyChanged("TerritoryType");
                NotifyPropertyChanged("TerritoryType_Code");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 250 Параметры расчета для ОКС 2018 года (KO_UNIT_PARAMS_OKS_2018)
    /// </summary>
    [RegisterInfo(RegisterID = 250)]
    [Serializable]
    public partial class OMUnitParamsOks2018 : OMBaseClass<OMUnitParamsOks2018>
    {
    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 251 Параметры расчета для ЗУ 2018 года (KO_UNIT_PARAMS_ZU_2018)
    /// </summary>
    [RegisterInfo(RegisterID = 251)]
    [Serializable]
    public partial class OMUnitParamsZu2018 : OMBaseClass<OMUnitParamsZu2018>
    {
    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 252 Параметры расчета для ОКС 2016 года (KO_UNIT_PARAMS_OKS_2016)
    /// </summary>
    [RegisterInfo(RegisterID = 252)]
    [Serializable]
    public partial class OMUnitParamsOks2016 : OMBaseClass<OMUnitParamsOks2016>
    {

        private long _id;
        /// <summary>
        /// 25200100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 25200100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _field98;
        /// <summary>
        /// 25209800 Здания для налога_2016 (FIELD_98)
        /// </summary>
        [RegisterAttribute(AttributeID = 25209800)]
        public string Field98
        {
            get
            {
                CheckPropertyInited("Field98");
                return _field98;
            }
            set
            {
                _field98 = value;
                NotifyPropertyChanged("Field98");
            }
        }


        private string _field99;
        /// <summary>
        /// 25209900 Группа здания_жил (FIELD_99)
        /// </summary>
        [RegisterAttribute(AttributeID = 25209900)]
        public string Field99
        {
            get
            {
                CheckPropertyInited("Field99");
                return _field99;
            }
            set
            {
                _field99 = value;
                NotifyPropertyChanged("Field99");
            }
        }


        private string _field100;
        /// <summary>
        /// 25210000 Здание для налога_2017 (FIELD_100)
        /// </summary>
        [RegisterAttribute(AttributeID = 25210000)]
        public string Field100
        {
            get
            {
                CheckPropertyInited("Field100");
                return _field100;
            }
            set
            {
                _field100 = value;
                NotifyPropertyChanged("Field100");
            }
        }


        private string _field101;
        /// <summary>
        /// 25210100 Административный округ  (FIELD_101)
        /// </summary>
        [RegisterAttribute(AttributeID = 25210100)]
        public string Field101
        {
            get
            {
                CheckPropertyInited("Field101");
                return _field101;
            }
            set
            {
                _field101 = value;
                NotifyPropertyChanged("Field101");
            }
        }


        private string _field102;
        /// <summary>
        /// 25210200 Расстояние до центра административного округа (FIELD_102)
        /// </summary>
        [RegisterAttribute(AttributeID = 25210200)]
        public string Field102
        {
            get
            {
                CheckPropertyInited("Field102");
                return _field102;
            }
            set
            {
                _field102 = value;
                NotifyPropertyChanged("Field102");
            }
        }


        private string _field103;
        /// <summary>
        /// 25210300 Район г.Москвы (FIELD_103)
        /// </summary>
        [RegisterAttribute(AttributeID = 25210300)]
        public string Field103
        {
            get
            {
                CheckPropertyInited("Field103");
                return _field103;
            }
            set
            {
                _field103 = value;
                NotifyPropertyChanged("Field103");
            }
        }


        private string _field104;
        /// <summary>
        /// 25210400 Расстояние до ближайшей ж/д станции, ж/д платформы, ж/д вокзала (FIELD_104)
        /// </summary>
        [RegisterAttribute(AttributeID = 25210400)]
        public string Field104
        {
            get
            {
                CheckPropertyInited("Field104");
                return _field104;
            }
            set
            {
                _field104 = value;
                NotifyPropertyChanged("Field104");
            }
        }


        private string _field105;
        /// <summary>
        /// 25210500 Расстояние до исторического центра г.Москвы (FIELD_105)
        /// </summary>
        [RegisterAttribute(AttributeID = 25210500)]
        public string Field105
        {
            get
            {
                CheckPropertyInited("Field105");
                return _field105;
            }
            set
            {
                _field105 = value;
                NotifyPropertyChanged("Field105");
            }
        }


        private string _field106;
        /// <summary>
        /// 25210600 Местоположение относительно кольцевых транпорстных магистралей города (FIELD_106)
        /// </summary>
        [RegisterAttribute(AttributeID = 25210600)]
        public string Field106
        {
            get
            {
                CheckPropertyInited("Field106");
                return _field106;
            }
            set
            {
                _field106 = value;
                NotifyPropertyChanged("Field106");
            }
        }


        private string _field107;
        /// <summary>
        /// 25210700 Расстояние до зон рекреации (FIELD_107)
        /// </summary>
        [RegisterAttribute(AttributeID = 25210700)]
        public string Field107
        {
            get
            {
                CheckPropertyInited("Field107");
                return _field107;
            }
            set
            {
                _field107 = value;
                NotifyPropertyChanged("Field107");
            }
        }


        private string _field108;
        /// <summary>
        /// 25210800 Расстояние до МКАД (FIELD_108)
        /// </summary>
        [RegisterAttribute(AttributeID = 25210800)]
        public string Field108
        {
            get
            {
                CheckPropertyInited("Field108");
                return _field108;
            }
            set
            {
                _field108 = value;
                NotifyPropertyChanged("Field108");
            }
        }


        private string _field109;
        /// <summary>
        /// 25210900 Расстояние до ближайшей остановки общественного транспорта (FIELD_109)
        /// </summary>
        [RegisterAttribute(AttributeID = 25210900)]
        public string Field109
        {
            get
            {
                CheckPropertyInited("Field109");
                return _field109;
            }
            set
            {
                _field109 = value;
                NotifyPropertyChanged("Field109");
            }
        }


        private string _field110;
        /// <summary>
        /// 25211000 Расстояние до промышленных зон (FIELD_110)
        /// </summary>
        [RegisterAttribute(AttributeID = 25211000)]
        public string Field110
        {
            get
            {
                CheckPropertyInited("Field110");
                return _field110;
            }
            set
            {
                _field110 = value;
                NotifyPropertyChanged("Field110");
            }
        }


        private string _field111;
        /// <summary>
        /// 25211100 Промзоны (FIELD_111)
        /// </summary>
        [RegisterAttribute(AttributeID = 25211100)]
        public string Field111
        {
            get
            {
                CheckPropertyInited("Field111");
                return _field111;
            }
            set
            {
                _field111 = value;
                NotifyPropertyChanged("Field111");
            }
        }


        private string _field112;
        /// <summary>
        /// 25211200 Ближайшая станция метро (FIELD_112)
        /// </summary>
        [RegisterAttribute(AttributeID = 25211200)]
        public string Field112
        {
            get
            {
                CheckPropertyInited("Field112");
                return _field112;
            }
            set
            {
                _field112 = value;
                NotifyPropertyChanged("Field112");
            }
        }


        private string _field113;
        /// <summary>
        /// 25211300 Расстояние до ближайшей станции метро (FIELD_113)
        /// </summary>
        [RegisterAttribute(AttributeID = 25211300)]
        public string Field113
        {
            get
            {
                CheckPropertyInited("Field113");
                return _field113;
            }
            set
            {
                _field113 = value;
                NotifyPropertyChanged("Field113");
            }
        }


        private string _field114;
        /// <summary>
        /// 25211400 Расстояние до ближайщей магистрали города (FIELD_114)
        /// </summary>
        [RegisterAttribute(AttributeID = 25211400)]
        public string Field114
        {
            get
            {
                CheckPropertyInited("Field114");
                return _field114;
            }
            set
            {
                _field114 = value;
                NotifyPropertyChanged("Field114");
            }
        }


        private string _field115;
        /// <summary>
        /// 25211500 Инфраструктура (FIELD_115)
        /// </summary>
        [RegisterAttribute(AttributeID = 25211500)]
        public string Field115
        {
            get
            {
                CheckPropertyInited("Field115");
                return _field115;
            }
            set
            {
                _field115 = value;
                NotifyPropertyChanged("Field115");
            }
        }


        private string _field116;
        /// <summary>
        /// 25211600 Крупные торговые объекты_150 (FIELD_116)
        /// </summary>
        [RegisterAttribute(AttributeID = 25211600)]
        public string Field116
        {
            get
            {
                CheckPropertyInited("Field116");
                return _field116;
            }
            set
            {
                _field116 = value;
                NotifyPropertyChanged("Field116");
            }
        }


        private string _field117;
        /// <summary>
        /// 25211700 Крупные торговые объекты_1500 (FIELD_117)
        /// </summary>
        [RegisterAttribute(AttributeID = 25211700)]
        public string Field117
        {
            get
            {
                CheckPropertyInited("Field117");
                return _field117;
            }
            set
            {
                _field117 = value;
                NotifyPropertyChanged("Field117");
            }
        }


        private string _field118;
        /// <summary>
        /// 25211800 Бизнес центры  (FIELD_118)
        /// </summary>
        [RegisterAttribute(AttributeID = 25211800)]
        public string Field118
        {
            get
            {
                CheckPropertyInited("Field118");
                return _field118;
            }
            set
            {
                _field118 = value;
                NotifyPropertyChanged("Field118");
            }
        }


        private string _field119;
        /// <summary>
        /// 25211900 Группа_ГКН (Амосов) (FIELD_119)
        /// </summary>
        [RegisterAttribute(AttributeID = 25211900)]
        public string Field119
        {
            get
            {
                CheckPropertyInited("Field119");
                return _field119;
            }
            set
            {
                _field119 = value;
                NotifyPropertyChanged("Field119");
            }
        }


        private string _field120;
        /// <summary>
        /// 25212000 Группа_ГКН (Пшеничкин) (FIELD_120)
        /// </summary>
        [RegisterAttribute(AttributeID = 25212000)]
        public string Field120
        {
            get
            {
                CheckPropertyInited("Field120");
                return _field120;
            }
            set
            {
                _field120 = value;
                NotifyPropertyChanged("Field120");
            }
        }


        private string _field121;
        /// <summary>
        /// 25212100 Группа_БТИ (Никонова) (FIELD_121)
        /// </summary>
        [RegisterAttribute(AttributeID = 25212100)]
        public string Field121
        {
            get
            {
                CheckPropertyInited("Field121");
                return _field121;
            }
            set
            {
                _field121 = value;
                NotifyPropertyChanged("Field121");
            }
        }


        private string _field122;
        /// <summary>
        /// 25212200 Группа_БТИ (Шпагина) (FIELD_122)
        /// </summary>
        [RegisterAttribute(AttributeID = 25212200)]
        public string Field122
        {
            get
            {
                CheckPropertyInited("Field122");
                return _field122;
            }
            set
            {
                _field122 = value;
                NotifyPropertyChanged("Field122");
            }
        }


        private string _field123;
        /// <summary>
        /// 25212300 Способ расчета Графики (FIELD_123)
        /// </summary>
        [RegisterAttribute(AttributeID = 25212300)]
        public string Field123
        {
            get
            {
                CheckPropertyInited("Field123");
                return _field123;
            }
            set
            {
                _field123 = value;
                NotifyPropertyChanged("Field123");
            }
        }


        private string _field124;
        /// <summary>
        /// 25212400 Уточненный КК (FIELD_124)
        /// </summary>
        [RegisterAttribute(AttributeID = 25212400)]
        public string Field124
        {
            get
            {
                CheckPropertyInited("Field124");
                return _field124;
            }
            set
            {
                _field124 = value;
                NotifyPropertyChanged("Field124");
            }
        }


        private string _field125;
        /// <summary>
        /// 25212500 Группа_ГИН (FIELD_125)
        /// </summary>
        [RegisterAttribute(AttributeID = 25212500)]
        public string Field125
        {
            get
            {
                CheckPropertyInited("Field125");
                return _field125;
            }
            set
            {
                _field125 = value;
                NotifyPropertyChanged("Field125");
            }
        }


        private string _field126;
        /// <summary>
        /// 25212600 УПКС_2014 (FIELD_126)
        /// </summary>
        [RegisterAttribute(AttributeID = 25212600)]
        public string Field126
        {
            get
            {
                CheckPropertyInited("Field126");
                return _field126;
            }
            set
            {
                _field126 = value;
                NotifyPropertyChanged("Field126");
            }
        }


        private string _field127;
        /// <summary>
        /// 25212700 КС_2014 (FIELD_127)
        /// </summary>
        [RegisterAttribute(AttributeID = 25212700)]
        public string Field127
        {
            get
            {
                CheckPropertyInited("Field127");
                return _field127;
            }
            set
            {
                _field127 = value;
                NotifyPropertyChanged("Field127");
            }
        }


        private string _field128;
        /// <summary>
        /// 25212800 Группа_2014 (FIELD_128)
        /// </summary>
        [RegisterAttribute(AttributeID = 25212800)]
        public string Field128
        {
            get
            {
                CheckPropertyInited("Field128");
                return _field128;
            }
            set
            {
                _field128 = value;
                NotifyPropertyChanged("Field128");
            }
        }


        private string _field129;
        /// <summary>
        /// 25212900 Подгруппа_2014 (FIELD_129)
        /// </summary>
        [RegisterAttribute(AttributeID = 25212900)]
        public string Field129
        {
            get
            {
                CheckPropertyInited("Field129");
                return _field129;
            }
            set
            {
                _field129 = value;
                NotifyPropertyChanged("Field129");
            }
        }


        private string _field130;
        /// <summary>
        /// 25213000 Группа здания_нежил (FIELD_130)
        /// </summary>
        [RegisterAttribute(AttributeID = 25213000)]
        public string Field130
        {
            get
            {
                CheckPropertyInited("Field130");
                return _field130;
            }
            set
            {
                _field130 = value;
                NotifyPropertyChanged("Field130");
            }
        }


        private string _field131;
        /// <summary>
        /// 25213100 Назначение_БТИ (FIELD_131)
        /// </summary>
        [RegisterAttribute(AttributeID = 25213100)]
        public string Field131
        {
            get
            {
                CheckPropertyInited("Field131");
                return _field131;
            }
            set
            {
                _field131 = value;
                NotifyPropertyChanged("Field131");
            }
        }


        private string _field132;
        /// <summary>
        /// 25213200 Элитное жилье (FIELD_132)
        /// </summary>
        [RegisterAttribute(AttributeID = 25213200)]
        public string Field132
        {
            get
            {
                CheckPropertyInited("Field132");
                return _field132;
            }
            set
            {
                _field132 = value;
                NotifyPropertyChanged("Field132");
            }
        }


        private string _field133;
        /// <summary>
        /// 25213300 Территория (осн/доп) (FIELD_133)
        /// </summary>
        [RegisterAttribute(AttributeID = 25213300)]
        public string Field133
        {
            get
            {
                CheckPropertyInited("Field133");
                return _field133;
            }
            set
            {
                _field133 = value;
                NotifyPropertyChanged("Field133");
            }
        }


        private string _field134;
        /// <summary>
        /// 25213400 Подгруппа_жилье (FIELD_134)
        /// </summary>
        [RegisterAttribute(AttributeID = 25213400)]
        public string Field134
        {
            get
            {
                CheckPropertyInited("Field134");
                return _field134;
            }
            set
            {
                _field134 = value;
                NotifyPropertyChanged("Field134");
            }
        }


        private string _field135;
        /// <summary>
        /// 25213500 БТИ_2012 (FIELD_135)
        /// </summary>
        [RegisterAttribute(AttributeID = 25213500)]
        public string Field135
        {
            get
            {
                CheckPropertyInited("Field135");
                return _field135;
            }
            set
            {
                _field135 = value;
                NotifyPropertyChanged("Field135");
            }
        }


        private string _field136;
        /// <summary>
        /// 25213600 Недвижимость_2 2012 (FIELD_136)
        /// </summary>
        [RegisterAttribute(AttributeID = 25213600)]
        public string Field136
        {
            get
            {
                CheckPropertyInited("Field136");
                return _field136;
            }
            set
            {
                _field136 = value;
                NotifyPropertyChanged("Field136");
            }
        }


        private string _field137;
        /// <summary>
        /// 25213700 Назначение_БТИ 2016 (FIELD_137)
        /// </summary>
        [RegisterAttribute(AttributeID = 25213700)]
        public string Field137
        {
            get
            {
                CheckPropertyInited("Field137");
                return _field137;
            }
            set
            {
                _field137 = value;
                NotifyPropertyChanged("Field137");
            }
        }


        private string _field138;
        /// <summary>
        /// 25213800 Год постройки БТИ 2016 (FIELD_138)
        /// </summary>
        [RegisterAttribute(AttributeID = 25213800)]
        public string Field138
        {
            get
            {
                CheckPropertyInited("Field138");
                return _field138;
            }
            set
            {
                _field138 = value;
                NotifyPropertyChanged("Field138");
            }
        }


        private string _field139;
        /// <summary>
        /// 25213900 Год ввода БТИ 2016 (FIELD_139)
        /// </summary>
        [RegisterAttribute(AttributeID = 25213900)]
        public string Field139
        {
            get
            {
                CheckPropertyInited("Field139");
                return _field139;
            }
            set
            {
                _field139 = value;
                NotifyPropertyChanged("Field139");
            }
        }


        private string _field140;
        /// <summary>
        /// 25214000 Материал стен БТИ 2016 (FIELD_140)
        /// </summary>
        [RegisterAttribute(AttributeID = 25214000)]
        public string Field140
        {
            get
            {
                CheckPropertyInited("Field140");
                return _field140;
            }
            set
            {
                _field140 = value;
                NotifyPropertyChanged("Field140");
            }
        }


        private string _field141;
        /// <summary>
        /// 25214100 Этажность БТИ 2016 (FIELD_141)
        /// </summary>
        [RegisterAttribute(AttributeID = 25214100)]
        public string Field141
        {
            get
            {
                CheckPropertyInited("Field141");
                return _field141;
            }
            set
            {
                _field141 = value;
                NotifyPropertyChanged("Field141");
            }
        }


        private string _field142;
        /// <summary>
        /// 25214200 Год постройки (ввода) итого (FIELD_142)
        /// </summary>
        [RegisterAttribute(AttributeID = 25214200)]
        public string Field142
        {
            get
            {
                CheckPropertyInited("Field142");
                return _field142;
            }
            set
            {
                _field142 = value;
                NotifyPropertyChanged("Field142");
            }
        }


        private string _field143;
        /// <summary>
        /// 25214300 Материал стен (FIELD_143)
        /// </summary>
        [RegisterAttribute(AttributeID = 25214300)]
        public string Field143
        {
            get
            {
                CheckPropertyInited("Field143");
                return _field143;
            }
            set
            {
                _field143 = value;
                NotifyPropertyChanged("Field143");
            }
        }


        private string _field144;
        /// <summary>
        /// 25214400 Вид гаража (FIELD_144)
        /// </summary>
        [RegisterAttribute(AttributeID = 25214400)]
        public string Field144
        {
            get
            {
                CheckPropertyInited("Field144");
                return _field144;
            }
            set
            {
                _field144 = value;
                NotifyPropertyChanged("Field144");
            }
        }


        private string _field145;
        /// <summary>
        /// 25214500 Торговый коридор (FIELD_145)
        /// </summary>
        [RegisterAttribute(AttributeID = 25214500)]
        public string Field145
        {
            get
            {
                CheckPropertyInited("Field145");
                return _field145;
            }
            set
            {
                _field145 = value;
                NotifyPropertyChanged("Field145");
            }
        }


        private string _field146;
        /// <summary>
        /// 25214600 Площадь (FIELD_146)
        /// </summary>
        [RegisterAttribute(AttributeID = 25214600)]
        public string Field146
        {
            get
            {
                CheckPropertyInited("Field146");
                return _field146;
            }
            set
            {
                _field146 = value;
                NotifyPropertyChanged("Field146");
            }
        }


        private string _field147;
        /// <summary>
        /// 25214700 Класс элитного жилья (FIELD_147)
        /// </summary>
        [RegisterAttribute(AttributeID = 25214700)]
        public string Field147
        {
            get
            {
                CheckPropertyInited("Field147");
                return _field147;
            }
            set
            {
                _field147 = value;
                NotifyPropertyChanged("Field147");
            }
        }


        private string _field148;
        /// <summary>
        /// 25214800 Количество квартир в здании (FIELD_148)
        /// </summary>
        [RegisterAttribute(AttributeID = 25214800)]
        public string Field148
        {
            get
            {
                CheckPropertyInited("Field148");
                return _field148;
            }
            set
            {
                _field148 = value;
                NotifyPropertyChanged("Field148");
            }
        }


        private string _field149;
        /// <summary>
        /// 25214900 Причина переноса в группу с НХ (FIELD_149)
        /// </summary>
        [RegisterAttribute(AttributeID = 25214900)]
        public string Field149
        {
            get
            {
                CheckPropertyInited("Field149");
                return _field149;
            }
            set
            {
                _field149 = value;
                NotifyPropertyChanged("Field149");
            }
        }


        private string _field150;
        /// <summary>
        /// 25215000 Код аналога (FIELD_150)
        /// </summary>
        [RegisterAttribute(AttributeID = 25215000)]
        public string Field150
        {
            get
            {
                CheckPropertyInited("Field150");
                return _field150;
            }
            set
            {
                _field150 = value;
                NotifyPropertyChanged("Field150");
            }
        }


        private string _field151;
        /// <summary>
        /// 25215100 Коэфициент перехода (FIELD_151)
        /// </summary>
        [RegisterAttribute(AttributeID = 25215100)]
        public string Field151
        {
            get
            {
                CheckPropertyInited("Field151");
                return _field151;
            }
            set
            {
                _field151 = value;
                NotifyPropertyChanged("Field151");
            }
        }


        private string _field152;
        /// <summary>
        /// 25215200 Региональный коэффициент (FIELD_152)
        /// </summary>
        [RegisterAttribute(AttributeID = 25215200)]
        public string Field152
        {
            get
            {
                CheckPropertyInited("Field152");
                return _field152;
            }
            set
            {
                _field152 = value;
                NotifyPropertyChanged("Field152");
            }
        }


        private string _field153;
        /// <summary>
        /// 25215300 Процент готовности (FIELD_153)
        /// </summary>
        [RegisterAttribute(AttributeID = 25215300)]
        public string Field153
        {
            get
            {
                CheckPropertyInited("Field153");
                return _field153;
            }
            set
            {
                _field153 = value;
                NotifyPropertyChanged("Field153");
            }
        }


        private string _field154;
        /// <summary>
        /// 25215400 Прибль предпринимателя (FIELD_154)
        /// </summary>
        [RegisterAttribute(AttributeID = 25215400)]
        public string Field154
        {
            get
            {
                CheckPropertyInited("Field154");
                return _field154;
            }
            set
            {
                _field154 = value;
                NotifyPropertyChanged("Field154");
            }
        }


        private string _field155;
        /// <summary>
        /// 25215500 НДС  (FIELD_155)
        /// </summary>
        [RegisterAttribute(AttributeID = 25215500)]
        public string Field155
        {
            get
            {
                CheckPropertyInited("Field155");
                return _field155;
            }
            set
            {
                _field155 = value;
                NotifyPropertyChanged("Field155");
            }
        }


        private string _field156;
        /// <summary>
        /// 25215600 Износ  (FIELD_156)
        /// </summary>
        [RegisterAttribute(AttributeID = 25215600)]
        public string Field156
        {
            get
            {
                CheckPropertyInited("Field156");
                return _field156;
            }
            set
            {
                _field156 = value;
                NotifyPropertyChanged("Field156");
            }
        }


        private string _field157;
        /// <summary>
        /// 25215700 Коэфициент различия (S или V)  (FIELD_157)
        /// </summary>
        [RegisterAttribute(AttributeID = 25215700)]
        public string Field157
        {
            get
            {
                CheckPropertyInited("Field157");
                return _field157;
            }
            set
            {
                _field157 = value;
                NotifyPropertyChanged("Field157");
            }
        }


        private string _field158;
        /// <summary>
        /// 25215800 Высота потолка  (FIELD_158)
        /// </summary>
        [RegisterAttribute(AttributeID = 25215800)]
        public string Field158
        {
            get
            {
                CheckPropertyInited("Field158");
                return _field158;
            }
            set
            {
                _field158 = value;
                NotifyPropertyChanged("Field158");
            }
        }


        private string _field159;
        /// <summary>
        /// 25215900 Коэф. толщины стен  (FIELD_159)
        /// </summary>
        [RegisterAttribute(AttributeID = 25215900)]
        public string Field159
        {
            get
            {
                CheckPropertyInited("Field159");
                return _field159;
            }
            set
            {
                _field159 = value;
                NotifyPropertyChanged("Field159");
            }
        }


        private string _field160;
        /// <summary>
        /// 25216000 Группа_2014 для сравнения (FIELD_160)
        /// </summary>
        [RegisterAttribute(AttributeID = 25216000)]
        public string Field160
        {
            get
            {
                CheckPropertyInited("Field160");
                return _field160;
            }
            set
            {
                _field160 = value;
                NotifyPropertyChanged("Field160");
            }
        }


        private string _field161;
        /// <summary>
        /// 25216100 Группа ДЭПР (FIELD_161)
        /// </summary>
        [RegisterAttribute(AttributeID = 25216100)]
        public string Field161
        {
            get
            {
                CheckPropertyInited("Field161");
                return _field161;
            }
            set
            {
                _field161 = value;
                NotifyPropertyChanged("Field161");
            }
        }


        private string _field162;
        /// <summary>
        /// 25216200 Группа_30.06.16 (FIELD_162)
        /// </summary>
        [RegisterAttribute(AttributeID = 25216200)]
        public string Field162
        {
            get
            {
                CheckPropertyInited("Field162");
                return _field162;
            }
            set
            {
                _field162 = value;
                NotifyPropertyChanged("Field162");
            }
        }


        private string _field163;
        /// <summary>
        /// 25216300 Аварийность (FIELD_163)
        /// </summary>
        [RegisterAttribute(AttributeID = 25216300)]
        public string Field163
        {
            get
            {
                CheckPropertyInited("Field163");
                return _field163;
            }
            set
            {
                _field163 = value;
                NotifyPropertyChanged("Field163");
            }
        }


        private string _field164;
        /// <summary>
        /// 25216400 УПКС жил. дома  (FIELD_164)
        /// </summary>
        [RegisterAttribute(AttributeID = 25216400)]
        public string Field164
        {
            get
            {
                CheckPropertyInited("Field164");
                return _field164;
            }
            set
            {
                _field164 = value;
                NotifyPropertyChanged("Field164");
            }
        }


        private string _field165;
        /// <summary>
        /// 25216500 Мультипликатор  (FIELD_165)
        /// </summary>
        [RegisterAttribute(AttributeID = 25216500)]
        public string Field165
        {
            get
            {
                CheckPropertyInited("Field165");
                return _field165;
            }
            set
            {
                _field165 = value;
                NotifyPropertyChanged("Field165");
            }
        }


        private string _field166;
        /// <summary>
        /// 25216600 Поправка на материал  (FIELD_166)
        /// </summary>
        [RegisterAttribute(AttributeID = 25216600)]
        public string Field166
        {
            get
            {
                CheckPropertyInited("Field166");
                return _field166;
            }
            set
            {
                _field166 = value;
                NotifyPropertyChanged("Field166");
            }
        }


        private string _field167;
        /// <summary>
        /// 25216700 Высота потолка  (FIELD_167)
        /// </summary>
        [RegisterAttribute(AttributeID = 25216700)]
        public string Field167
        {
            get
            {
                CheckPropertyInited("Field167");
                return _field167;
            }
            set
            {
                _field167 = value;
                NotifyPropertyChanged("Field167");
            }
        }


        private string _field169;
        /// <summary>
        /// 25216900 Группировка 06.07.16 (FIELD_169)
        /// </summary>
        [RegisterAttribute(AttributeID = 25216900)]
        public string Field169
        {
            get
            {
                CheckPropertyInited("Field169");
                return _field169;
            }
            set
            {
                _field169 = value;
                NotifyPropertyChanged("Field169");
            }
        }


        private string _field170;
        /// <summary>
        /// 25217000 Номер группы помещения ДЭПР (FIELD_170)
        /// </summary>
        [RegisterAttribute(AttributeID = 25217000)]
        public string Field170
        {
            get
            {
                CheckPropertyInited("Field170");
                return _field170;
            }
            set
            {
                _field170 = value;
                NotifyPropertyChanged("Field170");
            }
        }


        private string _field171;
        /// <summary>
        /// 25217100 Присвоение гр нежил помещ (FIELD_171)
        /// </summary>
        [RegisterAttribute(AttributeID = 25217100)]
        public string Field171
        {
            get
            {
                CheckPropertyInited("Field171");
                return _field171;
            }
            set
            {
                _field171 = value;
                NotifyPropertyChanged("Field171");
            }
        }


        private string _field172;
        /// <summary>
        /// 25217200 Код поселка  (FIELD_172)
        /// </summary>
        [RegisterAttribute(AttributeID = 25217200)]
        public string Field172
        {
            get
            {
                CheckPropertyInited("Field172");
                return _field172;
            }
            set
            {
                _field172 = value;
                NotifyPropertyChanged("Field172");
            }
        }


        private string _field173;
        /// <summary>
        /// 25217300 Корректировка на физический износ  (FIELD_173)
        /// </summary>
        [RegisterAttribute(AttributeID = 25217300)]
        public string Field173
        {
            get
            {
                CheckPropertyInited("Field173");
                return _field173;
            }
            set
            {
                _field173 = value;
                NotifyPropertyChanged("Field173");
            }
        }


        private string _field174;
        /// <summary>
        /// 25217400 Корректировка на КС  (FIELD_174)
        /// </summary>
        [RegisterAttribute(AttributeID = 25217400)]
        public string Field174
        {
            get
            {
                CheckPropertyInited("Field174");
                return _field174;
            }
            set
            {
                _field174 = value;
                NotifyPropertyChanged("Field174");
            }
        }


        private string _field175;
        /// <summary>
        /// 25217500 Здания для налога 22.07.2016 (FIELD_175)
        /// </summary>
        [RegisterAttribute(AttributeID = 25217500)]
        public string Field175
        {
            get
            {
                CheckPropertyInited("Field175");
                return _field175;
            }
            set
            {
                _field175 = value;
                NotifyPropertyChanged("Field175");
            }
        }


        private string _field176;
        /// <summary>
        /// 25217600 Группа 2013 (FIELD_176)
        /// </summary>
        [RegisterAttribute(AttributeID = 25217600)]
        public string Field176
        {
            get
            {
                CheckPropertyInited("Field176");
                return _field176;
            }
            set
            {
                _field176 = value;
                NotifyPropertyChanged("Field176");
            }
        }


        private string _field177;
        /// <summary>
        /// 25217700 УПКС 2013 (FIELD_177)
        /// </summary>
        [RegisterAttribute(AttributeID = 25217700)]
        public string Field177
        {
            get
            {
                CheckPropertyInited("Field177");
                return _field177;
            }
            set
            {
                _field177 = value;
                NotifyPropertyChanged("Field177");
            }
        }


        private string _field178;
        /// <summary>
        /// 25217800 КС 2013 (FIELD_178)
        /// </summary>
        [RegisterAttribute(AttributeID = 25217800)]
        public string Field178
        {
            get
            {
                CheckPropertyInited("Field178");
                return _field178;
            }
            set
            {
                _field178 = value;
                NotifyPropertyChanged("Field178");
            }
        }


        private string _field179;
        /// <summary>
        /// 25217900 Акт обследования (FIELD_179)
        /// </summary>
        [RegisterAttribute(AttributeID = 25217900)]
        public string Field179
        {
            get
            {
                CheckPropertyInited("Field179");
                return _field179;
            }
            set
            {
                _field179 = value;
                NotifyPropertyChanged("Field179");
            }
        }


        private string _field180;
        /// <summary>
        /// 25218000 Дата акта обследования (FIELD_180)
        /// </summary>
        [RegisterAttribute(AttributeID = 25218000)]
        public string Field180
        {
            get
            {
                CheckPropertyInited("Field180");
                return _field180;
            }
            set
            {
                _field180 = value;
                NotifyPropertyChanged("Field180");
            }
        }


        private string _field181;
        /// <summary>
        /// 25218100 УПКС в фонде (FIELD_181)
        /// </summary>
        [RegisterAttribute(AttributeID = 25218100)]
        public string Field181
        {
            get
            {
                CheckPropertyInited("Field181");
                return _field181;
            }
            set
            {
                _field181 = value;
                NotifyPropertyChanged("Field181");
            }
        }


        private string _field182;
        /// <summary>
        /// 25218200 Группа в фонде (FIELD_182)
        /// </summary>
        [RegisterAttribute(AttributeID = 25218200)]
        public string Field182
        {
            get
            {
                CheckPropertyInited("Field182");
                return _field182;
            }
            set
            {
                _field182 = value;
                NotifyPropertyChanged("Field182");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 253 Параметры расчета для ЗУ 2016 года (KO_UNIT_PARAMS_ZU_2016)
    /// </summary>
    [RegisterInfo(RegisterID = 253)]
    [Serializable]
    public partial class OMUnitParamsZu2016 : OMBaseClass<OMUnitParamsZu2016>
    {

        private long _id;
        /// <summary>
        /// 25300100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 25300100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _field176;
        /// <summary>
        /// 25317600 Расстояние до престижных магистралей (FIELD_176)
        /// </summary>
        [RegisterAttribute(AttributeID = 25317600)]
        public string Field176
        {
            get
            {
                CheckPropertyInited("Field176");
                return _field176;
            }
            set
            {
                _field176 = value;
                NotifyPropertyChanged("Field176");
            }
        }


        private string _field177;
        /// <summary>
        /// 25317700 Расстояние до рекреационных территорий (FIELD_177)
        /// </summary>
        [RegisterAttribute(AttributeID = 25317700)]
        public string Field177
        {
            get
            {
                CheckPropertyInited("Field177");
                return _field177;
            }
            set
            {
                _field177 = value;
                NotifyPropertyChanged("Field177");
            }
        }


        private string _field178;
        /// <summary>
        /// 25317800 Москва, Зеленоград или присоединенные территории (FIELD_178)
        /// </summary>
        [RegisterAttribute(AttributeID = 25317800)]
        public string Field178
        {
            get
            {
                CheckPropertyInited("Field178");
                return _field178;
            }
            set
            {
                _field178 = value;
                NotifyPropertyChanged("Field178");
            }
        }


        private string _field179;
        /// <summary>
        /// 25317900 Расстояние до центра Зеленограда (Крюково) (FIELD_179)
        /// </summary>
        [RegisterAttribute(AttributeID = 25317900)]
        public string Field179
        {
            get
            {
                CheckPropertyInited("Field179");
                return _field179;
            }
            set
            {
                _field179 = value;
                NotifyPropertyChanged("Field179");
            }
        }


        private string _field180;
        /// <summary>
        /// 25318000 Расстояние до центра Зеленограда (Префектура) (FIELD_180)
        /// </summary>
        [RegisterAttribute(AttributeID = 25318000)]
        public string Field180
        {
            get
            {
                CheckPropertyInited("Field180");
                return _field180;
            }
            set
            {
                _field180 = value;
                NotifyPropertyChanged("Field180");
            }
        }


        private string _field181;
        /// <summary>
        /// 25318100 Расстояние до остановок общест. транспорта Зеленограда (FIELD_181)
        /// </summary>
        [RegisterAttribute(AttributeID = 25318100)]
        public string Field181
        {
            get
            {
                CheckPropertyInited("Field181");
                return _field181;
            }
            set
            {
                _field181 = value;
                NotifyPropertyChanged("Field181");
            }
        }


        private string _field182;
        /// <summary>
        /// 25318200 Расстояние объекта до центра города (Кремль, СМ) (FIELD_182)
        /// </summary>
        [RegisterAttribute(AttributeID = 25318200)]
        public string Field182
        {
            get
            {
                CheckPropertyInited("Field182");
                return _field182;
            }
            set
            {
                _field182 = value;
                NotifyPropertyChanged("Field182");
            }
        }


        private string _field183;
        /// <summary>
        /// 25318300 Расстояние до станций метрополитена (до 1,5 км) (FIELD_183)
        /// </summary>
        [RegisterAttribute(AttributeID = 25318300)]
        public string Field183
        {
            get
            {
                CheckPropertyInited("Field183");
                return _field183;
            }
            set
            {
                _field183 = value;
                NotifyPropertyChanged("Field183");
            }
        }


        private string _field184;
        /// <summary>
        /// 25318400 Историческая застройка (связность, СМ) (FIELD_184)
        /// </summary>
        [RegisterAttribute(AttributeID = 25318400)]
        public string Field184
        {
            get
            {
                CheckPropertyInited("Field184");
                return _field184;
            }
            set
            {
                _field184 = value;
                NotifyPropertyChanged("Field184");
            }
        }


        private string _field185;
        /// <summary>
        /// 25318500 Элитные жилые комплексы (связность, СМ)  (FIELD_185)
        /// </summary>
        [RegisterAttribute(AttributeID = 25318500)]
        public string Field185
        {
            get
            {
                CheckPropertyInited("Field185");
                return _field185;
            }
            set
            {
                _field185 = value;
                NotifyPropertyChanged("Field185");
            }
        }


        private string _field186;
        /// <summary>
        /// 25318600 Производственно-научные зоны (связность) (FIELD_186)
        /// </summary>
        [RegisterAttribute(AttributeID = 25318600)]
        public string Field186
        {
            get
            {
                CheckPropertyInited("Field186");
                return _field186;
            }
            set
            {
                _field186 = value;
                NotifyPropertyChanged("Field186");
            }
        }


        private string _field187;
        /// <summary>
        /// 25318700 Размещение в границах санитарно-защитных зон производственных предприятий (СМ) (FIELD_187)
        /// </summary>
        [RegisterAttribute(AttributeID = 25318700)]
        public string Field187
        {
            get
            {
                CheckPropertyInited("Field187");
                return _field187;
            }
            set
            {
                _field187 = value;
                NotifyPropertyChanged("Field187");
            }
        }


        private string _field188;
        /// <summary>
        /// 25318800 Связность с центром города (Кремль) (FIELD_188)
        /// </summary>
        [RegisterAttribute(AttributeID = 25318800)]
        public string Field188
        {
            get
            {
                CheckPropertyInited("Field188");
                return _field188;
            }
            set
            {
                _field188 = value;
                NotifyPropertyChanged("Field188");
            }
        }


        private string _field189;
        /// <summary>
        /// 25318900 Расстояние до ближайшей транспортной магистрали ведущих городских направлений, федерального и регионального значений (FIELD_189)
        /// </summary>
        [RegisterAttribute(AttributeID = 25318900)]
        public string Field189
        {
            get
            {
                CheckPropertyInited("Field189");
                return _field189;
            }
            set
            {
                _field189 = value;
                NotifyPropertyChanged("Field189");
            }
        }


        private string _field190;
        /// <summary>
        /// 25319000 Расстояние до МКАД (НМ) (FIELD_190)
        /// </summary>
        [RegisterAttribute(AttributeID = 25319000)]
        public string Field190
        {
            get
            {
                CheckPropertyInited("Field190");
                return _field190;
            }
            set
            {
                _field190 = value;
                NotifyPropertyChanged("Field190");
            }
        }


        private string _field191;
        /// <summary>
        /// 25319100 Школы, детские сады (связность, НМ)  (FIELD_191)
        /// </summary>
        [RegisterAttribute(AttributeID = 25319100)]
        public string Field191
        {
            get
            {
                CheckPropertyInited("Field191");
                return _field191;
            }
            set
            {
                _field191 = value;
                NotifyPropertyChanged("Field191");
            }
        }


        private string _field192;
        /// <summary>
        /// 25319200 Расстояние до г. Троицк, Московский, Щербинка (FIELD_192)
        /// </summary>
        [RegisterAttribute(AttributeID = 25319200)]
        public string Field192
        {
            get
            {
                CheckPropertyInited("Field192");
                return _field192;
            }
            set
            {
                _field192 = value;
                NotifyPropertyChanged("Field192");
            }
        }


        private string _field193;
        /// <summary>
        /// 25319300 Расстояние до ближайшей школы и детского сада (НМ) (FIELD_193)
        /// </summary>
        [RegisterAttribute(AttributeID = 25319300)]
        public string Field193
        {
            get
            {
                CheckPropertyInited("Field193");
                return _field193;
            }
            set
            {
                _field193 = value;
                NotifyPropertyChanged("Field193");
            }
        }


        private string _field194;
        /// <summary>
        /// 25319400 Элитные коттеджные поселки (связность) (FIELD_194)
        /// </summary>
        [RegisterAttribute(AttributeID = 25319400)]
        public string Field194
        {
            get
            {
                CheckPropertyInited("Field194");
                return _field194;
            }
            set
            {
                _field194 = value;
                NotifyPropertyChanged("Field194");
            }
        }


        private string _field195;
        /// <summary>
        /// 25319500 Расстояние до ближайших 3 центров Зеленограда (Крюково, пл. Юности, Префектура) (FIELD_195)
        /// </summary>
        [RegisterAttribute(AttributeID = 25319500)]
        public string Field195
        {
            get
            {
                CheckPropertyInited("Field195");
                return _field195;
            }
            set
            {
                _field195 = value;
                NotifyPropertyChanged("Field195");
            }
        }


        private string _field196;
        /// <summary>
        /// 25319600 Расстояние до престижных магистралей (до 2 км) (FIELD_196)
        /// </summary>
        [RegisterAttribute(AttributeID = 25319600)]
        public string Field196
        {
            get
            {
                CheckPropertyInited("Field196");
                return _field196;
            }
            set
            {
                _field196 = value;
                NotifyPropertyChanged("Field196");
            }
        }


        private string _field197;
        /// <summary>
        /// 25319700 Расстояние до МКАД (СМ) (FIELD_197)
        /// </summary>
        [RegisterAttribute(AttributeID = 25319700)]
        public string Field197
        {
            get
            {
                CheckPropertyInited("Field197");
                return _field197;
            }
            set
            {
                _field197 = value;
                NotifyPropertyChanged("Field197");
            }
        }


        private string _field198;
        /// <summary>
        /// 25319800 Бизнес-центры (связность, СМ) (FIELD_198)
        /// </summary>
        [RegisterAttribute(AttributeID = 25319800)]
        public string Field198
        {
            get
            {
                CheckPropertyInited("Field198");
                return _field198;
            }
            set
            {
                _field198 = value;
                NotifyPropertyChanged("Field198");
            }
        }


        private string _field199;
        /// <summary>
        /// 25319900 Остановки общественного транспорта (связность, НМ) (FIELD_199)
        /// </summary>
        [RegisterAttribute(AttributeID = 25319900)]
        public string Field199
        {
            get
            {
                CheckPropertyInited("Field199");
                return _field199;
            }
            set
            {
                _field199 = value;
                NotifyPropertyChanged("Field199");
            }
        }


        private string _field200;
        /// <summary>
        /// 25320000 Расстояние до центра городов и городских округов (НМ) (FIELD_200)
        /// </summary>
        [RegisterAttribute(AttributeID = 25320000)]
        public string Field200
        {
            get
            {
                CheckPropertyInited("Field200");
                return _field200;
            }
            set
            {
                _field200 = value;
                NotifyPropertyChanged("Field200");
            }
        }


        private string _field201;
        /// <summary>
        /// 25320100 Расстояние до водоема (FIELD_201)
        /// </summary>
        [RegisterAttribute(AttributeID = 25320100)]
        public string Field201
        {
            get
            {
                CheckPropertyInited("Field201");
                return _field201;
            }
            set
            {
                _field201 = value;
                NotifyPropertyChanged("Field201");
            }
        }


        private string _field202;
        /// <summary>
        /// 25320200 Расположение на территории элитных коттеджных поселков (FIELD_202)
        /// </summary>
        [RegisterAttribute(AttributeID = 25320200)]
        public string Field202
        {
            get
            {
                CheckPropertyInited("Field202");
                return _field202;
            }
            set
            {
                _field202 = value;
                NotifyPropertyChanged("Field202");
            }
        }


        private string _field203;
        /// <summary>
        /// 25320300 Остановки общественного транспорта Зеленограда (связность) (FIELD_203)
        /// </summary>
        [RegisterAttribute(AttributeID = 25320300)]
        public string Field203
        {
            get
            {
                CheckPropertyInited("Field203");
                return _field203;
            }
            set
            {
                _field203 = value;
                NotifyPropertyChanged("Field203");
            }
        }


        private string _field204;
        /// <summary>
        /// 25320400 Объекты деловой активности Зеленограда (Связность ) (FIELD_204)
        /// </summary>
        [RegisterAttribute(AttributeID = 25320400)]
        public string Field204
        {
            get
            {
                CheckPropertyInited("Field204");
                return _field204;
            }
            set
            {
                _field204 = value;
                NotifyPropertyChanged("Field204");
            }
        }


        private string _field205;
        /// <summary>
        /// 25320500 Объекты деловой активности (связность, СМ) (FIELD_205)
        /// </summary>
        [RegisterAttribute(AttributeID = 25320500)]
        public string Field205
        {
            get
            {
                CheckPropertyInited("Field205");
                return _field205;
            }
            set
            {
                _field205 = value;
                NotifyPropertyChanged("Field205");
            }
        }


        private string _field206;
        /// <summary>
        /// 25320600 Производственная застройка (связность, СМ) (FIELD_206)
        /// </summary>
        [RegisterAttribute(AttributeID = 25320600)]
        public string Field206
        {
            get
            {
                CheckPropertyInited("Field206");
                return _field206;
            }
            set
            {
                _field206 = value;
                NotifyPropertyChanged("Field206");
            }
        }


        private string _field207;
        /// <summary>
        /// 25320700 Крупные торговые объекты (связность) (FIELD_207)
        /// </summary>
        [RegisterAttribute(AttributeID = 25320700)]
        public string Field207
        {
            get
            {
                CheckPropertyInited("Field207");
                return _field207;
            }
            set
            {
                _field207 = value;
                NotifyPropertyChanged("Field207");
            }
        }


        private string _field208;
        /// <summary>
        /// 25320800 Центр городов и городских округов (связность, НМ) (FIELD_208)
        /// </summary>
        [RegisterAttribute(AttributeID = 25320800)]
        public string Field208
        {
            get
            {
                CheckPropertyInited("Field208");
                return _field208;
            }
            set
            {
                _field208 = value;
                NotifyPropertyChanged("Field208");
            }
        }


        private string _field209;
        /// <summary>
        /// 25320900 Лесные массивы, парки, скверы, бульвары (связность) (FIELD_209)
        /// </summary>
        [RegisterAttribute(AttributeID = 25320900)]
        public string Field209
        {
            get
            {
                CheckPropertyInited("Field209");
                return _field209;
            }
            set
            {
                _field209 = value;
                NotifyPropertyChanged("Field209");
            }
        }


        private string _field210;
        /// <summary>
        /// 25321000 Расстояние до станций метрополитена (FIELD_210)
        /// </summary>
        [RegisterAttribute(AttributeID = 25321000)]
        public string Field210
        {
            get
            {
                CheckPropertyInited("Field210");
                return _field210;
            }
            set
            {
                _field210 = value;
                NotifyPropertyChanged("Field210");
            }
        }


        private string _field211;
        /// <summary>
        /// 25321100 ТЭЦ (Зоны влияния, СМ) (FIELD_211)
        /// </summary>
        [RegisterAttribute(AttributeID = 25321100)]
        public string Field211
        {
            get
            {
                CheckPropertyInited("Field211");
                return _field211;
            }
            set
            {
                _field211 = value;
                NotifyPropertyChanged("Field211");
            }
        }


        private string _field212;
        /// <summary>
        /// 25321200 Направления по сторонам света (НМ) (FIELD_212)
        /// </summary>
        [RegisterAttribute(AttributeID = 25321200)]
        public string Field212
        {
            get
            {
                CheckPropertyInited("Field212");
                return _field212;
            }
            set
            {
                _field212 = value;
                NotifyPropertyChanged("Field212");
            }
        }


        private string _field213;
        /// <summary>
        /// 25321300 Москва или присоединенные территории (FIELD_213)
        /// </summary>
        [RegisterAttribute(AttributeID = 25321300)]
        public string Field213
        {
            get
            {
                CheckPropertyInited("Field213");
                return _field213;
            }
            set
            {
                _field213 = value;
                NotifyPropertyChanged("Field213");
            }
        }


        private string _field214;
        /// <summary>
        /// 25321400 Расстояние до железнодорожной грузовой станции (FIELD_214)
        /// </summary>
        [RegisterAttribute(AttributeID = 25321400)]
        public string Field214
        {
            get
            {
                CheckPropertyInited("Field214");
                return _field214;
            }
            set
            {
                _field214 = value;
                NotifyPropertyChanged("Field214");
            }
        }


        private string _field215;
        /// <summary>
        /// 25321500 Расстояние до речных портов (СМ) (FIELD_215)
        /// </summary>
        [RegisterAttribute(AttributeID = 25321500)]
        public string Field215
        {
            get
            {
                CheckPropertyInited("Field215");
                return _field215;
            }
            set
            {
                _field215 = value;
                NotifyPropertyChanged("Field215");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 254 Таблица соответствия кода и группы (KO_COMPLIANCE_GUIDE)
    /// </summary>
    [RegisterInfo(RegisterID = 254)]
    [Serializable]
    public partial class OMComplianceGuide : OMBaseClass<OMComplianceGuide>
    {

        private long _id;
        /// <summary>
        /// 25400100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 25400100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _code;
        /// <summary>
        /// 25400200 Значение кода (CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25400200)]
        public string Code
        {
            get
            {
                CheckPropertyInited("Code");
                return _code;
            }
            set
            {
                _code = value;
                NotifyPropertyChanged("Code");
            }
        }


        private string _subgroup;
        /// <summary>
        /// 25400300 Подгруппа соответствия  (SUBGROUP)
        /// </summary>
        [RegisterAttribute(AttributeID = 25400300)]
        public string SubGroup
        {
            get
            {
                CheckPropertyInited("SubGroup");
                return _subgroup;
            }
            set
            {
                _subgroup = value;
                NotifyPropertyChanged("SubGroup");
            }
        }


        private string _typeproperty;
        /// <summary>
        /// 25400400 Тип объекта  (TYPE_PROPERTY)
        /// </summary>
        [RegisterAttribute(AttributeID = 25400400)]
        public string TypeProperty
        {
            get
            {
                CheckPropertyInited("TypeProperty");
                return _typeproperty;
            }
            set
            {
                _typeproperty = value;
                NotifyPropertyChanged("TypeProperty");
            }
        }


        private PropertyTypes _typeproperty_Code;
        /// <summary>
        /// 25400400 Тип объекта  (справочный код) (TYPE_PROPERTY_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25400400)]
        public PropertyTypes TypeProperty_Code
        {
            get
            {
                CheckPropertyInited("TypeProperty_Code");
                return this._typeproperty_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typeproperty))
                    {
                         _typeproperty = descr;
                    }
                }
                else
                {
                     _typeproperty = descr;
                }

                this._typeproperty_Code = value;
                NotifyPropertyChanged("TypeProperty");
                NotifyPropertyChanged("TypeProperty_Code");
            }
        }


        private string _typeroom;
        /// <summary>
        /// 25400600 Тип помещения (TYPE_ROOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 25400600)]
        public string TypeRoom
        {
            get
            {
                CheckPropertyInited("TypeRoom");
                return _typeroom;
            }
            set
            {
                _typeroom = value;
                NotifyPropertyChanged("TypeRoom");
            }
        }


        private KoTypeOfRoom _typeroom_Code;
        /// <summary>
        /// 25400600 Тип помещения (справочный код) (TYPE_ROOM_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25400600)]
        public KoTypeOfRoom TypeRoom_Code
        {
            get
            {
                CheckPropertyInited("TypeRoom_Code");
                return this._typeroom_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typeroom))
                    {
                         _typeroom = descr;
                    }
                }
                else
                {
                     _typeroom = descr;
                }

                this._typeroom_Code = value;
                NotifyPropertyChanged("TypeRoom");
                NotifyPropertyChanged("TypeRoom_Code");
            }
        }


        private long _tourid;
        /// <summary>
        /// 25400700 Идентификатор тура (TOUR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25400700)]
        public long TourId
        {
            get
            {
                CheckPropertyInited("TourId");
                return _tourid;
            }
            set
            {
                _tourid = value;
                NotifyPropertyChanged("TourId");
            }
        }


        private string _territorytype;
        /// <summary>
        /// 25400800 Тип территории (TERRITORY_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25400800)]
        public string TerritoryType
        {
            get
            {
                CheckPropertyInited("TerritoryType");
                return _territorytype;
            }
            set
            {
                _territorytype = value;
                NotifyPropertyChanged("TerritoryType");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 255 Реестр для зависимостей расчета (KO_CALC_GROUP)
    /// </summary>
    [RegisterInfo(RegisterID = 255)]
    [Serializable]
    public partial class OMCalcGroup : OMBaseClass<OMCalcGroup>
    {

        private long _id;
        /// <summary>
        /// 25500100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 25500100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _groupid;
        /// <summary>
        /// 25500200 Идентификатор группы (GROUP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25500200)]
        public long? GroupId
        {
            get
            {
                CheckPropertyInited("GroupId");
                return _groupid;
            }
            set
            {
                _groupid = value;
                NotifyPropertyChanged("GroupId");
            }
        }


        private long? _parentcalcgroupid;
        /// <summary>
        /// 25500300 Идентификатор группы, на основе которой считается текущая группа (PARENT_CALC_GROUP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25500300)]
        public long? ParentCalcGroupId
        {
            get
            {
                CheckPropertyInited("ParentCalcGroupId");
                return _parentcalcgroupid;
            }
            set
            {
                _parentcalcgroupid = value;
                NotifyPropertyChanged("ParentCalcGroupId");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 256 Реестр для изменения сведений об объектах оценки (KO_UNIT_CHANGE)
    /// </summary>
    [RegisterInfo(RegisterID = 256)]
    [Serializable]
    public partial class OMUnitChange : OMBaseClass<OMUnitChange>
    {

        private long _id;
        /// <summary>
        /// 25600100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 25600100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _unitid;
        /// <summary>
        /// 25600200 Идентификатор единицы оценки (ID_UNIT)
        /// </summary>
        [RegisterAttribute(AttributeID = 25600200)]
        public long UnitId
        {
            get
            {
                CheckPropertyInited("UnitId");
                return _unitid;
            }
            set
            {
                _unitid = value;
                NotifyPropertyChanged("UnitId");
            }
        }


        private string _oldvalue;
        /// <summary>
        /// 25600300 Cтарое значение (OLD_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25600300)]
        public string OldValue
        {
            get
            {
                CheckPropertyInited("OldValue");
                return _oldvalue;
            }
            set
            {
                _oldvalue = value;
                NotifyPropertyChanged("OldValue");
            }
        }


        private string _newvalue;
        /// <summary>
        /// 25600400 Новое значение (NEW_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25600400)]
        public string NewValue
        {
            get
            {
                CheckPropertyInited("NewValue");
                return _newvalue;
            }
            set
            {
                _newvalue = value;
                NotifyPropertyChanged("NewValue");
            }
        }


        private string _changestatus;
        /// <summary>
        /// 25600500 Статус изменения (STATUS_CHANGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25600500)]
        public string ChangeStatus
        {
            get
            {
                CheckPropertyInited("ChangeStatus");
                return _changestatus;
            }
            set
            {
                _changestatus = value;
                NotifyPropertyChanged("ChangeStatus");
            }
        }


        private KoChangeStatus _changestatus_Code;
        /// <summary>
        /// 25600500 Статус изменения (справочный код) (STATUS_CHANGE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25600500)]
        public KoChangeStatus ChangeStatus_Code
        {
            get
            {
                CheckPropertyInited("ChangeStatus_Code");
                return this._changestatus_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_changestatus))
                    {
                         _changestatus = descr;
                    }
                }
                else
                {
                     _changestatus = descr;
                }

                this._changestatus_Code = value;
                NotifyPropertyChanged("ChangeStatus");
                NotifyPropertyChanged("ChangeStatus_Code");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 257 Соответствие атрибутов KO и GBU (KO_TRANSFER_ATTRIBUTES)
    /// </summary>
    [RegisterInfo(RegisterID = 257)]
    [Serializable]
    public partial class OMTransferAttributes : OMBaseClass<OMTransferAttributes>
    {

        private long _id;
        /// <summary>
        /// 25700100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 25700100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _tourid;
        /// <summary>
        /// 25700200 Идентификатор тура (TOUR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25700200)]
        public long TourId
        {
            get
            {
                CheckPropertyInited("TourId");
                return _tourid;
            }
            set
            {
                _tourid = value;
                NotifyPropertyChanged("TourId");
            }
        }


        private bool _isoks;
        /// <summary>
        /// 25700300 ОКС или ЗУ (IS_OKS)
        /// </summary>
        [RegisterAttribute(AttributeID = 25700300)]
        public bool IsOks
        {
            get
            {
                CheckPropertyInited("IsOks");
                return _isoks;
            }
            set
            {
                _isoks = value;
                NotifyPropertyChanged("IsOks");
            }
        }


        private long _koid;
        /// <summary>
        /// 25700400 Идентификатор в таблице KO (KO_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25700400)]
        public long KoId
        {
            get
            {
                CheckPropertyInited("KoId");
                return _koid;
            }
            set
            {
                _koid = value;
                NotifyPropertyChanged("KoId");
            }
        }


        private long _gbuid;
        /// <summary>
        /// 25700500 Идентификатор в таблице GBU (GBU_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25700500)]
        public long GbuId
        {
            get
            {
                CheckPropertyInited("GbuId");
                return _gbuid;
            }
            set
            {
                _gbuid = value;
                NotifyPropertyChanged("GbuId");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 258 Реестр настройками использования заданных атрибутов для тура (KO_TOUR_ATTRIBUTE_SETTINGS)
    /// </summary>
    [RegisterInfo(RegisterID = 258)]
    [Serializable]
    public partial class OMTourAttributeSettings : OMBaseClass<OMTourAttributeSettings>
    {

        private long _id;
        /// <summary>
        /// 25800100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 25800100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _tourid;
        /// <summary>
        /// 25800200 Идентификатор тура (TOUR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25800200)]
        public long TourId
        {
            get
            {
                CheckPropertyInited("TourId");
                return _tourid;
            }
            set
            {
                _tourid = value;
                NotifyPropertyChanged("TourId");
            }
        }


        private string _attributeusingtype;
        /// <summary>
        /// 25800400 Тип использования атрибута (ATTRIBUTE_USING_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25800400)]
        public string AttributeUsingType
        {
            get
            {
                CheckPropertyInited("AttributeUsingType");
                return _attributeusingtype;
            }
            set
            {
                _attributeusingtype = value;
                NotifyPropertyChanged("AttributeUsingType");
            }
        }


        private KoAttributeUsingType _attributeusingtype_Code;
        /// <summary>
        /// 25800400 Тип использования атрибута (справочный код) (ATTRIBUTE_USING_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25800400)]
        public KoAttributeUsingType AttributeUsingType_Code
        {
            get
            {
                CheckPropertyInited("AttributeUsingType_Code");
                return this._attributeusingtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_attributeusingtype))
                    {
                         _attributeusingtype = descr;
                    }
                }
                else
                {
                     _attributeusingtype = descr;
                }

                this._attributeusingtype_Code = value;
                NotifyPropertyChanged("AttributeUsingType");
                NotifyPropertyChanged("AttributeUsingType_Code");
            }
        }


        private long? _attributeid;
        /// <summary>
        /// 25800500 Идентификатор атрибута (ATTRIBUTE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25800500)]
        public long? AttributeId
        {
            get
            {
                CheckPropertyInited("AttributeId");
                return _attributeid;
            }
            set
            {
                _attributeid = value;
                NotifyPropertyChanged("AttributeId");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 259 Выгрузка отчетов (KO_REPORT_HISTORY)
    /// </summary>
    [RegisterInfo(RegisterID = 259)]
    [Serializable]
    public partial class OMReportHistory : OMBaseClass<OMReportHistory>
    {

        private long _id;
        /// <summary>
        /// 25900100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 25900100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _reporttype;
        /// <summary>
        /// 25900200 Тип отчета (REPORT_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25900200)]
        public string ReportType
        {
            get
            {
                CheckPropertyInited("ReportType");
                return _reporttype;
            }
            set
            {
                _reporttype = value;
                NotifyPropertyChanged("ReportType");
            }
        }


        private KoReportType _reporttype_Code;
        /// <summary>
        /// 25900200 Тип отчета (справочный код) (REPORT_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25900200)]
        public KoReportType ReportType_Code
        {
            get
            {
                CheckPropertyInited("ReportType_Code");
                return this._reporttype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_reporttype))
                    {
                         _reporttype = descr;
                    }
                }
                else
                {
                     _reporttype = descr;
                }

                this._reporttype_Code = value;
                NotifyPropertyChanged("ReportType");
                NotifyPropertyChanged("ReportType_Code");
            }
        }


        private DateTime? _createdate;
        /// <summary>
        /// 25900300 Дата создания (CREATE_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25900300)]
        public DateTime? CreateDate
        {
            get
            {
                CheckPropertyInited("CreateDate");
                return _createdate;
            }
            set
            {
                _createdate = value;
                NotifyPropertyChanged("CreateDate");
            }
        }


        private DateTime? _startdate;
        /// <summary>
        /// 25900400 Дата начала (START_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25900400)]
        public DateTime? StartDate
        {
            get
            {
                CheckPropertyInited("StartDate");
                return _startdate;
            }
            set
            {
                _startdate = value;
                NotifyPropertyChanged("StartDate");
            }
        }


        private DateTime? _enddate;
        /// <summary>
        /// 25900500 Дата окончания (END_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25900500)]
        public DateTime? EndDate
        {
            get
            {
                CheckPropertyInited("EndDate");
                return _enddate;
            }
            set
            {
                _enddate = value;
                NotifyPropertyChanged("EndDate");
            }
        }


        private string _status;
        /// <summary>
        /// 25900600 Статус (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 25900600)]
        public string Status
        {
            get
            {
                CheckPropertyInited("Status");
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }


        private ObjectModel.Directory.Common.ImportStatus _status_Code;
        /// <summary>
        /// 25900600 Статус (справочный код) (STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25900600)]
        public ObjectModel.Directory.Common.ImportStatus Status_Code
        {
            get
            {
                CheckPropertyInited("Status_Code");
                return this._status_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_status))
                    {
                         _status = descr;
                    }
                }
                else
                {
                     _status = descr;
                }

                this._status_Code = value;
                NotifyPropertyChanged("Status");
                NotifyPropertyChanged("Status_Code");
            }
        }


        private long? _userid;
        /// <summary>
        /// 25900700 Пользователь (USER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25900700)]
        public long? UserId
        {
            get
            {
                CheckPropertyInited("UserId");
                return _userid;
            }
            set
            {
                _userid = value;
                NotifyPropertyChanged("UserId");
            }
        }


        private string _parameters;
        /// <summary>
        /// 25900800 Параметры (PARAMETERS)
        /// </summary>
        [RegisterAttribute(AttributeID = 25900800)]
        public string Parameters
        {
            get
            {
                CheckPropertyInited("Parameters");
                return _parameters;
            }
            set
            {
                _parameters = value;
                NotifyPropertyChanged("Parameters");
            }
        }


        private long? _progress;
        /// <summary>
        /// 25900900 Прогресс выполнения (PROGRESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 25900900)]
        public long? Progress
        {
            get
            {
                CheckPropertyInited("Progress");
                return _progress;
            }
            set
            {
                _progress = value;
                NotifyPropertyChanged("Progress");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 260 Реестр настройки автоматического расчета (KO_AUTO_CALCULATION_SETTINGS)
    /// </summary>
    [RegisterInfo(RegisterID = 260)]
    [Serializable]
    public partial class OMAutoCalculationSettings : OMBaseClass<OMAutoCalculationSettings>
    {

        private long _id;
        /// <summary>
        /// 26000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 26000100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _idtour;
        /// <summary>
        /// 26000200 Идентификатор тура оценки (TOUR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 26000200)]
        public long IdTour
        {
            get
            {
                CheckPropertyInited("IdTour");
                return _idtour;
            }
            set
            {
                _idtour = value;
                NotifyPropertyChanged("IdTour");
            }
        }


        private bool _calcparcel;
        /// <summary>
        /// 26000300 Объекты расчета: true-Земельный участок, false-ОКС (CALC_PARCEL)
        /// </summary>
        [RegisterAttribute(AttributeID = 26000300)]
        public bool CalcParcel
        {
            get
            {
                CheckPropertyInited("CalcParcel");
                return _calcparcel;
            }
            set
            {
                _calcparcel = value;
                NotifyPropertyChanged("CalcParcel");
            }
        }


        private long _idgroup;
        /// <summary>
        /// 26000400 Идентификатор группы оценки (GROUP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 26000400)]
        public long IdGroup
        {
            get
            {
                CheckPropertyInited("IdGroup");
                return _idgroup;
            }
            set
            {
                _idgroup = value;
                NotifyPropertyChanged("IdGroup");
            }
        }


        private long _numberpriority;
        /// <summary>
        /// 26000500 Приоритет в очереди расчета (NUMBER_PRIORITY)
        /// </summary>
        [RegisterAttribute(AttributeID = 26000500)]
        public long NumberPriority
        {
            get
            {
                CheckPropertyInited("NumberPriority");
                return _numberpriority;
            }
            set
            {
                _numberpriority = value;
                NotifyPropertyChanged("NumberPriority");
            }
        }


        private bool _calcstage1;
        /// <summary>
        /// 26000600 Предварительный расчет (CALC_STAGE_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 26000600)]
        public bool CalcStage1
        {
            get
            {
                CheckPropertyInited("CalcStage1");
                return _calcstage1;
            }
            set
            {
                _calcstage1 = value;
                NotifyPropertyChanged("CalcStage1");
            }
        }


        private bool _calcstage2;
        /// <summary>
        /// 26000700 Расчет поправок/коэффициентов (CALC_STAGE_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 26000700)]
        public bool CalcStage2
        {
            get
            {
                CheckPropertyInited("CalcStage2");
                return _calcstage2;
            }
            set
            {
                _calcstage2 = value;
                NotifyPropertyChanged("CalcStage2");
            }
        }


        private bool _calcstage3;
        /// <summary>
        /// 26000800 Окончательный расчет (CALC_STAGE_3)
        /// </summary>
        [RegisterAttribute(AttributeID = 26000800)]
        public bool CalcStage3
        {
            get
            {
                CheckPropertyInited("CalcStage3");
                return _calcstage3;
            }
            set
            {
                _calcstage3 = value;
                NotifyPropertyChanged("CalcStage3");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 261 Реестр с настройками атрибутов для Актуализации кадастровых данных (KO_UPDATE_CADASTRAL_DATA_ATTR_SETTINGS)
    /// </summary>
    [RegisterInfo(RegisterID = 261)]
    [Serializable]
    public partial class OMUpdateCadastralDataAttributeSettings : OMBaseClass<OMUpdateCadastralDataAttributeSettings>
    {

        private long _id;
        /// <summary>
        /// 26100100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 26100100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _attributeusingtype;
        /// <summary>
        /// 26100200 Тип использования атрибута (ATTRIBUTE_USING_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 26100200)]
        public string AttributeUsingType
        {
            get
            {
                CheckPropertyInited("AttributeUsingType");
                return _attributeusingtype;
            }
            set
            {
                _attributeusingtype = value;
                NotifyPropertyChanged("AttributeUsingType");
            }
        }


        private KoUpdateCadastralDataAttributeType _attributeusingtype_Code;
        /// <summary>
        /// 26100200 Тип использования атрибута (справочный код) (ATTRIBUTE_USING_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 26100200)]
        public KoUpdateCadastralDataAttributeType AttributeUsingType_Code
        {
            get
            {
                CheckPropertyInited("AttributeUsingType_Code");
                return this._attributeusingtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_attributeusingtype))
                    {
                         _attributeusingtype = descr;
                    }
                }
                else
                {
                     _attributeusingtype = descr;
                }

                this._attributeusingtype_Code = value;
                NotifyPropertyChanged("AttributeUsingType");
                NotifyPropertyChanged("AttributeUsingType_Code");
            }
        }


        private long? _attributeid;
        /// <summary>
        /// 26100300 Идентификатор атрибута (ATTRIBUTE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 26100300)]
        public long? AttributeId
        {
            get
            {
                CheckPropertyInited("AttributeId");
                return _attributeid;
            }
            set
            {
                _attributeid = value;
                NotifyPropertyChanged("AttributeId");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 262 Реестр с данными о процессах выгрузки результатов оценки (KO_UNLOAD_RESULT_QUEUE)
    /// </summary>
    [RegisterInfo(RegisterID = 262)]
    [Serializable]
    public partial class OMUnloadResultQueue : OMBaseClass<OMUnloadResultQueue>
    {

        private long _id;
        /// <summary>
        /// 26200100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 26200100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _userid;
        /// <summary>
        /// 26200200 Идентификатор пользователя (USER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 26200200)]
        public long UserId
        {
            get
            {
                CheckPropertyInited("UserId");
                return _userid;
            }
            set
            {
                _userid = value;
                NotifyPropertyChanged("UserId");
            }
        }


        private string _status;
        /// <summary>
        /// 26200300 Статус ()
        /// </summary>
        [RegisterAttribute(AttributeID = 26200300)]
        public string Status
        {
            get
            {
                CheckPropertyInited("Status");
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }


        private ObjectModel.Directory.Common.ImportStatus _status_Code;
        /// <summary>
        /// 26200300 Статус (справочный код) (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 26200300)]
        public ObjectModel.Directory.Common.ImportStatus Status_Code
        {
            get
            {
                CheckPropertyInited("Status_Code");
                return this._status_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_status))
                    {
                         _status = descr;
                    }
                }
                else
                {
                     _status = descr;
                }

                this._status_Code = value;
                NotifyPropertyChanged("Status");
                NotifyPropertyChanged("Status_Code");
            }
        }


        private DateTime _datecreated;
        /// <summary>
        /// 26200400 Дата создания (DATE_CREATED)
        /// </summary>
        [RegisterAttribute(AttributeID = 26200400)]
        public DateTime DateCreated
        {
            get
            {
                CheckPropertyInited("DateCreated");
                return _datecreated;
            }
            set
            {
                _datecreated = value;
                NotifyPropertyChanged("DateCreated");
            }
        }


        private DateTime? _datestarted;
        /// <summary>
        /// 26200500 Дата запуска (DATE_STARTED)
        /// </summary>
        [RegisterAttribute(AttributeID = 26200500)]
        public DateTime? DateStarted
        {
            get
            {
                CheckPropertyInited("DateStarted");
                return _datestarted;
            }
            set
            {
                _datestarted = value;
                NotifyPropertyChanged("DateStarted");
            }
        }


        private DateTime? _datefinished;
        /// <summary>
        /// 26200600 Дата завершения (DATE_FINISHED)
        /// </summary>
        [RegisterAttribute(AttributeID = 26200600)]
        public DateTime? DateFinished
        {
            get
            {
                CheckPropertyInited("DateFinished");
                return _datefinished;
            }
            set
            {
                _datefinished = value;
                NotifyPropertyChanged("DateFinished");
            }
        }


        private string _errormessage;
        /// <summary>
        /// 26200700 Сообщение об ошибке (ERROR_MESSAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 26200700)]
        public string ErrorMessage
        {
            get
            {
                CheckPropertyInited("ErrorMessage");
                return _errormessage;
            }
            set
            {
                _errormessage = value;
                NotifyPropertyChanged("ErrorMessage");
            }
        }


        private string _unloadtypesmapping;
        /// <summary>
        /// 26200800 Список выгрузок (UNLOAD_TYPES_MAPPING)
        /// </summary>
        [RegisterAttribute(AttributeID = 26200800)]
        public string UnloadTypesMapping
        {
            get
            {
                CheckPropertyInited("UnloadTypesMapping");
                return _unloadtypesmapping;
            }
            set
            {
                _unloadtypesmapping = value;
                NotifyPropertyChanged("UnloadTypesMapping");
            }
        }


        private long? _unloadtotalcount;
        /// <summary>
        /// 26200900 Общее количество выгрузок (UNLOAD_TOTAL_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 26200900)]
        public long? UnloadTotalCount
        {
            get
            {
                CheckPropertyInited("UnloadTotalCount");
                return _unloadtotalcount;
            }
            set
            {
                _unloadtotalcount = value;
                NotifyPropertyChanged("UnloadTotalCount");
            }
        }


        private long? _unloadcurrentcount;
        /// <summary>
        /// 26201000 Текущее количество выгрузок (UNLOAD_CURRENT_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 26201000)]
        public long? UnloadCurrentCount
        {
            get
            {
                CheckPropertyInited("UnloadCurrentCount");
                return _unloadcurrentcount;
            }
            set
            {
                _unloadcurrentcount = value;
                NotifyPropertyChanged("UnloadCurrentCount");
            }
        }


        private string _currentunloadtype;
        /// <summary>
        /// 26201100 Текущая выгрузка ()
        /// </summary>
        [RegisterAttribute(AttributeID = 26201100)]
        public string CurrentUnloadType
        {
            get
            {
                CheckPropertyInited("CurrentUnloadType");
                return _currentunloadtype;
            }
            set
            {
                _currentunloadtype = value;
                NotifyPropertyChanged("CurrentUnloadType");
            }
        }


        private KoUnloadResultType _currentunloadtype_Code;
        /// <summary>
        /// 26201100 Текущая выгрузка (справочный код) (CURRENT_UNLOAD_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 26201100)]
        public KoUnloadResultType CurrentUnloadType_Code
        {
            get
            {
                CheckPropertyInited("CurrentUnloadType_Code");
                return this._currentunloadtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_currentunloadtype))
                    {
                         _currentunloadtype = descr;
                    }
                }
                else
                {
                     _currentunloadtype = descr;
                }

                this._currentunloadtype_Code = value;
                NotifyPropertyChanged("CurrentUnloadType");
                NotifyPropertyChanged("CurrentUnloadType_Code");
            }
        }


        private long? _currentunloadprogress;
        /// <summary>
        /// 26201200 Прогресс выполнения текущей выгрузки (CURRENT_UNLOAD_PROGRESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 26201200)]
        public long? CurrentUnloadProgress
        {
            get
            {
                CheckPropertyInited("CurrentUnloadProgress");
                return _currentunloadprogress;
            }
            set
            {
                _currentunloadprogress = value;
                NotifyPropertyChanged("CurrentUnloadProgress");
            }
        }


        private string _exportfilesinfo;
        /// <summary>
        /// 26201300 Список выгруженных файлов (EXPORT_FILES_INFO)
        /// </summary>
        [RegisterAttribute(AttributeID = 26201300)]
        public string ExportFilesInfo
        {
            get
            {
                CheckPropertyInited("ExportFilesInfo");
                return _exportfilesinfo;
            }
            set
            {
                _exportfilesinfo = value;
                NotifyPropertyChanged("ExportFilesInfo");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 263 Настройки факторов (KO_FACTOR_SETTINGS)
    /// </summary>
    [RegisterInfo(RegisterID = 263)]
    [Serializable]
    public partial class OMFactorSettings : OMBaseClass<OMFactorSettings>
    {

        private long _id;
        /// <summary>
        /// 26300100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 26300100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _factorid;
        /// <summary>
        /// 26300200 Идентификатор фактора (FACTOR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 26300200)]
        public long? FactorId
        {
            get
            {
                CheckPropertyInited("FactorId");
                return _factorid;
            }
            set
            {
                _factorid = value;
                NotifyPropertyChanged("FactorId");
            }
        }


        private string _inheritance;
        /// <summary>
        /// 26300300 Тип наследования (справочник KOFactorInheritance) (INHERITANCE)
        /// </summary>
        [RegisterAttribute(AttributeID = 26300300)]
        public string Inheritance
        {
            get
            {
                CheckPropertyInited("Inheritance");
                return _inheritance;
            }
            set
            {
                _inheritance = value;
                NotifyPropertyChanged("Inheritance");
            }
        }


        private ObjectModel.Directory.KO.FactorInheritance _inheritance_Code;
        /// <summary>
        /// 26300300 Тип наследования (справочник KOFactorInheritance) (справочный код) (INHERITANCE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 26300300)]
        public ObjectModel.Directory.KO.FactorInheritance Inheritance_Code
        {
            get
            {
                CheckPropertyInited("Inheritance_Code");
                return this._inheritance_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_inheritance))
                    {
                         _inheritance = descr;
                    }
                }
                else
                {
                     _inheritance = descr;
                }

                this._inheritance_Code = value;
                NotifyPropertyChanged("Inheritance");
                NotifyPropertyChanged("Inheritance_Code");
            }
        }


        private string _source;
        /// <summary>
        /// 26300400 Источник для факторов отсутствующих в данных ГБУ (SOURCE)
        /// </summary>
        [RegisterAttribute(AttributeID = 26300400)]
        public string Source
        {
            get
            {
                CheckPropertyInited("Source");
                return _source;
            }
            set
            {
                _source = value;
                NotifyPropertyChanged("Source");
            }
        }


        private long? _correctfactorid;
        /// <summary>
        /// 26300500 Идентификатор корректируемого фактора (CORRECT_FACTOR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 26300500)]
        public long? CorrectFactorId
        {
            get
            {
                CheckPropertyInited("CorrectFactorId");
                return _correctfactorid;
            }
            set
            {
                _correctfactorid = value;
                NotifyPropertyChanged("CorrectFactorId");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 264 Моделирование. Справочники (KO_MODELING_DICTIONARIES)
    /// </summary>
    [RegisterInfo(RegisterID = 264)]
    [Serializable]
    public partial class OMModelingDictionary : OMBaseClass<OMModelingDictionary>
    {

        private long _id;
        /// <summary>
        /// 26400100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 26400100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _name;
        /// <summary>
        /// 26400200 Имя (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 26400200)]
        public string Name
        {
            get
            {
                CheckPropertyInited("Name");
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }


        private string _type;
        /// <summary>
        /// 26400300 Тип (TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 26400300)]
        public string Type
        {
            get
            {
                CheckPropertyInited("Type");
                return _type;
            }
            set
            {
                _type = value;
                NotifyPropertyChanged("Type");
            }
        }


        private ObjectModel.Directory.ES.ReferenceItemCodeType _type_Code;
        /// <summary>
        /// 26400300 Тип (справочный код) (TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 26400300)]
        public ObjectModel.Directory.ES.ReferenceItemCodeType Type_Code
        {
            get
            {
                CheckPropertyInited("Type_Code");
                return this._type_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_type))
                    {
                         _type = descr;
                    }
                }
                else
                {
                     _type = descr;
                }

                this._type_Code = value;
                NotifyPropertyChanged("Type");
                NotifyPropertyChanged("Type_Code");
            }
        }

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 265 Моделирование. Значения справочников (KO_MODELING_DICTIONARIES_VALUES)
    /// </summary>
    [RegisterInfo(RegisterID = 265)]
    [Serializable]
    public partial class OMModelingDictionariesValues : OMBaseClass<OMModelingDictionariesValues>
    {

        private long _id;
        /// <summary>
        /// 26500100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 26500100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _dictionaryid;
        /// <summary>
        /// 26500200 ИД справочника (DICTIONARY_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 26500200)]
        public long DictionaryId
        {
            get
            {
                CheckPropertyInited("DictionaryId");
                return _dictionaryid;
            }
            set
            {
                _dictionaryid = value;
                NotifyPropertyChanged("DictionaryId");
            }
        }


        private string _value;
        /// <summary>
        /// 26500300 Значение (VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 26500300)]
        public string Value
        {
            get
            {
                CheckPropertyInited("Value");
                return _value;
            }
            set
            {
                _value = value;
                NotifyPropertyChanged("Value");
            }
        }


        private decimal? _calculationvalue;
        /// <summary>
        /// 26500400 Значение для расчета (CALCULATION_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 26500400)]
        public decimal? CalculationValue
        {
            get
            {
                CheckPropertyInited("CalculationValue");
                return _calculationvalue;
            }
            set
            {
                _calculationvalue = value;
                NotifyPropertyChanged("CalculationValue");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 300 Экспертное заключение (SUD_ZAK)
    /// </summary>
    [RegisterInfo(RegisterID = 300)]
    [Serializable]
    public partial class OMZak : OMBaseClass<OMZak>
    {

        private long _id;
        /// <summary>
        /// 30000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 30000100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _number;
        /// <summary>
        /// 30000200 Номер заключения (NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 30000200)]
        public string Number
        {
            get
            {
                CheckPropertyInited("Number");
                return _number;
            }
            set
            {
                _number = value;
                NotifyPropertyChanged("Number");
            }
        }


        private DateTime? _date;
        /// <summary>
        /// 30000300 Дата заключения (DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30000300)]
        public DateTime? Date
        {
            get
            {
                CheckPropertyInited("Date");
                return _date;
            }
            set
            {
                _date = value;
                NotifyPropertyChanged("Date");
            }
        }


        private string _org;
        /// <summary>
        /// 30000400 Организация (ORG)
        /// </summary>
        [RegisterAttribute(AttributeID = 30000400)]
        public string Org
        {
            get
            {
                CheckPropertyInited("Org");
                return _org;
            }
            set
            {
                _org = value;
                NotifyPropertyChanged("Org");
            }
        }


        private string _fio;
        /// <summary>
        /// 30000500 ФИО эксперта (FIO)
        /// </summary>
        [RegisterAttribute(AttributeID = 30000500)]
        public string Fio
        {
            get
            {
                CheckPropertyInited("Fio");
                return _fio;
            }
            set
            {
                _fio = value;
                NotifyPropertyChanged("Fio");
            }
        }


        private string _sro;
        /// <summary>
        /// 30000600 СРО (SRO)
        /// </summary>
        [RegisterAttribute(AttributeID = 30000600)]
        public string Sro
        {
            get
            {
                CheckPropertyInited("Sro");
                return _sro;
            }
            set
            {
                _sro = value;
                NotifyPropertyChanged("Sro");
            }
        }


        private DateTime? _recdate;
        /// <summary>
        /// 30000700 Дата  сдачи рецензии (REC_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30000700)]
        public DateTime? RecDate
        {
            get
            {
                CheckPropertyInited("RecDate");
                return _recdate;
            }
            set
            {
                _recdate = value;
                NotifyPropertyChanged("RecDate");
            }
        }


        private string _recuser;
        /// <summary>
        /// 30000800 Исполнитель рецензии (REC_USER)
        /// </summary>
        [RegisterAttribute(AttributeID = 30000800)]
        public string RecUser
        {
            get
            {
                CheckPropertyInited("RecUser");
                return _recuser;
            }
            set
            {
                _recuser = value;
                NotifyPropertyChanged("RecUser");
            }
        }


        private string _recletter;
        /// <summary>
        /// 30000900 Номер письма (REC_LETTER)
        /// </summary>
        [RegisterAttribute(AttributeID = 30000900)]
        public string RecLetter
        {
            get
            {
                CheckPropertyInited("RecLetter");
                return _recletter;
            }
            set
            {
                _recletter = value;
                NotifyPropertyChanged("RecLetter");
            }
        }


        private long? _recbefore;
        /// <summary>
        /// 30001000 Предварительная рецензия (REC_BEFORE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30001000)]
        public long? RecBefore
        {
            get
            {
                CheckPropertyInited("RecBefore");
                return _recbefore;
            }
            set
            {
                _recbefore = value;
                NotifyPropertyChanged("RecBefore");
            }
        }


        private long? _recafter;
        /// <summary>
        /// 30001100 Рецензия после анализа (REC_AFTER)
        /// </summary>
        [RegisterAttribute(AttributeID = 30001100)]
        public long? RecAfter
        {
            get
            {
                CheckPropertyInited("RecAfter");
                return _recafter;
            }
            set
            {
                _recafter = value;
                NotifyPropertyChanged("RecAfter");
            }
        }


        private long? _recsoglas;
        /// <summary>
        /// 30001200 Согласовано с руководителем (REC_SOGLAS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30001200)]
        public long? RecSoglas
        {
            get
            {
                CheckPropertyInited("RecSoglas");
                return _recsoglas;
            }
            set
            {
                _recsoglas = value;
                NotifyPropertyChanged("RecSoglas");
            }
        }


        private long? _idorg;
        /// <summary>
        /// 30001300 Идентификатор организации (ID_ORG)
        /// </summary>
        [RegisterAttribute(AttributeID = 30001300)]
        public long? IdOrg
        {
            get
            {
                CheckPropertyInited("IdOrg");
                return _idorg;
            }
            set
            {
                _idorg = value;
                NotifyPropertyChanged("IdOrg");
            }
        }


        private long? _idfio;
        /// <summary>
        /// 30001400 Идентификатор эксперта (ID_FIO)
        /// </summary>
        [RegisterAttribute(AttributeID = 30001400)]
        public long? IdFio
        {
            get
            {
                CheckPropertyInited("IdFio");
                return _idfio;
            }
            set
            {
                _idfio = value;
                NotifyPropertyChanged("IdFio");
            }
        }


        private long? _idsro;
        /// <summary>
        /// 30001500 Идентификатор СРО (ID_SRO)
        /// </summary>
        [RegisterAttribute(AttributeID = 30001500)]
        public long? IdSro
        {
            get
            {
                CheckPropertyInited("IdSro");
                return _idsro;
            }
            set
            {
                _idsro = value;
                NotifyPropertyChanged("IdSro");
            }
        }


        private bool _containsattachments;
        /// <summary>
        /// 30001600 Признак наличия образа ()
        /// </summary>
        [RegisterAttribute(AttributeID = 30001600)]
        public bool ContainsAttachments
        {
            get
            {
                CheckPropertyInited("ContainsAttachments");
                return _containsattachments;
            }
            set
            {
                _containsattachments = value;
                NotifyPropertyChanged("ContainsAttachments");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 301 Журнал (SUD_LOG)
    /// </summary>
    [RegisterInfo(RegisterID = 301)]
    [Serializable]
    public partial class OMLog : OMBaseClass<OMLog>
    {

        private long _id;
        /// <summary>
        /// 30100100  (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 30100100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _iduser;
        /// <summary>
        /// 30100200  (ID_USER)
        /// </summary>
        [RegisterAttribute(AttributeID = 30100200)]
        public long? IdUser
        {
            get
            {
                CheckPropertyInited("IdUser");
                return _iduser;
            }
            set
            {
                _iduser = value;
                NotifyPropertyChanged("IdUser");
            }
        }


        private long _idtable;
        /// <summary>
        /// 30100300  (ID_TABLE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30100300)]
        public long IdTable
        {
            get
            {
                CheckPropertyInited("IdTable");
                return _idtable;
            }
            set
            {
                _idtable = value;
                NotifyPropertyChanged("IdTable");
            }
        }


        private string _typeoper;
        /// <summary>
        /// 30100400  (TYPE_OPER)
        /// </summary>
        [RegisterAttribute(AttributeID = 30100400)]
        public string TypeOper
        {
            get
            {
                CheckPropertyInited("TypeOper");
                return _typeoper;
            }
            set
            {
                _typeoper = value;
                NotifyPropertyChanged("TypeOper");
            }
        }


        private string _xmldata;
        /// <summary>
        /// 30100500  (XML_DATA)
        /// </summary>
        [RegisterAttribute(AttributeID = 30100500)]
        public string XmlData
        {
            get
            {
                CheckPropertyInited("XmlData");
                return _xmldata;
            }
            set
            {
                _xmldata = value;
                NotifyPropertyChanged("XmlData");
            }
        }


        private DateTime? _dateoper;
        /// <summary>
        /// 30100600  (DATE_OPER)
        /// </summary>
        [RegisterAttribute(AttributeID = 30100600)]
        public DateTime? DateOper
        {
            get
            {
                CheckPropertyInited("DateOper");
                return _dateoper;
            }
            set
            {
                _dateoper = value;
                NotifyPropertyChanged("DateOper");
            }
        }


        private string _nametable;
        /// <summary>
        /// 30100700  (NAME_TABLE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30100700)]
        public string NameTable
        {
            get
            {
                CheckPropertyInited("NameTable");
                return _nametable;
            }
            set
            {
                _nametable = value;
                NotifyPropertyChanged("NameTable");
            }
        }


        private long? _idrecord;
        /// <summary>
        /// 30100800  (ID_RECORD)
        /// </summary>
        [RegisterAttribute(AttributeID = 30100800)]
        public long? IdRecord
        {
            get
            {
                CheckPropertyInited("IdRecord");
                return _idrecord;
            }
            set
            {
                _idrecord = value;
                NotifyPropertyChanged("IdRecord");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 302 Связь заключения с объектом (SUD_ZAKLINK)
    /// </summary>
    [RegisterInfo(RegisterID = 302)]
    [Serializable]
    public partial class OMZakLink : OMBaseClass<OMZakLink>
    {

        private long _id;
        /// <summary>
        /// 30200100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 30200100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _idobject;
        /// <summary>
        /// 30200200 Идентификатор объекта (ID_OBJECT)
        /// </summary>
        [RegisterAttribute(AttributeID = 30200200)]
        public long? IdObject
        {
            get
            {
                CheckPropertyInited("IdObject");
                return _idobject;
            }
            set
            {
                _idobject = value;
                NotifyPropertyChanged("IdObject");
            }
        }


        private long? _idzak;
        /// <summary>
        /// 30200300 Идентификатор заключения (ID_ZAK)
        /// </summary>
        [RegisterAttribute(AttributeID = 30200300)]
        public long? IdZak
        {
            get
            {
                CheckPropertyInited("IdZak");
                return _idzak;
            }
            set
            {
                _idzak = value;
                NotifyPropertyChanged("IdZak");
            }
        }


        private string _use;
        /// <summary>
        /// 30200400 Текущее использование (USE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30200400)]
        public string Use
        {
            get
            {
                CheckPropertyInited("Use");
                return _use;
            }
            set
            {
                _use = value;
                NotifyPropertyChanged("Use");
            }
        }


        private decimal? _rs;
        /// <summary>
        /// 30200500 Рыночная стоимость (RS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30200500)]
        public decimal? Rs
        {
            get
            {
                CheckPropertyInited("Rs");
                return _rs;
            }
            set
            {
                _rs = value;
                NotifyPropertyChanged("Rs");
            }
        }


        private decimal? _uprs;
        /// <summary>
        /// 30200600 Удельная стоимость (UPRS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30200600)]
        public decimal? Uprs
        {
            get
            {
                CheckPropertyInited("Uprs");
                return _uprs;
            }
            set
            {
                _uprs = value;
                NotifyPropertyChanged("Uprs");
            }
        }


        private string _descr;
        /// <summary>
        /// 30200700 Примечание (DESCR)
        /// </summary>
        [RegisterAttribute(AttributeID = 30200700)]
        public string Descr
        {
            get
            {
                CheckPropertyInited("Descr");
                return _descr;
            }
            set
            {
                _descr = value;
                NotifyPropertyChanged("Descr");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 303 Расчет ДРС (SUD_DRS)
    /// </summary>
    [RegisterInfo(RegisterID = 303)]
    [Serializable]
    public partial class OMDRS : OMBaseClass<OMDRS>
    {

        private long _idobject;
        /// <summary>
        /// 30300100 Идентификатор объекта (ID_OBJECT)
        /// </summary>
        [PrimaryKey(AttributeID = 30300100)]
        public long IdObject
        {
            get
            {
                CheckPropertyInited("IdObject");
                return _idobject;
            }
            set
            {
                _idobject = value;
                NotifyPropertyChanged("IdObject");
            }
        }


        private string _drsgroup;
        /// <summary>
        /// 30300200 Оценочная группа (DRS_GROUP)
        /// </summary>
        [RegisterAttribute(AttributeID = 30300200)]
        public string DrsGroup
        {
            get
            {
                CheckPropertyInited("DrsGroup");
                return _drsgroup;
            }
            set
            {
                _drsgroup = value;
                NotifyPropertyChanged("DrsGroup");
            }
        }


        private decimal? _drssq1;
        /// <summary>
        /// 30300300 Подвал (DRS_SQ1)
        /// </summary>
        [RegisterAttribute(AttributeID = 30300300)]
        public decimal? DrsSq1
        {
            get
            {
                CheckPropertyInited("DrsSq1");
                return _drssq1;
            }
            set
            {
                _drssq1 = value;
                NotifyPropertyChanged("DrsSq1");
            }
        }


        private decimal? _drssq2;
        /// <summary>
        /// 30300400 Цоколь (DRS_SQ2)
        /// </summary>
        [RegisterAttribute(AttributeID = 30300400)]
        public decimal? DrsSq2
        {
            get
            {
                CheckPropertyInited("DrsSq2");
                return _drssq2;
            }
            set
            {
                _drssq2 = value;
                NotifyPropertyChanged("DrsSq2");
            }
        }


        private decimal? _drssq3;
        /// <summary>
        /// 30300500 Торговля (DRS_SQ3)
        /// </summary>
        [RegisterAttribute(AttributeID = 30300500)]
        public decimal? DrsSq3
        {
            get
            {
                CheckPropertyInited("DrsSq3");
                return _drssq3;
            }
            set
            {
                _drssq3 = value;
                NotifyPropertyChanged("DrsSq3");
            }
        }


        private decimal? _drssq4;
        /// <summary>
        /// 30300600 Офис (DRS_SQ4)
        /// </summary>
        [RegisterAttribute(AttributeID = 30300600)]
        public decimal? DrsSq4
        {
            get
            {
                CheckPropertyInited("DrsSq4");
                return _drssq4;
            }
            set
            {
                _drssq4 = value;
                NotifyPropertyChanged("DrsSq4");
            }
        }


        private decimal? _drssq5;
        /// <summary>
        /// 30300700 Производство (DRS_SQ5)
        /// </summary>
        [RegisterAttribute(AttributeID = 30300700)]
        public decimal? DrsSq5
        {
            get
            {
                CheckPropertyInited("DrsSq5");
                return _drssq5;
            }
            set
            {
                _drssq5 = value;
                NotifyPropertyChanged("DrsSq5");
            }
        }


        private decimal? _drssq6;
        /// <summary>
        /// 30300800 Гаражи, паркинг (DRS_SQ6)
        /// </summary>
        [RegisterAttribute(AttributeID = 30300800)]
        public decimal? DrsSq6
        {
            get
            {
                CheckPropertyInited("DrsSq6");
                return _drssq6;
            }
            set
            {
                _drssq6 = value;
                NotifyPropertyChanged("DrsSq6");
            }
        }


        private decimal? _drssq7;
        /// <summary>
        /// 30300900 Социальное (DRS_SQ7)
        /// </summary>
        [RegisterAttribute(AttributeID = 30300900)]
        public decimal? DrsSq7
        {
            get
            {
                CheckPropertyInited("DrsSq7");
                return _drssq7;
            }
            set
            {
                _drssq7 = value;
                NotifyPropertyChanged("DrsSq7");
            }
        }


        private decimal? _drssq8;
        /// <summary>
        /// 30301000 Апартаменты (DRS_SQ8)
        /// </summary>
        [RegisterAttribute(AttributeID = 30301000)]
        public decimal? DrsSq8
        {
            get
            {
                CheckPropertyInited("DrsSq8");
                return _drssq8;
            }
            set
            {
                _drssq8 = value;
                NotifyPropertyChanged("DrsSq8");
            }
        }


        private decimal? _drssq9;
        /// <summary>
        /// 30301100 Иное назначение(15.7) (DRS_SQ9)
        /// </summary>
        [RegisterAttribute(AttributeID = 30301100)]
        public decimal? DrsSq9
        {
            get
            {
                CheckPropertyInited("DrsSq9");
                return _drssq9;
            }
            set
            {
                _drssq9 = value;
                NotifyPropertyChanged("DrsSq9");
            }
        }


        private string _drssost;
        /// <summary>
        /// 30301200 Техническое состояние (DRS_SOST)
        /// </summary>
        [RegisterAttribute(AttributeID = 30301200)]
        public string DrsSost
        {
            get
            {
                CheckPropertyInited("DrsSost");
                return _drssost;
            }
            set
            {
                _drssost = value;
                NotifyPropertyChanged("DrsSost");
            }
        }


        private string _drsprichin;
        /// <summary>
        /// 30301300 Причина пересчета (DRS_PRICHIN)
        /// </summary>
        [RegisterAttribute(AttributeID = 30301300)]
        public string DrsPrichin
        {
            get
            {
                CheckPropertyInited("DrsPrichin");
                return _drsprichin;
            }
            set
            {
                _drsprichin = value;
                NotifyPropertyChanged("DrsPrichin");
            }
        }


        private decimal? _drsupdrs;
        /// <summary>
        /// 30301400 УПДРС (DRS_UPDRS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30301400)]
        public decimal? DrsUpdrs
        {
            get
            {
                CheckPropertyInited("DrsUpdrs");
                return _drsupdrs;
            }
            set
            {
                _drsupdrs = value;
                NotifyPropertyChanged("DrsUpdrs");
            }
        }


        private decimal? _drsdrs;
        /// <summary>
        /// 30301500 ДРС (DRS_DRS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30301500)]
        public decimal? DrsDrs
        {
            get
            {
                CheckPropertyInited("DrsDrs");
                return _drsdrs;
            }
            set
            {
                _drsdrs = value;
                NotifyPropertyChanged("DrsDrs");
            }
        }


        private string _drsowner;
        /// <summary>
        /// 30301600 Источник (DRS_OWNER)
        /// </summary>
        [RegisterAttribute(AttributeID = 30301600)]
        public string DrsOwner
        {
            get
            {
                CheckPropertyInited("DrsOwner");
                return _drsowner;
            }
            set
            {
                _drsowner = value;
                NotifyPropertyChanged("DrsOwner");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 304 Связь отчета с объектом (SUD_OTCHETLINK)
    /// </summary>
    [RegisterInfo(RegisterID = 304)]
    [Serializable]
    public partial class OMOtchetLink : OMBaseClass<OMOtchetLink>
    {

        private long _id;
        /// <summary>
        /// 30400100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 30400100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _idobject;
        /// <summary>
        /// 30400200 Идентификатор объекта (ID_OBJECT)
        /// </summary>
        [RegisterAttribute(AttributeID = 30400200)]
        public long? IdObject
        {
            get
            {
                CheckPropertyInited("IdObject");
                return _idobject;
            }
            set
            {
                _idobject = value;
                NotifyPropertyChanged("IdObject");
            }
        }


        private long? _idotchet;
        /// <summary>
        /// 30400300 Идентификатор отчета (ID_OTCHET)
        /// </summary>
        [RegisterAttribute(AttributeID = 30400300)]
        public long? IdOtchet
        {
            get
            {
                CheckPropertyInited("IdOtchet");
                return _idotchet;
            }
            set
            {
                _idotchet = value;
                NotifyPropertyChanged("IdOtchet");
            }
        }


        private string _use;
        /// <summary>
        /// 30400400 Использование по отчету (USE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30400400)]
        public string Use
        {
            get
            {
                CheckPropertyInited("Use");
                return _use;
            }
            set
            {
                _use = value;
                NotifyPropertyChanged("Use");
            }
        }


        private decimal? _rs;
        /// <summary>
        /// 30400500 Рыночная стоимость по отчету (RS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30400500)]
        public decimal? Rs
        {
            get
            {
                CheckPropertyInited("Rs");
                return _rs;
            }
            set
            {
                _rs = value;
                NotifyPropertyChanged("Rs");
            }
        }


        private decimal? _uprs;
        /// <summary>
        /// 30400600 УПРС по отчету (UPRS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30400600)]
        public decimal? Uprs
        {
            get
            {
                CheckPropertyInited("Uprs");
                return _uprs;
            }
            set
            {
                _uprs = value;
                NotifyPropertyChanged("Uprs");
            }
        }


        private string _descr;
        /// <summary>
        /// 30400700 Примечание (DESCR)
        /// </summary>
        [RegisterAttribute(AttributeID = 30400700)]
        public string Descr
        {
            get
            {
                CheckPropertyInited("Descr");
                return _descr;
            }
            set
            {
                _descr = value;
                NotifyPropertyChanged("Descr");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 305 Статус объекта (SUD_OBJECTSTATUS)
    /// </summary>
    [RegisterInfo(RegisterID = 305)]
    [Serializable]
    public partial class OMObjectStatus : OMBaseClass<OMObjectStatus>
    {

        private long _id;
        /// <summary>
        /// 30500100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 30500100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private bool _kn;
        /// <summary>
        /// 30500200 Кадастровый номер (KN)
        /// </summary>
        [RegisterAttribute(AttributeID = 30500200)]
        public bool Kn
        {
            get
            {
                CheckPropertyInited("Kn");
                return _kn;
            }
            set
            {
                _kn = value;
                NotifyPropertyChanged("Kn");
            }
        }


        private bool _date;
        /// <summary>
        /// 30500300 Дата (DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30500300)]
        public bool Date
        {
            get
            {
                CheckPropertyInited("Date");
                return _date;
            }
            set
            {
                _date = value;
                NotifyPropertyChanged("Date");
            }
        }


        private bool _square;
        /// <summary>
        /// 30500400 Площадь (SQUARE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30500400)]
        public bool Square
        {
            get
            {
                CheckPropertyInited("Square");
                return _square;
            }
            set
            {
                _square = value;
                NotifyPropertyChanged("Square");
            }
        }


        private bool _kc;
        /// <summary>
        /// 30500500 Кадастровая стоимость (KC)
        /// </summary>
        [RegisterAttribute(AttributeID = 30500500)]
        public bool Kc
        {
            get
            {
                CheckPropertyInited("Kc");
                return _kc;
            }
            set
            {
                _kc = value;
                NotifyPropertyChanged("Kc");
            }
        }


        private bool _namecenter;
        /// <summary>
        /// 30500600 Наимеование центра (NAME_CENTER)
        /// </summary>
        [RegisterAttribute(AttributeID = 30500600)]
        public bool NameCenter
        {
            get
            {
                CheckPropertyInited("NameCenter");
                return _namecenter;
            }
            set
            {
                _namecenter = value;
                NotifyPropertyChanged("NameCenter");
            }
        }


        private bool _statdgi;
        /// <summary>
        /// 30500700 Статус ДГИ (STAT_DGI)
        /// </summary>
        [RegisterAttribute(AttributeID = 30500700)]
        public bool StatDgi
        {
            get
            {
                CheckPropertyInited("StatDgi");
                return _statdgi;
            }
            set
            {
                _statdgi = value;
                NotifyPropertyChanged("StatDgi");
            }
        }


        private bool _owner;
        /// <summary>
        /// 30500800 Владелец (OWNER)
        /// </summary>
        [RegisterAttribute(AttributeID = 30500800)]
        public bool Owner
        {
            get
            {
                CheckPropertyInited("Owner");
                return _owner;
            }
            set
            {
                _owner = value;
                NotifyPropertyChanged("Owner");
            }
        }


        private bool _adres;
        /// <summary>
        /// 30500900 Адрес (ADRES)
        /// </summary>
        [RegisterAttribute(AttributeID = 30500900)]
        public bool Adres
        {
            get
            {
                CheckPropertyInited("Adres");
                return _adres;
            }
            set
            {
                _adres = value;
                NotifyPropertyChanged("Adres");
            }
        }


        private bool _typeobj;
        /// <summary>
        /// 30501000 Тип объекта (TYPEOBJ)
        /// </summary>
        [RegisterAttribute(AttributeID = 30501000)]
        public bool Typeobj
        {
            get
            {
                CheckPropertyInited("Typeobj");
                return _typeobj;
            }
            set
            {
                _typeobj = value;
                NotifyPropertyChanged("Typeobj");
            }
        }


        private bool _status;
        /// <summary>
        /// 30501100 Статус (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30501100)]
        public bool Status
        {
            get
            {
                CheckPropertyInited("Status");
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }


        private bool _applicanttype;
        /// <summary>
        /// 30501200 Тип заявителя (APPLICANT_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30501200)]
        public bool ApplicantType
        {
            get
            {
                CheckPropertyInited("ApplicantType");
                return _applicanttype;
            }
            set
            {
                _applicanttype = value;
                NotifyPropertyChanged("ApplicantType");
            }
        }


        private bool _typeofownership;
        /// <summary>
        /// 30501300 Форма собственности (TYPE_OF_OWNERSHIP)
        /// </summary>
        [RegisterAttribute(AttributeID = 30501300)]
        public bool TypeOfOwnership
        {
            get
            {
                CheckPropertyInited("TypeOfOwnership");
                return _typeofownership;
            }
            set
            {
                _typeofownership = value;
                NotifyPropertyChanged("TypeOfOwnership");
            }
        }


        private bool _exception;
        /// <summary>
        /// 30501400 Исключение (EXCEPTION)
        /// </summary>
        [RegisterAttribute(AttributeID = 30501400)]
        public bool Exception
        {
            get
            {
                CheckPropertyInited("Exception");
                return _exception;
            }
            set
            {
                _exception = value;
                NotifyPropertyChanged("Exception");
            }
        }


        private bool _additionalanalysis;
        /// <summary>
        /// 30501500 Дополнительный анализ (ADDITIONAL_ANALYSIS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30501500)]
        public bool AdditionalAnalysis
        {
            get
            {
                CheckPropertyInited("AdditionalAnalysis");
                return _additionalanalysis;
            }
            set
            {
                _additionalanalysis = value;
                NotifyPropertyChanged("AdditionalAnalysis");
            }
        }


        private bool _issatisfied;
        /// <summary>
        /// 30501600 Статус удовлетворения объекта (IS_SATISFIED)
        /// </summary>
        [RegisterAttribute(AttributeID = 30501600)]
        public bool IsSatisfied
        {
            get
            {
                CheckPropertyInited("IsSatisfied");
                return _issatisfied;
            }
            set
            {
                _issatisfied = value;
                NotifyPropertyChanged("IsSatisfied");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 306 Связь отчета и статуса (SUD_OTCHETLINKSTATUS)
    /// </summary>
    [RegisterInfo(RegisterID = 306)]
    [Serializable]
    public partial class OMOtchetLinkStatus : OMBaseClass<OMOtchetLinkStatus>
    {

        private long _id;
        /// <summary>
        /// 30600100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 30600100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _idobject;
        /// <summary>
        /// 30600200 ID_OBJECT (ID_OBJECT)
        /// </summary>
        [RegisterAttribute(AttributeID = 30600200)]
        public long IdObject
        {
            get
            {
                CheckPropertyInited("IdObject");
                return _idobject;
            }
            set
            {
                _idobject = value;
                NotifyPropertyChanged("IdObject");
            }
        }


        private long _idotchet;
        /// <summary>
        /// 30600300 ID_OTCHET (ID_OTCHET)
        /// </summary>
        [RegisterAttribute(AttributeID = 30600300)]
        public long IdOtchet
        {
            get
            {
                CheckPropertyInited("IdOtchet");
                return _idotchet;
            }
            set
            {
                _idotchet = value;
                NotifyPropertyChanged("IdOtchet");
            }
        }


        private long _use;
        /// <summary>
        /// 30600400 USE (USE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30600400)]
        public long Use
        {
            get
            {
                CheckPropertyInited("Use");
                return _use;
            }
            set
            {
                _use = value;
                NotifyPropertyChanged("Use");
            }
        }


        private long _rs;
        /// <summary>
        /// 30600500 RS (RS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30600500)]
        public long Rs
        {
            get
            {
                CheckPropertyInited("Rs");
                return _rs;
            }
            set
            {
                _rs = value;
                NotifyPropertyChanged("Rs");
            }
        }


        private long _uprs;
        /// <summary>
        /// 30600600 UPRS (UPRS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30600600)]
        public long Uprs
        {
            get
            {
                CheckPropertyInited("Uprs");
                return _uprs;
            }
            set
            {
                _uprs = value;
                NotifyPropertyChanged("Uprs");
            }
        }


        private long _descr;
        /// <summary>
        /// 30600700 DESCR (DESCR)
        /// </summary>
        [RegisterAttribute(AttributeID = 30600700)]
        public long Descr
        {
            get
            {
                CheckPropertyInited("Descr");
                return _descr;
            }
            set
            {
                _descr = value;
                NotifyPropertyChanged("Descr");
            }
        }


        private long? _status;
        /// <summary>
        /// 30600800 Статус (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30600800)]
        public long? Status
        {
            get
            {
                CheckPropertyInited("Status");
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 307 Статус отчета (SUD_OTCHETSTATUS)
    /// </summary>
    [RegisterInfo(RegisterID = 307)]
    [Serializable]
    public partial class OMOtchetStatus : OMBaseClass<OMOtchetStatus>
    {

        private long _id;
        /// <summary>
        /// 30700100 ID (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 30700100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _number;
        /// <summary>
        /// 30700200 NUMBER (NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 30700200)]
        public long Number
        {
            get
            {
                CheckPropertyInited("Number");
                return _number;
            }
            set
            {
                _number = value;
                NotifyPropertyChanged("Number");
            }
        }


        private long _date;
        /// <summary>
        /// 30700300 DATE (DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30700300)]
        public long Date
        {
            get
            {
                CheckPropertyInited("Date");
                return _date;
            }
            set
            {
                _date = value;
                NotifyPropertyChanged("Date");
            }
        }


        private long _datein;
        /// <summary>
        /// 30700400 DATE_IN (DATE_IN)
        /// </summary>
        [RegisterAttribute(AttributeID = 30700400)]
        public long DateIn
        {
            get
            {
                CheckPropertyInited("DateIn");
                return _datein;
            }
            set
            {
                _datein = value;
                NotifyPropertyChanged("DateIn");
            }
        }


        private long _jalob;
        /// <summary>
        /// 30700500 JALOB (JALOB)
        /// </summary>
        [RegisterAttribute(AttributeID = 30700500)]
        public long Jalob
        {
            get
            {
                CheckPropertyInited("Jalob");
                return _jalob;
            }
            set
            {
                _jalob = value;
                NotifyPropertyChanged("Jalob");
            }
        }


        private long _idorg;
        /// <summary>
        /// 30700600 ID_ORG (ID_ORG)
        /// </summary>
        [RegisterAttribute(AttributeID = 30700600)]
        public long IdOrg
        {
            get
            {
                CheckPropertyInited("IdOrg");
                return _idorg;
            }
            set
            {
                _idorg = value;
                NotifyPropertyChanged("IdOrg");
            }
        }


        private long _idfio;
        /// <summary>
        /// 30700700 ID_FIO (ID_FIO)
        /// </summary>
        [RegisterAttribute(AttributeID = 30700700)]
        public long IdFio
        {
            get
            {
                CheckPropertyInited("IdFio");
                return _idfio;
            }
            set
            {
                _idfio = value;
                NotifyPropertyChanged("IdFio");
            }
        }


        private long _idsro;
        /// <summary>
        /// 30700800 ID_SRO (ID_SRO)
        /// </summary>
        [RegisterAttribute(AttributeID = 30700800)]
        public long IdSro
        {
            get
            {
                CheckPropertyInited("IdSro");
                return _idsro;
            }
            set
            {
                _idsro = value;
                NotifyPropertyChanged("IdSro");
            }
        }


        private long _status;
        /// <summary>
        /// 30700900 Статус (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30700900)]
        public long Status
        {
            get
            {
                CheckPropertyInited("Status");
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 308 Отчеты (SUD_OTCHET)
    /// </summary>
    [RegisterInfo(RegisterID = 308)]
    [Serializable]
    public partial class OMOtchet : OMBaseClass<OMOtchet>
    {

        private long _id;
        /// <summary>
        /// 30800100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 30800100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _number;
        /// <summary>
        /// 30800200 Номер отчета (NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 30800200)]
        public string Number
        {
            get
            {
                CheckPropertyInited("Number");
                return _number;
            }
            set
            {
                _number = value;
                NotifyPropertyChanged("Number");
            }
        }


        private DateTime? _date;
        /// <summary>
        /// 30800300 Дата отчета (DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30800300)]
        public DateTime? Date
        {
            get
            {
                CheckPropertyInited("Date");
                return _date;
            }
            set
            {
                _date = value;
                NotifyPropertyChanged("Date");
            }
        }


        private string _org;
        /// <summary>
        /// 30800400 Организация (ORG)
        /// </summary>
        [RegisterAttribute(AttributeID = 30800400)]
        public string Org
        {
            get
            {
                CheckPropertyInited("Org");
                return _org;
            }
            set
            {
                _org = value;
                NotifyPropertyChanged("Org");
            }
        }


        private string _fio;
        /// <summary>
        /// 30800500 ФИО оценщика (FIO)
        /// </summary>
        [RegisterAttribute(AttributeID = 30800500)]
        public string Fio
        {
            get
            {
                CheckPropertyInited("Fio");
                return _fio;
            }
            set
            {
                _fio = value;
                NotifyPropertyChanged("Fio");
            }
        }


        private string _sro;
        /// <summary>
        /// 30800600 СРО (SRO)
        /// </summary>
        [RegisterAttribute(AttributeID = 30800600)]
        public string Sro
        {
            get
            {
                CheckPropertyInited("Sro");
                return _sro;
            }
            set
            {
                _sro = value;
                NotifyPropertyChanged("Sro");
            }
        }


        private DateTime? _datein;
        /// <summary>
        /// 30800700 Дата получения (DATE_IN)
        /// </summary>
        [RegisterAttribute(AttributeID = 30800700)]
        public DateTime? DateIn
        {
            get
            {
                CheckPropertyInited("DateIn");
                return _datein;
            }
            set
            {
                _datein = value;
                NotifyPropertyChanged("DateIn");
            }
        }


        private long? _jalob;
        /// <summary>
        /// 30800800 Жалоба в СРО (JALOB)
        /// </summary>
        [RegisterAttribute(AttributeID = 30800800)]
        public long? Jalob
        {
            get
            {
                CheckPropertyInited("Jalob");
                return _jalob;
            }
            set
            {
                _jalob = value;
                NotifyPropertyChanged("Jalob");
            }
        }


        private long? _idorg;
        /// <summary>
        /// 30800900 Идентификатор организации (ID_ORG)
        /// </summary>
        [RegisterAttribute(AttributeID = 30800900)]
        public long? IdOrg
        {
            get
            {
                CheckPropertyInited("IdOrg");
                return _idorg;
            }
            set
            {
                _idorg = value;
                NotifyPropertyChanged("IdOrg");
            }
        }


        private long? _idfio;
        /// <summary>
        /// 30801000 Идентификатор оценщика (ID_FIO)
        /// </summary>
        [RegisterAttribute(AttributeID = 30801000)]
        public long? IdFio
        {
            get
            {
                CheckPropertyInited("IdFio");
                return _idfio;
            }
            set
            {
                _idfio = value;
                NotifyPropertyChanged("IdFio");
            }
        }


        private long? _idsro;
        /// <summary>
        /// 30801100 Идентификатор СРО (ID_SRO)
        /// </summary>
        [RegisterAttribute(AttributeID = 30801100)]
        public long? IdSro
        {
            get
            {
                CheckPropertyInited("IdSro");
                return _idsro;
            }
            set
            {
                _idsro = value;
                NotifyPropertyChanged("IdSro");
            }
        }


        private bool _containsattachments;
        /// <summary>
        /// 30801200 Признак наличия образа ()
        /// </summary>
        [RegisterAttribute(AttributeID = 30801200)]
        public bool ContainsAttachments
        {
            get
            {
                CheckPropertyInited("ContainsAttachments");
                return _containsattachments;
            }
            set
            {
                _containsattachments = value;
                NotifyPropertyChanged("ContainsAttachments");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 309 Связь суда и статуса (SUD_SUDLINKSTATUS)
    /// </summary>
    [RegisterInfo(RegisterID = 309)]
    [Serializable]
    public partial class OMSudLinkStatus : OMBaseClass<OMSudLinkStatus>
    {

        private long _id;
        /// <summary>
        /// 30900100 ID (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 30900100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _idobject;
        /// <summary>
        /// 30900200 ID_OBJECT (ID_OBJECT)
        /// </summary>
        [RegisterAttribute(AttributeID = 30900200)]
        public long IdObject
        {
            get
            {
                CheckPropertyInited("IdObject");
                return _idobject;
            }
            set
            {
                _idobject = value;
                NotifyPropertyChanged("IdObject");
            }
        }


        private long _idsud;
        /// <summary>
        /// 30900300 ID_SUD (ID_SUD)
        /// </summary>
        [RegisterAttribute(AttributeID = 30900300)]
        public long IdSud
        {
            get
            {
                CheckPropertyInited("IdSud");
                return _idsud;
            }
            set
            {
                _idsud = value;
                NotifyPropertyChanged("IdSud");
            }
        }


        private long _use;
        /// <summary>
        /// 30900400 USE (USE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30900400)]
        public long Use
        {
            get
            {
                CheckPropertyInited("Use");
                return _use;
            }
            set
            {
                _use = value;
                NotifyPropertyChanged("Use");
            }
        }


        private long _rs;
        /// <summary>
        /// 30900500 RS (RS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30900500)]
        public long Rs
        {
            get
            {
                CheckPropertyInited("Rs");
                return _rs;
            }
            set
            {
                _rs = value;
                NotifyPropertyChanged("Rs");
            }
        }


        private long _uprs;
        /// <summary>
        /// 30900600 UPRS (UPRS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30900600)]
        public long Uprs
        {
            get
            {
                CheckPropertyInited("Uprs");
                return _uprs;
            }
            set
            {
                _uprs = value;
                NotifyPropertyChanged("Uprs");
            }
        }


        private long _descr;
        /// <summary>
        /// 30900700 DESCR (DESCR)
        /// </summary>
        [RegisterAttribute(AttributeID = 30900700)]
        public long Descr
        {
            get
            {
                CheckPropertyInited("Descr");
                return _descr;
            }
            set
            {
                _descr = value;
                NotifyPropertyChanged("Descr");
            }
        }


        private long? _status;
        /// <summary>
        /// 30900800 Статус (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30900800)]
        public long? Status
        {
            get
            {
                CheckPropertyInited("Status");
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 310 Статус (SUD_SUDSTATUS)
    /// </summary>
    [RegisterInfo(RegisterID = 310)]
    [Serializable]
    public partial class OMSudStatus : OMBaseClass<OMSudStatus>
    {

        private long _id;
        /// <summary>
        /// 31000100 Статус идентификатора (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 31000100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _name;
        /// <summary>
        /// 31000200 Статус Наименования (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 31000200)]
        public long Name
        {
            get
            {
                CheckPropertyInited("Name");
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }


        private long _number;
        /// <summary>
        /// 31000300 Статус Номера (NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 31000300)]
        public long Number
        {
            get
            {
                CheckPropertyInited("Number");
                return _number;
            }
            set
            {
                _number = value;
                NotifyPropertyChanged("Number");
            }
        }


        private long _date;
        /// <summary>
        /// 31000400 Статуса Даты (DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31000400)]
        public long Date
        {
            get
            {
                CheckPropertyInited("Date");
                return _date;
            }
            set
            {
                _date = value;
                NotifyPropertyChanged("Date");
            }
        }


        private long _suddate;
        /// <summary>
        /// 31000500 Статус Даты заседания (SUD_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31000500)]
        public long SudDate
        {
            get
            {
                CheckPropertyInited("SudDate");
                return _suddate;
            }
            set
            {
                _suddate = value;
                NotifyPropertyChanged("SudDate");
            }
        }


        private long _status;
        /// <summary>
        /// 31000600 Статус Статуса дела (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 31000600)]
        public long Status
        {
            get
            {
                CheckPropertyInited("Status");
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }


        private long? _astatus;
        /// <summary>
        /// 31000700 Статус Итоговый (ASTATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 31000700)]
        public long? Astatus
        {
            get
            {
                CheckPropertyInited("Astatus");
                return _astatus;
            }
            set
            {
                _astatus = value;
                NotifyPropertyChanged("Astatus");
            }
        }


        private long _archivenumber;
        /// <summary>
        /// 31000800 Статус Номер архивный (ARCHIVE_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 31000800)]
        public long ArchiveNumber
        {
            get
            {
                CheckPropertyInited("ArchiveNumber");
                return _archivenumber;
            }
            set
            {
                _archivenumber = value;
                NotifyPropertyChanged("ArchiveNumber");
            }
        }


        private long _appealnumber;
        /// <summary>
        /// 31000900 Статус Номер аппеляции (APPEAL_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 31000900)]
        public long AppealNumber
        {
            get
            {
                CheckPropertyInited("AppealNumber");
                return _appealnumber;
            }
            set
            {
                _appealnumber = value;
                NotifyPropertyChanged("AppealNumber");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 311 Связь заключения и статуса (SUD_ZAKLINKSTATUS)
    /// </summary>
    [RegisterInfo(RegisterID = 311)]
    [Serializable]
    public partial class OMZakLinkStatus : OMBaseClass<OMZakLinkStatus>
    {

        private long _id;
        /// <summary>
        /// 31100100 ID (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 31100100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _idobject;
        /// <summary>
        /// 31100200 ID_OBJECT (ID_OBJECT)
        /// </summary>
        [RegisterAttribute(AttributeID = 31100200)]
        public long IdObject
        {
            get
            {
                CheckPropertyInited("IdObject");
                return _idobject;
            }
            set
            {
                _idobject = value;
                NotifyPropertyChanged("IdObject");
            }
        }


        private long _idzak;
        /// <summary>
        /// 31100300 ID_ZAK (ID_ZAK)
        /// </summary>
        [RegisterAttribute(AttributeID = 31100300)]
        public long IdZak
        {
            get
            {
                CheckPropertyInited("IdZak");
                return _idzak;
            }
            set
            {
                _idzak = value;
                NotifyPropertyChanged("IdZak");
            }
        }


        private long _use;
        /// <summary>
        /// 31100400 USE (USE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31100400)]
        public long Use
        {
            get
            {
                CheckPropertyInited("Use");
                return _use;
            }
            set
            {
                _use = value;
                NotifyPropertyChanged("Use");
            }
        }


        private long _rs;
        /// <summary>
        /// 31100500 RS (RS)
        /// </summary>
        [RegisterAttribute(AttributeID = 31100500)]
        public long Rs
        {
            get
            {
                CheckPropertyInited("Rs");
                return _rs;
            }
            set
            {
                _rs = value;
                NotifyPropertyChanged("Rs");
            }
        }


        private long _uprs;
        /// <summary>
        /// 31100600 UPRS (UPRS)
        /// </summary>
        [RegisterAttribute(AttributeID = 31100600)]
        public long Uprs
        {
            get
            {
                CheckPropertyInited("Uprs");
                return _uprs;
            }
            set
            {
                _uprs = value;
                NotifyPropertyChanged("Uprs");
            }
        }


        private long _descr;
        /// <summary>
        /// 31100700 DESCR (DESCR)
        /// </summary>
        [RegisterAttribute(AttributeID = 31100700)]
        public long Descr
        {
            get
            {
                CheckPropertyInited("Descr");
                return _descr;
            }
            set
            {
                _descr = value;
                NotifyPropertyChanged("Descr");
            }
        }


        private long? _status;
        /// <summary>
        /// 31100800 Статус (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 31100800)]
        public long? Status
        {
            get
            {
                CheckPropertyInited("Status");
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 312 Статус заключения (SUD_ZAKSTATUS)
    /// </summary>
    [RegisterInfo(RegisterID = 312)]
    [Serializable]
    public partial class OMZakStatus : OMBaseClass<OMZakStatus>
    {

        private long _id;
        /// <summary>
        /// 31200100 ID (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 31200100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _number;
        /// <summary>
        /// 31200200 NUMBER (NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 31200200)]
        public long Number
        {
            get
            {
                CheckPropertyInited("Number");
                return _number;
            }
            set
            {
                _number = value;
                NotifyPropertyChanged("Number");
            }
        }


        private long _date;
        /// <summary>
        /// 31200300 DATE (DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31200300)]
        public long Date
        {
            get
            {
                CheckPropertyInited("Date");
                return _date;
            }
            set
            {
                _date = value;
                NotifyPropertyChanged("Date");
            }
        }


        private long _recdate;
        /// <summary>
        /// 31200400 REC_DATE (REC_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31200400)]
        public long RecDate
        {
            get
            {
                CheckPropertyInited("RecDate");
                return _recdate;
            }
            set
            {
                _recdate = value;
                NotifyPropertyChanged("RecDate");
            }
        }


        private long _recuser;
        /// <summary>
        /// 31200500 REC_USER (REC_USER)
        /// </summary>
        [RegisterAttribute(AttributeID = 31200500)]
        public long RecUser
        {
            get
            {
                CheckPropertyInited("RecUser");
                return _recuser;
            }
            set
            {
                _recuser = value;
                NotifyPropertyChanged("RecUser");
            }
        }


        private long _recletter;
        /// <summary>
        /// 31200600 REC_LETTER (REC_LETTER)
        /// </summary>
        [RegisterAttribute(AttributeID = 31200600)]
        public long RecLetter
        {
            get
            {
                CheckPropertyInited("RecLetter");
                return _recletter;
            }
            set
            {
                _recletter = value;
                NotifyPropertyChanged("RecLetter");
            }
        }


        private long _recbefore;
        /// <summary>
        /// 31200700 REC_BEFORE (REC_BEFORE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31200700)]
        public long RecBefore
        {
            get
            {
                CheckPropertyInited("RecBefore");
                return _recbefore;
            }
            set
            {
                _recbefore = value;
                NotifyPropertyChanged("RecBefore");
            }
        }


        private long _recafter;
        /// <summary>
        /// 31200800 REC_AFTER (REC_AFTER)
        /// </summary>
        [RegisterAttribute(AttributeID = 31200800)]
        public long RecAfter
        {
            get
            {
                CheckPropertyInited("RecAfter");
                return _recafter;
            }
            set
            {
                _recafter = value;
                NotifyPropertyChanged("RecAfter");
            }
        }


        private long _recsoglas;
        /// <summary>
        /// 31200900 REC_SOGLAS (REC_SOGLAS)
        /// </summary>
        [RegisterAttribute(AttributeID = 31200900)]
        public long RecSoglas
        {
            get
            {
                CheckPropertyInited("RecSoglas");
                return _recsoglas;
            }
            set
            {
                _recsoglas = value;
                NotifyPropertyChanged("RecSoglas");
            }
        }


        private long _idorg;
        /// <summary>
        /// 31201000 ID_ORG (ID_ORG)
        /// </summary>
        [RegisterAttribute(AttributeID = 31201000)]
        public long IdOrg
        {
            get
            {
                CheckPropertyInited("IdOrg");
                return _idorg;
            }
            set
            {
                _idorg = value;
                NotifyPropertyChanged("IdOrg");
            }
        }


        private long _idfio;
        /// <summary>
        /// 31201100 ID_FIO (ID_FIO)
        /// </summary>
        [RegisterAttribute(AttributeID = 31201100)]
        public long IdFio
        {
            get
            {
                CheckPropertyInited("IdFio");
                return _idfio;
            }
            set
            {
                _idfio = value;
                NotifyPropertyChanged("IdFio");
            }
        }


        private long _idsro;
        /// <summary>
        /// 31201200 ID_SRO (ID_SRO)
        /// </summary>
        [RegisterAttribute(AttributeID = 31201200)]
        public long IdSro
        {
            get
            {
                CheckPropertyInited("IdSro");
                return _idsro;
            }
            set
            {
                _idsro = value;
                NotifyPropertyChanged("IdSro");
            }
        }


        private long _status;
        /// <summary>
        /// 31201300 Статус (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 31201300)]
        public long Status
        {
            get
            {
                CheckPropertyInited("Status");
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 313 Справочник ФИО, организаций, СРО (SUD_DICT)
    /// </summary>
    [RegisterInfo(RegisterID = 313)]
    [Serializable]
    public partial class OMDict : OMBaseClass<OMDict>
    {

        private long _id;
        /// <summary>
        /// 31300100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 31300100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _type;
        /// <summary>
        /// 31300200 Тип справочника (Физ.лицо, Организация, СРО) (TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31300200)]
        public long Type
        {
            get
            {
                CheckPropertyInited("Type");
                return _type;
            }
            set
            {
                _type = value;
                NotifyPropertyChanged("Type");
            }
        }


        private string _name;
        /// <summary>
        /// 31300300 Наименование (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 31300300)]
        public string Name
        {
            get
            {
                CheckPropertyInited("Name");
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }


        private long? _idparent;
        /// <summary>
        /// 31300400 Ссылка на родительскую запись (ID_PARENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 31300400)]
        public long? IdParent
        {
            get
            {
                CheckPropertyInited("IdParent");
                return _idparent;
            }
            set
            {
                _idparent = value;
                NotifyPropertyChanged("IdParent");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 314 Связь судебного дела и объекта (SUD_SUDLINK)
    /// </summary>
    [RegisterInfo(RegisterID = 314)]
    [Serializable]
    public partial class OMSudLink : OMBaseClass<OMSudLink>
    {

        private long _id;
        /// <summary>
        /// 31400100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 31400100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _idobject;
        /// <summary>
        /// 31400200 Идентификатор объекта (ID_OBJECT)
        /// </summary>
        [RegisterAttribute(AttributeID = 31400200)]
        public long? IdObject
        {
            get
            {
                CheckPropertyInited("IdObject");
                return _idobject;
            }
            set
            {
                _idobject = value;
                NotifyPropertyChanged("IdObject");
            }
        }


        private long? _idsud;
        /// <summary>
        /// 31400300 Идентификатор судебного дела (ID_SUD)
        /// </summary>
        [RegisterAttribute(AttributeID = 31400300)]
        public long? IdSud
        {
            get
            {
                CheckPropertyInited("IdSud");
                return _idsud;
            }
            set
            {
                _idsud = value;
                NotifyPropertyChanged("IdSud");
            }
        }


        private string _use;
        /// <summary>
        /// 31400400 Пока не используется (USE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31400400)]
        public string Use
        {
            get
            {
                CheckPropertyInited("Use");
                return _use;
            }
            set
            {
                _use = value;
                NotifyPropertyChanged("Use");
            }
        }


        private decimal? _rs;
        /// <summary>
        /// 31400500 Рыночная стоимость по судебному решению (RS)
        /// </summary>
        [RegisterAttribute(AttributeID = 31400500)]
        public decimal? Rs
        {
            get
            {
                CheckPropertyInited("Rs");
                return _rs;
            }
            set
            {
                _rs = value;
                NotifyPropertyChanged("Rs");
            }
        }


        private decimal? _uprs;
        /// <summary>
        /// 31400600 Удельный показатель (UPRS)
        /// </summary>
        [RegisterAttribute(AttributeID = 31400600)]
        public decimal? Uprs
        {
            get
            {
                CheckPropertyInited("Uprs");
                return _uprs;
            }
            set
            {
                _uprs = value;
                NotifyPropertyChanged("Uprs");
            }
        }


        private string _descr;
        /// <summary>
        /// 31400700 Примечание (DESCR)
        /// </summary>
        [RegisterAttribute(AttributeID = 31400700)]
        public string Descr
        {
            get
            {
                CheckPropertyInited("Descr");
                return _descr;
            }
            set
            {
                _descr = value;
                NotifyPropertyChanged("Descr");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 315 Объект (SUD_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 315)]
    [Serializable]
    public partial class OMObject : OMBaseClass<OMObject>
    {

        private long _id;
        /// <summary>
        /// 31500100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 31500100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _kn;
        /// <summary>
        /// 31500200 Кадастровый номер объекта (KN)
        /// </summary>
        [RegisterAttribute(AttributeID = 31500200)]
        public string Kn
        {
            get
            {
                CheckPropertyInited("Kn");
                return _kn;
            }
            set
            {
                _kn = value;
                NotifyPropertyChanged("Kn");
            }
        }


        private DateTime? _date;
        /// <summary>
        /// 31500300 Дата определения стоимости (DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31500300)]
        public DateTime? Date
        {
            get
            {
                CheckPropertyInited("Date");
                return _date;
            }
            set
            {
                _date = value;
                NotifyPropertyChanged("Date");
            }
        }


        private decimal? _square;
        /// <summary>
        /// 31500400 Площадь, кв. (SQUARE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31500400)]
        public decimal? Square
        {
            get
            {
                CheckPropertyInited("Square");
                return _square;
            }
            set
            {
                _square = value;
                NotifyPropertyChanged("Square");
            }
        }


        private decimal? _kc;
        /// <summary>
        /// 31500500 Оспариваемая КС (KC)
        /// </summary>
        [RegisterAttribute(AttributeID = 31500500)]
        public decimal? Kc
        {
            get
            {
                CheckPropertyInited("Kc");
                return _kc;
            }
            set
            {
                _kc = value;
                NotifyPropertyChanged("Kc");
            }
        }


        private string _namecenter;
        /// <summary>
        /// 31500600 Наименование (ТЦ, БЦ) (NAME_CENTER)
        /// </summary>
        [RegisterAttribute(AttributeID = 31500600)]
        public string NameCenter
        {
            get
            {
                CheckPropertyInited("NameCenter");
                return _namecenter;
            }
            set
            {
                _namecenter = value;
                NotifyPropertyChanged("NameCenter");
            }
        }


        private string _statdgi;
        /// <summary>
        /// 31500700 Внесено в статистику ДГИ (STAT_DGI)
        /// </summary>
        [RegisterAttribute(AttributeID = 31500700)]
        public string StatDgi
        {
            get
            {
                CheckPropertyInited("StatDgi");
                return _statdgi;
            }
            set
            {
                _statdgi = value;
                NotifyPropertyChanged("StatDgi");
            }
        }


        private string _owner;
        /// <summary>
        /// 31500800 Заказчик / Административный истец (OWNER)
        /// </summary>
        [RegisterAttribute(AttributeID = 31500800)]
        public string Owner
        {
            get
            {
                CheckPropertyInited("Owner");
                return _owner;
            }
            set
            {
                _owner = value;
                NotifyPropertyChanged("Owner");
            }
        }


        private string _adres;
        /// <summary>
        /// 31500900 Адрес (ADRES)
        /// </summary>
        [RegisterAttribute(AttributeID = 31500900)]
        public string Adres
        {
            get
            {
                CheckPropertyInited("Adres");
                return _adres;
            }
            set
            {
                _adres = value;
                NotifyPropertyChanged("Adres");
            }
        }


        private string _typeobj;
        /// <summary>
        /// 31501000 Тип объекта ()
        /// </summary>
        [RegisterAttribute(AttributeID = 31501000)]
        public string Typeobj
        {
            get
            {
                CheckPropertyInited("Typeobj");
                return _typeobj;
            }
            set
            {
                _typeobj = value;
                NotifyPropertyChanged("Typeobj");
            }
        }


        private ObjectModel.Directory.Sud.SudObjectType _typeobj_Code;
        /// <summary>
        /// 31501000 Тип объекта (справочный код) (TYPEOBJ)
        /// </summary>
        [RegisterAttribute(AttributeID = 31501000)]
        public ObjectModel.Directory.Sud.SudObjectType Typeobj_Code
        {
            get
            {
                CheckPropertyInited("Typeobj_Code");
                return this._typeobj_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typeobj))
                    {
                         _typeobj = descr;
                    }
                }
                else
                {
                     _typeobj = descr;
                }

                this._typeobj_Code = value;
                NotifyPropertyChanged("Typeobj");
                NotifyPropertyChanged("Typeobj_Code");
            }
        }


        private string _workstat;
        /// <summary>
        /// 31501100 Статус обработки ()
        /// </summary>
        [RegisterAttribute(AttributeID = 31501100)]
        public string Workstat
        {
            get
            {
                CheckPropertyInited("Workstat");
                return _workstat;
            }
            set
            {
                _workstat = value;
                NotifyPropertyChanged("Workstat");
            }
        }


        private ObjectModel.Directory.Sud.ProcessingStatus _workstat_Code;
        /// <summary>
        /// 31501100 Статус обработки (справочный код) (WORKSTAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 31501100)]
        public ObjectModel.Directory.Sud.ProcessingStatus Workstat_Code
        {
            get
            {
                CheckPropertyInited("Workstat_Code");
                return this._workstat_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_workstat))
                    {
                         _workstat = descr;
                    }
                }
                else
                {
                     _workstat = descr;
                }

                this._workstat_Code = value;
                NotifyPropertyChanged("Workstat");
                NotifyPropertyChanged("Workstat_Code");
            }
        }


        private bool _approvestatus;
        /// <summary>
        /// 31501200 Утверждено ()
        /// </summary>
        [RegisterAttribute(AttributeID = 31501200)]
        public bool ApproveStatus
        {
            get
            {
                CheckPropertyInited("ApproveStatus");
                return _approvestatus;
            }
            set
            {
                _approvestatus = value;
                NotifyPropertyChanged("ApproveStatus");
            }
        }


        private string _applicanttype;
        /// <summary>
        /// 31501300 Тип заявителя ()
        /// </summary>
        [RegisterAttribute(AttributeID = 31501300)]
        public string ApplicantType
        {
            get
            {
                CheckPropertyInited("ApplicantType");
                return _applicanttype;
            }
            set
            {
                _applicanttype = value;
                NotifyPropertyChanged("ApplicantType");
            }
        }


        private ObjectModel.Directory.Sud.ApplicantType _applicanttype_Code;
        /// <summary>
        /// 31501300 Тип заявителя (справочный код) (APPLICANTTYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31501300)]
        public ObjectModel.Directory.Sud.ApplicantType ApplicantType_Code
        {
            get
            {
                CheckPropertyInited("ApplicantType_Code");
                return this._applicanttype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_applicanttype))
                    {
                         _applicanttype = descr;
                    }
                }
                else
                {
                     _applicanttype = descr;
                }

                this._applicanttype_Code = value;
                NotifyPropertyChanged("ApplicantType");
                NotifyPropertyChanged("ApplicantType_Code");
            }
        }


        private string _typeofownership;
        /// <summary>
        /// 31501400 Форма собственности ()
        /// </summary>
        [RegisterAttribute(AttributeID = 31501400)]
        public string TypeOfOwnership
        {
            get
            {
                CheckPropertyInited("TypeOfOwnership");
                return _typeofownership;
            }
            set
            {
                _typeofownership = value;
                NotifyPropertyChanged("TypeOfOwnership");
            }
        }


        private ObjectModel.Directory.Sud.TypeOfOwnership _typeofownership_Code;
        /// <summary>
        /// 31501400 Форма собственности (справочный код) (TYPEOFOWNERSHIP)
        /// </summary>
        [RegisterAttribute(AttributeID = 31501400)]
        public ObjectModel.Directory.Sud.TypeOfOwnership TypeOfOwnership_Code
        {
            get
            {
                CheckPropertyInited("TypeOfOwnership_Code");
                return this._typeofownership_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typeofownership))
                    {
                         _typeofownership = descr;
                    }
                }
                else
                {
                     _typeofownership = descr;
                }

                this._typeofownership_Code = value;
                NotifyPropertyChanged("TypeOfOwnership");
                NotifyPropertyChanged("TypeOfOwnership_Code");
            }
        }


        private long? _exception;
        /// <summary>
        /// 31501500 Исключение (EXCEPTION)
        /// </summary>
        [RegisterAttribute(AttributeID = 31501500)]
        public long? Exception
        {
            get
            {
                CheckPropertyInited("Exception");
                return _exception;
            }
            set
            {
                _exception = value;
                NotifyPropertyChanged("Exception");
            }
        }


        private long? _additionalanalysis;
        /// <summary>
        /// 31501600 Дополнительный анализ (ADDITIONAL_ANALYSIS)
        /// </summary>
        [RegisterAttribute(AttributeID = 31501600)]
        public long? AdditionalAnalysis
        {
            get
            {
                CheckPropertyInited("AdditionalAnalysis");
                return _additionalanalysis;
            }
            set
            {
                _additionalanalysis = value;
                NotifyPropertyChanged("AdditionalAnalysis");
            }
        }


        private long? _issatisfied;
        /// <summary>
        /// 31501700 Статус удовлетворения объекта (IS_SATISFIED)
        /// </summary>
        [RegisterAttribute(AttributeID = 31501700)]
        public long? IsSatisfied
        {
            get
            {
                CheckPropertyInited("IsSatisfied");
                return _issatisfied;
            }
            set
            {
                _issatisfied = value;
                NotifyPropertyChanged("IsSatisfied");
            }
        }


        private long? _changeuser;
        /// <summary>
        /// 31501800 Последний пользователь вносивший изменения (CHANGE_USER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 31501800)]
        public long? ChangeUser
        {
            get
            {
                CheckPropertyInited("ChangeUser");
                return _changeuser;
            }
            set
            {
                _changeuser = value;
                NotifyPropertyChanged("ChangeUser");
            }
        }


        private DateTime? _changedate;
        /// <summary>
        /// 31501900 Дата последнего изменения (CHANGE_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31501900)]
        public DateTime? ChangeDate
        {
            get
            {
                CheckPropertyInited("ChangeDate");
                return _changedate;
            }
            set
            {
                _changedate = value;
                NotifyPropertyChanged("ChangeDate");
            }
        }


        private bool? _isremoved;
        /// <summary>
        /// 31502000 Признак удаленного объекта (IS_REMOVED)
        /// </summary>
        [RegisterAttribute(AttributeID = 31502000)]
        public bool? IsRemoved
        {
            get
            {
                CheckPropertyInited("IsRemoved");
                return _isremoved;
            }
            set
            {
                _isremoved = value;
                NotifyPropertyChanged("IsRemoved");
            }
        }


        private string _reasonforremove;
        /// <summary>
        /// 31502100 Причина удаления (REASON_FOR_REMOVE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31502100)]
        public string ReasonForRemove
        {
            get
            {
                CheckPropertyInited("ReasonForRemove");
                return _reasonforremove;
            }
            set
            {
                _reasonforremove = value;
                NotifyPropertyChanged("ReasonForRemove");
            }
        }


        private bool _containsattachments;
        /// <summary>
        /// 31502200 Признак наличия образа ()
        /// </summary>
        [RegisterAttribute(AttributeID = 31502200)]
        public bool ContainsAttachments
        {
            get
            {
                CheckPropertyInited("ContainsAttachments");
                return _containsattachments;
            }
            set
            {
                _containsattachments = value;
                NotifyPropertyChanged("ContainsAttachments");
            }
        }


        private bool? _isdecisionenteredintoforce;
        /// <summary>
        /// 31502300 Решение вступило в законную силу (IS_DECISION_ENTERED_INTO_FORCE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31502300)]
        public bool? IsDecisionEnteredIntoForce
        {
            get
            {
                CheckPropertyInited("IsDecisionEnteredIntoForce");
                return _isdecisionenteredintoforce;
            }
            set
            {
                _isdecisionenteredintoforce = value;
                NotifyPropertyChanged("IsDecisionEnteredIntoForce");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 316 Судебное заседание (SUD_SUD)
    /// </summary>
    [RegisterInfo(RegisterID = 316)]
    [Serializable]
    public partial class OMSud : OMBaseClass<OMSud>
    {

        private long _id;
        /// <summary>
        /// 31600100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 31600100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _name;
        /// <summary>
        /// 31600200 Наименование суда (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 31600200)]
        public string Name
        {
            get
            {
                CheckPropertyInited("Name");
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }


        private string _number;
        /// <summary>
        /// 31600300 Номер судебного дела (NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 31600300)]
        public string Number
        {
            get
            {
                CheckPropertyInited("Number");
                return _number;
            }
            set
            {
                _number = value;
                NotifyPropertyChanged("Number");
            }
        }


        private DateTime? _date;
        /// <summary>
        /// 31600400 Дата заседания (DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31600400)]
        public DateTime? Date
        {
            get
            {
                CheckPropertyInited("Date");
                return _date;
            }
            set
            {
                _date = value;
                NotifyPropertyChanged("Date");
            }
        }


        private DateTime? _suddate;
        /// <summary>
        /// 31600500 Дата судебного акта (SUD_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31600500)]
        public DateTime? SudDate
        {
            get
            {
                CheckPropertyInited("SudDate");
                return _suddate;
            }
            set
            {
                _suddate = value;
                NotifyPropertyChanged("SudDate");
            }
        }


        private string _status;
        /// <summary>
        /// 31600600 Статус дела ()
        /// </summary>
        [RegisterAttribute(AttributeID = 31600600)]
        public string Status
        {
            get
            {
                CheckPropertyInited("Status");
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }


        private ObjectModel.Directory.Sud.CourtStatus _status_Code;
        /// <summary>
        /// 31600600 Статус дела (справочный код) (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 31600600)]
        public ObjectModel.Directory.Sud.CourtStatus Status_Code
        {
            get
            {
                CheckPropertyInited("Status_Code");
                return this._status_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_status))
                    {
                         _status = descr;
                    }
                }
                else
                {
                     _status = descr;
                }

                this._status_Code = value;
                NotifyPropertyChanged("Status");
                NotifyPropertyChanged("Status_Code");
            }
        }


        private bool _containsattachments;
        /// <summary>
        /// 31600700 Признак наличия образа ()
        /// </summary>
        [RegisterAttribute(AttributeID = 31600700)]
        public bool ContainsAttachments
        {
            get
            {
                CheckPropertyInited("ContainsAttachments");
                return _containsattachments;
            }
            set
            {
                _containsattachments = value;
                NotifyPropertyChanged("ContainsAttachments");
            }
        }


        private string _archivenumber;
        /// <summary>
        /// 31600800 Номер архивный (ARCHIVE_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 31600800)]
        public string ArchiveNumber
        {
            get
            {
                CheckPropertyInited("ArchiveNumber");
                return _archivenumber;
            }
            set
            {
                _archivenumber = value;
                NotifyPropertyChanged("ArchiveNumber");
            }
        }


        private string _appealnumber;
        /// <summary>
        /// 31600900 Номер апелляции (APPEAL_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 31600900)]
        public string AppealNumber
        {
            get
            {
                CheckPropertyInited("AppealNumber");
                return _appealnumber;
            }
            set
            {
                _appealnumber = value;
                NotifyPropertyChanged("AppealNumber");
            }
        }


        private DateTime? _appealdate;
        /// <summary>
        /// 31601000 Дата определения (апелляция) (APPEAL_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31601000)]
        public DateTime? AppealDate
        {
            get
            {
                CheckPropertyInited("AppealDate");
                return _appealdate;
            }
            set
            {
                _appealdate = value;
                NotifyPropertyChanged("AppealDate");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 317 Варианты значений полей со статусами (SUD_PARAM)
    /// </summary>
    [RegisterInfo(RegisterID = 317)]
    [Serializable]
    public partial class OMParam : OMBaseClass<OMParam>
    {

        private long _pid;
        /// <summary>
        /// 31700100 Идентификатор (PID)
        /// </summary>
        [PrimaryKey(AttributeID = 31700100)]
        public long Pid
        {
            get
            {
                CheckPropertyInited("Pid");
                return _pid;
            }
            set
            {
                _pid = value;
                NotifyPropertyChanged("Pid");
            }
        }


        private long _idtable;
        /// <summary>
        /// 31700200 Идентификатор таблицы (ID_TABLE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31700200)]
        public long IdTable
        {
            get
            {
                CheckPropertyInited("IdTable");
                return _idtable;
            }
            set
            {
                _idtable = value;
                NotifyPropertyChanged("IdTable");
            }
        }


        private long _id;
        /// <summary>
        /// 31700300 Идентификатор записи в таблице (ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 31700300)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _paramname;
        /// <summary>
        /// 31700400 Имя поля в таблице (PARAM_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 31700400)]
        public string ParamName
        {
            get
            {
                CheckPropertyInited("ParamName");
                return _paramname;
            }
            set
            {
                _paramname = value;
                NotifyPropertyChanged("ParamName");
            }
        }


        private DateTime? _paramdate;
        /// <summary>
        /// 31700500 Значение в формате даты (PARAM_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31700500)]
        public DateTime? ParamDate
        {
            get
            {
                CheckPropertyInited("ParamDate");
                return _paramdate;
            }
            set
            {
                _paramdate = value;
                NotifyPropertyChanged("ParamDate");
            }
        }


        private decimal? _paramdouble;
        /// <summary>
        /// 31700600 Значение в формате числа (PARAM_DOUBLE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31700600)]
        public decimal? ParamDouble
        {
            get
            {
                CheckPropertyInited("ParamDouble");
                return _paramdouble;
            }
            set
            {
                _paramdouble = value;
                NotifyPropertyChanged("ParamDouble");
            }
        }


        private string _paramchar;
        /// <summary>
        /// 31700700 Значение в формате текста (PARAM_CHAR)
        /// </summary>
        [RegisterAttribute(AttributeID = 31700700)]
        public string ParamChar
        {
            get
            {
                CheckPropertyInited("ParamChar");
                return _paramchar;
            }
            set
            {
                _paramchar = value;
                NotifyPropertyChanged("ParamChar");
            }
        }


        private long? _paramint;
        /// <summary>
        /// 31700800 Значение в формате целого (PARAM_INT)
        /// </summary>
        [RegisterAttribute(AttributeID = 31700800)]
        public long? ParamInt
        {
            get
            {
                CheckPropertyInited("ParamInt");
                return _paramint;
            }
            set
            {
                _paramint = value;
                NotifyPropertyChanged("ParamInt");
            }
        }


        private long _iduser;
        /// <summary>
        /// 31700900 Идентификатор пользователя (ID_USER)
        /// </summary>
        [RegisterAttribute(AttributeID = 31700900)]
        public long IdUser
        {
            get
            {
                CheckPropertyInited("IdUser");
                return _iduser;
            }
            set
            {
                _iduser = value;
                NotifyPropertyChanged("IdUser");
            }
        }


        private DateTime _dateuser;
        /// <summary>
        /// 31701000 Дата изменения (DATE_USER)
        /// </summary>
        [RegisterAttribute(AttributeID = 31701000)]
        public DateTime DateUser
        {
            get
            {
                CheckPropertyInited("DateUser");
                return _dateuser;
            }
            set
            {
                _dateuser = value;
                NotifyPropertyChanged("DateUser");
            }
        }


        private string _paramstatus;
        /// <summary>
        /// 31701100 Статус параметра ()
        /// </summary>
        [RegisterAttribute(AttributeID = 31701100)]
        public string ParamStatus
        {
            get
            {
                CheckPropertyInited("ParamStatus");
                return _paramstatus;
            }
            set
            {
                _paramstatus = value;
                NotifyPropertyChanged("ParamStatus");
            }
        }


        private ObjectModel.Directory.Sud.ProcessingStatus _paramstatus_Code;
        /// <summary>
        /// 31701100 Статус параметра (справочный код) (PARAM_STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 31701100)]
        public ObjectModel.Directory.Sud.ProcessingStatus ParamStatus_Code
        {
            get
            {
                CheckPropertyInited("ParamStatus_Code");
                return this._paramstatus_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_paramstatus))
                    {
                         _paramstatus = descr;
                    }
                }
                else
                {
                     _paramstatus = descr;
                }

                this._paramstatus_Code = value;
                NotifyPropertyChanged("ParamStatus");
                NotifyPropertyChanged("ParamStatus_Code");
            }
        }

    }
}

namespace ObjectModel.Sud
{
    /// <summary>
    /// 318 Результаты выполнения процесса дополнительного анализа (SUD_DOPANALIZ_LOG)
    /// </summary>
    [RegisterInfo(RegisterID = 318)]
    [Serializable]
    public partial class OMDopAnalisLog : OMBaseClass<OMDopAnalisLog>
    {

        private long _id;
        /// <summary>
        /// 31800100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 31800100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _idobject;
        /// <summary>
        /// 31800200 Ид объекта (ID_OBJECT)
        /// </summary>
        [RegisterAttribute(AttributeID = 31800200)]
        public long IdObject
        {
            get
            {
                CheckPropertyInited("IdObject");
                return _idobject;
            }
            set
            {
                _idobject = value;
                NotifyPropertyChanged("IdObject");
            }
        }


        private string _kn;
        /// <summary>
        /// 31800300 Кадастровый номер объекта (KN)
        /// </summary>
        [RegisterAttribute(AttributeID = 31800300)]
        public string Kn
        {
            get
            {
                CheckPropertyInited("Kn");
                return _kn;
            }
            set
            {
                _kn = value;
                NotifyPropertyChanged("Kn");
            }
        }


        private string _address;
        /// <summary>
        /// 31800400 Адрес объекта (ADDRESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 31800400)]
        public string Address
        {
            get
            {
                CheckPropertyInited("Address");
                return _address;
            }
            set
            {
                _address = value;
                NotifyPropertyChanged("Address");
            }
        }


        private DateTime? _datedefinition;
        /// <summary>
        /// 31800500 Дата определения (DATE_DEFINITION)
        /// </summary>
        [RegisterAttribute(AttributeID = 31800500)]
        public DateTime? DateDefinition
        {
            get
            {
                CheckPropertyInited("DateDefinition");
                return _datedefinition;
            }
            set
            {
                _datedefinition = value;
                NotifyPropertyChanged("DateDefinition");
            }
        }


        private decimal? _kc;
        /// <summary>
        /// 31800600 Оспариваемая КС (KC)
        /// </summary>
        [RegisterAttribute(AttributeID = 31800600)]
        public decimal? Kc
        {
            get
            {
                CheckPropertyInited("Kc");
                return _kc;
            }
            set
            {
                _kc = value;
                NotifyPropertyChanged("Kc");
            }
        }


        private string _sudnumber;
        /// <summary>
        /// 31800700 Номер судебного дела (SUD_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 31800700)]
        public string SudNumber
        {
            get
            {
                CheckPropertyInited("SudNumber");
                return _sudnumber;
            }
            set
            {
                _sudnumber = value;
                NotifyPropertyChanged("SudNumber");
            }
        }


        private long _parametercase;
        /// <summary>
        /// 31800800 Признак по которому был установлен параметр доп анализа (PARAMETER_CASE)
        /// </summary>
        [RegisterAttribute(AttributeID = 31800800)]
        public long ParameterCase
        {
            get
            {
                CheckPropertyInited("ParameterCase");
                return _parametercase;
            }
            set
            {
                _parametercase = value;
                NotifyPropertyChanged("ParameterCase");
            }
        }


        private long _idprocess;
        /// <summary>
        /// 31800900 Уникальные номер процесса, который запустил процедуру доп анализа (ID_PROCESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 31800900)]
        public long IdProcess
        {
            get
            {
                CheckPropertyInited("IdProcess");
                return _idprocess;
            }
            set
            {
                _idprocess = value;
                NotifyPropertyChanged("IdProcess");
            }
        }


        private string _typeobj;
        /// <summary>
        /// 31801000 Тип объекта ()
        /// </summary>
        [RegisterAttribute(AttributeID = 31801000)]
        public string TypeObj
        {
            get
            {
                CheckPropertyInited("TypeObj");
                return _typeobj;
            }
            set
            {
                _typeobj = value;
                NotifyPropertyChanged("TypeObj");
            }
        }


        private ObjectModel.Directory.Sud.SudObjectType _typeobj_Code;
        /// <summary>
        /// 31801000 Тип объекта (справочный код) (TYPEOBJ)
        /// </summary>
        [RegisterAttribute(AttributeID = 31801000)]
        public ObjectModel.Directory.Sud.SudObjectType TypeObj_Code
        {
            get
            {
                CheckPropertyInited("TypeObj_Code");
                return this._typeobj_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typeobj))
                    {
                         _typeobj = descr;
                    }
                }
                else
                {
                     _typeobj = descr;
                }

                this._typeobj_Code = value;
                NotifyPropertyChanged("TypeObj");
                NotifyPropertyChanged("TypeObj_Code");
            }
        }

    }
}

namespace ObjectModel.Commission
{
    /// <summary>
    /// 400 Решение комиссий (COMISSION_COST)
    /// </summary>
    [RegisterInfo(RegisterID = 400)]
    [Serializable]
    public partial class OMCost : OMBaseClass<OMCost>
    {

        private long _id;
        /// <summary>
        /// 40000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 40000100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _commissiontype;
        /// <summary>
        /// 40000200 Тип комиссии ()
        /// </summary>
        [RegisterAttribute(AttributeID = 40000200)]
        public string CommissionType
        {
            get
            {
                CheckPropertyInited("CommissionType");
                return _commissiontype;
            }
            set
            {
                _commissiontype = value;
                NotifyPropertyChanged("CommissionType");
            }
        }


        private ObjectModel.Directory.Commission.CommissionType _commissiontype_Code;
        /// <summary>
        /// 40000200 Тип комиссии (справочный код) (TYPE_COMMISSION)
        /// </summary>
        [RegisterAttribute(AttributeID = 40000200)]
        public ObjectModel.Directory.Commission.CommissionType CommissionType_Code
        {
            get
            {
                CheckPropertyInited("CommissionType_Code");
                return this._commissiontype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_commissiontype))
                    {
                         _commissiontype = descr;
                    }
                }
                else
                {
                     _commissiontype = descr;
                }

                this._commissiontype_Code = value;
                NotifyPropertyChanged("CommissionType");
                NotifyPropertyChanged("CommissionType_Code");
            }
        }


        private string _kn;
        /// <summary>
        /// 40000300 Кадастровый номер объекта (KN_OBJECT)
        /// </summary>
        [RegisterAttribute(AttributeID = 40000300)]
        public string Kn
        {
            get
            {
                CheckPropertyInited("Kn");
                return _kn;
            }
            set
            {
                _kn = value;
                NotifyPropertyChanged("Kn");
            }
        }


        private decimal? _kc;
        /// <summary>
        /// 40000400 Оспариваемая кадастровая стоимость (KC_OBJECT)
        /// </summary>
        [RegisterAttribute(AttributeID = 40000400)]
        public decimal? Kc
        {
            get
            {
                CheckPropertyInited("Kc");
                return _kc;
            }
            set
            {
                _kc = value;
                NotifyPropertyChanged("Kc");
            }
        }


        private DateTime? _datekc;
        /// <summary>
        /// 40000500 Дата определения кадастровой стоимости (DATE_KC)
        /// </summary>
        [RegisterAttribute(AttributeID = 40000500)]
        public DateTime? DateKc
        {
            get
            {
                CheckPropertyInited("DateKc");
                return _datekc;
            }
            set
            {
                _datekc = value;
                NotifyPropertyChanged("DateKc");
            }
        }


        private string _statementnumber;
        /// <summary>
        /// 40000600 Номер заявления (NUM_STATEMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 40000600)]
        public string StatementNumber
        {
            get
            {
                CheckPropertyInited("StatementNumber");
                return _statementnumber;
            }
            set
            {
                _statementnumber = value;
                NotifyPropertyChanged("StatementNumber");
            }
        }


        private DateTime? _statementdate;
        /// <summary>
        /// 40000700 Дата заявления (DATE_STATEMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 40000700)]
        public DateTime? StatementDate
        {
            get
            {
                CheckPropertyInited("StatementDate");
                return _statementdate;
            }
            set
            {
                _statementdate = value;
                NotifyPropertyChanged("StatementDate");
            }
        }


        private string _applicantstatus;
        /// <summary>
        /// 40000800 Статус заявителя ()
        /// </summary>
        [RegisterAttribute(AttributeID = 40000800)]
        public string ApplicantStatus
        {
            get
            {
                CheckPropertyInited("ApplicantStatus");
                return _applicantstatus;
            }
            set
            {
                _applicantstatus = value;
                NotifyPropertyChanged("ApplicantStatus");
            }
        }


        private ObjectModel.Directory.Commission.ApplicantStatus _applicantstatus_Code;
        /// <summary>
        /// 40000800 Статус заявителя (справочный код) (STATUS_APPLICANT)
        /// </summary>
        [RegisterAttribute(AttributeID = 40000800)]
        public ObjectModel.Directory.Commission.ApplicantStatus ApplicantStatus_Code
        {
            get
            {
                CheckPropertyInited("ApplicantStatus_Code");
                return this._applicantstatus_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_applicantstatus))
                    {
                         _applicantstatus = descr;
                    }
                }
                else
                {
                     _applicantstatus = descr;
                }

                this._applicantstatus_Code = value;
                NotifyPropertyChanged("ApplicantStatus");
                NotifyPropertyChanged("ApplicantStatus_Code");
            }
        }


        private string _decisionnumber;
        /// <summary>
        /// 40000900 Номер решения (NUM_DECISION)
        /// </summary>
        [RegisterAttribute(AttributeID = 40000900)]
        public string DecisionNumber
        {
            get
            {
                CheckPropertyInited("DecisionNumber");
                return _decisionnumber;
            }
            set
            {
                _decisionnumber = value;
                NotifyPropertyChanged("DecisionNumber");
            }
        }


        private DateTime? _decisiondate;
        /// <summary>
        /// 40001000 Дата решения (DATE_DECISION)
        /// </summary>
        [RegisterAttribute(AttributeID = 40001000)]
        public DateTime? DecisionDate
        {
            get
            {
                CheckPropertyInited("DecisionDate");
                return _decisiondate;
            }
            set
            {
                _decisiondate = value;
                NotifyPropertyChanged("DecisionDate");
            }
        }


        private string _decisionresult;
        /// <summary>
        /// 40001100 Решение комиссии ()
        /// </summary>
        [RegisterAttribute(AttributeID = 40001100)]
        public string DecisionResult
        {
            get
            {
                CheckPropertyInited("DecisionResult");
                return _decisionresult;
            }
            set
            {
                _decisionresult = value;
                NotifyPropertyChanged("DecisionResult");
            }
        }


        private ObjectModel.Directory.Commission.DecisionResult _decisionresult_Code;
        /// <summary>
        /// 40001100 Решение комиссии (справочный код) (RESULT_DECISION)
        /// </summary>
        [RegisterAttribute(AttributeID = 40001100)]
        public ObjectModel.Directory.Commission.DecisionResult DecisionResult_Code
        {
            get
            {
                CheckPropertyInited("DecisionResult_Code");
                return this._decisionresult_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_decisionresult))
                    {
                         _decisionresult = descr;
                    }
                }
                else
                {
                     _decisionresult = descr;
                }

                this._decisionresult_Code = value;
                NotifyPropertyChanged("DecisionResult");
                NotifyPropertyChanged("DecisionResult_Code");
            }
        }


        private decimal? _marketvalue;
        /// <summary>
        /// 40001200 Рыночная стоимость после оспаривания (MARKET_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40001200)]
        public decimal? MarketValue
        {
            get
            {
                CheckPropertyInited("MarketValue");
                return _marketvalue;
            }
            set
            {
                _marketvalue = value;
                NotifyPropertyChanged("MarketValue");
            }
        }


        private decimal? _commissionkc;
        /// <summary>
        /// 40001300 Кадастровая стоимость по решению (KC_COMMISSION)
        /// </summary>
        [RegisterAttribute(AttributeID = 40001300)]
        public decimal? CommissionKc
        {
            get
            {
                CheckPropertyInited("CommissionKc");
                return _commissionkc;
            }
            set
            {
                _commissionkc = value;
                NotifyPropertyChanged("CommissionKc");
            }
        }


        private string _commissiongroup;
        /// <summary>
        /// 40001400 Группа после оспаривания (GROUP_COMMISSION)
        /// </summary>
        [RegisterAttribute(AttributeID = 40001400)]
        public string CommissionGroup
        {
            get
            {
                CheckPropertyInited("CommissionGroup");
                return _commissiongroup;
            }
            set
            {
                _commissiongroup = value;
                NotifyPropertyChanged("CommissionGroup");
            }
        }


        private string _commissionchange;
        /// <summary>
        /// 40001500 Изменения характеристик (CHANGE_COMMISSION)
        /// </summary>
        [RegisterAttribute(AttributeID = 40001500)]
        public string CommissionChange
        {
            get
            {
                CheckPropertyInited("CommissionChange");
                return _commissionchange;
            }
            set
            {
                _commissionchange = value;
                NotifyPropertyChanged("CommissionChange");
            }
        }

    }
}

namespace ObjectModel.Declarations
{
    /// <summary>
    /// 500 Книги (DECLARATIONS_BOOK)
    /// </summary>
    [RegisterInfo(RegisterID = 500)]
    [Serializable]
    public partial class OMBook : OMBaseClass<OMBook>
    {

        private long _id;
        /// <summary>
        /// 50000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 50000100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _prefics;
        /// <summary>
        /// 50000200 Префикс (PREFICS)
        /// </summary>
        [RegisterAttribute(AttributeID = 50000200)]
        public string Prefics
        {
            get
            {
                CheckPropertyInited("Prefics");
                return _prefics;
            }
            set
            {
                _prefics = value;
                NotifyPropertyChanged("Prefics");
            }
        }


        private DateTime _datebegin;
        /// <summary>
        /// 50000300 Дата начала (DATE_BEGIN)
        /// </summary>
        [RegisterAttribute(AttributeID = 50000300)]
        public DateTime DateBegin
        {
            get
            {
                CheckPropertyInited("DateBegin");
                return _datebegin;
            }
            set
            {
                _datebegin = value;
                NotifyPropertyChanged("DateBegin");
            }
        }


        private DateTime _dateend;
        /// <summary>
        /// 50000400 Дата окончания (DATE_END)
        /// </summary>
        [RegisterAttribute(AttributeID = 50000400)]
        public DateTime DateEnd
        {
            get
            {
                CheckPropertyInited("DateEnd");
                return _dateend;
            }
            set
            {
                _dateend = value;
                NotifyPropertyChanged("DateEnd");
            }
        }


        private string _status;
        /// <summary>
        /// 50000500 Статус ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50000500)]
        public string Status
        {
            get
            {
                CheckPropertyInited("Status");
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }


        private ObjectModel.Directory.Declarations.BookStatus _status_Code;
        /// <summary>
        /// 50000500 Статус (справочный код) (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 50000500)]
        public ObjectModel.Directory.Declarations.BookStatus Status_Code
        {
            get
            {
                CheckPropertyInited("Status_Code");
                return this._status_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_status))
                    {
                         _status = descr;
                    }
                }
                else
                {
                     _status = descr;
                }

                this._status_Code = value;
                NotifyPropertyChanged("Status");
                NotifyPropertyChanged("Status_Code");
            }
        }


        private string _type;
        /// <summary>
        /// 50000600 Тип ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50000600)]
        public string Type
        {
            get
            {
                CheckPropertyInited("Type");
                return _type;
            }
            set
            {
                _type = value;
                NotifyPropertyChanged("Type");
            }
        }


        private ObjectModel.Directory.Declarations.BookType _type_Code;
        /// <summary>
        /// 50000600 Тип (справочный код) (TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 50000600)]
        public ObjectModel.Directory.Declarations.BookType Type_Code
        {
            get
            {
                CheckPropertyInited("Type_Code");
                return this._type_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_type))
                    {
                         _type = descr;
                    }
                }
                else
                {
                     _type = descr;
                }

                this._type_Code = value;
                NotifyPropertyChanged("Type");
                NotifyPropertyChanged("Type_Code");
            }
        }

    }
}

namespace ObjectModel.Declarations
{
    /// <summary>
    /// 501 Декларация (DECLARATIONS_DECLARATION)
    /// </summary>
    [RegisterInfo(RegisterID = 501)]
    [Serializable]
    public partial class OMDeclaration : OMBaseClass<OMDeclaration>
    {

        private long _id;
        /// <summary>
        /// 50100100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 50100100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _book_id;
        /// <summary>
        /// 50100200 Идентификатор книги (BOOK_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50100200)]
        public long Book_Id
        {
            get
            {
                CheckPropertyInited("Book_Id");
                return _book_id;
            }
            set
            {
                _book_id = value;
                NotifyPropertyChanged("Book_Id");
            }
        }


        private long? _owner_id;
        /// <summary>
        /// 50100300 Идентификатор заявителя (он же собственник) (OWNER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50100300)]
        public long? Owner_Id
        {
            get
            {
                CheckPropertyInited("Owner_Id");
                return _owner_id;
            }
            set
            {
                _owner_id = value;
                NotifyPropertyChanged("Owner_Id");
            }
        }


        private long? _agent_id;
        /// <summary>
        /// 50100400 Идентификатор представителя заявителя (AGENT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50100400)]
        public long? Agent_Id
        {
            get
            {
                CheckPropertyInited("Agent_Id");
                return _agent_id;
            }
            set
            {
                _agent_id = value;
                NotifyPropertyChanged("Agent_Id");
            }
        }


        private long? _userisp_id;
        /// <summary>
        /// 50100500 Идентификатор исполнителя (USER_ISP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50100500)]
        public long? UserIsp_Id
        {
            get
            {
                CheckPropertyInited("UserIsp_Id");
                return _userisp_id;
            }
            set
            {
                _userisp_id = value;
                NotifyPropertyChanged("UserIsp_Id");
            }
        }


        private long? _userreg_id;
        /// <summary>
        /// 50100600 Идентификатор регистратора (USER_REG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50100600)]
        public long? UserReg_Id
        {
            get
            {
                CheckPropertyInited("UserReg_Id");
                return _userreg_id;
            }
            set
            {
                _userreg_id = value;
                NotifyPropertyChanged("UserReg_Id");
            }
        }


        private string _numin;
        /// <summary>
        /// 50100700 Входящий номер (NUM_IN)
        /// </summary>
        [RegisterAttribute(AttributeID = 50100700)]
        public string NumIn
        {
            get
            {
                CheckPropertyInited("NumIn");
                return _numin;
            }
            set
            {
                _numin = value;
                NotifyPropertyChanged("NumIn");
            }
        }


        private DateTime? _datein;
        /// <summary>
        /// 50100800 Входящая дата ГБУ (DATE_IN)
        /// </summary>
        [RegisterAttribute(AttributeID = 50100800)]
        public DateTime? DateIn
        {
            get
            {
                CheckPropertyInited("DateIn");
                return _datein;
            }
            set
            {
                _datein = value;
                NotifyPropertyChanged("DateIn");
            }
        }


        private DateTime? _durationin;
        /// <summary>
        /// 50100900 Срок рассмотрения декларации (DURATION_IN)
        /// </summary>
        [RegisterAttribute(AttributeID = 50100900)]
        public DateTime? DurationIn
        {
            get
            {
                CheckPropertyInited("DurationIn");
                return _durationin;
            }
            set
            {
                _durationin = value;
                NotifyPropertyChanged("DurationIn");
            }
        }


        private string _status;
        /// <summary>
        /// 50101000 Статус декларации ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50101000)]
        public string Status
        {
            get
            {
                CheckPropertyInited("Status");
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }


        private ObjectModel.Directory.Declarations.StatusDec _status_Code;
        /// <summary>
        /// 50101000 Статус декларации (справочный код) (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 50101000)]
        public ObjectModel.Directory.Declarations.StatusDec Status_Code
        {
            get
            {
                CheckPropertyInited("Status_Code");
                return this._status_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_status))
                    {
                         _status = descr;
                    }
                }
                else
                {
                     _status = descr;
                }

                this._status_Code = value;
                NotifyPropertyChanged("Status");
                NotifyPropertyChanged("Status_Code");
            }
        }


        private string _cadastralnumobj;
        /// <summary>
        /// 50101100 Кадастровый номер объекта (CADASTRAL_NUM_OBJ)
        /// </summary>
        [RegisterAttribute(AttributeID = 50101100)]
        public string CadastralNumObj
        {
            get
            {
                CheckPropertyInited("CadastralNumObj");
                return _cadastralnumobj;
            }
            set
            {
                _cadastralnumobj = value;
                NotifyPropertyChanged("CadastralNumObj");
            }
        }


        private string _typeobj;
        /// <summary>
        /// 50101200 Тип объекта ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50101200)]
        public string TypeObj
        {
            get
            {
                CheckPropertyInited("TypeObj");
                return _typeobj;
            }
            set
            {
                _typeobj = value;
                NotifyPropertyChanged("TypeObj");
            }
        }


        private ObjectModel.Directory.Declarations.ObjectType _typeobj_Code;
        /// <summary>
        /// 50101200 Тип объекта (справочный код) (TYPE_OBJ)
        /// </summary>
        [RegisterAttribute(AttributeID = 50101200)]
        public ObjectModel.Directory.Declarations.ObjectType TypeObj_Code
        {
            get
            {
                CheckPropertyInited("TypeObj_Code");
                return this._typeobj_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typeobj))
                    {
                         _typeobj = descr;
                    }
                }
                else
                {
                     _typeobj = descr;
                }

                this._typeobj_Code = value;
                NotifyPropertyChanged("TypeObj");
                NotifyPropertyChanged("TypeObj_Code");
            }
        }


        private DateTime? _dateinisp;
        /// <summary>
        /// 50101300 Дата выдачи исполнителю (DATE_IN_ISP)
        /// </summary>
        [RegisterAttribute(AttributeID = 50101300)]
        public DateTime? DateInIsp
        {
            get
            {
                CheckPropertyInited("DateInIsp");
                return _dateinisp;
            }
            set
            {
                _dateinisp = value;
                NotifyPropertyChanged("DateInIsp");
            }
        }


        private DateTime? _dateinplan;
        /// <summary>
        /// 50101400 Плановая дата внесения (DATE_IN_PLAN)
        /// </summary>
        [RegisterAttribute(AttributeID = 50101400)]
        public DateTime? DateInPlan
        {
            get
            {
                CheckPropertyInited("DateInPlan");
                return _dateinplan;
            }
            set
            {
                _dateinplan = value;
                NotifyPropertyChanged("DateInPlan");
            }
        }


        private DateTime? _dateinfact;
        /// <summary>
        /// 50101500 Фактическая дата внесения (DATE_IN_FACT)
        /// </summary>
        [RegisterAttribute(AttributeID = 50101500)]
        public DateTime? DateInFact
        {
            get
            {
                CheckPropertyInited("DateInFact");
                return _dateinfact;
            }
            set
            {
                _dateinfact = value;
                NotifyPropertyChanged("DateInFact");
            }
        }


        private string _checkpoint1;
        /// <summary>
        /// 50101600 Проверка декларации на соответствии утвержденной форме Приказа № 846 от 27.12.2016 ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50101600)]
        public string CheckPoint1
        {
            get
            {
                CheckPropertyInited("CheckPoint1");
                return _checkpoint1;
            }
            set
            {
                _checkpoint1 = value;
                NotifyPropertyChanged("CheckPoint1");
            }
        }


        private ObjectModel.Directory.Declarations.CheckStatus _checkpoint1_Code;
        /// <summary>
        /// 50101600 Проверка декларации на соответствии утвержденной форме Приказа № 846 от 27.12.2016 (справочный код) (CHECK_POINT1)
        /// </summary>
        [RegisterAttribute(AttributeID = 50101600)]
        public ObjectModel.Directory.Declarations.CheckStatus CheckPoint1_Code
        {
            get
            {
                CheckPropertyInited("CheckPoint1_Code");
                return this._checkpoint1_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_checkpoint1))
                    {
                         _checkpoint1 = descr;
                    }
                }
                else
                {
                     _checkpoint1 = descr;
                }

                this._checkpoint1_Code = value;
                NotifyPropertyChanged("CheckPoint1");
                NotifyPropertyChanged("CheckPoint1_Code");
            }
        }


        private string _checkpoint2;
        /// <summary>
        /// 50101700 Проверка декларации на наличие основных данных, позволяющих идентифицировать объект ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50101700)]
        public string CheckPoint2
        {
            get
            {
                CheckPropertyInited("CheckPoint2");
                return _checkpoint2;
            }
            set
            {
                _checkpoint2 = value;
                NotifyPropertyChanged("CheckPoint2");
            }
        }


        private ObjectModel.Directory.Declarations.CheckStatus _checkpoint2_Code;
        /// <summary>
        /// 50101700 Проверка декларации на наличие основных данных, позволяющих идентифицировать объект (справочный код) (CHECK_POINT2)
        /// </summary>
        [RegisterAttribute(AttributeID = 50101700)]
        public ObjectModel.Directory.Declarations.CheckStatus CheckPoint2_Code
        {
            get
            {
                CheckPropertyInited("CheckPoint2_Code");
                return this._checkpoint2_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_checkpoint2))
                    {
                         _checkpoint2 = descr;
                    }
                }
                else
                {
                     _checkpoint2 = descr;
                }

                this._checkpoint2_Code = value;
                NotifyPropertyChanged("CheckPoint2");
                NotifyPropertyChanged("CheckPoint2_Code");
            }
        }


        private string _checkpoint3;
        /// <summary>
        /// 50101800 Уведомление о продлении сроков рассмотрения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50101800)]
        public string CheckPoint3
        {
            get
            {
                CheckPropertyInited("CheckPoint3");
                return _checkpoint3;
            }
            set
            {
                _checkpoint3 = value;
                NotifyPropertyChanged("CheckPoint3");
            }
        }


        private ObjectModel.Directory.Declarations.CheckStatus _checkpoint3_Code;
        /// <summary>
        /// 50101800 Уведомление о продлении сроков рассмотрения (справочный код) (CHECK_POINT3)
        /// </summary>
        [RegisterAttribute(AttributeID = 50101800)]
        public ObjectModel.Directory.Declarations.CheckStatus CheckPoint3_Code
        {
            get
            {
                CheckPropertyInited("CheckPoint3_Code");
                return this._checkpoint3_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_checkpoint3))
                    {
                         _checkpoint3 = descr;
                    }
                }
                else
                {
                     _checkpoint3 = descr;
                }

                this._checkpoint3_Code = value;
                NotifyPropertyChanged("CheckPoint3");
                NotifyPropertyChanged("CheckPoint3_Code");
            }
        }


        private string _checkpoint4;
        /// <summary>
        /// 50101900 Декларация, прошедшая формальную проверку, подлежит дальнейшей проверке ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50101900)]
        public string CheckPoint4
        {
            get
            {
                CheckPropertyInited("CheckPoint4");
                return _checkpoint4;
            }
            set
            {
                _checkpoint4 = value;
                NotifyPropertyChanged("CheckPoint4");
            }
        }


        private ObjectModel.Directory.Declarations.CheckStatus _checkpoint4_Code;
        /// <summary>
        /// 50101900 Декларация, прошедшая формальную проверку, подлежит дальнейшей проверке (справочный код) (CHECK_POINT4)
        /// </summary>
        [RegisterAttribute(AttributeID = 50101900)]
        public ObjectModel.Directory.Declarations.CheckStatus CheckPoint4_Code
        {
            get
            {
                CheckPropertyInited("CheckPoint4_Code");
                return this._checkpoint4_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_checkpoint4))
                    {
                         _checkpoint4 = descr;
                    }
                }
                else
                {
                     _checkpoint4 = descr;
                }

                this._checkpoint4_Code = value;
                NotifyPropertyChanged("CheckPoint4");
                NotifyPropertyChanged("CheckPoint4_Code");
            }
        }


        private DateTime? _datecheckplan;
        /// <summary>
        /// 50102000 Плановая дата рассмотрения (DATE_CHECK_PLAN)
        /// </summary>
        [RegisterAttribute(AttributeID = 50102000)]
        public DateTime? DateCheckPlan
        {
            get
            {
                CheckPropertyInited("DateCheckPlan");
                return _datecheckplan;
            }
            set
            {
                _datecheckplan = value;
                NotifyPropertyChanged("DateCheckPlan");
            }
        }


        private DateTime? _datecheckfact;
        /// <summary>
        /// 50102100 Фактическая дата рассмотрения (DATE_CHECK_FACT)
        /// </summary>
        [RegisterAttribute(AttributeID = 50102100)]
        public DateTime? DateCheckFact
        {
            get
            {
                CheckPropertyInited("DateCheckFact");
                return _datecheckfact;
            }
            set
            {
                _datecheckfact = value;
                NotifyPropertyChanged("DateCheckFact");
            }
        }


        private string _checkuved;
        /// <summary>
        /// 50102200 Подготовка уведомления ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50102200)]
        public string CheckUved
        {
            get
            {
                CheckPropertyInited("CheckUved");
                return _checkuved;
            }
            set
            {
                _checkuved = value;
                NotifyPropertyChanged("CheckUved");
            }
        }


        private ObjectModel.Directory.Declarations.CheckStatus _checkuved_Code;
        /// <summary>
        /// 50102200 Подготовка уведомления (справочный код) (CHECK_UVED)
        /// </summary>
        [RegisterAttribute(AttributeID = 50102200)]
        public ObjectModel.Directory.Declarations.CheckStatus CheckUved_Code
        {
            get
            {
                CheckPropertyInited("CheckUved_Code");
                return this._checkuved_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_checkuved))
                    {
                         _checkuved = descr;
                    }
                }
                else
                {
                     _checkuved = descr;
                }

                this._checkuved_Code = value;
                NotifyPropertyChanged("CheckUved");
                NotifyPropertyChanged("CheckUved_Code");
            }
        }


        private DateTime? _dateuvedplan;
        /// <summary>
        /// 50102300 Плановая дата уведомления (DATE_UVED_PLAN)
        /// </summary>
        [RegisterAttribute(AttributeID = 50102300)]
        public DateTime? DateUvedPlan
        {
            get
            {
                CheckPropertyInited("DateUvedPlan");
                return _dateuvedplan;
            }
            set
            {
                _dateuvedplan = value;
                NotifyPropertyChanged("DateUvedPlan");
            }
        }


        private DateTime? _dateuvedfact;
        /// <summary>
        /// 50102400 Фактическая дата уведомления (DATE_UVED_FACT)
        /// </summary>
        [RegisterAttribute(AttributeID = 50102400)]
        public DateTime? DateUvedFact
        {
            get
            {
                CheckPropertyInited("DateUvedFact");
                return _dateuvedfact;
            }
            set
            {
                _dateuvedfact = value;
                NotifyPropertyChanged("DateUvedFact");
            }
        }


        private DateTime? _dateend;
        /// <summary>
        /// 50102500 Фактическая дата завершения (DATE_END)
        /// </summary>
        [RegisterAttribute(AttributeID = 50102500)]
        public DateTime? DateEnd
        {
            get
            {
                CheckPropertyInited("DateEnd");
                return _dateend;
            }
            set
            {
                _dateend = value;
                NotifyPropertyChanged("DateEnd");
            }
        }


        private string _ownertype;
        /// <summary>
        /// 50102600 Податель декларации ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50102600)]
        public string OwnerType
        {
            get
            {
                CheckPropertyInited("OwnerType");
                return _ownertype;
            }
            set
            {
                _ownertype = value;
                NotifyPropertyChanged("OwnerType");
            }
        }


        private ObjectModel.Directory.Declarations.OwnerType _ownertype_Code;
        /// <summary>
        /// 50102600 Податель декларации (справочный код) (OWNER_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 50102600)]
        public ObjectModel.Directory.Declarations.OwnerType OwnerType_Code
        {
            get
            {
                CheckPropertyInited("OwnerType_Code");
                return this._ownertype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_ownertype))
                    {
                         _ownertype = descr;
                    }
                }
                else
                {
                     _ownertype = descr;
                }

                this._ownertype_Code = value;
                NotifyPropertyChanged("OwnerType");
                NotifyPropertyChanged("OwnerType_Code");
            }
        }


        private string _uvedtypeowner;
        /// <summary>
        /// 50102700 Тип уведомления заявителя ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50102700)]
        public string UvedTypeOwner
        {
            get
            {
                CheckPropertyInited("UvedTypeOwner");
                return _uvedtypeowner;
            }
            set
            {
                _uvedtypeowner = value;
                NotifyPropertyChanged("UvedTypeOwner");
            }
        }


        private ObjectModel.Directory.Declarations.SendUvedType _uvedtypeowner_Code;
        /// <summary>
        /// 50102700 Тип уведомления заявителя (справочный код) (UVED_TYPE_OWNER)
        /// </summary>
        [RegisterAttribute(AttributeID = 50102700)]
        public ObjectModel.Directory.Declarations.SendUvedType UvedTypeOwner_Code
        {
            get
            {
                CheckPropertyInited("UvedTypeOwner_Code");
                return this._uvedtypeowner_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_uvedtypeowner))
                    {
                         _uvedtypeowner = descr;
                    }
                }
                else
                {
                     _uvedtypeowner = descr;
                }

                this._uvedtypeowner_Code = value;
                NotifyPropertyChanged("UvedTypeOwner");
                NotifyPropertyChanged("UvedTypeOwner_Code");
            }
        }


        private string _uvedtypeagent;
        /// <summary>
        /// 50102800 Тип уведомления представителя заявителя ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50102800)]
        public string UvedTypeAgent
        {
            get
            {
                CheckPropertyInited("UvedTypeAgent");
                return _uvedtypeagent;
            }
            set
            {
                _uvedtypeagent = value;
                NotifyPropertyChanged("UvedTypeAgent");
            }
        }


        private ObjectModel.Directory.Declarations.SendUvedType _uvedtypeagent_Code;
        /// <summary>
        /// 50102800 Тип уведомления представителя заявителя (справочный код) (UVED_TYPE_AGENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 50102800)]
        public ObjectModel.Directory.Declarations.SendUvedType UvedTypeAgent_Code
        {
            get
            {
                CheckPropertyInited("UvedTypeAgent_Code");
                return this._uvedtypeagent_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_uvedtypeagent))
                    {
                         _uvedtypeagent = descr;
                    }
                }
                else
                {
                     _uvedtypeagent = descr;
                }

                this._uvedtypeagent_Code = value;
                NotifyPropertyChanged("UvedTypeAgent");
                NotifyPropertyChanged("UvedTypeAgent_Code");
            }
        }


        private string _certificatenum;
        /// <summary>
        /// 50102900 Номер документа, удостоверяющего полномочия  представителя заявителя  (CERTIFICATE_NUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 50102900)]
        public string CertificateNum
        {
            get
            {
                CheckPropertyInited("CertificateNum");
                return _certificatenum;
            }
            set
            {
                _certificatenum = value;
                NotifyPropertyChanged("CertificateNum");
            }
        }


        private DateTime? _certificatedate;
        /// <summary>
        /// 50103000 Дата документа, удостоверяющего полномочия представителя заявителя  (CERTIFICATE_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 50103000)]
        public DateTime? CertificateDate
        {
            get
            {
                CheckPropertyInited("CertificateDate");
                return _certificatedate;
            }
            set
            {
                _certificatedate = value;
                NotifyPropertyChanged("CertificateDate");
            }
        }


        private string _certificatename;
        /// <summary>
        /// 50103100 Название документа, удостоверяющего полномочия представителя заявителя  (CERTIFICATE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50103100)]
        public string CertificateName
        {
            get
            {
                CheckPropertyInited("CertificateName");
                return _certificatename;
            }
            set
            {
                _certificatename = value;
                NotifyPropertyChanged("CertificateName");
            }
        }


        private string _purposedec;
        /// <summary>
        /// 50103200 Цель предоставления декларации ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50103200)]
        public string PurposeDec
        {
            get
            {
                CheckPropertyInited("PurposeDec");
                return _purposedec;
            }
            set
            {
                _purposedec = value;
                NotifyPropertyChanged("PurposeDec");
            }
        }


        private ObjectModel.Directory.Declarations.DeclarationPurpose _purposedec_Code;
        /// <summary>
        /// 50103200 Цель предоставления декларации (справочный код) (PURPOSE_DEC)
        /// </summary>
        [RegisterAttribute(AttributeID = 50103200)]
        public ObjectModel.Directory.Declarations.DeclarationPurpose PurposeDec_Code
        {
            get
            {
                CheckPropertyInited("PurposeDec_Code");
                return this._purposedec_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_purposedec))
                    {
                         _purposedec = descr;
                    }
                }
                else
                {
                     _purposedec = descr;
                }

                this._purposedec_Code = value;
                NotifyPropertyChanged("PurposeDec");
                NotifyPropertyChanged("PurposeDec_Code");
            }
        }


        private DateTime? _checktime;
        /// <summary>
        /// 50103300 Контрольный срок (CHECK_TIME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50103300)]
        public DateTime? CheckTime
        {
            get
            {
                CheckPropertyInited("CheckTime");
                return _checktime;
            }
            set
            {
                _checktime = value;
                NotifyPropertyChanged("CheckTime");
            }
        }


        private long? _spdappid;
        /// <summary>
        /// 50103400 Идентификатор СПД (SPD_APP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50103400)]
        public long? SpdAppId
        {
            get
            {
                CheckPropertyInited("SpdAppId");
                return _spdappid;
            }
            set
            {
                _spdappid = value;
                NotifyPropertyChanged("SpdAppId");
            }
        }


        private string _spdappname;
        /// <summary>
        /// 50103500 Номер заявки СПД (SPD_APP_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50103500)]
        public string SpdAppName
        {
            get
            {
                CheckPropertyInited("SpdAppName");
                return _spdappname;
            }
            set
            {
                _spdappname = value;
                NotifyPropertyChanged("SpdAppName");
            }
        }


        private DateTime? _spdappdate;
        /// <summary>
        /// 50103600 Дата заявки СПД (SPD_APP_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 50103600)]
        public DateTime? SpdAppDate
        {
            get
            {
                CheckPropertyInited("SpdAppDate");
                return _spdappdate;
            }
            set
            {
                _spdappdate = value;
                NotifyPropertyChanged("SpdAppDate");
            }
        }

    }
}

namespace ObjectModel.Declarations
{
    /// <summary>
    /// 502 Характеристики ОКС (DECLARATIONS_HAR_OKS)
    /// </summary>
    [RegisterInfo(RegisterID = 502)]
    [Serializable]
    public partial class OMHarOKS : OMBaseClass<OMHarOKS>
    {

        private long _id;
        /// <summary>
        /// 50200100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 50200100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _declaration_id;
        /// <summary>
        /// 50200200 Идентификатор декларации (DECLARATION_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50200200)]
        public long? Declaration_Id
        {
            get
            {
                CheckPropertyInited("Declaration_Id");
                return _declaration_id;
            }
            set
            {
                _declaration_id = value;
                NotifyPropertyChanged("Declaration_Id");
            }
        }


        private string _har_1;
        /// <summary>
        /// 50200300 Вид объекта недвижимости  (HAR_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 50200300)]
        public string Har_1
        {
            get
            {
                CheckPropertyInited("Har_1");
                return _har_1;
            }
            set
            {
                _har_1 = value;
                NotifyPropertyChanged("Har_1");
            }
        }


        private string _har_2;
        /// <summary>
        /// 50200400 Адрес (описание местоположения)  (HAR_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 50200400)]
        public string Har_2
        {
            get
            {
                CheckPropertyInited("Har_2");
                return _har_2;
            }
            set
            {
                _har_2 = value;
                NotifyPropertyChanged("Har_2");
            }
        }


        private decimal? _har_3;
        /// <summary>
        /// 50200500 Площадь  (HAR_3)
        /// </summary>
        [RegisterAttribute(AttributeID = 50200500)]
        public decimal? Har_3
        {
            get
            {
                CheckPropertyInited("Har_3");
                return _har_3;
            }
            set
            {
                _har_3 = value;
                NotifyPropertyChanged("Har_3");
            }
        }


        private string _har_4;
        /// <summary>
        /// 50200600 Тип и значение основной характеристики сооружения  (HAR_4)
        /// </summary>
        [RegisterAttribute(AttributeID = 50200600)]
        public string Har_4
        {
            get
            {
                CheckPropertyInited("Har_4");
                return _har_4;
            }
            set
            {
                _har_4 = value;
                NotifyPropertyChanged("Har_4");
            }
        }


        private string _har_5;
        /// <summary>
        /// 50200700 Степень готовности объекта незавершенного строительства  (HAR_5)
        /// </summary>
        [RegisterAttribute(AttributeID = 50200700)]
        public string Har_5
        {
            get
            {
                CheckPropertyInited("Har_5");
                return _har_5;
            }
            set
            {
                _har_5 = value;
                NotifyPropertyChanged("Har_5");
            }
        }


        private string _har_6;
        /// <summary>
        /// 50200800 Проектируемый тип и значение основной характеристики объекта незавершенного строительства  (HAR_6)
        /// </summary>
        [RegisterAttribute(AttributeID = 50200800)]
        public string Har_6
        {
            get
            {
                CheckPropertyInited("Har_6");
                return _har_6;
            }
            set
            {
                _har_6 = value;
                NotifyPropertyChanged("Har_6");
            }
        }


        private string _har_7;
        /// <summary>
        /// 50200900 Проектируемое назначение здания, сооружения, строительство которых не завершено (для объектов незавершенного строительства) (HAR_7)
        /// </summary>
        [RegisterAttribute(AttributeID = 50200900)]
        public string Har_7
        {
            get
            {
                CheckPropertyInited("Har_7");
                return _har_7;
            }
            set
            {
                _har_7 = value;
                NotifyPropertyChanged("Har_7");
            }
        }


        private string _har_8;
        /// <summary>
        /// 50201000 Количество этажей  (HAR_8)
        /// </summary>
        [RegisterAttribute(AttributeID = 50201000)]
        public string Har_8
        {
            get
            {
                CheckPropertyInited("Har_8");
                return _har_8;
            }
            set
            {
                _har_8 = value;
                NotifyPropertyChanged("Har_8");
            }
        }


        private string _har_9;
        /// <summary>
        /// 50201100 Номер этажа здания или сооружения, на котором расположено помещение или машино-место (HAR_9)
        /// </summary>
        [RegisterAttribute(AttributeID = 50201100)]
        public string Har_9
        {
            get
            {
                CheckPropertyInited("Har_9");
                return _har_9;
            }
            set
            {
                _har_9 = value;
                NotifyPropertyChanged("Har_9");
            }
        }


        private string _har_10;
        /// <summary>
        /// 50201200 Материал наружных стен, если объектом недвижимости является здание (HAR_10)
        /// </summary>
        [RegisterAttribute(AttributeID = 50201200)]
        public string Har_10
        {
            get
            {
                CheckPropertyInited("Har_10");
                return _har_10;
            }
            set
            {
                _har_10 = value;
                NotifyPropertyChanged("Har_10");
            }
        }


        private string _har_11;
        /// <summary>
        /// 50201300 Материал основных несущих конструкций, перекрытий (HAR_11)
        /// </summary>
        [RegisterAttribute(AttributeID = 50201300)]
        public string Har_11
        {
            get
            {
                CheckPropertyInited("Har_11");
                return _har_11;
            }
            set
            {
                _har_11 = value;
                NotifyPropertyChanged("Har_11");
            }
        }


        private string _har_12;
        /// <summary>
        /// 50201400 Материал кровли (HAR_12)
        /// </summary>
        [RegisterAttribute(AttributeID = 50201400)]
        public string Har_12
        {
            get
            {
                CheckPropertyInited("Har_12");
                return _har_12;
            }
            set
            {
                _har_12 = value;
                NotifyPropertyChanged("Har_12");
            }
        }


        private DateTime? _har_13;
        /// <summary>
        /// 50201500 Год ввода в эксплуатацию объекта недвижимости  (HAR_13)
        /// </summary>
        [RegisterAttribute(AttributeID = 50201500)]
        public DateTime? Har_13
        {
            get
            {
                CheckPropertyInited("Har_13");
                return _har_13;
            }
            set
            {
                _har_13 = value;
                NotifyPropertyChanged("Har_13");
            }
        }


        private DateTime? _har_14;
        /// <summary>
        /// 50201600 Год завершения строительства объекта недвижимости  (HAR_14)
        /// </summary>
        [RegisterAttribute(AttributeID = 50201600)]
        public DateTime? Har_14
        {
            get
            {
                CheckPropertyInited("Har_14");
                return _har_14;
            }
            set
            {
                _har_14 = value;
                NotifyPropertyChanged("Har_14");
            }
        }


        private DateTime? _har_15;
        /// <summary>
        /// 50201700 Дата окончания проведения капитального ремонта (HAR_15)
        /// </summary>
        [RegisterAttribute(AttributeID = 50201700)]
        public DateTime? Har_15
        {
            get
            {
                CheckPropertyInited("Har_15");
                return _har_15;
            }
            set
            {
                _har_15 = value;
                NotifyPropertyChanged("Har_15");
            }
        }


        private DateTime? _har_16;
        /// <summary>
        /// 50201800 Дата окончания проведения реконструкции (HAR_16)
        /// </summary>
        [RegisterAttribute(AttributeID = 50201800)]
        public DateTime? Har_16
        {
            get
            {
                CheckPropertyInited("Har_16");
                return _har_16;
            }
            set
            {
                _har_16 = value;
                NotifyPropertyChanged("Har_16");
            }
        }


        private string _har_17;
        /// <summary>
        /// 50201900 Вид жилого помещения  (HAR_17)
        /// </summary>
        [RegisterAttribute(AttributeID = 50201900)]
        public string Har_17
        {
            get
            {
                CheckPropertyInited("Har_17");
                return _har_17;
            }
            set
            {
                _har_17 = value;
                NotifyPropertyChanged("Har_17");
            }
        }


        private string _har_18;
        /// <summary>
        /// 50202000 Вид или виды разрешенного использования  (HAR_18)
        /// </summary>
        [RegisterAttribute(AttributeID = 50202000)]
        public string Har_18
        {
            get
            {
                CheckPropertyInited("Har_18");
                return _har_18;
            }
            set
            {
                _har_18 = value;
                NotifyPropertyChanged("Har_18");
            }
        }


        private string _har_19;
        /// <summary>
        /// 50202100 Сведения о включении объекта недвижимости в единый государственный реестр объектов культурного наследия  (HAR_19)
        /// </summary>
        [RegisterAttribute(AttributeID = 50202100)]
        public string Har_19
        {
            get
            {
                CheckPropertyInited("Har_19");
                return _har_19;
            }
            set
            {
                _har_19 = value;
                NotifyPropertyChanged("Har_19");
            }
        }


        private string _har_20;
        /// <summary>
        /// 50202200 Физический износ  (HAR_20)
        /// </summary>
        [RegisterAttribute(AttributeID = 50202200)]
        public string Har_20
        {
            get
            {
                CheckPropertyInited("Har_20");
                return _har_20;
            }
            set
            {
                _har_20 = value;
                NotifyPropertyChanged("Har_20");
            }
        }


        private string _har_21;
        /// <summary>
        /// 50202300 Описание коммуникаций, в том числе их удаленность  (HAR_21)
        /// </summary>
        [RegisterAttribute(AttributeID = 50202300)]
        public string Har_21
        {
            get
            {
                CheckPropertyInited("Har_21");
                return _har_21;
            }
            set
            {
                _har_21 = value;
                NotifyPropertyChanged("Har_21");
            }
        }


        private string _har_21_1_1;
        /// <summary>
        /// 50202400 Наличие/отсутствие подключения к электрическим сетям  ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50202400)]
        public string Har_21_1_1
        {
            get
            {
                CheckPropertyInited("Har_21_1_1");
                return _har_21_1_1;
            }
            set
            {
                _har_21_1_1 = value;
                NotifyPropertyChanged("Har_21_1_1");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_21_1_1_Code;
        /// <summary>
        /// 50202400 Наличие/отсутствие подключения к электрическим сетям  (справочный код) (HAR_21_1_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 50202400)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_21_1_1_Code
        {
            get
            {
                CheckPropertyInited("Har_21_1_1_Code");
                return this._har_21_1_1_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_21_1_1))
                    {
                         _har_21_1_1 = descr;
                    }
                }
                else
                {
                     _har_21_1_1 = descr;
                }

                this._har_21_1_1_Code = value;
                NotifyPropertyChanged("Har_21_1_1");
                NotifyPropertyChanged("Har_21_1_1_Code");
            }
        }


        private string _har_21_1_2;
        /// <summary>
        /// 50202500 Возможность/отсутствие возможности подключения к сетям инженерно-технического обеспечения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50202500)]
        public string Har_21_1_2
        {
            get
            {
                CheckPropertyInited("Har_21_1_2");
                return _har_21_1_2;
            }
            set
            {
                _har_21_1_2 = value;
                NotifyPropertyChanged("Har_21_1_2");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_21_1_2_Code;
        /// <summary>
        /// 50202500 Возможность/отсутствие возможности подключения к сетям инженерно-технического обеспечения (справочный код) (HAR_21_1_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 50202500)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_21_1_2_Code
        {
            get
            {
                CheckPropertyInited("Har_21_1_2_Code");
                return this._har_21_1_2_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_21_1_2))
                    {
                         _har_21_1_2 = descr;
                    }
                }
                else
                {
                     _har_21_1_2 = descr;
                }

                this._har_21_1_2_Code = value;
                NotifyPropertyChanged("Har_21_1_2");
                NotifyPropertyChanged("Har_21_1_2_Code");
            }
        }


        private string _har_21_1_3;
        /// <summary>
        /// 50202600 Мощность электрической сети  (HAR_21_1_3)
        /// </summary>
        [RegisterAttribute(AttributeID = 50202600)]
        public string Har_21_1_3
        {
            get
            {
                CheckPropertyInited("Har_21_1_3");
                return _har_21_1_3;
            }
            set
            {
                _har_21_1_3 = value;
                NotifyPropertyChanged("Har_21_1_3");
            }
        }


        private string _har_21_2_1;
        /// <summary>
        /// 50202700 Наличие/отсутствие подключения к сетям газораспределения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50202700)]
        public string Har_21_2_1
        {
            get
            {
                CheckPropertyInited("Har_21_2_1");
                return _har_21_2_1;
            }
            set
            {
                _har_21_2_1 = value;
                NotifyPropertyChanged("Har_21_2_1");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_21_2_1_Code;
        /// <summary>
        /// 50202700 Наличие/отсутствие подключения к сетям газораспределения (справочный код) (HAR_21_2_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 50202700)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_21_2_1_Code
        {
            get
            {
                CheckPropertyInited("Har_21_2_1_Code");
                return this._har_21_2_1_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_21_2_1))
                    {
                         _har_21_2_1 = descr;
                    }
                }
                else
                {
                     _har_21_2_1 = descr;
                }

                this._har_21_2_1_Code = value;
                NotifyPropertyChanged("Har_21_2_1");
                NotifyPropertyChanged("Har_21_2_1_Code");
            }
        }


        private string _har_21_2_2;
        /// <summary>
        /// 50202800 Возможность/отсутствие возможности подключения к сетям газораспределения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50202800)]
        public string Har_21_2_2
        {
            get
            {
                CheckPropertyInited("Har_21_2_2");
                return _har_21_2_2;
            }
            set
            {
                _har_21_2_2 = value;
                NotifyPropertyChanged("Har_21_2_2");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_21_2_2_Code;
        /// <summary>
        /// 50202800 Возможность/отсутствие возможности подключения к сетям газораспределения (справочный код) (HAR_21_2_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 50202800)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_21_2_2_Code
        {
            get
            {
                CheckPropertyInited("Har_21_2_2_Code");
                return this._har_21_2_2_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_21_2_2))
                    {
                         _har_21_2_2 = descr;
                    }
                }
                else
                {
                     _har_21_2_2 = descr;
                }

                this._har_21_2_2_Code = value;
                NotifyPropertyChanged("Har_21_2_2");
                NotifyPropertyChanged("Har_21_2_2_Code");
            }
        }


        private string _har_21_2_3;
        /// <summary>
        /// 50202900 Мощность сетей газораспределения  (HAR_21_2_3)
        /// </summary>
        [RegisterAttribute(AttributeID = 50202900)]
        public string Har_21_2_3
        {
            get
            {
                CheckPropertyInited("Har_21_2_3");
                return _har_21_2_3;
            }
            set
            {
                _har_21_2_3 = value;
                NotifyPropertyChanged("Har_21_2_3");
            }
        }


        private string _har_21_3_1;
        /// <summary>
        /// 50203000 Наличие/отсутствие централизованного подключения к системе водоснабжения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50203000)]
        public string Har_21_3_1
        {
            get
            {
                CheckPropertyInited("Har_21_3_1");
                return _har_21_3_1;
            }
            set
            {
                _har_21_3_1 = value;
                NotifyPropertyChanged("Har_21_3_1");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_21_3_1_Code;
        /// <summary>
        /// 50203000 Наличие/отсутствие централизованного подключения к системе водоснабжения (справочный код) (HAR_21_3_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 50203000)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_21_3_1_Code
        {
            get
            {
                CheckPropertyInited("Har_21_3_1_Code");
                return this._har_21_3_1_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_21_3_1))
                    {
                         _har_21_3_1 = descr;
                    }
                }
                else
                {
                     _har_21_3_1 = descr;
                }

                this._har_21_3_1_Code = value;
                NotifyPropertyChanged("Har_21_3_1");
                NotifyPropertyChanged("Har_21_3_1_Code");
            }
        }


        private string _har_21_3_2;
        /// <summary>
        /// 50203100 Возможность/отсутствие возможности подключения к системе водоснабжения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50203100)]
        public string Har_21_3_2
        {
            get
            {
                CheckPropertyInited("Har_21_3_2");
                return _har_21_3_2;
            }
            set
            {
                _har_21_3_2 = value;
                NotifyPropertyChanged("Har_21_3_2");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_21_3_2_Code;
        /// <summary>
        /// 50203100 Возможность/отсутствие возможности подключения к системе водоснабжения (справочный код) (HAR_21_3_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 50203100)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_21_3_2_Code
        {
            get
            {
                CheckPropertyInited("Har_21_3_2_Code");
                return this._har_21_3_2_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_21_3_2))
                    {
                         _har_21_3_2 = descr;
                    }
                }
                else
                {
                     _har_21_3_2 = descr;
                }

                this._har_21_3_2_Code = value;
                NotifyPropertyChanged("Har_21_3_2");
                NotifyPropertyChanged("Har_21_3_2_Code");
            }
        }


        private string _har_21_4_1;
        /// <summary>
        /// 50203200 Наличие/отсутствие централизованного подключения к системе теплоснабжения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50203200)]
        public string Har_21_4_1
        {
            get
            {
                CheckPropertyInited("Har_21_4_1");
                return _har_21_4_1;
            }
            set
            {
                _har_21_4_1 = value;
                NotifyPropertyChanged("Har_21_4_1");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_21_4_1_Code;
        /// <summary>
        /// 50203200 Наличие/отсутствие централизованного подключения к системе теплоснабжения (справочный код) (HAR_21_4_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 50203200)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_21_4_1_Code
        {
            get
            {
                CheckPropertyInited("Har_21_4_1_Code");
                return this._har_21_4_1_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_21_4_1))
                    {
                         _har_21_4_1 = descr;
                    }
                }
                else
                {
                     _har_21_4_1 = descr;
                }

                this._har_21_4_1_Code = value;
                NotifyPropertyChanged("Har_21_4_1");
                NotifyPropertyChanged("Har_21_4_1_Code");
            }
        }


        private string _har_21_4_2;
        /// <summary>
        /// 50203300 Возможность/отсутствие возможности подключения к системе теплоснабжения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50203300)]
        public string Har_21_4_2
        {
            get
            {
                CheckPropertyInited("Har_21_4_2");
                return _har_21_4_2;
            }
            set
            {
                _har_21_4_2 = value;
                NotifyPropertyChanged("Har_21_4_2");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_21_4_2_Code;
        /// <summary>
        /// 50203300 Возможность/отсутствие возможности подключения к системе теплоснабжения (справочный код) (HAR_21_4_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 50203300)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_21_4_2_Code
        {
            get
            {
                CheckPropertyInited("Har_21_4_2_Code");
                return this._har_21_4_2_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_21_4_2))
                    {
                         _har_21_4_2 = descr;
                    }
                }
                else
                {
                     _har_21_4_2 = descr;
                }

                this._har_21_4_2_Code = value;
                NotifyPropertyChanged("Har_21_4_2");
                NotifyPropertyChanged("Har_21_4_2_Code");
            }
        }


        private string _har_21_5_1;
        /// <summary>
        /// 50203400 Наличие/отсутствие централизованного подключения к системе водоотведения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50203400)]
        public string Har_21_5_1
        {
            get
            {
                CheckPropertyInited("Har_21_5_1");
                return _har_21_5_1;
            }
            set
            {
                _har_21_5_1 = value;
                NotifyPropertyChanged("Har_21_5_1");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_21_5_1_Code;
        /// <summary>
        /// 50203400 Наличие/отсутствие централизованного подключения к системе водоотведения (справочный код) (HAR_21_5_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 50203400)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_21_5_1_Code
        {
            get
            {
                CheckPropertyInited("Har_21_5_1_Code");
                return this._har_21_5_1_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_21_5_1))
                    {
                         _har_21_5_1 = descr;
                    }
                }
                else
                {
                     _har_21_5_1 = descr;
                }

                this._har_21_5_1_Code = value;
                NotifyPropertyChanged("Har_21_5_1");
                NotifyPropertyChanged("Har_21_5_1_Code");
            }
        }


        private string _har_21_5_2;
        /// <summary>
        /// 50203500 Возможность/отсутствие возможности подключения к системе водоотведения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50203500)]
        public string Har_21_5_2
        {
            get
            {
                CheckPropertyInited("Har_21_5_2");
                return _har_21_5_2;
            }
            set
            {
                _har_21_5_2 = value;
                NotifyPropertyChanged("Har_21_5_2");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_21_5_2_Code;
        /// <summary>
        /// 50203500 Возможность/отсутствие возможности подключения к системе водоотведения (справочный код) (HAR_21_5_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 50203500)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_21_5_2_Code
        {
            get
            {
                CheckPropertyInited("Har_21_5_2_Code");
                return this._har_21_5_2_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_21_5_2))
                    {
                         _har_21_5_2 = descr;
                    }
                }
                else
                {
                     _har_21_5_2 = descr;
                }

                this._har_21_5_2_Code = value;
                NotifyPropertyChanged("Har_21_5_2");
                NotifyPropertyChanged("Har_21_5_2_Code");
            }
        }

    }
}

namespace ObjectModel.Declarations
{
    /// <summary>
    /// 503 Характеристики ЗУ (DECLARATIONS_HAR_PARCEL)
    /// </summary>
    [RegisterInfo(RegisterID = 503)]
    [Serializable]
    public partial class OMHarParcel : OMBaseClass<OMHarParcel>
    {

        private long _id;
        /// <summary>
        /// 50300100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 50300100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _declaration_id;
        /// <summary>
        /// 50300200 Идентификатор декларации (DECLARATION_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50300200)]
        public long? Declaration_Id
        {
            get
            {
                CheckPropertyInited("Declaration_Id");
                return _declaration_id;
            }
            set
            {
                _declaration_id = value;
                NotifyPropertyChanged("Declaration_Id");
            }
        }


        private string _har_1;
        /// <summary>
        /// 50300300 Адрес земельного участка (HAR_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 50300300)]
        public string Har_1
        {
            get
            {
                CheckPropertyInited("Har_1");
                return _har_1;
            }
            set
            {
                _har_1 = value;
                NotifyPropertyChanged("Har_1");
            }
        }


        private decimal? _har_2;
        /// <summary>
        /// 50300400 Площадь  (HAR_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 50300400)]
        public decimal? Har_2
        {
            get
            {
                CheckPropertyInited("Har_2");
                return _har_2;
            }
            set
            {
                _har_2 = value;
                NotifyPropertyChanged("Har_2");
            }
        }


        private string _har_3;
        /// <summary>
        /// 50300500 Категория земель  (HAR_3)
        /// </summary>
        [RegisterAttribute(AttributeID = 50300500)]
        public string Har_3
        {
            get
            {
                CheckPropertyInited("Har_3");
                return _har_3;
            }
            set
            {
                _har_3 = value;
                NotifyPropertyChanged("Har_3");
            }
        }


        private string _har_4;
        /// <summary>
        /// 50300600 Вид разрешенного использования  (HAR_4)
        /// </summary>
        [RegisterAttribute(AttributeID = 50300600)]
        public string Har_4
        {
            get
            {
                CheckPropertyInited("Har_4");
                return _har_4;
            }
            set
            {
                _har_4 = value;
                NotifyPropertyChanged("Har_4");
            }
        }


        private string _har_5;
        /// <summary>
        /// 50300700 Фактическое использование земельного участка (HAR_5)
        /// </summary>
        [RegisterAttribute(AttributeID = 50300700)]
        public string Har_5
        {
            get
            {
                CheckPropertyInited("Har_5");
                return _har_5;
            }
            set
            {
                _har_5 = value;
                NotifyPropertyChanged("Har_5");
            }
        }


        private string _har_6;
        /// <summary>
        /// 50300800 Сведения о лесах, водных объектах и об иных природных объектах (HAR_6)
        /// </summary>
        [RegisterAttribute(AttributeID = 50300800)]
        public string Har_6
        {
            get
            {
                CheckPropertyInited("Har_6");
                return _har_6;
            }
            set
            {
                _har_6 = value;
                NotifyPropertyChanged("Har_6");
            }
        }


        private string _har_7;
        /// <summary>
        /// 50300900 Сведения о том, что земельный участок полностью или частично расположен в границах зоны с особыми условиями  (HAR_7)
        /// </summary>
        [RegisterAttribute(AttributeID = 50300900)]
        public string Har_7
        {
            get
            {
                CheckPropertyInited("Har_7");
                return _har_7;
            }
            set
            {
                _har_7 = value;
                NotifyPropertyChanged("Har_7");
            }
        }


        private string _har_8;
        /// <summary>
        /// 50301000 Сведения о том, что земельный участок расположен в границах особо охраняемой природной территории (HAR_8)
        /// </summary>
        [RegisterAttribute(AttributeID = 50301000)]
        public string Har_8
        {
            get
            {
                CheckPropertyInited("Har_8");
                return _har_8;
            }
            set
            {
                _har_8 = value;
                NotifyPropertyChanged("Har_8");
            }
        }


        private string _har_9;
        /// <summary>
        /// 50301100 Сведения о том, что земельный участок расположен в границах особой экономической зоны (HAR_9)
        /// </summary>
        [RegisterAttribute(AttributeID = 50301100)]
        public string Har_9
        {
            get
            {
                CheckPropertyInited("Har_9");
                return _har_9;
            }
            set
            {
                _har_9 = value;
                NotifyPropertyChanged("Har_9");
            }
        }


        private string _har_10;
        /// <summary>
        /// 50301200 Сведения об установленных сервитутах (HAR_10)
        /// </summary>
        [RegisterAttribute(AttributeID = 50301200)]
        public string Har_10
        {
            get
            {
                CheckPropertyInited("Har_10");
                return _har_10;
            }
            set
            {
                _har_10 = value;
                NotifyPropertyChanged("Har_10");
            }
        }


        private string _har_11;
        /// <summary>
        /// 50301300 Удаленность от автомобильных дорог с твердым покрытием (HAR_11)
        /// </summary>
        [RegisterAttribute(AttributeID = 50301300)]
        public string Har_11
        {
            get
            {
                CheckPropertyInited("Har_11");
                return _har_11;
            }
            set
            {
                _har_11 = value;
                NotifyPropertyChanged("Har_11");
            }
        }


        private string _har_12;
        /// <summary>
        /// 50301400 Сведения о наличии/отсутствии подъездных путей  (HAR_12)
        /// </summary>
        [RegisterAttribute(AttributeID = 50301400)]
        public string Har_12
        {
            get
            {
                CheckPropertyInited("Har_12");
                return _har_12;
            }
            set
            {
                _har_12 = value;
                NotifyPropertyChanged("Har_12");
            }
        }


        private string _har_13;
        /// <summary>
        /// 50301500 Описание коммуникаций, в том числе их удаленность  (HAR_13)
        /// </summary>
        [RegisterAttribute(AttributeID = 50301500)]
        public string Har_13
        {
            get
            {
                CheckPropertyInited("Har_13");
                return _har_13;
            }
            set
            {
                _har_13 = value;
                NotifyPropertyChanged("Har_13");
            }
        }


        private string _har_13_1_1;
        /// <summary>
        /// 50301600 Наличие/отсутствие подключения к электрическим сетям  ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50301600)]
        public string Har_13_1_1
        {
            get
            {
                CheckPropertyInited("Har_13_1_1");
                return _har_13_1_1;
            }
            set
            {
                _har_13_1_1 = value;
                NotifyPropertyChanged("Har_13_1_1");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_13_1_1_Code;
        /// <summary>
        /// 50301600 Наличие/отсутствие подключения к электрическим сетям  (справочный код) (HAR_13_1_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 50301600)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_13_1_1_Code
        {
            get
            {
                CheckPropertyInited("Har_13_1_1_Code");
                return this._har_13_1_1_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_13_1_1))
                    {
                         _har_13_1_1 = descr;
                    }
                }
                else
                {
                     _har_13_1_1 = descr;
                }

                this._har_13_1_1_Code = value;
                NotifyPropertyChanged("Har_13_1_1");
                NotifyPropertyChanged("Har_13_1_1_Code");
            }
        }


        private string _har_13_1_2;
        /// <summary>
        /// 50301700 Возможность/отсутствие возможности подключения к сетям ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50301700)]
        public string Har_13_1_2
        {
            get
            {
                CheckPropertyInited("Har_13_1_2");
                return _har_13_1_2;
            }
            set
            {
                _har_13_1_2 = value;
                NotifyPropertyChanged("Har_13_1_2");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_13_1_2_Code;
        /// <summary>
        /// 50301700 Возможность/отсутствие возможности подключения к сетям (справочный код) (HAR_13_1_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 50301700)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_13_1_2_Code
        {
            get
            {
                CheckPropertyInited("Har_13_1_2_Code");
                return this._har_13_1_2_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_13_1_2))
                    {
                         _har_13_1_2 = descr;
                    }
                }
                else
                {
                     _har_13_1_2 = descr;
                }

                this._har_13_1_2_Code = value;
                NotifyPropertyChanged("Har_13_1_2");
                NotifyPropertyChanged("Har_13_1_2_Code");
            }
        }


        private string _har_13_1_3;
        /// <summary>
        /// 50301800 Мощность электрической сети  (HAR_13_1_3)
        /// </summary>
        [RegisterAttribute(AttributeID = 50301800)]
        public string Har_13_1_3
        {
            get
            {
                CheckPropertyInited("Har_13_1_3");
                return _har_13_1_3;
            }
            set
            {
                _har_13_1_3 = value;
                NotifyPropertyChanged("Har_13_1_3");
            }
        }


        private string _har_13_2_1;
        /// <summary>
        /// 50301900 Наличие/отсутствие подключения к сетям газораспределения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50301900)]
        public string Har_13_2_1
        {
            get
            {
                CheckPropertyInited("Har_13_2_1");
                return _har_13_2_1;
            }
            set
            {
                _har_13_2_1 = value;
                NotifyPropertyChanged("Har_13_2_1");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_13_2_1_Code;
        /// <summary>
        /// 50301900 Наличие/отсутствие подключения к сетям газораспределения (справочный код) (HAR_13_2_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 50301900)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_13_2_1_Code
        {
            get
            {
                CheckPropertyInited("Har_13_2_1_Code");
                return this._har_13_2_1_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_13_2_1))
                    {
                         _har_13_2_1 = descr;
                    }
                }
                else
                {
                     _har_13_2_1 = descr;
                }

                this._har_13_2_1_Code = value;
                NotifyPropertyChanged("Har_13_2_1");
                NotifyPropertyChanged("Har_13_2_1_Code");
            }
        }


        private string _har_13_2_2;
        /// <summary>
        /// 50302000 Возможность/отсутствие возможности подключения к сетям газораспределения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50302000)]
        public string Har_13_2_2
        {
            get
            {
                CheckPropertyInited("Har_13_2_2");
                return _har_13_2_2;
            }
            set
            {
                _har_13_2_2 = value;
                NotifyPropertyChanged("Har_13_2_2");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_13_2_2_Code;
        /// <summary>
        /// 50302000 Возможность/отсутствие возможности подключения к сетям газораспределения (справочный код) (HAR_13_2_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 50302000)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_13_2_2_Code
        {
            get
            {
                CheckPropertyInited("Har_13_2_2_Code");
                return this._har_13_2_2_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_13_2_2))
                    {
                         _har_13_2_2 = descr;
                    }
                }
                else
                {
                     _har_13_2_2 = descr;
                }

                this._har_13_2_2_Code = value;
                NotifyPropertyChanged("Har_13_2_2");
                NotifyPropertyChanged("Har_13_2_2_Code");
            }
        }


        private string _har_13_2_3;
        /// <summary>
        /// 50302100 Мощность сетей газораспределения  (HAR_13_2_3)
        /// </summary>
        [RegisterAttribute(AttributeID = 50302100)]
        public string Har_13_2_3
        {
            get
            {
                CheckPropertyInited("Har_13_2_3");
                return _har_13_2_3;
            }
            set
            {
                _har_13_2_3 = value;
                NotifyPropertyChanged("Har_13_2_3");
            }
        }


        private string _har_13_3_1;
        /// <summary>
        /// 50302200 Наличие/отсутствие централизованного подключения к системе водоснабжения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50302200)]
        public string Har_13_3_1
        {
            get
            {
                CheckPropertyInited("Har_13_3_1");
                return _har_13_3_1;
            }
            set
            {
                _har_13_3_1 = value;
                NotifyPropertyChanged("Har_13_3_1");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_13_3_1_Code;
        /// <summary>
        /// 50302200 Наличие/отсутствие централизованного подключения к системе водоснабжения (справочный код) (HAR_13_3_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 50302200)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_13_3_1_Code
        {
            get
            {
                CheckPropertyInited("Har_13_3_1_Code");
                return this._har_13_3_1_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_13_3_1))
                    {
                         _har_13_3_1 = descr;
                    }
                }
                else
                {
                     _har_13_3_1 = descr;
                }

                this._har_13_3_1_Code = value;
                NotifyPropertyChanged("Har_13_3_1");
                NotifyPropertyChanged("Har_13_3_1_Code");
            }
        }


        private string _har_13_3_2;
        /// <summary>
        /// 50302300 Возможность/отсутствие возможности подключения к системе водоснабжения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50302300)]
        public string Har_13_3_2
        {
            get
            {
                CheckPropertyInited("Har_13_3_2");
                return _har_13_3_2;
            }
            set
            {
                _har_13_3_2 = value;
                NotifyPropertyChanged("Har_13_3_2");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_13_3_2_Code;
        /// <summary>
        /// 50302300 Возможность/отсутствие возможности подключения к системе водоснабжения (справочный код) (HAR_13_3_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 50302300)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_13_3_2_Code
        {
            get
            {
                CheckPropertyInited("Har_13_3_2_Code");
                return this._har_13_3_2_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_13_3_2))
                    {
                         _har_13_3_2 = descr;
                    }
                }
                else
                {
                     _har_13_3_2 = descr;
                }

                this._har_13_3_2_Code = value;
                NotifyPropertyChanged("Har_13_3_2");
                NotifyPropertyChanged("Har_13_3_2_Code");
            }
        }


        private string _har_13_4_1;
        /// <summary>
        /// 50302400 Наличие/отсутствие централизованного подключения к системе теплоснабжения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50302400)]
        public string Har_13_4_1
        {
            get
            {
                CheckPropertyInited("Har_13_4_1");
                return _har_13_4_1;
            }
            set
            {
                _har_13_4_1 = value;
                NotifyPropertyChanged("Har_13_4_1");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_13_4_1_Code;
        /// <summary>
        /// 50302400 Наличие/отсутствие централизованного подключения к системе теплоснабжения (справочный код) (HAR_13_4_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 50302400)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_13_4_1_Code
        {
            get
            {
                CheckPropertyInited("Har_13_4_1_Code");
                return this._har_13_4_1_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_13_4_1))
                    {
                         _har_13_4_1 = descr;
                    }
                }
                else
                {
                     _har_13_4_1 = descr;
                }

                this._har_13_4_1_Code = value;
                NotifyPropertyChanged("Har_13_4_1");
                NotifyPropertyChanged("Har_13_4_1_Code");
            }
        }


        private string _har_13_4_2;
        /// <summary>
        /// 50302500 Возможность/отсутствие возможности подключения к системе теплоснабжения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50302500)]
        public string Har_13_4_2
        {
            get
            {
                CheckPropertyInited("Har_13_4_2");
                return _har_13_4_2;
            }
            set
            {
                _har_13_4_2 = value;
                NotifyPropertyChanged("Har_13_4_2");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_13_4_2_Code;
        /// <summary>
        /// 50302500 Возможность/отсутствие возможности подключения к системе теплоснабжения (справочный код) (HAR_13_4_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 50302500)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_13_4_2_Code
        {
            get
            {
                CheckPropertyInited("Har_13_4_2_Code");
                return this._har_13_4_2_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_13_4_2))
                    {
                         _har_13_4_2 = descr;
                    }
                }
                else
                {
                     _har_13_4_2 = descr;
                }

                this._har_13_4_2_Code = value;
                NotifyPropertyChanged("Har_13_4_2");
                NotifyPropertyChanged("Har_13_4_2_Code");
            }
        }


        private string _har_13_5_1;
        /// <summary>
        /// 50302600 Наличие/отсутствие централизованного подключения к системе водоотведения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50302600)]
        public string Har_13_5_1
        {
            get
            {
                CheckPropertyInited("Har_13_5_1");
                return _har_13_5_1;
            }
            set
            {
                _har_13_5_1 = value;
                NotifyPropertyChanged("Har_13_5_1");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_13_5_1_Code;
        /// <summary>
        /// 50302600 Наличие/отсутствие централизованного подключения к системе водоотведения (справочный код) (HAR_13_5_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 50302600)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_13_5_1_Code
        {
            get
            {
                CheckPropertyInited("Har_13_5_1_Code");
                return this._har_13_5_1_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_13_5_1))
                    {
                         _har_13_5_1 = descr;
                    }
                }
                else
                {
                     _har_13_5_1 = descr;
                }

                this._har_13_5_1_Code = value;
                NotifyPropertyChanged("Har_13_5_1");
                NotifyPropertyChanged("Har_13_5_1_Code");
            }
        }


        private string _har_13_5_2;
        /// <summary>
        /// 50302700 Возможность/отсутствие возможности подключения к системе водоотведения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50302700)]
        public string Har_13_5_2
        {
            get
            {
                CheckPropertyInited("Har_13_5_2");
                return _har_13_5_2;
            }
            set
            {
                _har_13_5_2 = value;
                NotifyPropertyChanged("Har_13_5_2");
            }
        }


        private ObjectModel.Directory.Declarations.HarAvailability _har_13_5_2_Code;
        /// <summary>
        /// 50302700 Возможность/отсутствие возможности подключения к системе водоотведения (справочный код) (HAR_13_5_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 50302700)]
        public ObjectModel.Directory.Declarations.HarAvailability Har_13_5_2_Code
        {
            get
            {
                CheckPropertyInited("Har_13_5_2_Code");
                return this._har_13_5_2_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_har_13_5_2))
                    {
                         _har_13_5_2 = descr;
                    }
                }
                else
                {
                     _har_13_5_2 = descr;
                }

                this._har_13_5_2_Code = value;
                NotifyPropertyChanged("Har_13_5_2");
                NotifyPropertyChanged("Har_13_5_2_Code");
            }
        }


        private string _har_14;
        /// <summary>
        /// 50302800 Удаленность относительно ближайшего водного объекта  (HAR_14)
        /// </summary>
        [RegisterAttribute(AttributeID = 50302800)]
        public string Har_14
        {
            get
            {
                CheckPropertyInited("Har_14");
                return _har_14;
            }
            set
            {
                _har_14 = value;
                NotifyPropertyChanged("Har_14");
            }
        }


        private string _har_15;
        /// <summary>
        /// 50302900 Удаленность относительно ближайшей рекреационной зоны  (HAR_15)
        /// </summary>
        [RegisterAttribute(AttributeID = 50302900)]
        public string Har_15
        {
            get
            {
                CheckPropertyInited("Har_15");
                return _har_15;
            }
            set
            {
                _har_15 = value;
                NotifyPropertyChanged("Har_15");
            }
        }


        private string _har_16;
        /// <summary>
        /// 50303000 Удаленность относительно железных дорог  (HAR_16)
        /// </summary>
        [RegisterAttribute(AttributeID = 50303000)]
        public string Har_16
        {
            get
            {
                CheckPropertyInited("Har_16");
                return _har_16;
            }
            set
            {
                _har_16 = value;
                NotifyPropertyChanged("Har_16");
            }
        }


        private string _har_17;
        /// <summary>
        /// 50303100 Удаленность относительно железнодорожных вокзалов (станций) (HAR_17)
        /// </summary>
        [RegisterAttribute(AttributeID = 50303100)]
        public string Har_17
        {
            get
            {
                CheckPropertyInited("Har_17");
                return _har_17;
            }
            set
            {
                _har_17 = value;
                NotifyPropertyChanged("Har_17");
            }
        }


        private string _har_18;
        /// <summary>
        /// 50303200 Удаленность от зоны разработки полезных ископаемых (HAR_18)
        /// </summary>
        [RegisterAttribute(AttributeID = 50303200)]
        public string Har_18
        {
            get
            {
                CheckPropertyInited("Har_18");
                return _har_18;
            }
            set
            {
                _har_18 = value;
                NotifyPropertyChanged("Har_18");
            }
        }


        private string _har_19;
        /// <summary>
        /// 50303300 Вид угодий  (HAR_19)
        /// </summary>
        [RegisterAttribute(AttributeID = 50303300)]
        public string Har_19
        {
            get
            {
                CheckPropertyInited("Har_19");
                return _har_19;
            }
            set
            {
                _har_19 = value;
                NotifyPropertyChanged("Har_19");
            }
        }


        private string _har_20;
        /// <summary>
        /// 50303400 Показатели состояния почв  (HAR_20)
        /// </summary>
        [RegisterAttribute(AttributeID = 50303400)]
        public string Har_20
        {
            get
            {
                CheckPropertyInited("Har_20");
                return _har_20;
            }
            set
            {
                _har_20 = value;
                NotifyPropertyChanged("Har_20");
            }
        }


        private string _har_21;
        /// <summary>
        /// 50303500 Наличие недостатков, препятствующих рациональному использованию и охране земель (HAR_21)
        /// </summary>
        [RegisterAttribute(AttributeID = 50303500)]
        public string Har_21
        {
            get
            {
                CheckPropertyInited("Har_21");
                return _har_21;
            }
            set
            {
                _har_21 = value;
                NotifyPropertyChanged("Har_21");
            }
        }

    }
}

namespace ObjectModel.Declarations
{
    /// <summary>
    /// 504 Результаты (DECLARATIONS_RESULT)
    /// </summary>
    [RegisterInfo(RegisterID = 504)]
    [Serializable]
    public partial class OMResult : OMBaseClass<OMResult>
    {

        private long _declaration_id;
        /// <summary>
        /// 50400100 Идентификатор декларации (DECLARATION_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 50400100)]
        public long Declaration_Id
        {
            get
            {
                CheckPropertyInited("Declaration_Id");
                return _declaration_id;
            }
            set
            {
                _declaration_id = value;
                NotifyPropertyChanged("Declaration_Id");
            }
        }


        private string _textyes;
        /// <summary>
        /// 50400200 Положительный отзыв по декларации (TEXT_YES)
        /// </summary>
        [RegisterAttribute(AttributeID = 50400200)]
        public string TextYes
        {
            get
            {
                CheckPropertyInited("TextYes");
                return _textyes;
            }
            set
            {
                _textyes = value;
                NotifyPropertyChanged("TextYes");
            }
        }


        private string _textno;
        /// <summary>
        /// 50400300 Отрицательный отзыв по декларации (TEXT_NO)
        /// </summary>
        [RegisterAttribute(AttributeID = 50400300)]
        public string TextNo
        {
            get
            {
                CheckPropertyInited("TextNo");
                return _textno;
            }
            set
            {
                _textno = value;
                NotifyPropertyChanged("TextNo");
            }
        }

    }
}

namespace ObjectModel.Declarations
{
    /// <summary>
    /// 505 Субъекты (DECLARATIONS_SUBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 505)]
    [Serializable]
    public partial class OMSubject : OMBaseClass<OMSubject>
    {

        private long _id;
        /// <summary>
        /// 50500100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 50500100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _name;
        /// <summary>
        /// 50500200 Наименование юридического лица  (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50500200)]
        public string Name
        {
            get
            {
                CheckPropertyInited("Name");
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }


        private string _f_name;
        /// <summary>
        /// 50500300 Фамилия физического лица/представителя заявителя (F_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50500300)]
        public string F_Name
        {
            get
            {
                CheckPropertyInited("F_Name");
                return _f_name;
            }
            set
            {
                _f_name = value;
                NotifyPropertyChanged("F_Name");
            }
        }


        private string _i_name;
        /// <summary>
        /// 50500400 Имя физического лица/представителя заявителя (I_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50500400)]
        public string I_Name
        {
            get
            {
                CheckPropertyInited("I_Name");
                return _i_name;
            }
            set
            {
                _i_name = value;
                NotifyPropertyChanged("I_Name");
            }
        }


        private string _o_name;
        /// <summary>
        /// 50500500 Отчество физического лица/представителя заявителя (O_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50500500)]
        public string O_Name
        {
            get
            {
                CheckPropertyInited("O_Name");
                return _o_name;
            }
            set
            {
                _o_name = value;
                NotifyPropertyChanged("O_Name");
            }
        }


        private string _mail;
        /// <summary>
        /// 50500700 Адрес электронной почты (MAIL)
        /// </summary>
        [RegisterAttribute(AttributeID = 50500700)]
        public string Mail
        {
            get
            {
                CheckPropertyInited("Mail");
                return _mail;
            }
            set
            {
                _mail = value;
                NotifyPropertyChanged("Mail");
            }
        }


        private string _telefon;
        /// <summary>
        /// 50500800 Телефон для связи  (TELEFON)
        /// </summary>
        [RegisterAttribute(AttributeID = 50500800)]
        public string Telefon
        {
            get
            {
                CheckPropertyInited("Telefon");
                return _telefon;
            }
            set
            {
                _telefon = value;
                NotifyPropertyChanged("Telefon");
            }
        }


        private string _type;
        /// <summary>
        /// 50501000 Тип субъекта ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50501000)]
        public string Type
        {
            get
            {
                CheckPropertyInited("Type");
                return _type;
            }
            set
            {
                _type = value;
                NotifyPropertyChanged("Type");
            }
        }


        private ObjectModel.Directory.Declarations.SubjectType _type_Code;
        /// <summary>
        /// 50501000 Тип субъекта (справочный код) (TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 50501000)]
        public ObjectModel.Directory.Declarations.SubjectType Type_Code
        {
            get
            {
                CheckPropertyInited("Type_Code");
                return this._type_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_type))
                    {
                         _type = descr;
                    }
                }
                else
                {
                     _type = descr;
                }

                this._type_Code = value;
                NotifyPropertyChanged("Type");
                NotifyPropertyChanged("Type_Code");
            }
        }


        private string _zip;
        /// <summary>
        /// 50501100 Индекс (ZIP)
        /// </summary>
        [RegisterAttribute(AttributeID = 50501100)]
        public string Zip
        {
            get
            {
                CheckPropertyInited("Zip");
                return _zip;
            }
            set
            {
                _zip = value;
                NotifyPropertyChanged("Zip");
            }
        }


        private string _city;
        /// <summary>
        /// 50501200 Город (CITY)
        /// </summary>
        [RegisterAttribute(AttributeID = 50501200)]
        public string City
        {
            get
            {
                CheckPropertyInited("City");
                return _city;
            }
            set
            {
                _city = value;
                NotifyPropertyChanged("City");
            }
        }


        private string _street;
        /// <summary>
        /// 50501300 Улица (STREET)
        /// </summary>
        [RegisterAttribute(AttributeID = 50501300)]
        public string Street
        {
            get
            {
                CheckPropertyInited("Street");
                return _street;
            }
            set
            {
                _street = value;
                NotifyPropertyChanged("Street");
            }
        }


        private string _house;
        /// <summary>
        /// 50501400 Дом (HOUSE)
        /// </summary>
        [RegisterAttribute(AttributeID = 50501400)]
        public string House
        {
            get
            {
                CheckPropertyInited("House");
                return _house;
            }
            set
            {
                _house = value;
                NotifyPropertyChanged("House");
            }
        }


        private string _building;
        /// <summary>
        /// 50501500 Строение (BUILDING)
        /// </summary>
        [RegisterAttribute(AttributeID = 50501500)]
        public string Building
        {
            get
            {
                CheckPropertyInited("Building");
                return _building;
            }
            set
            {
                _building = value;
                NotifyPropertyChanged("Building");
            }
        }


        private string _flat;
        /// <summary>
        /// 50501600 Квартира (FLAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 50501600)]
        public string Flat
        {
            get
            {
                CheckPropertyInited("Flat");
                return _flat;
            }
            set
            {
                _flat = value;
                NotifyPropertyChanged("Flat");
            }
        }


        private string _officenumber;
        /// <summary>
        /// 50501700 Номер офиса/помещения (OFFICE_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 50501700)]
        public string OfficeNumber
        {
            get
            {
                CheckPropertyInited("OfficeNumber");
                return _officenumber;
            }
            set
            {
                _officenumber = value;
                NotifyPropertyChanged("OfficeNumber");
            }
        }

    }
}

namespace ObjectModel.Declarations
{
    /// <summary>
    /// 506 Уведомления (DECLARATIONS_UVED)
    /// </summary>
    [RegisterInfo(RegisterID = 506)]
    [Serializable]
    public partial class OMUved : OMBaseClass<OMUved>
    {

        private long _id;
        /// <summary>
        /// 50600100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 50600100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _declaration_id;
        /// <summary>
        /// 50600200 Идентификатор декларации (DECLARATION_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50600200)]
        public long Declaration_Id
        {
            get
            {
                CheckPropertyInited("Declaration_Id");
                return _declaration_id;
            }
            set
            {
                _declaration_id = value;
                NotifyPropertyChanged("Declaration_Id");
            }
        }


        private long _book_id;
        /// <summary>
        /// 50600300 Идентификатор книги (BOOK_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50600300)]
        public long Book_Id
        {
            get
            {
                CheckPropertyInited("Book_Id");
                return _book_id;
            }
            set
            {
                _book_id = value;
                NotifyPropertyChanged("Book_Id");
            }
        }


        private string _num;
        /// <summary>
        /// 50600400 Номер (NUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 50600400)]
        public string Num
        {
            get
            {
                CheckPropertyInited("Num");
                return _num;
            }
            set
            {
                _num = value;
                NotifyPropertyChanged("Num");
            }
        }


        private DateTime? _date;
        /// <summary>
        /// 50600500 Дата (DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 50600500)]
        public DateTime? Date
        {
            get
            {
                CheckPropertyInited("Date");
                return _date;
            }
            set
            {
                _date = value;
                NotifyPropertyChanged("Date");
            }
        }


        private string _type;
        /// <summary>
        /// 50600600 Тип уведомления ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50600600)]
        public string Type
        {
            get
            {
                CheckPropertyInited("Type");
                return _type;
            }
            set
            {
                _type = value;
                NotifyPropertyChanged("Type");
            }
        }


        private ObjectModel.Directory.Declarations.UvedType _type_Code;
        /// <summary>
        /// 50600600 Тип уведомления (справочный код) (TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 50600600)]
        public ObjectModel.Directory.Declarations.UvedType Type_Code
        {
            get
            {
                CheckPropertyInited("Type_Code");
                return this._type_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_type))
                    {
                         _type = descr;
                    }
                }
                else
                {
                     _type = descr;
                }

                this._type_Code = value;
                NotifyPropertyChanged("Type");
                NotifyPropertyChanged("Type_Code");
            }
        }


        private string _mailnum;
        /// <summary>
        /// 50600700 Номер почтового уведомления (MAIL_NUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 50600700)]
        public string MailNum
        {
            get
            {
                CheckPropertyInited("MailNum");
                return _mailnum;
            }
            set
            {
                _mailnum = value;
                NotifyPropertyChanged("MailNum");
            }
        }


        private DateTime? _maildate;
        /// <summary>
        /// 50600800 Дата почтового уведомления (MAIL_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 50600800)]
        public DateTime? MailDate
        {
            get
            {
                CheckPropertyInited("MailDate");
                return _maildate;
            }
            set
            {
                _maildate = value;
                NotifyPropertyChanged("MailDate");
            }
        }


        private string _rejectionreason;
        /// <summary>
        /// 50600900 Причина отказа (REJECTION_REASON)
        /// </summary>
        [RegisterAttribute(AttributeID = 50600900)]
        public string RejectionReason
        {
            get
            {
                CheckPropertyInited("RejectionReason");
                return _rejectionreason;
            }
            set
            {
                _rejectionreason = value;
                NotifyPropertyChanged("RejectionReason");
            }
        }


        private string _rejectionreasontype;
        /// <summary>
        /// 50601000 Тип причины отказа для уведомления об отказе декларации ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50601000)]
        public string RejectionReasonType
        {
            get
            {
                CheckPropertyInited("RejectionReasonType");
                return _rejectionreasontype;
            }
            set
            {
                _rejectionreasontype = value;
                NotifyPropertyChanged("RejectionReasonType");
            }
        }


        private ObjectModel.Directory.Declarations.RejectionReasonType _rejectionreasontype_Code;
        /// <summary>
        /// 50601000 Тип причины отказа для уведомления об отказе декларации (справочный код) (REJECTION_REASON_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 50601000)]
        public ObjectModel.Directory.Declarations.RejectionReasonType RejectionReasonType_Code
        {
            get
            {
                CheckPropertyInited("RejectionReasonType_Code");
                return this._rejectionreasontype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_rejectionreasontype))
                    {
                         _rejectionreasontype = descr;
                    }
                }
                else
                {
                     _rejectionreasontype = descr;
                }

                this._rejectionreasontype_Code = value;
                NotifyPropertyChanged("RejectionReasonType");
                NotifyPropertyChanged("RejectionReasonType_Code");
            }
        }


        private string _annex;
        /// <summary>
        /// 50601100 Приложение (ANNEX)
        /// </summary>
        [RegisterAttribute(AttributeID = 50601100)]
        public string Annex
        {
            get
            {
                CheckPropertyInited("Annex");
                return _annex;
            }
            set
            {
                _annex = value;
                NotifyPropertyChanged("Annex");
            }
        }

    }
}

namespace ObjectModel.Declarations
{
    /// <summary>
    /// 507 Связь типов причин отказа и уведомлений об отказе и возврате документов (DECLARATIONS_UVED_REJECTION_REASON_TYPE)
    /// </summary>
    [RegisterInfo(RegisterID = 507)]
    [Serializable]
    public partial class OMUvedRejectionReasonType : OMBaseClass<OMUvedRejectionReasonType>
    {

        private long _id;
        /// <summary>
        /// 50701000 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 50701000)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _uvedid;
        /// <summary>
        /// 50702000 Идентификатор уведомления (UVED_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50702000)]
        public long UvedId
        {
            get
            {
                CheckPropertyInited("UvedId");
                return _uvedid;
            }
            set
            {
                _uvedid = value;
                NotifyPropertyChanged("UvedId");
            }
        }


        private string _rejectionreasontype;
        /// <summary>
        /// 50703000 Тип причины отказа ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50703000)]
        public string RejectionReasonType
        {
            get
            {
                CheckPropertyInited("RejectionReasonType");
                return _rejectionreasontype;
            }
            set
            {
                _rejectionreasontype = value;
                NotifyPropertyChanged("RejectionReasonType");
            }
        }


        private ObjectModel.Directory.Declarations.RejectionReasonType _rejectionreasontype_Code;
        /// <summary>
        /// 50703000 Тип причины отказа (справочный код) (REJECTION_REASON_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 50703000)]
        public ObjectModel.Directory.Declarations.RejectionReasonType RejectionReasonType_Code
        {
            get
            {
                CheckPropertyInited("RejectionReasonType_Code");
                return this._rejectionreasontype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_rejectionreasontype))
                    {
                         _rejectionreasontype = descr;
                    }
                }
                else
                {
                     _rejectionreasontype = descr;
                }

                this._rejectionreasontype_Code = value;
                NotifyPropertyChanged("RejectionReasonType");
                NotifyPropertyChanged("RejectionReasonType_Code");
            }
        }

    }
}

namespace ObjectModel.Declarations
{
    /// <summary>
    /// 508 Подписант (DECLARATIONS_SIGNATORY)
    /// </summary>
    [RegisterInfo(RegisterID = 508)]
    [Serializable]
    public partial class OMSignatory : OMBaseClass<OMSignatory>
    {

        private long _id;
        /// <summary>
        /// 50801000 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 50801000)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _fullname;
        /// <summary>
        /// 50802000 ФИО (FULL_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50802000)]
        public string FullName
        {
            get
            {
                CheckPropertyInited("FullName");
                return _fullname;
            }
            set
            {
                _fullname = value;
                NotifyPropertyChanged("FullName");
            }
        }


        private string _position;
        /// <summary>
        /// 50803000 Должность (POSITION)
        /// </summary>
        [RegisterAttribute(AttributeID = 50803000)]
        public string Position
        {
            get
            {
                CheckPropertyInited("Position");
                return _position;
            }
            set
            {
                _position = value;
                NotifyPropertyChanged("Position");
            }
        }

    }
}

namespace ObjectModel.Declarations
{
    /// <summary>
    /// 509 Характеристики ОКС. Дополнительная информация (DECLARATIONS_HAR_OKS_ADDITIONAL_INFO)
    /// </summary>
    [RegisterInfo(RegisterID = 509)]
    [Serializable]
    public partial class OMHarOKSAdditionalInfo : OMBaseClass<OMHarOKSAdditionalInfo>
    {

        private long _id;
        /// <summary>
        /// 50900100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 50900100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _haroksid;
        /// <summary>
        /// 50900200 Идентификатор Набора характеристик (HAR_OKS_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50900200)]
        public long HarOKSId
        {
            get
            {
                CheckPropertyInited("HarOKSId");
                return _haroksid;
            }
            set
            {
                _haroksid = value;
                NotifyPropertyChanged("HarOKSId");
            }
        }


        private string _haroksname;
        /// <summary>
        /// 50900300 Наименование характеристики (HAR_OKS_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50900300)]
        public string HarOKSName
        {
            get
            {
                CheckPropertyInited("HarOKSName");
                return _haroksname;
            }
            set
            {
                _haroksname = value;
                NotifyPropertyChanged("HarOKSName");
            }
        }


        private string _harstatus;
        /// <summary>
        /// 50900400 Статус характеристики ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50900400)]
        public string HarStatus
        {
            get
            {
                CheckPropertyInited("HarStatus");
                return _harstatus;
            }
            set
            {
                _harstatus = value;
                NotifyPropertyChanged("HarStatus");
            }
        }


        private ObjectModel.Directory.Declarations.HarStatus _harstatus_Code;
        /// <summary>
        /// 50900400 Статус характеристики (справочный код) (HAR_STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 50900400)]
        public ObjectModel.Directory.Declarations.HarStatus HarStatus_Code
        {
            get
            {
                CheckPropertyInited("HarStatus_Code");
                return this._harstatus_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_harstatus))
                    {
                         _harstatus = descr;
                    }
                }
                else
                {
                     _harstatus = descr;
                }

                this._harstatus_Code = value;
                NotifyPropertyChanged("HarStatus");
                NotifyPropertyChanged("HarStatus_Code");
            }
        }


        private bool? _isusedindeclaration;
        /// <summary>
        /// 50900500 Характеристика отображается в декларации (IS_USED_IN_DECLARATION)
        /// </summary>
        [RegisterAttribute(AttributeID = 50900500)]
        public bool? IsUsedInDeclaration
        {
            get
            {
                CheckPropertyInited("IsUsedInDeclaration");
                return _isusedindeclaration;
            }
            set
            {
                _isusedindeclaration = value;
                NotifyPropertyChanged("IsUsedInDeclaration");
            }
        }

    }
}

namespace ObjectModel.Declarations
{
    /// <summary>
    /// 510 Характеристики ЗУ. Дополнительная информация (DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO)
    /// </summary>
    [RegisterInfo(RegisterID = 510)]
    [Serializable]
    public partial class OMHarParcelAdditionalInfo : OMBaseClass<OMHarParcelAdditionalInfo>
    {

        private long _id;
        /// <summary>
        /// 51000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 51000100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _harparcelid;
        /// <summary>
        /// 51000200 Идентификатор Набора характеристик (HAR_PARCEL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 51000200)]
        public long HarParcelId
        {
            get
            {
                CheckPropertyInited("HarParcelId");
                return _harparcelid;
            }
            set
            {
                _harparcelid = value;
                NotifyPropertyChanged("HarParcelId");
            }
        }


        private string _harparcelname;
        /// <summary>
        /// 51000300 Наименование характеристики (HAR_PARCEL_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 51000300)]
        public string HarParcelName
        {
            get
            {
                CheckPropertyInited("HarParcelName");
                return _harparcelname;
            }
            set
            {
                _harparcelname = value;
                NotifyPropertyChanged("HarParcelName");
            }
        }


        private string _harstatus;
        /// <summary>
        /// 51000400 Статус характеристики ()
        /// </summary>
        [RegisterAttribute(AttributeID = 51000400)]
        public string HarStatus
        {
            get
            {
                CheckPropertyInited("HarStatus");
                return _harstatus;
            }
            set
            {
                _harstatus = value;
                NotifyPropertyChanged("HarStatus");
            }
        }


        private ObjectModel.Directory.Declarations.HarStatus _harstatus_Code;
        /// <summary>
        /// 51000400 Статус характеристики (справочный код) (HAR_STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 51000400)]
        public ObjectModel.Directory.Declarations.HarStatus HarStatus_Code
        {
            get
            {
                CheckPropertyInited("HarStatus_Code");
                return this._harstatus_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_harstatus))
                    {
                         _harstatus = descr;
                    }
                }
                else
                {
                     _harstatus = descr;
                }

                this._harstatus_Code = value;
                NotifyPropertyChanged("HarStatus");
                NotifyPropertyChanged("HarStatus_Code");
            }
        }


        private bool? _isusedindeclaration;
        /// <summary>
        /// 51000500 Характеристика отображается в декларации (IS_USED_IN_DECLARATION)
        /// </summary>
        [RegisterAttribute(AttributeID = 51000500)]
        public bool? IsUsedInDeclaration
        {
            get
            {
                CheckPropertyInited("IsUsedInDeclaration");
                return _isusedindeclaration;
            }
            set
            {
                _isusedindeclaration = value;
                NotifyPropertyChanged("IsUsedInDeclaration");
            }
        }

    }
}

namespace ObjectModel.ES
{
    /// <summary>
    /// 600 Экспресс оценка (ES_EXPRESS_SCORE)
    /// </summary>
    [RegisterInfo(RegisterID = 600)]
    [Serializable]
    public partial class OMExpressScore : OMBaseClass<OMExpressScore>
    {

        private long _id;
        /// <summary>
        /// 60000100 Идентификатор (Id)
        /// </summary>
        [PrimaryKey(AttributeID = 60000100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _kadastralnumber;
        /// <summary>
        /// 60000200 Кадастровый номер (KN)
        /// </summary>
        [RegisterAttribute(AttributeID = 60000200)]
        public string KadastralNumber
        {
            get
            {
                CheckPropertyInited("KadastralNumber");
                return _kadastralnumber;
            }
            set
            {
                _kadastralnumber = value;
                NotifyPropertyChanged("KadastralNumber");
            }
        }


        private DateTime? _datecost;
        /// <summary>
        /// 60000300 Дата оценки (DATE_COST)
        /// </summary>
        [RegisterAttribute(AttributeID = 60000300)]
        public DateTime? DateCost
        {
            get
            {
                CheckPropertyInited("DateCost");
                return _datecost;
            }
            set
            {
                _datecost = value;
                NotifyPropertyChanged("DateCost");
            }
        }


        private decimal? _summarycost;
        /// <summary>
        /// 60000400 Общая стоимость (SUMMARY_COST)
        /// </summary>
        [RegisterAttribute(AttributeID = 60000400)]
        public decimal? SummaryCost
        {
            get
            {
                CheckPropertyInited("SummaryCost");
                return _summarycost;
            }
            set
            {
                _summarycost = value;
                NotifyPropertyChanged("SummaryCost");
            }
        }


        private decimal? _costsquaremeter;
        /// <summary>
        /// 60000500 Стоимость за квадратный метр (COST_SQUARE_METER)
        /// </summary>
        [RegisterAttribute(AttributeID = 60000500)]
        public decimal? CostSquareMeter
        {
            get
            {
                CheckPropertyInited("CostSquareMeter");
                return _costsquaremeter;
            }
            set
            {
                _costsquaremeter = value;
                NotifyPropertyChanged("CostSquareMeter");
            }
        }


        private decimal _square;
        /// <summary>
        /// 60000600 Площадь (SQUARE)
        /// </summary>
        [RegisterAttribute(AttributeID = 60000600)]
        public decimal Square
        {
            get
            {
                CheckPropertyInited("Square");
                return _square;
            }
            set
            {
                _square = value;
                NotifyPropertyChanged("Square");
            }
        }


        private long _floor;
        /// <summary>
        /// 60000700 Этаж (FLOOR)
        /// </summary>
        [RegisterAttribute(AttributeID = 60000700)]
        public long Floor
        {
            get
            {
                CheckPropertyInited("Floor");
                return _floor;
            }
            set
            {
                _floor = value;
                NotifyPropertyChanged("Floor");
            }
        }


        private long _objectid;
        /// <summary>
        /// 60000800 Ид объекта из таблицы ОКС (OBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 60000800)]
        public long Objectid
        {
            get
            {
                CheckPropertyInited("Objectid");
                return _objectid;
            }
            set
            {
                _objectid = value;
                NotifyPropertyChanged("Objectid");
            }
        }


        private string _scenariotype;
        /// <summary>
        /// 60000900 Тип сценария расчета ()
        /// </summary>
        [RegisterAttribute(AttributeID = 60000900)]
        public string ScenarioType
        {
            get
            {
                CheckPropertyInited("ScenarioType");
                return _scenariotype;
            }
            set
            {
                _scenariotype = value;
                NotifyPropertyChanged("ScenarioType");
            }
        }


        private ObjectModel.Directory.ES.ScenarioType _scenariotype_Code;
        /// <summary>
        /// 60000900 Тип сценария расчета (справочный код) (SCENARIO_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 60000900)]
        public ObjectModel.Directory.ES.ScenarioType ScenarioType_Code
        {
            get
            {
                CheckPropertyInited("ScenarioType_Code");
                return this._scenariotype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_scenariotype))
                    {
                         _scenariotype = descr;
                    }
                }
                else
                {
                     _scenariotype = descr;
                }

                this._scenariotype_Code = value;
                NotifyPropertyChanged("ScenarioType");
                NotifyPropertyChanged("ScenarioType_Code");
            }
        }


        private string _dealtype;
        /// <summary>
        /// 60001000 Тип сделки ()
        /// </summary>
        [RegisterAttribute(AttributeID = 60001000)]
        public string DealType
        {
            get
            {
                CheckPropertyInited("DealType");
                return _dealtype;
            }
            set
            {
                _dealtype = value;
                NotifyPropertyChanged("DealType");
            }
        }


        private DealType _dealtype_Code;
        /// <summary>
        /// 60001000 Тип сделки (справочный код) (DEAL_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 60001000)]
        public DealType DealType_Code
        {
            get
            {
                CheckPropertyInited("DealType_Code");
                return this._dealtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_dealtype))
                    {
                         _dealtype = descr;
                    }
                }
                else
                {
                     _dealtype = descr;
                }

                this._dealtype_Code = value;
                NotifyPropertyChanged("DealType");
                NotifyPropertyChanged("DealType_Code");
            }
        }


        private string _segmenttype;
        /// <summary>
        /// 60001100 Тип сегмента ()
        /// </summary>
        [RegisterAttribute(AttributeID = 60001100)]
        public string SegmentType
        {
            get
            {
                CheckPropertyInited("SegmentType");
                return _segmenttype;
            }
            set
            {
                _segmenttype = value;
                NotifyPropertyChanged("SegmentType");
            }
        }


        private MarketSegment _segmenttype_Code;
        /// <summary>
        /// 60001100 Тип сегмента (справочный код) (SEGMENT_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 60001100)]
        public MarketSegment SegmentType_Code
        {
            get
            {
                CheckPropertyInited("SegmentType_Code");
                return this._segmenttype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_segmenttype))
                    {
                         _segmenttype = descr;
                    }
                }
                else
                {
                     _segmenttype = descr;
                }

                this._segmenttype_Code = value;
                NotifyPropertyChanged("SegmentType");
                NotifyPropertyChanged("SegmentType_Code");
            }
        }


        private string _address;
        /// <summary>
        /// 60001200 Адрес (ADDRESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 60001200)]
        public string Address
        {
            get
            {
                CheckPropertyInited("Address");
                return _address;
            }
            set
            {
                _address = value;
                NotifyPropertyChanged("Address");
            }
        }


        private long? _targetmarketobjectid;
        /// <summary>
        /// 60001300 ИД целевого объекта из market_core_object (TARGET_MARKET_OBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 60001300)]
        public long? TargetMarketObjectId
        {
            get
            {
                CheckPropertyInited("TargetMarketObjectId");
                return _targetmarketobjectid;
            }
            set
            {
                _targetmarketobjectid = value;
                NotifyPropertyChanged("TargetMarketObjectId");
            }
        }

    }
}

namespace ObjectModel.ES
{
    /// <summary>
    /// 601 Экспресс оценка. Год постройки (ES_YEAR_CONSTRUCTION)
    /// </summary>
    [RegisterInfo(RegisterID = 601)]
    [Serializable]
    public partial class OMYearConstruction : OMBaseClass<OMYearConstruction>
    {

        private long _id;
        /// <summary>
        /// 60100100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 60100100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _numberrange;
        /// <summary>
        /// 60100200 Номер диапазона (NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 60100200)]
        public long NumberRange
        {
            get
            {
                CheckPropertyInited("NumberRange");
                return _numberrange;
            }
            set
            {
                _numberrange = value;
                NotifyPropertyChanged("NumberRange");
            }
        }


        private long _yearfrom;
        /// <summary>
        /// 60100300 Год постройки от (YEAR_FROM)
        /// </summary>
        [RegisterAttribute(AttributeID = 60100300)]
        public long YearFrom
        {
            get
            {
                CheckPropertyInited("YearFrom");
                return _yearfrom;
            }
            set
            {
                _yearfrom = value;
                NotifyPropertyChanged("YearFrom");
            }
        }


        private long _yearto;
        /// <summary>
        /// 60100400 Год постройки до (YEAR_TO)
        /// </summary>
        [RegisterAttribute(AttributeID = 60100400)]
        public long YearTo
        {
            get
            {
                CheckPropertyInited("YearTo");
                return _yearto;
            }
            set
            {
                _yearto = value;
                NotifyPropertyChanged("YearTo");
            }
        }

    }
}

namespace ObjectModel.ES
{
    /// <summary>
    /// 602 Экспресс оценка. Площадь помещений (ES_SQUARE)
    /// </summary>
    [RegisterInfo(RegisterID = 602)]
    [Serializable]
    public partial class OMSquare : OMBaseClass<OMSquare>
    {

        private long _id;
        /// <summary>
        /// 60200100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 60200100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _numberrange;
        /// <summary>
        /// 60200200 Номер диапозона (NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 60200200)]
        public long NumberRange
        {
            get
            {
                CheckPropertyInited("NumberRange");
                return _numberrange;
            }
            set
            {
                _numberrange = value;
                NotifyPropertyChanged("NumberRange");
            }
        }


        private long _squarefrom;
        /// <summary>
        /// 60200300 Площадь от (SQUARE_FROM)
        /// </summary>
        [RegisterAttribute(AttributeID = 60200300)]
        public long SquareFrom
        {
            get
            {
                CheckPropertyInited("SquareFrom");
                return _squarefrom;
            }
            set
            {
                _squarefrom = value;
                NotifyPropertyChanged("SquareFrom");
            }
        }


        private long _squareto;
        /// <summary>
        /// 60200400 Площадь до (SQUARE_TO)
        /// </summary>
        [RegisterAttribute(AttributeID = 60200400)]
        public long SquareTo
        {
            get
            {
                CheckPropertyInited("SquareTo");
                return _squareto;
            }
            set
            {
                _squareto = value;
                NotifyPropertyChanged("SquareTo");
            }
        }

    }
}

namespace ObjectModel.ES
{
    /// <summary>
    /// 603 Экспресс оценка. Материал стен (ES_WALL_MATERIAL)
    /// </summary>
    [RegisterInfo(RegisterID = 603)]
    [Serializable]
    public partial class OMWallMaterial : OMBaseClass<OMWallMaterial>
    {

        private long _id;
        /// <summary>
        /// 60300100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 60300100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _wallmaterial;
        /// <summary>
        /// 60300200 Материал стен (WALL_MATERIAL)
        /// </summary>
        [RegisterAttribute(AttributeID = 60300200)]
        public string WallMaterial
        {
            get
            {
                CheckPropertyInited("WallMaterial");
                return _wallmaterial;
            }
            set
            {
                _wallmaterial = value;
                NotifyPropertyChanged("WallMaterial");
            }
        }


        private long _mark;
        /// <summary>
        /// 60300300 Метка (MARK)
        /// </summary>
        [RegisterAttribute(AttributeID = 60300300)]
        public long Mark
        {
            get
            {
                CheckPropertyInited("Mark");
                return _mark;
            }
            set
            {
                _mark = value;
                NotifyPropertyChanged("Mark");
            }
        }

    }
}

namespace ObjectModel.ES
{
    /// <summary>
    /// 608 Связь экспресс оценки и объектов аналогов (ES_TO_MARKET_CORE_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 608)]
    [Serializable]
    public partial class OMEsToMarketCoreObject : OMBaseClass<OMEsToMarketCoreObject>
    {

        private long _id;
        /// <summary>
        /// 60800100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 60800100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _esid;
        /// <summary>
        /// 60800200 Идентификатор экспресс оценки (ES_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 60800200)]
        public long EsId
        {
            get
            {
                CheckPropertyInited("EsId");
                return _esid;
            }
            set
            {
                _esid = value;
                NotifyPropertyChanged("EsId");
            }
        }


        private long _marketobjectid;
        /// <summary>
        /// 60800300 Идентификатор объктов аналогов (MARKET_OBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 60800300)]
        public long MarketObjectId
        {
            get
            {
                CheckPropertyInited("MarketObjectId");
                return _marketobjectid;
            }
            set
            {
                _marketobjectid = value;
                NotifyPropertyChanged("MarketObjectId");
            }
        }

    }
}

namespace ObjectModel.ES
{
    /// <summary>
    /// 609 Экспресс оценка. Справочники (ES_REFERENCE)
    /// </summary>
    [RegisterInfo(RegisterID = 609)]
    [Serializable]
    public partial class OMEsReference : OMBaseClass<OMEsReference>
    {

        private long _id;
        /// <summary>
        /// 60900100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 60900100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _name;
        /// <summary>
        /// 60900200 Наименование справочника (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 60900200)]
        public string Name
        {
            get
            {
                CheckPropertyInited("Name");
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }


        private string _valuetype;
        /// <summary>
        /// 60900300 Тип значения (VALUE_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 60900300)]
        public string ValueType
        {
            get
            {
                CheckPropertyInited("ValueType");
                return _valuetype;
            }
            set
            {
                _valuetype = value;
                NotifyPropertyChanged("ValueType");
            }
        }


        private ObjectModel.Directory.ES.ReferenceItemCodeType _valuetype_Code;
        /// <summary>
        /// 60900300 Тип значения (справочный код) (VALUE_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 60900300)]
        public ObjectModel.Directory.ES.ReferenceItemCodeType ValueType_Code
        {
            get
            {
                CheckPropertyInited("ValueType_Code");
                return this._valuetype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_valuetype))
                    {
                         _valuetype = descr;
                    }
                }
                else
                {
                     _valuetype = descr;
                }

                this._valuetype_Code = value;
                NotifyPropertyChanged("ValueType");
                NotifyPropertyChanged("ValueType_Code");
            }
        }

    }
}

namespace ObjectModel.ES
{
    /// <summary>
    /// 610 Экспресс оценка. Значения справочников (ES_REFERENCE_ITEM)
    /// </summary>
    [RegisterInfo(RegisterID = 610)]
    [Serializable]
    public partial class OMEsReferenceItem : OMBaseClass<OMEsReferenceItem>
    {

        private long _id;
        /// <summary>
        /// 61000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 61000100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _referenceid;
        /// <summary>
        /// 61000200 Идентификатор справочника (ES_REFERENCE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 61000200)]
        public long ReferenceId
        {
            get
            {
                CheckPropertyInited("ReferenceId");
                return _referenceid;
            }
            set
            {
                _referenceid = value;
                NotifyPropertyChanged("ReferenceId");
            }
        }


        private string _value;
        /// <summary>
        /// 61000300 Значение (VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 61000300)]
        public string Value
        {
            get
            {
                CheckPropertyInited("Value");
                return _value;
            }
            set
            {
                _value = value;
                NotifyPropertyChanged("Value");
            }
        }


        private decimal? _calculationvalue;
        /// <summary>
        /// 61000500 Значение для расчета (CALCULATION_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 61000500)]
        public decimal? CalculationValue
        {
            get
            {
                CheckPropertyInited("CalculationValue");
                return _calculationvalue;
            }
            set
            {
                _calculationvalue = value;
                NotifyPropertyChanged("CalculationValue");
            }
        }

    }
}

namespace ObjectModel.Es
{
    /// <summary>
    /// 611 Экспресс оценка. Настройка параметров (ES_SETTINGS_PARAMS)
    /// </summary>
    [RegisterInfo(RegisterID = 611)]
    [Serializable]
    public partial class OMSettingsParams : OMBaseClass<OMSettingsParams>
    {

        private long _id;
        /// <summary>
        /// 61100100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 61100100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _tourid;
        /// <summary>
        /// 61100200 Идентификатор тура (ID_TOUR)
        /// </summary>
        [RegisterAttribute(AttributeID = 61100200)]
        public long TourId
        {
            get
            {
                CheckPropertyInited("TourId");
                return _tourid;
            }
            set
            {
                _tourid = value;
                NotifyPropertyChanged("TourId");
            }
        }


        private long _registerid;
        /// <summary>
        /// 61100300 Идентификатор реестра атрибутов КО (ID_REGISTER)
        /// </summary>
        [RegisterAttribute(AttributeID = 61100300)]
        public long Registerid
        {
            get
            {
                CheckPropertyInited("Registerid");
                return _registerid;
            }
            set
            {
                _registerid = value;
                NotifyPropertyChanged("Registerid");
            }
        }


        private string _costfacrors;
        /// <summary>
        /// 61100400 Оценочные факторы  (COST_FACTORS)
        /// </summary>
        [RegisterAttribute(AttributeID = 61100400)]
        public string CostFacrors
        {
            get
            {
                CheckPropertyInited("CostFacrors");
                return _costfacrors;
            }
            set
            {
                _costfacrors = value;
                NotifyPropertyChanged("CostFacrors");
            }
        }


        private string _segmenttype;
        /// <summary>
        /// 61100500 Тип сегмента ()
        /// </summary>
        [RegisterAttribute(AttributeID = 61100500)]
        public string SegmentType
        {
            get
            {
                CheckPropertyInited("SegmentType");
                return _segmenttype;
            }
            set
            {
                _segmenttype = value;
                NotifyPropertyChanged("SegmentType");
            }
        }


        private MarketSegment _segmenttype_Code;
        /// <summary>
        /// 61100500 Тип сегмента (справочный код) (SEGMENT_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 61100500)]
        public MarketSegment SegmentType_Code
        {
            get
            {
                CheckPropertyInited("SegmentType_Code");
                return this._segmenttype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_segmenttype))
                    {
                         _segmenttype = descr;
                    }
                }
                else
                {
                     _segmenttype = descr;
                }

                this._segmenttype_Code = value;
                NotifyPropertyChanged("SegmentType");
                NotifyPropertyChanged("SegmentType_Code");
            }
        }


        private long? _buildcadnumber;
        /// <summary>
        /// 61100600 Идентификатор атрибута кадастрового номера строения (BUILD_CAD_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 61100600)]
        public long? BuildCadNumber
        {
            get
            {
                CheckPropertyInited("BuildCadNumber");
                return _buildcadnumber;
            }
            set
            {
                _buildcadnumber = value;
                NotifyPropertyChanged("BuildCadNumber");
            }
        }

    }
}

namespace ObjectModel.ES
{
    /// <summary>
    /// 612 Экспресс оценка. Значения объекта оценки (ES_TARGET_OBJECT_VALUE)
    /// </summary>
    [RegisterInfo(RegisterID = 612)]
    [Serializable]
    public partial class OMTargetObjectValue : OMBaseClass<OMTargetObjectValue>
    {

        private long _unitid;
        /// <summary>
        /// 61200100 Идентификатор (UNIT_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 61200100)]
        public long UnitId
        {
            get
            {
                CheckPropertyInited("UnitId");
                return _unitid;
            }
            set
            {
                _unitid = value;
                NotifyPropertyChanged("UnitId");
            }
        }


        private string _attributevalue;
        /// <summary>
        /// 61200200 Значения атрибутов (ATTRIBUTE_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 61200200)]
        public string AttributeValue
        {
            get
            {
                CheckPropertyInited("AttributeValue");
                return _attributevalue;
            }
            set
            {
                _attributevalue = value;
                NotifyPropertyChanged("AttributeValue");
            }
        }

    }
}

namespace ObjectModel.Modeling
{
    /// <summary>
    /// 702 Связь модели и объектов аналогов (MODELING_MODEL_TO_MARKET_OBJECTS)
    /// </summary>
    [RegisterInfo(RegisterID = 702)]
    [Serializable]
    public partial class OMModelToMarketObjects : OMBaseClass<OMModelToMarketObjects>
    {

        private long _id;
        /// <summary>
        /// 70200100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 70200100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _cadastralnumber;
        /// <summary>
        /// 70200200 Кадастровый номер объекта (CADASTRAL_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 70200200)]
        public string CadastralNumber
        {
            get
            {
                CheckPropertyInited("CadastralNumber");
                return _cadastralnumber;
            }
            set
            {
                _cadastralnumber = value;
                NotifyPropertyChanged("CadastralNumber");
            }
        }


        private decimal _price;
        /// <summary>
        /// 70200300 Цена объекта (PRICE)
        /// </summary>
        [RegisterAttribute(AttributeID = 70200300)]
        public decimal Price
        {
            get
            {
                CheckPropertyInited("Price");
                return _price;
            }
            set
            {
                _price = value;
                NotifyPropertyChanged("Price");
            }
        }


        private bool? _isexcluded;
        /// <summary>
        /// 70200400 Исключение объекта из расчета (IS_EXCLUDED)
        /// </summary>
        [RegisterAttribute(AttributeID = 70200400)]
        public bool? IsExcluded
        {
            get
            {
                CheckPropertyInited("IsExcluded");
                return _isexcluded;
            }
            set
            {
                _isexcluded = value;
                NotifyPropertyChanged("IsExcluded");
            }
        }


        private long _modelid;
        /// <summary>
        /// 70200500 Идентификатор модели (MODEL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 70200500)]
        public long ModelId
        {
            get
            {
                CheckPropertyInited("ModelId");
                return _modelid;
            }
            set
            {
                _modelid = value;
                NotifyPropertyChanged("ModelId");
            }
        }


        private string _coefficients;
        /// <summary>
        /// 70200600 Рассчитанные коэффициенты для объекта (COEFFICIENTS)
        /// </summary>
        [RegisterAttribute(AttributeID = 70200600)]
        public string Coefficients
        {
            get
            {
                CheckPropertyInited("Coefficients");
                return _coefficients;
            }
            set
            {
                _coefficients = value;
                NotifyPropertyChanged("Coefficients");
            }
        }


        private bool? _isfortraining;
        /// <summary>
        /// 70200700 Признак: используется ли объект для обучения модели (IS_FOR_TRAINING)
        /// </summary>
        [RegisterAttribute(AttributeID = 70200700)]
        public bool? IsForTraining
        {
            get
            {
                CheckPropertyInited("IsForTraining");
                return _isfortraining;
            }
            set
            {
                _isfortraining = value;
                NotifyPropertyChanged("IsForTraining");
            }
        }


        private decimal? _pricefrommodel;
        /// <summary>
        /// 70200800 Цена объекта, спрогнозированная моделью (PRICE_FROM_MODEL)
        /// </summary>
        [RegisterAttribute(AttributeID = 70200800)]
        public decimal? PriceFromModel
        {
            get
            {
                CheckPropertyInited("PriceFromModel");
                return _pricefrommodel;
            }
            set
            {
                _pricefrommodel = value;
                NotifyPropertyChanged("PriceFromModel");
            }
        }

    }
}

namespace ObjectModel.Common
{
    /// <summary>
    /// 800 Журнал выгрузки данных (COMMON_EXPORT_BY_TEMPLATES)
    /// </summary>
    [RegisterInfo(RegisterID = 800)]
    [Serializable]
    public partial class OMExportByTemplates : OMBaseClass<OMExportByTemplates>
    {

        private long _id;
        /// <summary>
        /// 80000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 80000100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _userid;
        /// <summary>
        /// 80000200 Идентификатор пользователя (USER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 80000200)]
        public long UserId
        {
            get
            {
                CheckPropertyInited("UserId");
                return _userid;
            }
            set
            {
                _userid = value;
                NotifyPropertyChanged("UserId");
            }
        }


        private long _status;
        /// <summary>
        /// 80000300 Статус (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 80000300)]
        public long Status
        {
            get
            {
                CheckPropertyInited("Status");
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }


        private DateTime _datecreated;
        /// <summary>
        /// 80000400 Дата создания (DATE_CREATED)
        /// </summary>
        [RegisterAttribute(AttributeID = 80000400)]
        public DateTime DateCreated
        {
            get
            {
                CheckPropertyInited("DateCreated");
                return _datecreated;
            }
            set
            {
                _datecreated = value;
                NotifyPropertyChanged("DateCreated");
            }
        }


        private DateTime? _datestarted;
        /// <summary>
        /// 80000500 Дата запуска (DATE_STARTED)
        /// </summary>
        [RegisterAttribute(AttributeID = 80000500)]
        public DateTime? DateStarted
        {
            get
            {
                CheckPropertyInited("DateStarted");
                return _datestarted;
            }
            set
            {
                _datestarted = value;
                NotifyPropertyChanged("DateStarted");
            }
        }


        private DateTime? _datefinished;
        /// <summary>
        /// 80000600 Дата завершения (DATE_FINISHED)
        /// </summary>
        [RegisterAttribute(AttributeID = 80000600)]
        public DateTime? DateFinished
        {
            get
            {
                CheckPropertyInited("DateFinished");
                return _datefinished;
            }
            set
            {
                _datefinished = value;
                NotifyPropertyChanged("DateFinished");
            }
        }


        private string _templatefilename;
        /// <summary>
        /// 80000700 Имя файла шаблона (TEMPLATE_FILE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 80000700)]
        public string TemplateFileName
        {
            get
            {
                CheckPropertyInited("TemplateFileName");
                return _templatefilename;
            }
            set
            {
                _templatefilename = value;
                NotifyPropertyChanged("TemplateFileName");
            }
        }


        private string _columnsmapping;
        /// <summary>
        /// 80000800 Параметры соответствия колонок и показателей (COLUMNS_MAPPING)
        /// </summary>
        [RegisterAttribute(AttributeID = 80000800)]
        public string ColumnsMapping
        {
            get
            {
                CheckPropertyInited("ColumnsMapping");
                return _columnsmapping;
            }
            set
            {
                _columnsmapping = value;
                NotifyPropertyChanged("ColumnsMapping");
            }
        }


        private long? _errorid;
        /// <summary>
        /// 80000900 ИД Ошибки (ERROR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 80000900)]
        public long? ErrorId
        {
            get
            {
                CheckPropertyInited("ErrorId");
                return _errorid;
            }
            set
            {
                _errorid = value;
                NotifyPropertyChanged("ErrorId");
            }
        }


        private string _resultmessage;
        /// <summary>
        /// 80001000 Результирующее сообщение (RESULT_MESSAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 80001000)]
        public string ResultMessage
        {
            get
            {
                CheckPropertyInited("ResultMessage");
                return _resultmessage;
            }
            set
            {
                _resultmessage = value;
                NotifyPropertyChanged("ResultMessage");
            }
        }


        private long _mainregisterid;
        /// <summary>
        /// 80001100 ИД основного реестра (MAIN_REGISTER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 80001100)]
        public long MainRegisterId
        {
            get
            {
                CheckPropertyInited("MainRegisterId");
                return _mainregisterid;
            }
            set
            {
                _mainregisterid = value;
                NotifyPropertyChanged("MainRegisterId");
            }
        }


        private string _registerviewid;
        /// <summary>
        /// 80001200 ИД представления реестра (REGISTER_VIEW_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 80001200)]
        public string RegisterViewId
        {
            get
            {
                CheckPropertyInited("RegisterViewId");
                return _registerviewid;
            }
            set
            {
                _registerviewid = value;
                NotifyPropertyChanged("RegisterViewId");
            }
        }


        private string _resultfilename;
        /// <summary>
        /// 80001300 Имя результирующего файла (RESULT_FILE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 80001300)]
        public string ResultFileName
        {
            get
            {
                CheckPropertyInited("ResultFileName");
                return _resultfilename;
            }
            set
            {
                _resultfilename = value;
                NotifyPropertyChanged("ResultFileName");
            }
        }


        private string _fileextension;
        /// <summary>
        /// 80001400 Тип файла (FILE_EXTENSION)
        /// </summary>
        [RegisterAttribute(AttributeID = 80001400)]
        public string FileExtension
        {
            get
            {
                CheckPropertyInited("FileExtension");
                return _fileextension;
            }
            set
            {
                _fileextension = value;
                NotifyPropertyChanged("FileExtension");
            }
        }


        private string _filetemplatetitle;
        /// <summary>
        /// 80001500 Наименование файла шаблона (FILE_TEMPLATE_TITLE)
        /// </summary>
        [RegisterAttribute(AttributeID = 80001500)]
        public string FileTemplateTitle
        {
            get
            {
                CheckPropertyInited("FileTemplateTitle");
                return _filetemplatetitle;
            }
            set
            {
                _filetemplatetitle = value;
                NotifyPropertyChanged("FileTemplateTitle");
            }
        }


        private string _fileresulttitle;
        /// <summary>
        /// 80001600 Наименование результирующего файла (FILE_RESULT_TITLE)
        /// </summary>
        [RegisterAttribute(AttributeID = 80001600)]
        public string FileResultTitle
        {
            get
            {
                CheckPropertyInited("FileResultTitle");
                return _fileresulttitle;
            }
            set
            {
                _fileresulttitle = value;
                NotifyPropertyChanged("FileResultTitle");
            }
        }

    }
}

namespace ObjectModel.Common
{
    /// <summary>
    /// 801 Журнал загрузки данных (COMMON_IMPORT_DATA_LOG)
    /// </summary>
    [RegisterInfo(RegisterID = 801)]
    [Serializable]
    public partial class OMImportDataLog : OMBaseClass<OMImportDataLog>
    {

        private long _id;
        /// <summary>
        /// 80100100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 80100100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long _userid;
        /// <summary>
        /// 80100200 Идентификатор пользователя (USER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 80100200)]
        public long UserId
        {
            get
            {
                CheckPropertyInited("UserId");
                return _userid;
            }
            set
            {
                _userid = value;
                NotifyPropertyChanged("UserId");
            }
        }


        private string _status;
        /// <summary>
        /// 80100300 Статус ()
        /// </summary>
        [RegisterAttribute(AttributeID = 80100300)]
        public string Status
        {
            get
            {
                CheckPropertyInited("Status");
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }


        private ObjectModel.Directory.Common.ImportStatus _status_Code;
        /// <summary>
        /// 80100300 Статус (справочный код) (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 80100300)]
        public ObjectModel.Directory.Common.ImportStatus Status_Code
        {
            get
            {
                CheckPropertyInited("Status_Code");
                return this._status_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_status))
                    {
                         _status = descr;
                    }
                }
                else
                {
                     _status = descr;
                }

                this._status_Code = value;
                NotifyPropertyChanged("Status");
                NotifyPropertyChanged("Status_Code");
            }
        }


        private DateTime _datecreated;
        /// <summary>
        /// 80100400 Дата создания (DATE_CREATED)
        /// </summary>
        [RegisterAttribute(AttributeID = 80100400)]
        public DateTime DateCreated
        {
            get
            {
                CheckPropertyInited("DateCreated");
                return _datecreated;
            }
            set
            {
                _datecreated = value;
                NotifyPropertyChanged("DateCreated");
            }
        }


        private DateTime? _datestarted;
        /// <summary>
        /// 80100500 Дата запуска (DATE_STARTED)
        /// </summary>
        [RegisterAttribute(AttributeID = 80100500)]
        public DateTime? DateStarted
        {
            get
            {
                CheckPropertyInited("DateStarted");
                return _datestarted;
            }
            set
            {
                _datestarted = value;
                NotifyPropertyChanged("DateStarted");
            }
        }


        private DateTime? _datefinished;
        /// <summary>
        /// 80100600 Дата завершения (DATE_FINISHED)
        /// </summary>
        [RegisterAttribute(AttributeID = 80100600)]
        public DateTime? DateFinished
        {
            get
            {
                CheckPropertyInited("DateFinished");
                return _datefinished;
            }
            set
            {
                _datefinished = value;
                NotifyPropertyChanged("DateFinished");
            }
        }


        private string _datafilename;
        /// <summary>
        /// 80100700 Имя файла данных (DATA_FILE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 80100700)]
        public string DataFileName
        {
            get
            {
                CheckPropertyInited("DataFileName");
                return _datafilename;
            }
            set
            {
                _datafilename = value;
                NotifyPropertyChanged("DataFileName");
            }
        }


        private string _columnsmapping;
        /// <summary>
        /// 80100800 Параметры соответствия колонок и показателей (COLUMNS_MAPPING)
        /// </summary>
        [RegisterAttribute(AttributeID = 80100800)]
        public string ColumnsMapping
        {
            get
            {
                CheckPropertyInited("ColumnsMapping");
                return _columnsmapping;
            }
            set
            {
                _columnsmapping = value;
                NotifyPropertyChanged("ColumnsMapping");
            }
        }


        private long? _errorid;
        /// <summary>
        /// 80100900 ИД Ошибки (ERROR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 80100900)]
        public long? ErrorId
        {
            get
            {
                CheckPropertyInited("ErrorId");
                return _errorid;
            }
            set
            {
                _errorid = value;
                NotifyPropertyChanged("ErrorId");
            }
        }


        private string _resultmessage;
        /// <summary>
        /// 80101000 Результирующее сообщение (RESULT_MESSAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 80101000)]
        public string ResultMessage
        {
            get
            {
                CheckPropertyInited("ResultMessage");
                return _resultmessage;
            }
            set
            {
                _resultmessage = value;
                NotifyPropertyChanged("ResultMessage");
            }
        }


        private long _mainregisterid;
        /// <summary>
        /// 80101100 ИД основного реестра (MAIN_REGISTER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 80101100)]
        public long MainRegisterId
        {
            get
            {
                CheckPropertyInited("MainRegisterId");
                return _mainregisterid;
            }
            set
            {
                _mainregisterid = value;
                NotifyPropertyChanged("MainRegisterId");
            }
        }


        private string _registerviewid;
        /// <summary>
        /// 80101200 ИД представления реестра (REGISTER_VIEW_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 80101200)]
        public string RegisterViewId
        {
            get
            {
                CheckPropertyInited("RegisterViewId");
                return _registerviewid;
            }
            set
            {
                _registerviewid = value;
                NotifyPropertyChanged("RegisterViewId");
            }
        }


        private long? _registerid;
        /// <summary>
        /// 80101300 ИД регистра (REGISTER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 80101300)]
        public long? RegisterId
        {
            get
            {
                CheckPropertyInited("RegisterId");
                return _registerid;
            }
            set
            {
                _registerid = value;
                NotifyPropertyChanged("RegisterId");
            }
        }


        private long? _objectid;
        /// <summary>
        /// 80101400 ИД объекта (OBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 80101400)]
        public long? ObjectId
        {
            get
            {
                CheckPropertyInited("ObjectId");
                return _objectid;
            }
            set
            {
                _objectid = value;
                NotifyPropertyChanged("ObjectId");
            }
        }


        private long? _totalnumberofobjects;
        /// <summary>
        /// 80101500 Общее число объектов в файле (TOTAL_NUMBER_OF_OBJECTS)
        /// </summary>
        [RegisterAttribute(AttributeID = 80101500)]
        public long? TotalNumberOfObjects
        {
            get
            {
                CheckPropertyInited("TotalNumberOfObjects");
                return _totalnumberofobjects;
            }
            set
            {
                _totalnumberofobjects = value;
                NotifyPropertyChanged("TotalNumberOfObjects");
            }
        }


        private long? _numberofimportedobjects;
        /// <summary>
        /// 80101600 Число загруженных объектов в файле (NUMBER_OF_IMPORTED_OBJECTS)
        /// </summary>
        [RegisterAttribute(AttributeID = 80101600)]
        public long? NumberOfImportedObjects
        {
            get
            {
                CheckPropertyInited("NumberOfImportedObjects");
                return _numberofimportedobjects;
            }
            set
            {
                _numberofimportedobjects = value;
                NotifyPropertyChanged("NumberOfImportedObjects");
            }
        }


        private long? _documentid;
        /// <summary>
        /// 80101700 ИД документа (DOCUMENT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 80101700)]
        public long? DocumentId
        {
            get
            {
                CheckPropertyInited("DocumentId");
                return _documentid;
            }
            set
            {
                _documentid = value;
                NotifyPropertyChanged("DocumentId");
            }
        }


        private string _resultfilename;
        /// <summary>
        /// 80101800 Имя результирующего файла (RESULT_FILE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 80101800)]
        public string ResultFileName
        {
            get
            {
                CheckPropertyInited("ResultFileName");
                return _resultfilename;
            }
            set
            {
                _resultfilename = value;
                NotifyPropertyChanged("ResultFileName");
            }
        }


        private string _fileextension;
        /// <summary>
        /// 80101900 Тип файла (FILE_EXTENSION)
        /// </summary>
        [RegisterAttribute(AttributeID = 80101900)]
        public string FileExtension
        {
            get
            {
                CheckPropertyInited("FileExtension");
                return _fileextension;
            }
            set
            {
                _fileextension = value;
                NotifyPropertyChanged("FileExtension");
            }
        }


        private string _datafiletitle;
        /// <summary>
        /// 80102000 Наименование файла данных (DATA_FILE_TITLE)
        /// </summary>
        [RegisterAttribute(AttributeID = 80102000)]
        public string DataFileTitle
        {
            get
            {
                CheckPropertyInited("DataFileTitle");
                return _datafiletitle;
            }
            set
            {
                _datafiletitle = value;
                NotifyPropertyChanged("DataFileTitle");
            }
        }


        private string _resultfiletitle;
        /// <summary>
        /// 80102100 Наименование результирующего файла (RESULT_FILE_TITLE)
        /// </summary>
        [RegisterAttribute(AttributeID = 80102100)]
        public string ResultFileTitle
        {
            get
            {
                CheckPropertyInited("ResultFileTitle");
                return _resultfiletitle;
            }
            set
            {
                _resultfiletitle = value;
                NotifyPropertyChanged("ResultFileTitle");
            }
        }

    }
}

namespace ObjectModel.Common
{
    /// <summary>
    /// 802 Сохраненные данные форм  (COMMON_DATA_FORM_STORAGE)
    /// </summary>
    [RegisterInfo(RegisterID = 802)]
    [Serializable]
    public partial class OMDataFormStorage : OMBaseClass<OMDataFormStorage>
    {

        private long _id;
        /// <summary>
        /// 80200100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 80200100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private long? _userid;
        /// <summary>
        /// 80200200 ИД пользователя (ID_USER)
        /// </summary>
        [RegisterAttribute(AttributeID = 80200200)]
        public long? UserId
        {
            get
            {
                CheckPropertyInited("UserId");
                return _userid;
            }
            set
            {
                _userid = value;
                NotifyPropertyChanged("UserId");
            }
        }


        private string _formtype;
        /// <summary>
        /// 80200300 Тип формы ()
        /// </summary>
        [RegisterAttribute(AttributeID = 80200300)]
        public string FormType
        {
            get
            {
                CheckPropertyInited("FormType");
                return _formtype;
            }
            set
            {
                _formtype = value;
                NotifyPropertyChanged("FormType");
            }
        }


        private ObjectModel.Directory.Common.DataFormStorege _formtype_Code;
        /// <summary>
        /// 80200300 Тип формы (справочный код) (FORMTYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 80200300)]
        public ObjectModel.Directory.Common.DataFormStorege FormType_Code
        {
            get
            {
                CheckPropertyInited("FormType_Code");
                return this._formtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_formtype))
                    {
                         _formtype = descr;
                    }
                }
                else
                {
                     _formtype = descr;
                }

                this._formtype_Code = value;
                NotifyPropertyChanged("FormType");
                NotifyPropertyChanged("FormType_Code");
            }
        }


        private string _data;
        /// <summary>
        /// 80200400 Данные (DATA_FORM)
        /// </summary>
        [RegisterAttribute(AttributeID = 80200400)]
        public string Data
        {
            get
            {
                CheckPropertyInited("Data");
                return _data;
            }
            set
            {
                _data = value;
                NotifyPropertyChanged("Data");
            }
        }


        private string _templatename;
        /// <summary>
        /// 80200500 Имя шаблона (TEMPLATE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 80200500)]
        public string TemplateName
        {
            get
            {
                CheckPropertyInited("TemplateName");
                return _templatename;
            }
            set
            {
                _templatename = value;
                NotifyPropertyChanged("TemplateName");
            }
        }


        private bool? _iscommon;
        /// <summary>
        /// 80200600 Признак общего шаблона (IS_COMMON)
        /// </summary>
        [RegisterAttribute(AttributeID = 80200600)]
        public bool? IsCommon
        {
            get
            {
                CheckPropertyInited("IsCommon");
                return _iscommon;
            }
            set
            {
                _iscommon = value;
                NotifyPropertyChanged("IsCommon");
            }
        }

    }
}
