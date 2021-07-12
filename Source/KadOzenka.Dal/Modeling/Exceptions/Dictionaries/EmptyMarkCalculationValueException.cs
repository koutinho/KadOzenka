using System;

namespace KadOzenka.Dal.Modeling.Exceptions.Dictionaries
{
	public class EmptyMarkCalculationValueException : Exception
	{
		public EmptyMarkCalculationValueException(): base("Метка не может быть пустой")
		{
			
		}
	}
}
