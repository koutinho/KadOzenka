using System;

namespace ModelingBusiness.Factors.Exceptions.Manual
{
	public class EmptyCorrectTermForFactorException : Exception
	{
		public EmptyCorrectTermForFactorException() : base("Не передано 'Корректирующее слагаемое'")
		{

		}
	}
}
