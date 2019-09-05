using System.ComponentModel;
using Core.ObjectModel.CustomAttribute;
using Core.Shared.Attributes;

namespace ObjectModel.Directory
{
    /// <summary>
    /// Виды сторонних площадок (1)
    ///</summary>
    [ReferenceInfo(ReferenceId = 1)]
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
    }

    /// <summary>
    /// Виды объектов недвижимости (2)
    ///</summary>
    [ReferenceInfo(ReferenceId = 2)]
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

}
