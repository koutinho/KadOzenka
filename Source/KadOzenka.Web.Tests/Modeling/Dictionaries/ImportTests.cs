using System.Linq;
using KadOzenka.Web.Models.Modeling;
using NUnit.Framework;

namespace KadOzenka.Web.Tests.Modeling.Dictionaries
{
	public class ImportTests : BaseModelingTests
	{
		[Test]
		public void CanNot_Import_New_Dictionary_Without_Name()
		{
			var model = new PartialDictionaryModel
			{
				ModelPrefix = RandomGenerator.GetRandomString(),
				DictionaryId = RandomGenerator.GenerateRandomInteger(),
				DeleteOldValues = false,
				NewDictionaryName = null,
				IsNewDictionary = true
			};

			var errors = ModelValidator.Validate(model);

			Assert.That(errors.Count, Is.EqualTo(1));
			Assert.IsTrue(errors.First().ErrorMessage.Contains(PartialDictionaryModel.EmptyDictionaryNameErrorMessage),
				errors.GetAllErrorMessagesAsOneString());
		}

		[Test]
		public void CanNot_Import_Old_Dictionary_Without_Id()
		{
			var model = new PartialDictionaryModel
			{
				ModelPrefix = RandomGenerator.GetRandomString(),
				DictionaryId = null,
				DeleteOldValues = false,
				NewDictionaryName = null,
				IsNewDictionary = false
			};

			var errors = ModelValidator.Validate(model);

			Assert.That(errors.Count, Is.EqualTo(1));
			Assert.IsTrue(errors.First().ErrorMessage.Contains(PartialDictionaryModel.NoDictionaryIdErrorMessage),
				errors.GetAllErrorMessagesAsOneString());
		}
	}
}