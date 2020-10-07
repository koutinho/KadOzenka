using System;

namespace KadOzenka.WebServices.Exceptions
{
	public class BadRequestException : Exception
	{
		public BadRequestException(string message) : base(message)
		{

		}
	}
}
