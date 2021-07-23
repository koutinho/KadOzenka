using System;

namespace ModelingBusiness.Modeling.Exceptions
{
	public class CanNotCreateMarksBecauseNoMarketObjectsException : Exception
	{
		public CanNotCreateMarksBecauseNoMarketObjectsException() : base("У модели нет ативных объектов из контрольной или обучающей выборок")
		{
			
		}
	}
}
