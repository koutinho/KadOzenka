using System;
using ObjectModel.Directory.Ko;

namespace KadOzenka.Dal.Modeling.Exceptions
{
	public class UnknownMarkTypeException : Exception
	{
		public UnknownMarkTypeException(MarkType markType) : base($"Передан неизвестный тип метки: '{markType}'")
		{

		}
	}
}
