using System;

namespace KadOzenka.Dal.ScoreCommon.Dto
{
	public enum AttributeType
	{
		None = 0,
		String = 1,
		Number = 2,
		Date = 3
	}

	public class AttributeDataDto
	{
		public long Id { get; set; }

		public dynamic Value { get; set; }

		public AttributeDataDto(PureAttributeDataDto es)
		{
			Id = es.Id;
			Value = es.Value;
		}

		private DateTime _date;
		public DateTime DateValue
		{
			get
			{
				if (Type == AttributeType.Date && DateTime.TryParse(Value.ToString(), out _date))
				{
					return _date;
				}
				
				return DateTime.MinValue;
			}
		}

		private decimal _numberValue;
		public decimal NumberValue
		{
			get
			{
				if (Type == AttributeType.Number && decimal.TryParse(Value.ToString(), out _numberValue))
				{
					return _numberValue;
				}

				return _numberValue;
			}
		}


		public string StringValue
		{
			get
			{
				if (Type == AttributeType.String && Value is string)
				{
					return Value;
				}

				return "";
			}
		}

		public AttributeType Type
		{
			get
			{
				if (Value is string)
					return AttributeType.String;
				if (Value is decimal)
					return AttributeType.Number;
				if (Value is DateTime)
					return AttributeType.Date;

				return AttributeType.None;
			}
		}
	}

	public class PureAttributeDataDto
	{
		public long Id { get; set; }

		public dynamic Value { get; set; }
	}

}