using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KadOzenka.Web.Models.Task
{
	public class GroupTreeDto
	{
		public long? Id { get; set; }
		public long? ParentId { get; set; }
		public string GroupName { get; set; }
		public long? TourId { get; set; }
	}
}
