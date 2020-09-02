using System;
using System.Linq;
using Core.RefLib;
using Core.Register;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using ObjectModel.Core.Shared;

namespace KadOzenka.Dal.Tours
{
	public class UnitFactor
	{
		public long AttributeId { get; }
		public object Value  { get; private set; }

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

		public long? ReferenceId { get; private set; }
		public long? ReferenceItemId { get; private set; }

		private RegisterAttribute _attributeData;
		private RegisterAttribute AttributeData
		{
			get
			{
				if (_attributeData == null) 
					_attributeData = RegisterCache.GetAttributeData((int)AttributeId);

				return _attributeData;
			}
		}

		public UnitFactor(long attributeId)
		{
			AttributeId = attributeId;
		}

		public void SetFactorValue(string val)
		{
			int? referenceItemId = null;
			if (AttributeData.CodeField.IsNotEmpty() && AttributeData.ReferenceId > 0)
			{
				OMReferenceItem item = ReferencesCommon.GetItems(AttributeData.ReferenceId.Value, false)
					.FirstOrDefault(y => y.Value == val);
				if (item != null) referenceItemId = (int)item.ItemId;
			}

			object value = null;
			switch (AttributeData.Type)
			{
				case RegisterAttributeType.INTEGER:
					value = val.ParseToLongNullable();
					break;
				case RegisterAttributeType.DECIMAL:
					value = val.ParseToDecimalNullable();
					break;
				case RegisterAttributeType.BOOLEAN:
					value = val.ParseToBooleanNullable();
					break;
				case RegisterAttributeType.STRING:
					value = val.ParseToStringNullable();
					break;
				case RegisterAttributeType.DATE:
					value = val.ParseToDateTimeNullable();
					break;
			}

			ReferenceId = AttributeData.ReferenceId;
			ReferenceItemId = referenceItemId;
			Value = value;
		}

		public string GetFactorName()
		{
			return AttributeData.Name;
		}

		public string GetValueInString()
		{
			if (LongValue.HasValue)
			{
				return LongValue.ToString();
			}

			if (DecimalValue.HasValue)
			{
				return DecimalValue.ToString();
			}

			if (BoolValue.HasValue)
			{
				return BoolValue.Value ? "Да" : "Нет";
			}

			if (DateTimeValue.HasValue)
			{
				return DateTimeValue.GetString();
			}

			return StringValue;
		}
	}
}
