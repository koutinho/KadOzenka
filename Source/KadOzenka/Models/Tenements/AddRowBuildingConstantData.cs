using ObjectModel.Insur;
using System.Collections.Generic;
using System.Data;

namespace CIPJS.Models.Tenements
{
    public class AddRowBuildingConstantData
    {
        public List<BuildingConsolidatedDataDto> Models { get; set; }

        public Dictionary<string, string> Sources { get; set; }

        public string Group { get; set; }

        public DataTable BuildingTable { get; set; }

        public DataTable EgrnTable { get; set; }

        public List<DataTable> BtiTables { get; set; }

        public List<OMInputNach> MfcSources { get; set; }
    }
}
