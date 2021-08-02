using System;

namespace ModelingBusiness.Factors.Exceptions.Automatic
{
	public class AttributeForbiddenForActivationException : Exception
	{
		public AttributeForbiddenForActivationException() : base("Атрибут недоступен для активации")
		{
			
		}
	}
}
