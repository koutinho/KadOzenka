using System;

namespace KadOzenka.Dal.Modeling.Model.Exceptions
{
	public class ModelCrudException : Exception
	{
		public ModelCrudException(string message) : base(message)
		{
			
		}
	}
}
