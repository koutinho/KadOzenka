using System.Collections.Generic;

namespace CIPJS.Models.Tenements
{
    public class BuildingConsolidatedDataDto
    {
        public string Group { get; set; }

        public string Name { get; set; }

        public string Source { get; set; }

        public ConsolidatedDataValueDto Mkd { get; set; }

        public ConsolidatedDataValueDto Egrn { get; set; }

        public List<ConsolidatedDataValueDto> Btis { get; set; }

        public ConsolidatedDataValueDto Mfc { get; set; }

        public int OrdinalNumber { get; set; }

        public string NameClass { get; set; }

        public BuildingConsolidatedDataDto()
        {
            NameClass = "";
        }
    }
}