using System;

namespace KadOzenka.Dal.ScoreCommon.Dto
{
	public enum ParameterType
	{
		None = 0,
		String = 1,
		Number = 2,
		Date = 3
	}

	public class ParameterDto
	{
		public long Id { get; set; }

		public dynamic Value { get; set; }

		public ParameterDto(PureParameterDto es)
		{
			Id = es.Id;
			Value = es.Value;
		}

		private DateTime _date;
		public DateTime DateValue
		{
			get
			{
				if (GeTypeEstimatedParameter() == ParameterType.Date &&
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
				if (GeTypeEstimatedParameter() == ParameterType.Number &&
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
				if (GeTypeEstimatedParameter() == ParameterType.String &&
				    Value is string)
				{
					return Value;
				}

				return "";
			}
		}

		public ParameterType GeTypeEstimatedParameter()
		{
			if (Value is string)
				return ParameterType.String;
			if (Value is decimal)
				return ParameterType.Number;
			if (Value is DateTime)
				return ParameterType.Date;

			return ParameterType.None;
		}
	}

	public class PureParameterDto
	{
		public long Id { get; set; }

		public dynamic Value { get; set; }
	}

}