using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Dto.Factors;
using ObjectModel.Directory.ES;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling
{
	public class ModelFactorsService
	{
		public OMModelFactor GetFactorById(long id)
		{
			var factor = OMModelFactor.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
			if (factor == null)
				throw new Exception($"Не найден фактор модели с ИД '{id}'");

			return factor;
		}

		public int AddFactor(ModelFactorDto dto)
		{
			if (dto.FactorId == null)
				throw new Exception("Не передан ИД фактора");

			AddModelAttributes(dto.GeneralModelId, new List<ModelAttributeRelationDto>
			{
				new ModelAttributeRelationDto {AttributeId = dto.FactorId.Value, DictionaryId = dto.DictionaryId}
			});

			return new OMModelFactor
			{
				ModelId = dto.GeneralModelId,
				FactorId = dto.FactorId,
				MarkerId = -1,
				Weight = dto.Weight,
				B0 = dto.Weight,
				SignDiv = dto.SignDiv,
				SignAdd = dto.SignAdd,
				SignMarket = dto.SignMarket,
				DictionaryId = null,
				//TODO CIPJSKO-526 доделать для карточки тура
				TypifiedModelId = null
			}.Save();
		}

		public void UpdateFactor(ModelFactorDto dto)
		{
			var factor = GetFactorById(dto.Id);

			factor.Weight = dto.Weight;
			factor.B0 = dto.B0;
			factor.SignDiv = dto.SignDiv;
			factor.SignAdd = dto.SignAdd;
			factor.SignMarket = dto.SignMarket;

			factor.Save();
		}

		public void AddModelAttributes(long? generalModelId, List<ModelAttributeRelationDto> attributes)
		{
			if (attributes == null || attributes.Count == 0)
				return;

			var model = OMModel.Where(x => x.Id == generalModelId).Select(x => new
			{
				x.ParentGroup.Id,
				x.ParentGroup.GroupName
			}).ExecuteFirstOrDefault();
			if (model == null)
				throw new Exception($"Не найдена модель с ИД '{generalModelId}'");
			if (model.ParentGroup == null)
				throw new Exception($"Для модели '{model.Name}' (ИД='{generalModelId}') не найдена группа");

			var tour = OMTourGroup.Where(x => x.GroupId == model.ParentGroup.Id).Select(x => new
			{
				x.ParentTour.Id, 
				x.ParentTour.Year
			}).ExecuteFirstOrDefault()?.ParentTour;
			if (tour == null)
				throw new Exception($"Не найден тур для группы '{model.ParentGroup?.GroupName}' (ИД='{model.ParentGroup.Id}')");
			//в 2016 почти все атрибуты забиты как строки, поэтому его не валидируем
			if (tour.Year == 2016)
				return;

			ValidateAttributes(attributes, tour.Id);

			attributes.ForEach(x =>
			{
				new OMModelAttribute
				{
					GeneralModelId = generalModelId.GetValueOrDefault(),
					AttributeId = x.AttributeId,
					DictionaryId = x.DictionaryId
				}.Save();
			});
		}

		
		#region Support Methods

		private void ValidateAttributes(List<ModelAttributeRelationDto> attributes, long tourId)
		{
			var errors = new List<string>();

			var attributeIds = attributes.Select(x => x.AttributeId).ToList();
			var dictionaryIds = attributes.Select(x => x.DictionaryId).ToList();

			var omAttributes = RegisterCache.RegisterAttributes.Values.Where(x => attributeIds.Contains(x.Id)).ToList();
			var omDictionaries = OMModelingDictionary.Where(x => dictionaryIds.Contains(x.Id)).Select(x => x.Type_Code).Execute();

			foreach (var modelAttribute in attributes)
			{
				var attribute = omAttributes.FirstOrDefault(y => y.Id == modelAttribute.AttributeId);

				if ((attribute?.Type == RegisterAttributeType.STRING || attribute?.Type == RegisterAttributeType.DATE) &&
				    modelAttribute.DictionaryId.GetValueOrDefault() == 0)
				{
					errors.Add($"Для атрибута '{attribute.Name}' нужно выбрать словарь");
				}
				if (modelAttribute.DictionaryId.GetValueOrDefault() != 0)
				{
					var dictionaryType = omDictionaries.FirstOrDefault(x => x.Id == modelAttribute.DictionaryId)?.Type_Code;
					switch (attribute?.Type)
					{
						case RegisterAttributeType.STRING:
						{
							if (dictionaryType != ReferenceItemCodeType.String)
								errors.Add(GenerateMessage(attribute.Name, ReferenceItemCodeType.String));
							break;
						}
						case RegisterAttributeType.DATE:
						{
							if (dictionaryType != ReferenceItemCodeType.Date)
								errors.Add(GenerateMessage(attribute.Name, ReferenceItemCodeType.Date));
							break;
						}
					}
				}
			}

			if(errors.Count > 0)
				throw new Exception(string.Join("<br>", errors));
		}

		private string GenerateMessage(string attributeName, ReferenceItemCodeType dictionaryType)
		{
			return $"Выберите словарь типа '{dictionaryType.GetEnumDescription()}' для атрибута '{attributeName}'";
		}

		#endregion
	}
}
