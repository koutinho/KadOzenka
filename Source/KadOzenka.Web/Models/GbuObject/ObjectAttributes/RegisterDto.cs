using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Gbu;

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
		    var attr = attributes.Select(x => x.AttributeId).ToList();
		    var settingsList = OMAttributeSettings.Where(x => attr.Contains(x.AttributeId)).SelectAll().Execute();
		    foreach (var attribute in attributes)
		    {
		        RegisterAttributes.Add(new AttributeDto(attribute, objectId, settingsList.FirstOrDefault(x=>x.AttributeId == attribute.AttributeId)));
            }
		}
	}
}
