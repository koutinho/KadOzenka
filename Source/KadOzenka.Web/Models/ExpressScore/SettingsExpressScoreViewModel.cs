using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Register;
using Core.Shared.Extensions;
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

		public bool IsEdit { get; set; }

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

			ValidateLandShareDic(errors);
			ValidateIndexDate(errors);
			ValidateVat(errors);
            ValidateSquareCostFactor(errors);
            ValidateCorrectionByBargainCoef(errors);
            ValidateOperatingCostsCoef(errors);
            ValidateShowInCalculatePageParameter(errors);
            ValidateDefaultComplexCostFactorValue(errors);


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
                        var dictionaryType = GetDictionaryType(complexFactor.DictionaryId);

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

        #region Support Methods

        private void ValidateVat(List<ValidationResult> errors)
        {
            if (CostFactors == null || !CostFactors.IsVatIncluded.GetValueOrDefault())
                return;

            var varDictionaryId = CostFactors.VatDictionaryId.GetValueOrDefault();
            if (varDictionaryId == 0)
            {
                errors.Add(new ValidationResult("Для учета НДС нужно выбрать справочник."));
            }
            else
            {
                var dicType = GetDictionaryType(varDictionaryId);
                if (dicType != ReferenceItemCodeType.String)
                {
                    errors.Add(new ValidationResult(@"Справочник для корректировки НДС должен быть типа ""строка""."));
                }
            }
        }

        private void ValidateSquareCostFactor(List<ValidationResult> errors)
        {
	        if (CostFactors.IsSquareFactorUsedInCalculations.GetValueOrDefault())
            {
	            if (CostFactors.ComplexCostFactors == null ||
	                CostFactors.ComplexCostFactors != null && CostFactors.ComplexCostFactors.All(x =>
		                x.ComplexCostFactorType != ComplexCostFactorSpecialization.SquareFactor))
	            {
		            errors.Add(new ValidationResult("Не создан фактор площади."));
	            }
            }
        }

        private void ValidateCorrectionByBargainCoef(List<ValidationResult> errors)
        {
	        if (CostFactors.IsCorrectionByBargainUsedInCalculations.GetValueOrDefault() && !CostFactors.CorrectionByBargainCoef.HasValue)
	        {
				errors.Add(new ValidationResult("Не указан коэффициент корректировки на торг."));
			}
        }

        private void ValidateOperatingCostsCoef(List<ValidationResult> errors)
        {
	        if (CostFactors.IsOperatingCostsUsedInCalculations.GetValueOrDefault() && !CostFactors.OperatingCostsCoef.HasValue)
	        {
		        errors.Add(new ValidationResult("Не указан коэффициент операционных расходов."));
	        }
        }

        private void ValidateShowInCalculatePageParameter(List<ValidationResult> errors)
        {
	        if (CostFactors.ComplexCostFactors != null && CostFactors.ComplexCostFactors.Count > 0)
	        {
		        foreach (var complexCostFactor in CostFactors.ComplexCostFactors)
		        {
			        if (complexCostFactor.ShowInCalculatePage && complexCostFactor.DictionaryId == 0 ||
			            complexCostFactor.DictionaryId == null)
			        {
						errors.Add(new ValidationResult($@"Для оценочного фактора ""{complexCostFactor.Name}"" необходимо заполнить словарь. Т.к выбрана возможность изменения параметра на странице расчетов."));
			        }
		        }
	        }

        }

        private void ValidateDefaultComplexCostFactorValue(List<ValidationResult> errors)
        {
	        if (CostFactors.ComplexCostFactors != null && CostFactors.ComplexCostFactors.Count > 0)
	        {
		        foreach (var complexCostFactor in CostFactors.ComplexCostFactors)
		        {
			        if (complexCostFactor.ShowInCalculatePage && complexCostFactor.DefaultValue.IsNullOrEmpty())
			        {
				        errors.Add(new ValidationResult($@"Для оценочного фактора ""{complexCostFactor.Name}"" необходимо заполнить ""Значение по умолчанию"". Т.к выбрана возможность изменения параметра на странице расчетов."));
			        }
		        }
	        }
        }

        private void ValidateIndexDate(List<ValidationResult> errors)
        {
	        if (CostFactors != null && (CostFactors.IndexDateDicId == 0 || CostFactors.IndexDateDicId == null))
	        {
		        errors.Add(new ValidationResult(errorMessage: "Выберите справочник для корректировки даты."));
	        } else
	        {
		      var reference =  OMEsReference.Where(x => x.Id == CostFactors.IndexDateDicId).Select(x => x.UseInterval)
			        .ExecuteFirstOrDefault();

		      if (reference != null && reference.UseInterval.GetValueOrDefault())
		      {
			      errors.Add(new ValidationResult(errorMessage: "Cправочник для корректировки даты не может быть интервальным"));
				}
	        }

	        if (CostFactors != null && CostFactors.IndexDateDicId != 0 && CostFactors.IndexDateDicId != null)
	        {
		        var dicType = GetDictionaryType(CostFactors.IndexDateDicId);
		        if (dicType != ReferenceItemCodeType.Date)
		        {
			        errors.Add(new ValidationResult(errorMessage: @"Справочник для корректировки даты должен быть типа ""дата""."));
		        }
	        }
		}

        private void ValidateLandShareDic(List<ValidationResult> errors)
        {
	        if (CostFactors != null && (CostFactors.LandShareDicId == 0 || CostFactors.LandShareDicId == null))
	        {
		        errors.Add(new ValidationResult(errorMessage: "Выберите справочник для корректировки земельного участка."));
	        }else
	        {
		        var reference = OMEsReference.Where(x => x.Id == CostFactors.LandShareDicId).Select(x => x.UseInterval)
			        .ExecuteFirstOrDefault();

		        if (reference != null && reference.UseInterval.GetValueOrDefault())
		        {
			        errors.Add(new ValidationResult(errorMessage: "Cправочник для корректировки корректировки земельного участка не может быть интервальным"));
		        }
	        }


	        if (CostFactors != null && CostFactors.LandShareDicId != 0 && CostFactors.LandShareDicId != null)
	        {
		        var dicType = GetDictionaryType(CostFactors.LandShareDicId);
		        if (dicType != ReferenceItemCodeType.Number)
		        {
			        errors.Add(new ValidationResult(errorMessage: @"Справочник для корректировки земельного участка должен быть типа ""число""."));
		        }
	        }
		}

		private ReferenceItemCodeType? GetDictionaryType(decimal? dictionaryId)
        {
            return OMEsReference.Where(x => x.Id == dictionaryId)
                .Select(x => x.ValueType_Code)
                .ExecuteFirstOrDefault()
                ?.ValueType_Code;
        }

        #endregion
    }
}