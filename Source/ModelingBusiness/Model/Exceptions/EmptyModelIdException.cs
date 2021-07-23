using System;

namespace ModelingBusiness.Model.Exceptions
{
	public class EmptyModelIdException : Exception
	{
		public EmptyModelIdException(string message) : base(message)
		{
			
		}
	}
}
