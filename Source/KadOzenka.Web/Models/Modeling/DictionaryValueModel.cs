using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Core.SRD;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Directory.ES;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Modeling
{
    public class DictionaryValueModel
    {
        public long Id { get; set; }

        [Display(Name = "Справочник")]
        [Required(ErrorMessage = "Поле 'Справочник' обязательное")]
        public long DictionaryId { get; set; }

        [Display(Name = "Код справочника")]
        public string Value { get; set; }

        [Display(Name = "Значение для расчета")]
        public decimal? CalcValue { get; set; }

        public string DictionaryName { get; set; }

        public ReferenceItemCodeType ValueType { get; set; }

        public decimal? NumberValue { get; set; }

        public DateTime? DateTimeValue { get; set; }

        public bool IsEditItem { get; set; }



        public static DictionaryValueModel ToModel(OMModelingDictionariesValues value, OMModelingDictionary dictionary)
        {
	        var isEmptyValue = string.IsNullOrWhiteSpace(value?.Value);

            var stringValue = dictionary.Type_Code == ReferenceItemCodeType.String && !isEmptyValue
                ? value.Value
                : null;

            var numberValue = dictionary.Type_Code == ReferenceItemCodeType.Number && !isEmptyValue
                ? decimal.TryParse(value.Value, out var number) ? number : (decimal?)null
                : null;

            var dateValue = dictionary.Type_Code == ReferenceItemCodeType.Date && !isEmptyValue
                ? DateTime.TryParse(value.Value, out var date) ? date : (DateTime?)null
                : null;

            return new DictionaryValueModel
            {
                Id = value?.Id ?? -1,
                DictionaryId = value?.DictionaryId ?? dictionary.Id,
                DictionaryName = dictionary.Name,
                ValueType = dictionary.Type_Code,
                Value = stringValue,
                NumberValue = numberValue,
                DateTimeValue = dateValue,
                CalcValue = value?.CalculationValue,
                IsEditItem = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.KO_DICT_MODELS)
            };
        }

        public DictionaryValueDto ToDto()
        {
            var value = GetValue();

            return new DictionaryValueDto
            {
                Id = Id,
                DictionaryId = DictionaryId,
                Value = value,
                CalcValue = CalcValue
            };
        }

        public string GetValue()
        {
	        string value;
            switch (ValueType)
	        {
		        case ReferenceItemCodeType.Date:
			        value = DateTimeValue?.Date.ToString(CultureInfo.CurrentCulture);
			        break;
		        case ReferenceItemCodeType.Number:
			        value = NumberValue?.ToString();
			        break;
		        default:
			        value = Value;
			        break;
	        }

            return value;
        }
    }
}
