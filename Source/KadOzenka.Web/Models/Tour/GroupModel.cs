using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Groups.Dto.Consts;

namespace KadOzenka.Web.Models.Tour
{
	public class GroupModel
	{
		public long? Id { get; set; }
		public long? RatingTourId { get; set; }

		public string ObjType { get; set; }
		public long? ParentGroupId { get; set; }

		public long? GroupingMechanismId { get; set; }

        [Display(Name = "Наименование")]
		public string Name { get; set; }

        public bool IsReadOnly { get; set; }


        public static GroupModel ToModel(GroupDto group)
        {
            return new GroupModel
            {
                Id = group.Id,
                Name = group.Name
            };
        }

        public static GroupDto FromModel(GroupModel group)
        {
            return new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                GroupingMechanismId = group.GroupingMechanismId,
                ParentGroupId = group.ParentGroupId,
                RatingTourId = group.RatingTourId
            };
        }
    }
}
