using ObjectModel.Directory.ES;

namespace KadOzenka.Dal.ExpressScore.Dto
{
    public class ReferenceItemDto
    {
        public long Id { get; set; }
        public long ReferenceId { get; set; }
        public string CommonValue { get; set; }
        public string Value { get; set; }
        public decimal? CalcValue { get; set; }
    }
}
