﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Oks;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Directory.Core.LongProcess;

namespace KadOzenka.Web.Models.Modeling
{
	public class AutomaticModelingModel
    {
		public long Id { get; set; }
        public bool IsModelWasTrained => HasLinearTrainingResult || HasExponentialTrainingResult || HasMultiplicativeTrainingResult;
        public bool HasFormedObjectArray { get; private set; }
        public bool HasProcessToFormObjectArrayInQueue { get; private set; }
        public bool HasLinearTrainingResult { get; set; }
        public bool HasExponentialTrainingResult { get; set; }
        public bool HasMultiplicativeTrainingResult { get; set; }

        [Display(Name = "Имя")]
		[Required(ErrorMessage = "Не заполнено Имя")]
		public string Name { get; set; }

		[Display(Name = "Описание")]
		public string Description { get; set; }

		[Display(Name = "Тип")]
		public string TypeStr { get; set; }

		[Display(Name = "Тип")]
		public KoModelType Type { get; set; }

		[Display(Name = "Алгоритм расчета")]
		public KoAlgoritmType AlgorithmType { get; set; }

		[Display(Name = "Алгоритм для рассчета Кадастровой стоимости")]
		[Required(ErrorMessage = "Не выбран Алгоритм для рассчета Кадастровой стоимости")]
        public KoAlgoritmType AlgorithmTypeForCadastralPriceCalculation { get; set; }

        [Display(Name = "Тип расчета")]
		public string CalculationType { get; set; }

		[Display(Name = "Свободный член")]
		public decimal? A0 { get; set; }

		[Display(Name = "Свободный член из предыдущего тура")]
		public decimal? A0ForPreviousTour { get; set; }

        [Display(Name = "Тур")]
		[Required(ErrorMessage = "Не выбран Тур")]
		public long TourId { get; set; }

		[Display(Name = "Тур")]
		public long TourYear { get; set; }

		[Display(Name = "Группа")]
		[Required(ErrorMessage = "Не выбрана Группа")]
		public long? GroupId { get; set; }

        [Display(Name = "Группа")]
        public string GroupName { get; set; }

        [Display(Name = "Тип")]
		public ObjectType ObjectType { get; set; }

		public List<ModelAttributeRelationDto> Attributes { get; set; }

		public static AutomaticModelingModel ToModel(ModelingModelDto entity, bool hasFormedObjectArray)
		{
			return new AutomaticModelingModel
            {
                Id = entity.ModelId,
                GroupId = entity.GroupId,
                GroupName = entity.GroupName,
                TourId = entity.TourId,
                TourYear = entity.TourYear,
                Name = entity.Name,
                Description = entity.Description,
                Attributes = entity.Attributes,
                ObjectType = entity.IsOksObjectType ? ObjectType.Oks : ObjectType.ZU,
                HasLinearTrainingResult = !string.IsNullOrWhiteSpace(entity.LinearTrainingResult),
                HasExponentialTrainingResult = !string.IsNullOrWhiteSpace(entity.ExponentialTrainingResult),
                HasMultiplicativeTrainingResult = !string.IsNullOrWhiteSpace(entity.MultiplicativeTrainingResult),
                HasFormedObjectArray = hasFormedObjectArray,
                HasProcessToFormObjectArrayInQueue = CheckProcessToFormObjectArrayExistsInQueue(entity.ModelId),
                TypeStr = entity.Type.GetEnumDescription(),
                AlgorithmType = entity.AlgorithmType,
                AlgorithmTypeForCadastralPriceCalculation = entity.AlgorithmType,
                CalculationType = entity.CalculationType.GetEnumDescription(),
                A0 = entity.A0,
                A0ForPreviousTour = entity.A0ForPreviousTour
			};
        }

		public static ModelingModelDto FromModel(AutomaticModelingModel model)
        {
	        return new ModelingModelDto
			{
				ModelId =  model.Id,
				Name = model.Name,
                Description = string.IsNullOrWhiteSpace(model.Description) ? "-" : model.Description,
				TourId = model.TourId,
                GroupId = model.GroupId,
                IsOksObjectType = model.ObjectType == ObjectType.Oks,
                AlgorithmType = model.AlgorithmType,
                AlgorithmTypeForCadastralPriceCalculation = model.AlgorithmTypeForCadastralPriceCalculation,
                A0 = model.A0,
				A0ForPreviousTour = model.A0ForPreviousTour,
                Type = model.Type
            };
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