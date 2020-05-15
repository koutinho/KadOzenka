using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Register;
using KadOzenka.Dal.ExpressScore.Dto;
using ObjectModel.Directory;
using ObjectModel.Directory.ES;
using ObjectModel.ES;

namespace KadOzenka.Web.Models.ExpressScore
{
	public class SettingsExpressScoreViewModel : IValidatableObject
	{
		[Required(ErrorMessage = "Выберите тур")]
		public long? TourId { get; set; }

		/// <summary>
		/// ид реестра в которм хранятся данные для оценки
		/// </summary>
		[Required(ErrorMessage = "Выберите реестр")]
		public long? FactorRegisterId { get; set; }

		/// <summary>
		/// Список факторов для экспресс оценки
		/// </summary>
		public CostFactorsDto CostFactors { get; set; }

		/// <summary>
		/// Тип сегмента
		/// </summary>
		[Required(ErrorMessage = "Не передан тип сегмента")]
		public MarketSegment? SegmentType { get; set; }


		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			List<ValidationResult> errors = new List<ValidationResult>();
			if (CostFactors == null || (CostFactors.SimpleCostFactors == null && CostFactors.ComplexCostFactors == null))
			{
				errors.Add(new ValidationResult(errorMessage: "Нет параметров"));
			}

			if (CostFactors.SimpleCostFactors != null && CostFactors.SimpleCostFactors.Count == 0 && CostFactors.ComplexCostFactors !=null && CostFactors.ComplexCostFactors.Count == 0)
			{
				errors.Add(new ValidationResult(errorMessage: "Нет параметров"));
			}

			if (CostFactors.SimpleCostFactors != null && CostFactors.SimpleCostFactors.Count != 0)
			{
				if (CostFactors.SimpleCostFactors.Any(x => x.Name == null || x.Coefficient == null || x.Coefficient == 0))
				{
					errors.Add(new ValidationResult(errorMessage: "Заполните обязательные параметры"));
				}

			}

			if (CostFactors.ComplexCostFactors != null && CostFactors.ComplexCostFactors.Count != 0)
			{
				if (CostFactors.ComplexCostFactors.Any(x => x.Name == null || x.Coefficient == null || x.Coefficient == 0 || x.AttributeId == 0 ||  x.AttributeId == null))
				{
					errors.Add(new ValidationResult(errorMessage: "Заполните обязательные параметры."));
				}

			}

			if (CostFactors != null && CostFactors.YearBuildId == 0 || CostFactors.YearBuildId == null)
			{
				errors.Add(new ValidationResult(errorMessage: "Заполните атрибут года постройки."));
			}

			if (CostFactors != null && CostFactors.IndexDateDicId == 0 || CostFactors.IndexDateDicId == null)
			{
				errors.Add(new ValidationResult(errorMessage: "Выберите справочник для корректировки даты."));
			}

			if (CostFactors != null && CostFactors.LandShareDicId == 0 || CostFactors.LandShareDicId == null)
			{
				errors.Add(new ValidationResult(errorMessage: "Выберите справочник для корректировки земельного участка."));
			}

			if(CostFactors != null && CostFactors.IndexDateDicId != 0 && CostFactors.IndexDateDicId != null)
			{
				var dicType = OMEsReference.Where(x => x.Id == CostFactors.IndexDateDicId).SelectAll()
					.ExecuteFirstOrDefault()?.ValueType_Code;
				if (dicType != ReferenceItemCodeType.Date)
				{
					errors.Add(new ValidationResult(errorMessage: @"Справочник для корректировки даты должен быть типа ""дата""."));

				}
			}

			if (CostFactors != null && CostFactors.LandShareDicId != 0 && CostFactors.LandShareDicId != null)
			{
				var dicType = OMEsReference.Where(x => x.Id == CostFactors.LandShareDicId).SelectAll()
					.ExecuteFirstOrDefault()?.ValueType_Code;
				if (dicType != ReferenceItemCodeType.Number)
				{
					errors.Add(new ValidationResult(errorMessage: @"Справочник для корректировки земельного участка должен быть типа ""число""."));

				}
			}

			if (CostFactors.ComplexCostFactors != null && CostFactors.ComplexCostFactors.Count != 0)
			{
				foreach (var complexFactor in CostFactors.ComplexCostFactors)
				{
					var attributeType = RegisterCache.RegisterAttributes.Values
						.FirstOrDefault(y => y.Id == complexFactor.AttributeId)?.Type;
					if ((attributeType == RegisterAttributeType.STRING ||
					     attributeType == RegisterAttributeType.DATE) &&
					    (complexFactor.DictionaryId == 0 || complexFactor.DictionaryId == null))
					{
						errors.Add(new ValidationResult(errorMessage: $@"Для коэффициента ""{complexFactor.Name}"" необходимо выбрать словарь"));
					}
				}

			}

			if (CostFactors.ComplexCostFactors != null)
			{
				foreach (var complexFactor in CostFactors.ComplexCostFactors)
				{
					if (complexFactor.DictionaryId != 0 && complexFactor.DictionaryId != null)
					{
						var attributeType = RegisterCache.RegisterAttributes.Values
							.FirstOrDefault(y => y.Id == complexFactor.AttributeId)?.Type;
						var dictionaryType = OMEsReference.Where(x => x.Id == complexFactor.DictionaryId).SelectAll().ExecuteFirstOrDefault()
							.ValueType_Code;

						switch (attributeType)
						{
							case RegisterAttributeType.STRING:
							{
								if (dictionaryType != ReferenceItemCodeType.String)
								{
									errors.Add(new ValidationResult($@"Выберите словарь типа ""строка"" для коэффициента {complexFactor.Name}"));
								}
								break;
							}
							case RegisterAttributeType.DATE:
							{
								if (dictionaryType != ReferenceItemCodeType.Date)
								{
									errors.Add(new ValidationResult($@"Выберите словарь типа ""дата"" для коэффициента {complexFactor.Name}"));
								}
								break;
							}
							case RegisterAttributeType.INTEGER :
							{
								if (dictionaryType != ReferenceItemCodeType.Number)
								{
									errors.Add(new ValidationResult($@"Выберите словарь типа ""число"" для коэффициента {complexFactor.Name}"));
								}
								break;
							}
							case RegisterAttributeType.DECIMAL:
							{
								if (dictionaryType != ReferenceItemCodeType.Number)
								{
									errors.Add(new ValidationResult($@"Выберите словарь типа ""число"" для коэффициента {complexFactor.Name}"));
								}
								break;
							}
						}
					}
				}

			}

			return errors;
		}
	}
}