using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.KoObject;
using KadOzenka.Dal.Tours.Dto;

namespace KadOzenka.Web.Models.GbuObject
{
	public class EstimatedGroupViewModel
	{
        [Display(Name = "Задание на оценку")]
	    [Required(ErrorMessage = "Параметр Задание на оценку обязательный")]
        public long? IdTask { get; set; }

        [Display(Name = "Статус")]
        public List<UnitChangeStatus> UnitChangeStatus { get; set; }

		[Required(ErrorMessage = "Атрибут для проставления оценочной группы обязательный")]
		public long? IdEstimatedSubGroup { get; set; }

        public EstimatedGroupModel ToGroupModel(TourEstimatedGroupAttributeParamsDto paramsDto)
        {
            return new EstimatedGroupModel
            {
                IdTask = IdTask.Value,
                UnitChangeStatus = UnitChangeStatus,
                IdCodeQuarter = paramsDto.IdCodeQuarter,
                IdCodeGroup = paramsDto.IdCodeGroup,
                IdTerritoryType = paramsDto.IdTerritoryType,
                IdEstimatedSubGroup = IdEstimatedSubGroup.GetValueOrDefault()
            };
        }
    }
}