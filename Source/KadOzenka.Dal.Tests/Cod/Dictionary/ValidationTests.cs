using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.CodDictionary.Entities;
using KadOzenka.Dal.CodDictionary.Resources;
using KadOzenka.Dal.Registers.Entities;
using NUnit.Framework;

namespace KadOzenka.Dal.Tests.Cod.Dictionary
{
    [TestFixture]
    public class ValidationTests : BaseCodTests
    {
        [Test]
        public void CanNot_Create_Dictionary_Without_Name()
        {
            var codDictionaryDto = new CodDictionaryDto
            {
                Name = null,
                Values = new List<AttributePure>{ new AttributePure(RandomGenerator.GenerateRandomInteger(), RandomGenerator.GetRandomString()) }
            };

            var errors = CodDictionary.CodDictionaryService.ValidateCodDictionary(codDictionaryDto).ToList();

            Assert.That(errors.Count, Is.EqualTo(1));
            Assert.That(errors.First().ErrorMessage, Is.EqualTo(CodMessages.EmptyDictionaryName));
        }

        [Test]
        public void CanNot_Create_Dictionary_Without_Values()
        {
            var codDictionaryDto = new CodDictionaryDto
            {
                Name = RandomGenerator.GetRandomString(),
                Values = null
            };

            var errors = CodDictionary.CodDictionaryService.ValidateCodDictionary(codDictionaryDto).ToList();

            Assert.That(errors.Count, Is.EqualTo(1));
            Assert.That(errors.First().ErrorMessage, Is.EqualTo(CodMessages.NoDictionaryValues));
        }

        [Test]
        public void CanNot_Create_Dictionary_If_Max_Values_Count_Is_Exceeded()
        {
            var codDictionaryDto =
                CreateDictionaryDto(numberOfValues: CodDictionaryConsts.MaxValuesCount +
                                                    RandomGenerator.GenerateRandomInteger());

            var errors = CodDictionary.CodDictionaryService.ValidateCodDictionary(codDictionaryDto).ToList();

            Assert.That(errors.Count, Is.EqualTo(1));
            Assert.That(errors.First().ErrorMessage, Does.StartWith(CodMessages.MaxValuesCountExceed));
        }

        [Test]
        public void CanNot_Create_Dictionary_If_Min_Values_Count_IsNot_Exceeded()
        {
            var codDictionaryDto = CreateDictionaryDto(numberOfValues: 0);

            var errors = CodDictionary.CodDictionaryService.ValidateCodDictionary(codDictionaryDto).ToList();

            Assert.That(errors.Count, Is.EqualTo(1));
            Assert.That(errors.First().ErrorMessage, Does.StartWith(CodMessages.MinValuesCountExceed));
        }

        [Test]
        public void CanNot_Create_Dictionary_If_Value_Name_Is_Equal_To_Code_Column_Name()
        {
            var codDictionaryDto = new CodDictionaryDto
            {
                Name = RandomGenerator.GetRandomString(),
                Values = new List<AttributePure> {new AttributePure(RandomGenerator.GenerateRandomInteger(), CodDictionaryConsts.CodeColumnName)}
            };

            var errors = CodDictionary.CodDictionaryService.ValidateCodDictionary(codDictionaryDto).ToList();

            Assert.That(errors.Count, Is.EqualTo(1));
            Assert.That(errors.First().ErrorMessage, Does.StartWith(CodMessages.ForbiddenValueName));
        }

        [Test]
        public void CanNot_Create_Dictionary_If_Value_Is_Empty()
        {
            var codDictionaryDto = new CodDictionaryDto
            {
                Name = RandomGenerator.GetRandomString(),
                Values = new List<AttributePure> {new AttributePure(RandomGenerator.GenerateRandomInteger(), null)}
            };

            var errors = CodDictionary.CodDictionaryService.ValidateCodDictionary(codDictionaryDto).ToList();

            Assert.That(errors.Count, Is.EqualTo(1));
            Assert.That(errors.First().ErrorMessage, Does.StartWith(string.Format(CodMessages.EmptyValueName, 1)));
        }
    }
}