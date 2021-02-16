using System;

namespace KadOzenka.Dal.Modeling.Exceptions
{
	public class ModelNotFoundByIdException : Exception
	{
		public ModelNotFoundByIdException(string message) : base(message)
		{
			
		}
	}
}
