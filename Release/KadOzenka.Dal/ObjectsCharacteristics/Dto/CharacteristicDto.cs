using Core.Register;

namespace KadOzenka.Dal.ObjectsCharacteristics.Dto
{
    public class CharacteristicDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long RegisterId { get; set; }
        public RegisterAttributeType Type { get; set; }
        public long? ReferenceId { get; set; }
    }
}
