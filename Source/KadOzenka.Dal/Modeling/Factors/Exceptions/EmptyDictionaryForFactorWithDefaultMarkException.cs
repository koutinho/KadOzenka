using System;
using KadOzenka.Dal.Modeling.Resources;

namespace KadOzenka.Dal.Modeling.Factors.Exceptions
{
	public class EmptyDictionaryForFactorWithDefaultMarkException : Exception
	{
		public EmptyDictionaryForFactorWithDefaultMarkException() : base(Messages.EmptyDictionaryForFactorWithDefaultMark)
		{

		}
	}
}
