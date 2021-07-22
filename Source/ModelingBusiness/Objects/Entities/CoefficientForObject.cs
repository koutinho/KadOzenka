namespace ModelingBusiness.Objects.Entities
{
	public class CoefficientForObject
	{
		public long AttributeId { get; set; }
		public decimal? Coefficient { get; set; }
		public string Value { get; set; }

		public CoefficientForObject(long attributeId)
		{
			AttributeId = attributeId;
		}

		//для сериализации нужен конструктор без параметров
		protected CoefficientForObject()
		{

		}
	}
}