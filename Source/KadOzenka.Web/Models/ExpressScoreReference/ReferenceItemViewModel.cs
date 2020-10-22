using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Core.SRD;
using KadOzenka.Dal.ExpressScore.Dto;
using ObjectModel.Directory.ES;
using ObjectModel.ES;

namespace KadOzenka.Web.Models.ExpressScoreReference
{
    public class ReferenceItemViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Справочник")]
        [Required(ErrorMessage = "Поле Справочник обязательное")]
        public long ReferenceId { get; set; }

        public string ReferenceName { get; set; }
        public ReferenceItemCodeType ReferenceValueType { get; set; }

        /// <summary>
        /// Общий код справочника
        /// </summary>
        [Display(Name = "Общий код справочника")]
        public string CommonValue { get; set; }

        /// <summary>
        /// Код справочника
        /// </summary>
        [Display(Name = "Код справочника")]
        public string Value { get; set; }

        public decimal? NumberValue { get; set; }

        public DateTime? DateTimeValue { get; set; }

        /// <summary>
        /// Значение для расчета
        /// </summary>
        [Display(Name = "Значение для расчета")]
        public decimal? CalcValue { get; set; }

        public bool IsEditItem { get; set; }

        public static ReferenceItemViewModel ToModel(OMEsReferenceItem referenceItem, OMEsReference reference)
        {
            if (referenceItem == null)
            {
                return new ReferenceItemViewModel
                {
                    Id = -1,
                    ReferenceId = reference.Id,
                    ReferenceName = reference.Name,
                    ReferenceValueType = reference.ValueType_Code,
                    IsEditItem = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRESSSCORE_REFERENCES_EDIT)
            };
            }

            var res = new ReferenceItemViewModel
            {
	            Id = referenceItem.Id,
	            ReferenceId = referenceItem.ReferenceId,
	            ReferenceName = reference.Name,
	            ReferenceValueType = reference.ValueType_Code,
	            CalcValue = referenceItem.CalculationValue,
                CommonValue = referenceItem.CommonValue,
	            IsEditItem = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRESSSCORE_REFERENCES_EDIT)
            };

            switch (reference.ValueType_Code)
            {
                case ReferenceItemCodeType.String:
	                res.Value = referenceItem.Value;
	                break;
                case ReferenceItemCodeType.Number:
	                res.NumberValue = decimal.TryParse(referenceItem.Value, out var dVal) ? dVal : (decimal?) null;
	                break;
                case ReferenceItemCodeType.Date:
	                res.DateTimeValue = DateTime.TryParse(referenceItem.Value, out var date) ? date : (DateTime?) null;
                    break;
            }

            return res;
        }

        public ReferenceItemDto ToDto()
        {
            string value;

            if (ReferenceValueType == ReferenceItemCodeType.Date)
            {
	            value = DateTimeValue?.Date.ToShortDateString();
            } else if (ReferenceValueType == ReferenceItemCodeType.Number)
            {
                value = NumberValue?.ToString();
            }
            else
            {
                value = Value;
            }

            return new ReferenceItemDto
            {
                Id = Id,
                ReferenceId = ReferenceId,
                Value = value,
                CommonValue = CommonValue,
                CalcValue = CalcValue
            };
        }
    }
}
