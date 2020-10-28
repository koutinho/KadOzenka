using System;

namespace KadOzenka.Dal.ScoreCommon.Dto
{
	public class DateReference
	{
		public string CommonValue { get; set; }

		public DateTime Key { get; set; }

		public decimal Value { get; set; }
	}

	public class DateReferenceInterval
	{
		public string CommonValue { get; set; }
		public DateTime KeyFrom { get; set; }

		public DateTime KeyTo { get; set; }

		public decimal Value { get; set; }
	}
}