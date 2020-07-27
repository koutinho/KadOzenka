using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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

        public static ReferenceItemViewModel ToModel(OMEsReferenceItem entity, OMEsReference reference)
        {
            if (entity == null)
            {
                return new ReferenceItemViewModel
                {
                    Id = -1,
                    ReferenceId = reference.Id,
                    ReferenceName = reference.Name,
                    ReferenceValueType = reference.ValueType_Code
                };
            }

            var value = reference.ValueType_Code == ReferenceItemCodeType.String && entity.Value != null
                ? entity.Value
                : null;
            var numberValue = reference.ValueType_Code == ReferenceItemCodeType.Number && entity.Value != null
                ? decimal.Parse(entity.Value)
                : (decimal?) null;
            var dateValue = reference.ValueType_Code == ReferenceItemCodeType.Date && entity.Value != null
                ? DateTime.Parse(entity.Value)
                : (DateTime?)null;

            return new ReferenceItemViewModel
            {
                Id = entity.Id,
                ReferenceId = entity.ReferenceId,
                ReferenceName = reference.Name,
                ReferenceValueType = reference.ValueType_Code,
                Value = value,
                NumberValue = numberValue,
                DateTimeValue = dateValue,
                CalcValue = entity.CalculationValue
            };
        }

        public ReferenceItemDto ToDto()
        {
            string value = null;
            if (ReferenceValueType == ReferenceItemCodeType.Date)
            {
                value = DateTimeValue?.Date.ToString(CultureInfo.CurrentCulture);
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
                CalcValue = CalcValue
            };
        }
    }
}
