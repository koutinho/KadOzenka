using System;

namespace KadOzenka.Dal.Modeling.Dictionaries.Exceptions
{
	public class DictionaryAlreadyExistsException : Exception
	{
		public DictionaryAlreadyExistsException(string name) : base($"Словарь '{name}' уже существует")
		{
			
		}
	}
}
