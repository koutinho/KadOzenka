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
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// ЦИАН (1)
		/// </summary>
		[Description("ЦИАН")]
        [EnumCode("1")]
        [ShortTitle("")]
        Cian = 1,
		/// <summary>
		/// Авито (2)
		/// </summary>
		[Description("Авито")]
        [EnumCode("2")]
        [ShortTitle("")]
        Avito = 2,
		/// <summary>
		/// Яндекс недвижимость (3)
		/// </summary>
		[Description("Яндекс недвижимость")]
        [EnumCode("3")]
        [ShortTitle("")]
        YandexProterty = 3,
		/// <summary>
		/// Росреестр (737)
		/// </summary>
		[Description("Росреестр")]
        [EnumCode("4")]
        [ShortTitle("")]
        Rosreestr = 737,
    }
}

namespace ObjectModel.Directory
{
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
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// Земельный участок (4)
		/// </summary>
		[Description("Земельный участок")]
        [EnumCode("002001001000")]
        [ShortTitle("")]
        Stead = 4,
		/// <summary>
		/// Здание (5)
		/// </summary>
		[Description("Здание")]
        [EnumCode("002001002000")]
        [ShortTitle("")]
        Building = 5,
		/// <summary>
		/// Помещение (6)
		/// </summary>
		[Description("Помещение")]
        [EnumCode("002001003000")]
        [ShortTitle("")]
        Pllacement = 6,
		/// <summary>
		/// Сооружение (7)
		/// </summary>
		[Description("Сооружение")]
        [EnumCode("002001004000")]
        [ShortTitle("")]
        Construction = 7,
		/// <summary>
		/// Объект незавершённого строительства (8)
		/// </summary>
		[Description("Объект незавершённого строительства")]
        [EnumCode("002001005000")]
        [ShortTitle("")]
        UncompletedBuilding = 8,
		/// <summary>
		/// Предприятие как имущественный комплекс (9)
		/// </summary>
		[Description("Предприятие как имущественный комплекс")]
        [EnumCode("002001006000")]
        [ShortTitle("")]
        Company = 9,
		/// <summary>
		/// Единый недвижимый комплекс (10)
		/// </summary>
		[Description("Единый недвижимый комплекс")]
        [EnumCode("002001008000")]
        [ShortTitle("")]
        UnitedPropertyComplex = 10,
		/// <summary>
		/// Машино-место (11)
		/// </summary>
		[Description("Машино-место")]
        [EnumCode("002001009000")]
        [ShortTitle("")]
        Parking = 11,
		/// <summary>
		/// Иной объект недвижимости (12)
		/// </summary>
		[Description("Иной объект недвижимости")]
        [EnumCode("002001010000")]
        [ShortTitle("")]
        Other = 12,
		/// <summary>
		/// Сооружения, ОНС, ЕНК, и иные ОН (802)
		/// </summary>
		[Description("Сооружения, ОНС, ЕНК, и иные ОН")]
        [EnumCode("002001010001")]
        [ShortTitle("")]
        OtherMore = 802,
    }
}

namespace ObjectModel.Directory
{
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
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// Предложение-продажа (733)
		/// </summary>
		[Description("Предложение-продажа")]
        [EnumCode("1")]
        [ShortTitle("")]
        SaleSuggestion = 733,
		/// <summary>
		/// Сделка купли-продажи (734)
		/// </summary>
		[Description("Сделка купли-продажи")]
        [EnumCode("2")]
        [ShortTitle("")]
        SaleDeal = 734,
		/// <summary>
		/// Предложение-аренда (735)
		/// </summary>
		[Description("Предложение-аренда")]
        [EnumCode("3")]
        [ShortTitle("")]
        RentSuggestion = 735,
		/// <summary>
		/// Сделка-аренда (736)
		/// </summary>
		[Description("Сделка-аренда")]
        [EnumCode("4")]
        [ShortTitle("")]
        RentDeal = 736,
    }
}

namespace ObjectModel.Directory
{
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
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// Не обработан (738)
		/// </summary>
		[Description("Не обработан")]
        [EnumCode("1")]
        [ShortTitle("")]
        DoNotProcessed = 738,
		/// <summary>
		/// Получен адрес (739)
		/// </summary>
		[Description("Получен адрес")]
        [EnumCode("2")]
        [ShortTitle("")]
        AddressStep = 739,
		/// <summary>
		/// Получен кадастровый номер (740)
		/// </summary>
		[Description("Получен кадастровый номер")]
        [EnumCode("3")]
        [ShortTitle("")]
        CadastralNumberStep = 740,
		/// <summary>
		/// В работу (741)
		/// </summary>
		[Description("В работу")]
        [EnumCode("4")]
        [ShortTitle("")]
        InProcess = 741,
		/// <summary>
		/// Обработан (742)
		/// </summary>
		[Description("Обработан")]
        [EnumCode("5")]
        [ShortTitle("")]
        Dealed = 742,
		/// <summary>
		/// Исключён (743)
		/// </summary>
		[Description("Исключён")]
        [EnumCode("6")]
        [ShortTitle("")]
        Excluded = 743,
    }
}

namespace ObjectModel.Directory
{
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
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// Апартаменты (744)
		/// </summary>
		[Description("Апартаменты")]
        [EnumCode("104")]
        [ShortTitle("")]
        Appartment = 744,
		/// <summary>
		/// Гаражи (745)
		/// </summary>
		[Description("Гаражи")]
        [EnumCode("2")]
        [ShortTitle("")]
        Parking = 745,
		/// <summary>
		/// Гостиницы (746)
		/// </summary>
		[Description("Гостиницы")]
        [EnumCode("3")]
        [ShortTitle("")]
        Hotel = 746,
		/// <summary>
		/// ИЖС (747)
		/// </summary>
		[Description("ИЖС")]
        [EnumCode("101")]
        [ShortTitle("")]
        IZHS = 747,
		/// <summary>
		/// Машиноместа (748)
		/// </summary>
		[Description("Машиноместа")]
        [EnumCode("1")]
        [ShortTitle("")]
        CarParking = 748,
		/// <summary>
		/// МЖС (749)
		/// </summary>
		[Description("МЖС")]
        [EnumCode("102")]
        [ShortTitle("")]
        MZHS = 749,
		/// <summary>
		/// Офисы (750)
		/// </summary>
		[Description("Офисы")]
        [EnumCode("5")]
        [ShortTitle("")]
        Office = 750,
		/// <summary>
		/// Производство и склады (751)
		/// </summary>
		[Description("Производство и склады")]
        [EnumCode("4")]
        [ShortTitle("")]
        Factory = 751,
		/// <summary>
		/// Садоводческое, огородническое и дачное использование (752)
		/// </summary>
		[Description("Садоводческое, огородническое и дачное использование")]
        [EnumCode("106")]
        [ShortTitle("")]
        Garden = 752,
		/// <summary>
		/// Санатории (753)
		/// </summary>
		[Description("Санатории")]
        [EnumCode("105")]
        [ShortTitle("")]
        Sanatorium = 753,
		/// <summary>
		/// Торговля (754)
		/// </summary>
		[Description("Торговля")]
        [EnumCode("6")]
        [ShortTitle("")]
        Trading = 754,
		/// <summary>
		/// Общепит (795)
		/// </summary>
		[Description("Общепит")]
        [EnumCode("7")]
        [ShortTitle("")]
        PublicCatering = 795,
		/// <summary>
		/// Без сегмента (798)
		/// </summary>
		[Description("Без сегмента")]
        [EnumCode("9")]
        [ShortTitle("")]
        NoSegment = 798,
    }
}

namespace ObjectModel.Directory
{
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
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// Кирпичные (755)
		/// </summary>
		[Description("Кирпичные")]
        [EnumCode("1")]
        [ShortTitle("")]
        Brick = 755,
		/// <summary>
		/// Монолитные (756)
		/// </summary>
		[Description("Монолитные")]
        [EnumCode("2")]
        [ShortTitle("")]
        Monolit = 756,
		/// <summary>
		/// Панельные и блочные (757)
		/// </summary>
		[Description("Панельные и блочные")]
        [EnumCode("3")]
        [ShortTitle("")]
        Panel = 757,
		/// <summary>
		/// Иное (758)
		/// </summary>
		[Description("Иное")]
        [EnumCode("4")]
        [ShortTitle("")]
        Other = 758,
    }
}

namespace ObjectModel.Directory
{
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
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// А (760)
		/// </summary>
		[Description("А")]
        [EnumCode("1")]
        [ShortTitle("")]
        A = 760,
		/// <summary>
		/// В (761)
		/// </summary>
		[Description("В")]
        [EnumCode("2")]
        [ShortTitle("")]
        B = 761,
		/// <summary>
		/// В+ (762)
		/// </summary>
		[Description("В+")]
        [EnumCode("3")]
        [ShortTitle("")]
        Bplus = 762,
		/// <summary>
		/// A+ (901)
		/// </summary>
		[Description("A+")]
        [EnumCode("4")]
        [ShortTitle("")]
        Aplus = 901,
		/// <summary>
		/// C (902)
		/// </summary>
		[Description("C")]
        [EnumCode("5")]
        [ShortTitle("")]
        C = 902,
    }
}

namespace ObjectModel.Directory
{
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
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// Некорректный адрес (763)
		/// </summary>
		[Description("Некорректный адрес")]
        [EnumCode("1")]
        [ShortTitle("")]
        NoAddress = 763,
		/// <summary>
		/// Отсутствует кадастровый номер (764)
		/// </summary>
		[Description("Отсутствует кадастровый номер")]
        [EnumCode("2")]
        [ShortTitle("")]
        NoCadastralNumber = 764,
		/// <summary>
		/// Отсутствует площадь (765)
		/// </summary>
		[Description("Отсутствует площадь")]
        [EnumCode("3")]
        [ShortTitle("")]
        NoArea = 765,
		/// <summary>
		/// Отсутствует цена (766)
		/// </summary>
		[Description("Отсутствует цена")]
        [EnumCode("4")]
        [ShortTitle("")]
        NoPrice = 766,
		/// <summary>
		/// Дубль (767)
		/// </summary>
		[Description("Дубль")]
        [EnumCode("5")]
        [ShortTitle("")]
        Duplicate = 767,
		/// <summary>
		/// Не Москва (768)
		/// </summary>
		[Description("Не Москва")]
        [EnumCode("6")]
        [ShortTitle("")]
        NoLocation = 768,
		/// <summary>
		/// Коридор цен (769)
		/// </summary>
		[Description("Коридор цен")]
        [EnumCode("7")]
        [ShortTitle("")]
        IncorrectPrice = 769,
		/// <summary>
		/// Предложение продажы прав аренды (776)
		/// </summary>
		[Description("Предложение продажы прав аренды")]
        [EnumCode("8")]
        [ShortTitle("")]
        ContainsPPA = 776,
		/// <summary>
		/// Маркер: мебель (777)
		/// </summary>
		[Description("Маркер: мебель")]
        [EnumCode("9")]
        [ShortTitle("")]
        ContainsFurniture = 777,
		/// <summary>
		/// Является ОНС (778)
		/// </summary>
		[Description("Является ОНС")]
        [EnumCode("10")]
        [ShortTitle("")]
        IsUncompleted = 778,
		/// <summary>
		/// Не содержит описания (779)
		/// </summary>
		[Description("Не содержит описания")]
        [EnumCode("11")]
        [ShortTitle("")]
        DoNotHaveDescription = 779,
		/// <summary>
		/// Неприемлимое назначение (780)
		/// </summary>
		[Description("Неприемлимое назначение")]
        [EnumCode("12")]
        [ShortTitle("")]
        UnacceptableAssignment = 780,
		/// <summary>
		/// Продажа бизнеса (781)
		/// </summary>
		[Description("Продажа бизнеса")]
        [EnumCode("13")]
        [ShortTitle("")]
        BusinessSelling = 781,
		/// <summary>
		/// Неприемлимые условия (782)
		/// </summary>
		[Description("Неприемлимые условия")]
        [EnumCode("14")]
        [ShortTitle("")]
        UnacceptableConditions = 782,
		/// <summary>
		/// Удалено (799)
		/// </summary>
		[Description("Удалено")]
        [EnumCode("15")]
        [ShortTitle("")]
        Deleted = 799,
		/// <summary>
		/// Снято с публикации (800)
		/// </summary>
		[Description("Снято с публикации")]
        [EnumCode("16")]
        [ShortTitle("")]
        Unpublished = 800,
		/// <summary>
		/// Выставлено на электронные торги (801)
		/// </summary>
		[Description("Выставлено на электронные торги")]
        [EnumCode("17")]
        [ShortTitle("")]
        Auction = 801,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Виды объектов недвижимости ЦИПЖС (118)
    ///</summary>
    [ReferenceInfo(ReferenceId = 118)]
    public enum PropertyTypesCIPJS : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// Земельные участки (803)
		/// </summary>
		[Description("Земельные участки")]
        [EnumCode("1")]
        [ShortTitle("")]
        LandArea = 803,
		/// <summary>
		/// Здания (804)
		/// </summary>
		[Description("Здания")]
        [EnumCode("2")]
        [ShortTitle("")]
        Buildings = 804,
		/// <summary>
		/// Помещения (805)
		/// </summary>
		[Description("Помещения")]
        [EnumCode("3")]
        [ShortTitle("")]
        Placements = 805,
		/// <summary>
		/// Сооружения, ОНС, ЕНК и иные ОН (806)
		/// </summary>
		[Description("Сооружения, ОНС, ЕНК и иные ОН")]
        [EnumCode("4")]
        [ShortTitle("")]
        OtherAndMore = 806,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Административные округа (119)
    ///</summary>
    [ReferenceInfo(ReferenceId = 119)]
    public enum Hunteds : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// ЦАО (903)
		/// </summary>
		[Description("ЦАО")]
        [EnumCode("1")]
        [ShortTitle("")]
        CAO = 903,
		/// <summary>
		/// САО (904)
		/// </summary>
		[Description("САО")]
        [EnumCode("2")]
        [ShortTitle("")]
        SAO = 904,
		/// <summary>
		/// СВАО (905)
		/// </summary>
		[Description("СВАО")]
        [EnumCode("3")]
        [ShortTitle("")]
        SVAO = 905,
		/// <summary>
		/// ВАО (906)
		/// </summary>
		[Description("ВАО")]
        [EnumCode("4")]
        [ShortTitle("")]
        VAO = 906,
		/// <summary>
		/// ЮВАО (907)
		/// </summary>
		[Description("ЮВАО")]
        [EnumCode("5")]
        [ShortTitle("")]
        YVAO = 907,
		/// <summary>
		/// ЮАО (908)
		/// </summary>
		[Description("ЮАО")]
        [EnumCode("6")]
        [ShortTitle("")]
        YAO = 908,
		/// <summary>
		/// ЮЗАО (909)
		/// </summary>
		[Description("ЮЗАО")]
        [EnumCode("7")]
        [ShortTitle("")]
        YZAO = 909,
		/// <summary>
		/// ЗАО (910)
		/// </summary>
		[Description("ЗАО")]
        [EnumCode("8")]
        [ShortTitle("")]
        ZAO = 910,
		/// <summary>
		/// СЗАО (911)
		/// </summary>
		[Description("СЗАО")]
        [EnumCode("9")]
        [ShortTitle("")]
        SZAO = 911,
		/// <summary>
		/// ЗелАО (912)
		/// </summary>
		[Description("ЗелАО")]
        [EnumCode("10")]
        [ShortTitle("")]
        ZelAO = 912,
		/// <summary>
		/// НАО (913)
		/// </summary>
		[Description("НАО")]
        [EnumCode("11")]
        [ShortTitle("")]
        NAO = 913,
		/// <summary>
		/// ТАО (914)
		/// </summary>
		[Description("ТАО")]
        [EnumCode("12")]
        [ShortTitle("")]
        TAO = 914,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Районы (120)
    ///</summary>
    [ReferenceInfo(ReferenceId = 120)]
    public enum Districts : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// Арбат (915)
		/// </summary>
		[Description("Арбат")]
        [EnumCode("1")]
        [ShortTitle("")]
        Arbat = 915,
		/// <summary>
		/// Басманный (916)
		/// </summary>
		[Description("Басманный")]
        [EnumCode("2")]
        [ShortTitle("")]
        Basmanny = 916,
		/// <summary>
		/// Замоскворечье (917)
		/// </summary>
		[Description("Замоскворечье")]
        [EnumCode("3")]
        [ShortTitle("")]
        Zamoskvorechye = 917,
		/// <summary>
		/// Красносельский (918)
		/// </summary>
		[Description("Красносельский")]
        [EnumCode("4")]
        [ShortTitle("")]
        Krasnoselsky = 918,
		/// <summary>
		/// Мещанский (919)
		/// </summary>
		[Description("Мещанский")]
        [EnumCode("5")]
        [ShortTitle("")]
        Meshchansky = 919,
		/// <summary>
		/// Пресненский (920)
		/// </summary>
		[Description("Пресненский")]
        [EnumCode("6")]
        [ShortTitle("")]
        Presnensky = 920,
		/// <summary>
		/// Таганский (921)
		/// </summary>
		[Description("Таганский")]
        [EnumCode("7")]
        [ShortTitle("")]
        Tagansky = 921,
		/// <summary>
		/// Тверской (922)
		/// </summary>
		[Description("Тверской")]
        [EnumCode("8")]
        [ShortTitle("")]
        Tverskoy = 922,
		/// <summary>
		/// Хамовники (923)
		/// </summary>
		[Description("Хамовники")]
        [EnumCode("9")]
        [ShortTitle("")]
        Khamovniki = 923,
		/// <summary>
		/// Якиманка (924)
		/// </summary>
		[Description("Якиманка")]
        [EnumCode("10")]
        [ShortTitle("")]
        Yakimanka = 924,
		/// <summary>
		/// Аэропорт (925)
		/// </summary>
		[Description("Аэропорт")]
        [EnumCode("11")]
        [ShortTitle("")]
        Aeroport = 925,
		/// <summary>
		/// Беговой (926)
		/// </summary>
		[Description("Беговой")]
        [EnumCode("12")]
        [ShortTitle("")]
        Begovoy = 926,
		/// <summary>
		/// Бескудниковский (927)
		/// </summary>
		[Description("Бескудниковский")]
        [EnumCode("13")]
        [ShortTitle("")]
        Beskudnikovsky = 927,
		/// <summary>
		/// Войковский (928)
		/// </summary>
		[Description("Войковский")]
        [EnumCode("14")]
        [ShortTitle("")]
        Voykovsky = 928,
		/// <summary>
		/// Головинский (929)
		/// </summary>
		[Description("Головинский")]
        [EnumCode("15")]
        [ShortTitle("")]
        Golovinsky = 929,
		/// <summary>
		/// Восточное Дегунино (930)
		/// </summary>
		[Description("Восточное Дегунино")]
        [EnumCode("16")]
        [ShortTitle("")]
        VostochnoyeDegunino = 930,
		/// <summary>
		/// Западное Дегунино (931)
		/// </summary>
		[Description("Западное Дегунино")]
        [EnumCode("17")]
        [ShortTitle("")]
        ZapadnoyeDegunino = 931,
		/// <summary>
		/// Дмитровский (932)
		/// </summary>
		[Description("Дмитровский")]
        [EnumCode("18")]
        [ShortTitle("")]
        Dmitrovsky = 932,
		/// <summary>
		/// Коптево (933)
		/// </summary>
		[Description("Коптево")]
        [EnumCode("19")]
        [ShortTitle("")]
        Koptevo = 933,
		/// <summary>
		/// Левобережный (934)
		/// </summary>
		[Description("Левобережный")]
        [EnumCode("20")]
        [ShortTitle("")]
        Levoberezhny = 934,
		/// <summary>
		/// Молжаниновский (935)
		/// </summary>
		[Description("Молжаниновский")]
        [EnumCode("21")]
        [ShortTitle("")]
        Molzhaninovsky = 935,
		/// <summary>
		/// Савёловский (936)
		/// </summary>
		[Description("Савёловский")]
        [EnumCode("22")]
        [ShortTitle("")]
        Savyolovsky = 936,
		/// <summary>
		/// Сокол (937)
		/// </summary>
		[Description("Сокол")]
        [EnumCode("23")]
        [ShortTitle("")]
        Sokol = 937,
		/// <summary>
		/// Тимирязевский (938)
		/// </summary>
		[Description("Тимирязевский")]
        [EnumCode("24")]
        [ShortTitle("")]
        Timiryazevsky = 938,
		/// <summary>
		/// Ховрино (939)
		/// </summary>
		[Description("Ховрино")]
        [EnumCode("25")]
        [ShortTitle("")]
        Khovrino = 939,
		/// <summary>
		/// Хорошёвский (940)
		/// </summary>
		[Description("Хорошёвский")]
        [EnumCode("26")]
        [ShortTitle("")]
        Khoroshyovsky = 940,
    }
}

namespace ObjectModel.Directory
{
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
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// Исходный (783)
		/// </summary>
		[Description("Исходный")]
        [EnumCode("")]
        [ShortTitle("")]
        Initial = 783,
		/// <summary>
		/// Новый (784)
		/// </summary>
		[Description("Новый")]
        [EnumCode("")]
        [ShortTitle("")]
        New = 784,
		/// <summary>
		/// Пересчитанный (785)
		/// </summary>
		[Description("Пересчитанный")]
        [EnumCode("")]
        [ShortTitle("")]
        Recalculated = 785,
		/// <summary>
		/// Годовой (786)
		/// </summary>
		[Description("Годовой")]
        [EnumCode("")]
        [ShortTitle("")]
        Annual = 786,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Тип статьи (201)
    ///</summary>
    [ReferenceInfo(ReferenceId = 201)]
    public enum KoNoteType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Ежедневка (1)
		/// </summary>
		[Description("Ежедневка")]
        [EnumCode("1")]
        [ShortTitle("Ежедневка")]
        Day = 1,
		/// <summary>
		/// Обращение (2)
		/// </summary>
		[Description("Обращение")]
        [EnumCode("2")]
        [ShortTitle("Обращение")]
        Petition = 2,
		/// <summary>
		/// Годовые (3)
		/// </summary>
		[Description("Годовые")]
        [EnumCode("3")]
        [ShortTitle("Годовые")]
        Year = 3,
		/// <summary>
		/// Исходный перечень (4)
		/// </summary>
		[Description("Исходный перечень")]
        [EnumCode("4")]
        [ShortTitle("Исходный перечень")]
        Initial = 4,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Статусы заданий на оценку (202)
    ///</summary>
    [ReferenceInfo(ReferenceId = 202)]
    public enum KoTaskStatus : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// В работе (1)
		/// </summary>
		[Description("В работе")]
        [EnumCode("1")]
        [ShortTitle("В работе")]
        InWork = 1,
		/// <summary>
		/// Готово (2)
		/// </summary>
		[Description("Готово")]
        [EnumCode("2")]
        [ShortTitle("Готово")]
        Ready = 2,
    }
}

namespace ObjectModel.Directory
{
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
        [ShortTitle("")]
        None = 0,

    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Механизм группировки (204)
    ///</summary>
    [ReferenceInfo(ReferenceId = 204)]
    public enum KoGroupAlgoritm : long
    {
		/// <summary>
		/// Моделирование (1)
		/// </summary>
		[Description("Моделирование")]
        [EnumCode("1")]
        [ShortTitle("Моделирование")]
        Model = 1,
		/// <summary>
		/// Затратный (2)
		/// </summary>
		[Description("Затратный")]
        [EnumCode("2")]
        [ShortTitle("Затратный")]
        Cost = 2,
		/// <summary>
		/// Нормативный (3)
		/// </summary>
		[Description("Нормативный")]
        [EnumCode("3")]
        [ShortTitle("Нормативный")]
        Normative = 3,
		/// <summary>
		/// Здания по помещениям (8)
		/// </summary>
		[Description("Здания по помещениям")]
        [EnumCode("8")]
        [ShortTitle("Здания по помещениям")]
        BuildingOnFlat = 8,
		/// <summary>
		/// Помещения по зданиям (9)
		/// </summary>
		[Description("Помещения по зданиям")]
        [EnumCode("9")]
        [ShortTitle("Помещения по зданиям")]
        FlatOnBuilding = 9,
		/// <summary>
		/// Среднее (10)
		/// </summary>
		[Description("Среднее")]
        [EnumCode("10")]
        [ShortTitle("Среднее")]
        AVG = 10,
		/// <summary>
		/// ОНС (11)
		/// </summary>
		[Description("ОНС")]
        [EnumCode("11")]
        [ShortTitle("ОНС")]
        UnComplited = 11,
		/// <summary>
		/// Минимальное (12)
		/// </summary>
		[Description("Минимальное")]
        [EnumCode("12")]
        [ShortTitle("Минимальное")]
        Min = 12,
		/// <summary>
		/// Эталонный (13)
		/// </summary>
		[Description("Эталонный")]
        [EnumCode("13")]
        [ShortTitle("Эталонный")]
        Etalon = 13,
		/// <summary>
		/// Основная группа ОКС (98)
		/// </summary>
		[Description("Основная группа ОКС")]
        [EnumCode("98")]
        [ShortTitle("Основная группа ОКС")]
        MainOKS = 98,
		/// <summary>
		/// Основная группа Участки (99)
		/// </summary>
		[Description("Основная группа Участки")]
        [EnumCode("99")]
        [ShortTitle("Основная группа Участки")]
        MainParcel = 99,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Алгоритм рассчёта (205)
    ///</summary>
    [ReferenceInfo(ReferenceId = 205)]
    public enum KoAlgoritmType : long
    {
		/// <summary>
		/// Экспоненциальная (1)
		/// </summary>
		[Description("Экспоненциальная")]
        [EnumCode("1")]
        [ShortTitle("Экспоненциальная")]
        Exp = 1,
		/// <summary>
		/// Линейная (2)
		/// </summary>
		[Description("Линейная")]
        [EnumCode("2")]
        [ShortTitle("Линейная")]
        Line = 2,
		/// <summary>
		/// Мультипликативная (3)
		/// </summary>
		[Description("Мультипликативная")]
        [EnumCode("3")]
        [ShortTitle("Мультипликативная")]
        Multi = 3,
    }
}

namespace ObjectModel.Directory
{
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
        [ShortTitle("")]
        None = 0,

    }
}

namespace ObjectModel.Directory
{
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
        [ShortTitle("")]
        None = 0,

    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Результат анализа стоимости (208)
    ///</summary>
    [ReferenceInfo(ReferenceId = 208)]
    public enum KoStatusResultCalc : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// Стоимость не изменилась (787)
		/// </summary>
		[Description("Стоимость не изменилась")]
        [EnumCode("")]
        [ShortTitle("")]
        CostNotChanged = 787,
		/// <summary>
		/// Стоимость изменилась (788)
		/// </summary>
		[Description("Стоимость изменилась")]
        [EnumCode("")]
        [ShortTitle("")]
        CostChanged = 788,
		/// <summary>
		/// Неверный расчет (техническая ошибка) (789)
		/// </summary>
		[Description("Неверный расчет (техническая ошибка)")]
        [EnumCode("")]
        [ShortTitle("")]
        ErrorTechnical = 789,
		/// <summary>
		/// Неверный расчет (ошибка в данных) (790)
		/// </summary>
		[Description("Неверный расчет (ошибка в данных)")]
        [EnumCode("")]
        [ShortTitle("")]
        ErrorInData = 790,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Cтатус расчета (209)
    ///</summary>
    [ReferenceInfo(ReferenceId = 209)]
    public enum KoStatusRepeatCalc : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// Исходный (791)
		/// </summary>
		[Description("Исходный")]
        [EnumCode("")]
        [ShortTitle("")]
        Initial = 791,
		/// <summary>
		/// Новый (792)
		/// </summary>
		[Description("Новый")]
        [EnumCode("")]
        [ShortTitle("")]
        New = 792,
		/// <summary>
		/// Повторный (793)
		/// </summary>
		[Description("Повторный")]
        [EnumCode("")]
        [ShortTitle("")]
        Repeated = 793,
		/// <summary>
		/// Повторный (исходный) (794)
		/// </summary>
		[Description("Повторный (исходный)")]
        [EnumCode("")]
        [ShortTitle("")]
        RepeatedInitial = 794,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Тип объекта, по которому было рассчитано среднее/минимальное значение (210)
    ///</summary>
    [ReferenceInfo(ReferenceId = 210)]
    public enum KoParentCalcType : long
    {
		/// <summary>
		/// значение отсутствует (0)
		/// </summary>
		[Description("значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("значение отсутствует")]
        None = 0,
		/// <summary>
		/// кадастровый квартал (1)
		/// </summary>
		[Description("кадастровый квартал")]
        [EnumCode("1")]
        [ShortTitle("кадастровый квартал")]
        CadastralBlock = 1,
		/// <summary>
		/// кадастровый район (2)
		/// </summary>
		[Description("кадастровый район")]
        [EnumCode("2")]
        [ShortTitle("кадастровый район")]
        CadastralRegion = 2,
		/// <summary>
		/// субъект РФ (3)
		/// </summary>
		[Description("субъект РФ")]
        [EnumCode("3")]
        [ShortTitle("субъект РФ")]
        RfSubject = 3,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Тип помещения (211)
    ///</summary>
    [ReferenceInfo(ReferenceId = 211)]
    public enum KoTypeOfRoom : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Жилое (1)
		/// </summary>
		[Description("Жилое")]
        [EnumCode("1")]
        [ShortTitle("Жилое")]
        Residential = 1,
		/// <summary>
		/// Не жилое (2)
		/// </summary>
		[Description("Не жилое")]
        [EnumCode("2")]
        [ShortTitle("Не жилое")]
        NotResidential = 2,
    }
}

namespace ObjectModel.Directory.Sud
{
    /// <summary>
    /// Тип объекта (300)
    ///</summary>
    [ReferenceInfo(ReferenceId = 300)]
    public enum SudObjectType : long
    {
		/// <summary>
		/// Участок (1)
		/// </summary>
		[Description("Участок")]
        [EnumCode("1")]
        [ShortTitle("Участок")]
        Site = 1,
		/// <summary>
		/// Здание (2)
		/// </summary>
		[Description("Здание")]
        [EnumCode("2")]
        [ShortTitle("Здание")]
        Building = 2,
		/// <summary>
		/// Помещение (3)
		/// </summary>
		[Description("Помещение")]
        [EnumCode("3")]
        [ShortTitle("Помещение")]
        Room = 3,
		/// <summary>
		/// Сооружение (4)
		/// </summary>
		[Description("Сооружение")]
        [EnumCode("4")]
        [ShortTitle("Сооружение")]
        Construction = 4,
		/// <summary>
		/// Онс (5)
		/// </summary>
		[Description("Онс")]
        [EnumCode("5")]
        [ShortTitle("Онс")]
        Ons = 5,
		/// <summary>
		/// Машиноместо (6)
		/// </summary>
		[Description("Машиноместо")]
        [EnumCode("6")]
        [ShortTitle("Машиноместо")]
        ParkingPlace = 6,
		/// <summary>
		/// Нет данных (7)
		/// </summary>
		[Description("Нет данных")]
        [EnumCode("7")]
        [ShortTitle("Нет данных")]
        None = 7,
    }
}

namespace ObjectModel.Directory.Sud
{
    /// <summary>
    /// Статус обработки (301)
    ///</summary>
    [ReferenceInfo(ReferenceId = 301)]
    public enum ProcessingStatus : long
    {
		/// <summary>
		/// В работе (0)
		/// </summary>
		[Description("В работе")]
        [EnumCode("0")]
        [ShortTitle("В работе")]
        InWork = 0,
		/// <summary>
		/// Актуальный (1)
		/// </summary>
		[Description("Актуальный")]
        [EnumCode("1")]
        [ShortTitle("Актуальный")]
        Processed = 1,
    }
}

namespace ObjectModel.Directory.Sud
{
    /// <summary>
    /// Тип заявителя (302)
    ///</summary>
    [ReferenceInfo(ReferenceId = 302)]
    public enum ApplicantType : long
    {
		/// <summary>
		/// Физическое лицо (1)
		/// </summary>
		[Description("Физическое лицо")]
        [EnumCode("1")]
        [ShortTitle("Физическое лицо")]
        Individual = 1,
		/// <summary>
		/// Юридическое лицо (2)
		/// </summary>
		[Description("Юридическое лицо")]
        [EnumCode("2")]
        [ShortTitle("Юридическое лицо")]
        Entity = 2,
    }
}

namespace ObjectModel.Directory.Sud
{
    /// <summary>
    /// Форма собственности (303)
    ///</summary>
    [ReferenceInfo(ReferenceId = 303)]
    public enum TypeOfOwnership : long
    {
		/// <summary>
		/// Федеральное имущество (1)
		/// </summary>
		[Description("Федеральное имущество")]
        [EnumCode("1")]
        [ShortTitle("Федеральное имущество")]
        FederalProperty = 1,
		/// <summary>
		/// Город Москва (2)
		/// </summary>
		[Description("Город Москва")]
        [EnumCode("2")]
        [ShortTitle("Город Москва")]
        MoscowCity = 2,
		/// <summary>
		/// Иное (3)
		/// </summary>
		[Description("Иное")]
        [EnumCode("3")]
        [ShortTitle("Иное")]
        Other = 3,
    }
}

namespace ObjectModel.Directory.Sud
{
    /// <summary>
    /// Статус судебного решения (304)
    ///</summary>
    [ReferenceInfo(ReferenceId = 304)]
    public enum CourtStatus : long
    {
		/// <summary>
		/// Без статуса (0)
		/// </summary>
		[Description("Без статуса")]
        [EnumCode("0")]
        [ShortTitle("Без статуса")]
        None = 0,
		/// <summary>
		/// Отказано (1)
		/// </summary>
		[Description("Отказано")]
        [EnumCode("1")]
        [ShortTitle("Отказано")]
        Denied = 1,
		/// <summary>
		/// Удовлетворено (2)
		/// </summary>
		[Description("Удовлетворено")]
        [EnumCode("2")]
        [ShortTitle("Удовлетворено")]
        Satisfied = 2,
		/// <summary>
		/// Приостановлено (3)
		/// </summary>
		[Description("Приостановлено")]
        [EnumCode("3")]
        [ShortTitle("Приостановлено")]
        Paused = 3,
		/// <summary>
		/// Частично удовлетворено (4)
		/// </summary>
		[Description("Частично удовлетворено")]
        [EnumCode("4")]
        [ShortTitle("Частично удовлетворено")]
        PartiallySatisfied = 4,
    }
}

namespace ObjectModel.Directory.Commission
{
    /// <summary>
    /// Тип комиссии (400)
    ///</summary>
    [ReferenceInfo(ReferenceId = 400)]
    public enum CommissionType : long
    {
		/// <summary>
		/// По установлению кадастровой стоимости (1)
		/// </summary>
		[Description("По установлению кадастровой стоимости")]
        [EnumCode("1")]
        [ShortTitle("По установлению кадастровой стоимости")]
        OnSetCadCost = 1,
		/// <summary>
		/// По недостоверности (2)
		/// </summary>
		[Description("По недостоверности")]
        [EnumCode("2")]
        [ShortTitle("По недостоверности")]
        OnUnreliability = 2,
    }
}

namespace ObjectModel.Directory.Commission
{
    /// <summary>
    /// Статус заявителя (401)
    ///</summary>
    [ReferenceInfo(ReferenceId = 401)]
    public enum ApplicantStatus : long
    {
		/// <summary>
		/// ФЛ (0)
		/// </summary>
		[Description("ФЛ")]
        [EnumCode("0")]
        [ShortTitle("ФЛ")]
        FL = 0,
		/// <summary>
		/// ЮЛ (1)
		/// </summary>
		[Description("ЮЛ")]
        [EnumCode("1")]
        [ShortTitle("ЮЛ")]
        UL = 1,
		/// <summary>
		/// ДГИ (2)
		/// </summary>
		[Description("ДГИ")]
        [EnumCode("2")]
        [ShortTitle("ДГИ")]
        DGI = 2,
		/// <summary>
		/// ИП (3)
		/// </summary>
		[Description("ИП")]
        [EnumCode("3")]
        [ShortTitle("ИП")]
        IP = 3,
		/// <summary>
		/// ОГВ (4)
		/// </summary>
		[Description("ОГВ")]
        [EnumCode("4")]
        [ShortTitle("ОГВ")]
        OGV = 4,
		/// <summary>
		/// ФЛ, ЮЛ (5)
		/// </summary>
		[Description("ФЛ, ЮЛ")]
        [EnumCode("5")]
        [ShortTitle("ФЛ, ЮЛ")]
        FlUl = 5,
		/// <summary>
		/// ФЛ, ЮЛ, ИП (6)
		/// </summary>
		[Description("ФЛ, ЮЛ, ИП")]
        [EnumCode("6")]
        [ShortTitle("ФЛ, ЮЛ, ИП")]
        FlUlIp = 6,
		/// <summary>
		/// Нет данных (7)
		/// </summary>
		[Description("Нет данных")]
        [EnumCode("7")]
        [ShortTitle("Нет данных")]
        NoData = 7,
    }
}

namespace ObjectModel.Directory.Commission
{
    /// <summary>
    /// Решение комиссии (402)
    ///</summary>
    [ReferenceInfo(ReferenceId = 402)]
    public enum DecisionResult : long
    {
		/// <summary>
		/// Положительное решение (1)
		/// </summary>
		[Description("Положительное решение")]
        [EnumCode("1")]
        [ShortTitle("Положительное решение")]
        Approved = 1,
		/// <summary>
		/// Отказано (2)
		/// </summary>
		[Description("Отказано")]
        [EnumCode("2")]
        [ShortTitle("Отказано")]
        Rejected = 2,
    }
}

namespace ObjectModel.Directory.Declarations
{
    /// <summary>
    /// Наличие характеристики (500)
    ///</summary>
    [ReferenceInfo(ReferenceId = 500)]
    public enum HarAvailability : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// отсутствует (1)
		/// </summary>
		[Description("отсутствует")]
        [EnumCode("1")]
        [ShortTitle("отсутствует")]
        NotExists = 1,
		/// <summary>
		/// имеется (2)
		/// </summary>
		[Description("имеется")]
        [EnumCode("2")]
        [ShortTitle("имеется")]
        Exists = 2,
    }
}

namespace ObjectModel.Directory.Declarations
{
    /// <summary>
    /// Статус книги (501)
    ///</summary>
    [ReferenceInfo(ReferenceId = 501)]
    public enum BookStatus : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// В работе (1)
		/// </summary>
		[Description("В работе")]
        [EnumCode("1")]
        [ShortTitle("В работе")]
        InWork = 1,
		/// <summary>
		/// Закрыто (2)
		/// </summary>
		[Description("Закрыто")]
        [EnumCode("2")]
        [ShortTitle("Закрыто")]
        Closed = 2,
    }
}

namespace ObjectModel.Directory.Declarations
{
    /// <summary>
    /// Тип книги (502)
    ///</summary>
    [ReferenceInfo(ReferenceId = 502)]
    public enum BookType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Книга деклараций (1)
		/// </summary>
		[Description("Книга деклараций")]
        [EnumCode("1")]
        [ShortTitle("Книга деклараций")]
        Declarations = 1,
		/// <summary>
		/// Книга уведомлений (2)
		/// </summary>
		[Description("Книга уведомлений")]
        [EnumCode("2")]
        [ShortTitle("Книга уведомлений")]
        Notifications = 2,
    }
}

namespace ObjectModel.Directory.Declarations
{
    /// <summary>
    /// Тип субъекта деклараций (503)
    ///</summary>
    [ReferenceInfo(ReferenceId = 503)]
    public enum SubjectType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Физлицо (1)
		/// </summary>
		[Description("Физлицо")]
        [EnumCode("1")]
        [ShortTitle("Физлицо")]
        Fl = 1,
		/// <summary>
		/// Юрлицо (2)
		/// </summary>
		[Description("Юрлицо")]
        [EnumCode("2")]
        [ShortTitle("Юрлицо")]
        Ul = 2,
    }
}

namespace ObjectModel.Directory.Declarations
{
    /// <summary>
    /// Тип уведомления (504)
    ///</summary>
    [ReferenceInfo(ReferenceId = 504)]
    public enum UvedType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Уведомление о принятии декларации (1)
		/// </summary>
		[Description("Уведомление о принятии декларации")]
        [EnumCode("1")]
        [ShortTitle("Уведомление о принятии декларации")]
        Item1 = 1,
		/// <summary>
		/// Уведомление об учете информации из декларации (3)
		/// </summary>
		[Description("Уведомление об учете информации из декларации")]
        [EnumCode("3")]
        [ShortTitle("Уведомление об учете информации из декларации")]
        Item3 = 3,
		/// <summary>
		/// Уведомление об отказе в учете информации из декларации (4)
		/// </summary>
		[Description("Уведомление об отказе в учете информации из декларации")]
        [EnumCode("4")]
        [ShortTitle("Уведомление об отказе в учете информации из декларации")]
        Item4 = 4,
		/// <summary>
		/// Уведомление об отказе в рассмотрении декларации и возврате документов (5)
		/// </summary>
		[Description("Уведомление об отказе в рассмотрении декларации и возврате документов")]
        [EnumCode("5")]
        [ShortTitle("Уведомление об отказе в рассмотрении декларации и возврате документов")]
        Item5 = 5,
    }
}

namespace ObjectModel.Directory.Declarations
{
    /// <summary>
    /// Статус результата (505)
    ///</summary>
    [ReferenceInfo(ReferenceId = 505)]
    public enum StatusDec : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Отказ в рассмотрении (1)
		/// </summary>
		[Description("Отказ в рассмотрении")]
        [EnumCode("1")]
        [ShortTitle("Отказ в рассмотрении")]
        Rejection = 1,
		/// <summary>
		/// Принято на рассмотрение (2)
		/// </summary>
		[Description("Принято на рассмотрение")]
        [EnumCode("2")]
        [ShortTitle("Принято на рассмотрение")]
        Accepted = 2,
		/// <summary>
		/// Рассмотрено (3)
		/// </summary>
		[Description("Рассмотрено")]
        [EnumCode("3")]
        [ShortTitle("Рассмотрено")]
        Considered = 3,
    }
}

namespace ObjectModel.Directory.Declarations
{
    /// <summary>
    /// Тип объекта декларации (506)
    ///</summary>
    [ReferenceInfo(ReferenceId = 506)]
    public enum ObjectType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Земельный участок (1)
		/// </summary>
		[Description("Земельный участок")]
        [EnumCode("1")]
        [ShortTitle("Земельный участок")]
        Site = 1,
		/// <summary>
		/// Здание (2)
		/// </summary>
		[Description("Здание")]
        [EnumCode("2")]
        [ShortTitle("Здание")]
        Building = 2,
		/// <summary>
		/// Помещение (3)
		/// </summary>
		[Description("Помещение")]
        [EnumCode("3")]
        [ShortTitle("Помещение")]
        Room = 3,
		/// <summary>
		/// Сооружение (4)
		/// </summary>
		[Description("Сооружение")]
        [EnumCode("4")]
        [ShortTitle("Сооружение")]
        Construction = 4,
		/// <summary>
		/// Машино-место (5)
		/// </summary>
		[Description("Машино-место")]
        [EnumCode("5")]
        [ShortTitle("Машино-место")]
        ParkingPlace = 5,
		/// <summary>
		/// ОНС (6)
		/// </summary>
		[Description("ОНС")]
        [EnumCode("6")]
        [ShortTitle("ОНС")]
        Ons = 6,
		/// <summary>
		/// Единый недвижимый комплекс (7)
		/// </summary>
		[Description("Единый недвижимый комплекс")]
        [EnumCode("7")]
        [ShortTitle("Единый недвижимый комплекс")]
        Ens = 7,
		/// <summary>
		/// Производственно-имущественный комплекс (8)
		/// </summary>
		[Description("Производственно-имущественный комплекс")]
        [EnumCode("8")]
        [ShortTitle("Производственно-имущественный комплекс")]
        Pik = 8,
		/// <summary>
		/// Иное (9)
		/// </summary>
		[Description("Иное")]
        [EnumCode("9")]
        [ShortTitle("Иное")]
        Other = 9,
    }
}

namespace ObjectModel.Directory.Declarations
{
    /// <summary>
    /// Статус проверки (507)
    ///</summary>
    [ReferenceInfo(ReferenceId = 507)]
    public enum CheckStatus : long
    {
		/// <summary>
		/// В работе (0)
		/// </summary>
		[Description("В работе")]
        [EnumCode("0")]
        [ShortTitle("В работе")]
        InWork = 0,
		/// <summary>
		/// Да (1)
		/// </summary>
		[Description("Да")]
        [EnumCode("1")]
        [ShortTitle("Да")]
        Yes = 1,
		/// <summary>
		/// Нет (2)
		/// </summary>
		[Description("Нет")]
        [EnumCode("2")]
        [ShortTitle("Нет")]
        No = 2,
    }
}

namespace ObjectModel.Directory.Declarations
{
    /// <summary>
    /// Цель декларации (508)
    ///</summary>
    [ReferenceInfo(ReferenceId = 508)]
    public enum DeclarationPurpose : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Декларация подается с целью доведения информации о характеристиках объекта недвижимости (1)
		/// </summary>
		[Description("Декларация подается с целью доведения информации о характеристиках объекта недвижимости")]
        [EnumCode("1")]
        [ShortTitle("Декларация подается с целью доведения информации о характеристиках объекта недвижимости")]
        Item1 = 1,
		/// <summary>
		/// Декларация подается с целью предоставления отчета об определении рыночной стоимости объекта недвижимости (2)
		/// </summary>
		[Description("Декларация подается с целью предоставления отчета об определении рыночной стоимости объекта недвижимости")]
        [EnumCode("2")]
        [ShortTitle("Декларация подается с целью предоставления отчета об определении рыночной стоимости объекта недвижимости")]
        Item2 = 2,
    }
}

namespace ObjectModel.Directory.Declarations
{
    /// <summary>
    /// Тип отправки уведомления (509)
    ///</summary>
    [ReferenceInfo(ReferenceId = 509)]
    public enum SendUvedType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Нет (1)
		/// </summary>
		[Description("Нет")]
        [EnumCode("1")]
        [ShortTitle("Нет")]
        No = 1,
		/// <summary>
		/// На почтовый адрес (2)
		/// </summary>
		[Description("На почтовый адрес")]
        [EnumCode("2")]
        [ShortTitle("На почтовый адрес")]
        Post = 2,
		/// <summary>
		/// На электронный адрес (3)
		/// </summary>
		[Description("На электронный адрес")]
        [EnumCode("3")]
        [ShortTitle("На электронный адрес")]
        Email = 3,
		/// <summary>
		/// На руки (4)
		/// </summary>
		[Description("На руки")]
        [EnumCode("4")]
        [ShortTitle("На руки")]
        OnHands = 4,
    }
}

namespace ObjectModel.Directory.Declarations
{
    /// <summary>
    /// Тип владельца (510)
    ///</summary>
    [ReferenceInfo(ReferenceId = 510)]
    public enum OwnerType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Заявитель (правообладатель) (1)
		/// </summary>
		[Description("Заявитель (правообладатель)")]
        [EnumCode("1")]
        [ShortTitle("Заявитель (правообладатель)")]
        Item1 = 1,
		/// <summary>
		/// Представитель заявителя (2)
		/// </summary>
		[Description("Представитель заявителя")]
        [EnumCode("2")]
        [ShortTitle("Представитель заявителя")]
        Item2 = 2,
    }
}

namespace ObjectModel.Directory.Declarations
{
    /// <summary>
    /// Тип причины отказа для уведомлления об отказе декларации (511)
    ///</summary>
    [ReferenceInfo(ReferenceId = 511)]
    public enum RejectionReasonType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Заявитель, подавший декларацию, не является правообладателем объекта недвижимости, в отношении которого подается декларация (1)
		/// </summary>
		[Description("Заявитель, подавший декларацию, не является правообладателем объекта недвижимости, в отношении которого подается декларация")]
        [EnumCode("1")]
        [ShortTitle("Заявитель, подавший декларацию, не является правообладателем объекта недвижимости, в отношении которого подается декларация")]
        ApplicantIsNotObjectTypeOwner = 1,
		/// <summary>
		/// К декларации не приложены документы, предусмотренные пунктом 2 Приказа о декларациях (2)
		/// </summary>
		[Description("К декларации не приложены документы, предусмотренные пунктом 2 Приказа о декларациях")]
        [EnumCode("2")]
        [ShortTitle("К декларации не приложены документы, предусмотренные пунктом 2 Приказа о декларациях")]
        DocumentsAreNotAttached = 2,
		/// <summary>
		/// Декларация не соответствует форме, предусмотренной приложением № 2 к Приказу о декларациях (3)
		/// </summary>
		[Description("Декларация не соответствует форме, предусмотренной приложением № 2 к Приказу о декларациях")]
        [EnumCode("3")]
        [ShortTitle("Декларация не соответствует форме, предусмотренной приложением № 2 к Приказу о декларациях")]
        DeclarationDoesNotMatchForm = 3,
		/// <summary>
		/// Декларация не заверена в соответствии с пунктом 3 Приказа о декларациях (4)
		/// </summary>
		[Description("Декларация не заверена в соответствии с пунктом 3 Приказа о декларациях")]
        [EnumCode("4")]
        [ShortTitle("Декларация не заверена в соответствии с пунктом 3 Приказа о декларациях")]
        DeclarationIsNotCertified  = 4,
		/// <summary>
		/// Декларация и прилагаемые к ней документы представлены не в соответствии с требованиями, предусмотренными пунктом 4 Приказа о декларациях (5)
		/// </summary>
		[Description("Декларация и прилагаемые к ней документы представлены не в соответствии с требованиями, предусмотренными пунктом 4 Приказа о декларациях")]
        [EnumCode("5")]
        [ShortTitle("Декларация и прилагаемые к ней документы представлены не в соответствии с требованиями, предусмотренными пунктом 4 Приказа о декларациях")]
        DeclarationAndDocumentsDoNotMeetRequirements = 5,
		/// <summary>
		/// Иное (вручную) (6)
		/// </summary>
		[Description("Иное (вручную)")]
        [EnumCode("6")]
        [ShortTitle("Иное (вручную)")]
        Other = 6,
    }
}

namespace ObjectModel.Directory.Declarations
{
    /// <summary>
    /// Статус характеристики (512)
    ///</summary>
    [ReferenceInfo(ReferenceId = 512)]
    public enum HarStatus : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Принято (1)
		/// </summary>
		[Description("Принято")]
        [EnumCode("1")]
        [ShortTitle("Принято")]
        Accepted = 1,
		/// <summary>
		/// Не принято (2)
		/// </summary>
		[Description("Не принято")]
        [EnumCode("2")]
        [ShortTitle("Не принято")]
        Rejected = 2,
    }
}

namespace ObjectModel.Directory.Common
{
    /// <summary>
    /// Статус импорта данных (800)
    ///</summary>
    [ReferenceInfo(ReferenceId = 800)]
    public enum ImportStatus : long
    {
		/// <summary>
		/// Создана (0)
		/// </summary>
		[Description("Создана")]
        [EnumCode("0")]
        [ShortTitle("Создана")]
        Added = 0,
		/// <summary>
		/// В работе (1)
		/// </summary>
		[Description("В работе")]
        [EnumCode("1")]
        [ShortTitle("В работе")]
        Running = 1,
		/// <summary>
		/// Завершено (2)
		/// </summary>
		[Description("Завершено")]
        [EnumCode("2")]
        [ShortTitle("Завершено")]
        Completed = 2,
		/// <summary>
		/// Ошибка (3)
		/// </summary>
		[Description("Ошибка")]
        [EnumCode("3")]
        [ShortTitle("Ошибка")]
        Faulted = 3,
    }
}

namespace ObjectModel.Directory.Common
{
    /// <summary>
    /// Тип формы (801)
    ///</summary>
    [ReferenceInfo(ReferenceId = 801)]
    public enum DataFormStorege : long
    {
		/// <summary>
		/// Нормализация (1)
		/// </summary>
		[Description("Нормализация")]
        [EnumCode("1")]
        [ShortTitle("Нормализация")]
        Normalisation = 1,
		/// <summary>
		/// Гармонизация (2)
		/// </summary>
		[Description("Гармонизация")]
        [EnumCode("2")]
        [ShortTitle("Гармонизация")]
        Harmonization = 2,
		/// <summary>
		/// Гармонизация по классификатору ЦОД (3)
		/// </summary>
		[Description("Гармонизация по классификатору ЦОД")]
        [EnumCode("3")]
        [ShortTitle("Гармонизация по классификатору ЦОД")]
        HarmonizationCOD = 3,
		/// <summary>
		/// Выборка из справочника ЦОД (4)
		/// </summary>
		[Description("Выборка из справочника ЦОД")]
        [EnumCode("4")]
        [ShortTitle("Выборка из справочника ЦОД")]
        UnloadingFromDict = 4,
    }
}

