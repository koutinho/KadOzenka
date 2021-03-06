using System;

namespace ModelingBusiness.Dictionaries.Exceptions
{
	public class TheSameMarkExistsException : Exception
	{
		public TheSameMarkExistsException(string dictionaryName, string value) : base(
			$"Значение '{value}' в справочнике '{dictionaryName}' уже существует.")
		{

		}
	}
}
