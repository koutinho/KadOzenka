using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Core.SRD;
using KadOzenka.Dal.Tours.Dto;
using ObjectModel.Directory.ES;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Tour
{
    public class GroupingDictionaryValueModel
    {
        public long Id { get; set; }

        [Display(Name = "Справочник")]
        [Required(ErrorMessage = "Поле 'Справочник' обязательное")]
        public long DictionaryId { get; set; }

        [Display(Name = "Значение")]
        public string Value { get; set; }

        [Display(Name = "Код")]
        public string CodeValue { get; set; }

        public string DictionaryName { get; set; }

        public ReferenceItemCodeType ValueType { get; set; }

        public decimal? NumberValue { get; set; }

        public DateTime? DateTimeValue { get; set; }

        public bool IsEditItem { get; set; }



        public static GroupingDictionaryValueModel ToModel(OMGroupingDictionariesValues value, OMGroupingDictionary dictionary)
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

            return new GroupingDictionaryValueModel
            {
                Id = value?.Id ?? -1,
                DictionaryId = value?.DictionaryId ?? dictionary.Id,
                DictionaryName = dictionary.Name,
                ValueType = dictionary.Type_Code,
                Value = stringValue,
                NumberValue = numberValue,
                DateTimeValue = dateValue,
                CodeValue = value?.GroupingValue,
                IsEditItem = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.KO_DICT_MODELS_DICTIONARIES_VALUES_MODIFICATION)
            };
        }

        public GroupingDictionaryValueDto ToDto()
        {
            var value = GetValue();

            return new GroupingDictionaryValueDto
            {
                Id = Id,
                DictionaryId = DictionaryId,
                Value = value,
                CodeValue = CodeValue
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
