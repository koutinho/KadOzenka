using System;

namespace KadOzenka.Dal.Modeling.Factors.Exceptions
{
	public class EmptyDictionaryForFactorWithDefaultMarkException : Exception
	{
		public EmptyDictionaryForFactorWithDefaultMarkException() : base(
			"Для фактора с типом метки 'По умолчанию' обязательно нужно заполнить имя словаря")
		{

		}
	}
}
