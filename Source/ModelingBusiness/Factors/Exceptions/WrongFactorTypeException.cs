using System;

namespace ModelingBusiness.Factors.Exceptions
{
	public class WrongFactorTypeException : Exception
	{
		public WrongFactorTypeException() : base("Фактор относится к нечисловому типу, нужно установить тип метки 'Метка по умолчанию'")
		{

		}
	}
}
