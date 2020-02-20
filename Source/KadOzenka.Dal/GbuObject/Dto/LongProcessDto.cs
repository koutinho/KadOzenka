using ObjectModel.Directory.Core.LongProcess;

namespace KadOzenka.Dal.GbuObject.Dto
{
	public class LongProcessDto
	{
		public long Id { get; set; }
		public long ProcessTypeId { get; set; }
		public string Name { get; set; }
		public Status? StatusCode { get; set; }
		public string StatusName { get; set; }
		public long? Progress { get; set; }
	}
}
