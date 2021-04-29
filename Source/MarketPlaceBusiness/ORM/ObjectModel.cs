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
    internal partial class OMCoreObject : OMBaseClass<OMCoreObject>
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
    internal partial class OMYandexAddress : OMBaseClass<OMYandexAddress>
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
        /// 10100800 Уточнение округа (область) (PROVINCE_2)
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
        /// 10101400 Уточнение округа (район) (DISTRICT_2)
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

namespace ObjectModel.Market
{
    /// <summary>
    /// 105 Таблица, содержащая ретроспективу цен по объектам (MARKET_PRICE_HISTORY)
    /// </summary>
    [RegisterInfo(RegisterID = 105)]
    [Serializable]
    internal partial class OMPriceHistory : OMBaseClass<OMPriceHistory>
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
    /// 113 Таблица, содержащая ретроспективу цен по объектам с учетом корректировки на комнатность (MARKET_PRICE_AFTER_CORRECTION_BY_ROOMS_H)
    /// </summary>
    [RegisterInfo(RegisterID = 113)]
    [Serializable]
    internal partial class OMPriceAfterCorrectionByRoomsHistory : OMBaseClass<OMPriceAfterCorrectionByRoomsHistory>
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
