using System.Collections.Generic;
using KadOzenka.Dal.GbuObject;

namespace KadOzenka.Web.Models.GbuObject.ObjectAttributes
{
	public class RegisterDto
	{
		public long Id { get; set; }
	    public long ObjectId { get; set; }
        public string Name { get; set; }
		public List<AttributeDto> RegisterAttributes { get; set; }

		public RegisterDto(long id, long objectId, string name, List<GbuObjectAttribute> attributes)
		{
			Id = id;
		    ObjectId = objectId;
			Name = name;
		    RegisterAttributes = new List<AttributeDto>();
            foreach (var attribute in attributes)
		    {
		        RegisterAttributes.Add(new AttributeDto(attribute, objectId));
            }
		}
	}
}
