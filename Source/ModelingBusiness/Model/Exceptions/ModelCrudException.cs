using System;

namespace ModelingBusiness.Model.Exceptions
{
	public class ModelCrudException : Exception
	{
		public ModelCrudException(string message) : base(message)
		{
			
		}
	}
}
