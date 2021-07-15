using System;
using Core.Shared.Extensions;
using ObjectModel.Directory.KO;

namespace KadOzenka.Dal.Modeling.Dictionaries.Exceptions
{
	public class MarkValueConvertingException : Exception
	{
		public MarkValueConvertingException(string value, ModelDictionaryType type) 
			: base($"Значение '{value}' не может быть приведено к типу '{type.GetEnumDescription()}' (тип фактора, к которому привязан словарь)")
		{

		}
	}
}
