namespace KadOzenka.Dal.ScoreCommon.Dto
{
	public class NumberReference
	{
		public string CommonValue { get; set; }
		public decimal Key { get; set; }
		public decimal Value { get; set; }
	}

	public class NumberReferenceInterval
	{
		public string CommonValue { get; set; }
		public decimal KeyFrom { get; set; }
		public decimal KeyTo { get; set; }
		public decimal Value { get; set; }
	}
}