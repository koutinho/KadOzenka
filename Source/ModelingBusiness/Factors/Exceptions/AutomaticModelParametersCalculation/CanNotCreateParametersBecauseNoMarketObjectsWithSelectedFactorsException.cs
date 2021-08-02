using System;

namespace ModelingBusiness.Factors.Exceptions.AutomaticModelParametersCalculation
{
	public class CanNotCreateParametersBecauseNoMarketObjectsWithSelectedFactorsException : Exception
	{
		public CanNotCreateParametersBecauseNoMarketObjectsWithSelectedFactorsException() 
			: base("У модели нет активных объектов из контрольной или обучающей выборок с заполненными факторами модели")
		{
			
		}
	}
}
