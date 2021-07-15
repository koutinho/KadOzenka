using System;

namespace KadOzenka.Dal.Modeling.Model.Exceptions
{
	public class ModelNotFoundByIdException : Exception
	{
		public ModelNotFoundByIdException(string message) : base(message)
		{
			
		}
	}
}
