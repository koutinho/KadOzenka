using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KadOzenka.Web.Models.Tour
{
	public class ParentCalcGroupModel
	{
		public long Id { get; set; }
		public long? GroupId { get; set; }
		public long? ParentCalcGroupId { get; set; }
		public string Title { get; set; }
	}
}
