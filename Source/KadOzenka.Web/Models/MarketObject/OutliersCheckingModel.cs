using System;

namespace KadOzenka.Web.Models.MarketObject
{
	public class OutliersCheckingModel
	{
		public bool HasHistory { get; set; }
		public DateTime? LastDateCreated { get; set; }
		public DateTime? LastDateStarted { get; set; }
		public DateTime? LastDateFinished { get; set; }
		public string LastStatus { get; set; }
		public string LastMarketSegment { get; set; }
		public string LastPropertyTypes { get; set; }
		public long? LastTotalObjectsCount { get; set; }
		public long? LastExcludedObjectsCount { get; set; }
		public string LastReportDownloadLink { get; set; }


		public bool HasCurrentAddedProcess { get; set; }
		public bool HasCurrentRunningProcess { get; set; }
		public long? CurrentProgress { get; set; }
	}
}
