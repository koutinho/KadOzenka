using System;

namespace ModelingBusiness.Dictionaries.Exceptions.Mark
{
	public class EmptyMarkValueException : Exception
	{
		public EmptyMarkValueException(): base("Значение не может быть пустым")
		{
			
		}
	}
}
