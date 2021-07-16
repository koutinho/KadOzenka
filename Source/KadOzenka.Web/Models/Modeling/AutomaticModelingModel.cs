﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.LongProcess.Common;
using KadOzenka.Dal.LongProcess.Modeling;
using KadOzenka.Dal.Modeling.Factors.Entities;
using KadOzenka.Dal.Modeling.Model.Entities;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.Modeling
{
	public class AutomaticModelingModel : GeneralModelingModel
    {
	    public bool IsModelWasTrained => HasLinearTrainingResult || HasExponentialTrainingResult || HasMultiplicativeTrainingResult;
        public bool HasFormedObjectArray { get; private set; }
        //public bool HasProcessToFormObjectArrayInQueue { get; private set; }
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

				HasLinearTrainingResult = !string.IsNullOrWhiteSpace(entity.LinearTrainingResult),
				HasExponentialTrainingResult = !string.IsNullOrWhiteSpace(entity.ExponentialTrainingResult),
				HasMultiplicativeTrainingResult = !string.IsNullOrWhiteSpace(entity.MultiplicativeTrainingResult),
				HasFormedObjectArray = hasFormedObjectArray,
				//HasProcessToFormObjectArrayInQueue = new LongProcessService().HasActiveProcessInQueue(ObjectFormationForModelingProcess.ProcessId, entity.ModelId),
				AlgorithmTypeForCadastralPriceCalculation = entity.AlgorithmTypeForCadastralPriceCalculation,
				AlgorithmType = entity.AlgorithmTypeForCadastralPriceCalculation,
				A0ForPreviousTour = entity.A0ForPreviousTour,
				Attributes = entity.Attributes
			};
        }

		public override ModelDto ToDto()
        {
	        var dto = base.ToDto();
	        
	        dto.A0ForPreviousTour = A0ForPreviousTour;
	        dto.AlgorithmType = AlgorithmType;

	        return dto;
        }
    }
}
