using System;

namespace ModelingBusiness.Modeling.Exceptions
{
	public class CanNotCreateMarksBecauseNoFactorsException : Exception
	{
		public CanNotCreateMarksBecauseNoFactorsException() : base("У модели нет факторов.Создание меток невозможно.")
		{
			
		}
	}
}
