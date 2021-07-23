using System;

namespace ModelingBusiness.Dictionaries.Exceptions
{
	public class DictionaryAlreadyExistsException : Exception
	{
		public DictionaryAlreadyExistsException(string name) : base($"Словарь '{name}' уже существует")
		{
			
		}
	}
}
