using System;

namespace ModelingBusiness.Model.Exceptions
{
	public class ModelNotFoundByIdException : Exception
	{
		public ModelNotFoundByIdException(string message) : base(message)
		{
			
		}
	}
}
