using System;
using Core.Register;
using Core.Shared.Extensions;

namespace KadOzenka.Dal.DataImport.Validation
{
	public class CastingToAttributeTypeException : Exception
	{
		public CastingToAttributeTypeException(long? attributeId, object value)
			: base(CreateMessage(attributeId, value))
		{
		}

		private static string CreateMessage(long? attributeId, object value)
		{
			var attribute = RegisterCache.GetAttributeData(attributeId.GetValueOrDefault());
			return $"'{attribute.Name} ({attribute.Type.GetEnumDescription()})' = '{value}'";
		}
	}
}
