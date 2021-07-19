using System.ComponentModel.DataAnnotations;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject.Dto;
using ModelingBusiness.Model.Entities;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.Modeling
{
    public class ManualModelingModel : GeneralModelingModel
    {
	    public bool IsPartial { get; set; }

        [Display(Name = "Формула")]
        public string Formula { get; set; }

        [Display(Name = "Метод расчета")]
        public string CalculationMethod => CalculationMethodCode.GetEnumDescription();

        [Display(Name = "Метод расчета")]
        public KoCalculationMethod CalculationMethodCode { get; set; }

		
        public static ManualModelingModel ToModel(ModelDto entity)
        {
	        return new ManualModelingModel
	        {
		        Id = entity.ModelId,
		        Name = entity.Name,
		        Description = entity.Description,
		        Type = entity.Type,
		        AlgorithmTypeForCadastralPriceCalculation = entity.AlgorithmTypeForCadastralPriceCalculation,
		        CalculationType = entity.CalculationType,
		        TourId = entity.TourId,
		        TourYear = entity.TourYear,
				ObjectType = entity.IsOksObjectType ? ObjectTypeExtended.Oks : ObjectTypeExtended.Zu,
				GroupId = entity.GroupId,
		        A0 = entity.A0,
		        Formula = entity.Formula,
		        CalculationMethodCode = entity.CalculationMethod,
				IsActive = entity.IsActive
			};
        }

        public override ModelDto ToDto()
        {
	        var dto = base.ToDto();
	       
	        dto.CalculationMethod = CalculationMethodCode;

	        return dto;
        }
    }
}
