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
		/// <summary>
		/// Кадастровый квартал (2190)
		/// </summary>
		[Description("Кадастровый квартал")]
        [EnumCode("002001010002")]
        [ShortTitle("")]
        CadastralQuartal = 2190,
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
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Квартира (1)
		/// </summary>
		[Description("Квартира")]
        [EnumCode("1")]
        [ShortTitle("Квартира")]
        Flat = 1,
		/// <summary>
		/// Койко-место (2)
		/// </summary>
		[Description("Койко-место")]
        [EnumCode("2")]
        [ShortTitle("Койко-место")]
        Bed = 2,
		/// <summary>
		/// Комната (3)
		/// </summary>
		[Description("Комната")]
        [EnumCode("3")]
        [ShortTitle("Комната")]
        Room = 3,
		/// <summary>
		/// Дом/дача (4)
		/// </summary>
		[Description("Дом/дача")]
        [EnumCode("4")]
        [ShortTitle("Дом/дача")]
        House = 4,
		/// <summary>
		/// Коттедж (5)
		/// </summary>
		[Description("Коттедж")]
        [EnumCode("5")]
        [ShortTitle("Коттедж")]
        Cottage = 5,
		/// <summary>
		/// Таунхаус (6)
		/// </summary>
		[Description("Таунхаус")]
        [EnumCode("6")]
        [ShortTitle("Таунхаус")]
        Townhouse = 6,
		/// <summary>
		/// Часть дома (7)
		/// </summary>
		[Description("Часть дома")]
        [EnumCode("7")]
        [ShortTitle("Часть дома")]
        HouseShare = 7,
		/// <summary>
		/// Гараж (8)
		/// </summary>
		[Description("Гараж")]
        [EnumCode("8")]
        [ShortTitle("Гараж")]
        Garage = 8,
		/// <summary>
		/// Готовый бизнес (9)
		/// </summary>
		[Description("Готовый бизнес")]
        [EnumCode("9")]
        [ShortTitle("Готовый бизнес")]
        Business = 9,
		/// <summary>
		/// Здание (10)
		/// </summary>
		[Description("Здание")]
        [EnumCode("10")]
        [ShortTitle("Здание")]
        Building = 10,
		/// <summary>
		/// Коммерческая земля (11)
		/// </summary>
		[Description("Коммерческая земля")]
        [EnumCode("11")]
        [ShortTitle("Коммерческая земля")]
        CommercialLand = 11,
		/// <summary>
		/// Офис (12)
		/// </summary>
		[Description("Офис")]
        [EnumCode("12")]
        [ShortTitle("Офис")]
        Office = 12,
		/// <summary>
		/// Помещение свободного назначения (13)
		/// </summary>
		[Description("Помещение свободного назначения")]
        [EnumCode("13")]
        [ShortTitle("Помещение свободного назначения")]
        FreeAppointmentObject = 13,
		/// <summary>
		/// Производство (14)
		/// </summary>
		[Description("Производство")]
        [EnumCode("14")]
        [ShortTitle("Производство")]
        Industry = 14,
		/// <summary>
		/// Склад (15)
		/// </summary>
		[Description("Склад")]
        [EnumCode("15")]
        [ShortTitle("Склад")]
        Warehouse = 15,
		/// <summary>
		/// Торговая площадь (16)
		/// </summary>
		[Description("Торговая площадь")]
        [EnumCode("16")]
        [ShortTitle("Торговая площадь")]
        ShoppingArea = 16,
		/// <summary>
		/// Доля в квартире (17)
		/// </summary>
		[Description("Доля в квартире")]
        [EnumCode("17")]
        [ShortTitle("Доля в квартире")]
        FlatShare = 17,
		/// <summary>
		/// Квартира в новостройке (17)
		/// </summary>
		[Description("Квартира в новостройке")]
        [EnumCode("17")]
        [ShortTitle("Квартира в новостройке")]
        NewBuildingFlat = 17,
		/// <summary>
		/// Участок (18)
		/// </summary>
		[Description("Участок")]
        [EnumCode("18")]
        [ShortTitle("Участок")]
        Land = 18,
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
		/// <summary>
		/// Бетонные (1079)
		/// </summary>
		[Description("Бетонные")]
        [EnumCode("5")]
        [ShortTitle("")]
        Betonnie = 1079,
		/// <summary>
		/// Бетонные, Деревянные (1080)
		/// </summary>
		[Description("Бетонные, Деревянные")]
        [EnumCode("6")]
        [ShortTitle("")]
        BetonnieDerevyannie = 1080,
		/// <summary>
		/// Бетонные, Деревянные, Из прочих материалов (1081)
		/// </summary>
		[Description("Бетонные, Деревянные, Из прочих материалов")]
        [EnumCode("7")]
        [ShortTitle("")]
        BetonnieDerevyannieIzProchihMaterialov = 1081,
		/// <summary>
		/// Бетонные, Деревянные, Металлические, Из прочих материалов (1082)
		/// </summary>
		[Description("Бетонные, Деревянные, Металлические, Из прочих материалов")]
        [EnumCode("8")]
        [ShortTitle("")]
        BetonnieDerevyannieMetallicheskieIzProchihMaterialov = 1082,
		/// <summary>
		/// Бетонные, Железобетонные (1083)
		/// </summary>
		[Description("Бетонные, Железобетонные")]
        [EnumCode("9")]
        [ShortTitle("")]
        BetonnieZhelezobetonnie = 1083,
		/// <summary>
		/// Бетонные, Железобетонные, Смешанные, Из прочих материалов (1084)
		/// </summary>
		[Description("Бетонные, Железобетонные, Смешанные, Из прочих материалов")]
        [EnumCode("10")]
        [ShortTitle("")]
        BetonnieZhelezobetonnieSmeshannieIzProchihMaterialov = 1084,
		/// <summary>
		/// Бетонные, Из легкобетонных панелей, Железобетонные (1085)
		/// </summary>
		[Description("Бетонные, Из легкобетонных панелей, Железобетонные")]
        [EnumCode("11")]
        [ShortTitle("")]
        BetonnieIzLegkobetonnihPanelejZhelezobetonnie = 1085,
		/// <summary>
		/// Бетонные, Из мелких бетонных блоков (1086)
		/// </summary>
		[Description("Бетонные, Из мелких бетонных блоков")]
        [EnumCode("12")]
        [ShortTitle("")]
        BetonnieIzMelkihBetonnihBlokov = 1086,
		/// <summary>
		/// Бетонные, Из мелких бетонных блоков, Крупнопанельные (1087)
		/// </summary>
		[Description("Бетонные, Из мелких бетонных блоков, Крупнопанельные")]
        [EnumCode("13")]
        [ShortTitle("")]
        BetonnieIzMelkihBetonnihBlokovKrupnopaneljnie = 1087,
		/// <summary>
		/// Бетонные, Из прочих материалов (1088)
		/// </summary>
		[Description("Бетонные, Из прочих материалов")]
        [EnumCode("14")]
        [ShortTitle("")]
        BetonnieIzProchihMaterialov = 1088,
		/// <summary>
		/// Бетонные, Из прочих материалов, Смешанные (1089)
		/// </summary>
		[Description("Бетонные, Из прочих материалов, Смешанные")]
        [EnumCode("15")]
        [ShortTitle("")]
        BetonnieIzProchihMaterialovSmeshannie = 1089,
		/// <summary>
		/// Бетонные, Каменные, Кирпичные (1090)
		/// </summary>
		[Description("Бетонные, Каменные, Кирпичные")]
        [EnumCode("16")]
        [ShortTitle("")]
        BetonnieKamennieKirpichnie = 1090,
		/// <summary>
		/// Бетонные, Каменные, Смешанные (1091)
		/// </summary>
		[Description("Бетонные, Каменные, Смешанные")]
        [EnumCode("17")]
        [ShortTitle("")]
        BetonnieKamennieSmeshannie = 1091,
		/// <summary>
		/// Бетонные, Каркасно-обшивные (1092)
		/// </summary>
		[Description("Бетонные, Каркасно-обшивные")]
        [EnumCode("18")]
        [ShortTitle("")]
        BetonnieKarkasnoObshivnie = 1092,
		/// <summary>
		/// Бетонные, Кирпичные (1093)
		/// </summary>
		[Description("Бетонные, Кирпичные")]
        [EnumCode("19")]
        [ShortTitle("")]
        BetonnieKirpichnie = 1093,
		/// <summary>
		/// Бетонные, Кирпичные, Деревянные, Каменные (1094)
		/// </summary>
		[Description("Бетонные, Кирпичные, Деревянные, Каменные")]
        [EnumCode("20")]
        [ShortTitle("")]
        BetonnieKirpichnieDerevyannieKamennie = 1094,
		/// <summary>
		/// Бетонные, Кирпичные, Из мелких бетонных блоков (1095)
		/// </summary>
		[Description("Бетонные, Кирпичные, Из мелких бетонных блоков")]
        [EnumCode("21")]
        [ShortTitle("")]
        BetonnieKirpichnieIzMelkihBetonnihBlokov = 1095,
		/// <summary>
		/// Бетонные, Кирпичные, Каменные (1096)
		/// </summary>
		[Description("Бетонные, Кирпичные, Каменные")]
        [EnumCode("22")]
        [ShortTitle("")]
        BetonnieKirpichnieKamennie = 1096,
		/// <summary>
		/// Бетонные, Металлические (1097)
		/// </summary>
		[Description("Бетонные, Металлические")]
        [EnumCode("23")]
        [ShortTitle("")]
        BetonnieMetallicheskie = 1097,
		/// <summary>
		/// Бетонные, Металлические, Из прочих материалов (1098)
		/// </summary>
		[Description("Бетонные, Металлические, Из прочих материалов")]
        [EnumCode("24")]
        [ShortTitle("")]
        BetonnieMetallicheskieIzProchihMaterialov = 1098,
		/// <summary>
		/// Бетонные, Монолитные (1099)
		/// </summary>
		[Description("Бетонные, Монолитные")]
        [EnumCode("25")]
        [ShortTitle("")]
        BetonnieMonolitnie = 1099,
		/// <summary>
		/// Бетонные, Монолитные, Из прочих материалов (1100)
		/// </summary>
		[Description("Бетонные, Монолитные, Из прочих материалов")]
        [EnumCode("26")]
        [ShortTitle("")]
        BetonnieMonolitnieIzProchihMaterialov = 1100,
		/// <summary>
		/// Бетонные, Шлакобетонные (1101)
		/// </summary>
		[Description("Бетонные, Шлакобетонные")]
        [EnumCode("27")]
        [ShortTitle("")]
        BetonnieShlakobetonnie = 1101,
		/// <summary>
		/// Блочные (1102)
		/// </summary>
		[Description("Блочные")]
        [EnumCode("28")]
        [ShortTitle("")]
        Blochnie = 1102,
		/// <summary>
		/// Гипсобетонные (1103)
		/// </summary>
		[Description("Гипсобетонные")]
        [EnumCode("29")]
        [ShortTitle("")]
        Gipsobetonnie = 1103,
		/// <summary>
		/// Деревянные (1104)
		/// </summary>
		[Description("Деревянные")]
        [EnumCode("30")]
        [ShortTitle("")]
        Derevyannie = 1104,
		/// <summary>
		/// Деревянные брусовые (1105)
		/// </summary>
		[Description("Деревянные брусовые")]
        [EnumCode("31")]
        [ShortTitle("")]
        DerevyannieBrusovie = 1105,
		/// <summary>
		/// Деревянные, Бетонные (1106)
		/// </summary>
		[Description("Деревянные, Бетонные")]
        [EnumCode("32")]
        [ShortTitle("")]
        DerevyannieBetonnie = 1106,
		/// <summary>
		/// Деревянные, Бетонные, Металлические (1107)
		/// </summary>
		[Description("Деревянные, Бетонные, Металлические")]
        [EnumCode("33")]
        [ShortTitle("")]
        DerevyannieBetonnieMetallicheskie = 1107,
		/// <summary>
		/// Деревянные, Железобетонные (1108)
		/// </summary>
		[Description("Деревянные, Железобетонные")]
        [EnumCode("34")]
        [ShortTitle("")]
        DerevyannieZhelezobetonnie = 1108,
		/// <summary>
		/// Деревянные, Из мелких бетонных блоков (1109)
		/// </summary>
		[Description("Деревянные, Из мелких бетонных блоков")]
        [EnumCode("35")]
        [ShortTitle("")]
        DerevyannieIzMelkihBetonnihBlokov = 1109,
		/// <summary>
		/// Деревянные, Из мелких бетонных блоков, Кирпичные (1110)
		/// </summary>
		[Description("Деревянные, Из мелких бетонных блоков, Кирпичные")]
        [EnumCode("36")]
        [ShortTitle("")]
        DerevyannieIzMelkihBetonnihBlokovKirpichnie = 1110,
		/// <summary>
		/// Деревянные, Из прочих материалов (1111)
		/// </summary>
		[Description("Деревянные, Из прочих материалов")]
        [EnumCode("37")]
        [ShortTitle("")]
        DerevyannieIzProchihMaterialov = 1111,
		/// <summary>
		/// Деревянные, Из прочих материалов, Каменные (1112)
		/// </summary>
		[Description("Деревянные, Из прочих материалов, Каменные")]
        [EnumCode("38")]
        [ShortTitle("")]
        DerevyannieIzProchihMaterialovKamennie = 1112,
		/// <summary>
		/// Деревянные, Каменные (1113)
		/// </summary>
		[Description("Деревянные, Каменные")]
        [EnumCode("39")]
        [ShortTitle("")]
        DerevyannieKamennie = 1113,
		/// <summary>
		/// Деревянные, Каркасно-засыпные (1114)
		/// </summary>
		[Description("Деревянные, Каркасно-засыпные")]
        [EnumCode("40")]
        [ShortTitle("")]
        DerevyannieKarkasnoZasipnie = 1114,
		/// <summary>
		/// Деревянные, Каркасно-обшивные (1115)
		/// </summary>
		[Description("Деревянные, Каркасно-обшивные")]
        [EnumCode("41")]
        [ShortTitle("")]
        DerevyannieKarkasnoObshivnie = 1115,
		/// <summary>
		/// Деревянные, Кирпичные (1116)
		/// </summary>
		[Description("Деревянные, Кирпичные")]
        [EnumCode("42")]
        [ShortTitle("")]
        DerevyannieKirpichnie = 1116,
		/// <summary>
		/// Деревянные, Кирпичные, Железобетонные (1117)
		/// </summary>
		[Description("Деревянные, Кирпичные, Железобетонные")]
        [EnumCode("43")]
        [ShortTitle("")]
        DerevyannieKirpichnieZhelezobetonnie = 1117,
		/// <summary>
		/// Деревянные, Металлические (1118)
		/// </summary>
		[Description("Деревянные, Металлические")]
        [EnumCode("44")]
        [ShortTitle("")]
        DerevyannieMetallicheskie = 1118,
		/// <summary>
		/// Деревянные, Рубленые (1119)
		/// </summary>
		[Description("Деревянные, Рубленые")]
        [EnumCode("45")]
        [ShortTitle("")]
        DerevyannieRublenie = 1119,
		/// <summary>
		/// Деревянные, Шлакобетонные (1120)
		/// </summary>
		[Description("Деревянные, Шлакобетонные")]
        [EnumCode("46")]
        [ShortTitle("")]
        DerevyannieShlakobetonnie = 1120,
		/// <summary>
		/// Деревянный каркас без обшивки (1121)
		/// </summary>
		[Description("Деревянный каркас без обшивки")]
        [EnumCode("47")]
        [ShortTitle("")]
        DerevyannijKarkasBezObshivki = 1121,
		/// <summary>
		/// Деревянный каркас без обшивки, Деревянные (1122)
		/// </summary>
		[Description("Деревянный каркас без обшивки, Деревянные")]
        [EnumCode("48")]
        [ShortTitle("")]
        DerevyannijKarkasBezObshivkiDerevyannie = 1122,
		/// <summary>
		/// Деревянный каркас без обшивки, Металлические (1123)
		/// </summary>
		[Description("Деревянный каркас без обшивки, Металлические")]
        [EnumCode("49")]
        [ShortTitle("")]
        DerevyannijKarkasBezObshivkiMetallicheskie = 1123,
		/// <summary>
		/// Дощатые (1124)
		/// </summary>
		[Description("Дощатые")]
        [EnumCode("50")]
        [ShortTitle("")]
        Doschatie = 1124,
		/// <summary>
		/// Дощатые, Деревянные (1125)
		/// </summary>
		[Description("Дощатые, Деревянные")]
        [EnumCode("51")]
        [ShortTitle("")]
        DoschatieDerevyannie = 1125,
		/// <summary>
		/// Дощатые, Железобетонные (1126)
		/// </summary>
		[Description("Дощатые, Железобетонные")]
        [EnumCode("52")]
        [ShortTitle("")]
        DoschatieZhelezobetonnie = 1126,
		/// <summary>
		/// Дощатые, Из мелких бетонных блоков (1127)
		/// </summary>
		[Description("Дощатые, Из мелких бетонных блоков")]
        [EnumCode("53")]
        [ShortTitle("")]
        DoschatieIzMelkihBetonnihBlokov = 1127,
		/// <summary>
		/// Дощатые, Из прочих материалов (1128)
		/// </summary>
		[Description("Дощатые, Из прочих материалов")]
        [EnumCode("54")]
        [ShortTitle("")]
        DoschatieIzProchihMaterialov = 1128,
		/// <summary>
		/// Дощатые, Каркасно-засыпные (1129)
		/// </summary>
		[Description("Дощатые, Каркасно-засыпные")]
        [EnumCode("55")]
        [ShortTitle("")]
        DoschatieKarkasnoZasipnie = 1129,
		/// <summary>
		/// Дощатые, Кирпичные (1130)
		/// </summary>
		[Description("Дощатые, Кирпичные")]
        [EnumCode("56")]
        [ShortTitle("")]
        DoschatieKirpichnie = 1130,
		/// <summary>
		/// Дощатые, Кирпичные облегченные (1131)
		/// </summary>
		[Description("Дощатые, Кирпичные облегченные")]
        [EnumCode("57")]
        [ShortTitle("")]
        DoschatieKirpichnieOblegchennie = 1131,
		/// <summary>
		/// Дощатые, Крупноблочные (1132)
		/// </summary>
		[Description("Дощатые, Крупноблочные")]
        [EnumCode("58")]
        [ShortTitle("")]
        DoschatieKrupnoblochnie = 1132,
		/// <summary>
		/// Дощатые, Металлические (1133)
		/// </summary>
		[Description("Дощатые, Металлические")]
        [EnumCode("59")]
        [ShortTitle("")]
        DoschatieMetallicheskie = 1133,
		/// <summary>
		/// Дощатые, Шлакобетонные (1134)
		/// </summary>
		[Description("Дощатые, Шлакобетонные")]
        [EnumCode("60")]
        [ShortTitle("")]
        DoschatieShlakobetonnie = 1134,
		/// <summary>
		/// Железобетонные (1135)
		/// </summary>
		[Description("Железобетонные")]
        [EnumCode("61")]
        [ShortTitle("")]
        Zhelezobetonnie = 1135,
		/// <summary>
		/// Железобетонные, Бетонные, Кирпичные (1136)
		/// </summary>
		[Description("Железобетонные, Бетонные, Кирпичные")]
        [EnumCode("62")]
        [ShortTitle("")]
        ZhelezobetonnieBetonnieKirpichnie = 1136,
		/// <summary>
		/// Железобетонные, Деревянные (1137)
		/// </summary>
		[Description("Железобетонные, Деревянные")]
        [EnumCode("63")]
        [ShortTitle("")]
        ZhelezobetonnieDerevyannie = 1137,
		/// <summary>
		/// Железобетонные, Из железобетонных сегментов (1138)
		/// </summary>
		[Description("Железобетонные, Из железобетонных сегментов")]
        [EnumCode("64")]
        [ShortTitle("")]
        ZhelezobetonnieIzZhelezobetonnihSegmentov = 1138,
		/// <summary>
		/// Железобетонные, Из легкобетонных панелей (1139)
		/// </summary>
		[Description("Железобетонные, Из легкобетонных панелей")]
        [EnumCode("65")]
        [ShortTitle("")]
        ZhelezobetonnieIzLegkobetonnihPanelej = 1139,
		/// <summary>
		/// Железобетонные, Из мелких бетонных блоков (1140)
		/// </summary>
		[Description("Железобетонные, Из мелких бетонных блоков")]
        [EnumCode("66")]
        [ShortTitle("")]
        ZhelezobetonnieIzMelkihBetonnihBlokov = 1140,
		/// <summary>
		/// Железобетонные, Из мелких бетонных блоков, Из прочих материалов (1141)
		/// </summary>
		[Description("Железобетонные, Из мелких бетонных блоков, Из прочих материалов")]
        [EnumCode("67")]
        [ShortTitle("")]
        ZhelezobetonnieIzMelkihBetonnihBlokovIzProchihMaterialov = 1141,
		/// <summary>
		/// Железобетонные, Из мелких бетонных блоков, Кирпичные (1142)
		/// </summary>
		[Description("Железобетонные, Из мелких бетонных блоков, Кирпичные")]
        [EnumCode("68")]
        [ShortTitle("")]
        ZhelezobetonnieIzMelkihBetonnihBlokovKirpichnie = 1142,
		/// <summary>
		/// Железобетонные, Из прочих материалов (1143)
		/// </summary>
		[Description("Железобетонные, Из прочих материалов")]
        [EnumCode("69")]
        [ShortTitle("")]
        ZhelezobetonnieIzProchihMaterialov = 1143,
		/// <summary>
		/// Железобетонные, Из прочих материалов, Кирпичные (1144)
		/// </summary>
		[Description("Железобетонные, Из прочих материалов, Кирпичные")]
        [EnumCode("70")]
        [ShortTitle("")]
        ZhelezobetonnieIzProchihMaterialovKirpichnie = 1144,
		/// <summary>
		/// Железобетонные, Из прочих материалов, Монолитные (1145)
		/// </summary>
		[Description("Железобетонные, Из прочих материалов, Монолитные")]
        [EnumCode("71")]
        [ShortTitle("")]
        ZhelezobetonnieIzProchihMaterialovMonolitnie = 1145,
		/// <summary>
		/// Железобетонные, Каркасно-обшивные (1146)
		/// </summary>
		[Description("Железобетонные, Каркасно-обшивные")]
        [EnumCode("72")]
        [ShortTitle("")]
        ZhelezobetonnieKarkasnoObshivnie = 1146,
		/// <summary>
		/// Железобетонные, Каркасно-панельные (1147)
		/// </summary>
		[Description("Железобетонные, Каркасно-панельные")]
        [EnumCode("73")]
        [ShortTitle("")]
        ZhelezobetonnieKarkasnoPaneljnie = 1147,
		/// <summary>
		/// Железобетонные, Кирпичные (1148)
		/// </summary>
		[Description("Железобетонные, Кирпичные")]
        [EnumCode("74")]
        [ShortTitle("")]
        ZhelezobetonnieKirpichnie = 1148,
		/// <summary>
		/// Железобетонные, Кирпичные, Бетонные (1149)
		/// </summary>
		[Description("Железобетонные, Кирпичные, Бетонные")]
        [EnumCode("75")]
        [ShortTitle("")]
        ZhelezobetonnieKirpichnieBetonnie = 1149,
		/// <summary>
		/// Железобетонные, Кирпичные, Деревянные (1150)
		/// </summary>
		[Description("Железобетонные, Кирпичные, Деревянные")]
        [EnumCode("76")]
        [ShortTitle("")]
        ZhelezobetonnieKirpichnieDerevyannie = 1150,
		/// <summary>
		/// Железобетонные, Кирпичные, Из прочих материалов (1151)
		/// </summary>
		[Description("Железобетонные, Кирпичные, Из прочих материалов")]
        [EnumCode("77")]
        [ShortTitle("")]
        ZhelezobetonnieKirpichnieIzProchihMaterialov = 1151,
		/// <summary>
		/// Железобетонные, Кирпичные, Монолитные (1152)
		/// </summary>
		[Description("Железобетонные, Кирпичные, Монолитные")]
        [EnumCode("78")]
        [ShortTitle("")]
        ZhelezobetonnieKirpichnieMonolitnie = 1152,
		/// <summary>
		/// Железобетонные, Крупноблочные (1153)
		/// </summary>
		[Description("Железобетонные, Крупноблочные")]
        [EnumCode("79")]
        [ShortTitle("")]
        ZhelezobetonnieKrupnoblochnie = 1153,
		/// <summary>
		/// Железобетонные, Крупнопанельные (1154)
		/// </summary>
		[Description("Железобетонные, Крупнопанельные")]
        [EnumCode("80")]
        [ShortTitle("")]
        ZhelezobetonnieKrupnopaneljnie = 1154,
		/// <summary>
		/// Железобетонные, Металлические (1155)
		/// </summary>
		[Description("Железобетонные, Металлические")]
        [EnumCode("81")]
        [ShortTitle("")]
        ZhelezobetonnieMetallicheskie = 1155,
		/// <summary>
		/// Железобетонные, Монолитные (1156)
		/// </summary>
		[Description("Железобетонные, Монолитные")]
        [EnumCode("82")]
        [ShortTitle("")]
        ZhelezobetonnieMonolitnie = 1156,
		/// <summary>
		/// Железобетонные, Монолитные, Из прочих материалов (1157)
		/// </summary>
		[Description("Железобетонные, Монолитные, Из прочих материалов")]
        [EnumCode("83")]
        [ShortTitle("")]
        ZhelezobetonnieMonolitnieIzProchihMaterialov = 1157,
		/// <summary>
		/// Железобетонные, Монолитные, Кирпичные, Из мелких бетонных блоков (1158)
		/// </summary>
		[Description("Железобетонные, Монолитные, Кирпичные, Из мелких бетонных блоков")]
        [EnumCode("84")]
        [ShortTitle("")]
        ZhelezobetonnieMonolitnieKirpichnieIzMelkihBetonnihBlokov = 1158,
		/// <summary>
		/// Железобетонные, Шлакобетонные (1159)
		/// </summary>
		[Description("Железобетонные, Шлакобетонные")]
        [EnumCode("85")]
        [ShortTitle("")]
        ZhelezobetonnieShlakobetonnie = 1159,
		/// <summary>
		/// Железобетонные, Шлакобетонные, Кирпичные (1160)
		/// </summary>
		[Description("Железобетонные, Шлакобетонные, Кирпичные")]
        [EnumCode("86")]
        [ShortTitle("")]
        ZhelezobetonnieShlakobetonnieKirpichnie = 1160,
		/// <summary>
		/// Из железобетонных сегментов (1161)
		/// </summary>
		[Description("Из железобетонных сегментов")]
        [EnumCode("87")]
        [ShortTitle("")]
        IzZhelezobetonnihSegmentov = 1161,
		/// <summary>
		/// Из железобетонных сегментов, Кирпичные (1162)
		/// </summary>
		[Description("Из железобетонных сегментов, Кирпичные")]
        [EnumCode("88")]
        [ShortTitle("")]
        IzZhelezobetonnihSegmentovKirpichnie = 1162,
		/// <summary>
		/// Из железобетонных сегментов, Металлические (1163)
		/// </summary>
		[Description("Из железобетонных сегментов, Металлические")]
        [EnumCode("89")]
        [ShortTitle("")]
        IzZhelezobetonnihSegmentovMetallicheskie = 1163,
		/// <summary>
		/// Из легкобетонных панелей (1164)
		/// </summary>
		[Description("Из легкобетонных панелей")]
        [EnumCode("90")]
        [ShortTitle("")]
        IzLegkobetonnihPanelej = 1164,
		/// <summary>
		/// Из легкобетонных панелей, Деревянные, Из мелких бетонных блоков (1165)
		/// </summary>
		[Description("Из легкобетонных панелей, Деревянные, Из мелких бетонных блоков")]
        [EnumCode("91")]
        [ShortTitle("")]
        IzLegkobetonnihPanelejDerevyannieIzMelkihBetonnihBlokov = 1165,
		/// <summary>
		/// Из легкобетонных панелей, Железобетонные, Бетонные (1166)
		/// </summary>
		[Description("Из легкобетонных панелей, Железобетонные, Бетонные")]
        [EnumCode("92")]
        [ShortTitle("")]
        IzLegkobetonnihPanelejZhelezobetonnieBetonnie = 1166,
		/// <summary>
		/// Из легкобетонных панелей, Из мелких бетонных блоков (1167)
		/// </summary>
		[Description("Из легкобетонных панелей, Из мелких бетонных блоков")]
        [EnumCode("93")]
        [ShortTitle("")]
        IzLegkobetonnihPanelejIzMelkihBetonnihBlokov = 1167,
		/// <summary>
		/// Из легкобетонных панелей, Кирпичные (1168)
		/// </summary>
		[Description("Из легкобетонных панелей, Кирпичные")]
        [EnumCode("94")]
        [ShortTitle("")]
        IzLegkobetonnihPanelejKirpichnie = 1168,
		/// <summary>
		/// Из легкобетонных панелей, Кирпичные облегченные (1169)
		/// </summary>
		[Description("Из легкобетонных панелей, Кирпичные облегченные")]
        [EnumCode("95")]
        [ShortTitle("")]
        IzLegkobetonnihPanelejKirpichnieOblegchennie = 1169,
		/// <summary>
		/// Из легкобетонных панелей, Металлические (1170)
		/// </summary>
		[Description("Из легкобетонных панелей, Металлические")]
        [EnumCode("96")]
        [ShortTitle("")]
        IzLegkobetonnihPanelejMetallicheskie = 1170,
		/// <summary>
		/// Из легкобетонных панелей, Монолитные (1171)
		/// </summary>
		[Description("Из легкобетонных панелей, Монолитные")]
        [EnumCode("97")]
        [ShortTitle("")]
        IzLegkobetonnihPanelejMonolitnie = 1171,
		/// <summary>
		/// Из мелких бетонных блоков (1172)
		/// </summary>
		[Description("Из мелких бетонных блоков")]
        [EnumCode("98")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokov = 1172,
		/// <summary>
		/// Из мелких бетонных блоков, Бетонные (1173)
		/// </summary>
		[Description("Из мелких бетонных блоков, Бетонные")]
        [EnumCode("99")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovBetonnie = 1173,
		/// <summary>
		/// Из мелких бетонных блоков, Деревянные (1174)
		/// </summary>
		[Description("Из мелких бетонных блоков, Деревянные")]
        [EnumCode("100")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovDerevyannie = 1174,
		/// <summary>
		/// Из мелких бетонных блоков, Железобетонные (1175)
		/// </summary>
		[Description("Из мелких бетонных блоков, Железобетонные")]
        [EnumCode("101")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovZhelezobetonnie = 1175,
		/// <summary>
		/// Из мелких бетонных блоков, Железобетонные, Монолитные (1176)
		/// </summary>
		[Description("Из мелких бетонных блоков, Железобетонные, Монолитные")]
        [EnumCode("102")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovZhelezobetonnieMonolitnie = 1176,
		/// <summary>
		/// Из мелких бетонных блоков, Из железобетонных сегментов (1177)
		/// </summary>
		[Description("Из мелких бетонных блоков, Из железобетонных сегментов")]
        [EnumCode("103")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovIzZhelezobetonnihSegmentov = 1177,
		/// <summary>
		/// Из мелких бетонных блоков, Из легкобетонных панелей (1178)
		/// </summary>
		[Description("Из мелких бетонных блоков, Из легкобетонных панелей")]
        [EnumCode("104")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovIzLegkobetonnihPanelej = 1178,
		/// <summary>
		/// Из мелких бетонных блоков, Из прочих материалов (1179)
		/// </summary>
		[Description("Из мелких бетонных блоков, Из прочих материалов")]
        [EnumCode("105")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovIzProchihMaterialov = 1179,
		/// <summary>
		/// Из мелких бетонных блоков, Из унифицированных железобетонных элементов (1180)
		/// </summary>
		[Description("Из мелких бетонных блоков, Из унифицированных железобетонных элементов")]
        [EnumCode("106")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovIzUnificirovannihZhelezobetonnihElementov = 1180,
		/// <summary>
		/// Из мелких бетонных блоков, Каркасно-засыпные (1181)
		/// </summary>
		[Description("Из мелких бетонных блоков, Каркасно-засыпные")]
        [EnumCode("107")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovKarkasnoZasipnie = 1181,
		/// <summary>
		/// Из мелких бетонных блоков, Каркасно-обшивные (1182)
		/// </summary>
		[Description("Из мелких бетонных блоков, Каркасно-обшивные")]
        [EnumCode("108")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovKarkasnoObshivnie = 1182,
		/// <summary>
		/// Из мелких бетонных блоков, Каркасно-обшивные, Кирпичные (1183)
		/// </summary>
		[Description("Из мелких бетонных блоков, Каркасно-обшивные, Кирпичные")]
        [EnumCode("109")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovKarkasnoObshivnieKirpichnie = 1183,
		/// <summary>
		/// Из мелких бетонных блоков, Каркасно-панельные (1184)
		/// </summary>
		[Description("Из мелких бетонных блоков, Каркасно-панельные")]
        [EnumCode("110")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovKarkasnoPaneljnie = 1184,
		/// <summary>
		/// Из мелких бетонных блоков, Кирпичные (1185)
		/// </summary>
		[Description("Из мелких бетонных блоков, Кирпичные")]
        [EnumCode("111")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovKirpichnie = 1185,
		/// <summary>
		/// Из мелких бетонных блоков, Кирпичные облегченные (1186)
		/// </summary>
		[Description("Из мелких бетонных блоков, Кирпичные облегченные")]
        [EnumCode("112")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovKirpichnieOblegchennie = 1186,
		/// <summary>
		/// Из мелких бетонных блоков, Крупнопанельные (1187)
		/// </summary>
		[Description("Из мелких бетонных блоков, Крупнопанельные")]
        [EnumCode("113")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovKrupnopaneljnie = 1187,
		/// <summary>
		/// Из мелких бетонных блоков, Металлические (1188)
		/// </summary>
		[Description("Из мелких бетонных блоков, Металлические")]
        [EnumCode("114")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovMetallicheskie = 1188,
		/// <summary>
		/// Из мелких бетонных блоков, Металлические, Кирпичные (1189)
		/// </summary>
		[Description("Из мелких бетонных блоков, Металлические, Кирпичные")]
        [EnumCode("115")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovMetallicheskieKirpichnie = 1189,
		/// <summary>
		/// Из мелких бетонных блоков, Монолитные (1190)
		/// </summary>
		[Description("Из мелких бетонных блоков, Монолитные")]
        [EnumCode("116")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovMonolitnie = 1190,
		/// <summary>
		/// Из мелких бетонных блоков, Монолитные, Из прочих материалов (1191)
		/// </summary>
		[Description("Из мелких бетонных блоков, Монолитные, Из прочих материалов")]
        [EnumCode("117")]
        [ShortTitle("")]
        IzMelkihBetonnihBlokovMonolitnieIzProchihMaterialov = 1191,
		/// <summary>
		/// Из природного камня (1192)
		/// </summary>
		[Description("Из природного камня")]
        [EnumCode("118")]
        [ShortTitle("")]
        IzPrirodnogoKamnya = 1192,
		/// <summary>
		/// Из природного камня, Бетонные, Деревянные (1193)
		/// </summary>
		[Description("Из природного камня, Бетонные, Деревянные")]
        [EnumCode("119")]
        [ShortTitle("")]
        IzPrirodnogoKamnyaBetonnieDerevyannie = 1193,
		/// <summary>
		/// Из природного камня, Из мелких бетонных блоков (1194)
		/// </summary>
		[Description("Из природного камня, Из мелких бетонных блоков")]
        [EnumCode("120")]
        [ShortTitle("")]
        IzPrirodnogoKamnyaIzMelkihBetonnihBlokov = 1194,
		/// <summary>
		/// Из природного камня, Каркасно-обшивные (1195)
		/// </summary>
		[Description("Из природного камня, Каркасно-обшивные")]
        [EnumCode("121")]
        [ShortTitle("")]
        IzPrirodnogoKamnyaKarkasnoObshivnie = 1195,
		/// <summary>
		/// Из природного камня, Кирпичные (1196)
		/// </summary>
		[Description("Из природного камня, Кирпичные")]
        [EnumCode("122")]
        [ShortTitle("")]
        IzPrirodnogoKamnyaKirpichnie = 1196,
		/// <summary>
		/// Из природного камня, Монолитные (1197)
		/// </summary>
		[Description("Из природного камня, Монолитные")]
        [EnumCode("123")]
        [ShortTitle("")]
        IzPrirodnogoKamnyaMonolitnie = 1197,
		/// <summary>
		/// Из природного камня, Шлакобетонные (1198)
		/// </summary>
		[Description("Из природного камня, Шлакобетонные")]
        [EnumCode("124")]
        [ShortTitle("")]
        IzPrirodnogoKamnyaShlakobetonnie = 1198,
		/// <summary>
		/// Из прочих материалов (1199)
		/// </summary>
		[Description("Из прочих материалов")]
        [EnumCode("125")]
        [ShortTitle("")]
        IzProchihMaterialov = 1199,
		/// <summary>
		/// Из прочих материалов, Бетонные (1200)
		/// </summary>
		[Description("Из прочих материалов, Бетонные")]
        [EnumCode("126")]
        [ShortTitle("")]
        IzProchihMaterialovBetonnie = 1200,
		/// <summary>
		/// Из прочих материалов, Бетонные, Деревянные, Металлические (1201)
		/// </summary>
		[Description("Из прочих материалов, Бетонные, Деревянные, Металлические")]
        [EnumCode("127")]
        [ShortTitle("")]
        IzProchihMaterialovBetonnieDerevyannieMetallicheskie = 1201,
		/// <summary>
		/// Из прочих материалов, Деревянные (1202)
		/// </summary>
		[Description("Из прочих материалов, Деревянные")]
        [EnumCode("128")]
        [ShortTitle("")]
        IzProchihMaterialovDerevyannie = 1202,
		/// <summary>
		/// Из прочих материалов, Деревянные, Шлакобетонные, Кирпичные (1203)
		/// </summary>
		[Description("Из прочих материалов, Деревянные, Шлакобетонные, Кирпичные")]
        [EnumCode("129")]
        [ShortTitle("")]
        IzProchihMaterialovDerevyannieShlakobetonnieKirpichnie = 1203,
		/// <summary>
		/// Из прочих материалов, Железобетонные (1204)
		/// </summary>
		[Description("Из прочих материалов, Железобетонные")]
        [EnumCode("130")]
        [ShortTitle("")]
        IzProchihMaterialovZhelezobetonnie = 1204,
		/// <summary>
		/// Из прочих материалов, Железобетонные, Кирпичные (1205)
		/// </summary>
		[Description("Из прочих материалов, Железобетонные, Кирпичные")]
        [EnumCode("131")]
        [ShortTitle("")]
        IzProchihMaterialovZhelezobetonnieKirpichnie = 1205,
		/// <summary>
		/// Из прочих материалов, Из легкобетонных панелей (1206)
		/// </summary>
		[Description("Из прочих материалов, Из легкобетонных панелей")]
        [EnumCode("132")]
        [ShortTitle("")]
        IzProchihMaterialovIzLegkobetonnihPanelej = 1206,
		/// <summary>
		/// Из прочих материалов, Из мелких бетонных блоков (1207)
		/// </summary>
		[Description("Из прочих материалов, Из мелких бетонных блоков")]
        [EnumCode("133")]
        [ShortTitle("")]
        IzProchihMaterialovIzMelkihBetonnihBlokov = 1207,
		/// <summary>
		/// Из прочих материалов, Каменные (1208)
		/// </summary>
		[Description("Из прочих материалов, Каменные")]
        [EnumCode("134")]
        [ShortTitle("")]
        IzProchihMaterialovKamennie = 1208,
		/// <summary>
		/// Из прочих материалов, Каменные, Деревянные (1209)
		/// </summary>
		[Description("Из прочих материалов, Каменные, Деревянные")]
        [EnumCode("135")]
        [ShortTitle("")]
        IzProchihMaterialovKamennieDerevyannie = 1209,
		/// <summary>
		/// Из прочих материалов, Каркасно-обшивные (1210)
		/// </summary>
		[Description("Из прочих материалов, Каркасно-обшивные")]
        [EnumCode("136")]
        [ShortTitle("")]
        IzProchihMaterialovKarkasnoObshivnie = 1210,
		/// <summary>
		/// Из прочих материалов, Кирпичные (1211)
		/// </summary>
		[Description("Из прочих материалов, Кирпичные")]
        [EnumCode("137")]
        [ShortTitle("")]
        IzProchihMaterialovKirpichnie = 1211,
		/// <summary>
		/// Из прочих материалов, Кирпичные облегченные (1212)
		/// </summary>
		[Description("Из прочих материалов, Кирпичные облегченные")]
        [EnumCode("138")]
        [ShortTitle("")]
        IzProchihMaterialovKirpichnieOblegchennie = 1212,
		/// <summary>
		/// Из прочих материалов, Кирпичные, Бетонные (1213)
		/// </summary>
		[Description("Из прочих материалов, Кирпичные, Бетонные")]
        [EnumCode("139")]
        [ShortTitle("")]
        IzProchihMaterialovKirpichnieBetonnie = 1213,
		/// <summary>
		/// Из прочих материалов, Кирпичные, Железобетонные (1214)
		/// </summary>
		[Description("Из прочих материалов, Кирпичные, Железобетонные")]
        [EnumCode("140")]
        [ShortTitle("")]
        IzProchihMaterialovKirpichnieZhelezobetonnie = 1214,
		/// <summary>
		/// Из прочих материалов, Кирпичные, Каркасно-панельные (1215)
		/// </summary>
		[Description("Из прочих материалов, Кирпичные, Каркасно-панельные")]
        [EnumCode("141")]
        [ShortTitle("")]
        IzProchihMaterialovKirpichnieKarkasnoPaneljnie = 1215,
		/// <summary>
		/// Из прочих материалов, Крупнопанельные (1216)
		/// </summary>
		[Description("Из прочих материалов, Крупнопанельные")]
        [EnumCode("142")]
        [ShortTitle("")]
        IzProchihMaterialovKrupnopaneljnie = 1216,
		/// <summary>
		/// Из прочих материалов, Металлические (1217)
		/// </summary>
		[Description("Из прочих материалов, Металлические")]
        [EnumCode("143")]
        [ShortTitle("")]
        IzProchihMaterialovMetallicheskie = 1217,
		/// <summary>
		/// Из прочих материалов, Монолитные (1218)
		/// </summary>
		[Description("Из прочих материалов, Монолитные")]
        [EnumCode("144")]
        [ShortTitle("")]
        IzProchihMaterialovMonolitnie = 1218,
		/// <summary>
		/// Из прочих материалов, Монолитные, Из мелких бетонных блоков (1219)
		/// </summary>
		[Description("Из прочих материалов, Монолитные, Из мелких бетонных блоков")]
        [EnumCode("145")]
        [ShortTitle("")]
        IzProchihMaterialovMonolitnieIzMelkihBetonnihBlokov = 1219,
		/// <summary>
		/// Из прочих материалов, Сборно-щитовые (1220)
		/// </summary>
		[Description("Из прочих материалов, Сборно-щитовые")]
        [EnumCode("146")]
        [ShortTitle("")]
        IzProchihMaterialovSbornoSchitovie = 1220,
		/// <summary>
		/// Из прочих материалов, Смешанные (1221)
		/// </summary>
		[Description("Из прочих материалов, Смешанные")]
        [EnumCode("147")]
        [ShortTitle("")]
        IzProchihMaterialovSmeshannie = 1221,
		/// <summary>
		/// Из прочих материалов, Шлакобетонные (1222)
		/// </summary>
		[Description("Из прочих материалов, Шлакобетонные")]
        [EnumCode("148")]
        [ShortTitle("")]
        IzProchihMaterialovShlakobetonnie = 1222,
		/// <summary>
		/// Из прочих материалов, Шлакобетонные, Кирпичные (1223)
		/// </summary>
		[Description("Из прочих материалов, Шлакобетонные, Кирпичные")]
        [EnumCode("149")]
        [ShortTitle("")]
        IzProchihMaterialovShlakobetonnieKirpichnie = 1223,
		/// <summary>
		/// Из унифицированных железобетонных элементов (1224)
		/// </summary>
		[Description("Из унифицированных железобетонных элементов")]
        [EnumCode("150")]
        [ShortTitle("")]
        IzUnificirovannihZhelezobetonnihElementov = 1224,
		/// <summary>
		/// Из унифицированных железобетонных элементов, Из железобетонных сегментов (1225)
		/// </summary>
		[Description("Из унифицированных железобетонных элементов, Из железобетонных сегментов")]
        [EnumCode("151")]
        [ShortTitle("")]
        IzUnificirovannihZhelezobetonnihElementovIzZhelezobetonnihSegmentov = 1225,
		/// <summary>
		/// Из унифицированных железобетонных элементов, Металлические (1226)
		/// </summary>
		[Description("Из унифицированных железобетонных элементов, Металлические")]
        [EnumCode("152")]
        [ShortTitle("")]
        IzUnificirovannihZhelezobetonnihElementovMetallicheskie = 1226,
		/// <summary>
		/// Каменные (1227)
		/// </summary>
		[Description("Каменные")]
        [EnumCode("153")]
        [ShortTitle("")]
        Kamennie = 1227,
		/// <summary>
		/// Каменные и бетонные (1228)
		/// </summary>
		[Description("Каменные и бетонные")]
        [EnumCode("154")]
        [ShortTitle("")]
        KamennieIBetonnie = 1228,
		/// <summary>
		/// Каменные и бетонные, Из легкобетонных панелей (1229)
		/// </summary>
		[Description("Каменные и бетонные, Из легкобетонных панелей")]
        [EnumCode("155")]
        [ShortTitle("")]
        KamennieIBetonnieIzLegkobetonnihPanelej = 1229,
		/// <summary>
		/// Каменные и бетонные, Кирпичные (1230)
		/// </summary>
		[Description("Каменные и бетонные, Кирпичные")]
        [EnumCode("156")]
        [ShortTitle("")]
        KamennieIBetonnieKirpichnie = 1230,
		/// <summary>
		/// Каменные и бетонные, Шлакобетонные (1231)
		/// </summary>
		[Description("Каменные и бетонные, Шлакобетонные")]
        [EnumCode("157")]
        [ShortTitle("")]
        KamennieIBetonnieShlakobetonnie = 1231,
		/// <summary>
		/// Каменные и деревянные (1232)
		/// </summary>
		[Description("Каменные и деревянные")]
        [EnumCode("158")]
        [ShortTitle("")]
        KamennieIDerevyannie = 1232,
		/// <summary>
		/// Каменные и деревянные, Каменные и бетонные (1233)
		/// </summary>
		[Description("Каменные и деревянные, Каменные и бетонные")]
        [EnumCode("159")]
        [ShortTitle("")]
        KamennieIDerevyannieKamennieIBetonnie = 1233,
		/// <summary>
		/// Каменные и деревянные, Кирпичные (1234)
		/// </summary>
		[Description("Каменные и деревянные, Кирпичные")]
        [EnumCode("160")]
        [ShortTitle("")]
        KamennieIDerevyannieKirpichnie = 1234,
		/// <summary>
		/// Каменные, Бетонные (1235)
		/// </summary>
		[Description("Каменные, Бетонные")]
        [EnumCode("161")]
        [ShortTitle("")]
        KamennieBetonnie = 1235,
		/// <summary>
		/// Каменные, Деревянные (1236)
		/// </summary>
		[Description("Каменные, Деревянные")]
        [EnumCode("162")]
        [ShortTitle("")]
        KamennieDerevyannie = 1236,
		/// <summary>
		/// Каменные, Из мелких бетонных блоков (1237)
		/// </summary>
		[Description("Каменные, Из мелких бетонных блоков")]
        [EnumCode("163")]
        [ShortTitle("")]
        KamennieIzMelkihBetonnihBlokov = 1237,
		/// <summary>
		/// Каменные, Из прочих материалов (1238)
		/// </summary>
		[Description("Каменные, Из прочих материалов")]
        [EnumCode("164")]
        [ShortTitle("")]
        KamennieIzProchihMaterialov = 1238,
		/// <summary>
		/// Каменные, Каменные и бетонные (1239)
		/// </summary>
		[Description("Каменные, Каменные и бетонные")]
        [EnumCode("165")]
        [ShortTitle("")]
        KamennieKamennieIBetonnie = 1239,
		/// <summary>
		/// Каменные, Кирпичные (1240)
		/// </summary>
		[Description("Каменные, Кирпичные")]
        [EnumCode("166")]
        [ShortTitle("")]
        KamennieKirpichnie = 1240,
		/// <summary>
		/// Каменные, Крупнопанельные, Кирпичные (1241)
		/// </summary>
		[Description("Каменные, Крупнопанельные, Кирпичные")]
        [EnumCode("167")]
        [ShortTitle("")]
        KamennieKrupnopaneljnieKirpichnie = 1241,
		/// <summary>
		/// Каменные, Рубленые (1242)
		/// </summary>
		[Description("Каменные, Рубленые")]
        [EnumCode("168")]
        [ShortTitle("")]
        KamennieRublenie = 1242,
		/// <summary>
		/// Каркас монолит-ж/б с заполнением пенобет.блоками с утеплител (1243)
		/// </summary>
		[Description("Каркас монолит-ж/б с заполнением пенобет.блоками с утеплител")]
        [EnumCode("169")]
        [ShortTitle("")]
        KarkasMonolitZhBSZapolneniemPenobetBlokamiSUteplitel = 1243,
		/// <summary>
		/// Каркасно-засыпные (1244)
		/// </summary>
		[Description("Каркасно-засыпные")]
        [EnumCode("170")]
        [ShortTitle("")]
        KarkasnoZasipnie = 1244,
		/// <summary>
		/// Каркасно-засыпные, Деревянные (1245)
		/// </summary>
		[Description("Каркасно-засыпные, Деревянные")]
        [EnumCode("171")]
        [ShortTitle("")]
        KarkasnoZasipnieDerevyannie = 1245,
		/// <summary>
		/// Каркасно-засыпные, Дощатые (1246)
		/// </summary>
		[Description("Каркасно-засыпные, Дощатые")]
        [EnumCode("172")]
        [ShortTitle("")]
        KarkasnoZasipnieDoschatie = 1246,
		/// <summary>
		/// Каркасно-засыпные, Из прочих материалов (1247)
		/// </summary>
		[Description("Каркасно-засыпные, Из прочих материалов")]
        [EnumCode("173")]
        [ShortTitle("")]
        KarkasnoZasipnieIzProchihMaterialov = 1247,
		/// <summary>
		/// Каркасно-засыпные, Каркасно-обшивные, Смешанные (1248)
		/// </summary>
		[Description("Каркасно-засыпные, Каркасно-обшивные, Смешанные")]
        [EnumCode("174")]
        [ShortTitle("")]
        KarkasnoZasipnieKarkasnoObshivnieSmeshannie = 1248,
		/// <summary>
		/// Каркасно-засыпные, Сборно-щитовые (1249)
		/// </summary>
		[Description("Каркасно-засыпные, Сборно-щитовые")]
        [EnumCode("175")]
        [ShortTitle("")]
        KarkasnoZasipnieSbornoSchitovie = 1249,
		/// <summary>
		/// Каркасно-засыпные, Шлакобетонные (1250)
		/// </summary>
		[Description("Каркасно-засыпные, Шлакобетонные")]
        [EnumCode("176")]
        [ShortTitle("")]
        KarkasnoZasipnieShlakobetonnie = 1250,
		/// <summary>
		/// Каркасно-обшивные (1251)
		/// </summary>
		[Description("Каркасно-обшивные")]
        [EnumCode("177")]
        [ShortTitle("")]
        KarkasnoObshivnie = 1251,
		/// <summary>
		/// Каркасно-обшивные, Бетонные (1252)
		/// </summary>
		[Description("Каркасно-обшивные, Бетонные")]
        [EnumCode("178")]
        [ShortTitle("")]
        KarkasnoObshivnieBetonnie = 1252,
		/// <summary>
		/// Каркасно-обшивные, Деревянные (1253)
		/// </summary>
		[Description("Каркасно-обшивные, Деревянные")]
        [EnumCode("179")]
        [ShortTitle("")]
        KarkasnoObshivnieDerevyannie = 1253,
		/// <summary>
		/// Каркасно-обшивные, Дощатые (1254)
		/// </summary>
		[Description("Каркасно-обшивные, Дощатые")]
        [EnumCode("180")]
        [ShortTitle("")]
        KarkasnoObshivnieDoschatie = 1254,
		/// <summary>
		/// Каркасно-обшивные, Железобетонные (1255)
		/// </summary>
		[Description("Каркасно-обшивные, Железобетонные")]
        [EnumCode("181")]
        [ShortTitle("")]
        KarkasnoObshivnieZhelezobetonnie = 1255,
		/// <summary>
		/// Каркасно-обшивные, Из легкобетонных панелей (1256)
		/// </summary>
		[Description("Каркасно-обшивные, Из легкобетонных панелей")]
        [EnumCode("182")]
        [ShortTitle("")]
        KarkasnoObshivnieIzLegkobetonnihPanelej = 1256,
		/// <summary>
		/// Каркасно-обшивные, Из мелких бетонных блоков (1257)
		/// </summary>
		[Description("Каркасно-обшивные, Из мелких бетонных блоков")]
        [EnumCode("183")]
        [ShortTitle("")]
        KarkasnoObshivnieIzMelkihBetonnihBlokov = 1257,
		/// <summary>
		/// Каркасно-обшивные, Из прочих материалов (1258)
		/// </summary>
		[Description("Каркасно-обшивные, Из прочих материалов")]
        [EnumCode("184")]
        [ShortTitle("")]
        KarkasnoObshivnieIzProchihMaterialov = 1258,
		/// <summary>
		/// Каркасно-обшивные, Из унифицированных железобетонных элементов (1259)
		/// </summary>
		[Description("Каркасно-обшивные, Из унифицированных железобетонных элементов")]
        [EnumCode("185")]
        [ShortTitle("")]
        KarkasnoObshivnieIzUnificirovannihZhelezobetonnihElementov = 1259,
		/// <summary>
		/// Каркасно-обшивные, Кирпичные облегченные (1260)
		/// </summary>
		[Description("Каркасно-обшивные, Кирпичные облегченные")]
        [EnumCode("186")]
        [ShortTitle("")]
        KarkasnoObshivnieKirpichnieOblegchennie = 1260,
		/// <summary>
		/// Каркасно-обшивные, Крупноблочные (1261)
		/// </summary>
		[Description("Каркасно-обшивные, Крупноблочные")]
        [EnumCode("187")]
        [ShortTitle("")]
        KarkasnoObshivnieKrupnoblochnie = 1261,
		/// <summary>
		/// Каркасно-обшивные, Металлические (1262)
		/// </summary>
		[Description("Каркасно-обшивные, Металлические")]
        [EnumCode("188")]
        [ShortTitle("")]
        KarkasnoObshivnieMetallicheskie = 1262,
		/// <summary>
		/// Каркасно-обшивные, Рубленые (1263)
		/// </summary>
		[Description("Каркасно-обшивные, Рубленые")]
        [EnumCode("189")]
        [ShortTitle("")]
        KarkasnoObshivnieRublenie = 1263,
		/// <summary>
		/// Каркасно-обшивные, Сборно-щитовые (1264)
		/// </summary>
		[Description("Каркасно-обшивные, Сборно-щитовые")]
        [EnumCode("190")]
        [ShortTitle("")]
        KarkasnoObshivnieSbornoSchitovie = 1264,
		/// <summary>
		/// Каркасно-обшивные, Сборно-щитовые, Металлические (1265)
		/// </summary>
		[Description("Каркасно-обшивные, Сборно-щитовые, Металлические")]
        [EnumCode("191")]
        [ShortTitle("")]
        KarkasnoObshivnieSbornoSchitovieMetallicheskie = 1265,
		/// <summary>
		/// Каркасно-обшивные, Шлакобетонные (1266)
		/// </summary>
		[Description("Каркасно-обшивные, Шлакобетонные")]
        [EnumCode("192")]
        [ShortTitle("")]
        KarkasnoObshivnieShlakobetonnie = 1266,
		/// <summary>
		/// Каркасно-панельные (1267)
		/// </summary>
		[Description("Каркасно-панельные")]
        [EnumCode("193")]
        [ShortTitle("")]
        KarkasnoPaneljnie = 1267,
		/// <summary>
		/// Каркасно-панельные, Кирпичные, Из легкобетонных панелей (1268)
		/// </summary>
		[Description("Каркасно-панельные, Кирпичные, Из легкобетонных панелей")]
        [EnumCode("194")]
        [ShortTitle("")]
        KarkasnoPaneljnieKirpichnieIzLegkobetonnihPanelej = 1268,
		/// <summary>
		/// Каркасно-панельные, Кирпичные, Из мелких бетонных блоков (1269)
		/// </summary>
		[Description("Каркасно-панельные, Кирпичные, Из мелких бетонных блоков")]
        [EnumCode("195")]
        [ShortTitle("")]
        KarkasnoPaneljnieKirpichnieIzMelkihBetonnihBlokov = 1269,
		/// <summary>
		/// Кирпичные облегченные (1270)
		/// </summary>
		[Description("Кирпичные облегченные")]
        [EnumCode("196")]
        [ShortTitle("")]
        KirpichnieOblegchennie = 1270,
		/// <summary>
		/// Кирпичные облегченные, Бетонные (1271)
		/// </summary>
		[Description("Кирпичные облегченные, Бетонные")]
        [EnumCode("197")]
        [ShortTitle("")]
        KirpichnieOblegchennieBetonnie = 1271,
		/// <summary>
		/// Кирпичные облегченные, Деревянные (1272)
		/// </summary>
		[Description("Кирпичные облегченные, Деревянные")]
        [EnumCode("198")]
        [ShortTitle("")]
        KirpichnieOblegchennieDerevyannie = 1272,
		/// <summary>
		/// Кирпичные облегченные, Деревянный каркас без обшивки (1273)
		/// </summary>
		[Description("Кирпичные облегченные, Деревянный каркас без обшивки")]
        [EnumCode("199")]
        [ShortTitle("")]
        KirpichnieOblegchennieDerevyannijKarkasBezObshivki = 1273,
		/// <summary>
		/// Кирпичные облегченные, Дощатые (1274)
		/// </summary>
		[Description("Кирпичные облегченные, Дощатые")]
        [EnumCode("200")]
        [ShortTitle("")]
        KirpichnieOblegchennieDoschatie = 1274,
		/// <summary>
		/// Кирпичные облегченные, Железобетонные (1275)
		/// </summary>
		[Description("Кирпичные облегченные, Железобетонные")]
        [EnumCode("201")]
        [ShortTitle("")]
        KirpichnieOblegchennieZhelezobetonnie = 1275,
		/// <summary>
		/// Кирпичные облегченные, Железобетонные, Монолитные (1276)
		/// </summary>
		[Description("Кирпичные облегченные, Железобетонные, Монолитные")]
        [EnumCode("202")]
        [ShortTitle("")]
        KirpichnieOblegchennieZhelezobetonnieMonolitnie = 1276,
		/// <summary>
		/// Кирпичные облегченные, Из железобетонных сегментов (1277)
		/// </summary>
		[Description("Кирпичные облегченные, Из железобетонных сегментов")]
        [EnumCode("203")]
        [ShortTitle("")]
        KirpichnieOblegchennieIzZhelezobetonnihSegmentov = 1277,
		/// <summary>
		/// Кирпичные облегченные, Из легкобетонных панелей (1278)
		/// </summary>
		[Description("Кирпичные облегченные, Из легкобетонных панелей")]
        [EnumCode("204")]
        [ShortTitle("")]
        KirpichnieOblegchennieIzLegkobetonnihPanelej = 1278,
		/// <summary>
		/// Кирпичные облегченные, Из мелких бетонных блоков (1279)
		/// </summary>
		[Description("Кирпичные облегченные, Из мелких бетонных блоков")]
        [EnumCode("205")]
        [ShortTitle("")]
        KirpichnieOblegchennieIzMelkihBetonnihBlokov = 1279,
		/// <summary>
		/// Кирпичные облегченные, Из мелких бетонных блоков, Металлические (1280)
		/// </summary>
		[Description("Кирпичные облегченные, Из мелких бетонных блоков, Металлические")]
        [EnumCode("206")]
        [ShortTitle("")]
        KirpichnieOblegchennieIzMelkihBetonnihBlokovMetallicheskie = 1280,
		/// <summary>
		/// Кирпичные облегченные, Из мелких бетонных блоков, Шлакобетонные (1281)
		/// </summary>
		[Description("Кирпичные облегченные, Из мелких бетонных блоков, Шлакобетонные")]
        [EnumCode("207")]
        [ShortTitle("")]
        KirpichnieOblegchennieIzMelkihBetonnihBlokovShlakobetonnie = 1281,
		/// <summary>
		/// Кирпичные облегченные, Из природного камня, Из мелких бетонных блоков (1282)
		/// </summary>
		[Description("Кирпичные облегченные, Из природного камня, Из мелких бетонных блоков")]
        [EnumCode("208")]
        [ShortTitle("")]
        KirpichnieOblegchennieIzPrirodnogoKamnyaIzMelkihBetonnihBlokov = 1282,
		/// <summary>
		/// Кирпичные облегченные, Из природного камня, Шлакобетонные (1283)
		/// </summary>
		[Description("Кирпичные облегченные, Из природного камня, Шлакобетонные")]
        [EnumCode("209")]
        [ShortTitle("")]
        KirpichnieOblegchennieIzPrirodnogoKamnyaShlakobetonnie = 1283,
		/// <summary>
		/// Кирпичные облегченные, Из прочих материалов (1284)
		/// </summary>
		[Description("Кирпичные облегченные, Из прочих материалов")]
        [EnumCode("210")]
        [ShortTitle("")]
        KirpichnieOblegchennieIzProchihMaterialov = 1284,
		/// <summary>
		/// Кирпичные облегченные, Из прочих материалов, Из мелких бетонных блоков, Монолитные (1285)
		/// </summary>
		[Description("Кирпичные облегченные, Из прочих материалов, Из мелких бетонных блоков, Монолитные")]
        [EnumCode("211")]
        [ShortTitle("")]
        KirpichnieOblegchennieIzProchihMaterialovIzMelkihBetonnihBlokovMonolitnie = 1285,
		/// <summary>
		/// Кирпичные облегченные, Из унифицированных железобетонных элементов (1286)
		/// </summary>
		[Description("Кирпичные облегченные, Из унифицированных железобетонных элементов")]
        [EnumCode("212")]
        [ShortTitle("")]
        KirpichnieOblegchennieIzUnificirovannihZhelezobetonnihElementov = 1286,
		/// <summary>
		/// Кирпичные облегченные, Каменные и бетонные (1287)
		/// </summary>
		[Description("Кирпичные облегченные, Каменные и бетонные")]
        [EnumCode("213")]
        [ShortTitle("")]
        KirpichnieOblegchennieKamennieIBetonnie = 1287,
		/// <summary>
		/// Кирпичные облегченные, Каркасно-засыпные (1288)
		/// </summary>
		[Description("Кирпичные облегченные, Каркасно-засыпные")]
        [EnumCode("214")]
        [ShortTitle("")]
        KirpichnieOblegchennieKarkasnoZasipnie = 1288,
		/// <summary>
		/// Кирпичные облегченные, Каркасно-обшивные (1289)
		/// </summary>
		[Description("Кирпичные облегченные, Каркасно-обшивные")]
        [EnumCode("215")]
        [ShortTitle("")]
        KirpichnieOblegchennieKarkasnoObshivnie = 1289,
		/// <summary>
		/// Кирпичные облегченные, Каркасно-обшивные, Из мелких бетонных блоков (1290)
		/// </summary>
		[Description("Кирпичные облегченные, Каркасно-обшивные, Из мелких бетонных блоков")]
        [EnumCode("216")]
        [ShortTitle("")]
        KirpichnieOblegchennieKarkasnoObshivnieIzMelkihBetonnihBlokov = 1290,
		/// <summary>
		/// Кирпичные облегченные, Каркасно-обшивные, Сборно-щитовые (1291)
		/// </summary>
		[Description("Кирпичные облегченные, Каркасно-обшивные, Сборно-щитовые")]
        [EnumCode("217")]
        [ShortTitle("")]
        KirpichnieOblegchennieKarkasnoObshivnieSbornoSchitovie = 1291,
		/// <summary>
		/// Кирпичные облегченные, Каркасно-обшивные, Шлакобетонные (1292)
		/// </summary>
		[Description("Кирпичные облегченные, Каркасно-обшивные, Шлакобетонные")]
        [EnumCode("218")]
        [ShortTitle("")]
        KirpichnieOblegchennieKarkasnoObshivnieShlakobetonnie = 1292,
		/// <summary>
		/// Кирпичные облегченные, Крупноблочные (1293)
		/// </summary>
		[Description("Кирпичные облегченные, Крупноблочные")]
        [EnumCode("219")]
        [ShortTitle("")]
        KirpichnieOblegchennieKrupnoblochnie = 1293,
		/// <summary>
		/// Кирпичные облегченные, Крупнопанельные (1294)
		/// </summary>
		[Description("Кирпичные облегченные, Крупнопанельные")]
        [EnumCode("220")]
        [ShortTitle("")]
        KirpichnieOblegchennieKrupnopaneljnie = 1294,
		/// <summary>
		/// Кирпичные облегченные, Легкие из местных материалов (1295)
		/// </summary>
		[Description("Кирпичные облегченные, Легкие из местных материалов")]
        [EnumCode("221")]
        [ShortTitle("")]
        KirpichnieOblegchennieLegkieIzMestnihMaterialov = 1295,
		/// <summary>
		/// Кирпичные облегченные, Металлические (1296)
		/// </summary>
		[Description("Кирпичные облегченные, Металлические")]
        [EnumCode("222")]
        [ShortTitle("")]
        KirpichnieOblegchennieMetallicheskie = 1296,
		/// <summary>
		/// Кирпичные облегченные, Монолитные (1297)
		/// </summary>
		[Description("Кирпичные облегченные, Монолитные")]
        [EnumCode("223")]
        [ShortTitle("")]
        KirpichnieOblegchennieMonolitnie = 1297,
		/// <summary>
		/// Кирпичные облегченные, Рубленые (1298)
		/// </summary>
		[Description("Кирпичные облегченные, Рубленые")]
        [EnumCode("224")]
        [ShortTitle("")]
        KirpichnieOblegchennieRublenie = 1298,
		/// <summary>
		/// Кирпичные облегченные, Рубленые, Дощатые (1299)
		/// </summary>
		[Description("Кирпичные облегченные, Рубленые, Дощатые")]
        [EnumCode("225")]
        [ShortTitle("")]
        KirpichnieOblegchennieRublenieDoschatie = 1299,
		/// <summary>
		/// Кирпичные облегченные, Рубленые, Из мелких бетонных блоков (1300)
		/// </summary>
		[Description("Кирпичные облегченные, Рубленые, Из мелких бетонных блоков")]
        [EnumCode("226")]
        [ShortTitle("")]
        KirpichnieOblegchennieRublenieIzMelkihBetonnihBlokov = 1300,
		/// <summary>
		/// Кирпичные облегченные, Рубленые, Из прочих материалов (1301)
		/// </summary>
		[Description("Кирпичные облегченные, Рубленые, Из прочих материалов")]
        [EnumCode("227")]
        [ShortTitle("")]
        KirpichnieOblegchennieRublenieIzProchihMaterialov = 1301,
		/// <summary>
		/// Кирпичные облегченные, Рубленые, Каркасно-обшивные (1302)
		/// </summary>
		[Description("Кирпичные облегченные, Рубленые, Каркасно-обшивные")]
        [EnumCode("228")]
        [ShortTitle("")]
        KirpichnieOblegchennieRublenieKarkasnoObshivnie = 1302,
		/// <summary>
		/// Кирпичные облегченные, Рубленые, Каркасно-обшивные, Сборно-щитовые (1303)
		/// </summary>
		[Description("Кирпичные облегченные, Рубленые, Каркасно-обшивные, Сборно-щитовые")]
        [EnumCode("229")]
        [ShortTitle("")]
        KirpichnieOblegchennieRublenieKarkasnoObshivnieSbornoSchitovie = 1303,
		/// <summary>
		/// Кирпичные облегченные, Рубленые, Крупноблочные (1304)
		/// </summary>
		[Description("Кирпичные облегченные, Рубленые, Крупноблочные")]
        [EnumCode("230")]
        [ShortTitle("")]
        KirpichnieOblegchennieRublenieKrupnoblochnie = 1304,
		/// <summary>
		/// Кирпичные облегченные, Рубленые, Шлакобетонные (1305)
		/// </summary>
		[Description("Кирпичные облегченные, Рубленые, Шлакобетонные")]
        [EnumCode("231")]
        [ShortTitle("")]
        KirpichnieOblegchennieRublenieShlakobetonnie = 1305,
		/// <summary>
		/// Кирпичные облегченные, Сборно-щитовые (1306)
		/// </summary>
		[Description("Кирпичные облегченные, Сборно-щитовые")]
        [EnumCode("232")]
        [ShortTitle("")]
        KirpichnieOblegchennieSbornoSchitovie = 1306,
		/// <summary>
		/// Кирпичные облегченные, Шлакобетонные (1307)
		/// </summary>
		[Description("Кирпичные облегченные, Шлакобетонные")]
        [EnumCode("233")]
        [ShortTitle("")]
        KirpichnieOblegchennieShlakobetonnie = 1307,
		/// <summary>
		/// Кирпичные, Бетонные (1308)
		/// </summary>
		[Description("Кирпичные, Бетонные")]
        [EnumCode("234")]
        [ShortTitle("")]
        KirpichnieBetonnie = 1308,
		/// <summary>
		/// Кирпичные, Бетонные, Деревянные (1309)
		/// </summary>
		[Description("Кирпичные, Бетонные, Деревянные")]
        [EnumCode("235")]
        [ShortTitle("")]
        KirpichnieBetonnieDerevyannie = 1309,
		/// <summary>
		/// Кирпичные, Бетонные, Деревянные, Металлические (1310)
		/// </summary>
		[Description("Кирпичные, Бетонные, Деревянные, Металлические")]
        [EnumCode("236")]
        [ShortTitle("")]
        KirpichnieBetonnieDerevyannieMetallicheskie = 1310,
		/// <summary>
		/// Кирпичные, Бетонные, Железобетонные (1311)
		/// </summary>
		[Description("Кирпичные, Бетонные, Железобетонные")]
        [EnumCode("237")]
        [ShortTitle("")]
        KirpichnieBetonnieZhelezobetonnie = 1311,
		/// <summary>
		/// Кирпичные, Бетонные, Из прочих материалов (1312)
		/// </summary>
		[Description("Кирпичные, Бетонные, Из прочих материалов")]
        [EnumCode("238")]
        [ShortTitle("")]
        KirpichnieBetonnieIzProchihMaterialov = 1312,
		/// <summary>
		/// Кирпичные, Бетонные, Металлические (1313)
		/// </summary>
		[Description("Кирпичные, Бетонные, Металлические")]
        [EnumCode("239")]
        [ShortTitle("")]
        KirpichnieBetonnieMetallicheskie = 1313,
		/// <summary>
		/// Кирпичные, Деревянные (1314)
		/// </summary>
		[Description("Кирпичные, Деревянные")]
        [EnumCode("240")]
        [ShortTitle("")]
        KirpichnieDerevyannie = 1314,
		/// <summary>
		/// Кирпичные, Деревянные, Бетонные (1315)
		/// </summary>
		[Description("Кирпичные, Деревянные, Бетонные")]
        [EnumCode("241")]
        [ShortTitle("")]
        KirpichnieDerevyannieBetonnie = 1315,
		/// <summary>
		/// Кирпичные, Деревянные, Каркасно-обшивные (1316)
		/// </summary>
		[Description("Кирпичные, Деревянные, Каркасно-обшивные")]
        [EnumCode("242")]
        [ShortTitle("")]
        KirpichnieDerevyannieKarkasnoObshivnie = 1316,
		/// <summary>
		/// Кирпичные, Деревянный каркас без обшивки (1317)
		/// </summary>
		[Description("Кирпичные, Деревянный каркас без обшивки")]
        [EnumCode("243")]
        [ShortTitle("")]
        KirpichnieDerevyannijKarkasBezObshivki = 1317,
		/// <summary>
		/// Кирпичные, Дощатые (1318)
		/// </summary>
		[Description("Кирпичные, Дощатые")]
        [EnumCode("244")]
        [ShortTitle("")]
        KirpichnieDoschatie = 1318,
		/// <summary>
		/// Кирпичные, Дощатые, Железобетонные (1319)
		/// </summary>
		[Description("Кирпичные, Дощатые, Железобетонные")]
        [EnumCode("245")]
        [ShortTitle("")]
        KirpichnieDoschatieZhelezobetonnie = 1319,
		/// <summary>
		/// Кирпичные, Дощатые, Из мелких бетонных блоков (1320)
		/// </summary>
		[Description("Кирпичные, Дощатые, Из мелких бетонных блоков")]
        [EnumCode("246")]
        [ShortTitle("")]
        KirpichnieDoschatieIzMelkihBetonnihBlokov = 1320,
		/// <summary>
		/// Кирпичные, Железобетонные (1321)
		/// </summary>
		[Description("Кирпичные, Железобетонные")]
        [EnumCode("247")]
        [ShortTitle("")]
        KirpichnieZhelezobetonnie = 1321,
		/// <summary>
		/// Кирпичные, Железобетонные, Деревянные (1322)
		/// </summary>
		[Description("Кирпичные, Железобетонные, Деревянные")]
        [EnumCode("248")]
        [ShortTitle("")]
        KirpichnieZhelezobetonnieDerevyannie = 1322,
		/// <summary>
		/// Кирпичные, Железобетонные, Из прочих материалов (1323)
		/// </summary>
		[Description("Кирпичные, Железобетонные, Из прочих материалов")]
        [EnumCode("249")]
        [ShortTitle("")]
        KirpichnieZhelezobetonnieIzProchihMaterialov = 1323,
		/// <summary>
		/// Кирпичные, Железобетонные, Металлические (1324)
		/// </summary>
		[Description("Кирпичные, Железобетонные, Металлические")]
        [EnumCode("250")]
        [ShortTitle("")]
        KirpichnieZhelezobetonnieMetallicheskie = 1324,
		/// <summary>
		/// Кирпичные, Железобетонные, Монолитные (1325)
		/// </summary>
		[Description("Кирпичные, Железобетонные, Монолитные")]
        [EnumCode("251")]
        [ShortTitle("")]
        KirpichnieZhelezobetonnieMonolitnie = 1325,
		/// <summary>
		/// Кирпичные, Из железобетонных сегментов (1326)
		/// </summary>
		[Description("Кирпичные, Из железобетонных сегментов")]
        [EnumCode("252")]
        [ShortTitle("")]
        KirpichnieIzZhelezobetonnihSegmentov = 1326,
		/// <summary>
		/// Кирпичные, Из легкобетонных панелей (1327)
		/// </summary>
		[Description("Кирпичные, Из легкобетонных панелей")]
        [EnumCode("253")]
        [ShortTitle("")]
        KirpichnieIzLegkobetonnihPanelej = 1327,
		/// <summary>
		/// Кирпичные, Из легкобетонных панелей, Железобетонные, Металлические (1328)
		/// </summary>
		[Description("Кирпичные, Из легкобетонных панелей, Железобетонные, Металлические")]
        [EnumCode("254")]
        [ShortTitle("")]
        KirpichnieIzLegkobetonnihPanelejZhelezobetonnieMetallicheskie = 1328,
		/// <summary>
		/// Кирпичные, Из легкобетонных панелей, Из прочих материалов (1329)
		/// </summary>
		[Description("Кирпичные, Из легкобетонных панелей, Из прочих материалов")]
        [EnumCode("255")]
        [ShortTitle("")]
        KirpichnieIzLegkobetonnihPanelejIzProchihMaterialov = 1329,
		/// <summary>
		/// Кирпичные, Из мелких бетонных блоков (1330)
		/// </summary>
		[Description("Кирпичные, Из мелких бетонных блоков")]
        [EnumCode("256")]
        [ShortTitle("")]
        KirpichnieIzMelkihBetonnihBlokov = 1330,
		/// <summary>
		/// Кирпичные, Из мелких бетонных блоков, Железобетонные (1331)
		/// </summary>
		[Description("Кирпичные, Из мелких бетонных блоков, Железобетонные")]
        [EnumCode("257")]
        [ShortTitle("")]
        KirpichnieIzMelkihBetonnihBlokovZhelezobetonnie = 1331,
		/// <summary>
		/// Кирпичные, Из мелких бетонных блоков, Из железобетонных сегментов (1332)
		/// </summary>
		[Description("Кирпичные, Из мелких бетонных блоков, Из железобетонных сегментов")]
        [EnumCode("258")]
        [ShortTitle("")]
        KirpichnieIzMelkihBetonnihBlokovIzZhelezobetonnihSegmentov = 1332,
		/// <summary>
		/// Кирпичные, Из мелких бетонных блоков, Из легкобетонных панелей (1333)
		/// </summary>
		[Description("Кирпичные, Из мелких бетонных блоков, Из легкобетонных панелей")]
        [EnumCode("259")]
        [ShortTitle("")]
        KirpichnieIzMelkihBetonnihBlokovIzLegkobetonnihPanelej = 1333,
		/// <summary>
		/// Кирпичные, Из мелких бетонных блоков, Из легкобетонных панелей, Из прочих материалов (1334)
		/// </summary>
		[Description("Кирпичные, Из мелких бетонных блоков, Из легкобетонных панелей, Из прочих материалов")]
        [EnumCode("260")]
        [ShortTitle("")]
        KirpichnieIzMelkihBetonnihBlokovIzLegkobetonnihPanelejIzProchihMaterialov = 1334,
		/// <summary>
		/// Кирпичные, Из мелких бетонных блоков, Из унифицированных железобетонных элементов (1335)
		/// </summary>
		[Description("Кирпичные, Из мелких бетонных блоков, Из унифицированных железобетонных элементов")]
        [EnumCode("261")]
        [ShortTitle("")]
        KirpichnieIzMelkihBetonnihBlokovIzUnificirovannihZhelezobetonnihElementov = 1335,
		/// <summary>
		/// Кирпичные, Из мелких бетонных блоков, Шлакобетонные (1336)
		/// </summary>
		[Description("Кирпичные, Из мелких бетонных блоков, Шлакобетонные")]
        [EnumCode("262")]
        [ShortTitle("")]
        KirpichnieIzMelkihBetonnihBlokovShlakobetonnie = 1336,
		/// <summary>
		/// Кирпичные, Из природного камня (1337)
		/// </summary>
		[Description("Кирпичные, Из природного камня")]
        [EnumCode("263")]
        [ShortTitle("")]
        KirpichnieIzPrirodnogoKamnya = 1337,
		/// <summary>
		/// Кирпичные, Из природного камня, Дощатые (1338)
		/// </summary>
		[Description("Кирпичные, Из природного камня, Дощатые")]
        [EnumCode("264")]
        [ShortTitle("")]
        KirpichnieIzPrirodnogoKamnyaDoschatie = 1338,
		/// <summary>
		/// Кирпичные, Из природного камня, Крупнопанельные (1339)
		/// </summary>
		[Description("Кирпичные, Из природного камня, Крупнопанельные")]
        [EnumCode("265")]
        [ShortTitle("")]
        KirpichnieIzPrirodnogoKamnyaKrupnopaneljnie = 1339,
		/// <summary>
		/// Кирпичные, Из природного камня, Крупнопанельные, Крупноблочные (1340)
		/// </summary>
		[Description("Кирпичные, Из природного камня, Крупнопанельные, Крупноблочные")]
        [EnumCode("266")]
        [ShortTitle("")]
        KirpichnieIzPrirodnogoKamnyaKrupnopaneljnieKrupnoblochnie = 1340,
		/// <summary>
		/// Кирпичные, Из прочих материалов (1341)
		/// </summary>
		[Description("Кирпичные, Из прочих материалов")]
        [EnumCode("267")]
        [ShortTitle("")]
        KirpichnieIzProchihMaterialov = 1341,
		/// <summary>
		/// Кирпичные, Из прочих материалов, Бетонные (1342)
		/// </summary>
		[Description("Кирпичные, Из прочих материалов, Бетонные")]
        [EnumCode("268")]
        [ShortTitle("")]
        KirpichnieIzProchihMaterialovBetonnie = 1342,
		/// <summary>
		/// Кирпичные, Из прочих материалов, Деревянные (1343)
		/// </summary>
		[Description("Кирпичные, Из прочих материалов, Деревянные")]
        [EnumCode("269")]
        [ShortTitle("")]
        KirpichnieIzProchihMaterialovDerevyannie = 1343,
		/// <summary>
		/// Кирпичные, Из прочих материалов, Железобетонные (1344)
		/// </summary>
		[Description("Кирпичные, Из прочих материалов, Железобетонные")]
        [EnumCode("270")]
        [ShortTitle("")]
        KirpichnieIzProchihMaterialovZhelezobetonnie = 1344,
		/// <summary>
		/// Кирпичные, Из прочих материалов, Каркасно-панельные (1345)
		/// </summary>
		[Description("Кирпичные, Из прочих материалов, Каркасно-панельные")]
        [EnumCode("271")]
        [ShortTitle("")]
        KirpichnieIzProchihMaterialovKarkasnoPaneljnie = 1345,
		/// <summary>
		/// Кирпичные, Из прочих материалов, Крупнопанельные (1346)
		/// </summary>
		[Description("Кирпичные, Из прочих материалов, Крупнопанельные")]
        [EnumCode("272")]
        [ShortTitle("")]
        KirpichnieIzProchihMaterialovKrupnopaneljnie = 1346,
		/// <summary>
		/// Кирпичные, Из прочих материалов, Монолитные (1347)
		/// </summary>
		[Description("Кирпичные, Из прочих материалов, Монолитные")]
        [EnumCode("273")]
        [ShortTitle("")]
        KirpichnieIzProchihMaterialovMonolitnie = 1347,
		/// <summary>
		/// Кирпичные, Из унифицированных железобетонных элементов (1348)
		/// </summary>
		[Description("Кирпичные, Из унифицированных железобетонных элементов")]
        [EnumCode("274")]
        [ShortTitle("")]
        KirpichnieIzUnificirovannihZhelezobetonnihElementov = 1348,
		/// <summary>
		/// Кирпичные, Каменные (1349)
		/// </summary>
		[Description("Кирпичные, Каменные")]
        [EnumCode("275")]
        [ShortTitle("")]
        KirpichnieKamennie = 1349,
		/// <summary>
		/// Кирпичные, Каменные и бетонные (1350)
		/// </summary>
		[Description("Кирпичные, Каменные и бетонные")]
        [EnumCode("276")]
        [ShortTitle("")]
        KirpichnieKamennieIBetonnie = 1350,
		/// <summary>
		/// Кирпичные, Каменные и деревянные (1351)
		/// </summary>
		[Description("Кирпичные, Каменные и деревянные")]
        [EnumCode("277")]
        [ShortTitle("")]
        KirpichnieKamennieIDerevyannie = 1351,
		/// <summary>
		/// Кирпичные, Каменные, Из прочих материалов (1352)
		/// </summary>
		[Description("Кирпичные, Каменные, Из прочих материалов")]
        [EnumCode("278")]
        [ShortTitle("")]
        KirpichnieKamennieIzProchihMaterialov = 1352,
		/// <summary>
		/// Кирпичные, Каркасно-засыпные (1353)
		/// </summary>
		[Description("Кирпичные, Каркасно-засыпные")]
        [EnumCode("279")]
        [ShortTitle("")]
        KirpichnieKarkasnoZasipnie = 1353,
		/// <summary>
		/// Кирпичные, Каркасно-обшивные (1354)
		/// </summary>
		[Description("Кирпичные, Каркасно-обшивные")]
        [EnumCode("280")]
        [ShortTitle("")]
        KirpichnieKarkasnoObshivnie = 1354,
		/// <summary>
		/// Кирпичные, Каркасно-обшивные, Сборно-щитовые (1355)
		/// </summary>
		[Description("Кирпичные, Каркасно-обшивные, Сборно-щитовые")]
        [EnumCode("281")]
        [ShortTitle("")]
        KirpichnieKarkasnoObshivnieSbornoSchitovie = 1355,
		/// <summary>
		/// Кирпичные, Каркасно-панельные (1356)
		/// </summary>
		[Description("Кирпичные, Каркасно-панельные")]
        [EnumCode("282")]
        [ShortTitle("")]
        KirpichnieKarkasnoPaneljnie = 1356,
		/// <summary>
		/// Кирпичные, Кирпичные облегченные (1357)
		/// </summary>
		[Description("Кирпичные, Кирпичные облегченные")]
        [EnumCode("283")]
        [ShortTitle("")]
        KirpichnieKirpichnieOblegchennie = 1357,
		/// <summary>
		/// Кирпичные, Кирпичные облегченные, Из мелких бетонных блоков (1358)
		/// </summary>
		[Description("Кирпичные, Кирпичные облегченные, Из мелких бетонных блоков")]
        [EnumCode("284")]
        [ShortTitle("")]
        KirpichnieKirpichnieOblegchennieIzMelkihBetonnihBlokov = 1358,
		/// <summary>
		/// Кирпичные, Кирпичные облегченные, Из природного камня, Из мелких бетонных блоков (1359)
		/// </summary>
		[Description("Кирпичные, Кирпичные облегченные, Из природного камня, Из мелких бетонных блоков")]
        [EnumCode("285")]
        [ShortTitle("")]
        KirpichnieKirpichnieOblegchennieIzPrirodnogoKamnyaIzMelkihBetonnihBlokov = 1359,
		/// <summary>
		/// Кирпичные, Кирпичные облегченные, Рубленые (1360)
		/// </summary>
		[Description("Кирпичные, Кирпичные облегченные, Рубленые")]
        [EnumCode("286")]
        [ShortTitle("")]
        KirpichnieKirpichnieOblegchennieRublenie = 1360,
		/// <summary>
		/// Кирпичные, Крупноблочные (1361)
		/// </summary>
		[Description("Кирпичные, Крупноблочные")]
        [EnumCode("287")]
        [ShortTitle("")]
        KirpichnieKrupnoblochnie = 1361,
		/// <summary>
		/// Кирпичные, Крупнопанельные (1362)
		/// </summary>
		[Description("Кирпичные, Крупнопанельные")]
        [EnumCode("288")]
        [ShortTitle("")]
        KirpichnieKrupnopaneljnie = 1362,
		/// <summary>
		/// Кирпичные, Крупнопанельные, Из прочих материалов (1363)
		/// </summary>
		[Description("Кирпичные, Крупнопанельные, Из прочих материалов")]
        [EnumCode("289")]
        [ShortTitle("")]
        KirpichnieKrupnopaneljnieIzProchihMaterialov = 1363,
		/// <summary>
		/// Кирпичные, Легкие из местных материалов (1364)
		/// </summary>
		[Description("Кирпичные, Легкие из местных материалов")]
        [EnumCode("290")]
        [ShortTitle("")]
        KirpichnieLegkieIzMestnihMaterialov = 1364,
		/// <summary>
		/// Кирпичные, Металлические (1365)
		/// </summary>
		[Description("Кирпичные, Металлические")]
        [EnumCode("291")]
        [ShortTitle("")]
        KirpichnieMetallicheskie = 1365,
		/// <summary>
		/// Кирпичные, Монолитные (1366)
		/// </summary>
		[Description("Кирпичные, Монолитные")]
        [EnumCode("292")]
        [ShortTitle("")]
        KirpichnieMonolitnie = 1366,
		/// <summary>
		/// Кирпичные, Монолитные, Железобетонные (1367)
		/// </summary>
		[Description("Кирпичные, Монолитные, Железобетонные")]
        [EnumCode("293")]
        [ShortTitle("")]
        KirpichnieMonolitnieZhelezobetonnie = 1367,
		/// <summary>
		/// Кирпичные, Монолитные, Из прочих материалов (1368)
		/// </summary>
		[Description("Кирпичные, Монолитные, Из прочих материалов")]
        [EnumCode("294")]
        [ShortTitle("")]
        KirpichnieMonolitnieIzProchihMaterialov = 1368,
		/// <summary>
		/// Кирпичные, Монолитные, Металлические (1369)
		/// </summary>
		[Description("Кирпичные, Монолитные, Металлические")]
        [EnumCode("295")]
        [ShortTitle("")]
        KirpichnieMonolitnieMetallicheskie = 1369,
		/// <summary>
		/// Кирпичные, Рубленые (1370)
		/// </summary>
		[Description("Кирпичные, Рубленые")]
        [EnumCode("296")]
        [ShortTitle("")]
        KirpichnieRublenie = 1370,
		/// <summary>
		/// Кирпичные, Рубленые, Дощатые (1371)
		/// </summary>
		[Description("Кирпичные, Рубленые, Дощатые")]
        [EnumCode("297")]
        [ShortTitle("")]
        KirpichnieRublenieDoschatie = 1371,
		/// <summary>
		/// Кирпичные, Рубленые, Каркасно-обшивные (1372)
		/// </summary>
		[Description("Кирпичные, Рубленые, Каркасно-обшивные")]
        [EnumCode("298")]
        [ShortTitle("")]
        KirpichnieRublenieKarkasnoObshivnie = 1372,
		/// <summary>
		/// Кирпичные, Сборно-щитовые (1373)
		/// </summary>
		[Description("Кирпичные, Сборно-щитовые")]
        [EnumCode("299")]
        [ShortTitle("")]
        KirpichnieSbornoSchitovie = 1373,
		/// <summary>
		/// Кирпичные, Смешанные (1374)
		/// </summary>
		[Description("Кирпичные, Смешанные")]
        [EnumCode("300")]
        [ShortTitle("")]
        KirpichnieSmeshannie = 1374,
		/// <summary>
		/// Кирпичные, Смешанные, Шлакобетонные (1375)
		/// </summary>
		[Description("Кирпичные, Смешанные, Шлакобетонные")]
        [EnumCode("301")]
        [ShortTitle("")]
        KirpichnieSmeshannieShlakobetonnie = 1375,
		/// <summary>
		/// Кирпичные, Шлакобетонные (1376)
		/// </summary>
		[Description("Кирпичные, Шлакобетонные")]
        [EnumCode("302")]
        [ShortTitle("")]
        KirpichnieShlakobetonnie = 1376,
		/// <summary>
		/// Кирпичный (1377)
		/// </summary>
		[Description("Кирпичный")]
        [EnumCode("303")]
        [ShortTitle("")]
        Kirpichnij = 1377,
		/// <summary>
		/// Крупноблочные (1378)
		/// </summary>
		[Description("Крупноблочные")]
        [EnumCode("304")]
        [ShortTitle("")]
        Krupnoblochnie = 1378,
		/// <summary>
		/// Крупноблочные, Из мелких бетонных блоков (1379)
		/// </summary>
		[Description("Крупноблочные, Из мелких бетонных блоков")]
        [EnumCode("305")]
        [ShortTitle("")]
        KrupnoblochnieIzMelkihBetonnihBlokov = 1379,
		/// <summary>
		/// Крупноблочные, Из прочих материалов (1380)
		/// </summary>
		[Description("Крупноблочные, Из прочих материалов")]
        [EnumCode("306")]
        [ShortTitle("")]
        KrupnoblochnieIzProchihMaterialov = 1380,
		/// <summary>
		/// Крупноблочные, Кирпичные (1381)
		/// </summary>
		[Description("Крупноблочные, Кирпичные")]
        [EnumCode("307")]
        [ShortTitle("")]
        KrupnoblochnieKirpichnie = 1381,
		/// <summary>
		/// Крупноблочные, Кирпичные облегченные (1382)
		/// </summary>
		[Description("Крупноблочные, Кирпичные облегченные")]
        [EnumCode("308")]
        [ShortTitle("")]
        KrupnoblochnieKirpichnieOblegchennie = 1382,
		/// <summary>
		/// Крупноблочные, Кирпичные, Из прочих материалов (1383)
		/// </summary>
		[Description("Крупноблочные, Кирпичные, Из прочих материалов")]
        [EnumCode("309")]
        [ShortTitle("")]
        KrupnoblochnieKirpichnieIzProchihMaterialov = 1383,
		/// <summary>
		/// Крупноблочные, Монолитные (1384)
		/// </summary>
		[Description("Крупноблочные, Монолитные")]
        [EnumCode("310")]
        [ShortTitle("")]
        KrupnoblochnieMonolitnie = 1384,
		/// <summary>
		/// Крупнопанельные (1385)
		/// </summary>
		[Description("Крупнопанельные")]
        [EnumCode("311")]
        [ShortTitle("")]
        Krupnopaneljnie = 1385,
		/// <summary>
		/// Крупнопанельные, Бетонные (1386)
		/// </summary>
		[Description("Крупнопанельные, Бетонные")]
        [EnumCode("312")]
        [ShortTitle("")]
        KrupnopaneljnieBetonnie = 1386,
		/// <summary>
		/// Крупнопанельные, Железобетонные (1387)
		/// </summary>
		[Description("Крупнопанельные, Железобетонные")]
        [EnumCode("313")]
        [ShortTitle("")]
        KrupnopaneljnieZhelezobetonnie = 1387,
		/// <summary>
		/// Крупнопанельные, Из легкобетонных панелей (1388)
		/// </summary>
		[Description("Крупнопанельные, Из легкобетонных панелей")]
        [EnumCode("314")]
        [ShortTitle("")]
        KrupnopaneljnieIzLegkobetonnihPanelej = 1388,
		/// <summary>
		/// Крупнопанельные, Из мелких бетонных блоков (1389)
		/// </summary>
		[Description("Крупнопанельные, Из мелких бетонных блоков")]
        [EnumCode("315")]
        [ShortTitle("")]
        KrupnopaneljnieIzMelkihBetonnihBlokov = 1389,
		/// <summary>
		/// Крупнопанельные, Из прочих материалов (1390)
		/// </summary>
		[Description("Крупнопанельные, Из прочих материалов")]
        [EnumCode("316")]
        [ShortTitle("")]
        KrupnopaneljnieIzProchihMaterialov = 1390,
		/// <summary>
		/// Крупнопанельные, Кирпичные (1391)
		/// </summary>
		[Description("Крупнопанельные, Кирпичные")]
        [EnumCode("317")]
        [ShortTitle("")]
        KrupnopaneljnieKirpichnie = 1391,
		/// <summary>
		/// Крупнопанельные, Крупноблочные (1392)
		/// </summary>
		[Description("Крупнопанельные, Крупноблочные")]
        [EnumCode("318")]
        [ShortTitle("")]
        KrupnopaneljnieKrupnoblochnie = 1392,
		/// <summary>
		/// Крупнопанельные, Крупноблочные, Кирпичные (1393)
		/// </summary>
		[Description("Крупнопанельные, Крупноблочные, Кирпичные")]
        [EnumCode("319")]
        [ShortTitle("")]
        KrupnopaneljnieKrupnoblochnieKirpichnie = 1393,
		/// <summary>
		/// Крупнопанельные, Металлические (1394)
		/// </summary>
		[Description("Крупнопанельные, Металлические")]
        [EnumCode("320")]
        [ShortTitle("")]
        KrupnopaneljnieMetallicheskie = 1394,
		/// <summary>
		/// Легкие из местных материалов (1395)
		/// </summary>
		[Description("Легкие из местных материалов")]
        [EnumCode("321")]
        [ShortTitle("")]
        LegkieIzMestnihMaterialov = 1395,
		/// <summary>
		/// Легкие из местных материалов, Из легкобетонных панелей (1396)
		/// </summary>
		[Description("Легкие из местных материалов, Из легкобетонных панелей")]
        [EnumCode("322")]
        [ShortTitle("")]
        LegkieIzMestnihMaterialovIzLegkobetonnihPanelej = 1396,
		/// <summary>
		/// Легкие из местных материалов, Из мелких бетонных блоков (1397)
		/// </summary>
		[Description("Легкие из местных материалов, Из мелких бетонных блоков")]
        [EnumCode("323")]
        [ShortTitle("")]
        LegkieIzMestnihMaterialovIzMelkihBetonnihBlokov = 1397,
		/// <summary>
		/// Легкие из местных материалов, Кирпичные (1398)
		/// </summary>
		[Description("Легкие из местных материалов, Кирпичные")]
        [EnumCode("324")]
        [ShortTitle("")]
        LegkieIzMestnihMaterialovKirpichnie = 1398,
		/// <summary>
		/// Легкие из местных материалов, Монолитные (1399)
		/// </summary>
		[Description("Легкие из местных материалов, Монолитные")]
        [EnumCode("325")]
        [ShortTitle("")]
        LegkieIzMestnihMaterialovMonolitnie = 1399,
		/// <summary>
		/// Легкобетонные блоки (1400)
		/// </summary>
		[Description("Легкобетонные блоки")]
        [EnumCode("326")]
        [ShortTitle("")]
        LegkobetonnieBloki = 1400,
		/// <summary>
		/// Легкобетонные блоки с утеплением (1401)
		/// </summary>
		[Description("Легкобетонные блоки с утеплением")]
        [EnumCode("327")]
        [ShortTitle("")]
        LegkobetonnieBlokiSUtepleniem = 1401,
		/// <summary>
		/// Металлические (1402)
		/// </summary>
		[Description("Металлические")]
        [EnumCode("328")]
        [ShortTitle("")]
        Metallicheskie = 1402,
		/// <summary>
		/// Металлические, Дощатые (1403)
		/// </summary>
		[Description("Металлические, Дощатые")]
        [EnumCode("329")]
        [ShortTitle("")]
        MetallicheskieDoschatie = 1403,
		/// <summary>
		/// Металлические, Из прочих материалов (1404)
		/// </summary>
		[Description("Металлические, Из прочих материалов")]
        [EnumCode("330")]
        [ShortTitle("")]
        MetallicheskieIzProchihMaterialov = 1404,
		/// <summary>
		/// Металлические, Каркасно-обшивные (1405)
		/// </summary>
		[Description("Металлические, Каркасно-обшивные")]
        [EnumCode("331")]
        [ShortTitle("")]
        MetallicheskieKarkasnoObshivnie = 1405,
		/// <summary>
		/// Металлические, Кирпичные (1406)
		/// </summary>
		[Description("Металлические, Кирпичные")]
        [EnumCode("332")]
        [ShortTitle("")]
        MetallicheskieKirpichnie = 1406,
		/// <summary>
		/// Металлические, Кирпичные облегченные, Каркасно-обшивные (1407)
		/// </summary>
		[Description("Металлические, Кирпичные облегченные, Каркасно-обшивные")]
        [EnumCode("333")]
        [ShortTitle("")]
        MetallicheskieKirpichnieOblegchennieKarkasnoObshivnie = 1407,
		/// <summary>
		/// Металлические, Монолитные (1408)
		/// </summary>
		[Description("Металлические, Монолитные")]
        [EnumCode("334")]
        [ShortTitle("")]
        MetallicheskieMonolitnie = 1408,
		/// <summary>
		/// Металлические, Смешанные (1409)
		/// </summary>
		[Description("Металлические, Смешанные")]
        [EnumCode("335")]
        [ShortTitle("")]
        MetallicheskieSmeshannie = 1409,
		/// <summary>
		/// Монолиные (бетонные) (1410)
		/// </summary>
		[Description("Монолиные (бетонные)")]
        [EnumCode("336")]
        [ShortTitle("")]
        MonolinieBetonnie = 1410,
		/// <summary>
		/// Монолитные (ж-б) (1411)
		/// </summary>
		[Description("Монолитные (ж-б)")]
        [EnumCode("337")]
        [ShortTitle("")]
        MonolitnieZhB = 1411,
		/// <summary>
		/// Монолитные, Бетонные (1412)
		/// </summary>
		[Description("Монолитные, Бетонные")]
        [EnumCode("338")]
        [ShortTitle("")]
        MonolitnieBetonnie = 1412,
		/// <summary>
		/// Монолитные, Бетонные, Кирпичные, Каменные, Из мелких бетонных блоков (1413)
		/// </summary>
		[Description("Монолитные, Бетонные, Кирпичные, Каменные, Из мелких бетонных блоков")]
        [EnumCode("339")]
        [ShortTitle("")]
        MonolitnieBetonnieKirpichnieKamennieIzMelkihBetonnihBlokov = 1413,
		/// <summary>
		/// Монолитные, Железобетонные (1414)
		/// </summary>
		[Description("Монолитные, Железобетонные")]
        [EnumCode("340")]
        [ShortTitle("")]
        MonolitnieZhelezobetonnie = 1414,
		/// <summary>
		/// Монолитные, Железобетонные, Бетонные (1415)
		/// </summary>
		[Description("Монолитные, Железобетонные, Бетонные")]
        [EnumCode("341")]
        [ShortTitle("")]
        MonolitnieZhelezobetonnieBetonnie = 1415,
		/// <summary>
		/// Монолитные, Железобетонные, Из мелких бетонных блоков (1416)
		/// </summary>
		[Description("Монолитные, Железобетонные, Из мелких бетонных блоков")]
        [EnumCode("342")]
        [ShortTitle("")]
        MonolitnieZhelezobetonnieIzMelkihBetonnihBlokov = 1416,
		/// <summary>
		/// Монолитные, Железобетонные, Из прочих материалов (1417)
		/// </summary>
		[Description("Монолитные, Железобетонные, Из прочих материалов")]
        [EnumCode("343")]
        [ShortTitle("")]
        MonolitnieZhelezobetonnieIzProchihMaterialov = 1417,
		/// <summary>
		/// Монолитные, Железобетонные, Кирпичные (1418)
		/// </summary>
		[Description("Монолитные, Железобетонные, Кирпичные")]
        [EnumCode("344")]
        [ShortTitle("")]
        MonolitnieZhelezobetonnieKirpichnie = 1418,
		/// <summary>
		/// Монолитные, Железобетонные, Кирпичные, Из мелких бетонных блоков (1419)
		/// </summary>
		[Description("Монолитные, Железобетонные, Кирпичные, Из мелких бетонных блоков")]
        [EnumCode("345")]
        [ShortTitle("")]
        MonolitnieZhelezobetonnieKirpichnieIzMelkihBetonnihBlokov = 1419,
		/// <summary>
		/// Монолитные, Железобетонные, Металлические (1420)
		/// </summary>
		[Description("Монолитные, Железобетонные, Металлические")]
        [EnumCode("346")]
        [ShortTitle("")]
        MonolitnieZhelezobetonnieMetallicheskie = 1420,
		/// <summary>
		/// Монолитные, Из мелких бетонных блоков (1421)
		/// </summary>
		[Description("Монолитные, Из мелких бетонных блоков")]
        [EnumCode("347")]
        [ShortTitle("")]
        MonolitnieIzMelkihBetonnihBlokov = 1421,
		/// <summary>
		/// Монолитные, Из мелких бетонных блоков, Из прочих материалов (1422)
		/// </summary>
		[Description("Монолитные, Из мелких бетонных блоков, Из прочих материалов")]
        [EnumCode("348")]
        [ShortTitle("")]
        MonolitnieIzMelkihBetonnihBlokovIzProchihMaterialov = 1422,
		/// <summary>
		/// Монолитные, Из мелких бетонных блоков, Кирпичные облегченные (1423)
		/// </summary>
		[Description("Монолитные, Из мелких бетонных блоков, Кирпичные облегченные")]
        [EnumCode("349")]
        [ShortTitle("")]
        MonolitnieIzMelkihBetonnihBlokovKirpichnieOblegchennie = 1423,
		/// <summary>
		/// Монолитные, Из мелких бетонных блоков, Кирпичные, Из прочих материалов (1424)
		/// </summary>
		[Description("Монолитные, Из мелких бетонных блоков, Кирпичные, Из прочих материалов")]
        [EnumCode("350")]
        [ShortTitle("")]
        MonolitnieIzMelkihBetonnihBlokovKirpichnieIzProchihMaterialov = 1424,
		/// <summary>
		/// Монолитные, Из прочих материалов (1425)
		/// </summary>
		[Description("Монолитные, Из прочих материалов")]
        [EnumCode("351")]
        [ShortTitle("")]
        MonolitnieIzProchihMaterialov = 1425,
		/// <summary>
		/// Монолитные, Каркасно-панельные (1426)
		/// </summary>
		[Description("Монолитные, Каркасно-панельные")]
        [EnumCode("352")]
        [ShortTitle("")]
        MonolitnieKarkasnoPaneljnie = 1426,
		/// <summary>
		/// Монолитные, Кирпичные (1427)
		/// </summary>
		[Description("Монолитные, Кирпичные")]
        [EnumCode("353")]
        [ShortTitle("")]
        MonolitnieKirpichnie = 1427,
		/// <summary>
		/// Монолитные, Кирпичные, Бетонные (1428)
		/// </summary>
		[Description("Монолитные, Кирпичные, Бетонные")]
        [EnumCode("354")]
        [ShortTitle("")]
        MonolitnieKirpichnieBetonnie = 1428,
		/// <summary>
		/// Монолитные, Кирпичные, Железобетонные (1429)
		/// </summary>
		[Description("Монолитные, Кирпичные, Железобетонные")]
        [EnumCode("355")]
        [ShortTitle("")]
        MonolitnieKirpichnieZhelezobetonnie = 1429,
		/// <summary>
		/// Монолитные, Кирпичные, Из мелких бетонных блоков (1430)
		/// </summary>
		[Description("Монолитные, Кирпичные, Из мелких бетонных блоков")]
        [EnumCode("356")]
        [ShortTitle("")]
        MonolitnieKirpichnieIzMelkihBetonnihBlokov = 1430,
		/// <summary>
		/// Монолитные, Кирпичные, Из мелких бетонных блоков, Из прочих материалов (1431)
		/// </summary>
		[Description("Монолитные, Кирпичные, Из мелких бетонных блоков, Из прочих материалов")]
        [EnumCode("357")]
        [ShortTitle("")]
        MonolitnieKirpichnieIzMelkihBetonnihBlokovIzProchihMaterialov = 1431,
		/// <summary>
		/// Монолитные, Кирпичные, Из природного камня (1432)
		/// </summary>
		[Description("Монолитные, Кирпичные, Из природного камня")]
        [EnumCode("358")]
        [ShortTitle("")]
        MonolitnieKirpichnieIzPrirodnogoKamnya = 1432,
		/// <summary>
		/// Монолитные, Кирпичные, Из прочих материалов (1433)
		/// </summary>
		[Description("Монолитные, Кирпичные, Из прочих материалов")]
        [EnumCode("359")]
        [ShortTitle("")]
        MonolitnieKirpichnieIzProchihMaterialov = 1433,
		/// <summary>
		/// Монолитные, Крупноблочные (1434)
		/// </summary>
		[Description("Монолитные, Крупноблочные")]
        [EnumCode("360")]
        [ShortTitle("")]
        MonolitnieKrupnoblochnie = 1434,
		/// <summary>
		/// Монолитные, Крупнопанельные (1435)
		/// </summary>
		[Description("Монолитные, Крупнопанельные")]
        [EnumCode("361")]
        [ShortTitle("")]
        MonolitnieKrupnopaneljnie = 1435,
		/// <summary>
		/// Монолитные, Металлические (1436)
		/// </summary>
		[Description("Монолитные, Металлические")]
        [EnumCode("362")]
        [ShortTitle("")]
        MonolitnieMetallicheskie = 1436,
		/// <summary>
		/// Монолитные, Шлакобетонные (1437)
		/// </summary>
		[Description("Монолитные, Шлакобетонные")]
        [EnumCode("363")]
        [ShortTitle("")]
        MonolitnieShlakobetonnie = 1437,
		/// <summary>
		/// Монолитный железобетон (1438)
		/// </summary>
		[Description("Монолитный железобетон")]
        [EnumCode("364")]
        [ShortTitle("")]
        MonolitnijZhelezobeton = 1438,
		/// <summary>
		/// Не установлен (1439)
		/// </summary>
		[Description("Не установлен")]
        [EnumCode("365")]
        [ShortTitle("")]
        NeUstanovlen = 1439,
		/// <summary>
		/// Панели типа "Сэндвич" (1440)
		/// </summary>
		[Description("Панели типа \"Сэндвич\"")]
        [EnumCode("366")]
        [ShortTitle("")]
        PaneliTipaSendvich = 1440,
		/// <summary>
		/// Панельные (1441)
		/// </summary>
		[Description("Панельные")]
        [EnumCode("367")]
        [ShortTitle("")]
        Paneljnie = 1441,
		/// <summary>
		/// Пеноблоки (1442)
		/// </summary>
		[Description("Пеноблоки")]
        [EnumCode("368")]
        [ShortTitle("")]
        Penobloki = 1442,
		/// <summary>
		/// Прочие (каркасно-засыпные) (1443)
		/// </summary>
		[Description("Прочие (каркасно-засыпные)")]
        [EnumCode("369")]
        [ShortTitle("")]
        ProchieKarkasnoZasipnie = 1443,
		/// <summary>
		/// Рубленые (1444)
		/// </summary>
		[Description("Рубленые")]
        [EnumCode("370")]
        [ShortTitle("")]
        Rublenie = 1444,
		/// <summary>
		/// Рубленые, Деревянные (1445)
		/// </summary>
		[Description("Рубленые, Деревянные")]
        [EnumCode("371")]
        [ShortTitle("")]
        RublenieDerevyannie = 1445,
		/// <summary>
		/// Рубленые, Дощатые (1446)
		/// </summary>
		[Description("Рубленые, Дощатые")]
        [EnumCode("372")]
        [ShortTitle("")]
        RublenieDoschatie = 1446,
		/// <summary>
		/// Рубленые, Дощатые, Из мелких бетонных блоков (1447)
		/// </summary>
		[Description("Рубленые, Дощатые, Из мелких бетонных блоков")]
        [EnumCode("373")]
        [ShortTitle("")]
        RublenieDoschatieIzMelkihBetonnihBlokov = 1447,
		/// <summary>
		/// Рубленые, Дощатые, Кирпичные (1448)
		/// </summary>
		[Description("Рубленые, Дощатые, Кирпичные")]
        [EnumCode("374")]
        [ShortTitle("")]
        RublenieDoschatieKirpichnie = 1448,
		/// <summary>
		/// Рубленые, Железобетонные (1449)
		/// </summary>
		[Description("Рубленые, Железобетонные")]
        [EnumCode("375")]
        [ShortTitle("")]
        RublenieZhelezobetonnie = 1449,
		/// <summary>
		/// Рубленые, Из легкобетонных панелей (1450)
		/// </summary>
		[Description("Рубленые, Из легкобетонных панелей")]
        [EnumCode("376")]
        [ShortTitle("")]
        RublenieIzLegkobetonnihPanelej = 1450,
		/// <summary>
		/// Рубленые, Из мелких бетонных блоков (1451)
		/// </summary>
		[Description("Рубленые, Из мелких бетонных блоков")]
        [EnumCode("377")]
        [ShortTitle("")]
        RublenieIzMelkihBetonnihBlokov = 1451,
		/// <summary>
		/// Рубленые, Из мелких бетонных блоков, Каркасно-обшивные (1452)
		/// </summary>
		[Description("Рубленые, Из мелких бетонных блоков, Каркасно-обшивные")]
        [EnumCode("378")]
        [ShortTitle("")]
        RublenieIzMelkihBetonnihBlokovKarkasnoObshivnie = 1452,
		/// <summary>
		/// Рубленые, Из мелких бетонных блоков, Монолитные (1453)
		/// </summary>
		[Description("Рубленые, Из мелких бетонных блоков, Монолитные")]
        [EnumCode("379")]
        [ShortTitle("")]
        RublenieIzMelkihBetonnihBlokovMonolitnie = 1453,
		/// <summary>
		/// Рубленые, Из прочих материалов (1454)
		/// </summary>
		[Description("Рубленые, Из прочих материалов")]
        [EnumCode("380")]
        [ShortTitle("")]
        RublenieIzProchihMaterialov = 1454,
		/// <summary>
		/// Рубленые, Каркасно-засыпные (1455)
		/// </summary>
		[Description("Рубленые, Каркасно-засыпные")]
        [EnumCode("381")]
        [ShortTitle("")]
        RublenieKarkasnoZasipnie = 1455,
		/// <summary>
		/// Рубленые, Каркасно-обшивные (1456)
		/// </summary>
		[Description("Рубленые, Каркасно-обшивные")]
        [EnumCode("382")]
        [ShortTitle("")]
        RublenieKarkasnoObshivnie = 1456,
		/// <summary>
		/// Рубленые, Каркасно-обшивные, Из мелких бетонных блоков (1457)
		/// </summary>
		[Description("Рубленые, Каркасно-обшивные, Из мелких бетонных блоков")]
        [EnumCode("383")]
        [ShortTitle("")]
        RublenieKarkasnoObshivnieIzMelkihBetonnihBlokov = 1457,
		/// <summary>
		/// Рубленые, Кирпичные (1458)
		/// </summary>
		[Description("Рубленые, Кирпичные")]
        [EnumCode("384")]
        [ShortTitle("")]
        RublenieKirpichnie = 1458,
		/// <summary>
		/// Рубленые, Кирпичные облегченные (1459)
		/// </summary>
		[Description("Рубленые, Кирпичные облегченные")]
        [EnumCode("385")]
        [ShortTitle("")]
        RublenieKirpichnieOblegchennie = 1459,
		/// <summary>
		/// Рубленые, Крупнопанельные (1460)
		/// </summary>
		[Description("Рубленые, Крупнопанельные")]
        [EnumCode("386")]
        [ShortTitle("")]
        RublenieKrupnopaneljnie = 1460,
		/// <summary>
		/// Рубленые, Легкие из местных материалов (1461)
		/// </summary>
		[Description("Рубленые, Легкие из местных материалов")]
        [EnumCode("387")]
        [ShortTitle("")]
        RublenieLegkieIzMestnihMaterialov = 1461,
		/// <summary>
		/// Рубленые, Металлические (1462)
		/// </summary>
		[Description("Рубленые, Металлические")]
        [EnumCode("388")]
        [ShortTitle("")]
        RublenieMetallicheskie = 1462,
		/// <summary>
		/// Рубленые, Монолитные (1463)
		/// </summary>
		[Description("Рубленые, Монолитные")]
        [EnumCode("389")]
        [ShortTitle("")]
        RublenieMonolitnie = 1463,
		/// <summary>
		/// Рубленые, Сборно-щитовые (1464)
		/// </summary>
		[Description("Рубленые, Сборно-щитовые")]
        [EnumCode("390")]
        [ShortTitle("")]
        RublenieSbornoSchitovie = 1464,
		/// <summary>
		/// Рубленые, Шлакобетонные (1465)
		/// </summary>
		[Description("Рубленые, Шлакобетонные")]
        [EnumCode("391")]
        [ShortTitle("")]
        RublenieShlakobetonnie = 1465,
		/// <summary>
		/// Сборно-щитовые (1466)
		/// </summary>
		[Description("Сборно-щитовые")]
        [EnumCode("392")]
        [ShortTitle("")]
        SbornoSchitovie = 1466,
		/// <summary>
		/// Сборно-щитовые, Дощатые (1467)
		/// </summary>
		[Description("Сборно-щитовые, Дощатые")]
        [EnumCode("393")]
        [ShortTitle("")]
        SbornoSchitovieDoschatie = 1467,
		/// <summary>
		/// Сборно-щитовые, Из легкобетонных панелей (1468)
		/// </summary>
		[Description("Сборно-щитовые, Из легкобетонных панелей")]
        [EnumCode("394")]
        [ShortTitle("")]
        SbornoSchitovieIzLegkobetonnihPanelej = 1468,
		/// <summary>
		/// Сборно-щитовые, Из мелких бетонных блоков (1469)
		/// </summary>
		[Description("Сборно-щитовые, Из мелких бетонных блоков")]
        [EnumCode("395")]
        [ShortTitle("")]
        SbornoSchitovieIzMelkihBetonnihBlokov = 1469,
		/// <summary>
		/// Сборно-щитовые, Из прочих материалов (1470)
		/// </summary>
		[Description("Сборно-щитовые, Из прочих материалов")]
        [EnumCode("396")]
        [ShortTitle("")]
        SbornoSchitovieIzProchihMaterialov = 1470,
		/// <summary>
		/// Сборно-щитовые, Кирпичные облегченные (1471)
		/// </summary>
		[Description("Сборно-щитовые, Кирпичные облегченные")]
        [EnumCode("397")]
        [ShortTitle("")]
        SbornoSchitovieKirpichnieOblegchennie = 1471,
		/// <summary>
		/// Сборно-щитовые, Металлические (1472)
		/// </summary>
		[Description("Сборно-щитовые, Металлические")]
        [EnumCode("398")]
        [ShortTitle("")]
        SbornoSchitovieMetallicheskie = 1472,
		/// <summary>
		/// Сборно-щитовые, Рубленые (1473)
		/// </summary>
		[Description("Сборно-щитовые, Рубленые")]
        [EnumCode("399")]
        [ShortTitle("")]
        SbornoSchitovieRublenie = 1473,
		/// <summary>
		/// Смешанные (1474)
		/// </summary>
		[Description("Смешанные")]
        [EnumCode("400")]
        [ShortTitle("")]
        Smeshannie = 1474,
		/// <summary>
		/// Смешанные, Бетонные, Железобетонные (1475)
		/// </summary>
		[Description("Смешанные, Бетонные, Железобетонные")]
        [EnumCode("401")]
        [ShortTitle("")]
        SmeshannieBetonnieZhelezobetonnie = 1475,
		/// <summary>
		/// Смешанные, Бетонные, Каркасно-панельные (1476)
		/// </summary>
		[Description("Смешанные, Бетонные, Каркасно-панельные")]
        [EnumCode("402")]
        [ShortTitle("")]
        SmeshannieBetonnieKarkasnoPaneljnie = 1476,
		/// <summary>
		/// Смешанные, Деревянные, Дощатые (1477)
		/// </summary>
		[Description("Смешанные, Деревянные, Дощатые")]
        [EnumCode("403")]
        [ShortTitle("")]
        SmeshannieDerevyannieDoschatie = 1477,
		/// <summary>
		/// Смешанные, Из прочих материалов (1478)
		/// </summary>
		[Description("Смешанные, Из прочих материалов")]
        [EnumCode("404")]
        [ShortTitle("")]
        SmeshannieIzProchihMaterialov = 1478,
		/// <summary>
		/// Смешанные, Каменные и бетонные (1479)
		/// </summary>
		[Description("Смешанные, Каменные и бетонные")]
        [EnumCode("405")]
        [ShortTitle("")]
        SmeshannieKamennieIBetonnie = 1479,
		/// <summary>
		/// Смешанные, Каменные и деревянные (1480)
		/// </summary>
		[Description("Смешанные, Каменные и деревянные")]
        [EnumCode("406")]
        [ShortTitle("")]
        SmeshannieKamennieIDerevyannie = 1480,
		/// <summary>
		/// Смешанные, Кирпичные (1481)
		/// </summary>
		[Description("Смешанные, Кирпичные")]
        [EnumCode("407")]
        [ShortTitle("")]
        SmeshannieKirpichnie = 1481,
		/// <summary>
		/// Стеклопакеты сплошные (1482)
		/// </summary>
		[Description("Стеклопакеты сплошные")]
        [EnumCode("408")]
        [ShortTitle("")]
        SteklopaketiSploshnie = 1482,
		/// <summary>
		/// Стены (1483)
		/// </summary>
		[Description("Стены")]
        [EnumCode("409")]
        [ShortTitle("")]
        Steni = 1483,
		/// <summary>
		/// Шлакобетонные (1484)
		/// </summary>
		[Description("Шлакобетонные")]
        [EnumCode("410")]
        [ShortTitle("")]
        Shlakobetonnie = 1484,
		/// <summary>
		/// Шлакобетонные и шлакоблочные из тепл.бетона и шлакозалив. (1485)
		/// </summary>
		[Description("Шлакобетонные и шлакоблочные из тепл.бетона и шлакозалив.")]
        [EnumCode("411")]
        [ShortTitle("")]
        ShlakobetonnieIShlakoblochnieIzTeplBetonaIShlakozaliv = 1485,
		/// <summary>
		/// Шлакобетонные, Деревянный каркас без обшивки (1486)
		/// </summary>
		[Description("Шлакобетонные, Деревянный каркас без обшивки")]
        [EnumCode("412")]
        [ShortTitle("")]
        ShlakobetonnieDerevyannijKarkasBezObshivki = 1486,
		/// <summary>
		/// Шлакобетонные, Дощатые (1487)
		/// </summary>
		[Description("Шлакобетонные, Дощатые")]
        [EnumCode("413")]
        [ShortTitle("")]
        ShlakobetonnieDoschatie = 1487,
		/// <summary>
		/// Шлакобетонные, Железобетонные (1488)
		/// </summary>
		[Description("Шлакобетонные, Железобетонные")]
        [EnumCode("414")]
        [ShortTitle("")]
        ShlakobetonnieZhelezobetonnie = 1488,
		/// <summary>
		/// Шлакобетонные, Из легкобетонных панелей (1489)
		/// </summary>
		[Description("Шлакобетонные, Из легкобетонных панелей")]
        [EnumCode("415")]
        [ShortTitle("")]
        ShlakobetonnieIzLegkobetonnihPanelej = 1489,
		/// <summary>
		/// Шлакобетонные, Из мелких бетонных блоков (1490)
		/// </summary>
		[Description("Шлакобетонные, Из мелких бетонных блоков")]
        [EnumCode("416")]
        [ShortTitle("")]
        ShlakobetonnieIzMelkihBetonnihBlokov = 1490,
		/// <summary>
		/// Шлакобетонные, Из прочих материалов (1491)
		/// </summary>
		[Description("Шлакобетонные, Из прочих материалов")]
        [EnumCode("417")]
        [ShortTitle("")]
        ShlakobetonnieIzProchihMaterialov = 1491,
		/// <summary>
		/// Шлакобетонные, Из унифицированных железобетонных элементов (1492)
		/// </summary>
		[Description("Шлакобетонные, Из унифицированных железобетонных элементов")]
        [EnumCode("418")]
        [ShortTitle("")]
        ShlakobetonnieIzUnificirovannihZhelezobetonnihElementov = 1492,
		/// <summary>
		/// Шлакобетонные, Каменные и бетонные (1493)
		/// </summary>
		[Description("Шлакобетонные, Каменные и бетонные")]
        [EnumCode("419")]
        [ShortTitle("")]
        ShlakobetonnieKamennieIBetonnie = 1493,
		/// <summary>
		/// Шлакобетонные, Каменные и деревянные (1494)
		/// </summary>
		[Description("Шлакобетонные, Каменные и деревянные")]
        [EnumCode("420")]
        [ShortTitle("")]
        ShlakobetonnieKamennieIDerevyannie = 1494,
		/// <summary>
		/// Шлакобетонные, Каркасно-засыпные (1495)
		/// </summary>
		[Description("Шлакобетонные, Каркасно-засыпные")]
        [EnumCode("421")]
        [ShortTitle("")]
        ShlakobetonnieKarkasnoZasipnie = 1495,
		/// <summary>
		/// Шлакобетонные, Каркасно-обшивные (1496)
		/// </summary>
		[Description("Шлакобетонные, Каркасно-обшивные")]
        [EnumCode("422")]
        [ShortTitle("")]
        ShlakobetonnieKarkasnoObshivnie = 1496,
		/// <summary>
		/// Шлакобетонные, Кирпичные (1497)
		/// </summary>
		[Description("Шлакобетонные, Кирпичные")]
        [EnumCode("423")]
        [ShortTitle("")]
        ShlakobetonnieKirpichnie = 1497,
		/// <summary>
		/// Шлакобетонные, Кирпичные облегченные (1498)
		/// </summary>
		[Description("Шлакобетонные, Кирпичные облегченные")]
        [EnumCode("424")]
        [ShortTitle("")]
        ShlakobetonnieKirpichnieOblegchennie = 1498,
		/// <summary>
		/// Шлакобетонные, Кирпичные облегченные, Дощатые (1499)
		/// </summary>
		[Description("Шлакобетонные, Кирпичные облегченные, Дощатые")]
        [EnumCode("425")]
        [ShortTitle("")]
        ShlakobetonnieKirpichnieOblegchennieDoschatie = 1499,
		/// <summary>
		/// Шлакобетонные, Кирпичные облегченные, Из легкобетонных панелей (1500)
		/// </summary>
		[Description("Шлакобетонные, Кирпичные облегченные, Из легкобетонных панелей")]
        [EnumCode("426")]
        [ShortTitle("")]
        ShlakobetonnieKirpichnieOblegchennieIzLegkobetonnihPanelej = 1500,
		/// <summary>
		/// Шлакобетонные, Кирпичные облегченные, Из мелких бетонных блоков (1501)
		/// </summary>
		[Description("Шлакобетонные, Кирпичные облегченные, Из мелких бетонных блоков")]
        [EnumCode("427")]
        [ShortTitle("")]
        ShlakobetonnieKirpichnieOblegchennieIzMelkihBetonnihBlokov = 1501,
		/// <summary>
		/// Шлакобетонные, Кирпичные, Железобетонные (1502)
		/// </summary>
		[Description("Шлакобетонные, Кирпичные, Железобетонные")]
        [EnumCode("428")]
        [ShortTitle("")]
        ShlakobetonnieKirpichnieZhelezobetonnie = 1502,
		/// <summary>
		/// Шлакобетонные, Металлические (1503)
		/// </summary>
		[Description("Шлакобетонные, Металлические")]
        [EnumCode("429")]
        [ShortTitle("")]
        ShlakobetonnieMetallicheskie = 1503,
		/// <summary>
		/// Шлакобетонные, Рубленые (1504)
		/// </summary>
		[Description("Шлакобетонные, Рубленые")]
        [EnumCode("430")]
        [ShortTitle("")]
        ShlakobetonnieRublenie = 1504,
		/// <summary>
		/// Шлакобетонные, Рубленые, Дощатые (1505)
		/// </summary>
		[Description("Шлакобетонные, Рубленые, Дощатые")]
        [EnumCode("431")]
        [ShortTitle("")]
        ShlakobetonnieRublenieDoschatie = 1505,
		/// <summary>
		/// Шлакобетонные, Сборно-щитовые (1506)
		/// </summary>
		[Description("Шлакобетонные, Сборно-щитовые")]
        [EnumCode("432")]
        [ShortTitle("")]
        ShlakobetonnieSbornoSchitovie = 1506,
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
		/// <summary>
		/// B- (1000902)
		/// </summary>
		[Description("B-")]
        [EnumCode("6")]
        [ShortTitle("")]
        Bminus = 1000902,
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
		/// <summary>
		/// Некорректная цена (807)
		/// </summary>
		[Description("Некорректная цена")]
        [EnumCode("18")]
        [ShortTitle("")]
        UnacceptablePrice = 807,
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
        [ShortTitle("Центральный")]
        CAO = 903,
		/// <summary>
		/// САО (904)
		/// </summary>
		[Description("САО")]
        [EnumCode("2")]
        [ShortTitle("Северный")]
        SAO = 904,
		/// <summary>
		/// СВАО (905)
		/// </summary>
		[Description("СВАО")]
        [EnumCode("3")]
        [ShortTitle("Северо-Восточный")]
        SVAO = 905,
		/// <summary>
		/// ВАО (906)
		/// </summary>
		[Description("ВАО")]
        [EnumCode("4")]
        [ShortTitle("Восточный")]
        VAO = 906,
		/// <summary>
		/// ЮВАО (907)
		/// </summary>
		[Description("ЮВАО")]
        [EnumCode("5")]
        [ShortTitle("Юго-Восточный")]
        YVAO = 907,
		/// <summary>
		/// ЮАО (908)
		/// </summary>
		[Description("ЮАО")]
        [EnumCode("6")]
        [ShortTitle("Южный")]
        YAO = 908,
		/// <summary>
		/// ЮЗАО (909)
		/// </summary>
		[Description("ЮЗАО")]
        [EnumCode("7")]
        [ShortTitle("Юго-Западный")]
        YZAO = 909,
		/// <summary>
		/// ЗАО (910)
		/// </summary>
		[Description("ЗАО")]
        [EnumCode("8")]
        [ShortTitle("Западный")]
        ZAO = 910,
		/// <summary>
		/// СЗАО (911)
		/// </summary>
		[Description("СЗАО")]
        [EnumCode("9")]
        [ShortTitle("Северо-Западный")]
        SZAO = 911,
		/// <summary>
		/// ЗелАО (912)
		/// </summary>
		[Description("ЗелАО")]
        [EnumCode("10")]
        [ShortTitle("Зеленоградский")]
        ZelAO = 912,
		/// <summary>
		/// НАО (913)
		/// </summary>
		[Description("НАО")]
        [EnumCode("11")]
        [ShortTitle("НАО")]
        NAO = 913,
		/// <summary>
		/// ТАО (914)
		/// </summary>
		[Description("ТАО")]
        [EnumCode("12")]
        [ShortTitle("ТАО")]
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
		/// <summary>
		/// Алексеевский (941)
		/// </summary>
		[Description("Алексеевский")]
        [EnumCode("27")]
        [ShortTitle("")]
        Alexeyevsky = 941,
		/// <summary>
		/// Алтуфьевский (942)
		/// </summary>
		[Description("Алтуфьевский")]
        [EnumCode("28")]
        [ShortTitle("")]
        Altufyevsky = 942,
		/// <summary>
		/// Бабушкинский (943)
		/// </summary>
		[Description("Бабушкинский")]
        [EnumCode("29")]
        [ShortTitle("")]
        Babushkinsky = 943,
		/// <summary>
		/// Бибирево (944)
		/// </summary>
		[Description("Бибирево")]
        [EnumCode("30")]
        [ShortTitle("")]
        Bibirevo = 944,
		/// <summary>
		/// Бутырский (945)
		/// </summary>
		[Description("Бутырский")]
        [EnumCode("31")]
        [ShortTitle("")]
        Butyrsky = 945,
		/// <summary>
		/// Лианозово (946)
		/// </summary>
		[Description("Лианозово")]
        [EnumCode("32")]
        [ShortTitle("")]
        Lianozovo = 946,
		/// <summary>
		/// Лосиноостровский (947)
		/// </summary>
		[Description("Лосиноостровский")]
        [EnumCode("33")]
        [ShortTitle("")]
        Losinoostrovsky = 947,
		/// <summary>
		/// Марфино (948)
		/// </summary>
		[Description("Марфино")]
        [EnumCode("34")]
        [ShortTitle("")]
        Marfino = 948,
		/// <summary>
		/// Марьина роща (949)
		/// </summary>
		[Description("Марьина роща")]
        [EnumCode("35")]
        [ShortTitle("")]
        MaryinaRoshcha = 949,
		/// <summary>
		/// Останкинский (950)
		/// </summary>
		[Description("Останкинский")]
        [EnumCode("36")]
        [ShortTitle("")]
        Ostankinsky = 950,
		/// <summary>
		/// Отрадное (951)
		/// </summary>
		[Description("Отрадное")]
        [EnumCode("37")]
        [ShortTitle("")]
        Otradnoye = 951,
		/// <summary>
		/// Ростокино (952)
		/// </summary>
		[Description("Ростокино")]
        [EnumCode("38")]
        [ShortTitle("")]
        Rostokino = 952,
		/// <summary>
		/// Свиблово (953)
		/// </summary>
		[Description("Свиблово")]
        [EnumCode("39")]
        [ShortTitle("")]
        Sviblovo = 953,
		/// <summary>
		/// Северное Медведково (954)
		/// </summary>
		[Description("Северное Медведково")]
        [EnumCode("40")]
        [ShortTitle("")]
        SevernoyeMedvedkovo = 954,
		/// <summary>
		/// Северный (955)
		/// </summary>
		[Description("Северный")]
        [EnumCode("41")]
        [ShortTitle("")]
        Severny = 955,
		/// <summary>
		/// Южное Медведково (956)
		/// </summary>
		[Description("Южное Медведково")]
        [EnumCode("42")]
        [ShortTitle("")]
        YuzhnoyeMedvedkovo = 956,
		/// <summary>
		/// Ярославский (957)
		/// </summary>
		[Description("Ярославский")]
        [EnumCode("43")]
        [ShortTitle("")]
        Yaroslavsky = 957,
		/// <summary>
		/// Богородское (958)
		/// </summary>
		[Description("Богородское")]
        [EnumCode("44")]
        [ShortTitle("")]
        Bogorodskoye = 958,
		/// <summary>
		/// Вешняки (959)
		/// </summary>
		[Description("Вешняки")]
        [EnumCode("45")]
        [ShortTitle("")]
        Veshnyaki = 959,
		/// <summary>
		/// Восточное Измайлово (960)
		/// </summary>
		[Description("Восточное Измайлово")]
        [EnumCode("46")]
        [ShortTitle("")]
        VostochnoyeIzmaylovo = 960,
		/// <summary>
		/// Восточный (961)
		/// </summary>
		[Description("Восточный")]
        [EnumCode("47")]
        [ShortTitle("")]
        Vostochny = 961,
		/// <summary>
		/// Гольяново (962)
		/// </summary>
		[Description("Гольяново")]
        [EnumCode("48")]
        [ShortTitle("")]
        Golyanovo = 962,
		/// <summary>
		/// Ивановское (963)
		/// </summary>
		[Description("Ивановское")]
        [EnumCode("49")]
        [ShortTitle("")]
        Ivanovskoye = 963,
		/// <summary>
		/// Измайлово (964)
		/// </summary>
		[Description("Измайлово")]
        [EnumCode("50")]
        [ShortTitle("")]
        Izmaylovo = 964,
		/// <summary>
		/// Косино-Ухтомский (965)
		/// </summary>
		[Description("Косино-Ухтомский")]
        [EnumCode("51")]
        [ShortTitle("")]
        KosinoUkhtomsky = 965,
		/// <summary>
		/// Метрогородок (966)
		/// </summary>
		[Description("Метрогородок")]
        [EnumCode("52")]
        [ShortTitle("")]
        Metrogorodok = 966,
		/// <summary>
		/// Новогиреево (967)
		/// </summary>
		[Description("Новогиреево")]
        [EnumCode("53")]
        [ShortTitle("")]
        Novogireyevo = 967,
		/// <summary>
		/// Новокосино (968)
		/// </summary>
		[Description("Новокосино")]
        [EnumCode("54")]
        [ShortTitle("")]
        Novokosino = 968,
		/// <summary>
		/// Перово (969)
		/// </summary>
		[Description("Перово")]
        [EnumCode("55")]
        [ShortTitle("")]
        Perovo = 969,
		/// <summary>
		/// Преображенское (970)
		/// </summary>
		[Description("Преображенское")]
        [EnumCode("56")]
        [ShortTitle("")]
        Preobrazhenskoye = 970,
		/// <summary>
		/// Северное Измайлово (971)
		/// </summary>
		[Description("Северное Измайлово")]
        [EnumCode("57")]
        [ShortTitle("")]
        SevernoyeIzmaylovo = 971,
		/// <summary>
		/// Соколиная гора (972)
		/// </summary>
		[Description("Соколиная гора")]
        [EnumCode("58")]
        [ShortTitle("")]
        SokolinayaGora = 972,
		/// <summary>
		/// Сокольники (973)
		/// </summary>
		[Description("Сокольники")]
        [EnumCode("59")]
        [ShortTitle("")]
        Sokolniki = 973,
		/// <summary>
		/// Выхино-Жулебино (974)
		/// </summary>
		[Description("Выхино-Жулебино")]
        [EnumCode("60")]
        [ShortTitle("")]
        VykhinoZhulebino = 974,
		/// <summary>
		/// Капотня (975)
		/// </summary>
		[Description("Капотня")]
        [EnumCode("61")]
        [ShortTitle("")]
        Kapotnya = 975,
		/// <summary>
		/// Кузьминки (976)
		/// </summary>
		[Description("Кузьминки")]
        [EnumCode("62")]
        [ShortTitle("")]
        Kuzminki = 976,
		/// <summary>
		/// Лефортово (977)
		/// </summary>
		[Description("Лефортово")]
        [EnumCode("63")]
        [ShortTitle("")]
        Lefortovo = 977,
		/// <summary>
		/// Люблино (978)
		/// </summary>
		[Description("Люблино")]
        [EnumCode("64")]
        [ShortTitle("")]
        Lyublino = 978,
		/// <summary>
		/// Марьино (979)
		/// </summary>
		[Description("Марьино")]
        [EnumCode("65")]
        [ShortTitle("")]
        Maryino = 979,
		/// <summary>
		/// Некрасовка (980)
		/// </summary>
		[Description("Некрасовка")]
        [EnumCode("66")]
        [ShortTitle("")]
        Nekrasovka = 980,
		/// <summary>
		/// Нижегородский (981)
		/// </summary>
		[Description("Нижегородский")]
        [EnumCode("67")]
        [ShortTitle("")]
        Nizhegorodsky = 981,
		/// <summary>
		/// Печатники (982)
		/// </summary>
		[Description("Печатники")]
        [EnumCode("68")]
        [ShortTitle("")]
        Pechatniki = 982,
		/// <summary>
		/// Рязанский (983)
		/// </summary>
		[Description("Рязанский")]
        [EnumCode("69")]
        [ShortTitle("")]
        Ryazansky = 983,
		/// <summary>
		/// Текстильщики (984)
		/// </summary>
		[Description("Текстильщики")]
        [EnumCode("70")]
        [ShortTitle("")]
        Tekstilshchiki = 984,
		/// <summary>
		/// Южнопортовый (985)
		/// </summary>
		[Description("Южнопортовый")]
        [EnumCode("71")]
        [ShortTitle("")]
        Yuzhnoportovy = 985,
		/// <summary>
		/// Бирюлёво Восточное (986)
		/// </summary>
		[Description("Бирюлёво Восточное")]
        [EnumCode("72")]
        [ShortTitle("")]
        BiryulyovoVostochnoye = 986,
		/// <summary>
		/// Бирюлёво Западное (987)
		/// </summary>
		[Description("Бирюлёво Западное")]
        [EnumCode("73")]
        [ShortTitle("")]
        BiryulyovoZapadnoye = 987,
		/// <summary>
		/// Братеево (988)
		/// </summary>
		[Description("Братеево")]
        [EnumCode("74")]
        [ShortTitle("")]
        Brateyevo = 988,
		/// <summary>
		/// Даниловский (989)
		/// </summary>
		[Description("Даниловский")]
        [EnumCode("75")]
        [ShortTitle("")]
        Danilovsky = 989,
		/// <summary>
		/// Донской (990)
		/// </summary>
		[Description("Донской")]
        [EnumCode("76")]
        [ShortTitle("")]
        Donskoy = 990,
		/// <summary>
		/// Зябликово (991)
		/// </summary>
		[Description("Зябликово")]
        [EnumCode("77")]
        [ShortTitle("")]
        Zyablikovo = 991,
		/// <summary>
		/// Москворечье-Сабурово (992)
		/// </summary>
		[Description("Москворечье-Сабурово")]
        [EnumCode("78")]
        [ShortTitle("")]
        MoskvorechyeSaburovo = 992,
		/// <summary>
		/// Нагатино-Садовники (993)
		/// </summary>
		[Description("Нагатино-Садовники")]
        [EnumCode("79")]
        [ShortTitle("")]
        NagatinoSadovniki = 993,
		/// <summary>
		/// Нагатинский Затон (994)
		/// </summary>
		[Description("Нагатинский Затон")]
        [EnumCode("80")]
        [ShortTitle("")]
        NagatinskyZaton = 994,
		/// <summary>
		/// Нагорный (995)
		/// </summary>
		[Description("Нагорный")]
        [EnumCode("81")]
        [ShortTitle("")]
        Nagorny = 995,
		/// <summary>
		/// Орехово-Борисово Северное (996)
		/// </summary>
		[Description("Орехово-Борисово Северное")]
        [EnumCode("82")]
        [ShortTitle("")]
        OrekhovoBorisovoSevernoye = 996,
		/// <summary>
		/// Орехово-Борисово Южное (997)
		/// </summary>
		[Description("Орехово-Борисово Южное")]
        [EnumCode("83")]
        [ShortTitle("")]
        OrekhovoBorisovoYuzhnoye = 997,
		/// <summary>
		/// Царицыно (998)
		/// </summary>
		[Description("Царицыно")]
        [EnumCode("84")]
        [ShortTitle("")]
        Tsaritsyno = 998,
		/// <summary>
		/// Чертаново Северное (999)
		/// </summary>
		[Description("Чертаново Северное")]
        [EnumCode("85")]
        [ShortTitle("")]
        ChertanovoSevernoye = 999,
		/// <summary>
		/// Чертаново Центральное (1000)
		/// </summary>
		[Description("Чертаново Центральное")]
        [EnumCode("86")]
        [ShortTitle("")]
        ChertanovoTsentralnoye = 1000,
		/// <summary>
		/// Чертаново Южное (1001)
		/// </summary>
		[Description("Чертаново Южное")]
        [EnumCode("87")]
        [ShortTitle("")]
        ChertanovoYuzhnoye = 1001,
		/// <summary>
		/// Академический (1002)
		/// </summary>
		[Description("Академический")]
        [EnumCode("88")]
        [ShortTitle("")]
        Akademichesky = 1002,
		/// <summary>
		/// Гагаринский (1003)
		/// </summary>
		[Description("Гагаринский")]
        [EnumCode("89")]
        [ShortTitle("")]
        Gagarinsky = 1003,
		/// <summary>
		/// Зюзино (1004)
		/// </summary>
		[Description("Зюзино")]
        [EnumCode("90")]
        [ShortTitle("")]
        Zyuzino = 1004,
		/// <summary>
		/// Коньково (1005)
		/// </summary>
		[Description("Коньково")]
        [EnumCode("91")]
        [ShortTitle("")]
        Konkovo = 1005,
		/// <summary>
		/// Котловка (1006)
		/// </summary>
		[Description("Котловка")]
        [EnumCode("92")]
        [ShortTitle("")]
        Kotlovka = 1006,
		/// <summary>
		/// Ломоносовский (1007)
		/// </summary>
		[Description("Ломоносовский")]
        [EnumCode("93")]
        [ShortTitle("")]
        Lomonosovsky = 1007,
		/// <summary>
		/// Обручевский (1008)
		/// </summary>
		[Description("Обручевский")]
        [EnumCode("94")]
        [ShortTitle("")]
        Obruchevsky = 1008,
		/// <summary>
		/// Северное Бутово (1009)
		/// </summary>
		[Description("Северное Бутово")]
        [EnumCode("95")]
        [ShortTitle("")]
        SevernoyeButovo = 1009,
		/// <summary>
		/// Тёплый Стан (1010)
		/// </summary>
		[Description("Тёплый Стан")]
        [EnumCode("96")]
        [ShortTitle("")]
        TyoplyStan = 1010,
		/// <summary>
		/// Черёмушки (1011)
		/// </summary>
		[Description("Черёмушки")]
        [EnumCode("97")]
        [ShortTitle("")]
        Cheryomushki = 1011,
		/// <summary>
		/// Южное Бутово (1012)
		/// </summary>
		[Description("Южное Бутово")]
        [EnumCode("98")]
        [ShortTitle("")]
        YuzhnoyeButovo = 1012,
		/// <summary>
		/// Ясенево (1013)
		/// </summary>
		[Description("Ясенево")]
        [EnumCode("99")]
        [ShortTitle("")]
        Yasenevo = 1013,
		/// <summary>
		/// Внуково (1014)
		/// </summary>
		[Description("Внуково")]
        [EnumCode("100")]
        [ShortTitle("")]
        Vnukovo = 1014,
		/// <summary>
		/// Дорогомилово (1015)
		/// </summary>
		[Description("Дорогомилово")]
        [EnumCode("101")]
        [ShortTitle("")]
        Dorogomilovo = 1015,
		/// <summary>
		/// Крылатское (1016)
		/// </summary>
		[Description("Крылатское")]
        [EnumCode("102")]
        [ShortTitle("")]
        Krylatskoye = 1016,
		/// <summary>
		/// Кунцево (1017)
		/// </summary>
		[Description("Кунцево")]
        [EnumCode("103")]
        [ShortTitle("")]
        Kuntsevo = 1017,
		/// <summary>
		/// Можайский (1018)
		/// </summary>
		[Description("Можайский")]
        [EnumCode("104")]
        [ShortTitle("")]
        Mozhaysky = 1018,
		/// <summary>
		/// Ново-Переделкино (1019)
		/// </summary>
		[Description("Ново-Переделкино")]
        [EnumCode("105")]
        [ShortTitle("")]
        NovoPeredelkino = 1019,
		/// <summary>
		/// Очаково-Матвеевское (1020)
		/// </summary>
		[Description("Очаково-Матвеевское")]
        [EnumCode("106")]
        [ShortTitle("")]
        OchakovoMatveyevskoye = 1020,
		/// <summary>
		/// Проспект Вернадского (1021)
		/// </summary>
		[Description("Проспект Вернадского")]
        [EnumCode("107")]
        [ShortTitle("")]
        ProspektVernadskogo = 1021,
		/// <summary>
		/// Раменки (1022)
		/// </summary>
		[Description("Раменки")]
        [EnumCode("108")]
        [ShortTitle("")]
        Ramenki = 1022,
		/// <summary>
		/// Солнцево (1023)
		/// </summary>
		[Description("Солнцево")]
        [EnumCode("109")]
        [ShortTitle("")]
        Solntsevo = 1023,
		/// <summary>
		/// Тропарево-Никулино (1024)
		/// </summary>
		[Description("Тропарево-Никулино")]
        [EnumCode("110")]
        [ShortTitle("")]
        TroparyovoNikulino = 1024,
		/// <summary>
		/// Филёвский Парк (1025)
		/// </summary>
		[Description("Филёвский Парк")]
        [EnumCode("111")]
        [ShortTitle("")]
        FilyovskyPark = 1025,
		/// <summary>
		/// Фили-Давыдково (1026)
		/// </summary>
		[Description("Фили-Давыдково")]
        [EnumCode("112")]
        [ShortTitle("")]
        FiliDavydkovo = 1026,
		/// <summary>
		/// Куркино (1027)
		/// </summary>
		[Description("Куркино")]
        [EnumCode("113")]
        [ShortTitle("")]
        Kurkino = 1027,
		/// <summary>
		/// Митино (1028)
		/// </summary>
		[Description("Митино")]
        [EnumCode("114")]
        [ShortTitle("")]
        Mitino = 1028,
		/// <summary>
		/// Покровское-Стрешнево (1029)
		/// </summary>
		[Description("Покровское-Стрешнево")]
        [EnumCode("115")]
        [ShortTitle("")]
        PokrovskoyeStreshnevo = 1029,
		/// <summary>
		/// Северное Тушино (1030)
		/// </summary>
		[Description("Северное Тушино")]
        [EnumCode("116")]
        [ShortTitle("")]
        SevernoyeTushino = 1030,
		/// <summary>
		/// Строгино (1031)
		/// </summary>
		[Description("Строгино")]
        [EnumCode("117")]
        [ShortTitle("")]
        Strogino = 1031,
		/// <summary>
		/// Хорошёво-Мнёвники (1032)
		/// </summary>
		[Description("Хорошёво-Мнёвники")]
        [EnumCode("118")]
        [ShortTitle("")]
        KhoroshyovoMnyovniki = 1032,
		/// <summary>
		/// Щукино (1033)
		/// </summary>
		[Description("Щукино")]
        [EnumCode("119")]
        [ShortTitle("")]
        Shchukino = 1033,
		/// <summary>
		/// Южное Тушино (1034)
		/// </summary>
		[Description("Южное Тушино")]
        [EnumCode("120")]
        [ShortTitle("")]
        YuzhnoyeTushino = 1034,
		/// <summary>
		/// Матушкино (1035)
		/// </summary>
		[Description("Матушкино")]
        [EnumCode("121")]
        [ShortTitle("")]
        Matushkino = 1035,
		/// <summary>
		/// Савёлки (1036)
		/// </summary>
		[Description("Савёлки")]
        [EnumCode("122")]
        [ShortTitle("")]
        Savyolki = 1036,
		/// <summary>
		/// Старое Крюково (1037)
		/// </summary>
		[Description("Старое Крюково")]
        [EnumCode("123")]
        [ShortTitle("")]
        StaroyeKryukovo = 1037,
		/// <summary>
		/// Силино (1038)
		/// </summary>
		[Description("Силино")]
        [EnumCode("124")]
        [ShortTitle("")]
        Silino = 1038,
		/// <summary>
		/// Крюково (1039)
		/// </summary>
		[Description("Крюково")]
        [EnumCode("125")]
        [ShortTitle("")]
        Kryukovo = 1039,
		/// <summary>
		/// Внуковское (1040)
		/// </summary>
		[Description("Внуковское")]
        [EnumCode("126")]
        [ShortTitle("")]
        Vnukovskoye = 1040,
		/// <summary>
		/// Воскресенское (1041)
		/// </summary>
		[Description("Воскресенское")]
        [EnumCode("127")]
        [ShortTitle("")]
        Voskresenskoye = 1041,
		/// <summary>
		/// Десёновское (1042)
		/// </summary>
		[Description("Десёновское")]
        [EnumCode("128")]
        [ShortTitle("")]
        Desyonovskoye = 1042,
		/// <summary>
		/// Кокошкино (1043)
		/// </summary>
		[Description("Кокошкино")]
        [EnumCode("129")]
        [ShortTitle("")]
        Kokoshkino = 1043,
		/// <summary>
		/// Марушкинское (1044)
		/// </summary>
		[Description("Марушкинское")]
        [EnumCode("130")]
        [ShortTitle("")]
        Marushkinskoye = 1044,
		/// <summary>
		/// Московский (1045)
		/// </summary>
		[Description("Московский")]
        [EnumCode("131")]
        [ShortTitle("")]
        Moskovsky = 1045,
		/// <summary>
		/// Мосрентген (1046)
		/// </summary>
		[Description("Мосрентген")]
        [EnumCode("132")]
        [ShortTitle("")]
        Mosrentgen = 1046,
		/// <summary>
		/// Рязановское (1047)
		/// </summary>
		[Description("Рязановское")]
        [EnumCode("133")]
        [ShortTitle("")]
        Ryazanovskoye = 1047,
		/// <summary>
		/// Сосенское (1048)
		/// </summary>
		[Description("Сосенское")]
        [EnumCode("134")]
        [ShortTitle("")]
        Sosenskoye = 1048,
		/// <summary>
		/// Филимонковское (1049)
		/// </summary>
		[Description("Филимонковское")]
        [EnumCode("135")]
        [ShortTitle("")]
        Filimonkovskoye = 1049,
		/// <summary>
		/// Щербинка (1050)
		/// </summary>
		[Description("Щербинка")]
        [EnumCode("136")]
        [ShortTitle("")]
        Shcherbinka = 1050,
		/// <summary>
		/// Вороновское (1051)
		/// </summary>
		[Description("Вороновское")]
        [EnumCode("137")]
        [ShortTitle("")]
        Voronovskoye = 1051,
		/// <summary>
		/// Киевский (1052)
		/// </summary>
		[Description("Киевский")]
        [EnumCode("138")]
        [ShortTitle("")]
        Kiyevsky = 1052,
		/// <summary>
		/// Клёновское (1053)
		/// </summary>
		[Description("Клёновское")]
        [EnumCode("139")]
        [ShortTitle("")]
        Klenovskoye = 1053,
		/// <summary>
		/// Краснопахорское (1054)
		/// </summary>
		[Description("Краснопахорское")]
        [EnumCode("140")]
        [ShortTitle("")]
        Krasnopakhorskoye = 1054,
		/// <summary>
		/// Михайлово-Ярцевское (1055)
		/// </summary>
		[Description("Михайлово-Ярцевское")]
        [EnumCode("141")]
        [ShortTitle("")]
        MikhaylovoYartsevskoye = 1055,
		/// <summary>
		/// Новофёдоровское (1056)
		/// </summary>
		[Description("Новофёдоровское")]
        [EnumCode("142")]
        [ShortTitle("")]
        Novofyodorovskoye = 1056,
		/// <summary>
		/// Первомайское (1057)
		/// </summary>
		[Description("Первомайское")]
        [EnumCode("143")]
        [ShortTitle("")]
        Pervomayskoye = 1057,
		/// <summary>
		/// Роговское (1058)
		/// </summary>
		[Description("Роговское")]
        [EnumCode("144")]
        [ShortTitle("")]
        Rogovskoye = 1058,
		/// <summary>
		/// Троицк (1059)
		/// </summary>
		[Description("Троицк")]
        [EnumCode("145")]
        [ShortTitle("")]
        Troitsky = 1059,
		/// <summary>
		/// Щаповское (1060)
		/// </summary>
		[Description("Щаповское")]
        [EnumCode("146")]
        [ShortTitle("")]
        Shchapovskoye = 1060,
    }
}

namespace ObjectModel.Directory.MarketObjects
{
    /// <summary>
    /// Типы корректировок (121)
    ///</summary>
    [ReferenceInfo(ReferenceId = 121)]
    public enum CorrectionTypes : long
    {
		/// <summary>
		/// Корректировка на дату (1)
		/// </summary>
		[Description("Корректировка на дату")]
        [EnumCode("1")]
        [ShortTitle("Корректировка на дату")]
        CorrectionByDate = 1,
		/// <summary>
		/// Корректировка на торг (2)
		/// </summary>
		[Description("Корректировка на торг")]
        [EnumCode("2")]
        [ShortTitle("Корректировка на торг")]
        CorrectionByBargain = 2,
		/// <summary>
		/// Корректировка на комнатность (3)
		/// </summary>
		[Description("Корректировка на комнатность")]
        [EnumCode("3")]
        [ShortTitle("Корректировка на комнатность")]
        CorrectionByRoom = 3,
		/// <summary>
		/// Корректировка на этажность (4)
		/// </summary>
		[Description("Корректировка на этажность")]
        [EnumCode("4")]
        [ShortTitle("Корректировка на этажность")]
        CorrectionByStage = 4,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Налог (122)
    ///</summary>
    [ReferenceInfo(ReferenceId = 122)]
    public enum VatType : long
    {
		/// <summary>
		/// Нет данных (0)
		/// </summary>
		[Description("Нет данных")]
        [EnumCode("0")]
        [ShortTitle("Нет данных")]
        None = 0,
		/// <summary>
		/// НДС включен (1)
		/// </summary>
		[Description("НДС включен")]
        [EnumCode("1")]
        [ShortTitle("НДС включен")]
        NDS = 1,
		/// <summary>
		/// УСН (2)
		/// </summary>
		[Description("УСН")]
        [EnumCode("2")]
        [ShortTitle("УСН")]
        USN = 2,
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
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
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
		/// Нежилое (2)
		/// </summary>
		[Description("Нежилое")]
        [EnumCode("2")]
        [ShortTitle("Нежилое")]
        NotResidential = 2,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Статус изменения единицы оценки (212)
    ///</summary>
    [ReferenceInfo(ReferenceId = 212)]
    public enum KoChangeStatus : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Тип_объекта (1)
		/// </summary>
		[Description("Тип_объекта")]
        [EnumCode("1")]
        [ShortTitle("Тип_объекта")]
        TypeObject = 1,
		/// <summary>
		/// Площадь (2)
		/// </summary>
		[Description("Площадь")]
        [EnumCode("2")]
        [ShortTitle("Площадь")]
        Square = 2,
		/// <summary>
		/// Номер ЗУ (3)
		/// </summary>
		[Description("Номер ЗУ")]
        [EnumCode("3")]
        [ShortTitle("Номер ЗУ")]
        NumberParcel = 3,
		/// <summary>
		/// Наименование (4)
		/// </summary>
		[Description("Наименование")]
        [EnumCode("4")]
        [ShortTitle("Наименование")]
        Name = 4,
		/// <summary>
		/// Назначение (5)
		/// </summary>
		[Description("Назначение")]
        [EnumCode("5")]
        [ShortTitle("Назначение")]
        Assignment = 5,
		/// <summary>
		/// Адрес (6)
		/// </summary>
		[Description("Адрес")]
        [EnumCode("6")]
        [ShortTitle("Адрес")]
        Adress = 6,
		/// <summary>
		/// Местоположение (7)
		/// </summary>
		[Description("Местоположение")]
        [EnumCode("7")]
        [ShortTitle("Местоположение")]
        Place = 7,
		/// <summary>
		/// Год постройки (8)
		/// </summary>
		[Description("Год постройки")]
        [EnumCode("8")]
        [ShortTitle("Год постройки")]
        YearBuild = 8,
		/// <summary>
		/// Год ввода в эксплуатацию (9)
		/// </summary>
		[Description("Год ввода в эксплуатацию")]
        [EnumCode("9")]
        [ShortTitle("Год ввода в эксплуатацию")]
        YearUse = 9,
		/// <summary>
		/// Этажность (10)
		/// </summary>
		[Description("Этажность")]
        [EnumCode("10")]
        [ShortTitle("Этажность")]
        Floors = 10,
		/// <summary>
		/// Подземная этажность (11)
		/// </summary>
		[Description("Подземная этажность")]
        [EnumCode("11")]
        [ShortTitle("Подземная этажность")]
        DownFloors = 11,
		/// <summary>
		/// Кадастровый квартал (12)
		/// </summary>
		[Description("Кадастровый квартал")]
        [EnumCode("12")]
        [ShortTitle("Кадастровый квартал")]
        CadastralBlock = 12,
		/// <summary>
		/// Материал стен (13)
		/// </summary>
		[Description("Материал стен")]
        [EnumCode("13")]
        [ShortTitle("Материал стен")]
        Walls = 13,
		/// <summary>
		/// Кадастровый номер здания (14)
		/// </summary>
		[Description("Кадастровый номер здания")]
        [EnumCode("14")]
        [ShortTitle("Кадастровый номер здания")]
        CadastralBuilding = 14,
		/// <summary>
		/// Этаж (15)
		/// </summary>
		[Description("Этаж")]
        [EnumCode("15")]
        [ShortTitle("Этаж")]
        NumberFloor = 15,
		/// <summary>
		/// Характеристика (16)
		/// </summary>
		[Description("Характеристика")]
        [EnumCode("16")]
        [ShortTitle("Характеристика")]
        Parameter = 16,
		/// <summary>
		/// Процент готовности (17)
		/// </summary>
		[Description("Процент готовности")]
        [EnumCode("17")]
        [ShortTitle("Процент готовности")]
        Procent = 17,
		/// <summary>
		/// Вид использования (18)
		/// </summary>
		[Description("Вид использования")]
        [EnumCode("18")]
        [ShortTitle("Вид использования")]
        Use = 18,
		/// <summary>
		/// Категория (19)
		/// </summary>
		[Description("Категория")]
        [EnumCode("19")]
        [ShortTitle("Категория")]
        Category = 19,
		/// <summary>
		/// Обращение (20)
		/// </summary>
		[Description("Обращение")]
        [EnumCode("20")]
        [ShortTitle("Обращение")]
        Appeal = 20,
		/// <summary>
		/// Добавление нового объекта (21)
		/// </summary>
		[Description("Добавление нового объекта")]
        [EnumCode("21")]
        [ShortTitle("Добавление нового объекта")]
        NewObjectAddition = 21,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Тип расчета (213)
    ///</summary>
    [ReferenceInfo(ReferenceId = 213)]
    public enum KoCalculationType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// Доходный (1061)
		/// </summary>
		[Description("Доходный")]
        [EnumCode("1061")]
        [ShortTitle("")]
        Profitable = 1061,
		/// <summary>
		/// Затратный (1062)
		/// </summary>
		[Description("Затратный")]
        [EnumCode("1062")]
        [ShortTitle("")]
        Costly = 1062,
		/// <summary>
		/// Сравнительный (1063)
		/// </summary>
		[Description("Сравнительный")]
        [EnumCode("1063")]
        [ShortTitle("")]
        Comparative = 1063,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Метод расчета (214)
    ///</summary>
    [ReferenceInfo(ReferenceId = 214)]
    public enum KoCalculationMethod : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// Метод статистического (регрессионного) моделирования (1064)
		/// </summary>
		[Description("Метод статистического (регрессионного) моделирования")]
        [EnumCode("1064")]
        [ShortTitle("")]
        StatisticalModeling = 1064,
		/// <summary>
		/// Метод типового (эталонного) объекта оценки (1065)
		/// </summary>
		[Description("Метод типового (эталонного) объекта оценки")]
        [EnumCode("1065")]
        [ShortTitle("")]
        TypicalObjectOfAssessment = 1065,
		/// <summary>
		/// Метод моделирования на основе УПКС (1066)
		/// </summary>
		[Description("Метод моделирования на основе УПКС")]
        [EnumCode("1066")]
        [ShortTitle("")]
        SimulationBasedOnUPKS = 1066,
		/// <summary>
		/// Метод индексации прошлых результатов (1067)
		/// </summary>
		[Description("Метод индексации прошлых результатов")]
        [EnumCode("1067")]
        [ShortTitle("")]
        PastResultsIndexing = 1067,
		/// <summary>
		/// Индивидуальный расчет (1511)
		/// </summary>
		[Description("Индивидуальный расчет")]
        [EnumCode("1511")]
        [ShortTitle("")]
        IndividualCalculation = 1511,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Тип использования атрибута (215)
    ///</summary>
    [ReferenceInfo(ReferenceId = 215)]
    public enum KoAttributeUsingType : long
    {
		/// <summary>
		/// Атрибут кода группы (1)
		/// </summary>
		[Description("Атрибут кода группы")]
        [EnumCode("1")]
        [ShortTitle("Атрибут кода группы")]
        CodeGroupAttribute = 1,
		/// <summary>
		/// Атрибут кадастрового квартала (2)
		/// </summary>
		[Description("Атрибут кадастрового квартала")]
        [EnumCode("2")]
        [ShortTitle("Атрибут кадастрового квартала")]
        CodeQuarterAttribute = 2,
		/// <summary>
		/// Атрибут типа территории (3)
		/// </summary>
		[Description("Атрибут типа территории")]
        [EnumCode("3")]
        [ShortTitle("Атрибут типа территории")]
        TerritoryTypeAttribute = 3,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Вид отчета (216)
    ///</summary>
    [ReferenceInfo(ReferenceId = 216)]
    public enum KoReportType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// Выгрузка изменений (1068)
		/// </summary>
		[Description("Выгрузка изменений")]
        [EnumCode("1068")]
        [ShortTitle("")]
        UnloadChange = 1068,
		/// <summary>
		/// Выгрузка истории по объектам (1069)
		/// </summary>
		[Description("Выгрузка истории по объектам")]
        [EnumCode("1069")]
        [ShortTitle("")]
        UnloadHistory = 1069,
		/// <summary>
		/// Таблица 4. Группировка объектов недвижимости (1070)
		/// </summary>
		[Description("Таблица 4. Группировка объектов недвижимости")]
        [EnumCode("1070")]
        [ShortTitle("")]
        UnloadTable04 = 1070,
		/// <summary>
		/// Таблица 5. Результаты моделирования (1071)
		/// </summary>
		[Description("Таблица 5. Результаты моделирования")]
        [EnumCode("1071")]
        [ShortTitle("")]
        UnloadTable05 = 1071,
		/// <summary>
		/// Таблица 7. Обобщенные показатели по кадастровым районам (1072)
		/// </summary>
		[Description("Таблица 7. Обобщенные показатели по кадастровым районам")]
        [EnumCode("1072")]
        [ShortTitle("")]
        UnloadTable07 = 1072,
		/// <summary>
		/// Таблица 8. Минимальные, максимальные, средние УПКС по кадастровым кварталам (1073)
		/// </summary>
		[Description("Таблица 8. Минимальные, максимальные, средние УПКС по кадастровым кварталам")]
        [EnumCode("1073")]
        [ShortTitle("")]
        UnloadTable08 = 1073,
		/// <summary>
		/// Таблица 9. Результаты определения кадастровой стоимости (1074)
		/// </summary>
		[Description("Таблица 9. Результаты определения кадастровой стоимости")]
        [EnumCode("1074")]
        [ShortTitle("")]
        UnloadTable09 = 1074,
		/// <summary>
		/// Таблица 10. Результаты государственной кадастровой оценки (1075)
		/// </summary>
		[Description("Таблица 10. Результаты государственной кадастровой оценки")]
        [EnumCode("1075")]
        [ShortTitle("")]
        UnloadTable10 = 1075,
		/// <summary>
		/// Таблица 11. Сводные результаты по кадастровому району (1076)
		/// </summary>
		[Description("Таблица 11. Сводные результаты по кадастровому району")]
        [EnumCode("1076")]
        [ShortTitle("")]
        UnloadTable11 = 1076,
		/// <summary>
		/// Выгрузка XML 1: КНомер, УПКСЗ, КСтоимость (1077)
		/// </summary>
		[Description("Выгрузка XML 1: КНомер, УПКСЗ, КСтоимость")]
        [EnumCode("1077")]
        [ShortTitle("")]
        UnloadXML1 = 1077,
		/// <summary>
		/// Выгрузка XML 2 результатов Кадастровой оценки по группам (1078)
		/// </summary>
		[Description("Выгрузка XML 2 результатов Кадастровой оценки по группам")]
        [EnumCode("1078")]
        [ShortTitle("")]
        UnloadXML2 = 1078,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Тип территории (217)
    ///</summary>
    [ReferenceInfo(ReferenceId = 217)]
    public enum TerritoryType : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// Основная (1507)
		/// </summary>
		[Description("Основная")]
        [EnumCode("1")]
        [ShortTitle("")]
        Main = 1507,
		/// <summary>
		/// Дополнительная (1508)
		/// </summary>
		[Description("Дополнительная")]
        [EnumCode("2")]
        [ShortTitle("")]
        Additional = 1508,
		/// <summary>
		/// Основная и Дополнительная (1509)
		/// </summary>
		[Description("Основная и Дополнительная")]
        [EnumCode("3")]
        [ShortTitle("")]
        MainAndAdditional = 1509,
		/// <summary>
		/// Нет (1510)
		/// </summary>
		[Description("Нет")]
        [EnumCode("4")]
        [ShortTitle("")]
        No = 1510,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Способ моделирования (218)
    ///</summary>
    [ReferenceInfo(ReferenceId = 218)]
    public enum KoModelingWay : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// Индивидуальная оценка (1512)
		/// </summary>
		[Description("Индивидуальная оценка")]
        [EnumCode("1512")]
        [ShortTitle("")]
        IndividualCalculation = 1512,
		/// <summary>
		/// Массовая оценка (1513)
		/// </summary>
		[Description("Массовая оценка")]
        [EnumCode("1513")]
        [ShortTitle("")]
        MassCalculation = 1513,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Тип выгрузки результатов кадастровой оценки (220)
    ///</summary>
    [ReferenceInfo(ReferenceId = 220)]
    public enum KoUnloadResultType : long
    {
		/// <summary>
		/// Тип не указан (0)
		/// </summary>
		[Description("Тип не указан")]
        [EnumCode("0")]
        [ShortTitle("Тип не указан")]
        None = 0,
		/// <summary>
		/// Выгрузка изменений (1)
		/// </summary>
		[Description("Выгрузка изменений")]
        [EnumCode("1")]
        [ShortTitle("Выгрузка изменений")]
        UnloadChange = 1,
		/// <summary>
		/// Выгрузка истории по объектам (2)
		/// </summary>
		[Description("Выгрузка истории по объектам")]
        [EnumCode("2")]
        [ShortTitle("Выгрузка истории по объектам")]
        UnloadHistory = 2,
		/// <summary>
		/// Таблица 4. Группировка объектов недвижимости (3)
		/// </summary>
		[Description("Таблица 4. Группировка объектов недвижимости")]
        [EnumCode("3")]
        [ShortTitle("Таблица 4. Группировка объектов недвижимости")]
        UnloadTable04 = 3,
		/// <summary>
		/// Таблица 5. Результаты моделирования (4)
		/// </summary>
		[Description("Таблица 5. Результаты моделирования")]
        [EnumCode("4")]
        [ShortTitle("Таблица 5. Результаты моделирования")]
        UnloadTable05 = 4,
		/// <summary>
		/// Таблица 7. Обобщенные показатели по кадастровым районам (5)
		/// </summary>
		[Description("Таблица 7. Обобщенные показатели по кадастровым районам")]
        [EnumCode("5")]
        [ShortTitle("Таблица 7. Обобщенные показатели по кадастровым районам")]
        UnloadTable07 = 5,
		/// <summary>
		/// Таблица 8. Минимальные, максимальные, средние УПКС по кадастровым кварталам (6)
		/// </summary>
		[Description("Таблица 8. Минимальные, максимальные, средние УПКС по кадастровым кварталам")]
        [EnumCode("6")]
        [ShortTitle("Таблица 8. Минимальные, максимальные, средние УПКС по кадастровым кварталам")]
        UnloadTable08 = 6,
		/// <summary>
		/// Таблица 9. Результаты определения кадастровой стоимости (7)
		/// </summary>
		[Description("Таблица 9. Результаты определения кадастровой стоимости")]
        [EnumCode("7")]
        [ShortTitle("Таблица 9. Результаты определения кадастровой стоимости")]
        UnloadTable09 = 7,
		/// <summary>
		/// Таблица 10. Результаты государственной кадастровой оценки (8)
		/// </summary>
		[Description("Таблица 10. Результаты государственной кадастровой оценки")]
        [EnumCode("8")]
        [ShortTitle("Таблица 10. Результаты государственной кадастровой оценки")]
        UnloadTable10 = 8,
		/// <summary>
		/// Таблица 11. Сводные результаты по кадастровому району (9)
		/// </summary>
		[Description("Таблица 11. Сводные результаты по кадастровому району")]
        [EnumCode("9")]
        [ShortTitle("Таблица 11. Сводные результаты по кадастровому району")]
        UnloadTable11 = 9,
		/// <summary>
		/// Выгрузка в XML результатов Кадастровой оценки по объектам (10)
		/// </summary>
		[Description("Выгрузка в XML результатов Кадастровой оценки по объектам")]
        [EnumCode("10")]
        [ShortTitle("Выгрузка в XML результатов Кадастровой оценки по объектам")]
        UnloadXML1 = 10,
		/// <summary>
		/// Выгрузка в XML результатов Кадастровой оценки по группам (11)
		/// </summary>
		[Description("Выгрузка в XML результатов Кадастровой оценки по группам")]
        [EnumCode("11")]
        [ShortTitle("Выгрузка в XML результатов Кадастровой оценки по группам")]
        UnloadXML2 = 11,
		/// <summary>
		/// Выгрузка в XML результатов Кадастровой оценки по исходящим документам (12)
		/// </summary>
		[Description("Выгрузка в XML результатов Кадастровой оценки по исходящим документам")]
        [EnumCode("12")]
        [ShortTitle("Выгрузка в XML результатов Кадастровой оценки по исходящим документам")]
        UnloadDEKOResponseDocExportToXml = 12,
		/// <summary>
		/// Выгрузка в XML результатов Кадастровой оценки для ВУОН (13)
		/// </summary>
		[Description("Выгрузка в XML результатов Кадастровой оценки для ВУОН")]
        [EnumCode("13")]
        [ShortTitle("Выгрузка в XML результатов Кадастровой оценки для ВУОН")]
        UnloadDEKOVuonExportToXml = 13,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Статусы обновления Единииц оценки (221)
    ///</summary>
    [ReferenceInfo(ReferenceId = 221)]
    public enum UnitUpdateStatus : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Новый (1)
		/// </summary>
		[Description("Новый")]
        [EnumCode("1")]
        [ShortTitle("Новый")]
        New = 1,
		/// <summary>
		/// Без изменений (2)
		/// </summary>
		[Description("Без изменений")]
        [EnumCode("2")]
        [ShortTitle("Без изменений")]
        WithoutChanges = 2,
		/// <summary>
		/// Изменение группы (3)
		/// </summary>
		[Description("Изменение группы")]
        [EnumCode("3")]
        [ShortTitle("Изменение группы")]
        GroupChange = 3,
		/// <summary>
		/// Изменение характеристик ЕГРН (4)
		/// </summary>
		[Description("Изменение характеристик ЕГРН")]
        [EnumCode("4")]
        [ShortTitle("Изменение характеристик ЕГРН")]
        EgrnChanges = 4,
		/// <summary>
		/// Изменение ФС (5)
		/// </summary>
		[Description("Изменение ФС")]
        [EnumCode("5")]
        [ShortTitle("Изменение ФС")]
        FsChange = 5,
		/// <summary>
		/// Изменение группы, Изменение ФС (6)
		/// </summary>
		[Description("Изменение группы, Изменение ФС")]
        [EnumCode("6")]
        [ShortTitle("Изменение группы, Изменение ФС")]
        GroupAndFsChange = 6,
		/// <summary>
		/// Изменение группы, Изменение ЕГРН (7)
		/// </summary>
		[Description("Изменение группы, Изменение ЕГРН")]
        [EnumCode("7")]
        [ShortTitle("Изменение группы, Изменение ЕГРН")]
        GroupAndEgrnChange = 7,
		/// <summary>
		/// Изменение группы, Изменение ФС, Изменение характеристик ЕГРН (8)
		/// </summary>
		[Description("Изменение группы, Изменение ФС, Изменение характеристик ЕГРН")]
        [EnumCode("8")]
        [ShortTitle("Изменение группы, Изменение ФС, Изменение характеристик ЕГРН")]
        GroupAndFsAndEgrnChanges = 8,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Тип модели (222)
    ///</summary>
    [ReferenceInfo(ReferenceId = 222)]
    public enum KoModelType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Ручное вычисление (1)
		/// </summary>
		[Description("Ручное вычисление")]
        [EnumCode("1")]
        [ShortTitle("Ручное вычисление")]
        Manual = 1,
		/// <summary>
		/// Автоматический расчет (2)
		/// </summary>
		[Description("Автоматический расчет")]
        [EnumCode("2")]
        [ShortTitle("Автоматический расчет")]
        Automatic = 2,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Тип атрибута (для различных системных настроек) (223)
    ///</summary>
    [ReferenceInfo(ReferenceId = 223)]
    public enum KoAttributeTypeForSettings : long
    {
		/// <summary>
		/// Атрибут кадастрового квартала (1)
		/// </summary>
		[Description("Атрибут кадастрового квартала")]
        [EnumCode("1")]
        [ShortTitle("Атрибут кадастрового квартала")]
        CadastralQuarter = 1,
		/// <summary>
		/// Атрибут кадастрового номера здания (2)
		/// </summary>
		[Description("Атрибут кадастрового номера здания")]
        [EnumCode("2")]
        [ShortTitle("Атрибут кадастрового номера здания")]
        BuildingCadastralNumber = 2,
		/// <summary>
		/// Атрибут оценочной группы (3)
		/// </summary>
		[Description("Атрибут оценочной группы")]
        [EnumCode("3")]
        [ShortTitle("Атрибут оценочной группы")]
        EvaluativeGroup = 3,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Статус после сравнения протоколов загрузки (224)
    ///</summary>
    [ReferenceInfo(ReferenceId = 224)]
    public enum KoDataComparingTaskChangesStatus : long
    {
		/// <summary>
		/// Проверка не проводилась (0)
		/// </summary>
		[Description("Проверка не проводилась")]
        [EnumCode("0")]
        [ShortTitle("Проверка не проводилась")]
        ComparingWasNotPerformed = 0,
		/// <summary>
		/// Данные совпадают (1)
		/// </summary>
		[Description("Данные совпадают")]
        [EnumCode("1")]
        [ShortTitle("Данные совпадают")]
        DataAreMatch = 1,
		/// <summary>
		/// Имеются расхождения (2)
		/// </summary>
		[Description("Имеются расхождения")]
        [EnumCode("2")]
        [ShortTitle("Имеются расхождения")]
        ThereAreInconsistencies = 2,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Статус после сравнения протоколов КС (225)
    ///</summary>
    [ReferenceInfo(ReferenceId = 225)]
    public enum KoDataComparingCadastralCostStatus : long
    {
		/// <summary>
		/// Проверка не проводилась (0)
		/// </summary>
		[Description("Проверка не проводилась")]
        [EnumCode("0")]
        [ShortTitle("Проверка не проводилась")]
        ComparingWasNotPerformed = 0,
		/// <summary>
		/// Данные совпадают (1)
		/// </summary>
		[Description("Данные совпадают")]
        [EnumCode("1")]
        [ShortTitle("Данные совпадают")]
        DataAreMatch = 1,
		/// <summary>
		/// Наборы данных для сравнения не совпадают (2)
		/// </summary>
		[Description("Наборы данных для сравнения не совпадают")]
        [EnumCode("2")]
        [ShortTitle("Наборы данных для сравнения не совпадают")]
        ThereAreUnitSetsInconsistencies = 2,
		/// <summary>
		/// Имеются несоответствия в кадастровой стоимости (3)
		/// </summary>
		[Description("Имеются несоответствия в кадастровой стоимости")]
        [EnumCode("3")]
        [ShortTitle("Имеются несоответствия в кадастровой стоимости")]
        ThereAreUnitCostsInconsistencies = 3,
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

namespace ObjectModel.Directory.ES
{
    /// <summary>
    /// Тип данных кода справочника (600)
    ///</summary>
    [ReferenceInfo(ReferenceId = 600)]
    public enum ReferenceItemCodeType : long
    {
		/// <summary>
		/// Число (1)
		/// </summary>
		[Description("Число")]
        [EnumCode("1")]
        [ShortTitle("Число")]
        Number = 1,
		/// <summary>
		/// Строка (4)
		/// </summary>
		[Description("Строка")]
        [EnumCode("4")]
        [ShortTitle("Строка")]
        String = 4,
		/// <summary>
		/// Дата и время (5)
		/// </summary>
		[Description("Дата и время")]
        [EnumCode("5")]
        [ShortTitle("Дата и время")]
        Date = 5,
    }
}

namespace ObjectModel.Directory.ES
{
    /// <summary>
    /// Сценарий расчета (601)
    ///</summary>
    [ReferenceInfo(ReferenceId = 601)]
    public enum ScenarioType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Расчет ЕОН (ОКС + ЗУ) (1)
		/// </summary>
		[Description("Расчет ЕОН (ОКС + ЗУ)")]
        [EnumCode("1")]
        [ShortTitle("Расчет ЕОН (ОКС + ЗУ)")]
        Eon = 1,
		/// <summary>
		/// Расчет ОКС без доли ЗУ (2)
		/// </summary>
		[Description("Расчет ОКС без доли ЗУ")]
        [EnumCode("2")]
        [ShortTitle("Расчет ОКС без доли ЗУ")]
        Oks = 2,
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
		/// Выборка из справочника ЦОД (4)
		/// </summary>
		[Description("Выборка из справочника ЦОД")]
        [EnumCode("4")]
        [ShortTitle("Выборка из справочника ЦОД")]
        UnloadingFromDict = 4,
		/// <summary>
		/// Проставление оценочной группы (5)
		/// </summary>
		[Description("Проставление оценочной группы")]
        [EnumCode("5")]
        [ShortTitle("Проставление оценочной группы")]
        EstimatedGroup = 5,
		/// <summary>
		/// Перенос атрибутов (без создания) (6)
		/// </summary>
		[Description("Перенос атрибутов (без создания)")]
        [EnumCode("6")]
        [ShortTitle("Перенос атрибутов (без создания)")]
        TransferAttributesWithoutCreate = 6,
		/// <summary>
		/// Перенос атрибутов (с созданием) (7)
		/// </summary>
		[Description("Перенос атрибутов (с созданием)")]
        [EnumCode("7")]
        [ShortTitle("Перенос атрибутов (с созданием)")]
        TransferAttributesWithCreate = 7,
		/// <summary>
		/// Наследование (8)
		/// </summary>
		[Description("Наследование")]
        [EnumCode("8")]
        [ShortTitle("Наследование")]
        Inheritance = 8,
		/// <summary>
		/// Выгрузка факторов единиц оценки по заданию на оценку (9)
		/// </summary>
		[Description("Выгрузка факторов единиц оценки по заданию на оценку")]
        [EnumCode("9")]
        [ShortTitle("Выгрузка факторов единиц оценки по заданию на оценку")]
        ExportFactorsByTask = 9,
		/// <summary>
		/// Финализация нормализации (10)
		/// </summary>
		[Description("Финализация нормализации")]
        [EnumCode("10")]
        [ShortTitle("Финализация нормализации")]
        NormalisationFinal = 10,
    }
}

namespace ObjectModel.Directory.Common
{
    /// <summary>
    /// Статус экспорта файлов (802)
    ///</summary>
    [ReferenceInfo(ReferenceId = 802)]
    public enum ExportStatus : long
    {
		/// <summary>
		/// Создана (0)
		/// </summary>
		[Description("Создана")]
        [EnumCode("0")]
        [ShortTitle("Создана")]
        Added = 0,
		/// <summary>
		/// Запущена (1)
		/// </summary>
		[Description("Запущена")]
        [EnumCode("1")]
        [ShortTitle("Запущена")]
        Running = 1,
		/// <summary>
		/// Завершена (2)
		/// </summary>
		[Description("Завершена")]
        [EnumCode("2")]
        [ShortTitle("Завершена")]
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

namespace ObjectModel.Directory.KO
{
    /// <summary>
    /// Тип наследования (902)
    ///</summary>
    [ReferenceInfo(ReferenceId = 902)]
    public enum FactorInheritance : long
    {
        /// <summary>
        /// Значение отсутствует
        /// </summary>
        [Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("")]
        None = 0,

		/// <summary>
		/// Наследование при совпадении квартала (2191)
		/// </summary>
		[Description("Наследование при совпадении квартала")]
        [EnumCode("1")]
        [ShortTitle("")]
        ftKvartal = 2191,
		/// <summary>
		/// Наследование при совпадении года постройки (2192)
		/// </summary>
		[Description("Наследование при совпадении года постройки")]
        [EnumCode("2")]
        [ShortTitle("")]
        ftYear = 2192,
		/// <summary>
		/// Наследование при совпадении материала стен  (2193)
		/// </summary>
		[Description("Наследование при совпадении материала стен ")]
        [EnumCode("3")]
        [ShortTitle("")]
        ftWall = 2193,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Линия застройки (12083)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12083)]
    public enum HouseLineType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Первая (1)
		/// </summary>
		[Description("Первая")]
        [EnumCode("1")]
        [ShortTitle("Первая")]
        First = 1,
		/// <summary>
		/// Вторая (2)
		/// </summary>
		[Description("Вторая")]
        [EnumCode("2")]
        [ShortTitle("Вторая")]
        Second  = 2,
		/// <summary>
		/// Иная (3)
		/// </summary>
		[Description("Иная")]
        [EnumCode("3")]
        [ShortTitle("Иная")]
        Other = 3,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Состояние отделки (12084)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12084)]
    public enum FinishingCondition : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Требуется косметический ремонт (1)
		/// </summary>
		[Description("Требуется косметический ремонт")]
        [EnumCode("1")]
        [ShortTitle("Требуется косметический ремонт")]
        CosmeticRepairsRequired = 1,
		/// <summary>
		/// Дизайнерский ремонт (2)
		/// </summary>
		[Description("Дизайнерский ремонт")]
        [EnumCode("2")]
        [ShortTitle("Дизайнерский ремонт")]
        Design = 2,
		/// <summary>
		/// Под чистовую отделку (3)
		/// </summary>
		[Description("Под чистовую отделку")]
        [EnumCode("3")]
        [ShortTitle("Под чистовую отделку")]
        Finishing = 3,
		/// <summary>
		/// Требуется капитальный ремонт (4)
		/// </summary>
		[Description("Требуется капитальный ремонт")]
        [EnumCode("4")]
        [ShortTitle("Требуется капитальный ремонт")]
        MajorRepairsRequired = 4,
		/// <summary>
		/// Офисная отделка (5)
		/// </summary>
		[Description("Офисная отделка")]
        [EnumCode("5")]
        [ShortTitle("Офисная отделка")]
        Office = 5,
		/// <summary>
		/// Типовой ремонт (6)
		/// </summary>
		[Description("Типовой ремонт")]
        [EnumCode("6")]
        [ShortTitle("Типовой ремонт")]
        Typical = 6,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Тип дома (12085)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12085)]
    public enum HouseType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Газобетонный блок (1)
		/// </summary>
		[Description("Газобетонный блок")]
        [EnumCode("1")]
        [ShortTitle("Газобетонный блок")]
        AerocreteBlock = 1,
		/// <summary>
		/// Блочный (2)
		/// </summary>
		[Description("Блочный")]
        [EnumCode("2")]
        [ShortTitle("Блочный")]
        Block = 2,
		/// <summary>
		/// Щитовой (3)
		/// </summary>
		[Description("Щитовой")]
        [EnumCode("3")]
        [ShortTitle("Щитовой")]
        Boards = 3,
		/// <summary>
		/// Кирпичный (4)
		/// </summary>
		[Description("Кирпичный")]
        [EnumCode("4")]
        [ShortTitle("Кирпичный")]
        Brick = 4,
		/// <summary>
		/// Пенобетонный блок (5)
		/// </summary>
		[Description("Пенобетонный блок")]
        [EnumCode("5")]
        [ShortTitle("Пенобетонный блок")]
        FoamConcreteBlock = 5,
		/// <summary>
		/// Газосиликатный блок (6)
		/// </summary>
		[Description("Газосиликатный блок")]
        [EnumCode("6")]
        [ShortTitle("Газосиликатный блок")]
        GasSilicateBlock = 6,
		/// <summary>
		/// Монолитный (7)
		/// </summary>
		[Description("Монолитный")]
        [EnumCode("7")]
        [ShortTitle("Монолитный")]
        Monolith = 7,
		/// <summary>
		/// Монолитно-кирпичный (8)
		/// </summary>
		[Description("Монолитно-кирпичный")]
        [EnumCode("8")]
        [ShortTitle("Монолитно-кирпичный")]
        MonolithBrick = 8,
		/// <summary>
		/// Старый фонд (9)
		/// </summary>
		[Description("Старый фонд")]
        [EnumCode("9")]
        [ShortTitle("Старый фонд")]
        Old = 9,
		/// <summary>
		/// Панельный (10)
		/// </summary>
		[Description("Панельный")]
        [EnumCode("10")]
        [ShortTitle("Панельный")]
        Panel  = 10,
		/// <summary>
		/// Сталинский (11)
		/// </summary>
		[Description("Сталинский")]
        [EnumCode("11")]
        [ShortTitle("Сталинский")]
        Stalin = 11,
		/// <summary>
		/// Каркасный (12)
		/// </summary>
		[Description("Каркасный")]
        [EnumCode("12")]
        [ShortTitle("Каркасный")]
        Wireframe = 12,
		/// <summary>
		/// Деревянный (13)
		/// </summary>
		[Description("Деревянный")]
        [EnumCode("13")]
        [ShortTitle("Деревянный")]
        Wood = 13,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Планировка (12086)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12086)]
    public enum Layout : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Кабинетная (1)
		/// </summary>
		[Description("Кабинетная")]
        [EnumCode("1")]
        [ShortTitle("Кабинетная")]
        Cabinet = 1,
		/// <summary>
		/// Коридорная (2)
		/// </summary>
		[Description("Коридорная")]
        [EnumCode("2")]
        [ShortTitle("Коридорная")]
        Corridorplan = 2,
		/// <summary>
		/// Смешанная (3)
		/// </summary>
		[Description("Смешанная")]
        [EnumCode("3")]
        [ShortTitle("Смешанная")]
        Mixed = 3,
		/// <summary>
		/// Открытая (4)
		/// </summary>
		[Description("Открытая")]
        [EnumCode("4")]
        [ShortTitle("Открытая")]
        OpenSpace = 4,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Вид разрешённого использования (12087)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12087)]
    public enum PermittedUseType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Cельскохозяйственное использование (1)
		/// </summary>
		[Description("Cельскохозяйственное использование")]
        [EnumCode("1")]
        [ShortTitle("Cельскохозяйственное использование")]
        Agricultural = 1,
		/// <summary>
		/// Деловое управление (2)
		/// </summary>
		[Description("Деловое управление")]
        [EnumCode("2")]
        [ShortTitle("Деловое управление")]
        BusinessManagement = 2,
		/// <summary>
		/// Общее пользование территории (3)
		/// </summary>
		[Description("Общее пользование территории")]
        [EnumCode("3")]
        [ShortTitle("Общее пользование территории")]
        CommonUseArea = 3,
		/// <summary>
		/// Высотная застройка (4)
		/// </summary>
		[Description("Высотная застройка")]
        [EnumCode("4")]
        [ShortTitle("Высотная застройка")]
        HighriseBuildings = 4,
		/// <summary>
		/// Гостиничное обслуживание (5)
		/// </summary>
		[Description("Гостиничное обслуживание")]
        [EnumCode("5")]
        [ShortTitle("Гостиничное обслуживание")]
        HotelAmenities = 5,
		/// <summary>
		/// Индивидуальное жилищное строительство (ИЖС) (6)
		/// </summary>
		[Description("Индивидуальное жилищное строительство (ИЖС)")]
        [EnumCode("6")]
        [ShortTitle("Индивидуальное жилищное строительство (ИЖС)")]
        IndividualHousingConstruction = 6,
		/// <summary>
		/// Промышленность (7)
		/// </summary>
		[Description("Промышленность")]
        [EnumCode("7")]
        [ShortTitle("Промышленность")]
        Industry = 7,
		/// <summary>
		/// Отдых (рекреация) (8)
		/// </summary>
		[Description("Отдых (рекреация)")]
        [EnumCode("8")]
        [ShortTitle("Отдых (рекреация)")]
        Leisure = 8,
		/// <summary>
		/// Малоэтажное жилищное строительство (МЖС) (9)
		/// </summary>
		[Description("Малоэтажное жилищное строительство (МЖС)")]
        [EnumCode("9")]
        [ShortTitle("Малоэтажное жилищное строительство (МЖС)")]
        LowriseHousing = 9,
		/// <summary>
		/// Общественное использование объектов капитального строительства (10)
		/// </summary>
		[Description("Общественное использование объектов капитального строительства")]
        [EnumCode("10")]
        [ShortTitle("Общественное использование объектов капитального строительства")]
        PublicUseOfCapitalConstruction = 10,
		/// <summary>
		/// Обслуживание автотранспорта (11)
		/// </summary>
		[Description("Обслуживание автотранспорта")]
        [EnumCode("11")]
        [ShortTitle("Обслуживание автотранспорта")]
        ServiceVehicles = 11,
		/// <summary>
		/// Торговые центры (12)
		/// </summary>
		[Description("Торговые центры")]
        [EnumCode("12")]
        [ShortTitle("Торговые центры")]
        ShoppingCenters = 12,
		/// <summary>
		/// Склады (13)
		/// </summary>
		[Description("Склады")]
        [EnumCode("13")]
        [ShortTitle("Склады")]
        Warehouses = 13,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Подъездные пути (12088)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12088)]
    public enum DrivewayType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Асфальтированная дорога (1)
		/// </summary>
		[Description("Асфальтированная дорога")]
        [EnumCode("1")]
        [ShortTitle("Асфальтированная дорога")]
        Asphalt = 1,
		/// <summary>
		/// Грунтовая дорога (2)
		/// </summary>
		[Description("Грунтовая дорога")]
        [EnumCode("2")]
        [ShortTitle("Грунтовая дорога")]
        Ground = 2,
		/// <summary>
		/// Нет (3)
		/// </summary>
		[Description("Нет")]
        [EnumCode("3")]
        [ShortTitle("Нет")]
        No = 3,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Единица измерения (12089)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12089)]
    public enum ParcelAreaUnitType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Гектар (1)
		/// </summary>
		[Description("Гектар")]
        [EnumCode("1")]
        [ShortTitle("Гектар")]
        Hectare = 1,
		/// <summary>
		/// Сотка (2)
		/// </summary>
		[Description("Сотка")]
        [EnumCode("2")]
        [ShortTitle("Сотка")]
        Sotka = 2,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Тип участка (12090)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12090)]
    public enum ParcelType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// В собственности (1)
		/// </summary>
		[Description("В собственности")]
        [EnumCode("1")]
        [ShortTitle("В собственности")]
        Owned = 1,
		/// <summary>
		/// В аренде (2)
		/// </summary>
		[Description("В аренде")]
        [EnumCode("2")]
        [ShortTitle("В аренде")]
        Rent = 2,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Статус земли (12091)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12091)]
    public enum ParcelStatus : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Фермерское хозяйство (1)
		/// </summary>
		[Description("Фермерское хозяйство")]
        [EnumCode("1")]
        [ShortTitle("Фермерское хозяйство")]
        Farm = 1,
		/// <summary>
		/// Садоводство (2)
		/// </summary>
		[Description("Садоводство")]
        [EnumCode("2")]
        [ShortTitle("Садоводство")]
        Gardening = 2,
		/// <summary>
		/// Индивидуальное жилищное строительство (3)
		/// </summary>
		[Description("Индивидуальное жилищное строительство")]
        [EnumCode("3")]
        [ShortTitle("Индивидуальное жилищное строительство")]
        IndividualHousingConstruction = 3,
		/// <summary>
		/// Земля промышленного назначения (4)
		/// </summary>
		[Description("Земля промышленного назначения")]
        [EnumCode("4")]
        [ShortTitle("Земля промышленного назначения")]
        IndustrialLand = 4,
		/// <summary>
		/// Инвестпроект (5)
		/// </summary>
		[Description("Инвестпроект")]
        [EnumCode("5")]
        [ShortTitle("Инвестпроект")]
        InvestmentProject = 5,
		/// <summary>
		/// Личное подсобное хозяйство (6)
		/// </summary>
		[Description("Личное подсобное хозяйство")]
        [EnumCode("6")]
        [ShortTitle("Личное подсобное хозяйство")]
        PrivateFarm = 6,
		/// <summary>
		/// Дачное некоммерческое партнерство (7)
		/// </summary>
		[Description("Дачное некоммерческое партнерство")]
        [EnumCode("7")]
        [ShortTitle("Дачное некоммерческое партнерство")]
        SuburbanNonProfitPartnership = 7,
		/// <summary>
		/// Участок сельскохозяйственного назначения (8)
		/// </summary>
		[Description("Участок сельскохозяйственного назначения")]
        [EnumCode("8")]
        [ShortTitle("Участок сельскохозяйственного назначения")]
        ForAgriculturalPurposes = 8,
		/// <summary>
		/// Участок промышленности, транспорта, связи и иного не сельхоз. назначения (9)
		/// </summary>
		[Description("Участок промышленности, транспорта, связи и иного не сельхоз. назначения")]
        [EnumCode("9")]
        [ShortTitle("Участок промышленности, транспорта, связи и иного не сельхоз. назначения")]
        IndustryTransportCommunications = 9,
		/// <summary>
		/// Поселений (10)
		/// </summary>
		[Description("Поселений")]
        [EnumCode("10")]
        [ShortTitle("Поселений")]
        Settlements = 10,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Локация электроснабжения (12092)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12092)]
    public enum ElectricityLocationType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// По границе участка (1)
		/// </summary>
		[Description("По границе участка")]
        [EnumCode("1")]
        [ShortTitle("По границе участка")]
        Border = 1,
		/// <summary>
		/// На участке (2)
		/// </summary>
		[Description("На участке")]
        [EnumCode("2")]
        [ShortTitle("На участке")]
        Location = 2,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Локация газоснабжения (12093)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12093)]
    public enum GasLocationType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// По границе участка (1)
		/// </summary>
		[Description("По границе участка")]
        [EnumCode("1")]
        [ShortTitle("По границе участка")]
        Border = 1,
		/// <summary>
		/// На участке (2)
		/// </summary>
		[Description("На участке")]
        [EnumCode("2")]
        [ShortTitle("На участке")]
        Location = 2,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Давление газа (12094)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12094)]
    public enum GasPressureType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Высокое (1)
		/// </summary>
		[Description("Высокое")]
        [EnumCode("1")]
        [ShortTitle("Высокое")]
        High = 1,
		/// <summary>
		/// Среднее (2)
		/// </summary>
		[Description("Среднее")]
        [EnumCode("2")]
        [ShortTitle("Среднее")]
        Middle = 2,
		/// <summary>
		/// Низкое (3)
		/// </summary>
		[Description("Низкое")]
        [EnumCode("3")]
        [ShortTitle("Низкое")]
        Low = 3,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Локация канализации (12095)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12095)]
    public enum DrainageLocationType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// По границе участка (1)
		/// </summary>
		[Description("По границе участка")]
        [EnumCode("1")]
        [ShortTitle("По границе участка")]
        Border = 1,
		/// <summary>
		/// На участке (2)
		/// </summary>
		[Description("На участке")]
        [EnumCode("2")]
        [ShortTitle("На участке")]
        Location = 2,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Тип канализации (12096)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12096)]
    public enum DrainageType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Автономная (1)
		/// </summary>
		[Description("Автономная")]
        [EnumCode("1")]
        [ShortTitle("Автономная")]
        Autonomous = 1,
		/// <summary>
		/// Центральная (2)
		/// </summary>
		[Description("Центральная")]
        [EnumCode("2")]
        [ShortTitle("Центральная")]
        Central = 2,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Локация водоснабжения (12097)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12097)]
    public enum WaterLocationType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// По границе участка (1)
		/// </summary>
		[Description("По границе участка")]
        [EnumCode("1")]
        [ShortTitle("По границе участка")]
        Border = 1,
		/// <summary>
		/// На участке (2)
		/// </summary>
		[Description("На участке")]
        [EnumCode("2")]
        [ShortTitle("На участке")]
        Location = 2,
    }
}

namespace ObjectModel.Directory
{
    /// <summary>
    /// Тип водоснабжения (12098)
    ///</summary>
    [ReferenceInfo(ReferenceId = 12098)]
    public enum WaterType : long
    {
		/// <summary>
		/// Значение отсутствует (0)
		/// </summary>
		[Description("Значение отсутствует")]
        [EnumCode("0")]
        [ShortTitle("Значение отсутствует")]
        None = 0,
		/// <summary>
		/// Автономная (1)
		/// </summary>
		[Description("Автономная")]
        [EnumCode("1")]
        [ShortTitle("Автономная")]
        Autonomous = 1,
		/// <summary>
		/// Центральная (2)
		/// </summary>
		[Description("Центральная")]
        [EnumCode("2")]
        [ShortTitle("Центральная")]
        Central = 2,
		/// <summary>
		/// Водонапорная станция (3)
		/// </summary>
		[Description("Водонапорная станция")]
        [EnumCode("3")]
        [ShortTitle("Водонапорная станция")]
        PumpingStation = 3,
		/// <summary>
		/// Водозаборный узел (4)
		/// </summary>
		[Description("Водозаборный узел")]
        [EnumCode("4")]
        [ShortTitle("Водозаборный узел")]
        WaterIntakeFacility = 4,
		/// <summary>
		/// Водонапорная башня (5)
		/// </summary>
		[Description("Водонапорная башня")]
        [EnumCode("5")]
        [ShortTitle("Водонапорная башня")]
        WaterTower = 5,
    }
}

