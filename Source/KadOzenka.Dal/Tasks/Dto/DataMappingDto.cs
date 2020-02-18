using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KadOzenka.Dal.Models.Task
{
	public class DataMappingDto
	{
		public long ObjectId { get; set; }
		public long AttributeId { get; set; }
		public string Attribute { get; set; }

		public string Value { get; set; }
		public string OldValue { get; set; }
	}
}
