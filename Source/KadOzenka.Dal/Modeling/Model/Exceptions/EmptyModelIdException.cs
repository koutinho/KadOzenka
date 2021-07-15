using System;

namespace KadOzenka.Dal.Modeling.Model.Exceptions
{
	public class EmptyModelIdException : Exception
	{
		public EmptyModelIdException(string message) : base(message)
		{
			
		}
	}
}
