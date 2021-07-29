using System;
using Core.Register;
using Core.Shared.Extensions;

namespace ModelingBusiness.Dictionaries.Exceptions
{
	public class UnsupportedDictionaryTypeException : Exception
	{
		public UnsupportedDictionaryTypeException(RegisterAttributeType type)
			: base($"Для фактора с типом '{type.GetEnumDescription()}' нельзя создать словарь меток")
		{

		}
	}
}
