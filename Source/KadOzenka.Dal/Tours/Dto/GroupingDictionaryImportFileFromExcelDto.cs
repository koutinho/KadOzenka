namespace KadOzenka.Dal.Tours.Dto
{
	public class GroupingDictionaryImportFileFromExcelDto
	{
		public bool IsNewDictionary { get; set; }

		public string NewDictionaryName { get; set; }

		public long DictionaryId { get; set; }

		public bool DeleteOldValues { get; set; } = false;

		public GroupingDictionaryImportFileInfoDto FileInfo { get; set; }
	}
}