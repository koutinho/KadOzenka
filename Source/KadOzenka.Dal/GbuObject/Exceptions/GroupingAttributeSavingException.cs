using System;

namespace KadOzenka.Dal.GbuObject.Exceptions
{
	public class GroupingAttributeSavingException : Exception
	{
		public GroupingAttributeSavingException(string message, Exception inException) : base(message, inException)
		{
			
		}
	}
}
