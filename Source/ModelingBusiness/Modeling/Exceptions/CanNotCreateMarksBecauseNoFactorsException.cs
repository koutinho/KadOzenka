using System;
using Core.Shared.Extensions;
using ObjectModel.Directory.Ko;

namespace ModelingBusiness.Modeling.Exceptions
{
	public class CanNotCreateMarksBecauseNoFactorsException : Exception
	{
		public CanNotCreateMarksBecauseNoFactorsException() 
			: base($"У модели нет факторов с типом метки '{MarkType.Default.GetEnumDescription()}'. Создание меток невозможно.")
		{
			
		}
	}
}
