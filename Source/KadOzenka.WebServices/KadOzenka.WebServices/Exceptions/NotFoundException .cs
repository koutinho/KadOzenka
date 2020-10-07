using System;

namespace KadOzenka.WebServices.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException(string message) : base(message)
		{

		}
	}
}
