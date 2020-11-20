using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Oks;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Directory.Core.LongProcess;

namespace KadOzenka.Web.Models.Modeling
{
	public class AutomaticModelingModel : GeneralModelingModel
    {
	    public bool IsModelWasTrained => HasLinearTrainingResult || HasExponentialTrainingResult || HasMultiplicativeTrainingResult;
        public bool HasFormedObjectArray { get; private set; }
        public bool HasProcessToFormObjectArrayInQueue { get; private set; }
        public bool HasLinearTrainingResult { get; set; }
        public bool HasExponentialTrainingResult { get; set; }
        public bool HasMultiplicativeTrainingResult { get; set; }
        public List<ModelAttributeRelationDto> Attributes { get; set; }


        [Display(Name = "Группа")]
        public string GroupName { get; set; }

        [Display(Name = "Алгоритм расчета")]
        public KoAlgoritmType AlgorithmType { get; set; }

        [Display(Name = "Свободный член из предыдущего тура")]
		public decimal? A0ForPreviousTour { get; set; }

		

		public static AutomaticModelingModel ToModel(ModelingModelDto entity, bool hasFormedObjectArray)
		{
			return new AutomaticModelingModel
			{
				Id = entity.ModelId,
				Name = entity.Name,
				Description = entity.Description,
				Type = entity.Type,
				AlgorithmType = entity.AlgorithmTypeForCadastralPriceCalculation,
				CalculationType = entity.CalculationType,
				TourId = entity.TourId,
				TourYear = entity.TourYear,
				ObjectType = entity.IsOksObjectType ? ObjectTypeExtended.Oks : ObjectTypeExtended.Zu,
				GroupName = entity.GroupName,
				GroupId = entity.GroupId,
				A0 = entity.A0,

				HasLinearTrainingResult = !string.IsNullOrWhiteSpace(entity.LinearTrainingResult),
				HasExponentialTrainingResult = !string.IsNullOrWhiteSpace(entity.ExponentialTrainingResult),
				HasMultiplicativeTrainingResult = !string.IsNullOrWhiteSpace(entity.MultiplicativeTrainingResult),
				HasFormedObjectArray = hasFormedObjectArray,
				HasProcessToFormObjectArrayInQueue = CheckProcessToFormObjectArrayExistsInQueue(entity.ModelId),
				AlgorithmTypeForCadastralPriceCalculation = entity.AlgorithmTypeForCadastralPriceCalculation,
				A0ForPreviousTour = entity.A0ForPreviousTour,
				Attributes = entity.Attributes
			};
        }

		public override ModelingModelDto ToDto()
        {
	        var dto = base.ToDto();
	        
	        dto.A0ForPreviousTour = A0ForPreviousTour;

	        return dto;
        }

		public static bool CheckProcessToFormObjectArrayExistsInQueue(long? modelId)
		{
			var possibleStatuses = new List<Status> { Status.Running, Status.Added, Status.PrepareToRun };

			return OMQueue.Where(x =>
					x.ProcessTypeId == ObjectFormationForModelingProcess.ProcessId &&
					possibleStatuses.Contains(x.Status_Code) && x.ObjectId == modelId)
				.ExecuteExists();
		}
    }
}
