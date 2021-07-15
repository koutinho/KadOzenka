using System;
using KadOzenka.Dal.Modeling.Resources;

namespace KadOzenka.Dal.Modeling.Factors.Exceptions
{
	public class EmptyCorrectTermForFactorException : Exception
	{
		public EmptyCorrectTermForFactorException() : base(Messages.EmptyCorrectTermForFactor)
		{

		}
	}
}
