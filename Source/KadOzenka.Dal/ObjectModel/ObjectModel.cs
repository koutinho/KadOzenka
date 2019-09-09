using System;
using Core.ObjectModel;
using Core.ObjectModel.CustomAttribute;
using Core.Shared.Extensions;
using ObjectModel.Directory;
namespace ObjectModel.Market
{
    /// <summary>
    /// 100 Таблица, содержащая объекты аналоги (MARKET_CORE_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 100)]
    [Serializable]
    public sealed partial class OMCoreObject : OMBaseClass<OMCoreObject>
    {

        private long _id;
        /// <summary>
        /// 10002000 Уникальный идентификатор объекта недвижимости сторонней площадки (ID)
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


        private long _marketcode;
        /// <summary>
        /// 10002200 Числовой идентификатор типа сторонней площадки (MARKET_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10002200)]
        public long MarketCode
        {
            get
            {
                CheckPropertyInited("MarketCode");
                return _marketcode;
            }
            set
            {
                _marketcode = value;
                NotifyPropertyChanged("MarketCode");
            }
        }


        private string _market;
        /// <summary>
        /// 10002300 Наименование сторонней площадки (MARKET)
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
        /// 10002300 Наименование сторонней площадки (справочный код) (MARKET_CODE)
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


        private long _propertytypecode;
        /// <summary>
        /// 10002400 Числовой иденификатор типа объекта недвижимости (PROPERTY_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10002400)]
        public long PropertyTypeCode
        {
            get
            {
                CheckPropertyInited("PropertyTypeCode");
                return _propertytypecode;
            }
            set
            {
                _propertytypecode = value;
                NotifyPropertyChanged("PropertyTypeCode");
            }
        }


        private string _propertytype;
        /// <summary>
        /// 10002500 Наименование типа объекта недвижимости (PROPERTY_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10002500)]
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
        /// 10002500 Наименование типа объекта недвижимости (справочный код) (PROPERTY_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10002500)]
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


        private long _marketid;
        /// <summary>
        /// 10002600 Уникальный идентификатор в рамках сторонней площадки (MARKET_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 10002600)]
        public long MarketId
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


        private long? _price;
        /// <summary>
        /// 10002700 Цена объекта недвижимости (PRICE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10002700)]
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


        private DateTime? _parsertime;
        /// <summary>
        /// 10002800 Время обнаружения объявления парсером (PARSER_TIME)
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
        /// 10002900 Название региона, к которому относится объект недвижимости (REGION)
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
        /// 10003000 Название города, к которому относится объект недвижимости (CITY)
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
        /// 10003100 Адрес объекта недвижимости (ADDRESS)
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
        /// 10003200 Ближайшие станции метро списком через запятую (METRO)
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
        /// 10003300 URL-адреса изображений объекта недвижимости списком через запятую (IMAGES)
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
        /// 10003400 Описание объекта недвижимости (DESCRIPTION)
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


        private decimal? _lng;
        /// <summary>
        /// 10003600 Долгота (LNG)
        /// </summary>
        [RegisterAttribute(AttributeID = 10003600)]
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

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 101 Таблица, содержащая объекты полученные с ЦИАНа (MARKET_CIAN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 101)]
    [Serializable]
    public sealed partial class OMCianObject : OMBaseClass<OMCianObject>
    {

        private long _id;
        /// <summary>
        /// 10100100 Уникальный идентификатор записи (ID)
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


        private long? _roomscount;
        /// <summary>
        /// 10100200 Количество комнат в объекте недвижимости (ROOMS_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10100200)]
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


        private long? _floornumber;
        /// <summary>
        /// 10100300 Этаж, на котором расположен объект недвижимости (FLOOR_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 10100300)]
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
        /// 10100400 Количество этажей в объекте недвижимости (FLOORS_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10100400)]
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
        /// 10100500 Общая площадь объекта недвижимости (AREA)
        /// </summary>
        [RegisterAttribute(AttributeID = 10100500)]
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
        /// 10100600 Площадь кухни объекта недвижимости (AREA_KITCHEN)
        /// </summary>
        [RegisterAttribute(AttributeID = 10100600)]
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
        /// 10100700 Жилая площадь объекта недвижимости (AREA_LIVING)
        /// </summary>
        [RegisterAttribute(AttributeID = 10100700)]
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
        /// 10100800 Площадь земельного участка, на котором расположен объект недвижимости (AREA_LAND)
        /// </summary>
        [RegisterAttribute(AttributeID = 10100800)]
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
        /// 10100900 Год постройки объекта недвижимости (BUILDING_YEAR)
        /// </summary>
        [RegisterAttribute(AttributeID = 10100900)]
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


        private string _dealtype;
        /// <summary>
        /// 10101000 Тип предлагаемой в объявлении сделки (DEAL_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10101000)]
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


        private string _category;
        /// <summary>
        /// 10101100 Категория, к которой относится объект недвижимости (CATEGORY)
        /// </summary>
        [RegisterAttribute(AttributeID = 10101100)]
        public string Category
        {
            get
            {
                CheckPropertyInited("Category");
                return _category;
            }
            set
            {
                _category = value;
                NotifyPropertyChanged("Category");
            }
        }


        private string _subcategory;
        /// <summary>
        /// 10101200 Подкатегория, к которой относится объект недвижимости (SUBCATEGORY)
        /// </summary>
        [RegisterAttribute(AttributeID = 10101200)]
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


        private long? _categoryid;
        /// <summary>
        /// 10101300 Идентификатор категории, к которой относится объект недвижимости (CATEGORY_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 10101300)]
        public long? CategoryId
        {
            get
            {
                CheckPropertyInited("CategoryId");
                return _categoryid;
            }
            set
            {
                _categoryid = value;
                NotifyPropertyChanged("CategoryId");
            }
        }


        private long? _regionid;
        /// <summary>
        /// 10101400 Идентификатор региона, к которому относится объект недвижимости (REGION_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 10101400)]
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
        /// 10101500 Идентификатор города, к которому относится объект недвижимости (CITY_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 10101500)]
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

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 102 Таблица, содержащая объекты полученные с авито (MARKET_AVITO_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 102)]
    [Serializable]
    public sealed partial class OMAvitoObject : OMBaseClass<OMAvitoObject>
    {

        private long _id;
        /// <summary>
        /// 10200100 Уникальный идентификатор объекта авито (ID)
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


        private string _title;
        /// <summary>
        /// 10200200 Заголовок объявления (TITLE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10200200)]
        public string Title
        {
            get
            {
                CheckPropertyInited("Title");
                return _title;
            }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }


        private string _district;
        /// <summary>
        /// 10200300 Название округа, к которому относится объект недвижимости (DISTRICT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10200300)]
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

    }
}
