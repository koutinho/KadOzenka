using System.Collections.Generic;

namespace ModelingBusiness.Objects.Import.Entities
{
	public class ModelObjectsFromExcelData
	{
		public long? Id { get; set; }
		public int RowIndexInFile { get; set; }
		public List<Column> Columns { get; set; }

		public ModelObjectsFromExcelData()
		{
			Columns = new List<Column>();
		}
	}

	public class Column
	{
		public long AttributeId { get; set; }
		public object ValueToUpdate { get; set; }
	}
}