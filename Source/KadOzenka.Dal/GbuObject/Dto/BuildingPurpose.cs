using System.ComponentModel;

namespace KadOzenka.Dal.GbuObject.Dto
{
    public enum BuildingPurpose
    {
        [Description("Значение отсутствует")]
        None = 0,

        [Description("Жилое")]
        Live = 1,

        [Description("Нежилое")]
        NotLive = 2,

        [Description("Многоквартирный дом")]
        ApartmentHouse = 3,

        [Description("Жилое и Многоквартирный дом")]
        LiveAndApartmentHouse = 4
    }
}
