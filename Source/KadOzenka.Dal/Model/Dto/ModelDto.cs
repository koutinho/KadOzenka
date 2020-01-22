using ObjectModel.Directory;

namespace KadOzenka.Dal.Model.Dto
{
    public class ModelDto
    {
        public long Id { get; set; }
        public long? GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Formula { get; set; }
        public string AlgorithmType { get; set; }
        public KoAlgoritmType AlgorithmTypeCode { get; set; }
        public decimal? A0 { get; set; }
    }
}
