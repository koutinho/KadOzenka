using System;

namespace ModelingBusiness.Modeling.Exceptions
{
	public class CanNotCreateMarksForNonAutomaticModelException : Exception
	{
		public CanNotCreateMarksForNonAutomaticModelException() 
			: base("Расчет параметров для не автоматической модели запрещен")
		{
			
		}
	}
}
