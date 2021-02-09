using System;

namespace KadOzenka.Dal.RecycleBin.Dto
{
	public class RecycleBinDto
	{
		public long Id { get; set; }
		public DateTime DeletedTime { get; set; }
		public string ObjectType { get; set; }
		public string ObjectName { get; set; }
		public long ObjectRegisterId { get; set; }
	}
}
