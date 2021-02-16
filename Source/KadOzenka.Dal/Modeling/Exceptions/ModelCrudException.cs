using System;

namespace KadOzenka.Dal.Modeling.Exceptions
{
	public class ModelCrudException : Exception
	{
		public ModelCrudException(string message) : base(message)
		{
			
		}
	}
}
