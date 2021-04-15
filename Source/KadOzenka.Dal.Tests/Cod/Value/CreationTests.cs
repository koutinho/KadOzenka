using System.Collections.Generic;
using Core.Register;
using Core.Register.RegisterEntities;
using KadOzenka.Dal.CodDictionary.Entities;
using Moq;
using NUnit.Framework;
using ObjectModel.Core.Register;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tests.Cod.Value
{
    [TestFixture]
    public class CreationTests : BaseCodTests
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
    }
}