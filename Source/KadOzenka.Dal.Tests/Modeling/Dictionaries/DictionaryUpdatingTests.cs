using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Dictionaries;
using ModelingBusiness.Dictionaries.Exceptions;
using Moq;
using NUnit.Framework;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Modeling.Dictionaries
{
	[TestFixture]
	public class DictionaryUpdatingTests : BaseDictionaryTests
	{
		[Test]
		public void CanNot_Update_Dictionary_If_Dictionary_With_The_Same_Name_Already_Exists_In_Model()
		{
			var dictionaryToUpdate = new DictionaryBuilder().Build();
			var existedDictionary = new DictionaryBuilder().Build();
			ModelDictionaryRepository.Setup(x => x.GetById(dictionaryToUpdate.Id, null)).Returns(dictionaryToUpdate);
			ModelDictionaryRepository.Setup(x =>
				x.GetEntitiesByCondition(It.IsAny<Expression<Func<OMModelingDictionary, bool>>>(), null)).Returns(new List<OMModelingDictionary>{existedDictionary});

			Assert.Throws<DictionaryAlreadyExistsException>(() =>
				ModelDictionaryService.UpdateDictionary(dictionaryToUpdate.Id, existedDictionary.Name,
					new List<long> {existedDictionary.Id}));

			ModelDictionaryRepository.Verify(foo => foo.Save(It.IsAny<OMModelingDictionary>()), Times.Never);
		}
	}
}