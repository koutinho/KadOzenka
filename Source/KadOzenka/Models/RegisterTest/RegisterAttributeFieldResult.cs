using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIPJS.Models.RegisterTest
{
    public class RegisterAttributeFieldResult
    {
        public long AttributeId { get; set; }

        public object Value { get; set; }
    }

	public class RegisterObjectResult
	{
		public List<RegisterAttributeFieldResult> List { get; set; }
		public int RegisterId { get; set; }
		public int ObjectId { get; set; }
	}
}
