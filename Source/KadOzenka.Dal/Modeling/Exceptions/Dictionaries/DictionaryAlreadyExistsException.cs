using System;

namespace KadOzenka.Dal.Modeling.Exceptions.Dictionaries
{
	public class DictionaryAlreadyExistsException : Exception
	{
		public DictionaryAlreadyExistsException(string name) : base($"Словарь '{name}' уже существует")
		{
			
		}
	}
}
