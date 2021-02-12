namespace KadOzenka.Dal.Modeling.Entities
{
	public class ColumnToAttributeMapping
	{
		public int ColumnIndex { get; set; }

		//для нормализованных атрибутов ИД идет с префиксом _1 (для значения)и _2 для коэффициента
		public string AttributeId { get; set; }
	}
}
