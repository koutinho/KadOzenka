namespace KadOzenka.Dal.Modeling.Dto
{
    public class DictionaryMarkDto
    {
        public long Id { get; set; }
        public long DictionaryId { get; set; }
        public string Value { get; set; }
        public decimal? CalcValue { get; set; }
    }
}
