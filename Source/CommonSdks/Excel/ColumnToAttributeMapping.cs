namespace CommonSdks.Excel
{
    public class ColumnToAttributeMapping
	{
		public int ColumnIndex { get; set; }
		public long AttributeId { get; set; }

		public ColumnToAttributeMapping()
		{
			
		}

		public ColumnToAttributeMapping(int columnIndex, long attributeId)
		{
			ColumnIndex = columnIndex;
			AttributeId = attributeId;
		}
	}
}
