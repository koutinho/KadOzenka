using System;
using KadOzenka.Dal.Modeling.Resources;

namespace KadOzenka.Dal.Modeling.Factors.Exceptions
{
	public class EmptyKForFactorException : Exception
	{
		public EmptyKForFactorException() : base(Messages.EmptyKForFactor)
		{

		}
	}
}
