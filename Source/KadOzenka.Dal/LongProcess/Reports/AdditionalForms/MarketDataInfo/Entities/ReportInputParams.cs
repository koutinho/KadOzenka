using System;

namespace KadOzenka.Dal.LongProcess.Reports.AdditionalForms.MarketDataInfo.Entities
{
	public class ReportInputParams
	{
		public DateTime? DateFrom { get; set; }

		public DateTime? DateTo { get; set; }

		public long TypeOfUseCodeAttributeId { get; set; }

		public long OksGroupAttributeId { get; set; }

		public long TypeOfUseAttributeId { get; set; }
	}
}