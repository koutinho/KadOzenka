using System;

namespace KadOzenka.Dal.Modeling.Dictionaries.Exceptions
{
	public class EmptyMarkValueException : Exception
	{
		public EmptyMarkValueException(): base("Значение не может быть пустым")
		{
			
		}
	}
}
