using System;
using KadOzenka.Dal.Modeling.Resources;

namespace KadOzenka.Dal.Modeling.Exceptions.Factors
{
	public class EmptyKForFactorException : Exception
	{
		public EmptyKForFactorException() : base(Messages.EmptyKForFactor)
		{

		}
	}
}
