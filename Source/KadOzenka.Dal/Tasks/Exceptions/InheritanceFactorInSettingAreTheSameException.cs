using System;
using KadOzenka.Dal.Tasks.Resources;

namespace KadOzenka.Dal.Tasks.Exceptions
{
	public class InheritanceFactorInSettingAreTheSameException : Exception
	{
		public InheritanceFactorInSettingAreTheSameException() : base(Messages.InheritanceFactorsAreTheSame)
		{
			
		}
	}
}
