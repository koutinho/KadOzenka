using System.Collections.Generic;

namespace ModelingBusiness.Objects.Entities
{
	public class ModelObjectsConstructor
	{
		public int? IdColumnIndex { get; set; }
		public bool IsCreation => IdColumnIndex == null;
		public long? ModelId { get; set; }
		public List<ColumnToAttributeMapping> ColumnsMapping { get; set; }
	}


	public class ColumnToAttributeMapping
	{
		public int ColumnIndex { get; set; }

		//для нормализованных атрибутов ИД идет с префиксом _1 (для значения)и _2 для коэффициента
		public string AttributeId { get; set; }


		public ColumnToAttributeMapping()
		{
			
		}

		public ColumnToAttributeMapping(int columnIndex, string attributeId)
		{
			ColumnIndex = columnIndex;
			AttributeId = attributeId;
		}
	}
}
