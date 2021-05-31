using System;
using KadOzenka.Dal.Enum;

namespace KadOzenka.Dal.Models.Filters
{
	public class Filters
	{
		public FilteringType Type { get; set; }
		public DateFilter DateFilter { get; set; }
		public StringFilter StringFilter { get; set; }
		public NumberFilter NumberFilter { get; set; }
		public BoolFilter BoolFilter { get; set; }
		public ReferenceFilter ReferenceFilter { get; set; }
	}

	public class DateFilter
	{
		public FilteringTypeDate FilteringType { get; set; }
		public DateTime? Value { get; set; }
		public DateTime? Value2 { get; set; }
	}

	public class StringFilter
	{
		public FilteringTypeString FilteringType { get; set; }
		public string Value { get; set; }

	}

	public class NumberFilter
	{
		public FilteringTypeNumber FilteringType { get; set; }
		public decimal? Value { get; set; }
		public decimal? Value2 { get; set; }
	}

	public class ReferenceFilter
	{
		public FilteringTypeReference FilteringType { get; set; }
		public long? Value { get; set; }
	}

	public class BoolFilter
	{
		public FilteringTypeBool FilteringType { get; set; }
		public bool? Value { get; set; }
	}
}