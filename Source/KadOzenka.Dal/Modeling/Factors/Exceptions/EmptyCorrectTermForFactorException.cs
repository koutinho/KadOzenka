using System;

namespace KadOzenka.Dal.Modeling.Factors.Exceptions
{
	public class EmptyCorrectTermForFactorException : Exception
	{
		public EmptyCorrectTermForFactorException() : base("Не передано 'Корректирующее слагаемое'")
		{

		}
	}
}
