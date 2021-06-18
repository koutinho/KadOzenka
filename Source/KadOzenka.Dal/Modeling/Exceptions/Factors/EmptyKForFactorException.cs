using System;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Resources;
using ObjectModel.Directory.Ko;

namespace KadOzenka.Dal.Modeling.Exceptions.Factors
{
	public class EmptyKForFactorException : Exception
	{
		public EmptyKForFactorException(MarkType markType) 
			: base(string.Format(Messages.EmptyKForFactor, markType.GetEnumDescription()))
		{

		}
	}
}
