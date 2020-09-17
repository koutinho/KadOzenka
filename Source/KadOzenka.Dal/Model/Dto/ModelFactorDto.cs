using Core.Register;

namespace KadOzenka.Dal.Model.Dto
{
    public class ModelFactorDto
    {
        public long Id { get; set; }
        public long? ModelId { get; set; }
        public long FactorId { get; set; }
        public string Factor { get; set; }
        public RegisterAttributeType Type { get; set; }
        public long RegisterId { get; set; }
        public long? MarkerId { get; set; }
        public decimal Weight { get; set; }
        public decimal B0 { get; set; }
        public bool SignDiv { get; set; }
        public bool SignAdd { get; set; }
        public bool SignMarket { get; set; }
    }

    public class ModelFactorPure
    {
        public long Id { get; set; }
        public long FactorId { get; set; }
        public string Name { get; set; }
        public long RegisterId { get; set; }
    }
}
