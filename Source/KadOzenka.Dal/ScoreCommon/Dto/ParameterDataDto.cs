using System;
using Microsoft.CodeAnalysis;

namespace KadOzenka.Dal.ScoreCommon.Dto
{
	public enum ParameterType
	{
		None = 0,
		String = 1,
		Number = 2,
		Date = 3
	}

	public class ParameterDataDto
	{
		public long Id { get; set; }

		public dynamic Value { get; set; }

		public ParameterDataDto(PureParameterDataDto es)
		{
			Id = es.Id;
			Value = es.Value;
		}

		private DateTime _date;

		public DateTime DateValue => _date;

		private decimal _numberValue;
		public decimal NumberValue => _numberValue;


		public string StringValue
		{
			get
			{
				if (Type == ParameterType.String && Value is string)
				{
					return Value;
				}

				return "";
			}
		}

		public ParameterType Type
		{
			get
			{
				if (decimal.TryParse((string)Value.ToString().Replace('.', ','), out _numberValue))
					return ParameterType.Number;
				if (DateTime.TryParse(Value.ToString(), out _date))
					return ParameterType.Date;
				if (Value is string)
					return ParameterType.String;
				return ParameterType.None;
			}
		}
	}

	public class PureParameterDataDto
	{
		public long Id { get; set; }

		public dynamic Value { get; set; }
	}

}