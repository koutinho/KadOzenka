using System;

namespace KadOzenka.Dal.Modeling.Exceptions.Dictionaries
{
	public class EmptyMarkValueException : Exception
	{
		public EmptyMarkValueException(): base("Значение не может быть пустым")
		{
			
		}
	}
}
