using System;

namespace KadOzenka.Dal.Modeling.Dictionaries.Exceptions
{
	public class TheSameMarkExistsException : Exception
	{
		public TheSameMarkExistsException(string dictionaryName, string value) : base(
			$"Значение '{value}' в справочнике '{dictionaryName}' уже существует.")
		{

		}
	}
}
