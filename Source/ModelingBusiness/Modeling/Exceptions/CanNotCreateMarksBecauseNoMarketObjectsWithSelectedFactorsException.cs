using System;

namespace ModelingBusiness.Modeling.Exceptions
{
	public class CanNotCreateMarksBecauseNoMarketObjectsWithSelectedFactorsException : Exception
	{
		public CanNotCreateMarksBecauseNoMarketObjectsWithSelectedFactorsException() : base("У модели нет ативных объектов из контрольной или обучающей выборок с заполненными факторами модели")
		{
			
		}
	}
}
