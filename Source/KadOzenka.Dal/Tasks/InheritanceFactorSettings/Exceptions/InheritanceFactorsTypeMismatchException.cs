using System;
using KadOzenka.Dal.Tasks.Resources;

namespace KadOzenka.Dal.Tasks.InheritanceFactorSettings.Exceptions
{
	public class InheritanceFactorsTypeMismatchException : Exception
	{
		public InheritanceFactorsTypeMismatchException() 
			: base(Messages.InheritanceFactorsTypeMismatch)
		{
			
		}
	}
}
