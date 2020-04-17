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

        /// <summary>
        /// Код справочника
        /// </summary>
        [Display(Name = "Код справочника")]
        public string Value { get; set; }

        public decimal? NumberValue { get; set; }

        public DateTime? DateTimeValue { get; set; }


        /// <summary>
        /// Тип данных значения справочника
        /// </summary>
        [Display(Name = "Тип данных")]
        [Required(ErrorMessage = "Поле Тип данных значения справочника обязательное")]
        public ReferenceItemCodeType ValueType { get; set; } = ReferenceItemCodeType.String;

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
                };
            }

            var value = entity.ValueType_Code == ReferenceItemCodeType.String && entity.Value != null
                ? entity.Value
                : null;
            var numberValue = entity.ValueType_Code == ReferenceItemCodeType.Number && entity.Value != null
                ? decimal.Parse(entity.Value)
                : (decimal?) null;
            var dateValue = entity.ValueType_Code == ReferenceItemCodeType.Date && entity.Value != null
                ? DateTime.Parse(entity.Value)
                : (DateTime?)null;

            return new ReferenceItemViewModel
            {
                Id = entity.Id,
                ReferenceId = entity.ReferenceId,
                ReferenceName = reference.Name,
                Value = value,
                NumberValue = numberValue,
                DateTimeValue = dateValue,
                ValueType = entity.ValueType_Code,
                CalcValue = entity.CalculationValue
            };
        }

        public ReferenceItemDto ToDto()
        {
            string value = null;
            if (ValueType == ReferenceItemCodeType.Date)
            {
                value = DateTimeValue?.Date.ToString(CultureInfo.CurrentCulture);
            } else if (ValueType == ReferenceItemCodeType.Number)
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
                ValueType = ValueType,
                CalcValue = CalcValue
            };
        }
    }
}
