using System;
using KadOzenka.Dal.Modeling.Resources;

namespace KadOzenka.Dal.Modeling.Exceptions.Factors
{
	public class WrongFactorTypeException : Exception
	{
		public WrongFactorTypeException() : base(Messages.WrongFactorType)
		{

		}
	}
}
