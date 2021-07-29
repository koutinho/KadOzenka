using System;

namespace ModelingBusiness.Dictionaries.Exceptions.Dictionary
{
	public class EmptyDictionaryNameException : Exception
	{
		public EmptyDictionaryNameException() : base("Нельзя создать словарь с пустым именем")
		{
			
		}
	}
}
