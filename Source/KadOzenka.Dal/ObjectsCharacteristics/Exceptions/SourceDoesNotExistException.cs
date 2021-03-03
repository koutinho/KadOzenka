using System;
using KadOzenka.Dal.ObjectsCharacteristics.Resources;

namespace KadOzenka.Dal.ObjectsCharacteristics.Exceptions
{
	public class SourceDoesNotExistException : Exception
	{
		public SourceDoesNotExistException(long registerId) : base(string.Format(Messages.SourceDoesNotExist, registerId)) 
		{ }
	}
}
