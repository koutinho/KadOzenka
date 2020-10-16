using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Oks;
using ObjectModel.Directory;
using ObjectModel.Directory.ES;
using ObjectModel.Ko;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Modeling
{
	public class ModelingModel : IValidatableObject
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
		public KoAlgoritmType Type { get; set; }

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


		public static ModelingModel ToModel(ModelingModelDto entity, List<OMModelTypified> typifiedModels)
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
                HasMultiplicativeTrainingResult = HasTrainingResult(typifiedModels, KoAlgoritmType.Multi)
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
                IsOksObjectType = model.ObjectType == ObjectType.Oks
			};
		}

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            //в 2016 почти все атрибуты забиты как строки, поэтому его не валидируем
            var tour = OMTour.Where(x => x.Id == TourId).Select(x => x.Year).ExecuteFirstOrDefault();
            if (tour != null && tour.Year == 2016)
                return errors;

            if (Attributes == null || Attributes.Count == 0)
                return errors;

            var attributeIds = Attributes.Select(x => x.AttributeId);
            var dictionaryIds = Attributes.Select(x => x.DictionaryId);
            var omAttributes = RegisterCache.RegisterAttributes.Values.Where(x => attributeIds.Contains(x.Id)).ToList();
            var omDictionaries = OMModelingDictionary.Where(x => dictionaryIds.Contains(x.Id)).Select(x => x.Type_Code).Execute();

            foreach (var modelAttribute in Attributes)
            {
                var attribute = omAttributes.FirstOrDefault(y => y.Id == modelAttribute.AttributeId);

                if ((attribute?.Type == RegisterAttributeType.STRING || attribute?.Type == RegisterAttributeType.DATE) &&
                    modelAttribute.DictionaryId.GetValueOrDefault() == 0)
                {
                    errors.Add(new ValidationResult($@"Для атрибута '{attribute.Name}' нужно выбрать словарь"));
                }
                if (modelAttribute.DictionaryId.GetValueOrDefault() != 0)
                {
                    var dictionaryType = omDictionaries.FirstOrDefault(x => x.Id == modelAttribute.DictionaryId)?.Type_Code;
                    switch (attribute?.Type)
                    {
                        case RegisterAttributeType.STRING:
                        {
                            if (dictionaryType != ReferenceItemCodeType.String)
                                errors.Add(new ValidationResult(GenerateMessage(attribute.Name, ReferenceItemCodeType.String)));
                            break;
                        }
                        case RegisterAttributeType.DATE:
                        {
                            if (dictionaryType != ReferenceItemCodeType.Date)
                                errors.Add(new ValidationResult(GenerateMessage(attribute.Name, ReferenceItemCodeType.Date)));
                            break;
                        }
                    }
                }
            }

            return errors;
        }


        #region Support Methods

        private string GenerateMessage(string attributeName, ReferenceItemCodeType dictionaryType)
        {
            return $"Выберите словарь типа '{dictionaryType.GetEnumDescription()}' для атрибута '{attributeName}'";
        }

        private static bool HasTrainingResult(List<OMModelTypified> typifiedModels, KoAlgoritmType type)
        {
            return typifiedModels?.Any(x => x.AlgoritmType_Code == type && !string.IsNullOrWhiteSpace(x.TrainingResult)) ?? false;
        }

        #endregion
    }
}
