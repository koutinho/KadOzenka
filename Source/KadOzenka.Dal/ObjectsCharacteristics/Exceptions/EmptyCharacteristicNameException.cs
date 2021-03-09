using System;
using KadOzenka.Dal.ObjectsCharacteristics.Resources;

namespace KadOzenka.Dal.ObjectsCharacteristics.Exceptions
{
	public class EmptyCharacteristicNameException : Exception
	{
		public EmptyCharacteristicNameException() : base(Messages.EmptyCharacteristicName)
		{
		}
	}
}
