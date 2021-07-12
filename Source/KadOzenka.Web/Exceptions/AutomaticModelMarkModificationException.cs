using System;
using KadOzenka.Web.Resources;

namespace KadOzenka.Web.Exceptions
{
	public class AutomaticModelMarkModificationException : Exception
	{
		public AutomaticModelMarkModificationException() : base(WebMessages.AutomaticModelMarkModification)
		{
			
		}
	}
}
