using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.GbuObject.Dto;
using ModelingBusiness.Factors.Entities;
using ModelingBusiness.Model.Entities;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.Modeling
{
	public class AutomaticModelingModel : GeneralModelingModel
    {
	    public bool IsModelWasTrained { get; private init; }
		public bool HasFormedObjectArray { get; private init; }
        //public bool HasProcessToFormObjectArrayInQueue { get; private set; }
        public bool HasLinearTrainingResult { get; private init; }
		public bool HasExponentialTrainingResult { get; private init; }
		public bool HasMultiplicativeTrainingResult { get; private init; }
		public List<ModelFactorRelationDto> Attributes { get; set; }

        [Display(Name = "Группа")]
        public string GroupName { get; set; }

        [Display(Name = "Алгоритм расчета")]
        public KoAlgoritmType AlgorithmType { get; set; }


        
		public static AutomaticModelingModel ToModel(ModelDto entity, bool hasFormedObjectArray)
		{
			return new AutomaticModelingModel
			{
				Id = entity.ModelId,
				Name = entity.Name,
				Description = entity.Description,
				Type = entity.Type,
				CalculationType = entity.CalculationType,
				TourId = entity.TourId,
				TourYear = entity.TourYear,
				ObjectType = entity.IsOksObjectType ? ObjectTypeExtended.Oks : ObjectTypeExtended.Zu,
				GroupName = entity.GroupName,
				GroupId = entity.GroupId,
				A0 = entity.A0,
				IsActive = entity.IsActive,

				IsModelWasTrained = entity.IsModelWasTrained,
				HasLinearTrainingResult = entity.HasLinearTrainingResult,
				HasExponentialTrainingResult = entity.HasExponentialTrainingResult,
				HasMultiplicativeTrainingResult = entity.HasMultiplicativeTrainingResult,
				HasFormedObjectArray = hasFormedObjectArray,
				//HasProcessToFormObjectArrayInQueue = new LongProcessService().HasActiveProcessInQueue(ObjectFormationForModelingProcess.ProcessId, entity.ModelId),
				AlgorithmTypeForCadastralPriceCalculation = entity.AlgorithmTypeForCadastralPriceCalculation,
				AlgorithmType = entity.AlgorithmTypeForCadastralPriceCalculation,
				Attributes = entity.Attributes
			};
        }

		public override ModelDto ToDto()
        {
	        var dto = base.ToDto();
	        
	        dto.AlgorithmType = AlgorithmType;

	        return dto;
        }
    }
}
