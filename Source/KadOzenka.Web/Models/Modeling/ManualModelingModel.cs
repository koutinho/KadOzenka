using System.ComponentModel.DataAnnotations;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Modeling
{
    public class ManualModelingModel
    {
        public long GeneralModelId { get; set; }
        public bool IsPartial { get; set; }

        [Display(Name= "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Тип")]
        public string Type { get; set; }

        [Display(Name = "Группа")]
        public long? GroupId { get; set; }

        [Display(Name = "Формула")]
        public string Formula { get; set; }

        public string AlgorithmType { get; set; }

        [Display(Name = "Алгоритм расчета")]
        public KoAlgoritmType AlgorithmTypeCode { get; set; }

        public string CalculationType { get; set; }

        [Display(Name = "Тип расчета")]
        public KoCalculationType CalculationTypeCode { get; set; }

        public string CalculationMethod { get; set; }

        [Display(Name = "Метод расчета")]
        public KoCalculationMethod CalculationMethodCode { get; set; }

		[Display(Name = "Свободный член")]
        public decimal? A0 { get; set; }

        public static ManualModelingModel ToModel(OMModel model)
        {
            return new ManualModelingModel
            {
	            GeneralModelId = model.Id,
                GroupId = model.GroupId,
                Name = model.Name,
                Description = model.Description,
                Formula = model.Formula,
                AlgorithmType = model.AlgoritmType,
                AlgorithmTypeCode = model.AlgoritmType_Code,
                A0 = model.A0,
				CalculationType = model.CalculationType,
				CalculationTypeCode = model.CalculationType_Code,
				CalculationMethod = model.CalculationMethod,
				CalculationMethodCode = model.CalculationMethod_Code,
                Type = model.Type_Code.GetEnumDescription()
            };
        }

        public ModelingModelDto ToDto()
        {
	        return new ModelingModelDto
            {
		        ModelId = GeneralModelId,
		        GroupId = GroupId,
		        Name = Name,
		        Description = Description,
		        AlgorithmType = AlgorithmTypeCode,
		        A0 = A0,
		        CalculationType = CalculationTypeCode,
		        CalculationMethod = CalculationMethodCode
            };
        }
    }
}
