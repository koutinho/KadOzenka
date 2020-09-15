using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.RefLib;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.ExpressScore.Dto;
using KadOzenka.Dal.Tours.Dto;
using ObjectModel.Core.Shared;

namespace KadOzenka.Web.Models.ExpressScore
{
	public class AttributePureModel : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
        public RegisterAttributeType Type { get; set; }
        public int? ReferenceId { get; set; }
        public int? ReferenceItemId { get; set; }

        public decimal? DecimalValue
        {
	        get => Value is decimal d
		        ? (decimal)d
		        : (decimal?)null;
	        set => Value = value;
        }

        public long? LongValue
        {
	        get => Value is long d
		        ? (long)d
		        : (long?)null;
	        set => Value = value;
        }

        public bool? BoolValue
        {
	        get => Value is bool d
		        ? (bool)d
		        : (bool?)null;
	        set => Value = value;
        }

        public DateTime? DateTimeValue
        {
	        get => Value is DateTime d
		        ? (DateTime)d
		        : (DateTime?)null;
	        set => Value = value;
        }

        public string StringValue
        {
	        get => Value is string d
		        ? (string)d
		        : null;
	        set => Value = value;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
	        if (ReferenceId.HasValue && !ReferenceItemId.HasValue)
	        {
		        yield return
			        new ValidationResult($"Заполните {Name}");
            }
	        if (!ReferenceId.HasValue && Value == null)
	        {
		        yield return
			        new ValidationResult($"Заполните {Name}");
	        }
        }

        public AttributeValueDto ToDto()
        {
	        if (ReferenceId.HasValue)
	        {
		        Value = ReferencesCommon.GetItems(ReferenceId.Value, false)
			        .FirstOrDefault(y => y.ItemId == ReferenceItemId)?.Value;
            }

            return new AttributeValueDto
            {
	            Id = Id,
                Value = Value
            };
        }
    }

	public class TargetObjectModel
    {
        public long UnitId { get; set; }

        public List<AttributePureModel> Attributes { get; set; }


        public static TargetObjectModel ToModel(TargetObjectDto targetObjectDto)
        {
            var emptyAttributes = targetObjectDto.Attributes.Where(x => string.IsNullOrEmpty(x.Value)).ToList();

            return new TargetObjectModel
            {
                UnitId = targetObjectDto.UnitId,
                Attributes = emptyAttributes.Select(x =>
                {
	                var attributeData = RegisterCache.GetAttributeData(x.Id);
	                int? referenceItemId = null;
                    if (attributeData.CodeField.IsNotEmpty() && attributeData.ReferenceId > 0)
                    {
	                    OMReferenceItem item = ReferencesCommon.GetItems(attributeData.ReferenceId.Value, false)
		                    .FirstOrDefault(y => y.Value == x.Value);
	                    if (item != null) referenceItemId = (int)item.ItemId;
                    }

                    object value = null;
                    switch (attributeData.Type)
                    {
	                    case RegisterAttributeType.INTEGER:
		                    value = x.Value.ParseToLongNullable();
		                    break;
	                    case RegisterAttributeType.DECIMAL:
		                    value = x.Value.ParseToDecimalNullable();
		                    break;
	                    case RegisterAttributeType.BOOLEAN:
		                    value = x.Value.ParseToBooleanNullable();
		                    break;
	                    case RegisterAttributeType.STRING:
		                    value = x.Value.ParseToStringNullable();
		                    break;
	                    case RegisterAttributeType.DATE:
		                    value = x.Value.ParseToDateTimeNullable();
		                    break;
                    }

                    return new AttributePureModel
                    {
                        Id = x.Id,
                        Name = attributeData.Name,
                        Type = attributeData.Type,
                        ReferenceId = attributeData.ReferenceId,
                        ReferenceItemId = referenceItemId,
                        Value = value
                    };
                }).ToList()
            };
        }
    }
}
