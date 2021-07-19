using System;

namespace ModelingBusiness.Dictionaries.Exceptions
{
	public class EmptyMarkValueException : Exception
	{
		public EmptyMarkValueException(): base("Значение не может быть пустым")
		{
			
		}
	}
}
