using System;

namespace ModelingBusiness.Factors.Exceptions
{
	public class EmptyKForFactorException : Exception
	{
		public EmptyKForFactorException() : base("Не передан 'К'")
		{

		}
	}
}
