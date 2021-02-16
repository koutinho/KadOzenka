using System;

namespace KadOzenka.Dal.Tours.Exceptions
{
	public class TourAlreadyExistsException : Exception
	{
		public TourAlreadyExistsException(string message) : base(message)
		{

		}
	}
}
