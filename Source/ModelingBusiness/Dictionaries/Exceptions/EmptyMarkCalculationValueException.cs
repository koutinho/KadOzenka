using System;

namespace ModelingBusiness.Dictionaries.Exceptions
{
	public class EmptyMarkCalculationValueException : Exception
	{
		public EmptyMarkCalculationValueException(): base("Метка не может быть пустой")
		{
			
		}
	}
}
