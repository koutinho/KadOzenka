using System;

namespace ModelingBusiness.Modeling.Exceptions
{
	public class CanNotCreateMarksBecauseNoDictionaryException : Exception
	{
		public CanNotCreateMarksBecauseNoDictionaryException(string factorName) : base($"У фактора '{factorName}' нет словаря.")
		{
			
		}
	}
}
