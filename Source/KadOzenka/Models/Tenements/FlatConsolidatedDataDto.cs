namespace CIPJS.Models.Tenements
{
    public class FlatConsolidatedDataDto
    {
        public string Group { get; set; }

        public string Name { get; set; }

        public string Source { get; set; }

        public ConsolidatedDataValueDto Flat { get; set; }

        public ConsolidatedDataValueDto Egrn { get; set; }

        public ConsolidatedDataValueDto Bti { get; set; }

        public ConsolidatedDataValueDto Mfc { get; set; }

        public int OrdinalNumber { get; set; }
    }
}