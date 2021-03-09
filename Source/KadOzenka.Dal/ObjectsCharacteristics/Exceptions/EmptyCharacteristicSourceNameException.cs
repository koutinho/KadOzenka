using System;
using KadOzenka.Dal.ObjectsCharacteristics.Resources;

namespace KadOzenka.Dal.ObjectsCharacteristics.Exceptions
{
	public class EmptyCharacteristicSourceNameException : Exception
	{
		public EmptyCharacteristicSourceNameException() : base(Messages.EmptyCharacteristicSourceName)
		{
		}
	}
}
