using System;

namespace ModelingBusiness.Objects.Exceptions
{
	public class ObjectIsForControlAndForTrainingAtTheSameTimeException : Exception
	{
		public ObjectIsForControlAndForTrainingAtTheSameTimeException()
			: base("Объект не может быть в контрольной и обучающей выборках одновременно")
		{

		}
	}
}
