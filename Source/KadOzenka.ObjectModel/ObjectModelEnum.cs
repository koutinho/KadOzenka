using System.ComponentModel;
using Core.ObjectModel.CustomAttribute;
using Core.Shared.Attributes;

namespace ObjectModel.Directory
{
    /// <summary>
    /// Виды сторонних площадок (101)
    ///</summary>
    [ReferenceInfo(ReferenceId = 101)]
    public enum MarketTypes : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// ЦИАН (1)
        /// </summary>
        [Description("ЦИАН")]
        [EnumCode("1")]
        Cian = 1,
        /// <summary>
        /// Авито (2)
        /// </summary>
        [Description("Авито")]
        [EnumCode("2")]
        Avito = 2,
        /// <summary>
        /// Яндекс недвижимость (3)
        /// </summary>
        [Description("Яндекс недвижимость")]
        [EnumCode("3")]
        YandexProterty = 3,
        /// <summary>
        /// Росреестр (737)
        /// </summary>
        [Description("Росреестр")]
        [EnumCode("4")]
        Rosreestr = 737,
    }

    /// <summary>
    /// Виды объектов недвижимости (102)
    ///</summary>
    [ReferenceInfo(ReferenceId = 102)]
    public enum PropertyTypes : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Земельный участок (4)
        /// </summary>
        [Description("Земельный участок")]
        [EnumCode("002001001000")]
        Stead = 4,
        /// <summary>
        /// Здание (5)
        /// </summary>
        [Description("Здание")]
        [EnumCode("002001002000")]
        Building = 5,
        /// <summary>
        /// Помещение (6)
        /// </summary>
        [Description("Помещение")]
        [EnumCode("002001003000")]
        Pllacement = 6,
        /// <summary>
        /// Сооружение (7)
        /// </summary>
        [Description("Сооружение")]
        [EnumCode("002001004000")]
        Construction = 7,
        /// <summary>
        /// Объект незавершённого строительства (8)
        /// </summary>
        [Description("Объект незавершённого строительства")]
        [EnumCode("002001005000")]
        UncompletedBuilding = 8,
        /// <summary>
        /// Предприятие как имущественный комплекс (9)
        /// </summary>
        [Description("Предприятие как имущественный комплекс")]
        [EnumCode("002001006000")]
        Company = 9,
        /// <summary>
        /// Единый недвижимый комплекс (10)
        /// </summary>
        [Description("Единый недвижимый комплекс")]
        [EnumCode("002001008000")]
        UnitedPropertyComplex = 10,
        /// <summary>
        /// Машино-место (11)
        /// </summary>
        [Description("Машино-место")]
        [EnumCode("002001009000")]
        Parking = 11,
        /// <summary>
        /// Иной объект недвижимости (12)
        /// </summary>
        [Description("Иной объект недвижимости")]
        [EnumCode("002001010000")]
        Other = 12,
    }

    /// <summary>
    /// Тип сделки (110)
    ///</summary>
    [ReferenceInfo(ReferenceId = 110)]
    public enum DealType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Предложение-продажа (733)
        /// </summary>
        [Description("Предложение-продажа")]
        [EnumCode("1")]
        SaleSuggestion = 733,
        /// <summary>
        /// Сделка купли-продажи (734)
        /// </summary>
        [Description("Сделка купли-продажи")]
        [EnumCode("2")]
        SaleDeal = 734,
        /// <summary>
        /// Предложение-аренда (735)
        /// </summary>
        [Description("Предложение-аренда")]
        [EnumCode("3")]
        RentSuggestion = 735,
        /// <summary>
        /// Сделка-аренда (736)
        /// </summary>
        [Description("Сделка-аренда")]
        [EnumCode("4")]
        RentDeal = 736,
    }

    /// <summary>
    /// Процесс обработки (113)
    ///</summary>
    [ReferenceInfo(ReferenceId = 113)]
    public enum ProcessStep : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Не обработан (738)
        /// </summary>
        [Description("Не обработан")]
        [EnumCode("1")]
        DoNotProcessed = 738,
        /// <summary>
        /// Получен адрес (739)
        /// </summary>
        [Description("Получен адрес")]
        [EnumCode("2")]
        AddressStep = 739,
        /// <summary>
        /// Получен кадастровый номер (740)
        /// </summary>
        [Description("Получен кадастровый номер")]
        [EnumCode("3")]
        CadastralNumberStep = 740,
        /// <summary>
        /// В работе (741)
        /// </summary>
        [Description("В работе")]
        [EnumCode("4")]
        InProcess = 741,
        /// <summary>
        /// Обработан (742)
        /// </summary>
        [Description("Обработан")]
        [EnumCode("5")]
        Dealed = 742,
        /// <summary>
        /// Исключён (743)
        /// </summary>
        [Description("Исключён")]
        [EnumCode("6")]
        Excluded = 743,
    }

    /// <summary>
    /// Сегмент рынка недвижимости (114)
    ///</summary>
    [ReferenceInfo(ReferenceId = 114)]
    public enum MarketSegment : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Апартаменты (744)
        /// </summary>
        [Description("Апартаменты")]
        [EnumCode("1")]
        Appartment = 744,
        /// <summary>
        /// Гаражи (745)
        /// </summary>
        [Description("Гаражи")]
        [EnumCode("2")]
        Parking = 745,
        /// <summary>
        /// Гостиницы (746)
        /// </summary>
        [Description("Гостиницы")]
        [EnumCode("3")]
        Hotel = 746,
        /// <summary>
        /// ИЖС (747)
        /// </summary>
        [Description("ИЖС")]
        [EnumCode("4")]
        IZHS = 747,
        /// <summary>
        /// Машиноместа (748)
        /// </summary>
        [Description("Машиноместа")]
        [EnumCode("5")]
        CarParking = 748,
        /// <summary>
        /// МЖС (749)
        /// </summary>
        [Description("МЖС")]
        [EnumCode("6")]
        MZHS = 749,
        /// <summary>
        /// Офисы (750)
        /// </summary>
        [Description("Офисы")]
        [EnumCode("7")]
        Office = 750,
        /// <summary>
        /// Производство и склады (751)
        /// </summary>
        [Description("Производство и склады")]
        [EnumCode("8")]
        Factory = 751,
        /// <summary>
        /// Садоводческое, огородническое и дачное использование (752)
        /// </summary>
        [Description("Садоводческое, огородническое и дачное использование")]
        [EnumCode("9")]
        Garden = 752,
        /// <summary>
        /// Санатории (753)
        /// </summary>
        [Description("Санатории")]
        [EnumCode("10")]
        Sanatorium = 753,
        /// <summary>
        /// Торговля (754)
        /// </summary>
        [Description("Торговля")]
        [EnumCode("11")]
        Trading = 754,
    }

    /// <summary>
    /// Материал стен (115)
    ///</summary>
    [ReferenceInfo(ReferenceId = 115)]
    public enum WallMaterial : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Кирпичные (755)
        /// </summary>
        [Description("Кирпичные")]
        [EnumCode("1")]
        Brick = 755,
        /// <summary>
        /// Монолитные (756)
        /// </summary>
        [Description("Монолитные")]
        [EnumCode("2")]
        Monolit = 756,
        /// <summary>
        /// Панельные и блочные (757)
        /// </summary>
        [Description("Панельные и блочные")]
        [EnumCode("3")]
        Panel = 757,
        /// <summary>
        /// Иное (758)
        /// </summary>
        [Description("Иное")]
        [EnumCode("4")]
        Other = 758,
    }

    /// <summary>
    /// Класс качества (116)
    ///</summary>
    [ReferenceInfo(ReferenceId = 116)]
    public enum QualityClass : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// А (760)
        /// </summary>
        [Description("А")]
        [EnumCode("1")]
        A = 760,
        /// <summary>
        /// В (761)
        /// </summary>
        [Description("В")]
        [EnumCode("2")]
        B = 761,
        /// <summary>
        /// В+ (762)
        /// </summary>
        [Description("В+")]
        [EnumCode("3")]
        Bplus = 762,
    }

    /// <summary>
    /// Причина исключения (117)
    ///</summary>
    [ReferenceInfo(ReferenceId = 117)]
    public enum ExclusionStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Некорректный адрес (763)
        /// </summary>
        [Description("Некорректный адрес")]
        [EnumCode("1")]
        NoAddress = 763,
        /// <summary>
        /// Отсутствует кадастровый номер (764)
        /// </summary>
        [Description("Отсутствует кадастровый номер")]
        [EnumCode("2")]
        NoCadastralNumber = 764,
        /// <summary>
        /// Отсутствует площадь (765)
        /// </summary>
        [Description("Отсутствует площадь")]
        [EnumCode("3")]
        NoArea = 765,
        /// <summary>
        /// Отсутствует цена (766)
        /// </summary>
        [Description("Отсутствует цена")]
        [EnumCode("4")]
        NoPrice = 766,
        /// <summary>
        /// Дубль (767)
        /// </summary>
        [Description("Дубль")]
        [EnumCode("5")]
        Duplicate = 767,
        /// <summary>
        /// Не Москва (768)
        /// </summary>
        [Description("Не Москва")]
        [EnumCode("6")]
        NoLocation = 768,
        /// <summary>
        /// Коридор цен (769)
        /// </summary>
        [Description("Коридор цен")]
        [EnumCode("7")]
        IncorrectPrice = 769,
        /// <summary>
        /// Предложение продажы прав аренды (776)
        /// </summary>
        [Description("Предложение продажы прав аренды")]
        [EnumCode("8")]
        ContainsPPA = 776,
        /// <summary>
        /// Маркер: мебель (777)
        /// </summary>
        [Description("Маркер: мебель")]
        [EnumCode("9")]
        ContainsFurniture = 777,
        /// <summary>
        /// Является ОНС (778)
        /// </summary>
        [Description("Является ОНС")]
        [EnumCode("10")]
        IsUncompleted = 778,
        /// <summary>
        /// Не содержит описания (779)
        /// </summary>
        [Description("Не содержит описания")]
        [EnumCode("11")]
        DoNotHaveDescription = 779,
        /// <summary>
        /// Неприемлимое назначение (780)
        /// </summary>
        [Description("Неприемлимое назначение")]
        [EnumCode("12")]
        UnacceptableAssignment = 780,
        /// <summary>
        /// Продажа бизнеса (781)
        /// </summary>
        [Description("Продажа бизнеса")]
        [EnumCode("13")]
        BusinessSelling = 781,
        /// <summary>
        /// Неприемлимые условия (782)
        /// </summary>
        [Description("Неприемлимые условия")]
        [EnumCode("14")]
        UnacceptableConditions = 782,
    }

    /// <summary>
    /// Статусы единицы оценки (200)
    ///</summary>
    [ReferenceInfo(ReferenceId = 200)]
    public enum KoUnitStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Тип статьи (201)
    ///</summary>
    [ReferenceInfo(ReferenceId = 201)]
    public enum KoNoteType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Статусы заданий на оценку (202)
    ///</summary>
    [ReferenceInfo(ReferenceId = 202)]
    public enum KoTaskStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Тип документа (203)
    ///</summary>
    [ReferenceInfo(ReferenceId = 203)]
    public enum KoDocType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Механизм группировки (204)
    ///</summary>
    [ReferenceInfo(ReferenceId = 204)]
    public enum KoGroupAlgoritm : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Алгоритм рассчёта (205)
    ///</summary>
    [ReferenceInfo(ReferenceId = 205)]
    public enum KoAlgoritmType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Статус модели тура (206)
    ///</summary>
    [ReferenceInfo(ReferenceId = 206)]
    public enum KoModelStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Тип данных фактора (207)
    ///</summary>
    [ReferenceInfo(ReferenceId = 207)]
    public enum KoFactorDataType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
    }

    /// <summary>
    /// Тип объекта судебного решения (300)
    ///</summary>
    [ReferenceInfo(ReferenceId = 300)]
    public enum SudObjectType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// Участок (770)
        /// </summary>
        [Description("Участок")]
        [EnumCode("1")]
        Site = 770,
        /// <summary>
        /// Здание (771)
        /// </summary>
        [Description("Здание")]
        [EnumCode("2")]
        Building = 771,
        /// <summary>
        /// Помещение (772)
        /// </summary>
        [Description("Помещение")]
        [EnumCode("3")]
        Room = 772,
        /// <summary>
        /// Сооружение (773)
        /// </summary>
        [Description("Сооружение")]
        [EnumCode("4")]
        Construction = 773,
        /// <summary>
        /// Онс (774)
        /// </summary>
        [Description("Онс")]
        [EnumCode("5")]
        Ons = 774,
        /// <summary>
        /// Машиноместо (775)
        /// </summary>
        [Description("Машиноместо")]
        [EnumCode("6")]
        ParkingPlace = 775,
    }

    /// <summary>
    /// Статус обработки (301)
    ///</summary>
    [ReferenceInfo(ReferenceId = 301)]
    public enum SudProcessingStatus : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        None = 0,
        /// <summary>
        /// В работе (783)
        /// </summary>
        [Description("В работе")]
        [EnumCode("0")]
        InWork = 783,
        /// <summary>
        /// Актуальный (784)
        /// </summary>
        [Description("Актуальный")]
        [EnumCode("1")]
        Processed = 784,
    }

}
