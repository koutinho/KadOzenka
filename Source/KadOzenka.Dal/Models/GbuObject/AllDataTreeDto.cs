using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KadOzenka.Web.Models.GbuObject
{
    public class AllDataTreeDto
    {
		public string NodeId { get; set; }

		public string ParentNodeId { get; set; }

		public long AttributeId { get; set; }

		public long RegisterId { get; set; }
		
		public string NodeText { get; set; }

		public int Level { get; set; }

		public string ContentUrl { get; set; }

		public bool HasChilds { get; set; }
	}
}
