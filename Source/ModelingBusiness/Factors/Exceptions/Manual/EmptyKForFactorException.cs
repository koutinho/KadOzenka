using System;

namespace ModelingBusiness.Factors.Exceptions.Manual
{
	public class EmptyKForFactorException : Exception
	{
		public EmptyKForFactorException() : base("Не передан 'К'")
		{

		}
	}
}
