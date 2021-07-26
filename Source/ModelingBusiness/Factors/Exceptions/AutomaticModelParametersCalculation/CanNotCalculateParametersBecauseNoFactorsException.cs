using System;
using Core.Shared.Extensions;
using ObjectModel.Directory.Ko;

namespace ModelingBusiness.Factors.Exceptions.AutomaticModelParametersCalculation
{
	public class CanNotCalculateParametersBecauseNoFactorsException : Exception
	{
		public CanNotCalculateParametersBecauseNoFactorsException() 
			: base($"У модели нет факторов с типом метки '{MarkType.Straight.GetEnumDescription()}' или '{MarkType.Reverse.GetEnumDescription()}'. Создание меток невозможно.")
		{
			
		}
	}
}
