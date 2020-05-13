namespace KadOzenka.Dal.Modeling.Dto
{
	public class CoefficientForObject
    {
        public long AttributeId { get; set; }

        public decimal? Coefficient { get; set; }

        //временное решение (нужно логировать ошибки). в будущем будем обрабатывать этот кейс раньше
        public string Message { get; set; }

        public CoefficientForObject(long attributeId)
        {
            AttributeId = attributeId;
        }

        protected CoefficientForObject()
        {

        }
    }
}
