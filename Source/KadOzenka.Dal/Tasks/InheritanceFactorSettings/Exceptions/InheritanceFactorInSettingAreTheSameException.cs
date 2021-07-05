using System;
using KadOzenka.Dal.Tasks.Resources;

namespace KadOzenka.Dal.Tasks.InheritanceFactorSettings.Exceptions
{
	public class InheritanceFactorInSettingAreTheSameException : Exception
	{
		public InheritanceFactorInSettingAreTheSameException() : base(Messages.InheritanceFactorsAreTheSame)
		{
			
		}
	}
}
