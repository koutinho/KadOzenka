namespace ModelingBusiness.Dictionaries.Entities
{
	public class DictionaryImportFileFromExcelDto
	{
		public long DictionaryId { get; set; }

		public bool DeleteOldValues { get; set; } = false;

		public DictionaryImportFileInfoDto FileInfo { get; set; }
	}
}