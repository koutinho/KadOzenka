using System;
using KadOzenka.Dal.Modeling.Resources;

namespace KadOzenka.Dal.Modeling.Factors.Exceptions
{
	public class EmptyKForFactorWithStraightMarkException : Exception
	{
		public EmptyKForFactorWithStraightMarkException() : base(Messages.EmptyKForFactorWithStraightMark)
		{

		}
	}
}
