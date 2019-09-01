using System;
using Core.ObjectModel;
using Core.ObjectModel.CustomAttribute;
using Core.Shared.Extensions;
using ObjectModel.Directory;
namespace ObjectModel.Bti
{
    /// <summary>
    /// 50 БТИ: Адрес (BTI_ADDRESS_Q)
    /// </summary>
    [RegisterInfo(RegisterID = 50)]
    [Serializable]
    public sealed partial class OMADDRESS : OMBaseClass<OMADDRESS>
    {

        private long _empid;
        /// <summary>
        /// 50000100 Инд.номер (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 50000100)]
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


        private string _fullname;
        /// <summary>
        /// 50000200 Полное наименование (FULL_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50000200)]
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
        /// 50000300 Краткое наименование (SHORT_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50000300)]
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


        private string _nameforsort;
        /// <summary>
        /// 50000400 Наименование для сортировки (NAME_FOR_SORT)
        /// </summary>
        [RegisterAttribute(AttributeID = 50000400)]
        public string NameForSort
        {
            get
            {
                CheckPropertyInited("NameForSort");
                return _nameforsort;
            }
            set
            {
                _nameforsort = value;
                NotifyPropertyChanged("NameForSort");
            }
        }


        private string _mainname;
        /// <summary>
        /// 50000500 Основное наименование (MAIN_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50000500)]
        public string MainName
        {
            get
            {
                CheckPropertyInited("MainName");
                return _mainname;
            }
            set
            {
                _mainname = value;
                NotifyPropertyChanged("MainName");
            }
        }


        private string _mainnameprint;
        /// <summary>
        /// 50000600 Основное наименование (MAIN_NAME_PRINT)
        /// </summary>
        [RegisterAttribute(AttributeID = 50000600)]
        public string MainNamePrint
        {
            get
            {
                CheckPropertyInited("MainNamePrint");
                return _mainnameprint;
            }
            set
            {
                _mainnameprint = value;
                NotifyPropertyChanged("MainNamePrint");
            }
        }


        private string _fullnameprint;
        /// <summary>
        /// 50000700 Полное наименование (FULL_NAME_PRINT)
        /// </summary>
        [RegisterAttribute(AttributeID = 50000700)]
        public string FullNamePrint
        {
            get
            {
                CheckPropertyInited("FullNamePrint");
                return _fullnameprint;
            }
            set
            {
                _fullnameprint = value;
                NotifyPropertyChanged("FullNamePrint");
            }
        }


        private string _idinds;
        /// <summary>
        /// 50000800 ID в источнике данных (ID_IN_DS)
        /// </summary>
        [RegisterAttribute(AttributeID = 50000800)]
        public string IdInDs
        {
            get
            {
                CheckPropertyInited("IdInDs");
                return _idinds;
            }
            set
            {
                _idinds = value;
                NotifyPropertyChanged("IdInDs");
            }
        }


        private string _datasourcename;
        /// <summary>
        /// 50000900 Источник данных (DATA_SOURCE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50000900)]
        public string DataSourceName
        {
            get
            {
                CheckPropertyInited("DataSourceName");
                return _datasourcename;
            }
            set
            {
                _datasourcename = value;
                NotifyPropertyChanged("DataSourceName");
            }
        }


        private InsuranceSourceType _datasourcename_Code;
        /// <summary>
        /// 50000900 Источник данных (справочный код) (DATA_SOURCE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50000900)]
        public InsuranceSourceType DataSourceName_Code
        {
            get
            {
                CheckPropertyInited("DataSourceName_Code");
                return this._datasourcename_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_datasourcename))
                    {
                         _datasourcename = descr;
                    }
                }
                else
                {
                     _datasourcename = descr;
                }

                this._datasourcename_Code = value;
                NotifyPropertyChanged("DataSourceName");
                NotifyPropertyChanged("DataSourceName_Code");
            }
        }


        private string _subjectrfname;
        /// <summary>
        /// 50001000 Субъект РФ (SUBJECT_RF_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50001000)]
        public string SubjectRfName
        {
            get
            {
                CheckPropertyInited("SubjectRfName");
                return _subjectrfname;
            }
            set
            {
                _subjectrfname = value;
                NotifyPropertyChanged("SubjectRfName");
            }
        }


        private long? _subjectrfname_Code;
        /// <summary>
        /// 50001000 Субъект РФ (справочный код) (SUBJECT_RF_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50001000)]
        public long? SubjectRfName_Code
        {
            get
            {
                CheckPropertyInited("SubjectRfName_Code");
                return _subjectrfname_Code;
            }
            set
            {
                _subjectrfname_Code = value;
                NotifyPropertyChanged("SubjectRfName_Code");
            }
        }


        private long? _okrugid;
        /// <summary>
        /// 50001100 Округ (OKRUG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50001100)]
        public long? OkrugId
        {
            get
            {
                CheckPropertyInited("OkrugId");
                return _okrugid;
            }
            set
            {
                _okrugid = value;
                NotifyPropertyChanged("OkrugId");
            }
        }


        private long? _districtid;
        /// <summary>
        /// 50001200 Район (DISTRICT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50001200)]
        public long? DistrictId
        {
            get
            {
                CheckPropertyInited("DistrictId");
                return _districtid;
            }
            set
            {
                _districtid = value;
                NotifyPropertyChanged("DistrictId");
            }
        }


        private string _settlementname;
        /// <summary>
        /// 50001300 Городское/сельское поселение (SETTLEMENT_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50001300)]
        public string SettlementName
        {
            get
            {
                CheckPropertyInited("SettlementName");
                return _settlementname;
            }
            set
            {
                _settlementname = value;
                NotifyPropertyChanged("SettlementName");
            }
        }


        private long? _settlementname_Code;
        /// <summary>
        /// 50001300 Городское/сельское поселение (справочный код) (SETTLEMENT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50001300)]
        public long? SettlementName_Code
        {
            get
            {
                CheckPropertyInited("SettlementName_Code");
                return _settlementname_Code;
            }
            set
            {
                _settlementname_Code = value;
                NotifyPropertyChanged("SettlementName_Code");
            }
        }


        private string _townname;
        /// <summary>
        /// 50001400 Город/НП (TOWN_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50001400)]
        public string TownName
        {
            get
            {
                CheckPropertyInited("TownName");
                return _townname;
            }
            set
            {
                _townname = value;
                NotifyPropertyChanged("TownName");
            }
        }


        private long? _townname_Code;
        /// <summary>
        /// 50001400 Город/НП (справочный код) (TOWN_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50001400)]
        public long? TownName_Code
        {
            get
            {
                CheckPropertyInited("TownName_Code");
                return _townname_Code;
            }
            set
            {
                _townname_Code = value;
                NotifyPropertyChanged("TownName_Code");
            }
        }


        private string _psename;
        /// <summary>
        /// 50001500 Элемент планир. структуры (PSE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50001500)]
        public string PseName
        {
            get
            {
                CheckPropertyInited("PseName");
                return _psename;
            }
            set
            {
                _psename = value;
                NotifyPropertyChanged("PseName");
            }
        }


        private long? _psename_Code;
        /// <summary>
        /// 50001500 Элемент планир. структуры (справочный код) (PSE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50001500)]
        public long? PseName_Code
        {
            get
            {
                CheckPropertyInited("PseName_Code");
                return _psename_Code;
            }
            set
            {
                _psename_Code = value;
                NotifyPropertyChanged("PseName_Code");
            }
        }


        private string _streetname;
        /// <summary>
        /// 50001600 Улица (STREET_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50001600)]
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


        private long? _streetname_Code;
        /// <summary>
        /// 50001600 Улица (справочный код) (STREET_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50001600)]
        public long? StreetName_Code
        {
            get
            {
                CheckPropertyInited("StreetName_Code");
                return _streetname_Code;
            }
            set
            {
                _streetname_Code = value;
                NotifyPropertyChanged("StreetName_Code");
            }
        }


        private string _propertytypename;
        /// <summary>
        /// 50001700 Тип владения (PROPERTY_TYPE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50001700)]
        public string PropertyTypeName
        {
            get
            {
                CheckPropertyInited("PropertyTypeName");
                return _propertytypename;
            }
            set
            {
                _propertytypename = value;
                NotifyPropertyChanged("PropertyTypeName");
            }
        }


        private long? _propertytypename_Code;
        /// <summary>
        /// 50001700 Тип владения (справочный код) (PROPERTY_TYPE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50001700)]
        public long? PropertyTypeName_Code
        {
            get
            {
                CheckPropertyInited("PropertyTypeName_Code");
                return _propertytypename_Code;
            }
            set
            {
                _propertytypename_Code = value;
                NotifyPropertyChanged("PropertyTypeName_Code");
            }
        }


        private string _plotnumber;
        /// <summary>
        /// 50001800 Номер участка (PLOT_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 50001800)]
        public string PlotNumber
        {
            get
            {
                CheckPropertyInited("PlotNumber");
                return _plotnumber;
            }
            set
            {
                _plotnumber = value;
                NotifyPropertyChanged("PlotNumber");
            }
        }


        private string _housenumber;
        /// <summary>
        /// 50001900 Дом (HOUSE_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 50001900)]
        public string HouseNumber
        {
            get
            {
                CheckPropertyInited("HouseNumber");
                return _housenumber;
            }
            set
            {
                _housenumber = value;
                NotifyPropertyChanged("HouseNumber");
            }
        }


        private string _korpusnumber;
        /// <summary>
        /// 50002000 Корпус (KORPUS_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 50002000)]
        public string KorpusNumber
        {
            get
            {
                CheckPropertyInited("KorpusNumber");
                return _korpusnumber;
            }
            set
            {
                _korpusnumber = value;
                NotifyPropertyChanged("KorpusNumber");
            }
        }


        private string _structuretypename;
        /// <summary>
        /// 50002100 Тип сооружения (адр) (STRUCTURE_TYPE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 50002100)]
        public string StructureTypeName
        {
            get
            {
                CheckPropertyInited("StructureTypeName");
                return _structuretypename;
            }
            set
            {
                _structuretypename = value;
                NotifyPropertyChanged("StructureTypeName");
            }
        }


        private long? _structuretypename_Code;
        /// <summary>
        /// 50002100 Тип сооружения (адр) (справочный код) (STRUCTURE_TYPE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50002100)]
        public long? StructureTypeName_Code
        {
            get
            {
                CheckPropertyInited("StructureTypeName_Code");
                return _structuretypename_Code;
            }
            set
            {
                _structuretypename_Code = value;
                NotifyPropertyChanged("StructureTypeName_Code");
            }
        }


        private string _structurenumber;
        /// <summary>
        /// 50002200 Строение (STRUCTURE_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 50002200)]
        public string StructureNumber
        {
            get
            {
                CheckPropertyInited("StructureNumber");
                return _structurenumber;
            }
            set
            {
                _structurenumber = value;
                NotifyPropertyChanged("StructureNumber");
            }
        }


        private string _letternumber;
        /// <summary>
        /// 50002300 Литера (LETTER_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 50002300)]
        public string LetterNumber
        {
            get
            {
                CheckPropertyInited("LetterNumber");
                return _letternumber;
            }
            set
            {
                _letternumber = value;
                NotifyPropertyChanged("LetterNumber");
            }
        }


        private string _locationdesc;
        /// <summary>
        /// 50002400 Описание местоположения (LOCATION_DESC)
        /// </summary>
        [RegisterAttribute(AttributeID = 50002400)]
        public string LocationDesc
        {
            get
            {
                CheckPropertyInited("LocationDesc");
                return _locationdesc;
            }
            set
            {
                _locationdesc = value;
                NotifyPropertyChanged("LocationDesc");
            }
        }


        private string _okatocode;
        /// <summary>
        /// 50002600 Код ОКАТО (OKATO_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 50002600)]
        public string OkatoCode
        {
            get
            {
                CheckPropertyInited("OkatoCode");
                return _okatocode;
            }
            set
            {
                _okatocode = value;
                NotifyPropertyChanged("OkatoCode");
            }
        }


        private string _kladrcode;
        /// <summary>
        /// 50002700 Код КЛАДР (KLADR_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 50002700)]
        public string KladrCode
        {
            get
            {
                CheckPropertyInited("KladrCode");
                return _kladrcode;
            }
            set
            {
                _kladrcode = value;
                NotifyPropertyChanged("KladrCode");
            }
        }


        private string _indexpostal;
        /// <summary>
        /// 50002800 Почтовый индекс (INDEX_POSTAL)
        /// </summary>
        [RegisterAttribute(AttributeID = 50002800)]
        public string IndexPostal
        {
            get
            {
                CheckPropertyInited("IndexPostal");
                return _indexpostal;
            }
            set
            {
                _indexpostal = value;
                NotifyPropertyChanged("IndexPostal");
            }
        }


        private string _typecorpus;
        /// <summary>
        /// 50002900 Тип корпуса (TYPE_CORPUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 50002900)]
        public string TypeCorpus
        {
            get
            {
                CheckPropertyInited("TypeCorpus");
                return _typecorpus;
            }
            set
            {
                _typecorpus = value;
                NotifyPropertyChanged("TypeCorpus");
            }
        }


        private long? _typecorpus_Code;
        /// <summary>
        /// 50002900 Тип корпуса (справочный код) (TYPE_CORPUS_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50002900)]
        public long? TypeCorpus_Code
        {
            get
            {
                CheckPropertyInited("TypeCorpus_Code");
                return _typecorpus_Code;
            }
            set
            {
                _typecorpus_Code = value;
                NotifyPropertyChanged("TypeCorpus_Code");
            }
        }


        private string _codensi;
        /// <summary>
        /// 50003000 ИД адреса в НСИ (CODE_NSI)
        /// </summary>
        [RegisterAttribute(AttributeID = 50003000)]
        public string CodeNsi
        {
            get
            {
                CheckPropertyInited("CodeNsi");
                return _codensi;
            }
            set
            {
                _codensi = value;
                NotifyPropertyChanged("CodeNsi");
            }
        }


        private string _other;
        /// <summary>
        /// 50003100 Иное (OTHER)
        /// </summary>
        [RegisterAttribute(AttributeID = 50003100)]
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


        private string _oktmo;
        /// <summary>
        /// 50003200 Код ОКТМО (OKTMO)
        /// </summary>
        [RegisterAttribute(AttributeID = 50003200)]
        public string Oktmo
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


        private Oktmo _oktmo_Code;
        /// <summary>
        /// 50003200 Код ОКТМО (справочный код) (OKTMO_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 50003200)]
        public Oktmo Oktmo_Code
        {
            get
            {
                CheckPropertyInited("Oktmo_Code");
                return this._oktmo_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_oktmo))
                    {
                         _oktmo = descr;
                    }
                }
                else
                {
                     _oktmo = descr;
                }

                this._oktmo_Code = value;
                NotifyPropertyChanged("Oktmo");
                NotifyPropertyChanged("Oktmo_Code");
            }
        }


        private string _fullmixaddress;
        /// <summary>
        /// 50003300 Адрес для полнотекстового поиска (FULL_MIX_ADDRESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 50003300)]
        public string FullMixAddress
        {
            get
            {
                CheckPropertyInited("FullMixAddress");
                return _fullmixaddress;
            }
            set
            {
                _fullmixaddress = value;
                NotifyPropertyChanged("FullMixAddress");
            }
        }


        private string _oktmofromokato;
        /// <summary>
        /// 50003400 Код ОКТМО по ОКАТО ()
        /// </summary>
        [RegisterAttribute(AttributeID = 50003400)]
        public string OktmoFromOkato
        {
            get
            {
                CheckPropertyInited("OktmoFromOkato");
                return _oktmofromokato;
            }
            set
            {
                _oktmofromokato = value;
                NotifyPropertyChanged("OktmoFromOkato");
            }
        }


        private long? _addressorlocation;
        /// <summary>
        /// 50003500 Признак адреса (ADDRESS_OR_LOCATION)
        /// </summary>
        [RegisterAttribute(AttributeID = 50003500)]
        public long? AddressOrLocation
        {
            get
            {
                CheckPropertyInited("AddressOrLocation");
                return _addressorlocation;
            }
            set
            {
                _addressorlocation = value;
                NotifyPropertyChanged("AddressOrLocation");
            }
        }


        private string _codefias;
        /// <summary>
        /// 50003600 Код ФИАС (CODE_FIAS)
        /// </summary>
        [RegisterAttribute(AttributeID = 50003600)]
        public string CodeFias
        {
            get
            {
                CheckPropertyInited("CodeFias");
                return _codefias;
            }
            set
            {
                _codefias = value;
                NotifyPropertyChanged("CodeFias");
            }
        }

    }
}

namespace ObjectModel.Bti
{
    /// <summary>
    /// 52 БТИ: Адрес здания (связь) (BTI_ADDRLINK_Q)
    /// </summary>
    [RegisterInfo(RegisterID = 52)]
    [Serializable]
    public sealed partial class OMADDRLINK : OMBaseClass<OMADDRLINK>
    {

        private string _addressstate;
        /// <summary>
        /// 5200100 Состояние адреса (ADDRESS_STATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 5200100)]
        public string AddressState
        {
            get
            {
                CheckPropertyInited("AddressState");
                return _addressstate;
            }
            set
            {
                _addressstate = value;
                NotifyPropertyChanged("AddressState");
            }
        }


        private string _addressstatename;
        /// <summary>
        /// 5200101 Состояние адреса - С.А. (ADDRESS_STATE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 5200101)]
        public string AddressStateName
        {
            get
            {
                CheckPropertyInited("AddressStateName");
                return _addressstatename;
            }
            set
            {
                _addressstatename = value;
                NotifyPropertyChanged("AddressStateName");
            }
        }


        private long? _addressstatename_Code;
        /// <summary>
        /// 5200101 Состояние адреса - С.А. (справочный код) (ADDRESS_STATE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 5200101)]
        public long? AddressStateName_Code
        {
            get
            {
                CheckPropertyInited("AddressStateName_Code");
                return _addressstatename_Code;
            }
            set
            {
                _addressstatename_Code = value;
                NotifyPropertyChanged("AddressStateName_Code");
            }
        }


        private DateTime? _addressstatedate;
        /// <summary>
        /// 5200102 Дата состояния - С.А. (ADDRESS_STATE_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 5200102)]
        public DateTime? AddressStateDate
        {
            get
            {
                CheckPropertyInited("AddressStateDate");
                return _addressstatedate;
            }
            set
            {
                _addressstatedate = value;
                NotifyPropertyChanged("AddressStateDate");
            }
        }


        private string _addressstatusname;
        /// <summary>
        /// 5200200 Статус адреса (ADDRESS_STATUS_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 5200200)]
        public string AddressStatusName
        {
            get
            {
                CheckPropertyInited("AddressStatusName");
                return _addressstatusname;
            }
            set
            {
                _addressstatusname = value;
                NotifyPropertyChanged("AddressStatusName");
            }
        }


        private AddressStatus _addressstatusname_Code;
        /// <summary>
        /// 5200200 Статус адреса (справочный код) (ADDRESS_STATUS_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 5200200)]
        public AddressStatus AddressStatusName_Code
        {
            get
            {
                CheckPropertyInited("AddressStatusName_Code");
                return this._addressstatusname_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_addressstatusname))
                    {
                         _addressstatusname = descr;
                    }
                }
                else
                {
                     _addressstatusname = descr;
                }

                this._addressstatusname_Code = value;
                NotifyPropertyChanged("AddressStatusName");
                NotifyPropertyChanged("AddressStatusName_Code");
            }
        }


        private long _empid;
        /// <summary>
        /// 5200300 Инд.номер (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 5200300)]
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


        private string _grounddocument;
        /// <summary>
        /// 5200400 Документ основание (Д.О.) (GROUND_DOCUMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 5200400)]
        public string GroundDocument
        {
            get
            {
                CheckPropertyInited("GroundDocument");
                return _grounddocument;
            }
            set
            {
                _grounddocument = value;
                NotifyPropertyChanged("GroundDocument");
            }
        }


        private string _grounddocumenttypename;
        /// <summary>
        /// 5200401 Тип документа - Д.О. (GROUND_DOCUMENT_TYPE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 5200401)]
        public string GroundDocumentTypeName
        {
            get
            {
                CheckPropertyInited("GroundDocumentTypeName");
                return _grounddocumenttypename;
            }
            set
            {
                _grounddocumenttypename = value;
                NotifyPropertyChanged("GroundDocumentTypeName");
            }
        }


        private long? _grounddocumenttypename_Code;
        /// <summary>
        /// 5200401 Тип документа - Д.О. (справочный код) (GROUND_DOCUMENT_TYPE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 5200401)]
        public long? GroundDocumentTypeName_Code
        {
            get
            {
                CheckPropertyInited("GroundDocumentTypeName_Code");
                return _grounddocumenttypename_Code;
            }
            set
            {
                _grounddocumenttypename_Code = value;
                NotifyPropertyChanged("GroundDocumentTypeName_Code");
            }
        }


        private string _grounddocumentnumber;
        /// <summary>
        /// 5200402 Номер документа - Д.О. (GROUND_DOCUMENT_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 5200402)]
        public string GroundDocumentNumber
        {
            get
            {
                CheckPropertyInited("GroundDocumentNumber");
                return _grounddocumentnumber;
            }
            set
            {
                _grounddocumentnumber = value;
                NotifyPropertyChanged("GroundDocumentNumber");
            }
        }


        private DateTime? _grounddocumentdateissue;
        /// <summary>
        /// 5200403 Дата выдачи документа - Д.О. (GROUND_DOCUMENT_DATE_ISSUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 5200403)]
        public DateTime? GroundDocumentDateIssue
        {
            get
            {
                CheckPropertyInited("GroundDocumentDateIssue");
                return _grounddocumentdateissue;
            }
            set
            {
                _grounddocumentdateissue = value;
                NotifyPropertyChanged("GroundDocumentDateIssue");
            }
        }


        private string _grounddocumentcontentname;
        /// <summary>
        /// 5200404 Содержание документа - Д.О. (GROUND_DOCUMENT_CONTENT_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 5200404)]
        public string GroundDocumentContentName
        {
            get
            {
                CheckPropertyInited("GroundDocumentContentName");
                return _grounddocumentcontentname;
            }
            set
            {
                _grounddocumentcontentname = value;
                NotifyPropertyChanged("GroundDocumentContentName");
            }
        }


        private long? _grounddocumentcontentname_Code;
        /// <summary>
        /// 5200404 Содержание документа - Д.О. (справочный код) (GROUND_DOCUMENT_CONTENT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 5200404)]
        public long? GroundDocumentContentName_Code
        {
            get
            {
                CheckPropertyInited("GroundDocumentContentName_Code");
                return _grounddocumentcontentname_Code;
            }
            set
            {
                _grounddocumentcontentname_Code = value;
                NotifyPropertyChanged("GroundDocumentContentName_Code");
            }
        }


        private long? _unad;
        /// <summary>
        /// 5200500 UNAD (UNAD)
        /// </summary>
        [RegisterAttribute(AttributeID = 5200500)]
        public long? Unad
        {
            get
            {
                CheckPropertyInited("Unad");
                return _unad;
            }
            set
            {
                _unad = value;
                NotifyPropertyChanged("Unad");
            }
        }


        private long? _addressid;
        /// <summary>
        /// 5200600 Адрес (ADDRESS_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 5200600)]
        public long? AddressId
        {
            get
            {
                CheckPropertyInited("AddressId");
                return _addressid;
            }
            set
            {
                _addressid = value;
                NotifyPropertyChanged("AddressId");
            }
        }


        private long? _buildingid;
        /// <summary>
        /// 5200700 ID объекта (BUILDING_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 5200700)]
        public long? BuildingId
        {
            get
            {
                CheckPropertyInited("BuildingId");
                return _buildingid;
            }
            set
            {
                _buildingid = value;
                NotifyPropertyChanged("BuildingId");
            }
        }


        private long? _regnumber;
        /// <summary>
        /// 5200800 Регистрационный номер в адресном реестре (REG_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 5200800)]
        public long? RegNumber
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


        private DateTime? _regdate;
        /// <summary>
        /// 5200900 Дата регистрации адреса (REG_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 5200900)]
        public DateTime? RegDate
        {
            get
            {
                CheckPropertyInited("RegDate");
                return _regdate;
            }
            set
            {
                _regdate = value;
                NotifyPropertyChanged("RegDate");
            }
        }


        private string _regdoctypename;
        /// <summary>
        /// 5201000 Тип документа-основания (рег) (REG_DOC_TYPE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 5201000)]
        public string RegDocTypeName
        {
            get
            {
                CheckPropertyInited("RegDocTypeName");
                return _regdoctypename;
            }
            set
            {
                _regdoctypename = value;
                NotifyPropertyChanged("RegDocTypeName");
            }
        }


        private long? _regdoctypename_Code;
        /// <summary>
        /// 5201000 Тип документа-основания (рег) (справочный код) (REG_DOC_TYPE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 5201000)]
        public long? RegDocTypeName_Code
        {
            get
            {
                CheckPropertyInited("RegDocTypeName_Code");
                return _regdoctypename_Code;
            }
            set
            {
                _regdoctypename_Code = value;
                NotifyPropertyChanged("RegDocTypeName_Code");
            }
        }


        private string _regdocnumber;
        /// <summary>
        /// 5201100 Номер документа-основания (рег) (REG_DOC_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 5201100)]
        public string RegDocNumber
        {
            get
            {
                CheckPropertyInited("RegDocNumber");
                return _regdocnumber;
            }
            set
            {
                _regdocnumber = value;
                NotifyPropertyChanged("RegDocNumber");
            }
        }


        private DateTime? _regdocdate;
        /// <summary>
        /// 5201200 Дата документа-основания (рег) (REG_DOC_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 5201200)]
        public DateTime? RegDocDate
        {
            get
            {
                CheckPropertyInited("RegDocDate");
                return _regdocdate;
            }
            set
            {
                _regdocdate = value;
                NotifyPropertyChanged("RegDocDate");
            }
        }


        private string _regdoccontentname;
        /// <summary>
        /// 5201300 Содержание документа-основания (рег) (REG_DOC_CONTENT_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 5201300)]
        public string RegDocContentName
        {
            get
            {
                CheckPropertyInited("RegDocContentName");
                return _regdoccontentname;
            }
            set
            {
                _regdoccontentname = value;
                NotifyPropertyChanged("RegDocContentName");
            }
        }


        private long? _regdoccontentname_Code;
        /// <summary>
        /// 5201300 Содержание документа-основания (рег) (справочный код) (REG_DOC_CONTENT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 5201300)]
        public long? RegDocContentName_Code
        {
            get
            {
                CheckPropertyInited("RegDocContentName_Code");
                return _regdoccontentname_Code;
            }
            set
            {
                _regdoccontentname_Code = value;
                NotifyPropertyChanged("RegDocContentName_Code");
            }
        }


        private long? _registerobjectnumber;
        /// <summary>
        /// 5201400 Номер реестра объекта (REGISTER_OBJECT_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 5201400)]
        public long? RegisterObjectNumber
        {
            get
            {
                CheckPropertyInited("RegisterObjectNumber");
                return _registerobjectnumber;
            }
            set
            {
                _registerobjectnumber = value;
                NotifyPropertyChanged("RegisterObjectNumber");
            }
        }


        private string _comments;
        /// <summary>
        /// 5201500 Комментарий  (COMMENTS)
        /// </summary>
        [RegisterAttribute(AttributeID = 5201500)]
        public string Comments
        {
            get
            {
                CheckPropertyInited("Comments");
                return _comments;
            }
            set
            {
                _comments = value;
                NotifyPropertyChanged("Comments");
            }
        }


        private string _textsource;
        /// <summary>
        /// 5201600 Источник данных (TEXT_SOURCE)
        /// </summary>
        [RegisterAttribute(AttributeID = 5201600)]
        public string TextSource
        {
            get
            {
                CheckPropertyInited("TextSource");
                return _textsource;
            }
            set
            {
                _textsource = value;
                NotifyPropertyChanged("TextSource");
            }
        }


        private InsuranceSourceType _textsource_Code;
        /// <summary>
        /// 5201600 Источник данных (справочный код) (ID_SOURCE)
        /// </summary>
        [RegisterAttribute(AttributeID = 5201600)]
        public InsuranceSourceType TextSource_Code
        {
            get
            {
                CheckPropertyInited("TextSource_Code");
                return this._textsource_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_textsource))
                    {
                         _textsource = descr;
                    }
                }
                else
                {
                     _textsource = descr;
                }

                this._textsource_Code = value;
                NotifyPropertyChanged("TextSource");
                NotifyPropertyChanged("TextSource_Code");
            }
        }

    }
}

namespace ObjectModel.Bti
{
    /// <summary>
    /// 250 БТИ: Адрес (BTI_FADS_Q)
    /// </summary>
    [RegisterInfo(RegisterID = 250)]
    [Serializable]
    public sealed partial class OMFads : OMBaseClass<OMFads>
    {

        private long _empid;
        /// <summary>
        /// 25000100 Инд.номер (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 25000100)]
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


        private long? _objid;
        /// <summary>
        /// 25000200 ID объекта недвижимости (OBJ_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25000200)]
        public long? ObjId
        {
            get
            {
                CheckPropertyInited("ObjId");
                return _objid;
            }
            set
            {
                _objid = value;
                NotifyPropertyChanged("ObjId");
            }
        }


        private string _objtype;
        /// <summary>
        /// 25000300 Тип объекта недвижимости (OBJ_TYPE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25000300)]
        public string ObjType
        {
            get
            {
                CheckPropertyInited("ObjType");
                return _objtype;
            }
            set
            {
                _objtype = value;
                NotifyPropertyChanged("ObjType");
            }
        }


        private long? _objtype_Code;
        /// <summary>
        /// 25000300 Тип объекта недвижимости (справочный код) (OBJ_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25000300)]
        public long? ObjType_Code
        {
            get
            {
                CheckPropertyInited("ObjType_Code");
                return _objtype_Code;
            }
            set
            {
                _objtype_Code = value;
                NotifyPropertyChanged("ObjType_Code");
            }
        }


        private long? _btibuildingid;
        /// <summary>
        /// 25000400 BtiBuilding ID (BTI_BUILDING_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25000400)]
        public long? BtiBuildingId
        {
            get
            {
                CheckPropertyInited("BtiBuildingId");
                return _btibuildingid;
            }
            set
            {
                _btibuildingid = value;
                NotifyPropertyChanged("BtiBuildingId");
            }
        }


        private long? _unad;
        /// <summary>
        /// 25000500 Порядковый номер адреса для объекта (UNAD)
        /// </summary>
        [RegisterAttribute(AttributeID = 25000500)]
        public long? Unad
        {
            get
            {
                CheckPropertyInited("Unad");
                return _unad;
            }
            set
            {
                _unad = value;
                NotifyPropertyChanged("Unad");
            }
        }


        private string _p1;
        /// <summary>
        /// 25000600 Субъект РФ (P1_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25000600)]
        public string P1
        {
            get
            {
                CheckPropertyInited("P1");
                return _p1;
            }
            set
            {
                _p1 = value;
                NotifyPropertyChanged("P1");
            }
        }


        private long? _p1_Code;
        /// <summary>
        /// 25000600 Субъект РФ (справочный код) (P1)
        /// </summary>
        [RegisterAttribute(AttributeID = 25000600)]
        public long? P1_Code
        {
            get
            {
                CheckPropertyInited("P1_Code");
                return _p1_Code;
            }
            set
            {
                _p1_Code = value;
                NotifyPropertyChanged("P1_Code");
            }
        }


        private string _p3;
        /// <summary>
        /// 25000700 Поселение (P3_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25000700)]
        public string P3
        {
            get
            {
                CheckPropertyInited("P3");
                return _p3;
            }
            set
            {
                _p3 = value;
                NotifyPropertyChanged("P3");
            }
        }


        private long? _p3_Code;
        /// <summary>
        /// 25000700 Поселение (справочный код) (P3)
        /// </summary>
        [RegisterAttribute(AttributeID = 25000700)]
        public long? P3_Code
        {
            get
            {
                CheckPropertyInited("P3_Code");
                return _p3_Code;
            }
            set
            {
                _p3_Code = value;
                NotifyPropertyChanged("P3_Code");
            }
        }


        private string _p4;
        /// <summary>
        /// 25000800 Город (P4_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25000800)]
        public string P4
        {
            get
            {
                CheckPropertyInited("P4");
                return _p4;
            }
            set
            {
                _p4 = value;
                NotifyPropertyChanged("P4");
            }
        }


        private long? _p4_Code;
        /// <summary>
        /// 25000800 Город (справочный код) (P4)
        /// </summary>
        [RegisterAttribute(AttributeID = 25000800)]
        public long? P4_Code
        {
            get
            {
                CheckPropertyInited("P4_Code");
                return _p4_Code;
            }
            set
            {
                _p4_Code = value;
                NotifyPropertyChanged("P4_Code");
            }
        }


        private string _p5;
        /// <summary>
        /// 25000900 Муниципальный округ (P5_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25000900)]
        public string P5
        {
            get
            {
                CheckPropertyInited("P5");
                return _p5;
            }
            set
            {
                _p5 = value;
                NotifyPropertyChanged("P5");
            }
        }


        private long? _p5_Code;
        /// <summary>
        /// 25000900 Муниципальный округ (справочный код) (P5)
        /// </summary>
        [RegisterAttribute(AttributeID = 25000900)]
        public long? P5_Code
        {
            get
            {
                CheckPropertyInited("P5_Code");
                return _p5_Code;
            }
            set
            {
                _p5_Code = value;
                NotifyPropertyChanged("P5_Code");
            }
        }


        private string _p6;
        /// <summary>
        /// 25001000 Населённый пункт (P6_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25001000)]
        public string P6
        {
            get
            {
                CheckPropertyInited("P6");
                return _p6;
            }
            set
            {
                _p6 = value;
                NotifyPropertyChanged("P6");
            }
        }


        private long? _p6_Code;
        /// <summary>
        /// 25001000 Населённый пункт (справочный код) (P6)
        /// </summary>
        [RegisterAttribute(AttributeID = 25001000)]
        public long? P6_Code
        {
            get
            {
                CheckPropertyInited("P6_Code");
                return _p6_Code;
            }
            set
            {
                _p6_Code = value;
                NotifyPropertyChanged("P6_Code");
            }
        }


        private string _p7;
        /// <summary>
        /// 25001100 Улица, элемент планировочной структуры (P7_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25001100)]
        public string P7
        {
            get
            {
                CheckPropertyInited("P7");
                return _p7;
            }
            set
            {
                _p7 = value;
                NotifyPropertyChanged("P7");
            }
        }


        private long? _p7_Code;
        /// <summary>
        /// 25001100 Улица, элемент планировочной структуры (справочный код) (P7)
        /// </summary>
        [RegisterAttribute(AttributeID = 25001100)]
        public long? P7_Code
        {
            get
            {
                CheckPropertyInited("P7_Code");
                return _p7_Code;
            }
            set
            {
                _p7_Code = value;
                NotifyPropertyChanged("P7_Code");
            }
        }


        private string _p90;
        /// <summary>
        /// 25001200 Дополнительный адресообразующий элемент (P90_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25001200)]
        public string P90
        {
            get
            {
                CheckPropertyInited("P90");
                return _p90;
            }
            set
            {
                _p90 = value;
                NotifyPropertyChanged("P90");
            }
        }


        private long? _p90_Code;
        /// <summary>
        /// 25001200 Дополнительный адресообразующий элемент (справочный код) (P90)
        /// </summary>
        [RegisterAttribute(AttributeID = 25001200)]
        public long? P90_Code
        {
            get
            {
                CheckPropertyInited("P90_Code");
                return _p90_Code;
            }
            set
            {
                _p90_Code = value;
                NotifyPropertyChanged("P90_Code");
            }
        }


        private string _p91;
        /// <summary>
        /// 25001300 Уточнение дополнительного адресообразующего элемента (P91_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25001300)]
        public string P91
        {
            get
            {
                CheckPropertyInited("P91");
                return _p91;
            }
            set
            {
                _p91 = value;
                NotifyPropertyChanged("P91");
            }
        }


        private long? _p91_Code;
        /// <summary>
        /// 25001300 Уточнение дополнительного адресообразующего элемента (справочный код) (P91)
        /// </summary>
        [RegisterAttribute(AttributeID = 25001300)]
        public long? P91_Code
        {
            get
            {
                CheckPropertyInited("P91_Code");
                return _p91_Code;
            }
            set
            {
                _p91_Code = value;
                NotifyPropertyChanged("P91_Code");
            }
        }


        private string _l1type;
        /// <summary>
        /// 25001400 Тип номера дом/владение/участок (L1_TYPE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25001400)]
        public string L1Type
        {
            get
            {
                CheckPropertyInited("L1Type");
                return _l1type;
            }
            set
            {
                _l1type = value;
                NotifyPropertyChanged("L1Type");
            }
        }


        private long? _l1type_Code;
        /// <summary>
        /// 25001400 Тип номера дом/владение/участок (справочный код) (L1_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25001400)]
        public long? L1Type_Code
        {
            get
            {
                CheckPropertyInited("L1Type_Code");
                return _l1type_Code;
            }
            set
            {
                _l1type_Code = value;
                NotifyPropertyChanged("L1Type_Code");
            }
        }


        private string _l1value;
        /// <summary>
        /// 25001500 Номер дома/владения/участка (L1_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25001500)]
        public string L1Value
        {
            get
            {
                CheckPropertyInited("L1Value");
                return _l1value;
            }
            set
            {
                _l1value = value;
                NotifyPropertyChanged("L1Value");
            }
        }


        private string _l2type;
        /// <summary>
        /// 25001600 Тип номера корпуса (L2_TYPE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25001600)]
        public string L2Type
        {
            get
            {
                CheckPropertyInited("L2Type");
                return _l2type;
            }
            set
            {
                _l2type = value;
                NotifyPropertyChanged("L2Type");
            }
        }


        private long? _l2type_Code;
        /// <summary>
        /// 25001600 Тип номера корпуса (справочный код) (L2_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25001600)]
        public long? L2Type_Code
        {
            get
            {
                CheckPropertyInited("L2Type_Code");
                return _l2type_Code;
            }
            set
            {
                _l2type_Code = value;
                NotifyPropertyChanged("L2Type_Code");
            }
        }


        private string _l2value;
        /// <summary>
        /// 25001700 Номер корпуса (L2_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25001700)]
        public string L2Value
        {
            get
            {
                CheckPropertyInited("L2Value");
                return _l2value;
            }
            set
            {
                _l2value = value;
                NotifyPropertyChanged("L2Value");
            }
        }


        private string _l3type;
        /// <summary>
        /// 25001800 Тип номера строения/сооружения (L3_TYPE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25001800)]
        public string L3Type
        {
            get
            {
                CheckPropertyInited("L3Type");
                return _l3type;
            }
            set
            {
                _l3type = value;
                NotifyPropertyChanged("L3Type");
            }
        }


        private long? _l3type_Code;
        /// <summary>
        /// 25001800 Тип номера строения/сооружения (справочный код) (L3_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25001800)]
        public long? L3Type_Code
        {
            get
            {
                CheckPropertyInited("L3Type_Code");
                return _l3type_Code;
            }
            set
            {
                _l3type_Code = value;
                NotifyPropertyChanged("L3Type_Code");
            }
        }


        private string _l3value;
        /// <summary>
        /// 25001900 Номер строения/сооружения (L3_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25001900)]
        public string L3Value
        {
            get
            {
                CheckPropertyInited("L3Value");
                return _l3value;
            }
            set
            {
                _l3value = value;
                NotifyPropertyChanged("L3Value");
            }
        }


        private string _l4type;
        /// <summary>
        /// 25002000 Тип номера помещения (L4_TYPE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25002000)]
        public string L4Type
        {
            get
            {
                CheckPropertyInited("L4Type");
                return _l4type;
            }
            set
            {
                _l4type = value;
                NotifyPropertyChanged("L4Type");
            }
        }


        private long? _l4type_Code;
        /// <summary>
        /// 25002000 Тип номера помещения (справочный код) (L4_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25002000)]
        public long? L4Type_Code
        {
            get
            {
                CheckPropertyInited("L4Type_Code");
                return _l4type_Code;
            }
            set
            {
                _l4type_Code = value;
                NotifyPropertyChanged("L4Type_Code");
            }
        }


        private string _l4value;
        /// <summary>
        /// 25002100 Номер помещения (L4_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25002100)]
        public string L4Value
        {
            get
            {
                CheckPropertyInited("L4Value");
                return _l4value;
            }
            set
            {
                _l4value = value;
                NotifyPropertyChanged("L4Value");
            }
        }


        private string _admarea;
        /// <summary>
        /// 25002200 Административный округ (ADM_AREA_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25002200)]
        public string AdmArea
        {
            get
            {
                CheckPropertyInited("AdmArea");
                return _admarea;
            }
            set
            {
                _admarea = value;
                NotifyPropertyChanged("AdmArea");
            }
        }


        private long? _admarea_Code;
        /// <summary>
        /// 25002200 Административный округ (справочный код) (ADM_AREA)
        /// </summary>
        [RegisterAttribute(AttributeID = 25002200)]
        public long? AdmArea_Code
        {
            get
            {
                CheckPropertyInited("AdmArea_Code");
                return _admarea_Code;
            }
            set
            {
                _admarea_Code = value;
                NotifyPropertyChanged("AdmArea_Code");
            }
        }


        private string _district;
        /// <summary>
        /// 25002300 Муниципальный округ/поселение (DISTRICT_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25002300)]
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
        /// 25002300 Муниципальный округ/поселение (справочный код) (DISTRICT)
        /// </summary>
        [RegisterAttribute(AttributeID = 25002300)]
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


        private long? _nreg;
        /// <summary>
        /// 25002400 Регистрационный номер в Адресном реестре (NREG)
        /// </summary>
        [RegisterAttribute(AttributeID = 25002400)]
        public long? Nreg
        {
            get
            {
                CheckPropertyInited("Nreg");
                return _nreg;
            }
            set
            {
                _nreg = value;
                NotifyPropertyChanged("Nreg");
            }
        }


        private DateTime? _dreg;
        /// <summary>
        /// 25002500 Дата регистрации в Адресном реестре (DREG)
        /// </summary>
        [RegisterAttribute(AttributeID = 25002500)]
        public DateTime? Dreg
        {
            get
            {
                CheckPropertyInited("Dreg");
                return _dreg;
            }
            set
            {
                _dreg = value;
                NotifyPropertyChanged("Dreg");
            }
        }


        private string _kadn;
        /// <summary>
        /// 25002600 Кадастровый номер объекта недвижимости (KAD_N)
        /// </summary>
        [RegisterAttribute(AttributeID = 25002600)]
        public string KadN
        {
            get
            {
                CheckPropertyInited("KadN");
                return _kadn;
            }
            set
            {
                _kadn = value;
                NotifyPropertyChanged("KadN");
            }
        }


        private string _kadzu;
        /// <summary>
        /// 25002700 Кадастровый номер земельного участка (для ОКС) (KAD_ZU)
        /// </summary>
        [RegisterAttribute(AttributeID = 25002700)]
        public string KadZu
        {
            get
            {
                CheckPropertyInited("KadZu");
                return _kadzu;
            }
            set
            {
                _kadzu = value;
                NotifyPropertyChanged("KadZu");
            }
        }


        private string _tdoc;
        /// <summary>
        /// 25002800 Документ-основание регистрационных действий (TDOC_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25002800)]
        public string TDoc
        {
            get
            {
                CheckPropertyInited("TDoc");
                return _tdoc;
            }
            set
            {
                _tdoc = value;
                NotifyPropertyChanged("TDoc");
            }
        }


        private long? _tdoc_Code;
        /// <summary>
        /// 25002800 Документ-основание регистрационных действий (справочный код) (TDOC)
        /// </summary>
        [RegisterAttribute(AttributeID = 25002800)]
        public long? TDoc_Code
        {
            get
            {
                CheckPropertyInited("TDoc_Code");
                return _tdoc_Code;
            }
            set
            {
                _tdoc_Code = value;
                NotifyPropertyChanged("TDoc_Code");
            }
        }


        private string _ndoc;
        /// <summary>
        /// 25002900 № документа о регистрации адреса (NDOC)
        /// </summary>
        [RegisterAttribute(AttributeID = 25002900)]
        public string Ndoc
        {
            get
            {
                CheckPropertyInited("Ndoc");
                return _ndoc;
            }
            set
            {
                _ndoc = value;
                NotifyPropertyChanged("Ndoc");
            }
        }


        private DateTime? _ddoc;
        /// <summary>
        /// 25003000 Дата документа о регистрации адреса (DDOC)
        /// </summary>
        [RegisterAttribute(AttributeID = 25003000)]
        public DateTime? Ddoc
        {
            get
            {
                CheckPropertyInited("Ddoc");
                return _ddoc;
            }
            set
            {
                _ddoc = value;
                NotifyPropertyChanged("Ddoc");
            }
        }


        private long? _unom;
        /// <summary>
        /// 25003100 UNOM объекта (только для строения, сооружения, ЗУ, ОНС) (UNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 25003100)]
        public long? Unom
        {
            get
            {
                CheckPropertyInited("Unom");
                return _unom;
            }
            set
            {
                _unom = value;
                NotifyPropertyChanged("Unom");
            }
        }


        private string _adrtype;
        /// <summary>
        /// 25003200 Тип адреса (ADR_TYPE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25003200)]
        public string AdrType
        {
            get
            {
                CheckPropertyInited("AdrType");
                return _adrtype;
            }
            set
            {
                _adrtype = value;
                NotifyPropertyChanged("AdrType");
            }
        }


        private AddressStatus _adrtype_Code;
        /// <summary>
        /// 25003200 Тип адреса (справочный код) (ADR_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25003200)]
        public AddressStatus AdrType_Code
        {
            get
            {
                CheckPropertyInited("AdrType_Code");
                return this._adrtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_adrtype))
                    {
                         _adrtype = descr;
                    }
                }
                else
                {
                     _adrtype = descr;
                }

                this._adrtype_Code = value;
                NotifyPropertyChanged("AdrType");
                NotifyPropertyChanged("AdrType_Code");
            }
        }


        private string _sostad;
        /// <summary>
        /// 25003300 Состояние адреса (SOSTAD_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25003300)]
        public string Sostad
        {
            get
            {
                CheckPropertyInited("Sostad");
                return _sostad;
            }
            set
            {
                _sostad = value;
                NotifyPropertyChanged("Sostad");
            }
        }


        private long? _sostad_Code;
        /// <summary>
        /// 25003300 Состояние адреса (справочный код) (SOSTAD)
        /// </summary>
        [RegisterAttribute(AttributeID = 25003300)]
        public long? Sostad_Code
        {
            get
            {
                CheckPropertyInited("Sostad_Code");
                return _sostad_Code;
            }
            set
            {
                _sostad_Code = value;
                NotifyPropertyChanged("Sostad_Code");
            }
        }


        private string _kladr;
        /// <summary>
        /// 25003400 Код КЛАДР для адресообразующиего элемента нижнего уровня (KLADR)
        /// </summary>
        [RegisterAttribute(AttributeID = 25003400)]
        public string Kladr
        {
            get
            {
                CheckPropertyInited("Kladr");
                return _kladr;
            }
            set
            {
                _kladr = value;
                NotifyPropertyChanged("Kladr");
            }
        }


        private string _nfias;
        /// <summary>
        /// 25003500 Код (GUID) ФИАС для адреса (N_FIAS)
        /// </summary>
        [RegisterAttribute(AttributeID = 25003500)]
        public string Nfias
        {
            get
            {
                CheckPropertyInited("Nfias");
                return _nfias;
            }
            set
            {
                _nfias = value;
                NotifyPropertyChanged("Nfias");
            }
        }


        private DateTime? _dfias;
        /// <summary>
        /// 25003600 Дата начала действия адреса в ФИАС (D_FIAS)
        /// </summary>
        [RegisterAttribute(AttributeID = 25003600)]
        public DateTime? Dfias
        {
            get
            {
                CheckPropertyInited("Dfias");
                return _dfias;
            }
            set
            {
                _dfias = value;
                NotifyPropertyChanged("Dfias");
            }
        }


        private string _vid;
        /// <summary>
        /// 25003700 Вид адреса (VID_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25003700)]
        public string Vid
        {
            get
            {
                CheckPropertyInited("Vid");
                return _vid;
            }
            set
            {
                _vid = value;
                NotifyPropertyChanged("Vid");
            }
        }


        private long? _vid_Code;
        /// <summary>
        /// 25003700 Вид адреса (справочный код) (VID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25003700)]
        public long? Vid_Code
        {
            get
            {
                CheckPropertyInited("Vid_Code");
                return _vid_Code;
            }
            set
            {
                _vid_Code = value;
                NotifyPropertyChanged("Vid_Code");
            }
        }


        private bool? _mainadr;
        /// <summary>
        /// 25003800 Признак "главного адреса" объекта (MAIN_ADR)
        /// </summary>
        [RegisterAttribute(AttributeID = 25003800)]
        public bool? MainAdr
        {
            get
            {
                CheckPropertyInited("MainAdr");
                return _mainadr;
            }
            set
            {
                _mainadr = value;
                NotifyPropertyChanged("MainAdr");
            }
        }


        private string _commnt;
        /// <summary>
        /// 25003900 Комментарий к адресу (COMMNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 25003900)]
        public string Commnt
        {
            get
            {
                CheckPropertyInited("Commnt");
                return _commnt;
            }
            set
            {
                _commnt = value;
                NotifyPropertyChanged("Commnt");
            }
        }


        private string _fadstatus;
        /// <summary>
        /// 25004000 Статус адреса (FAD_STATUS_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25004000)]
        public string FadStatus
        {
            get
            {
                CheckPropertyInited("FadStatus");
                return _fadstatus;
            }
            set
            {
                _fadstatus = value;
                NotifyPropertyChanged("FadStatus");
            }
        }


        private long? _fadstatus_Code;
        /// <summary>
        /// 25004000 Статус адреса (справочный код) (FAD_STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 25004000)]
        public long? FadStatus_Code
        {
            get
            {
                CheckPropertyInited("FadStatus_Code");
                return _fadstatus_Code;
            }
            set
            {
                _fadstatus_Code = value;
                NotifyPropertyChanged("FadStatus_Code");
            }
        }


        private bool? _buildingaddress;
        /// <summary>
        /// 25004100 Признак "строительного адреса" объекта (BUILDING_ADDRESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 25004100)]
        public bool? BuildingAddress
        {
            get
            {
                CheckPropertyInited("BuildingAddress");
                return _buildingaddress;
            }
            set
            {
                _buildingaddress = value;
                NotifyPropertyChanged("BuildingAddress");
            }
        }


        private string _ao;
        /// <summary>
        /// 25004200 Адм. округ (AO_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25004200)]
        public string AO
        {
            get
            {
                CheckPropertyInited("AO");
                return _ao;
            }
            set
            {
                _ao = value;
                NotifyPropertyChanged("AO");
            }
        }


        private long? _ao_Code;
        /// <summary>
        /// 25004200 Адм. округ (справочный код) (AO)
        /// </summary>
        [RegisterAttribute(AttributeID = 25004200)]
        public long? AO_Code
        {
            get
            {
                CheckPropertyInited("AO_Code");
                return _ao_Code;
            }
            set
            {
                _ao_Code = value;
                NotifyPropertyChanged("AO_Code");
            }
        }


        private string _mr;
        /// <summary>
        /// 25004300 Муниципальный округ (MR_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25004300)]
        public string MR
        {
            get
            {
                CheckPropertyInited("MR");
                return _mr;
            }
            set
            {
                _mr = value;
                NotifyPropertyChanged("MR");
            }
        }


        private long? _mr_Code;
        /// <summary>
        /// 25004300 Муниципальный округ (справочный код) (MR)
        /// </summary>
        [RegisterAttribute(AttributeID = 25004300)]
        public long? MR_Code
        {
            get
            {
                CheckPropertyInited("MR_Code");
                return _mr_Code;
            }
            set
            {
                _mr_Code = value;
                NotifyPropertyChanged("MR_Code");
            }
        }


        private DateTime? _dateedit;
        /// <summary>
        /// 25004400 Дата изменения (DATEEDIT)
        /// </summary>
        [RegisterAttribute(AttributeID = 25004400)]
        public DateTime? DateEdit
        {
            get
            {
                CheckPropertyInited("DateEdit");
                return _dateedit;
            }
            set
            {
                _dateedit = value;
                NotifyPropertyChanged("DateEdit");
            }
        }


        private long? _eaid;
        /// <summary>
        /// 25004500 EAID (EAID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25004500)]
        public long? EaId
        {
            get
            {
                CheckPropertyInited("EaId");
                return _eaid;
            }
            set
            {
                _eaid = value;
                NotifyPropertyChanged("EaId");
            }
        }


        private long? _status;
        /// <summary>
        /// 25004600 Статус (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 25004600)]
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


        private string _adres;
        /// <summary>
        /// 25004700 Полное юридическое написание адреса или описания местоположения (ADRES)
        /// </summary>
        [RegisterAttribute(AttributeID = 25004700)]
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

    }
}

namespace ObjectModel.Bti
{
    /// <summary>
    /// 251 БТИ: Здание (BTI_BUILDING_Q)
    /// </summary>
    [RegisterInfo(RegisterID = 251)]
    [Serializable]
    public sealed partial class OMBtiBuilding : OMBaseClass<OMBtiBuilding>
    {

        private long _empid;
        /// <summary>
        /// 25100100 Инд.номер (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 25100100)]
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


        private long? _unom;
        /// <summary>
        /// 25100200 UNOM (UNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 25100200)]
        public long? Unom
        {
            get
            {
                CheckPropertyInited("Unom");
                return _unom;
            }
            set
            {
                _unom = value;
                NotifyPropertyChanged("Unom");
            }
        }


        private string _kl;
        /// <summary>
        /// 25100300 Класс строения (KL)
        /// </summary>
        [RegisterAttribute(AttributeID = 25100300)]
        public string Kl
        {
            get
            {
                CheckPropertyInited("Kl");
                return _kl;
            }
            set
            {
                _kl = value;
                NotifyPropertyChanged("Kl");
            }
        }


        private BuildingClass _kl_Code;
        /// <summary>
        /// 25100300 Класс строения (справочный код) (KL_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25100300)]
        public BuildingClass Kl_Code
        {
            get
            {
                CheckPropertyInited("Kl_Code");
                return this._kl_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_kl))
                    {
                         _kl = descr;
                    }
                }
                else
                {
                     _kl = descr;
                }

                this._kl_Code = value;
                NotifyPropertyChanged("Kl");
                NotifyPropertyChanged("Kl_Code");
            }
        }


        private string _naz;
        /// <summary>
        /// 25100400 Назначение (NAZ)
        /// </summary>
        [RegisterAttribute(AttributeID = 25100400)]
        public string Naz
        {
            get
            {
                CheckPropertyInited("Naz");
                return _naz;
            }
            set
            {
                _naz = value;
                NotifyPropertyChanged("Naz");
            }
        }


        private Purpose _naz_Code;
        /// <summary>
        /// 25100400 Назначение (справочный код) (NAZ_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25100400)]
        public Purpose Naz_Code
        {
            get
            {
                CheckPropertyInited("Naz_Code");
                return this._naz_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_naz))
                    {
                         _naz = descr;
                    }
                }
                else
                {
                     _naz = descr;
                }

                this._naz_Code = value;
                NotifyPropertyChanged("Naz");
                NotifyPropertyChanged("Naz_Code");
            }
        }


        private string _mst;
        /// <summary>
        /// 25100500 Материал стен (MST)
        /// </summary>
        [RegisterAttribute(AttributeID = 25100500)]
        public string Mst
        {
            get
            {
                CheckPropertyInited("Mst");
                return _mst;
            }
            set
            {
                _mst = value;
                NotifyPropertyChanged("Mst");
            }
        }


        private long? _mst_Code;
        /// <summary>
        /// 25100500 Материал стен (справочный код) (MST_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25100500)]
        public long? Mst_Code
        {
            get
            {
                CheckPropertyInited("Mst_Code");
                return _mst_Code;
            }
            set
            {
                _mst_Code = value;
                NotifyPropertyChanged("Mst_Code");
            }
        }


        private long? _et;
        /// <summary>
        /// 25100600 Этажность максимальная (ET)
        /// </summary>
        [RegisterAttribute(AttributeID = 25100600)]
        public long? Et
        {
            get
            {
                CheckPropertyInited("Et");
                return _et;
            }
            set
            {
                _et = value;
                NotifyPropertyChanged("Et");
            }
        }


        private long? _gdpostr;
        /// <summary>
        /// 25100700 Год постройки (GDPOSTR)
        /// </summary>
        [RegisterAttribute(AttributeID = 25100700)]
        public long? GdPostr
        {
            get
            {
                CheckPropertyInited("GdPostr");
                return _gdpostr;
            }
            set
            {
                _gdpostr = value;
                NotifyPropertyChanged("GdPostr");
            }
        }


        private string _kadn;
        /// <summary>
        /// 25100800 Кадастровый номер (К.Н.) (KAD_N)
        /// </summary>
        [RegisterAttribute(AttributeID = 25100800)]
        public string KadN
        {
            get
            {
                CheckPropertyInited("KadN");
                return _kadn;
            }
            set
            {
                _kadn = value;
                NotifyPropertyChanged("KadN");
            }
        }


        private long? _etmin;
        /// <summary>
        /// 25100900 Этажность минимальная (ET_MIN)
        /// </summary>
        [RegisterAttribute(AttributeID = 25100900)]
        public long? EtMin
        {
            get
            {
                CheckPropertyInited("EtMin");
                return _etmin;
            }
            set
            {
                _etmin = value;
                NotifyPropertyChanged("EtMin");
            }
        }


        private long? _etpdz;
        /// <summary>
        /// 25101000 Этажность подземная (ET_PDZ)
        /// </summary>
        [RegisterAttribute(AttributeID = 25101000)]
        public long? EtPdz
        {
            get
            {
                CheckPropertyInited("EtPdz");
                return _etpdz;
            }
            set
            {
                _etpdz = value;
                NotifyPropertyChanged("EtPdz");
            }
        }


        private decimal? _opl;
        /// <summary>
        /// 25101200 Площадь общая (OPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 25101200)]
        public decimal? Opl
        {
            get
            {
                CheckPropertyInited("Opl");
                return _opl;
            }
            set
            {
                _opl = value;
                NotifyPropertyChanged("Opl");
            }
        }


        private string _sost;
        /// <summary>
        /// 25101400 Состояние строения (SOST)
        /// </summary>
        [RegisterAttribute(AttributeID = 25101400)]
        public string Sost
        {
            get
            {
                CheckPropertyInited("Sost");
                return _sost;
            }
            set
            {
                _sost = value;
                NotifyPropertyChanged("Sost");
            }
        }


        private StructureStatus _sost_Code;
        /// <summary>
        /// 25101400 Состояние строения (справочный код) (SOST_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25101400)]
        public StructureStatus Sost_Code
        {
            get
            {
                CheckPropertyInited("Sost_Code");
                return this._sost_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_sost))
                    {
                         _sost = descr;
                    }
                }
                else
                {
                     _sost = descr;
                }

                this._sost_Code = value;
                NotifyPropertyChanged("Sost");
                NotifyPropertyChanged("Sost_Code");
            }
        }


        private DateTime? _dtsost;
        /// <summary>
        /// 25101500 Дата состояния (DTSOST)
        /// </summary>
        [RegisterAttribute(AttributeID = 25101500)]
        public DateTime? DtSost
        {
            get
            {
                CheckPropertyInited("DtSost");
                return _dtsost;
            }
            set
            {
                _dtsost = value;
                NotifyPropertyChanged("DtSost");
            }
        }


        private bool? _gddo1917;
        /// <summary>
        /// 25101900 Признак постройки до 1917 г. (GDDO1917)
        /// </summary>
        [RegisterAttribute(AttributeID = 25101900)]
        public bool? GdDo1917
        {
            get
            {
                CheckPropertyInited("GdDo1917");
                return _gddo1917;
            }
            set
            {
                _gddo1917 = value;
                NotifyPropertyChanged("GdDo1917");
            }
        }


        private bool? _pamarc;
        /// <summary>
        /// 25102400 Признак памятника архитектуры (PAMARC)
        /// </summary>
        [RegisterAttribute(AttributeID = 25102400)]
        public bool? Pamarc
        {
            get
            {
                CheckPropertyInited("Pamarc");
                return _pamarc;
            }
            set
            {
                _pamarc = value;
                NotifyPropertyChanged("Pamarc");
            }
        }


        private bool? _avarzd;
        /// <summary>
        /// 25102500 Признак Аварийное (AVARZD)
        /// </summary>
        [RegisterAttribute(AttributeID = 25102500)]
        public bool? Avarzd
        {
            get
            {
                CheckPropertyInited("Avarzd");
                return _avarzd;
            }
            set
            {
                _avarzd = value;
                NotifyPropertyChanged("Avarzd");
            }
        }


        private DateTime? _dtavarzd;
        /// <summary>
        /// 25102600 Дата решения об аварийности здания (DTAVARZD)
        /// </summary>
        [RegisterAttribute(AttributeID = 25102600)]
        public DateTime? DtAvarzd
        {
            get
            {
                CheckPropertyInited("DtAvarzd");
                return _dtavarzd;
            }
            set
            {
                _dtavarzd = value;
                NotifyPropertyChanged("DtAvarzd");
            }
        }


        private bool? _samovol;
        /// <summary>
        /// 25102800 Признак Самовольное возведение (SAMOVOL)
        /// </summary>
        [RegisterAttribute(AttributeID = 25102800)]
        public bool? Samovol
        {
            get
            {
                CheckPropertyInited("Samovol");
                return _samovol;
            }
            set
            {
                _samovol = value;
                NotifyPropertyChanged("Samovol");
            }
        }


        private decimal? _oplg;
        /// <summary>
        /// 25103000 Площадь общая жилых помещений (OPL_G)
        /// </summary>
        [RegisterAttribute(AttributeID = 25103000)]
        public decimal? OplG
        {
            get
            {
                CheckPropertyInited("OplG");
                return _oplg;
            }
            set
            {
                _oplg = value;
                NotifyPropertyChanged("OplG");
            }
        }


        private decimal? _opln;
        /// <summary>
        /// 25103200 Площадь общая нежилых помещений (OPL_N)
        /// </summary>
        [RegisterAttribute(AttributeID = 25103200)]
        public decimal? OplN
        {
            get
            {
                CheckPropertyInited("OplN");
                return _opln;
            }
            set
            {
                _opln = value;
                NotifyPropertyChanged("OplN");
            }
        }


        private decimal? _narpl;
        /// <summary>
        /// 25103400 Площадь застройки (NARPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 25103400)]
        public decimal? Narpl
        {
            get
            {
                CheckPropertyInited("Narpl");
                return _narpl;
            }
            set
            {
                _narpl = value;
                NotifyPropertyChanged("Narpl");
            }
        }


        private string _ser;
        /// <summary>
        /// 25103600 Серия проекта (SER)
        /// </summary>
        [RegisterAttribute(AttributeID = 25103600)]
        public string Ser
        {
            get
            {
                CheckPropertyInited("Ser");
                return _ser;
            }
            set
            {
                _ser = value;
                NotifyPropertyChanged("Ser");
            }
        }


        private long? _ser_Code;
        /// <summary>
        /// 25103600 Серия проекта (справочный код) (SER_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25103600)]
        public long? Ser_Code
        {
            get
            {
                CheckPropertyInited("Ser_Code");
                return _ser_Code;
            }
            set
            {
                _ser_Code = value;
                NotifyPropertyChanged("Ser_Code");
            }
        }


        private long? _gdpereob;
        /// <summary>
        /// 25104000 Год переоборудования (GDPEREOB)
        /// </summary>
        [RegisterAttribute(AttributeID = 25104000)]
        public long? GdPereob
        {
            get
            {
                CheckPropertyInited("GdPereob");
                return _gdpereob;
            }
            set
            {
                _gdpereob = value;
                NotifyPropertyChanged("GdPereob");
            }
        }


        private long? _gdkaprem;
        /// <summary>
        /// 25104100 Год капитального ремонта (GDKAPREM)
        /// </summary>
        [RegisterAttribute(AttributeID = 25104100)]
        public long? GdKaprem
        {
            get
            {
                CheckPropertyInited("GdKaprem");
                return _gdkaprem;
            }
            set
            {
                _gdkaprem = value;
                NotifyPropertyChanged("GdKaprem");
            }
        }


        private decimal? _ppl;
        /// <summary>
        /// 25104200 Площадь общая с летними жилых помещений (PPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 25104200)]
        public decimal? Ppl
        {
            get
            {
                CheckPropertyInited("Ppl");
                return _ppl;
            }
            set
            {
                _ppl = value;
                NotifyPropertyChanged("Ppl");
            }
        }


        private long? _kap;
        /// <summary>
        /// 25104300 Капитальность (KAP)
        /// </summary>
        [RegisterAttribute(AttributeID = 25104300)]
        public long? Kap
        {
            get
            {
                CheckPropertyInited("Kap");
                return _kap;
            }
            set
            {
                _kap = value;
                NotifyPropertyChanged("Kap");
            }
        }


        private string _komm;
        /// <summary>
        /// 25104500 Комментарий  (KOMM)
        /// </summary>
        [RegisterAttribute(AttributeID = 25104500)]
        public string Komm
        {
            get
            {
                CheckPropertyInited("Komm");
                return _komm;
            }
            set
            {
                _komm = value;
                NotifyPropertyChanged("Komm");
            }
        }


        private string _source;
        /// <summary>
        /// 25106600 Источник данных (SOURCE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25106600)]
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


        private InsuranceSourceType _source_Code;
        /// <summary>
        /// 25106600 Источник данных (справочный код) (SOURCE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25106600)]
        public InsuranceSourceType Source_Code
        {
            get
            {
                CheckPropertyInited("Source_Code");
                return this._source_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_source))
                    {
                         _source = descr;
                    }
                }
                else
                {
                     _source = descr;
                }

                this._source_Code = value;
                NotifyPropertyChanged("Source");
                NotifyPropertyChanged("Source_Code");
            }
        }


        private string _kat;
        /// <summary>
        /// 25107900 Тип строения (KAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 25107900)]
        public string Kat
        {
            get
            {
                CheckPropertyInited("Kat");
                return _kat;
            }
            set
            {
                _kat = value;
                NotifyPropertyChanged("Kat");
            }
        }


        private long? _kat_Code;
        /// <summary>
        /// 25107900 Тип строения (справочный код) (KAT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25107900)]
        public long? Kat_Code
        {
            get
            {
                CheckPropertyInited("Kat_Code");
                return _kat_Code;
            }
            set
            {
                _kat_Code = value;
                NotifyPropertyChanged("Kat_Code");
            }
        }


        private string _objtype;
        /// <summary>
        /// 25108700 Тип здания (OBJ_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25108700)]
        public string ObjType
        {
            get
            {
                CheckPropertyInited("ObjType");
                return _objtype;
            }
            set
            {
                _objtype = value;
                NotifyPropertyChanged("ObjType");
            }
        }


        private long? _objtype_Code;
        /// <summary>
        /// 25108700 Тип здания (справочный код) (OBJ_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25108700)]
        public long? ObjType_Code
        {
            get
            {
                CheckPropertyInited("ObjType_Code");
                return _objtype_Code;
            }
            set
            {
                _objtype_Code = value;
                NotifyPropertyChanged("ObjType_Code");
            }
        }


        private DateTime? _downloaddate;
        /// <summary>
        /// 25108900 Дата загрузки сведений (DOWNLOAD_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25108900)]
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


        private decimal? _pdvpln;
        /// <summary>
        /// 25109100 Площадь нежилых подвалов (PDVPL_N)
        /// </summary>
        [RegisterAttribute(AttributeID = 25109100)]
        public decimal? PdvplN
        {
            get
            {
                CheckPropertyInited("PdvplN");
                return _pdvpln;
            }
            set
            {
                _pdvpln = value;
                NotifyPropertyChanged("PdvplN");
            }
        }


        private decimal? _actproc;
        /// <summary>
        /// 25110000 Процент износа (ACTPROC)
        /// </summary>
        [RegisterAttribute(AttributeID = 25110000)]
        public decimal? ActProc
        {
            get
            {
                CheckPropertyInited("ActProc");
                return _actproc;
            }
            set
            {
                _actproc = value;
                NotifyPropertyChanged("ActProc");
            }
        }


        private long? _gdproc;
        /// <summary>
        /// 25110100 Год установки процента износа (GDPROC)
        /// </summary>
        [RegisterAttribute(AttributeID = 25110100)]
        public long? GdProc
        {
            get
            {
                CheckPropertyInited("GdProc");
                return _gdproc;
            }
            set
            {
                _gdproc = value;
                NotifyPropertyChanged("GdProc");
            }
        }


        private decimal? _krovpl;
        /// <summary>
        /// 25110200 Площадь кровли (KROVPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 25110200)]
        public decimal? Krovpl
        {
            get
            {
                CheckPropertyInited("Krovpl");
                return _krovpl;
            }
            set
            {
                _krovpl = value;
                NotifyPropertyChanged("Krovpl");
            }
        }


        private long? _lfpq;
        /// <summary>
        /// 25110300 Количество пассажирских лифтов (LFPQ)
        /// </summary>
        [RegisterAttribute(AttributeID = 25110300)]
        public long? Lfpq
        {
            get
            {
                CheckPropertyInited("Lfpq");
                return _lfpq;
            }
            set
            {
                _lfpq = value;
                NotifyPropertyChanged("Lfpq");
            }
        }


        private long? _lfgpq;
        /// <summary>
        /// 25110400 Количество грузопассажирских лифтов (LFGPQ)
        /// </summary>
        [RegisterAttribute(AttributeID = 25110400)]
        public long? Lfgpq
        {
            get
            {
                CheckPropertyInited("Lfgpq");
                return _lfgpq;
            }
            set
            {
                _lfgpq = value;
                NotifyPropertyChanged("Lfgpq");
            }
        }


        private long? _lfgq;
        /// <summary>
        /// 25110500 Количество грузовых лифтов (LFGQ)
        /// </summary>
        [RegisterAttribute(AttributeID = 25110500)]
        public long? Lfgq
        {
            get
            {
                CheckPropertyInited("Lfgq");
                return _lfgq;
            }
            set
            {
                _lfgq = value;
                NotifyPropertyChanged("Lfgq");
            }
        }


        private long? _pmqg;
        /// <summary>
        /// 25110600 Количество жилых помещений (PMQ_G)
        /// </summary>
        [RegisterAttribute(AttributeID = 25110600)]
        public long? PmqG
        {
            get
            {
                CheckPropertyInited("PmqG");
                return _pmqg;
            }
            set
            {
                _pmqg = value;
                NotifyPropertyChanged("PmqG");
            }
        }


        private long? _kmqg;
        /// <summary>
        /// 25110700 Количество комнат в жилых помещениях (KMQ_G)
        /// </summary>
        [RegisterAttribute(AttributeID = 25110700)]
        public long? KmqG
        {
            get
            {
                CheckPropertyInited("KmqG");
                return _kmqg;
            }
            set
            {
                _kmqg = value;
                NotifyPropertyChanged("KmqG");
            }
        }


        private long? _kwq;
        /// <summary>
        /// 25110800 Количество квартир (KWQ)
        /// </summary>
        [RegisterAttribute(AttributeID = 25110800)]
        public long? Kwq
        {
            get
            {
                CheckPropertyInited("Kwq");
                return _kwq;
            }
            set
            {
                _kwq = value;
                NotifyPropertyChanged("Kwq");
            }
        }


        private long? _prkor;
        /// <summary>
        /// 25110900 Отметка. 1 - считать как корпус, 0 - нет. Отметка "Считать как корпус" - говорит, что это уникальный объект недвижимости. Иначе это часть другого объекта недвижимости, выделенная для целей статистического учёта. (PRKOR)
        /// </summary>
        [RegisterAttribute(AttributeID = 25110900)]
        public long? Prkor
        {
            get
            {
                CheckPropertyInited("Prkor");
                return _prkor;
            }
            set
            {
                _prkor = value;
                NotifyPropertyChanged("Prkor");
            }
        }


        private decimal? _hpl;
        /// <summary>
        /// 25111000 Площадь холодных помещений (HPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 25111000)]
        public decimal? Hpl
        {
            get
            {
                CheckPropertyInited("Hpl");
                return _hpl;
            }
            set
            {
                _hpl = value;
                NotifyPropertyChanged("Hpl");
            }
        }


        private long? _eleq;
        /// <summary>
        /// 25111100 Количество электрических плит (ELEQ)
        /// </summary>
        [RegisterAttribute(AttributeID = 25111100)]
        public long? Eleq
        {
            get
            {
                CheckPropertyInited("Eleq");
                return _eleq;
            }
            set
            {
                _eleq = value;
                NotifyPropertyChanged("Eleq");
            }
        }


        private long? _gazq;
        /// <summary>
        /// 25111200 Количество газовых плит (GAZQ)
        /// </summary>
        [RegisterAttribute(AttributeID = 25111200)]
        public long? Gazq
        {
            get
            {
                CheckPropertyInited("Gazq");
                return _gazq;
            }
            set
            {
                _gazq = value;
                NotifyPropertyChanged("Gazq");
            }
        }


        private decimal? _bpl;
        /// <summary>
        /// 25111300 Площадь балконов (BPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 25111300)]
        public decimal? Bpl
        {
            get
            {
                CheckPropertyInited("Bpl");
                return _bpl;
            }
            set
            {
                _bpl = value;
                NotifyPropertyChanged("Bpl");
            }
        }


        private decimal? _lpl;
        /// <summary>
        /// 25111400 Площадь лоджий (LPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 25111400)]
        public decimal? Lpl
        {
            get
            {
                CheckPropertyInited("Lpl");
                return _lpl;
            }
            set
            {
                _lpl = value;
                NotifyPropertyChanged("Lpl");
            }
        }


        private string _perekr;
        /// <summary>
        /// 25111500 Материал перекрытий (PEREKR)
        /// </summary>
        [RegisterAttribute(AttributeID = 25111500)]
        public string Perekr
        {
            get
            {
                CheckPropertyInited("Perekr");
                return _perekr;
            }
            set
            {
                _perekr = value;
                NotifyPropertyChanged("Perekr");
            }
        }


        private long? _perekr_Code;
        /// <summary>
        /// 25111500 Материал перекрытий (справочный код) (PEREKR_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25111500)]
        public long? Perekr_Code
        {
            get
            {
                CheckPropertyInited("Perekr_Code");
                return _perekr_Code;
            }
            set
            {
                _perekr_Code = value;
                NotifyPropertyChanged("Perekr_Code");
            }
        }


        private string _krov;
        /// <summary>
        /// 25111600 Материал кровли (KROV)
        /// </summary>
        [RegisterAttribute(AttributeID = 25111600)]
        public string Krov
        {
            get
            {
                CheckPropertyInited("Krov");
                return _krov;
            }
            set
            {
                _krov = value;
                NotifyPropertyChanged("Krov");
            }
        }


        private long? _krov_Code;
        /// <summary>
        /// 25111600 Материал кровли (справочный код) (KROV_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25111600)]
        public long? Krov_Code
        {
            get
            {
                CheckPropertyInited("Krov_Code");
                return _krov_Code;
            }
            set
            {
                _krov_Code = value;
                NotifyPropertyChanged("Krov_Code");
            }
        }


        private string _otskorp;
        /// <summary>
        /// 25111700 Состояние отселения корпуса (OTSKORP)
        /// </summary>
        [RegisterAttribute(AttributeID = 25111700)]
        public string Otskorp
        {
            get
            {
                CheckPropertyInited("Otskorp");
                return _otskorp;
            }
            set
            {
                _otskorp = value;
                NotifyPropertyChanged("Otskorp");
            }
        }


        private long? _otskorp_Code;
        /// <summary>
        /// 25111700 Состояние отселения корпуса (справочный код) (OTSKORP_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25111700)]
        public long? Otskorp_Code
        {
            get
            {
                CheckPropertyInited("Otskorp_Code");
                return _otskorp_Code;
            }
            set
            {
                _otskorp_Code = value;
                NotifyPropertyChanged("Otskorp_Code");
            }
        }

    }
}

namespace ObjectModel.Bti
{
    /// <summary>
    /// 253 БТИ: Этаж (BTI_FLOOR_Q)
    /// </summary>
    [RegisterInfo(RegisterID = 253)]
    [Serializable]
    public sealed partial class OMFloor : OMBaseClass<OMFloor>
    {

        private long _empid;
        /// <summary>
        /// 25300100 Инд.номер (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 25300100)]
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


        private long? _buildingid;
        /// <summary>
        /// 25300200 ID объекта (BUILDING_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25300200)]
        public long? BuildingId
        {
            get
            {
                CheckPropertyInited("BuildingId");
                return _buildingid;
            }
            set
            {
                _buildingid = value;
                NotifyPropertyChanged("BuildingId");
            }
        }


        private string _typename;
        /// <summary>
        /// 25300300 Тип этажа (TYPE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25300300)]
        public string TypeName
        {
            get
            {
                CheckPropertyInited("TypeName");
                return _typename;
            }
            set
            {
                _typename = value;
                NotifyPropertyChanged("TypeName");
            }
        }


        private long? _typename_Code;
        /// <summary>
        /// 25300300 Тип этажа (справочный код) (TYPE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25300300)]
        public long? TypeName_Code
        {
            get
            {
                CheckPropertyInited("TypeName_Code");
                return _typename_Code;
            }
            set
            {
                _typename_Code = value;
                NotifyPropertyChanged("TypeName_Code");
            }
        }


        private long? _floornumber;
        /// <summary>
        /// 25300400 Номер этажа (FLOOR_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 25300400)]
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


        private long? _floornumberpp;
        /// <summary>
        /// 25300500 Номер этажа п/п (FLOOR_NUMBER_PP)
        /// </summary>
        [RegisterAttribute(AttributeID = 25300500)]
        public long? FloorNumberPp
        {
            get
            {
                CheckPropertyInited("FloorNumberPp");
                return _floornumberpp;
            }
            set
            {
                _floornumberpp = value;
                NotifyPropertyChanged("FloorNumberPp");
            }
        }


        private decimal? _areapp;
        /// <summary>
        /// 25300600 Площадь_пп (AREA_PP)
        /// </summary>
        [RegisterAttribute(AttributeID = 25300600)]
        public decimal? AreaPp
        {
            get
            {
                CheckPropertyInited("AreaPp");
                return _areapp;
            }
            set
            {
                _areapp = value;
                NotifyPropertyChanged("AreaPp");
            }
        }


        private string _guidpp;
        /// <summary>
        /// 25300700 GUID_пп (GUID_PP)
        /// </summary>
        [RegisterAttribute(AttributeID = 25300700)]
        public string GuidPp
        {
            get
            {
                CheckPropertyInited("GuidPp");
                return _guidpp;
            }
            set
            {
                _guidpp = value;
                NotifyPropertyChanged("GuidPp");
            }
        }


        private long? _numberpp;
        /// <summary>
        /// 25300800 Номер_этажа_пп (NUMBER_PP)
        /// </summary>
        [RegisterAttribute(AttributeID = 25300800)]
        public long? NumberPp
        {
            get
            {
                CheckPropertyInited("NumberPp");
                return _numberpp;
            }
            set
            {
                _numberpp = value;
                NotifyPropertyChanged("NumberPp");
            }
        }


        private string _typepp;
        /// <summary>
        /// 25300900 Тип_этажа_пп (TYPE_PP)
        /// </summary>
        [RegisterAttribute(AttributeID = 25300900)]
        public string TypePp
        {
            get
            {
                CheckPropertyInited("TypePp");
                return _typepp;
            }
            set
            {
                _typepp = value;
                NotifyPropertyChanged("TypePp");
            }
        }


        private bool? _isundeground;
        /// <summary>
        /// 25301000 Признак Подземный (IS_UNDEGROUND)
        /// </summary>
        [RegisterAttribute(AttributeID = 25301000)]
        public bool? IsUndeground
        {
            get
            {
                CheckPropertyInited("IsUndeground");
                return _isundeground;
            }
            set
            {
                _isundeground = value;
                NotifyPropertyChanged("IsUndeground");
            }
        }


        private long? _registerobjectnumber;
        /// <summary>
        /// 25301100 Номер реестра (REGISTER_OBJECT_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 25301100)]
        public long? RegisterObjectNumber
        {
            get
            {
                CheckPropertyInited("RegisterObjectNumber");
                return _registerobjectnumber;
            }
            set
            {
                _registerobjectnumber = value;
                NotifyPropertyChanged("RegisterObjectNumber");
            }
        }


        private bool? _floorplanpresence;
        /// <summary>
        /// 25301200 Наличие поэтажного плана (FLOOR_PLAN_PRESENCE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25301200)]
        public bool? FloorPlanPresence
        {
            get
            {
                CheckPropertyInited("FloorPlanPresence");
                return _floorplanpresence;
            }
            set
            {
                _floorplanpresence = value;
                NotifyPropertyChanged("FloorPlanPresence");
            }
        }

    }
}

namespace ObjectModel.Bti
{
    /// <summary>
    /// 254 БТИ: Помещение (BTI_PREMASE)
    /// </summary>
    [RegisterInfo(RegisterID = 254)]
    [Serializable]
    public sealed partial class OMPremase : OMBaseClass<OMPremase>
    {

        private long _empid;
        /// <summary>
        /// 25400100 Инд.номер (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 25400100)]
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


        private string _kadastr;
        /// <summary>
        /// 25400200 Кадастровый номер (К.Н.) (KADASTR)
        /// </summary>
        [RegisterAttribute(AttributeID = 25400200)]
        public string Kadastr
        {
            get
            {
                CheckPropertyInited("Kadastr");
                return _kadastr;
            }
            set
            {
                _kadastr = value;
                NotifyPropertyChanged("Kadastr");
            }
        }


        private long? _floorid;
        /// <summary>
        /// 25400300 Этаж (FLOOR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25400300)]
        public long? FloorId
        {
            get
            {
                CheckPropertyInited("FloorId");
                return _floorid;
            }
            set
            {
                _floorid = value;
                NotifyPropertyChanged("FloorId");
            }
        }


        private DateTime? _inspectiondate;
        /// <summary>
        /// 25400400 Дата обследования (INSPECTION_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25400400)]
        public DateTime? InspectionDate
        {
            get
            {
                CheckPropertyInited("InspectionDate");
                return _inspectiondate;
            }
            set
            {
                _inspectiondate = value;
                NotifyPropertyChanged("InspectionDate");
            }
        }


        private long? _unkv;
        /// <summary>
        /// 25400700 Уникальный идентификатор помещения в рамках строения (UNKV)
        /// </summary>
        [RegisterAttribute(AttributeID = 25400700)]
        public long? Unkv
        {
            get
            {
                CheckPropertyInited("Unkv");
                return _unkv;
            }
            set
            {
                _unkv = value;
                NotifyPropertyChanged("Unkv");
            }
        }


        private string _classname;
        /// <summary>
        /// 25400800 Класс помещения (CLASS_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25400800)]
        public string ClassName
        {
            get
            {
                CheckPropertyInited("ClassName");
                return _classname;
            }
            set
            {
                _classname = value;
                NotifyPropertyChanged("ClassName");
            }
        }


        private long? _classname_Code;
        /// <summary>
        /// 25400800 Класс помещения (справочный код) (CLASS_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25400800)]
        public long? ClassName_Code
        {
            get
            {
                CheckPropertyInited("ClassName_Code");
                return _classname_Code;
            }
            set
            {
                _classname_Code = value;
                NotifyPropertyChanged("ClassName_Code");
            }
        }


        private string _typename;
        /// <summary>
        /// 25400900 Тип помещения (TYPE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25400900)]
        public string TypeName
        {
            get
            {
                CheckPropertyInited("TypeName");
                return _typename;
            }
            set
            {
                _typename = value;
                NotifyPropertyChanged("TypeName");
            }
        }


        private PremisesTypes _typename_Code;
        /// <summary>
        /// 25400900 Тип помещения (справочный код) (TYPE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25400900)]
        public PremisesTypes TypeName_Code
        {
            get
            {
                CheckPropertyInited("TypeName_Code");
                return this._typename_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typename))
                    {
                         _typename = descr;
                    }
                }
                else
                {
                     _typename = descr;
                }

                this._typename_Code = value;
                NotifyPropertyChanged("TypeName");
                NotifyPropertyChanged("TypeName_Code");
            }
        }


        private decimal? _totalarea;
        /// <summary>
        /// 25401000 Общая площадь помещения (TOTAL_AREA)
        /// </summary>
        [RegisterAttribute(AttributeID = 25401000)]
        public decimal? TotalArea
        {
            get
            {
                CheckPropertyInited("TotalArea");
                return _totalarea;
            }
            set
            {
                _totalarea = value;
                NotifyPropertyChanged("TotalArea");
            }
        }


        private decimal? _livingarea;
        /// <summary>
        /// 25401001 Жилая площадь (LIVING_AREA)
        /// </summary>
        [RegisterAttribute(AttributeID = 25401001)]
        public decimal? LivingArea
        {
            get
            {
                CheckPropertyInited("LivingArea");
                return _livingarea;
            }
            set
            {
                _livingarea = value;
                NotifyPropertyChanged("LivingArea");
            }
        }


        private decimal? _totalareawithsummer;
        /// <summary>
        /// 25401002 Общая площадь (с летними) (TOTAL_AREA_WITH_SUMMER)
        /// </summary>
        [RegisterAttribute(AttributeID = 25401002)]
        public decimal? TotalAreaWithSummer
        {
            get
            {
                CheckPropertyInited("TotalAreaWithSummer");
                return _totalareawithsummer;
            }
            set
            {
                _totalareawithsummer = value;
                NotifyPropertyChanged("TotalAreaWithSummer");
            }
        }


        private long? _height;
        /// <summary>
        /// 25401400 Высота помещения (HEIGHT)
        /// </summary>
        [RegisterAttribute(AttributeID = 25401400)]
        public long? Height
        {
            get
            {
                CheckPropertyInited("Height");
                return _height;
            }
            set
            {
                _height = value;
                NotifyPropertyChanged("Height");
            }
        }


        private string _kvnom;
        /// <summary>
        /// 25401700 Номер помещения (KVNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 25401700)]
        public string Kvnom
        {
            get
            {
                CheckPropertyInited("Kvnom");
                return _kvnom;
            }
            set
            {
                _kvnom = value;
                NotifyPropertyChanged("Kvnom");
            }
        }


        private long? _sectionnumber;
        /// <summary>
        /// 25401800 Номер секции  (SECTION_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 25401800)]
        public long? SectionNumber
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


        private long? _idinsource;
        /// <summary>
        /// 25403300 ID объекта в системе источнике (ID_IN_SOURCE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25403300)]
        public long? IdInSource
        {
            get
            {
                CheckPropertyInited("IdInSource");
                return _idinsource;
            }
            set
            {
                _idinsource = value;
                NotifyPropertyChanged("IdInSource");
            }
        }


        private long? _roomscount;
        /// <summary>
        /// 25403400 Количество жилых комнат (ROOMS_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 25403400)]
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


        private DateTime? _updatedate;
        /// <summary>
        /// 25403500 Дата обновления (UPDATE_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25403500)]
        public DateTime? UpdateDate
        {
            get
            {
                CheckPropertyInited("UpdateDate");
                return _updatedate;
            }
            set
            {
                _updatedate = value;
                NotifyPropertyChanged("UpdateDate");
            }
        }


        private string _tet;
        /// <summary>
        /// 25403600 Тип этажа (TET)
        /// </summary>
        [RegisterAttribute(AttributeID = 25403600)]
        public string Tet
        {
            get
            {
                CheckPropertyInited("Tet");
                return _tet;
            }
            set
            {
                _tet = value;
                NotifyPropertyChanged("Tet");
            }
        }


        private long? _tet_Code;
        /// <summary>
        /// 25403600 Тип этажа (справочный код) (TET_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25403600)]
        public long? Tet_Code
        {
            get
            {
                CheckPropertyInited("Tet_Code");
                return _tet_Code;
            }
            set
            {
                _tet_Code = value;
                NotifyPropertyChanged("Tet_Code");
            }
        }


        private string _objtype;
        /// <summary>
        /// 25403700 Тип объекта недвижимости (OBJ_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25403700)]
        public string ObjType
        {
            get
            {
                CheckPropertyInited("ObjType");
                return _objtype;
            }
            set
            {
                _objtype = value;
                NotifyPropertyChanged("ObjType");
            }
        }


        private long? _objtype_Code;
        /// <summary>
        /// 25403700 Тип объекта недвижимости (справочный код) (OBJ_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25403700)]
        public long? ObjType_Code
        {
            get
            {
                CheckPropertyInited("ObjType_Code");
                return _objtype_Code;
            }
            set
            {
                _objtype_Code = value;
                NotifyPropertyChanged("ObjType_Code");
            }
        }


        private long? _unom;
        /// <summary>
        /// 25403800 UNOM (UNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 25403800)]
        public long? Unom
        {
            get
            {
                CheckPropertyInited("Unom");
                return _unom;
            }
            set
            {
                _unom = value;
                NotifyPropertyChanged("Unom");
            }
        }


        private decimal? _zpl;
        /// <summary>
        /// 25403900 Не входящие в общую площадь (ZPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 25403900)]
        public decimal? Zpl
        {
            get
            {
                CheckPropertyInited("Zpl");
                return _zpl;
            }
            set
            {
                _zpl = value;
                NotifyPropertyChanged("Zpl");
            }
        }


        private bool _bit0;
        /// <summary>
        /// 25404000 Признак архива (BIT0)
        /// </summary>
        [RegisterAttribute(AttributeID = 25404000)]
        public bool Bit0
        {
            get
            {
                CheckPropertyInited("Bit0");
                return _bit0;
            }
            set
            {
                _bit0 = value;
                NotifyPropertyChanged("Bit0");
            }
        }


        private string _tres;
        /// <summary>
        /// 25404100 Тип последнего решения по помещению (TRES)
        /// </summary>
        [RegisterAttribute(AttributeID = 25404100)]
        public string Tres
        {
            get
            {
                CheckPropertyInited("Tres");
                return _tres;
            }
            set
            {
                _tres = value;
                NotifyPropertyChanged("Tres");
            }
        }


        private SolutionTypes _tres_Code;
        /// <summary>
        /// 25404100 Тип последнего решения по помещению (справочный код) (TRES_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25404100)]
        public SolutionTypes Tres_Code
        {
            get
            {
                CheckPropertyInited("Tres_Code");
                return this._tres_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_tres))
                    {
                         _tres = descr;
                    }
                }
                else
                {
                     _tres = descr;
                }

                this._tres_Code = value;
                NotifyPropertyChanged("Tres");
                NotifyPropertyChanged("Tres_Code");
            }
        }


        private string _sres;
        /// <summary>
        /// 25404200 Содержание последнего решения (SRES)
        /// </summary>
        [RegisterAttribute(AttributeID = 25404200)]
        public string Sres
        {
            get
            {
                CheckPropertyInited("Sres");
                return _sres;
            }
            set
            {
                _sres = value;
                NotifyPropertyChanged("Sres");
            }
        }


        private SolutionContent _sres_Code;
        /// <summary>
        /// 25404200 Содержание последнего решения (справочный код) (SRES_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25404200)]
        public SolutionContent Sres_Code
        {
            get
            {
                CheckPropertyInited("Sres_Code");
                return this._sres_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_sres))
                    {
                         _sres = descr;
                    }
                }
                else
                {
                     _sres = descr;
                }

                this._sres_Code = value;
                NotifyPropertyChanged("Sres");
                NotifyPropertyChanged("Sres_Code");
            }
        }


        private DateTime? _dres;
        /// <summary>
        /// 25404300 Дата вынесения последнего решения (DRES)
        /// </summary>
        [RegisterAttribute(AttributeID = 25404300)]
        public DateTime? Dres
        {
            get
            {
                CheckPropertyInited("Dres");
                return _dres;
            }
            set
            {
                _dres = value;
                NotifyPropertyChanged("Dres");
            }
        }


        private string _nres;
        /// <summary>
        /// 25404400 № последнего решения (NRES)
        /// </summary>
        [RegisterAttribute(AttributeID = 25404400)]
        public string Nres
        {
            get
            {
                CheckPropertyInited("Nres");
                return _nres;
            }
            set
            {
                _nres = value;
                NotifyPropertyChanged("Nres");
            }
        }


        private DateTime? _ardtvv;
        /// <summary>
        /// 25404500 Последняя дата ввода информации об обременении помещения (AR_DTVV)
        /// </summary>
        [RegisterAttribute(AttributeID = 25404500)]
        public DateTime? ArDtvv
        {
            get
            {
                CheckPropertyInited("ArDtvv");
                return _ardtvv;
            }
            set
            {
                _ardtvv = value;
                NotifyPropertyChanged("ArDtvv");
            }
        }


        private DateTime? _ardt;
        /// <summary>
        /// 25404600 Дата регистрации последнего обременения (AR_DT)
        /// </summary>
        [RegisterAttribute(AttributeID = 25404600)]
        public DateTime? ArDt
        {
            get
            {
                CheckPropertyInited("ArDt");
                return _ardt;
            }
            set
            {
                _ardt = value;
                NotifyPropertyChanged("ArDt");
            }
        }


        private string _arosn;
        /// <summary>
        /// 25404700 Тип обременения (AR_OSN)
        /// </summary>
        [RegisterAttribute(AttributeID = 25404700)]
        public string ArOsn
        {
            get
            {
                CheckPropertyInited("ArOsn");
                return _arosn;
            }
            set
            {
                _arosn = value;
                NotifyPropertyChanged("ArOsn");
            }
        }


        private BurdenStatus _arosn_Code;
        /// <summary>
        /// 25404700 Тип обременения (справочный код) (AR_OSN_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25404700)]
        public BurdenStatus ArOsn_Code
        {
            get
            {
                CheckPropertyInited("ArOsn_Code");
                return this._arosn_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_arosn))
                    {
                         _arosn = descr;
                    }
                }
                else
                {
                     _arosn = descr;
                }

                this._arosn_Code = value;
                NotifyPropertyChanged("ArOsn");
                NotifyPropertyChanged("ArOsn_Code");
            }
        }


        private DateTime? _ardtsn;
        /// <summary>
        /// 25404800 Дата снятия последнего обременения (AR_DTSN)
        /// </summary>
        [RegisterAttribute(AttributeID = 25404800)]
        public DateTime? ArDtsn
        {
            get
            {
                CheckPropertyInited("ArDtsn");
                return _ardtsn;
            }
            set
            {
                _ardtsn = value;
                NotifyPropertyChanged("ArDtsn");
            }
        }


        private string _arosnsn;
        /// <summary>
        /// 25404900 Тип снятия обременения (AR_OSNSN)
        /// </summary>
        [RegisterAttribute(AttributeID = 25404900)]
        public string ArOsnsn
        {
            get
            {
                CheckPropertyInited("ArOsnsn");
                return _arosnsn;
            }
            set
            {
                _arosnsn = value;
                NotifyPropertyChanged("ArOsnsn");
            }
        }


        private BurdenStatus _arosnsn_Code;
        /// <summary>
        /// 25404900 Тип снятия обременения (справочный код) (AR_OSNSN_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25404900)]
        public BurdenStatus ArOsnsn_Code
        {
            get
            {
                CheckPropertyInited("ArOsnsn_Code");
                return this._arosnsn_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_arosnsn))
                    {
                         _arosnsn = descr;
                    }
                }
                else
                {
                     _arosnsn = descr;
                }

                this._arosnsn_Code = value;
                NotifyPropertyChanged("ArOsnsn");
                NotifyPropertyChanged("ArOsnsn_Code");
            }
        }

    }
}

namespace ObjectModel.Bti
{
    /// <summary>
    /// 257 БТИ: Комната (BTI_ROOMS)
    /// </summary>
    [RegisterInfo(RegisterID = 257)]
    [Serializable]
    public sealed partial class OMRooms : OMBaseClass<OMRooms>
    {

        private long _empid;
        /// <summary>
        /// 25700100 Инд.номер (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 25700100)]
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


        private long? _premiseid;
        /// <summary>
        /// 25700200 Помещение (PREMISE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25700200)]
        public long? PremiseId
        {
            get
            {
                CheckPropertyInited("PremiseId");
                return _premiseid;
            }
            set
            {
                _premiseid = value;
                NotifyPropertyChanged("PremiseId");
            }
        }


        private string _purposename;
        /// <summary>
        /// 25700300 Назначение комнаты (PURPOSE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25700300)]
        public string PurposeName
        {
            get
            {
                CheckPropertyInited("PurposeName");
                return _purposename;
            }
            set
            {
                _purposename = value;
                NotifyPropertyChanged("PurposeName");
            }
        }


        private long? _purposename_Code;
        /// <summary>
        /// 25700300 Назначение комнаты (справочный код) (PURPOSE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25700300)]
        public long? PurposeName_Code
        {
            get
            {
                CheckPropertyInited("PurposeName_Code");
                return _purposename_Code;
            }
            set
            {
                _purposename_Code = value;
                NotifyPropertyChanged("PurposeName_Code");
            }
        }


        private string _specialpurposename;
        /// <summary>
        /// 25700400 Специальное назначение комнаты (SPECIAL_PURPOSE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25700400)]
        public string SpecialPurposeName
        {
            get
            {
                CheckPropertyInited("SpecialPurposeName");
                return _specialpurposename;
            }
            set
            {
                _specialpurposename = value;
                NotifyPropertyChanged("SpecialPurposeName");
            }
        }


        private long? _specialpurposename_Code;
        /// <summary>
        /// 25700400 Специальное назначение комнаты (справочный код) (SPECIAL_PURPOSE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25700400)]
        public long? SpecialPurposeName_Code
        {
            get
            {
                CheckPropertyInited("SpecialPurposeName_Code");
                return _specialpurposename_Code;
            }
            set
            {
                _specialpurposename_Code = value;
                NotifyPropertyChanged("SpecialPurposeName_Code");
            }
        }


        private string _areakindname;
        /// <summary>
        /// 25700500 Вид площади комнаты (AREA_KIND_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25700500)]
        public string AreaKindName
        {
            get
            {
                CheckPropertyInited("AreaKindName");
                return _areakindname;
            }
            set
            {
                _areakindname = value;
                NotifyPropertyChanged("AreaKindName");
            }
        }


        private long? _areakindname_Code;
        /// <summary>
        /// 25700500 Вид площади комнаты (справочный код) (AREA_KIND_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25700500)]
        public long? AreaKindName_Code
        {
            get
            {
                CheckPropertyInited("AreaKindName_Code");
                return _areakindname_Code;
            }
            set
            {
                _areakindname_Code = value;
                NotifyPropertyChanged("AreaKindName_Code");
            }
        }


        private string _areatypename;
        /// <summary>
        /// 25700600 Тип площади комнаты (AREA_TYPE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25700600)]
        public string AreaTypeName
        {
            get
            {
                CheckPropertyInited("AreaTypeName");
                return _areatypename;
            }
            set
            {
                _areatypename = value;
                NotifyPropertyChanged("AreaTypeName");
            }
        }


        private PremisesTypes _areatypename_Code;
        /// <summary>
        /// 25700600 Тип площади комнаты (справочный код) (AREA_TYPE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25700600)]
        public PremisesTypes AreaTypeName_Code
        {
            get
            {
                CheckPropertyInited("AreaTypeName_Code");
                return this._areatypename_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_areatypename))
                    {
                         _areatypename = descr;
                    }
                }
                else
                {
                     _areatypename = descr;
                }

                this._areatypename_Code = value;
                NotifyPropertyChanged("AreaTypeName");
                NotifyPropertyChanged("AreaTypeName_Code");
            }
        }


        private long? _height;
        /// <summary>
        /// 25700700 Высота (HEIGHT)
        /// </summary>
        [RegisterAttribute(AttributeID = 25700700)]
        public long? Height
        {
            get
            {
                CheckPropertyInited("Height");
                return _height;
            }
            set
            {
                _height = value;
                NotifyPropertyChanged("Height");
            }
        }


        private DateTime? _surveydate;
        /// <summary>
        /// 25700800 Дата обследования (SURVEY_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25700800)]
        public DateTime? SurveyDate
        {
            get
            {
                CheckPropertyInited("SurveyDate");
                return _surveydate;
            }
            set
            {
                _surveydate = value;
                NotifyPropertyChanged("SurveyDate");
            }
        }


        private string _areacalculationformula;
        /// <summary>
        /// 25700900 Формула подсчета площади (AREA_CALCULATION_FORMULA)
        /// </summary>
        [RegisterAttribute(AttributeID = 25700900)]
        public string AreaCalculationFormula
        {
            get
            {
                CheckPropertyInited("AreaCalculationFormula");
                return _areacalculationformula;
            }
            set
            {
                _areacalculationformula = value;
                NotifyPropertyChanged("AreaCalculationFormula");
            }
        }


        private decimal? _area;
        /// <summary>
        /// 25701000 Площадь комнаты (AREA)
        /// </summary>
        [RegisterAttribute(AttributeID = 25701000)]
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


        private long? _numberpp;
        /// <summary>
        /// 25701100 Номер пп (NUMBER_PP)
        /// </summary>
        [RegisterAttribute(AttributeID = 25701100)]
        public long? NumberPp
        {
            get
            {
                CheckPropertyInited("NumberPp");
                return _numberpp;
            }
            set
            {
                _numberpp = value;
                NotifyPropertyChanged("NumberPp");
            }
        }


        private long? _floorid;
        /// <summary>
        /// 25701200 Этаж (FLOOR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25701200)]
        public long? FloorId
        {
            get
            {
                CheckPropertyInited("FloorId");
                return _floorid;
            }
            set
            {
                _floorid = value;
                NotifyPropertyChanged("FloorId");
            }
        }


        private string _plannumber;
        /// <summary>
        /// 25701300 Номер на плане (PLAN_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 25701300)]
        public string PlanNumber
        {
            get
            {
                CheckPropertyInited("PlanNumber");
                return _plannumber;
            }
            set
            {
                _plannumber = value;
                NotifyPropertyChanged("PlanNumber");
            }
        }


        private string _documentnumber;
        /// <summary>
        /// 25701400 Номер для документов (DOCUMENT_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 25701400)]
        public string DocumentNumber
        {
            get
            {
                CheckPropertyInited("DocumentNumber");
                return _documentnumber;
            }
            set
            {
                _documentnumber = value;
                NotifyPropertyChanged("DocumentNumber");
            }
        }


        private string _reductionrationame;
        /// <summary>
        /// 25701500 Понижающий коэффициент (REDUCTION_RATIO_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 25701500)]
        public string ReductionRatioName
        {
            get
            {
                CheckPropertyInited("ReductionRatioName");
                return _reductionrationame;
            }
            set
            {
                _reductionrationame = value;
                NotifyPropertyChanged("ReductionRatioName");
            }
        }


        private long? _reductionrationame_Code;
        /// <summary>
        /// 25701500 Понижающий коэффициент (справочный код) (REDUCTION_RATIO_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 25701500)]
        public long? ReductionRatioName_Code
        {
            get
            {
                CheckPropertyInited("ReductionRatioName_Code");
                return _reductionrationame_Code;
            }
            set
            {
                _reductionrationame_Code = value;
                NotifyPropertyChanged("ReductionRatioName_Code");
            }
        }


        private string _guidpp;
        /// <summary>
        /// 25701600 GUID_пп (GUID_PP)
        /// </summary>
        [RegisterAttribute(AttributeID = 25701600)]
        public string GuidPp
        {
            get
            {
                CheckPropertyInited("GuidPp");
                return _guidpp;
            }
            set
            {
                _guidpp = value;
                NotifyPropertyChanged("GuidPp");
            }
        }


        private decimal? _areapp;
        /// <summary>
        /// 25701700 Площадь_пп (AREA_PP)
        /// </summary>
        [RegisterAttribute(AttributeID = 25701700)]
        public decimal? AreaPp
        {
            get
            {
                CheckPropertyInited("AreaPp");
                return _areapp;
            }
            set
            {
                _areapp = value;
                NotifyPropertyChanged("AreaPp");
            }
        }


        private string _numberroompp;
        /// <summary>
        /// 25701800 Номер_комнаты_пп  (NUMBER_ROOM_PP)
        /// </summary>
        [RegisterAttribute(AttributeID = 25701800)]
        public string NumberRoomPp
        {
            get
            {
                CheckPropertyInited("NumberRoomPp");
                return _numberroompp;
            }
            set
            {
                _numberroompp = value;
                NotifyPropertyChanged("NumberRoomPp");
            }
        }


        private bool? _isrefittedwopermission;
        /// <summary>
        /// 25701900 Переоборудовано без разрешения (IS_REFITTED_WO_PERMISSION)
        /// </summary>
        [RegisterAttribute(AttributeID = 25701900)]
        public bool? IsRefittedWoPermission
        {
            get
            {
                CheckPropertyInited("IsRefittedWoPermission");
                return _isrefittedwopermission;
            }
            set
            {
                _isrefittedwopermission = value;
                NotifyPropertyChanged("IsRefittedWoPermission");
            }
        }


        private bool? _iscommonprorertyappartment;
        /// <summary>
        /// 25702000 Общее имущество многоквартирного дома (IS_COMMON_PRORERTY_APPARTMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 25702000)]
        public bool? IsCommonProrertyAppartment
        {
            get
            {
                CheckPropertyInited("IsCommonProrertyAppartment");
                return _iscommonprorertyappartment;
            }
            set
            {
                _iscommonprorertyappartment = value;
                NotifyPropertyChanged("IsCommonProrertyAppartment");
            }
        }


        private bool? _iscommonprorertycondominium;
        /// <summary>
        /// 25702100 Общее имущество кондоминиума (IS_COMMON_PRORERTY_CONDOMINIUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 25702100)]
        public bool? IsCommonProrertyCondominium
        {
            get
            {
                CheckPropertyInited("IsCommonProrertyCondominium");
                return _iscommonprorertycondominium;
            }
            set
            {
                _iscommonprorertycondominium = value;
                NotifyPropertyChanged("IsCommonProrertyCondominium");
            }
        }


        private string _kadastrnumber;
        /// <summary>
        /// 25702200 Кадастровый номер (KADASTR_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 25702200)]
        public string KadastrNumber
        {
            get
            {
                CheckPropertyInited("KadastrNumber");
                return _kadastrnumber;
            }
            set
            {
                _kadastrnumber = value;
                NotifyPropertyChanged("KadastrNumber");
            }
        }


        private long? _idinsource;
        /// <summary>
        /// 25702300 ID объекта в системе источнике (ID_IN_SOURCE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25702300)]
        public long? IdInSource
        {
            get
            {
                CheckPropertyInited("IdInSource");
                return _idinsource;
            }
            set
            {
                _idinsource = value;
                NotifyPropertyChanged("IdInSource");
            }
        }


        private DateTime? _updatedate;
        /// <summary>
        /// 25702400 Дата обновления (UPDATE_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 25702400)]
        public DateTime? UpdateDate
        {
            get
            {
                CheckPropertyInited("UpdateDate");
                return _updatedate;
            }
            set
            {
                _updatedate = value;
                NotifyPropertyChanged("UpdateDate");
            }
        }

    }
}

namespace ObjectModel.Bti
{
    /// <summary>
    /// 258 БТИ: Округ (REF_ADDR_OKRUG)
    /// </summary>
    [RegisterInfo(RegisterID = 258)]
    [Serializable]
    public sealed partial class OMBtiOkrug : OMBaseClass<OMBtiOkrug>
    {

        private long _id;
        /// <summary>
        /// 258000100 Идентификатор (OKRUG_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 258000100)]
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
        /// 258000200 Полное наименование округа (FULL_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 258000200)]
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


        private string _shortname;
        /// <summary>
        /// 258000300 Сокращенное наименование округа (SHORT_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 258000300)]
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


        private long? _code;
        /// <summary>
        /// 258000400 Код округа (STEKS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 258000400)]
        public long? Code
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


        private long? _subjectrfid;
        /// <summary>
        /// 258000500 Ссылка на субъект (SUBJECT_RF_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 258000500)]
        public long? SubjectRFId
        {
            get
            {
                CheckPropertyInited("SubjectRFId");
                return _subjectrfid;
            }
            set
            {
                _subjectrfid = value;
                NotifyPropertyChanged("SubjectRFId");
            }
        }


        private string _nameforsort;
        /// <summary>
        /// 258000600 Наименование для сортировки (NAME_FOR_SORT)
        /// </summary>
        [RegisterAttribute(AttributeID = 258000600)]
        public string NameForSort
        {
            get
            {
                CheckPropertyInited("NameForSort");
                return _nameforsort;
            }
            set
            {
                _nameforsort = value;
                NotifyPropertyChanged("NameForSort");
            }
        }


        private string _omkcode;
        /// <summary>
        /// 258000700 Код ОМК (OMK_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 258000700)]
        public string OmkCode
        {
            get
            {
                CheckPropertyInited("OmkCode");
                return _omkcode;
            }
            set
            {
                _omkcode = value;
                NotifyPropertyChanged("OmkCode");
            }
        }


        private long? _typeref;
        /// <summary>
        /// 258000800 Тип (TYPE_REF)
        /// </summary>
        [RegisterAttribute(AttributeID = 258000800)]
        public long? TypeRef
        {
            get
            {
                CheckPropertyInited("TypeRef");
                return _typeref;
            }
            set
            {
                _typeref = value;
                NotifyPropertyChanged("TypeRef");
            }
        }


        private string _codegivc;
        /// <summary>
        /// 258000900 Код округа GIVC (CODE_GIVC)
        /// </summary>
        [RegisterAttribute(AttributeID = 258000900)]
        public string CodeGivc
        {
            get
            {
                CheckPropertyInited("CodeGivc");
                return _codegivc;
            }
            set
            {
                _codegivc = value;
                NotifyPropertyChanged("CodeGivc");
            }
        }


        private long? _insurancecompanyid;
        /// <summary>
        /// 258001000 Идентификатор страховой компании (INSURANCE_COMPANY_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 258001000)]
        public long? InsuranceCompanyId
        {
            get
            {
                CheckPropertyInited("InsuranceCompanyId");
                return _insurancecompanyid;
            }
            set
            {
                _insurancecompanyid = value;
                NotifyPropertyChanged("InsuranceCompanyId");
            }
        }

    }
}

namespace ObjectModel.Bti
{
    /// <summary>
    /// 259 БТИ: Район (REF_ADDR_DISTRICT)
    /// </summary>
    [RegisterInfo(RegisterID = 259)]
    [Serializable]
    public sealed partial class OMBtiDistrict : OMBaseClass<OMBtiDistrict>
    {

        private long _id;
        /// <summary>
        /// 259000100 Идентификатор (DISTRICT_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 259000100)]
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
        /// 259000200 Полное наименование района (FULL_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 259000200)]
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


        private string _shortname;
        /// <summary>
        /// 259000300 Краткое наименование района (SHORT_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 259000300)]
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


        private long? _code;
        /// <summary>
        /// 259000400 Код района (STEKS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 259000400)]
        public long? Code
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


        private long? _okrugid;
        /// <summary>
        /// 259000500 Идентификатор округа (OKRUG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 259000500)]
        public long? OkrugId
        {
            get
            {
                CheckPropertyInited("OkrugId");
                return _okrugid;
            }
            set
            {
                _okrugid = value;
                NotifyPropertyChanged("OkrugId");
            }
        }


        private long? _subjectrfid;
        /// <summary>
        /// 259000600 Ссылка на субъект (SUBJECT_RF_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 259000600)]
        public long? SubjectRFId
        {
            get
            {
                CheckPropertyInited("SubjectRFId");
                return _subjectrfid;
            }
            set
            {
                _subjectrfid = value;
                NotifyPropertyChanged("SubjectRFId");
            }
        }


        private string _nameforsort;
        /// <summary>
        /// 259000700 Наименование для сортировки (NAME_FOR_SORT)
        /// </summary>
        [RegisterAttribute(AttributeID = 259000700)]
        public string NameForSort
        {
            get
            {
                CheckPropertyInited("NameForSort");
                return _nameforsort;
            }
            set
            {
                _nameforsort = value;
                NotifyPropertyChanged("NameForSort");
            }
        }


        private string _omkcode;
        /// <summary>
        /// 259000800 Код ОМК (OMK_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 259000800)]
        public string OmkCode
        {
            get
            {
                CheckPropertyInited("OmkCode");
                return _omkcode;
            }
            set
            {
                _omkcode = value;
                NotifyPropertyChanged("OmkCode");
            }
        }


        private long? _typeref;
        /// <summary>
        /// 259000900 Тип (TYPE_REF)
        /// </summary>
        [RegisterAttribute(AttributeID = 259000900)]
        public long? TypeRef
        {
            get
            {
                CheckPropertyInited("TypeRef");
                return _typeref;
            }
            set
            {
                _typeref = value;
                NotifyPropertyChanged("TypeRef");
            }
        }


        private string _codegivc;
        /// <summary>
        /// 259001000 Код округа GIVC (CODE_GIVC)
        /// </summary>
        [RegisterAttribute(AttributeID = 259001000)]
        public string CodeGivc
        {
            get
            {
                CheckPropertyInited("CodeGivc");
                return _codegivc;
            }
            set
            {
                _codegivc = value;
                NotifyPropertyChanged("CodeGivc");
            }
        }

    }
}

namespace ObjectModel.Bti
{
    /// <summary>
    /// 260 БТИ: Диапазоны квартир (BTI_DIAPKV_Q)
    /// </summary>
    [RegisterInfo(RegisterID = 260)]
    [Serializable]
    public sealed partial class OMDiapKv : OMBaseClass<OMDiapKv>
    {

        private long? _empid;
        /// <summary>
        /// 26000100 Идентификатор (EMP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 26000100)]
        public long? EmpId
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


        private long? _objid;
        /// <summary>
        /// 26000200 Внешний идентификатор (OBJ_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 26000200)]
        public long? ObjId
        {
            get
            {
                CheckPropertyInited("ObjId");
                return _objid;
            }
            set
            {
                _objid = value;
                NotifyPropertyChanged("ObjId");
            }
        }


        private string _objtype;
        /// <summary>
        /// 26000300 Тип (OBJ_TYPE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 26000300)]
        public string ObjType
        {
            get
            {
                CheckPropertyInited("ObjType");
                return _objtype;
            }
            set
            {
                _objtype = value;
                NotifyPropertyChanged("ObjType");
            }
        }


        private long? _objtype_Code;
        /// <summary>
        /// 26000300 Тип (справочный код) (OBJ_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 26000300)]
        public long? ObjType_Code
        {
            get
            {
                CheckPropertyInited("ObjType_Code");
                return _objtype_Code;
            }
            set
            {
                _objtype_Code = value;
                NotifyPropertyChanged("ObjType_Code");
            }
        }


        private long? _unom;
        /// <summary>
        /// 26000400 Уникальный идентификатор здания (UNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 26000400)]
        public long? Unom
        {
            get
            {
                CheckPropertyInited("Unom");
                return _unom;
            }
            set
            {
                _unom = value;
                NotifyPropertyChanged("Unom");
            }
        }


        private long? _n1;
        /// <summary>
        /// 26000500 N1 (N1)
        /// </summary>
        [RegisterAttribute(AttributeID = 26000500)]
        public long? N1
        {
            get
            {
                CheckPropertyInited("N1");
                return _n1;
            }
            set
            {
                _n1 = value;
                NotifyPropertyChanged("N1");
            }
        }


        private long? _i1;
        /// <summary>
        /// 26000600 Минимальный номер помещения (I1)
        /// </summary>
        [RegisterAttribute(AttributeID = 26000600)]
        public long? I1
        {
            get
            {
                CheckPropertyInited("I1");
                return _i1;
            }
            set
            {
                _i1 = value;
                NotifyPropertyChanged("I1");
            }
        }


        private long? _n2;
        /// <summary>
        /// 26000700 N2 (N2)
        /// </summary>
        [RegisterAttribute(AttributeID = 26000700)]
        public long? N2
        {
            get
            {
                CheckPropertyInited("N2");
                return _n2;
            }
            set
            {
                _n2 = value;
                NotifyPropertyChanged("N2");
            }
        }


        private string _i2;
        /// <summary>
        /// 26000800 Максимальный номер помещения (I2)
        /// </summary>
        [RegisterAttribute(AttributeID = 26000800)]
        public string I2
        {
            get
            {
                CheckPropertyInited("I2");
                return _i2;
            }
            set
            {
                _i2 = value;
                NotifyPropertyChanged("I2");
            }
        }


        private long? _q;
        /// <summary>
        /// 26000900 Количество помещений в диапазоне (Q)
        /// </summary>
        [RegisterAttribute(AttributeID = 26000900)]
        public long? Q
        {
            get
            {
                CheckPropertyInited("Q");
                return _q;
            }
            set
            {
                _q = value;
                NotifyPropertyChanged("Q");
            }
        }


        private long? _dup;
        /// <summary>
        /// 26001000 dup (DUP)
        /// </summary>
        [RegisterAttribute(AttributeID = 26001000)]
        public long? Dup
        {
            get
            {
                CheckPropertyInited("Dup");
                return _dup;
            }
            set
            {
                _dup = value;
                NotifyPropertyChanged("Dup");
            }
        }


        private string _rim;
        /// <summary>
        /// 26001100 rim (RIM)
        /// </summary>
        [RegisterAttribute(AttributeID = 26001100)]
        public string Rim
        {
            get
            {
                CheckPropertyInited("Rim");
                return _rim;
            }
            set
            {
                _rim = value;
                NotifyPropertyChanged("Rim");
            }
        }


        private long _id;
        /// <summary>
        /// 26001200 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 26001200)]
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

namespace ObjectModel.Insur
{
    /// <summary>
    /// 301 Реестр загрузки файлов (INSUR_INPUT_FILE)
    /// </summary>
    [RegisterInfo(RegisterID = 301)]
    [Serializable]
    public sealed partial class OMInputFile : OMBaseClass<OMInputFile>
    {

        private long _empid;
        /// <summary>
        /// 301000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 301000100)]
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


        private string _filename;
        /// <summary>
        /// 301000200 Название файла (FILE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 301000200)]
        public string FileName
        {
            get
            {
                CheckPropertyInited("FileName");
                return _filename;
            }
            set
            {
                _filename = value;
                NotifyPropertyChanged("FileName");
            }
        }


        private string _typefile;
        /// <summary>
        /// 301000300 Код типа файла  на основании справочника «Код типа файла» (TYPE_FILE)
        /// </summary>
        [RegisterAttribute(AttributeID = 301000300)]
        public string TypeFile
        {
            get
            {
                CheckPropertyInited("TypeFile");
                return _typefile;
            }
            set
            {
                _typefile = value;
                NotifyPropertyChanged("TypeFile");
            }
        }


        private TypeFile _typefile_Code;
        /// <summary>
        /// 301000300 Код типа файла  на основании справочника «Код типа файла» (справочный код) (TYPE_FILE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 301000300)]
        public TypeFile TypeFile_Code
        {
            get
            {
                CheckPropertyInited("TypeFile_Code");
                return this._typefile_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typefile))
                    {
                         _typefile = descr;
                    }
                }
                else
                {
                     _typefile = descr;
                }

                this._typefile_Code = value;
                NotifyPropertyChanged("TypeFile");
                NotifyPropertyChanged("TypeFile_Code");
            }
        }


        private DateTime? _periodregdate;
        /// <summary>
        /// 301000400 Период учета данных в Системе (PERIOD_REG_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 301000400)]
        public DateTime? PeriodRegDate
        {
            get
            {
                CheckPropertyInited("PeriodRegDate");
                return _periodregdate;
            }
            set
            {
                _periodregdate = value;
                NotifyPropertyChanged("PeriodRegDate");
            }
        }


        private long? _districtid;
        /// <summary>
        /// 301000500 Идентификатор района (DISTRICT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 301000500)]
        public long? DistrictId
        {
            get
            {
                CheckPropertyInited("DistrictId");
                return _districtid;
            }
            set
            {
                _districtid = value;
                NotifyPropertyChanged("DistrictId");
            }
        }


        private string _typesource;
        /// <summary>
        /// 301000600 Источник  (1-МФЦ, 2-СК) (TYPE_SOURCE)
        /// </summary>
        [RegisterAttribute(AttributeID = 301000600)]
        public string TypeSource
        {
            get
            {
                CheckPropertyInited("TypeSource");
                return _typesource;
            }
            set
            {
                _typesource = value;
                NotifyPropertyChanged("TypeSource");
            }
        }


        private InsuranceSourceType _typesource_Code;
        /// <summary>
        /// 301000600 Источник  (1-МФЦ, 2-СК) (справочный код) (TYPE_SOURCE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 301000600)]
        public InsuranceSourceType TypeSource_Code
        {
            get
            {
                CheckPropertyInited("TypeSource_Code");
                return this._typesource_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typesource))
                    {
                         _typesource = descr;
                    }
                }
                else
                {
                     _typesource = descr;
                }

                this._typesource_Code = value;
                NotifyPropertyChanged("TypeSource");
                NotifyPropertyChanged("TypeSource_Code");
            }
        }


        private DateTime? _dateinput;
        /// <summary>
        /// 301000700 Дата загрузки в Систему (DATE_INPUT)
        /// </summary>
        [RegisterAttribute(AttributeID = 301000700)]
        public DateTime? DateInput
        {
            get
            {
                CheckPropertyInited("DateInput");
                return _dateinput;
            }
            set
            {
                _dateinput = value;
                NotifyPropertyChanged("DateInput");
            }
        }


        private long? _countstr;
        /// <summary>
        /// 301000800 Количество строк в файле (COUNT_STR)
        /// </summary>
        [RegisterAttribute(AttributeID = 301000800)]
        public long? CountStr
        {
            get
            {
                CheckPropertyInited("CountStr");
                return _countstr;
            }
            set
            {
                _countstr = value;
                NotifyPropertyChanged("CountStr");
            }
        }


        private string _status;
        /// <summary>
        /// 301000900 Статус загрузки/обработки файла (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 301000900)]
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


        private UFKFileProcessingStatus _status_Code;
        /// <summary>
        /// 301000900 Статус загрузки/обработки файла (справочный код) (STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 301000900)]
        public UFKFileProcessingStatus Status_Code
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


        private long? _linkpackage;
        /// <summary>
        /// 301001000 Идентификатор пакета загрузки (LINK_PACKAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 301001000)]
        public long? LinkPackage
        {
            get
            {
                CheckPropertyInited("LinkPackage");
                return _linkpackage;
            }
            set
            {
                _linkpackage = value;
                NotifyPropertyChanged("LinkPackage");
            }
        }


        private long? _userid;
        /// <summary>
        /// 301001100 Ссылка на пользователя (USER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 301001100)]
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


        private decimal? _sumall;
        /// <summary>
        /// 301001200 Общая сумма по данным файла (SUM_ALL)
        /// </summary>
        [RegisterAttribute(AttributeID = 301001200)]
        public decimal? SumAll
        {
            get
            {
                CheckPropertyInited("SumAll");
                return _sumall;
            }
            set
            {
                _sumall = value;
                NotifyPropertyChanged("SumAll");
            }
        }


        private long? _filestorageid;
        /// <summary>
        /// 301001300 Ссылка на OMFileStorage (FILE_STORAGE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 301001300)]
        public long? FileStorageId
        {
            get
            {
                CheckPropertyInited("FileStorageId");
                return _filestorageid;
            }
            set
            {
                _filestorageid = value;
                NotifyPropertyChanged("FileStorageId");
            }
        }


        private bool? _criteriaset;
        /// <summary>
        /// 301001400 Пройдена процедура установки критериев (CRITERIA_SET)
        /// </summary>
        [RegisterAttribute(AttributeID = 301001400)]
        public bool? CriteriaSet
        {
            get
            {
                CheckPropertyInited("CriteriaSet");
                return _criteriaset;
            }
            set
            {
                _criteriaset = value;
                NotifyPropertyChanged("CriteriaSet");
            }
        }


        private long? _kodpost;
        /// <summary>
        /// 301001500 Код поставщика (KOD_POST)
        /// </summary>
        [RegisterAttribute(AttributeID = 301001500)]
        public long? KodPost
        {
            get
            {
                CheckPropertyInited("KodPost");
                return _kodpost;
            }
            set
            {
                _kodpost = value;
                NotifyPropertyChanged("KodPost");
            }
        }


        private long? _parentid;
        /// <summary>
        /// 301001600 Ссылка на ранее загруженный файл (PARENT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 301001600)]
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


        private long? _countstrload;
        /// <summary>
        /// 301001700 Загружено строк (COUNT_STR_LOAD)
        /// </summary>
        [RegisterAttribute(AttributeID = 301001700)]
        public long? CountStrLoad
        {
            get
            {
                CheckPropertyInited("CountStrLoad");
                return _countstrload;
            }
            set
            {
                _countstrload = value;
                NotifyPropertyChanged("CountStrLoad");
            }
        }


        private long? _logfileid;
        /// <summary>
        /// 301001800 Идентификатор журнала загрузки (LOG_FILE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 301001800)]
        public long? LogFileId
        {
            get
            {
                CheckPropertyInited("LogFileId");
                return _logfileid;
            }
            set
            {
                _logfileid = value;
                NotifyPropertyChanged("LogFileId");
            }
        }


        private long? _insuranceorganizationid;
        /// <summary>
        /// 301001900 Ссылка на страховую организацию (INSURANCE_ORGANIZATION_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 301001900)]
        public long? InsuranceOrganizationId
        {
            get
            {
                CheckPropertyInited("InsuranceOrganizationId");
                return _insuranceorganizationid;
            }
            set
            {
                _insuranceorganizationid = value;
                NotifyPropertyChanged("InsuranceOrganizationId");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 302 Реестр журналов обработки пакета файлов (INSUR_LOG_FILE)
    /// </summary>
    [RegisterInfo(RegisterID = 302)]
    [Serializable]
    public sealed partial class OMLogFile : OMBaseClass<OMLogFile>
    {

        private long _empid;
        /// <summary>
        /// 302000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 302000100)]
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


        private long? _filestorageid;
        /// <summary>
        /// 302000200 Ссылка на загружаемый файл (FILE_STORAGE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 302000200)]
        public long? FileStorageId
        {
            get
            {
                CheckPropertyInited("FileStorageId");
                return _filestorageid;
            }
            set
            {
                _filestorageid = value;
                NotifyPropertyChanged("FileStorageId");
            }
        }


        private DateTime? _loaddate;
        /// <summary>
        /// 302000300 Дата загрузки (LOADDATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 302000300)]
        public DateTime? Loaddate
        {
            get
            {
                CheckPropertyInited("Loaddate");
                return _loaddate;
            }
            set
            {
                _loaddate = value;
                NotifyPropertyChanged("Loaddate");
            }
        }


        private string _tracedata;
        /// <summary>
        /// 302000400 Результаты загрузки пакет данных (LOG-файл) (TRACEDATA)
        /// </summary>
        [RegisterAttribute(AttributeID = 302000400)]
        public string Tracedata
        {
            get
            {
                CheckPropertyInited("Tracedata");
                return _tracedata;
            }
            set
            {
                _tracedata = value;
                NotifyPropertyChanged("Tracedata");
            }
        }


        private long? _okrugid;
        /// <summary>
        /// 302000500 Идентификатор округа (OKRUG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 302000500)]
        public long? OkrugId
        {
            get
            {
                CheckPropertyInited("OkrugId");
                return _okrugid;
            }
            set
            {
                _okrugid = value;
                NotifyPropertyChanged("OkrugId");
            }
        }


        private DateTime? _periodregdate;
        /// <summary>
        /// 302000600 Период (PERIOD_REG_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 302000600)]
        public DateTime? PeriodRegDate
        {
            get
            {
                CheckPropertyInited("PeriodRegDate");
                return _periodregdate;
            }
            set
            {
                _periodregdate = value;
                NotifyPropertyChanged("PeriodRegDate");
            }
        }


        private string _status;
        /// <summary>
        /// 302000700 Прогресс загрузки (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 302000700)]
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


        private MfcUploadFileStatus _status_Code;
        /// <summary>
        /// 302000700 Прогресс загрузки (справочный код) (STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 302000700)]
        public MfcUploadFileStatus Status_Code
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


        private string _generalstatus;
        /// <summary>
        /// 302000800 Общий статус загрузки (GENERAL_STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 302000800)]
        public string GeneralStatus
        {
            get
            {
                CheckPropertyInited("GeneralStatus");
                return _generalstatus;
            }
            set
            {
                _generalstatus = value;
                NotifyPropertyChanged("GeneralStatus");
            }
        }


        private MfcGeneralUploadStatus _generalstatus_Code;
        /// <summary>
        /// 302000800 Общий статус загрузки (справочный код) (GENERAL_STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 302000800)]
        public MfcGeneralUploadStatus GeneralStatus_Code
        {
            get
            {
                CheckPropertyInited("GeneralStatus_Code");
                return this._generalstatus_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_generalstatus))
                    {
                         _generalstatus = descr;
                    }
                }
                else
                {
                     _generalstatus = descr;
                }

                this._generalstatus_Code = value;
                NotifyPropertyChanged("GeneralStatus");
                NotifyPropertyChanged("GeneralStatus_Code");
            }
        }


        private DateTime? _startdate;
        /// <summary>
        /// 302000900 Дата начала загрузки (START_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 302000900)]
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
        /// 302001000 Дата окончания загрузки (END_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 302001000)]
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

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 303 Реестр банковских файлов оплат (INSUR_BANK_PLAT)
    /// </summary>
    [RegisterInfo(RegisterID = 303)]
    [Serializable]
    public sealed partial class OMBankPlat : OMBaseClass<OMBankPlat>
    {

        private long? _empid;
        /// <summary>
        /// 303000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 303000100)]
        public long? EmpId
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


        private long? _linkidfile;
        /// <summary>
        /// 303000200 Ссылка на INSUR_INPUT_FILE (LINK_ID_FILE)
        /// </summary>
        [RegisterAttribute(AttributeID = 303000200)]
        public long? LinkIdFile
        {
            get
            {
                CheckPropertyInited("LinkIdFile");
                return _linkidfile;
            }
            set
            {
                _linkidfile = value;
                NotifyPropertyChanged("LinkIdFile");
            }
        }


        private long? _linksvodbank;
        /// <summary>
        /// 303000300 Ссылка на INSUR_SVOD_BANK (LINK_SVOD_BANK)
        /// </summary>
        [RegisterAttribute(AttributeID = 303000300)]
        public long? LinkSvodBank
        {
            get
            {
                CheckPropertyInited("LinkSvodBank");
                return _linksvodbank;
            }
            set
            {
                _linksvodbank = value;
                NotifyPropertyChanged("LinkSvodBank");
            }
        }


        private long? _districtid;
        /// <summary>
        /// 303000400 Идентификатор района (DISTRICT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 303000400)]
        public long? DistrictId
        {
            get
            {
                CheckPropertyInited("DistrictId");
                return _districtid;
            }
            set
            {
                _districtid = value;
                NotifyPropertyChanged("DistrictId");
            }
        }


        private DateTime? _bankday;
        /// <summary>
        /// 303000500 Банковский день (BANK_DAY)
        /// </summary>
        [RegisterAttribute(AttributeID = 303000500)]
        public DateTime? BankDay
        {
            get
            {
                CheckPropertyInited("BankDay");
                return _bankday;
            }
            set
            {
                _bankday = value;
                NotifyPropertyChanged("BankDay");
            }
        }


        private string _kodpl;
        /// <summary>
        /// 303000600 Код плательщика (KODPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 303000600)]
        public string Kodpl
        {
            get
            {
                CheckPropertyInited("Kodpl");
                return _kodpl;
            }
            set
            {
                _kodpl = value;
                NotifyPropertyChanged("Kodpl");
            }
        }


        private DateTime? _periodregdate;
        /// <summary>
        /// 303000700 Первое число месяца, в котором данные должны быть учтены на ФСП (PERIOD_REG_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 303000700)]
        public DateTime? PeriodRegDate
        {
            get
            {
                CheckPropertyInited("PeriodRegDate");
                return _periodregdate;
            }
            set
            {
                _periodregdate = value;
                NotifyPropertyChanged("PeriodRegDate");
            }
        }


        private DateTime? _period;
        /// <summary>
        /// 303000800 Оплачиваемый период (PERIOD)
        /// </summary>
        [RegisterAttribute(AttributeID = 303000800)]
        public DateTime? Period
        {
            get
            {
                CheckPropertyInited("Period");
                return _period;
            }
            set
            {
                _period = value;
                NotifyPropertyChanged("Period");
            }
        }


        private string _nomdoc;
        /// <summary>
        /// 303000900 Номер документа (NOM_DOC)
        /// </summary>
        [RegisterAttribute(AttributeID = 303000900)]
        public string NomDoc
        {
            get
            {
                CheckPropertyInited("NomDoc");
                return _nomdoc;
            }
            set
            {
                _nomdoc = value;
                NotifyPropertyChanged("NomDoc");
            }
        }


        private decimal? _sumall;
        /// <summary>
        /// 303001000 Cумма платежа (всего по ЕПД) (SUM_ALL)
        /// </summary>
        [RegisterAttribute(AttributeID = 303001000)]
        public decimal? SumAll
        {
            get
            {
                CheckPropertyInited("SumAll");
                return _sumall;
            }
            set
            {
                _sumall = value;
                NotifyPropertyChanged("SumAll");
            }
        }


        private decimal? _kombankall;
        /// <summary>
        /// 303001100 Комиссия Банка (KOM_BANK_ALL)
        /// </summary>
        [RegisterAttribute(AttributeID = 303001100)]
        public decimal? KomBankAll
        {
            get
            {
                CheckPropertyInited("KomBankAll");
                return _kombankall;
            }
            set
            {
                _kombankall = value;
                NotifyPropertyChanged("KomBankAll");
            }
        }


        private long? _bicbank;
        /// <summary>
        /// 303001200 БИК Банка (BIC_BANK)
        /// </summary>
        [RegisterAttribute(AttributeID = 303001200)]
        public long? BicBank
        {
            get
            {
                CheckPropertyInited("BicBank");
                return _bicbank;
            }
            set
            {
                _bicbank = value;
                NotifyPropertyChanged("BicBank");
            }
        }


        private DateTime? _datapp;
        /// <summary>
        /// 303001300 Дата платежа (DATA_PP)
        /// </summary>
        [RegisterAttribute(AttributeID = 303001300)]
        public DateTime? DataPp
        {
            get
            {
                CheckPropertyInited("DataPp");
                return _datapp;
            }
            set
            {
                _datapp = value;
                NotifyPropertyChanged("DataPp");
            }
        }


        private long? _coddoc;
        /// <summary>
        /// 303001400 Уникальный код документа (COD_DOC)
        /// </summary>
        [RegisterAttribute(AttributeID = 303001400)]
        public long? CodDoc
        {
            get
            {
                CheckPropertyInited("CodDoc");
                return _coddoc;
            }
            set
            {
                _coddoc = value;
                NotifyPropertyChanged("CodDoc");
            }
        }


        private long? _kodysl;
        /// <summary>
        /// 303001500 Код услуги (KOD_YSL)
        /// </summary>
        [RegisterAttribute(AttributeID = 303001500)]
        public long? KodYsl
        {
            get
            {
                CheckPropertyInited("KodYsl");
                return _kodysl;
            }
            set
            {
                _kodysl = value;
                NotifyPropertyChanged("KodYsl");
            }
        }


        private long? _kodpost;
        /// <summary>
        /// 303001600 Код поставщика (KOD_POST)
        /// </summary>
        [RegisterAttribute(AttributeID = 303001600)]
        public long? KodPost
        {
            get
            {
                CheckPropertyInited("KodPost");
                return _kodpost;
            }
            set
            {
                _kodpost = value;
                NotifyPropertyChanged("KodPost");
            }
        }


        private decimal? _sumbycode;
        /// <summary>
        /// 303001700 Сумма платежа по коду (SUM_BY_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 303001700)]
        public decimal? SumByCode
        {
            get
            {
                CheckPropertyInited("SumByCode");
                return _sumbycode;
            }
            set
            {
                _sumbycode = value;
                NotifyPropertyChanged("SumByCode");
            }
        }


        private decimal? _kombank;
        /// <summary>
        /// 303001800 Комиссия Банка (KOM_BANK)
        /// </summary>
        [RegisterAttribute(AttributeID = 303001800)]
        public decimal? KomBank
        {
            get
            {
                CheckPropertyInited("KomBank");
                return _kombank;
            }
            set
            {
                _kombank = value;
                NotifyPropertyChanged("KomBank");
            }
        }


        private decimal? _kombankobr;
        /// <summary>
        /// 303001900 Комиссия за обработку (KOM_BANK_OBR)
        /// </summary>
        [RegisterAttribute(AttributeID = 303001900)]
        public decimal? KomBankObr
        {
            get
            {
                CheckPropertyInited("KomBankObr");
                return _kombankobr;
            }
            set
            {
                _kombankobr = value;
                NotifyPropertyChanged("KomBankObr");
            }
        }


        private decimal? _komeirc;
        /// <summary>
        /// 303002000 Комиссия ЕИРЦ (KOM_EIRC)
        /// </summary>
        [RegisterAttribute(AttributeID = 303002000)]
        public decimal? KomEirc
        {
            get
            {
                CheckPropertyInited("KomEirc");
                return _komeirc;
            }
            set
            {
                _komeirc = value;
                NotifyPropertyChanged("KomEirc");
            }
        }


        private decimal? _komplat;
        /// <summary>
        /// 303002100 Комиссия за работу с плательщиками (KOM_PLAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 303002100)]
        public decimal? KomPlat
        {
            get
            {
                CheckPropertyInited("KomPlat");
                return _komplat;
            }
            set
            {
                _komplat = value;
                NotifyPropertyChanged("KomPlat");
            }
        }


        private DateTime? _docperiod;
        /// <summary>
        /// 303002200 Период документа (DOC_PERIOD)
        /// </summary>
        [RegisterAttribute(AttributeID = 303002200)]
        public DateTime? DocPeriod
        {
            get
            {
                CheckPropertyInited("DocPeriod");
                return _docperiod;
            }
            set
            {
                _docperiod = value;
                NotifyPropertyChanged("DocPeriod");
            }
        }


        private long? _flagvozvr;
        /// <summary>
        /// 303002300 Признак распределения (FLAG_VOZVR)
        /// </summary>
        [RegisterAttribute(AttributeID = 303002300)]
        public long? FlagVozvr
        {
            get
            {
                CheckPropertyInited("FlagVozvr");
                return _flagvozvr;
            }
            set
            {
                _flagvozvr = value;
                NotifyPropertyChanged("FlagVozvr");
            }
        }


        private long? _typeopl;
        /// <summary>
        /// 303002400 Тип оплаты (TYPE_OPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 303002400)]
        public long? TypeOpl
        {
            get
            {
                CheckPropertyInited("TypeOpl");
                return _typeopl;
            }
            set
            {
                _typeopl = value;
                NotifyPropertyChanged("TypeOpl");
            }
        }


        private long? _kodypravl;
        /// <summary>
        /// 303002500 Код управляющей компании (KOD_YPRAVL)
        /// </summary>
        [RegisterAttribute(AttributeID = 303002500)]
        public long? KodYpravl
        {
            get
            {
                CheckPropertyInited("KodYpravl");
                return _kodypravl;
            }
            set
            {
                _kodypravl = value;
                NotifyPropertyChanged("KodYpravl");
            }
        }


        private long? _flagnach;
        /// <summary>
        /// 303002600 Признак начисления (FLAG_NACH)
        /// </summary>
        [RegisterAttribute(AttributeID = 303002600)]
        public long? FlagNach
        {
            get
            {
                CheckPropertyInited("FlagNach");
                return _flagnach;
            }
            set
            {
                _flagnach = value;
                NotifyPropertyChanged("FlagNach");
            }
        }


        private decimal? _sumvsego;
        /// <summary>
        /// 303002700 Сумма платежей в файле (SUM_VSEGO)
        /// </summary>
        [RegisterAttribute(AttributeID = 303002700)]
        public decimal? SumVsego
        {
            get
            {
                CheckPropertyInited("SumVsego");
                return _sumvsego;
            }
            set
            {
                _sumvsego = value;
                NotifyPropertyChanged("SumVsego");
            }
        }


        private decimal? _strokvsego;
        /// <summary>
        /// 303002800 Кол-во строк в файле (STROK_VSEGO)
        /// </summary>
        [RegisterAttribute(AttributeID = 303002800)]
        public decimal? StrokVsego
        {
            get
            {
                CheckPropertyInited("StrokVsego");
                return _strokvsego;
            }
            set
            {
                _strokvsego = value;
                NotifyPropertyChanged("StrokVsego");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 304 Реестр cводные данные по файлу оплат (INSUR_SVOD_BANK)
    /// </summary>
    [RegisterInfo(RegisterID = 304)]
    [Serializable]
    public sealed partial class OMSvodBank : OMBaseClass<OMSvodBank>
    {

        private long _empid;
        /// <summary>
        /// 304000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 304000100)]
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


        private long? _linkidfile;
        /// <summary>
        /// 304000200 Ссылка на файл загрузки, строка в Реестре INSUR_INPUT_FILE (LINK_ID_FILE)
        /// </summary>
        [RegisterAttribute(AttributeID = 304000200)]
        public long? LinkIdFile
        {
            get
            {
                CheckPropertyInited("LinkIdFile");
                return _linkidfile;
            }
            set
            {
                _linkidfile = value;
                NotifyPropertyChanged("LinkIdFile");
            }
        }


        private string _filename;
        /// <summary>
        /// 304000300 Название файла (FILE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 304000300)]
        public string FileName
        {
            get
            {
                CheckPropertyInited("FileName");
                return _filename;
            }
            set
            {
                _filename = value;
                NotifyPropertyChanged("FileName");
            }
        }


        private DateTime? _bankday;
        /// <summary>
        /// 304000400 Банковский день (BANK_DAY)
        /// </summary>
        [RegisterAttribute(AttributeID = 304000400)]
        public DateTime? BankDay
        {
            get
            {
                CheckPropertyInited("BankDay");
                return _bankday;
            }
            set
            {
                _bankday = value;
                NotifyPropertyChanged("BankDay");
            }
        }


        private long? _districtid;
        /// <summary>
        /// 304000500 Идентификатор района (DISTRICT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 304000500)]
        public long? DistrictId
        {
            get
            {
                CheckPropertyInited("DistrictId");
                return _districtid;
            }
            set
            {
                _districtid = value;
                NotifyPropertyChanged("DistrictId");
            }
        }


        private long? _str;
        /// <summary>
        /// 304000600 Кол-во строк в файле (STR)
        /// </summary>
        [RegisterAttribute(AttributeID = 304000600)]
        public long? Str
        {
            get
            {
                CheckPropertyInited("Str");
                return _str;
            }
            set
            {
                _str = value;
                NotifyPropertyChanged("Str");
            }
        }


        private decimal? _paysum;
        /// <summary>
        /// 304000700 Общая сумма платежей (PAY_SUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 304000700)]
        public decimal? PaySum
        {
            get
            {
                CheckPropertyInited("PaySum");
                return _paysum;
            }
            set
            {
                _paysum = value;
                NotifyPropertyChanged("PaySum");
            }
        }


        private string _codpost;
        /// <summary>
        /// 304000800 Код поставщика (COD_POST)
        /// </summary>
        [RegisterAttribute(AttributeID = 304000800)]
        public string CodPost
        {
            get
            {
                CheckPropertyInited("CodPost");
                return _codpost;
            }
            set
            {
                _codpost = value;
                NotifyPropertyChanged("CodPost");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 305 Реестр начислений (INSUR_INPUT_NACH)
    /// </summary>
    [RegisterInfo(RegisterID = 305)]
    [Serializable]
    public sealed partial class OMInputNach : OMBaseClass<OMInputNach>
    {

        private long _empid;
        /// <summary>
        /// 305000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 305000100)]
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


        private long? _linkidfile;
        /// <summary>
        /// 305000200 Ссылка на файл загрузки, строка в Реестре INSUR_INPUT_FILE (LINK_ID_FILE)
        /// </summary>
        [RegisterAttribute(AttributeID = 305000200)]
        public long? LinkIdFile
        {
            get
            {
                CheckPropertyInited("LinkIdFile");
                return _linkidfile;
            }
            set
            {
                _linkidfile = value;
                NotifyPropertyChanged("LinkIdFile");
            }
        }


        private long? _fspid;
        /// <summary>
        /// 305000300 Ссылка на реестр ФСП INSUR_FSP (FSP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 305000300)]
        public long? FspId
        {
            get
            {
                CheckPropertyInited("FspId");
                return _fspid;
            }
            set
            {
                _fspid = value;
                NotifyPropertyChanged("FspId");
            }
        }


        private string _typesource;
        /// <summary>
        /// 305000400 Источник  (1-МФЦ, 2-СК, 4-ГБУ) (TYPE_SOURCE)
        /// </summary>
        [RegisterAttribute(AttributeID = 305000400)]
        public string TypeSource
        {
            get
            {
                CheckPropertyInited("TypeSource");
                return _typesource;
            }
            set
            {
                _typesource = value;
                NotifyPropertyChanged("TypeSource");
            }
        }


        private InsuranceSourceType _typesource_Code;
        /// <summary>
        /// 305000400 Источник  (1-МФЦ, 2-СК, 4-ГБУ) (справочный код) (TYPE_SOURCE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 305000400)]
        public InsuranceSourceType TypeSource_Code
        {
            get
            {
                CheckPropertyInited("TypeSource_Code");
                return this._typesource_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typesource))
                    {
                         _typesource = descr;
                    }
                }
                else
                {
                     _typesource = descr;
                }

                this._typesource_Code = value;
                NotifyPropertyChanged("TypeSource");
                NotifyPropertyChanged("TypeSource_Code");
            }
        }


        private string _statusidentif;
        /// <summary>
        /// 305000500 Статус идентификации записи (STATUS_IDENTIF)
        /// </summary>
        [RegisterAttribute(AttributeID = 305000500)]
        public string StatusIdentif
        {
            get
            {
                CheckPropertyInited("StatusIdentif");
                return _statusidentif;
            }
            set
            {
                _statusidentif = value;
                NotifyPropertyChanged("StatusIdentif");
            }
        }


        private StatusIdentifikacii _statusidentif_Code;
        /// <summary>
        /// 305000500 Статус идентификации записи (справочный код) (STATUS_IDENTIF_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 305000500)]
        public StatusIdentifikacii StatusIdentif_Code
        {
            get
            {
                CheckPropertyInited("StatusIdentif_Code");
                return this._statusidentif_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_statusidentif))
                    {
                         _statusidentif = descr;
                    }
                }
                else
                {
                     _statusidentif = descr;
                }

                this._statusidentif_Code = value;
                NotifyPropertyChanged("StatusIdentif");
                NotifyPropertyChanged("StatusIdentif_Code");
            }
        }


        private DateTime? _periodregdate;
        /// <summary>
        /// 305000700 Период учета данных в Системе (PERIOD_REG_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 305000700)]
        public DateTime? PeriodRegDate
        {
            get
            {
                CheckPropertyInited("PeriodRegDate");
                return _periodregdate;
            }
            set
            {
                _periodregdate = value;
                NotifyPropertyChanged("PeriodRegDate");
            }
        }


        private DateTime? _period;
        /// <summary>
        /// 305000800 Период, за который произведена оплата (PERIOD)
        /// </summary>
        [RegisterAttribute(AttributeID = 305000800)]
        public DateTime? Period
        {
            get
            {
                CheckPropertyInited("Period");
                return _period;
            }
            set
            {
                _period = value;
                NotifyPropertyChanged("Period");
            }
        }


        private long? _districtid;
        /// <summary>
        /// 305000900 Идентификатор района (DISTRICT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 305000900)]
        public long? DistrictId
        {
            get
            {
                CheckPropertyInited("DistrictId");
                return _districtid;
            }
            set
            {
                _districtid = value;
                NotifyPropertyChanged("DistrictId");
            }
        }


        private long? _kod;
        /// <summary>
        /// 305001000 Код страховой организации  (KOD)
        /// </summary>
        [RegisterAttribute(AttributeID = 305001000)]
        public long? Kod
        {
            get
            {
                CheckPropertyInited("Kod");
                return _kod;
            }
            set
            {
                _kod = value;
                NotifyPropertyChanged("Kod");
            }
        }


        private long? _unom;
        /// <summary>
        /// 305001200 Уникальный номер дома (UNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 305001200)]
        public long? Unom
        {
            get
            {
                CheckPropertyInited("Unom");
                return _unom;
            }
            set
            {
                _unom = value;
                NotifyPropertyChanged("Unom");
            }
        }


        private string _adrest;
        /// <summary>
        /// 305001300 Адрес дома  (ADRES_T)
        /// </summary>
        [RegisterAttribute(AttributeID = 305001300)]
        public string AdresT
        {
            get
            {
                CheckPropertyInited("AdresT");
                return _adrest;
            }
            set
            {
                _adrest = value;
                NotifyPropertyChanged("AdresT");
            }
        }


        private string _unkva;
        /// <summary>
        /// 305001400 Уникальный номер квартиры в доме (UNKVA)
        /// </summary>
        [RegisterAttribute(AttributeID = 305001400)]
        public string Unkva
        {
            get
            {
                CheckPropertyInited("Unkva");
                return _unkva;
            }
            set
            {
                _unkva = value;
                NotifyPropertyChanged("Unkva");
            }
        }


        private string _nomi;
        /// <summary>
        /// 305001500 Индекс квартиры (NOMI) (NOMI)
        /// </summary>
        [RegisterAttribute(AttributeID = 305001500)]
        public string Nomi
        {
            get
            {
                CheckPropertyInited("Nomi");
                return _nomi;
            }
            set
            {
                _nomi = value;
                NotifyPropertyChanged("Nomi");
            }
        }


        private string _nom;
        /// <summary>
        /// 305001600 Номер квартиры (NOM) (NOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 305001600)]
        public string Nom
        {
            get
            {
                CheckPropertyInited("Nom");
                return _nom;
            }
            set
            {
                _nom = value;
                NotifyPropertyChanged("Nom");
            }
        }


        private string _kvnom;
        /// <summary>
        /// 305001700 Номер квартиры (KVNOM) (KVNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 305001700)]
        public string Kvnom
        {
            get
            {
                CheckPropertyInited("Kvnom");
                return _kvnom;
            }
            set
            {
                _kvnom = value;
                NotifyPropertyChanged("Kvnom");
            }
        }


        private long? _flatstatusid;
        /// <summary>
        /// 305001800 Ссылка на статус жилого помещения (FLAT_STATUS_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 305001800)]
        public long? FlatStatusId
        {
            get
            {
                CheckPropertyInited("FlatStatusId");
                return _flatstatusid;
            }
            set
            {
                _flatstatusid = value;
                NotifyPropertyChanged("FlatStatusId");
            }
        }


        private long? _flattypeid;
        /// <summary>
        /// 305001900 Ссылка на тип жилого помещения (FLAT_TYPE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 305001900)]
        public long? FlatTypeId
        {
            get
            {
                CheckPropertyInited("FlatTypeId");
                return _flattypeid;
            }
            set
            {
                _flattypeid = value;
                NotifyPropertyChanged("FlatTypeId");
            }
        }


        private long? _kolgp;
        /// <summary>
        /// 305002000 Количество жилых помещений в квартире (KOLGP)
        /// </summary>
        [RegisterAttribute(AttributeID = 305002000)]
        public long? Kolgp
        {
            get
            {
                CheckPropertyInited("Kolgp");
                return _kolgp;
            }
            set
            {
                _kolgp = value;
                NotifyPropertyChanged("Kolgp");
            }
        }


        private decimal? _fopl;
        /// <summary>
        /// 305002100 Общая площадь квартиры (FOPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 305002100)]
        public decimal? Fopl
        {
            get
            {
                CheckPropertyInited("Fopl");
                return _fopl;
            }
            set
            {
                _fopl = value;
                NotifyPropertyChanged("Fopl");
            }
        }


        private decimal? _opl;
        /// <summary>
        /// 305002200 Подлежащая страхованию площадь жилого помещения (OPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 305002200)]
        public decimal? Opl
        {
            get
            {
                CheckPropertyInited("Opl");
                return _opl;
            }
            set
            {
                _opl = value;
                NotifyPropertyChanged("Opl");
            }
        }


        private string _kodpl;
        /// <summary>
        /// 305002300 Код плательщика (KODPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 305002300)]
        public string Kodpl
        {
            get
            {
                CheckPropertyInited("Kodpl");
                return _kodpl;
            }
            set
            {
                _kodpl = value;
                NotifyPropertyChanged("Kodpl");
            }
        }


        private long? _ls;
        /// <summary>
        /// 305002400 Лицевой счет (LS)
        /// </summary>
        [RegisterAttribute(AttributeID = 305002400)]
        public long? Ls
        {
            get
            {
                CheckPropertyInited("Ls");
                return _ls;
            }
            set
            {
                _ls = value;
                NotifyPropertyChanged("Ls");
            }
        }


        private decimal? _sumnach;
        /// <summary>
        /// 305002500 Величина начисленного страхового взноса (SUM_NACH)
        /// </summary>
        [RegisterAttribute(AttributeID = 305002500)]
        public decimal? SumNach
        {
            get
            {
                CheckPropertyInited("SumNach");
                return _sumnach;
            }
            set
            {
                _sumnach = value;
                NotifyPropertyChanged("SumNach");
            }
        }


        private long? _flagunomno;
        /// <summary>
        /// 305002600 1/0 (UNOM найден в Адресном списке/ UNOM не найден в Адресном списке) (FLAG_UNOM_NO)
        /// </summary>
        [RegisterAttribute(AttributeID = 305002600)]
        public long? FlagUnomNo
        {
            get
            {
                CheckPropertyInited("FlagUnomNo");
                return _flagunomno;
            }
            set
            {
                _flagunomno = value;
                NotifyPropertyChanged("FlagUnomNo");
            }
        }


        private string _fio;
        /// <summary>
        /// 305002700 ФИО плательщика (FIO)
        /// </summary>
        [RegisterAttribute(AttributeID = 305002700)]
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


        private string _loadstatus;
        /// <summary>
        /// 305002800 Статус загрузки (LOAD_STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 305002800)]
        public string LoadStatus
        {
            get
            {
                CheckPropertyInited("LoadStatus");
                return _loadstatus;
            }
            set
            {
                _loadstatus = value;
                NotifyPropertyChanged("LoadStatus");
            }
        }


        private LoadStatus _loadstatus_Code;
        /// <summary>
        /// 305002800 Статус загрузки (справочный код) (LOAD_STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 305002800)]
        public LoadStatus LoadStatus_Code
        {
            get
            {
                CheckPropertyInited("LoadStatus_Code");
                return this._loadstatus_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_loadstatus))
                    {
                         _loadstatus = descr;
                    }
                }
                else
                {
                     _loadstatus = descr;
                }

                this._loadstatus_Code = value;
                NotifyPropertyChanged("LoadStatus");
                NotifyPropertyChanged("LoadStatus_Code");
            }
        }


        private string _criteriajson;
        /// <summary>
        /// 305002900 Список вхождения по критериям (CRITERIA_JSON)
        /// </summary>
        [RegisterAttribute(AttributeID = 305002900)]
        public string CriteriaJson
        {
            get
            {
                CheckPropertyInited("CriteriaJson");
                return _criteriajson;
            }
            set
            {
                _criteriajson = value;
                NotifyPropertyChanged("CriteriaJson");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 306 Реестр зачислений (платежей) (INSUR_INPUT_PLAT)
    /// </summary>
    [RegisterInfo(RegisterID = 306)]
    [Serializable]
    public sealed partial class OMInputPlat : OMBaseClass<OMInputPlat>
    {

        private long _empid;
        /// <summary>
        /// 306000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 306000100)]
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


        private long? _linkidfile;
        /// <summary>
        /// 306000200 Ссылка на файл загрузки, строка в Реестре INSUR_INPUT_FILE (LINK_ID_FILE)
        /// </summary>
        [RegisterAttribute(AttributeID = 306000200)]
        public long? LinkIdFile
        {
            get
            {
                CheckPropertyInited("LinkIdFile");
                return _linkidfile;
            }
            set
            {
                _linkidfile = value;
                NotifyPropertyChanged("LinkIdFile");
            }
        }


        private long? _fspid;
        /// <summary>
        /// 306000300 Ссылка на реестр ФСП INSUR_FSP (FSP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 306000300)]
        public long? FspId
        {
            get
            {
                CheckPropertyInited("FspId");
                return _fspid;
            }
            set
            {
                _fspid = value;
                NotifyPropertyChanged("FspId");
            }
        }


        private long? _linkbankid;
        /// <summary>
        /// 306000400 Ссылка на реестр c банковскими днями INSUR_BANK_PLAT (LINK_BANK_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 306000400)]
        public long? LinkBankId
        {
            get
            {
                CheckPropertyInited("LinkBankId");
                return _linkbankid;
            }
            set
            {
                _linkbankid = value;
                NotifyPropertyChanged("LinkBankId");
            }
        }


        private long? _unom;
        /// <summary>
        /// 306000500 Уникальный номер дома (UNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 306000500)]
        public long? Unom
        {
            get
            {
                CheckPropertyInited("Unom");
                return _unom;
            }
            set
            {
                _unom = value;
                NotifyPropertyChanged("Unom");
            }
        }


        private string _adres;
        /// <summary>
        /// 306000600 Адрес дома (ADRES)
        /// </summary>
        [RegisterAttribute(AttributeID = 306000600)]
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


        private string _nom;
        /// <summary>
        /// 306000700 Номер квартиры (NOM) (NOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 306000700)]
        public string Nom
        {
            get
            {
                CheckPropertyInited("Nom");
                return _nom;
            }
            set
            {
                _nom = value;
                NotifyPropertyChanged("Nom");
            }
        }


        private string _kodpl;
        /// <summary>
        /// 306000800 Код плательщика (KODPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 306000800)]
        public string Kodpl
        {
            get
            {
                CheckPropertyInited("Kodpl");
                return _kodpl;
            }
            set
            {
                _kodpl = value;
                NotifyPropertyChanged("Kodpl");
            }
        }


        private long? _ls;
        /// <summary>
        /// 306000900 Лицевой счет (LS)
        /// </summary>
        [RegisterAttribute(AttributeID = 306000900)]
        public long? Ls
        {
            get
            {
                CheckPropertyInited("Ls");
                return _ls;
            }
            set
            {
                _ls = value;
                NotifyPropertyChanged("Ls");
            }
        }


        private string _txid;
        /// <summary>
        /// 306001000 Банковская кодировка (TX_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 306001000)]
        public string TxId
        {
            get
            {
                CheckPropertyInited("TxId");
                return _txid;
            }
            set
            {
                _txid = value;
                NotifyPropertyChanged("TxId");
            }
        }


        private DateTime? _pmtdate;
        /// <summary>
        /// 306001100 Дата оплаты (PMT_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 306001100)]
        public DateTime? PmtDate
        {
            get
            {
                CheckPropertyInited("PmtDate");
                return _pmtdate;
            }
            set
            {
                _pmtdate = value;
                NotifyPropertyChanged("PmtDate");
            }
        }


        private DateTime? _dateintofk;
        /// <summary>
        /// 306001200 Дата поступления оплаты в банк (DATE_IN_TOFK)
        /// </summary>
        [RegisterAttribute(AttributeID = 306001200)]
        public DateTime? DateInTofk
        {
            get
            {
                CheckPropertyInited("DateInTofk");
                return _dateintofk;
            }
            set
            {
                _dateintofk = value;
                NotifyPropertyChanged("DateInTofk");
            }
        }


        private DateTime? _period;
        /// <summary>
        /// 306001300 Период, за который произведена оплата (PERIOD)
        /// </summary>
        [RegisterAttribute(AttributeID = 306001300)]
        public DateTime? Period
        {
            get
            {
                CheckPropertyInited("Period");
                return _period;
            }
            set
            {
                _period = value;
                NotifyPropertyChanged("Period");
            }
        }


        private DateTime? _periodregdate;
        /// <summary>
        /// 306001400 Период учета данных в Системе (PERIOD_REG_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 306001400)]
        public DateTime? PeriodRegDate
        {
            get
            {
                CheckPropertyInited("PeriodRegDate");
                return _periodregdate;
            }
            set
            {
                _periodregdate = value;
                NotifyPropertyChanged("PeriodRegDate");
            }
        }


        private decimal? _sumnach;
        /// <summary>
        /// 306001500 Начисленная сумма (SUM_NACH)
        /// </summary>
        [RegisterAttribute(AttributeID = 306001500)]
        public decimal? SumNach
        {
            get
            {
                CheckPropertyInited("SumNach");
                return _sumnach;
            }
            set
            {
                _sumnach = value;
                NotifyPropertyChanged("SumNach");
            }
        }


        private decimal? _sumopl;
        /// <summary>
        /// 306001600 Оплаченная сумма (может быть отрицательным числом) (SUM_OPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 306001600)]
        public decimal? SumOpl
        {
            get
            {
                CheckPropertyInited("SumOpl");
                return _sumopl;
            }
            set
            {
                _sumopl = value;
                NotifyPropertyChanged("SumOpl");
            }
        }


        private decimal? _fee;
        /// <summary>
        /// 306001700 Комиссия банка (FEE)
        /// </summary>
        [RegisterAttribute(AttributeID = 306001700)]
        public decimal? Fee
        {
            get
            {
                CheckPropertyInited("Fee");
                return _fee;
            }
            set
            {
                _fee = value;
                NotifyPropertyChanged("Fee");
            }
        }


        private decimal? _opl;
        /// <summary>
        /// 306001800 Площадь, подлежащая страхованию (OPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 306001800)]
        public decimal? Opl
        {
            get
            {
                CheckPropertyInited("Opl");
                return _opl;
            }
            set
            {
                _opl = value;
                NotifyPropertyChanged("Opl");
            }
        }


        private long? _flattypeid;
        /// <summary>
        /// 306001900 Идентификатор типа жилого помещения (FLAT_TYPE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 306001900)]
        public long? FlatTypeId
        {
            get
            {
                CheckPropertyInited("FlatTypeId");
                return _flattypeid;
            }
            set
            {
                _flattypeid = value;
                NotifyPropertyChanged("FlatTypeId");
            }
        }


        private string _fio;
        /// <summary>
        /// 306002000 ФИО (FIO)
        /// </summary>
        [RegisterAttribute(AttributeID = 306002000)]
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


        private string _comment;
        /// <summary>
        /// 306002100 Комментарий (COMMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 306002100)]
        public string Comment
        {
            get
            {
                CheckPropertyInited("Comment");
                return _comment;
            }
            set
            {
                _comment = value;
                NotifyPropertyChanged("Comment");
            }
        }


        private string _statusidentif;
        /// <summary>
        /// 306002200 Статус идентификации записи (STATUS_IDENTIF)
        /// </summary>
        [RegisterAttribute(AttributeID = 306002200)]
        public string StatusIdentif
        {
            get
            {
                CheckPropertyInited("StatusIdentif");
                return _statusidentif;
            }
            set
            {
                _statusidentif = value;
                NotifyPropertyChanged("StatusIdentif");
            }
        }


        private StatusIdentifikacii _statusidentif_Code;
        /// <summary>
        /// 306002200 Статус идентификации записи (справочный код) (STATUS_IDENTIF_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 306002200)]
        public StatusIdentifikacii StatusIdentif_Code
        {
            get
            {
                CheckPropertyInited("StatusIdentif_Code");
                return this._statusidentif_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_statusidentif))
                    {
                         _statusidentif = descr;
                    }
                }
                else
                {
                     _statusidentif = descr;
                }

                this._statusidentif_Code = value;
                NotifyPropertyChanged("StatusIdentif");
                NotifyPropertyChanged("StatusIdentif_Code");
            }
        }


        private string _typesource;
        /// <summary>
        /// 306002400 Источник  (1-МФЦ, 2-СК, 3-ГБУ) (TYPE_SOURCE)
        /// </summary>
        [RegisterAttribute(AttributeID = 306002400)]
        public string TypeSource
        {
            get
            {
                CheckPropertyInited("TypeSource");
                return _typesource;
            }
            set
            {
                _typesource = value;
                NotifyPropertyChanged("TypeSource");
            }
        }


        private InsuranceSourceType _typesource_Code;
        /// <summary>
        /// 306002400 Источник  (1-МФЦ, 2-СК, 3-ГБУ) (справочный код) (TYPE_SOURCE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 306002400)]
        public InsuranceSourceType TypeSource_Code
        {
            get
            {
                CheckPropertyInited("TypeSource_Code");
                return this._typesource_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typesource))
                    {
                         _typesource = descr;
                    }
                }
                else
                {
                     _typesource = descr;
                }

                this._typesource_Code = value;
                NotifyPropertyChanged("TypeSource");
                NotifyPropertyChanged("TypeSource_Code");
            }
        }


        private long? _flagunomno;
        /// <summary>
        /// 306002500 1/0 (UNOM найден в Адресном списке/ UNOM не найден в Адресном списке) (FLAG_UNOM_NO)
        /// </summary>
        [RegisterAttribute(AttributeID = 306002500)]
        public long? FlagUnomNo
        {
            get
            {
                CheckPropertyInited("FlagUnomNo");
                return _flagunomno;
            }
            set
            {
                _flagunomno = value;
                NotifyPropertyChanged("FlagUnomNo");
            }
        }


        private string _typedoc;
        /// <summary>
        /// 306002600 Тип договора  (TYPE_DOC)
        /// </summary>
        [RegisterAttribute(AttributeID = 306002600)]
        public string TypeDoc
        {
            get
            {
                CheckPropertyInited("TypeDoc");
                return _typedoc;
            }
            set
            {
                _typedoc = value;
                NotifyPropertyChanged("TypeDoc");
            }
        }


        private ContractType _typedoc_Code;
        /// <summary>
        /// 306002600 Тип договора  (справочный код) (TYPE_DOC_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 306002600)]
        public ContractType TypeDoc_Code
        {
            get
            {
                CheckPropertyInited("TypeDoc_Code");
                return this._typedoc_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typedoc))
                    {
                         _typedoc = descr;
                    }
                }
                else
                {
                     _typedoc = descr;
                }

                this._typedoc_Code = value;
                NotifyPropertyChanged("TypeDoc");
                NotifyPropertyChanged("TypeDoc_Code");
            }
        }


        private long? _kod;
        /// <summary>
        /// 306002700 Код страховой организации (KOD)
        /// </summary>
        [RegisterAttribute(AttributeID = 306002700)]
        public long? Kod
        {
            get
            {
                CheckPropertyInited("Kod");
                return _kod;
            }
            set
            {
                _kod = value;
                NotifyPropertyChanged("Kod");
            }
        }


        private string _ndog;
        /// <summary>
        /// 306002800 Уникальный номер договора страхования (для платежей по договорам  общего имущества) (NDOG)
        /// </summary>
        [RegisterAttribute(AttributeID = 306002800)]
        public string Ndog
        {
            get
            {
                CheckPropertyInited("Ndog");
                return _ndog;
            }
            set
            {
                _ndog = value;
                NotifyPropertyChanged("Ndog");
            }
        }


        private DateTime? _ndogdat;
        /// <summary>
        /// 306002900 Дата начала действия договора(для платежей по договорам  общего имущества) (NDOGDAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 306002900)]
        public DateTime? Ndogdat
        {
            get
            {
                CheckPropertyInited("Ndogdat");
                return _ndogdat;
            }
            set
            {
                _ndogdat = value;
                NotifyPropertyChanged("Ndogdat");
            }
        }


        private string _ndops;
        /// <summary>
        /// 306003000 Номер дополнительного соглашения((для платежей по договорам  общего имущества) (NDOPS)
        /// </summary>
        [RegisterAttribute(AttributeID = 306003000)]
        public string Ndops
        {
            get
            {
                CheckPropertyInited("Ndops");
                return _ndops;
            }
            set
            {
                _ndops = value;
                NotifyPropertyChanged("Ndops");
            }
        }


        private string _paynumber;
        /// <summary>
        /// 306003100 Номер платежного поручения по договору (для платежей по договорам  общего имущества) (PAYNUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 306003100)]
        public string Paynumber
        {
            get
            {
                CheckPropertyInited("Paynumber");
                return _paynumber;
            }
            set
            {
                _paynumber = value;
                NotifyPropertyChanged("Paynumber");
            }
        }


        private long? _policysvdid;
        /// <summary>
        /// 306003200 Ссылка на строку в Реестре INSUR_POLICY_SVD (INSUR_POLICY_SVD_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 306003200)]
        public long? PolicySvdId
        {
            get
            {
                CheckPropertyInited("PolicySvdId");
                return _policysvdid;
            }
            set
            {
                _policysvdid = value;
                NotifyPropertyChanged("PolicySvdId");
            }
        }


        private long? _districtid;
        /// <summary>
        /// 306003300 Идентификатор района (DISTRICT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 306003300)]
        public long? DistrictId
        {
            get
            {
                CheckPropertyInited("DistrictId");
                return _districtid;
            }
            set
            {
                _districtid = value;
                NotifyPropertyChanged("DistrictId");
            }
        }


        private string _loadstatus;
        /// <summary>
        /// 306003500 Статус загрузки (LOAD_STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 306003500)]
        public string LoadStatus
        {
            get
            {
                CheckPropertyInited("LoadStatus");
                return _loadstatus;
            }
            set
            {
                _loadstatus = value;
                NotifyPropertyChanged("LoadStatus");
            }
        }


        private LoadStatus _loadstatus_Code;
        /// <summary>
        /// 306003500 Статус загрузки (справочный код) (LOAD_STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 306003500)]
        public LoadStatus LoadStatus_Code
        {
            get
            {
                CheckPropertyInited("LoadStatus_Code");
                return this._loadstatus_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_loadstatus))
                    {
                         _loadstatus = descr;
                    }
                }
                else
                {
                     _loadstatus = descr;
                }

                this._loadstatus_Code = value;
                NotifyPropertyChanged("LoadStatus");
                NotifyPropertyChanged("LoadStatus_Code");
            }
        }


        private string _criteriajson;
        /// <summary>
        /// 306003600 Список вхождения по критериям (CRITERIA_JSON)
        /// </summary>
        [RegisterAttribute(AttributeID = 306003600)]
        public string CriteriaJson
        {
            get
            {
                CheckPropertyInited("CriteriaJson");
                return _criteriajson;
            }
            set
            {
                _criteriajson = value;
                NotifyPropertyChanged("CriteriaJson");
            }
        }


        private long? _linkallpropertyid;
        /// <summary>
        /// 306003700 Ссылка на реестр AllProrepty (LINK_ALL_PROPERTY_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 306003700)]
        public long? LinkAllPropertyId
        {
            get
            {
                CheckPropertyInited("LinkAllPropertyId");
                return _linkallpropertyid;
            }
            set
            {
                _linkallpropertyid = value;
                NotifyPropertyChanged("LinkAllPropertyId");
            }
        }


        private long? _insuranceorganizationid;
        /// <summary>
        /// 306003800 Ссылка на страховую организацию (INSURANCE_ORGANIZATION_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 306003800)]
        public long? InsuranceOrganizationId
        {
            get
            {
                CheckPropertyInited("InsuranceOrganizationId");
                return _insuranceorganizationid;
            }
            set
            {
                _insuranceorganizationid = value;
                NotifyPropertyChanged("InsuranceOrganizationId");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 307 Реестр ведомости учета страховых взносов (INSUR_BALANCE)
    /// </summary>
    [RegisterInfo(RegisterID = 307)]
    [Serializable]
    public sealed partial class OMBalance : OMBaseClass<OMBalance>
    {

        private long? _empid;
        /// <summary>
        /// 307000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 307000100)]
        public long? EmpId
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


        private long? _fspid;
        /// <summary>
        /// 307000200 Ссылка на ФСП INSUR_FSP_Q.EMP_ID (FSP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 307000200)]
        public long? FspId
        {
            get
            {
                CheckPropertyInited("FspId");
                return _fspid;
            }
            set
            {
                _fspid = value;
                NotifyPropertyChanged("FspId");
            }
        }


        private DateTime? _periodregdate;
        /// <summary>
        /// 307000300 Период учета данных в Системе (PERIOD_REG_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 307000300)]
        public DateTime? PeriodRegDate
        {
            get
            {
                CheckPropertyInited("PeriodRegDate");
                return _periodregdate;
            }
            set
            {
                _periodregdate = value;
                NotifyPropertyChanged("PeriodRegDate");
            }
        }


        private bool? _flagopl;
        /// <summary>
        /// 307000400 1/0 (Оплачено/не оплачено начисление) (FLAG_OPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 307000400)]
        public bool? FlagOpl
        {
            get
            {
                CheckPropertyInited("FlagOpl");
                return _flagopl;
            }
            set
            {
                _flagopl = value;
                NotifyPropertyChanged("FlagOpl");
            }
        }


        private long? _linkinputnach;
        /// <summary>
        /// 307000500 Ссылка на запись по начислению в INSUR_INPUT_NACH (LINK_INPUT_NACH)
        /// </summary>
        [RegisterAttribute(AttributeID = 307000500)]
        public long? LinkInputNach
        {
            get
            {
                CheckPropertyInited("LinkInputNach");
                return _linkinputnach;
            }
            set
            {
                _linkinputnach = value;
                NotifyPropertyChanged("LinkInputNach");
            }
        }


        private bool? _flaginsur;
        /// <summary>
        /// 307000600 1/0 (Застрахован период/Не застрахован) (FLAG_INSUR)
        /// </summary>
        [RegisterAttribute(AttributeID = 307000600)]
        public bool? FlagInsur
        {
            get
            {
                CheckPropertyInited("FlagInsur");
                return _flaginsur;
            }
            set
            {
                _flaginsur = value;
                NotifyPropertyChanged("FlagInsur");
            }
        }


        private decimal? _ostatoksum;
        /// <summary>
        /// 307000700 Нераспределенный остаток на начало периода (OSTATOK_SUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 307000700)]
        public decimal? OstatokSum
        {
            get
            {
                CheckPropertyInited("OstatokSum");
                return _ostatoksum;
            }
            set
            {
                _ostatoksum = value;
                NotifyPropertyChanged("OstatokSum");
            }
        }


        private decimal? _sumopl;
        /// <summary>
        /// 307000800 Сумма зачислений, нарастающим итогом (SUM_OPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 307000800)]
        public decimal? SumOpl
        {
            get
            {
                CheckPropertyInited("SumOpl");
                return _sumopl;
            }
            set
            {
                _sumopl = value;
                NotifyPropertyChanged("SumOpl");
            }
        }


        private decimal? _sumnachmfc;
        /// <summary>
        /// 307000900 Сумма начислений МФЦ в периоде, нарастающим итогом (SUM_NACH_MFC)
        /// </summary>
        [RegisterAttribute(AttributeID = 307000900)]
        public decimal? SumNachMfc
        {
            get
            {
                CheckPropertyInited("SumNachMfc");
                return _sumnachmfc;
            }
            set
            {
                _sumnachmfc = value;
                NotifyPropertyChanged("SumNachMfc");
            }
        }


        private decimal? _sumnachgby;
        /// <summary>
        /// 307001000 Сумма начислений ГБУ в периоде, нарастающим итогом (SUM_NACH_GBY)
        /// </summary>
        [RegisterAttribute(AttributeID = 307001000)]
        public decimal? SumNachGby
        {
            get
            {
                CheckPropertyInited("SumNachGby");
                return _sumnachgby;
            }
            set
            {
                _sumnachgby = value;
                NotifyPropertyChanged("SumNachGby");
            }
        }


        private decimal? _sumnachopl;
        /// <summary>
        /// 307001100 Сумма оплаченных начислений  в периоде, нарастающим итогом (SUM_NACH_OPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 307001100)]
        public decimal? SumNachOpl
        {
            get
            {
                CheckPropertyInited("SumNachOpl");
                return _sumnachopl;
            }
            set
            {
                _sumnachopl = value;
                NotifyPropertyChanged("SumNachOpl");
            }
        }


        private DateTime? _strahend;
        /// <summary>
        /// 307001200 Последний застрахованный период (STRAH_END)
        /// </summary>
        [RegisterAttribute(AttributeID = 307001200)]
        public DateTime? StrahEnd
        {
            get
            {
                CheckPropertyInited("StrahEnd");
                return _strahend;
            }
            set
            {
                _strahend = value;
                NotifyPropertyChanged("StrahEnd");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 308 Реестр Финансовых счетов плательщиков (ФСП) (INSUR_FSP_Q)
    /// </summary>
    [RegisterInfo(RegisterID = 308)]
    [Serializable]
    public sealed partial class OMFsp : OMBaseClass<OMFsp>
    {

        private long _empid;
        /// <summary>
        /// 308000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 308000100)]
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


        private string _fsptype;
        /// <summary>
        /// 308000200 Тип ФСП (FSP_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 308000200)]
        public string FspType
        {
            get
            {
                CheckPropertyInited("FspType");
                return _fsptype;
            }
            set
            {
                _fsptype = value;
                NotifyPropertyChanged("FspType");
            }
        }


        private FSPType _fsptype_Code;
        /// <summary>
        /// 308000200 Тип ФСП (справочный код) (FSP_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 308000200)]
        public FSPType FspType_Code
        {
            get
            {
                CheckPropertyInited("FspType_Code");
                return this._fsptype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_fsptype))
                    {
                         _fsptype = descr;
                    }
                }
                else
                {
                     _fsptype = descr;
                }

                this._fsptype_Code = value;
                NotifyPropertyChanged("FspType");
                NotifyPropertyChanged("FspType_Code");
            }
        }


        private string _fspnumber;
        /// <summary>
        /// 308000400 Номер  ФСП  (FSP_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 308000400)]
        public string FspNumber
        {
            get
            {
                CheckPropertyInited("FspNumber");
                return _fspnumber;
            }
            set
            {
                _fspnumber = value;
                NotifyPropertyChanged("FspNumber");
            }
        }


        private long? _ls;
        /// <summary>
        /// 308000500 Номер лицевого счета (LS)
        /// </summary>
        [RegisterAttribute(AttributeID = 308000500)]
        public long? Ls
        {
            get
            {
                CheckPropertyInited("Ls");
                return _ls;
            }
            set
            {
                _ls = value;
                NotifyPropertyChanged("Ls");
            }
        }


        private long? _objid;
        /// <summary>
        /// 308000600 Идентификатор объекта  (OBJ_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 308000600)]
        public long? ObjId
        {
            get
            {
                CheckPropertyInited("ObjId");
                return _objid;
            }
            set
            {
                _objid = value;
                NotifyPropertyChanged("ObjId");
            }
        }


        private long? _objreestrid;
        /// <summary>
        /// 308000700 Номер реестра объекта  (OBJ_REESTR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 308000700)]
        public long? ObjReestrId
        {
            get
            {
                CheckPropertyInited("ObjReestrId");
                return _objreestrid;
            }
            set
            {
                _objreestrid = value;
                NotifyPropertyChanged("ObjReestrId");
            }
        }


        private long? _contractid;
        /// <summary>
        /// 308000800 Идентификатор договора ( полиса/свидетельства/договора страхования общего имущества) (CONTRACT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 308000800)]
        public long? ContractId
        {
            get
            {
                CheckPropertyInited("ContractId");
                return _contractid;
            }
            set
            {
                _contractid = value;
                NotifyPropertyChanged("ContractId");
            }
        }


        private long? _idreestrcontr;
        /// <summary>
        /// 308000900 Номер реестра договоров (ID_REESTR_CONTR)
        /// </summary>
        [RegisterAttribute(AttributeID = 308000900)]
        public long? IdReestrContr
        {
            get
            {
                CheckPropertyInited("IdReestrContr");
                return _idreestrcontr;
            }
            set
            {
                _idreestrcontr = value;
                NotifyPropertyChanged("IdReestrContr");
            }
        }


        private DateTime? _dateopen;
        /// <summary>
        /// 308001000 Дата создания ФСП (DATE_OPEN)
        /// </summary>
        [RegisterAttribute(AttributeID = 308001000)]
        public DateTime? DateOpen
        {
            get
            {
                CheckPropertyInited("DateOpen");
                return _dateopen;
            }
            set
            {
                _dateopen = value;
                NotifyPropertyChanged("DateOpen");
            }
        }


        private string _kodpl;
        /// <summary>
        /// 308001100 Код плательщика (KODPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 308001100)]
        public string Kodpl
        {
            get
            {
                CheckPropertyInited("Kodpl");
                return _kodpl;
            }
            set
            {
                _kodpl = value;
                NotifyPropertyChanged("Kodpl");
            }
        }


        private decimal? _oplkodpl;
        /// <summary>
        /// 308001200 Площадь, подлежащая страхованию  (OPL_KODPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 308001200)]
        public decimal? OplKodpl
        {
            get
            {
                CheckPropertyInited("OplKodpl");
                return _oplkodpl;
            }
            set
            {
                _oplkodpl = value;
                NotifyPropertyChanged("OplKodpl");
            }
        }


        private bool? _flagmanyobj;
        /// <summary>
        /// 308001300 Содержит множество объектов (FLAG_MANY_OBJ)
        /// </summary>
        [RegisterAttribute(AttributeID = 308001300)]
        public bool? FlagManyObj
        {
            get
            {
                CheckPropertyInited("FlagManyObj");
                return _flagmanyobj;
            }
            set
            {
                _flagmanyobj = value;
                NotifyPropertyChanged("FlagManyObj");
            }
        }


        private long? _numobj;
        /// <summary>
        /// 308001400 Всего помещений, связанных с ФСП (NUM_OBJ)
        /// </summary>
        [RegisterAttribute(AttributeID = 308001400)]
        public long? NumObj
        {
            get
            {
                CheckPropertyInited("NumObj");
                return _numobj;
            }
            set
            {
                _numobj = value;
                NotifyPropertyChanged("NumObj");
            }
        }


        private DateTime? _oplkodplupdatedate;
        /// <summary>
        /// 308001500 Дата изменения площади ФСП (OPL_KODPL_UPDATE_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 308001500)]
        public DateTime? OplKodplUpdateDate
        {
            get
            {
                CheckPropertyInited("OplKodplUpdateDate");
                return _oplkodplupdatedate;
            }
            set
            {
                _oplkodplupdatedate = value;
                NotifyPropertyChanged("OplKodplUpdateDate");
            }
        }


        private long? _unomfsp;
        /// <summary>
        /// 308001800 УНОМ (МФЦ) (UNOM_FSP)
        /// </summary>
        [RegisterAttribute(AttributeID = 308001800)]
        public long? UnomFsp
        {
            get
            {
                CheckPropertyInited("UnomFsp");
                return _unomfsp;
            }
            set
            {
                _unomfsp = value;
                NotifyPropertyChanged("UnomFsp");
            }
        }


        private string _kvnomfsp;
        /// <summary>
        /// 308001900 Номер квартиры (МФЦ) (KVNOM_FSP)
        /// </summary>
        [RegisterAttribute(AttributeID = 308001900)]
        public string KvnomFsp
        {
            get
            {
                CheckPropertyInited("KvnomFsp");
                return _kvnomfsp;
            }
            set
            {
                _kvnomfsp = value;
                NotifyPropertyChanged("KvnomFsp");
            }
        }


        private decimal _controlnach;
        /// <summary>
        /// 308002000 Контрольное начисление ()
        /// </summary>
        [RegisterAttribute(AttributeID = 308002000)]
        public decimal ControlNach
        {
            get
            {
                CheckPropertyInited("ControlNach");
                return _controlnach;
            }
            set
            {
                _controlnach = value;
                NotifyPropertyChanged("ControlNach");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 309 Реестр страховых полисов и свидетельств (INSUR_POLICY_SVD)
    /// </summary>
    [RegisterInfo(RegisterID = 309)]
    [Serializable]
    public sealed partial class OMPolicySvd : OMBaseClass<OMPolicySvd>
    {

        private long? _empid;
        /// <summary>
        /// 309000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 309000100)]
        public long? EmpId
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


        private string _typedoc;
        /// <summary>
        /// 309000200 Тип документа (TYPE_DOC)
        /// </summary>
        [RegisterAttribute(AttributeID = 309000200)]
        public string TypeDoc
        {
            get
            {
                CheckPropertyInited("TypeDoc");
                return _typedoc;
            }
            set
            {
                _typedoc = value;
                NotifyPropertyChanged("TypeDoc");
            }
        }


        private DocumentType _typedoc_Code;
        /// <summary>
        /// 309000200 Тип документа (справочный код) (TYPE_DOC_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 309000200)]
        public DocumentType TypeDoc_Code
        {
            get
            {
                CheckPropertyInited("TypeDoc_Code");
                return this._typedoc_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typedoc))
                    {
                         _typedoc = descr;
                    }
                }
                else
                {
                     _typedoc = descr;
                }

                this._typedoc_Code = value;
                NotifyPropertyChanged("TypeDoc");
                NotifyPropertyChanged("TypeDoc_Code");
            }
        }


        private long? _fspid;
        /// <summary>
        /// 309000300 Ссылка на ФСП INSUR_FSP_Q.EMP_ID (FSP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 309000300)]
        public long? FspId
        {
            get
            {
                CheckPropertyInited("FspId");
                return _fspid;
            }
            set
            {
                _fspid = value;
                NotifyPropertyChanged("FspId");
            }
        }


        private long? _linkidfile;
        /// <summary>
        /// 309000400 Ссылка на файл загрузки, строка в Реестре INSUR_INPUT_FILE (LINK_ID_FILE)
        /// </summary>
        [RegisterAttribute(AttributeID = 309000400)]
        public long? LinkIdFile
        {
            get
            {
                CheckPropertyInited("LinkIdFile");
                return _linkidfile;
            }
            set
            {
                _linkidfile = value;
                NotifyPropertyChanged("LinkIdFile");
            }
        }


        private long? _linkidfileend;
        /// <summary>
        /// 309000500 Ссылка на файл загрузки, используется при загрузке данных из файла POLYC_D.DBF (LINK_ID_FILE_END)
        /// </summary>
        [RegisterAttribute(AttributeID = 309000500)]
        public long? LinkIdFileEnd
        {
            get
            {
                CheckPropertyInited("LinkIdFileEnd");
                return _linkidfileend;
            }
            set
            {
                _linkidfileend = value;
                NotifyPropertyChanged("LinkIdFileEnd");
            }
        }


        private long? _insuranceorganizationid;
        /// <summary>
        /// 309000600 Ссылка на страховую организацию (INSURANCE_ORGANIZATION_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 309000600)]
        public long? InsuranceOrganizationId
        {
            get
            {
                CheckPropertyInited("InsuranceOrganizationId");
                return _insuranceorganizationid;
            }
            set
            {
                _insuranceorganizationid = value;
                NotifyPropertyChanged("InsuranceOrganizationId");
            }
        }


        private long? _okrugid;
        /// <summary>
        /// 309000700 ID округа  (OKRUG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 309000700)]
        public long? OkrugId
        {
            get
            {
                CheckPropertyInited("OkrugId");
                return _okrugid;
            }
            set
            {
                _okrugid = value;
                NotifyPropertyChanged("OkrugId");
            }
        }


        private long? _districtid;
        /// <summary>
        /// 309000800 ID района  (DISTRICT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 309000800)]
        public long? DistrictId
        {
            get
            {
                CheckPropertyInited("DistrictId");
                return _districtid;
            }
            set
            {
                _districtid = value;
                NotifyPropertyChanged("DistrictId");
            }
        }


        private long? _orgtype;
        /// <summary>
        /// 309000900 Код формы объединения собственников (6 – ЖСК, ЖК,  7 – ТСЖ, 8 – БО (без объединения собственников), 0 – нет данных) (ORG_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 309000900)]
        public long? OrgType
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


        private long? _orgid;
        /// <summary>
        /// 309001000 Код управляющей организации (УО) (ORG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 309001000)]
        public long? OrgId
        {
            get
            {
                CheckPropertyInited("OrgId");
                return _orgid;
            }
            set
            {
                _orgid = value;
                NotifyPropertyChanged("OrgId");
            }
        }


        private long? _platid;
        /// <summary>
        /// 309001100 Код вида организации, начисляющей страховые взносы – по справочнику «Виды организаций, начисляющих страховые взносы» (PLAT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 309001100)]
        public long? PlatId
        {
            get
            {
                CheckPropertyInited("PlatId");
                return _platid;
            }
            set
            {
                _platid = value;
                NotifyPropertyChanged("PlatId");
            }
        }


        private long? _unom;
        /// <summary>
        /// 309001200 Уникальный номер строения  (UNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 309001200)]
        public long? Unom
        {
            get
            {
                CheckPropertyInited("Unom");
                return _unom;
            }
            set
            {
                _unom = value;
                NotifyPropertyChanged("Unom");
            }
        }


        private string _unkva;
        /// <summary>
        /// 309001300 Уникальный номер квартиры  в доме (UNKVA)
        /// </summary>
        [RegisterAttribute(AttributeID = 309001300)]
        public string Unkva
        {
            get
            {
                CheckPropertyInited("Unkva");
                return _unkva;
            }
            set
            {
                _unkva = value;
                NotifyPropertyChanged("Unkva");
            }
        }


        private string _nom;
        /// <summary>
        /// 309001400 Номер квартиры (NOM) (NOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 309001400)]
        public string Nom
        {
            get
            {
                CheckPropertyInited("Nom");
                return _nom;
            }
            set
            {
                _nom = value;
                NotifyPropertyChanged("Nom");
            }
        }


        private string _nomi;
        /// <summary>
        /// 309001500 Индекс квартиры (NOMI) (NOMI)
        /// </summary>
        [RegisterAttribute(AttributeID = 309001500)]
        public string Nomi
        {
            get
            {
                CheckPropertyInited("Nomi");
                return _nomi;
            }
            set
            {
                _nomi = value;
                NotifyPropertyChanged("Nomi");
            }
        }


        private string _kvnom;
        /// <summary>
        /// 309001600 Номер квартиры (KVNOM) (KVNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 309001600)]
        public string Kvnom
        {
            get
            {
                CheckPropertyInited("Kvnom");
                return _kvnom;
            }
            set
            {
                _kvnom = value;
                NotifyPropertyChanged("Kvnom");
            }
        }


        private long? _kolgp;
        /// <summary>
        /// 309001700 Количество жилых помещений в квартире (KOLGP)
        /// </summary>
        [RegisterAttribute(AttributeID = 309001700)]
        public long? Kolgp
        {
            get
            {
                CheckPropertyInited("Kolgp");
                return _kolgp;
            }
            set
            {
                _kolgp = value;
                NotifyPropertyChanged("Kolgp");
            }
        }


        private long? _flatstatus;
        /// <summary>
        /// 309001800 Код статуса жилого помещения (FLATSTATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 309001800)]
        public long? Flatstatus
        {
            get
            {
                CheckPropertyInited("Flatstatus");
                return _flatstatus;
            }
            set
            {
                _flatstatus = value;
                NotifyPropertyChanged("Flatstatus");
            }
        }


        private decimal? _fopl;
        /// <summary>
        /// 309001900 Общая площадь квартиры (FOPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 309001900)]
        public decimal? Fopl
        {
            get
            {
                CheckPropertyInited("Fopl");
                return _fopl;
            }
            set
            {
                _fopl = value;
                NotifyPropertyChanged("Fopl");
            }
        }


        private decimal? _opl;
        /// <summary>
        /// 309002000 Подлежащая страхованию площадь жилого помещения (OPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 309002000)]
        public decimal? Opl
        {
            get
            {
                CheckPropertyInited("Opl");
                return _opl;
            }
            set
            {
                _opl = value;
                NotifyPropertyChanged("Opl");
            }
        }


        private long? _kodpl;
        /// <summary>
        /// 309002100 Код плательщика (KODPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 309002100)]
        public long? Kodpl
        {
            get
            {
                CheckPropertyInited("Kodpl");
                return _kodpl;
            }
            set
            {
                _kodpl = value;
                NotifyPropertyChanged("Kodpl");
            }
        }


        private long? _ls;
        /// <summary>
        /// 309002200 Лицевой счет (LS)
        /// </summary>
        [RegisterAttribute(AttributeID = 309002200)]
        public long? Ls
        {
            get
            {
                CheckPropertyInited("Ls");
                return _ls;
            }
            set
            {
                _ls = value;
                NotifyPropertyChanged("Ls");
            }
        }


        private string _npol;
        /// <summary>
        /// 309002300 Уникальный номер страхового полиса/уникальный номер страхового свидетельства (NPOL)
        /// </summary>
        [RegisterAttribute(AttributeID = 309002300)]
        public string Npol
        {
            get
            {
                CheckPropertyInited("Npol");
                return _npol;
            }
            set
            {
                _npol = value;
                NotifyPropertyChanged("Npol");
            }
        }


        private DateTime? _dat;
        /// <summary>
        /// 309002400 Дата начала действия договора  (DAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 309002400)]
        public DateTime? Dat
        {
            get
            {
                CheckPropertyInited("Dat");
                return _dat;
            }
            set
            {
                _dat = value;
                NotifyPropertyChanged("Dat");
            }
        }


        private decimal? _vznos;
        /// <summary>
        /// 309002500 Страховой взнос за 1 кв. м. в месяц (VZNOS)
        /// </summary>
        [RegisterAttribute(AttributeID = 309002500)]
        public decimal? Vznos
        {
            get
            {
                CheckPropertyInited("Vznos");
                return _vznos;
            }
            set
            {
                _vznos = value;
                NotifyPropertyChanged("Vznos");
            }
        }


        private string _pralt;
        /// <summary>
        /// 309002600 Справочник условия страхования (PRALT)
        /// </summary>
        [RegisterAttribute(AttributeID = 309002600)]
        public string Pralt
        {
            get
            {
                CheckPropertyInited("Pralt");
                return _pralt;
            }
            set
            {
                _pralt = value;
                NotifyPropertyChanged("Pralt");
            }
        }


        private InsuranceTerms _pralt_Code;
        /// <summary>
        /// 309002600 Справочник условия страхования (справочный код) (PRALT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 309002600)]
        public InsuranceTerms Pralt_Code
        {
            get
            {
                CheckPropertyInited("Pralt_Code");
                return this._pralt_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_pralt))
                    {
                         _pralt = descr;
                    }
                }
                else
                {
                     _pralt = descr;
                }

                this._pralt_Code = value;
                NotifyPropertyChanged("Pralt");
                NotifyPropertyChanged("Pralt_Code");
            }
        }


        private long? _pr;
        /// <summary>
        /// 309002700 Признак договора (1– страховщик несет ответственность, 2 – страховщик не несет ответственности) (PR)
        /// </summary>
        [RegisterAttribute(AttributeID = 309002700)]
        public long? Pr
        {
            get
            {
                CheckPropertyInited("Pr");
                return _pr;
            }
            set
            {
                _pr = value;
                NotifyPropertyChanged("Pr");
            }
        }


        private decimal? _ss;
        /// <summary>
        /// 309002800 Страховая сумма (SS)
        /// </summary>
        [RegisterAttribute(AttributeID = 309002800)]
        public decimal? Ss
        {
            get
            {
                CheckPropertyInited("Ss");
                return _ss;
            }
            set
            {
                _ss = value;
                NotifyPropertyChanged("Ss");
            }
        }


        private long? _kolds;
        /// <summary>
        /// 309002900 Количество свидетельств в квартире (KOLDS)
        /// </summary>
        [RegisterAttribute(AttributeID = 309002900)]
        public long? Kolds
        {
            get
            {
                CheckPropertyInited("Kolds");
                return _kolds;
            }
            set
            {
                _kolds = value;
                NotifyPropertyChanged("Kolds");
            }
        }


        private decimal? _soplvz;
        /// <summary>
        /// 309003000 Сумма страхового взноса, уплаченного в отчетном месяце (SOPLVZ)
        /// </summary>
        [RegisterAttribute(AttributeID = 309003000)]
        public decimal? Soplvz
        {
            get
            {
                CheckPropertyInited("Soplvz");
                return _soplvz;
            }
            set
            {
                _soplvz = value;
                NotifyPropertyChanged("Soplvz");
            }
        }


        private DateTime? _datend;
        /// <summary>
        /// 309003100 Дата досрочного погашения договора (DAT_END)
        /// </summary>
        [RegisterAttribute(AttributeID = 309003100)]
        public DateTime? DatEnd
        {
            get
            {
                CheckPropertyInited("DatEnd");
                return _datend;
            }
            set
            {
                _datend = value;
                NotifyPropertyChanged("DatEnd");
            }
        }


        private string _typeprop;
        /// <summary>
        /// 309003200 Тип собственности (TYPE_PROP)
        /// </summary>
        [RegisterAttribute(AttributeID = 309003200)]
        public string TypeProp
        {
            get
            {
                CheckPropertyInited("TypeProp");
                return _typeprop;
            }
            set
            {
                _typeprop = value;
                NotifyPropertyChanged("TypeProp");
            }
        }


        private TypeProperty _typeprop_Code;
        /// <summary>
        /// 309003200 Тип собственности (справочный код) (TYPE_PROP_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 309003200)]
        public TypeProperty TypeProp_Code
        {
            get
            {
                CheckPropertyInited("TypeProp_Code");
                return this._typeprop_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typeprop))
                    {
                         _typeprop = descr;
                    }
                }
                else
                {
                     _typeprop = descr;
                }

                this._typeprop_Code = value;
                NotifyPropertyChanged("TypeProp");
                NotifyPropertyChanged("TypeProp_Code");
            }
        }


        private long? _inputuser;
        /// <summary>
        /// 309003300 Идентификатор пользователя, создавшего полис (INPUT_USER)
        /// </summary>
        [RegisterAttribute(AttributeID = 309003300)]
        public long? InputUser
        {
            get
            {
                CheckPropertyInited("InputUser");
                return _inputuser;
            }
            set
            {
                _inputuser = value;
                NotifyPropertyChanged("InputUser");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 310 Реестр договоров страхования общего имущества (INSUR_ALL_PROPERTY)
    /// </summary>
    [RegisterInfo(RegisterID = 310)]
    [Serializable]
    public sealed partial class OMAllProperty : OMBaseClass<OMAllProperty>
    {

        private long _empid;
        /// <summary>
        /// 310000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 310000100)]
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


        private long? _fspid;
        /// <summary>
        /// 310000200 Ссылка на ФСП INSUR_FSP_Q.EMP_ID (FSP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 310000200)]
        public long? FspId
        {
            get
            {
                CheckPropertyInited("FspId");
                return _fspid;
            }
            set
            {
                _fspid = value;
                NotifyPropertyChanged("FspId");
            }
        }


        private long? _insuranceid;
        /// <summary>
        /// 310000300 Код страховой организации – по справочнику «Страховые организации» (INSURANCE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 310000300)]
        public long? InsuranceId
        {
            get
            {
                CheckPropertyInited("InsuranceId");
                return _insuranceid;
            }
            set
            {
                _insuranceid = value;
                NotifyPropertyChanged("InsuranceId");
            }
        }


        private long? _okrugid;
        /// <summary>
        /// 310000400 Код административного округа  (OKRUG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 310000400)]
        public long? OkrugId
        {
            get
            {
                CheckPropertyInited("OkrugId");
                return _okrugid;
            }
            set
            {
                _okrugid = value;
                NotifyPropertyChanged("OkrugId");
            }
        }


        private long? _unom;
        /// <summary>
        /// 310000500 Уникальный номер строения  (UNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 310000500)]
        public long? Unom
        {
            get
            {
                CheckPropertyInited("Unom");
                return _unom;
            }
            set
            {
                _unom = value;
                NotifyPropertyChanged("Unom");
            }
        }


        private string _orgtype;
        /// <summary>
        /// 310000600 Код формы объединения собственников (6 – ЖСК, ЖК,  7 – ТСЖ, 8 – БО (без объединения собственников), 0 – нет данных) (ORG_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 310000600)]
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


        private FormAssociationOwners _orgtype_Code;
        /// <summary>
        /// 310000600 Код формы объединения собственников (6 – ЖСК, ЖК,  7 – ТСЖ, 8 – БО (без объединения собственников), 0 – нет данных) (справочный код) (ORG_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 310000600)]
        public FormAssociationOwners OrgType_Code
        {
            get
            {
                CheckPropertyInited("OrgType_Code");
                return this._orgtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_orgtype))
                    {
                         _orgtype = descr;
                    }
                }
                else
                {
                     _orgtype = descr;
                }

                this._orgtype_Code = value;
                NotifyPropertyChanged("OrgType");
                NotifyPropertyChanged("OrgType_Code");
            }
        }


        private long? _subjectid;
        /// <summary>
        /// 310000700 Код управляющей организации (по справочнику «Управляющие организации») (SUBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 310000700)]
        public long? SubjectId
        {
            get
            {
                CheckPropertyInited("SubjectId");
                return _subjectid;
            }
            set
            {
                _subjectid = value;
                NotifyPropertyChanged("SubjectId");
            }
        }


        private string _name;
        /// <summary>
        /// 310000800 Наименование страхователя (ЖСК, ЖК, ТСЖ, управляющей организации) (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 310000800)]
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


        private string _ndog;
        /// <summary>
        /// 310000900 Уникальный номер договора страхования (NDOG)
        /// </summary>
        [RegisterAttribute(AttributeID = 310000900)]
        public string Ndog
        {
            get
            {
                CheckPropertyInited("Ndog");
                return _ndog;
            }
            set
            {
                _ndog = value;
                NotifyPropertyChanged("Ndog");
            }
        }


        private DateTime? _ndogdat;
        /// <summary>
        /// 310001000 Дата начала действия договора страхования  (NDOGDAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 310001000)]
        public DateTime? Ndogdat
        {
            get
            {
                CheckPropertyInited("Ndogdat");
                return _ndogdat;
            }
            set
            {
                _ndogdat = value;
                NotifyPropertyChanged("Ndogdat");
            }
        }


        private decimal? _st1;
        /// <summary>
        /// 310001100 Страховая стоимость конструктивных элементов и помещений общего пользования (ST1)
        /// </summary>
        [RegisterAttribute(AttributeID = 310001100)]
        public decimal? St1
        {
            get
            {
                CheckPropertyInited("St1");
                return _st1;
            }
            set
            {
                _st1 = value;
                NotifyPropertyChanged("St1");
            }
        }


        private decimal? _st2;
        /// <summary>
        /// 310001200 Страховая стоимость внеквартирного инженерного оборудования (ST2)
        /// </summary>
        [RegisterAttribute(AttributeID = 310001200)]
        public decimal? St2
        {
            get
            {
                CheckPropertyInited("St2");
                return _st2;
            }
            set
            {
                _st2 = value;
                NotifyPropertyChanged("St2");
            }
        }


        private decimal? _st3;
        /// <summary>
        /// 310001300 Страховая стоимость лифтового оборудования (ST3)
        /// </summary>
        [RegisterAttribute(AttributeID = 310001300)]
        public decimal? St3
        {
            get
            {
                CheckPropertyInited("St3");
                return _st3;
            }
            set
            {
                _st3 = value;
                NotifyPropertyChanged("St3");
            }
        }


        private decimal? _ss1;
        /// <summary>
        /// 310001400 Страховая сумма конструктивных элементов и помещений общего пользования (SS1)
        /// </summary>
        [RegisterAttribute(AttributeID = 310001400)]
        public decimal? Ss1
        {
            get
            {
                CheckPropertyInited("Ss1");
                return _ss1;
            }
            set
            {
                _ss1 = value;
                NotifyPropertyChanged("Ss1");
            }
        }


        private decimal? _ss2;
        /// <summary>
        /// 310001500 Страховая сумма внеквартирного инженерного оборудования (SS2)
        /// </summary>
        [RegisterAttribute(AttributeID = 310001500)]
        public decimal? Ss2
        {
            get
            {
                CheckPropertyInited("Ss2");
                return _ss2;
            }
            set
            {
                _ss2 = value;
                NotifyPropertyChanged("Ss2");
            }
        }


        private decimal? _ss3;
        /// <summary>
        /// 310001600 Страховая сумма лифтового оборудования (SS3)
        /// </summary>
        [RegisterAttribute(AttributeID = 310001600)]
        public decimal? Ss3
        {
            get
            {
                CheckPropertyInited("Ss3");
                return _ss3;
            }
            set
            {
                _ss3 = value;
                NotifyPropertyChanged("Ss3");
            }
        }


        private decimal? _part;
        /// <summary>
        /// 310001700 Доля ответственности страховой организации в возмещении ущерба (PART)
        /// </summary>
        [RegisterAttribute(AttributeID = 310001700)]
        public decimal? Part
        {
            get
            {
                CheckPropertyInited("Part");
                return _part;
            }
            set
            {
                _part = value;
                NotifyPropertyChanged("Part");
            }
        }


        private decimal? _partcity;
        /// <summary>
        /// 310001800 Доля города Москвы в праве на общее имущество (PART_CITY)
        /// </summary>
        [RegisterAttribute(AttributeID = 310001800)]
        public decimal? PartCity
        {
            get
            {
                CheckPropertyInited("PartCity");
                return _partcity;
            }
            set
            {
                _partcity = value;
                NotifyPropertyChanged("PartCity");
            }
        }


        private string _paysign;
        /// <summary>
        /// 310001900 Признак рассрочки платежа (PAYSIGN)
        /// </summary>
        [RegisterAttribute(AttributeID = 310001900)]
        public string Paysign
        {
            get
            {
                CheckPropertyInited("Paysign");
                return _paysign;
            }
            set
            {
                _paysign = value;
                NotifyPropertyChanged("Paysign");
            }
        }


        private SignInstallmentPayment _paysign_Code;
        /// <summary>
        /// 310001900 Признак рассрочки платежа (справочный код) (PAYSIGN_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 310001900)]
        public SignInstallmentPayment Paysign_Code
        {
            get
            {
                CheckPropertyInited("Paysign_Code");
                return this._paysign_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_paysign))
                    {
                         _paysign = descr;
                    }
                }
                else
                {
                     _paysign = descr;
                }

                this._paysign_Code = value;
                NotifyPropertyChanged("Paysign");
                NotifyPropertyChanged("Paysign_Code");
            }
        }


        private decimal? _raspripay;
        /// <summary>
        /// 310002000 Рассчитанный размер страховой премии (RAS_PRIPAY)
        /// </summary>
        [RegisterAttribute(AttributeID = 310002000)]
        public decimal? RasPripay
        {
            get
            {
                CheckPropertyInited("RasPripay");
                return _raspripay;
            }
            set
            {
                _raspripay = value;
                NotifyPropertyChanged("RasPripay");
            }
        }


        private long? _linkidfile;
        /// <summary>
        /// 310002100 Ссылка на INSUR_INPUT_FILE (LINK_ID_FILE)
        /// </summary>
        [RegisterAttribute(AttributeID = 310002100)]
        public long? LinkIdFile
        {
            get
            {
                CheckPropertyInited("LinkIdFile");
                return _linkidfile;
            }
            set
            {
                _linkidfile = value;
                NotifyPropertyChanged("LinkIdFile");
            }
        }


        private long? _objid;
        /// <summary>
        /// 310002200 Идентификатор объекта (ссылка на INSUR_BUILDING) (OBJ_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 310002200)]
        public long? ObjId
        {
            get
            {
                CheckPropertyInited("ObjId");
                return _objid;
            }
            set
            {
                _objid = value;
                NotifyPropertyChanged("ObjId");
            }
        }


        private string _status;
        /// <summary>
        /// 310002300 Статус (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 310002300)]
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


        private ContractStatus _status_Code;
        /// <summary>
        /// 310002300 Статус (справочный код) (STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 310002300)]
        public ContractStatus Status_Code
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


        private long? _orgidfile;
        /// <summary>
        /// 310002400 ORG_ID_FILE (ORG_ID_FILE)
        /// </summary>
        [RegisterAttribute(AttributeID = 310002400)]
        public long? OrgIdFile
        {
            get
            {
                CheckPropertyInited("OrgIdFile");
                return _orgidfile;
            }
            set
            {
                _orgidfile = value;
                NotifyPropertyChanged("OrgIdFile");
            }
        }


        private long? _invoicecount;
        /// <summary>
        /// 310002500 Количество счетов по договору ()
        /// </summary>
        [RegisterAttribute(AttributeID = 310002500)]
        public long? InvoiceCount
        {
            get
            {
                CheckPropertyInited("InvoiceCount");
                return _invoicecount;
            }
            set
            {
                _invoicecount = value;
                NotifyPropertyChanged("InvoiceCount");
            }
        }


        private long? _platcount;
        /// <summary>
        /// 310002600 Количество платежей ()
        /// </summary>
        [RegisterAttribute(AttributeID = 310002600)]
        public long? PlatCount
        {
            get
            {
                CheckPropertyInited("PlatCount");
                return _platcount;
            }
            set
            {
                _platcount = value;
                NotifyPropertyChanged("PlatCount");
            }
        }


        private decimal? _partcityroubles;
        /// <summary>
        /// 310002700 Доля города, руб ()
        /// </summary>
        [RegisterAttribute(AttributeID = 310002700)]
        public decimal? PartCityRoubles
        {
            get
            {
                CheckPropertyInited("PartCityRoubles");
                return _partcityroubles;
            }
            set
            {
                _partcityroubles = value;
                NotifyPropertyChanged("PartCityRoubles");
            }
        }


        private string _inn;
        /// <summary>
        /// 310002800 ИНН (INN)
        /// </summary>
        [RegisterAttribute(AttributeID = 310002800)]
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

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 311 Реестр доп. соглашений по договорам общего имущества (INSUR_DOP_ALL_PROPERTY)
    /// </summary>
    [RegisterInfo(RegisterID = 311)]
    [Serializable]
    public sealed partial class OMDopAllProperty : OMBaseClass<OMDopAllProperty>
    {

        private long? _empid;
        /// <summary>
        /// 311000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 311000100)]
        public long? EmpId
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


        private long? _contractid;
        /// <summary>
        /// 311000200 Ссылка на INSUR_ALL_PROPERTY (CONTRACT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 311000200)]
        public long? ContractId
        {
            get
            {
                CheckPropertyInited("ContractId");
                return _contractid;
            }
            set
            {
                _contractid = value;
                NotifyPropertyChanged("ContractId");
            }
        }


        private long? _kod;
        /// <summary>
        /// 311000300 Код страховой организации (KOD)
        /// </summary>
        [RegisterAttribute(AttributeID = 311000300)]
        public long? Kod
        {
            get
            {
                CheckPropertyInited("Kod");
                return _kod;
            }
            set
            {
                _kod = value;
                NotifyPropertyChanged("Kod");
            }
        }


        private long? _unom;
        /// <summary>
        /// 311000400 Уникальный номер строения  (UNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 311000400)]
        public long? Unom
        {
            get
            {
                CheckPropertyInited("Unom");
                return _unom;
            }
            set
            {
                _unom = value;
                NotifyPropertyChanged("Unom");
            }
        }


        private string _ndog;
        /// <summary>
        /// 311000500 Уникальный номер договора страхования (NDOG)
        /// </summary>
        [RegisterAttribute(AttributeID = 311000500)]
        public string Ndog
        {
            get
            {
                CheckPropertyInited("Ndog");
                return _ndog;
            }
            set
            {
                _ndog = value;
                NotifyPropertyChanged("Ndog");
            }
        }


        private DateTime? _ndogdat;
        /// <summary>
        /// 311000600 Дата начала действия договора страхования  (NDOGDAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 311000600)]
        public DateTime? Ndogdat
        {
            get
            {
                CheckPropertyInited("Ndogdat");
                return _ndogdat;
            }
            set
            {
                _ndogdat = value;
                NotifyPropertyChanged("Ndogdat");
            }
        }


        private decimal? _ndops;
        /// <summary>
        /// 311000700 Номер дополнительного соглашения (NDOPS)
        /// </summary>
        [RegisterAttribute(AttributeID = 311000700)]
        public decimal? Ndops
        {
            get
            {
                CheckPropertyInited("Ndops");
                return _ndops;
            }
            set
            {
                _ndops = value;
                NotifyPropertyChanged("Ndops");
            }
        }


        private decimal? _st1new;
        /// <summary>
        /// 311000800 Страховая стоимость конструктивных элементов и помещений общего пользования (новое значение по доп соглашению) (ST1_NEW)
        /// </summary>
        [RegisterAttribute(AttributeID = 311000800)]
        public decimal? St1New
        {
            get
            {
                CheckPropertyInited("St1New");
                return _st1new;
            }
            set
            {
                _st1new = value;
                NotifyPropertyChanged("St1New");
            }
        }


        private decimal? _st2new;
        /// <summary>
        /// 311000900 Страховая стоимость внеквартирного инженерного оборудования (новое значение по доп соглашению) (ST2_NEW)
        /// </summary>
        [RegisterAttribute(AttributeID = 311000900)]
        public decimal? St2New
        {
            get
            {
                CheckPropertyInited("St2New");
                return _st2new;
            }
            set
            {
                _st2new = value;
                NotifyPropertyChanged("St2New");
            }
        }


        private decimal? _st3new;
        /// <summary>
        /// 311001000 Страховая стоимость лифтового оборудования(новое значение по доп соглашению) (ST3_NEW)
        /// </summary>
        [RegisterAttribute(AttributeID = 311001000)]
        public decimal? St3New
        {
            get
            {
                CheckPropertyInited("St3New");
                return _st3new;
            }
            set
            {
                _st3new = value;
                NotifyPropertyChanged("St3New");
            }
        }


        private decimal? _ss1new;
        /// <summary>
        /// 311001100 Страховая сумма конструктивных элементов и помещений общего пользования(новое значение по доп соглашению) (SS1_NEW)
        /// </summary>
        [RegisterAttribute(AttributeID = 311001100)]
        public decimal? Ss1New
        {
            get
            {
                CheckPropertyInited("Ss1New");
                return _ss1new;
            }
            set
            {
                _ss1new = value;
                NotifyPropertyChanged("Ss1New");
            }
        }


        private decimal? _ss2new;
        /// <summary>
        /// 311001200 Страховая сумма внеквартирного инженерного оборудования(новое значение по доп соглашению) (SS2_NEW)
        /// </summary>
        [RegisterAttribute(AttributeID = 311001200)]
        public decimal? Ss2New
        {
            get
            {
                CheckPropertyInited("Ss2New");
                return _ss2new;
            }
            set
            {
                _ss2new = value;
                NotifyPropertyChanged("Ss2New");
            }
        }


        private decimal? _ss3new;
        /// <summary>
        /// 311001300 Страховая сумма лифтового оборудования(новое значение по доп соглашению) (SS3_NEW)
        /// </summary>
        [RegisterAttribute(AttributeID = 311001300)]
        public decimal? Ss3New
        {
            get
            {
                CheckPropertyInited("Ss3New");
                return _ss3new;
            }
            set
            {
                _ss3new = value;
                NotifyPropertyChanged("Ss3New");
            }
        }


        private decimal? _partnew;
        /// <summary>
        /// 311001400 Доля ответственности страховой организации в возмещении ущерба(новое значение по доп соглашению) (PART_NEW)
        /// </summary>
        [RegisterAttribute(AttributeID = 311001400)]
        public decimal? PartNew
        {
            get
            {
                CheckPropertyInited("PartNew");
                return _partnew;
            }
            set
            {
                _partnew = value;
                NotifyPropertyChanged("PartNew");
            }
        }


        private decimal? _partcitynew;
        /// <summary>
        /// 311001500 Доля города Москвы в праве на общее имущество(новое значение по доп соглашению) (PART_CITY_NEW)
        /// </summary>
        [RegisterAttribute(AttributeID = 311001500)]
        public decimal? PartCityNew
        {
            get
            {
                CheckPropertyInited("PartCityNew");
                return _partcitynew;
            }
            set
            {
                _partcitynew = value;
                NotifyPropertyChanged("PartCityNew");
            }
        }


        private decimal? _paysignnew;
        /// <summary>
        /// 311001600 Признак рассрочки платежа (PAYSIGN_NEW)
        /// </summary>
        [RegisterAttribute(AttributeID = 311001600)]
        public decimal? PaysignNew
        {
            get
            {
                CheckPropertyInited("PaysignNew");
                return _paysignnew;
            }
            set
            {
                _paysignnew = value;
                NotifyPropertyChanged("PaysignNew");
            }
        }


        private decimal? _raspripaynew;
        /// <summary>
        /// 311001700 Рассчитанный размер страховой премии (RAS_PRIPAY_NEW)
        /// </summary>
        [RegisterAttribute(AttributeID = 311001700)]
        public decimal? RasPripayNew
        {
            get
            {
                CheckPropertyInited("RasPripayNew");
                return _raspripaynew;
            }
            set
            {
                _raspripaynew = value;
                NotifyPropertyChanged("RasPripayNew");
            }
        }


        private long? _linkidfile;
        /// <summary>
        /// 311001800 Ссылка на INSUR_INPUT_FILE (LINK_ID_FILE)
        /// </summary>
        [RegisterAttribute(AttributeID = 311001800)]
        public long? LinkIdFile
        {
            get
            {
                CheckPropertyInited("LinkIdFile");
                return _linkidfile;
            }
            set
            {
                _linkidfile = value;
                NotifyPropertyChanged("LinkIdFile");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 312 Реестр расчетов параметров объектов общего имущества (INSUR_PARAM_CALCULATION)
    /// </summary>
    [RegisterInfo(RegisterID = 312)]
    [Serializable]
    public sealed partial class OMParamCalculation : OMBaseClass<OMParamCalculation>
    {

        private long _empid;
        /// <summary>
        /// 312000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 312000100)]
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


        private long? _objid;
        /// <summary>
        /// 312000200 Идентификатор объекта (Ссылка на реестр зданий) (OBJ_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 312000200)]
        public long? ObjId
        {
            get
            {
                CheckPropertyInited("ObjId");
                return _objid;
            }
            set
            {
                _objid = value;
                NotifyPropertyChanged("ObjId");
            }
        }


        private long? _contractid;
        /// <summary>
        /// 312000300 Идентификатор договора , ссылка на INSUR_ALL_ PROPERTY (CONTRACT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 312000300)]
        public long? ContractId
        {
            get
            {
                CheckPropertyInited("ContractId");
                return _contractid;
            }
            set
            {
                _contractid = value;
                NotifyPropertyChanged("ContractId");
            }
        }


        private long? _insuranceid;
        /// <summary>
        /// 312000400 Идентификатор страховой организации – по справочнику «Страховые организации» (INSURANCE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 312000400)]
        public long? InsuranceId
        {
            get
            {
                CheckPropertyInited("InsuranceId");
                return _insuranceid;
            }
            set
            {
                _insuranceid = value;
                NotifyPropertyChanged("InsuranceId");
            }
        }


        private decimal? _partсоmpensation;
        /// <summary>
        /// 312000500 Доля ответственности страховой организации по возмещению вреда (PART_СОMPENSATION)
        /// </summary>
        [RegisterAttribute(AttributeID = 312000500)]
        public decimal? PartСоmpensation
        {
            get
            {
                CheckPropertyInited("PartСоmpensation");
                return _partсоmpensation;
            }
            set
            {
                _partсоmpensation = value;
                NotifyPropertyChanged("PartСоmpensation");
            }
        }


        private decimal? _actualcost;
        /// <summary>
        /// 312000600 Действительная стоимость дома, руб (ACTUAL_COST)
        /// </summary>
        [RegisterAttribute(AttributeID = 312000600)]
        public decimal? ActualCost
        {
            get
            {
                CheckPropertyInited("ActualCost");
                return _actualcost;
            }
            set
            {
                _actualcost = value;
                NotifyPropertyChanged("ActualCost");
            }
        }


        private decimal? _coefactualcost;
        /// <summary>
        /// 312000700 Коэффициент пересчета действительной стоимости (COEF_ACTUAL_COST)
        /// </summary>
        [RegisterAttribute(AttributeID = 312000700)]
        public decimal? CoefActualCost
        {
            get
            {
                CheckPropertyInited("CoefActualCost");
                return _coefactualcost;
            }
            set
            {
                _coefactualcost = value;
                NotifyPropertyChanged("CoefActualCost");
            }
        }


        private decimal? _actualcostcurrent;
        /// <summary>
        /// 312000800 Действительная стоимость дома (в пересчете на текущую цену), руб (ACTUAL_COST_CURRENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 312000800)]
        public decimal? ActualCostCurrent
        {
            get
            {
                CheckPropertyInited("ActualCostCurrent");
                return _actualcostcurrent;
            }
            set
            {
                _actualcostcurrent = value;
                NotifyPropertyChanged("ActualCostCurrent");
            }
        }


        private decimal? _indicatorr;
        /// <summary>
        /// 312000900 Показатель рациональности объемно-планировочного решения, R (INDICATOR_R)
        /// </summary>
        [RegisterAttribute(AttributeID = 312000900)]
        public decimal? IndicatorR
        {
            get
            {
                CheckPropertyInited("IndicatorR");
                return _indicatorr;
            }
            set
            {
                _indicatorr = value;
                NotifyPropertyChanged("IndicatorR");
            }
        }


        private decimal? _calculatedarea;
        /// <summary>
        /// 312001000 Расчетная площадь для определения стоимости общего имущества в МКД, кв.м. (CALCULATED_AREA)
        /// </summary>
        [RegisterAttribute(AttributeID = 312001000)]
        public decimal? CalculatedArea
        {
            get
            {
                CheckPropertyInited("CalculatedArea");
                return _calculatedarea;
            }
            set
            {
                _calculatedarea = value;
                NotifyPropertyChanged("CalculatedArea");
            }
        }


        private decimal? _ui1;
        /// <summary>
        /// 312001100 Земляные работы, фундамент  (UI_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 312001100)]
        public decimal? Ui1
        {
            get
            {
                CheckPropertyInited("Ui1");
                return _ui1;
            }
            set
            {
                _ui1 = value;
                NotifyPropertyChanged("Ui1");
            }
        }


        private decimal? _ui2;
        /// <summary>
        /// 312001200 Стены и перегородки (UI_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 312001200)]
        public decimal? Ui2
        {
            get
            {
                CheckPropertyInited("Ui2");
                return _ui2;
            }
            set
            {
                _ui2 = value;
                NotifyPropertyChanged("Ui2");
            }
        }


        private decimal? _ui3;
        /// <summary>
        /// 312001300 Перекрытия (UI_3)
        /// </summary>
        [RegisterAttribute(AttributeID = 312001300)]
        public decimal? Ui3
        {
            get
            {
                CheckPropertyInited("Ui3");
                return _ui3;
            }
            set
            {
                _ui3 = value;
                NotifyPropertyChanged("Ui3");
            }
        }


        private decimal? _ui4;
        /// <summary>
        /// 312001400 Полы (UI_4)
        /// </summary>
        [RegisterAttribute(AttributeID = 312001400)]
        public decimal? Ui4
        {
            get
            {
                CheckPropertyInited("Ui4");
                return _ui4;
            }
            set
            {
                _ui4 = value;
                NotifyPropertyChanged("Ui4");
            }
        }


        private decimal? _ui5;
        /// <summary>
        /// 312001500 Проемы (UI_5)
        /// </summary>
        [RegisterAttribute(AttributeID = 312001500)]
        public decimal? Ui5
        {
            get
            {
                CheckPropertyInited("Ui5");
                return _ui5;
            }
            set
            {
                _ui5 = value;
                NotifyPropertyChanged("Ui5");
            }
        }


        private decimal? _ui6;
        /// <summary>
        /// 312001600 Отделочные работы (UI_6)
        /// </summary>
        [RegisterAttribute(AttributeID = 312001600)]
        public decimal? Ui6
        {
            get
            {
                CheckPropertyInited("Ui6");
                return _ui6;
            }
            set
            {
                _ui6 = value;
                NotifyPropertyChanged("Ui6");
            }
        }


        private decimal? _ui7;
        /// <summary>
        /// 312001700 Прочие (UI_7)
        /// </summary>
        [RegisterAttribute(AttributeID = 312001700)]
        public decimal? Ui7
        {
            get
            {
                CheckPropertyInited("Ui7");
                return _ui7;
            }
            set
            {
                _ui7 = value;
                NotifyPropertyChanged("Ui7");
            }
        }


        private decimal? _ui8;
        /// <summary>
        /// 312001800 Крыша (UI_8)
        /// </summary>
        [RegisterAttribute(AttributeID = 312001800)]
        public decimal? Ui8
        {
            get
            {
                CheckPropertyInited("Ui8");
                return _ui8;
            }
            set
            {
                _ui8 = value;
                NotifyPropertyChanged("Ui8");
            }
        }


        private decimal? _ui9;
        /// <summary>
        /// 312001900 Санитарно-технические работы и внутридомовое инженерное оборудование (исключая лифты) (UI_9)
        /// </summary>
        [RegisterAttribute(AttributeID = 312001900)]
        public decimal? Ui9
        {
            get
            {
                CheckPropertyInited("Ui9");
                return _ui9;
            }
            set
            {
                _ui9 = value;
                NotifyPropertyChanged("Ui9");
            }
        }


        private decimal? _ui10;
        /// <summary>
        /// 312002000 Лифты и лифтовое оборудование (UI_10)
        /// </summary>
        [RegisterAttribute(AttributeID = 312002000)]
        public decimal? Ui10
        {
            get
            {
                CheckPropertyInited("Ui10");
                return _ui10;
            }
            set
            {
                _ui10 = value;
                NotifyPropertyChanged("Ui10");
            }
        }


        private decimal? _ui11;
        /// <summary>
        /// 312002100 Всего (UI_11)
        /// </summary>
        [RegisterAttribute(AttributeID = 312002100)]
        public decimal? Ui11
        {
            get
            {
                CheckPropertyInited("Ui11");
                return _ui11;
            }
            set
            {
                _ui11 = value;
                NotifyPropertyChanged("Ui11");
            }
        }


        private decimal? _totalcost1;
        /// <summary>
        /// 312002200 Общая стоимость конструкций без санитарно-технических работ и внутридомового инженерного оборудования (TOTAL_COST_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 312002200)]
        public decimal? TotalCost1
        {
            get
            {
                CheckPropertyInited("TotalCost1");
                return _totalcost1;
            }
            set
            {
                _totalcost1 = value;
                NotifyPropertyChanged("TotalCost1");
            }
        }


        private decimal? _designcost1;
        /// <summary>
        /// 312002300 Сумма конструкций без санитарно-технических работ и внутридомового инженерного оборудования (DESIGN_COST_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 312002300)]
        public decimal? DesignCost1
        {
            get
            {
                CheckPropertyInited("DesignCost1");
                return _designcost1;
            }
            set
            {
                _designcost1 = value;
                NotifyPropertyChanged("DesignCost1");
            }
        }


        private decimal? _basicrate1;
        /// <summary>
        /// 312002400 Базовый тариф  (BASIC_RATE_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 312002400)]
        public decimal? BasicRate1
        {
            get
            {
                CheckPropertyInited("BasicRate1");
                return _basicrate1;
            }
            set
            {
                _basicrate1 = value;
                NotifyPropertyChanged("BasicRate1");
            }
        }


        private decimal? _annualbonus1;
        /// <summary>
        /// 312002500 Годовая премия  (ANNUAL_BONUS_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 312002500)]
        public decimal? AnnualBonus1
        {
            get
            {
                CheckPropertyInited("AnnualBonus1");
                return _annualbonus1;
            }
            set
            {
                _annualbonus1 = value;
                NotifyPropertyChanged("AnnualBonus1");
            }
        }


        private decimal? _totalcost2;
        /// <summary>
        /// 312002600 Общая стоимость конструкций по санитарно-техническим работам и внутридомовому инженерному оборудованию (TOTAL_COST_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 312002600)]
        public decimal? TotalCost2
        {
            get
            {
                CheckPropertyInited("TotalCost2");
                return _totalcost2;
            }
            set
            {
                _totalcost2 = value;
                NotifyPropertyChanged("TotalCost2");
            }
        }


        private decimal? _designcost2;
        /// <summary>
        /// 312002700 Сумма конструкций по санитарно-техническим работам и внутридомовому инженерному оборудованию (DESIGN_COST_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 312002700)]
        public decimal? DesignCost2
        {
            get
            {
                CheckPropertyInited("DesignCost2");
                return _designcost2;
            }
            set
            {
                _designcost2 = value;
                NotifyPropertyChanged("DesignCost2");
            }
        }


        private decimal? _basicrate2;
        /// <summary>
        /// 312002800 Базовый тариф (BASIC_RATE_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 312002800)]
        public decimal? BasicRate2
        {
            get
            {
                CheckPropertyInited("BasicRate2");
                return _basicrate2;
            }
            set
            {
                _basicrate2 = value;
                NotifyPropertyChanged("BasicRate2");
            }
        }


        private decimal? _annualbonus2;
        /// <summary>
        /// 312002900 Годовая премия (ANNUAL_BONUS_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 312002900)]
        public decimal? AnnualBonus2
        {
            get
            {
                CheckPropertyInited("AnnualBonus2");
                return _annualbonus2;
            }
            set
            {
                _annualbonus2 = value;
                NotifyPropertyChanged("AnnualBonus2");
            }
        }


        private decimal? _totalcost3;
        /// <summary>
        /// 312003000 Общая стоимость конструкций лифтов и лифтового оборудования (TOTAL_COST_3)
        /// </summary>
        [RegisterAttribute(AttributeID = 312003000)]
        public decimal? TotalCost3
        {
            get
            {
                CheckPropertyInited("TotalCost3");
                return _totalcost3;
            }
            set
            {
                _totalcost3 = value;
                NotifyPropertyChanged("TotalCost3");
            }
        }


        private decimal? _designcost3;
        /// <summary>
        /// 312003100 Сумма конструкций лифтов и лифтового оборудования (DESIGN_COST_3)
        /// </summary>
        [RegisterAttribute(AttributeID = 312003100)]
        public decimal? DesignCost3
        {
            get
            {
                CheckPropertyInited("DesignCost3");
                return _designcost3;
            }
            set
            {
                _designcost3 = value;
                NotifyPropertyChanged("DesignCost3");
            }
        }


        private decimal? _basicrate3;
        /// <summary>
        /// 312003200 Базовый тариф (BASIC_RATE_3)
        /// </summary>
        [RegisterAttribute(AttributeID = 312003200)]
        public decimal? BasicRate3
        {
            get
            {
                CheckPropertyInited("BasicRate3");
                return _basicrate3;
            }
            set
            {
                _basicrate3 = value;
                NotifyPropertyChanged("BasicRate3");
            }
        }


        private decimal? _annualbonus3;
        /// <summary>
        /// 312003300 Годовая премия (ANNUAL_BONUS_3)
        /// </summary>
        [RegisterAttribute(AttributeID = 312003300)]
        public decimal? AnnualBonus3
        {
            get
            {
                CheckPropertyInited("AnnualBonus3");
                return _annualbonus3;
            }
            set
            {
                _annualbonus3 = value;
                NotifyPropertyChanged("AnnualBonus3");
            }
        }


        private decimal? _sizeannualbonus;
        /// <summary>
        /// 312003400 Размер годовой премии по дому (SIZE_ANNUAL_BONUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 312003400)]
        public decimal? SizeAnnualBonus
        {
            get
            {
                CheckPropertyInited("SizeAnnualBonus");
                return _sizeannualbonus;
            }
            set
            {
                _sizeannualbonus = value;
                NotifyPropertyChanged("SizeAnnualBonus");
            }
        }


        private string _requestnumber;
        /// <summary>
        /// 312003500 Номер заявки (REQUEST_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 312003500)]
        public string RequestNumber
        {
            get
            {
                CheckPropertyInited("RequestNumber");
                return _requestnumber;
            }
            set
            {
                _requestnumber = value;
                NotifyPropertyChanged("RequestNumber");
            }
        }


        private DateTime? _requestdate;
        /// <summary>
        /// 312003600 Дата заявки (REQUEST_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 312003600)]
        public DateTime? RequestDate
        {
            get
            {
                CheckPropertyInited("RequestDate");
                return _requestdate;
            }
            set
            {
                _requestdate = value;
                NotifyPropertyChanged("RequestDate");
            }
        }


        private string _note;
        /// <summary>
        /// 312003700 Примечание (NOTE)
        /// </summary>
        [RegisterAttribute(AttributeID = 312003700)]
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


        private DateTime? _createddate;
        /// <summary>
        /// 312003800 Дата создания (CREATED_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 312003800)]
        public DateTime? CreatedDate
        {
            get
            {
                CheckPropertyInited("CreatedDate");
                return _createddate;
            }
            set
            {
                _createddate = value;
                NotifyPropertyChanged("CreatedDate");
            }
        }


        private DateTime? _approvaldate;
        /// <summary>
        /// 312003900 Дата согласования (APPROVAL_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 312003900)]
        public DateTime? ApprovalDate
        {
            get
            {
                CheckPropertyInited("ApprovalDate");
                return _approvaldate;
            }
            set
            {
                _approvaldate = value;
                NotifyPropertyChanged("ApprovalDate");
            }
        }


        private long? _createduserid;
        /// <summary>
        /// 312004000 Пользователь, который создал расчет (CREATED_USER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 312004000)]
        public long? CreatedUserId
        {
            get
            {
                CheckPropertyInited("CreatedUserId");
                return _createduserid;
            }
            set
            {
                _createduserid = value;
                NotifyPropertyChanged("CreatedUserId");
            }
        }


        private long? _approvaluserid;
        /// <summary>
        /// 312004100 Пользователь, который согласовал расчет (APPROVAL_USER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 312004100)]
        public long? ApprovalUserId
        {
            get
            {
                CheckPropertyInited("ApprovalUserId");
                return _approvaluserid;
            }
            set
            {
                _approvaluserid = value;
                NotifyPropertyChanged("ApprovalUserId");
            }
        }


        private string _status;
        /// <summary>
        /// 312004200 Статус расчета (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 312004200)]
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


        private CalculationStatus _status_Code;
        /// <summary>
        /// 312004200 Статус расчета (справочный код) (STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 312004200)]
        public CalculationStatus Status_Code
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


        private bool? _deleted;
        /// <summary>
        /// 312004300 Удалено, да/нет (1/0) (DELETED)
        /// </summary>
        [RegisterAttribute(AttributeID = 312004300)]
        public bool? Deleted
        {
            get
            {
                CheckPropertyInited("Deleted");
                return _deleted;
            }
            set
            {
                _deleted = value;
                NotifyPropertyChanged("Deleted");
            }
        }


        private long? _subjectid;
        /// <summary>
        /// 312004400 Страхователь (SUBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 312004400)]
        public long? SubjectId
        {
            get
            {
                CheckPropertyInited("SubjectId");
                return _subjectid;
            }
            set
            {
                _subjectid = value;
                NotifyPropertyChanged("SubjectId");
            }
        }


        private DateTime? _datefill1;
        /// <summary>
        /// 312004500 Дата заполнения 1 (пользователь создал расчет) (DATE_FILL_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 312004500)]
        public DateTime? DateFill1
        {
            get
            {
                CheckPropertyInited("DateFill1");
                return _datefill1;
            }
            set
            {
                _datefill1 = value;
                NotifyPropertyChanged("DateFill1");
            }
        }


        private long? _agreementid1;
        /// <summary>
        /// 312004600 Пользователь, создавший расчет (AGREEMENT_ID_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 312004600)]
        public long? AgreementId1
        {
            get
            {
                CheckPropertyInited("AgreementId1");
                return _agreementid1;
            }
            set
            {
                _agreementid1 = value;
                NotifyPropertyChanged("AgreementId1");
            }
        }


        private DateTime? _datefill2;
        /// <summary>
        /// 312004700 Дата заполнения 2 (пользователь согласовал расчет) (DATE_FILL_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 312004700)]
        public DateTime? DateFill2
        {
            get
            {
                CheckPropertyInited("DateFill2");
                return _datefill2;
            }
            set
            {
                _datefill2 = value;
                NotifyPropertyChanged("DateFill2");
            }
        }


        private long? _agreementid2;
        /// <summary>
        /// 312004800 Пользователь, согласовавший расчет (AGREEMENT_ID_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 312004800)]
        public long? AgreementId2
        {
            get
            {
                CheckPropertyInited("AgreementId2");
                return _agreementid2;
            }
            set
            {
                _agreementid2 = value;
                NotifyPropertyChanged("AgreementId2");
            }
        }


        private string _packagenum;
        /// <summary>
        /// 312004900 Номер пакета (PACKAGE_NUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 312004900)]
        public string PackageNum
        {
            get
            {
                CheckPropertyInited("PackageNum");
                return _packagenum;
            }
            set
            {
                _packagenum = value;
                NotifyPropertyChanged("PackageNum");
            }
        }


        private bool? _flagokrugl;
        /// <summary>
        /// 312005000 Округлять / Не округлять (FLAG_OKRUGL)
        /// </summary>
        [RegisterAttribute(AttributeID = 312005000)]
        public bool? FlagOkrugl
        {
            get
            {
                CheckPropertyInited("FlagOkrugl");
                return _flagokrugl;
            }
            set
            {
                _flagokrugl = value;
                NotifyPropertyChanged("FlagOkrugl");
            }
        }


        private decimal? _allarea;
        /// <summary>
        /// 312005100 Общая площадь по зданию (ALL_AREA)
        /// </summary>
        [RegisterAttribute(AttributeID = 312005100)]
        public decimal? AllArea
        {
            get
            {
                CheckPropertyInited("AllArea");
                return _allarea;
            }
            set
            {
                _allarea = value;
                NotifyPropertyChanged("AllArea");
            }
        }


        private bool? _allareaincludehpl;
        /// <summary>
        /// 312005400 Признак в расчете общей площади участвует площадь холодных помещений (ALL_AREA_INCLUDE_HPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 312005400)]
        public bool? AllAreaIncludeHpl
        {
            get
            {
                CheckPropertyInited("AllAreaIncludeHpl");
                return _allareaincludehpl;
            }
            set
            {
                _allareaincludehpl = value;
                NotifyPropertyChanged("AllAreaIncludeHpl");
            }
        }


        private bool? _calculatedareaincludehpl;
        /// <summary>
        /// 312005500 Признак в расчете расчетной площади участвует площадь холодных помещений (CALCULATED_AREA_INCLUDE_HPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 312005500)]
        public bool? CalculatedAreaIncludeHpl
        {
            get
            {
                CheckPropertyInited("CalculatedAreaIncludeHpl");
                return _calculatedareaincludehpl;
            }
            set
            {
                _calculatedareaincludehpl = value;
                NotifyPropertyChanged("CalculatedAreaIncludeHpl");
            }
        }


        private long? _okrugid;
        /// <summary>
        /// 312005600 Ссылка на округ (OKRUG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 312005600)]
        public long? OkrugId
        {
            get
            {
                CheckPropertyInited("OkrugId");
                return _okrugid;
            }
            set
            {
                _okrugid = value;
                NotifyPropertyChanged("OkrugId");
            }
        }


        private DateTime? _estimatedinsurancestartdate;
        /// <summary>
        /// 312005700 Предполагаемая дата начала действия страхования (ESTIMATED_INSURANCE_START_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 312005700)]
        public DateTime? EstimatedInsuranceStartDate
        {
            get
            {
                CheckPropertyInited("EstimatedInsuranceStartDate");
                return _estimatedinsurancestartdate;
            }
            set
            {
                _estimatedinsurancestartdate = value;
                NotifyPropertyChanged("EstimatedInsuranceStartDate");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 313 Реестр дел по расчету  суммы ущерба (INSUR_DAMAGE)
    /// </summary>
    [RegisterInfo(RegisterID = 313)]
    [Serializable]
    public sealed partial class OMDamage : OMBaseClass<OMDamage>
    {

        private long _empid;
        /// <summary>
        /// 313000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 313000100)]
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


        private long? _objid;
        /// <summary>
        /// 313000200 Идентификатор объекта (Ссылка на реестр зданий) (OBJ_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 313000200)]
        public long? ObjId
        {
            get
            {
                CheckPropertyInited("ObjId");
                return _objid;
            }
            set
            {
                _objid = value;
                NotifyPropertyChanged("ObjId");
            }
        }


        private long? _insurorgid;
        /// <summary>
        /// 313000400 Ссылка на справочник "Страховые компании" (INSUR_ORG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 313000400)]
        public long? InsurOrgId
        {
            get
            {
                CheckPropertyInited("InsurOrgId");
                return _insurorgid;
            }
            set
            {
                _insurorgid = value;
                NotifyPropertyChanged("InsurOrgId");
            }
        }


        private DateTime? _insurdate;
        /// <summary>
        /// 313000600 Исходящая дата дела от СК (INSUR_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313000600)]
        public DateTime? InsurDate
        {
            get
            {
                CheckPropertyInited("InsurDate");
                return _insurdate;
            }
            set
            {
                _insurdate = value;
                NotifyPropertyChanged("InsurDate");
            }
        }


        private string _insurnom;
        /// <summary>
        /// 313000700 Исходящий номер дела от СК (INSUR_NOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 313000700)]
        public string InsurNom
        {
            get
            {
                CheckPropertyInited("InsurNom");
                return _insurnom;
            }
            set
            {
                _insurnom = value;
                NotifyPropertyChanged("InsurNom");
            }
        }


        private DateTime? _datedoclastgby;
        /// <summary>
        /// 313000800 Дата поступления последнего документа в ЦИПиЖС (DATE_DOC_LAST_GBY)
        /// </summary>
        [RegisterAttribute(AttributeID = 313000800)]
        public DateTime? DateDocLastGBY
        {
            get
            {
                CheckPropertyInited("DateDocLastGBY");
                return _datedoclastgby;
            }
            set
            {
                _datedoclastgby = value;
                NotifyPropertyChanged("DateDocLastGBY");
            }
        }


        private DateTime? _datedocaddgby;
        /// <summary>
        /// 313000900 Дата досылки документов в ЦИПиЖС (DATE_DOC_ADD_GBY)
        /// </summary>
        [RegisterAttribute(AttributeID = 313000900)]
        public DateTime? DateDocAddGBY
        {
            get
            {
                CheckPropertyInited("DateDocAddGBY");
                return _datedocaddgby;
            }
            set
            {
                _datedocaddgby = value;
                NotifyPropertyChanged("DateDocAddGBY");
            }
        }


        private string _note;
        /// <summary>
        /// 313001000 Примечание (NOTE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313001000)]
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


        private DateTime? _damagedata;
        /// <summary>
        /// 313001100 Дата ущерба (DAMAGE_DATA)
        /// </summary>
        [RegisterAttribute(AttributeID = 313001100)]
        public DateTime? DamageData
        {
            get
            {
                CheckPropertyInited("DamageData");
                return _damagedata;
            }
            set
            {
                _damagedata = value;
                NotifyPropertyChanged("DamageData");
            }
        }


        private string _damagereasongp;
        /// <summary>
        /// 313001300 Причина ущерба для ЖП (на основании справочника «Причина ущерба для ЖП») (DAMAGE_REASON_GP)
        /// </summary>
        [RegisterAttribute(AttributeID = 313001300)]
        public string DamageReasonGP
        {
            get
            {
                CheckPropertyInited("DamageReasonGP");
                return _damagereasongp;
            }
            set
            {
                _damagereasongp = value;
                NotifyPropertyChanged("DamageReasonGP");
            }
        }


        private CausesOfDamageGP _damagereasongp_Code;
        /// <summary>
        /// 313001300 Причина ущерба для ЖП (на основании справочника «Причина ущерба для ЖП») (справочный код) (DAMAGE_REASON_GP_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313001300)]
        public CausesOfDamageGP DamageReasonGP_Code
        {
            get
            {
                CheckPropertyInited("DamageReasonGP_Code");
                return this._damagereasongp_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_damagereasongp))
                    {
                         _damagereasongp = descr;
                    }
                }
                else
                {
                     _damagereasongp = descr;
                }

                this._damagereasongp_Code = value;
                NotifyPropertyChanged("DamageReasonGP");
                NotifyPropertyChanged("DamageReasonGP_Code");
            }
        }


        private decimal? _estimatedvalue;
        /// <summary>
        /// 313001600 Расчетная стоимость (ESTIMATED_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313001600)]
        public decimal? EstimatedValue
        {
            get
            {
                CheckPropertyInited("EstimatedValue");
                return _estimatedvalue;
            }
            set
            {
                _estimatedvalue = value;
                NotifyPropertyChanged("EstimatedValue");
            }
        }


        private decimal? _insursum;
        /// <summary>
        /// 313001700 Страховая сумма (INSUR_SUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 313001700)]
        public decimal? InsurSum
        {
            get
            {
                CheckPropertyInited("InsurSum");
                return _insursum;
            }
            set
            {
                _insursum = value;
                NotifyPropertyChanged("InsurSum");
            }
        }


        private decimal? _parttown;
        /// <summary>
        /// 313001800 Доля ответсвенности города (PART_TOWN)
        /// </summary>
        [RegisterAttribute(AttributeID = 313001800)]
        public decimal? PartTown
        {
            get
            {
                CheckPropertyInited("PartTown");
                return _parttown;
            }
            set
            {
                _parttown = value;
                NotifyPropertyChanged("PartTown");
            }
        }


        private decimal? _strahplat;
        /// <summary>
        /// 313001900 Выплата СК (STRAH_PLAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 313001900)]
        public decimal? StrahPlat
        {
            get
            {
                CheckPropertyInited("StrahPlat");
                return _strahplat;
            }
            set
            {
                _strahplat = value;
                NotifyPropertyChanged("StrahPlat");
            }
        }


        private decimal? _partinsur;
        /// <summary>
        /// 313002000 Доля ответственности СК (PART_INSUR)
        /// </summary>
        [RegisterAttribute(AttributeID = 313002000)]
        public decimal? PartInsur
        {
            get
            {
                CheckPropertyInited("PartInsur");
                return _partinsur;
            }
            set
            {
                _partinsur = value;
                NotifyPropertyChanged("PartInsur");
            }
        }


        private string _typebuild;
        /// <summary>
        /// 313002100 Тип здания (выбор из справочника) (TYPE_BUILD)
        /// </summary>
        [RegisterAttribute(AttributeID = 313002100)]
        public string TypeBuild
        {
            get
            {
                CheckPropertyInited("TypeBuild");
                return _typebuild;
            }
            set
            {
                _typebuild = value;
                NotifyPropertyChanged("TypeBuild");
            }
        }


        private BuildingType _typebuild_Code;
        /// <summary>
        /// 313002100 Тип здания (выбор из справочника) (справочный код) (TYPE_BUILD_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313002100)]
        public BuildingType TypeBuild_Code
        {
            get
            {
                CheckPropertyInited("TypeBuild_Code");
                return this._typebuild_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typebuild))
                    {
                         _typebuild = descr;
                    }
                }
                else
                {
                     _typebuild = descr;
                }

                this._typebuild_Code = value;
                NotifyPropertyChanged("TypeBuild");
                NotifyPropertyChanged("TypeBuild_Code");
            }
        }


        private string _typecooker;
        /// <summary>
        /// 313002200 Плита (выбор из справочника) (TYPE_COOKER)
        /// </summary>
        [RegisterAttribute(AttributeID = 313002200)]
        public string TypeCooker
        {
            get
            {
                CheckPropertyInited("TypeCooker");
                return _typecooker;
            }
            set
            {
                _typecooker = value;
                NotifyPropertyChanged("TypeCooker");
            }
        }


        private StoveType _typecooker_Code;
        /// <summary>
        /// 313002200 Плита (выбор из справочника) (справочный код) (TYPE_COOKER_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313002200)]
        public StoveType TypeCooker_Code
        {
            get
            {
                CheckPropertyInited("TypeCooker_Code");
                return this._typecooker_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typecooker))
                    {
                         _typecooker = descr;
                    }
                }
                else
                {
                     _typecooker = descr;
                }

                this._typecooker_Code = value;
                NotifyPropertyChanged("TypeCooker");
                NotifyPropertyChanged("TypeCooker_Code");
            }
        }


        private string _typefloor;
        /// <summary>
        /// 313002300 Материал пола (выбор из справочника) (TYPE_FLOOR)
        /// </summary>
        [RegisterAttribute(AttributeID = 313002300)]
        public string TypeFloor
        {
            get
            {
                CheckPropertyInited("TypeFloor");
                return _typefloor;
            }
            set
            {
                _typefloor = value;
                NotifyPropertyChanged("TypeFloor");
            }
        }


        private FloorMaterial _typefloor_Code;
        /// <summary>
        /// 313002300 Материал пола (выбор из справочника) (справочный код) (TYPE_FLOOR_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313002300)]
        public FloorMaterial TypeFloor_Code
        {
            get
            {
                CheckPropertyInited("TypeFloor_Code");
                return this._typefloor_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typefloor))
                    {
                         _typefloor = descr;
                    }
                }
                else
                {
                     _typefloor = descr;
                }

                this._typefloor_Code = value;
                NotifyPropertyChanged("TypeFloor");
                NotifyPropertyChanged("TypeFloor_Code");
            }
        }


        private decimal? _sumdamage;
        /// <summary>
        /// 313002400 Сумма ущерба (SUM_DAMAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313002400)]
        public decimal? SumDamage
        {
            get
            {
                CheckPropertyInited("SumDamage");
                return _sumdamage;
            }
            set
            {
                _sumdamage = value;
                NotifyPropertyChanged("SumDamage");
            }
        }


        private decimal? _subsidy;
        /// <summary>
        /// 313002500 Размер субсидии (SUBSIDY)
        /// </summary>
        [RegisterAttribute(AttributeID = 313002500)]
        public decimal? Subsidy
        {
            get
            {
                CheckPropertyInited("Subsidy");
                return _subsidy;
            }
            set
            {
                _subsidy = value;
                NotifyPropertyChanged("Subsidy");
            }
        }


        private long? _agreementid1;
        /// <summary>
        /// 313002600 Согласующий 1, кто производит расчет (Идентификатор) (AGREEMENT_ID_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 313002600)]
        public long? AgreementId1
        {
            get
            {
                CheckPropertyInited("AgreementId1");
                return _agreementid1;
            }
            set
            {
                _agreementid1 = value;
                NotifyPropertyChanged("AgreementId1");
            }
        }


        private long? _agreementid2;
        /// <summary>
        /// 313002800 Согласующий 2, кто проверяет (Идентификатор) (AGREEMENT_ID_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 313002800)]
        public long? AgreementId2
        {
            get
            {
                CheckPropertyInited("AgreementId2");
                return _agreementid2;
            }
            set
            {
                _agreementid2 = value;
                NotifyPropertyChanged("AgreementId2");
            }
        }


        private DateTime? _dateinput;
        /// <summary>
        /// 313003000 Дата (DATE_INPUT)
        /// </summary>
        [RegisterAttribute(AttributeID = 313003000)]
        public DateTime? DateInput
        {
            get
            {
                CheckPropertyInited("DateInput");
                return _dateinput;
            }
            set
            {
                _dateinput = value;
                NotifyPropertyChanged("DateInput");
            }
        }


        private string _damagereasonoi;
        /// <summary>
        /// 313003100 Причина ущерба для ОИ (на основании справочника «Причина ущерба для ОИ») (DAMAGE_REASON_OI)
        /// </summary>
        [RegisterAttribute(AttributeID = 313003100)]
        public string DamageReasonOI
        {
            get
            {
                CheckPropertyInited("DamageReasonOI");
                return _damagereasonoi;
            }
            set
            {
                _damagereasonoi = value;
                NotifyPropertyChanged("DamageReasonOI");
            }
        }


        private CausesOfDamageOI _damagereasonoi_Code;
        /// <summary>
        /// 313003100 Причина ущерба для ОИ (на основании справочника «Причина ущерба для ОИ») (справочный код) (DAMAGE_REASON_OI_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313003100)]
        public CausesOfDamageOI DamageReasonOI_Code
        {
            get
            {
                CheckPropertyInited("DamageReasonOI_Code");
                return this._damagereasonoi_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_damagereasonoi))
                    {
                         _damagereasonoi = descr;
                    }
                }
                else
                {
                     _damagereasonoi = descr;
                }

                this._damagereasonoi_Code = value;
                NotifyPropertyChanged("DamageReasonOI");
                NotifyPropertyChanged("DamageReasonOI_Code");
            }
        }


        private decimal? _calculdamage;
        /// <summary>
        /// 313003200 Сумма ущерба, расчетная в Системе (CALCUL_DAMAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313003200)]
        public decimal? CalculDamage
        {
            get
            {
                CheckPropertyInited("CalculDamage");
                return _calculdamage;
            }
            set
            {
                _calculdamage = value;
                NotifyPropertyChanged("CalculDamage");
            }
        }


        private string _typedoc;
        /// <summary>
        /// 313003300 Тип договора (TYPE_DOC)
        /// </summary>
        [RegisterAttribute(AttributeID = 313003300)]
        public string TypeDoc
        {
            get
            {
                CheckPropertyInited("TypeDoc");
                return _typedoc;
            }
            set
            {
                _typedoc = value;
                NotifyPropertyChanged("TypeDoc");
            }
        }


        private ContractType _typedoc_Code;
        /// <summary>
        /// 313003300 Тип договора (справочный код) (TYPE_DOC_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313003300)]
        public ContractType TypeDoc_Code
        {
            get
            {
                CheckPropertyInited("TypeDoc_Code");
                return this._typedoc_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typedoc))
                    {
                         _typedoc = descr;
                    }
                }
                else
                {
                     _typedoc = descr;
                }

                this._typedoc_Code = value;
                NotifyPropertyChanged("TypeDoc");
                NotifyPropertyChanged("TypeDoc_Code");
            }
        }


        private DateTime? _datefill1;
        /// <summary>
        /// 313003600 Дата заполнения_1 (DATE_FILL_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 313003600)]
        public DateTime? DateFill1
        {
            get
            {
                CheckPropertyInited("DateFill1");
                return _datefill1;
            }
            set
            {
                _datefill1 = value;
                NotifyPropertyChanged("DateFill1");
            }
        }


        private DateTime? _datefill2;
        /// <summary>
        /// 313003700 Дата заполнения_2 (DATE_FILL_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 313003700)]
        public DateTime? DateFill2
        {
            get
            {
                CheckPropertyInited("DateFill2");
                return _datefill2;
            }
            set
            {
                _datefill2 = value;
                NotifyPropertyChanged("DateFill2");
            }
        }


        private long? _mainagreementid;
        /// <summary>
        /// 313005000 Основной согласующий (Идентификатор) (MAIN_AGREEMENT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 313005000)]
        public long? MainAgreementId
        {
            get
            {
                CheckPropertyInited("MainAgreementId");
                return _mainagreementid;
            }
            set
            {
                _mainagreementid = value;
                NotifyPropertyChanged("MainAgreementId");
            }
        }


        private DateTime? _datefillmain;
        /// <summary>
        /// 313005100 Дата заполнения_3 (DATE_FILL_MAIN)
        /// </summary>
        [RegisterAttribute(AttributeID = 313005100)]
        public DateTime? DateFillMain
        {
            get
            {
                CheckPropertyInited("DateFillMain");
                return _datefillmain;
            }
            set
            {
                _datefillmain = value;
                NotifyPropertyChanged("DateFillMain");
            }
        }


        private long? _signatureid;
        /// <summary>
        /// 313005200 Подписант акта (Идентификатор) (SIGNATURE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 313005200)]
        public long? SignatureId
        {
            get
            {
                CheckPropertyInited("SignatureId");
                return _signatureid;
            }
            set
            {
                _signatureid = value;
                NotifyPropertyChanged("SignatureId");
            }
        }


        private DateTime? _datefillsignature;
        /// <summary>
        /// 313005300 Дата заполнения_4 (DATE_FILL_SIGNATURE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313005300)]
        public DateTime? DateFillSignature
        {
            get
            {
                CheckPropertyInited("DateFillSignature");
                return _datefillsignature;
            }
            set
            {
                _datefillsignature = value;
                NotifyPropertyChanged("DateFillSignature");
            }
        }


        private string _damageamountstatus;
        /// <summary>
        /// 313005900 Статус расчета ущерба (DAMAGE_AMOUNT_STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 313005900)]
        public string DamageAmountStatus
        {
            get
            {
                CheckPropertyInited("DamageAmountStatus");
                return _damageamountstatus;
            }
            set
            {
                _damageamountstatus = value;
                NotifyPropertyChanged("DamageAmountStatus");
            }
        }


        private StatusDamageAmount _damageamountstatus_Code;
        /// <summary>
        /// 313005900 Статус расчета ущерба (справочный код) (DAMAGE_AMOUNT_STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313005900)]
        public StatusDamageAmount DamageAmountStatus_Code
        {
            get
            {
                CheckPropertyInited("DamageAmountStatus_Code");
                return this._damageamountstatus_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_damageamountstatus))
                    {
                         _damageamountstatus = descr;
                    }
                }
                else
                {
                     _damageamountstatus = descr;
                }

                this._damageamountstatus_Code = value;
                NotifyPropertyChanged("DamageAmountStatus");
                NotifyPropertyChanged("DamageAmountStatus_Code");
            }
        }


        private long? _objreestrid;
        /// <summary>
        /// 313006000 Номер реестра объекта (OBJ_REESTR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 313006000)]
        public long? ObjReestrId
        {
            get
            {
                CheckPropertyInited("ObjReestrId");
                return _objreestrid;
            }
            set
            {
                _objreestrid = value;
                NotifyPropertyChanged("ObjReestrId");
            }
        }


        private string _nomdoc;
        /// <summary>
        /// 313006100 Номер дела в ГБУ (NOM_DOC)
        /// </summary>
        [RegisterAttribute(AttributeID = 313006100)]
        public string NomDoc
        {
            get
            {
                CheckPropertyInited("NomDoc");
                return _nomdoc;
            }
            set
            {
                _nomdoc = value;
                NotifyPropertyChanged("NomDoc");
            }
        }


        private DateTime? _nomdate;
        /// <summary>
        /// 313006200 Дата дела в ОПС (NOM_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313006200)]
        public DateTime? NomDate
        {
            get
            {
                CheckPropertyInited("NomDate");
                return _nomdate;
            }
            set
            {
                _nomdate = value;
                NotifyPropertyChanged("NomDate");
            }
        }


        private DateTime? _dateinputgby;
        /// <summary>
        /// 313006300 Дата поступления дела в ЦИПиЖС (DATE_INPUT_GBY)
        /// </summary>
        [RegisterAttribute(AttributeID = 313006300)]
        public DateTime? DateInputGBY
        {
            get
            {
                CheckPropertyInited("DateInputGBY");
                return _dateinputgby;
            }
            set
            {
                _dateinputgby = value;
                NotifyPropertyChanged("DateInputGBY");
            }
        }


        private string _subreasondamagereason;
        /// <summary>
        /// 313006500 Подпричины ущерба по ЖП (справочник, 12134) (SUBREASON_DAMAGE_REASON)
        /// </summary>
        [RegisterAttribute(AttributeID = 313006500)]
        public string SubreasonDamageReason
        {
            get
            {
                CheckPropertyInited("SubreasonDamageReason");
                return _subreasondamagereason;
            }
            set
            {
                _subreasondamagereason = value;
                NotifyPropertyChanged("SubreasonDamageReason");
            }
        }


        private SubReasonCausesOfDamage _subreasondamagereason_Code;
        /// <summary>
        /// 313006500 Подпричины ущерба по ЖП (справочник, 12134) (справочный код) (SUBREASON_DAMAGE_REASON_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313006500)]
        public SubReasonCausesOfDamage SubreasonDamageReason_Code
        {
            get
            {
                CheckPropertyInited("SubreasonDamageReason_Code");
                return this._subreasondamagereason_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_subreasondamagereason))
                    {
                         _subreasondamagereason = descr;
                    }
                }
                else
                {
                     _subreasondamagereason = descr;
                }

                this._subreasondamagereason_Code = value;
                NotifyPropertyChanged("SubreasonDamageReason");
                NotifyPropertyChanged("SubreasonDamageReason_Code");
            }
        }


        private string _refinementsubreason;
        /// <summary>
        /// 313006600 Уточнение Подпричины ущерба (справочник, 12135) (REFINEMENT_SUBREASON)
        /// </summary>
        [RegisterAttribute(AttributeID = 313006600)]
        public string RefinementSubreason
        {
            get
            {
                CheckPropertyInited("RefinementSubreason");
                return _refinementsubreason;
            }
            set
            {
                _refinementsubreason = value;
                NotifyPropertyChanged("RefinementSubreason");
            }
        }


        private RefinementSubReasonCOD _refinementsubreason_Code;
        /// <summary>
        /// 313006600 Уточнение Подпричины ущерба (справочник, 12135) (справочный код) (REFINEMENT_SUBREASON_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313006600)]
        public RefinementSubReasonCOD RefinementSubreason_Code
        {
            get
            {
                CheckPropertyInited("RefinementSubreason_Code");
                return this._refinementsubreason_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_refinementsubreason))
                    {
                         _refinementsubreason = descr;
                    }
                }
                else
                {
                     _refinementsubreason = descr;
                }

                this._refinementsubreason_Code = value;
                NotifyPropertyChanged("RefinementSubreason");
                NotifyPropertyChanged("RefinementSubreason_Code");
            }
        }


        private DateTime? _datecontrol;
        /// <summary>
        /// 313006700 Дата передано на проверку (DATE_CONTROL)
        /// </summary>
        [RegisterAttribute(AttributeID = 313006700)]
        public DateTime? DateControl
        {
            get
            {
                CheckPropertyInited("DateControl");
                return _datecontrol;
            }
            set
            {
                _datecontrol = value;
                NotifyPropertyChanged("DateControl");
            }
        }


        private long? _controluserid;
        /// <summary>
        /// 313006800 Пользователь, передавщий на проверку (Идентификатор) (CONTROL_USER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 313006800)]
        public long? ControlUserId
        {
            get
            {
                CheckPropertyInited("ControlUserId");
                return _controluserid;
            }
            set
            {
                _controluserid = value;
                NotifyPropertyChanged("ControlUserId");
            }
        }


        private decimal? _franchise;
        /// <summary>
        /// 313006900 Условная франшиза (FRANCHISE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313006900)]
        public decimal? Franchise
        {
            get
            {
                CheckPropertyInited("Franchise");
                return _franchise;
            }
            set
            {
                _franchise = value;
                NotifyPropertyChanged("Franchise");
            }
        }


        private bool? _flagslygebka;
        /// <summary>
        /// 313007000 Выплата по служебной записке (FLAG_SLYGEBKA)
        /// </summary>
        [RegisterAttribute(AttributeID = 313007000)]
        public bool? FlagSlygebka
        {
            get
            {
                CheckPropertyInited("FlagSlygebka");
                return _flagslygebka;
            }
            set
            {
                _flagslygebka = value;
                NotifyPropertyChanged("FlagSlygebka");
            }
        }


        private string _additionaldata;
        /// <summary>
        /// 313007100 Дополнительная информация (ADDITIONAL_DATA)
        /// </summary>
        [RegisterAttribute(AttributeID = 313007100)]
        public string AdditionalData
        {
            get
            {
                CheckPropertyInited("AdditionalData");
                return _additionaldata;
            }
            set
            {
                _additionaldata = value;
                NotifyPropertyChanged("AdditionalData");
            }
        }


        private decimal? _sumdamagebase;
        /// <summary>
        /// 313007200 Сумма ущерба (базовая) (SUM_DAMAGE_BASE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313007200)]
        public decimal? SumDamageBase
        {
            get
            {
                CheckPropertyInited("SumDamageBase");
                return _sumdamagebase;
            }
            set
            {
                _sumdamagebase = value;
                NotifyPropertyChanged("SumDamageBase");
            }
        }


        private string _calcnote;
        /// <summary>
        /// 313007300 Примечание расчета ущерба (CALC_NOTE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313007300)]
        public string CalcNote
        {
            get
            {
                CheckPropertyInited("CalcNote");
                return _calcnote;
            }
            set
            {
                _calcnote = value;
                NotifyPropertyChanged("CalcNote");
            }
        }


        private long? _sortnumber;
        /// <summary>
        /// 313007400 Номер документа (для сортировки) ()
        /// </summary>
        [RegisterAttribute(AttributeID = 313007400)]
        public long? SortNumber
        {
            get
            {
                CheckPropertyInited("SortNumber");
                return _sortnumber;
            }
            set
            {
                _sortnumber = value;
                NotifyPropertyChanged("SortNumber");
            }
        }


        private bool? _estimatedvaluedifferent;
        /// <summary>
        /// 313007500 Признак расхождения расчетной стоимости (ESTIMATED_VALUE_DIFFERENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 313007500)]
        public bool? EstimatedValueDifferent
        {
            get
            {
                CheckPropertyInited("EstimatedValueDifferent");
                return _estimatedvaluedifferent;
            }
            set
            {
                _estimatedvaluedifferent = value;
                NotifyPropertyChanged("EstimatedValueDifferent");
            }
        }


        private string _shortaddress;
        /// <summary>
        /// 313007600 Короткий адрес помещения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 313007600)]
        public string ShortAddress
        {
            get
            {
                CheckPropertyInited("ShortAddress");
                return _shortaddress;
            }
            set
            {
                _shortaddress = value;
                NotifyPropertyChanged("ShortAddress");
            }
        }


        private DateTime? _reestrpaydate;
        /// <summary>
        /// 313007700 Дата реестра выплат ()
        /// </summary>
        [RegisterAttribute(AttributeID = 313007700)]
        public DateTime? ReestrPayDate
        {
            get
            {
                CheckPropertyInited("ReestrPayDate");
                return _reestrpaydate;
            }
            set
            {
                _reestrpaydate = value;
                NotifyPropertyChanged("ReestrPayDate");
            }
        }


        private long? _typedoccode;
        /// <summary>
        /// 313007800 Тип договора (1 - Общего имущества или по 2 -  Жилищным помещениям) (TYPE_DOC_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313007800)]
        public long? TypeDocCode
        {
            get
            {
                CheckPropertyInited("TypeDocCode");
                return _typedoccode;
            }
            set
            {
                _typedoccode = value;
                NotifyPropertyChanged("TypeDocCode");
            }
        }


        private long? _damagereasongpcode;
        /// <summary>
        /// 313007900 Причина ущерба по ЖП (на основании справочника «Причина ущерба для ЖП») (Code) (DAMAGE_REASON_GP_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313007900)]
        public long? DamageReasonGpCode
        {
            get
            {
                CheckPropertyInited("DamageReasonGpCode");
                return _damagereasongpcode;
            }
            set
            {
                _damagereasongpcode = value;
                NotifyPropertyChanged("DamageReasonGpCode");
            }
        }


        private long? _damagereasonoicode;
        /// <summary>
        /// 313008000 Причина ущерба по ОИ (на основании справочника «Причина ущерба для ОИ», code) (DAMAGE_REASON_OI_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313008000)]
        public long? DamageReasonOiCode
        {
            get
            {
                CheckPropertyInited("DamageReasonOiCode");
                return _damagereasonoicode;
            }
            set
            {
                _damagereasonoicode = value;
                NotifyPropertyChanged("DamageReasonOiCode");
            }
        }


        private long? _typebuildcode;
        /// <summary>
        /// 313008100 Тип здания (выбор из справочника «Типы зданий») (TYPE_BUILD_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313008100)]
        public long? TypeBuildCode
        {
            get
            {
                CheckPropertyInited("TypeBuildCode");
                return _typebuildcode;
            }
            set
            {
                _typebuildcode = value;
                NotifyPropertyChanged("TypeBuildCode");
            }
        }


        private long? _typecookercode;
        /// <summary>
        /// 313008200 Плита (выбор из справочника) (TYPE_COOKER_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313008200)]
        public long? TypeCookerCode
        {
            get
            {
                CheckPropertyInited("TypeCookerCode");
                return _typecookercode;
            }
            set
            {
                _typecookercode = value;
                NotifyPropertyChanged("TypeCookerCode");
            }
        }


        private long? _typefloorcode;
        /// <summary>
        /// 313008300 Материал пола (выбор из справочника) (TYPE_FLOOR_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313008300)]
        public long? TypeFloorCode
        {
            get
            {
                CheckPropertyInited("TypeFloorCode");
                return _typefloorcode;
            }
            set
            {
                _typefloorcode = value;
                NotifyPropertyChanged("TypeFloorCode");
            }
        }


        private long? _damageamountstatuscode;
        /// <summary>
        /// 313008400 Статус дела расчета ущерба (code, 12165) (DAMAGE_AMOUNT_STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313008400)]
        public long? DamageAmountStatusCode
        {
            get
            {
                CheckPropertyInited("DamageAmountStatusCode");
                return _damageamountstatuscode;
            }
            set
            {
                _damageamountstatuscode = value;
                NotifyPropertyChanged("DamageAmountStatusCode");
            }
        }


        private long? _subreasondamagereasoncode;
        /// <summary>
        /// 313008500 Подпричины ущерба по ЖП (справочник, code (SUBREASON_DAMAGE_REASON_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313008500)]
        public long? SubreasonDamageReasonCode
        {
            get
            {
                CheckPropertyInited("SubreasonDamageReasonCode");
                return _subreasondamagereasoncode;
            }
            set
            {
                _subreasondamagereasoncode = value;
                NotifyPropertyChanged("SubreasonDamageReasonCode");
            }
        }


        private long? _refinementsubreasoncode;
        /// <summary>
        /// 313008600 Уточнение Подпричины ущерба (справочник, code (REFINEMENT_SUBREASON_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313008600)]
        public long? RefinementSubreasonCode
        {
            get
            {
                CheckPropertyInited("RefinementSubreasonCode");
                return _refinementsubreasoncode;
            }
            set
            {
                _refinementsubreasoncode = value;
                NotifyPropertyChanged("RefinementSubreasonCode");
            }
        }


        private long? _linkbasedelo;
        /// <summary>
        /// 313008700 Ссылка на основное дело (LINK_BASE_DELO)
        /// </summary>
        [RegisterAttribute(AttributeID = 313008700)]
        public long? LinkBaseDelo
        {
            get
            {
                CheckPropertyInited("LinkBaseDelo");
                return _linkbasedelo;
            }
            set
            {
                _linkbasedelo = value;
                NotifyPropertyChanged("LinkBaseDelo");
            }
        }


        private long? _userid;
        /// <summary>
        /// 313008800 Идентификатор пользователя создавшего Ущерб (USER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 313008800)]
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


        private string _basedelo;
        /// <summary>
        /// 313008900 Наименование базового дела по ущербу (BASE_DELO)
        /// </summary>
        [RegisterAttribute(AttributeID = 313008900)]
        public string BaseDelo
        {
            get
            {
                CheckPropertyInited("BaseDelo");
                return _basedelo;
            }
            set
            {
                _basedelo = value;
                NotifyPropertyChanged("BaseDelo");
            }
        }


        private bool? _flagzakluchreissue;
        /// <summary>
        /// 313009000 Перевыпуск заключения (FLAG_ZAKLUCH_REISSUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 313009000)]
        public bool? FlagZakluchReissue
        {
            get
            {
                CheckPropertyInited("FlagZakluchReissue");
                return _flagzakluchreissue;
            }
            set
            {
                _flagzakluchreissue = value;
                NotifyPropertyChanged("FlagZakluchReissue");
            }
        }


        private decimal? _damageelem1;
        /// <summary>
        /// 313009100 Значение ущерба 1 (DAMAGE_ELEM_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 313009100)]
        public decimal? DamageElem1
        {
            get
            {
                CheckPropertyInited("DamageElem1");
                return _damageelem1;
            }
            set
            {
                _damageelem1 = value;
                NotifyPropertyChanged("DamageElem1");
            }
        }


        private decimal? _damageelem2;
        /// <summary>
        /// 313009200 Значение ущерба 2 (DAMAGE_ELEM_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 313009200)]
        public decimal? DamageElem2
        {
            get
            {
                CheckPropertyInited("DamageElem2");
                return _damageelem2;
            }
            set
            {
                _damageelem2 = value;
                NotifyPropertyChanged("DamageElem2");
            }
        }


        private decimal? _damageelem3;
        /// <summary>
        /// 313009300 Значение ущерба 3 (DAMAGE_ELEM_3)
        /// </summary>
        [RegisterAttribute(AttributeID = 313009300)]
        public decimal? DamageElem3
        {
            get
            {
                CheckPropertyInited("DamageElem3");
                return _damageelem3;
            }
            set
            {
                _damageelem3 = value;
                NotifyPropertyChanged("DamageElem3");
            }
        }


        private long? _allpropertyid;
        /// <summary>
        /// 313009400 Ссылка на insur_all_property (ALL_PROPERTY_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 313009400)]
        public long? AllPropertyId
        {
            get
            {
                CheckPropertyInited("AllPropertyId");
                return _allpropertyid;
            }
            set
            {
                _allpropertyid = value;
                NotifyPropertyChanged("AllPropertyId");
            }
        }


        private string _fault;
        /// <summary>
        /// 313009500 Описание повреждений (FAULT)
        /// </summary>
        [RegisterAttribute(AttributeID = 313009500)]
        public string Fault
        {
            get
            {
                CheckPropertyInited("Fault");
                return _fault;
            }
            set
            {
                _fault = value;
                NotifyPropertyChanged("Fault");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 314 Реестр страховых выплат (INSUR_PAY_TO)
    /// </summary>
    [RegisterInfo(RegisterID = 314)]
    [Serializable]
    public sealed partial class OMPayTo : OMBaseClass<OMPayTo>
    {

        private long? _empid;
        /// <summary>
        /// 314000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 314000100)]
        public long? EmpId
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


        private long? _fspid;
        /// <summary>
        /// 314000200 Ссылка на ФСП INSUR_FSP_Q.EMP_ID (FSP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 314000200)]
        public long? FspId
        {
            get
            {
                CheckPropertyInited("FspId");
                return _fspid;
            }
            set
            {
                _fspid = value;
                NotifyPropertyChanged("FspId");
            }
        }


        private long? _contractid;
        /// <summary>
        /// 314000300 -Ссылка на  запись в INSUR_POLICY_SVD или  INSUR_ALL_PROPERTY (CONTRACT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 314000300)]
        public long? ContractId
        {
            get
            {
                CheckPropertyInited("ContractId");
                return _contractid;
            }
            set
            {
                _contractid = value;
                NotifyPropertyChanged("ContractId");
            }
        }


        private long? _idreestrcontr;
        /// <summary>
        /// 314000400 Номер реестра: (ID_REESTR_CONTR)
        /// </summary>
        [RegisterAttribute(AttributeID = 314000400)]
        public long? IdReestrContr
        {
            get
            {
                CheckPropertyInited("IdReestrContr");
                return _idreestrcontr;
            }
            set
            {
                _idreestrcontr = value;
                NotifyPropertyChanged("IdReestrContr");
            }
        }


        private string _typedoc;
        /// <summary>
        /// 314000500 Тип договора  (TYPE_DOC)
        /// </summary>
        [RegisterAttribute(AttributeID = 314000500)]
        public string TypeDoc
        {
            get
            {
                CheckPropertyInited("TypeDoc");
                return _typedoc;
            }
            set
            {
                _typedoc = value;
                NotifyPropertyChanged("TypeDoc");
            }
        }


        private ContractType _typedoc_Code;
        /// <summary>
        /// 314000500 Тип договора  (справочный код) (TYPE_DOC_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 314000500)]
        public ContractType TypeDoc_Code
        {
            get
            {
                CheckPropertyInited("TypeDoc_Code");
                return this._typedoc_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typedoc))
                    {
                         _typedoc = descr;
                    }
                }
                else
                {
                     _typedoc = descr;
                }

                this._typedoc_Code = value;
                NotifyPropertyChanged("TypeDoc");
                NotifyPropertyChanged("TypeDoc_Code");
            }
        }


        private DateTime? _period;
        /// <summary>
        /// 314000600 Первый день месяца, за который предоставляются данные (PERIOD)
        /// </summary>
        [RegisterAttribute(AttributeID = 314000600)]
        public DateTime? Period
        {
            get
            {
                CheckPropertyInited("Period");
                return _period;
            }
            set
            {
                _period = value;
                NotifyPropertyChanged("Period");
            }
        }


        private long? _insuranceorganizationid;
        /// <summary>
        /// 314000700 Ссылка на справочник «Страховые организации» (INSURANCE_ORGANIZATION_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 314000700)]
        public long? InsuranceOrganizationId
        {
            get
            {
                CheckPropertyInited("InsuranceOrganizationId");
                return _insuranceorganizationid;
            }
            set
            {
                _insuranceorganizationid = value;
                NotifyPropertyChanged("InsuranceOrganizationId");
            }
        }


        private long? _aok;
        /// <summary>
        /// 314000800 Код административного округа* (AOK)
        /// </summary>
        [RegisterAttribute(AttributeID = 314000800)]
        public long? Aok
        {
            get
            {
                CheckPropertyInited("Aok");
                return _aok;
            }
            set
            {
                _aok = value;
                NotifyPropertyChanged("Aok");
            }
        }


        private long? _unom;
        /// <summary>
        /// 314000900 Уникальный номер дома* (UNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 314000900)]
        public long? Unom
        {
            get
            {
                CheckPropertyInited("Unom");
                return _unom;
            }
            set
            {
                _unom = value;
                NotifyPropertyChanged("Unom");
            }
        }


        private string _nom;
        /// <summary>
        /// 314001000 Номер квартиры* (NOM) (NOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 314001000)]
        public string Nom
        {
            get
            {
                CheckPropertyInited("Nom");
                return _nom;
            }
            set
            {
                _nom = value;
                NotifyPropertyChanged("Nom");
            }
        }


        private string _nomi;
        /// <summary>
        /// 314001100 Индекс квартиры* (NOMI) (NOMI)
        /// </summary>
        [RegisterAttribute(AttributeID = 314001100)]
        public string Nomi
        {
            get
            {
                CheckPropertyInited("Nomi");
                return _nomi;
            }
            set
            {
                _nomi = value;
                NotifyPropertyChanged("Nomi");
            }
        }


        private string _npol;
        /// <summary>
        /// 314001200 Номер полиса или страхового свидетельства (NPOL)
        /// </summary>
        [RegisterAttribute(AttributeID = 314001200)]
        public string Npol
        {
            get
            {
                CheckPropertyInited("Npol");
                return _npol;
            }
            set
            {
                _npol = value;
                NotifyPropertyChanged("Npol");
            }
        }


        private DateTime? _npoldate;
        /// <summary>
        /// 314001300 Дата начала действия договора (NPOLDATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 314001300)]
        public DateTime? Npoldate
        {
            get
            {
                CheckPropertyInited("Npoldate");
                return _npoldate;
            }
            set
            {
                _npoldate = value;
                NotifyPropertyChanged("Npoldate");
            }
        }


        private string _comnumber;
        /// <summary>
        /// 314001400 Номер акта осмотра жилого помещения/объекта общего имущества (COMNUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 314001400)]
        public string Comnumber
        {
            get
            {
                CheckPropertyInited("Comnumber");
                return _comnumber;
            }
            set
            {
                _comnumber = value;
                NotifyPropertyChanged("Comnumber");
            }
        }


        private DateTime? _comdate;
        /// <summary>
        /// 314001500 Дата акта осмотра жилого помещения/объекта общего имущества (COMDATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 314001500)]
        public DateTime? Comdate
        {
            get
            {
                CheckPropertyInited("Comdate");
                return _comdate;
            }
            set
            {
                _comdate = value;
                NotifyPropertyChanged("Comdate");
            }
        }


        private decimal? _sl;
        /// <summary>
        /// 314001600 Размер ущерба (SL)
        /// </summary>
        [RegisterAttribute(AttributeID = 314001600)]
        public decimal? Sl
        {
            get
            {
                CheckPropertyInited("Sl");
                return _sl;
            }
            set
            {
                _sl = value;
                NotifyPropertyChanged("Sl");
            }
        }


        private decimal? _spfact;
        /// <summary>
        /// 314001700 Сумма фактически выплаченного страхового                                              возмещения (SP_FACT)
        /// </summary>
        [RegisterAttribute(AttributeID = 314001700)]
        public decimal? SpFact
        {
            get
            {
                CheckPropertyInited("SpFact");
                return _spfact;
            }
            set
            {
                _spfact = value;
                NotifyPropertyChanged("SpFact");
            }
        }


        private decimal? _spno;
        /// <summary>
        /// 314001800 Размер удержанной части страхового возмещения (SP_NO)
        /// </summary>
        [RegisterAttribute(AttributeID = 314001800)]
        public decimal? SpNo
        {
            get
            {
                CheckPropertyInited("SpNo");
                return _spno;
            }
            set
            {
                _spno = value;
                NotifyPropertyChanged("SpNo");
            }
        }


        private string _factnumber;
        /// <summary>
        /// 314001900 Номер платежного поручения страховой организации (FACTNUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 314001900)]
        public string Factnumber
        {
            get
            {
                CheckPropertyInited("Factnumber");
                return _factnumber;
            }
            set
            {
                _factnumber = value;
                NotifyPropertyChanged("Factnumber");
            }
        }


        private DateTime? _factdate;
        /// <summary>
        /// 314002000 Дата платежного поручения страховой организации (FACTDATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 314002000)]
        public DateTime? Factdate
        {
            get
            {
                CheckPropertyInited("Factdate");
                return _factdate;
            }
            set
            {
                _factdate = value;
                NotifyPropertyChanged("Factdate");
            }
        }


        private long? _subjectid;
        /// <summary>
        /// 314002100 Наименование страхователя (ЖСК, ЖК, ТСЖ, управляющей компании)  (SUBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 314002100)]
        public long? SubjectId
        {
            get
            {
                CheckPropertyInited("SubjectId");
                return _subjectid;
            }
            set
            {
                _subjectid = value;
                NotifyPropertyChanged("SubjectId");
            }
        }


        private string _ndoc;
        /// <summary>
        /// 314002200 Номер договора страхования общего имущества (NDOC)
        /// </summary>
        [RegisterAttribute(AttributeID = 314002200)]
        public string Ndoc
        {
            get
            {
                CheckPropertyInited("Ndoc");
                return _ndoc;
            }
            set
            {
                _ndoc = value;
                NotifyPropertyChanged("Ndoc");
            }
        }


        private DateTime? _ndogdat;
        /// <summary>
        /// 314002300 Дата начала действия договора (NDOGDAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 314002300)]
        public DateTime? Ndogdat
        {
            get
            {
                CheckPropertyInited("Ndogdat");
                return _ndogdat;
            }
            set
            {
                _ndogdat = value;
                NotifyPropertyChanged("Ndogdat");
            }
        }


        private long? _linkidfile;
        /// <summary>
        /// 314002400 Ссылка на INSUR_INPUT_FILE (LINK_ID_FILE)
        /// </summary>
        [RegisterAttribute(AttributeID = 314002400)]
        public long? LinkIdFile
        {
            get
            {
                CheckPropertyInited("LinkIdFile");
                return _linkidfile;
            }
            set
            {
                _linkidfile = value;
                NotifyPropertyChanged("LinkIdFile");
            }
        }


        private long? _objid;
        /// <summary>
        /// 314002500 Ссылка на объект (OBJ_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 314002500)]
        public long? ObjId
        {
            get
            {
                CheckPropertyInited("ObjId");
                return _objid;
            }
            set
            {
                _objid = value;
                NotifyPropertyChanged("ObjId");
            }
        }


        private long? _objreestrid;
        /// <summary>
        /// 314002600 Номер реестра объекта (OBJ_REESTR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 314002600)]
        public long? ObjReestrId
        {
            get
            {
                CheckPropertyInited("ObjReestrId");
                return _objreestrid;
            }
            set
            {
                _objreestrid = value;
                NotifyPropertyChanged("ObjReestrId");
            }
        }


        private long? _linkdamageid;
        /// <summary>
        /// 314002700 Ссылка на реестр дел (LINK_DAMAGE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 314002700)]
        public long? LinkDamageId
        {
            get
            {
                CheckPropertyInited("LinkDamageId");
                return _linkdamageid;
            }
            set
            {
                _linkdamageid = value;
                NotifyPropertyChanged("LinkDamageId");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 315 Реестр сведений об отказах в страховых выплатах (INSUR_NO_PAY)
    /// </summary>
    [RegisterInfo(RegisterID = 315)]
    [Serializable]
    public sealed partial class OMNoPay : OMBaseClass<OMNoPay>
    {

        private long? _empid;
        /// <summary>
        /// 315000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 315000100)]
        public long? EmpId
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


        private long? _fspid;
        /// <summary>
        /// 315000200 Ссылка на ФСП INSUR_FSP_Q.EMP_ID (FSP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 315000200)]
        public long? FspId
        {
            get
            {
                CheckPropertyInited("FspId");
                return _fspid;
            }
            set
            {
                _fspid = value;
                NotifyPropertyChanged("FspId");
            }
        }


        private long? _contractid;
        /// <summary>
        /// 315000300 Ссылка на  запись в INSUR_POLICY_SVD или  INSUR_ALL_PROPERTY (CONTRACT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 315000300)]
        public long? ContractId
        {
            get
            {
                CheckPropertyInited("ContractId");
                return _contractid;
            }
            set
            {
                _contractid = value;
                NotifyPropertyChanged("ContractId");
            }
        }


        private long? _idreestrcontr;
        /// <summary>
        /// 315000500 Номер реестра: (ID_REESTR_CONTR)
        /// </summary>
        [RegisterAttribute(AttributeID = 315000500)]
        public long? IdReestrContr
        {
            get
            {
                CheckPropertyInited("IdReestrContr");
                return _idreestrcontr;
            }
            set
            {
                _idreestrcontr = value;
                NotifyPropertyChanged("IdReestrContr");
            }
        }


        private string _typedoc;
        /// <summary>
        /// 315000800 Тип договора  (TYPE_DOC)
        /// </summary>
        [RegisterAttribute(AttributeID = 315000800)]
        public string TypeDoc
        {
            get
            {
                CheckPropertyInited("TypeDoc");
                return _typedoc;
            }
            set
            {
                _typedoc = value;
                NotifyPropertyChanged("TypeDoc");
            }
        }


        private ContractType _typedoc_Code;
        /// <summary>
        /// 315000800 Тип договора  (справочный код) (TYPE_DOC_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 315000800)]
        public ContractType TypeDoc_Code
        {
            get
            {
                CheckPropertyInited("TypeDoc_Code");
                return this._typedoc_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typedoc))
                    {
                         _typedoc = descr;
                    }
                }
                else
                {
                     _typedoc = descr;
                }

                this._typedoc_Code = value;
                NotifyPropertyChanged("TypeDoc");
                NotifyPropertyChanged("TypeDoc_Code");
            }
        }


        private DateTime? _periodregdate;
        /// <summary>
        /// 315001100 Первый день месяца, за который предоставляются данные (PERIOD_REG_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 315001100)]
        public DateTime? PeriodRegDate
        {
            get
            {
                CheckPropertyInited("PeriodRegDate");
                return _periodregdate;
            }
            set
            {
                _periodregdate = value;
                NotifyPropertyChanged("PeriodRegDate");
            }
        }


        private long? _insuranceorganizationid;
        /// <summary>
        /// 315001200 Ссылка на справочник «Страховые организации» (INSURANCE_ORGANIZATION_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 315001200)]
        public long? InsuranceOrganizationId
        {
            get
            {
                CheckPropertyInited("InsuranceOrganizationId");
                return _insuranceorganizationid;
            }
            set
            {
                _insuranceorganizationid = value;
                NotifyPropertyChanged("InsuranceOrganizationId");
            }
        }


        private long? _okrugid;
        /// <summary>
        /// 315001300 Код административного округа (OKRUG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 315001300)]
        public long? OkrugId
        {
            get
            {
                CheckPropertyInited("OkrugId");
                return _okrugid;
            }
            set
            {
                _okrugid = value;
                NotifyPropertyChanged("OkrugId");
            }
        }


        private string _npol;
        /// <summary>
        /// 315001400 Номер полиса или страхового свидетельства (NPOL)
        /// </summary>
        [RegisterAttribute(AttributeID = 315001400)]
        public string Npol
        {
            get
            {
                CheckPropertyInited("Npol");
                return _npol;
            }
            set
            {
                _npol = value;
                NotifyPropertyChanged("Npol");
            }
        }


        private DateTime? _npoldate;
        /// <summary>
        /// 315001500 Дата начала действия договора (NPOLDATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 315001500)]
        public DateTime? Npoldate
        {
            get
            {
                CheckPropertyInited("Npoldate");
                return _npoldate;
            }
            set
            {
                _npoldate = value;
                NotifyPropertyChanged("Npoldate");
            }
        }


        private DateTime? _eventdat;
        /// <summary>
        /// 315001600 Дата страхового события (EVENTDAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 315001600)]
        public DateTime? Eventdat
        {
            get
            {
                CheckPropertyInited("Eventdat");
                return _eventdat;
            }
            set
            {
                _eventdat = value;
                NotifyPropertyChanged("Eventdat");
            }
        }


        private DateTime? _appdat;
        /// <summary>
        /// 315001700 Дата заявления о страховом событии (APPDAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 315001700)]
        public DateTime? Appdat
        {
            get
            {
                CheckPropertyInited("Appdat");
                return _appdat;
            }
            set
            {
                _appdat = value;
                NotifyPropertyChanged("Appdat");
            }
        }


        private string _reject;
        /// <summary>
        /// 315001800 Причины отказа в страховой выплате – по справочнику «Причины отказа в выплате по договору страхования жилого помещения» /«Причины отказа в выплате по договору страхования общего помещения» (REJECT)
        /// </summary>
        [RegisterAttribute(AttributeID = 315001800)]
        public string Reject
        {
            get
            {
                CheckPropertyInited("Reject");
                return _reject;
            }
            set
            {
                _reject = value;
                NotifyPropertyChanged("Reject");
            }
        }


        private ReasonsRefusalInsurancePaymentLivingPremise _reject_Code;
        /// <summary>
        /// 315001800 Причины отказа в страховой выплате – по справочнику «Причины отказа в выплате по договору страхования жилого помещения» /«Причины отказа в выплате по договору страхования общего помещения» (справочный код) (REJECT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 315001800)]
        public ReasonsRefusalInsurancePaymentLivingPremise Reject_Code
        {
            get
            {
                CheckPropertyInited("Reject_Code");
                return this._reject_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_reject))
                    {
                         _reject = descr;
                    }
                }
                else
                {
                     _reject = descr;
                }

                this._reject_Code = value;
                NotifyPropertyChanged("Reject");
                NotifyPropertyChanged("Reject_Code");
            }
        }


        private string _renumber;
        /// <summary>
        /// 315001900 Номер письма страховой организации об отказе в страховой выплате (RENUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 315001900)]
        public string Renumber
        {
            get
            {
                CheckPropertyInited("Renumber");
                return _renumber;
            }
            set
            {
                _renumber = value;
                NotifyPropertyChanged("Renumber");
            }
        }


        private DateTime? _redat;
        /// <summary>
        /// 315002000 Дата письма страховой организации об отказе в страховой выплате (REDAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 315002000)]
        public DateTime? Redat
        {
            get
            {
                CheckPropertyInited("Redat");
                return _redat;
            }
            set
            {
                _redat = value;
                NotifyPropertyChanged("Redat");
            }
        }


        private long? _unom;
        /// <summary>
        /// 315002100 Уникальный номер строения  (UNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 315002100)]
        public long? Unom
        {
            get
            {
                CheckPropertyInited("Unom");
                return _unom;
            }
            set
            {
                _unom = value;
                NotifyPropertyChanged("Unom");
            }
        }


        private string _orgtype;
        /// <summary>
        /// 315002200 Код формы объединения собственников (6-ЖСК,ЖК, 7-ТСЖ, 8-БО (без объединения собственников), 0-нет данных) (ORG_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 315002200)]
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


        private FormAssociationOwners _orgtype_Code;
        /// <summary>
        /// 315002200 Код формы объединения собственников (6-ЖСК,ЖК, 7-ТСЖ, 8-БО (без объединения собственников), 0-нет данных) (справочный код) (ORG_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 315002200)]
        public FormAssociationOwners OrgType_Code
        {
            get
            {
                CheckPropertyInited("OrgType_Code");
                return this._orgtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_orgtype))
                    {
                         _orgtype = descr;
                    }
                }
                else
                {
                     _orgtype = descr;
                }

                this._orgtype_Code = value;
                NotifyPropertyChanged("OrgType");
                NotifyPropertyChanged("OrgType_Code");
            }
        }


        private long? _subjectid;
        /// <summary>
        /// 315002300 Код управляющей организации (по справочнику «Управляющие организации») (SUBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 315002300)]
        public long? SubjectId
        {
            get
            {
                CheckPropertyInited("SubjectId");
                return _subjectid;
            }
            set
            {
                _subjectid = value;
                NotifyPropertyChanged("SubjectId");
            }
        }


        private string _name;
        /// <summary>
        /// 315002400 Наименование страхователя (ЖСК, ЖК, ТСЖ, управляющей организации) (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 315002400)]
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


        private string _ndog;
        /// <summary>
        /// 315002500 Уникальный номер договора страхования (NDOG)
        /// </summary>
        [RegisterAttribute(AttributeID = 315002500)]
        public string Ndog
        {
            get
            {
                CheckPropertyInited("Ndog");
                return _ndog;
            }
            set
            {
                _ndog = value;
                NotifyPropertyChanged("Ndog");
            }
        }


        private DateTime? _ndogdat;
        /// <summary>
        /// 315002600 Дата начала действия договора страхования  (NDOGDAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 315002600)]
        public DateTime? Ndogdat
        {
            get
            {
                CheckPropertyInited("Ndogdat");
                return _ndogdat;
            }
            set
            {
                _ndogdat = value;
                NotifyPropertyChanged("Ndogdat");
            }
        }


        private DateTime? _phonedat;
        /// <summary>
        /// 315002700 Дата сообщения о страховом событии (PHONEDAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 315002700)]
        public DateTime? Phonedat
        {
            get
            {
                CheckPropertyInited("Phonedat");
                return _phonedat;
            }
            set
            {
                _phonedat = value;
                NotifyPropertyChanged("Phonedat");
            }
        }


        private string _inspnumber;
        /// <summary>
        /// 315002800 Номер акта осмотра объекта по заявленному событию (INSPNUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 315002800)]
        public string Inspnumber
        {
            get
            {
                CheckPropertyInited("Inspnumber");
                return _inspnumber;
            }
            set
            {
                _inspnumber = value;
                NotifyPropertyChanged("Inspnumber");
            }
        }


        private DateTime? _inspdat;
        /// <summary>
        /// 315002900 Дата акта осмотра объекта по заявленному событию (INSPDAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 315002900)]
        public DateTime? Inspdat
        {
            get
            {
                CheckPropertyInited("Inspdat");
                return _inspdat;
            }
            set
            {
                _inspdat = value;
                NotifyPropertyChanged("Inspdat");
            }
        }


        private string _reason;
        /// <summary>
        /// 315003000 Код предполагаемой причины страхового события – по справочнику  «Причины страховых событий по договору страхования общего имущества» (REASON)
        /// </summary>
        [RegisterAttribute(AttributeID = 315003000)]
        public string Reason
        {
            get
            {
                CheckPropertyInited("Reason");
                return _reason;
            }
            set
            {
                _reason = value;
                NotifyPropertyChanged("Reason");
            }
        }


        private CausesOfDamageGP _reason_Code;
        /// <summary>
        /// 315003000 Код предполагаемой причины страхового события – по справочнику  «Причины страховых событий по договору страхования общего имущества» (справочный код) (REASON_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 315003000)]
        public CausesOfDamageGP Reason_Code
        {
            get
            {
                CheckPropertyInited("Reason_Code");
                return this._reason_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_reason))
                    {
                         _reason = descr;
                    }
                }
                else
                {
                     _reason = descr;
                }

                this._reason_Code = value;
                NotifyPropertyChanged("Reason");
                NotifyPropertyChanged("Reason_Code");
            }
        }


        private string _reasabs;
        /// <summary>
        /// 315003100 Код причины отсутствия решения по страховому событию – по справочнику “Причины отсутствия решения о страховой выплате по договору страхования общего имущества» (REASABS)
        /// </summary>
        [RegisterAttribute(AttributeID = 315003100)]
        public string Reasabs
        {
            get
            {
                CheckPropertyInited("Reasabs");
                return _reasabs;
            }
            set
            {
                _reasabs = value;
                NotifyPropertyChanged("Reasabs");
            }
        }


        private ReasonsAbsenceDecision _reasabs_Code;
        /// <summary>
        /// 315003100 Код причины отсутствия решения по страховому событию – по справочнику “Причины отсутствия решения о страховой выплате по договору страхования общего имущества» (справочный код) (REASABS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 315003100)]
        public ReasonsAbsenceDecision Reasabs_Code
        {
            get
            {
                CheckPropertyInited("Reasabs_Code");
                return this._reasabs_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_reasabs))
                    {
                         _reasabs = descr;
                    }
                }
                else
                {
                     _reasabs = descr;
                }

                this._reasabs_Code = value;
                NotifyPropertyChanged("Reasabs");
                NotifyPropertyChanged("Reasabs_Code");
            }
        }


        private long? _linkidfile;
        /// <summary>
        /// 315003200 Ссылка на INSUR_INPUT_FILE (LINK_ID_FILE)
        /// </summary>
        [RegisterAttribute(AttributeID = 315003200)]
        public long? LinkIdFile
        {
            get
            {
                CheckPropertyInited("LinkIdFile");
                return _linkidfile;
            }
            set
            {
                _linkidfile = value;
                NotifyPropertyChanged("LinkIdFile");
            }
        }


        private long? _objid;
        /// <summary>
        /// 315003300 Ссылка на объект (OBJ_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 315003300)]
        public long? ObjId
        {
            get
            {
                CheckPropertyInited("ObjId");
                return _objid;
            }
            set
            {
                _objid = value;
                NotifyPropertyChanged("ObjId");
            }
        }


        private long? _objreestrid;
        /// <summary>
        /// 315003400 Номер реестра объекта (OBJ_REESTR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 315003400)]
        public long? ObjReestrId
        {
            get
            {
                CheckPropertyInited("ObjReestrId");
                return _objreestrid;
            }
            set
            {
                _objreestrid = value;
                NotifyPropertyChanged("ObjReestrId");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 316 Реестр объектов страхования МКД (INSUR_BUILDING_Q)
    /// </summary>
    [RegisterInfo(RegisterID = 316)]
    [Serializable]
    public sealed partial class OMBuilding : OMBaseClass<OMBuilding>
    {

        private long _empid;
        /// <summary>
        /// 316000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 316000100)]
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


        private DateTime? _loaddate;
        /// <summary>
        /// 316000200 Дата загрузки данных (LOAD_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 316000200)]
        public DateTime? LoadDate
        {
            get
            {
                CheckPropertyInited("LoadDate");
                return _loaddate;
            }
            set
            {
                _loaddate = value;
                NotifyPropertyChanged("LoadDate");
            }
        }


        private string _cadasrnum;
        /// <summary>
        /// 316000300 Кадастровый номер (CADASTR_NUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 316000300)]
        public string CadasrNum
        {
            get
            {
                CheckPropertyInited("CadasrNum");
                return _cadasrnum;
            }
            set
            {
                _cadasrnum = value;
                NotifyPropertyChanged("CadasrNum");
            }
        }


        private string _statusegrn;
        /// <summary>
        /// 316000400 Ссылка на номер из справочника «Статус объекта ЕГРН» (STATUS_EGRN)
        /// </summary>
        [RegisterAttribute(AttributeID = 316000400)]
        public string StatusEgrn
        {
            get
            {
                CheckPropertyInited("StatusEgrn");
                return _statusegrn;
            }
            set
            {
                _statusegrn = value;
                NotifyPropertyChanged("StatusEgrn");
            }
        }


        private State _statusegrn_Code;
        /// <summary>
        /// 316000400 Ссылка на номер из справочника «Статус объекта ЕГРН» (справочный код) (STATUS_EGRN_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 316000400)]
        public State StatusEgrn_Code
        {
            get
            {
                CheckPropertyInited("StatusEgrn_Code");
                return this._statusegrn_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_statusegrn))
                    {
                         _statusegrn = descr;
                    }
                }
                else
                {
                     _statusegrn = descr;
                }

                this._statusegrn_Code = value;
                NotifyPropertyChanged("StatusEgrn");
                NotifyPropertyChanged("StatusEgrn_Code");
            }
        }


        private string _statussostbti;
        /// <summary>
        /// 316000500 Ссылка на номер из справочника «Статус состояния БТИ» (STATUS_SOST_BTI)
        /// </summary>
        [RegisterAttribute(AttributeID = 316000500)]
        public string StatusSostBti
        {
            get
            {
                CheckPropertyInited("StatusSostBti");
                return _statussostbti;
            }
            set
            {
                _statussostbti = value;
                NotifyPropertyChanged("StatusSostBti");
            }
        }


        private StructureStatus _statussostbti_Code;
        /// <summary>
        /// 316000500 Ссылка на номер из справочника «Статус состояния БТИ» (справочный код) (STATUS_SOST_BTI_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 316000500)]
        public StructureStatus StatusSostBti_Code
        {
            get
            {
                CheckPropertyInited("StatusSostBti_Code");
                return this._statussostbti_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_statussostbti))
                    {
                         _statussostbti = descr;
                    }
                }
                else
                {
                     _statussostbti = descr;
                }

                this._statussostbti_Code = value;
                NotifyPropertyChanged("StatusSostBti");
                NotifyPropertyChanged("StatusSostBti_Code");
            }
        }


        private DateTime? _cadastrdate;
        /// <summary>
        /// 316000600 Дата постановки на кадастровый учет (CADASTR_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 316000600)]
        public DateTime? CadastrDate
        {
            get
            {
                CheckPropertyInited("CadastrDate");
                return _cadastrdate;
            }
            set
            {
                _cadastrdate = value;
                NotifyPropertyChanged("CadastrDate");
            }
        }


        private long? _okrugid;
        /// <summary>
        /// 316000700 Идентификатор округа из справочника округов (OKRUG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 316000700)]
        public long? OkrugId
        {
            get
            {
                CheckPropertyInited("OkrugId");
                return _okrugid;
            }
            set
            {
                _okrugid = value;
                NotifyPropertyChanged("OkrugId");
            }
        }


        private long? _districtid;
        /// <summary>
        /// 316000800 Идентификатор района из справочника районов (DISTRICT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 316000800)]
        public long? DistrictId
        {
            get
            {
                CheckPropertyInited("DistrictId");
                return _districtid;
            }
            set
            {
                _districtid = value;
                NotifyPropertyChanged("DistrictId");
            }
        }


        private long? _unom;
        /// <summary>
        /// 316000900 UNOM (UNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 316000900)]
        public long? Unom
        {
            get
            {
                CheckPropertyInited("Unom");
                return _unom;
            }
            set
            {
                _unom = value;
                NotifyPropertyChanged("Unom");
            }
        }


        private string _typemkd;
        /// <summary>
        /// 316001000 Справочник «Статус дома ГБУ»  (TYPE_MKD)
        /// </summary>
        [RegisterAttribute(AttributeID = 316001000)]
        public string TypeMkd
        {
            get
            {
                CheckPropertyInited("TypeMkd");
                return _typemkd;
            }
            set
            {
                _typemkd = value;
                NotifyPropertyChanged("TypeMkd");
            }
        }


        private long? _typemkd_Code;
        /// <summary>
        /// 316001000 Справочник «Статус дома ГБУ»  (справочный код) (TYPE_MKD_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 316001000)]
        public long? TypeMkd_Code
        {
            get
            {
                CheckPropertyInited("TypeMkd_Code");
                return _typemkd_Code;
            }
            set
            {
                _typemkd_Code = value;
                NotifyPropertyChanged("TypeMkd_Code");
            }
        }


        private long? _yearstroi;
        /// <summary>
        /// 316001100 Год постройки (YEAR_STROI)
        /// </summary>
        [RegisterAttribute(AttributeID = 316001100)]
        public long? YearStroi
        {
            get
            {
                CheckPropertyInited("YearStroi");
                return _yearstroi;
            }
            set
            {
                _yearstroi = value;
                NotifyPropertyChanged("YearStroi");
            }
        }


        private long? _countfloor;
        /// <summary>
        /// 316001200 Кол-во этажей (COUNT_FLOOR)
        /// </summary>
        [RegisterAttribute(AttributeID = 316001200)]
        public long? CountFloor
        {
            get
            {
                CheckPropertyInited("CountFloor");
                return _countfloor;
            }
            set
            {
                _countfloor = value;
                NotifyPropertyChanged("CountFloor");
            }
        }


        private long? _kolgp;
        /// <summary>
        /// 316001300 Кол-во квартир в доме (KOL_GP)
        /// </summary>
        [RegisterAttribute(AttributeID = 316001300)]
        public long? KolGp
        {
            get
            {
                CheckPropertyInited("KolGp");
                return _kolgp;
            }
            set
            {
                _kolgp = value;
                NotifyPropertyChanged("KolGp");
            }
        }


        private decimal? _opl;
        /// <summary>
        /// 316001400 Общая площадь (OPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 316001400)]
        public decimal? Opl
        {
            get
            {
                CheckPropertyInited("Opl");
                return _opl;
            }
            set
            {
                _opl = value;
                NotifyPropertyChanged("Opl");
            }
        }


        private decimal? _oplg;
        /// <summary>
        /// 316001500 Площадь жилых помещений (OPL_G)
        /// </summary>
        [RegisterAttribute(AttributeID = 316001500)]
        public decimal? OplG
        {
            get
            {
                CheckPropertyInited("OplG");
                return _oplg;
            }
            set
            {
                _oplg = value;
                NotifyPropertyChanged("OplG");
            }
        }


        private decimal? _opln;
        /// <summary>
        /// 316001600 Площадь нежилых помещений (OPL_N)
        /// </summary>
        [RegisterAttribute(AttributeID = 316001600)]
        public decimal? OplN
        {
            get
            {
                CheckPropertyInited("OplN");
                return _opln;
            }
            set
            {
                _opln = value;
                NotifyPropertyChanged("OplN");
            }
        }


        private decimal? _bpl;
        /// <summary>
        /// 316001700 Площадь балконов (BPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 316001700)]
        public decimal? Bpl
        {
            get
            {
                CheckPropertyInited("Bpl");
                return _bpl;
            }
            set
            {
                _bpl = value;
                NotifyPropertyChanged("Bpl");
            }
        }


        private decimal? _hpl;
        /// <summary>
        /// 316001800 Площадь холодных помещений (HPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 316001800)]
        public decimal? Hpl
        {
            get
            {
                CheckPropertyInited("Hpl");
                return _hpl;
            }
            set
            {
                _hpl = value;
                NotifyPropertyChanged("Hpl");
            }
        }


        private decimal? _lpl;
        /// <summary>
        /// 316001900 Площадь лоджий (LPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 316001900)]
        public decimal? Lpl
        {
            get
            {
                CheckPropertyInited("Lpl");
                return _lpl;
            }
            set
            {
                _lpl = value;
                NotifyPropertyChanged("Lpl");
            }
        }


        private long? _lfpq;
        /// <summary>
        /// 316002000 Кол-во лифтов пассажирских (LFPQ)
        /// </summary>
        [RegisterAttribute(AttributeID = 316002000)]
        public long? Lfpq
        {
            get
            {
                CheckPropertyInited("Lfpq");
                return _lfpq;
            }
            set
            {
                _lfpq = value;
                NotifyPropertyChanged("Lfpq");
            }
        }


        private long? _lfgpq;
        /// <summary>
        /// 316002100 Кол-во лифтов грузопассажирских (LFGPQ)
        /// </summary>
        [RegisterAttribute(AttributeID = 316002100)]
        public long? Lfgpq
        {
            get
            {
                CheckPropertyInited("Lfgpq");
                return _lfgpq;
            }
            set
            {
                _lfgpq = value;
                NotifyPropertyChanged("Lfgpq");
            }
        }


        private long? _lfgq;
        /// <summary>
        /// 316002200 Кол-во лифтов грузовых (LFGQ)
        /// </summary>
        [RegisterAttribute(AttributeID = 316002200)]
        public long? Lfgq
        {
            get
            {
                CheckPropertyInited("Lfgq");
                return _lfgq;
            }
            set
            {
                _lfgq = value;
                NotifyPropertyChanged("Lfgq");
            }
        }


        private string _guidfiasmkd;
        /// <summary>
        /// 316002300 GUID-ссылка в справочнике ФИАС на адрес МКД (GUID_FIAS_MKD)
        /// </summary>
        [RegisterAttribute(AttributeID = 316002300)]
        public string GuidFiasMkd
        {
            get
            {
                CheckPropertyInited("GuidFiasMkd");
                return _guidfiasmkd;
            }
            set
            {
                _guidfiasmkd = value;
                NotifyPropertyChanged("GuidFiasMkd");
            }
        }


        private long? _linkbtifsks;
        /// <summary>
        /// 316002400 Ссылка на Реестр связей с объектами БТИ   (LINK_BTI_FSKS)
        /// </summary>
        [RegisterAttribute(AttributeID = 316002400)]
        public long? LinkBtiFsks
        {
            get
            {
                CheckPropertyInited("LinkBtiFsks");
                return _linkbtifsks;
            }
            set
            {
                _linkbtifsks = value;
                NotifyPropertyChanged("LinkBtiFsks");
            }
        }


        private long? _linkegrnbild;
        /// <summary>
        /// 316002500 Ссылка на Реестр зданий  в Росреестре (LINK_EGRN_BILD)
        /// </summary>
        [RegisterAttribute(AttributeID = 316002500)]
        public long? LinkEgrnBild
        {
            get
            {
                CheckPropertyInited("LinkEgrnBild");
                return _linkegrnbild;
            }
            set
            {
                _linkegrnbild = value;
                NotifyPropertyChanged("LinkEgrnBild");
            }
        }


        private string _sourceatrib;
        /// <summary>
        /// 316002600 Источник заполнения (SOURCE_INPUT)
        /// </summary>
        [RegisterAttribute(AttributeID = 316002600)]
        public string SourceAtrib
        {
            get
            {
                CheckPropertyInited("SourceAtrib");
                return _sourceatrib;
            }
            set
            {
                _sourceatrib = value;
                NotifyPropertyChanged("SourceAtrib");
            }
        }


        private SourceInput _sourceatrib_Code;
        /// <summary>
        /// 316002600 Источник заполнения (справочный код) (SOURCE_INPUT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 316002600)]
        public SourceInput SourceAtrib_Code
        {
            get
            {
                CheckPropertyInited("SourceAtrib_Code");
                return this._sourceatrib_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_sourceatrib))
                    {
                         _sourceatrib = descr;
                    }
                }
                else
                {
                     _sourceatrib = descr;
                }

                this._sourceatrib_Code = value;
                NotifyPropertyChanged("SourceAtrib");
                NotifyPropertyChanged("SourceAtrib_Code");
            }
        }


        private bool? _flaginsur;
        /// <summary>
        /// 316002700 Признак Подлежит страхованию (ручной ввод) (FLAG_INSUR)
        /// </summary>
        [RegisterAttribute(AttributeID = 316002700)]
        public bool? FlagInsur
        {
            get
            {
                CheckPropertyInited("FlagInsur");
                return _flaginsur;
            }
            set
            {
                _flaginsur = value;
                NotifyPropertyChanged("FlagInsur");
            }
        }


        private bool? _flaginsurcalculated;
        /// <summary>
        /// 316002701 Признак Подлежит страхованию (расчетный) (FLAG_INSUR_CALCULATED)
        /// </summary>
        [RegisterAttribute(AttributeID = 316002701)]
        public bool? FlagInsurCalculated
        {
            get
            {
                CheckPropertyInited("FlagInsurCalculated");
                return _flaginsurcalculated;
            }
            set
            {
                _flaginsurcalculated = value;
                NotifyPropertyChanged("FlagInsurCalculated");
            }
        }


        private string _purposename;
        /// <summary>
        /// 316002900 Назначение объекта (PURPOSE_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 316002900)]
        public string PurposeName
        {
            get
            {
                CheckPropertyInited("PurposeName");
                return _purposename;
            }
            set
            {
                _purposename = value;
                NotifyPropertyChanged("PurposeName");
            }
        }


        private Purpose _purposename_Code;
        /// <summary>
        /// 316002900 Назначение объекта (справочный код) (PURPOSE_NAME_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 316002900)]
        public Purpose PurposeName_Code
        {
            get
            {
                CheckPropertyInited("PurposeName_Code");
                return this._purposename_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_purposename))
                    {
                         _purposename = descr;
                    }
                }
                else
                {
                     _purposename = descr;
                }

                this._purposename_Code = value;
                NotifyPropertyChanged("PurposeName");
                NotifyPropertyChanged("PurposeName_Code");
            }
        }


        private decimal? _krovpl;
        /// <summary>
        /// 316003000 Площадь кровли (KROVPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 316003000)]
        public decimal? Krovpl
        {
            get
            {
                CheckPropertyInited("Krovpl");
                return _krovpl;
            }
            set
            {
                _krovpl = value;
                NotifyPropertyChanged("Krovpl");
            }
        }


        private decimal? _stroiprice;
        /// <summary>
        /// 316003100 Строительная стоимость (STROI_PRICE)
        /// </summary>
        [RegisterAttribute(AttributeID = 316003100)]
        public decimal? StroiPrice
        {
            get
            {
                CheckPropertyInited("StroiPrice");
                return _stroiprice;
            }
            set
            {
                _stroiprice = value;
                NotifyPropertyChanged("StroiPrice");
            }
        }


        private long? _addressid;
        /// <summary>
        /// 316004400 Ссылка на адрес МКД (ADDRESS_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 316004400)]
        public long? AddressId
        {
            get
            {
                CheckPropertyInited("AddressId");
                return _addressid;
            }
            set
            {
                _addressid = value;
                NotifyPropertyChanged("AddressId");
            }
        }


        private DateTime? _cadastrremove;
        /// <summary>
        /// 316004500 Дата снятия с кадастрового учета (CADASTR_REMOVE)
        /// </summary>
        [RegisterAttribute(AttributeID = 316004500)]
        public DateTime? CadastrRemove
        {
            get
            {
                CheckPropertyInited("CadastrRemove");
                return _cadastrremove;
            }
            set
            {
                _cadastrremove = value;
                NotifyPropertyChanged("CadastrRemove");
            }
        }


        private string _codekladr;
        /// <summary>
        /// 316004600 Код КЛАДР (CODE_KLADR)
        /// </summary>
        [RegisterAttribute(AttributeID = 316004600)]
        public string CodeKladr
        {
            get
            {
                CheckPropertyInited("CodeKladr");
                return _codekladr;
            }
            set
            {
                _codekladr = value;
                NotifyPropertyChanged("CodeKladr");
            }
        }


        private decimal? _epl;
        /// <summary>
        /// 316004700 Площадь помещений, не входящих в общую зону, кв.м. (EPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 316004700)]
        public decimal? Epl
        {
            get
            {
                CheckPropertyInited("Epl");
                return _epl;
            }
            set
            {
                _epl = value;
                NotifyPropertyChanged("Epl");
            }
        }


        private decimal? _pizn;
        /// <summary>
        /// 316004800 Процент износа (PIZN)
        /// </summary>
        [RegisterAttribute(AttributeID = 316004800)]
        public decimal? Pizn
        {
            get
            {
                CheckPropertyInited("Pizn");
                return _pizn;
            }
            set
            {
                _pizn = value;
                NotifyPropertyChanged("Pizn");
            }
        }


        private string _attributesource;
        /// <summary>
        /// 316004900 Источник атрибутов (SOURCE_ATRIB)
        /// </summary>
        [RegisterAttribute(AttributeID = 316004900)]
        public string AttributeSource
        {
            get
            {
                CheckPropertyInited("AttributeSource");
                return _attributesource;
            }
            set
            {
                _attributesource = value;
                NotifyPropertyChanged("AttributeSource");
            }
        }


        private decimal? _accruedsumcurrent;
        /// <summary>
        /// 316005000 Сумма взносов за МКД на текущий месяц ()
        /// </summary>
        [RegisterAttribute(AttributeID = 316005000)]
        public decimal? AccruedSumCurrent
        {
            get
            {
                CheckPropertyInited("AccruedSumCurrent");
                return _accruedsumcurrent;
            }
            set
            {
                _accruedsumcurrent = value;
                NotifyPropertyChanged("AccruedSumCurrent");
            }
        }


        private decimal? _accruedoplcurrent;
        /// <summary>
        /// 316005100 Сумма площадей за МКД на текущий месяц ()
        /// </summary>
        [RegisterAttribute(AttributeID = 316005100)]
        public decimal? AccruedOplCurrent
        {
            get
            {
                CheckPropertyInited("AccruedOplCurrent");
                return _accruedoplcurrent;
            }
            set
            {
                _accruedoplcurrent = value;
                NotifyPropertyChanged("AccruedOplCurrent");
            }
        }


        private decimal? _accruedsumlast;
        /// <summary>
        /// 316005200 Сумма взносов за МКД на прошлый месяц ()
        /// </summary>
        [RegisterAttribute(AttributeID = 316005200)]
        public decimal? AccruedSumLast
        {
            get
            {
                CheckPropertyInited("AccruedSumLast");
                return _accruedsumlast;
            }
            set
            {
                _accruedsumlast = value;
                NotifyPropertyChanged("AccruedSumLast");
            }
        }


        private decimal? _accruedopllast;
        /// <summary>
        /// 316005300 Сумма площадей за МКД на прошлый месяц ()
        /// </summary>
        [RegisterAttribute(AttributeID = 316005300)]
        public decimal? AccruedOplLast
        {
            get
            {
                CheckPropertyInited("AccruedOplLast");
                return _accruedopllast;
            }
            set
            {
                _accruedopllast = value;
                NotifyPropertyChanged("AccruedOplLast");
            }
        }


        private decimal? _creditedsumcurrent;
        /// <summary>
        /// 316005400 Сумма оплат за МКД на текущий месяц ()
        /// </summary>
        [RegisterAttribute(AttributeID = 316005400)]
        public decimal? CreditedSumCurrent
        {
            get
            {
                CheckPropertyInited("CreditedSumCurrent");
                return _creditedsumcurrent;
            }
            set
            {
                _creditedsumcurrent = value;
                NotifyPropertyChanged("CreditedSumCurrent");
            }
        }


        private decimal? _creditedoplcurrent;
        /// <summary>
        /// 316005500 Сумма площадей за МКД на текущий месяц ()
        /// </summary>
        [RegisterAttribute(AttributeID = 316005500)]
        public decimal? CreditedOplCurrent
        {
            get
            {
                CheckPropertyInited("CreditedOplCurrent");
                return _creditedoplcurrent;
            }
            set
            {
                _creditedoplcurrent = value;
                NotifyPropertyChanged("CreditedOplCurrent");
            }
        }


        private decimal? _creditedsumlast;
        /// <summary>
        /// 316005600 Сумма оплат за МКД на прошлый месяц ()
        /// </summary>
        [RegisterAttribute(AttributeID = 316005600)]
        public decimal? CreditedSumLast
        {
            get
            {
                CheckPropertyInited("CreditedSumLast");
                return _creditedsumlast;
            }
            set
            {
                _creditedsumlast = value;
                NotifyPropertyChanged("CreditedSumLast");
            }
        }


        private decimal? _creditedopllast;
        /// <summary>
        /// 316005700 Сумма площадей за МКД на прошлый месяц ()
        /// </summary>
        [RegisterAttribute(AttributeID = 316005700)]
        public decimal? CreditedOplLast
        {
            get
            {
                CheckPropertyInited("CreditedOplLast");
                return _creditedopllast;
            }
            set
            {
                _creditedopllast = value;
                NotifyPropertyChanged("CreditedOplLast");
            }
        }


        private string _note;
        /// <summary>
        /// 316005800 Примечание (NOTE)
        /// </summary>
        [RegisterAttribute(AttributeID = 316005800)]
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


        private long? _platid;
        /// <summary>
        /// 316006000 Есть начисление по квартирам в МКД в предыдущий период (идентификатор) ()
        /// </summary>
        [RegisterAttribute(AttributeID = 316006000)]
        public long? PlatId
        {
            get
            {
                CheckPropertyInited("PlatId");
                return _platid;
            }
            set
            {
                _platid = value;
                NotifyPropertyChanged("PlatId");
            }
        }


        private string _platidt;
        /// <summary>
        /// 316006100 Есть начисление по квартирам в МКД в предыдущий период (наименование) ()
        /// </summary>
        [RegisterAttribute(AttributeID = 316006100)]
        public string PlatIdT
        {
            get
            {
                CheckPropertyInited("PlatIdT");
                return _platidt;
            }
            set
            {
                _platidt = value;
                NotifyPropertyChanged("PlatIdT");
            }
        }


        private string _sostcode;
        /// <summary>
        /// 316006300 Код состояния строения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 316006300)]
        public string SostCode
        {
            get
            {
                CheckPropertyInited("SostCode");
                return _sostcode;
            }
            set
            {
                _sostcode = value;
                NotifyPropertyChanged("SostCode");
            }
        }


        private string _klcode;
        /// <summary>
        /// 316006400 Код класса строения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 316006400)]
        public string KlCode
        {
            get
            {
                CheckPropertyInited("KlCode");
                return _klcode;
            }
            set
            {
                _klcode = value;
                NotifyPropertyChanged("KlCode");
            }
        }


        private string _nazcode;
        /// <summary>
        /// 316006500 Код назначения строения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 316006500)]
        public string NazCode
        {
            get
            {
                CheckPropertyInited("NazCode");
                return _nazcode;
            }
            set
            {
                _nazcode = value;
                NotifyPropertyChanged("NazCode");
            }
        }


        private string _mstcode;
        /// <summary>
        /// 316006600 Код материала строения ()
        /// </summary>
        [RegisterAttribute(AttributeID = 316006600)]
        public string MstCode
        {
            get
            {
                CheckPropertyInited("MstCode");
                return _mstcode;
            }
            set
            {
                _mstcode = value;
                NotifyPropertyChanged("MstCode");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 317 Реестр объектов страхования жилых помещений (INSUR_FLAT_Q)
    /// </summary>
    [RegisterInfo(RegisterID = 317)]
    [Serializable]
    public sealed partial class OMFlat : OMBaseClass<OMFlat>
    {

        private long _empid;
        /// <summary>
        /// 317000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 317000100)]
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


        private DateTime? _loaddate;
        /// <summary>
        /// 317000200 Дата загрузки данных (LOAD_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 317000200)]
        public DateTime? LoadDate
        {
            get
            {
                CheckPropertyInited("LoadDate");
                return _loaddate;
            }
            set
            {
                _loaddate = value;
                NotifyPropertyChanged("LoadDate");
            }
        }


        private string _cadastrnum;
        /// <summary>
        /// 317000300 Кадастровый номер (CADASTR_NUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 317000300)]
        public string CadastrNum
        {
            get
            {
                CheckPropertyInited("CadastrNum");
                return _cadastrnum;
            }
            set
            {
                _cadastrnum = value;
                NotifyPropertyChanged("CadastrNum");
            }
        }


        private long? _unom;
        /// <summary>
        /// 317000600 UNOM (UNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 317000600)]
        public long? Unom
        {
            get
            {
                CheckPropertyInited("Unom");
                return _unom;
            }
            set
            {
                _unom = value;
                NotifyPropertyChanged("Unom");
            }
        }


        private string _kvnom;
        /// <summary>
        /// 317000700 Номер квартиры (KVNOM) (KVNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 317000700)]
        public string Kvnom
        {
            get
            {
                CheckPropertyInited("Kvnom");
                return _kvnom;
            }
            set
            {
                _kvnom = value;
                NotifyPropertyChanged("Kvnom");
            }
        }


        private string _klassflat;
        /// <summary>
        /// 317000800 Назначение помещения (KLASS_FLAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 317000800)]
        public string KlassFlat
        {
            get
            {
                CheckPropertyInited("KlassFlat");
                return _klassflat;
            }
            set
            {
                _klassflat = value;
                NotifyPropertyChanged("KlassFlat");
            }
        }


        private Assftp_cd _klassflat_Code;
        /// <summary>
        /// 317000800 Назначение помещения (справочный код) (KLASS_FLAT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 317000800)]
        public Assftp_cd KlassFlat_Code
        {
            get
            {
                CheckPropertyInited("KlassFlat_Code");
                return this._klassflat_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_klassflat))
                    {
                         _klassflat = descr;
                    }
                }
                else
                {
                     _klassflat = descr;
                }

                this._klassflat_Code = value;
                NotifyPropertyChanged("KlassFlat");
                NotifyPropertyChanged("KlassFlat_Code");
            }
        }


        private string _typeflat;
        /// <summary>
        /// 317000900 Тип помещения (TYPE_FLAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 317000900)]
        public string TypeFlat
        {
            get
            {
                CheckPropertyInited("TypeFlat");
                return _typeflat;
            }
            set
            {
                _typeflat = value;
                NotifyPropertyChanged("TypeFlat");
            }
        }


        private Assftp1 _typeflat_Code;
        /// <summary>
        /// 317000900 Тип помещения (справочный код) (TYPE_FLAT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 317000900)]
        public Assftp1 TypeFlat_Code
        {
            get
            {
                CheckPropertyInited("TypeFlat_Code");
                return this._typeflat_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typeflat))
                    {
                         _typeflat = descr;
                    }
                }
                else
                {
                     _typeflat = descr;
                }

                this._typeflat_Code = value;
                NotifyPropertyChanged("TypeFlat");
                NotifyPropertyChanged("TypeFlat_Code");
            }
        }


        private long? _flatstatus;
        /// <summary>
        /// 317001000 Статус жилого помещения (FLATSTATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 317001000)]
        public long? FlatStatus
        {
            get
            {
                CheckPropertyInited("FlatStatus");
                return _flatstatus;
            }
            set
            {
                _flatstatus = value;
                NotifyPropertyChanged("FlatStatus");
            }
        }


        private long? _prkom;
        /// <summary>
        /// 317001100 Признак коммунальности квартиры (PRKOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 317001100)]
        public long? Prkom
        {
            get
            {
                CheckPropertyInited("Prkom");
                return _prkom;
            }
            set
            {
                _prkom = value;
                NotifyPropertyChanged("Prkom");
            }
        }


        private long? _kolgp;
        /// <summary>
        /// 317001200 Кол-во комнат в квартире (KOL_GP)
        /// </summary>
        [RegisterAttribute(AttributeID = 317001200)]
        public long? KolGp
        {
            get
            {
                CheckPropertyInited("KolGp");
                return _kolgp;
            }
            set
            {
                _kolgp = value;
                NotifyPropertyChanged("KolGp");
            }
        }


        private decimal? _fopl;
        /// <summary>
        /// 317001300 Общая площадь (FOPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 317001300)]
        public decimal? Fopl
        {
            get
            {
                CheckPropertyInited("Fopl");
                return _fopl;
            }
            set
            {
                _fopl = value;
                NotifyPropertyChanged("Fopl");
            }
        }


        private decimal? _ppl;
        /// <summary>
        /// 317001400 Площадь с летними (PPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 317001400)]
        public decimal? Ppl
        {
            get
            {
                CheckPropertyInited("Ppl");
                return _ppl;
            }
            set
            {
                _ppl = value;
                NotifyPropertyChanged("Ppl");
            }
        }


        private decimal? _gpl;
        /// <summary>
        /// 317001500 Площадь жилая (GPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 317001500)]
        public decimal? Gpl
        {
            get
            {
                CheckPropertyInited("Gpl");
                return _gpl;
            }
            set
            {
                _gpl = value;
                NotifyPropertyChanged("Gpl");
            }
        }


        private string _guidfiasflat;
        /// <summary>
        /// 317001600 Код ФИАС помещения (GUID_FIAS_FLAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 317001600)]
        public string GuidFiasFlat
        {
            get
            {
                CheckPropertyInited("GuidFiasFlat");
                return _guidfiasflat;
            }
            set
            {
                _guidfiasflat = value;
                NotifyPropertyChanged("GuidFiasFlat");
            }
        }


        private string _guidfiasmkd;
        /// <summary>
        /// 317001700 Код ФИАС МКД (GUID_FIAS_MKD)
        /// </summary>
        [RegisterAttribute(AttributeID = 317001700)]
        public string GuidFiasMkd
        {
            get
            {
                CheckPropertyInited("GuidFiasMkd");
                return _guidfiasmkd;
            }
            set
            {
                _guidfiasmkd = value;
                NotifyPropertyChanged("GuidFiasMkd");
            }
        }


        private long? _linkbtiflat;
        /// <summary>
        /// 317001800 Ссылка на Реестр связей с помещением БТИ   (LINK_BTI_FLAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 317001800)]
        public long? LinkBtiFlat
        {
            get
            {
                CheckPropertyInited("LinkBtiFlat");
                return _linkbtiflat;
            }
            set
            {
                _linkbtiflat = value;
                NotifyPropertyChanged("LinkBtiFlat");
            }
        }


        private long? _linkinsuregrn;
        /// <summary>
        /// 317001900 Ссылка на Реестр зданий  в Росреестре (LINK_INSUR_EGRN)
        /// </summary>
        [RegisterAttribute(AttributeID = 317001900)]
        public long? LinkInsurEgrn
        {
            get
            {
                CheckPropertyInited("LinkInsurEgrn");
                return _linkinsuregrn;
            }
            set
            {
                _linkinsuregrn = value;
                NotifyPropertyChanged("LinkInsurEgrn");
            }
        }


        private long? _linkobjectmkd;
        /// <summary>
        /// 317002000 Ссылка на МКД (LINK_OBJECT_MKD)
        /// </summary>
        [RegisterAttribute(AttributeID = 317002000)]
        public long? LinkObjectMkd
        {
            get
            {
                CheckPropertyInited("LinkObjectMkd");
                return _linkobjectmkd;
            }
            set
            {
                _linkobjectmkd = value;
                NotifyPropertyChanged("LinkObjectMkd");
            }
        }


        private string _sourceatrib;
        /// <summary>
        /// 317002100 Источник заполнения (SOURCE_INPUT)
        /// </summary>
        [RegisterAttribute(AttributeID = 317002100)]
        public string SourceAtrib
        {
            get
            {
                CheckPropertyInited("SourceAtrib");
                return _sourceatrib;
            }
            set
            {
                _sourceatrib = value;
                NotifyPropertyChanged("SourceAtrib");
            }
        }


        private SourceInput _sourceatrib_Code;
        /// <summary>
        /// 317002100 Источник заполнения (справочный код) (SOURCE_INPUT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 317002100)]
        public SourceInput SourceAtrib_Code
        {
            get
            {
                CheckPropertyInited("SourceAtrib_Code");
                return this._sourceatrib_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_sourceatrib))
                    {
                         _sourceatrib = descr;
                    }
                }
                else
                {
                     _sourceatrib = descr;
                }

                this._sourceatrib_Code = value;
                NotifyPropertyChanged("SourceAtrib");
                NotifyPropertyChanged("SourceAtrib_Code");
            }
        }


        private bool? _flaginsur;
        /// <summary>
        /// 317002200 Признак участия в программе страхования (FLAG_INSUR)
        /// </summary>
        [RegisterAttribute(AttributeID = 317002200)]
        public bool? FlagInsur
        {
            get
            {
                CheckPropertyInited("FlagInsur");
                return _flaginsur;
            }
            set
            {
                _flaginsur = value;
                NotifyPropertyChanged("FlagInsur");
            }
        }


        private string _addressfiasmkd;
        /// <summary>
        /// 317002300 Адрес МКД в ФИАС (ADDRESS_FIAS_MKD)
        /// </summary>
        [RegisterAttribute(AttributeID = 317002300)]
        public string AddressFiasMkd
        {
            get
            {
                CheckPropertyInited("AddressFiasMkd");
                return _addressfiasmkd;
            }
            set
            {
                _addressfiasmkd = value;
                NotifyPropertyChanged("AddressFiasMkd");
            }
        }


        private string _typeflat2;
        /// <summary>
        /// 317002400 Тип помещения (по назначению) (TYPE_FLAT_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 317002400)]
        public string TypeFlat2
        {
            get
            {
                CheckPropertyInited("TypeFlat2");
                return _typeflat2;
            }
            set
            {
                _typeflat2 = value;
                NotifyPropertyChanged("TypeFlat2");
            }
        }


        private Assftp_cd _typeflat2_Code;
        /// <summary>
        /// 317002400 Тип помещения (по назначению) (справочный код) (TYPE_FLAT_2_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 317002400)]
        public Assftp_cd TypeFlat2_Code
        {
            get
            {
                CheckPropertyInited("TypeFlat2_Code");
                return this._typeflat2_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typeflat2))
                    {
                         _typeflat2 = descr;
                    }
                }
                else
                {
                     _typeflat2 = descr;
                }

                this._typeflat2_Code = value;
                NotifyPropertyChanged("TypeFlat2");
                NotifyPropertyChanged("TypeFlat2_Code");
            }
        }


        private DateTime? _cadastrdate;
        /// <summary>
        /// 317002500 Дата постановки на кадастровый учет (CADASTR_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 317002500)]
        public DateTime? CadastrDate
        {
            get
            {
                CheckPropertyInited("CadastrDate");
                return _cadastrdate;
            }
            set
            {
                _cadastrdate = value;
                NotifyPropertyChanged("CadastrDate");
            }
        }


        private string _statusegrn;
        /// <summary>
        /// 317002600 Статус объекта ЕГРН (STATUS_EGRN)
        /// </summary>
        [RegisterAttribute(AttributeID = 317002600)]
        public string StatusEgrn
        {
            get
            {
                CheckPropertyInited("StatusEgrn");
                return _statusegrn;
            }
            set
            {
                _statusegrn = value;
                NotifyPropertyChanged("StatusEgrn");
            }
        }


        private State _statusegrn_Code;
        /// <summary>
        /// 317002600 Статус объекта ЕГРН (справочный код) (STATUS_EGRN_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 317002600)]
        public State StatusEgrn_Code
        {
            get
            {
                CheckPropertyInited("StatusEgrn_Code");
                return this._statusegrn_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_statusegrn))
                    {
                         _statusegrn = descr;
                    }
                }
                else
                {
                     _statusegrn = descr;
                }

                this._statusegrn_Code = value;
                NotifyPropertyChanged("StatusEgrn");
                NotifyPropertyChanged("StatusEgrn_Code");
            }
        }


        private decimal? _opl;
        /// <summary>
        /// 317002700 (НЕ ИСПОЛЬЗОВАТЬ) Общая площадь (OPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 317002700)]
        public decimal? Opl
        {
            get
            {
                CheckPropertyInited("Opl");
                return _opl;
            }
            set
            {
                _opl = value;
                NotifyPropertyChanged("Opl");
            }
        }


        private DateTime? _cadastrremove;
        /// <summary>
        /// 317003100 Дата снятия с кадастрового учета (CADASTR_REMOVE)
        /// </summary>
        [RegisterAttribute(AttributeID = 317003100)]
        public DateTime? CadastrRemove
        {
            get
            {
                CheckPropertyInited("CadastrRemove");
                return _cadastrremove;
            }
            set
            {
                _cadastrremove = value;
                NotifyPropertyChanged("CadastrRemove");
            }
        }


        private string _codekladr;
        /// <summary>
        /// 317003200 Код КЛАДР (CODE_KLADR)
        /// </summary>
        [RegisterAttribute(AttributeID = 317003200)]
        public string CodeKladr
        {
            get
            {
                CheckPropertyInited("CodeKladr");
                return _codekladr;
            }
            set
            {
                _codekladr = value;
                NotifyPropertyChanged("CodeKladr");
            }
        }


        private string _attributesource;
        /// <summary>
        /// 317003300 Источники атрибутов (SOURCE_ATRIB)
        /// </summary>
        [RegisterAttribute(AttributeID = 317003300)]
        public string AttributeSource
        {
            get
            {
                CheckPropertyInited("AttributeSource");
                return _attributesource;
            }
            set
            {
                _attributesource = value;
                NotifyPropertyChanged("AttributeSource");
            }
        }


        private decimal? _accruedsumcurrent;
        /// <summary>
        /// 317003400 Сумма взносов за квартиру на текущий месяц ()
        /// </summary>
        [RegisterAttribute(AttributeID = 317003400)]
        public decimal? AccruedSumCurrent
        {
            get
            {
                CheckPropertyInited("AccruedSumCurrent");
                return _accruedsumcurrent;
            }
            set
            {
                _accruedsumcurrent = value;
                NotifyPropertyChanged("AccruedSumCurrent");
            }
        }


        private decimal? _accruedoplcurrent;
        /// <summary>
        /// 317003500 Сумма площадей за квартиру на текущий месяц ()
        /// </summary>
        [RegisterAttribute(AttributeID = 317003500)]
        public decimal? AccruedOplCurrent
        {
            get
            {
                CheckPropertyInited("AccruedOplCurrent");
                return _accruedoplcurrent;
            }
            set
            {
                _accruedoplcurrent = value;
                NotifyPropertyChanged("AccruedOplCurrent");
            }
        }


        private decimal? _accruedsumlast;
        /// <summary>
        /// 317003600 Сумма взносов за квартиру на прошлый месяц ()
        /// </summary>
        [RegisterAttribute(AttributeID = 317003600)]
        public decimal? AccruedSumLast
        {
            get
            {
                CheckPropertyInited("AccruedSumLast");
                return _accruedsumlast;
            }
            set
            {
                _accruedsumlast = value;
                NotifyPropertyChanged("AccruedSumLast");
            }
        }


        private decimal? _accruedopllast;
        /// <summary>
        /// 317003700 Сумма площадей за квартиру на прошлый месяц ()
        /// </summary>
        [RegisterAttribute(AttributeID = 317003700)]
        public decimal? AccruedOplLast
        {
            get
            {
                CheckPropertyInited("AccruedOplLast");
                return _accruedopllast;
            }
            set
            {
                _accruedopllast = value;
                NotifyPropertyChanged("AccruedOplLast");
            }
        }


        private decimal? _creditedsumcurrent;
        /// <summary>
        /// 317003800 Сумма оплат за квартиру на текущий месяц ()
        /// </summary>
        [RegisterAttribute(AttributeID = 317003800)]
        public decimal? CreditedSumCurrent
        {
            get
            {
                CheckPropertyInited("CreditedSumCurrent");
                return _creditedsumcurrent;
            }
            set
            {
                _creditedsumcurrent = value;
                NotifyPropertyChanged("CreditedSumCurrent");
            }
        }


        private decimal? _creditedoplcurrent;
        /// <summary>
        /// 317003900 Сумма площадей за квартиру на текущий месяц ()
        /// </summary>
        [RegisterAttribute(AttributeID = 317003900)]
        public decimal? CreditedOplCurrent
        {
            get
            {
                CheckPropertyInited("CreditedOplCurrent");
                return _creditedoplcurrent;
            }
            set
            {
                _creditedoplcurrent = value;
                NotifyPropertyChanged("CreditedOplCurrent");
            }
        }


        private decimal? _creditedsumlast;
        /// <summary>
        /// 317004000 Сумма оплат за квартиру на прошлый месяц ()
        /// </summary>
        [RegisterAttribute(AttributeID = 317004000)]
        public decimal? CreditedSumLast
        {
            get
            {
                CheckPropertyInited("CreditedSumLast");
                return _creditedsumlast;
            }
            set
            {
                _creditedsumlast = value;
                NotifyPropertyChanged("CreditedSumLast");
            }
        }


        private decimal? _creditedopllast;
        /// <summary>
        /// 317004100 Сумма площадей за квартиру на прошлый месяц ()
        /// </summary>
        [RegisterAttribute(AttributeID = 317004100)]
        public decimal? CreditedOplLast
        {
            get
            {
                CheckPropertyInited("CreditedOplLast");
                return _creditedopllast;
            }
            set
            {
                _creditedopllast = value;
                NotifyPropertyChanged("CreditedOplLast");
            }
        }


        private string _note;
        /// <summary>
        /// 317004200 Примечание (NOTE)
        /// </summary>
        [RegisterAttribute(AttributeID = 317004200)]
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


        private decimal? _oplkodplsum;
        /// <summary>
        /// 317004900 Сумма площадей, подлежащих страхованию ()
        /// </summary>
        [RegisterAttribute(AttributeID = 317004900)]
        public decimal? OplKodplSum
        {
            get
            {
                CheckPropertyInited("OplKodplSum");
                return _oplkodplsum;
            }
            set
            {
                _oplkodplsum = value;
                NotifyPropertyChanged("OplKodplSum");
            }
        }


        private bool? _oplkodplmismatch;
        /// <summary>
        /// 317005000 Признак расхождения общей площади ЖП и суммы площадей, подлежащих страхованию ()
        /// </summary>
        [RegisterAttribute(AttributeID = 317005000)]
        public bool? OplKodplMismatch
        {
            get
            {
                CheckPropertyInited("OplKodplMismatch");
                return _oplkodplmismatch;
            }
            set
            {
                _oplkodplmismatch = value;
                NotifyPropertyChanged("OplKodplMismatch");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 318 Реестр связи здания с адресом (INSUR_ADDRLINK)
    /// </summary>
    [RegisterInfo(RegisterID = 318)]
    [Serializable]
    public sealed partial class OMAddrlink : OMBaseClass<OMAddrlink>
    {
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 319 Реестр адресов (INSUR_ADDRESS)
    /// </summary>
    [RegisterInfo(RegisterID = 319)]
    [Serializable]
    public sealed partial class OMAddress : OMBaseClass<OMAddress>
    {

        private long _empid;
        /// <summary>
        /// 319000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 319000100)]
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


        private string _guidfiashouse;
        /// <summary>
        /// 319000200 GUID-ссылка в справочнике ФИАС на адрес МКД (GUID_FIAS_HOUSE)
        /// </summary>
        [RegisterAttribute(AttributeID = 319000200)]
        public string GuidFiasHouse
        {
            get
            {
                CheckPropertyInited("GuidFiasHouse");
                return _guidfiashouse;
            }
            set
            {
                _guidfiashouse = value;
                NotifyPropertyChanged("GuidFiasHouse");
            }
        }


        private string _fulladdress;
        /// <summary>
        /// 319000300 Полный адрес МКД по справочнику ФИАС (FULL_ADDRESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 319000300)]
        public string FullAddress
        {
            get
            {
                CheckPropertyInited("FullAddress");
                return _fulladdress;
            }
            set
            {
                _fulladdress = value;
                NotifyPropertyChanged("FullAddress");
            }
        }


        private string _city;
        /// <summary>
        /// 319000400 Город (CITY)
        /// </summary>
        [RegisterAttribute(AttributeID = 319000400)]
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
        /// 319000500 Улица (STREET)
        /// </summary>
        [RegisterAttribute(AttributeID = 319000500)]
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
        /// 319000600 Дом (HOUSE)
        /// </summary>
        [RegisterAttribute(AttributeID = 319000600)]
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


        private string _corpus;
        /// <summary>
        /// 319000700 Корпус (CORPUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 319000700)]
        public string Corpus
        {
            get
            {
                CheckPropertyInited("Corpus");
                return _corpus;
            }
            set
            {
                _corpus = value;
                NotifyPropertyChanged("Corpus");
            }
        }


        private string _structure;
        /// <summary>
        /// 319000800 Строение (STRUCTURE)
        /// </summary>
        [RegisterAttribute(AttributeID = 319000800)]
        public string Structure
        {
            get
            {
                CheckPropertyInited("Structure");
                return _structure;
            }
            set
            {
                _structure = value;
                NotifyPropertyChanged("Structure");
            }
        }


        private string _region;
        /// <summary>
        /// 319000900 Регион (REGION)
        /// </summary>
        [RegisterAttribute(AttributeID = 319000900)]
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


        private string _guidfiasstreet;
        /// <summary>
        /// 319001000 GUID-ссылка в справочнике ФИАС на адрес улицы (GUID_FIAS_STREET)
        /// </summary>
        [RegisterAttribute(AttributeID = 319001000)]
        public string GuidFiasStreet
        {
            get
            {
                CheckPropertyInited("GuidFiasStreet");
                return _guidfiasstreet;
            }
            set
            {
                _guidfiasstreet = value;
                NotifyPropertyChanged("GuidFiasStreet");
            }
        }


        private string _sourceaddress;
        /// <summary>
        /// 319001100 Источник поступления адреса (SOURCE_ADDRESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 319001100)]
        public string SourceAddress
        {
            get
            {
                CheckPropertyInited("SourceAddress");
                return _sourceaddress;
            }
            set
            {
                _sourceaddress = value;
                NotifyPropertyChanged("SourceAddress");
            }
        }


        private AddressSource _sourceaddress_Code;
        /// <summary>
        /// 319001100 Источник поступления адреса (справочный код) (SOURCE_ADDRESS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 319001100)]
        public AddressSource SourceAddress_Code
        {
            get
            {
                CheckPropertyInited("SourceAddress_Code");
                return this._sourceaddress_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_sourceaddress))
                    {
                         _sourceaddress = descr;
                    }
                }
                else
                {
                     _sourceaddress = descr;
                }

                this._sourceaddress_Code = value;
                NotifyPropertyChanged("SourceAddress");
                NotifyPropertyChanged("SourceAddress_Code");
            }
        }


        private string _postalcode;
        /// <summary>
        /// 319001200 Почтовый индекс (POSTAL_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 319001200)]
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


        private string _typecity;
        /// <summary>
        /// 319001300 Тип города (TYPE_CITY)
        /// </summary>
        [RegisterAttribute(AttributeID = 319001300)]
        public string TypeCity
        {
            get
            {
                CheckPropertyInited("TypeCity");
                return _typecity;
            }
            set
            {
                _typecity = value;
                NotifyPropertyChanged("TypeCity");
            }
        }


        private string _typestreet;
        /// <summary>
        /// 319001400 Тип улицы (TYPE_STREET)
        /// </summary>
        [RegisterAttribute(AttributeID = 319001400)]
        public string TypeStreet
        {
            get
            {
                CheckPropertyInited("TypeStreet");
                return _typestreet;
            }
            set
            {
                _typestreet = value;
                NotifyPropertyChanged("TypeStreet");
            }
        }


        private string _typehouse;
        /// <summary>
        /// 319001500 Тип дома (TYPE_HOUSE)
        /// </summary>
        [RegisterAttribute(AttributeID = 319001500)]
        public string TypeHouse
        {
            get
            {
                CheckPropertyInited("TypeHouse");
                return _typehouse;
            }
            set
            {
                _typehouse = value;
                NotifyPropertyChanged("TypeHouse");
            }
        }


        private string _typecorpus;
        /// <summary>
        /// 319001600 Тип корпуса (TYPE_CORPUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 319001600)]
        public string TypeCorpus
        {
            get
            {
                CheckPropertyInited("TypeCorpus");
                return _typecorpus;
            }
            set
            {
                _typecorpus = value;
                NotifyPropertyChanged("TypeCorpus");
            }
        }


        private string _typestructure;
        /// <summary>
        /// 319001700 Тип строения (TYPE_STRUCTURE)
        /// </summary>
        [RegisterAttribute(AttributeID = 319001700)]
        public string TypeStructure
        {
            get
            {
                CheckPropertyInited("TypeStructure");
                return _typestructure;
            }
            set
            {
                _typestructure = value;
                NotifyPropertyChanged("TypeStructure");
            }
        }


        private string _typeregion;
        /// <summary>
        /// 319001800 Тип региона (TYPE_REGION)
        /// </summary>
        [RegisterAttribute(AttributeID = 319001800)]
        public string TypeRegion
        {
            get
            {
                CheckPropertyInited("TypeRegion");
                return _typeregion;
            }
            set
            {
                _typeregion = value;
                NotifyPropertyChanged("TypeRegion");
            }
        }


        private string _type_avtonomnyy_okrug;
        /// <summary>
        /// 319001900 Тип автономного округа (TYPE_AVTONOMNYY_OKRUG)
        /// </summary>
        [RegisterAttribute(AttributeID = 319001900)]
        public string type_avtonomnyy_okrug
        {
            get
            {
                CheckPropertyInited("type_avtonomnyy_okrug");
                return _type_avtonomnyy_okrug;
            }
            set
            {
                _type_avtonomnyy_okrug = value;
                NotifyPropertyChanged("type_avtonomnyy_okrug");
            }
        }


        private string _avtonomnyy_okrug;
        /// <summary>
        /// 319002000 Автономный округ (AVTONOMNYY_OKRUG)
        /// </summary>
        [RegisterAttribute(AttributeID = 319002000)]
        public string avtonomnyy_okrug
        {
            get
            {
                CheckPropertyInited("avtonomnyy_okrug");
                return _avtonomnyy_okrug;
            }
            set
            {
                _avtonomnyy_okrug = value;
                NotifyPropertyChanged("avtonomnyy_okrug");
            }
        }


        private string _note;
        /// <summary>
        /// 319002100 Тип района (TYPE_DISTRICT)
        /// </summary>
        [RegisterAttribute(AttributeID = 319002100)]
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


        private string _district;
        /// <summary>
        /// 319002200 Район (DISTRICT)
        /// </summary>
        [RegisterAttribute(AttributeID = 319002200)]
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


        private string _typeurbanterritory;
        /// <summary>
        /// 319002300 Тип внутригородской территории (TYPE_URBAN_TERRITORY)
        /// </summary>
        [RegisterAttribute(AttributeID = 319002300)]
        public string TypeUrbanTerritory
        {
            get
            {
                CheckPropertyInited("TypeUrbanTerritory");
                return _typeurbanterritory;
            }
            set
            {
                _typeurbanterritory = value;
                NotifyPropertyChanged("TypeUrbanTerritory");
            }
        }


        private string _urbanterritory;
        /// <summary>
        /// 319002400 Внутригородская территория (URBAN_TERRITORY)
        /// </summary>
        [RegisterAttribute(AttributeID = 319002400)]
        public string UrbanTerritory
        {
            get
            {
                CheckPropertyInited("UrbanTerritory");
                return _urbanterritory;
            }
            set
            {
                _urbanterritory = value;
                NotifyPropertyChanged("UrbanTerritory");
            }
        }


        private string _typelocality;
        /// <summary>
        /// 319002500 Тип населенного пункта (TYPE_LOCALITY)
        /// </summary>
        [RegisterAttribute(AttributeID = 319002500)]
        public string TypeLocality
        {
            get
            {
                CheckPropertyInited("TypeLocality");
                return _typelocality;
            }
            set
            {
                _typelocality = value;
                NotifyPropertyChanged("TypeLocality");
            }
        }


        private string _locality;
        /// <summary>
        /// 319002600 Населенный пункт (LOCALITY)
        /// </summary>
        [RegisterAttribute(AttributeID = 319002600)]
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


        private string _guidregion;
        /// <summary>
        /// 319002700 Guid-ссылка региона на ФИАС (GUID_REGION)
        /// </summary>
        [RegisterAttribute(AttributeID = 319002700)]
        public string GuidRegion
        {
            get
            {
                CheckPropertyInited("GuidRegion");
                return _guidregion;
            }
            set
            {
                _guidregion = value;
                NotifyPropertyChanged("GuidRegion");
            }
        }


        private string _shortaddress;
        /// <summary>
        /// 319002800 Короткий адрес (без региона, индекса и города) (SHORT_ADDRESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 319002800)]
        public string ShortAddress
        {
            get
            {
                CheckPropertyInited("ShortAddress");
                return _shortaddress;
            }
            set
            {
                _shortaddress = value;
                NotifyPropertyChanged("ShortAddress");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 320 Справочник округов МФЦ (INSUR_OKRUG)
    /// </summary>
    [RegisterInfo(RegisterID = 320)]
    [Serializable]
    public sealed partial class OMOkrug : OMBaseClass<OMOkrug>
    {

        private long _id;
        /// <summary>
        /// 320000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 320000100)]
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


        private long _code;
        /// <summary>
        /// 320000200 Код округа в МФЦ (CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 320000200)]
        public long Code
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


        private string _name;
        /// <summary>
        /// 320000300 Наименование Округа (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 320000300)]
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


        private string _shortname;
        /// <summary>
        /// 320000400 Сокращенное наименование Округа (SHORT_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 320000400)]
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


        private long? _insurancecompanyid;
        /// <summary>
        /// 320000500 Страховая компания (INSURANCE_COMPANY_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 320000500)]
        public long? InsuranceCompanyId
        {
            get
            {
                CheckPropertyInited("InsuranceCompanyId");
                return _insurancecompanyid;
            }
            set
            {
                _insurancecompanyid = value;
                NotifyPropertyChanged("InsuranceCompanyId");
            }
        }


        private long? _refaddrokrugid;
        /// <summary>
        /// 320000600 Округ в БТИ (REF_ADDR_OKRUG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 320000600)]
        public long? RefAddrOkrugId
        {
            get
            {
                CheckPropertyInited("RefAddrOkrugId");
                return _refaddrokrugid;
            }
            set
            {
                _refaddrokrugid = value;
                NotifyPropertyChanged("RefAddrOkrugId");
            }
        }


        private long? _refaddrokrugcodegivc;
        /// <summary>
        /// 320000700 Код округа в БТИ (REF_ADDR_OKRUG_CODE_GIVC)
        /// </summary>
        [RegisterAttribute(AttributeID = 320000700)]
        public long? RefAddrOkrugCodeGivc
        {
            get
            {
                CheckPropertyInited("RefAddrOkrugCodeGivc");
                return _refaddrokrugcodegivc;
            }
            set
            {
                _refaddrokrugcodegivc = value;
                NotifyPropertyChanged("RefAddrOkrugCodeGivc");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 321 Справочник районов МФЦ (INSUR_DISTRICT)
    /// </summary>
    [RegisterInfo(RegisterID = 321)]
    [Serializable]
    public sealed partial class OMDistrict : OMBaseClass<OMDistrict>
    {

        private long _id;
        /// <summary>
        /// 321000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 321000100)]
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
        /// 321000200 Наименование (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 321000200)]
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


        private long? _code;
        /// <summary>
        /// 321000300 Код (CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 321000300)]
        public long? Code
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


        private long? _okrugid;
        /// <summary>
        /// 321000400 Идентификатор округа (OKRUG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 321000400)]
        public long? OkrugId
        {
            get
            {
                CheckPropertyInited("OkrugId");
                return _okrugid;
            }
            set
            {
                _okrugid = value;
                NotifyPropertyChanged("OkrugId");
            }
        }


        private long? _refaddrdistrictid;
        /// <summary>
        /// 321000500 Ссылка на район в БТИ (REF_ADDR_DISTRICT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 321000500)]
        public long? RefAddrDistrictId
        {
            get
            {
                CheckPropertyInited("RefAddrDistrictId");
                return _refaddrdistrictid;
            }
            set
            {
                _refaddrdistrictid = value;
                NotifyPropertyChanged("RefAddrDistrictId");
            }
        }


        private long? _refaddrdistrictcodegivc;
        /// <summary>
        /// 321000600 Код района в БТИ (REF_ADDR_DISTRICT_CODE_GIVC)
        /// </summary>
        [RegisterAttribute(AttributeID = 321000600)]
        public long? RefAddrDistrictCodeGivc
        {
            get
            {
                CheckPropertyInited("RefAddrDistrictCodeGivc");
                return _refaddrdistrictcodegivc;
            }
            set
            {
                _refaddrdistrictcodegivc = value;
                NotifyPropertyChanged("RefAddrDistrictCodeGivc");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 322 Хранилище файлов (INSUR_FILE_STORAGE)
    /// </summary>
    [RegisterInfo(RegisterID = 322)]
    [Serializable]
    public sealed partial class OMFileStorage : OMBaseClass<OMFileStorage>
    {

        private long _id;
        /// <summary>
        /// 322000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 322000100)]
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


        private string _filename;
        /// <summary>
        /// 322000200 Наименование файла (FILENAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 322000200)]
        public string Filename
        {
            get
            {
                CheckPropertyInited("Filename");
                return _filename;
            }
            set
            {
                _filename = value;
                NotifyPropertyChanged("Filename");
            }
        }


        private bool? _isvirtualdirectory;
        /// <summary>
        /// 322000300 Признак виртуальной директории (IS_VIRTUAL_DIRECTORY)
        /// </summary>
        [RegisterAttribute(AttributeID = 322000300)]
        public bool? IsVirtualDirectory
        {
            get
            {
                CheckPropertyInited("IsVirtualDirectory");
                return _isvirtualdirectory;
            }
            set
            {
                _isvirtualdirectory = value;
                NotifyPropertyChanged("IsVirtualDirectory");
            }
        }


        private long? _virtualdirectoryid;
        /// <summary>
        /// 322000400 Идентификатор виртуальной папки (VIRTUAL_DIRECTORY_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 322000400)]
        public long? VirtualDirectoryId
        {
            get
            {
                CheckPropertyInited("VirtualDirectoryId");
                return _virtualdirectoryid;
            }
            set
            {
                _virtualdirectoryid = value;
                NotifyPropertyChanged("VirtualDirectoryId");
            }
        }


        private DateTime? _periodregdate;
        /// <summary>
        /// 322000500 Период учета (PERIOD_REG_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 322000500)]
        public DateTime? PeriodRegDate
        {
            get
            {
                CheckPropertyInited("PeriodRegDate");
                return _periodregdate;
            }
            set
            {
                _periodregdate = value;
                NotifyPropertyChanged("PeriodRegDate");
            }
        }


        private string _hash;
        /// <summary>
        /// 322000600 Хэш (HASH)
        /// </summary>
        [RegisterAttribute(AttributeID = 322000600)]
        public string Hash
        {
            get
            {
                CheckPropertyInited("Hash");
                return _hash;
            }
            set
            {
                _hash = value;
                NotifyPropertyChanged("Hash");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 323 Справочник Виды документов-оснований (INSUR_DOC_BASE_TYPE)
    /// </summary>
    [RegisterInfo(RegisterID = 323)]
    [Serializable]
    public sealed partial class OMDocBaseType : OMBaseClass<OMDocBaseType>
    {

        private long _id;
        /// <summary>
        /// 32300100 ИД (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 32300100)]
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


        private string _documentbase;
        /// <summary>
        /// 32300300 Документ-основание (DOCUMENT_BASE)
        /// </summary>
        [RegisterAttribute(AttributeID = 32300300)]
        public string DocumentBase
        {
            get
            {
                CheckPropertyInited("DocumentBase");
                return _documentbase;
            }
            set
            {
                _documentbase = value;
                NotifyPropertyChanged("DocumentBase");
            }
        }


        private string _type;
        /// <summary>
        /// 32300400 Вид (TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 32300400)]
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


        private long? _order;
        /// <summary>
        /// 32300500 Порядок сортировки (ORDINAL)
        /// </summary>
        [RegisterAttribute(AttributeID = 32300500)]
        public long? Order
        {
            get
            {
                CheckPropertyInited("Order");
                return _order;
            }
            set
            {
                _order = value;
                NotifyPropertyChanged("Order");
            }
        }


        private bool? _needsetdate;
        /// <summary>
        /// 32300600 Признак, что дата обязательная для заполнения (NEED_SET_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 32300600)]
        public bool? NeedSetDate
        {
            get
            {
                CheckPropertyInited("NeedSetDate");
                return _needsetdate;
            }
            set
            {
                _needsetdate = value;
                NotifyPropertyChanged("NeedSetDate");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 324 Методики оценки ущерба (INSUR_DAMAGE_ASSESSMENT_METHOD)
    /// </summary>
    [RegisterInfo(RegisterID = 324)]
    [Serializable]
    public sealed partial class OMDamageAssessmentMethod : OMBaseClass<OMDamageAssessmentMethod>
    {

        private long _id;
        /// <summary>
        /// 324000100 ИД (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 324000100)]
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


        private string _damagesymptom;
        /// <summary>
        /// 324000200 Признаки  ущерба (DAMAGE_SYMPTOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 324000200)]
        public string DamageSymptom
        {
            get
            {
                CheckPropertyInited("DamageSymptom");
                return _damagesymptom;
            }
            set
            {
                _damagesymptom = value;
                NotifyPropertyChanged("DamageSymptom");
            }
        }


        private string _materialdamage;
        /// <summary>
        /// 324000300 Материальный ущерб, % (MATERIAL_DAMAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 324000300)]
        public string MaterialDamage
        {
            get
            {
                CheckPropertyInited("MaterialDamage");
                return _materialdamage;
            }
            set
            {
                _materialdamage = value;
                NotifyPropertyChanged("MaterialDamage");
            }
        }


        private string _workcomposition;
        /// <summary>
        /// 324000400 Примерный состав работ для устранения ущерба (WORK_COMPOSITION)
        /// </summary>
        [RegisterAttribute(AttributeID = 324000400)]
        public string WorkComposition
        {
            get
            {
                CheckPropertyInited("WorkComposition");
                return _workcomposition;
            }
            set
            {
                _workcomposition = value;
                NotifyPropertyChanged("WorkComposition");
            }
        }


        private string _elementconstructiondescription;
        /// <summary>
        /// 324000500 Элемент конструкции (описание) (ELEMENT_CONSTRUCTION_DESCRIPTION)
        /// </summary>
        [RegisterAttribute(AttributeID = 324000500)]
        public string ElementConstructionDescription
        {
            get
            {
                CheckPropertyInited("ElementConstructionDescription");
                return _elementconstructiondescription;
            }
            set
            {
                _elementconstructiondescription = value;
                NotifyPropertyChanged("ElementConstructionDescription");
            }
        }


        private string _elementconstruction;
        /// <summary>
        /// 324000600 Элемент конструкции (справочник) (ELEMENT_CONSTRUCTION)
        /// </summary>
        [RegisterAttribute(AttributeID = 324000600)]
        public string ElementConstruction
        {
            get
            {
                CheckPropertyInited("ElementConstruction");
                return _elementconstruction;
            }
            set
            {
                _elementconstruction = value;
                NotifyPropertyChanged("ElementConstruction");
            }
        }


        private ElementsOfConstructions _elementconstruction_Code;
        /// <summary>
        /// 324000600 Элемент конструкции (справочник) (справочный код) (ELEMENT_CONSTRUCTION_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 324000600)]
        public ElementsOfConstructions ElementConstruction_Code
        {
            get
            {
                CheckPropertyInited("ElementConstruction_Code");
                return this._elementconstruction_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_elementconstruction))
                    {
                         _elementconstruction = descr;
                    }
                }
                else
                {
                     _elementconstruction = descr;
                }

                this._elementconstruction_Code = value;
                NotifyPropertyChanged("ElementConstruction");
                NotifyPropertyChanged("ElementConstruction_Code");
            }
        }


        private decimal? _materialdamagemin;
        /// <summary>
        /// 324000700 Минимальный процент урона (MATERIAL_DAMAGE_MIN)
        /// </summary>
        [RegisterAttribute(AttributeID = 324000700)]
        public decimal? MaterialDamageMin
        {
            get
            {
                CheckPropertyInited("MaterialDamageMin");
                return _materialdamagemin;
            }
            set
            {
                _materialdamagemin = value;
                NotifyPropertyChanged("MaterialDamageMin");
            }
        }


        private decimal? _materialdamagemax;
        /// <summary>
        /// 324000800 Максимальный процент урона (MATERIAL_DAMAGE_MAX)
        /// </summary>
        [RegisterAttribute(AttributeID = 324000800)]
        public decimal? MaterialDamageMax
        {
            get
            {
                CheckPropertyInited("MaterialDamageMax");
                return _materialdamagemax;
            }
            set
            {
                _materialdamagemax = value;
                NotifyPropertyChanged("MaterialDamageMax");
            }
        }


        private long? _refid;
        /// <summary>
        /// 324000900 Ссылка на реестр справочников (REF_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 324000900)]
        public long? RefId
        {
            get
            {
                CheckPropertyInited("RefId");
                return _refid;
            }
            set
            {
                _refid = value;
                NotifyPropertyChanged("RefId");
            }
        }


        private long? _refitemid;
        /// <summary>
        /// 324001000 Ссылка на запись реестра справочников для фильтрации (REF_ITEM_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 324001000)]
        public long? RefItemId
        {
            get
            {
                CheckPropertyInited("RefItemId");
                return _refitemid;
            }
            set
            {
                _refitemid = value;
                NotifyPropertyChanged("RefItemId");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 325 Реестр загружаемых пакетов (INSUR_INPUT_FILE_PACKAGE)
    /// </summary>
    [RegisterInfo(RegisterID = 325)]
    [Serializable]
    public sealed partial class OMInputFilePackage : OMBaseClass<OMInputFilePackage>
    {

        private long _id;
        /// <summary>
        /// 32500100 ИД (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 32500100)]
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


        private DateTime? _periodregdate;
        /// <summary>
        /// 32500200 Период (PERIOD_REG_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 32500200)]
        public DateTime? PeriodRegDate
        {
            get
            {
                CheckPropertyInited("PeriodRegDate");
                return _periodregdate;
            }
            set
            {
                _periodregdate = value;
                NotifyPropertyChanged("PeriodRegDate");
            }
        }


        private long _okrugid;
        /// <summary>
        /// 32500300 Идентификатор округа (OKRUG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 32500300)]
        public long OkrugId
        {
            get
            {
                CheckPropertyInited("OkrugId");
                return _okrugid;
            }
            set
            {
                _okrugid = value;
                NotifyPropertyChanged("OkrugId");
            }
        }


        private long? _countdistrict;
        /// <summary>
        /// 32500400 Количество районов всего (COUNT_DISTRICT)
        /// </summary>
        [RegisterAttribute(AttributeID = 32500400)]
        public long? CountDistrict
        {
            get
            {
                CheckPropertyInited("CountDistrict");
                return _countdistrict;
            }
            set
            {
                _countdistrict = value;
                NotifyPropertyChanged("CountDistrict");
            }
        }


        private long? _districtnachloadedcount;
        /// <summary>
        /// 32500500 Загружено файлов с начислениями (NUM_NUCN_FILE)
        /// </summary>
        [RegisterAttribute(AttributeID = 32500500)]
        public long? DistrictNachLoadedCount
        {
            get
            {
                CheckPropertyInited("DistrictNachLoadedCount");
                return _districtnachloadedcount;
            }
            set
            {
                _districtnachloadedcount = value;
                NotifyPropertyChanged("DistrictNachLoadedCount");
            }
        }


        private long? _districtnachprocessedcount;
        /// <summary>
        /// 32500600 Обработано файлов с начислениями (NUM_NUCN_OBRABOT_FILE)
        /// </summary>
        [RegisterAttribute(AttributeID = 32500600)]
        public long? DistrictNachProcessedCount
        {
            get
            {
                CheckPropertyInited("DistrictNachProcessedCount");
                return _districtnachprocessedcount;
            }
            set
            {
                _districtnachprocessedcount = value;
                NotifyPropertyChanged("DistrictNachProcessedCount");
            }
        }


        private long? _districtplatloadedcount;
        /// <summary>
        /// 32500700 Загружено файлов с зачислениями (NUM_PLAT_FILE)
        /// </summary>
        [RegisterAttribute(AttributeID = 32500700)]
        public long? DistrictPlatLoadedCount
        {
            get
            {
                CheckPropertyInited("DistrictPlatLoadedCount");
                return _districtplatloadedcount;
            }
            set
            {
                _districtplatloadedcount = value;
                NotifyPropertyChanged("DistrictPlatLoadedCount");
            }
        }


        private long? _districtplatprocessedcount;
        /// <summary>
        /// 32500800 Обработано файлов с зачислениями (NUM_PLAT_OBRABOT_FILE)
        /// </summary>
        [RegisterAttribute(AttributeID = 32500800)]
        public long? DistrictPlatProcessedCount
        {
            get
            {
                CheckPropertyInited("DistrictPlatProcessedCount");
                return _districtplatprocessedcount;
            }
            set
            {
                _districtplatprocessedcount = value;
                NotifyPropertyChanged("DistrictPlatProcessedCount");
            }
        }


        private long? _bankplatfilescount;
        /// <summary>
        /// 32500900 Банковские строки. Загружено файлов (NUM_BANK_FILE)
        /// </summary>
        [RegisterAttribute(AttributeID = 32500900)]
        public long? BankPlatFilesCount
        {
            get
            {
                CheckPropertyInited("BankPlatFilesCount");
                return _bankplatfilescount;
            }
            set
            {
                _bankplatfilescount = value;
                NotifyPropertyChanged("BankPlatFilesCount");
            }
        }


        private bool? _packageprocessedcompletely;
        /// <summary>
        /// 32501000 Обработан полностью (FLAG_OBRAB_ALL)
        /// </summary>
        [RegisterAttribute(AttributeID = 32501000)]
        public bool? PackageProcessedCompletely
        {
            get
            {
                CheckPropertyInited("PackageProcessedCompletely");
                return _packageprocessedcompletely;
            }
            set
            {
                _packageprocessedcompletely = value;
                NotifyPropertyChanged("PackageProcessedCompletely");
            }
        }


        private bool? _nachprocessedcompletely;
        /// <summary>
        /// 32501100 Обработаны начисления (FLAG_OBRAB_NACH)
        /// </summary>
        [RegisterAttribute(AttributeID = 32501100)]
        public bool? NachProcessedCompletely
        {
            get
            {
                CheckPropertyInited("NachProcessedCompletely");
                return _nachprocessedcompletely;
            }
            set
            {
                _nachprocessedcompletely = value;
                NotifyPropertyChanged("NachProcessedCompletely");
            }
        }


        private bool? _strahprocessedcompletely;
        /// <summary>
        /// 32501200 Обработаны зачисления (FLAG_OBRAB_PLAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 32501200)]
        public bool? StrahProcessedCompletely
        {
            get
            {
                CheckPropertyInited("StrahProcessedCompletely");
                return _strahprocessedcompletely;
            }
            set
            {
                _strahprocessedcompletely = value;
                NotifyPropertyChanged("StrahProcessedCompletely");
            }
        }


        private bool? _strahidentifiedcompletely;
        /// <summary>
        /// 32501300 Установлена связь со строками банка (FLAG_BANK_PLAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 32501300)]
        public bool? StrahIdentifiedCompletely
        {
            get
            {
                CheckPropertyInited("StrahIdentifiedCompletely");
                return _strahidentifiedcompletely;
            }
            set
            {
                _strahidentifiedcompletely = value;
                NotifyPropertyChanged("StrahIdentifiedCompletely");
            }
        }


        private long? _straherrorscount;
        /// <summary>
        /// 32501400 Количество ошибок МФЦ (MFC_MISTAKE)
        /// </summary>
        [RegisterAttribute(AttributeID = 32501400)]
        public long? StrahErrorsCount
        {
            get
            {
                CheckPropertyInited("StrahErrorsCount");
                return _straherrorscount;
            }
            set
            {
                _straherrorscount = value;
                NotifyPropertyChanged("StrahErrorsCount");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 326 Реестр связи объекта страхования МКД с объектами БТИ (INSUR_LINK_BUILD_BTI)
    /// </summary>
    [RegisterInfo(RegisterID = 326)]
    [Serializable]
    public sealed partial class OMLinkBuildBti   : OMBaseClass<OMLinkBuildBti  >
    {

        private long _empid;
        /// <summary>
        /// 326000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 326000100)]
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


        private long? _idbtifsks;
        /// <summary>
        /// 326000200 Ссылка на запись в Реестре зданий БТИ  (ID_BTI_FSKS)
        /// </summary>
        [RegisterAttribute(AttributeID = 326000200)]
        public long? IdBtiFsks
        {
            get
            {
                CheckPropertyInited("IdBtiFsks");
                return _idbtifsks;
            }
            set
            {
                _idbtifsks = value;
                NotifyPropertyChanged("IdBtiFsks");
            }
        }


        private long? _idinsurbuild;
        /// <summary>
        /// 326000300 Ссылка на запись в Реестре объектов страхования МКД INSUR_BUILDING (ID_INSUR_BUILD)
        /// </summary>
        [RegisterAttribute(AttributeID = 326000300)]
        public long? IdInsurBuild
        {
            get
            {
                CheckPropertyInited("IdInsurBuild");
                return _idinsurbuild;
            }
            set
            {
                _idinsurbuild = value;
                NotifyPropertyChanged("IdInsurBuild");
            }
        }


        private bool? _flagdublunom;
        /// <summary>
        /// 326000400 1/0 ( Дублируется UNOM = да, нет) (FLAG_DUBL_UNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 326000400)]
        public bool? FlagDublUnom
        {
            get
            {
                CheckPropertyInited("FlagDublUnom");
                return _flagdublunom;
            }
            set
            {
                _flagdublunom = value;
                NotifyPropertyChanged("FlagDublUnom");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 327 Реестр связи между объектом страхования ЖП с помещениями в Росреестре (INSUR_LINK_FLAT_EGRN)
    /// </summary>
    [RegisterInfo(RegisterID = 327)]
    [Serializable]
    public sealed partial class OMLinkFlatEgrn : OMBaseClass<OMLinkFlatEgrn>
    {

        private long _empid;
        /// <summary>
        /// 327000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 327000100)]
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


        private long? _idinsurflat;
        /// <summary>
        /// 327000200 Ссылка на запись в Реестре зданий БТИ (ID_ INSUR_FLAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 327000200)]
        public long? IdInsurFlat
        {
            get
            {
                CheckPropertyInited("IdInsurFlat");
                return _idinsurflat;
            }
            set
            {
                _idinsurflat = value;
                NotifyPropertyChanged("IdInsurFlat");
            }
        }


        private long? _idegrnflat;
        /// <summary>
        /// 327000300 Ссылка на запись в Реестре объектов страхования МКД INSUR_BUILDING (ID_EGRN_ FLAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 327000300)]
        public long? IdEgrnFlat
        {
            get
            {
                CheckPropertyInited("IdEgrnFlat");
                return _idegrnflat;
            }
            set
            {
                _idegrnflat = value;
                NotifyPropertyChanged("IdEgrnFlat");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 328 Справочник «Страховые организации» (INSUR_INSURANCE_ORGANIZATION)
    /// </summary>
    [RegisterInfo(RegisterID = 328)]
    [Serializable]
    public sealed partial class OMInsuranceOrganization : OMBaseClass<OMInsuranceOrganization>
    {

        private long _id;
        /// <summary>
        /// 328000100 Уникальный идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 328000100)]
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
        /// 328000200 Наименование страховой организации (FULL_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 328000200)]
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
        /// 328000300 Сокращенное наименование страховой компании (SHORT_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 328000300)]
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


        private long? _code;
        /// <summary>
        /// 328000400 Код (CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 328000400)]
        public long? Code
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

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 329 Справочник «Доля ответственности СК» (INSUR_PART_COMPENSATION)
    /// </summary>
    [RegisterInfo(RegisterID = 329)]
    [Serializable]
    public sealed partial class OMPartCompensation : OMBaseClass<OMPartCompensation>
    {

        private long _empid;
        /// <summary>
        /// 329000100 Уникальный номер записи Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 329000100)]
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


        private DateTime? _datebegin;
        /// <summary>
        /// 329000200 Дата начала действия (DATE_BEGIN)
        /// </summary>
        [RegisterAttribute(AttributeID = 329000200)]
        public DateTime? DateBegin
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


        private long? _type;
        /// <summary>
        /// 329000300 Тип отвественности СК и города (TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 329000300)]
        public long? Type
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


        private decimal? _icvalue;
        /// <summary>
        /// 329000400 Доля ответственности СК (IC_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 329000400)]
        public decimal? IcValue
        {
            get
            {
                CheckPropertyInited("IcValue");
                return _icvalue;
            }
            set
            {
                _icvalue = value;
                NotifyPropertyChanged("IcValue");
            }
        }


        private decimal? _cityvalue;
        /// <summary>
        /// 329000500 Доля ответственности города (CITY_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 329000500)]
        public decimal? CityValue
        {
            get
            {
                CheckPropertyInited("CityValue");
                return _cityvalue;
            }
            set
            {
                _cityvalue = value;
                NotifyPropertyChanged("CityValue");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 330 Справочник «Базовый тариф» (INSUR_BASE_TARIFF)
    /// </summary>
    [RegisterInfo(RegisterID = 330)]
    [Serializable]
    public sealed partial class OMBaseTariff : OMBaseClass<OMBaseTariff>
    {

        private long _empid;
        /// <summary>
        /// 330000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 330000100)]
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


        private string _name;
        /// <summary>
        /// 330000200 Наименование (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 330000200)]
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


        private decimal? _value;
        /// <summary>
        /// 330000300 Значение (VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 330000300)]
        public decimal? Value
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

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 331 Справочник укрупненных показателей удельного веса восстановительной стоимости конструктивных элементов жилого помещения (INSUR_INTEGRATED_INDICATORS_REPL_COST)
    /// </summary>
    [RegisterInfo(RegisterID = 331)]
    [Serializable]
    public sealed partial class OMIntegrateIndicatorsReplecmentCost : OMBaseClass<OMIntegrateIndicatorsReplecmentCost>
    {

        private long _id;
        /// <summary>
        /// 331000100 Уникальный номер записи (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 331000100)]
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


        private string _stovetype;
        /// <summary>
        /// 331000200 Тип плиты (STOVE_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 331000200)]
        public string StoveType
        {
            get
            {
                CheckPropertyInited("StoveType");
                return _stovetype;
            }
            set
            {
                _stovetype = value;
                NotifyPropertyChanged("StoveType");
            }
        }


        private StoveType _stovetype_Code;
        /// <summary>
        /// 331000200 Тип плиты (справочный код) (STOVE_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 331000200)]
        public StoveType StoveType_Code
        {
            get
            {
                CheckPropertyInited("StoveType_Code");
                return this._stovetype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_stovetype))
                    {
                         _stovetype = descr;
                    }
                }
                else
                {
                     _stovetype = descr;
                }

                this._stovetype_Code = value;
                NotifyPropertyChanged("StoveType");
                NotifyPropertyChanged("StoveType_Code");
            }
        }


        private string _elementsconstructions;
        /// <summary>
        /// 331000300 Элемент конструкции (ELEMENTS_CONSTRUCTIONS)
        /// </summary>
        [RegisterAttribute(AttributeID = 331000300)]
        public string ElementsConstructions
        {
            get
            {
                CheckPropertyInited("ElementsConstructions");
                return _elementsconstructions;
            }
            set
            {
                _elementsconstructions = value;
                NotifyPropertyChanged("ElementsConstructions");
            }
        }


        private ElementsOfConstructions _elementsconstructions_Code;
        /// <summary>
        /// 331000300 Элемент конструкции (справочный код) (ELEMENTS_CONSTRUCTIONS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 331000300)]
        public ElementsOfConstructions ElementsConstructions_Code
        {
            get
            {
                CheckPropertyInited("ElementsConstructions_Code");
                return this._elementsconstructions_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_elementsconstructions))
                    {
                         _elementsconstructions = descr;
                    }
                }
                else
                {
                     _elementsconstructions = descr;
                }

                this._elementsconstructions_Code = value;
                NotifyPropertyChanged("ElementsConstructions");
                NotifyPropertyChanged("ElementsConstructions_Code");
            }
        }


        private string _floormaterial;
        /// <summary>
        /// 331000400 Материал пола (FLOOR_MATERIAL)
        /// </summary>
        [RegisterAttribute(AttributeID = 331000400)]
        public string FloorMaterial
        {
            get
            {
                CheckPropertyInited("FloorMaterial");
                return _floormaterial;
            }
            set
            {
                _floormaterial = value;
                NotifyPropertyChanged("FloorMaterial");
            }
        }


        private FloorMaterial _floormaterial_Code;
        /// <summary>
        /// 331000400 Материал пола (справочный код) (FLOOR_MATERIAL_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 331000400)]
        public FloorMaterial FloorMaterial_Code
        {
            get
            {
                CheckPropertyInited("FloorMaterial_Code");
                return this._floormaterial_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_floormaterial))
                    {
                         _floormaterial = descr;
                    }
                }
                else
                {
                     _floormaterial = descr;
                }

                this._floormaterial_Code = value;
                NotifyPropertyChanged("FloorMaterial");
                NotifyPropertyChanged("FloorMaterial_Code");
            }
        }


        private string _buildingtype;
        /// <summary>
        /// 331000500 Тип здания (BUILDING_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 331000500)]
        public string BuildingType
        {
            get
            {
                CheckPropertyInited("BuildingType");
                return _buildingtype;
            }
            set
            {
                _buildingtype = value;
                NotifyPropertyChanged("BuildingType");
            }
        }


        private BuildingType _buildingtype_Code;
        /// <summary>
        /// 331000500 Тип здания (справочный код) (BUILDING_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 331000500)]
        public BuildingType BuildingType_Code
        {
            get
            {
                CheckPropertyInited("BuildingType_Code");
                return this._buildingtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_buildingtype))
                    {
                         _buildingtype = descr;
                    }
                }
                else
                {
                     _buildingtype = descr;
                }

                this._buildingtype_Code = value;
                NotifyPropertyChanged("BuildingType");
                NotifyPropertyChanged("BuildingType_Code");
            }
        }


        private decimal? _costvalue;
        /// <summary>
        /// 331000600 Удельный вес стоимости (COST_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 331000600)]
        public decimal? CostValue
        {
            get
            {
                CheckPropertyInited("CostValue");
                return _costvalue;
            }
            set
            {
                _costvalue = value;
                NotifyPropertyChanged("CostValue");
            }
        }


        private long? _parentid;
        /// <summary>
        /// 331000700 Идентификтаор родителя (PARENT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 331000700)]
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


        private bool? _haschilds;
        /// <summary>
        /// 331000800 Принзнак наличия детей ()
        /// </summary>
        [RegisterAttribute(AttributeID = 331000800)]
        public bool? HasChilds
        {
            get
            {
                CheckPropertyInited("HasChilds");
                return _haschilds;
            }
            set
            {
                _haschilds = value;
                NotifyPropertyChanged("HasChilds");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 332 Справочник "Статус квартиры /доли" (INSUR_FLAT_STATUS)
    /// </summary>
    [RegisterInfo(RegisterID = 332)]
    [Serializable]
    public sealed partial class OMFlatStatus : OMBaseClass<OMFlatStatus>
    {

        private long _id;
        /// <summary>
        /// 332000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 332000100)]
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
        /// 332000200 Наименование (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 332000200)]
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


        private long? _code;
        /// <summary>
        /// 332000300 Код (CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 332000300)]
        public long? Code
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


        private string _shortname;
        /// <summary>
        /// 332000400 Краткое название (SHORT_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 332000400)]
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

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 333 Справочник Тип жилого помещения (INSUR_FLAT_TYPE)
    /// </summary>
    [RegisterInfo(RegisterID = 333)]
    [Serializable]
    public sealed partial class OMFlatType : OMBaseClass<OMFlatType>
    {

        private long _id;
        /// <summary>
        /// 333000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 333000100)]
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
        /// 333000200 Наименование (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 333000200)]
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


        private long? _code;
        /// <summary>
        /// 333000300 Код (CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 333000300)]
        public long? Code
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


        private string _shortname;
        /// <summary>
        /// 333000400 Краткое название (SHORT_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 333000400)]
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

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 334 Реестр проектов договоров страхования (INSUR_AGREEMENT_PROJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 334)]
    [Serializable]
    public sealed partial class OMAgreementProject : OMBaseClass<OMAgreementProject>
    {

        private long _empid;
        /// <summary>
        /// 334000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 334000100)]
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


        private long? _gotuserid;
        /// <summary>
        /// 334000200 Пользователь, который получил проект договора (GOT_USER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 334000200)]
        public long? GotUserId
        {
            get
            {
                CheckPropertyInited("GotUserId");
                return _gotuserid;
            }
            set
            {
                _gotuserid = value;
                NotifyPropertyChanged("GotUserId");
            }
        }


        private DateTime? _gotdate;
        /// <summary>
        /// 334000300 Дата получения проекта договора (GOT_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 334000300)]
        public DateTime? GotDate
        {
            get
            {
                CheckPropertyInited("GotDate");
                return _gotdate;
            }
            set
            {
                _gotdate = value;
                NotifyPropertyChanged("GotDate");
            }
        }


        private long? _approvaluserid;
        /// <summary>
        /// 334000400 Пользователь, который согласовал проект договора (APPROVAL_USER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 334000400)]
        public long? ApprovalUserId
        {
            get
            {
                CheckPropertyInited("ApprovalUserId");
                return _approvaluserid;
            }
            set
            {
                _approvaluserid = value;
                NotifyPropertyChanged("ApprovalUserId");
            }
        }


        private DateTime? _approvaldate;
        /// <summary>
        /// 334000500 Дата согласования проекта договора (APPROVAL_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 334000500)]
        public DateTime? ApprovalDate
        {
            get
            {
                CheckPropertyInited("ApprovalDate");
                return _approvaldate;
            }
            set
            {
                _approvaldate = value;
                NotifyPropertyChanged("ApprovalDate");
            }
        }


        private string _note;
        /// <summary>
        /// 334000600 Примечание (NOTE)
        /// </summary>
        [RegisterAttribute(AttributeID = 334000600)]
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


        private long? _calculationid;
        /// <summary>
        /// 334000700 Идентификатор расчета (CALCULATION_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 334000700)]
        public long? CalculationId
        {
            get
            {
                CheckPropertyInited("CalculationId");
                return _calculationid;
            }
            set
            {
                _calculationid = value;
                NotifyPropertyChanged("CalculationId");
            }
        }


        private string _commentspravka;
        /// <summary>
        /// 334000800 Для отчета Справка, пункт Замечания по документу (COMMENT_SPRAVKA)
        /// </summary>
        [RegisterAttribute(AttributeID = 334000800)]
        public string CommentSpravka
        {
            get
            {
                CheckPropertyInited("CommentSpravka");
                return _commentspravka;
            }
            set
            {
                _commentspravka = value;
                NotifyPropertyChanged("CommentSpravka");
            }
        }


        private string _resumespravka;
        /// <summary>
        /// 334000900 Для отчета Справка, пункт Принятое решение с учетом замечаний (RESUME_SPRAVKA)
        /// </summary>
        [RegisterAttribute(AttributeID = 334000900)]
        public string ResumeSpravka
        {
            get
            {
                CheckPropertyInited("ResumeSpravka");
                return _resumespravka;
            }
            set
            {
                _resumespravka = value;
                NotifyPropertyChanged("ResumeSpravka");
            }
        }


        private decimal? _partmoscow;
        /// <summary>
        /// 334001000 Доля города Москвы, % (PART_MOSCOW)
        /// </summary>
        [RegisterAttribute(AttributeID = 334001000)]
        public decimal? PartMoscow
        {
            get
            {
                CheckPropertyInited("PartMoscow");
                return _partmoscow;
            }
            set
            {
                _partmoscow = value;
                NotifyPropertyChanged("PartMoscow");
            }
        }


        private bool? _kat1;
        /// <summary>
        /// 334001100 Использовать расчет для первой категории (KAT_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 334001100)]
        public bool? Kat1
        {
            get
            {
                CheckPropertyInited("Kat1");
                return _kat1;
            }
            set
            {
                _kat1 = value;
                NotifyPropertyChanged("Kat1");
            }
        }


        private bool? _kat2;
        /// <summary>
        /// 334001200 Использовать расчет для второй категории (KAT_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 334001200)]
        public bool? Kat2
        {
            get
            {
                CheckPropertyInited("Kat2");
                return _kat2;
            }
            set
            {
                _kat2 = value;
                NotifyPropertyChanged("Kat2");
            }
        }


        private bool? _kat3;
        /// <summary>
        /// 334001300 Использовать расчет для третьей категории (KAT_3)
        /// </summary>
        [RegisterAttribute(AttributeID = 334001300)]
        public bool? Kat3
        {
            get
            {
                CheckPropertyInited("Kat3");
                return _kat3;
            }
            set
            {
                _kat3 = value;
                NotifyPropertyChanged("Kat3");
            }
        }


        private string _progectnum;
        /// <summary>
        /// 334001400 № проекта договора (PROGECT_NUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 334001400)]
        public string ProgectNum
        {
            get
            {
                CheckPropertyInited("ProgectNum");
                return _progectnum;
            }
            set
            {
                _progectnum = value;
                NotifyPropertyChanged("ProgectNum");
            }
        }


        private decimal? _sizebonusmkd;
        /// <summary>
        /// 334001500 Годовой страховой премии по дому (SIZE_BONUS_MKD)
        /// </summary>
        [RegisterAttribute(AttributeID = 334001500)]
        public decimal? SizeBonusMkd
        {
            get
            {
                CheckPropertyInited("SizeBonusMkd");
                return _sizebonusmkd;
            }
            set
            {
                _sizebonusmkd = value;
                NotifyPropertyChanged("SizeBonusMkd");
            }
        }


        private decimal _cityshare;
        /// <summary>
        /// 334001800 Доля города, руб ()
        /// </summary>
        [RegisterAttribute(AttributeID = 334001800)]
        public decimal CityShare
        {
            get
            {
                CheckPropertyInited("CityShare");
                return _cityshare;
            }
            set
            {
                _cityshare = value;
                NotifyPropertyChanged("CityShare");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 340 Реестр документов-оснований дел (INSUR_DOCUMENTS)
    /// </summary>
    [RegisterInfo(RegisterID = 340)]
    [Serializable]
    public sealed partial class OMDocuments : OMBaseClass<OMDocuments>
    {

        private long _empid;
        /// <summary>
        /// 340000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 340000100)]
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


        private long? _doctypeid;
        /// <summary>
        /// 340000200 Вид документа-основания (выбор из справочника, справочник «Виды документов-оснований») (DOC_TYPE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 340000200)]
        public long? DocTypeId
        {
            get
            {
                CheckPropertyInited("DocTypeId");
                return _doctypeid;
            }
            set
            {
                _doctypeid = value;
                NotifyPropertyChanged("DocTypeId");
            }
        }


        private bool? _docishave;
        /// <summary>
        /// 340000300 Признак о предоставлении (Да/Нет) (DOC_IS_HAVE)
        /// </summary>
        [RegisterAttribute(AttributeID = 340000300)]
        public bool? DocIsHave
        {
            get
            {
                CheckPropertyInited("DocIsHave");
                return _docishave;
            }
            set
            {
                _docishave = value;
                NotifyPropertyChanged("DocIsHave");
            }
        }


        private string _doctypem;
        /// <summary>
        /// 340000400 Тип (бумажная/электронная) (DOC_TYPE_M)
        /// </summary>
        [RegisterAttribute(AttributeID = 340000400)]
        public string DocTypeM
        {
            get
            {
                CheckPropertyInited("DocTypeM");
                return _doctypem;
            }
            set
            {
                _doctypem = value;
                NotifyPropertyChanged("DocTypeM");
            }
        }


        private TypeDocBaseCase _doctypem_Code;
        /// <summary>
        /// 340000400 Тип (бумажная/электронная) (справочный код) (DOC_TYPE_M_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 340000400)]
        public TypeDocBaseCase DocTypeM_Code
        {
            get
            {
                CheckPropertyInited("DocTypeM_Code");
                return this._doctypem_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_doctypem))
                    {
                         _doctypem = descr;
                    }
                }
                else
                {
                     _doctypem = descr;
                }

                this._doctypem_Code = value;
                NotifyPropertyChanged("DocTypeM");
                NotifyPropertyChanged("DocTypeM_Code");
            }
        }


        private string _docnumber;
        /// <summary>
        /// 340000500 Номер (DOC_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 340000500)]
        public string DocNumber
        {
            get
            {
                CheckPropertyInited("DocNumber");
                return _docnumber;
            }
            set
            {
                _docnumber = value;
                NotifyPropertyChanged("DocNumber");
            }
        }


        private DateTime? _docdate;
        /// <summary>
        /// 340000600 Дата (DOC_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 340000600)]
        public DateTime? DocDate
        {
            get
            {
                CheckPropertyInited("DocDate");
                return _docdate;
            }
            set
            {
                _docdate = value;
                NotifyPropertyChanged("DocDate");
            }
        }


        private long? _docorgid;
        /// <summary>
        /// 340000700 Организация  (выбор из справочника, справочник  «Страховые организации) (DOC_ORG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 340000700)]
        public long? DocOrgId
        {
            get
            {
                CheckPropertyInited("DocOrgId");
                return _docorgid;
            }
            set
            {
                _docorgid = value;
                NotifyPropertyChanged("DocOrgId");
            }
        }


        private string _fioscan;
        /// <summary>
        /// 340000800 ФИО сотрудника загрузившего документ (FIO_SCAN)
        /// </summary>
        [RegisterAttribute(AttributeID = 340000800)]
        public string FIOScan
        {
            get
            {
                CheckPropertyInited("FIOScan");
                return _fioscan;
            }
            set
            {
                _fioscan = value;
                NotifyPropertyChanged("FIOScan");
            }
        }


        private long? _objid;
        /// <summary>
        /// 340000900 Ссылка на уникальный номер записи (OBJ_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 340000900)]
        public long? ObjId
        {
            get
            {
                CheckPropertyInited("ObjId");
                return _objid;
            }
            set
            {
                _objid = value;
                NotifyPropertyChanged("ObjId");
            }
        }


        private long? _filestorageid;
        /// <summary>
        /// 340001000 Сcылка на хранилище документов (FILE_STORAGE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 340001000)]
        public long? FileStorageId
        {
            get
            {
                CheckPropertyInited("FileStorageId");
                return _filestorageid;
            }
            set
            {
                _filestorageid = value;
                NotifyPropertyChanged("FileStorageId");
            }
        }


        private long? _userid;
        /// <summary>
        /// 340001100 Пользователь, создавший запись (USER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 340001100)]
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


        private DateTime? _datecreate;
        /// <summary>
        /// 340001200 Дата ввода (DATE_CREATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 340001200)]
        public DateTime? DateCreate
        {
            get
            {
                CheckPropertyInited("DateCreate");
                return _datecreate;
            }
            set
            {
                _datecreate = value;
                NotifyPropertyChanged("DateCreate");
            }
        }


        private long? _reestrid;
        /// <summary>
        /// 340001300 Номер реестра записи (REESTR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 340001300)]
        public long? ReestrId
        {
            get
            {
                CheckPropertyInited("ReestrId");
                return _reestrid;
            }
            set
            {
                _reestrid = value;
                NotifyPropertyChanged("ReestrId");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 344 Справочник «Банки» (INSUR_BANK)
    /// </summary>
    [RegisterInfo(RegisterID = 344)]
    [Serializable]
    public sealed partial class OMBank : OMBaseClass<OMBank>
    {

        private long _empid;
        /// <summary>
        /// 344000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 344000100)]
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


        private string _bankname;
        /// <summary>
        /// 344000200 Наименование банка (BANK_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 344000200)]
        public string BankName
        {
            get
            {
                CheckPropertyInited("BankName");
                return _bankname;
            }
            set
            {
                _bankname = value;
                NotifyPropertyChanged("BankName");
            }
        }


        private DateTime? _dateinput;
        /// <summary>
        /// 344000300 Дата создания (DATE_INPUT)
        /// </summary>
        [RegisterAttribute(AttributeID = 344000300)]
        public DateTime? DateInput
        {
            get
            {
                CheckPropertyInited("DateInput");
                return _dateinput;
            }
            set
            {
                _dateinput = value;
                NotifyPropertyChanged("DateInput");
            }
        }


        private string _bankinn;
        /// <summary>
        /// 344000400 ИНН банка (INN)
        /// </summary>
        [RegisterAttribute(AttributeID = 344000400)]
        public string BankInn
        {
            get
            {
                CheckPropertyInited("BankInn");
                return _bankinn;
            }
            set
            {
                _bankinn = value;
                NotifyPropertyChanged("BankInn");
            }
        }


        private string _bankkpp;
        /// <summary>
        /// 344000500 КПП банка (KPP)
        /// </summary>
        [RegisterAttribute(AttributeID = 344000500)]
        public string BankKpp
        {
            get
            {
                CheckPropertyInited("BankKpp");
                return _bankkpp;
            }
            set
            {
                _bankkpp = value;
                NotifyPropertyChanged("BankKpp");
            }
        }


        private string _bankbic;
        /// <summary>
        /// 344000600 БИК банка (BIC)
        /// </summary>
        [RegisterAttribute(AttributeID = 344000600)]
        public string BankBic
        {
            get
            {
                CheckPropertyInited("BankBic");
                return _bankbic;
            }
            set
            {
                _bankbic = value;
                NotifyPropertyChanged("BankBic");
            }
        }


        private string _bankkoracc;
        /// <summary>
        /// 344000700 Корреспондентский счет (KOR_ACC)
        /// </summary>
        [RegisterAttribute(AttributeID = 344000700)]
        public string BankKorAcc
        {
            get
            {
                CheckPropertyInited("BankKorAcc");
                return _bankkoracc;
            }
            set
            {
                _bankkoracc = value;
                NotifyPropertyChanged("BankKorAcc");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 345 Справочник "Управляющие компании" (INSUR_SUBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 345)]
    [Serializable]
    public sealed partial class OMSubject : OMBaseClass<OMSubject>
    {

        private long _empid;
        /// <summary>
        /// 345000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 345000100)]
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


        private long? _okrugid;
        /// <summary>
        /// 345000200 Округ МФЦ (OKRUG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 345000200)]
        public long? OkrugId
        {
            get
            {
                CheckPropertyInited("OkrugId");
                return _okrugid;
            }
            set
            {
                _okrugid = value;
                NotifyPropertyChanged("OkrugId");
            }
        }


        private long? _kodorg;
        /// <summary>
        /// 345000300 Код организации (KOD_ORG)
        /// </summary>
        [RegisterAttribute(AttributeID = 345000300)]
        public long? KodOrg
        {
            get
            {
                CheckPropertyInited("KodOrg");
                return _kodorg;
            }
            set
            {
                _kodorg = value;
                NotifyPropertyChanged("KodOrg");
            }
        }


        private long? _kodupk;
        /// <summary>
        /// 345000400 Код управляющей компании (KOD_UPK)
        /// </summary>
        [RegisterAttribute(AttributeID = 345000400)]
        public long? KodUpk
        {
            get
            {
                CheckPropertyInited("KodUpk");
                return _kodupk;
            }
            set
            {
                _kodupk = value;
                NotifyPropertyChanged("KodUpk");
            }
        }


        private string _subjectname;
        /// <summary>
        /// 345000500 Название организации (SUBJECT_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 345000500)]
        public string SubjectName
        {
            get
            {
                CheckPropertyInited("SubjectName");
                return _subjectname;
            }
            set
            {
                _subjectname = value;
                NotifyPropertyChanged("SubjectName");
            }
        }


        private string _orgidt;
        /// <summary>
        /// 345000600 Идентификатор организации (ORG_ID_T)
        /// </summary>
        [RegisterAttribute(AttributeID = 345000600)]
        public string OrgIdT
        {
            get
            {
                CheckPropertyInited("OrgIdT");
                return _orgidt;
            }
            set
            {
                _orgidt = value;
                NotifyPropertyChanged("OrgIdT");
            }
        }


        private string _emplrole;
        /// <summary>
        /// 345000700 Должность руководителя (EMPL_ROLE)
        /// </summary>
        [RegisterAttribute(AttributeID = 345000700)]
        public string EmplRole
        {
            get
            {
                CheckPropertyInited("EmplRole");
                return _emplrole;
            }
            set
            {
                _emplrole = value;
                NotifyPropertyChanged("EmplRole");
            }
        }


        private string _fioadm;
        /// <summary>
        /// 345000800 ФИО руководителя (FIO_ADM)
        /// </summary>
        [RegisterAttribute(AttributeID = 345000800)]
        public string FioAdm
        {
            get
            {
                CheckPropertyInited("FioAdm");
                return _fioadm;
            }
            set
            {
                _fioadm = value;
                NotifyPropertyChanged("FioAdm");
            }
        }


        private string _orgadru;
        /// <summary>
        /// 345000900 Юридический адрес организации (ORG_ADR_U)
        /// </summary>
        [RegisterAttribute(AttributeID = 345000900)]
        public string OrgAdrU
        {
            get
            {
                CheckPropertyInited("OrgAdrU");
                return _orgadru;
            }
            set
            {
                _orgadru = value;
                NotifyPropertyChanged("OrgAdrU");
            }
        }


        private string _orgadrf;
        /// <summary>
        /// 345001000 Физический адрес организации (ORG_ADR_F)
        /// </summary>
        [RegisterAttribute(AttributeID = 345001000)]
        public string OrgAdrF
        {
            get
            {
                CheckPropertyInited("OrgAdrF");
                return _orgadrf;
            }
            set
            {
                _orgadrf = value;
                NotifyPropertyChanged("OrgAdrF");
            }
        }


        private string _orgphone;
        /// <summary>
        /// 345001100 Телефон организации (ORG_PHONE)
        /// </summary>
        [RegisterAttribute(AttributeID = 345001100)]
        public string OrgPhone
        {
            get
            {
                CheckPropertyInited("OrgPhone");
                return _orgphone;
            }
            set
            {
                _orgphone = value;
                NotifyPropertyChanged("OrgPhone");
            }
        }


        private DateTime? _dateinput;
        /// <summary>
        /// 345001200 Дата создания (DATE_INPUT)
        /// </summary>
        [RegisterAttribute(AttributeID = 345001200)]
        public DateTime? DateInput
        {
            get
            {
                CheckPropertyInited("DateInput");
                return _dateinput;
            }
            set
            {
                _dateinput = value;
                NotifyPropertyChanged("DateInput");
            }
        }


        private DateTime? _birthday;
        /// <summary>
        /// 345001300 Дата рождения (для физ лиц) (BIRTHDAY)
        /// </summary>
        [RegisterAttribute(AttributeID = 345001300)]
        public DateTime? Birthday
        {
            get
            {
                CheckPropertyInited("Birthday");
                return _birthday;
            }
            set
            {
                _birthday = value;
                NotifyPropertyChanged("Birthday");
            }
        }


        private string _inn;
        /// <summary>
        /// 345001400 ИНН (INN)
        /// </summary>
        [RegisterAttribute(AttributeID = 345001400)]
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
        /// 345001500 КПП (KPP)
        /// </summary>
        [RegisterAttribute(AttributeID = 345001500)]
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


        private string _rachacc;
        /// <summary>
        /// 345001600 Расчетный счет (RACH_ACC)
        /// </summary>
        [RegisterAttribute(AttributeID = 345001600)]
        public string RachAcc
        {
            get
            {
                CheckPropertyInited("RachAcc");
                return _rachacc;
            }
            set
            {
                _rachacc = value;
                NotifyPropertyChanged("RachAcc");
            }
        }


        private string _numcard;
        /// <summary>
        /// 345001800 Номер банковской карточки (NUM_CARD)
        /// </summary>
        [RegisterAttribute(AttributeID = 345001800)]
        public string NumCard
        {
            get
            {
                CheckPropertyInited("NumCard");
                return _numcard;
            }
            set
            {
                _numcard = value;
                NotifyPropertyChanged("NumCard");
            }
        }


        private string _nomdoc;
        /// <summary>
        /// 345001900 Номер документа, удостоверяющего личность (NOM_DOC)
        /// </summary>
        [RegisterAttribute(AttributeID = 345001900)]
        public string NomDoc
        {
            get
            {
                CheckPropertyInited("NomDoc");
                return _nomdoc;
            }
            set
            {
                _nomdoc = value;
                NotifyPropertyChanged("NomDoc");
            }
        }


        private DateTime? _datedoc;
        /// <summary>
        /// 345002000 Дата паспорта, удостоверяющего личность (DATE_DOC)
        /// </summary>
        [RegisterAttribute(AttributeID = 345002000)]
        public DateTime? DateDoc
        {
            get
            {
                CheckPropertyInited("DateDoc");
                return _datedoc;
            }
            set
            {
                _datedoc = value;
                NotifyPropertyChanged("DateDoc");
            }
        }


        private DateTime? _dateindoc;
        /// <summary>
        /// 345002100 Дата выдачи документа, удостоверяющего личность (DATE_IN_DOC)
        /// </summary>
        [RegisterAttribute(AttributeID = 345002100)]
        public DateTime? DateInDoc
        {
            get
            {
                CheckPropertyInited("DateInDoc");
                return _dateindoc;
            }
            set
            {
                _dateindoc = value;
                NotifyPropertyChanged("DateInDoc");
            }
        }


        private string _orgdoc;
        /// <summary>
        /// 345002200 Организация выдавшая документ, удостоверяющего личность (ORG_DOC)
        /// </summary>
        [RegisterAttribute(AttributeID = 345002200)]
        public string OrgDoc
        {
            get
            {
                CheckPropertyInited("OrgDoc");
                return _orgdoc;
            }
            set
            {
                _orgdoc = value;
                NotifyPropertyChanged("OrgDoc");
            }
        }


        private string _typesubject;
        /// <summary>
        /// 345002300 Тип субъекта (TYPE_SUBJECT)
        /// </summary>
        [RegisterAttribute(AttributeID = 345002300)]
        public string TypeSubject
        {
            get
            {
                CheckPropertyInited("TypeSubject");
                return _typesubject;
            }
            set
            {
                _typesubject = value;
                NotifyPropertyChanged("TypeSubject");
            }
        }


        private SubjectType _typesubject_Code;
        /// <summary>
        /// 345002300 Тип субъекта (справочный код) (TYPE_SUBJECT_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 345002300)]
        public SubjectType TypeSubject_Code
        {
            get
            {
                CheckPropertyInited("TypeSubject_Code");
                return this._typesubject_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typesubject))
                    {
                         _typesubject = descr;
                    }
                }
                else
                {
                     _typesubject = descr;
                }

                this._typesubject_Code = value;
                NotifyPropertyChanged("TypeSubject");
                NotifyPropertyChanged("TypeSubject_Code");
            }
        }


        private long? _bankid;
        /// <summary>
        /// 345002500 Банк (BANK_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 345002500)]
        public long? BankId
        {
            get
            {
                CheckPropertyInited("BankId");
                return _bankid;
            }
            set
            {
                _bankid = value;
                NotifyPropertyChanged("BankId");
            }
        }


        private string _nameforinvoice;
        /// <summary>
        /// 345002600 Название субъекта для счета (NAME_FOR_INVOICE)
        /// </summary>
        [RegisterAttribute(AttributeID = 345002600)]
        public string NameForInvoice
        {
            get
            {
                CheckPropertyInited("NameForInvoice");
                return _nameforinvoice;
            }
            set
            {
                _nameforinvoice = value;
                NotifyPropertyChanged("NameForInvoice");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 347 Справочник "Тарифы по страхованию общего имущества" (INSUR_COMMON_PROPERTY_TARIFF)
    /// </summary>
    [RegisterInfo(RegisterID = 347)]
    [Serializable]
    public sealed partial class OMCommonPropertyTariff : OMBaseClass<OMCommonPropertyTariff>
    {

        private long _id;
        /// <summary>
        /// 347000100 Уникальный номер записи (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 347000100)]
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


        private DateTime? _datebegin;
        /// <summary>
        /// 347000200 Дата начала действия (DATE_BEGIN)
        /// </summary>
        [RegisterAttribute(AttributeID = 347000200)]
        public DateTime? DateBegin
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


        private long? _category;
        /// <summary>
        /// 347000300 Категория (CATEGORY)
        /// </summary>
        [RegisterAttribute(AttributeID = 347000300)]
        public long? Category
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


        private decimal? _value;
        /// <summary>
        /// 347000400 Значение (VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 347000400)]
        public decimal? Value
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

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 348 Справочник «Страховая стоимость ЖП» (INSUR_LIVING_PREMISE_INSUR_COST)
    /// </summary>
    [RegisterInfo(RegisterID = 348)]
    [Serializable]
    public sealed partial class OMLivingPremiseInsurCost : OMBaseClass<OMLivingPremiseInsurCost>
    {

        private long _id;
        /// <summary>
        /// 348000100 Уникальный номер записи (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 348000100)]
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


        private DateTime? _datebegin;
        /// <summary>
        /// 348000200 Дата начала действия (DATE_BEGIN)
        /// </summary>
        [RegisterAttribute(AttributeID = 348000200)]
        public DateTime? DateBegin
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


        private string _condition;
        /// <summary>
        /// 348000300 Условие (CONDITION)
        /// </summary>
        [RegisterAttribute(AttributeID = 348000300)]
        public string Condition
        {
            get
            {
                CheckPropertyInited("Condition");
                return _condition;
            }
            set
            {
                _condition = value;
                NotifyPropertyChanged("Condition");
            }
        }


        private decimal? _value;
        /// <summary>
        /// 348000400 Значение (руб.) (VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 348000400)]
        public decimal? Value
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


        private decimal? _strahtarif;
        /// <summary>
        /// 348000500 Ставка ежемесячного страхового взноса за 1 кв м (STRAH_TARIF)
        /// </summary>
        [RegisterAttribute(AttributeID = 348000500)]
        public decimal? StrahTarif
        {
            get
            {
                CheckPropertyInited("StrahTarif");
                return _strahtarif;
            }
            set
            {
                _strahtarif = value;
                NotifyPropertyChanged("StrahTarif");
            }
        }


        private decimal? _strahbonus;
        /// <summary>
        /// 348000600 Страховая премия за 1 кв м (STRAH_BONUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 348000600)]
        public decimal? StrahBonus
        {
            get
            {
                CheckPropertyInited("StrahBonus");
                return _strahbonus;
            }
            set
            {
                _strahbonus = value;
                NotifyPropertyChanged("StrahBonus");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 349 Справочник "Доля ответственности СК и города" (INSUR_SHARE_RESPONSIBILITY_IC_CITY)
    /// </summary>
    [RegisterInfo(RegisterID = 349)]
    [Serializable]
    public sealed partial class OMShareResponsibilityICCity : OMBaseClass<OMShareResponsibilityICCity>
    {

        private long _id;
        /// <summary>
        /// 349000100 Уникальный номер записи (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 349000100)]
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


        private DateTime? _datebegin;
        /// <summary>
        /// 349000200 Дата начала  действия (DATE_BEGIN)
        /// </summary>
        [RegisterAttribute(AttributeID = 349000200)]
        public DateTime? DateBegin
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


        private string _type;
        /// <summary>
        /// 349000300 Тип (TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 349000300)]
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


        private long? _icshare;
        /// <summary>
        /// 349000400 Доля СК,% (IC_SHARE)
        /// </summary>
        [RegisterAttribute(AttributeID = 349000400)]
        public long? ICShare
        {
            get
            {
                CheckPropertyInited("ICShare");
                return _icshare;
            }
            set
            {
                _icshare = value;
                NotifyPropertyChanged("ICShare");
            }
        }


        private long? _cityshare;
        /// <summary>
        /// 349000500 Доля города,% (CITY_SHARE)
        /// </summary>
        [RegisterAttribute(AttributeID = 349000500)]
        public long? CityShare
        {
            get
            {
                CheckPropertyInited("CityShare");
                return _cityshare;
            }
            set
            {
                _cityshare = value;
                NotifyPropertyChanged("CityShare");
            }
        }


        private string _note;
        /// <summary>
        /// 349000600 Примечание (NOTE)
        /// </summary>
        [RegisterAttribute(AttributeID = 349000600)]
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

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 350 Реестр расчетов ущерба по элементам конструкций (INSUR_DAMAGE_AMOUNT)
    /// </summary>
    [RegisterInfo(RegisterID = 350)]
    [Serializable]
    public sealed partial class OMDamageAmount : OMBaseClass<OMDamageAmount>
    {

        private long _empid;
        /// <summary>
        /// 350000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 350000100)]
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


        private long _damageid;
        /// <summary>
        /// 350000200 Ссылка на дело (INSUR_DAMAGE) (DAMAGE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 350000200)]
        public long DamageId
        {
            get
            {
                CheckPropertyInited("DamageId");
                return _damageid;
            }
            set
            {
                _damageid = value;
                NotifyPropertyChanged("DamageId");
            }
        }


        private long? _damageassessmentmethodid;
        /// <summary>
        /// 350000300 Ссылка на справочник "Методики расчета ущерба" (DAMAGE_ASSESSMENT_METHOD_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 350000300)]
        public long? DamageAssessmentMethodId
        {
            get
            {
                CheckPropertyInited("DamageAssessmentMethodId");
                return _damageassessmentmethodid;
            }
            set
            {
                _damageassessmentmethodid = value;
                NotifyPropertyChanged("DamageAssessmentMethodId");
            }
        }


        private decimal? _materialdamage;
        /// <summary>
        /// 350000400 Материальный ущерб (MATERIAL_DAMAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 350000400)]
        public decimal? MaterialDamage
        {
            get
            {
                CheckPropertyInited("MaterialDamage");
                return _materialdamage;
            }
            set
            {
                _materialdamage = value;
                NotifyPropertyChanged("MaterialDamage");
            }
        }


        private decimal? _proportionreplacementcost;
        /// <summary>
        /// 350000500 Удельный вес восстановительной стоимости (PROPORTION_REPLACEMENT_COST)
        /// </summary>
        [RegisterAttribute(AttributeID = 350000500)]
        public decimal? ProportionReplacementCost
        {
            get
            {
                CheckPropertyInited("ProportionReplacementCost");
                return _proportionreplacementcost;
            }
            set
            {
                _proportionreplacementcost = value;
                NotifyPropertyChanged("ProportionReplacementCost");
            }
        }


        private decimal? _proportiondamagedarea;
        /// <summary>
        /// 350000600 Удельный вес поврежденного участка (PROPORTION_DAMAGED_AREA)
        /// </summary>
        [RegisterAttribute(AttributeID = 350000600)]
        public decimal? ProportionDamagedArea
        {
            get
            {
                CheckPropertyInited("ProportionDamagedArea");
                return _proportiondamagedarea;
            }
            set
            {
                _proportiondamagedarea = value;
                NotifyPropertyChanged("ProportionDamagedArea");
            }
        }


        private decimal? _damageamount;
        /// <summary>
        /// 350000700 Сумма ущерба (DAMAGE_AMOUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 350000700)]
        public decimal? DamageAmount
        {
            get
            {
                CheckPropertyInited("DamageAmount");
                return _damageamount;
            }
            set
            {
                _damageamount = value;
                NotifyPropertyChanged("DamageAmount");
            }
        }


        private string _elementconstruction;
        /// <summary>
        /// 350000800 Элемент конструкции (ELEMENT_CONSTRUCTION)
        /// </summary>
        [RegisterAttribute(AttributeID = 350000800)]
        public string ElementConstruction
        {
            get
            {
                CheckPropertyInited("ElementConstruction");
                return _elementconstruction;
            }
            set
            {
                _elementconstruction = value;
                NotifyPropertyChanged("ElementConstruction");
            }
        }


        private ElementsOfConstructions _elementconstruction_Code;
        /// <summary>
        /// 350000800 Элемент конструкции (справочный код) (ELEMENT_CONSTRUCTION_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 350000800)]
        public ElementsOfConstructions ElementConstruction_Code
        {
            get
            {
                CheckPropertyInited("ElementConstruction_Code");
                return this._elementconstruction_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_elementconstruction))
                    {
                         _elementconstruction = descr;
                    }
                }
                else
                {
                     _elementconstruction = descr;
                }

                this._elementconstruction_Code = value;
                NotifyPropertyChanged("ElementConstruction");
                NotifyPropertyChanged("ElementConstruction_Code");
            }
        }


        private decimal? _correction;
        /// <summary>
        /// 350000900 Поправочный коэффициент (CORRECTION)
        /// </summary>
        [RegisterAttribute(AttributeID = 350000900)]
        public decimal? Correction
        {
            get
            {
                CheckPropertyInited("Correction");
                return _correction;
            }
            set
            {
                _correction = value;
                NotifyPropertyChanged("Correction");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 351 Справочник «Страховой тариф» (INSUR_TARIFF)
    /// </summary>
    [RegisterInfo(RegisterID = 351)]
    [Serializable]
    public sealed partial class OMTariff : OMBaseClass<OMTariff>
    {

        private long _id;
        /// <summary>
        /// 351000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 351000100)]
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


        private DateTime? _datebegin;
        /// <summary>
        /// 351000200 Дата начала действия тарифа (DATE_BEGIN)
        /// </summary>
        [RegisterAttribute(AttributeID = 351000200)]
        public DateTime? DateBegin
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


        private decimal? _value;
        /// <summary>
        /// 351000300 Размер тарифа (VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 351000300)]
        public decimal? Value
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

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 352 Реестр регистрации изменения данных (INSUR_CHANGES_LOG)
    /// </summary>
    [RegisterInfo(RegisterID = 352)]
    [Serializable]
    public sealed partial class OMChangesLog : OMBaseClass<OMChangesLog>
    {

        private long _empid;
        /// <summary>
        /// 352000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 352000100)]
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


        private long? _objectid;
        /// <summary>
        /// 352000200 ID-записи, в которую вносятся изменения (OBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 352000200)]
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


        private long? _reestrid;
        /// <summary>
        /// 352000300 Номер реестра  для записи, в которую вносятся изменения (REESTR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 352000300)]
        public long? ReestrId
        {
            get
            {
                CheckPropertyInited("ReestrId");
                return _reestrid;
            }
            set
            {
                _reestrid = value;
                NotifyPropertyChanged("ReestrId");
            }
        }


        private DateTime? _loaddate;
        /// <summary>
        /// 352000400 Дата внесения изменения  (LOAD_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 352000400)]
        public DateTime? LoadDate
        {
            get
            {
                CheckPropertyInited("LoadDate");
                return _loaddate;
            }
            set
            {
                _loaddate = value;
                NotifyPropertyChanged("LoadDate");
            }
        }


        private string _operationtype;
        /// <summary>
        /// 352000500 Тип операции из справочника «Типы операций» (OPERATION_TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 352000500)]
        public string OperationType
        {
            get
            {
                CheckPropertyInited("OperationType");
                return _operationtype;
            }
            set
            {
                _operationtype = value;
                NotifyPropertyChanged("OperationType");
            }
        }


        private ChangeOperationType _operationtype_Code;
        /// <summary>
        /// 352000500 Тип операции из справочника «Типы операций» (справочный код) (OPERATION_TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 352000500)]
        public ChangeOperationType OperationType_Code
        {
            get
            {
                CheckPropertyInited("OperationType_Code");
                return this._operationtype_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_operationtype))
                    {
                         _operationtype = descr;
                    }
                }
                else
                {
                     _operationtype = descr;
                }

                this._operationtype_Code = value;
                NotifyPropertyChanged("OperationType");
                NotifyPropertyChanged("OperationType_Code");
            }
        }


        private string _reason;
        /// <summary>
        /// 352000600 Комментарий: причина изменения данных (REASON)
        /// </summary>
        [RegisterAttribute(AttributeID = 352000600)]
        public string Reason
        {
            get
            {
                CheckPropertyInited("Reason");
                return _reason;
            }
            set
            {
                _reason = value;
                NotifyPropertyChanged("Reason");
            }
        }


        private long? _userid;
        /// <summary>
        /// 352000700 Идентификатор пользователя (USER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 352000700)]
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


        private string _oldvalue;
        /// <summary>
        /// 352000800 Старое значение (OLD_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 352000800)]
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
        /// 352000900 Новое значение (NEW_VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 352000900)]
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

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 353 Справочник "Коэффициент пересчета действительной стоимости" (INSUR_ACTUAL_COST_RATIO)
    /// </summary>
    [RegisterInfo(RegisterID = 353)]
    [Serializable]
    public sealed partial class OMActualCostRatio : OMBaseClass<OMActualCostRatio>
    {

        private long _id;
        /// <summary>
        /// 353000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 353000100)]
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


        private DateTime? _datebegin;
        /// <summary>
        /// 353000200 Дата начала действия (DATE_BEGIN)
        /// </summary>
        [RegisterAttribute(AttributeID = 353000200)]
        public DateTime? DateBegin
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


        private decimal? _value;
        /// <summary>
        /// 353000300 Значение коэффициента (VALUE)
        /// </summary>
        [RegisterAttribute(AttributeID = 353000300)]
        public decimal? Value
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

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 354 Реестр оплат в системе ОПС (INSUR_REESTR_PAY)
    /// </summary>
    [RegisterInfo(RegisterID = 354)]
    [Serializable]
    public sealed partial class OMReestrPay : OMBaseClass<OMReestrPay>
    {

        private long _empid;
        /// <summary>
        /// 354000100 Уникальный номер (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 354000100)]
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


        private string _num;
        /// <summary>
        /// 354000200 Номер реестра оплат (NUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 354000200)]
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
        /// 354000300 Дата реестра оплат (DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 354000300)]
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


        private DateTime? _datacreation;
        /// <summary>
        /// 354000400 Дата создания (DATA_CREATION)
        /// </summary>
        [RegisterAttribute(AttributeID = 354000400)]
        public DateTime? DataCreation
        {
            get
            {
                CheckPropertyInited("DataCreation");
                return _datacreation;
            }
            set
            {
                _datacreation = value;
                NotifyPropertyChanged("DataCreation");
            }
        }


        private DateTime? _datapayment;
        /// <summary>
        /// 354000500 Дата оплаты (DATA_PAYMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 354000500)]
        public DateTime? DataPayment
        {
            get
            {
                CheckPropertyInited("DataPayment");
                return _datapayment;
            }
            set
            {
                _datapayment = value;
                NotifyPropertyChanged("DataPayment");
            }
        }


        private string _usercreation;
        /// <summary>
        /// 354000600 Пользователь, создавший реестр оплат (USER_CREATION)
        /// </summary>
        [RegisterAttribute(AttributeID = 354000600)]
        public string UserCreation
        {
            get
            {
                CheckPropertyInited("UserCreation");
                return _usercreation;
            }
            set
            {
                _usercreation = value;
                NotifyPropertyChanged("UserCreation");
            }
        }


        private string _userpayment;
        /// <summary>
        /// 354000700 Пользователь, который ввел информацию об оплате (USER_PAYMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 354000700)]
        public string UserPayment
        {
            get
            {
                CheckPropertyInited("UserPayment");
                return _userpayment;
            }
            set
            {
                _userpayment = value;
                NotifyPropertyChanged("UserPayment");
            }
        }


        private string _status;
        /// <summary>
        /// 354000800 Статус реестра оплат (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 354000800)]
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


        private ReestrPayStatus _status_Code;
        /// <summary>
        /// 354000800 Статус реестра оплат (справочный код) (STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 354000800)]
        public ReestrPayStatus Status_Code
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
        /// 354000900 Тип реестра оплат (TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 354000900)]
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


        private ReestrPayType _type_Code;
        /// <summary>
        /// 354000900 Тип реестра оплат (справочный код) (TYPE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 354000900)]
        public ReestrPayType Type_Code
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


        private string _note;
        /// <summary>
        /// 354001000 Примечание (NOTE)
        /// </summary>
        [RegisterAttribute(AttributeID = 354001000)]
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


        private long? _filestorageiddgi;
        /// <summary>
        /// 354001100 Ссылка на файл для статуса "Утвержден в ДГИ" (FILE_STORAGE_ID_DGI)
        /// </summary>
        [RegisterAttribute(AttributeID = 354001100)]
        public long? FileStorageIdDGI
        {
            get
            {
                CheckPropertyInited("FileStorageIdDGI");
                return _filestorageiddgi;
            }
            set
            {
                _filestorageiddgi = value;
                NotifyPropertyChanged("FileStorageIdDGI");
            }
        }


        private long? _filestorageidpay;
        /// <summary>
        /// 354001200 Ссылка на файл для статуса "Передано в оплату" (FILE_STORAGE_ID_PAY)
        /// </summary>
        [RegisterAttribute(AttributeID = 354001200)]
        public long? FileStorageIdPay
        {
            get
            {
                CheckPropertyInited("FileStorageIdPay");
                return _filestorageidpay;
            }
            set
            {
                _filestorageidpay = value;
                NotifyPropertyChanged("FileStorageIdPay");
            }
        }


        private DateTime? _datecancel;
        /// <summary>
        /// 354001300 Дата расформирования реестра (DATE_CANCEL)
        /// </summary>
        [RegisterAttribute(AttributeID = 354001300)]
        public DateTime? DateCancel
        {
            get
            {
                CheckPropertyInited("DateCancel");
                return _datecancel;
            }
            set
            {
                _datecancel = value;
                NotifyPropertyChanged("DateCancel");
            }
        }


        private long? _canceluserid;
        /// <summary>
        /// 354001400 Пользователь, расформировавший реестр (CANCEL_USER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 354001400)]
        public long? CancelUserId
        {
            get
            {
                CheckPropertyInited("CancelUserId");
                return _canceluserid;
            }
            set
            {
                _canceluserid = value;
                NotifyPropertyChanged("CancelUserId");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 355 Реестр счетов (INSUR_INVOICE)
    /// </summary>
    [RegisterInfo(RegisterID = 355)]
    [Serializable]
    public sealed partial class OMInvoice : OMBaseClass<OMInvoice>
    {

        private long _empid;
        /// <summary>
        /// 355000100 Уникальный номер (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 355000100)]
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


        private string _subjectname;
        /// <summary>
        /// 355000200 Получатель выплаты (SUBJECT_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 355000200)]
        public string SubjectName
        {
            get
            {
                CheckPropertyInited("SubjectName");
                return _subjectname;
            }
            set
            {
                _subjectname = value;
                NotifyPropertyChanged("SubjectName");
            }
        }


        private string _phone;
        /// <summary>
        /// 355000300 Телефон (PHONE)
        /// </summary>
        [RegisterAttribute(AttributeID = 355000300)]
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


        private DateTime? _datainput;
        /// <summary>
        /// 355000400 Дата создания (DATA_INPUT)
        /// </summary>
        [RegisterAttribute(AttributeID = 355000400)]
        public DateTime? DataInput
        {
            get
            {
                CheckPropertyInited("DataInput");
                return _datainput;
            }
            set
            {
                _datainput = value;
                NotifyPropertyChanged("DataInput");
            }
        }


        private string _inn;
        /// <summary>
        /// 355000500 ИНН (INN)
        /// </summary>
        [RegisterAttribute(AttributeID = 355000500)]
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
        /// 355000600 КПП (KPP)
        /// </summary>
        [RegisterAttribute(AttributeID = 355000600)]
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


        private string _bicbank;
        /// <summary>
        /// 355000700 Бик банка (BIC_BANK)
        /// </summary>
        [RegisterAttribute(AttributeID = 355000700)]
        public string BicBank
        {
            get
            {
                CheckPropertyInited("BicBank");
                return _bicbank;
            }
            set
            {
                _bicbank = value;
                NotifyPropertyChanged("BicBank");
            }
        }


        private string _bankname;
        /// <summary>
        /// 355000800 Название банка (BANK_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 355000800)]
        public string BankName
        {
            get
            {
                CheckPropertyInited("BankName");
                return _bankname;
            }
            set
            {
                _bankname = value;
                NotifyPropertyChanged("BankName");
            }
        }


        private string _koracc;
        /// <summary>
        /// 355000900 Корреспондентский счет (KOR_ACC)
        /// </summary>
        [RegisterAttribute(AttributeID = 355000900)]
        public string KorAcc
        {
            get
            {
                CheckPropertyInited("KorAcc");
                return _koracc;
            }
            set
            {
                _koracc = value;
                NotifyPropertyChanged("KorAcc");
            }
        }


        private string _rachacc;
        /// <summary>
        /// 355001000 Расчетный счет (RACH_ACC)
        /// </summary>
        [RegisterAttribute(AttributeID = 355001000)]
        public string RachAcc
        {
            get
            {
                CheckPropertyInited("RachAcc");
                return _rachacc;
            }
            set
            {
                _rachacc = value;
                NotifyPropertyChanged("RachAcc");
            }
        }


        private string _numcard;
        /// <summary>
        /// 355001200 Номер банковской карточки (NUM_CARD)
        /// </summary>
        [RegisterAttribute(AttributeID = 355001200)]
        public string NumCard
        {
            get
            {
                CheckPropertyInited("NumCard");
                return _numcard;
            }
            set
            {
                _numcard = value;
                NotifyPropertyChanged("NumCard");
            }
        }


        private long? _linkdamage;
        /// <summary>
        /// 355001300 Ссылка на INSUR_DAMAGE ( Реестр дел по расчету ущербов) (LINK_DAMAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 355001300)]
        public long? LinkDamage
        {
            get
            {
                CheckPropertyInited("LinkDamage");
                return _linkdamage;
            }
            set
            {
                _linkdamage = value;
                NotifyPropertyChanged("LinkDamage");
            }
        }


        private long? _linkallproperty;
        /// <summary>
        /// 355001400 Ссылка на INSUR_ALL_PROPERTY (Реестр договоров по общему имуществу) (LINK_ALL_PROPERTY)
        /// </summary>
        [RegisterAttribute(AttributeID = 355001400)]
        public long? LinkAllProperty
        {
            get
            {
                CheckPropertyInited("LinkAllProperty");
                return _linkallproperty;
            }
            set
            {
                _linkallproperty = value;
                NotifyPropertyChanged("LinkAllProperty");
            }
        }


        private long? _linkreestrpay;
        /// <summary>
        /// 355001500 Ссылка на INSUR_REESTR_PAY ( Реестр оплат ) (LINK_REESTR_PAY)
        /// </summary>
        [RegisterAttribute(AttributeID = 355001500)]
        public long? LinkReestrPay
        {
            get
            {
                CheckPropertyInited("LinkReestrPay");
                return _linkreestrpay;
            }
            set
            {
                _linkreestrpay = value;
                NotifyPropertyChanged("LinkReestrPay");
            }
        }


        private decimal? _sumopl;
        /// <summary>
        /// 355001600 Сумма выплаты для Реестра оплат доли города (SUM_OPL)
        /// </summary>
        [RegisterAttribute(AttributeID = 355001600)]
        public decimal? SumOpl
        {
            get
            {
                CheckPropertyInited("SumOpl");
                return _sumopl;
            }
            set
            {
                _sumopl = value;
                NotifyPropertyChanged("SumOpl");
            }
        }


        private long? _linkfsp;
        /// <summary>
        /// 355001700 Ссылка на INSUR_FSP ( Реестр ФСП) (LINK_FSP)
        /// </summary>
        [RegisterAttribute(AttributeID = 355001700)]
        public long? LinkFsp
        {
            get
            {
                CheckPropertyInited("LinkFsp");
                return _linkfsp;
            }
            set
            {
                _linkfsp = value;
                NotifyPropertyChanged("LinkFsp");
            }
        }


        private string _status;
        /// <summary>
        /// 355001800 Статус записи (STATUS_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 355001800)]
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


        private InvoiceStatus _status_Code;
        /// <summary>
        /// 355001800 Статус записи (справочный код) (STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 355001800)]
        public InvoiceStatus Status_Code
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


        private long? _notenopayid;
        /// <summary>
        /// 355001900 Причина отказа (NOTE_NO_PAY_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 355001900)]
        public long? NoteNoPayId
        {
            get
            {
                CheckPropertyInited("NoteNoPayId");
                return _notenopayid;
            }
            set
            {
                _notenopayid = value;
                NotifyPropertyChanged("NoteNoPayId");
            }
        }


        private long? _userid;
        /// <summary>
        /// 355002000 Пользователь, создавший счет (USER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 355002000)]
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


        private string _comment;
        /// <summary>
        /// 355002100 Комментарий (COMMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 355002100)]
        public string Comment
        {
            get
            {
                CheckPropertyInited("Comment");
                return _comment;
            }
            set
            {
                _comment = value;
                NotifyPropertyChanged("Comment");
            }
        }


        private string _innbank;
        /// <summary>
        /// 355002200 ИНН банка (INN_BANK)
        /// </summary>
        [RegisterAttribute(AttributeID = 355002200)]
        public string InnBank
        {
            get
            {
                CheckPropertyInited("InnBank");
                return _innbank;
            }
            set
            {
                _innbank = value;
                NotifyPropertyChanged("InnBank");
            }
        }


        private string _kppbank;
        /// <summary>
        /// 355002300 КПП банка (KPP_BANK)
        /// </summary>
        [RegisterAttribute(AttributeID = 355002300)]
        public string KppBank
        {
            get
            {
                CheckPropertyInited("KppBank");
                return _kppbank;
            }
            set
            {
                _kppbank = value;
                NotifyPropertyChanged("KppBank");
            }
        }


        private DateTime? _dateagree;
        /// <summary>
        /// 355002400 Дата согласования (DATE_AGREE)
        /// </summary>
        [RegisterAttribute(AttributeID = 355002400)]
        public DateTime? DateAgree
        {
            get
            {
                CheckPropertyInited("DateAgree");
                return _dateagree;
            }
            set
            {
                _dateagree = value;
                NotifyPropertyChanged("DateAgree");
            }
        }


        private long? _useragreeid;
        /// <summary>
        /// 355002500 Пользователь (USER_AGREE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 355002500)]
        public long? UserAgreeId
        {
            get
            {
                CheckPropertyInited("UserAgreeId");
                return _useragreeid;
            }
            set
            {
                _useragreeid = value;
                NotifyPropertyChanged("UserAgreeId");
            }
        }


        private string _numinvoice;
        /// <summary>
        /// 355002600 Номер счета (NUM_INVOICE)
        /// </summary>
        [RegisterAttribute(AttributeID = 355002600)]
        public string NumInvoice
        {
            get
            {
                CheckPropertyInited("NumInvoice");
                return _numinvoice;
            }
            set
            {
                _numinvoice = value;
                NotifyPropertyChanged("NumInvoice");
            }
        }


        private DateTime? _dateinvoice;
        /// <summary>
        /// 355002700 Дата счета (DATE_INVOICE)
        /// </summary>
        [RegisterAttribute(AttributeID = 355002700)]
        public DateTime? dateInvoice
        {
            get
            {
                CheckPropertyInited("dateInvoice");
                return _dateinvoice;
            }
            set
            {
                _dateinvoice = value;
                NotifyPropertyChanged("dateInvoice");
            }
        }


        private long? _subjectid;
        /// <summary>
        /// 355002800 Ссылка на субъект-получатель (SUBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 355002800)]
        public long? SubjectId
        {
            get
            {
                CheckPropertyInited("SubjectId");
                return _subjectid;
            }
            set
            {
                _subjectid = value;
                NotifyPropertyChanged("SubjectId");
            }
        }


        private long? _bankid;
        /// <summary>
        /// 355002900 Ссылка на банк (BANK_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 355002900)]
        public long? BankId
        {
            get
            {
                CheckPropertyInited("BankId");
                return _bankid;
            }
            set
            {
                _bankid = value;
                NotifyPropertyChanged("BankId");
            }
        }


        private long? _contractid;
        /// <summary>
        /// 355003000 Ссылка на номер договора или в INSUR_POLICY_SVD   или INSUR_ALL_PROPERTY (CONTRACT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 355003000)]
        public long? ContractId
        {
            get
            {
                CheckPropertyInited("ContractId");
                return _contractid;
            }
            set
            {
                _contractid = value;
                NotifyPropertyChanged("ContractId");
            }
        }


        private long? _reestrcontractid;
        /// <summary>
        /// 355003100 Номер реестра  или  INSUR_POLICY_SVD=309 ,  INSUR_ALL_PROPERTY= ?- (REESTR_CONTRACT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 355003100)]
        public long? ReestrContractId
        {
            get
            {
                CheckPropertyInited("ReestrContractId");
                return _reestrcontractid;
            }
            set
            {
                _reestrcontractid = value;
                NotifyPropertyChanged("ReestrContractId");
            }
        }


        private decimal? _partdog;
        /// <summary>
        /// 355003200 Доля в праве (PART_DOG)
        /// </summary>
        [RegisterAttribute(AttributeID = 355003200)]
        public decimal? PartDog
        {
            get
            {
                CheckPropertyInited("PartDog");
                return _partdog;
            }
            set
            {
                _partdog = value;
                NotifyPropertyChanged("PartDog");
            }
        }


        private string _svidpolycnum;
        /// <summary>
        /// 355003300 Свидетельство/Полис (SVID_POLYC_NUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 355003300)]
        public string SvidPolycNum
        {
            get
            {
                CheckPropertyInited("SvidPolycNum");
                return _svidpolycnum;
            }
            set
            {
                _svidpolycnum = value;
                NotifyPropertyChanged("SvidPolycNum");
            }
        }


        private DateTime? _svdpolycedate;
        /// <summary>
        /// 355003400 Свидетельство/Полис дата (SVD_POLYCE_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 355003400)]
        public DateTime? SvdPolyceDate
        {
            get
            {
                CheckPropertyInited("SvdPolyceDate");
                return _svdpolycedate;
            }
            set
            {
                _svdpolycedate = value;
                NotifyPropertyChanged("SvdPolyceDate");
            }
        }


        private decimal? _sort;
        /// <summary>
        /// 355003500 Сортировка для Заключения (SORT)
        /// </summary>
        [RegisterAttribute(AttributeID = 355003500)]
        public decimal? Sort
        {
            get
            {
                CheckPropertyInited("Sort");
                return _sort;
            }
            set
            {
                _sort = value;
                NotifyPropertyChanged("Sort");
            }
        }


        private string _useragree;
        /// <summary>
        /// 355003600 Пользователь, согласовавший счет ()
        /// </summary>
        [RegisterAttribute(AttributeID = 355003600)]
        public string UserAgree
        {
            get
            {
                CheckPropertyInited("UserAgree");
                return _useragree;
            }
            set
            {
                _useragree = value;
                NotifyPropertyChanged("UserAgree");
            }
        }


        private decimal? _insursymmaotchet;
        /// <summary>
        /// 355003700 Расчетная стоимость для заключения (INSUR_SYMMA_OTCHET)
        /// </summary>
        [RegisterAttribute(AttributeID = 355003700)]
        public decimal? InsurSymmaOtchet
        {
            get
            {
                CheckPropertyInited("InsurSymmaOtchet");
                return _insursymmaotchet;
            }
            set
            {
                _insursymmaotchet = value;
                NotifyPropertyChanged("InsurSymmaOtchet");
            }
        }


        private DateTime? _datazakluchenia;
        /// <summary>
        /// 355003800 Дата выпуска заключения (DATE_ZACKLUCHENIA)
        /// </summary>
        [RegisterAttribute(AttributeID = 355003800)]
        public DateTime? DataZakluchenia
        {
            get
            {
                CheckPropertyInited("DataZakluchenia");
                return _datazakluchenia;
            }
            set
            {
                _datazakluchenia = value;
                NotifyPropertyChanged("DataZakluchenia");
            }
        }


        private long? _linkinvoicesvod;
        /// <summary>
        /// 355003900 Ссылка на сводный реестр (LINK_INVOICE_SVOD)
        /// </summary>
        [RegisterAttribute(AttributeID = 355003900)]
        public long? LinkInvoiceSvod
        {
            get
            {
                CheckPropertyInited("LinkInvoiceSvod");
                return _linkinvoicesvod;
            }
            set
            {
                _linkinvoicesvod = value;
                NotifyPropertyChanged("LinkInvoiceSvod");
            }
        }


        private string _kbk;
        /// <summary>
        /// 355004100 КБК (KBK)
        /// </summary>
        [RegisterAttribute(AttributeID = 355004100)]
        public string Kbk
        {
            get
            {
                CheckPropertyInited("Kbk");
                return _kbk;
            }
            set
            {
                _kbk = value;
                NotifyPropertyChanged("Kbk");
            }
        }


        private string _oktmo;
        /// <summary>
        /// 355004200 ОКТМО (OKTMO)
        /// </summary>
        [RegisterAttribute(AttributeID = 355004200)]
        public string Oktmo
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

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 356 Реестр связи справочник причин ущерба и подпричн для ЖП (INSUR_LINK_CAUSES_SUBREASON_LP)
    /// </summary>
    [RegisterInfo(RegisterID = 356)]
    [Serializable]
    public sealed partial class OMLinkCausesSubreasonLP : OMBaseClass<OMLinkCausesSubreasonLP>
    {

        private long _id;
        /// <summary>
        /// 356000100 Уникальный номер записи (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 356000100)]
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


        private string _causesofdamage;
        /// <summary>
        /// 356000200 Причина ущерба по ЖП (справочник, 12125) (CAUSES_OF_DAMAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 356000200)]
        public string CausesOfDamage
        {
            get
            {
                CheckPropertyInited("CausesOfDamage");
                return _causesofdamage;
            }
            set
            {
                _causesofdamage = value;
                NotifyPropertyChanged("CausesOfDamage");
            }
        }


        private CausesOfDamageGP _causesofdamage_Code;
        /// <summary>
        /// 356000200 Причина ущерба по ЖП (справочник, 12125) (справочный код) (CAUSES_OF_DAMAGE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 356000200)]
        public CausesOfDamageGP CausesOfDamage_Code
        {
            get
            {
                CheckPropertyInited("CausesOfDamage_Code");
                return this._causesofdamage_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_causesofdamage))
                    {
                         _causesofdamage = descr;
                    }
                }
                else
                {
                     _causesofdamage = descr;
                }

                this._causesofdamage_Code = value;
                NotifyPropertyChanged("CausesOfDamage");
                NotifyPropertyChanged("CausesOfDamage_Code");
            }
        }


        private string _subresoncausesofdamage;
        /// <summary>
        /// 356000300 Подпричины ущерба по ЖП (справочник, 12134) (SUBREASON_CAUSES_OF_DAMAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 356000300)]
        public string SubresonCausesOfDamage
        {
            get
            {
                CheckPropertyInited("SubresonCausesOfDamage");
                return _subresoncausesofdamage;
            }
            set
            {
                _subresoncausesofdamage = value;
                NotifyPropertyChanged("SubresonCausesOfDamage");
            }
        }


        private SubReasonCausesOfDamage _subresoncausesofdamage_Code;
        /// <summary>
        /// 356000300 Подпричины ущерба по ЖП (справочник, 12134) (справочный код) (SUBREASON_CAUSES_OF_DAMAGE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 356000300)]
        public SubReasonCausesOfDamage SubresonCausesOfDamage_Code
        {
            get
            {
                CheckPropertyInited("SubresonCausesOfDamage_Code");
                return this._subresoncausesofdamage_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_subresoncausesofdamage))
                    {
                         _subresoncausesofdamage = descr;
                    }
                }
                else
                {
                     _subresoncausesofdamage = descr;
                }

                this._subresoncausesofdamage_Code = value;
                NotifyPropertyChanged("SubresonCausesOfDamage");
                NotifyPropertyChanged("SubresonCausesOfDamage_Code");
            }
        }


        private string _refinementsubreason;
        /// <summary>
        /// 356000400 Уточнение Подпричины ущерба (справочник, 12135) (REFINEMENT_SUBREASON)
        /// </summary>
        [RegisterAttribute(AttributeID = 356000400)]
        public string RefinementSubreason
        {
            get
            {
                CheckPropertyInited("RefinementSubreason");
                return _refinementsubreason;
            }
            set
            {
                _refinementsubreason = value;
                NotifyPropertyChanged("RefinementSubreason");
            }
        }


        private RefinementSubReasonCOD _refinementsubreason_Code;
        /// <summary>
        /// 356000400 Уточнение Подпричины ущерба (справочник, 12135) (справочный код) (REFINEMENT_SUBREASON_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 356000400)]
        public RefinementSubReasonCOD RefinementSubreason_Code
        {
            get
            {
                CheckPropertyInited("RefinementSubreason_Code");
                return this._refinementsubreason_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_refinementsubreason))
                    {
                         _refinementsubreason = descr;
                    }
                }
                else
                {
                     _refinementsubreason = descr;
                }

                this._refinementsubreason_Code = value;
                NotifyPropertyChanged("RefinementSubreason");
                NotifyPropertyChanged("RefinementSubreason_Code");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 357 Справочник "Причины отказа в выплате ущерба ГБУ" (INSUR_GBU_NO_PAY_REASON)
    /// </summary>
    [RegisterInfo(RegisterID = 357)]
    [Serializable]
    public sealed partial class OMGbuNoPayReason : OMBaseClass<OMGbuNoPayReason>
    {

        private long _id;
        /// <summary>
        /// 357000100 Уникальный номер записи (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 357000100)]
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


        private string _reason;
        /// <summary>
        /// 357000200 Причина (REASON)
        /// </summary>
        [RegisterAttribute(AttributeID = 357000200)]
        public string Reason
        {
            get
            {
                CheckPropertyInited("Reason");
                return _reason;
            }
            set
            {
                _reason = value;
                NotifyPropertyChanged("Reason");
            }
        }


        private string _typeinsur;
        /// <summary>
        /// 357000300 Вид страхования (TYPE_INSUR)
        /// </summary>
        [RegisterAttribute(AttributeID = 357000300)]
        public string TypeInsur
        {
            get
            {
                CheckPropertyInited("TypeInsur");
                return _typeinsur;
            }
            set
            {
                _typeinsur = value;
                NotifyPropertyChanged("TypeInsur");
            }
        }


        private string _shortexplanation;
        /// <summary>
        /// 357000400 Краткое пояснение (должно быть напечатано на заключении) (SHORT_EXPLANATION)
        /// </summary>
        [RegisterAttribute(AttributeID = 357000400)]
        public string ShortExplanation
        {
            get
            {
                CheckPropertyInited("ShortExplanation");
                return _shortexplanation;
            }
            set
            {
                _shortexplanation = value;
                NotifyPropertyChanged("ShortExplanation");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 358 Комментарии (INSUR_COMMENT)
    /// </summary>
    [RegisterInfo(RegisterID = 358)]
    [Serializable]
    public sealed partial class OMComment : OMBaseClass<OMComment>
    {

        private long _id;
        /// <summary>
        /// 358000100 Уникальный номер записи (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 358000100)]
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


        private string _comment;
        /// <summary>
        /// 358000200 Комментарий (COMMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 358000200)]
        public string Comment
        {
            get
            {
                CheckPropertyInited("Comment");
                return _comment;
            }
            set
            {
                _comment = value;
                NotifyPropertyChanged("Comment");
            }
        }


        private long? _userid;
        /// <summary>
        /// 358000300 Пользователь (USER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 358000300)]
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


        private DateTime? _datecreate;
        /// <summary>
        /// 358000400 Дата создания (DATE_CREATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 358000400)]
        public DateTime? DateCreate
        {
            get
            {
                CheckPropertyInited("DateCreate");
                return _datecreate;
            }
            set
            {
                _datecreate = value;
                NotifyPropertyChanged("DateCreate");
            }
        }


        private long? _linkobjectid;
        /// <summary>
        /// 358000500 Ссылка на реестр дел (LINK_OBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 358000500)]
        public long? LinkObjectId
        {
            get
            {
                CheckPropertyInited("LinkObjectId");
                return _linkobjectid;
            }
            set
            {
                _linkobjectid = value;
                NotifyPropertyChanged("LinkObjectId");
            }
        }


        private long? _linkreestrid;
        /// <summary>
        /// 358000600 Номер реестра (LINK_REESTR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 358000600)]
        public long? LinkReestrId
        {
            get
            {
                CheckPropertyInited("LinkReestrId");
                return _linkreestrid;
            }
            set
            {
                _linkreestrid = value;
                NotifyPropertyChanged("LinkReestrId");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 359 Журнал идентификации зачислений (INSUR_FILE_PLAT_IDENTIFY_LOG)
    /// </summary>
    [RegisterInfo(RegisterID = 359)]
    [Serializable]
    public sealed partial class OMFilePlatIdentifyLog : OMBaseClass<OMFilePlatIdentifyLog>
    {

        private long _empid;
        /// <summary>
        /// 359000100 Идентификатор (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 359000100)]
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


        private long? _inputfileid;
        /// <summary>
        /// 359000200 Идентификатор загруженного файла (INPUT_FILE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 359000200)]
        public long? InputFileId
        {
            get
            {
                CheckPropertyInited("InputFileId");
                return _inputfileid;
            }
            set
            {
                _inputfileid = value;
                NotifyPropertyChanged("InputFileId");
            }
        }


        private long? _platcount;
        /// <summary>
        /// 359000300 Количество записей для обработки (PLAT_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 359000300)]
        public long? PlatCount
        {
            get
            {
                CheckPropertyInited("PlatCount");
                return _platcount;
            }
            set
            {
                _platcount = value;
                NotifyPropertyChanged("PlatCount");
            }
        }


        private string _status;
        /// <summary>
        /// 359000500 Статус (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 359000500)]
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


        private IdentifyPlatStatus _status_Code;
        /// <summary>
        /// 359000500 Статус (справочный код) (STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 359000500)]
        public IdentifyPlatStatus Status_Code
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


        private DateTime? _startdate;
        /// <summary>
        /// 359000600 Дата начала процесса идентифкации (START_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 359000600)]
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
        /// 359000700 Дата окончания процесса идентификации (END_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 359000700)]
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


        private long? _identifiedcount;
        /// <summary>
        /// 359000800 Количество идентифицированных записей (IDENTIFIED_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 359000800)]
        public long? IdentifiedCount
        {
            get
            {
                CheckPropertyInited("IdentifiedCount");
                return _identifiedcount;
            }
            set
            {
                _identifiedcount = value;
                NotifyPropertyChanged("IdentifiedCount");
            }
        }


        private long? _notidentiedcount;
        /// <summary>
        /// 359000900 Количество неидентифицированных записей (NOT_IDENTIFIED_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 359000900)]
        public long? NotIdentiedCount
        {
            get
            {
                CheckPropertyInited("NotIdentiedCount");
                return _notidentiedcount;
            }
            set
            {
                _notidentiedcount = value;
                NotifyPropertyChanged("NotIdentiedCount");
            }
        }


        private bool? _needprocess;
        /// <summary>
        /// 359001000 Запускать процесс обработки после идентификации (NEED_PROCESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 359001000)]
        public bool? NeedProcess
        {
            get
            {
                CheckPropertyInited("NeedProcess");
                return _needprocess;
            }
            set
            {
                _needprocess = value;
                NotifyPropertyChanged("NeedProcess");
            }
        }

    }
}

namespace ObjectModel.ImportLog
{
    /// <summary>
    /// 360 Журнал формирования объектов страхования для Зданий (IMPORT_LOG_INSUR_BUILDING)
    /// </summary>
    [RegisterInfo(RegisterID = 360)]
    [Serializable]
    public sealed partial class OMInsurBuildingLog : OMBaseClass<OMInsurBuildingLog>
    {

        private long _id;
        /// <summary>
        /// 36000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 36000100)]
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


        private long? _ehdparcelid;
        /// <summary>
        /// 36000200 Идентификатор здания ЕГРН (EHD_PARCEL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 36000200)]
        public long? EhdParcelId
        {
            get
            {
                CheckPropertyInited("EhdParcelId");
                return _ehdparcelid;
            }
            set
            {
                _ehdparcelid = value;
                NotifyPropertyChanged("EhdParcelId");
            }
        }


        private long? _btibuildingid;
        /// <summary>
        /// 36000300 Идентификатор здания БТИ (BTI_BUILDING_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 36000300)]
        public long? BtiBuildingId
        {
            get
            {
                CheckPropertyInited("BtiBuildingId");
                return _btibuildingid;
            }
            set
            {
                _btibuildingid = value;
                NotifyPropertyChanged("BtiBuildingId");
            }
        }


        private long? _insurbuildingid;
        /// <summary>
        /// 36000400 Идентификатор объекта страхования МКД (INSUR_BUILDING_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 36000400)]
        public long? InsurBuildingId
        {
            get
            {
                CheckPropertyInited("InsurBuildingId");
                return _insurbuildingid;
            }
            set
            {
                _insurbuildingid = value;
                NotifyPropertyChanged("InsurBuildingId");
            }
        }


        private DateTime? _dateloaded;
        /// <summary>
        /// 36000500 Дата загрузки (DATE_LOADED)
        /// </summary>
        [RegisterAttribute(AttributeID = 36000500)]
        public DateTime? DateLoaded
        {
            get
            {
                CheckPropertyInited("DateLoaded");
                return _dateloaded;
            }
            set
            {
                _dateloaded = value;
                NotifyPropertyChanged("DateLoaded");
            }
        }


        private string _errormessage;
        /// <summary>
        /// 36000600 Сообщение об ошибке (ERROR_MESSAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 36000600)]
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


        private long? _errorid;
        /// <summary>
        /// 36000700 Идентификатор ошибки в журнале (ERROR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 36000700)]
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


        private long? _iserror;
        /// <summary>
        /// 36000800 Признак ошибки (IS_ERROR)
        /// </summary>
        [RegisterAttribute(AttributeID = 36000800)]
        public long? IsError
        {
            get
            {
                CheckPropertyInited("IsError");
                return _iserror;
            }
            set
            {
                _iserror = value;
                NotifyPropertyChanged("IsError");
            }
        }


        private DateTime? _updatedateehd;
        /// <summary>
        /// 36000900 Дата обновления объекта в источнике данных ЕГРН (UPDATE_DATE_EHD)
        /// </summary>
        [RegisterAttribute(AttributeID = 36000900)]
        public DateTime? UpdateDateEhd
        {
            get
            {
                CheckPropertyInited("UpdateDateEhd");
                return _updatedateehd;
            }
            set
            {
                _updatedateehd = value;
                NotifyPropertyChanged("UpdateDateEhd");
            }
        }


        private string _cadnum;
        /// <summary>
        /// 36001000 Кадастровый номер (CAD_NUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 36001000)]
        public string CadNum
        {
            get
            {
                CheckPropertyInited("CadNum");
                return _cadnum;
            }
            set
            {
                _cadnum = value;
                NotifyPropertyChanged("CadNum");
            }
        }


        private long? _unom;
        /// <summary>
        /// 36001100 УНОМ (UNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 36001100)]
        public long? Unom
        {
            get
            {
                CheckPropertyInited("Unom");
                return _unom;
            }
            set
            {
                _unom = value;
                NotifyPropertyChanged("Unom");
            }
        }


        private DateTime? _updatedatebti;
        /// <summary>
        /// 36001200 Дата обновления объекта в источнике данных БТИ (UPDATE_DATE_BTI)
        /// </summary>
        [RegisterAttribute(AttributeID = 36001200)]
        public DateTime? UpdateDateBti
        {
            get
            {
                CheckPropertyInited("UpdateDateBti");
                return _updatedatebti;
            }
            set
            {
                _updatedatebti = value;
                NotifyPropertyChanged("UpdateDateBti");
            }
        }


        private long? _errorattemptscount;
        /// <summary>
        /// 36001300 Количество неудачных попыток обновления объекта (ERROR_ATTEMPTS_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 36001300)]
        public long? ErrorAttemptsCount
        {
            get
            {
                CheckPropertyInited("ErrorAttemptsCount");
                return _errorattemptscount;
            }
            set
            {
                _errorattemptscount = value;
                NotifyPropertyChanged("ErrorAttemptsCount");
            }
        }

    }
}

namespace ObjectModel.ImportLog
{
    /// <summary>
    /// 361 Журнал формирования помещений страхования (IMPORT_LOG_INSUR_FLAT_B)
    /// </summary>
    [RegisterInfo(RegisterID = 361)]
    [Serializable]
    public sealed partial class OMInsurFlatBuildingLog : OMBaseClass<OMInsurFlatBuildingLog>
    {

        private long _id;
        /// <summary>
        /// 36100100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 36100100)]
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


        private long? _ehdparcelid;
        /// <summary>
        /// 36100200 Идентификатор здания ЕГРН (EHD_PARCEL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 36100200)]
        public long? EhdParcelId
        {
            get
            {
                CheckPropertyInited("EhdParcelId");
                return _ehdparcelid;
            }
            set
            {
                _ehdparcelid = value;
                NotifyPropertyChanged("EhdParcelId");
            }
        }


        private long? _btibuildingid;
        /// <summary>
        /// 36100300 Идентификатор здания БТИ (BTI_BUILDING_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 36100300)]
        public long? BtiBuildingId
        {
            get
            {
                CheckPropertyInited("BtiBuildingId");
                return _btibuildingid;
            }
            set
            {
                _btibuildingid = value;
                NotifyPropertyChanged("BtiBuildingId");
            }
        }


        private long? _insurbuildingid;
        /// <summary>
        /// 36100400 Идентификатор объекта страхования МКД (INSUR_BUILDING_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 36100400)]
        public long? InsurBuildingId
        {
            get
            {
                CheckPropertyInited("InsurBuildingId");
                return _insurbuildingid;
            }
            set
            {
                _insurbuildingid = value;
                NotifyPropertyChanged("InsurBuildingId");
            }
        }


        private DateTime? _dateloaded;
        /// <summary>
        /// 36100500 Дата загрузки (DATE_LOADED)
        /// </summary>
        [RegisterAttribute(AttributeID = 36100500)]
        public DateTime? DateLoaded
        {
            get
            {
                CheckPropertyInited("DateLoaded");
                return _dateloaded;
            }
            set
            {
                _dateloaded = value;
                NotifyPropertyChanged("DateLoaded");
            }
        }


        private string _errormessage;
        /// <summary>
        /// 36100600 Сообщение об ошибке (ERROR_MESSAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 36100600)]
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


        private long? _errorid;
        /// <summary>
        /// 36100700 Идентификатор ошибки в журнале (ERROR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 36100700)]
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


        private long? _iserror;
        /// <summary>
        /// 36100800 Признак ошибки (IS_ERROR)
        /// </summary>
        [RegisterAttribute(AttributeID = 36100800)]
        public long? IsError
        {
            get
            {
                CheckPropertyInited("IsError");
                return _iserror;
            }
            set
            {
                _iserror = value;
                NotifyPropertyChanged("IsError");
            }
        }


        private DateTime? _updatedateehd;
        /// <summary>
        /// 36100900 Дата обновления объекта в источнике данных БТИ (UPDATE_DATE_EHD)
        /// </summary>
        [RegisterAttribute(AttributeID = 36100900)]
        public DateTime? UpdateDateEhd
        {
            get
            {
                CheckPropertyInited("UpdateDateEhd");
                return _updatedateehd;
            }
            set
            {
                _updatedateehd = value;
                NotifyPropertyChanged("UpdateDateEhd");
            }
        }


        private DateTime? _updatedatebti;
        /// <summary>
        /// 36101000 Дата обновления объекта в источнике данных ЕГРН (UPDATE_DATE_BTI)
        /// </summary>
        [RegisterAttribute(AttributeID = 36101000)]
        public DateTime? UpdateDateBti
        {
            get
            {
                CheckPropertyInited("UpdateDateBti");
                return _updatedatebti;
            }
            set
            {
                _updatedatebti = value;
                NotifyPropertyChanged("UpdateDateBti");
            }
        }


        private string _cadnum;
        /// <summary>
        /// 36101100 Кадастровый номер (CAD_NUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 36101100)]
        public string CadNum
        {
            get
            {
                CheckPropertyInited("CadNum");
                return _cadnum;
            }
            set
            {
                _cadnum = value;
                NotifyPropertyChanged("CadNum");
            }
        }


        private long? _unom;
        /// <summary>
        /// 36101200 УНОМ (UNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 36101200)]
        public long? Unom
        {
            get
            {
                CheckPropertyInited("Unom");
                return _unom;
            }
            set
            {
                _unom = value;
                NotifyPropertyChanged("Unom");
            }
        }


        private long? _errorattemptscount;
        /// <summary>
        /// 36101300 Количество неудачных попыток обновления объекта (ERROR_ATTEMPTS_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 36101300)]
        public long? ErrorAttemptsCount
        {
            get
            {
                CheckPropertyInited("ErrorAttemptsCount");
                return _errorattemptscount;
            }
            set
            {
                _errorattemptscount = value;
                NotifyPropertyChanged("ErrorAttemptsCount");
            }
        }

    }
}

namespace ObjectModel.ImportLog
{
    /// <summary>
    /// 362 Журнал загрузки таблицы ehd.building_parcel (izk_rsm.CIPJS_IMPORT_BUILDING_PARCEL)
    /// </summary>
    [RegisterInfo(RegisterID = 362)]
    [Serializable]
    public sealed partial class OMEhdBuildingParcelLog : OMBaseClass<OMEhdBuildingParcelLog>
    {

        private long _id;
        /// <summary>
        /// 362000100 ID (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 362000100)]
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


        private long? _globalid;
        /// <summary>
        /// 362000200 GLOBAL_ID (GLOBAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 362000200)]
        public long? GlobalId
        {
            get
            {
                CheckPropertyInited("GlobalId");
                return _globalid;
            }
            set
            {
                _globalid = value;
                NotifyPropertyChanged("GlobalId");
            }
        }


        private DateTime? _updatedate;
        /// <summary>
        /// 362000300 UPDATE_DATE (UPDATE_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 362000300)]
        public DateTime? UpdateDate
        {
            get
            {
                CheckPropertyInited("UpdateDate");
                return _updatedate;
            }
            set
            {
                _updatedate = value;
                NotifyPropertyChanged("UpdateDate");
            }
        }


        private long? _iserror;
        /// <summary>
        /// 362000400 IS_ERROR (IS_ERROR)
        /// </summary>
        [RegisterAttribute(AttributeID = 362000400)]
        public long? IsError
        {
            get
            {
                CheckPropertyInited("IsError");
                return _iserror;
            }
            set
            {
                _iserror = value;
                NotifyPropertyChanged("IsError");
            }
        }


        private string _message;
        /// <summary>
        /// 362000500 MESSAGE (MESSAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 362000500)]
        public string Message
        {
            get
            {
                CheckPropertyInited("Message");
                return _message;
            }
            set
            {
                _message = value;
                NotifyPropertyChanged("Message");
            }
        }


        private long? _errorid;
        /// <summary>
        /// 362000600 ERROR_ID (ERROR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 362000600)]
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


        private string _taskid;
        /// <summary>
        /// 362000700 TASK_ID (TASK_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 362000700)]
        public string TaskId
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


        private DateTime? _insertdate;
        /// <summary>
        /// 362000800 INSERT_DATE (INSERT_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 362000800)]
        public DateTime? InsertDate
        {
            get
            {
                CheckPropertyInited("InsertDate");
                return _insertdate;
            }
            set
            {
                _insertdate = value;
                NotifyPropertyChanged("InsertDate");
            }
        }


        private DateTime? _importdate;
        /// <summary>
        /// 362000900 IMPORT_DATE (IMPORT_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 362000900)]
        public DateTime? ImportDate
        {
            get
            {
                CheckPropertyInited("ImportDate");
                return _importdate;
            }
            set
            {
                _importdate = value;
                NotifyPropertyChanged("ImportDate");
            }
        }

    }
}

namespace ObjectModel.ImportLog
{
    /// <summary>
    /// 363 Журнал загрузки таблицы ehd.register (izk_rsm.CIPJS_IMPORT_REGISTER)
    /// </summary>
    [RegisterInfo(RegisterID = 363)]
    [Serializable]
    public sealed partial class OMEhdRegisterLog : OMBaseClass<OMEhdRegisterLog>
    {

        private long _id;
        /// <summary>
        /// 363000100 ID (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 363000100)]
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


        private long? _globalid;
        /// <summary>
        /// 363000200 GLOBAL_ID (GLOBAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 363000200)]
        public long? GlobalId
        {
            get
            {
                CheckPropertyInited("GlobalId");
                return _globalid;
            }
            set
            {
                _globalid = value;
                NotifyPropertyChanged("GlobalId");
            }
        }


        private DateTime? _updatedate;
        /// <summary>
        /// 363000300 UPDATE_DATE (UPDATE_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 363000300)]
        public DateTime? UpdateDate
        {
            get
            {
                CheckPropertyInited("UpdateDate");
                return _updatedate;
            }
            set
            {
                _updatedate = value;
                NotifyPropertyChanged("UpdateDate");
            }
        }


        private long? _iserror;
        /// <summary>
        /// 363000400 IS_ERROR (IS_ERROR)
        /// </summary>
        [RegisterAttribute(AttributeID = 363000400)]
        public long? IsError
        {
            get
            {
                CheckPropertyInited("IsError");
                return _iserror;
            }
            set
            {
                _iserror = value;
                NotifyPropertyChanged("IsError");
            }
        }


        private string _message;
        /// <summary>
        /// 363000500 MESSAGE (MESSAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 363000500)]
        public string Message
        {
            get
            {
                CheckPropertyInited("Message");
                return _message;
            }
            set
            {
                _message = value;
                NotifyPropertyChanged("Message");
            }
        }


        private long? _errorid;
        /// <summary>
        /// 363000600 ERROR_ID (ERROR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 363000600)]
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


        private string _taskid;
        /// <summary>
        /// 363000700 TASK_ID (TASK_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 363000700)]
        public string TaskId
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


        private DateTime? _insertdate;
        /// <summary>
        /// 363000800 INSERT_DATE (INSERT_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 363000800)]
        public DateTime? InsertDate
        {
            get
            {
                CheckPropertyInited("InsertDate");
                return _insertdate;
            }
            set
            {
                _insertdate = value;
                NotifyPropertyChanged("InsertDate");
            }
        }


        private DateTime? _importdate;
        /// <summary>
        /// 363000900 IMPORT_DATE (IMPORT_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 363000900)]
        public DateTime? ImportDate
        {
            get
            {
                CheckPropertyInited("ImportDate");
                return _importdate;
            }
            set
            {
                _importdate = value;
                NotifyPropertyChanged("ImportDate");
            }
        }

    }
}

namespace ObjectModel.ImportLog
{
    /// <summary>
    /// 364 Журнал загрузки таблицы ehd.location (izk_rsm.CIPJS_IMPORT_LOCATION)
    /// </summary>
    [RegisterInfo(RegisterID = 364)]
    [Serializable]
    public sealed partial class OMEhdLocationLog : OMBaseClass<OMEhdLocationLog>
    {

        private long _id;
        /// <summary>
        /// 364000100 ID (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 364000100)]
        public long id
        {
            get
            {
                CheckPropertyInited("id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("id");
            }
        }


        private long? _globalid;
        /// <summary>
        /// 364000200 GLOBAL_ID (GLOBAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 364000200)]
        public long? GlobalId
        {
            get
            {
                CheckPropertyInited("GlobalId");
                return _globalid;
            }
            set
            {
                _globalid = value;
                NotifyPropertyChanged("GlobalId");
            }
        }


        private DateTime? _updatedate;
        /// <summary>
        /// 364000300 UPDATE_DATE (UPDATE_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 364000300)]
        public DateTime? UpdateDate
        {
            get
            {
                CheckPropertyInited("UpdateDate");
                return _updatedate;
            }
            set
            {
                _updatedate = value;
                NotifyPropertyChanged("UpdateDate");
            }
        }


        private long? _iserror;
        /// <summary>
        /// 364000400 IS_ERROR (IS_ERROR)
        /// </summary>
        [RegisterAttribute(AttributeID = 364000400)]
        public long? IsError
        {
            get
            {
                CheckPropertyInited("IsError");
                return _iserror;
            }
            set
            {
                _iserror = value;
                NotifyPropertyChanged("IsError");
            }
        }


        private string _message;
        /// <summary>
        /// 364000500 MESSAGE (MESSAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 364000500)]
        public string Message
        {
            get
            {
                CheckPropertyInited("Message");
                return _message;
            }
            set
            {
                _message = value;
                NotifyPropertyChanged("Message");
            }
        }


        private long? _errorid;
        /// <summary>
        /// 364000600 ERROR_ID (ERROR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 364000600)]
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


        private string _taskid;
        /// <summary>
        /// 364000700 TASK_ID (TASK_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 364000700)]
        public string TaskId
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


        private DateTime? _insertdate;
        /// <summary>
        /// 364000800 INSERT_DATE (INSERT_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 364000800)]
        public DateTime? InsertDate
        {
            get
            {
                CheckPropertyInited("InsertDate");
                return _insertdate;
            }
            set
            {
                _insertdate = value;
                NotifyPropertyChanged("InsertDate");
            }
        }


        private DateTime? _importdate;
        /// <summary>
        /// 364000900 IMPORT_DATE (IMPORT_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 364000900)]
        public DateTime? ImportDate
        {
            get
            {
                CheckPropertyInited("ImportDate");
                return _importdate;
            }
            set
            {
                _importdate = value;
                NotifyPropertyChanged("ImportDate");
            }
        }

    }
}

namespace ObjectModel.ImportLog
{
    /// <summary>
    /// 365 Журнал загрузки таблицы ehd.egrp (izk_rsm.CIPJS_IMPORT_EGRP)
    /// </summary>
    [RegisterInfo(RegisterID = 365)]
    [Serializable]
    public sealed partial class OMEhdEGRPLog : OMBaseClass<OMEhdEGRPLog>
    {

        private long _id;
        /// <summary>
        /// 365000100 ID (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 365000100)]
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


        private long? _globalid;
        /// <summary>
        /// 365000200 GLOBAL_ID (GLOBAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 365000200)]
        public long? GlobalId
        {
            get
            {
                CheckPropertyInited("GlobalId");
                return _globalid;
            }
            set
            {
                _globalid = value;
                NotifyPropertyChanged("GlobalId");
            }
        }


        private DateTime? _updatedate;
        /// <summary>
        /// 365000300 UPDATE_DATE (UPDATE_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 365000300)]
        public DateTime? UpdateDate
        {
            get
            {
                CheckPropertyInited("UpdateDate");
                return _updatedate;
            }
            set
            {
                _updatedate = value;
                NotifyPropertyChanged("UpdateDate");
            }
        }


        private long? _iserror;
        /// <summary>
        /// 365000400 IS_ERROR (IS_ERROR)
        /// </summary>
        [RegisterAttribute(AttributeID = 365000400)]
        public long? IsError
        {
            get
            {
                CheckPropertyInited("IsError");
                return _iserror;
            }
            set
            {
                _iserror = value;
                NotifyPropertyChanged("IsError");
            }
        }


        private string _message;
        /// <summary>
        /// 365000500 MESSAGE (MESSAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 365000500)]
        public string Message
        {
            get
            {
                CheckPropertyInited("Message");
                return _message;
            }
            set
            {
                _message = value;
                NotifyPropertyChanged("Message");
            }
        }


        private long? _errorid;
        /// <summary>
        /// 365000600 ERROR_ID (ERROR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 365000600)]
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


        private string _taskid;
        /// <summary>
        /// 365000700 TASK_ID (TASK_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 365000700)]
        public string TaskId
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


        private DateTime? _insertdate;
        /// <summary>
        /// 365000800 INSERT_DATE (INSERT_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 365000800)]
        public DateTime? InsertDate
        {
            get
            {
                CheckPropertyInited("InsertDate");
                return _insertdate;
            }
            set
            {
                _insertdate = value;
                NotifyPropertyChanged("InsertDate");
            }
        }


        private DateTime? _importdate;
        /// <summary>
        /// 365000900 IMPORT_DATE (IMPORT_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 365000900)]
        public DateTime? ImportDate
        {
            get
            {
                CheckPropertyInited("ImportDate");
                return _importdate;
            }
            set
            {
                _importdate = value;
                NotifyPropertyChanged("ImportDate");
            }
        }

    }
}

namespace ObjectModel.ImportLog
{
    /// <summary>
    /// 366 Журнал загрузки таблицы ehd.right (izk_rsm.CIPJS_IMPORT_RIGHT)
    /// </summary>
    [RegisterInfo(RegisterID = 366)]
    [Serializable]
    public sealed partial class OMEhdRightLog : OMBaseClass<OMEhdRightLog>
    {

        private long _id;
        /// <summary>
        /// 366000100 ID (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 366000100)]
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


        private long? _globalid;
        /// <summary>
        /// 366000200 GLOBAL_ID (GLOBAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 366000200)]
        public long? GlobalId
        {
            get
            {
                CheckPropertyInited("GlobalId");
                return _globalid;
            }
            set
            {
                _globalid = value;
                NotifyPropertyChanged("GlobalId");
            }
        }


        private DateTime? _updatedate;
        /// <summary>
        /// 366000300 UPDATE_DATE (UPDATE_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 366000300)]
        public DateTime? UpdateDate
        {
            get
            {
                CheckPropertyInited("UpdateDate");
                return _updatedate;
            }
            set
            {
                _updatedate = value;
                NotifyPropertyChanged("UpdateDate");
            }
        }


        private long? _iserror;
        /// <summary>
        /// 366000400 IS_ERROR (IS_ERROR)
        /// </summary>
        [RegisterAttribute(AttributeID = 366000400)]
        public long? IsError
        {
            get
            {
                CheckPropertyInited("IsError");
                return _iserror;
            }
            set
            {
                _iserror = value;
                NotifyPropertyChanged("IsError");
            }
        }


        private string _message;
        /// <summary>
        /// 366000500 MESSAGE (MESSAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 366000500)]
        public string Message
        {
            get
            {
                CheckPropertyInited("Message");
                return _message;
            }
            set
            {
                _message = value;
                NotifyPropertyChanged("Message");
            }
        }


        private long? _errorid;
        /// <summary>
        /// 366000600 ERROR_ID (ERROR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 366000600)]
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


        private string _taskid;
        /// <summary>
        /// 366000700 TASK_ID (TASK_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 366000700)]
        public string TaskId
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


        private DateTime? _insertdate;
        /// <summary>
        /// 366000800 INSERT_DATE (INSERT_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 366000800)]
        public DateTime? InsertDate
        {
            get
            {
                CheckPropertyInited("InsertDate");
                return _insertdate;
            }
            set
            {
                _insertdate = value;
                NotifyPropertyChanged("InsertDate");
            }
        }


        private DateTime? _importdate;
        /// <summary>
        /// 366000900 IMPORT_DATE (IMPORT_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 366000900)]
        public DateTime? ImportDate
        {
            get
            {
                CheckPropertyInited("ImportDate");
                return _importdate;
            }
            set
            {
                _importdate = value;
                NotifyPropertyChanged("ImportDate");
            }
        }

    }
}

namespace ObjectModel.ImportLog
{
    /// <summary>
    /// 367 Журнал загрузки таблицы ehd.old_numbers (izk_rsm.CIPJS_IMPORT_OLD_NUMBERS)
    /// </summary>
    [RegisterInfo(RegisterID = 367)]
    [Serializable]
    public sealed partial class OMEhdOldNumbersLog : OMBaseClass<OMEhdOldNumbersLog>
    {

        private long _id;
        /// <summary>
        /// 367000100 ID (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 367000100)]
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


        private long? _globalid;
        /// <summary>
        /// 367000200 GLOBAL_ID (GLOBAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 367000200)]
        public long? GlobalId
        {
            get
            {
                CheckPropertyInited("GlobalId");
                return _globalid;
            }
            set
            {
                _globalid = value;
                NotifyPropertyChanged("GlobalId");
            }
        }


        private DateTime? _updatedate;
        /// <summary>
        /// 367000300 UPDATE_DATE (UPDATE_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 367000300)]
        public DateTime? UpdateDate
        {
            get
            {
                CheckPropertyInited("UpdateDate");
                return _updatedate;
            }
            set
            {
                _updatedate = value;
                NotifyPropertyChanged("UpdateDate");
            }
        }


        private long? _iserror;
        /// <summary>
        /// 367000400 IS_ERROR (IS_ERROR)
        /// </summary>
        [RegisterAttribute(AttributeID = 367000400)]
        public long? IsError
        {
            get
            {
                CheckPropertyInited("IsError");
                return _iserror;
            }
            set
            {
                _iserror = value;
                NotifyPropertyChanged("IsError");
            }
        }


        private string _message;
        /// <summary>
        /// 367000500 MESSAGE (MESSAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 367000500)]
        public string Message
        {
            get
            {
                CheckPropertyInited("Message");
                return _message;
            }
            set
            {
                _message = value;
                NotifyPropertyChanged("Message");
            }
        }


        private long? _errorid;
        /// <summary>
        /// 367000600 ERROR_ID (ERROR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 367000600)]
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


        private string _taskid;
        /// <summary>
        /// 367000700 TASK_ID (TASK_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 367000700)]
        public string TaskId
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


        private DateTime? _insertdate;
        /// <summary>
        /// 367000800 INSERT_DATE (INSERT_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 367000800)]
        public DateTime? InsertDate
        {
            get
            {
                CheckPropertyInited("InsertDate");
                return _insertdate;
            }
            set
            {
                _insertdate = value;
                NotifyPropertyChanged("InsertDate");
            }
        }


        private DateTime? _importdate;
        /// <summary>
        /// 367000900 IMPORT_DATE (IMPORT_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 367000900)]
        public DateTime? ImportDate
        {
            get
            {
                CheckPropertyInited("ImportDate");
                return _importdate;
            }
            set
            {
                _importdate = value;
                NotifyPropertyChanged("ImportDate");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 368 Реестр связей типов здания с этажностью и типом констуркции (INSUR_TYPE_BUILDING_FLOOR_LINK)
    /// </summary>
    [RegisterInfo(RegisterID = 368)]
    [Serializable]
    public sealed partial class OMTypeBuldingFloorLink : OMBaseClass<OMTypeBuldingFloorLink>
    {

        private long _id;
        /// <summary>
        /// 368000100 Уникальный номер записи (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 368000100)]
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


        private string _typebuilding;
        /// <summary>
        /// 368000200 Тип здания (справочник "Типы зданий", 12132) (TYPE_BUILDING)
        /// </summary>
        [RegisterAttribute(AttributeID = 368000200)]
        public string TypeBuilding
        {
            get
            {
                CheckPropertyInited("TypeBuilding");
                return _typebuilding;
            }
            set
            {
                _typebuilding = value;
                NotifyPropertyChanged("TypeBuilding");
            }
        }


        private BuildingType _typebuilding_Code;
        /// <summary>
        /// 368000200 Тип здания (справочник "Типы зданий", 12132) (справочный код) (TYPE_BUILDING_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 368000200)]
        public BuildingType TypeBuilding_Code
        {
            get
            {
                CheckPropertyInited("TypeBuilding_Code");
                return this._typebuilding_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typebuilding))
                    {
                         _typebuilding = descr;
                    }
                }
                else
                {
                     _typebuilding = descr;
                }

                this._typebuilding_Code = value;
                NotifyPropertyChanged("TypeBuilding");
                NotifyPropertyChanged("TypeBuilding_Code");
            }
        }


        private string _typebuildingstructure;
        /// <summary>
        /// 368000300 Тип конструкции строения (справочник "Тип конструкции строения", 12143) (TYPE_BUILDING_STRUCTURE)
        /// </summary>
        [RegisterAttribute(AttributeID = 368000300)]
        public string TypeBuildingStructure
        {
            get
            {
                CheckPropertyInited("TypeBuildingStructure");
                return _typebuildingstructure;
            }
            set
            {
                _typebuildingstructure = value;
                NotifyPropertyChanged("TypeBuildingStructure");
            }
        }


        private TypeBuildingStructure _typebuildingstructure_Code;
        /// <summary>
        /// 368000300 Тип конструкции строения (справочник "Тип конструкции строения", 12143) (справочный код) (TYPE_BUILDING_STRUCTURE_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 368000300)]
        public TypeBuildingStructure TypeBuildingStructure_Code
        {
            get
            {
                CheckPropertyInited("TypeBuildingStructure_Code");
                return this._typebuildingstructure_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typebuildingstructure))
                    {
                         _typebuildingstructure = descr;
                    }
                }
                else
                {
                     _typebuildingstructure = descr;
                }

                this._typebuildingstructure_Code = value;
                NotifyPropertyChanged("TypeBuildingStructure");
                NotifyPropertyChanged("TypeBuildingStructure_Code");
            }
        }


        private string _typefloors;
        /// <summary>
        /// 368000400 Этажность строения (справочник "Этажность строения", 12144) (TYPE_FLOORS)
        /// </summary>
        [RegisterAttribute(AttributeID = 368000400)]
        public string TypeFloors
        {
            get
            {
                CheckPropertyInited("TypeFloors");
                return _typefloors;
            }
            set
            {
                _typefloors = value;
                NotifyPropertyChanged("TypeFloors");
            }
        }


        private TypeFloors _typefloors_Code;
        /// <summary>
        /// 368000400 Этажность строения (справочник "Этажность строения", 12144) (справочный код) (TYPE_FLOORS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 368000400)]
        public TypeFloors TypeFloors_Code
        {
            get
            {
                CheckPropertyInited("TypeFloors_Code");
                return this._typefloors_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_typefloors))
                    {
                         _typefloors = descr;
                    }
                }
                else
                {
                     _typefloors = descr;
                }

                this._typefloors_Code = value;
                NotifyPropertyChanged("TypeFloors");
                NotifyPropertyChanged("TypeFloors_Code");
            }
        }

    }
}

namespace ObjectModel.ImportLog
{
    /// <summary>
    /// 369 Журнал загрузки данных БТИ (izk_rsm.CIPJS_IMPORT_BTI_DAILY_LOG)
    /// </summary>
    [RegisterInfo(RegisterID = 369)]
    [Serializable]
    public sealed partial class OMBtiImportLog : OMBaseClass<OMBtiImportLog>
    {

        private long? _btiid;
        /// <summary>
        /// 36900100 Идентификатор здания в источнике БТИ (BTI_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 36900100)]
        public long? BtiId
        {
            get
            {
                CheckPropertyInited("BtiId");
                return _btiid;
            }
            set
            {
                _btiid = value;
                NotifyPropertyChanged("BtiId");
            }
        }


        private string _numcadnum;
        /// <summary>
        /// 36900200 Кадастровый номер (NUM_CADNUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 36900200)]
        public string NumCadnum
        {
            get
            {
                CheckPropertyInited("NumCadnum");
                return _numcadnum;
            }
            set
            {
                _numcadnum = value;
                NotifyPropertyChanged("NumCadnum");
            }
        }


        private string _unom;
        /// <summary>
        /// 36900300 УНОМ (UNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 36900300)]
        public string Unom
        {
            get
            {
                CheckPropertyInited("Unom");
                return _unom;
            }
            set
            {
                _unom = value;
                NotifyPropertyChanged("Unom");
            }
        }


        private long? _isnew;
        /// <summary>
        /// 36900400 Признак нового (IS_NEW)
        /// </summary>
        [RegisterAttribute(AttributeID = 36900400)]
        public long? IsNew
        {
            get
            {
                CheckPropertyInited("IsNew");
                return _isnew;
            }
            set
            {
                _isnew = value;
                NotifyPropertyChanged("IsNew");
            }
        }


        private long? _altbuildingid;
        /// <summary>
        /// 36900500 Идентификатор импортированного объекта БТИ (ALT_BUILDING_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 36900500)]
        public long? AltBuildingId
        {
            get
            {
                CheckPropertyInited("AltBuildingId");
                return _altbuildingid;
            }
            set
            {
                _altbuildingid = value;
                NotifyPropertyChanged("AltBuildingId");
            }
        }


        private DateTime? _dateedit;
        /// <summary>
        /// 36900600 Дата редактирования объекта в БТИ (DATEEDIT)
        /// </summary>
        [RegisterAttribute(AttributeID = 36900600)]
        public DateTime? Dateedit
        {
            get
            {
                CheckPropertyInited("Dateedit");
                return _dateedit;
            }
            set
            {
                _dateedit = value;
                NotifyPropertyChanged("Dateedit");
            }
        }


        private long? _iserror;
        /// <summary>
        /// 36900700 Признак ошибки (IS_ERROR)
        /// </summary>
        [RegisterAttribute(AttributeID = 36900700)]
        public long? IsError
        {
            get
            {
                CheckPropertyInited("IsError");
                return _iserror;
            }
            set
            {
                _iserror = value;
                NotifyPropertyChanged("IsError");
            }
        }


        private string _message;
        /// <summary>
        /// 36900800 Сообщение об ошибке (MESSAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 36900800)]
        public string Message
        {
            get
            {
                CheckPropertyInited("Message");
                return _message;
            }
            set
            {
                _message = value;
                NotifyPropertyChanged("Message");
            }
        }


        private long? _errorid;
        /// <summary>
        /// 36900900 Идентификатор ошибки (ERROR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 36900900)]
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


        private long? _taskid;
        /// <summary>
        /// 36901000 Идентификатор потока (TASK_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 36901000)]
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


        private DateTime? _insertdate;
        /// <summary>
        /// 36901100 Дата вставки записи в журнал (INSERT_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 36901100)]
        public DateTime? InsertDate
        {
            get
            {
                CheckPropertyInited("InsertDate");
                return _insertdate;
            }
            set
            {
                _insertdate = value;
                NotifyPropertyChanged("InsertDate");
            }
        }


        private DateTime? _importdate;
        /// <summary>
        /// 36901200 Дата обработки (IMPORT_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 36901200)]
        public DateTime? ImportDate
        {
            get
            {
                CheckPropertyInited("ImportDate");
                return _importdate;
            }
            set
            {
                _importdate = value;
                NotifyPropertyChanged("ImportDate");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 370 Журнал процесса обработки файлов МФЦ (INSUR_FILE_PROCESS_LOG)
    /// </summary>
    [RegisterInfo(RegisterID = 370)]
    [Serializable]
    public sealed partial class OMFileProcessLog : OMBaseClass<OMFileProcessLog>
    {

        private long _empid;
        /// <summary>
        /// 370000100 Идентификатор (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 370000100)]
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


        private long? _inputfileid;
        /// <summary>
        /// 370000200 Идентификатор файла для обработки (INPUT_FILE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 370000200)]
        public long? InputFileId
        {
            get
            {
                CheckPropertyInited("InputFileId");
                return _inputfileid;
            }
            set
            {
                _inputfileid = value;
                NotifyPropertyChanged("InputFileId");
            }
        }


        private long? _totalcount;
        /// <summary>
        /// 370000300 Количество записей для обработки (TOTAL_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 370000300)]
        public long? TotalCount
        {
            get
            {
                CheckPropertyInited("TotalCount");
                return _totalcount;
            }
            set
            {
                _totalcount = value;
                NotifyPropertyChanged("TotalCount");
            }
        }


        private string _status;
        /// <summary>
        /// 370000400 Статус обработки (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 370000400)]
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


        private FileProcessStatus _status_Code;
        /// <summary>
        /// 370000400 Статус обработки (справочный код) (STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 370000400)]
        public FileProcessStatus Status_Code
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


        private DateTime? _startdate;
        /// <summary>
        /// 370000500 Дата начала процесса обработки (START_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 370000500)]
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
        /// 370000600 Дата окончания процесса обработки (END_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 370000600)]
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


        private long? _processedcount;
        /// <summary>
        /// 370000700 Обработано записей (PROCESSED_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 370000700)]
        public long? ProcessedCount
        {
            get
            {
                CheckPropertyInited("ProcessedCount");
                return _processedcount;
            }
            set
            {
                _processedcount = value;
                NotifyPropertyChanged("ProcessedCount");
            }
        }


        private long? _totalfspcount;
        /// <summary>
        /// 370000800 Количество фсп для перерасчета (TOTAL_FSP_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 370000800)]
        public long? TotalFspCount
        {
            get
            {
                CheckPropertyInited("TotalFspCount");
                return _totalfspcount;
            }
            set
            {
                _totalfspcount = value;
                NotifyPropertyChanged("TotalFspCount");
            }
        }


        private long? _processedfspcont;
        /// <summary>
        /// 370000900 Количество рассчитанных ФСП (PROCESSED_FSP_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 370000900)]
        public long? ProcessedFspCont
        {
            get
            {
                CheckPropertyInited("ProcessedFspCont");
                return _processedfspcont;
            }
            set
            {
                _processedfspcont = value;
                NotifyPropertyChanged("ProcessedFspCont");
            }
        }


        private long? _errorcount;
        /// <summary>
        /// 370001000 Количество ошибочных записей (ERROR_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 370001000)]
        public long? ErrorCount
        {
            get
            {
                CheckPropertyInited("ErrorCount");
                return _errorcount;
            }
            set
            {
                _errorcount = value;
                NotifyPropertyChanged("ErrorCount");
            }
        }


        private long? _objecterrorcount;
        /// <summary>
        /// 370001100 Количество не связанных с объектом (OBJECT_ERROR_COUNT)
        /// </summary>
        [RegisterAttribute(AttributeID = 370001100)]
        public long? ObjectErrorCount
        {
            get
            {
                CheckPropertyInited("ObjectErrorCount");
                return _objecterrorcount;
            }
            set
            {
                _objecterrorcount = value;
                NotifyPropertyChanged("ObjectErrorCount");
            }
        }


        private string _errorlog;
        /// <summary>
        /// 370001200 Журнал ошибок (ERROR_LOG)
        /// </summary>
        [RegisterAttribute(AttributeID = 370001200)]
        public string ErrorLog
        {
            get
            {
                CheckPropertyInited("ErrorLog");
                return _errorlog;
            }
            set
            {
                _errorlog = value;
                NotifyPropertyChanged("ErrorLog");
            }
        }


        private long? _userid;
        /// <summary>
        /// 370001300 Пользователь (USER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 370001300)]
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

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 371 Реестр расчетных сводных показателей объектов страхования МКД (INSUR_SVOD_DATA_CALCULATED)
    /// </summary>
    [RegisterInfo(RegisterID = 371)]
    [Serializable]
    public sealed partial class OMBuildingSvodDataCalculated : OMBaseClass<OMBuildingSvodDataCalculated>
    {

        private long _id;
        /// <summary>
        /// 371000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 371000100)]
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


        private long? _unom;
        /// <summary>
        /// 371000200 УНОМ МКД (UNOM)
        /// </summary>
        [RegisterAttribute(AttributeID = 371000200)]
        public long? Unom
        {
            get
            {
                CheckPropertyInited("Unom");
                return _unom;
            }
            set
            {
                _unom = value;
                NotifyPropertyChanged("Unom");
            }
        }


        private long _linkmkd;
        /// <summary>
        /// 371000300 Ссылка на insur_building (LINK_MKD)
        /// </summary>
        [RegisterAttribute(AttributeID = 371000300)]
        public long LinkMkd
        {
            get
            {
                CheckPropertyInited("LinkMkd");
                return _linkmkd;
            }
            set
            {
                _linkmkd = value;
                NotifyPropertyChanged("LinkMkd");
            }
        }


        private long _kolgpegrn;
        /// <summary>
        /// 371000400 Расчетная величина количества квартир в МКД по данным ЕГРН (KOL_GP_EGRN)
        /// </summary>
        [RegisterAttribute(AttributeID = 371000400)]
        public long KolGpEgrn
        {
            get
            {
                CheckPropertyInited("KolGpEgrn");
                return _kolgpegrn;
            }
            set
            {
                _kolgpegrn = value;
                NotifyPropertyChanged("KolGpEgrn");
            }
        }


        private long _kolgpbti;
        /// <summary>
        /// 371000500 Расчетная величина количества квартир в МКД по данным БТИ (KOL_GP_BTI)
        /// </summary>
        [RegisterAttribute(AttributeID = 371000500)]
        public long KolGpBti
        {
            get
            {
                CheckPropertyInited("KolGpBti");
                return _kolgpbti;
            }
            set
            {
                _kolgpbti = value;
                NotifyPropertyChanged("KolGpBti");
            }
        }


        private long _kolgpmfc;
        /// <summary>
        /// 371000600 Расчетная величина количества квартир в МКД по данным МФЦ (KOL_GP_MFC)
        /// </summary>
        [RegisterAttribute(AttributeID = 371000600)]
        public long KolGpMfc
        {
            get
            {
                CheckPropertyInited("KolGpMfc");
                return _kolgpmfc;
            }
            set
            {
                _kolgpmfc = value;
                NotifyPropertyChanged("KolGpMfc");
            }
        }


        private decimal _nachmfctekperiod;
        /// <summary>
        /// 371000700 Сумма начислений по МКД за текущий период (NACH_MFC_TEK_PERIOD)
        /// </summary>
        [RegisterAttribute(AttributeID = 371000700)]
        public decimal NachMfcTekPeriod
        {
            get
            {
                CheckPropertyInited("NachMfcTekPeriod");
                return _nachmfctekperiod;
            }
            set
            {
                _nachmfctekperiod = value;
                NotifyPropertyChanged("NachMfcTekPeriod");
            }
        }


        private decimal _nachmfctekperiod1;
        /// <summary>
        /// 371000800 Сумма начислений по МКД за текущий период - 1 (NACH_MFC_TEK_PERIOD_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 371000800)]
        public decimal NachMfcTekPeriod1
        {
            get
            {
                CheckPropertyInited("NachMfcTekPeriod1");
                return _nachmfctekperiod1;
            }
            set
            {
                _nachmfctekperiod1 = value;
                NotifyPropertyChanged("NachMfcTekPeriod1");
            }
        }


        private decimal _nachmfctekperiod2;
        /// <summary>
        /// 371000900 Сумма начислений по МКД за текущий период - 2 (NACH_MFC_TEK_PERIOD_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 371000900)]
        public decimal NachMfcTekPeriod2
        {
            get
            {
                CheckPropertyInited("NachMfcTekPeriod2");
                return _nachmfctekperiod2;
            }
            set
            {
                _nachmfctekperiod2 = value;
                NotifyPropertyChanged("NachMfcTekPeriod2");
            }
        }


        private decimal _platmfctekperiod;
        /// <summary>
        /// 371001000 Сумма зачислений по МКД за текущий период (PLAT_MFC_TEK_PERIOD)
        /// </summary>
        [RegisterAttribute(AttributeID = 371001000)]
        public decimal PlatMfcTekPeriod
        {
            get
            {
                CheckPropertyInited("PlatMfcTekPeriod");
                return _platmfctekperiod;
            }
            set
            {
                _platmfctekperiod = value;
                NotifyPropertyChanged("PlatMfcTekPeriod");
            }
        }


        private decimal _platmfctekperiod1;
        /// <summary>
        /// 371001100 Сумма зачислений по МКД за текущий период - 1 (PLAT_MFC_TEK_PERIOD_1)
        /// </summary>
        [RegisterAttribute(AttributeID = 371001100)]
        public decimal PlatMfcTekPeriod1
        {
            get
            {
                CheckPropertyInited("PlatMfcTekPeriod1");
                return _platmfctekperiod1;
            }
            set
            {
                _platmfctekperiod1 = value;
                NotifyPropertyChanged("PlatMfcTekPeriod1");
            }
        }


        private decimal _platmfctekperiod2;
        /// <summary>
        /// 371001200 Сумма зачислений по МКД за текущий период - 2 (PLAT_MFC_TEK_PERIOD_2)
        /// </summary>
        [RegisterAttribute(AttributeID = 371001200)]
        public decimal PlatMfcTekPeriod2
        {
            get
            {
                CheckPropertyInited("PlatMfcTekPeriod2");
                return _platmfctekperiod2;
            }
            set
            {
                _platmfctekperiod2 = value;
                NotifyPropertyChanged("PlatMfcTekPeriod2");
            }
        }


        private long _linkbti;
        /// <summary>
        /// 371001300 Ссылка на bti_building (LINK_BTI)
        /// </summary>
        [RegisterAttribute(AttributeID = 371001300)]
        public long LinkBti
        {
            get
            {
                CheckPropertyInited("LinkBti");
                return _linkbti;
            }
            set
            {
                _linkbti = value;
                NotifyPropertyChanged("LinkBti");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 372 Подписанты (FM_PODPISANT)
    /// </summary>
    [RegisterInfo(RegisterID = 372)]
    [Serializable]
    public sealed partial class OMPodpisant : OMBaseClass<OMPodpisant>
    {

        private long _id;
        /// <summary>
        /// 37200100 ИД (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 37200100)]
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
        /// 37200200 Код (CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 37200200)]
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


        private string _name;
        /// <summary>
        /// 37200300 Наименование (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 37200300)]
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


        private string _post;
        /// <summary>
        /// 37200400 Должность (POST)
        /// </summary>
        [RegisterAttribute(AttributeID = 37200400)]
        public string Post
        {
            get
            {
                CheckPropertyInited("Post");
                return _post;
            }
            set
            {
                _post = value;
                NotifyPropertyChanged("Post");
            }
        }


        private string _text;
        /// <summary>
        /// 37200500 Формулировка "в лице" (TEXT)
        /// </summary>
        [RegisterAttribute(AttributeID = 37200500)]
        public string Text
        {
            get
            {
                CheckPropertyInited("Text");
                return _text;
            }
            set
            {
                _text = value;
                NotifyPropertyChanged("Text");
            }
        }


        private bool _isdeleted;
        /// <summary>
        /// 37200600 Признак удаления (IS_DELETED)
        /// </summary>
        [RegisterAttribute(AttributeID = 37200600)]
        public bool IsDeleted
        {
            get
            {
                CheckPropertyInited("IsDeleted");
                return _isdeleted;
            }
            set
            {
                _isdeleted = value;
                NotifyPropertyChanged("IsDeleted");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 373 Реестр связи СК с кодом поставщика (INSUR_STRAH_POST)
    /// </summary>
    [RegisterInfo(RegisterID = 373)]
    [Serializable]
    public sealed partial class OMLinkStrahPost : OMBaseClass<OMLinkStrahPost>
    {

        private long _id;
        /// <summary>
        /// 373000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 373000100)]
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


        private DateTime _datestart;
        /// <summary>
        /// 373000200 Дата начала (DATE_START)
        /// </summary>
        [RegisterAttribute(AttributeID = 373000200)]
        public DateTime DateStart
        {
            get
            {
                CheckPropertyInited("DateStart");
                return _datestart;
            }
            set
            {
                _datestart = value;
                NotifyPropertyChanged("DateStart");
            }
        }


        private long _linkcompany;
        /// <summary>
        /// 373000300 Ссылка на страховую компанию (LINK_COMPANY)
        /// </summary>
        [RegisterAttribute(AttributeID = 373000300)]
        public long LinkCompany
        {
            get
            {
                CheckPropertyInited("LinkCompany");
                return _linkcompany;
            }
            set
            {
                _linkcompany = value;
                NotifyPropertyChanged("LinkCompany");
            }
        }


        private bool _statusnew;
        /// <summary>
        /// 373000400 Новый код (STATUS_NEW)
        /// </summary>
        [RegisterAttribute(AttributeID = 373000400)]
        public bool StatusNew
        {
            get
            {
                CheckPropertyInited("StatusNew");
                return _statusnew;
            }
            set
            {
                _statusnew = value;
                NotifyPropertyChanged("StatusNew");
            }
        }


        private long _kodpost;
        /// <summary>
        /// 373000500 Код поставщика (KOD_POST)
        /// </summary>
        [RegisterAttribute(AttributeID = 373000500)]
        public long KodPost
        {
            get
            {
                CheckPropertyInited("KodPost");
                return _kodpost;
            }
            set
            {
                _kodpost = value;
                NotifyPropertyChanged("KodPost");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 374 История изменения UNOM в ФСП (INSUR_UNOM_UPDATE)
    /// </summary>
    [RegisterInfo(RegisterID = 374)]
    [Serializable]
    public sealed partial class OMUnomUpdate : OMBaseClass<OMUnomUpdate>
    {

        private long _id;
        /// <summary>
        /// 374000100 Уникальный номер записи (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 374000100)]
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


        private long _oldunom;
        /// <summary>
        /// 374000200 Старый UNOM (UNOM_OLD)
        /// </summary>
        [RegisterAttribute(AttributeID = 374000200)]
        public long OldUnom
        {
            get
            {
                CheckPropertyInited("OldUnom");
                return _oldunom;
            }
            set
            {
                _oldunom = value;
                NotifyPropertyChanged("OldUnom");
            }
        }


        private long _newunom;
        /// <summary>
        /// 374000300 Новый UNOM (UNOM_NEW)
        /// </summary>
        [RegisterAttribute(AttributeID = 374000300)]
        public long NewUnom
        {
            get
            {
                CheckPropertyInited("NewUnom");
                return _newunom;
            }
            set
            {
                _newunom = value;
                NotifyPropertyChanged("NewUnom");
            }
        }


        private long _oldlinkmkd;
        /// <summary>
        /// 374000400 Старая ссылка на МКД (LINK_MKD_OLD)
        /// </summary>
        [RegisterAttribute(AttributeID = 374000400)]
        public long OldLinkMkd
        {
            get
            {
                CheckPropertyInited("OldLinkMkd");
                return _oldlinkmkd;
            }
            set
            {
                _oldlinkmkd = value;
                NotifyPropertyChanged("OldLinkMkd");
            }
        }


        private long _newlinkmkd;
        /// <summary>
        /// 374000500 Новая ссылка на МКД (LINK_MKD_NEW)
        /// </summary>
        [RegisterAttribute(AttributeID = 374000500)]
        public long NewLinkMkd
        {
            get
            {
                CheckPropertyInited("NewLinkMkd");
                return _newlinkmkd;
            }
            set
            {
                _newlinkmkd = value;
                NotifyPropertyChanged("NewLinkMkd");
            }
        }


        private DateTime _datechange;
        /// <summary>
        /// 374000600 Дата изменения (DATE_CHANGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 374000600)]
        public DateTime DateChange
        {
            get
            {
                CheckPropertyInited("DateChange");
                return _datechange;
            }
            set
            {
                _datechange = value;
                NotifyPropertyChanged("DateChange");
            }
        }


        private string _note;
        /// <summary>
        /// 374000700 Примечание (NOTE)
        /// </summary>
        [RegisterAttribute(AttributeID = 374000700)]
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


        private long _userchange;
        /// <summary>
        /// 374000800 Пользователь, изменивший запись (USER_CHANGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 374000800)]
        public long UserChange
        {
            get
            {
                CheckPropertyInited("UserChange");
                return _userchange;
            }
            set
            {
                _userchange = value;
                NotifyPropertyChanged("UserChange");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 380 АП - Общая статистика (AP_COMMON)
    /// </summary>
    [RegisterInfo(RegisterID = 380)]
    [Serializable]
    public sealed partial class OMApCommon : OMBaseClass<OMApCommon>
    {
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 381 Сводный реестр счетов (INSUR_INVOICE_SVOD)
    /// </summary>
    [RegisterInfo(RegisterID = 381)]
    [Serializable]
    public sealed partial class OMInvoiceSvod : OMBaseClass<OMInvoiceSvod>
    {

        private long _empid;
        /// <summary>
        /// 381000100 Уникальный номер (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 381000100)]
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


        private decimal? _sumsvod;
        /// <summary>
        /// 381000200 Сумма связанных счетов (SUM_SVOD)
        /// </summary>
        [RegisterAttribute(AttributeID = 381000200)]
        public decimal? SumSvod
        {
            get
            {
                CheckPropertyInited("SumSvod");
                return _sumsvod;
            }
            set
            {
                _sumsvod = value;
                NotifyPropertyChanged("SumSvod");
            }
        }


        private long? _invoicevsego;
        /// <summary>
        /// 381000300 Количество связанных счетов (INVOICE_VSEGO)
        /// </summary>
        [RegisterAttribute(AttributeID = 381000300)]
        public long? InvoiceVsego
        {
            get
            {
                CheckPropertyInited("InvoiceVsego");
                return _invoicevsego;
            }
            set
            {
                _invoicevsego = value;
                NotifyPropertyChanged("InvoiceVsego");
            }
        }


        private string _status;
        /// <summary>
        /// 381000400 Статус счетов (STATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 381000400)]
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


        private InvoiceStatus _status_Code;
        /// <summary>
        /// 381000400 Статус счетов (справочный код) (STATUS_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 381000400)]
        public InvoiceStatus Status_Code
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


        private long? _subjectid;
        /// <summary>
        /// 381000500 Ссылка на субъект-получатель (SUBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 381000500)]
        public long? SubjectId
        {
            get
            {
                CheckPropertyInited("SubjectId");
                return _subjectid;
            }
            set
            {
                _subjectid = value;
                NotifyPropertyChanged("SubjectId");
            }
        }


        private string _subjectname;
        /// <summary>
        /// 381000600 Получатель выплаты (SUBJECT_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 381000600)]
        public string SubjectName
        {
            get
            {
                CheckPropertyInited("SubjectName");
                return _subjectname;
            }
            set
            {
                _subjectname = value;
                NotifyPropertyChanged("SubjectName");
            }
        }


        private string _numinvoice;
        /// <summary>
        /// 381000700 Номер счета (NUM_INVOICE)
        /// </summary>
        [RegisterAttribute(AttributeID = 381000700)]
        public string NumInvoice
        {
            get
            {
                CheckPropertyInited("NumInvoice");
                return _numinvoice;
            }
            set
            {
                _numinvoice = value;
                NotifyPropertyChanged("NumInvoice");
            }
        }


        private DateTime? _dateinvoice;
        /// <summary>
        /// 381000800 Дата счета (DATE_INVOICE)
        /// </summary>
        [RegisterAttribute(AttributeID = 381000800)]
        public DateTime? DateInvoice
        {
            get
            {
                CheckPropertyInited("DateInvoice");
                return _dateinvoice;
            }
            set
            {
                _dateinvoice = value;
                NotifyPropertyChanged("DateInvoice");
            }
        }


        private long? _linkreestrpay;
        /// <summary>
        /// 381000900 Ссылка на INSUR_REESTR_PAY ( Реестр оплат ) (LINK_REESTR_PAY)
        /// </summary>
        [RegisterAttribute(AttributeID = 381000900)]
        public long? LinkReestrPay
        {
            get
            {
                CheckPropertyInited("LinkReestrPay");
                return _linkreestrpay;
            }
            set
            {
                _linkreestrpay = value;
                NotifyPropertyChanged("LinkReestrPay");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 382 Реестр управляющих компаний (INSUR_ORG_UNOM)
    /// </summary>
    [RegisterInfo(RegisterID = 382)]
    [Serializable]
    public sealed partial class OMOrgUnom : OMBaseClass<OMOrgUnom>
    {

        private long _empid;
        /// <summary>
        /// 382000100 Уникальный номер (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 382000100)]
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


        private string _org;
        /// <summary>
        /// 382000200 Форма объединения организации (ORG)
        /// </summary>
        [RegisterAttribute(AttributeID = 382000200)]
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


        private FormAssociationOwners _org_Code;
        /// <summary>
        /// 382000200 Форма объединения организации (справочный код) (ORG_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 382000200)]
        public FormAssociationOwners Org_Code
        {
            get
            {
                CheckPropertyInited("Org_Code");
                return this._org_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_org))
                    {
                         _org = descr;
                    }
                }
                else
                {
                     _org = descr;
                }

                this._org_Code = value;
                NotifyPropertyChanged("Org");
                NotifyPropertyChanged("Org_Code");
            }
        }


        private long? _orgid;
        /// <summary>
        /// 382000300 Код управляющей компании (ORG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 382000300)]
        public long? OrgId
        {
            get
            {
                CheckPropertyInited("OrgId");
                return _orgid;
            }
            set
            {
                _orgid = value;
                NotifyPropertyChanged("OrgId");
            }
        }


        private long? _subjid;
        /// <summary>
        /// 382000400 Идентификатор субъекта (Ссылка на INSUR_SUBJECT) (SUBJ_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 382000400)]
        public long? SubjId
        {
            get
            {
                CheckPropertyInited("SubjId");
                return _subjid;
            }
            set
            {
                _subjid = value;
                NotifyPropertyChanged("SubjId");
            }
        }


        private long? _objid;
        /// <summary>
        /// 382000500 Идентификатор объекта (Ссылка на INSUR_BUILDING) (OBJ_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 382000500)]
        public long? ObjId
        {
            get
            {
                CheckPropertyInited("ObjId");
                return _objid;
            }
            set
            {
                _objid = value;
                NotifyPropertyChanged("ObjId");
            }
        }


        private DateTime? _dateinput;
        /// <summary>
        /// 382000600 Дата ввода/обновления информации об ORG_ID (DATE_INPUT)
        /// </summary>
        [RegisterAttribute(AttributeID = 382000600)]
        public DateTime? DateInput
        {
            get
            {
                CheckPropertyInited("DateInput");
                return _dateinput;
            }
            set
            {
                _dateinput = value;
                NotifyPropertyChanged("DateInput");
            }
        }


        private long? _kodpost;
        /// <summary>
        /// 382000700 Код поставщика (KOD_POST)
        /// </summary>
        [RegisterAttribute(AttributeID = 382000700)]
        public long? KodPost
        {
            get
            {
                CheckPropertyInited("KodPost");
                return _kodpost;
            }
            set
            {
                _kodpost = value;
                NotifyPropertyChanged("KodPost");
            }
        }


        private long? _kodysl;
        /// <summary>
        /// 382000800 Код услуги (KOD_YSL)
        /// </summary>
        [RegisterAttribute(AttributeID = 382000800)]
        public long? KodYsl
        {
            get
            {
                CheckPropertyInited("KodYsl");
                return _kodysl;
            }
            set
            {
                _kodysl = value;
                NotifyPropertyChanged("KodYsl");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 383 Реестр связи ФСП и объектами ЖП (INSUR_LINK_FSP_FLAT)
    /// </summary>
    [RegisterInfo(RegisterID = 383)]
    [Serializable]
    public sealed partial class OMLinkFspFlat : OMBaseClass<OMLinkFspFlat>
    {

        private long _empid;
        /// <summary>
        /// 383000100 Уникальный идентификатор (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 383000100)]
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


        private long? _fspid;
        /// <summary>
        /// 383000200 Ссылка на INSUR_FSP (FSP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 383000200)]
        public long? FspId
        {
            get
            {
                CheckPropertyInited("FspId");
                return _fspid;
            }
            set
            {
                _fspid = value;
                NotifyPropertyChanged("FspId");
            }
        }


        private long? _objid;
        /// <summary>
        /// 383000300 Ссылка на INSUR_FLAT (OBJ_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 383000300)]
        public long? ObjId
        {
            get
            {
                CheckPropertyInited("ObjId");
                return _objid;
            }
            set
            {
                _objid = value;
                NotifyPropertyChanged("ObjId");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 384 Статистика по ущербу по жилым помещениям (V_DJP_DAMAGE_STAT)
    /// </summary>
    [RegisterInfo(RegisterID = 384)]
    [Serializable]
    public sealed partial class OMvDjpDamageStat : OMBaseClass<OMvDjpDamageStat>
    {

        private DateTime? _column384000100;
        /// <summary>
        /// 384000100 Период ("Период")
        /// </summary>
        [RegisterAttribute(AttributeID = 384000100)]
        public DateTime? Column384000100
        {
            get
            {
                CheckPropertyInited("Column384000100");
                return _column384000100;
            }
            set
            {
                _column384000100 = value;
                NotifyPropertyChanged("Column384000100");
            }
        }


        private string _column384000200;
        /// <summary>
        /// 384000200 Округ ("Округ")
        /// </summary>
        [RegisterAttribute(AttributeID = 384000200)]
        public string Column384000200
        {
            get
            {
                CheckPropertyInited("Column384000200");
                return _column384000200;
            }
            set
            {
                _column384000200 = value;
                NotifyPropertyChanged("Column384000200");
            }
        }


        private decimal? _column384000300;
        /// <summary>
        /// 384000300 Количество заключений ("Количество заключений")
        /// </summary>
        [RegisterAttribute(AttributeID = 384000300)]
        public decimal? Column384000300
        {
            get
            {
                CheckPropertyInited("Column384000300");
                return _column384000300;
            }
            set
            {
                _column384000300 = value;
                NotifyPropertyChanged("Column384000300");
            }
        }


        private decimal? _column384000400;
        /// <summary>
        /// 384000400 Количество СС ("Количество СС")
        /// </summary>
        [RegisterAttribute(AttributeID = 384000400)]
        public decimal? Column384000400
        {
            get
            {
                CheckPropertyInited("Column384000400");
                return _column384000400;
            }
            set
            {
                _column384000400 = value;
                NotifyPropertyChanged("Column384000400");
            }
        }


        private decimal? _column384000500;
        /// <summary>
        /// 384000500 Ущерб ("Ущерб")
        /// </summary>
        [RegisterAttribute(AttributeID = 384000500)]
        public decimal? Column384000500
        {
            get
            {
                CheckPropertyInited("Column384000500");
                return _column384000500;
            }
            set
            {
                _column384000500 = value;
                NotifyPropertyChanged("Column384000500");
            }
        }


        private decimal? _column384000600;
        /// <summary>
        /// 384000600 Выплата из бюджета города ("Выплата из бюджета города")
        /// </summary>
        [RegisterAttribute(AttributeID = 384000600)]
        public decimal? Column384000600
        {
            get
            {
                CheckPropertyInited("Column384000600");
                return _column384000600;
            }
            set
            {
                _column384000600 = value;
                NotifyPropertyChanged("Column384000600");
            }
        }


        private decimal? _column384000700;
        /// <summary>
        /// 384000700 Выплата СК ("Выплата СК")
        /// </summary>
        [RegisterAttribute(AttributeID = 384000700)]
        public decimal? Column384000700
        {
            get
            {
                CheckPropertyInited("Column384000700");
                return _column384000700;
            }
            set
            {
                _column384000700 = value;
                NotifyPropertyChanged("Column384000700");
            }
        }


        private decimal? _column384000800;
        /// <summary>
        /// 384000800 Среднее возмещение по СС ("Среднее возмещение по СС")
        /// </summary>
        [RegisterAttribute(AttributeID = 384000800)]
        public decimal? Column384000800
        {
            get
            {
                CheckPropertyInited("Column384000800");
                return _column384000800;
            }
            set
            {
                _column384000800 = value;
                NotifyPropertyChanged("Column384000800");
            }
        }


        private decimal? _column384000900;
        /// <summary>
        /// 384000900 Средняя выплата из бюджета по СС ("Средняя выплата из бюджета по СС")
        /// </summary>
        [RegisterAttribute(AttributeID = 384000900)]
        public decimal? Column384000900
        {
            get
            {
                CheckPropertyInited("Column384000900");
                return _column384000900;
            }
            set
            {
                _column384000900 = value;
                NotifyPropertyChanged("Column384000900");
            }
        }


        private long? _column384001000;
        /// <summary>
        /// 384001000 damage_id (damage_id)
        /// </summary>
        [RegisterAttribute(AttributeID = 384001000)]
        public long? Column384001000
        {
            get
            {
                CheckPropertyInited("Column384001000");
                return _column384001000;
            }
            set
            {
                _column384001000 = value;
                NotifyPropertyChanged("Column384001000");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 385 Сптсок округов для выгрузок из реестра расчетов (V_OKRUG2IO)
    /// </summary>
    [RegisterInfo(RegisterID = 385)]
    [Serializable]
    public sealed partial class OMvOkrug2IO : OMBaseClass<OMvOkrug2IO>
    {

        private long? _column385000100;
        /// <summary>
        /// 385000100 Идентификатор округа ("Идентификатор округа")
        /// </summary>
        [PrimaryKey(AttributeID = 385000100)]
        public long? Column385000100
        {
            get
            {
                CheckPropertyInited("Column385000100");
                return _column385000100;
            }
            set
            {
                _column385000100 = value;
                NotifyPropertyChanged("Column385000100");
            }
        }


        private string _column385000200;
        /// <summary>
        /// 385000200 Округ ("Округ")
        /// </summary>
        [RegisterAttribute(AttributeID = 385000200)]
        public string Column385000200
        {
            get
            {
                CheckPropertyInited("Column385000200");
                return _column385000200;
            }
            set
            {
                _column385000200 = value;
                NotifyPropertyChanged("Column385000200");
            }
        }


        private long? _column385000300;
        /// <summary>
        /// 385000300 Идентификатор СК ("Идентификатор СК")
        /// </summary>
        [RegisterAttribute(AttributeID = 385000300)]
        public long? Column385000300
        {
            get
            {
                CheckPropertyInited("Column385000300");
                return _column385000300;
            }
            set
            {
                _column385000300 = value;
                NotifyPropertyChanged("Column385000300");
            }
        }


        private long? _column385000400;
        /// <summary>
        /// 385000400 Код СК ("Код СК")
        /// </summary>
        [RegisterAttribute(AttributeID = 385000400)]
        public long? Column385000400
        {
            get
            {
                CheckPropertyInited("Column385000400");
                return _column385000400;
            }
            set
            {
                _column385000400 = value;
                NotifyPropertyChanged("Column385000400");
            }
        }


        private string _column385000500;
        /// <summary>
        /// 385000500 Краткое наимеование СК ("Краткое наимеование СК")
        /// </summary>
        [RegisterAttribute(AttributeID = 385000500)]
        public string Column385000500
        {
            get
            {
                CheckPropertyInited("Column385000500");
                return _column385000500;
            }
            set
            {
                _column385000500 = value;
                NotifyPropertyChanged("Column385000500");
            }
        }


        private string _column385000600;
        /// <summary>
        /// 385000600 Наименование СК ("Наименование СК")
        /// </summary>
        [RegisterAttribute(AttributeID = 385000600)]
        public string Column385000600
        {
            get
            {
                CheckPropertyInited("Column385000600");
                return _column385000600;
            }
            set
            {
                _column385000600 = value;
                NotifyPropertyChanged("Column385000600");
            }
        }


        private long? _column385000700;
        /// <summary>
        /// 385000700 calc_id (calc_id)
        /// </summary>
        [RegisterAttribute(AttributeID = 385000700)]
        public long? Column385000700
        {
            get
            {
                CheckPropertyInited("Column385000700");
                return _column385000700;
            }
            set
            {
                _column385000700 = value;
                NotifyPropertyChanged("Column385000700");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 386 Месяцы для выгрузок по договорам (V_MONTHS4ALL_PROV)
    /// </summary>
    [RegisterInfo(RegisterID = 386)]
    [Serializable]
    public sealed partial class OMvMonth4AP : OMBaseClass<OMvMonth4AP>
    {

        private long? _column386000100;
        /// <summary>
        /// 386000100 Порядковый номер ("Порядковый номер")
        /// </summary>
        [PrimaryKey(AttributeID = 386000100)]
        public long? Column386000100
        {
            get
            {
                CheckPropertyInited("Column386000100");
                return _column386000100;
            }
            set
            {
                _column386000100 = value;
                NotifyPropertyChanged("Column386000100");
            }
        }


        private string _column386000200;
        /// <summary>
        /// 386000200 Тип ("Тип")
        /// </summary>
        [RegisterAttribute(AttributeID = 386000200)]
        public string Column386000200
        {
            get
            {
                CheckPropertyInited("Column386000200");
                return _column386000200;
            }
            set
            {
                _column386000200 = value;
                NotifyPropertyChanged("Column386000200");
            }
        }


        private string _column386000300;
        /// <summary>
        /// 386000300 Номер ("Номер")
        /// </summary>
        [RegisterAttribute(AttributeID = 386000300)]
        public string Column386000300
        {
            get
            {
                CheckPropertyInited("Column386000300");
                return _column386000300;
            }
            set
            {
                _column386000300 = value;
                NotifyPropertyChanged("Column386000300");
            }
        }


        private string _column386000400;
        /// <summary>
        /// 386000400 Месяц ("Месяц")
        /// </summary>
        [RegisterAttribute(AttributeID = 386000400)]
        public string Column386000400
        {
            get
            {
                CheckPropertyInited("Column386000400");
                return _column386000400;
            }
            set
            {
                _column386000400 = value;
                NotifyPropertyChanged("Column386000400");
            }
        }


        private long? _column386000500;
        /// <summary>
        /// 386000500 prop_id (prop_id)
        /// </summary>
        [RegisterAttribute(AttributeID = 386000500)]
        public long? Column386000500
        {
            get
            {
                CheckPropertyInited("Column386000500");
                return _column386000500;
            }
            set
            {
                _column386000500 = value;
                NotifyPropertyChanged("Column386000500");
            }
        }

    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 387 Реестр Управляющих компаний (INSUR_UPRAV_COMPANY)
    /// </summary>
    [RegisterInfo(RegisterID = 387)]
    [Serializable]
    public sealed partial class OMUpravCompany : OMBaseClass<OMUpravCompany>
    {

        private long _empid;
        /// <summary>
        /// 387000100 Уникальный номер (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 387000100)]
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


        private long? _okrygid;
        /// <summary>
        /// 387000200 Ссылка на ID округа в справочнике БТИ (OKRYG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 387000200)]
        public long? OkrygId
        {
            get
            {
                CheckPropertyInited("OkrygId");
                return _okrygid;
            }
            set
            {
                _okrygid = value;
                NotifyPropertyChanged("OkrygId");
            }
        }


        private string _kodorg;
        /// <summary>
        /// 387000300 Код организации (KOD_ORG)
        /// </summary>
        [RegisterAttribute(AttributeID = 387000300)]
        public string KodOrg
        {
            get
            {
                CheckPropertyInited("KodOrg");
                return _kodorg;
            }
            set
            {
                _kodorg = value;
                NotifyPropertyChanged("KodOrg");
            }
        }


        private long? _orgid;
        /// <summary>
        /// 387000400 Уникальный номер УК (ORG_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 387000400)]
        public long? OrgId
        {
            get
            {
                CheckPropertyInited("OrgId");
                return _orgid;
            }
            set
            {
                _orgid = value;
                NotifyPropertyChanged("OrgId");
            }
        }


        private string _inn;
        /// <summary>
        /// 387000500 ИНН УК (INN)
        /// </summary>
        [RegisterAttribute(AttributeID = 387000500)]
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


        private string _orgname;
        /// <summary>
        /// 387000600 Название УК ( полное) (ORG_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 387000600)]
        public string OrgName
        {
            get
            {
                CheckPropertyInited("OrgName");
                return _orgname;
            }
            set
            {
                _orgname = value;
                NotifyPropertyChanged("OrgName");
            }
        }


        private string _orgidt;
        /// <summary>
        /// 387000700 Название УК ( сокращенное) (ORG_ID_T)
        /// </summary>
        [RegisterAttribute(AttributeID = 387000700)]
        public string OrgIdT
        {
            get
            {
                CheckPropertyInited("OrgIdT");
                return _orgidt;
            }
            set
            {
                _orgidt = value;
                NotifyPropertyChanged("OrgIdT");
            }
        }


        private string _emplrole;
        /// <summary>
        /// 387000800 Должность ответственного лица (EMPL_ROLE)
        /// </summary>
        [RegisterAttribute(AttributeID = 387000800)]
        public string EmplRole
        {
            get
            {
                CheckPropertyInited("EmplRole");
                return _emplrole;
            }
            set
            {
                _emplrole = value;
                NotifyPropertyChanged("EmplRole");
            }
        }


        private string _fio;
        /// <summary>
        /// 387000900 ФИО (FIO)
        /// </summary>
        [RegisterAttribute(AttributeID = 387000900)]
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


        private string _orgaddru;
        /// <summary>
        /// 387001000 Юридический адрес (ORG_ADDR_U)
        /// </summary>
        [RegisterAttribute(AttributeID = 387001000)]
        public string OrgAddrU
        {
            get
            {
                CheckPropertyInited("OrgAddrU");
                return _orgaddru;
            }
            set
            {
                _orgaddru = value;
                NotifyPropertyChanged("OrgAddrU");
            }
        }


        private string _orgaddrf;
        /// <summary>
        /// 387001100 Фактический Адрес (ORG_ADDR_F)
        /// </summary>
        [RegisterAttribute(AttributeID = 387001100)]
        public string OrgAddrF
        {
            get
            {
                CheckPropertyInited("OrgAddrF");
                return _orgaddrf;
            }
            set
            {
                _orgaddrf = value;
                NotifyPropertyChanged("OrgAddrF");
            }
        }


        private string _orgphone;
        /// <summary>
        /// 387001200 Телефон (ORG_PHONE)
        /// </summary>
        [RegisterAttribute(AttributeID = 387001200)]
        public string OrgPhone
        {
            get
            {
                CheckPropertyInited("OrgPhone");
                return _orgphone;
            }
            set
            {
                _orgphone = value;
                NotifyPropertyChanged("OrgPhone");
            }
        }

    }
}

namespace ObjectModel.Ehd
{
    /// <summary>
    /// 400 Объекты ЕГРН (EHD_BUILD_PARCEL_Q)
    /// </summary>
    [RegisterInfo(RegisterID = 400)]
    [Serializable]
    public sealed partial class OMBuildParcel : OMBaseClass<OMBuildParcel>
    {

        private long _empid;
        /// <summary>
        /// 400000100 Идентификатор (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 400000100)]
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


        private long? _buildingparcelid;
        /// <summary>
        /// 400000200 ehd.building_parcel.id (BUILDING_PARCEL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 400000200)]
        public long? BuildingParcelId
        {
            get
            {
                CheckPropertyInited("BuildingParcelId");
                return _buildingparcelid;
            }
            set
            {
                _buildingparcelid = value;
                NotifyPropertyChanged("BuildingParcelId");
            }
        }


        private long? _globalid;
        /// <summary>
        /// 400000300 ehd.building_parcel.global_id (GLOBAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 400000300)]
        public long? GlobalId
        {
            get
            {
                CheckPropertyInited("GlobalId");
                return _globalid;
            }
            set
            {
                _globalid = value;
                NotifyPropertyChanged("GlobalId");
            }
        }


        private string _name;
        /// <summary>
        /// 400000400 Наименование (ehd.building_parcel.name) (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 400000400)]
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


        private string _assignationcode;
        /// <summary>
        /// 400000500 Код назначения (ehd.building_parcel.assignation_code) (ASSIGNATION_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 400000500)]
        public string AssignationCode
        {
            get
            {
                CheckPropertyInited("AssignationCode");
                return _assignationcode;
            }
            set
            {
                _assignationcode = value;
                NotifyPropertyChanged("AssignationCode");
            }
        }


        private decimal? _area;
        /// <summary>
        /// 400000600 Площадь в квадратных метрах (ehd.building_parcel.area) (AREA)
        /// </summary>
        [RegisterAttribute(AttributeID = 400000600)]
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


        private string _notes;
        /// <summary>
        /// 400000700 Примечание (ehd.building_parcel.notes) (NOTES)
        /// </summary>
        [RegisterAttribute(AttributeID = 400000700)]
        public string Notes
        {
            get
            {
                CheckPropertyInited("Notes");
                return _notes;
            }
            set
            {
                _notes = value;
                NotifyPropertyChanged("Notes");
            }
        }


        private string _assignationname;
        /// <summary>
        /// 400000800 Назначение (ASSIGNATION_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 400000800)]
        public string AssignationName
        {
            get
            {
                CheckPropertyInited("AssignationName");
                return _assignationname;
            }
            set
            {
                _assignationname = value;
                NotifyPropertyChanged("AssignationName");
            }
        }


        private BuildingPurposeRosreestr _assignationname_Code;
        /// <summary>
        /// 400000800 Назначение (справочный код) (ASSIGNATION_NAME_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 400000800)]
        public BuildingPurposeRosreestr AssignationName_Code
        {
            get
            {
                CheckPropertyInited("AssignationName_Code");
                return this._assignationname_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_assignationname))
                    {
                         _assignationname = descr;
                    }
                }
                else
                {
                     _assignationname = descr;
                }

                this._assignationname_Code = value;
                NotifyPropertyChanged("AssignationName");
                NotifyPropertyChanged("AssignationName_Code");
            }
        }


        private decimal? _degreereadiness;
        /// <summary>
        /// 400000900 Степень готовности в процентах (DEGREE_READINESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 400000900)]
        public decimal? DegreeReadiness
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


        private long? _actualehd;
        /// <summary>
        /// 400001000 global_id последней версии данного объекта, null для последней версии (ACTUAL_EHD)
        /// </summary>
        [RegisterAttribute(AttributeID = 400001000)]
        public long? ActualEHD
        {
            get
            {
                CheckPropertyInited("ActualEHD");
                return _actualehd;
            }
            set
            {
                _actualehd = value;
                NotifyPropertyChanged("ActualEHD");
            }
        }


        private DateTime? _updatedateehd;
        /// <summary>
        /// 400001100 Дата данного обновления (UPDATE_DATE_EHD)
        /// </summary>
        [RegisterAttribute(AttributeID = 400001100)]
        public DateTime? UpdateDateEHD
        {
            get
            {
                CheckPropertyInited("UpdateDateEHD");
                return _updatedateehd;
            }
            set
            {
                _updatedateehd = value;
                NotifyPropertyChanged("UpdateDateEHD");
            }
        }


        private string _type;
        /// <summary>
        /// 400001200 Виды объектов государственного кадастра недвижимости (гкн) и единого государственного реестра прав на недвижимое имущество и сделок с ним (егрп) (TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 400001200)]
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


        private long? _type_Code;
        /// <summary>
        /// 400001200 Виды объектов государственного кадастра недвижимости (гкн) и единого государственного реестра прав на недвижимое имущество и сделок с ним (егрп) (справочный код) (TYPE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 400001200)]
        public long? Type_Code
        {
            get
            {
                CheckPropertyInited("Type_Code");
                return _type_Code;
            }
            set
            {
                _type_Code = value;
                NotifyPropertyChanged("Type_Code");
            }
        }


        private string _subbuildings;
        /// <summary>
        /// 400001300 Сведения о частях здания (SUBBUILDINGS)
        /// </summary>
        [RegisterAttribute(AttributeID = 400001300)]
        public string Subbuildings
        {
            get
            {
                CheckPropertyInited("Subbuildings");
                return _subbuildings;
            }
            set
            {
                _subbuildings = value;
                NotifyPropertyChanged("Subbuildings");
            }
        }


        private string _objectid;
        /// <summary>
        /// 400001400 Кадастровый номер объекта (OBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 400001400)]
        public string ObjectId
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


        private long? _packageid;
        /// <summary>
        /// 400001500 ehd.building_parcel.package_id (PACKAGE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 400001500)]
        public long? PackageId
        {
            get
            {
                CheckPropertyInited("PackageId");
                return _packageid;
            }
            set
            {
                _packageid = value;
                NotifyPropertyChanged("PackageId");
            }
        }


        private DateTime? _actualondate;
        /// <summary>
        /// 400001600 Дата актуальности (ACTUAL_ON_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 400001600)]
        public DateTime? ActualOnDate
        {
            get
            {
                CheckPropertyInited("ActualOnDate");
                return _actualondate;
            }
            set
            {
                _actualondate = value;
                NotifyPropertyChanged("ActualOnDate");
            }
        }


        private DateTime? _loaddate;
        /// <summary>
        /// 400001700 Дата загрузки (LOAD_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 400001700)]
        public DateTime? LoadDate
        {
            get
            {
                CheckPropertyInited("LoadDate");
                return _loaddate;
            }
            set
            {
                _loaddate = value;
                NotifyPropertyChanged("LoadDate");
            }
        }

    }
}

namespace ObjectModel.Ehd
{
    /// <summary>
    /// 401 ehd.register (EHD_REGISTER_Q)
    /// </summary>
    [RegisterInfo(RegisterID = 401)]
    [Serializable]
    public sealed partial class OMRegister : OMBaseClass<OMRegister>
    {

        private long _empid;
        /// <summary>
        /// 401000100 Идентификатор (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 401000100)]
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


        private DateTime? _loaddate;
        /// <summary>
        /// 401000200 Дата загрузки (LOAD_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 401000200)]
        public DateTime? LoadDate
        {
            get
            {
                CheckPropertyInited("LoadDate");
                return _loaddate;
            }
            set
            {
                _loaddate = value;
                NotifyPropertyChanged("LoadDate");
            }
        }


        private long? _buildingparcelid;
        /// <summary>
        /// 401000300 ehd.register.BUILDING_PARCEL_ID (BUILDING_PARCEL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 401000300)]
        public long? BuildingParcelId
        {
            get
            {
                CheckPropertyInited("BuildingParcelId");
                return _buildingparcelid;
            }
            set
            {
                _buildingparcelid = value;
                NotifyPropertyChanged("BuildingParcelId");
            }
        }


        private long? _globalid;
        /// <summary>
        /// 401000400 ehd.register.global_id (GLOBAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 401000400)]
        public long? GlobalId
        {
            get
            {
                CheckPropertyInited("GlobalId");
                return _globalid;
            }
            set
            {
                _globalid = value;
                NotifyPropertyChanged("GlobalId");
            }
        }


        private string _cadastralnumberparent;
        /// <summary>
        /// 401000500 Кадастровый номер квартала (ehd.register.cadastral_number_parent) (CADASTRAL_NUMBER_PARENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 401000500)]
        public string CadastralNumberParent
        {
            get
            {
                CheckPropertyInited("CadastralNumberParent");
                return _cadastralnumberparent;
            }
            set
            {
                _cadastralnumberparent = value;
                NotifyPropertyChanged("CadastralNumberParent");
            }
        }


        private string _cadastralnumber;
        /// <summary>
        /// 401000600 Кадастровый номер (ehd.register.cadastral_number) (CADASTRAL_NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 401000600)]
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


        private DateTime? _datecreated;
        /// <summary>
        /// 401000700 Дата постановки на учет (ehd.register.date_created) (DATE_CREATED)
        /// </summary>
        [RegisterAttribute(AttributeID = 401000700)]
        public DateTime? DateCreated
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


        private DateTime? _dateremoved;
        /// <summary>
        /// 401000800 Дата снятия с учета (ehd.register.date_removed) (DATE_REMOVED)
        /// </summary>
        [RegisterAttribute(AttributeID = 401000800)]
        public DateTime? DateRemoved
        {
            get
            {
                CheckPropertyInited("DateRemoved");
                return _dateremoved;
            }
            set
            {
                _dateremoved = value;
                NotifyPropertyChanged("DateRemoved");
            }
        }


        private string _state;
        /// <summary>
        /// 401000900 Статус объекта Росреестр (STATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 401000900)]
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


        private State _state_Code;
        /// <summary>
        /// 401000900 Статус объекта Росреестр (справочный код) (STATE_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 401000900)]
        public State State_Code
        {
            get
            {
                CheckPropertyInited("State_Code");
                return this._state_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_state))
                    {
                         _state = descr;
                    }
                }
                else
                {
                     _state = descr;
                }

                this._state_Code = value;
                NotifyPropertyChanged("State");
                NotifyPropertyChanged("State_Code");
            }
        }


        private string _method;
        /// <summary>
        /// 401001000 Способ образования объекта (ehd.register.method) (METHOD)
        /// </summary>
        [RegisterAttribute(AttributeID = 401001000)]
        public string Method
        {
            get
            {
                CheckPropertyInited("Method");
                return _method;
            }
            set
            {
                _method = value;
                NotifyPropertyChanged("Method");
            }
        }


        private string _cadastralnumberoks;
        /// <summary>
        /// 401001100 Кадастровый номер здания или сооружения, в котором расположено помещение (CADASTRAL_NUMBER_OKS)
        /// </summary>
        [RegisterAttribute(AttributeID = 401001100)]
        public string CadastralNumberOks
        {
            get
            {
                CheckPropertyInited("CadastralNumberOks");
                return _cadastralnumberoks;
            }
            set
            {
                _cadastralnumberoks = value;
                NotifyPropertyChanged("CadastralNumberOks");
            }
        }


        private string _cadastralnumberkk;
        /// <summary>
        /// 401001200 Кадастровый номер кк, в котором расположено помещение (CADASTRAL_NUMBER_KK)
        /// </summary>
        [RegisterAttribute(AttributeID = 401001200)]
        public string CadastralNumberKk
        {
            get
            {
                CheckPropertyInited("CadastralNumberKk");
                return _cadastralnumberkk;
            }
            set
            {
                _cadastralnumberkk = value;
                NotifyPropertyChanged("CadastralNumberKk");
            }
        }


        private string _cadastralnumberflat;
        /// <summary>
        /// 401001300 Кадастровый номер квартиры, в которой расположена комната (CADASTRAL_NUMBER_FLAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 401001300)]
        public string CadastralNumberFlat
        {
            get
            {
                CheckPropertyInited("CadastralNumberFlat");
                return _cadastralnumberflat;
            }
            set
            {
                _cadastralnumberflat = value;
                NotifyPropertyChanged("CadastralNumberFlat");
            }
        }


        private string _totalass;
        /// <summary>
        /// 401001400 Нежилое помещение, являющееся общим имуществом в многоквартирном доме (TOTALASS)
        /// </summary>
        [RegisterAttribute(AttributeID = 401001400)]
        public string TotalAss
        {
            get
            {
                CheckPropertyInited("TotalAss");
                return _totalass;
            }
            set
            {
                _totalass = value;
                NotifyPropertyChanged("TotalAss");
            }
        }


        private string _assftp1;
        /// <summary>
        /// 401001500 Классификатор назначений помещений (ASSFTP1)
        /// </summary>
        [RegisterAttribute(AttributeID = 401001500)]
        public string Assftp1
        {
            get
            {
                CheckPropertyInited("Assftp1");
                return _assftp1;
            }
            set
            {
                _assftp1 = value;
                NotifyPropertyChanged("Assftp1");
            }
        }


        private Assftp1 _assftp1_Code;
        /// <summary>
        /// 401001500 Классификатор назначений помещений (справочный код) (ASSFTP1_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 401001500)]
        public Assftp1 Assftp1_Code
        {
            get
            {
                CheckPropertyInited("Assftp1_Code");
                return this._assftp1_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_assftp1))
                    {
                         _assftp1 = descr;
                    }
                }
                else
                {
                     _assftp1 = descr;
                }

                this._assftp1_Code = value;
                NotifyPropertyChanged("Assftp1");
                NotifyPropertyChanged("Assftp1_Code");
            }
        }


        private string _assftpcd;
        /// <summary>
        /// 401001600 Код назначения помещения (ASSFTP_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 401001600)]
        public string AssftpCd
        {
            get
            {
                CheckPropertyInited("AssftpCd");
                return _assftpcd;
            }
            set
            {
                _assftpcd = value;
                NotifyPropertyChanged("AssftpCd");
            }
        }


        private Assftp_cd _assftpcd_Code;
        /// <summary>
        /// 401001600 Код назначения помещения (справочный код) (ASSFTP_CD_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 401001600)]
        public Assftp_cd AssftpCd_Code
        {
            get
            {
                CheckPropertyInited("AssftpCd_Code");
                return this._assftpcd_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_assftpcd))
                    {
                         _assftpcd = descr;
                    }
                }
                else
                {
                     _assftpcd = descr;
                }

                this._assftpcd_Code = value;
                NotifyPropertyChanged("AssftpCd");
                NotifyPropertyChanged("AssftpCd_Code");
            }
        }

    }
}

namespace ObjectModel.Ehd
{
    /// <summary>
    /// 402 ehd.location (EHD_LOCATION_Q)
    /// </summary>
    [RegisterInfo(RegisterID = 402)]
    [Serializable]
    public sealed partial class OMLocation : OMBaseClass<OMLocation>
    {

        private long _empid;
        /// <summary>
        /// 402000100 Идентификатор (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 402000100)]
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


        private DateTime? _loaddate;
        /// <summary>
        /// 402000200 Дата загрузки (LOAD_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 402000200)]
        public DateTime? LoadDate
        {
            get
            {
                CheckPropertyInited("LoadDate");
                return _loaddate;
            }
            set
            {
                _loaddate = value;
                NotifyPropertyChanged("LoadDate");
            }
        }


        private long? _locationehdid;
        /// <summary>
        /// 402000300 ehd.location.id (ID_LOCATION_EHD)
        /// </summary>
        [RegisterAttribute(AttributeID = 402000300)]
        public long? LocationEhdId
        {
            get
            {
                CheckPropertyInited("LocationEhdId");
                return _locationehdid;
            }
            set
            {
                _locationehdid = value;
                NotifyPropertyChanged("LocationEhdId");
            }
        }


        private long? _parcelid;
        /// <summary>
        /// 402000400 ehd.location.parcel_id (PARCEL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 402000400)]
        public long? ParcelId
        {
            get
            {
                CheckPropertyInited("ParcelId");
                return _parcelid;
            }
            set
            {
                _parcelid = value;
                NotifyPropertyChanged("ParcelId");
            }
        }


        private long? _personid;
        /// <summary>
        /// 402000500 ehd.location.person_id (PERSON_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 402000500)]
        public long? PersonId
        {
            get
            {
                CheckPropertyInited("PersonId");
                return _personid;
            }
            set
            {
                _personid = value;
                NotifyPropertyChanged("PersonId");
            }
        }


        private long? _organizationid;
        /// <summary>
        /// 402000600 ehd.location.organization_id (ORGANIZATION_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 402000600)]
        public long? OrganizationId
        {
            get
            {
                CheckPropertyInited("OrganizationId");
                return _organizationid;
            }
            set
            {
                _organizationid = value;
                NotifyPropertyChanged("OrganizationId");
            }
        }


        private long? _buildingparcelid;
        /// <summary>
        /// 402000700 ehd.location.building_parcel_id (BUILDING_PARCEL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 402000700)]
        public long? BuildingParcelId
        {
            get
            {
                CheckPropertyInited("BuildingParcelId");
                return _buildingparcelid;
            }
            set
            {
                _buildingparcelid = value;
                NotifyPropertyChanged("BuildingParcelId");
            }
        }


        private long? _globalid;
        /// <summary>
        /// 402000800 ehd.location.global_id (GLOBAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 402000800)]
        public long? GlobalId
        {
            get
            {
                CheckPropertyInited("GlobalId");
                return _globalid;
            }
            set
            {
                _globalid = value;
                NotifyPropertyChanged("GlobalId");
            }
        }


        private string _placed;
        /// <summary>
        /// 402000900 Положение на дкк (PLACED)
        /// </summary>
        [RegisterAttribute(AttributeID = 402000900)]
        public string Placed
        {
            get
            {
                CheckPropertyInited("Placed");
                return _placed;
            }
            set
            {
                _placed = value;
                NotifyPropertyChanged("Placed");
            }
        }


        private string _inbounds;
        /// <summary>
        /// 402001000 В границах (IN_BOUNDS)
        /// </summary>
        [RegisterAttribute(AttributeID = 402001000)]
        public string InBounds
        {
            get
            {
                CheckPropertyInited("InBounds");
                return _inbounds;
            }
            set
            {
                _inbounds = value;
                NotifyPropertyChanged("InBounds");
            }
        }


        private string _codeokato;
        /// <summary>
        /// 402001100 Код окато (CODE_OKATO)
        /// </summary>
        [RegisterAttribute(AttributeID = 402001100)]
        public string CodeOkato
        {
            get
            {
                CheckPropertyInited("CodeOkato");
                return _codeokato;
            }
            set
            {
                _codeokato = value;
                NotifyPropertyChanged("CodeOkato");
            }
        }


        private string _codekladr;
        /// <summary>
        /// 402001200 Код кладр (CODE_KLADR)
        /// </summary>
        [RegisterAttribute(AttributeID = 402001200)]
        public string CodeKladr
        {
            get
            {
                CheckPropertyInited("CodeKladr");
                return _codekladr;
            }
            set
            {
                _codekladr = value;
                NotifyPropertyChanged("CodeKladr");
            }
        }


        private string _postalcode;
        /// <summary>
        /// 402001300 Почтовый индекс (POSTAL_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 402001300)]
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
        /// 402001400 Код региона (REGION)
        /// </summary>
        [RegisterAttribute(AttributeID = 402001400)]
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


        private string _district;
        /// <summary>
        /// 402001500 Наименование района (DISTRICT)
        /// </summary>
        [RegisterAttribute(AttributeID = 402001500)]
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


        private string _city;
        /// <summary>
        /// 402001600 Муниципальное образование (CITY)
        /// </summary>
        [RegisterAttribute(AttributeID = 402001600)]
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


        private string _urbandistrict;
        /// <summary>
        /// 402001700 Городской район (URBAN_DISTRICT)
        /// </summary>
        [RegisterAttribute(AttributeID = 402001700)]
        public string UrbanDistrict
        {
            get
            {
                CheckPropertyInited("UrbanDistrict");
                return _urbandistrict;
            }
            set
            {
                _urbandistrict = value;
                NotifyPropertyChanged("UrbanDistrict");
            }
        }


        private string _sovietvillage;
        /// <summary>
        /// 402001800 Сельсовет (SOVIET_VILLAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 402001800)]
        public string SovietVillage
        {
            get
            {
                CheckPropertyInited("SovietVillage");
                return _sovietvillage;
            }
            set
            {
                _sovietvillage = value;
                NotifyPropertyChanged("SovietVillage");
            }
        }


        private string _locality;
        /// <summary>
        /// 402001900 Населенный пункт (LOCALITY)
        /// </summary>
        [RegisterAttribute(AttributeID = 402001900)]
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


        private string _street;
        /// <summary>
        /// 402002000 Улица (STREET)
        /// </summary>
        [RegisterAttribute(AttributeID = 402002000)]
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


        private string _level1;
        /// <summary>
        /// 402002100 Дом (LEVEL1)
        /// </summary>
        [RegisterAttribute(AttributeID = 402002100)]
        public string Level1
        {
            get
            {
                CheckPropertyInited("Level1");
                return _level1;
            }
            set
            {
                _level1 = value;
                NotifyPropertyChanged("Level1");
            }
        }


        private string _level2;
        /// <summary>
        /// 402002200 Корпус (LEVEL2)
        /// </summary>
        [RegisterAttribute(AttributeID = 402002200)]
        public string Level2
        {
            get
            {
                CheckPropertyInited("Level2");
                return _level2;
            }
            set
            {
                _level2 = value;
                NotifyPropertyChanged("Level2");
            }
        }


        private string _level3;
        /// <summary>
        /// 402002300 Строение (LEVEL3)
        /// </summary>
        [RegisterAttribute(AttributeID = 402002300)]
        public string Level3
        {
            get
            {
                CheckPropertyInited("Level3");
                return _level3;
            }
            set
            {
                _level3 = value;
                NotifyPropertyChanged("Level3");
            }
        }


        private string _apartment;
        /// <summary>
        /// 402002400 Квартира (APARTMENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 402002400)]
        public string Apartment
        {
            get
            {
                CheckPropertyInited("Apartment");
                return _apartment;
            }
            set
            {
                _apartment = value;
                NotifyPropertyChanged("Apartment");
            }
        }


        private string _fulladdress;
        /// <summary>
        /// 402002500 ehd.location.full_address (FULL_ADDRESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 402002500)]
        public string FullAddress
        {
            get
            {
                CheckPropertyInited("FullAddress");
                return _fulladdress;
            }
            set
            {
                _fulladdress = value;
                NotifyPropertyChanged("FullAddress");
            }
        }


        private string _addresstotal;
        /// <summary>
        /// 402002600 Полный адрес (ADDRESS_TOTAL)
        /// </summary>
        [RegisterAttribute(AttributeID = 402002600)]
        public string AddressTotal
        {
            get
            {
                CheckPropertyInited("AddressTotal");
                return _addresstotal;
            }
            set
            {
                _addresstotal = value;
                NotifyPropertyChanged("AddressTotal");
            }
        }


        private string _other;
        /// <summary>
        /// 402002700 ehd.location.other (OTHER)
        /// </summary>
        [RegisterAttribute(AttributeID = 402002700)]
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

    }
}

namespace ObjectModel.Fias
{
    /// <summary>
    /// 403 Справочник адресов ФИАС (FIAS_ADDROBJ)
    /// </summary>
    [RegisterInfo(RegisterID = 403)]
    [Serializable]
    public sealed partial class OMAddrObj : OMBaseClass<OMAddrObj>
    {

        private long _id;
        /// <summary>
        /// 40300100 Идентификатор (EMP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 40300100)]
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


        private long? _actstatus;
        /// <summary>
        /// 40300200 Статус последней исторической записи в жизненном цикле адресного объекта (ACTSTATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 40300200)]
        public long? ActStatus
        {
            get
            {
                CheckPropertyInited("ActStatus");
                return _actstatus;
            }
            set
            {
                _actstatus = value;
                NotifyPropertyChanged("ActStatus");
            }
        }


        private long? _actstatus_Code;
        /// <summary>
        /// 40300200 Статус последней исторической записи в жизненном цикле адресного объекта (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40300200)]
        public long? ActStatus_Code
        {
            get
            {
                CheckPropertyInited("ActStatus_Code");
                return _actstatus_Code;
            }
            set
            {
                _actstatus_Code = value;
                NotifyPropertyChanged("ActStatus_Code");
            }
        }


        private string _aoguid;
        /// <summary>
        /// 40300300 Глобальный уникальный идентификатор адресного объекта (AOGUID)
        /// </summary>
        [RegisterAttribute(AttributeID = 40300300)]
        public string AoGuid
        {
            get
            {
                CheckPropertyInited("AoGuid");
                return _aoguid;
            }
            set
            {
                _aoguid = value;
                NotifyPropertyChanged("AoGuid");
            }
        }


        private long? _aoguid_Code;
        /// <summary>
        /// 40300300 Глобальный уникальный идентификатор адресного объекта (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40300300)]
        public long? AoGuid_Code
        {
            get
            {
                CheckPropertyInited("AoGuid_Code");
                return _aoguid_Code;
            }
            set
            {
                _aoguid_Code = value;
                NotifyPropertyChanged("AoGuid_Code");
            }
        }


        private string _aoid;
        /// <summary>
        /// 40300400 Уникальный идентификатор записи. Ключевое поле. (AOID)
        /// </summary>
        [RegisterAttribute(AttributeID = 40300400)]
        public string AoId
        {
            get
            {
                CheckPropertyInited("AoId");
                return _aoid;
            }
            set
            {
                _aoid = value;
                NotifyPropertyChanged("AoId");
            }
        }


        private long? _aoid_Code;
        /// <summary>
        /// 40300400 Уникальный идентификатор записи. Ключевое поле. (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40300400)]
        public long? AoId_Code
        {
            get
            {
                CheckPropertyInited("AoId_Code");
                return _aoid_Code;
            }
            set
            {
                _aoid_Code = value;
                NotifyPropertyChanged("AoId_Code");
            }
        }


        private long? _aolevel;
        /// <summary>
        /// 40300500 Уровень адресного объекта (AOLEVEL)
        /// </summary>
        [RegisterAttribute(AttributeID = 40300500)]
        public long? AoLevel
        {
            get
            {
                CheckPropertyInited("AoLevel");
                return _aolevel;
            }
            set
            {
                _aolevel = value;
                NotifyPropertyChanged("AoLevel");
            }
        }


        private long? _aolevel_Code;
        /// <summary>
        /// 40300500 Уровень адресного объекта (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40300500)]
        public long? AoLevel_Code
        {
            get
            {
                CheckPropertyInited("AoLevel_Code");
                return _aolevel_Code;
            }
            set
            {
                _aolevel_Code = value;
                NotifyPropertyChanged("AoLevel_Code");
            }
        }


        private string _areacode;
        /// <summary>
        /// 40300600 Код района (AREACODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40300600)]
        public string AreaCode
        {
            get
            {
                CheckPropertyInited("AreaCode");
                return _areacode;
            }
            set
            {
                _areacode = value;
                NotifyPropertyChanged("AreaCode");
            }
        }


        private long? _areacode_Code;
        /// <summary>
        /// 40300600 Код района (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40300600)]
        public long? AreaCode_Code
        {
            get
            {
                CheckPropertyInited("AreaCode_Code");
                return _areacode_Code;
            }
            set
            {
                _areacode_Code = value;
                NotifyPropertyChanged("AreaCode_Code");
            }
        }


        private string _autocode;
        /// <summary>
        /// 40300700 Код автономии (AUTOCODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40300700)]
        public string AutoCode
        {
            get
            {
                CheckPropertyInited("AutoCode");
                return _autocode;
            }
            set
            {
                _autocode = value;
                NotifyPropertyChanged("AutoCode");
            }
        }


        private long? _autocode_Code;
        /// <summary>
        /// 40300700 Код автономии (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40300700)]
        public long? AutoCode_Code
        {
            get
            {
                CheckPropertyInited("AutoCode_Code");
                return _autocode_Code;
            }
            set
            {
                _autocode_Code = value;
                NotifyPropertyChanged("AutoCode_Code");
            }
        }


        private long? _centstatus;
        /// <summary>
        /// 40300800 Статус центра (CENTSTATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 40300800)]
        public long? CentStatus
        {
            get
            {
                CheckPropertyInited("CentStatus");
                return _centstatus;
            }
            set
            {
                _centstatus = value;
                NotifyPropertyChanged("CentStatus");
            }
        }


        private long? _centstatus_Code;
        /// <summary>
        /// 40300800 Статус центра (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40300800)]
        public long? CentStatus_Code
        {
            get
            {
                CheckPropertyInited("CentStatus_Code");
                return _centstatus_Code;
            }
            set
            {
                _centstatus_Code = value;
                NotifyPropertyChanged("CentStatus_Code");
            }
        }


        private string _citycode;
        /// <summary>
        /// 40300900 Код города (CITYCODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40300900)]
        public string CityCode
        {
            get
            {
                CheckPropertyInited("CityCode");
                return _citycode;
            }
            set
            {
                _citycode = value;
                NotifyPropertyChanged("CityCode");
            }
        }


        private long? _citycode_Code;
        /// <summary>
        /// 40300900 Код города (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40300900)]
        public long? CityCode_Code
        {
            get
            {
                CheckPropertyInited("CityCode_Code");
                return _citycode_Code;
            }
            set
            {
                _citycode_Code = value;
                NotifyPropertyChanged("CityCode_Code");
            }
        }


        private string _code;
        /// <summary>
        /// 40301000 Код адресного элемента одной строкой с признаком актуальности из классификационного кода (CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301000)]
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


        private long? _code_Code;
        /// <summary>
        /// 40301000 Код адресного элемента одной строкой с признаком актуальности из классификационного кода (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301000)]
        public long? Code_Code
        {
            get
            {
                CheckPropertyInited("Code_Code");
                return _code_Code;
            }
            set
            {
                _code_Code = value;
                NotifyPropertyChanged("Code_Code");
            }
        }


        private long? _currstatus;
        /// <summary>
        /// 40301100 Статус актуальности КЛАДР 4 (последние две цифры в коде) (CURRSTATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301100)]
        public long? CurrStatus
        {
            get
            {
                CheckPropertyInited("CurrStatus");
                return _currstatus;
            }
            set
            {
                _currstatus = value;
                NotifyPropertyChanged("CurrStatus");
            }
        }


        private long? _currstatus_Code;
        /// <summary>
        /// 40301100 Статус актуальности КЛАДР 4 (последние две цифры в коде) (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301100)]
        public long? CurrStatus_Code
        {
            get
            {
                CheckPropertyInited("CurrStatus_Code");
                return _currstatus_Code;
            }
            set
            {
                _currstatus_Code = value;
                NotifyPropertyChanged("CurrStatus_Code");
            }
        }


        private DateTime? _enddate;
        /// <summary>
        /// 40301200 Окончание действия записи (ENDDATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301200)]
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


        private long? _enddate_Code;
        /// <summary>
        /// 40301200 Окончание действия записи (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301200)]
        public long? EndDate_Code
        {
            get
            {
                CheckPropertyInited("EndDate_Code");
                return _enddate_Code;
            }
            set
            {
                _enddate_Code = value;
                NotifyPropertyChanged("EndDate_Code");
            }
        }


        private string _formalname;
        /// <summary>
        /// 40301300 Формализованное наименование (FORMALNAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301300)]
        public string FormalName
        {
            get
            {
                CheckPropertyInited("FormalName");
                return _formalname;
            }
            set
            {
                _formalname = value;
                NotifyPropertyChanged("FormalName");
            }
        }


        private long? _formalname_Code;
        /// <summary>
        /// 40301300 Формализованное наименование (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301300)]
        public long? FormalName_Code
        {
            get
            {
                CheckPropertyInited("FormalName_Code");
                return _formalname_Code;
            }
            set
            {
                _formalname_Code = value;
                NotifyPropertyChanged("FormalName_Code");
            }
        }


        private string _ifnsfl;
        /// <summary>
        /// 40301400 Код ИФНС ФЛ (IFNSFL)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301400)]
        public string IfnsFl
        {
            get
            {
                CheckPropertyInited("IfnsFl");
                return _ifnsfl;
            }
            set
            {
                _ifnsfl = value;
                NotifyPropertyChanged("IfnsFl");
            }
        }


        private long? _ifnsfl_Code;
        /// <summary>
        /// 40301400 Код ИФНС ФЛ (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301400)]
        public long? IfnsFl_Code
        {
            get
            {
                CheckPropertyInited("IfnsFl_Code");
                return _ifnsfl_Code;
            }
            set
            {
                _ifnsfl_Code = value;
                NotifyPropertyChanged("IfnsFl_Code");
            }
        }


        private string _ifnsul;
        /// <summary>
        /// 40301500 Код ИФНС ЮЛ (IFNSUL)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301500)]
        public string IfnsUl
        {
            get
            {
                CheckPropertyInited("IfnsUl");
                return _ifnsul;
            }
            set
            {
                _ifnsul = value;
                NotifyPropertyChanged("IfnsUl");
            }
        }


        private long? _ifnsul_Code;
        /// <summary>
        /// 40301500 Код ИФНС ЮЛ (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301500)]
        public long? IfnsUl_Code
        {
            get
            {
                CheckPropertyInited("IfnsUl_Code");
                return _ifnsul_Code;
            }
            set
            {
                _ifnsul_Code = value;
                NotifyPropertyChanged("IfnsUl_Code");
            }
        }


        private string _nextid;
        /// <summary>
        /// 40301600 Идентификатор записи  связывания с последующей исторической записью (NEXTID)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301600)]
        public string NextId
        {
            get
            {
                CheckPropertyInited("NextId");
                return _nextid;
            }
            set
            {
                _nextid = value;
                NotifyPropertyChanged("NextId");
            }
        }


        private long? _nextid_Code;
        /// <summary>
        /// 40301600 Идентификатор записи  связывания с последующей исторической записью (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301600)]
        public long? NextId_Code
        {
            get
            {
                CheckPropertyInited("NextId_Code");
                return _nextid_Code;
            }
            set
            {
                _nextid_Code = value;
                NotifyPropertyChanged("NextId_Code");
            }
        }


        private string _offname;
        /// <summary>
        /// 40301700 Официальное наименование (OFFNAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301700)]
        public string OffName
        {
            get
            {
                CheckPropertyInited("OffName");
                return _offname;
            }
            set
            {
                _offname = value;
                NotifyPropertyChanged("OffName");
            }
        }


        private long? _offname_Code;
        /// <summary>
        /// 40301700 Официальное наименование (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301700)]
        public long? OffName_Code
        {
            get
            {
                CheckPropertyInited("OffName_Code");
                return _offname_Code;
            }
            set
            {
                _offname_Code = value;
                NotifyPropertyChanged("OffName_Code");
            }
        }


        private string _okato;
        /// <summary>
        /// 40301800 ОКАТО (OKATO)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301800)]
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
        /// 40301800 ОКАТО (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301800)]
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


        private string _oktmo;
        /// <summary>
        /// 40301900 ОКТМО (OKTMO)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301900)]
        public string Oktmo
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


        private long? _oktmo_Code;
        /// <summary>
        /// 40301900 ОКТМО (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40301900)]
        public long? Oktmo_Code
        {
            get
            {
                CheckPropertyInited("Oktmo_Code");
                return _oktmo_Code;
            }
            set
            {
                _oktmo_Code = value;
                NotifyPropertyChanged("Oktmo_Code");
            }
        }


        private long? _operstatus;
        /// <summary>
        /// 40302000 Статус действия над записью – причина появления записи (OPERSTATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302000)]
        public long? OperStatus
        {
            get
            {
                CheckPropertyInited("OperStatus");
                return _operstatus;
            }
            set
            {
                _operstatus = value;
                NotifyPropertyChanged("OperStatus");
            }
        }


        private long? _operstatus_Code;
        /// <summary>
        /// 40302000 Статус действия над записью – причина появления записи (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302000)]
        public long? OperStatus_Code
        {
            get
            {
                CheckPropertyInited("OperStatus_Code");
                return _operstatus_Code;
            }
            set
            {
                _operstatus_Code = value;
                NotifyPropertyChanged("OperStatus_Code");
            }
        }


        private string _parentguid;
        /// <summary>
        /// 40302100 Идентификатор объекта родительского объекта (PARENTGUID)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302100)]
        public string ParentGuid
        {
            get
            {
                CheckPropertyInited("ParentGuid");
                return _parentguid;
            }
            set
            {
                _parentguid = value;
                NotifyPropertyChanged("ParentGuid");
            }
        }


        private long? _parentguid_Code;
        /// <summary>
        /// 40302100 Идентификатор объекта родительского объекта (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302100)]
        public long? ParentGuid_Code
        {
            get
            {
                CheckPropertyInited("ParentGuid_Code");
                return _parentguid_Code;
            }
            set
            {
                _parentguid_Code = value;
                NotifyPropertyChanged("ParentGuid_Code");
            }
        }


        private string _placecode;
        /// <summary>
        /// 40302200 Код населенного пункта (PLACECODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302200)]
        public string PlaceCode
        {
            get
            {
                CheckPropertyInited("PlaceCode");
                return _placecode;
            }
            set
            {
                _placecode = value;
                NotifyPropertyChanged("PlaceCode");
            }
        }


        private long? _placecode_Code;
        /// <summary>
        /// 40302200 Код населенного пункта (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302200)]
        public long? PlaceCode_Code
        {
            get
            {
                CheckPropertyInited("PlaceCode_Code");
                return _placecode_Code;
            }
            set
            {
                _placecode_Code = value;
                NotifyPropertyChanged("PlaceCode_Code");
            }
        }


        private string _plaincode;
        /// <summary>
        /// 40302300 Код адресного элемента одной строкой без признака актуальности (последних двух цифр) (PLAINCODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302300)]
        public string PlainCode
        {
            get
            {
                CheckPropertyInited("PlainCode");
                return _plaincode;
            }
            set
            {
                _plaincode = value;
                NotifyPropertyChanged("PlainCode");
            }
        }


        private long? _plaincode_Code;
        /// <summary>
        /// 40302300 Код адресного элемента одной строкой без признака актуальности (последних двух цифр) (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302300)]
        public long? PlainCode_Code
        {
            get
            {
                CheckPropertyInited("PlainCode_Code");
                return _plaincode_Code;
            }
            set
            {
                _plaincode_Code = value;
                NotifyPropertyChanged("PlainCode_Code");
            }
        }


        private string _postalcode;
        /// <summary>
        /// 40302400 Почтовый индекс (POSTALCODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302400)]
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


        private long? _postalcode_Code;
        /// <summary>
        /// 40302400 Почтовый индекс (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302400)]
        public long? PostalCode_Code
        {
            get
            {
                CheckPropertyInited("PostalCode_Code");
                return _postalcode_Code;
            }
            set
            {
                _postalcode_Code = value;
                NotifyPropertyChanged("PostalCode_Code");
            }
        }


        private string _previd;
        /// <summary>
        /// 40302500 Идентификатор записи связывания с предыдушей исторической записью (PREVID)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302500)]
        public string PrevId
        {
            get
            {
                CheckPropertyInited("PrevId");
                return _previd;
            }
            set
            {
                _previd = value;
                NotifyPropertyChanged("PrevId");
            }
        }


        private long? _previd_Code;
        /// <summary>
        /// 40302500 Идентификатор записи связывания с предыдушей исторической записью (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302500)]
        public long? PrevId_Code
        {
            get
            {
                CheckPropertyInited("PrevId_Code");
                return _previd_Code;
            }
            set
            {
                _previd_Code = value;
                NotifyPropertyChanged("PrevId_Code");
            }
        }


        private string _regioncode;
        /// <summary>
        /// 40302600 Код региона (REGIONCODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302600)]
        public string RegionCode
        {
            get
            {
                CheckPropertyInited("RegionCode");
                return _regioncode;
            }
            set
            {
                _regioncode = value;
                NotifyPropertyChanged("RegionCode");
            }
        }


        private long? _regioncode_Code;
        /// <summary>
        /// 40302600 Код региона (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302600)]
        public long? RegionCode_Code
        {
            get
            {
                CheckPropertyInited("RegionCode_Code");
                return _regioncode_Code;
            }
            set
            {
                _regioncode_Code = value;
                NotifyPropertyChanged("RegionCode_Code");
            }
        }


        private string _shortname;
        /// <summary>
        /// 40302700 Краткое наименование типа объекта (SHORTNAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302700)]
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


        private long? _shortname_Code;
        /// <summary>
        /// 40302700 Краткое наименование типа объекта (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302700)]
        public long? ShortName_Code
        {
            get
            {
                CheckPropertyInited("ShortName_Code");
                return _shortname_Code;
            }
            set
            {
                _shortname_Code = value;
                NotifyPropertyChanged("ShortName_Code");
            }
        }


        private DateTime? _startdate;
        /// <summary>
        /// 40302800 Начало действия записи (STARTDATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302800)]
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


        private long? _startdate_Code;
        /// <summary>
        /// 40302800 Начало действия записи (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302800)]
        public long? StartDate_Code
        {
            get
            {
                CheckPropertyInited("StartDate_Code");
                return _startdate_Code;
            }
            set
            {
                _startdate_Code = value;
                NotifyPropertyChanged("StartDate_Code");
            }
        }


        private string _streetcode;
        /// <summary>
        /// 40302900 Код улицы (STREETCODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302900)]
        public string StreetCode
        {
            get
            {
                CheckPropertyInited("StreetCode");
                return _streetcode;
            }
            set
            {
                _streetcode = value;
                NotifyPropertyChanged("StreetCode");
            }
        }


        private long? _streetcode_Code;
        /// <summary>
        /// 40302900 Код улицы (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40302900)]
        public long? StreetCode_Code
        {
            get
            {
                CheckPropertyInited("StreetCode_Code");
                return _streetcode_Code;
            }
            set
            {
                _streetcode_Code = value;
                NotifyPropertyChanged("StreetCode_Code");
            }
        }


        private string _terrifnsfl;
        /// <summary>
        /// 40303000 Код территориального участка ИФНС ФЛ (TERRIFNSFL)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303000)]
        public string TerrIfnsFl
        {
            get
            {
                CheckPropertyInited("TerrIfnsFl");
                return _terrifnsfl;
            }
            set
            {
                _terrifnsfl = value;
                NotifyPropertyChanged("TerrIfnsFl");
            }
        }


        private long? _terrifnsfl_Code;
        /// <summary>
        /// 40303000 Код территориального участка ИФНС ФЛ (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303000)]
        public long? TerrIfnsFl_Code
        {
            get
            {
                CheckPropertyInited("TerrIfnsFl_Code");
                return _terrifnsfl_Code;
            }
            set
            {
                _terrifnsfl_Code = value;
                NotifyPropertyChanged("TerrIfnsFl_Code");
            }
        }


        private string _terrifnsul;
        /// <summary>
        /// 40303100 Код территориального участка ИФНС ЮЛ (TERRIFNSUL)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303100)]
        public string TerrIfnsUl
        {
            get
            {
                CheckPropertyInited("TerrIfnsUl");
                return _terrifnsul;
            }
            set
            {
                _terrifnsul = value;
                NotifyPropertyChanged("TerrIfnsUl");
            }
        }


        private long? _terrifnsul_Code;
        /// <summary>
        /// 40303100 Код территориального участка ИФНС ЮЛ (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303100)]
        public long? TerrIfnsUl_Code
        {
            get
            {
                CheckPropertyInited("TerrIfnsUl_Code");
                return _terrifnsul_Code;
            }
            set
            {
                _terrifnsul_Code = value;
                NotifyPropertyChanged("TerrIfnsUl_Code");
            }
        }


        private DateTime? _updatedate;
        /// <summary>
        /// 40303200 Дата  внесения (обновления) записи (UPDATEDATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303200)]
        public DateTime? UpdateDate
        {
            get
            {
                CheckPropertyInited("UpdateDate");
                return _updatedate;
            }
            set
            {
                _updatedate = value;
                NotifyPropertyChanged("UpdateDate");
            }
        }


        private long? _updatedate_Code;
        /// <summary>
        /// 40303200 Дата  внесения (обновления) записи (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303200)]
        public long? UpdateDate_Code
        {
            get
            {
                CheckPropertyInited("UpdateDate_Code");
                return _updatedate_Code;
            }
            set
            {
                _updatedate_Code = value;
                NotifyPropertyChanged("UpdateDate_Code");
            }
        }


        private string _ctarcode;
        /// <summary>
        /// 40303300 Код внутригородского района (CTARCODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303300)]
        public string CtarCode
        {
            get
            {
                CheckPropertyInited("CtarCode");
                return _ctarcode;
            }
            set
            {
                _ctarcode = value;
                NotifyPropertyChanged("CtarCode");
            }
        }


        private long? _ctarcode_Code;
        /// <summary>
        /// 40303300 Код внутригородского района (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303300)]
        public long? CtarCode_Code
        {
            get
            {
                CheckPropertyInited("CtarCode_Code");
                return _ctarcode_Code;
            }
            set
            {
                _ctarcode_Code = value;
                NotifyPropertyChanged("CtarCode_Code");
            }
        }


        private string _extrcode;
        /// <summary>
        /// 40303400 Код дополнительного адресообразующего элемента (EXTRCODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303400)]
        public string ExtrCode
        {
            get
            {
                CheckPropertyInited("ExtrCode");
                return _extrcode;
            }
            set
            {
                _extrcode = value;
                NotifyPropertyChanged("ExtrCode");
            }
        }


        private long? _extrcode_Code;
        /// <summary>
        /// 40303400 Код дополнительного адресообразующего элемента (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303400)]
        public long? ExtrCode_Code
        {
            get
            {
                CheckPropertyInited("ExtrCode_Code");
                return _extrcode_Code;
            }
            set
            {
                _extrcode_Code = value;
                NotifyPropertyChanged("ExtrCode_Code");
            }
        }


        private string _sextcode;
        /// <summary>
        /// 40303500 Код подчиненного дополнительного адресообразующего элемента (SEXTCODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303500)]
        public string SextCode
        {
            get
            {
                CheckPropertyInited("SextCode");
                return _sextcode;
            }
            set
            {
                _sextcode = value;
                NotifyPropertyChanged("SextCode");
            }
        }


        private long? _sextcode_Code;
        /// <summary>
        /// 40303500 Код подчиненного дополнительного адресообразующего элемента (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303500)]
        public long? SextCode_Code
        {
            get
            {
                CheckPropertyInited("SextCode_Code");
                return _sextcode_Code;
            }
            set
            {
                _sextcode_Code = value;
                NotifyPropertyChanged("SextCode_Code");
            }
        }


        private long? _livestatus;
        /// <summary>
        /// 40303600 Статус актуальности адресного объекта ФИАС на текущую дату (LIVESTATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303600)]
        public long? LiveStatus
        {
            get
            {
                CheckPropertyInited("LiveStatus");
                return _livestatus;
            }
            set
            {
                _livestatus = value;
                NotifyPropertyChanged("LiveStatus");
            }
        }


        private long? _livestatus_Code;
        /// <summary>
        /// 40303600 Статус актуальности адресного объекта ФИАС на текущую дату (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303600)]
        public long? LiveStatus_Code
        {
            get
            {
                CheckPropertyInited("LiveStatus_Code");
                return _livestatus_Code;
            }
            set
            {
                _livestatus_Code = value;
                NotifyPropertyChanged("LiveStatus_Code");
            }
        }


        private string _normdoc;
        /// <summary>
        /// 40303700 Внешний ключ на нормативный документ (NORMDOC)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303700)]
        public string NormDoc
        {
            get
            {
                CheckPropertyInited("NormDoc");
                return _normdoc;
            }
            set
            {
                _normdoc = value;
                NotifyPropertyChanged("NormDoc");
            }
        }


        private long? _normdoc_Code;
        /// <summary>
        /// 40303700 Внешний ключ на нормативный документ (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303700)]
        public long? NormDoc_Code
        {
            get
            {
                CheckPropertyInited("NormDoc_Code");
                return _normdoc_Code;
            }
            set
            {
                _normdoc_Code = value;
                NotifyPropertyChanged("NormDoc_Code");
            }
        }


        private string _plancode;
        /// <summary>
        /// 40303800 Код элемента планировочной структуры (PLANCODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303800)]
        public string PlanCode
        {
            get
            {
                CheckPropertyInited("PlanCode");
                return _plancode;
            }
            set
            {
                _plancode = value;
                NotifyPropertyChanged("PlanCode");
            }
        }


        private long? _plancode_Code;
        /// <summary>
        /// 40303800 Код элемента планировочной структуры (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303800)]
        public long? PlanCode_Code
        {
            get
            {
                CheckPropertyInited("PlanCode_Code");
                return _plancode_Code;
            }
            set
            {
                _plancode_Code = value;
                NotifyPropertyChanged("PlanCode_Code");
            }
        }


        private string _cadnum;
        /// <summary>
        /// 40303900 Кадастровый номер (CADNUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303900)]
        public string CadNum
        {
            get
            {
                CheckPropertyInited("CadNum");
                return _cadnum;
            }
            set
            {
                _cadnum = value;
                NotifyPropertyChanged("CadNum");
            }
        }


        private long? _cadnum_Code;
        /// <summary>
        /// 40303900 Кадастровый номер (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40303900)]
        public long? CadNum_Code
        {
            get
            {
                CheckPropertyInited("CadNum_Code");
                return _cadnum_Code;
            }
            set
            {
                _cadnum_Code = value;
                NotifyPropertyChanged("CadNum_Code");
            }
        }


        private long? _divtype;
        /// <summary>
        /// 40304000 Тип деления (DIVTYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40304000)]
        public long? DivType
        {
            get
            {
                CheckPropertyInited("DivType");
                return _divtype;
            }
            set
            {
                _divtype = value;
                NotifyPropertyChanged("DivType");
            }
        }


        private long? _divtype_Code;
        /// <summary>
        /// 40304000 Тип деления (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40304000)]
        public long? DivType_Code
        {
            get
            {
                CheckPropertyInited("DivType_Code");
                return _divtype_Code;
            }
            set
            {
                _divtype_Code = value;
                NotifyPropertyChanged("DivType_Code");
            }
        }

    }
}

namespace ObjectModel.Fias
{
    /// <summary>
    /// 404 Справочник адресов ФИАС (дома) (FIAS_HOUSE)
    /// </summary>
    [RegisterInfo(RegisterID = 404)]
    [Serializable]
    public sealed partial class OMHouse : OMBaseClass<OMHouse>
    {

        private string _aoguid;
        /// <summary>
        /// 40400100 Идентификатор ФИАС (AOGUID)
        /// </summary>
        [RegisterAttribute(AttributeID = 40400100)]
        public string AoGuid
        {
            get
            {
                CheckPropertyInited("AoGuid");
                return _aoguid;
            }
            set
            {
                _aoguid = value;
                NotifyPropertyChanged("AoGuid");
            }
        }


        private long? _aoguid_Code;
        /// <summary>
        /// 40400100 Идентификатор ФИАС (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40400100)]
        public long? AoGuid_Code
        {
            get
            {
                CheckPropertyInited("AoGuid_Code");
                return _aoguid_Code;
            }
            set
            {
                _aoguid_Code = value;
                NotifyPropertyChanged("AoGuid_Code");
            }
        }


        private string _houseguid;
        /// <summary>
        /// 40400200 Идентификатор дома (GUID) (HOUSEGUID)
        /// </summary>
        [RegisterAttribute(AttributeID = 40400200)]
        public string HouseGuid
        {
            get
            {
                CheckPropertyInited("HouseGuid");
                return _houseguid;
            }
            set
            {
                _houseguid = value;
                NotifyPropertyChanged("HouseGuid");
            }
        }


        private long? _houseguid_Code;
        /// <summary>
        /// 40400200 Идентификатор дома (GUID) (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40400200)]
        public long? HouseGuid_Code
        {
            get
            {
                CheckPropertyInited("HouseGuid_Code");
                return _houseguid_Code;
            }
            set
            {
                _houseguid_Code = value;
                NotifyPropertyChanged("HouseGuid_Code");
            }
        }


        private string _houseid;
        /// <summary>
        /// 40400300 Идентификатор дома (ID) (HOUSEID)
        /// </summary>
        [RegisterAttribute(AttributeID = 40400300)]
        public string HouseId
        {
            get
            {
                CheckPropertyInited("HouseId");
                return _houseid;
            }
            set
            {
                _houseid = value;
                NotifyPropertyChanged("HouseId");
            }
        }


        private long? _houseid_Code;
        /// <summary>
        /// 40400300 Идентификатор дома (ID) (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40400300)]
        public long? HouseId_Code
        {
            get
            {
                CheckPropertyInited("HouseId_Code");
                return _houseid_Code;
            }
            set
            {
                _houseid_Code = value;
                NotifyPropertyChanged("HouseId_Code");
            }
        }


        private long _id;
        /// <summary>
        /// 40400400 Идентификатор (COUNTER)
        /// </summary>
        [RegisterAttribute(AttributeID = 40400400)]
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


        private long? _id_Code;
        /// <summary>
        /// 40400400 Идентификатор (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40400400)]
        public long? Id_Code
        {
            get
            {
                CheckPropertyInited("Id_Code");
                return _id_Code;
            }
            set
            {
                _id_Code = value;
                NotifyPropertyChanged("Id_Code");
            }
        }


        private long? _housenum;
        /// <summary>
        /// 40400500 Номер дома (HOUSENUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 40400500)]
        public long? HouseNum
        {
            get
            {
                CheckPropertyInited("HouseNum");
                return _housenum;
            }
            set
            {
                _housenum = value;
                NotifyPropertyChanged("HouseNum");
            }
        }


        private long? _housenum_Code;
        /// <summary>
        /// 40400500 Номер дома (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40400500)]
        public long? HouseNum_Code
        {
            get
            {
                CheckPropertyInited("HouseNum_Code");
                return _housenum_Code;
            }
            set
            {
                _housenum_Code = value;
                NotifyPropertyChanged("HouseNum_Code");
            }
        }


        private long? _buildnum;
        /// <summary>
        /// 40400600 Номер корпуса (BUILDNUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 40400600)]
        public long? BuildNum
        {
            get
            {
                CheckPropertyInited("BuildNum");
                return _buildnum;
            }
            set
            {
                _buildnum = value;
                NotifyPropertyChanged("BuildNum");
            }
        }


        private long? _buildnum_Code;
        /// <summary>
        /// 40400600 Номер корпуса (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40400600)]
        public long? BuildNum_Code
        {
            get
            {
                CheckPropertyInited("BuildNum_Code");
                return _buildnum_Code;
            }
            set
            {
                _buildnum_Code = value;
                NotifyPropertyChanged("BuildNum_Code");
            }
        }


        private long? _strucnum;
        /// <summary>
        /// 40400700 Номер строения (STRUCNUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 40400700)]
        public long? StrucNum
        {
            get
            {
                CheckPropertyInited("StrucNum");
                return _strucnum;
            }
            set
            {
                _strucnum = value;
                NotifyPropertyChanged("StrucNum");
            }
        }


        private long? _strucnum_Code;
        /// <summary>
        /// 40400700 Номер строения (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40400700)]
        public long? StrucNum_Code
        {
            get
            {
                CheckPropertyInited("StrucNum_Code");
                return _strucnum_Code;
            }
            set
            {
                _strucnum_Code = value;
                NotifyPropertyChanged("StrucNum_Code");
            }
        }


        private long? _estatus;
        /// <summary>
        /// 40400800 Тип номера дома (ESTSTATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 40400800)]
        public long? Estatus
        {
            get
            {
                CheckPropertyInited("Estatus");
                return _estatus;
            }
            set
            {
                _estatus = value;
                NotifyPropertyChanged("Estatus");
            }
        }


        private long? _estatus_Code;
        /// <summary>
        /// 40400800 Тип номера дома (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40400800)]
        public long? Estatus_Code
        {
            get
            {
                CheckPropertyInited("Estatus_Code");
                return _estatus_Code;
            }
            set
            {
                _estatus_Code = value;
                NotifyPropertyChanged("Estatus_Code");
            }
        }


        private long? _strstatus;
        /// <summary>
        /// 40400900 Тип номера строения (STRSTATUS)
        /// </summary>
        [RegisterAttribute(AttributeID = 40400900)]
        public long? StrStatus
        {
            get
            {
                CheckPropertyInited("StrStatus");
                return _strstatus;
            }
            set
            {
                _strstatus = value;
                NotifyPropertyChanged("StrStatus");
            }
        }


        private long? _strstatus_Code;
        /// <summary>
        /// 40400900 Тип номера строения (справочный код) (Null)
        /// </summary>
        [RegisterAttribute(AttributeID = 40400900)]
        public long? StrStatus_Code
        {
            get
            {
                CheckPropertyInited("StrStatus_Code");
                return _strstatus_Code;
            }
            set
            {
                _strstatus_Code = value;
                NotifyPropertyChanged("StrStatus_Code");
            }
        }

    }
}

namespace ObjectModel.Ehd
{
    /// <summary>
    /// 405 EHD.EGRP (EHD_EGRP_Q)
    /// </summary>
    [RegisterInfo(RegisterID = 405)]
    [Serializable]
    public sealed partial class OMEgrp : OMBaseClass<OMEgrp>
    {

        private long _empid;
        /// <summary>
        /// 405000100 Идентификатор (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 405000100)]
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


        private long? _ehdegrnid;
        /// <summary>
        /// 405000200 id- записи в EHD.EGRP (связка с RIGHT_FROM_EHD) (ID_IN_EHD_EGRP)
        /// </summary>
        [RegisterAttribute(AttributeID = 405000200)]
        public long? EhdEgrnId
        {
            get
            {
                CheckPropertyInited("EhdEgrnId");
                return _ehdegrnid;
            }
            set
            {
                _ehdegrnid = value;
                NotifyPropertyChanged("EhdEgrnId");
            }
        }


        private decimal? _area;
        /// <summary>
        /// 405000300 Площадь  объекта ЕГРП (AREA)
        /// </summary>
        [RegisterAttribute(AttributeID = 405000300)]
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


        private long? _globalid;
        /// <summary>
        /// 405000400 Одноименное поле в EHD.EGRP (GLOBAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 405000400)]
        public long? GlobalId
        {
            get
            {
                CheckPropertyInited("GlobalId");
                return _globalid;
            }
            set
            {
                _globalid = value;
                NotifyPropertyChanged("GlobalId");
            }
        }


        private string _objtcd;
        /// <summary>
        /// 405000500 вид объекта недвижимости (OBJT_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405000500)]
        public string ObjtCd
        {
            get
            {
                CheckPropertyInited("ObjtCd");
                return _objtcd;
            }
            set
            {
                _objtcd = value;
                NotifyPropertyChanged("ObjtCd");
            }
        }


        private string _objecttpcd;
        /// <summary>
        /// 405000600 тип объекта недвижимости (OBJECTTP_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405000600)]
        public string ObjecttpCd
        {
            get
            {
                CheckPropertyInited("ObjecttpCd");
                return _objecttpcd;
            }
            set
            {
                _objecttpcd = value;
                NotifyPropertyChanged("ObjecttpCd");
            }
        }


        private string _regtpcd;
        /// <summary>
        /// 405000700 тип региона (REGTP_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405000700)]
        public string RegtpCd
        {
            get
            {
                CheckPropertyInited("RegtpCd");
                return _regtpcd;
            }
            set
            {
                _regtpcd = value;
                NotifyPropertyChanged("RegtpCd");
            }
        }


        private string _disttpcd;
        /// <summary>
        /// 405000800 тип района (DISTTP_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405000800)]
        public string DisttpCd
        {
            get
            {
                CheckPropertyInited("DisttpCd");
                return _disttpcd;
            }
            set
            {
                _disttpcd = value;
                NotifyPropertyChanged("DisttpCd");
            }
        }


        private string _citytpcd;
        /// <summary>
        /// 405000900 тип города (CITYTP_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405000900)]
        public string CitytpCd
        {
            get
            {
                CheckPropertyInited("CitytpCd");
                return _citytpcd;
            }
            set
            {
                _citytpcd = value;
                NotifyPropertyChanged("CitytpCd");
            }
        }


        private string _loctpcd;
        /// <summary>
        /// 405001000 тип населённых пунктов (LOCTP_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405001000)]
        public string LoctpCd
        {
            get
            {
                CheckPropertyInited("LoctpCd");
                return _loctpcd;
            }
            set
            {
                _loctpcd = value;
                NotifyPropertyChanged("LoctpCd");
            }
        }


        private string _strtpcd;
        /// <summary>
        /// 405001100 тип улицы (STRTP_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405001100)]
        public string StrtpCd
        {
            get
            {
                CheckPropertyInited("StrtpCd");
                return _strtpcd;
            }
            set
            {
                _strtpcd = value;
                NotifyPropertyChanged("StrtpCd");
            }
        }


        private string _level1tpcd;
        /// <summary>
        /// 405001200 тип дома (LEVEL1TP_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405001200)]
        public string Level1tpCd
        {
            get
            {
                CheckPropertyInited("Level1tpCd");
                return _level1tpcd;
            }
            set
            {
                _level1tpcd = value;
                NotifyPropertyChanged("Level1tpCd");
            }
        }


        private string _level2tpcd;
        /// <summary>
        /// 405001300 тип корпуса (LEVEL2TP_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405001300)]
        public string Level2tpCd
        {
            get
            {
                CheckPropertyInited("Level2tpCd");
                return _level2tpcd;
            }
            set
            {
                _level2tpcd = value;
                NotifyPropertyChanged("Level2tpCd");
            }
        }


        private string _level3tpcd;
        /// <summary>
        /// 405001400 тип строения (LEVEL3TP_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405001400)]
        public string Level3tpCd
        {
            get
            {
                CheckPropertyInited("Level3tpCd");
                return _level3tpcd;
            }
            set
            {
                _level3tpcd = value;
                NotifyPropertyChanged("Level3tpCd");
            }
        }


        private string _aparttpcd;
        /// <summary>
        /// 405001500 тип квартиры (APARTTP_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405001500)]
        public string AparttpCd
        {
            get
            {
                CheckPropertyInited("AparttpCd");
                return _aparttpcd;
            }
            set
            {
                _aparttpcd = value;
                NotifyPropertyChanged("AparttpCd");
            }
        }


        private string _purposetpcd;
        /// <summary>
        /// 405001600 код назначения объекта недвижимости (PURPOSETP_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405001600)]
        public string PurposetpCd
        {
            get
            {
                CheckPropertyInited("PurposetpCd");
                return _purposetpcd;
            }
            set
            {
                _purposetpcd = value;
                NotifyPropertyChanged("PurposetpCd");
            }
        }


        private string _objectstcd;
        /// <summary>
        /// 405001700 статус состояния записи объекта (OBJECTST_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405001700)]
        public string ObjectstCd
        {
            get
            {
                CheckPropertyInited("ObjectstCd");
                return _objectstcd;
            }
            set
            {
                _objectstcd = value;
                NotifyPropertyChanged("ObjectstCd");
            }
        }


        private string _actstcd;
        /// <summary>
        /// 405001800 статус актуальности информации об объекте (ACTST_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405001800)]
        public string ActstCd
        {
            get
            {
                CheckPropertyInited("ActstCd");
                return _actstcd;
            }
            set
            {
                _actstcd = value;
                NotifyPropertyChanged("ActstCd");
            }
        }


        private string _faktcd;
        /// <summary>
        /// 405001900 код вида использования земель (фактический) (FAKT_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405001900)]
        public string FaktCd
        {
            get
            {
                CheckPropertyInited("FaktCd");
                return _faktcd;
            }
            set
            {
                _faktcd = value;
                NotifyPropertyChanged("FaktCd");
            }
        }


        private string _bydoccd;
        /// <summary>
        /// 405002000 код вида использования земель (по документам) (BYDOC_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405002000)]
        public string BydocCd
        {
            get
            {
                CheckPropertyInited("BydocCd");
                return _bydoccd;
            }
            set
            {
                _bydoccd = value;
                NotifyPropertyChanged("BydocCd");
            }
        }


        private string _groundcatcd;
        /// <summary>
        /// 405002100 целевое назначение земли (категория) (GROUNDCAT_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405002100)]
        public string GroundcatCd
        {
            get
            {
                CheckPropertyInited("GroundcatCd");
                return _groundcatcd;
            }
            set
            {
                _groundcatcd = value;
                NotifyPropertyChanged("GroundcatCd");
            }
        }


        private string _purpose;
        /// <summary>
        /// 405002200 назначение объекта недвижимости (PURPOSE)
        /// </summary>
        [RegisterAttribute(AttributeID = 405002200)]
        public string Purpose
        {
            get
            {
                CheckPropertyInited("Purpose");
                return _purpose;
            }
            set
            {
                _purpose = value;
                NotifyPropertyChanged("Purpose");
            }
        }


        private string _invnum;
        /// <summary>
        /// 405002300 инвентарный номер (INVNUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 405002300)]
        public string Invnum
        {
            get
            {
                CheckPropertyInited("Invnum");
                return _invnum;
            }
            set
            {
                _invnum = value;
                NotifyPropertyChanged("Invnum");
            }
        }


        private string _literbti;
        /// <summary>
        /// 405002400 литер бти (LITERBTI)
        /// </summary>
        [RegisterAttribute(AttributeID = 405002400)]
        public string Literbti
        {
            get
            {
                CheckPropertyInited("Literbti");
                return _literbti;
            }
            set
            {
                _literbti = value;
                NotifyPropertyChanged("Literbti");
            }
        }


        private string _addrrefmark;
        /// <summary>
        /// 405002500 уточнение местоположения (ориентира) (ADDR_REFMARK)
        /// </summary>
        [RegisterAttribute(AttributeID = 405002500)]
        public string AddrRefmark
        {
            get
            {
                CheckPropertyInited("AddrRefmark");
                return _addrrefmark;
            }
            set
            {
                _addrrefmark = value;
                NotifyPropertyChanged("AddrRefmark");
            }
        }


        private string _addrid;
        /// <summary>
        /// 405002600 уникальный идентификатор адреса (ADDR_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 405002600)]
        public string AddrId
        {
            get
            {
                CheckPropertyInited("AddrId");
                return _addrid;
            }
            set
            {
                _addrid = value;
                NotifyPropertyChanged("AddrId");
            }
        }


        private string _addrcdcountry;
        /// <summary>
        /// 405002700 код страны по классификатору оксм (ADDR_CDCOUNTRY)
        /// </summary>
        [RegisterAttribute(AttributeID = 405002700)]
        public string AddrCdcountry
        {
            get
            {
                CheckPropertyInited("AddrCdcountry");
                return _addrcdcountry;
            }
            set
            {
                _addrcdcountry = value;
                NotifyPropertyChanged("AddrCdcountry");
            }
        }


        private string _addrcdokato;
        /// <summary>
        /// 405002800 окато (ADDR_CDOKATO)
        /// </summary>
        [RegisterAttribute(AttributeID = 405002800)]
        public string AddrCdokato
        {
            get
            {
                CheckPropertyInited("AddrCdokato");
                return _addrcdokato;
            }
            set
            {
                _addrcdokato = value;
                NotifyPropertyChanged("AddrCdokato");
            }
        }


        private string _addrpostcd;
        /// <summary>
        /// 405002900 почтовый индекс (ADDR_POSTCD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405002900)]
        public string AddrPostcd
        {
            get
            {
                CheckPropertyInited("AddrPostcd");
                return _addrpostcd;
            }
            set
            {
                _addrpostcd = value;
                NotifyPropertyChanged("AddrPostcd");
            }
        }


        private string _addrdistname;
        /// <summary>
        /// 405003000 наименование района (ADDR_DIST_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 405003000)]
        public string AddrDistName
        {
            get
            {
                CheckPropertyInited("AddrDistName");
                return _addrdistname;
            }
            set
            {
                _addrdistname = value;
                NotifyPropertyChanged("AddrDistName");
            }
        }


        private string _addrdistcd;
        /// <summary>
        /// 405003100 район. код кладр адресного объекта (ADDR_DIST_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405003100)]
        public string AddrDistCd
        {
            get
            {
                CheckPropertyInited("AddrDistCd");
                return _addrdistcd;
            }
            set
            {
                _addrdistcd = value;
                NotifyPropertyChanged("AddrDistCd");
            }
        }


        private string _addrcityname;
        /// <summary>
        /// 405003200 наименование города (ADDR_CITY_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 405003200)]
        public string AddrCityName
        {
            get
            {
                CheckPropertyInited("AddrCityName");
                return _addrcityname;
            }
            set
            {
                _addrcityname = value;
                NotifyPropertyChanged("AddrCityName");
            }
        }


        private string _addrcitycd;
        /// <summary>
        /// 405003300 город. код кладр адресного объекта (ADDR_CITY_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405003300)]
        public string AddrCityCd
        {
            get
            {
                CheckPropertyInited("AddrCityCd");
                return _addrcitycd;
            }
            set
            {
                _addrcitycd = value;
                NotifyPropertyChanged("AddrCityCd");
            }
        }


        private string _addrlocname;
        /// <summary>
        /// 405003400 наименование населенного пункта (ADDR_LOC_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 405003400)]
        public string AddrLocName
        {
            get
            {
                CheckPropertyInited("AddrLocName");
                return _addrlocname;
            }
            set
            {
                _addrlocname = value;
                NotifyPropertyChanged("AddrLocName");
            }
        }


        private string _addrloccd;
        /// <summary>
        /// 405003500 населенный пункт. код кладр адресного объекта (ADDR_LOC_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405003500)]
        public string AddrLocCd
        {
            get
            {
                CheckPropertyInited("AddrLocCd");
                return _addrloccd;
            }
            set
            {
                _addrloccd = value;
                NotifyPropertyChanged("AddrLocCd");
            }
        }


        private string _addrstrname;
        /// <summary>
        /// 405003600 наименование улицы (ADDR_STR_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 405003600)]
        public string AddrStrName
        {
            get
            {
                CheckPropertyInited("AddrStrName");
                return _addrstrname;
            }
            set
            {
                _addrstrname = value;
                NotifyPropertyChanged("AddrStrName");
            }
        }


        private string _addrstrcd;
        /// <summary>
        /// 405003700 улица. код кладр адресного объекта (ADDR_STR_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 405003700)]
        public string AddrStrCd
        {
            get
            {
                CheckPropertyInited("AddrStrCd");
                return _addrstrcd;
            }
            set
            {
                _addrstrcd = value;
                NotifyPropertyChanged("AddrStrCd");
            }
        }


        private string _addrlevel1num;
        /// <summary>
        /// 405003800 номер дома (ADDR_LEVEL1_NUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 405003800)]
        public string AddrLevel1Num
        {
            get
            {
                CheckPropertyInited("AddrLevel1Num");
                return _addrlevel1num;
            }
            set
            {
                _addrlevel1num = value;
                NotifyPropertyChanged("AddrLevel1Num");
            }
        }


        private string _addrlevel2num;
        /// <summary>
        /// 405003900 номер корпуса (ADDR_LEVEL2_NUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 405003900)]
        public string AddrLevel2Num
        {
            get
            {
                CheckPropertyInited("AddrLevel2Num");
                return _addrlevel2num;
            }
            set
            {
                _addrlevel2num = value;
                NotifyPropertyChanged("AddrLevel2Num");
            }
        }


        private string _addrlevel3num;
        /// <summary>
        /// 405004000 номер строения (ADDR_LEVEL3_NUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 405004000)]
        public string AddrLevel3Num
        {
            get
            {
                CheckPropertyInited("AddrLevel3Num");
                return _addrlevel3num;
            }
            set
            {
                _addrlevel3num = value;
                NotifyPropertyChanged("AddrLevel3Num");
            }
        }


        private string _addrapart;
        /// <summary>
        /// 405004100 номер квартиры (ADDR_APART)
        /// </summary>
        [RegisterAttribute(AttributeID = 405004100)]
        public string AddrApart
        {
            get
            {
                CheckPropertyInited("AddrApart");
                return _addrapart;
            }
            set
            {
                _addrapart = value;
                NotifyPropertyChanged("AddrApart");
            }
        }


        private string _addrother;
        /// <summary>
        /// 405004200 адрес. иное (ADDR_OTHER)
        /// </summary>
        [RegisterAttribute(AttributeID = 405004200)]
        public string AddrOther
        {
            get
            {
                CheckPropertyInited("AddrOther");
                return _addrother;
            }
            set
            {
                _addrother = value;
                NotifyPropertyChanged("AddrOther");
            }
        }


        private string _addrnote;
        /// <summary>
        /// 405004300 адрес. неформализованное описание (ADDR_NOTE)
        /// </summary>
        [RegisterAttribute(AttributeID = 405004300)]
        public string AddrNote
        {
            get
            {
                CheckPropertyInited("AddrNote");
                return _addrnote;
            }
            set
            {
                _addrnote = value;
                NotifyPropertyChanged("AddrNote");
            }
        }


        private string _numcadnum;
        /// <summary>
        /// 405004400 кадастровый номер ( связка с BULDING_PARCEL_FROM_EHD) (NUM_CADNUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 405004400)]
        public string NumCadnum
        {
            get
            {
                CheckPropertyInited("NumCadnum");
                return _numcadnum;
            }
            set
            {
                _numcadnum = value;
                NotifyPropertyChanged("NumCadnum");
            }
        }


        private string _numcondnum;
        /// <summary>
        /// 405004500 условный номер (NUM_CONDNUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 405004500)]
        public string NumCondnum
        {
            get
            {
                CheckPropertyInited("NumCondnum");
                return _numcondnum;
            }
            set
            {
                _numcondnum = value;
                NotifyPropertyChanged("NumCondnum");
            }
        }


        private string _name;
        /// <summary>
        /// 405004600 наименование объекта недвижимости (NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 405004600)]
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


        private string _floorgr;
        /// <summary>
        /// 405004700 этажность (для зданий) (FLOOR_GR)
        /// </summary>
        [RegisterAttribute(AttributeID = 405004700)]
        public string FloorGr
        {
            get
            {
                CheckPropertyInited("FloorGr");
                return _floorgr;
            }
            set
            {
                _floorgr = value;
                NotifyPropertyChanged("FloorGr");
            }
        }


        private string _floorund;
        /// <summary>
        /// 405004800 этажность подземная (для зданий) (FLOOR_UND)
        /// </summary>
        [RegisterAttribute(AttributeID = 405004800)]
        public string FloorUnd
        {
            get
            {
                CheckPropertyInited("FloorUnd");
                return _floorund;
            }
            set
            {
                _floorund = value;
                NotifyPropertyChanged("FloorUnd");
            }
        }


        private decimal? _techarheight;
        /// <summary>
        /// 405004900 высота (TECHAR_HEIGHT)
        /// </summary>
        [RegisterAttribute(AttributeID = 405004900)]
        public decimal? TecharHeight
        {
            get
            {
                CheckPropertyInited("TecharHeight");
                return _techarheight;
            }
            set
            {
                _techarheight = value;
                NotifyPropertyChanged("TecharHeight");
            }
        }


        private decimal? _techarlenght;
        /// <summary>
        /// 405005000 длина (TECHAR_LENGHT)
        /// </summary>
        [RegisterAttribute(AttributeID = 405005000)]
        public decimal? TecharLenght
        {
            get
            {
                CheckPropertyInited("TecharLenght");
                return _techarlenght;
            }
            set
            {
                _techarlenght = value;
                NotifyPropertyChanged("TecharLenght");
            }
        }


        private decimal? _techarvol;
        /// <summary>
        /// 405005100 объем (TECHAR_VOL)
        /// </summary>
        [RegisterAttribute(AttributeID = 405005100)]
        public decimal? TecharVol
        {
            get
            {
                CheckPropertyInited("TecharVol");
                return _techarvol;
            }
            set
            {
                _techarvol = value;
                NotifyPropertyChanged("TecharVol");
            }
        }


        private string _numfloor;
        /// <summary>
        /// 405005200 номер этажа (для помещений) (NUM_FLOOR)
        /// </summary>
        [RegisterAttribute(AttributeID = 405005200)]
        public string NumFloor
        {
            get
            {
                CheckPropertyInited("NumFloor");
                return _numfloor;
            }
            set
            {
                _numfloor = value;
                NotifyPropertyChanged("NumFloor");
            }
        }


        private string _numflat;
        /// <summary>
        /// 405005300 номер помещения (NUM_FLAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 405005300)]
        public string NumFlat
        {
            get
            {
                CheckPropertyInited("NumFlat");
                return _numflat;
            }
            set
            {
                _numflat = value;
                NotifyPropertyChanged("NumFlat");
            }
        }


        private DateTime? _regdt;
        /// <summary>
        /// 405005400 дата регистрации объекта (открытия подраздела i егрп) (REGDT)
        /// </summary>
        [RegisterAttribute(AttributeID = 405005400)]
        public DateTime? Regdt
        {
            get
            {
                CheckPropertyInited("Regdt");
                return _regdt;
            }
            set
            {
                _regdt = value;
                NotifyPropertyChanged("Regdt");
            }
        }


        private DateTime? _brkdt;
        /// <summary>
        /// 405005500 дата ликвидации, изменения объекта (закрытия подраздела i егрп) (BRKDT)
        /// </summary>
        [RegisterAttribute(AttributeID = 405005500)]
        public DateTime? Brkdt
        {
            get
            {
                CheckPropertyInited("Brkdt");
                return _brkdt;
            }
            set
            {
                _brkdt = value;
                NotifyPropertyChanged("Brkdt");
            }
        }


        private DateTime? _mdfdt;
        /// <summary>
        /// 405005600 дата внесения изменений (MDFDT)
        /// </summary>
        [RegisterAttribute(AttributeID = 405005600)]
        public DateTime? Mdfdt
        {
            get
            {
                CheckPropertyInited("Mdfdt");
                return _mdfdt;
            }
            set
            {
                _mdfdt = value;
                NotifyPropertyChanged("Mdfdt");
            }
        }


        private DateTime? _updt;
        /// <summary>
        /// 405005700 дата изменения информации (UPDT)
        /// </summary>
        [RegisterAttribute(AttributeID = 405005700)]
        public DateTime? Updt
        {
            get
            {
                CheckPropertyInited("Updt");
                return _updt;
            }
            set
            {
                _updt = value;
                NotifyPropertyChanged("Updt");
            }
        }


        private DateTime? _actdt;
        /// <summary>
        /// 405005800 дата, начиная с которой объект перестал быть актуальным (ACT_DT)
        /// </summary>
        [RegisterAttribute(AttributeID = 405005800)]
        public DateTime? ActDt
        {
            get
            {
                CheckPropertyInited("ActDt");
                return _actdt;
            }
            set
            {
                _actdt = value;
                NotifyPropertyChanged("ActDt");
            }
        }


        private string _objectid;
        /// <summary>
        /// 405005900 уникальный идентификатор объекта недвижимости в учетной системе (OBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 405005900)]
        public string ObjectId
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


        private DateTime? _updatedate;
        /// <summary>
        /// 405006000 дата данного обновления (UPDATE_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 405006000)]
        public DateTime? UpdateDate
        {
            get
            {
                CheckPropertyInited("UpdateDate");
                return _updatedate;
            }
            set
            {
                _updatedate = value;
                NotifyPropertyChanged("UpdateDate");
            }
        }


        private long? _actualid;
        /// <summary>
        /// 405006100 global_id последней версии данного участка, null для последней версии (ACTUAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 405006100)]
        public long? ActualId
        {
            get
            {
                CheckPropertyInited("ActualId");
                return _actualid;
            }
            set
            {
                _actualid = value;
                NotifyPropertyChanged("ActualId");
            }
        }


        private DateTime? _actualondate;
        /// <summary>
        /// 405006200 актуальность записи (ACTUAL_ON_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 405006200)]
        public DateTime? ActualOnDate
        {
            get
            {
                CheckPropertyInited("ActualOnDate");
                return _actualondate;
            }
            set
            {
                _actualondate = value;
                NotifyPropertyChanged("ActualOnDate");
            }
        }


        private string _addresstotal;
        /// <summary>
        /// 405006300 адрес (объединение всех полей) (ADDRESS_TOTAL)
        /// </summary>
        [RegisterAttribute(AttributeID = 405006300)]
        public string AddressTotal
        {
            get
            {
                CheckPropertyInited("AddressTotal");
                return _addresstotal;
            }
            set
            {
                _addresstotal = value;
                NotifyPropertyChanged("AddressTotal");
            }
        }


        private string _json;
        /// <summary>
        /// 405006400 json представление данного документа (JSON)
        /// </summary>
        [RegisterAttribute(AttributeID = 405006400)]
        public string Json
        {
            get
            {
                CheckPropertyInited("Json");
                return _json;
            }
            set
            {
                _json = value;
                NotifyPropertyChanged("Json");
            }
        }


        private DateTime? _loaddate;
        /// <summary>
        /// 405006500 Дата загрузки (LOAD_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 405006500)]
        public DateTime? LoadDate
        {
            get
            {
                CheckPropertyInited("LoadDate");
                return _loaddate;
            }
            set
            {
                _loaddate = value;
                NotifyPropertyChanged("LoadDate");
            }
        }

    }
}

namespace ObjectModel.Ehd
{
    /// <summary>
    /// 406 EHD.RIGHT (EHD_RIGHT_Q)
    /// </summary>
    [RegisterInfo(RegisterID = 406)]
    [Serializable]
    public sealed partial class OMRight : OMBaseClass<OMRight>
    {

        private long _empid;
        /// <summary>
        /// 406000100 Идентификатор (EMP_ID)
        /// </summary>
        [PrimaryKey(AttributeID = 406000100)]
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


        private long? _egrpid;
        /// <summary>
        /// 406000200 id_in ehd.egrp ( ссылка на ID - EGRP  в  Реестре EGRP_FROM_EHD) (EGRP_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 406000200)]
        public long? EgrpId
        {
            get
            {
                CheckPropertyInited("EgrpId");
                return _egrpid;
            }
            set
            {
                _egrpid = value;
                NotifyPropertyChanged("EgrpId");
            }
        }


        private long? _globalid;
        /// <summary>
        /// 406000300 global_id (GLOBAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 406000300)]
        public long? GlobalId
        {
            get
            {
                CheckPropertyInited("GlobalId");
                return _globalid;
            }
            set
            {
                _globalid = value;
                NotifyPropertyChanged("GlobalId");
            }
        }


        private long? _ehdrightid;
        /// <summary>
        /// 406000400 id  в EHD.RIGHT (ID_EHD_RIGHT)
        /// </summary>
        [RegisterAttribute(AttributeID = 406000400)]
        public long? EhdRightId
        {
            get
            {
                CheckPropertyInited("EhdRightId");
                return _ehdrightid;
            }
            set
            {
                _ehdrightid = value;
                NotifyPropertyChanged("EhdRightId");
            }
        }


        private DateTime? _mdfdt;
        /// <summary>
        /// 406000500 дата внесения изменений (MDFDT)
        /// </summary>
        [RegisterAttribute(AttributeID = 406000500)]
        public DateTime? Mdfdt
        {
            get
            {
                CheckPropertyInited("Mdfdt");
                return _mdfdt;
            }
            set
            {
                _mdfdt = value;
                NotifyPropertyChanged("Mdfdt");
            }
        }


        private string _objectid;
        /// <summary>
        /// 406000600 код егрп (OBJECT_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 406000600)]
        public string ObjectId
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


        private DateTime? _regcloseregdt;
        /// <summary>
        /// 406000700 регистрация действия. прекращение (дата государственной регистрации) (REG_CLOSE_REGDT)
        /// </summary>
        [RegisterAttribute(AttributeID = 406000700)]
        public DateTime? RegCloseRegdt
        {
            get
            {
                CheckPropertyInited("RegCloseRegdt");
                return _regcloseregdt;
            }
            set
            {
                _regcloseregdt = value;
                NotifyPropertyChanged("RegCloseRegdt");
            }
        }


        private string _regcloseregnum;
        /// <summary>
        /// 406000800 регистрация действия. прекращение (номер государственной регистрации) (REG_CLOSE_REGNUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 406000800)]
        public string RegCloseRegnum
        {
            get
            {
                CheckPropertyInited("RegCloseRegnum");
                return _regcloseregnum;
            }
            set
            {
                _regcloseregnum = value;
                NotifyPropertyChanged("RegCloseRegnum");
            }
        }


        private DateTime? _regopenregdt;
        /// <summary>
        /// 406000900 регистрация действия. возникновение (дата государственной регистрации) (REG_OPEN_REGDT)
        /// </summary>
        [RegisterAttribute(AttributeID = 406000900)]
        public DateTime? RegOpenRegdt
        {
            get
            {
                CheckPropertyInited("RegOpenRegdt");
                return _regopenregdt;
            }
            set
            {
                _regopenregdt = value;
                NotifyPropertyChanged("RegOpenRegdt");
            }
        }


        private string _regopenregnum;
        /// <summary>
        /// 406001000 регистрация действия. возникновение (номер государственной регистрации) (REG_OPEN_REGNUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 406001000)]
        public string RegOpenRegnum
        {
            get
            {
                CheckPropertyInited("RegOpenRegnum");
                return _regopenregnum;
            }
            set
            {
                _regopenregnum = value;
                NotifyPropertyChanged("RegOpenRegnum");
            }
        }


        private string _rightstcd;
        /// <summary>
        /// 406001100 статус состояния записи права (RIGHTST_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 406001100)]
        public string RightstCd
        {
            get
            {
                CheckPropertyInited("RightstCd");
                return _rightstcd;
            }
            set
            {
                _rightstcd = value;
                NotifyPropertyChanged("RightstCd");
            }
        }


        private string _righttpcd;
        /// <summary>
        /// 406001300 вид права (RIGHTTP_CD)
        /// </summary>
        [RegisterAttribute(AttributeID = 406001300)]
        public string RighttpCd
        {
            get
            {
                CheckPropertyInited("RighttpCd");
                return _righttpcd;
            }
            set
            {
                _righttpcd = value;
                NotifyPropertyChanged("RighttpCd");
            }
        }


        private VidPravaRasshirennoyChasti _righttpcd_Code;
        /// <summary>
        /// 406001300 вид права (справочный код) (RIGHTTP_CD_CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 406001300)]
        public VidPravaRasshirennoyChasti RighttpCd_Code
        {
            get
            {
                CheckPropertyInited("RighttpCd_Code");
                return this._righttpcd_Code;
            }
            set
            {
                string descr = value.GetEnumDescription();

                if (string.IsNullOrEmpty(descr))
                {
                    if (string.IsNullOrEmpty(_righttpcd))
                    {
                         _righttpcd = descr;
                    }
                }
                else
                {
                     _righttpcd = descr;
                }

                this._righttpcd_Code = value;
                NotifyPropertyChanged("RighttpCd");
                NotifyPropertyChanged("RighttpCd_Code");
            }
        }


        private string _rightkey;
        /// <summary>
        /// 406001400 код права (RIGHT_KEY)
        /// </summary>
        [RegisterAttribute(AttributeID = 406001400)]
        public string RightKey
        {
            get
            {
                CheckPropertyInited("RightKey");
                return _rightkey;
            }
            set
            {
                _rightkey = value;
                NotifyPropertyChanged("RightKey");
            }
        }


        private decimal? _sharecomflatden;
        /// <summary>
        /// 406001500 размер доли в праве общей долевой собственности на общее имущество в квартире (знаменатель дроби) (SHARECOMFLAT_DEN)
        /// </summary>
        [RegisterAttribute(AttributeID = 406001500)]
        public decimal? SharecomflatDen
        {
            get
            {
                CheckPropertyInited("SharecomflatDen");
                return _sharecomflatden;
            }
            set
            {
                _sharecomflatden = value;
                NotifyPropertyChanged("SharecomflatDen");
            }
        }


        private decimal? _sharecomflatnum;
        /// <summary>
        /// 406001600 размер доли в праве общей долевой собственности на общее имущество в квартире (числитель дроби) (SHARECOMFLAT_NUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 406001600)]
        public decimal? SharecomflatNum
        {
            get
            {
                CheckPropertyInited("SharecomflatNum");
                return _sharecomflatnum;
            }
            set
            {
                _sharecomflatnum = value;
                NotifyPropertyChanged("SharecomflatNum");
            }
        }


        private string _sharecomflattext;
        /// <summary>
        /// 406001700 размер доли в праве общей долевой собственности на общее имущество в квартире (значение текстом) (SHARECOMFLAT_TEXT)
        /// </summary>
        [RegisterAttribute(AttributeID = 406001700)]
        public string SharecomflatText
        {
            get
            {
                CheckPropertyInited("SharecomflatText");
                return _sharecomflattext;
            }
            set
            {
                _sharecomflattext = value;
                NotifyPropertyChanged("SharecomflatText");
            }
        }


        private decimal? _sharecomden;
        /// <summary>
        /// 406001800 размер доли в праве общей долевой собственности на общее имущество в многоквартирном доме (знаменатель дроби) (SHARECOM_DEN)
        /// </summary>
        [RegisterAttribute(AttributeID = 406001800)]
        public decimal? SharecomDen
        {
            get
            {
                CheckPropertyInited("SharecomDen");
                return _sharecomden;
            }
            set
            {
                _sharecomden = value;
                NotifyPropertyChanged("SharecomDen");
            }
        }


        private decimal? _sharecomnum;
        /// <summary>
        /// 406001900 размер доли в праве общей долевой собственности на общее имущество в многоквартирном доме (числитель дроби) (SHARECOM_NUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 406001900)]
        public decimal? SharecomNum
        {
            get
            {
                CheckPropertyInited("SharecomNum");
                return _sharecomnum;
            }
            set
            {
                _sharecomnum = value;
                NotifyPropertyChanged("SharecomNum");
            }
        }


        private string _sharecomtext;
        /// <summary>
        /// 406002000 размер доли в праве общей долевой собственности на общее имущество в многоквартирном доме (значение текстом) (SHARECOM_TEXT)
        /// </summary>
        [RegisterAttribute(AttributeID = 406002000)]
        public string SharecomText
        {
            get
            {
                CheckPropertyInited("SharecomText");
                return _sharecomtext;
            }
            set
            {
                _sharecomtext = value;
                NotifyPropertyChanged("SharecomText");
            }
        }


        private decimal? _shareden;
        /// <summary>
        /// 406002100 размер доли в праве (знаменатель дроби) (SHARE_DEN)
        /// </summary>
        [RegisterAttribute(AttributeID = 406002100)]
        public decimal? ShareDen
        {
            get
            {
                CheckPropertyInited("ShareDen");
                return _shareden;
            }
            set
            {
                _shareden = value;
                NotifyPropertyChanged("ShareDen");
            }
        }


        private decimal? _sharenum;
        /// <summary>
        /// 406002200 размер доли в праве (числитель дроби) (SHARE_NUM)
        /// </summary>
        [RegisterAttribute(AttributeID = 406002200)]
        public decimal? ShareNum
        {
            get
            {
                CheckPropertyInited("ShareNum");
                return _sharenum;
            }
            set
            {
                _sharenum = value;
                NotifyPropertyChanged("ShareNum");
            }
        }


        private string _sharetext;
        /// <summary>
        /// 406002300 размер доли в праве (значение текстом) (SHARE_TEXT)
        /// </summary>
        [RegisterAttribute(AttributeID = 406002300)]
        public string ShareText
        {
            get
            {
                CheckPropertyInited("ShareText");
                return _sharetext;
            }
            set
            {
                _sharetext = value;
                NotifyPropertyChanged("ShareText");
            }
        }


        private string _tpname;
        /// <summary>
        /// 406002400 вид права (TP_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 406002400)]
        public string TpName
        {
            get
            {
                CheckPropertyInited("TpName");
                return _tpname;
            }
            set
            {
                _tpname = value;
                NotifyPropertyChanged("TpName");
            }
        }


        private DateTime? _loaddate;
        /// <summary>
        /// 406002500 Дата загрузки (LOAD_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 406002500)]
        public DateTime? LoadDate
        {
            get
            {
                CheckPropertyInited("LoadDate");
                return _loaddate;
            }
            set
            {
                _loaddate = value;
                NotifyPropertyChanged("LoadDate");
            }
        }

    }
}

namespace ObjectModel.Ehd
{
    /// <summary>
    /// 407 EHD.OLD_NUMBERS (EHD_OLD_NUMBERS)
    /// </summary>
    [RegisterInfo(RegisterID = 407)]
    [Serializable]
    public sealed partial class OMOldNumber : OMBaseClass<OMOldNumber>
    {

        private long _id;
        /// <summary>
        /// 40700100 id (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 40700100)]
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


        private long? _globalid;
        /// <summary>
        /// 40700200 global_id (GLOBAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 40700200)]
        public long? GlobalId
        {
            get
            {
                CheckPropertyInited("GlobalId");
                return _globalid;
            }
            set
            {
                _globalid = value;
                NotifyPropertyChanged("GlobalId");
            }
        }


        private string _type;
        /// <summary>
        /// 40700300 type (TYPE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40700300)]
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


        private string _number;
        /// <summary>
        /// 40700400 number (NUMBER)
        /// </summary>
        [RegisterAttribute(AttributeID = 40700400)]
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
        /// 40700500 date (DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40700500)]
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


        private string _organ;
        /// <summary>
        /// 40700600 organ (ORGAN)
        /// </summary>
        [RegisterAttribute(AttributeID = 40700600)]
        public string Organ
        {
            get
            {
                CheckPropertyInited("Organ");
                return _organ;
            }
            set
            {
                _organ = value;
                NotifyPropertyChanged("Organ");
            }
        }


        private long _registerid;
        /// <summary>
        /// 40700700 register_id (REGISTER_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 40700700)]
        public long RegisterId
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


        private DateTime _loaddate;
        /// <summary>
        /// 40700800 Дата загрузки (LOAD_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 40700800)]
        public DateTime LoadDate
        {
            get
            {
                CheckPropertyInited("LoadDate");
                return _loaddate;
            }
            set
            {
                _loaddate = value;
                NotifyPropertyChanged("LoadDate");
            }
        }

    }
}

namespace ObjectModel.Ehd
{
    /// <summary>
    /// 408 Этажность (EHD_FLOORS)
    /// </summary>
    [RegisterInfo(RegisterID = 408)]
    [Serializable]
    public sealed partial class OMFloors : OMBaseClass<OMFloors>
    {

        private long _id;
        /// <summary>
        /// 40800100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 40800100)]
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


        private decimal? _buildingparcelid;
        /// <summary>
        /// 40800200 building_parcel_id (BUILDING_PARCEL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 40800200)]
        public decimal? BuildingParcelId
        {
            get
            {
                CheckPropertyInited("BuildingParcelId");
                return _buildingparcelid;
            }
            set
            {
                _buildingparcelid = value;
                NotifyPropertyChanged("BuildingParcelId");
            }
        }


        private decimal? _globalid;
        /// <summary>
        /// 40800300 global_id (GLOBAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 40800300)]
        public decimal? GlobalId
        {
            get
            {
                CheckPropertyInited("GlobalId");
                return _globalid;
            }
            set
            {
                _globalid = value;
                NotifyPropertyChanged("GlobalId");
            }
        }


        private string _floors;
        /// <summary>
        /// 40800400 Этажность (FLOORS)
        /// </summary>
        [RegisterAttribute(AttributeID = 40800400)]
        public string Floors
        {
            get
            {
                CheckPropertyInited("Floors");
                return _floors;
            }
            set
            {
                _floors = value;
                NotifyPropertyChanged("Floors");
            }
        }


        private string _undergroundfloors;
        /// <summary>
        /// 40800500 Ко-во подземных этажей (UNDERGROUND_FLOORS)
        /// </summary>
        [RegisterAttribute(AttributeID = 40800500)]
        public string UndergroundFloors
        {
            get
            {
                CheckPropertyInited("UndergroundFloors");
                return _undergroundfloors;
            }
            set
            {
                _undergroundfloors = value;
                NotifyPropertyChanged("UndergroundFloors");
            }
        }

    }
}

namespace ObjectModel.Ehd.Elements
{
    /// <summary>
    /// 409 Материал стен (EHD_ELEMENTS_CONSTRUCT)
    /// </summary>
    [RegisterInfo(RegisterID = 409)]
    [Serializable]
    public sealed partial class OMConstruct : OMBaseClass<OMConstruct>
    {

        private long _id;
        /// <summary>
        /// 40900100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 40900100)]
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


        private decimal? _buildingparcelid;
        /// <summary>
        /// 40900200 building_parcel_id (BUILDING_PARCEL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 40900200)]
        public decimal? BuildingParcelId
        {
            get
            {
                CheckPropertyInited("BuildingParcelId");
                return _buildingparcelid;
            }
            set
            {
                _buildingparcelid = value;
                NotifyPropertyChanged("BuildingParcelId");
            }
        }


        private decimal? _globalid;
        /// <summary>
        /// 40900300 global_id (GLOBAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 40900300)]
        public decimal? GlobalId
        {
            get
            {
                CheckPropertyInited("GlobalId");
                return _globalid;
            }
            set
            {
                _globalid = value;
                NotifyPropertyChanged("GlobalId");
            }
        }


        private string _wall;
        /// <summary>
        /// 40900400 Материал стен (WALL)
        /// </summary>
        [RegisterAttribute(AttributeID = 40900400)]
        public string Wall
        {
            get
            {
                CheckPropertyInited("Wall");
                return _wall;
            }
            set
            {
                _wall = value;
                NotifyPropertyChanged("Wall");
            }
        }

    }
}

namespace ObjectModel.Ehd.Exploitation
{
    /// <summary>
    /// 410 Постройка/ввод в эксплуатацию (EHD_EXPLOITATION_CHAR)
    /// </summary>
    [RegisterInfo(RegisterID = 410)]
    [Serializable]
    public sealed partial class OMChar : OMBaseClass<OMChar>
    {

        private long _id;
        /// <summary>
        /// 41000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 41000100)]
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


        private decimal? _buildingparcelid;
        /// <summary>
        /// 41000200 building_parcel_id (BUILDING_PARCEL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 41000200)]
        public decimal? BuildingParcelId
        {
            get
            {
                CheckPropertyInited("BuildingParcelId");
                return _buildingparcelid;
            }
            set
            {
                _buildingparcelid = value;
                NotifyPropertyChanged("BuildingParcelId");
            }
        }


        private decimal? _globalid;
        /// <summary>
        /// 41000300 global_id (GLOBAL_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 41000300)]
        public decimal? GlobalId
        {
            get
            {
                CheckPropertyInited("GlobalId");
                return _globalid;
            }
            set
            {
                _globalid = value;
                NotifyPropertyChanged("GlobalId");
            }
        }


        private string _yearbuilt;
        /// <summary>
        /// 41000400 Год постройки (YEAR_BUILT)
        /// </summary>
        [RegisterAttribute(AttributeID = 41000400)]
        public string YearBuilt
        {
            get
            {
                CheckPropertyInited("YearBuilt");
                return _yearbuilt;
            }
            set
            {
                _yearbuilt = value;
                NotifyPropertyChanged("YearBuilt");
            }
        }


        private string _yearused;
        /// <summary>
        /// 41000500 Год ввода в экстплуатацию (YEAR_USED)
        /// </summary>
        [RegisterAttribute(AttributeID = 41000500)]
        public string YearUsed
        {
            get
            {
                CheckPropertyInited("YearUsed");
                return _yearused;
            }
            set
            {
                _yearused = value;
                NotifyPropertyChanged("YearUsed");
            }
        }

    }
}

namespace ObjectModel.MV
{
    /// <summary>
    /// 500 Список REFRESH MATERIALIZED VIEW (MV_REFRESH_LIST)
    /// </summary>
    [RegisterInfo(RegisterID = 500)]
    [Serializable]
    public sealed partial class OMRefreshList : OMBaseClass<OMRefreshList>
    {

        private long _id;
        /// <summary>
        /// 500000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 500000100)]
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


        private string _mvname;
        /// <summary>
        /// 500000200 Название view (MV_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 500000200)]
        public string MvName
        {
            get
            {
                CheckPropertyInited("MvName");
                return _mvname;
            }
            set
            {
                _mvname = value;
                NotifyPropertyChanged("MvName");
            }
        }

    }
}

namespace ObjectModel.MV
{
    /// <summary>
    /// 501 Список логов RefreshList (MV_REFRESH_LOG)
    /// </summary>
    [RegisterInfo(RegisterID = 501)]
    [Serializable]
    public sealed partial class OMRefreshLog : OMBaseClass<OMRefreshLog>
    {

        private long _id;
        /// <summary>
        /// 501000100 Идентификатор (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 501000100)]
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


        private DateTime? _refreshdate;
        /// <summary>
        /// 501000200 Дата обновления (REFRESH_DATE)
        /// </summary>
        [RegisterAttribute(AttributeID = 501000200)]
        public DateTime? RefreshDate
        {
            get
            {
                CheckPropertyInited("RefreshDate");
                return _refreshdate;
            }
            set
            {
                _refreshdate = value;
                NotifyPropertyChanged("RefreshDate");
            }
        }


        private string _mvname;
        /// <summary>
        /// 501000300 Название view (MV_NAME)
        /// </summary>
        [RegisterAttribute(AttributeID = 501000300)]
        public string MvName
        {
            get
            {
                CheckPropertyInited("MvName");
                return _mvname;
            }
            set
            {
                _mvname = value;
                NotifyPropertyChanged("MvName");
            }
        }


        private string _msgevent;
        /// <summary>
        /// 501000400 Cообщение события (MSG_EVENT)
        /// </summary>
        [RegisterAttribute(AttributeID = 501000400)]
        public string MsgEvent
        {
            get
            {
                CheckPropertyInited("MsgEvent");
                return _msgevent;
            }
            set
            {
                _msgevent = value;
                NotifyPropertyChanged("MsgEvent");
            }
        }


        private string _errmessage;
        /// <summary>
        /// 501000500 Cообщение ошибки (ERR_MESSAGE)
        /// </summary>
        [RegisterAttribute(AttributeID = 501000500)]
        public string ErrMessage
        {
            get
            {
                CheckPropertyInited("ErrMessage");
                return _errmessage;
            }
            set
            {
                _errmessage = value;
                NotifyPropertyChanged("ErrMessage");
            }
        }

    }
}
