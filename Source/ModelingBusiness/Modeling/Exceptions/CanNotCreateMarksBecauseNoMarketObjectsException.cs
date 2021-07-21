using System;

namespace ModelingBusiness.Modeling.Exceptions
{
	public class CanNotCreateMarksBecauseNoMarketObjectsException : Exception
	{
		public CanNotCreateMarksBecauseNoMarketObjectsException() : base("У модели нет объектов. Создание меток невозможно.")
		{
			
		}
	}
}
