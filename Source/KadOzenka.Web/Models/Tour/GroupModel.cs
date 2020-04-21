using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Groups.Dto.Consts;
using ObjectModel.Directory;

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
	        var parentId = group.ParentGroupId;
	        if (parentId == -1)
	        {
		        if (group.GroupAlgoritmCode == KoGroupAlgoritm.MainOKS)
		        {
			        parentId = (long?)KoGroupAlgoritm.MainOKS;
		        }
		        else if (group.GroupAlgoritmCode == KoGroupAlgoritm.MainParcel)
		        {
			        parentId = (long?)KoGroupAlgoritm.MainParcel;
		        }
	        }

			return new GroupModel
            {
                Id = group.Id,
				RatingTourId = group.RatingTourId,
                Name = group.Name,
				ParentGroupId = parentId
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
