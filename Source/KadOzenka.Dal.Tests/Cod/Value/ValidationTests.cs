using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.CodDictionary.Entities;
using KadOzenka.Dal.CodDictionary.Resources;
using KadOzenka.Dal.Registers.Entities;
using NUnit.Framework;

namespace KadOzenka.Dal.Tests.Cod.Value
{
    [TestFixture]
    public class ValidationTests : BaseCodTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void CanNot_Create_Value_Without_Code(string code)
        {
            var value = CreateDictionaryValue();
            value.Code = code;

            var errors = CodDictionary.CodDictionaryService.ValidateDictionaryValue(value).ToList();

            Assert.That(errors.Count, Is.EqualTo(1));
            Assert.That(errors.First().ErrorMessage, Is.EqualTo(CodMessages.NoValueCode));
        }

        [Test]
        public void CanNot_Create_Value_Without_If_All_Values_Are_Null()
        {
            var values = new List<CodDictionaryValuePure>
            {
                new CodDictionaryValuePure(RandomGenerator.GenerateRandomInteger(), null),
                new CodDictionaryValuePure(RandomGenerator.GenerateRandomInteger(), string.Empty)
            };

            var value = new CodDictionaryValue(RandomGenerator.GenerateRandomInteger(), RandomGenerator.GetRandomString(), values);

            var errors = CodDictionary.CodDictionaryService.ValidateDictionaryValue(value).ToList();

            Assert.That(errors.Count, Is.EqualTo(1));
            Assert.That(errors.First().ErrorMessage, Is.EqualTo(CodMessages.AtLeastOneDictionaryValueNeeded));
        }
    }
}