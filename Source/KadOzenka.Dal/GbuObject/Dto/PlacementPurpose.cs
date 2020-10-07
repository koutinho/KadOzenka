using System.ComponentModel;

namespace KadOzenka.Dal.GbuObject.Dto
{
    public enum PlacementPurpose
    {
        [Description("Значение отсутствует")]
        None = 0,

        [Description("Жилое")]
        Live = 1,

        [Description("Нежилое")]
        NotLive = 2,

        [Description("Машино-место")]
        ParkingPlace = 3,

        [Description("Жилое и нежилое")]
        LiveAndNotLive = 3
    }
}
