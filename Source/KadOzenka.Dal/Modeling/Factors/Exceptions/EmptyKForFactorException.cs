using System;

namespace KadOzenka.Dal.Modeling.Factors.Exceptions
{
	public class EmptyKForFactorException : Exception
	{
		public EmptyKForFactorException() : base("Не передан 'К'")
		{

		}
	}
}
