using System;

namespace ModelingBusiness.Factors.Exceptions.AutomaticModelParametersCalculation
{
	public class CanNotCalculateParametersBecauseNoMarketObjectsException : Exception
	{
		public CanNotCalculateParametersBecauseNoMarketObjectsException() 
			: base("У модели нет ативных объектов из контрольной или обучающей выборок")
		{
			
		}
	}
}
