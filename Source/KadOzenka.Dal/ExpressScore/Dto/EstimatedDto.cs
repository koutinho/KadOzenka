using System;

namespace KadOzenka.Dal.ExpressScore.Dto
{
	public enum TypeEstimatedParameter
	{
		None = 0,
		String = 1,
		Number = 2,
		Date = 3
	}

	public class EstimatedDto
	{
		public long Id { get; set; }

		public dynamic Value { get; set; }

		public EstimatedDto(PureEstimatedDto es)
		{
			Id = es.Id;
			Value = es.Value;
		}

		private DateTime _date;
		public DateTime DateValue
		{
			get
			{
				if (GeTypeEstimatedParameter() == TypeEstimatedParameter.Date &&
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
				if (GeTypeEstimatedParameter() == TypeEstimatedParameter.Number &&
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
				if (GeTypeEstimatedParameter() == TypeEstimatedParameter.String &&
				    Value is string)
				{
					return Value;
				}

				return "";
			}
		}

		public TypeEstimatedParameter GeTypeEstimatedParameter()
		{
			if (Value is string)
				return TypeEstimatedParameter.String;
			if (Value is decimal)
				return TypeEstimatedParameter.Number;
			if (Value is DateTime)
				return TypeEstimatedParameter.Date;

			return TypeEstimatedParameter.None;
		}
	}

	public class PureEstimatedDto
	{
		public long Id { get; set; }

		public dynamic Value { get; set; }
	}

}