using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Castle.Components.DictionaryAdapter;
using Core.Register;
using Core.Register.RegisterEntities;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.CodDictionary.Entities;
using KadOzenka.Dal.CodDictionary.Resources;
using KadOzenka.Dal.Registers;
using Moq;
using NUnit.Framework;
using ObjectModel.Core.Register;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tests.Cod.Dictionary
{
    [TestFixture]
    public class UpdatingTests : BaseCodTests
    {
        [Test]
        public void Can_Update_Dictionary()
        {
            var register = new OMRegister { RegisterId = RandomGenerator.GenerateRandomInteger() };
            var dictionary = new OMCodJob { Id = RandomGenerator.GenerateRandomInteger(), RegisterId = register.RegisterId};

            CodDictionaryRepository.Setup(x => x.GetById(dictionary.Id, null)).Returns(dictionary);
            RegisterService.Setup(x => x.GetRegister(register.RegisterId)).Returns(register);
            RegisterAttributeRepository
                .Setup(x => x.GetEntitiesByCondition(It.IsAny<Expression<Func<OMAttribute, bool>>>(),
                    It.IsAny<Expression<Func<OMAttribute, object>>>())).Returns(new List<OMAttribute>());

            var dictionaryInput = CreateDictionaryDto(numberOfValues: 2);
            dictionaryInput.Id = dictionary.Id;
            CodDictionaryService.UpdateCodDictionary(dictionaryInput);

            CodDictionaryRepository.Verify(x => x.Save(dictionary), Times.Once);
            RegisterService.Verify(x => x.SaveRegister(register), Times.Once);
            RegisterCacheWrapper.Verify(x => x.UpdateCache(), Times.Once);
            Assert.That(dictionary.NameJob, Is.EqualTo(dictionaryInput.Name));
            Assert.That(register.RegisterDescription, Is.EqualTo(dictionaryInput.Name));
        }

        [Test]
        public void CanNot_Update_Dictionary()
        {
            var dictionaryInput = CreateDictionaryDto(numberOfValues: 2);
            dictionaryInput.Name = null;
            
            var exception = Assert.Throws<Exception>(() => CodDictionaryService.UpdateCodDictionary(dictionaryInput));

            Assert.That(exception.Message, Is.EqualTo(CodMessages.EmptyDictionaryName));
        }
    }
}