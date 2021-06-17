using System;
using KadOzenka.Dal.Modeling.Resources;

namespace KadOzenka.Dal.Modeling.Exceptions.Factors
{
	public class EmptyMarkTypeForFactor : Exception
	{
		public EmptyMarkTypeForFactor() : base(Messages.EmptyMarkType)
		{
			
		}
	}
}
