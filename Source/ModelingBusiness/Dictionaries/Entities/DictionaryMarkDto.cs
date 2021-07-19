namespace ModelingBusiness.Dictionaries.Entities
{
    public class DictionaryMarkDto
    {
        public long Id { get; set; }
        public long DictionaryId { get; set; }
        public string Value { get; set; }
        public decimal? CalculationValue { get; set; }
    }
}
