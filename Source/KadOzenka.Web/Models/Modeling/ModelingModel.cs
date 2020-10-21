using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Oks;
using ObjectModel.Directory;
using ObjectModel.Ko;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Modeling
{
	public class ModelingModel
    {
		public long Id { get; set; }
        public bool IsModelWasTrained { get; set; }
        public bool HasLinearTrainingResult { get; set; }
        public bool HasExponentialTrainingResult { get; set; }
        public bool HasMultiplicativeTrainingResult { get; set; }

        [Display(Name = "Имя")]
		[Required(ErrorMessage = "Не заполнено Имя")]
		public string Name { get; set; }

		[Display(Name = "Описание")]
		public string Description { get; set; }

		[Display(Name = "Тип")]
		public KoModelType Type { get; set; }

        [Display(Name = "Алгоритм расчета")]
		public KoAlgoritmType AlgorithmType { get; set; }

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


		public static ModelingModel ToModel(ModelingModelDto entity, List<OMModel> typifiedModels)
		{
			return new ModelingModel
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
                IsModelWasTrained = typifiedModels?.Any(x => !string.IsNullOrWhiteSpace(x.TrainingResult)) ?? false,
                HasLinearTrainingResult = HasTrainingResult(typifiedModels, KoAlgoritmType.Line),
                HasExponentialTrainingResult = HasTrainingResult(typifiedModels, KoAlgoritmType.Exp),
                HasMultiplicativeTrainingResult = HasTrainingResult(typifiedModels, KoAlgoritmType.Multi),
                Type = entity.Type,
                AlgorithmType = entity.AlgorithmType
            };
        }

		public static ModelingModelDto FromModel(ModelingModel model)
        {
            if (model.GroupId == null)
                throw new Exception("Не выбрана Группа");

            return new ModelingModelDto
			{
				ModelId =  model.Id,
				Name = model.Name,
                Description = string.IsNullOrWhiteSpace(model.Description) ? "-" : model.Description,
				TourId = model.TourId,
                GroupId = model.GroupId.Value,
				Attributes = model.Attributes,
                IsOksObjectType = model.ObjectType == ObjectType.Oks,
                Type = model.Type,
                AlgorithmType = model.AlgorithmType
            };
		}


		#region Support Methods

		private static bool HasTrainingResult(List<OMModel> typifiedModels, KoAlgoritmType type)
        {
            return typifiedModels?.Any(x => x.AlgoritmType_Code == type && !string.IsNullOrWhiteSpace(x.TrainingResult)) ?? false;
        }

        #endregion
    }
}
