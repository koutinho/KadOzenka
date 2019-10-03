using System;
using Core.ObjectModel;
using Core.ObjectModel.CustomAttribute;
using Core.Shared.Extensions;
using ObjectModel.Directory;
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
        /// 10002300 Источник данных (MARKET)
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
        /// 10002300 Источник данных (справочный код) (MARKET_CODE)
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


        private string _propertytype;
        /// <summary>
        /// 10002500 Тип объекта недвижимости (PROPERTY_TYPE)
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
        /// 10002500 Тип объекта недвижимости (справочный код) (PROPERTY_TYPE_CODE)
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
        /// 10002600 Идентификатор в Источнике данных (MARKET_ID)
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
        /// 10002700 Цена (PRICE)
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
        /// 10002800 Дата сделки (PARSER_TIME)
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
        /// 10003100 Адрес (ADDRESS)
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
        /// 10003200 Ближайшие станции метро (METRO)
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
        /// 10003400 Описание (DESCRIPTION)
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
        /// 10004100 Количество этажей (FLOOR_NUMBER)
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
        /// 10004600 Площадь земельного участка (AREA_LAND)
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


        private string _category;
        /// <summary>
        /// 10004800 Категория (CATEGORY)
        /// </summary>
        [RegisterAttribute(AttributeID = 10004800)]
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
        /// 10004900 Подкатегория (SUBCATEGORY)
        /// </summary>
        [RegisterAttribute(AttributeID = 10004900)]
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
        /// 10005000 Идентификатор категории (CATEGORY_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 10005000)]
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


        private string _district;
        /// <summary>
        /// 10005300 Район (DISTRICT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10005300)]
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


        private long? _district_Code;
        /// <summary>
        /// 10005300 Район (справочный код) (DISTRICT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10005300)]
        public long? District_Code
        {
            get
            {
                CheckPropertyInited("District_Code");
                return _district_Code;
            }
            set
            {
                _district_Code = value;
                NotifyPropertyChanged("District_Code");
            }
        }


        private string _cadastralnumber;
        /// <summary>
        /// 10005400 Кадастровый номер помещения (CADASTRAL_NUMBER)
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
        /// 10005600 Кадастровый номер квартала (CADASTRAL_QUARTAL)
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
        /// 10005700 Группа (KO_GROUP)
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
        /// 10005700 Группа (справочный код) (KO_GROUP_CODE)
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
        /// 10005800 Подгруппа (KO_SUBGROUP)
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
        /// 10005800 Подгруппа (справочный код) (KO_SUBGROUP_CODE)
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


        private decimal _pricepermeter;
        /// <summary>
        /// 100002701 Цена за кв.м (PRICE_PER_METER)
        /// </summary>
        [RegisterAttribute(AttributeID = 100002701)]
        public decimal PricePerMeter
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

namespace ObjectModel.KO
{
    /// <summary>
    /// 200 Объект кадастровой оценки (KO_MAIN_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 200)]
    [Serializable]
    public partial class OMMainObject : OMBaseClass<OMMainObject>
    {

        private long _id;
        /// <summary>
        /// 20002000 Уникальный идентификатор объекта кадастровой оценки (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 20002000)]
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
        /// 20002100 Кадастровый номер объекта оценки (CADASTRAL_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 20002100)]
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
        /// 20002200 Тип объекта кадастровой оценки (OBJECT_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20002200)]
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
        /// 20002200 Тип объекта кадастровой оценки (справочный код) (OBJECT_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20002200)]
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


        private long? _groupid;
        /// <summary>
        /// 20002300 Идентификатор группы объекта кадастровой оценки (GROUP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20002300)]
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

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 201 Единица кадастровой оценки (KO_UNIT)
    /// </summary>
    [RegisterInfo(RegisterID = 201)]
    [Serializable]
    public partial class OMUnit : OMBaseClass<OMUnit>
    {

        private long _id;
        /// <summary>
        /// 20102000 Уникальный идентификатор единицы объекта кадастровой оценки (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 20102000)]
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
        /// 20102100 Уникальный идентификатор объекта кадастровой оценки (OBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20102100)]
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
        /// 20102200 Идентификатор тура объекта кадастровой оценки (TOUR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20102200)]
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
        /// 20102300 Идентификатор задания единицы объекта кадастровой оценки (TASK_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20102300)]
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


        private long? _modelid;
        /// <summary>
        /// 20102400 Уникальный идентификатор модели (MODEL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20102400)]
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


        private long? _groupid;
        /// <summary>
        /// 20102500 Уникальный идентификатор группы (GROUP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20102500)]
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
        /// 20102600 Статус единицы оценки (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 20102600)]
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
        /// 20102600 Статус единицы оценки (справочный код) (STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20102600)]
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
        /// 20102700 Дата создания (CREATION_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20102700)]
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
        /// 20102800 Кадастровая стоимость (CADASTRAL_COST)
        /// </summary>
        [RegisterAttribute(AttributeID = 20102800)]
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
        /// 20202000 Уникальный идентификатор тура (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 20202000)]
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
        /// 20202100 Год проведения тур (YEAR)
        /// </summary>
        [RegisterAttribute(AttributeID = 20202100)]
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
        /// 20302000 Уникальный идентификатор задания на оценку (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 20302000)]
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
        /// 20302100 Дата создания (CREATION_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20302100)]
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
        /// 20302200 Идентификатор документа (DOCUMENT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20302200)]
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
        /// 20302300 Тип статьи (NOTE_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20302300)]
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
        /// 20302300 Тип статьи (справочный код) (NOTE_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20302300)]
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
        /// 20302400 Уникальный идентификатор тура (TOUR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20302400)]
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
        /// 20302500 Уникальный идентификатор документа ответа (RESPONSE_DOCUMENT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20302500)]
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
        /// 20302600 Статус (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 20302600)]
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
        /// 20302600 Статус (справочный код) (STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20302600)]
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

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 204 Документы (KO_DOCUMENT)
    /// </summary>
    [RegisterInfo(RegisterID = 204)]
    [Serializable]
    public partial class OMDocument : OMBaseClass<OMDocument>
    {

        private long _id;
        /// <summary>
        /// 20402000 Уникальный идентификатор документа (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 20402000)]
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
        /// 20402100 Дата создания (CREATION_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20402100)]
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


        private DateTime? _documentdate;
        /// <summary>
        /// 20402200 Дата документа (DOCUMENT_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20402200)]
        public DateTime? DocumentDate
        {
            get
            {
                CheckPropertyInited("DocumentDate");
                return _documentdate;
            }
            set
            {
                _documentdate = value;
                NotifyPropertyChanged("DocumentDate");
            }
        }


        private long? _number;
        /// <summary>
        /// 20402300 Номер документа (NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 20402300)]
        public long? Number
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


        private string _typecode;
        /// <summary>
        /// 20402400 Тип документа (TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20402400)]
        public string TypeCode
        {
            get
            {
                CheckPropertyInited("TypeCode");
                return _typecode;
            }
            set
            {
                _typecode = value;
                NotifyPropertyChanged("TypeCode");
            }
        }


        private KoDocType _typecode_Code;
        /// <summary>
        /// 20402400 Тип документа (справочный код) (TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20402400)]
        public KoDocType TypeCode_Code
        {
            get
            {
                CheckPropertyInited("TypeCode_Code");
                return this._typecode_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typecode))
                    {
                         _typecode = descr;
                    }
                }
                else
                {
                     _typecode = descr;
                }

                this._typecode_Code = value;
                NotifyPropertyChanged("TypeCode");
                NotifyPropertyChanged("TypeCode_Code");
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
        /// 20502000 Уникальный идентификатор группы (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 20502000)]
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
        /// 20502100 Уникальный идентификатор родительской группы (PARENT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20502100)]
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
        /// 20502200 Наименование группы (GROUP_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 20502200)]
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
        /// 20502300 Механизм группировки (GROUP_ALGORITM)
        /// </summary>
        [RegisterAttribute(AttributeID = 20502300)]
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
        /// 20502300 Механизм группировки (справочный код) (GROUP_ALGORITM_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20502300)]
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
        /// 20602000 Уникальный идентификатор модели (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 20602000)]
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
        /// 20602100 Идентификатор группы (GROUP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20602100)]
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
        /// 20602200 Наименование (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 20602200)]
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
        /// 20602300 Описание (DESCRIPTION)
        /// </summary>
        [RegisterAttribute(AttributeID = 20602300)]
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
        /// 20602400 Формула (FORMULA)
        /// </summary>
        [RegisterAttribute(AttributeID = 20602400)]
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
        /// 20602500 Метод рассчёта (ALGORITM_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20602500)]
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
        /// 20602500 Метод рассчёта (справочный код) (ALGORITM_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20602500)]
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

    }
}

namespace ObjectModel.KO
{
    /// <summary>
    /// 207 Модель тура (KO_TOUR_MODEL)
    /// </summary>
    [RegisterInfo(RegisterID = 207)]
    [Serializable]
    public partial class OMTourModel : OMBaseClass<OMTourModel>
    {

        private long _id;
        /// <summary>
        /// 20702000 Уникальный идентификатор модели тура (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 20702000)]
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
        /// 20702100 Идентификатор тура (TOUR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20702100)]
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


        private long? _modelid;
        /// <summary>
        /// 20702200 Идентификатор модели (MODEL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20702200)]
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


        private string _status;
        /// <summary>
        /// 20702300 Статус (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 20702300)]
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


        private KoModelStatus _status_Code;
        /// <summary>
        /// 20702300 Статус (справочный код) (STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 20702300)]
        public KoModelStatus Status_Code
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
        /// 20802000 Уникальный идентификатор фактора группы (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 20802000)]
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
        /// 20802100 Идентификатор группы (GROUP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20802100)]
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
        /// 20802200 Идентификатор фактора (FACTOR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 20802200)]
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
        /// 21002000 Уникальный идентификатор фактора модели (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 21002000)]
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


        private long? _modelid;
        /// <summary>
        /// 21002100 Идентификатор модели (MODEL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21002100)]
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
        /// 21002200 Идентификатор фактора (FACTOR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21002200)]
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
        /// 21002300 Идентификатор метки (MARKER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21002300)]
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
        /// 21102000 Уникальный идентификатор метки (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 21102000)]
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
        /// 21102100 Идентификатор группы (GROUP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21102100)]
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
        /// 21102200 Идентификатор фактора (FACTOR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 21102200)]
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
        /// 21102300 Значение фактора (VALUE_FACTOR)
        /// </summary>
        [RegisterAttribute(AttributeID = 21102300)]
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
        /// 21102400 Значение метки (METKA_FACTOR)
        /// </summary>
        [RegisterAttribute(AttributeID = 21102400)]
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

    }
}
