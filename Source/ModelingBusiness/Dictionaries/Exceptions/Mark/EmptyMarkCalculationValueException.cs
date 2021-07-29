using System;

namespace ModelingBusiness.Dictionaries.Exceptions.Mark
{
	public class EmptyMarkCalculationValueException : Exception
	{
		public EmptyMarkCalculationValueException(): base("Метка не может быть пустой")
		{
			
		}
	}
}
