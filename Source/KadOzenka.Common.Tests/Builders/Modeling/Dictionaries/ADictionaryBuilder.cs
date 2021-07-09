﻿using Core.Shared.Extensions;
using ObjectModel.Directory.KO;
using ObjectModel.KO;

namespace KadOzenka.Common.Tests.Builders.Modeling.Dictionaries
{
	public abstract class ADictionaryBuilder
	{
		protected readonly OMModelingDictionary _dictionary;


		protected ADictionaryBuilder()
		{
			var type = ModelDictionaryType.String;
			_dictionary = new OMModelingDictionary
			{
				Name = RandomGenerator.GetRandomString(),
				Type_Code = type,
				Type = type.GetEnumDescription()
			};
		}



		public abstract OMModelingDictionary Build();
	}
}