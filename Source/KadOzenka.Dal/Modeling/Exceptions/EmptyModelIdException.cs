using System;

namespace KadOzenka.Dal.Modeling.Exceptions
{
	public class EmptyModelIdException : Exception
	{
		public EmptyModelIdException(string message) : base(message)
		{
			
		}
	}
}
