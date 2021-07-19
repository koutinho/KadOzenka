using System;

namespace KadOzenka.Dal.Modeling.Factors.Exceptions
{
	public class WrongFactorTypeException : Exception
	{
		public WrongFactorTypeException() : base("Фактор относится к нечисловому типу, нужно установить тип метки 'Метка по умолчанию'")
		{

		}
	}
}
