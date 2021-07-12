using System;
using KadOzenka.Dal.Modeling.Resources;

namespace KadOzenka.Dal.Modeling.Exceptions.Factors
{
	public class EmptyDictionaryForFactorWithDefaultMarkException : Exception
	{
		public EmptyDictionaryForFactorWithDefaultMarkException() : base(Messages.EmptyDictionaryForFactorWithDefaultMark)
		{

		}
	}
}
