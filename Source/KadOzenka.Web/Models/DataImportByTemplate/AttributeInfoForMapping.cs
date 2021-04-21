namespace KadOzenka.Web.Models.DataImportByTemplate
{
	public class AttributeInfoForMapping
	{
		public int Id { get; set; }

		public string ItemId { get; set; }

		public int Ordinal { get; set; }

		public string Description { get; set; }

		public string FullDescription { get; set; }

		public int DocumentType { get; set; }

		public long AttributeId { get; set; }

		public int ReferenceId { get; set; }

		public string ParentId { get; set; }

		public bool IsSelected { get; set; }

		public bool Hidden { get; set; }
	}
}
