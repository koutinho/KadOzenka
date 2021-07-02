using System;
using KadOzenka.Dal.Tasks.Resources;

namespace KadOzenka.Dal.Tasks.Exceptions
{
	public class InheritanceFactorsTypeMismatchException : Exception
	{
		public InheritanceFactorsTypeMismatchException() 
			: base(Messages.InheritanceFactorsTypeMismatch)
		{
			
		}
	}
}
