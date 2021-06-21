using System;
using KadOzenka.Dal.Modeling.Resources;

namespace KadOzenka.Dal.Modeling.Exceptions.Factors
{
	public class EmptyKForFactorWithStraightMarkException : Exception
	{
		public EmptyKForFactorWithStraightMarkException() : base(Messages.EmptyKForFactorWithStraightMark)
		{

		}
	}
}
