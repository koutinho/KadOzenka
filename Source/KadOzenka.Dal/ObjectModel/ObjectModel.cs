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
    public partial class OMCoreObject : OMBaseClass<OMCoreObject>
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
    public partial class OMCianObject : OMBaseClass<OMCianObject>
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
    public partial class OMAvitoObject : OMBaseClass<OMAvitoObject>
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

namespace ObjectModel.Cld
{
    /// <summary>
    /// 304 Организации (CLD_SUBJECT_Q)
    /// </summary>
    [RegisterInfo(RegisterID = 304)]
    [Serializable]
    public partial class OMSubject : OMBaseClass<OMSubject>
    {

        private long _empid;
        /// <summary>
        /// 30400100 Идентификатор (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 30400100)]
        public long EmpId
        {
            get
            {
                CheckPropertyInited("EmpId");
                return _empid;
            }
            set
            {
                _empid = value;
                NotifyPropertyChanged("EmpId");
            }
        }


        private string _subjecttype;
        /// <summary>
        /// 30400200 Тип субъекта (SUBJECT_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30400200)]
        public string SubjectType
        {
            get
            {
                CheckPropertyInited("SubjectType");
                return _subjecttype;
            }
            set
            {
                _subjecttype = value;
                NotifyPropertyChanged("SubjectType");
            }
        }


        private long? _subjecttype_Code;
        /// <summary>
        /// 30400200 Тип субъекта (справочный код) (SUBJECT_TYPE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 30400200)]
        public long? SubjectType_Code
        {
            get
            {
                CheckPropertyInited("SubjectType_Code");
                return _subjecttype_Code;
            }
            set
            {
                _subjecttype_Code = value;
                NotifyPropertyChanged("SubjectType_Code");
            }
        }


        private DateTime? _downloaddate;
        /// <summary>
        /// 30400300 Дата загрузки сведений (DOWNLOAD_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30400300)]
        public DateTime? DownloadDate
        {
            get
            {
                CheckPropertyInited("DownloadDate");
                return _downloaddate;
            }
            set
            {
                _downloaddate = value;
                NotifyPropertyChanged("DownloadDate");
            }
        }


        private string _fullname;
        /// <summary>
        /// 30400400 Полное наименование (FULL_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 30400400)]
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


        private string _shortname;
        /// <summary>
        /// 30400500 Краткое наименование (SHORT_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 30400500)]
        public string ShortName
        {
            get
            {
                CheckPropertyInited("ShortName");
                return _shortname;
            }
            set
            {
                _shortname = value;
                NotifyPropertyChanged("ShortName");
            }
        }


        private string _shortorfullname;
        /// <summary>
        /// 30400501 Краткое или полное наименование ()
        /// </summary>
        [RegisterAttribute(AttributeID = 30400501)]
        public string ShortOrFullName
        {
            get
            {
                CheckPropertyInited("ShortOrFullName");
                return _shortorfullname;
            }
            set
            {
                _shortorfullname = value;
                NotifyPropertyChanged("ShortOrFullName");
            }
        }


        private string _inn;
        /// <summary>
        /// 30400600 ИНН (INN)
        /// </summary>
        [RegisterAttribute(AttributeID = 30400600)]
        public string Inn
        {
            get
            {
                CheckPropertyInited("Inn");
                return _inn;
            }
            set
            {
                _inn = value;
                NotifyPropertyChanged("Inn");
            }
        }


        private string _kpp;
        /// <summary>
        /// 30400700 КПП (KPP)
        /// </summary>
        [RegisterAttribute(AttributeID = 30400700)]
        public string Kpp
        {
            get
            {
                CheckPropertyInited("Kpp");
                return _kpp;
            }
            set
            {
                _kpp = value;
                NotifyPropertyChanged("Kpp");
            }
        }


        private string _okpo;
        /// <summary>
        /// 30400800 ОКПО (OKPO)
        /// </summary>
        [RegisterAttribute(AttributeID = 30400800)]
        public string Okpo
        {
            get
            {
                CheckPropertyInited("Okpo");
                return _okpo;
            }
            set
            {
                _okpo = value;
                NotifyPropertyChanged("Okpo");
            }
        }


        private string _ogrn;
        /// <summary>
        /// 30400900 Номер ОГРН (OGRN)
        /// </summary>
        [RegisterAttribute(AttributeID = 30400900)]
        public string Ogrn
        {
            get
            {
                CheckPropertyInited("Ogrn");
                return _ogrn;
            }
            set
            {
                _ogrn = value;
                NotifyPropertyChanged("Ogrn");
            }
        }


        private DateTime? _ogrndate;
        /// <summary>
        /// 30401000 Дата ОГРН (OGRN_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30401000)]
        public DateTime? OgrnDate
        {
            get
            {
                CheckPropertyInited("OgrnDate");
                return _ogrndate;
            }
            set
            {
                _ogrndate = value;
                NotifyPropertyChanged("OgrnDate");
            }
        }


        private string _regnumber;
        /// <summary>
        /// 30401100 Номер свидетельства о регистрации (REG_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 30401100)]
        public string RegNumber
        {
            get
            {
                CheckPropertyInited("RegNumber");
                return _regnumber;
            }
            set
            {
                _regnumber = value;
                NotifyPropertyChanged("RegNumber");
            }
        }


        private string _regorgan;
        /// <summary>
        /// 30401200 Орган регистрации (REG_ORGAN)
        /// </summary>
        [RegisterAttribute(AttributeID = 30401200)]
        public string RegOrgan
        {
            get
            {
                CheckPropertyInited("RegOrgan");
                return _regorgan;
            }
            set
            {
                _regorgan = value;
                NotifyPropertyChanged("RegOrgan");
            }
        }


        private string _phone;
        /// <summary>
        /// 30401300 Телефон (PHONE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30401300)]
        public string Phone
        {
            get
            {
                CheckPropertyInited("Phone");
                return _phone;
            }
            set
            {
                _phone = value;
                NotifyPropertyChanged("Phone");
            }
        }


        private string _fax;
        /// <summary>
        /// 30401400 Факс (FAX)
        /// </summary>
        [RegisterAttribute(AttributeID = 30401400)]
        public string Fax
        {
            get
            {
                CheckPropertyInited("Fax");
                return _fax;
            }
            set
            {
                _fax = value;
                NotifyPropertyChanged("Fax");
            }
        }


        private string _email;
        /// <summary>
        /// 30401500 Email (EMAIL)
        /// </summary>
        [RegisterAttribute(AttributeID = 30401500)]
        public string Email
        {
            get
            {
                CheckPropertyInited("Email");
                return _email;
            }
            set
            {
                _email = value;
                NotifyPropertyChanged("Email");
            }
        }


        private string _www;
        /// <summary>
        /// 30401600 www (WWW)
        /// </summary>
        [RegisterAttribute(AttributeID = 30401600)]
        public string Www
        {
            get
            {
                CheckPropertyInited("Www");
                return _www;
            }
            set
            {
                _www = value;
                NotifyPropertyChanged("Www");
            }
        }


        private string _surname;
        /// <summary>
        /// 30401700 Фамилия (SURNAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 30401700)]
        public string Surname
        {
            get
            {
                CheckPropertyInited("Surname");
                return _surname;
            }
            set
            {
                _surname = value;
                NotifyPropertyChanged("Surname");
            }
        }


        private string _name;
        /// <summary>
        /// 30401800 Имя (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 30401800)]
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


        private string _patronymic;
        /// <summary>
        /// 30401900 Отчество (PATRONYMIC)
        /// </summary>
        [RegisterAttribute(AttributeID = 30401900)]
        public string Patronymic
        {
            get
            {
                CheckPropertyInited("Patronymic");
                return _patronymic;
            }
            set
            {
                _patronymic = value;
                NotifyPropertyChanged("Patronymic");
            }
        }


        private string _legaladdress;
        /// <summary>
        /// 30402000 Юридический адрес (LEGAL_ADDRESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30402000)]
        public string LegalAddress
        {
            get
            {
                CheckPropertyInited("LegalAddress");
                return _legaladdress;
            }
            set
            {
                _legaladdress = value;
                NotifyPropertyChanged("LegalAddress");
            }
        }


        private string _postaddress;
        /// <summary>
        /// 30402100 Почтовый адрес (POST_ADDRESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30402100)]
        public string PostAddress
        {
            get
            {
                CheckPropertyInited("PostAddress");
                return _postaddress;
            }
            set
            {
                _postaddress = value;
                NotifyPropertyChanged("PostAddress");
            }
        }


        private string _orgtype;
        /// <summary>
        /// 30402200 Вид предприятия (ORG_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30402200)]
        public string OrgType
        {
            get
            {
                CheckPropertyInited("OrgType");
                return _orgtype;
            }
            set
            {
                _orgtype = value;
                NotifyPropertyChanged("OrgType");
            }
        }


        private long? _orgtype_Code;
        /// <summary>
        /// 30402200 Вид предприятия (справочный код) (ORG_TYPE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 30402200)]
        public long? OrgType_Code
        {
            get
            {
                CheckPropertyInited("OrgType_Code");
                return _orgtype_Code;
            }
            set
            {
                _orgtype_Code = value;
                NotifyPropertyChanged("OrgType_Code");
            }
        }


        private string _okfs;
        /// <summary>
        /// 30402300 ОКФС (OKFS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30402300)]
        public string Okfs
        {
            get
            {
                CheckPropertyInited("Okfs");
                return _okfs;
            }
            set
            {
                _okfs = value;
                NotifyPropertyChanged("Okfs");
            }
        }


        private long? _okfs_Code;
        /// <summary>
        /// 30402300 ОКФС (справочный код) (OKFS_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 30402300)]
        public long? Okfs_Code
        {
            get
            {
                CheckPropertyInited("Okfs_Code");
                return _okfs_Code;
            }
            set
            {
                _okfs_Code = value;
                NotifyPropertyChanged("Okfs_Code");
            }
        }


        private string _okopf;
        /// <summary>
        /// 30402400 ОКОПФ (OKOPF)
        /// </summary>
        [RegisterAttribute(AttributeID = 30402400)]
        public string Okopf
        {
            get
            {
                CheckPropertyInited("Okopf");
                return _okopf;
            }
            set
            {
                _okopf = value;
                NotifyPropertyChanged("Okopf");
            }
        }


        private long? _okopf_Code;
        /// <summary>
        /// 30402400 ОКОПФ (справочный код) (OKOPF_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 30402400)]
        public long? Okopf_Code
        {
            get
            {
                CheckPropertyInited("Okopf_Code");
                return _okopf_Code;
            }
            set
            {
                _okopf_Code = value;
                NotifyPropertyChanged("Okopf_Code");
            }
        }


        private string _okato;
        /// <summary>
        /// 30402500 ОКАТО (OKATO)
        /// </summary>
        [RegisterAttribute(AttributeID = 30402500)]
        public string Okato
        {
            get
            {
                CheckPropertyInited("Okato");
                return _okato;
            }
            set
            {
                _okato = value;
                NotifyPropertyChanged("Okato");
            }
        }


        private long? _okato_Code;
        /// <summary>
        /// 30402500 ОКАТО (справочный код) (OKATO_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 30402500)]
        public long? Okato_Code
        {
            get
            {
                CheckPropertyInited("Okato_Code");
                return _okato_Code;
            }
            set
            {
                _okato_Code = value;
                NotifyPropertyChanged("Okato_Code");
            }
        }


        private string _okved;
        /// <summary>
        /// 30402600 ОКВЭД (OKVED)
        /// </summary>
        [RegisterAttribute(AttributeID = 30402600)]
        public string Okved
        {
            get
            {
                CheckPropertyInited("Okved");
                return _okved;
            }
            set
            {
                _okved = value;
                NotifyPropertyChanged("Okved");
            }
        }


        private long? _okved_Code;
        /// <summary>
        /// 30402600 ОКВЭД (справочный код) (OKVED_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 30402600)]
        public long? Okved_Code
        {
            get
            {
                CheckPropertyInited("Okved_Code");
                return _okved_Code;
            }
            set
            {
                _okved_Code = value;
                NotifyPropertyChanged("Okved_Code");
            }
        }


        private long? _upid;
        /// <summary>
        /// 30402700 Ведомственная подчиненность (UP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 30402700)]
        public long? UpId
        {
            get
            {
                CheckPropertyInited("UpId");
                return _upid;
            }
            set
            {
                _upid = value;
                NotifyPropertyChanged("UpId");
            }
        }


        private DateTime? _createdate;
        /// <summary>
        /// 30402800 Дата создания (CREATE_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30402800)]
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


        private DateTime? _dateend;
        /// <summary>
        /// 30402900 Дата прекращения (DATE_END)
        /// </summary>
        [RegisterAttribute(AttributeID = 30402900)]
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


        private string _state;
        /// <summary>
        /// 30403000 Состояние субъекта (STATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30403000)]
        public string State
        {
            get
            {
                CheckPropertyInited("State");
                return _state;
            }
            set
            {
                _state = value;
                NotifyPropertyChanged("State");
            }
        }


        private long? _state_Code;
        /// <summary>
        /// 30403000 Состояние субъекта (справочный код) (STATE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 30403000)]
        public long? State_Code
        {
            get
            {
                CheckPropertyInited("State_Code");
                return _state_Code;
            }
            set
            {
                _state_Code = value;
                NotifyPropertyChanged("State_Code");
            }
        }


        private string _legalindex;
        /// <summary>
        /// 30403100 Юридический адрес: индекс (LEGAL_INDEX)
        /// </summary>
        [RegisterAttribute(AttributeID = 30403100)]
        public string LegalIndex
        {
            get
            {
                CheckPropertyInited("LegalIndex");
                return _legalindex;
            }
            set
            {
                _legalindex = value;
                NotifyPropertyChanged("LegalIndex");
            }
        }


        private string _legallocality;
        /// <summary>
        /// 30403200 Юридический адрес: нас.пункт (LEGAL_LOCALITY)
        /// </summary>
        [RegisterAttribute(AttributeID = 30403200)]
        public string LegalLocality
        {
            get
            {
                CheckPropertyInited("LegalLocality");
                return _legallocality;
            }
            set
            {
                _legallocality = value;
                NotifyPropertyChanged("LegalLocality");
            }
        }


        private string _postindex;
        /// <summary>
        /// 30403300 Почтовый адрес: индекс (POST_INDEX)
        /// </summary>
        [RegisterAttribute(AttributeID = 30403300)]
        public string PostIndex
        {
            get
            {
                CheckPropertyInited("PostIndex");
                return _postindex;
            }
            set
            {
                _postindex = value;
                NotifyPropertyChanged("PostIndex");
            }
        }


        private string _postlocality;
        /// <summary>
        /// 30403400 Почтовый адрес: нас.пункт (POST_LOCALITY)
        /// </summary>
        [RegisterAttribute(AttributeID = 30403400)]
        public string PostLocality
        {
            get
            {
                CheckPropertyInited("PostLocality");
                return _postlocality;
            }
            set
            {
                _postlocality = value;
                NotifyPropertyChanged("PostLocality");
            }
        }


        private string _gutype;
        /// <summary>
        /// 30403500 Тип ГУ (GU_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 30403500)]
        public string GUType
        {
            get
            {
                CheckPropertyInited("GUType");
                return _gutype;
            }
            set
            {
                _gutype = value;
                NotifyPropertyChanged("GUType");
            }
        }


        private long? _gutype_Code;
        /// <summary>
        /// 30403500 Тип ГУ (справочный код) (GU_TYPE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 30403500)]
        public long? GUType_Code
        {
            get
            {
                CheckPropertyInited("GUType_Code");
                return _gutype_Code;
            }
            set
            {
                _gutype_Code = value;
                NotifyPropertyChanged("GUType_Code");
            }
        }


        private long? _sourceidgu;
        /// <summary>
        /// 30403600 ИД в источнике (SOURCE_ID_GU)
        /// </summary>
        [RegisterAttribute(AttributeID = 30403600)]
        public long? SourceIdGu
        {
            get
            {
                CheckPropertyInited("SourceIdGu");
                return _sourceidgu;
            }
            set
            {
                _sourceidgu = value;
                NotifyPropertyChanged("SourceIdGu");
            }
        }


        private long? _oktmo;
        /// <summary>
        /// 30403700 ОКТМО (OKTMO)
        /// </summary>
        [RegisterAttribute(AttributeID = 30403700)]
        public long? Oktmo
        {
            get
            {
                CheckPropertyInited("Oktmo");
                return _oktmo;
            }
            set
            {
                _oktmo = value;
                NotifyPropertyChanged("Oktmo");
            }
        }


        private bool? _isoiv;
        /// <summary>
        /// 30403800 Признак ОИВ (IS_OIV)
        /// </summary>
        [RegisterAttribute(AttributeID = 30403800)]
        public bool? IsOiv
        {
            get
            {
                CheckPropertyInited("IsOiv");
                return _isoiv;
            }
            set
            {
                _isoiv = value;
                NotifyPropertyChanged("IsOiv");
            }
        }


        private long? _addagreementid;
        /// <summary>
        /// 30403900 Идентификатор доп. соглашения (ADD_AGREEMENT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 30403900)]
        public long? AddAgreementId
        {
            get
            {
                CheckPropertyInited("AddAgreementId");
                return _addagreementid;
            }
            set
            {
                _addagreementid = value;
                NotifyPropertyChanged("AddAgreementId");
            }
        }


        private string _postfias;
        /// <summary>
        /// 30404000 Почтовый адрес - ФИАС (POST_FIAS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30404000)]
        public string PostFias
        {
            get
            {
                CheckPropertyInited("PostFias");
                return _postfias;
            }
            set
            {
                _postfias = value;
                NotifyPropertyChanged("PostFias");
            }
        }


        private string _legalfias;
        /// <summary>
        /// 30404100 Юридический адрес - ФИАС (LEGAL_FIAS)
        /// </summary>
        [RegisterAttribute(AttributeID = 30404100)]
        public string LegalFias
        {
            get
            {
                CheckPropertyInited("LegalFias");
                return _legalfias;
            }
            set
            {
                _legalfias = value;
                NotifyPropertyChanged("LegalFias");
            }
        }

    }
}
