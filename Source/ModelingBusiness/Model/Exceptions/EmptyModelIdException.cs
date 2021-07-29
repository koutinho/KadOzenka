using System;

namespace ModelingBusiness.Model.Exceptions
{
	public class EmptyModelIdException : Exception
	{
		public EmptyModelIdException() : base("Не передан ИД Модели")
		{
			
		}
	}
}
