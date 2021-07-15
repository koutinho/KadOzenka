using System;

namespace KadOzenka.Dal.Modeling.Dictionaries.Exceptions
{
	public class EmptyMarkCalculationValueException : Exception
	{
		public EmptyMarkCalculationValueException(): base("Метка не может быть пустой")
		{
			
		}
	}
}
