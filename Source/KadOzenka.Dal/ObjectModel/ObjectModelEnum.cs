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

}
