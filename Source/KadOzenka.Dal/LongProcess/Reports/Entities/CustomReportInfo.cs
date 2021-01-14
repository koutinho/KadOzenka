using System.IO;

namespace KadOzenka.Dal.LongProcess.Reports.Entities
{
	public class CustomReportInfo
	{
		public Stream Stream { get; set; }
		public string FullFileName { get; set; }
		public string FileExtension { get; set; }
	}
}
