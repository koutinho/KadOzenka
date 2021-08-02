using System;

namespace ModelingBusiness.Factors.Exceptions.Manual
{
	public class EmptyKForFactorWithStraightMarkException : Exception
	{
		public EmptyKForFactorWithStraightMarkException() : base("'K' не может быть равным нулю для прямой метки (т.к. используется в знаменателе)")
		{

		}
	}
}
