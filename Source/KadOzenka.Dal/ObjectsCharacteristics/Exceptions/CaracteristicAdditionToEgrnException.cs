using System;
using KadOzenka.Dal.ObjectsCharacteristics.Resources;

namespace KadOzenka.Dal.ObjectsCharacteristics.Exceptions
{
	public class CaracteristicAdditionToEgrnException : Exception
	{
		public CaracteristicAdditionToEgrnException() : base(Messages.ForbiddenAdditionToEgrn)
		{
		}
	}
}
