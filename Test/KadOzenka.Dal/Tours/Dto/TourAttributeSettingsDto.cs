using ObjectModel.Directory;

namespace KadOzenka.Dal.Tours.Dto
{
    public class TourAttributeSettingsDto
    {
        public long TourId { get; set; }
        public KoAttributeUsingType KoAttributeUsingType { get; set; }
        public long? AttributeId { get; set; }
    }
}
