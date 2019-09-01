using System.ComponentModel;
using Core.ObjectModel.CustomAttribute;
using Core.Shared.Attributes;

namespace ObjectModel.Directory
{
    /// <summary>
    /// Источник данных (4)
    ///</summary>
    [ReferenceInfo(ReferenceId = 4)]
    public enum SourceType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Росреестр (1281646)
        /// </summary>
        [Description("Росреестр")]
        [EnumCode("Rosreestr")]
        Rosreestr = 1281646,
        /// <summary>
        /// Н2 (1299693)
        /// </summary>
        [Description("Н2")]
        [EnumCode("N2")]
        N2 = 1299693,
        /// <summary>
        /// БТИ (1299941)
        /// </summary>
        [Description("БТИ")]
        [EnumCode("01")]
        BTI = 1299941,
        /// <summary>
        /// Акционерные общества (1300124)
        /// </summary>
        [Description("Акционерные общества")]
        [EnumCode("JSC")]
        JSC = 1300124,
        /// <summary>
        /// Государственные унитарные предприятия (1300125)
        /// </summary>
        [Description("Государственные унитарные предприятия")]
        [EnumCode("UE")]
        UE = 1300125,
        /// <summary>
        /// Государственные учреждения (1300126)
        /// </summary>
        [Description("Государственные учреждения")]
        [EnumCode("PO")]
        PO = 1300126,
        /// <summary>
        /// СПД (1300544)
        /// </summary>
        [Description("СПД")]
        [EnumCode("SPD")]
        SPD = 1300544,
        /// <summary>
        /// Комбинированное представление (1301061)
        /// </summary>
        [Description("Комбинированное представление")]
        [EnumCode("CombinedView")]
        CombinedView = 1301061,
        /// <summary>
        /// ГУП Мосгоргеотрест (1301280)
        /// </summary>
        [Description("ГУП Мосгоргеотрест")]
        [EnumCode("GUPMGGT")]
        GUPMGGT = 1301280,
        /// <summary>
        /// Инвентаризация БТИ (1301733)
        /// </summary>
        [Description("Инвентаризация БТИ")]
        [EnumCode("INVENT")]
        InvBTI = 1301733,
        /// <summary>
        /// ФП (1412413)
        /// </summary>
        [Description("ФП")]
        [EnumCode("FP")]
        FP = 1412413,
        /// <summary>
        /// ДЖПиЖФ (10000072)
        /// </summary>
        [Description("ДЖПиЖФ")]
        [EnumCode("DJP")]
        DJP = 10000072,
        /// <summary>
        /// Инвентаризация ДГИ (10001064)
        /// </summary>
        [Description("Инвентаризация ДГИ")]
        [EnumCode("InvDGI")]
        InvDGI = 10001064,
        /// <summary>
        /// MS4 (10001065)
        /// </summary>
        [Description("MS4")]
        [EnumCode("MS4")]
        MS4 = 10001065,
        /// <summary>
        /// ДНиПП (10007575)
        /// </summary>
        [Description("ДНиПП")]
        [EnumCode("DNPP")]
        DNP = 10007575,
        /// <summary>
        /// ИСИО (10007576)
        /// </summary>
        [Description("ИСИО")]
        [EnumCode("ISIO")]
        Isio = 10007576,
        /// <summary>
        /// АС УР (НСИ) (10007606)
        /// </summary>
        [Description("АС УР (НСИ)")]
        [EnumCode("ASUR_NSI")]
        AsurNsi = 10007606,
        /// <summary>
        /// FLS2005 (100007607)
        /// </summary>
        [Description("FLS2005")]
        [EnumCode("")]
        Fls2005 = 100007607,
        /// <summary>
        /// Реестр договоров (1000235005)
        /// </summary>
        [Description("Реестр договоров")]
        [EnumCode("")]
        RegisterOfContracts = 1000235005,
    }

    /// <summary>
    /// БТИ: Класс строения (20)
    ///</summary>
    [ReferenceInfo(ReferenceId = 20)]
    public enum BuildingClass : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// жилые (1909)
        /// </summary>
        [Description("жилые")]
        [EnumCode("1")]
        Living = 1909,
    }

    /// <summary>
    /// БТИ: Назначение (21)
    ///</summary>
    [ReferenceInfo(ReferenceId = 21)]
    public enum Purpose : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// БТИ: Статус адреса (24)
    ///</summary>
    [ReferenceInfo(ReferenceId = 24)]
    public enum AddressStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Основной (770)
        /// </summary>
        [Description("Основной")]
        [EnumCode("1")]
        Main = 770,
        /// <summary>
        /// Официальный (771)
        /// </summary>
        [Description("Официальный")]
        [EnumCode("2")]
        AlternativeCurrent = 771,
    }

    /// <summary>
    /// БТИ: Тип площади комнаты (39)
    ///</summary>
    [ReferenceInfo(ReferenceId = 39)]
    public enum PremisesTypes : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Назначения здания (Росреестр) (60)
    ///</summary>
    [ReferenceInfo(ReferenceId = 60)]
    public enum BuildingPurposeRosreestr : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Нежилое здание (1281445)
        /// </summary>
        [Description("Нежилое здание")]
        [EnumCode("005001001000")]
        NonresidentialBuilding = 1281445,
        /// <summary>
        /// Жилой дом (1281446)
        /// </summary>
        [Description("Жилой дом")]
        [EnumCode("005001002000")]
        ResidentialBuilding = 1281446,
        /// <summary>
        /// Многоквартирный дом (1281447)
        /// </summary>
        [Description("Многоквартирный дом")]
        [EnumCode("005001003000")]
        ApartmentBuilding = 1281447,
        /// <summary>
        /// Иное (1281448)
        /// </summary>
        [Description("Иное")]
        [EnumCode("005001999000")]
        Otherwise = 1281448,
    }

    /// <summary>
    /// БТИ: Состояние строения (65)
    ///</summary>
    [ReferenceInfo(ReferenceId = 65)]
    public enum StructureStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// БТИ: Тип решения по помещению (71)
    ///</summary>
    [ReferenceInfo(ReferenceId = 71)]
    public enum SolutionTypes : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// БТИ: Содержание решения по помещению (72)
    ///</summary>
    [ReferenceInfo(ReferenceId = 72)]
    public enum SolutionContent : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// БТИ: Статус обременения (73)
    ///</summary>
    [ReferenceInfo(ReferenceId = 73)]
    public enum BurdenStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Источник заполнения (301)
    ///</summary>
    [ReferenceInfo(ReferenceId = 301)]
    public enum SourceInput : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// ЕГРН (301001)
        /// </summary>
        [Description("ЕГРН")]
        [EnumCode("1")]
        Egrn = 301001,
        /// <summary>
        /// БТИ (301002)
        /// </summary>
        [Description("БТИ")]
        [EnumCode("2")]
        Bti = 301002,
        /// <summary>
        /// Ручной ввод (301003)
        /// </summary>
        [Description("Ручной ввод")]
        [EnumCode("3")]
        ManualInput = 301003,
        /// <summary>
        /// МФЦ (301004)
        /// </summary>
        [Description("МФЦ")]
        [EnumCode("4")]
        Mfc = 301004,
        /// <summary>
        /// СК (301005)
        /// </summary>
        [Description("СК")]
        [EnumCode("5")]
        InsuranceCompany = 301005,
        /// <summary>
        /// Система (301006)
        /// </summary>
        [Description("Система")]
        [EnumCode("6")]
        System = 301006,
    }

    /// <summary>
    /// Статус расчета (303)
    ///</summary>
    [ReferenceInfo(ReferenceId = 303)]
    public enum CalculationStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Сформирован расчет (303001)
        /// </summary>
        [Description("Сформирован расчет")]
        [EnumCode("1")]
        Created = 303001,
        /// <summary>
        /// Согласован проект договора (303002)
        /// </summary>
        [Description("Согласован проект договора")]
        [EnumCode("2")]
        ProjectAgreed = 303002,
        /// <summary>
        /// Проект договора (303003)
        /// </summary>
        [Description("Проект договора")]
        [EnumCode("3")]
        ProjectAgreement = 303003,
        /// <summary>
        /// Заключен договор (303004)
        /// </summary>
        [Description("Заключен договор")]
        [EnumCode("4")]
        ContractConcluded = 303004,
        /// <summary>
        /// Согласован расчет (303005)
        /// </summary>
        [Description("Согласован расчет")]
        [EnumCode("5")]
        Agreed = 303005,
    }

    /// <summary>
    /// Доля ответственности СК (304)
    ///</summary>
    [ReferenceInfo(ReferenceId = 304)]
    public enum PartCompensationType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Жилые помещения по общему тарифу (304001)
        /// </summary>
        [Description("Жилые помещения по общему тарифу")]
        [EnumCode("1")]
        ComonRate = 304001,
        /// <summary>
        /// Жилые помещения по индивидуальному тарифу (304002)
        /// </summary>
        [Description("Жилые помещения по индивидуальному тарифу")]
        [EnumCode("2")]
        IndividualRate = 304002,
        /// <summary>
        /// Общее имущество (304003)
        /// </summary>
        [Description("Общее имущество")]
        [EnumCode("3")]
        CommonProperty = 304003,
    }

    /// <summary>
    /// Форма объединения собственников (305)
    ///</summary>
    [ReferenceInfo(ReferenceId = 305)]
    public enum FormAssociationOwners : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// ЖСК/ЖК (305001)
        /// </summary>
        [Description("ЖСК/ЖК")]
        [EnumCode("6")]
        GskGk = 305001,
        /// <summary>
        /// ТСЖ (305002)
        /// </summary>
        [Description("ТСЖ")]
        [EnumCode("7")]
        Tsg = 305002,
        /// <summary>
        /// БО (305003)
        /// </summary>
        [Description("БО")]
        [EnumCode("8")]
        Bo = 305003,
    }

    /// <summary>
    /// Признак рассрочки платежа (306)
    ///</summary>
    [ReferenceInfo(ReferenceId = 306)]
    public enum SignInstallmentPayment : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Полный платеж (306001)
        /// </summary>
        [Description("Полный платеж")]
        [EnumCode("1")]
        FullPayment = 306001,
        /// <summary>
        /// По полугодиям (306002)
        /// </summary>
        [Description("По полугодиям")]
        [EnumCode("2")]
        PaymentByHalfYear = 306002,
        /// <summary>
        /// По кварталам (306003)
        /// </summary>
        [Description("По кварталам")]
        [EnumCode("3")]
        PaymentByQuarter = 306003,
        /// <summary>
        /// По месяцам (306004)
        /// </summary>
        [Description("По месяцам")]
        [EnumCode("4")]
        PaymentByMonth = 306004,
        /// <summary>
        /// Иная рассрочка (306005)
        /// </summary>
        [Description("Иная рассрочка")]
        [EnumCode("5")]
        OtherInstallments = 306005,
    }

    /// <summary>
    /// Источник поступления адреса (307)
    ///</summary>
    [ReferenceInfo(ReferenceId = 307)]
    public enum AddressSource : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// ФИАС (307001)
        /// </summary>
        [Description("ФИАС")]
        [EnumCode("1")]
        Fias = 307001,
        /// <summary>
        /// БТИ (307002)
        /// </summary>
        [Description("БТИ")]
        [EnumCode("2")]
        Bti = 307002,
        /// <summary>
        /// ЕГРН (307003)
        /// </summary>
        [Description("ЕГРН")]
        [EnumCode("3")]
        Egrn = 307003,
        /// <summary>
        /// Вручную (307004)
        /// </summary>
        [Description("Вручную")]
        [EnumCode("4")]
        Manual = 307004,
    }

    /// <summary>
    /// Тип ФСП (333)
    ///</summary>
    [ReferenceInfo(ReferenceId = 333)]
    public enum FSPType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// ЕПД (333001)
        /// </summary>
        [Description("ЕПД")]
        [EnumCode("")]
        EPD = 333001,
        /// <summary>
        /// Полис (333002)
        /// </summary>
        [Description("Полис")]
        [EnumCode("")]
        Polis = 333002,
        /// <summary>
        /// Свидетельство (333003)
        /// </summary>
        [Description("Свидетельство")]
        [EnumCode("")]
        Svidetelstvo = 333003,
        /// <summary>
        /// Общее имущество (333004)
        /// </summary>
        [Description("Общее имущество")]
        [EnumCode("")]
        ObscheeImuschestvo = 333004,
    }

    /// <summary>
    /// Субъект РФ - ФИАС (401)
    ///</summary>
    [ReferenceInfo(ReferenceId = 401)]
    public enum FiasSubjectRf : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Адм. округ - ФИАС (402)
    ///</summary>
    [ReferenceInfo(ReferenceId = 402)]
    public enum FiasAdministrativeRegion : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Район - ФИАС (403)
    ///</summary>
    [ReferenceInfo(ReferenceId = 403)]
    public enum FiasDistrict : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Город(с/п) - ФИАС (404)
    ///</summary>
    [ReferenceInfo(ReferenceId = 404)]
    public enum FiasCity : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Район города - ФИАС (405)
    ///</summary>
    [ReferenceInfo(ReferenceId = 405)]
    public enum FiasCityDistrict : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Нас. пункт - ФИАС (406)
    ///</summary>
    [ReferenceInfo(ReferenceId = 406)]
    public enum FiasLocality : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Улица - ФИАС (407)
    ///</summary>
    [ReferenceInfo(ReferenceId = 407)]
    public enum FiasStreet : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Дом - ФИАС (408)
    ///</summary>
    [ReferenceInfo(ReferenceId = 408)]
    public enum FiasHouse : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Да / Нет (10079)
    ///</summary>
    [ReferenceInfo(ReferenceId = 10079)]
    public enum YesNo : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Да (1005995)
        /// </summary>
        [Description("Да")]
        [EnumCode("01")]
        Yes = 1005995,
        /// <summary>
        /// Нет (1005996)
        /// </summary>
        [Description("Нет")]
        [EnumCode("02")]
        No = 1005996,
    }

    /// <summary>
    /// Вид права расширенной части (12029)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12029)]
    public enum VidPravaRasshirennoyChasti : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Универсальный (1290855)
        /// </summary>
        [Description("Универсальный")]
        [EnumCode("")]
        Universalnyj = 1290855,
        /// <summary>
        /// Безвозмездное срочное пользование (1290856)
        /// </summary>
        [Description("Безвозмездное срочное пользование")]
        [EnumCode("01")]
        BezvozmezdnoeSrochnoePolzovanie = 1290856,
        /// <summary>
        /// Постоянное (бессрочное) пользование (1290857)
        /// </summary>
        [Description("Постоянное (бессрочное) пользование")]
        [EnumCode("02")]
        PostoyannoeBessrochnoePolzovanie = 1290857,
        /// <summary>
        /// Социальный наем (1290858)
        /// </summary>
        [Description("Социальный наем")]
        [EnumCode("03")]
        SotsialnyjNajm = 1290858,
        /// <summary>
        /// Региональная собственность (1290859)
        /// </summary>
        [Description("Региональная собственность")]
        [EnumCode("04")]
        RegionalnayaSobstvennost = 1290859,
        /// <summary>
        /// Безвозмездное пользование (1290860)
        /// </summary>
        [Description("Безвозмездное пользование")]
        [EnumCode("05")]
        BezvozmezdnoePolzovanie = 1290860,
        /// <summary>
        /// Доверительное управление (1290861)
        /// </summary>
        [Description("Доверительное управление")]
        [EnumCode("06")]
        DoveritelnoeUpravlenie = 1290861,
        /// <summary>
        /// Оперативное управление (1290862)
        /// </summary>
        [Description("Оперативное управление")]
        [EnumCode("07")]
        OperativnoeUpravlenie = 1290862,
        /// <summary>
        /// Иностранная собственность (1290863)
        /// </summary>
        [Description("Иностранная собственность")]
        [EnumCode("08")]
        InostrannayaSobstvennost = 1290863,
        /// <summary>
        /// Хозяйственное ведение (1290864)
        /// </summary>
        [Description("Хозяйственное ведение")]
        [EnumCode("09")]
        KhozyajstvennoeVedenie = 1290864,
        /// <summary>
        /// Аренда (1290865)
        /// </summary>
        [Description("Аренда")]
        [EnumCode("10")]
        Arenda = 1290865,
        /// <summary>
        /// Частная собственность (1290866)
        /// </summary>
        [Description("Частная собственность")]
        [EnumCode("11")]
        ChastnayaSobstvennost = 1290866,
        /// <summary>
        /// Федеральная собственность (1290867)
        /// </summary>
        [Description("Федеральная собственность")]
        [EnumCode("12")]
        FederalnayaSobstvennost = 1290867,
        /// <summary>
        /// Общедолевая собственность (1290868)
        /// </summary>
        [Description("Общедолевая собственность")]
        [EnumCode("13")]
        ObshedolevayaSobstvennost = 1290868,
        /// <summary>
        /// Субаренда (1290869)
        /// </summary>
        [Description("Субаренда")]
        [EnumCode("14")]
        Subarenda = 1290869,
        /// <summary>
        /// Муниципальная собственность (1290870)
        /// </summary>
        [Description("Муниципальная собственность")]
        [EnumCode("15")]
        MunitsipalnayaSobstvennost = 1290870,
        /// <summary>
        /// Собственность (1299708)
        /// </summary>
        [Description("Собственность")]
        [EnumCode("16")]
        Sobstvennost = 1299708,
        /// <summary>
        /// Незарегистрированная собственность г.Москвы (1299709)
        /// </summary>
        [Description("Незарегистрированная собственность г.Москвы")]
        [EnumCode("17")]
        NezaregistrirovannayaSobstvennostGmoskvy = 1299709,
        /// <summary>
        /// Возможная незарегистрированная собственность третьих лиц (1299710)
        /// </summary>
        [Description("Возможная незарегистрированная собственность третьих лиц")]
        [EnumCode("18")]
        VozmozhnayaNezaregistrirovannayaSobstvennostTretikhLits = 1299710,
        /// <summary>
        /// Долевая собственность (1301084)
        /// </summary>
        [Description("Долевая собственность")]
        [EnumCode("19")]
        DolevayaSobstvennost = 1301084,
        /// <summary>
        /// Совместная собственность (1301085)
        /// </summary>
        [Description("Совместная собственность")]
        [EnumCode("20")]
        SovmestnayaSobstvennost = 1301085,
        /// <summary>
        /// Пожизненное-наследуемое владение (1301086)
        /// </summary>
        [Description("Пожизненное-наследуемое владение")]
        [EnumCode("21")]
        PozhiznennoenasleduemoeVladenie = 1301086,
        /// <summary>
        /// Сервитут (право) (1301087)
        /// </summary>
        [Description("Сервитут (право)")]
        [EnumCode("22")]
        ServitutPravo = 1301087,
        /// <summary>
        /// Иные права (1301088)
        /// </summary>
        [Description("Иные права")]
        [EnumCode("23")]
        InyePrava = 1301088,
        /// <summary>
        /// Инвестиционный договор (1301161)
        /// </summary>
        [Description("Инвестиционный договор")]
        [EnumCode("")]
        InvestitsionnyjDogovor = 1301161,
        /// <summary>
        /// Залог (1301162)
        /// </summary>
        [Description("Залог")]
        [EnumCode("46")]
        Zalog = 1301162,
        /// <summary>
        /// Договор простого товарищества (1301164)
        /// </summary>
        [Description("Договор простого товарищества")]
        [EnumCode("")]
        DogovorProstogoTovarishestva = 1301164,
        /// <summary>
        /// Иные обременения (1301165)
        /// </summary>
        [Description("Иные обременения")]
        [EnumCode("")]
        InyeObremeneniya = 1301165,
        /// <summary>
        /// Иное право пользования (1301166)
        /// </summary>
        [Description("Иное право пользования")]
        [EnumCode("")]
        InoePravoPolzovaniya = 1301166,
        /// <summary>
        /// Коммерческий найм (10000073)
        /// </summary>
        [Description("Коммерческий найм")]
        [EnumCode("24")]
        KommercheskijNajm = 10000073,
        /// <summary>
        /// Ограничение распоряжения жилым помещением (10000074)
        /// </summary>
        [Description("Ограничение распоряжения жилым помещением")]
        [EnumCode("25")]
        OgranichenieRasporyazheniyaZhilymPomesheniem = 10000074,
        /// <summary>
        /// Запрещение сделок с имуществом (10000075)
        /// </summary>
        [Description("Запрещение сделок с имуществом")]
        [EnumCode("26")]
        ZapreshenieSdelokSImushestvom = 10000075,
        /// <summary>
        /// Ипотека в силу закона (10000076)
        /// </summary>
        [Description("Ипотека в силу закона")]
        [EnumCode("27")]
        IpotekaVSiluZakona = 10000076,
        /// <summary>
        /// Купля-продажа с рассрочкой платежа (10000077)
        /// </summary>
        [Description("Купля-продажа с рассрочкой платежа")]
        [EnumCode("28")]
        KuplyaprodazhaSRassrochkoj = 10000077,
        /// <summary>
        /// Найм служебного помещения (10000078)
        /// </summary>
        [Description("Найм служебного помещения")]
        [EnumCode("29")]
        NajmSluzhebnogoPomesheniya = 10000078,
        /// <summary>
        /// Купля-продажа ипотека (10000079)
        /// </summary>
        [Description("Купля-продажа ипотека")]
        [EnumCode("30")]
        KuplyaprodazhaPoSotsialnojIpoteke = 10000079,
        /// <summary>
        /// Срочное возмездное пользование (найм) (10000080)
        /// </summary>
        [Description("Срочное возмездное пользование (найм)")]
        [EnumCode("31")]
        SrochnoeVozmezdnoePolzovanieNajm = 10000080,
        /// <summary>
        /// Арест (10000081)
        /// </summary>
        [Description("Арест")]
        [EnumCode("32")]
        Arest = 10000081,
        /// <summary>
        /// Мена передача в порядке компенсации (10000082)
        /// </summary>
        [Description("Мена передача в порядке компенсации")]
        [EnumCode("33")]
        MenaPeredachaVPoryadkeKompensatsii = 10000082,
        /// <summary>
        /// Коммерческий наем (10000083)
        /// </summary>
        [Description("Коммерческий наем")]
        [EnumCode("34")]
        KommercheskijNaem = 10000083,
        /// <summary>
        /// Рента (10000084)
        /// </summary>
        [Description("Рента")]
        [EnumCode("35")]
        Renta = 10000084,
        /// <summary>
        /// Запрещение заключения сделок с имуществом (10000085)
        /// </summary>
        [Description("Запрещение заключения сделок с имуществом")]
        [EnumCode("36")]
        ZapreshenieZaklyucheniyaSdelokSImushestvom = 10000085,
        /// <summary>
        /// Найм в общежитии (10000086)
        /// </summary>
        [Description("Найм в общежитии")]
        [EnumCode("37")]
        NajmPomesheniyaVObshezhitii = 10000086,
        /// <summary>
        /// Мена с оплатой разницы по ипотеке (10000087)
        /// </summary>
        [Description("Мена с оплатой разницы по ипотеке")]
        [EnumCode("38")]
        MenaPoSotsialnojIpoteke = 10000087,
        /// <summary>
        /// Право требования по договору (10000088)
        /// </summary>
        [Description("Право требования по договору")]
        [EnumCode("39")]
        PravoTrebovaniyaPoDogovoru = 10000088,
        /// <summary>
        /// Залог в силу закона (10000089)
        /// </summary>
        [Description("Залог в силу закона")]
        [EnumCode("40")]
        ZalogVSiluZakona = 10000089,
        /// <summary>
        /// Найм маневренный фонд (10000090)
        /// </summary>
        [Description("Найм маневренный фонд")]
        [EnumCode("41")]
        NajmManevrennogoFonda = 10000090,
        /// <summary>
        /// Купля-продажа (10000091)
        /// </summary>
        [Description("Купля-продажа")]
        [EnumCode("42")]
        Kuplyaprodazha = 10000091,
        /// <summary>
        /// Мена (10000092)
        /// </summary>
        [Description("Мена")]
        [EnumCode("43")]
        Mena = 10000092,
        /// <summary>
        /// Арест (10000093)
        /// </summary>
        [Description("Арест")]
        [EnumCode("44")]
        Arest1 = 10000093,
        /// <summary>
        /// Ограничение (10000094)
        /// </summary>
        [Description("Ограничение")]
        [EnumCode("45")]
        Ogranichenie = 10000094,
        /// <summary>
        /// Временное пользование (10000314)
        /// </summary>
        [Description("Временное пользование")]
        [EnumCode("46")]
        VremennoePolzovanie = 10000314,
        /// <summary>
        /// Краткосрочная аренда (10000316)
        /// </summary>
        [Description("Краткосрочная аренда")]
        [EnumCode("48")]
        KratkosrochnayaArenda = 10000316,
        /// <summary>
        /// Краткосрочная аренда на период строительства (10000317)
        /// </summary>
        [Description("Краткосрочная аренда на период строительства")]
        [EnumCode("49")]
        KratkosrochnayaArendaNaPeriodStroitelstva = 10000317,
        /// <summary>
        /// Безвозмездное временное пользование (10000318)
        /// </summary>
        [Description("Безвозмездное временное пользование")]
        [EnumCode("50")]
        BezvozmezdnoeVremennoePolzovanie = 10000318,
        /// <summary>
        /// Аренда с множественностью лиц (10000319)
        /// </summary>
        [Description("Аренда с множественностью лиц")]
        [EnumCode("51")]
        ArendaSMnozhestvennostyuLits = 10000319,
        /// <summary>
        /// Право ограниченного пользования ЗУ (10000320)
        /// </summary>
        [Description("Право ограниченного пользования ЗУ")]
        [EnumCode("52")]
        PravoOgranichennogoPolzovaniyaZu = 10000320,
        /// <summary>
        /// Декларирование (отсутствует) (10000321)
        /// </summary>
        [Description("Декларирование (отсутствует)")]
        [EnumCode("53")]
        DeklarirovanieOtsutstvuet = 10000321,
        /// <summary>
        /// Резервирование (10000322)
        /// </summary>
        [Description("Резервирование")]
        [EnumCode("54")]
        Rezervirovanie = 10000322,
        /// <summary>
        /// Не определен (10000323)
        /// </summary>
        [Description("Не определен")]
        [EnumCode("55")]
        NeOpredelen = 10000323,
        /// <summary>
        /// Найм (10000324)
        /// </summary>
        [Description("Найм")]
        [EnumCode("")]
        Najm = 10000324,
        /// <summary>
        /// Передача в собственность равнозначное (12029001)
        /// </summary>
        [Description("Передача в собственность равнозначное")]
        [EnumCode("")]
        PeredachaVSobstvennostRavnoznachnoe = 12029001,
        /// <summary>
        /// Найм дети-сироты (12029002)
        /// </summary>
        [Description("Найм дети-сироты")]
        [EnumCode("")]
        NajmDetySiroty = 12029002,
        /// <summary>
        /// Найм краткосрочный (12029003)
        /// </summary>
        [Description("Найм краткосрочный")]
        [EnumCode("")]
        NajmKratkosrochniy = 12029003,
        /// <summary>
        /// обманутые вкладчики (12029004)
        /// </summary>
        [Description("обманутые вкладчики")]
        [EnumCode("")]
        ObmanutieVkladchiki = 12029004,
        /// <summary>
        /// обманутые вкладчики с доплатой (12029005)
        /// </summary>
        [Description("обманутые вкладчики с доплатой")]
        [EnumCode("")]
        ObmanutieVkladchikiSDoplatoi = 12029005,
        /// <summary>
        /// Пользование в общежитии (12029006)
        /// </summary>
        [Description("Пользование в общежитии")]
        [EnumCode("")]
        PolzovanieVObshejitii = 12029006,
        /// <summary>
        /// Переход права на равнозначное (12029007)
        /// </summary>
        [Description("Переход права на равнозначное")]
        [EnumCode("")]
        PerehodPravaNaRavnoznachnoe = 12029007,
        /// <summary>
        /// Переход права на равноценное (12029008)
        /// </summary>
        [Description("Переход права на равноценное")]
        [EnumCode("")]
        PerehodPravaNaRavnocennoe = 12029008,
        /// <summary>
        /// Передача в собственность бесплатно (12029009)
        /// </summary>
        [Description("Передача в собственность бесплатно")]
        [EnumCode("")]
        PeredachaVSobstvennostBesplatno = 12029009,
        /// <summary>
        /// Безвозмездное пользование в специализированном фонде (12029010)
        /// </summary>
        [Description("Безвозмездное пользование в специализированном фонде")]
        [EnumCode("")]
        BezvozmezdnoePolzovanieVSpecializirovannomFonde = 12029010,
        /// <summary>
        /// Безвозмездное пользование в специализированном фонде МК (12029011)
        /// </summary>
        [Description("Безвозмездное пользование в специализированном фонде МК")]
        [EnumCode("")]
        BezvozmezdnoePolzovanieVSpecializirovannomFondeMk = 12029011,
        /// <summary>
        /// Гос жилищный сертификат (12029012)
        /// </summary>
        [Description("Гос жилищный сертификат")]
        [EnumCode("")]
        GosGilishniyCertificat = 12029012,
        /// <summary>
        /// Коммерческий найм в бездотационном доме (12029013)
        /// </summary>
        [Description("Коммерческий найм в бездотационном доме")]
        [EnumCode("")]
        KommercheskiyNajmVBezdotacionnomDome = 12029013,
        /// <summary>
        /// Коммерческий найм молодая семья (12029014)
        /// </summary>
        [Description("Коммерческий найм молодая семья")]
        [EnumCode("")]
        KommercheskiyNajmMolodajaSemja = 12029014,
        /// <summary>
        /// Коммерческий найм молодая семья в бездотационном доме (12029015)
        /// </summary>
        [Description("Коммерческий найм молодая семья в бездотационном доме")]
        [EnumCode("")]
        KommercheskiyNajmMolodajaSemjaVBezdotacionnomDome = 12029015,
        /// <summary>
        /// Купля-продажа выкуп (12029016)
        /// </summary>
        [Description("Купля-продажа выкуп")]
        [EnumCode("")]
        KupljaProdajaVikup = 12029016,
        /// <summary>
        /// Купля-продажа с доплатой (12029017)
        /// </summary>
        [Description("Купля-продажа с доплатой")]
        [EnumCode("")]
        KupljaProdajaSDoplatoi = 12029017,
        /// <summary>
        /// Купля-продажа ипотека молодая семья (12029018)
        /// </summary>
        [Description("Купля-продажа ипотека молодая семья")]
        [EnumCode("")]
        KupljaProdajaIpotekaMolodajaSemja = 12029018,
        /// <summary>
        /// Купля-продажа льготная (12029019)
        /// </summary>
        [Description("Купля-продажа льготная")]
        [EnumCode("")]
        KupljaProdajaLgotnaja = 12029019,
        /// <summary>
        /// Купля-продажа с рассрочкой платежа молодая семья (12029020)
        /// </summary>
        [Description("Купля-продажа с рассрочкой платежа молодая семья")]
        [EnumCode("")]
        KupljaProdajaSRassrochkoiPlatejaMolodajaSemja = 12029020,
        /// <summary>
        /// Купля-продажа рынок (12029021)
        /// </summary>
        [Description("Купля-продажа рынок")]
        [EnumCode("")]
        KupljaProdajaRynok = 12029021,
        /// <summary>
        /// Мена с доплатой (12029022)
        /// </summary>
        [Description("Мена с доплатой")]
        [EnumCode("")]
        MenaSDoplatoi = 12029022,
        /// <summary>
        /// Мена с оплатой разницы в счет в комнаты (12029023)
        /// </summary>
        [Description("Мена с оплатой разницы в счет в комнаты")]
        [EnumCode("")]
        MenaSOplatoiVSchetKomnaty = 12029023,
        /// <summary>
        /// Мена с оплатой разницы в счет в комнаты молодая семья (12029024)
        /// </summary>
        [Description("Мена с оплатой разницы в счет в комнаты молодая семья")]
        [EnumCode("")]
        MenaSOplatoiVSchetKomnatyMolodajaSemja = 12029024,
    }

    /// <summary>
    /// Статус идентификации (12093)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12093)]
    public enum StatusIdentifikacii : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Учтена на ФСП (10000107)
        /// </summary>
        [Description("Учтена на ФСП")]
        [EnumCode("")]
        Identified = 10000107,
        /// <summary>
        /// Подтверждена Банком (10000109)
        /// </summary>
        [Description("Подтверждена Банком")]
        [EnumCode("")]
        PartiallyIdentified = 10000109,
        /// <summary>
        /// Не идентифицирован (10000111)
        /// </summary>
        [Description("Не идентифицирован")]
        [EnumCode("")]
        NotIdentified = 10000111,
        /// <summary>
        /// Ошибочная (12093004)
        /// </summary>
        [Description("Ошибочная")]
        [EnumCode("")]
        NotConfirmedByBank = 12093004,
    }

    /// <summary>
    /// Статус обработки файла (12119)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12119)]
    public enum UFKFileProcessingStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Обработан полностью (10000257)
        /// </summary>
        [Description("Обработан полностью")]
        [EnumCode("")]
        ProcessedCompletely = 10000257,
        /// <summary>
        /// Ошибки импорта (10000259)
        /// </summary>
        [Description("Ошибки импорта")]
        [EnumCode("")]
        ImportError = 10000259,
        /// <summary>
        /// Не обработан (10000261)
        /// </summary>
        [Description("Не обработан")]
        [EnumCode("")]
        NotProcessed = 10000261,
        /// <summary>
        /// Обработан частично (12119006)
        /// </summary>
        [Description("Обработан частично")]
        [EnumCode("")]
        ProcessedPartially = 12119006,
        /// <summary>
        /// Связан с банком (частично) (12119007)
        /// </summary>
        [Description("Связан с банком (частично)")]
        [EnumCode("")]
        LinkedBankPartially = 12119007,
        /// <summary>
        /// Связан с банком (полностью) (12119008)
        /// </summary>
        [Description("Связан с банком (полностью)")]
        [EnumCode("")]
        LinkedBankCompletely = 12119008,
        /// <summary>
        /// Идентификация (12119009)
        /// </summary>
        [Description("Идентификация")]
        [EnumCode("")]
        LinkedBankInProcess = 12119009,
        /// <summary>
        /// Обрабатывается (100008321)
        /// </summary>
        [Description("Обрабатывается")]
        [EnumCode("")]
        InProcess = 100008321,
        /// <summary>
        /// Загружен (100008322)
        /// </summary>
        [Description("Загружен")]
        [EnumCode("")]
        Loaded = 100008322,
    }

    /// <summary>
    /// Код типа файла (12120)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12120)]
    public enum TypeFile : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Банковские файлы оплат (12120001)
        /// </summary>
        [Description("Банковские файлы оплат")]
        [EnumCode("13")]
        BankPayment = 12120001,
        /// <summary>
        /// Реестр полисов (12120002)
        /// </summary>
        [Description("Реестр полисов")]
        [EnumCode("14")]
        Policy = 12120002,
        /// <summary>
        /// Начисления (12120003)
        /// </summary>
        [Description("Начисления")]
        [EnumCode("1")]
        Nach = 12120003,
        /// <summary>
        /// Зачисления (12120004)
        /// </summary>
        [Description("Зачисления")]
        [EnumCode("2")]
        Strah = 12120004,
        /// <summary>
        /// Реестр расторгнутых полисов (12120005)
        /// </summary>
        [Description("Реестр расторгнутых полисов")]
        [EnumCode("4")]
        TerminatedPolicy = 12120005,
        /// <summary>
        /// Реестр свидетельств (12120006)
        /// </summary>
        [Description("Реестр свидетельств")]
        [EnumCode("5")]
        Certificate = 12120006,
        /// <summary>
        /// Реестр страховых выплат по жилым помещениям (12120007)
        /// </summary>
        [Description("Реестр страховых выплат по жилым помещениям")]
        [EnumCode("6")]
        InsurancePayments = 12120007,
        /// <summary>
        /// Реестр сведений об отказах  в страховых выплатах по жилым помещениям (12120008)
        /// </summary>
        [Description("Реестр сведений об отказах  в страховых выплатах по жилым помещениям")]
        [EnumCode("7")]
        InsurancePaymentsRefusal = 12120008,
        /// <summary>
        /// Реестр сведений о заключенных договорах страхования общего имущества собственников помещений (12120009)
        /// </summary>
        [Description("Реестр сведений о заключенных договорах страхования общего имущества собственников помещений")]
        [EnumCode("8")]
        InsuranceContractConcluded = 12120009,
        /// <summary>
        /// Реестр сведений о заключенных дополнительных соглашениях к договорам страхования общего имущества собственников помещений (12120010)
        /// </summary>
        [Description("Реестр сведений о заключенных дополнительных соглашениях к договорам страхования общего имущества собственников помещений")]
        [EnumCode("9")]
        AddInsuranceContractConcluded = 12120010,
        /// <summary>
        /// Реестр сведений о поступивших платежах по договорам страхования общего имущества собственников помещений (12120011)
        /// </summary>
        [Description("Реестр сведений о поступивших платежах по договорам страхования общего имущества собственников помещений")]
        [EnumCode("10")]
        PaymentReceived = 12120011,
        /// <summary>
        /// Cведения о страховых выплатах по договорам страхования общего имущества собственников помещений (12120012)
        /// </summary>
        [Description("Cведения о страховых выплатах по договорам страхования общего имущества собственников помещений")]
        [EnumCode("11")]
        InsurancePaymentsUnderContracts = 12120012,
        /// <summary>
        /// Реестр сведений о заявленных и неурегулированных страховых событиях по договорам общего имущества собственников помещений и об отказах в страховых выплатах по ним (12120013)
        /// </summary>
        [Description("Реестр сведений о заявленных и неурегулированных страховых событиях по договорам общего имущества собственников помещений и об отказах в страховых выплатах по ним")]
        [EnumCode("12")]
        DeclaredUnsettledInsuranceEvents = 12120013,
    }

    /// <summary>
    /// БТИ: Источник данных для страхования (12121)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12121)]
    public enum InsuranceSourceType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// МФЦ (12121001)
        /// </summary>
        [Description("МФЦ")]
        [EnumCode("1")]
        Mfc = 12121001,
        /// <summary>
        /// СК (12121002)
        /// </summary>
        [Description("СК")]
        [EnumCode("2")]
        Sk = 12121002,
        /// <summary>
        /// Банк (12121003)
        /// </summary>
        [Description("Банк")]
        [EnumCode("5")]
        Bank = 12121003,
        /// <summary>
        /// ГБУ (12121004)
        /// </summary>
        [Description("ГБУ")]
        [EnumCode("3")]
        Gbu = 12121004,
        /// <summary>
        /// БТИ (12121005)
        /// </summary>
        [Description("БТИ")]
        [EnumCode("4")]
        Bti = 12121005,
    }

    /// <summary>
    /// Тип договора (12122)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12122)]
    public enum ContractType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Договор страхования жилых помещений (12122001)
        /// </summary>
        [Description("Договор страхования жилых помещений")]
        [EnumCode("1")]
        Dwelling = 12122001,
        /// <summary>
        /// Договор страхования общего имущества (12122002)
        /// </summary>
        [Description("Договор страхования общего имущества")]
        [EnumCode("2")]
        CommonOwnership = 12122002,
    }

    /// <summary>
    /// Тип документа (12123)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12123)]
    public enum DocumentType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Полис (12123001)
        /// </summary>
        [Description("Полис")]
        [EnumCode("1")]
        Polis = 12123001,
        /// <summary>
        /// Свидетельство (12123002)
        /// </summary>
        [Description("Свидетельство")]
        [EnumCode("2")]
        Certificate = 12123002,
        /// <summary>
        /// ЕПД (12123003)
        /// </summary>
        [Description("ЕПД")]
        [EnumCode("3")]
        EPD = 12123003,
    }

    /// <summary>
    /// Типы плит (12124)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12124)]
    public enum StoveType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Газовая (12124001)
        /// </summary>
        [Description("Газовая")]
        [EnumCode("1")]
        GasStove = 12124001,
        /// <summary>
        /// Электрическая (12124002)
        /// </summary>
        [Description("Электрическая")]
        [EnumCode("2")]
        ElectricalSotve = 12124002,
    }

    /// <summary>
    /// Причины ущерба для ЖП (12125)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12125)]
    public enum CausesOfDamageGP : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Иное (12125001)
        /// </summary>
        [Description("Иное")]
        [EnumCode("10")]
        OtherReason = 12125001,
        /// <summary>
        /// Пожар (12125002)
        /// </summary>
        [Description("Пожар")]
        [EnumCode("1")]
        Fire = 12125002,
        /// <summary>
        /// Последствия тушения пожара (12125003)
        /// </summary>
        [Description("Последствия тушения пожара")]
        [EnumCode("2")]
        ConsequencesOfFireExtinguishing = 12125003,
        /// <summary>
        /// Взрыв (12125004)
        /// </summary>
        [Description("Взрыв")]
        [EnumCode("3")]
        Explosion = 12125004,
        /// <summary>
        /// Авария систем водоснабжения (12125005)
        /// </summary>
        [Description("Авария систем водоснабжения")]
        [EnumCode("4")]
        AccidentOfWaterSupplySystems = 12125005,
        /// <summary>
        /// Авария систем отопления (12125006)
        /// </summary>
        [Description("Авария систем отопления")]
        [EnumCode("5")]
        AccidentOfHeatingSystems = 12125006,
        /// <summary>
        /// Авария систем канализации (12125007)
        /// </summary>
        [Description("Авария систем канализации")]
        [EnumCode("6")]
        AccidentOfSewageSystems = 12125007,
        /// <summary>
        /// Авария внутреннего водостока (12125008)
        /// </summary>
        [Description("Авария внутреннего водостока")]
        [EnumCode("7")]
        DownholeAccident = 12125008,
        /// <summary>
        /// Обрушение конструкций (12125009)
        /// </summary>
        [Description("Обрушение конструкций")]
        [EnumCode("8")]
        StructuralCollapse = 12125009,
        /// <summary>
        /// Сильный ветер (12125010)
        /// </summary>
        [Description("Сильный ветер")]
        [EnumCode("9")]
        StrongWind = 12125010,
    }

    /// <summary>
    /// Элементы конструкций (12126)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12126)]
    public enum ElementsOfConstructions : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Стены и перегородки (12126001)
        /// </summary>
        [Description("Стены и перегородки")]
        [EnumCode("1")]
        WallsAndPartitions = 12126001,
        /// <summary>
        /// Перекрытия (12126002)
        /// </summary>
        [Description("Перекрытия")]
        [EnumCode("2")]
        Overlapping = 12126002,
        /// <summary>
        /// Проемы: окна (12126003)
        /// </summary>
        [Description("Проемы: окна")]
        [EnumCode("3")]
        OpeningWindows = 12126003,
        /// <summary>
        /// Проемы: двери (12126004)
        /// </summary>
        [Description("Проемы: двери")]
        [EnumCode("4")]
        OpeningDoors = 12126004,
        /// <summary>
        /// Полы (12126005)
        /// </summary>
        [Description("Полы")]
        [EnumCode("5")]
        Floors = 12126005,
        /// <summary>
        /// Отделочные работы, в т.ч. (12126006)
        /// </summary>
        [Description("Отделочные работы, в т.ч.")]
        [EnumCode("6")]
        FinishingWorkIncl = 12126006,
        /// <summary>
        /// Окраска (12126007)
        /// </summary>
        [Description("Окраска")]
        [EnumCode("7")]
        FinishingWorkPainting = 12126007,
        /// <summary>
        /// Обои (12126008)
        /// </summary>
        [Description("Обои")]
        [EnumCode("8")]
        FinishingWorkWallpaper = 12126008,
        /// <summary>
        /// Облицовка керамической плиткой (12126009)
        /// </summary>
        [Description("Облицовка керамической плиткой")]
        [EnumCode("9")]
        FinishingWorkCeramic = 12126009,
        /// <summary>
        /// Центральное отопление (12126010)
        /// </summary>
        [Description("Центральное отопление")]
        [EnumCode("10")]
        CentralHeating = 12126010,
        /// <summary>
        /// Водопровод, канализация (12126011)
        /// </summary>
        [Description("Водопровод, канализация")]
        [EnumCode("11")]
        WaterSupplyAndSewerage = 12126011,
        /// <summary>
        /// Горячее водоснабжение (12126012)
        /// </summary>
        [Description("Горячее водоснабжение")]
        [EnumCode("12")]
        HotWaterSupply = 12126012,
        /// <summary>
        /// Электромонтажные работы (12126013)
        /// </summary>
        [Description("Электромонтажные работы")]
        [EnumCode("13")]
        ElectricInstallationWork = 12126013,
        /// <summary>
        /// Газоснабжение (12126014)
        /// </summary>
        [Description("Газоснабжение")]
        [EnumCode("14")]
        GasSupply = 12126014,
        /// <summary>
        /// Радио в т.ч. (12126015)
        /// </summary>
        [Description("Радио в т.ч.")]
        [EnumCode("15")]
        RadioIncl = 12126015,
        /// <summary>
        /// Провода (12126016)
        /// </summary>
        [Description("Провода")]
        [EnumCode("16")]
        RadioWires = 12126016,
        /// <summary>
        /// Вводное устройство (12126017)
        /// </summary>
        [Description("Вводное устройство")]
        [EnumCode("17")]
        RadioInputDevice = 12126017,
        /// <summary>
        /// Аппаратура (12126018)
        /// </summary>
        [Description("Аппаратура")]
        [EnumCode("18")]
        RadioEquipment = 12126018,
        /// <summary>
        /// Телевидение в т.ч. (12126019)
        /// </summary>
        [Description("Телевидение в т.ч.")]
        [EnumCode("19")]
        TelevisionIncl = 12126019,
        /// <summary>
        /// Провода (12126020)
        /// </summary>
        [Description("Провода")]
        [EnumCode("20")]
        TelevisionWires = 12126020,
        /// <summary>
        /// Вводное устройство (12126021)
        /// </summary>
        [Description("Вводное устройство")]
        [EnumCode("21")]
        TelevisionInputDevice = 12126021,
        /// <summary>
        /// Телефон в т.ч. (12126022)
        /// </summary>
        [Description("Телефон в т.ч.")]
        [EnumCode("22")]
        TelephoneIncl = 12126022,
        /// <summary>
        /// Провода (12126023)
        /// </summary>
        [Description("Провода")]
        [EnumCode("23")]
        TelephoneWires = 12126023,
        /// <summary>
        /// Вводное устройство (12126024)
        /// </summary>
        [Description("Вводное устройство")]
        [EnumCode("24")]
        TelephoneInputDevice = 12126024,
        /// <summary>
        /// Аппаратура (12126025)
        /// </summary>
        [Description("Аппаратура")]
        [EnumCode("25")]
        TelephoneEquipment = 12126025,
        /// <summary>
        /// Прочие (12126026)
        /// </summary>
        [Description("Прочие")]
        [EnumCode("26")]
        Other = 12126026,
    }

    /// <summary>
    /// Материал пола (12127)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12127)]
    public enum FloorMaterial : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Полы дощатые (12127001)
        /// </summary>
        [Description("Полы дощатые")]
        [EnumCode("1")]
        WoodFlooring = 12127001,
        /// <summary>
        /// Полы из ламината (12127002)
        /// </summary>
        [Description("Полы из ламината")]
        [EnumCode("2")]
        LaminateFlooring = 12127002,
        /// <summary>
        /// Полы из паркета (12127003)
        /// </summary>
        [Description("Полы из паркета")]
        [EnumCode("3")]
        ParquetFlooring = 12127003,
        /// <summary>
        /// Полы из рулонных материалов (12127004)
        /// </summary>
        [Description("Полы из рулонных материалов")]
        [EnumCode("4")]
        RollMaterialsFlooring = 12127004,
    }

    /// <summary>
    /// Причины отказа в страховой выплате по договору страхования жилого помещения (12128)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12128)]
    public enum ReasonsRefusalInsurancePaymentLivingPremise : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Причина, отличная от имеющих коды 1-12 (12128001)
        /// </summary>
        [Description("Причина, отличная от имеющих коды 1-12")]
        [EnumCode("0")]
        OtherReason = 12128001,
        /// <summary>
        /// Повреждение жилого помещения произошло в результате события, которое не предусмотрено договором страхования (не страховой случай) (12128002)
        /// </summary>
        [Description("Повреждение жилого помещения произошло в результате события, которое не предусмотрено договором страхования (не страховой случай)")]
        [EnumCode("1")]
        NoInsureCase = 12128002,
        /// <summary>
        /// Страховое событие произошло в неоплаченный период договора страхования (12128003)
        /// </summary>
        [Description("Страховое событие произошло в неоплаченный период договора страхования")]
        [EnumCode("2")]
        NoPayPeriod = 12128003,
        /// <summary>
        /// Ущерб возмещен полностью по иному договору страхования или за счет средств виновной стороны (12128004)
        /// </summary>
        [Description("Ущерб возмещен полностью по иному договору страхования или за счет средств виновной стороны")]
        [EnumCode("3")]
        PatGuiltySide = 12128004,
        /// <summary>
        /// Повреждения, связанные с предыдущими страховыми случаями (событиями), не устранены страхователем до наступления последующего страхового случая (нет увеличения ущерба по ранее поврежденным элементам конструкций) (12128005)
        /// </summary>
        [Description("Повреждения, связанные с предыдущими страховыми случаями (событиями), не устранены страхователем до наступления последующего страхового случая (нет увеличения ущерба по ранее поврежденным элементам конструкций)")]
        [EnumCode("4")]
        DamageBeforeCase = 12128005,
        /// <summary>
        /// Ремонт произведен до осмотра квартиры представителем страховой организации (12128006)
        /// </summary>
        [Description("Ремонт произведен до осмотра квартиры представителем страховой организации")]
        [EnumCode("5")]
        RepairBefore = 12128006,
        /// <summary>
        /// Страхователь не является гражданином России или является лицом без гражданства (12128007)
        /// </summary>
        [Description("Страхователь не является гражданином России или является лицом без гражданства")]
        [EnumCode("6")]
        PolicyholderNotRF = 12128007,
        /// <summary>
        /// Страхователь, являясь собственником жилого помещения, не зарегистрирован в нем (12128008)
        /// </summary>
        [Description("Страхователь, являясь собственником жилого помещения, не зарегистрирован в нем")]
        [EnumCode("7")]
        PolicyholderNotRegister = 12128008,
        /// <summary>
        /// Жилое помещение до наступления страхового случая (события) признано в установленном порядке аварийным (12128009)
        /// </summary>
        [Description("Жилое помещение до наступления страхового случая (события) признано в установленном порядке аварийным")]
        [EnumCode("8")]
        LivingPremiseEmergency = 12128009,
        /// <summary>
        /// Жилое помещение расположено в доме, включенном Правительством Москвы в ежегодный перечень адресов жилых домов, подлежащих освобождению в связи со сносом, реконструкцией, переоборудованием в нежилые, изъятием земельного участка или по другим основаниям (12128010)
        /// </summary>
        [Description("Жилое помещение расположено в доме, включенном Правительством Москвы в ежегодный перечень адресов жилых домов, подлежащих освобождению в связи со сносом, реконструкцией, переоборудованием в нежилые, изъятием земельного участка или по другим основаниям")]
        [EnumCode("9")]
        LivingPremiseMustDismissal = 12128010,
        /// <summary>
        /// На жилое помещение обращено взыскание по обязательствам (12128011)
        /// </summary>
        [Description("На жилое помещение обращено взыскание по обязательствам")]
        [EnumCode("10")]
        LivingPremiseMustPenalty = 12128011,
        /// <summary>
        /// Жилое помещение подлежит конфискации (12128012)
        /// </summary>
        [Description("Жилое помещение подлежит конфискации")]
        [EnumCode("11")]
        LivingPremiseMustConfiscation = 12128012,
        /// <summary>
        /// Прекращение права найма или права собственности на жилое помещение (12128013)
        /// </summary>
        [Description("Прекращение права найма или права собственности на жилое помещение")]
        [EnumCode("12")]
        LivingPremiseRightTermination = 12128013,
    }

    /// <summary>
    /// Причины отказа в страховой выплате по договору страхования общего имущества (12129)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12129)]
    public enum ReasonsRefusalInsurancePaymentCommonProperty : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Причина, отличная от имеющих коды 1-11 (12129001)
        /// </summary>
        [Description("Причина, отличная от имеющих коды 1-11")]
        [EnumCode("0")]
        OtherReason = 12129001,
        /// <summary>
        /// Повреждение объекта общего имущества произошло в результате события, которое не предусмотрено договором страхования (не страховой случай) (12129002)
        /// </summary>
        [Description("Повреждение объекта общего имущества произошло в результате события, которое не предусмотрено договором страхования (не страховой случай)")]
        [EnumCode("1")]
        NoInsureCase = 12129002,
        /// <summary>
        /// Страховое событие произошло в неоплаченный период договора страхования (12129003)
        /// </summary>
        [Description("Страховое событие произошло в неоплаченный период договора страхования")]
        [EnumCode("2")]
        NoPayPeriod = 12129003,
        /// <summary>
        /// Ущерб возмещен полностью по иному договору страхования или за счет средств виновной стороны (12129004)
        /// </summary>
        [Description("Ущерб возмещен полностью по иному договору страхования или за счет средств виновной стороны")]
        [EnumCode("3")]
        PatGuiltySide = 12129004,
        /// <summary>
        /// Повреждения, связанные с предыдущими страховыми случаями (событиями), не устранены страхователем до наступления последующего страхового случая (нет увеличения ущерба по ранее поврежденным объектам общего имущества) (12129005)
        /// </summary>
        [Description("Повреждения, связанные с предыдущими страховыми случаями (событиями), не устранены страхователем до наступления последующего страхового случая (нет увеличения ущерба по ранее поврежденным объектам общего имущества)")]
        [EnumCode("4")]
        DamageBeforeCase = 12129005,
        /// <summary>
        /// Ремонт поврежденного объекта общего имущества произведен до его осмотра представителем страховой организации (12129006)
        /// </summary>
        [Description("Ремонт поврежденного объекта общего имущества произведен до его осмотра представителем страховой организации")]
        [EnumCode("5")]
        RepairBefore = 12129006,
        /// <summary>
        /// Многоквартирный дом до наступления страхового случая (события) признан в установленном порядке аварийным или включен в ежегодный перечень адресов жилых домов, подлежащих освобождению в связи со сносом, реконструкцией, переоборудованием в нежилое строение, изъятием земельного участка или по другим основаниям (12129007)
        /// </summary>
        [Description("Многоквартирный дом до наступления страхового случая (события) признан в установленном порядке аварийным или включен в ежегодный перечень адресов жилых домов, подлежащих освобождению в связи со сносом, реконструкцией, переоборудованием в нежилое строение, изъятием земельного участка или по другим основаниям")]
        [EnumCode("6")]
        CommonPropertyMustDismissal = 12129007,
        /// <summary>
        /// Повреждение объектов общего имущества произошло вследствие умысла страхователя (12129008)
        /// </summary>
        [Description("Повреждение объектов общего имущества произошло вследствие умысла страхователя")]
        [EnumCode("7")]
        PolicyholderDamage = 12129008,
        /// <summary>
        /// Невыполнение страхователем правил пожарной безопасности, правил и норм технической эксплуатации жилищного фонда (отсутствует документальное подтверждение выполнения регламентных работ) (12129009)
        /// </summary>
        [Description("Невыполнение страхователем правил пожарной безопасности, правил и норм технической эксплуатации жилищного фонда (отсутствует документальное подтверждение выполнения регламентных работ)")]
        [EnumCode("8")]
        PolicyholderIgnoreFireRules = 12129009,
        /// <summary>
        /// Невыполнение страхователем в установленный срок требований (предписаний) в отношении состояния застрахованного общего имущества, выданных соответствующими органами надзора (12129010)
        /// </summary>
        [Description("Невыполнение страхователем в установленный срок требований (предписаний) в отношении состояния застрахованного общего имущества, выданных соответствующими органами надзора")]
        [EnumCode("9")]
        PolicyholderIgnoreDemands = 12129010,
        /// <summary>
        /// Размер причиненного ущерба меньше размера франшизы (12129011)
        /// </summary>
        [Description("Размер причиненного ущерба меньше размера франшизы")]
        [EnumCode("10")]
        AmountDamageLessThanDeductible = 12129011,
        /// <summary>
        /// В договоре страхования исчерпана страховая сумма по категории общего имущества, к которой относится застрахованный объект (12129012)
        /// </summary>
        [Description("В договоре страхования исчерпана страховая сумма по категории общего имущества, к которой относится застрахованный объект")]
        [EnumCode("11")]
        ExhaustedSumInsured = 12129012,
    }

    /// <summary>
    /// Причины отсутствия решения о страховой выплате по договору страхования общего имущества (12130)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12130)]
    public enum ReasonsAbsenceDecision : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Отсутствие заявления страхователя (12130001)
        /// </summary>
        [Description("Отсутствие заявления страхователя")]
        [EnumCode("0")]
        AbsenceStatementInsured = 12130001,
        /// <summary>
        /// Сбор документов страхователем (12130002)
        /// </summary>
        [Description("Сбор документов страхователем")]
        [EnumCode("1")]
        CollectionDocumentsInsured = 12130002,
        /// <summary>
        /// Сбор документов страховой организацией (12130003)
        /// </summary>
        [Description("Сбор документов страховой организацией")]
        [EnumCode("2")]
        CollectionDocumentsInsuranceOrganization = 12130003,
        /// <summary>
        /// Документов, представленных страхователем и собранных страховой организацией, недостаточно для принятия решения о выплате (12130004)
        /// </summary>
        [Description("Документов, представленных страхователем и собранных страховой организацией, недостаточно для принятия решения о выплате")]
        [EnumCode("3")]
        DocumentsNotEnoughDecision = 12130004,
        /// <summary>
        /// Отказ страхователя от страхового возмещения (12130005)
        /// </summary>
        [Description("Отказ страхователя от страхового возмещения")]
        [EnumCode("4")]
        RefusalPolicyholderCompensation = 12130005,
        /// <summary>
        /// Иная причина (12130006)
        /// </summary>
        [Description("Иная причина")]
        [EnumCode("5")]
        OtherReason = 12130006,
    }

    /// <summary>
    /// Тип жилого помещения (12131)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12131)]
    public enum LivingPremiseType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Отдельная квартира (12131002)
        /// </summary>
        [Description("Отдельная квартира")]
        [EnumCode("1")]
        SingleApartment = 12131002,
        /// <summary>
        /// Коммунальная квартира (12131003)
        /// </summary>
        [Description("Коммунальная квартира")]
        [EnumCode("2")]
        CommunalApartmentRoom = 12131003,
        /// <summary>
        /// Отдельная квартира в долевой собственности (12131004)
        /// </summary>
        [Description("Отдельная квартира в долевой собственности")]
        [EnumCode("3")]
        SingleApartmentSharedOwnership = 12131004,
    }

    /// <summary>
    /// Типы зданий (12132)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12132)]
    public enum BuildingType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Здания полносборные из ж.б. панелей и/или блоков (до 4-х этажей включительно) (12132001)
        /// </summary>
        [Description("Здания полносборные из ж.б. панелей и/или блоков (до 4-х этажей включительно)")]
        [EnumCode("1")]
        FullAssembledBuildingsFourFloors = 12132001,
        /// <summary>
        /// Здания полносборные из ж.б. панелей и/или блоков (5-13 этажей включительно) (12132002)
        /// </summary>
        [Description("Здания полносборные из ж.б. панелей и/или блоков (5-13 этажей включительно)")]
        [EnumCode("2")]
        FullAssembledBuildingsThirteenFloors = 12132002,
        /// <summary>
        /// Здания полносборные из ж.б. панелей и/или блоков (14 этажей и выше) (12132003)
        /// </summary>
        [Description("Здания полносборные из ж.б. панелей и/или блоков (14 этажей и выше)")]
        [EnumCode("3")]
        FullAssembledBuildingsMoreFourteenFloors = 12132003,
        /// <summary>
        /// Здания полносборные из ж.б. панелей и/или блоков (произвольной этажности) (12132004)
        /// </summary>
        [Description("Здания полносборные из ж.б. панелей и/или блоков (произвольной этажности)")]
        [EnumCode("4")]
        FullAssembledBuildingsFreeFloors = 12132004,
        /// <summary>
        /// Здания кирпичные (до 4-х этажей включительно) c центральным отоплением и горячим водоснабжением от АГВ (12132005)
        /// </summary>
        [Description("Здания кирпичные (до 4-х этажей включительно) c центральным отоплением и горячим водоснабжением от АГВ")]
        [EnumCode("5")]
        BrickBuildingsFourFloorsCentralHeatingHotWaterAGV = 12132005,
        /// <summary>
        /// Здания кирпичные (до 4-х этажей включительно) c центральным отоплением и горячим водоснабжением (12132006)
        /// </summary>
        [Description("Здания кирпичные (до 4-х этажей включительно) c центральным отоплением и горячим водоснабжением")]
        [EnumCode("6")]
        BrickBuildingsFourFloorsCentralHeatingHotWater = 12132006,
        /// <summary>
        /// Здания кирпичные (5-13 этажей включительно) (12132007)
        /// </summary>
        [Description("Здания кирпичные (5-13 этажей включительно)")]
        [EnumCode("7")]
        BrickBuildingsThirteenFloors = 12132007,
        /// <summary>
        /// Здания кирпичные (14 этажей и выше) (12132008)
        /// </summary>
        [Description("Здания кирпичные (14 этажей и выше)")]
        [EnumCode("8")]
        BrickBuildingsMoreFourteenFloors = 12132008,
        /// <summary>
        /// Здания кирпичные (произвольной этажности) c железобетонными перекрытиями (12132009)
        /// </summary>
        [Description("Здания кирпичные (произвольной этажности) c железобетонными перекрытиями")]
        [EnumCode("9")]
        BrickBuildingsFreeFloorsReinforcedConcreteSlabs = 12132009,
        /// <summary>
        /// Здания кирпичные (произвольной этажности) c деревянными перекрытиями (12132010)
        /// </summary>
        [Description("Здания кирпичные (произвольной этажности) c деревянными перекрытиями")]
        [EnumCode("10")]
        BrickBuildingsFreeFloorsWoodenFloors = 12132010,
        /// <summary>
        /// Здания из облегчённых блоков (до 5-ти этажей включительно) (12132011)
        /// </summary>
        [Description("Здания из облегчённых блоков (до 5-ти этажей включительно)")]
        [EnumCode("11")]
        LightBlockBuildingsFiveFloors = 12132011,
        /// <summary>
        /// Здания из облегчённых блоков (произвольной этажности c ж.б. перекрытиями) (12132012)
        /// </summary>
        [Description("Здания из облегчённых блоков (произвольной этажности c ж.б. перекрытиями)")]
        [EnumCode("12")]
        LightBlockBuildingsFreeFloorsReinforcedConcreteSlabs = 12132012,
        /// <summary>
        /// Здания из облегчённых блоков (произвольной этажности c дер/перекрытиями) (12132013)
        /// </summary>
        [Description("Здания из облегчённых блоков (произвольной этажности c дер/перекрытиями)")]
        [EnumCode("13")]
        LightBlockBuildingsFreeFloorsWoodenFloors = 12132013,
        /// <summary>
        /// Здания смешанные (до 3-х этажей включительно) (12132014)
        /// </summary>
        [Description("Здания смешанные (до 3-х этажей включительно)")]
        [EnumCode("14")]
        MixBuildingsThreeFloors = 12132014,
        /// <summary>
        /// Здания смешанные (произвольной этажности) (12132015)
        /// </summary>
        [Description("Здания смешанные (произвольной этажности)")]
        [EnumCode("15")]
        MixBuildingsFreeFloors = 12132015,
        /// <summary>
        /// Здания брусчатые или бревенчатые (до 3-х этажей включительно) (12132016)
        /// </summary>
        [Description("Здания брусчатые или бревенчатые (до 3-х этажей включительно)")]
        [EnumCode("16")]
        PavedLogBuildingsThreeFloors = 12132016,
        /// <summary>
        /// Здания из монолитного бетона (до 4-х этажей включительно) (12132017)
        /// </summary>
        [Description("Здания из монолитного бетона (до 4-х этажей включительно)")]
        [EnumCode("17")]
        MonolithicConcreteBuildingsFourFloors = 12132017,
        /// <summary>
        /// Здания из монолитного бетона (5-13 этажей включительно) (12132018)
        /// </summary>
        [Description("Здания из монолитного бетона (5-13 этажей включительно)")]
        [EnumCode("18")]
        MonolithicConcreteBuildingsThirteenFloors = 12132018,
        /// <summary>
        /// Здания из монолитного бетона  (14 этажей и выше) (12132019)
        /// </summary>
        [Description("Здания из монолитного бетона  (14 этажей и выше)")]
        [EnumCode("19")]
        MonolithicConcreteBuildingsMoreFourteenFloors = 12132019,
        /// <summary>
        /// Здания из монолитного бетона (произвольной этажности) (12132020)
        /// </summary>
        [Description("Здания из монолитного бетона (произвольной этажности)")]
        [EnumCode("20")]
        MonolithicConcreteBuildingsFreeFloors = 12132020,
    }

    /// <summary>
    /// Тип собственности (12133)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12133)]
    public enum TypeProperty : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Частная (12133001)
        /// </summary>
        [Description("Частная")]
        [EnumCode("")]
        PrivateProperty = 12133001,
        /// <summary>
        /// Общая (12133002)
        /// </summary>
        [Description("Общая")]
        [EnumCode("")]
        CommonProperty = 12133002,
    }

    /// <summary>
    /// Подпричины ущерба по ЖП (12134)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12134)]
    public enum SubReasonCausesOfDamage : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Прочее (12134001)
        /// </summary>
        [Description("Прочее")]
        [EnumCode("1")]
        OtherSubReason = 12134001,
        /// <summary>
        /// Неосторожное обращение с огнем (12134002)
        /// </summary>
        [Description("Неосторожное обращение с огнем")]
        [EnumCode("2")]
        CarelessHandlingOfFire = 12134002,
        /// <summary>
        /// Неисправность электропроводки (12134003)
        /// </summary>
        [Description("Неисправность электропроводки")]
        [EnumCode("3")]
        WiringFault = 12134003,
        /// <summary>
        /// Пожар вне дома (12134004)
        /// </summary>
        [Description("Пожар вне дома")]
        [EnumCode("4")]
        FireOutsideHome = 12134004,
        /// <summary>
        /// Подводка газа (12134005)
        /// </summary>
        [Description("Подводка газа")]
        [EnumCode("5")]
        GasSupply = 12134005,
        /// <summary>
        /// Газовые приборы (12134006)
        /// </summary>
        [Description("Газовые приборы")]
        [EnumCode("6")]
        GasAppliances = 12134006,
        /// <summary>
        /// Горячее водоснабжение (12134007)
        /// </summary>
        [Description("Горячее водоснабжение")]
        [EnumCode("7")]
        HotWaterSupply = 12134007,
        /// <summary>
        /// Холодное водоснабжение (12134008)
        /// </summary>
        [Description("Холодное водоснабжение")]
        [EnumCode("8")]
        ColdWaterSupply = 12134008,
        /// <summary>
        /// Смесители (12134009)
        /// </summary>
        [Description("Смесители")]
        [EnumCode("9")]
        Mixers = 12134009,
        /// <summary>
        /// Стояк (12134010)
        /// </summary>
        [Description("Стояк")]
        [EnumCode("10")]
        Riser = 12134010,
        /// <summary>
        /// Запорная арматура (12134011)
        /// </summary>
        [Description("Запорная арматура")]
        [EnumCode("11")]
        Valves = 12134011,
        /// <summary>
        /// Труба (12134012)
        /// </summary>
        [Description("Труба")]
        [EnumCode("12")]
        Pipe = 12134012,
        /// <summary>
        /// Радиатор (12134013)
        /// </summary>
        [Description("Радиатор")]
        [EnumCode("13")]
        Radiator = 12134013,
        /// <summary>
        /// Расширительный бак (12134014)
        /// </summary>
        [Description("Расширительный бак")]
        [EnumCode("14")]
        ExpansionTank = 12134014,
        /// <summary>
        /// Отопительные приборы (12134015)
        /// </summary>
        [Description("Отопительные приборы")]
        [EnumCode("15")]
        HeatingAppliances = 12134015,
        /// <summary>
        /// Лежак (12134016)
        /// </summary>
        [Description("Лежак")]
        [EnumCode("16")]
        Legak = 12134016,
        /// <summary>
        /// Приборы (12134017)
        /// </summary>
        [Description("Приборы")]
        [EnumCode("17")]
        Devices = 12134017,
        /// <summary>
        /// Примыкание воронки к кровле (12134018)
        /// </summary>
        [Description("Примыкание воронки к кровле")]
        [EnumCode("18")]
        FunneJunctionRoof = 12134018,
        /// <summary>
        /// Расчеканка раструба (12134019)
        /// </summary>
        [Description("Расчеканка раструба")]
        [EnumCode("19")]
        FlareBell = 12134019,
        /// <summary>
        /// Засор внутреннего водостока (12134020)
        /// </summary>
        [Description("Засор внутреннего водостока")]
        [EnumCode("20")]
        InternalDrainageBlockage = 12134020,
    }

    /// <summary>
    /// Уточнение подпричин ущерба по ЖП (12135)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12135)]
    public enum RefinementSubReasonCOD : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Прочее (12135001)
        /// </summary>
        [Description("Прочее")]
        [EnumCode("1")]
        OtherRefinement = 12135001,
        /// <summary>
        /// Стояк (12135002)
        /// </summary>
        [Description("Стояк")]
        [EnumCode("2")]
        RiserRefinement = 12135002,
        /// <summary>
        /// Запорная арматура (12135003)
        /// </summary>
        [Description("Запорная арматура")]
        [EnumCode("3")]
        ValvesRefinement = 12135003,
        /// <summary>
        /// Труба (12135004)
        /// </summary>
        [Description("Труба")]
        [EnumCode("4")]
        PipeRefinement = 12135004,
        /// <summary>
        /// Входной шаровой кран (12135005)
        /// </summary>
        [Description("Входной шаровой кран")]
        [EnumCode("5")]
        BallValveRefinement = 12135005,
        /// <summary>
        /// Гибкая подводка (12135006)
        /// </summary>
        [Description("Гибкая подводка")]
        [EnumCode("6")]
        FlexibleEyelinerRefinement = 12135006,
        /// <summary>
        /// Фильтр (12135007)
        /// </summary>
        [Description("Фильтр")]
        [EnumCode("7")]
        FilterRefinement = 12135007,
        /// <summary>
        /// Полотенцесушитель (12135008)
        /// </summary>
        [Description("Полотенцесушитель")]
        [EnumCode("8")]
        HeatedTowelRailRefinement = 12135008,
    }

    /// <summary>
    /// Причины ущерба для ОИ (12136)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12136)]
    public enum CausesOfDamageOI : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Иное (12136001)
        /// </summary>
        [Description("Иное")]
        [EnumCode("10")]
        OtherReason = 12136001,
        /// <summary>
        /// Пожар (12136002)
        /// </summary>
        [Description("Пожар")]
        [EnumCode("1")]
        Fire = 12136002,
        /// <summary>
        /// Последствия тушения пожара (12136003)
        /// </summary>
        [Description("Последствия тушения пожара")]
        [EnumCode("2")]
        ConsequencesOfFireExtinguishing = 12136003,
        /// <summary>
        /// Взрыв (12136004)
        /// </summary>
        [Description("Взрыв")]
        [EnumCode("3")]
        Explosion = 12136004,
        /// <summary>
        /// Авария систем водоснабжения (12136005)
        /// </summary>
        [Description("Авария систем водоснабжения")]
        [EnumCode("4")]
        AccidentOfWaterSupplySystems = 12136005,
        /// <summary>
        /// Авария систем отопления (12136006)
        /// </summary>
        [Description("Авария систем отопления")]
        [EnumCode("5")]
        AccidentOfHeatingSystems = 12136006,
        /// <summary>
        /// Авария систем канализации (12136007)
        /// </summary>
        [Description("Авария систем канализации")]
        [EnumCode("6")]
        AccidentOfSewageSystems = 12136007,
        /// <summary>
        /// Авария внутреннего водостока (12136008)
        /// </summary>
        [Description("Авария внутреннего водостока")]
        [EnumCode("7")]
        DownholeAccident = 12136008,
        /// <summary>
        /// Сильный ветер (12136009)
        /// </summary>
        [Description("Сильный ветер")]
        [EnumCode("8")]
        StrongWind = 12136009,
        /// <summary>
        /// Противоправные действия третьих лиц (12136010)
        /// </summary>
        [Description("Противоправные действия третьих лиц")]
        [EnumCode("9")]
        IllegalActions = 12136010,
    }

    /// <summary>
    /// Подписанты (12141)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12141)]
    public enum Podpisant : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Справочник «Тип субъекта» (12142)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12142)]
    public enum SubjectType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Физическое лицо (12142001)
        /// </summary>
        [Description("Физическое лицо")]
        [EnumCode("")]
        Individual = 12142001,
        /// <summary>
        /// Управляющая компания (12142002)
        /// </summary>
        [Description("Управляющая компания")]
        [EnumCode("")]
        ManagementCompany = 12142002,
        /// <summary>
        /// Юридическое лицо (12142003)
        /// </summary>
        [Description("Юридическое лицо")]
        [EnumCode("")]
        LegalEntity = 12142003,
    }

    /// <summary>
    /// Тип конструкции строения (12143)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12143)]
    public enum TypeBuildingStructure : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Здания полносборные из железобетонных панелей или блоков (12143001)
        /// </summary>
        [Description("Здания полносборные из железобетонных панелей или блоков")]
        [EnumCode("")]
        PrefabricatedReinforcedPanelsBlocks = 12143001,
        /// <summary>
        /// Здания кирпичные (12143002)
        /// </summary>
        [Description("Здания кирпичные")]
        [EnumCode("")]
        Brick = 12143002,
        /// <summary>
        /// Здания из облегчённых блоков (12143003)
        /// </summary>
        [Description("Здания из облегчённых блоков")]
        [EnumCode("")]
        Lightweight = 12143003,
        /// <summary>
        /// Здания смешанные (кирпичные, деревянные) (12143004)
        /// </summary>
        [Description("Здания смешанные (кирпичные, деревянные)")]
        [EnumCode("")]
        Mixed = 12143004,
        /// <summary>
        /// Здания брусчатые или бревенчатые (12143005)
        /// </summary>
        [Description("Здания брусчатые или бревенчатые")]
        [EnumCode("")]
        BlockTimbered = 12143005,
        /// <summary>
        /// Здания из монолитного железобетона (12143006)
        /// </summary>
        [Description("Здания из монолитного железобетона")]
        [EnumCode("")]
        MonolithicReinforced = 12143006,
    }

    /// <summary>
    /// Этажность строения (12144)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12144)]
    public enum TypeFloors : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Здания до 3-х этажей включительно (12144001)
        /// </summary>
        [Description("Здания до 3-х этажей включительно")]
        [EnumCode("")]
        BuildingsTthreeFloors = 12144001,
        /// <summary>
        /// Здания до 4-х этажей включительно (12144002)
        /// </summary>
        [Description("Здания до 4-х этажей включительно")]
        [EnumCode("")]
        BuildingsFourFloors = 12144002,
        /// <summary>
        /// Здания до 4-х этажей включительно с центральным отоплением и горячим водоснабжением от АГВ (12144003)
        /// </summary>
        [Description("Здания до 4-х этажей включительно с центральным отоплением и горячим водоснабжением от АГВ")]
        [EnumCode("")]
        BuildingsFourFloorsCentralHeatingAGW = 12144003,
        /// <summary>
        /// Здания до 4-х этажей включительно с центральным отоплением и горячим водоснабжением (12144004)
        /// </summary>
        [Description("Здания до 4-х этажей включительно с центральным отоплением и горячим водоснабжением")]
        [EnumCode("")]
        BuildingsFourFloorsCentralHeating = 12144004,
        /// <summary>
        /// Здания до 5-и этажей включительно (12144005)
        /// </summary>
        [Description("Здания до 5-и этажей включительно")]
        [EnumCode("")]
        BuildingsFiveFloors = 12144005,
        /// <summary>
        /// Здания  5 – 13 этажей (12144006)
        /// </summary>
        [Description("Здания  5 – 13 этажей")]
        [EnumCode("")]
        BuildingsThirteenFloors = 12144006,
        /// <summary>
        /// Здания 14 этажей и выше (12144007)
        /// </summary>
        [Description("Здания 14 этажей и выше")]
        [EnumCode("")]
        BuildingsFourteenFloors = 12144007,
        /// <summary>
        /// Здания произвольной (в т.ч. переменной) этажности (12144008)
        /// </summary>
        [Description("Здания произвольной (в т.ч. переменной) этажности")]
        [EnumCode("")]
        BuildingsVariable = 12144008,
        /// <summary>
        /// Здания произвольной (в т.ч. переменной) этажности с ж.б. перекрытиями (12144009)
        /// </summary>
        [Description("Здания произвольной (в т.ч. переменной) этажности с ж.б. перекрытиями")]
        [EnumCode("")]
        BuildingsVariableReinforced = 12144009,
        /// <summary>
        /// Здания произвольной (в т.ч. переменной) этажности с деревянными перекрытиями и перегородками (12144010)
        /// </summary>
        [Description("Здания произвольной (в т.ч. переменной) этажности с деревянными перекрытиями и перегородками")]
        [EnumCode("")]
        BuildingsVariableWood = 12144010,
    }

    /// <summary>
    /// БТИ: ОКТМО (12157)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12157)]
    public enum Oktmo : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Алексеевский (10001125)
        /// </summary>
        [Description("Алексеевский")]
        [EnumCode("45349000")]
        Alekseevski = 10001125,
        /// <summary>
        /// Алтуфьевский (10001126)
        /// </summary>
        [Description("Алтуфьевский")]
        [EnumCode("45350000")]
        Altufievski = 10001126,
        /// <summary>
        /// Академический (10001182)
        /// </summary>
        [Description("Академический")]
        [EnumCode("45397000")]
        Akademicheski = 10001182,
    }

    /// <summary>
    /// Статус загрузки начисления/зачисления (12158)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12158)]
    public enum LoadStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Загружен (12158001)
        /// </summary>
        [Description("Загружен")]
        [EnumCode("1")]
        Loaded = 12158001,
        /// <summary>
        /// Обработан (12158002)
        /// </summary>
        [Description("Обработан")]
        [EnumCode("2")]
        Processed = 12158002,
    }

    /// <summary>
    /// Статус дел на оплату (12159)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12159)]
    public enum PaymentCaseStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Сформирован (12159001)
        /// </summary>
        [Description("Сформирован")]
        [EnumCode("1")]
        Formed = 12159001,
        /// <summary>
        /// Выгружен (12159002)
        /// </summary>
        [Description("Выгружен")]
        [EnumCode("2")]
        Unloaded = 12159002,
        /// <summary>
        /// Передан на согласование (12159003)
        /// </summary>
        [Description("Передан на согласование")]
        [EnumCode("3")]
        SubmittedApproval = 12159003,
        /// <summary>
        /// Согласован (12159004)
        /// </summary>
        [Description("Согласован")]
        [EnumCode("4")]
        Agreed = 12159004,
        /// <summary>
        /// Не согласован (12159005)
        /// </summary>
        [Description("Не согласован")]
        [EnumCode("5")]
        NotAgreed = 12159005,
        /// <summary>
        /// Повторно выгружен (12159006)
        /// </summary>
        [Description("Повторно выгружен")]
        [EnumCode("6")]
        Reloaded = 12159006,
    }

    /// <summary>
    /// Статус объекта Росреестр (12160)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12160)]
    public enum State : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Ранее учтенный (1216001)
        /// </summary>
        [Description("Ранее учтенный")]
        [EnumCode("1")]
        PreviouslyPosted = 1216001,
        /// <summary>
        /// Временный (1216002)
        /// </summary>
        [Description("Временный")]
        [EnumCode("2")]
        Temporary = 1216002,
        /// <summary>
        /// Учтенный (1216003)
        /// </summary>
        [Description("Учтенный")]
        [EnumCode("3")]
        Posted = 1216003,
        /// <summary>
        /// Снят с учета (1216004)
        /// </summary>
        [Description("Снят с учета")]
        [EnumCode("4")]
        RemovedFromRegister = 1216004,
        /// <summary>
        /// Аннулированный (1216005)
        /// </summary>
        [Description("Аннулированный")]
        [EnumCode("5")]
        Cancelled = 1216005,
    }

    /// <summary>
    /// Классификатор типа помещения (12161)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12161)]
    public enum Assftp1 : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Квартира (1216101)
        /// </summary>
        [Description("Квартира")]
        [EnumCode("1")]
        Flat = 1216101,
        /// <summary>
        /// Комната (1216102)
        /// </summary>
        [Description("Комната")]
        [EnumCode("2")]
        Room = 1216102,
    }

    /// <summary>
    /// Код назначения помещения (12162)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12162)]
    public enum Assftp_cd : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Жилое помещение (1216201)
        /// </summary>
        [Description("Жилое помещение")]
        [EnumCode("1")]
        Dwelling = 1216201,
        /// <summary>
        /// Нежилое помещение (1216202)
        /// </summary>
        [Description("Нежилое помещение")]
        [EnumCode("2")]
        UninhabitedPremise = 1216202,
    }

    /// <summary>
    /// Условия страхования (12164)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12164)]
    public enum InsuranceTerms : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Общие (1216401)
        /// </summary>
        [Description("Общие")]
        [EnumCode("0")]
        Common = 1216401,
        /// <summary>
        /// Особые (1216402)
        /// </summary>
        [Description("Особые")]
        [EnumCode("1")]
        Special = 1216402,
    }

    /// <summary>
    /// Статус дела расчета ущерба (12165)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12165)]
    public enum StatusDamageAmount : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Создано (12165001)
        /// </summary>
        [Description("Создано")]
        [EnumCode("")]
        Created = 12165001,
        /// <summary>
        /// Расчет ущерба совпадает с данными СК (12165002)
        /// </summary>
        [Description("Расчет ущерба совпадает с данными СК")]
        [EnumCode("")]
        DamageAmountCoincides = 12165002,
        /// <summary>
        /// Расхождения со СК в расчете ущерба (12165003)
        /// </summary>
        [Description("Расхождения со СК в расчете ущерба")]
        [EnumCode("")]
        DamageAmountDiscrepancies = 12165003,
        /// <summary>
        /// Проверено (12165004)
        /// </summary>
        [Description("Проверено")]
        [EnumCode("")]
        Checked = 12165004,
        /// <summary>
        /// Согласовано (12165005)
        /// </summary>
        [Description("Согласовано")]
        [EnumCode("")]
        Agreed = 12165005,
        /// <summary>
        /// Сформирован реестр выплат (12165006)
        /// </summary>
        [Description("Сформирован реестр выплат")]
        [EnumCode("")]
        PaymentRegisterFormed = 12165006,
        /// <summary>
        /// Произведена выплата в полном объеме (12165007)
        /// </summary>
        [Description("Произведена выплата в полном объеме")]
        [EnumCode("")]
        FullPaid = 12165007,
        /// <summary>
        /// Передано на проверку (12165008)
        /// </summary>
        [Description("Передано на проверку")]
        [EnumCode("")]
        SendToCheck = 12165008,
        /// <summary>
        /// Произведена выплата частично (12165009)
        /// </summary>
        [Description("Произведена выплата частично")]
        [EnumCode("")]
        PartPaid = 12165009,
        /// <summary>
        /// Отказано в выплате (12165010)
        /// </summary>
        [Description("Отказано в выплате")]
        [EnumCode("")]
        Denied = 12165010,
    }

    /// <summary>
    /// Критерии сверки данных (12166)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12166)]
    public enum VerificationCriteria : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Для зачислений МФЦ отсутствуют соответствующие строки банка (12166001)
        /// </summary>
        [Description("Для зачислений МФЦ отсутствуют соответствующие строки банка")]
        [EnumCode("1")]
        NotIdentifiedPlat = 12166001,
        /// <summary>
        /// Для банковских строк оплат отсутствуют соответствующие зачисления в данных МФЦ (12166002)
        /// </summary>
        [Description("Для банковских строк оплат отсутствуют соответствующие зачисления в данных МФЦ")]
        [EnumCode("2")]
        BankPlatWithoutPlat = 12166002,
        /// <summary>
        /// Расхождения в суммах начислений (12166003)
        /// </summary>
        [Description("Расхождения в суммах начислений")]
        [EnumCode("3")]
        CalcSumMismatch = 12166003,
        /// <summary>
        /// UNOM с разными адресами (12166004)
        /// </summary>
        [Description("UNOM с разными адресами")]
        [EnumCode("4")]
        UnomAddressMismatch = 12166004,
        /// <summary>
        /// Подозрительные UNOM (12166005)
        /// </summary>
        [Description("Подозрительные UNOM")]
        [EnumCode("5")]
        SuspiciousUnom = 12166005,
        /// <summary>
        /// Несовпадение NOM+NOMI (12166006)
        /// </summary>
        [Description("Несовпадение NOM+NOMI")]
        [EnumCode("6")]
        KvnomNomMismatch = 12166006,
        /// <summary>
        /// Наличие более одного начисления на одного плательщика (12166007)
        /// </summary>
        [Description("Наличие более одного начисления на одного плательщика")]
        [EnumCode("7")]
        MoreThanOneNach = 12166007,
        /// <summary>
        /// Есть начисление, нет площади (12166008)
        /// </summary>
        [Description("Есть начисление, нет площади")]
        [EnumCode("8")]
        NachWithoutOpl = 12166008,
        /// <summary>
        /// Площадь страхования не совпадает с площадью квартиры, для отдельных квартир (12166009)
        /// </summary>
        [Description("Площадь страхования не совпадает с площадью квартиры, для отдельных квартир")]
        [EnumCode("9")]
        FoplOplMismatch = 12166009,
        /// <summary>
        /// В данных МФЦ квартира присутствует, а объект страхования (квартира) в Системе не найден (12166010)
        /// </summary>
        [Description("В данных МФЦ квартира присутствует, а объект страхования (квартира) в Системе не найден")]
        [EnumCode("10")]
        FlatNotFound = 12166010,
        /// <summary>
        /// В данных МФЦ неверная общая площадь квартиры (12166011)
        /// </summary>
        [Description("В данных МФЦ неверная общая площадь квартиры")]
        [EnumCode("11")]
        FlatOplMismatch = 12166011,
        /// <summary>
        /// В данных МФЦ неверное количество комнат в квартире (12166012)
        /// </summary>
        [Description("В данных МФЦ неверное количество комнат в квартире")]
        [EnumCode("12")]
        FlatKolgpMismatch = 12166012,
    }

    /// <summary>
    /// Тип документа-основания дела (12167)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12167)]
    public enum TypeDocBaseCase : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Бумажная (12167001)
        /// </summary>
        [Description("Бумажная")]
        [EnumCode("")]
        DocTypePaper = 12167001,
        /// <summary>
        /// Электронная (12167002)
        /// </summary>
        [Description("Электронная")]
        [EnumCode("")]
        DocTypeElectro = 12167002,
    }

    /// <summary>
    /// Тип реестра оплат (12168)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12168)]
    public enum ReestrPayType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Реестр выплат доли города по ущербу в ЖП (12168001)
        /// </summary>
        [Description("Реестр выплат доли города по ущербу в ЖП")]
        [EnumCode("")]
        DamageGP = 12168001,
        /// <summary>
        /// Реестр выплат доли города по ущербу в ОИ (12168002)
        /// </summary>
        [Description("Реестр выплат доли города по ущербу в ОИ")]
        [EnumCode("")]
        DamageOI = 12168002,
        /// <summary>
        /// Реестр возвратов части премии по ОИ (12168003)
        /// </summary>
        [Description("Реестр возвратов части премии по ОИ")]
        [EnumCode("")]
        ReturnBonusOI = 12168003,
    }

    /// <summary>
    /// Статусы реестра оплат (12169)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12169)]
    public enum ReestrPayStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Сформирован (12169001)
        /// </summary>
        [Description("Сформирован")]
        [EnumCode("")]
        Formed = 12169001,
        /// <summary>
        /// Передан в ДГИ (12169002)
        /// </summary>
        [Description("Передан в ДГИ")]
        [EnumCode("")]
        TransferredDGI = 12169002,
        /// <summary>
        /// Утвержден в ДГИ (12169003)
        /// </summary>
        [Description("Утвержден в ДГИ")]
        [EnumCode("")]
        ApprovedDGI = 12169003,
        /// <summary>
        /// Оплачен (12169004)
        /// </summary>
        [Description("Оплачен")]
        [EnumCode("")]
        Paid = 12169004,
        /// <summary>
        /// Требует корректировки (12169005)
        /// </summary>
        [Description("Требует корректировки")]
        [EnumCode("")]
        RequiresCorrects = 12169005,
        /// <summary>
        /// Передано в оплату (12169006)
        /// </summary>
        [Description("Передано в оплату")]
        [EnumCode("")]
        TransferredPayment = 12169006,
        /// <summary>
        /// Расформирован (12169007)
        /// </summary>
        [Description("Расформирован")]
        [EnumCode("")]
        Disbaned = 12169007,
    }

    /// <summary>
    /// Статус счета (12170)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12170)]
    public enum InvoiceStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Счет создан (12170001)
        /// </summary>
        [Description("Счет создан")]
        [EnumCode("")]
        Formed = 12170001,
        /// <summary>
        /// Счет включен в реестр выплат (12170002)
        /// </summary>
        [Description("Счет включен в реестр выплат")]
        [EnumCode("")]
        Included = 12170002,
        /// <summary>
        /// Счет оплачен (12170003)
        /// </summary>
        [Description("Счет оплачен")]
        [EnumCode("")]
        Paid = 12170003,
        /// <summary>
        /// Ошибка в реквизитах (12170004)
        /// </summary>
        [Description("Ошибка в реквизитах")]
        [EnumCode("")]
        ErrorInDetails = 12170004,
        /// <summary>
        /// Отказано в выплате (12170005)
        /// </summary>
        [Description("Отказано в выплате")]
        [EnumCode("")]
        Denied = 12170005,
        /// <summary>
        /// Счет передан на оплату (12170006)
        /// </summary>
        [Description("Счет передан на оплату")]
        [EnumCode("")]
        TransferredPayment = 12170006,
        /// <summary>
        /// Счет согласован (12170007)
        /// </summary>
        [Description("Счет согласован")]
        [EnumCode("")]
        Agreed = 12170007,
        /// <summary>
        /// Отказано в выплате/Согласован (12170008)
        /// </summary>
        [Description("Отказано в выплате/Согласован")]
        [EnumCode("")]
        DeniedAgreed = 12170008,
    }

    /// <summary>
    /// Статусы договора по ОИ (12171)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12171)]
    public enum ContractStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Создан (12171001)
        /// </summary>
        [Description("Создан")]
        [EnumCode("")]
        Created = 12171001,
        /// <summary>
        /// Подготовлено уведомление (12171002)
        /// </summary>
        [Description("Подготовлено уведомление")]
        [EnumCode("")]
        NotificationPrepared = 12171002,
        /// <summary>
        /// Согласован (12171003)
        /// </summary>
        [Description("Согласован")]
        [EnumCode("")]
        Agreed = 12171003,
        /// <summary>
        /// Сформирован реестр выплат (12171004)
        /// </summary>
        [Description("Сформирован реестр выплат")]
        [EnumCode("")]
        Formed = 12171004,
        /// <summary>
        /// Произведена выплата в полном объеме (12171005)
        /// </summary>
        [Description("Произведена выплата в полном объеме")]
        [EnumCode("")]
        FullPaymentMade = 12171005,
        /// <summary>
        /// Произведена частичная выплата (12171006)
        /// </summary>
        [Description("Произведена частичная выплата")]
        [EnumCode("")]
        PartialPaymentMade = 12171006,
    }

    /// <summary>
    /// Статус загрузки файлов МФЦ (12172)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12172)]
    public enum MfcUploadFileStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Подготовка процесса загрузки (12172001)
        /// </summary>
        [Description("Подготовка процесса загрузки")]
        [EnumCode("0")]
        Prepare = 12172001,
        /// <summary>
        /// Распаковка загружаемых файлов (12172002)
        /// </summary>
        [Description("Распаковка загружаемых файлов")]
        [EnumCode("5")]
        UnpackageFiles = 12172002,
        /// <summary>
        /// Обработка загружаемых файлов оплат (12172003)
        /// </summary>
        [Description("Обработка загружаемых файлов оплат")]
        [EnumCode("15")]
        BankPlatProcess = 12172003,
        /// <summary>
        /// Обработка загружаемых файлов начислений (12172004)
        /// </summary>
        [Description("Обработка загружаемых файлов начислений")]
        [EnumCode("30")]
        NachProcess = 12172004,
        /// <summary>
        /// Обработка загружаемых файлов зачислений (12172005)
        /// </summary>
        [Description("Обработка загружаемых файлов зачислений")]
        [EnumCode("45")]
        PlatProcess = 12172005,
        /// <summary>
        /// Установка критериев (12172006)
        /// </summary>
        [Description("Установка критериев")]
        [EnumCode("75")]
        CriteriaSet = 12172006,
        /// <summary>
        /// Сохранение данных загрузки (12172007)
        /// </summary>
        [Description("Сохранение данных загрузки")]
        [EnumCode("60")]
        DbSave = 12172007,
        /// <summary>
        /// Загрузка завершена (12172008)
        /// </summary>
        [Description("Загрузка завершена")]
        [EnumCode("100")]
        Finished = 12172008,
        /// <summary>
        /// Ошибка загрузки (12172009)
        /// </summary>
        [Description("Ошибка загрузки")]
        [EnumCode("100")]
        Error = 12172009,
        /// <summary>
        /// Установка критериев начислений (1/10) (12172010)
        /// </summary>
        [Description("Установка критериев начислений (1/10)")]
        [EnumCode("76.5")]
        CriteriaSetNach1 = 12172010,
        /// <summary>
        /// Установка критериев начислений (2/10) (12172011)
        /// </summary>
        [Description("Установка критериев начислений (2/10)")]
        [EnumCode("78")]
        CriteriaSetNach2 = 12172011,
        /// <summary>
        /// Установка критериев начислений (3/10) (12172012)
        /// </summary>
        [Description("Установка критериев начислений (3/10)")]
        [EnumCode("80")]
        CriteriaSetNach3 = 12172012,
        /// <summary>
        /// Установка критериев начислений (4/10) (12172013)
        /// </summary>
        [Description("Установка критериев начислений (4/10)")]
        [EnumCode("81.5")]
        CriteriaSetNach4 = 12172013,
        /// <summary>
        /// Установка критериев начислений (5/10) (12172014)
        /// </summary>
        [Description("Установка критериев начислений (5/10)")]
        [EnumCode("83")]
        CriteriaSetNach5 = 12172014,
        /// <summary>
        /// Установка критериев начислений (6/10) (12172015)
        /// </summary>
        [Description("Установка критериев начислений (6/10)")]
        [EnumCode("84.5")]
        CriteriaSetNach6 = 12172015,
        /// <summary>
        /// Установка критериев начислений (7/10) (12172016)
        /// </summary>
        [Description("Установка критериев начислений (7/10)")]
        [EnumCode("86")]
        CriteriaSetNach7 = 12172016,
        /// <summary>
        /// Установка критериев начислений (8/10) (12172017)
        /// </summary>
        [Description("Установка критериев начислений (8/10)")]
        [EnumCode("87.5")]
        CriteriaSetNach8 = 12172017,
        /// <summary>
        /// Установка критериев начислений (9/10) (12172018)
        /// </summary>
        [Description("Установка критериев начислений (9/10)")]
        [EnumCode("89")]
        CriteriaSetNach9 = 12172018,
        /// <summary>
        /// Установка критериев начислений (10/10) (12172019)
        /// </summary>
        [Description("Установка критериев начислений (10/10)")]
        [EnumCode("90.5")]
        CriteriaSetNach10 = 12172019,
        /// <summary>
        /// Установка критериев зачислений (1/5) (12172020)
        /// </summary>
        [Description("Установка критериев зачислений (1/5)")]
        [EnumCode("92")]
        CriteriaSetPlat1 = 12172020,
        /// <summary>
        /// Установка критериев зачислений (2/5) (12172021)
        /// </summary>
        [Description("Установка критериев зачислений (2/5)")]
        [EnumCode("93.5")]
        CriteriaSetPlat2 = 12172021,
        /// <summary>
        /// Установка критериев зачислений (3/5) (12172022)
        /// </summary>
        [Description("Установка критериев зачислений (3/5)")]
        [EnumCode("95.5")]
        CriteriaSetPlat3 = 12172022,
        /// <summary>
        /// Установка критериев зачислений (4/5) (12172023)
        /// </summary>
        [Description("Установка критериев зачислений (4/5)")]
        [EnumCode("97")]
        CriteriaSetPlat4 = 12172023,
        /// <summary>
        /// Установка критериев зачислений (5/5) (12172024)
        /// </summary>
        [Description("Установка критериев зачислений (5/5)")]
        [EnumCode("98.5")]
        CriteriaSetPlat5 = 12172024,
    }

    /// <summary>
    /// Основной статус загрузки МФЦ (12173)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12173)]
    public enum MfcGeneralUploadStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Загружается (12173001)
        /// </summary>
        [Description("Загружается")]
        [EnumCode("1")]
        Loading = 12173001,
        /// <summary>
        /// Загружен (12173002)
        /// </summary>
        [Description("Загружен")]
        [EnumCode("2")]
        Loaded = 12173002,
    }

    /// <summary>
    /// Статус идентифкации зачислений (12174)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12174)]
    public enum IdentifyPlatStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Подготовка процесса идентифкации (12174001)
        /// </summary>
        [Description("Подготовка процесса идентифкации")]
        [EnumCode("1")]
        Prepare = 12174001,
        /// <summary>
        /// Идентификация зачислений (12174002)
        /// </summary>
        [Description("Идентификация зачислений")]
        [EnumCode("2")]
        Identify = 12174002,
        /// <summary>
        /// Идентификация завершена (12174003)
        /// </summary>
        [Description("Идентификация завершена")]
        [EnumCode("3")]
        Finished = 12174003,
    }

    /// <summary>
    /// Тип операции по изменению данных (12175)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12175)]
    public enum ChangeOperationType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Изменение суммы платежа (12175001)
        /// </summary>
        [Description("Изменение суммы платежа")]
        [EnumCode("1")]
        SumOpl = 12175001,
        /// <summary>
        /// Изменение даты платежа (12175002)
        /// </summary>
        [Description("Изменение даты платежа")]
        [EnumCode("2")]
        DateOpl = 12175002,
        /// <summary>
        /// Изменение UNOM (12175003)
        /// </summary>
        [Description("Изменение UNOM")]
        [EnumCode("3")]
        Unom = 12175003,
        /// <summary>
        /// Изменение кода плательщика (12175004)
        /// </summary>
        [Description("Изменение кода плательщика")]
        [EnumCode("4")]
        Kodpl = 12175004,
        /// <summary>
        /// Изменение адреса дома (12175005)
        /// </summary>
        [Description("Изменение адреса дома")]
        [EnumCode("5")]
        Adres = 12175005,
        /// <summary>
        /// Изменение номера квартиры (12175006)
        /// </summary>
        [Description("Изменение номера квартиры")]
        [EnumCode("6")]
        Kvnom = 12175006,
        /// <summary>
        /// Изменение суммы начисления (12175007)
        /// </summary>
        [Description("Изменение суммы начисления")]
        [EnumCode("7")]
        SumNach = 12175007,
        /// <summary>
        /// Изменение суммы зачисления (12175008)
        /// </summary>
        [Description("Изменение суммы зачисления")]
        [EnumCode("8")]
        SumZach = 12175008,
        /// <summary>
        /// Изменение статуса идентификации (12175009)
        /// </summary>
        [Description("Изменение статуса идентификации")]
        [EnumCode("9")]
        StatusIdentif = 12175009,
        /// <summary>
        /// Изменение связи строки зачисления с оплатой (12175010)
        /// </summary>
        [Description("Изменение связи строки зачисления с оплатой")]
        [EnumCode("10")]
        LinkBankPlat = 12175010,
        /// <summary>
        /// Изменение кадастрового номера (12175011)
        /// </summary>
        [Description("Изменение кадастрового номера")]
        [EnumCode("11")]
        CadastrNum = 12175011,
        /// <summary>
        /// Изменение округа (12175012)
        /// </summary>
        [Description("Изменение округа")]
        [EnumCode("12")]
        OkrugId = 12175012,
        /// <summary>
        /// Изменение района (12175013)
        /// </summary>
        [Description("Изменение района")]
        [EnumCode("13")]
        DistrictId = 12175013,
        /// <summary>
        /// Изменение адреса (12175014)
        /// </summary>
        [Description("Изменение адреса")]
        [EnumCode("14")]
        AddressId = 12175014,
        /// <summary>
        /// Изменение года постройки (12175015)
        /// </summary>
        [Description("Изменение года постройки")]
        [EnumCode("15")]
        YearStroi = 12175015,
        /// <summary>
        /// Изменение общей площади (12175016)
        /// </summary>
        [Description("Изменение общей площади")]
        [EnumCode("16")]
        Opl = 12175016,
        /// <summary>
        /// Изменение округа (12175017)
        /// </summary>
        [Description("Изменение округа")]
        [EnumCode("17")]
        Aok = 12175017,
        /// <summary>
        /// Изменение района (12175018)
        /// </summary>
        [Description("Изменение района")]
        [EnumCode("18")]
        District = 12175018,
        /// <summary>
        /// Изменение общей площади (12175019)
        /// </summary>
        [Description("Изменение общей площади")]
        [EnumCode("19")]
        Fopl = 12175019,
        /// <summary>
        /// Изменение площади жилой (12175020)
        /// </summary>
        [Description("Изменение площади жилой")]
        [EnumCode("20")]
        Gpl = 12175020,
        /// <summary>
        /// Изменение ссылки в справочнике ФИАС на адрес МКД (12175021)
        /// </summary>
        [Description("Изменение ссылки в справочнике ФИАС на адрес МКД")]
        [EnumCode("21")]
        GuidFiasMkd = 12175021,
        /// <summary>
        /// Изменение площади лоджий (12175022)
        /// </summary>
        [Description("Изменение площади лоджий")]
        [EnumCode("22")]
        Lpl = 12175022,
        /// <summary>
        /// Изменение количества лифтов пассажирских (12175023)
        /// </summary>
        [Description("Изменение количества лифтов пассажирских")]
        [EnumCode("23")]
        Lfpq = 12175023,
        /// <summary>
        /// Изменение количества лифтов грузопассажирских (12175024)
        /// </summary>
        [Description("Изменение количества лифтов грузопассажирских")]
        [EnumCode("24")]
        Lfgpq = 12175024,
        /// <summary>
        /// Изменение количества лифтов грузовых (12175025)
        /// </summary>
        [Description("Изменение количества лифтов грузовых")]
        [EnumCode("25")]
        Lfgq = 12175025,
        /// <summary>
        /// Изменение площади кровли (12175026)
        /// </summary>
        [Description("Изменение площади кровли")]
        [EnumCode("26")]
        Krovpl = 12175026,
        /// <summary>
        /// Изменение назначения объекта (12175027)
        /// </summary>
        [Description("Изменение назначения объекта")]
        [EnumCode("27")]
        PurposeName = 12175027,
        /// <summary>
        /// Изменение статуса состояния БТИ (12175028)
        /// </summary>
        [Description("Изменение статуса состояния БТИ")]
        [EnumCode("28")]
        StatusSostBti = 12175028,
        /// <summary>
        /// Изменение площади холодных помещений (12175029)
        /// </summary>
        [Description("Изменение площади холодных помещений")]
        [EnumCode("29")]
        Hpl = 12175029,
        /// <summary>
        /// Изменение площади балконов (12175030)
        /// </summary>
        [Description("Изменение площади балконов")]
        [EnumCode("30")]
        Bpl = 12175030,
        /// <summary>
        /// Изменение площади нежилых помещений (12175031)
        /// </summary>
        [Description("Изменение площади нежилых помещений")]
        [EnumCode("31")]
        OplN = 12175031,
        /// <summary>
        /// Изменение площади жилых помещений (12175032)
        /// </summary>
        [Description("Изменение площади жилых помещений")]
        [EnumCode("32")]
        OplG = 12175032,
        /// <summary>
        /// Изменение количества комнат в квартире (12175033)
        /// </summary>
        [Description("Изменение количества комнат в квартире")]
        [EnumCode("33")]
        KolGp = 12175033,
        /// <summary>
        /// Изменение количества этажей (12175034)
        /// </summary>
        [Description("Изменение количества этажей")]
        [EnumCode("34")]
        CountFloor = 12175034,
        /// <summary>
        /// Изменение назначения помещения (12175035)
        /// </summary>
        [Description("Изменение назначения помещения")]
        [EnumCode("35")]
        KlassFlat = 12175035,
        /// <summary>
        /// Изменение типа помещения (12175036)
        /// </summary>
        [Description("Изменение типа помещения")]
        [EnumCode("36")]
        TypeFlat = 12175036,
        /// <summary>
        /// Cвязаны жилые помещения (12175037)
        /// </summary>
        [Description("Cвязаны жилые помещения")]
        [EnumCode("37")]
        LinkFlats = 12175037,
        /// <summary>
        /// Cвязаны многоквартирные дома (12175038)
        /// </summary>
        [Description("Cвязаны многоквартирные дома")]
        [EnumCode("38")]
        LinkBuildings = 12175038,
    }

    /// <summary>
    /// Статус обработки файлов (12176)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12176)]
    public enum FileProcessStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Подготовка процесса обработки (12176001)
        /// </summary>
        [Description("Подготовка процесса обработки")]
        [EnumCode("0")]
        Prepare = 12176001,
        /// <summary>
        /// Создание/связка с ФСП (12176002)
        /// </summary>
        [Description("Создание/связка с ФСП")]
        [EnumCode("75")]
        CreateBindFsp = 12176002,
        /// <summary>
        /// Перерасчет ФСП (12176003)
        /// </summary>
        [Description("Перерасчет ФСП")]
        [EnumCode("100")]
        RecalcFsp = 12176003,
        /// <summary>
        /// Обработка завершена (12176004)
        /// </summary>
        [Description("Обработка завершена")]
        [EnumCode("100")]
        Finished = 12176004,
    }

    /// <summary>
    /// Страховые компании (12177)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12177)]
    public enum InsurComp : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

}
