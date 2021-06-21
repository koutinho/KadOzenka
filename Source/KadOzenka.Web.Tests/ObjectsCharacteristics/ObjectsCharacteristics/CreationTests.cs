using System;
using System.Linq;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using KadOzenka.Web.Models.ObjectsCharacteristics;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Web.UnitTests.ObjectsCharacteristics.ObjectsCharacteristics
{
	public class CreationTests : BaseObjectCharacteristicsTests
	{
		[Test]
		public void Can_Add_Characteristic()
		{
			var model = new CharacteristicModel
			{
				Id = -1,
				Name = RandomGenerator.GetRandomString()
			};
			var newId = RandomGenerator.GenerateRandomInteger();
			ObjectsCharacteristicsService.Setup(x => x.AddCharacteristic(It.IsAny<CharacteristicDto>())).Returns(newId);

			var result = ObjectsCharacteristicsController.EditCharacteristic(model);
			var message = GetPropertyFromJson(result, "Message")?.ParseToStringNullable();
			var updatedModel = GetPropertyFromJson(result, "data");

			ObjectsCharacteristicsService.Verify(x => x.AddCharacteristic(It.IsAny<CharacteristicDto>()), Times.Once);
			ObjectsCharacteristicsService.Verify(x => x.EditCharacteristic(It.IsAny<CharacteristicDto>()), Times.Never);
			Assert.IsTrue(updatedModel is CharacteristicModel);
			Assert.AreEqual(newId, ((CharacteristicModel) updatedModel).Id);
			Assert.AreEqual("Характеристика успешно сохранена", message);
		}

		[Test]
		[TestCase(null)]
		[TestCase("")]
		public void CanNot_Add_Characteristic_With_Empty_Name(string name)
		{
			var model = new CharacteristicModel
			{
				Id = -1,
				Name = name
			};

			var errors = ModelValidator.Validate(model);

			Assert.That(errors.Count, Is.EqualTo(1));
			Assert.IsTrue(errors.First().ErrorMessage.Contains(CharacteristicModel.EmptyDictionaryNameErrorMessage),
				errors.GetAllErrorMessagesAsOneString());
		}

		[Test]
		public void CanNot_Add_Reference_Type_Characteristic_Without_Reference_Link()
		{
			var model = new CharacteristicModel
			{
				Id = -1,
				Name = RandomGenerator.GetRandomString(),
				Type = RegisterAttributeType.REFERENCE
			};

			var errors = ModelValidator.Validate(model);

			Assert.That(errors.Count, Is.EqualTo(1));
			Assert.IsTrue(errors.First().ErrorMessage.Contains(CharacteristicModel.ReferenceTypeWithoutReferenceLinkErrorMessage),
				errors.GetAllErrorMessagesAsOneString());
		}

		[Test]
		public void CanNot_Add_Characteristic_If_Model_State_Is_Invalid()
		{
			var controller = ObjectsCharacteristicsController;
			var method = new Func<CharacteristicModel, IActionResult>(controller.EditCharacteristic);

			CheckMethodValidateModelState(controller, method);
		}
	}
}
