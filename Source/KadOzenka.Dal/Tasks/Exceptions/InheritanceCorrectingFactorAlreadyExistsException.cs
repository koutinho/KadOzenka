using System;
using KadOzenka.Dal.Tasks.Resources;

namespace KadOzenka.Dal.Tasks.Exceptions
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
