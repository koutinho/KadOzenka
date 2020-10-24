using System;
using System.Collections.Generic;

namespace KadOzenka.Web.Models.Tour
{
	public class UnloadSettingsQueueModel
	{
		public long Id { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateStarted { get; set; }
		public DateTime? DateFinished { get; set; }
		public string Status { get; set; }
		public long? UnloadCurrentCount { get; set; }
		public long? UnloadTotalCount { get; set; }
		public string CurrentUnloadType { get; set; }
		public List<string> UnloadTypes { get; set; }
		public long? CurrentUnloadProgress { get; set; }
		public string LongProcessUrl { get; set; }
		public UnloadSettingsQueueExportFileModel ExportFile { get; set; }
	}
}
