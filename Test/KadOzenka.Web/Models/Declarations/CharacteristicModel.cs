using System;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Web.Models.Declarations
{
	public class CharacteristicModel
	{
		public CharacteristicAdditionalInfoModel AdditionalInfo { get; set; }
		public object Value { get; set; }

		public CharacteristicModel()
		{
			AdditionalInfo = new CharacteristicAdditionalInfoModel();
		}

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

		public HarAvailability? HarAvailabilityValue
		{
			get => Value is HarAvailability harAvailability
				? (HarAvailability) harAvailability
				: (HarAvailability?) null;
			set => Value = value;
		}
	}
}
