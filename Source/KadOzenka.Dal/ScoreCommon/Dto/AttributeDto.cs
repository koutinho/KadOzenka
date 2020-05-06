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

	public class AttributeDto
	{
		public long Id { get; set; }

		public dynamic Value { get; set; }

		public AttributeDto(PureAttributeDto es)
		{
			Id = es.Id;
			Value = es.Value;
		}

		private DateTime _date;
		public DateTime DateValue
		{
			get
			{
				if (GeTypeEstimatedParameter() == AttributeType.Date &&
				    DateTime.TryParse(Value.ToString(), out _date))
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
				if (GeTypeEstimatedParameter() == AttributeType.Number &&
				    decimal.TryParse(Value.ToString(), out _numberValue))
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
				if (GeTypeEstimatedParameter() == AttributeType.String &&
				    Value is string)
				{
					return Value;
				}

				return "";
			}
		}

		public AttributeType GeTypeEstimatedParameter()
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

	public class PureAttributeDto
	{
		public long Id { get; set; }

		public dynamic Value { get; set; }
	}

}