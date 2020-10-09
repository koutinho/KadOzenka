using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Register;
using KadOzenka.Dal.Enum;
using ObjectModel.Directory;

namespace KadOzenka.Dal.ExpressScore.Dto
{
	public class Cell
	{
		public string Key { get; set; }

		public string Value { get; private set; }
		public string CommonValue
		{
			get => Value;
			set => SetValue(value);
		}

		public void SetValue(string value)
		{
			if (Key != null && decimal.TryParse(Key, out var dKey)
			                && RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.Id == (long)dKey)?.Type == RegisterAttributeType.DECIMAL)
			{
				if (value != null && decimal.TryParse(value.Replace('.', ','), out var dVal))
				{
					Value = Math.Round(dVal, 2).ToString("N");
					return;
				}
			}

			Value = value;
		}
	}

	public class Header
	{
		public string DataField { get; set; }

		public string Text { get; set; }

		public int Width { get; set; }
	} 

	public class DataToGrid
	{
		public DataToGrid()
		{
			Headers = new List<Header>();
			Rows =  new List<List<Cell>>();
		}
		public List<Header> Headers { get; set; }

		public List<List<Cell>> Rows { get; set; }
	}
	public class AnalogResultDto : AnalogDto
	{
		public string Address { get; set; }

		public string Source { get; set; }

		public string TypeOfRoom { get; set; }

		public decimal SquarePrice { get; set; }
	}
	public class ResultCalculateDto
	{
		public int Id { get; set; }
		public decimal SummaryCost { get; set; }
		public decimal SquareCost { get; set; }
		public long ReportId { get; set; }
		public List<AnalogResultDto> Analogs { get; set; }
		public DealTypeShort DealType { get; set; }
		public string Address { get; set; }
		public decimal Area { get; set; }
		public MarketSegment MarketSegment { get; set; }

		/// <summary>
		/// Данные для грида 
		/// </summary>
		public string DataToGrid { get; set; }
	}
}