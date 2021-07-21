using System;

namespace ModelingBusiness.Modeling.Exceptions
{
	public class CanNotCreateMarksBecauseNoMarketObjectsException : Exception
	{
		public CanNotCreateMarksBecauseNoMarketObjectsException() : base("У модели нет объектов из контрольной или обучающей выборок. Создание меток невозможно.")
		{
			
		}
	}
}
