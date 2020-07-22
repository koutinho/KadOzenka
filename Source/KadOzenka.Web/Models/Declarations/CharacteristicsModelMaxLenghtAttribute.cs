using System;
using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.Declarations
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class CharacteristicsModelMaxLengthAttribute : ValidationAttribute
	{
		public long MaxLength { get; }

		public CharacteristicsModelMaxLengthAttribute(long maxLength)
		{
			MaxLength = maxLength;
		}

		public override bool IsValid(object value)
		{
			var characteristicsModel = value as CharacteristicModel;
			if (characteristicsModel == null)
			{
				throw new Exception($"Атрибут {nameof(CharacteristicsModelMaxLengthAttribute)} применим только к полям и свойствам типа {nameof(CharacteristicModel)}");
			}

			return string.IsNullOrEmpty(characteristicsModel.StringValue) || 
			       (!string.IsNullOrEmpty(characteristicsModel.StringValue) && characteristicsModel.StringValue?.Length <= MaxLength);
		}
	}
}
