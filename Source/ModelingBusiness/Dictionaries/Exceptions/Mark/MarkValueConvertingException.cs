using System;
using Core.Shared.Extensions;
using ObjectModel.KO;

namespace ModelingBusiness.Dictionaries.Exceptions.Mark
{
	public class MarkValueConvertingException : Exception
	{
		public MarkValueConvertingException(string value, OMModelingDictionary dictionary) 
			: base($"Значение '{value}' в словаре '{dictionary.Name}' не может быть приведено к типу словаря '{dictionary.Type_Code.GetEnumDescription()}' (тип фактора, к которому привязан словарь)")
		{

		}
	}
}
