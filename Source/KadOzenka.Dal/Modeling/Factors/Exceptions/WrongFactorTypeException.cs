using System;
using KadOzenka.Dal.Modeling.Resources;

namespace KadOzenka.Dal.Modeling.Factors.Exceptions
{
	public class WrongFactorTypeException : Exception
	{
		public WrongFactorTypeException() : base(Messages.WrongFactorType)
		{

		}
	}
}
