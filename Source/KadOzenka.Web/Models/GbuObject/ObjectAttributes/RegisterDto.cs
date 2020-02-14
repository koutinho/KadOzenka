using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KadOzenka.Dal.GbuObject;

namespace KadOzenka.Web.Models.GbuObject.ObjectAttributes
{
	public class RegisterDto
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public List<GbuObjectAttribute> RegisterAttributes { get; set; }

		public RegisterDto(long id, string name, List<GbuObjectAttribute> attributes)
		{
			Id = id;
			Name = name;
			RegisterAttributes = attributes;
		}
	}
}
