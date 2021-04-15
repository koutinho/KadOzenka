using System;
using System.Collections.Generic;
using Core.Register;
using Core.Register.RegisterEntities;
using KadOzenka.Dal.CodDictionary.Entities;
using KadOzenka.Dal.CodDictionary.Resources;
using Moq;
using NUnit.Framework;
using ObjectModel.Core.Register;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tests.Cod.Dictionary
{
    [TestFixture]
    public class CreationTests : BaseCodTests
    {
        [Test]
        public void During_Dictionary_Addition_Info_About_Dictionary_Is_Saved_To_Table()
        {
            var createdRegister = new OMRegister { RegisterId = RandomGenerator.GenerateRandomInteger() };
            var createdDictionary = new OMCodJob();
            var dictionaryInput = CreateDictionaryDto();
            RegisterCacheWrapper.Setup(x => x.GetRegistersCache()).Returns(new Dictionary<int, RegisterData>());
            CodDictionaryRepository.Setup(x => x.Save(It.IsAny<OMCodJob>())).Callback<OMCodJob>(x => createdDictionary = x);
            MapRegisterCreation(createdRegister);

            CodDictionaryService.AddCodDictionary(dictionaryInput);

            CodDictionaryRepository.Verify(x => x.Save(It.IsAny<OMCodJob>()), Times.Once);
            Assert.That(createdDictionary.NameJob, Is.EqualTo(dictionaryInput.Name));
            Assert.That(createdDictionary.RegisterId, Is.EqualTo(createdRegister.RegisterId));
        }

        [Test]
        public void During_Dictionary_Addition_New_Table_Is_Created()
        {
            var createdRegister = new OMRegister { RegisterId = RandomGenerator.GenerateRandomInteger() };
            var dictionaryInput = CreateDictionaryDto();
            RegisterCacheWrapper.Setup(x => x.GetRegistersCache()).Returns(new Dictionary<int, RegisterData>());
            MapRegisterCreation(createdRegister);

            CodDictionaryService.AddCodDictionary(dictionaryInput);

            RegisterService.Verify(
                x => x.CreateRegister(It.IsAny<string>(), dictionaryInput.Name, It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<long>()), Times.Once);
        }

        [Test]
        public void During_Dictionary_Addition_New_Columns_Are_Created()
        {
            var createdRegister = new OMRegister { RegisterId = RandomGenerator.GenerateRandomInteger() };
            var dictionaryInput = CreateDictionaryDto();
            RegisterCacheWrapper.Setup(x => x.GetRegistersCache()).Returns(new Dictionary<int, RegisterData>());
            MapRegisterCreation(createdRegister);

            CodDictionaryService.AddCodDictionary(dictionaryInput);

            RegisterAttributeService.Verify(
                x => x.CreateRegisterAttribute(dictionaryInput.Values[0].Name, createdRegister.RegisterId,
                    RegisterAttributeType.STRING, true, null, true), Times.Once);

            RegisterAttributeService.Verify(
                x => x.CreateRegisterAttribute(CodDictionaryConsts.CodeColumnName, createdRegister.RegisterId,
                    RegisterAttributeType.STRING, true, null, false), Times.Once);
        }

        [Test]
        public void CanNot_Add_Dictionary_Without_Name()
        {
            var dictionaryInput = CreateDictionaryDto();
            dictionaryInput.Name = null;

            var exception = Assert.Throws<Exception>(() => CodDictionaryService.AddCodDictionary(dictionaryInput));

            StringAssert.Contains(CodMessages.EmptyDictionaryName, exception.Message);
        }
    }
}