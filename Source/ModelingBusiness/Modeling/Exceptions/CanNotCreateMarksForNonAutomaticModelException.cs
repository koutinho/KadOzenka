using System;

namespace ModelingBusiness.Modeling.Exceptions
{
	public class CanNotCreateMarksForNonAutomaticModelException : Exception
	{
		public CanNotCreateMarksForNonAutomaticModelException() : base("Программное создание меток для не автоматической модели запрешено.")
		{
			
		}
	}
}
