using System;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Resources;
using ObjectModel.Directory.Ko;

namespace KadOzenka.Dal.Modeling.Exceptions.Factors
{
	public class EmptyCorrectTermForFactorException : Exception
	{
		public EmptyCorrectTermForFactorException(MarkType markType)
			: base(string.Format(Messages.EmptyCorrectTermForFactor, markType.GetEnumDescription()))
		{

		}
	}
}
