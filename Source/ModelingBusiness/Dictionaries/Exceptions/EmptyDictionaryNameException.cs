using System;

namespace ModelingBusiness.Dictionaries.Exceptions
{
	public class EmptyDictionaryNameException : Exception
	{
		public EmptyDictionaryNameException() : base("Нельзя создать словарь с пустым именем")
		{
			
		}
	}
}
