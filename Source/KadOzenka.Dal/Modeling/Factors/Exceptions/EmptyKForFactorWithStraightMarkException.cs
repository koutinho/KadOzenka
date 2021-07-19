using System;

namespace KadOzenka.Dal.Modeling.Factors.Exceptions
{
	public class EmptyKForFactorWithStraightMarkException : Exception
	{
		public EmptyKForFactorWithStraightMarkException() : base("'K' не может быть равным нулю для прямой метки (т.к. используется в знаменателе)")
		{

		}
	}
}
