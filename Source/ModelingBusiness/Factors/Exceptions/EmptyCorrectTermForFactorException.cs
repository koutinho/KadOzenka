using System;

namespace ModelingBusiness.Factors.Exceptions
{
	public class EmptyCorrectTermForFactorException : Exception
	{
		public EmptyCorrectTermForFactorException() : base("Не передано 'Корректирующее слагаемое'")
		{

		}
	}
}
