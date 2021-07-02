using System;
using KadOzenka.Dal.Tasks.Resources;

namespace KadOzenka.Dal.Tasks.InheritanceFactorSettings.Exceptions
{
	public class InheritanceCorrectingFactorAlreadyExistsException : Exception
	{
		public InheritanceCorrectingFactorAlreadyExistsException(string factorName)
			: base(GetMessage(factorName))
		{

		}

		private static string GetMessage(string factorName)
		{
			return string.Format(Messages.InheritanceCorrectingFactorAlreadyExists, factorName);
		}
	}
}
