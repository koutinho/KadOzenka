using System.ComponentModel.DataAnnotations;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Directory;
using ObjectModel.KO;

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

		
        public static ManualModelingModel ToModel(OMModel model)
        {
            return new ManualModelingModel
            {
	            Id = model.Id,
	            Name = model.Name,
	            Description = model.Description,
	            Type = model.Type_Code,
	            AlgorithmType = model.AlgoritmType_Code,
	            CalculationType = model.CalculationType_Code,
                //TODO Tour, objectType
                GroupId = model.GroupId,
                A0 = model.A0,
                Formula = model.Formula,
                CalculationMethodCode = model.CalculationMethod_Code
            };
        }

        public override ModelingModelDto ToDto()
        {
	        var dto = base.ToDto();
	       
	        dto.CalculationMethod = CalculationMethodCode;

	        return dto;
        }
    }
}
