using System;
using System.Collections.Generic;
using System.Text;

namespace KadOzenka.Dal.CommonFunctions.Dto
{
	public class RecycleBinDto
	{
		public long Id { get; set; }
		public DateTime DeletedTime { get; set; }
		public string ObjectType { get; set; }
		public string ObjectName { get; set; }
	}
}
