using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Register;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Dictionaries;
using ModelingBusiness.Dictionaries.Exceptions;
using ModelingBusiness.Dictionaries.Exceptions.Dictionary;
using Moq;
using NUnit.Framework;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Modeling.Dictionaries
{
	[TestFixture]
	public class DictionaryCreationTests : BaseDictionaryTests
	{
		[Test]
		public void CanNot_Create_Dictionary_Without_Name()
		{
			string dictionaryName = null;
			ModelDictionaryRepository.Setup(x =>
				x.GetEntitiesByCondition(It.IsAny<Expression<Func<OMModelingDictionary, bool>>>(), null)).Returns(new List<OMModelingDictionary>());

			Assert.Throws<EmptyDictionaryNameException>(() =>
				ModelDictionaryService.CreateDictionary(dictionaryName, RegisterAttributeType.INTEGER,
					new List<long>()));

			ModelDictionaryRepository.Verify(foo => foo.Save(It.IsAny<OMModelingDictionary>()), Times.Never);
		}

		[Test]
		public void CanNot_Create_Dictionary_If_Dictionary_With_The_Same_Name_Already_Exists_In_Model()
		{
			var dictionaryName = RandomGenerator.GetRandomString();
			var existedDictionary = new DictionaryBuilder().Name(dictionaryName).Build();
			ModelDictionaryRepository.Setup(x =>
				x.GetEntitiesByCondition(It.IsAny<Expression<Func<OMModelingDictionary, bool>>>(), null)).Returns(new List<OMModelingDictionary>{existedDictionary});

			Assert.Throws<DictionaryAlreadyExistsException>(() =>
				ModelDictionaryService.CreateDictionary(dictionaryName, RegisterAttributeType.INTEGER,
					new List<long> {existedDictionary.Id}));

			ModelDictionaryRepository.Verify(foo => foo.Save(It.IsAny<OMModelingDictionary>()), Times.Never);
		}

		[TestCase(RegisterAttributeType.BFILE)]
		[TestCase(RegisterAttributeType.BLOB)]
		[TestCase(RegisterAttributeType.LINK)]
		public void CanNot_Create_Dictionary_With_Specific_Types(RegisterAttributeType factorType)
		{
			string dictionaryName = RandomGenerator.GetRandomString();
			ModelDictionaryRepository.Setup(x =>
				x.GetEntitiesByCondition(It.IsAny<Expression<Func<OMModelingDictionary, bool>>>(), null)).Returns(new List<OMModelingDictionary>());

			Assert.Throws<UnsupportedDictionaryTypeException>(() =>
				ModelDictionaryService.CreateDictionary(dictionaryName, factorType, new List<long>()));

			ModelDictionaryRepository.Verify(foo => foo.Save(It.IsAny<OMModelingDictionary>()), Times.Never);
		}
	}
}