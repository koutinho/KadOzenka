using System;
using System.Collections.Generic;
using Core.Register;
using Core.Register.RegisterEntities;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.CodDictionary.Entities;
using KadOzenka.Dal.CodDictionary.Resources;
using Moq;
using NUnit.Framework;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tests.Cod.Value
{
    [TestFixture]
    public class EditionTests : BaseCodTests
    {
        [Test]
        public void Can_Add_Dictionary_Value()
        {
            var dictionary = new OMCodJob {Id = RandomGenerator.GenerateRandomInteger(), RegisterId = RandomGenerator.GenerateRandomInteger()};
            var value = CreateDictionaryValue();
            CodDictionaryRepository.Setup(x => x.GetById(dictionary.Id, null)).Returns(dictionary);
            RegisterCacheWrapper.Setup(x => x.GetRegisterAttributesCache())
                .Returns(new Dictionary<long, RegisterAttribute>
                {
                    {1, new RegisterAttribute{RegisterId = (int) dictionary.RegisterId, Name = CodDictionaryConsts.CodeColumnName}}
                });

            CodDictionaryService.EditDictionaryValue(dictionary.Id, value);

            RegisterObjectWrapper.Verify(
                x => x.SetAttributeValue(It.IsAny<RegisterObject>(), It.IsAny<long>(), It.IsAny<object>()),
                Times.AtLeastOnce);
            RegisterObjectWrapper.Verify(x => x.Save(It.IsAny<RegisterObject>()), Times.Once);
        }

        [Test]
        public void CanNot_Add_Value_Without_Code()
        {
            var value = CreateDictionaryValue();
            value.Code = null;

            var exception = Assert.Throws<Exception>(() =>
                CodDictionaryService.EditDictionaryValue(RandomGenerator.GenerateRandomInteger(), value));

            StringAssert.Contains(CodMessages.NoValueCode, exception.Message);
        }
    }
}