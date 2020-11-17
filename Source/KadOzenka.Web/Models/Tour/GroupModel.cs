using System.ComponentModel.DataAnnotations;
using Core.Shared.Extensions;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Groups.Dto.Consts;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.Tour
{
	public class GroupModel
	{
		public long? Id { get; set; }
		public long? RatingTourId { get; set; }
		public long? ModelId { get; set; }
        public GroupType GroupType { get; set; }

        public string ObjType { get; set; }

		public long? GroupingAlgorithmId { get; set; }

		public long? ParentGroupId { get; set; }

		[Display(Name = "Наименование")]
        [Required(ErrorMessage = "Не заполнено имя группы")]
        public string Name { get; set; }

        [Display(Name = "Номер")]
        [Required(ErrorMessage = "Не заполнен номер группы")]
        public int? Number { get; set; }

        public bool IsReadOnly { get; set; }


        public static GroupModel ToModel(GroupDto group)
        {
            var parentId = group.ParentGroupId;
            if (parentId == -1)
	        {
		        if (group.GroupAlgorithmCode == KoGroupAlgoritm.MainOKS)
		        {
			        parentId = (long?)KoGroupAlgoritm.MainOKS;
		        }
		        else if (group.GroupAlgorithmCode == KoGroupAlgoritm.MainParcel)
		        {
			        parentId = (long?)KoGroupAlgoritm.MainParcel;
		        }
	        }

            return new GroupModel
            {
                Id = group.Id,
				RatingTourId = group.RatingTourId,
                Name = group.Name,
                Number = group.Number,
				ParentGroupId = parentId,
                GroupType = group.GroupType,
                GroupingAlgorithmId = group.GroupingAlgorithmId,
				ObjType = group.GroupAlgorithmCode.GetEnumDescription()
			};
        }

        public static GroupDto FromModel(GroupModel group)
        {
            var parentId = group.ParentGroupId == (long?) KoGroupAlgoritm.MainOKS ||
                           group.ParentGroupId == (long?) KoGroupAlgoritm.MainParcel
                ? -1
                : group.ParentGroupId;

            return new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                Number = group.Number,
                GroupingAlgorithmId = group.GroupingAlgorithmId,
                ParentGroupId = parentId,
                RatingTourId = group.RatingTourId
            };
        }
    }
}
