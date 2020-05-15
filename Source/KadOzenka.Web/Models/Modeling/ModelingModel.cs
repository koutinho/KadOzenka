using System.Collections.Generic;
using ObjectModel.Directory;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Oks;
using ObjectModel.Directory.ES;
using ObjectModel.ES;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Modeling
{
	public class ModelingModel : IValidatableObject
    {
		public long Id { get; set; }
        public bool IsModelWasTrained { get; set; }

        [Display(Name = "Имя")]
		[Required(ErrorMessage = "Не заполнено Имя")]
		public string Name { get; set; }

        [Display(Name = "Тур")]
		[Required(ErrorMessage = "Не выбран Тур")]
		public long TourId { get; set; }

		[Display(Name = "Тур")]
		public long TourYear { get; set; }

		[Display(Name = "Сегмент")]
		[Required(ErrorMessage = "Не выбран Сегмент")]
		public MarketSegment MarketSegmentCode { get; set; }

		[Display(Name = "Сегмент")]
		public string MarketSegment => MarketSegmentCode.GetEnumDescription();

		[Display(Name = "Тип")]
		public ObjectType ObjectType { get; set; }

		public List<ModelAttributeRelationDto> Attributes { get; set; }


		public static ModelingModel ToModel(ModelingModelDto entity)
		{
			return new ModelingModel
			{
				Id = entity.ModelId,
				MarketSegmentCode = entity.MarketSegment,
				TourId = entity.TourId,
				TourYear = entity.TourYear,
				Name = entity.Name,
				Attributes = entity.Attributes,
                IsModelWasTrained = entity.WasTrained,
                ObjectType = entity.IsOksObjectType ? ObjectType.Oks : ObjectType.ZU
            };
		}

		public static ModelingModelDto FromModel(ModelingModel model)
		{
			return new ModelingModelDto
			{
				ModelId =  model.Id,
				Name = model.Name,
				TourId = model.TourId,
				MarketSegment = model.MarketSegmentCode,
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
            var omDictionaries = OMEsReference.Where(x => dictionaryIds.Contains(x.Id)).Select(x => x.Id).Select(x => x.ValueType_Code).Execute();

            foreach (var modelAttribute in Attributes)
            {
                var attribute = omAttributes.FirstOrDefault(y => y.Id == modelAttribute.AttributeId);

                if ((attribute?.Type == RegisterAttributeType.STRING || attribute?.Type == RegisterAttributeType.DATE) &&
                    modelAttribute.DictionaryId.GetValueOrDefault() == 0)
                {
                    errors.Add(new ValidationResult($@"Для аттрибута '{attribute.Name}' нужно выбрать словарь"));
                }
                if (modelAttribute.DictionaryId.GetValueOrDefault() != 0)
                {
                    var dictionaryType = omDictionaries.FirstOrDefault(x => x.Id == modelAttribute.DictionaryId)?.ValueType_Code;
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

        private string GenerateMessage(string attributeName, ReferenceItemCodeType referenceItemCodeType)
        {
            return $"Выберите словарь типа '{referenceItemCodeType.GetEnumDescription()}' для аттрибута '{attributeName}'";
        }
    }
}
