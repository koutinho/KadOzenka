using ObjectModel.Directory.ES;

namespace KadOzenka.Dal.Tours.Dto
{
    public class GroupingDictionaryImportFileInfoDto
    {
        public string FileName { get; set; }
        public string ValueColumnName { get; set; }
        public string CalcValueColumnName { get; set; }
        public ReferenceItemCodeType ValueType { get; set; }
    }
}
