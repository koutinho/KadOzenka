using System;

namespace ModelingBusiness.Model.Exceptions
{
	public class CanNotActivateNotPreparedAutomaticModelException : Exception
	{
		public CanNotActivateNotPreparedAutomaticModelException()
			: base("Невозможно активировать необученную модель")
		{

		}
	}
}
