using System;

namespace ModelingBusiness.Factors.Exceptions.AutomaticModelParametersCalculation
{
	public class CanNotCalculateParametersForNonAutomaticModelException : Exception
	{
		public CanNotCalculateParametersForNonAutomaticModelException() : base("Программное создание меток для не автоматической модели запрешено.")
		{
			
		}
	}
}
