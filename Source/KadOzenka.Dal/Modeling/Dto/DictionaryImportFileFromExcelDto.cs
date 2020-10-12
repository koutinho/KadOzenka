namespace KadOzenka.Dal.Modeling.Dto
{
	public class DictionaryImportFileFromExcelDto
	{
		public bool IsNewDictionary { get; set; }

		public string NewDictionaryName { get; set; }

		public long DictionaryId { get; set; }

		public bool DeleteOldValues { get; set; } = false;

		public DictionaryImportFileInfoDto FileInfo { get; set; }
	}
}