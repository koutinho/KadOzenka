using System.Collections.Generic;
using CommonSdks.Excel;

namespace ModelingBusiness.Objects.Entities
{
	public class ModelObjectsConstructor
	{
		public int? IdColumnIndex { get; set; }
		public bool IsCreation => IdColumnIndex == null;
		public long? ModelId { get; set; }
		public List<ColumnToAttributeMapping> ColumnsMapping { get; set; }
	}
}
