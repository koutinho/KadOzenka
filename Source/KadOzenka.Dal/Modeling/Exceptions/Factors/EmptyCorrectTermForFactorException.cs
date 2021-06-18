using System;
using KadOzenka.Dal.Modeling.Resources;

namespace KadOzenka.Dal.Modeling.Exceptions.Factors
{
	public class EmptyCorrectTermForFactorException : Exception
	{
		public EmptyCorrectTermForFactorException() : base(Messages.EmptyCorrectTermForFactor)
		{

		}
	}
}
