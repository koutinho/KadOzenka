using System;

namespace ModelingBusiness.Modeling.Exceptions
{
	public class CanNotCreateMarksBecauseNoMarketObjectsWithSelectedFactorsException : Exception
	{
		public CanNotCreateMarksBecauseNoMarketObjectsWithSelectedFactorsException() : base("У модели нет активных объектов из контрольной или обучающей выборок с заполненными факторами модели")
		{
			
		}
	}
}
