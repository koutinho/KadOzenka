using System;
using Core.Register;

namespace KadOzenka.Dal.DataImport.Validation
{
	public class CastingToAttributeTypeException : Exception
	{
		public CastingToAttributeTypeException(long? attributeId, object value)
			: base($"'{RegisterCache.GetAttributeData(attributeId.GetValueOrDefault()).Name}' = '{value}'")
		{
		}
	}
}
