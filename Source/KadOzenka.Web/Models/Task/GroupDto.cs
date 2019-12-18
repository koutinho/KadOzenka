using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KadOzenka.Web.Models.Task
{
	public class GroupDto
	{
		public long? Id { get; set; }
		public long? RatingTourId { get; set; }

		public string ObjType { get; set; }
		public long? ParentGroupId { get; set; }

		public long? GroupingMechanismId { get; set; }
		public string Name { get; set; }
	}
}
