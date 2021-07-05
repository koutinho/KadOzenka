using ObjectModel.Directory.ES;
using ObjectModel.Directory.KO;

namespace KadOzenka.Dal.Modeling.Dto
{
    public class DictionaryImportFileInfoDto
    {
        public string FileName { get; set; }
        public string ValueColumnName { get; set; }
        public string CalcValueColumnName { get; set; }
        public ModelDictionaryType ValueType { get; set; }
    }
}
