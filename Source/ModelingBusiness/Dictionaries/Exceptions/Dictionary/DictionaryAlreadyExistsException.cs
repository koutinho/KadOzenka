using System;

namespace ModelingBusiness.Dictionaries.Exceptions.Dictionary
{
	public class DictionaryAlreadyExistsException : Exception
	{
		public DictionaryAlreadyExistsException(string name) : base($"Словарь '{name}' уже существует")
		{
			
		}
	}
}
