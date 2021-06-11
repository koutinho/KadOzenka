using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Groups.Dto.Consts;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.Tour
{
	public class GroupModel : IValidatableObject
	{
		public long? Id { get; set; }
		public long? RatingTourId { get; set; }
		public GroupType GroupType { get; set; }

		public long? ParentGroupId { get; set; }

		[Display(Name = "Наименование")]
        [Required(ErrorMessage = "Не заполнено имя группы")]
        public string Name { get; set; }

        [Display(Name = "Механизм группировки")]
        public KoGroupAlgoritm GroupAlgorithm { get; set; }

        [Display(Name = "Номер")]
        [Required(ErrorMessage = "Не заполнен номер группы")]
        public int? Number { get; set; }

        [Display(Name = "Модель")]
        public long? ModelId { get; set; }
        public List<SelectListItem> Models { get; set; }

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
                GroupAlgorithm = group.GroupAlgorithmCode
			};
        }

        public static GroupDto FromModel(GroupModel group)
        {
	        return new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                Number = group.Number,
                GroupAlgorithmCode = group.GroupAlgorithm,
                ParentGroupId = group.ParentGroupId,
                RatingTourId = group.RatingTourId
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
	        if (string.IsNullOrWhiteSpace(Name))
		        yield return new ValidationResult("Не заполнено имя группы");

	        if (Number == null)
		        yield return new ValidationResult("Не заполнен номер");

	        if (RatingTourId == null)
	            yield return new ValidationResult("Не заполнен тур");
        }
    }
}
