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


        private string _market;
        /// <summary>
        /// 10002300 Источник объявления (MARKET)
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
        /// 10002300 Источник объявления (справочный код) (MARKET_CODE)
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


        private decimal _price;
        /// <summary>
        /// 10002700 Стоимость (PRICE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10002700)]
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


        private long? _floornumber;
        /// <summary>
        /// 10004100 Этаж (FLOOR_NUMBER)
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


        private decimal _area;
        /// <summary>
        /// 10004300 Общая площадь (AREA)
        /// </summary>
        [RegisterAttribute(AttributeID = 10004300)]
        public decimal Area
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


        private string _qualityclass;
        /// <summary>
        /// 10007200 Класс строения (QUALITY_CLASS)
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
        /// 10007200 Класс строения (справочный код) (QUALITY_CLASS_CODE)
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


        private DateTime? _downloaddate;
        /// <summary>
        /// 10007800 Дата загрузки (download_date)
        /// </summary>
        [RegisterAttribute(AttributeID = 10007800)]
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


        private string _externaladvertisementid;
        /// <summary>
        /// 10007900 Внешний Id объявления (external_advertisement_id)
        /// </summary>
        [RegisterAttribute(AttributeID = 10007900)]
        public string ExternalAdvertisementId
        {
            get
            {
                CheckPropertyInited("ExternalAdvertisementId");
                return _externaladvertisementid;
            }
            set
            {
                _externaladvertisementid = value;
                NotifyPropertyChanged("ExternalAdvertisementId");
            }
        }


        private string _advertisementdescription;
        /// <summary>
        /// 10008000 Текст объявления (advertisement_description)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008000)]
        public string AdvertisementDescription
        {
            get
            {
                CheckPropertyInited("AdvertisementDescription");
                return _advertisementdescription;
            }
            set
            {
                _advertisementdescription = value;
                NotifyPropertyChanged("AdvertisementDescription");
            }
        }


        private decimal? _areafrom;
        /// <summary>
        /// 10008100 Площадь от (area_from)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008100)]
        public decimal? AreaFrom
        {
            get
            {
                CheckPropertyInited("AreaFrom");
                return _areafrom;
            }
            set
            {
                _areafrom = value;
                NotifyPropertyChanged("AreaFrom");
            }
        }


        private string _name;
        /// <summary>
        /// 10008200 Название (name)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008200)]
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


        private long? _flatnumber;
        /// <summary>
        /// 10008300 Номер квартиры на площадке (flat_number)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008300)]
        public long? FlatNumber
        {
            get
            {
                CheckPropertyInited("FlatNumber");
                return _flatnumber;
            }
            set
            {
                _flatnumber = value;
                NotifyPropertyChanged("FlatNumber");
            }
        }


        private string _sectionnumber;
        /// <summary>
        /// 10008400 Номер секции (section_number)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008400)]
        public string SectionNumber
        {
            get
            {
                CheckPropertyInited("SectionNumber");
                return _sectionnumber;
            }
            set
            {
                _sectionnumber = value;
                NotifyPropertyChanged("SectionNumber");
            }
        }


        private string _flattype;
        /// <summary>
        /// 10008500 Тип квартиры (flat_type)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008500)]
        public string FlatType
        {
            get
            {
                CheckPropertyInited("FlatType");
                return _flattype;
            }
            set
            {
                _flattype = value;
                NotifyPropertyChanged("FlatType");
            }
        }


        private string _houseline;
        /// <summary>
        /// 10008600 Линия застройки домов (house_line)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008600)]
        public string HouseLine
        {
            get
            {
                CheckPropertyInited("HouseLine");
                return _houseline;
            }
            set
            {
                _houseline = value;
                NotifyPropertyChanged("HouseLine");
            }
        }


        private HouseLineType _houseline_Code;
        /// <summary>
        /// 10008600 Линия застройки домов (справочный код) (house_line_code)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008600)]
        public HouseLineType HouseLine_Code
        {
            get
            {
                CheckPropertyInited("HouseLine_Code");
                return this._houseline_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_houseline))
                    {
                         _houseline = descr;
                    }
                }
                else
                {
                     _houseline = descr;
                }

                this._houseline_Code = value;
                NotifyPropertyChanged("HouseLine");
                NotifyPropertyChanged("HouseLine_Code");
            }
        }


        private string _developer;
        /// <summary>
        /// 10008700 Застройщик (developer)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008700)]
        public string Developer
        {
            get
            {
                CheckPropertyInited("Developer");
                return _developer;
            }
            set
            {
                _developer = value;
                NotifyPropertyChanged("Developer");
            }
        }


        private string _finishingcondition;
        /// <summary>
        /// 10008800 Состояние отделки (finishing_condition)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008800)]
        public string FinishingCondition
        {
            get
            {
                CheckPropertyInited("FinishingCondition");
                return _finishingcondition;
            }
            set
            {
                _finishingcondition = value;
                NotifyPropertyChanged("FinishingCondition");
            }
        }


        private FinishingCondition _finishingcondition_Code;
        /// <summary>
        /// 10008800 Состояние отделки (справочный код) (finishing_condition_code)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008800)]
        public FinishingCondition FinishingCondition_Code
        {
            get
            {
                CheckPropertyInited("FinishingCondition_Code");
                return this._finishingcondition_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_finishingcondition))
                    {
                         _finishingcondition = descr;
                    }
                }
                else
                {
                     _finishingcondition = descr;
                }

                this._finishingcondition_Code = value;
                NotifyPropertyChanged("FinishingCondition");
                NotifyPropertyChanged("FinishingCondition_Code");
            }
        }


        private string _housetype;
        /// <summary>
        /// 10008900 Тип дома (house_type)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008900)]
        public string HouseType
        {
            get
            {
                CheckPropertyInited("HouseType");
                return _housetype;
            }
            set
            {
                _housetype = value;
                NotifyPropertyChanged("HouseType");
            }
        }


        private HouseType _housetype_Code;
        /// <summary>
        /// 10008900 Тип дома (справочный код) (house_type_code)
        /// </summary>
        [RegisterAttribute(AttributeID = 10008900)]
        public HouseType HouseType_Code
        {
            get
            {
                CheckPropertyInited("HouseType_Code");
                return this._housetype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_housetype))
                    {
                         _housetype = descr;
                    }
                }
                else
                {
                     _housetype = descr;
                }

                this._housetype_Code = value;
                NotifyPropertyChanged("HouseType");
                NotifyPropertyChanged("HouseType_Code");
            }
        }


        private string _layout;
        /// <summary>
        /// 10009000 Планировка (layout)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009000)]
        public string Layout
        {
            get
            {
                CheckPropertyInited("Layout");
                return _layout;
            }
            set
            {
                _layout = value;
                NotifyPropertyChanged("Layout");
            }
        }


        private Layout _layout_Code;
        /// <summary>
        /// 10009000 Планировка (справочный код) (layout_code)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009000)]
        public Layout Layout_Code
        {
            get
            {
                CheckPropertyInited("Layout_Code");
                return this._layout_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_layout))
                    {
                         _layout = descr;
                    }
                }
                else
                {
                     _layout = descr;
                }

                this._layout_Code = value;
                NotifyPropertyChanged("Layout");
                NotifyPropertyChanged("Layout_Code");
            }
        }


        private string _permittedusetype;
        /// <summary>
        /// 10009100 Вид разрешённого использования (permitted_use_type)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009100)]
        public string PermittedUseType
        {
            get
            {
                CheckPropertyInited("PermittedUseType");
                return _permittedusetype;
            }
            set
            {
                _permittedusetype = value;
                NotifyPropertyChanged("PermittedUseType");
            }
        }


        private PermittedUseType _permittedusetype_Code;
        /// <summary>
        /// 10009100 Вид разрешённого использования (справочный код) (permitted_use_type_code)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009100)]
        public PermittedUseType PermittedUseType_Code
        {
            get
            {
                CheckPropertyInited("PermittedUseType_Code");
                return this._permittedusetype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_permittedusetype))
                    {
                         _permittedusetype = descr;
                    }
                }
                else
                {
                     _permittedusetype = descr;
                }

                this._permittedusetype_Code = value;
                NotifyPropertyChanged("PermittedUseType");
                NotifyPropertyChanged("PermittedUseType_Code");
            }
        }


        private string _drivewaytype;
        /// <summary>
        /// 10009200 Подъездные пути (driveway_type)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009200)]
        public string DrivewayType
        {
            get
            {
                CheckPropertyInited("DrivewayType");
                return _drivewaytype;
            }
            set
            {
                _drivewaytype = value;
                NotifyPropertyChanged("DrivewayType");
            }
        }


        private DrivewayType _drivewaytype_Code;
        /// <summary>
        /// 10009200 Подъездные пути (справочный код) (driveway_type_code)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009200)]
        public DrivewayType DrivewayType_Code
        {
            get
            {
                CheckPropertyInited("DrivewayType_Code");
                return this._drivewaytype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_drivewaytype))
                    {
                         _drivewaytype = descr;
                    }
                }
                else
                {
                     _drivewaytype = descr;
                }

                this._drivewaytype_Code = value;
                NotifyPropertyChanged("DrivewayType");
                NotifyPropertyChanged("DrivewayType_Code");
            }
        }


        private string _parcelareaunittype;
        /// <summary>
        /// 10009300 Единица измерения площади участка (parcel_area_unit_type)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009300)]
        public string ParcelAreaUnitType
        {
            get
            {
                CheckPropertyInited("ParcelAreaUnitType");
                return _parcelareaunittype;
            }
            set
            {
                _parcelareaunittype = value;
                NotifyPropertyChanged("ParcelAreaUnitType");
            }
        }


        private ParcelAreaUnitType _parcelareaunittype_Code;
        /// <summary>
        /// 10009300 Единица измерения площади участка (справочный код) (parcel_area_unit_type_code)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009300)]
        public ParcelAreaUnitType ParcelAreaUnitType_Code
        {
            get
            {
                CheckPropertyInited("ParcelAreaUnitType_Code");
                return this._parcelareaunittype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_parcelareaunittype))
                    {
                         _parcelareaunittype = descr;
                    }
                }
                else
                {
                     _parcelareaunittype = descr;
                }

                this._parcelareaunittype_Code = value;
                NotifyPropertyChanged("ParcelAreaUnitType");
                NotifyPropertyChanged("ParcelAreaUnitType_Code");
            }
        }


        private string _parcelstatus;
        /// <summary>
        /// 10009400 Статус земли (parcel_status)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009400)]
        public string ParcelStatus
        {
            get
            {
                CheckPropertyInited("ParcelStatus");
                return _parcelstatus;
            }
            set
            {
                _parcelstatus = value;
                NotifyPropertyChanged("ParcelStatus");
            }
        }


        private ParcelStatus _parcelstatus_Code;
        /// <summary>
        /// 10009400 Статус земли (справочный код) (parcel_status_code)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009400)]
        public ParcelStatus ParcelStatus_Code
        {
            get
            {
                CheckPropertyInited("ParcelStatus_Code");
                return this._parcelstatus_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_parcelstatus))
                    {
                         _parcelstatus = descr;
                    }
                }
                else
                {
                     _parcelstatus = descr;
                }

                this._parcelstatus_Code = value;
                NotifyPropertyChanged("ParcelStatus");
                NotifyPropertyChanged("ParcelStatus_Code");
            }
        }


        private string _parceltype;
        /// <summary>
        /// 10009500 Тип участка (parcel_type)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009500)]
        public string ParcelType
        {
            get
            {
                CheckPropertyInited("ParcelType");
                return _parceltype;
            }
            set
            {
                _parceltype = value;
                NotifyPropertyChanged("ParcelType");
            }
        }


        private ParcelType _parceltype_Code;
        /// <summary>
        /// 10009500 Тип участка (справочный код) (parcel_type_code)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009500)]
        public ParcelType ParcelType_Code
        {
            get
            {
                CheckPropertyInited("ParcelType_Code");
                return this._parceltype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_parceltype))
                    {
                         _parceltype = descr;
                    }
                }
                else
                {
                     _parceltype = descr;
                }

                this._parceltype_Code = value;
                NotifyPropertyChanged("ParcelType");
                NotifyPropertyChanged("ParcelType_Code");
            }
        }


        private string _electricitylocationtype;
        /// <summary>
        /// 10009600 Локация электроснабжения (electricity_location_type)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009600)]
        public string ElectricityLocationType
        {
            get
            {
                CheckPropertyInited("ElectricityLocationType");
                return _electricitylocationtype;
            }
            set
            {
                _electricitylocationtype = value;
                NotifyPropertyChanged("ElectricityLocationType");
            }
        }


        private ElectricityLocationType _electricitylocationtype_Code;
        /// <summary>
        /// 10009600 Локация электроснабжения (справочный код) (electricity_location_type_code)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009600)]
        public ElectricityLocationType ElectricityLocationType_Code
        {
            get
            {
                CheckPropertyInited("ElectricityLocationType_Code");
                return this._electricitylocationtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_electricitylocationtype))
                    {
                         _electricitylocationtype = descr;
                    }
                }
                else
                {
                     _electricitylocationtype = descr;
                }

                this._electricitylocationtype_Code = value;
                NotifyPropertyChanged("ElectricityLocationType");
                NotifyPropertyChanged("ElectricityLocationType_Code");
            }
        }


        private bool? _possibilitytoconnectelectricity;
        /// <summary>
        /// 10009700 Возможность подключения электичества (possibility_to_connect_electricity)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009700)]
        public bool? PossibilityToConnectElectricity
        {
            get
            {
                CheckPropertyInited("PossibilityToConnectElectricity");
                return _possibilitytoconnectelectricity;
            }
            set
            {
                _possibilitytoconnectelectricity = value;
                NotifyPropertyChanged("PossibilityToConnectElectricity");
            }
        }


        private long? _electricitypower;
        /// <summary>
        /// 10009800 Мощность электричества, кВТ (electricity_power)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009800)]
        public long? ElectricityPower
        {
            get
            {
                CheckPropertyInited("ElectricityPower");
                return _electricitypower;
            }
            set
            {
                _electricitypower = value;
                NotifyPropertyChanged("ElectricityPower");
            }
        }


        private string _gaslocationtype;
        /// <summary>
        /// 10009900 Локация газоснабжения (gas_location_type)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009900)]
        public string GasLocationType
        {
            get
            {
                CheckPropertyInited("GasLocationType");
                return _gaslocationtype;
            }
            set
            {
                _gaslocationtype = value;
                NotifyPropertyChanged("GasLocationType");
            }
        }


        private GasLocationType _gaslocationtype_Code;
        /// <summary>
        /// 10009900 Локация газоснабжения (справочный код) (gas_location_type_code)
        /// </summary>
        [RegisterAttribute(AttributeID = 10009900)]
        public GasLocationType GasLocationType_Code
        {
            get
            {
                CheckPropertyInited("GasLocationType_Code");
                return this._gaslocationtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_gaslocationtype))
                    {
                         _gaslocationtype = descr;
                    }
                }
                else
                {
                     _gaslocationtype = descr;
                }

                this._gaslocationtype_Code = value;
                NotifyPropertyChanged("GasLocationType");
                NotifyPropertyChanged("GasLocationType_Code");
            }
        }


        private bool? _possibilitytoconnectgas;
        /// <summary>
        /// 10010000 Возможность подключения газа (possibility_to_connect_gas)
        /// </summary>
        [RegisterAttribute(AttributeID = 10010000)]
        public bool? PossibilityToConnectGas
        {
            get
            {
                CheckPropertyInited("PossibilityToConnectGas");
                return _possibilitytoconnectgas;
            }
            set
            {
                _possibilitytoconnectgas = value;
                NotifyPropertyChanged("PossibilityToConnectGas");
            }
        }


        private long? _gascapacity;
        /// <summary>
        /// 10010100 Емкость газа (gas_capacity)
        /// </summary>
        [RegisterAttribute(AttributeID = 10010100)]
        public long? GasCapacity
        {
            get
            {
                CheckPropertyInited("GasCapacity");
                return _gascapacity;
            }
            set
            {
                _gascapacity = value;
                NotifyPropertyChanged("GasCapacity");
            }
        }


        private string _gaspressuretype;
        /// <summary>
        /// 10010200 Давление газа (gas_pressure_type)
        /// </summary>
        [RegisterAttribute(AttributeID = 10010200)]
        public string GasPressureType
        {
            get
            {
                CheckPropertyInited("GasPressureType");
                return _gaspressuretype;
            }
            set
            {
                _gaspressuretype = value;
                NotifyPropertyChanged("GasPressureType");
            }
        }


        private GasPressureType _gaspressuretype_Code;
        /// <summary>
        /// 10010200 Давление газа (справочный код) (gas_pressure_type_code)
        /// </summary>
        [RegisterAttribute(AttributeID = 10010200)]
        public GasPressureType GasPressureType_Code
        {
            get
            {
                CheckPropertyInited("GasPressureType_Code");
                return this._gaspressuretype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_gaspressuretype))
                    {
                         _gaspressuretype = descr;
                    }
                }
                else
                {
                     _gaspressuretype = descr;
                }

                this._gaspressuretype_Code = value;
                NotifyPropertyChanged("GasPressureType");
                NotifyPropertyChanged("GasPressureType_Code");
            }
        }


        private string _drainagelocationtype;
        /// <summary>
        /// 10010300 Локация канализации (drainage_location_type)
        /// </summary>
        [RegisterAttribute(AttributeID = 10010300)]
        public string DrainageLocationType
        {
            get
            {
                CheckPropertyInited("DrainageLocationType");
                return _drainagelocationtype;
            }
            set
            {
                _drainagelocationtype = value;
                NotifyPropertyChanged("DrainageLocationType");
            }
        }


        private DrainageLocationType _drainagelocationtype_Code;
        /// <summary>
        /// 10010300 Локация канализации (справочный код) (drainage_location_type_code)
        /// </summary>
        [RegisterAttribute(AttributeID = 10010300)]
        public DrainageLocationType DrainageLocationType_Code
        {
            get
            {
                CheckPropertyInited("DrainageLocationType_Code");
                return this._drainagelocationtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_drainagelocationtype))
                    {
                         _drainagelocationtype = descr;
                    }
                }
                else
                {
                     _drainagelocationtype = descr;
                }

                this._drainagelocationtype_Code = value;
                NotifyPropertyChanged("DrainageLocationType");
                NotifyPropertyChanged("DrainageLocationType_Code");
            }
        }


        private bool? _possibilitytoconnectdrainage;
        /// <summary>
        /// 10010400 Возможность подключения канализации (possibility_to_connect_drainage)
        /// </summary>
        [RegisterAttribute(AttributeID = 10010400)]
        public bool? PossibilityToConnectDrainage
        {
            get
            {
                CheckPropertyInited("PossibilityToConnectDrainage");
                return _possibilitytoconnectdrainage;
            }
            set
            {
                _possibilitytoconnectdrainage = value;
                NotifyPropertyChanged("PossibilityToConnectDrainage");
            }
        }


        private long? _drainagecapacity;
        /// <summary>
        /// 10010500 Объем канализации (drainage_capacity)
        /// </summary>
        [RegisterAttribute(AttributeID = 10010500)]
        public long? DrainageCapacity
        {
            get
            {
                CheckPropertyInited("DrainageCapacity");
                return _drainagecapacity;
            }
            set
            {
                _drainagecapacity = value;
                NotifyPropertyChanged("DrainageCapacity");
            }
        }


        private string _drainagetype;
        /// <summary>
        /// 10010600 Тип канализации (drainage_type)
        /// </summary>
        [RegisterAttribute(AttributeID = 10010600)]
        public string DrainageType
        {
            get
            {
                CheckPropertyInited("DrainageType");
                return _drainagetype;
            }
            set
            {
                _drainagetype = value;
                NotifyPropertyChanged("DrainageType");
            }
        }


        private DrainageType _drainagetype_Code;
        /// <summary>
        /// 10010600 Тип канализации (справочный код) (drainage_type_code)
        /// </summary>
        [RegisterAttribute(AttributeID = 10010600)]
        public DrainageType DrainageType_Code
        {
            get
            {
                CheckPropertyInited("DrainageType_Code");
                return this._drainagetype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_drainagetype))
                    {
                         _drainagetype = descr;
                    }
                }
                else
                {
                     _drainagetype = descr;
                }

                this._drainagetype_Code = value;
                NotifyPropertyChanged("DrainageType");
                NotifyPropertyChanged("DrainageType_Code");
            }
        }


        private string _waterlocationtype;
        /// <summary>
        /// 10010700 Тип локации водоснабжения (water_location_type)
        /// </summary>
        [RegisterAttribute(AttributeID = 10010700)]
        public string WaterLocationType
        {
            get
            {
                CheckPropertyInited("WaterLocationType");
                return _waterlocationtype;
            }
            set
            {
                _waterlocationtype = value;
                NotifyPropertyChanged("WaterLocationType");
            }
        }


        private WaterLocationType _waterlocationtype_Code;
        /// <summary>
        /// 10010700 Тип локации водоснабжения (справочный код) (water_location_type_code)
        /// </summary>
        [RegisterAttribute(AttributeID = 10010700)]
        public WaterLocationType WaterLocationType_Code
        {
            get
            {
                CheckPropertyInited("WaterLocationType_Code");
                return this._waterlocationtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_waterlocationtype))
                    {
                         _waterlocationtype = descr;
                    }
                }
                else
                {
                     _waterlocationtype = descr;
                }

                this._waterlocationtype_Code = value;
                NotifyPropertyChanged("WaterLocationType");
                NotifyPropertyChanged("WaterLocationType_Code");
            }
        }


        private bool? _possibilitytoconnectwater;
        /// <summary>
        /// 10010800 Возможность подключения воды (possibility_to_connect_water)
        /// </summary>
        [RegisterAttribute(AttributeID = 10010800)]
        public bool? PossibilityToConnectWater
        {
            get
            {
                CheckPropertyInited("PossibilityToConnectWater");
                return _possibilitytoconnectwater;
            }
            set
            {
                _possibilitytoconnectwater = value;
                NotifyPropertyChanged("PossibilityToConnectWater");
            }
        }


        private long? _watercapacity;
        /// <summary>
        /// 10010900 Объем водоснабжения (water_capacity)
        /// </summary>
        [RegisterAttribute(AttributeID = 10010900)]
        public long? WaterCapacity
        {
            get
            {
                CheckPropertyInited("WaterCapacity");
                return _watercapacity;
            }
            set
            {
                _watercapacity = value;
                NotifyPropertyChanged("WaterCapacity");
            }
        }


        private string _watertype;
        /// <summary>
        /// 10011000 Тип водоснабжения (water_type)
        /// </summary>
        [RegisterAttribute(AttributeID = 10011000)]
        public string WaterType
        {
            get
            {
                CheckPropertyInited("WaterType");
                return _watertype;
            }
            set
            {
                _watertype = value;
                NotifyPropertyChanged("WaterType");
            }
        }


        private WaterType _watertype_Code;
        /// <summary>
        /// 10011000 Тип водоснабжения (справочный код) (water_type_code)
        /// </summary>
        [RegisterAttribute(AttributeID = 10011000)]
        public WaterType WaterType_Code
        {
            get
            {
                CheckPropertyInited("WaterType_Code");
                return this._watertype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_watertype))
                    {
                         _watertype = descr;
                    }
                }
                else
                {
                     _watertype = descr;
                }

                this._watertype_Code = value;
                NotifyPropertyChanged("WaterType");
                NotifyPropertyChanged("WaterType_Code");
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
    /// 118 Таблица, содержащая коэффициенты для проверки на выбросы (MARKET_COEFF_FOR_OUTLIERS_CHECKING)
    /// </summary>
    [RegisterInfo(RegisterID = 118)]
    [Serializable]
    public partial class OMCoefficientsOutliersChecking : OMBaseClass<OMCoefficientsOutliersChecking>
    {

        private long _id;
        /// <summary>
        /// 11800100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 11800100)]
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


        private long _zone;
        /// <summary>
        /// 11800200 Зона (ZONE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11800200)]
        public long Zone
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


        private string _district;
        /// <summary>
        /// 11800300 Округ (DISTRICT)
        /// </summary>
        [RegisterAttribute(AttributeID = 11800300)]
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
        /// 11800300 Округ (справочный код) (DISTRICT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11800300)]
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
        /// 11800400 Район (REGION)
        /// </summary>
        [RegisterAttribute(AttributeID = 11800400)]
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
        /// 11800400 Район (справочный код) (REGION_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11800400)]
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


        private decimal? _mindeltacoef;
        /// <summary>
        /// 11800500 Коэффициент разности минимального значения (MIN_DELTA_COEF)
        /// </summary>
        [RegisterAttribute(AttributeID = 11800500)]
        public decimal? MinDeltaCoef
        {
            get
            {
                CheckPropertyInited("MinDeltaCoef");
                return _mindeltacoef;
            }
            set
            {
                _mindeltacoef = value;
                NotifyPropertyChanged("MinDeltaCoef");
            }
        }


        private decimal? _maxdeltacoef;
        /// <summary>
        /// 11800600 Коэффициент разности максимального значения (MAX_DELTA_COEF)
        /// </summary>
        [RegisterAttribute(AttributeID = 11800600)]
        public decimal? MaxDeltaCoef
        {
            get
            {
                CheckPropertyInited("MaxDeltaCoef");
                return _maxdeltacoef;
            }
            set
            {
                _maxdeltacoef = value;
                NotifyPropertyChanged("MaxDeltaCoef");
            }
        }

    }
}

namespace ObjectModel.Market
{
    /// <summary>
    /// 119 Таблица, содержащая информацию о проведённых проверках на выбросы (MARKET_OUTLIERS_CHECKING_HISTORY)
    /// </summary>
    [RegisterInfo(RegisterID = 119)]
    [Serializable]
    public partial class OMOutliersCheckingHistory : OMBaseClass<OMOutliersCheckingHistory>
    {

        private long _id;
        /// <summary>
        /// 11900100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 11900100)]
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


        private DateTime _datecreated;
        /// <summary>
        /// 11900200 Дата создания (DATE_CREATED)
        /// </summary>
        [RegisterAttribute(AttributeID = 11900200)]
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
        /// 11900300 Дата запуска (DATE_STARTED)
        /// </summary>
        [RegisterAttribute(AttributeID = 11900300)]
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
        /// 11900400 Дата завершения (DATE_FINISHED)
        /// </summary>
        [RegisterAttribute(AttributeID = 11900400)]
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


        private string _status;
        /// <summary>
        /// 11900500 Статус (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 11900500)]
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
        /// 11900500 Статус (справочный код) (STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11900500)]
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


        private string _marketsegment;
        /// <summary>
        /// 11900600 Сегмент рынка (MARKET_SEGMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 11900600)]
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
        /// 11900600 Сегмент рынка (справочный код) (MARKET_SEGMENT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 11900600)]
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


        private long? _totalobjectscount;
        /// <summary>
        /// 11900700 Общее количество объектов (TOTAL_OBJECTS_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 11900700)]
        public long? TotalObjectsCount
        {
            get
            {
                CheckPropertyInited("TotalObjectsCount");
                return _totalobjectscount;
            }
            set
            {
                _totalobjectscount = value;
                NotifyPropertyChanged("TotalObjectsCount");
            }
        }


        private long? _currenthandledobjectscount;
        /// <summary>
        /// 11900800 Количество обработанных объектов (CURRENT_HANDLED_OBJECTS_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 11900800)]
        public long? CurrentHandledObjectsCount
        {
            get
            {
                CheckPropertyInited("CurrentHandledObjectsCount");
                return _currenthandledobjectscount;
            }
            set
            {
                _currenthandledobjectscount = value;
                NotifyPropertyChanged("CurrentHandledObjectsCount");
            }
        }


        private long? _excludedobjectscount;
        /// <summary>
        /// 11900900 Количество исключенных объектов (EXCLUDED_OBJECTS_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 11900900)]
        public long? ExcludedObjectsCount
        {
            get
            {
                CheckPropertyInited("ExcludedObjectsCount");
                return _excludedobjectscount;
            }
            set
            {
                _excludedobjectscount = value;
                NotifyPropertyChanged("ExcludedObjectsCount");
            }
        }


        private long? _exportid;
        /// <summary>
        /// 11901000 ИД экспорта с результатом  (EXPORT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 11901000)]
        public long? ExportId
        {
            get
            {
                CheckPropertyInited("ExportId");
                return _exportid;
            }
            set
            {
                _exportid = value;
                NotifyPropertyChanged("ExportId");
            }
        }


        private string _propertytypesmapping;
        /// <summary>
        /// 11901100 Список используемых видов объекта недвижимости (PROPERTY_TYPES_MAPPING)
        /// </summary>
        [RegisterAttribute(AttributeID = 11901100)]
        public string PropertyTypesMapping
        {
            get
            {
                CheckPropertyInited("PropertyTypesMapping");
                return _propertytypesmapping;
            }
            set
            {
                _propertytypesmapping = value;
                NotifyPropertyChanged("PropertyTypesMapping");
            }
        }

    }
}
