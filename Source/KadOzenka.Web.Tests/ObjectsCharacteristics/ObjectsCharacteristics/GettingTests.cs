using System;
using Core.Register;
using KadOzenka.Common.Tests;
using KadOzenka.Web.Models.ObjectsCharacteristics;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ObjectModel.Core.Register;
using ObjectModel.Gbu;

namespace KadOzenka.Web.UnitTests.ObjectsCharacteristics.ObjectsCharacteristics
{
	public class GettingTests: BaseObjectCharacteristicsTests
	{
		[Test]
		public void Can_Get_View_For_New_Characteristic()
		{
			var registerId = RandomGenerator.GenerateRandomInteger();

			var result = ObjectsCharacteristicsController.AddCharacteristic(registerId);

			var view = result as ViewResult;
			Assert.IsNotNull(view);
			Assert.IsNotNull(view.Model);
			Assert.IsTrue(view.Model is CharacteristicModel);
			Assert.AreEqual(-1, ((CharacteristicModel)view.Model).Id);
			Assert.AreEqual(registerId, ((CharacteristicModel)view.Model).RegisterId);
			Assert.IsTrue(view.ViewName == "~/Views/ObjectsCharacteristics/EditCharacteristic.cshtml");
		}

		[Test]
		[TestCase(RegisterAttributeType.BOOLEAN)]
		[TestCase(RegisterAttributeType.DATE)]
		[TestCase(RegisterAttributeType.DECIMAL)]
		[TestCase(RegisterAttributeType.INTEGER)]
		[TestCase(RegisterAttributeType.STRING)]
		public void Can_Get_View_For_Existed_Characteristic(RegisterAttributeType registerAttributeType)
		{
			var attribute = new OMAttribute
			{
				Id = RandomGenerator.GenerateRandomInteger(),
				RegisterId = RandomGenerator.GenerateRandomInteger(),
				Name = RandomGenerator.GetRandomString(),
				Type = (long)registerAttributeType,
			};
			RegisterAttributeService.Setup(x => x.GetRegisterAttribute(attribute.Id))
				.Returns(attribute);

			var result = ObjectsCharacteristicsController.EditCharacteristic(attribute.Id);

			var view = result as ViewResult;
			Assert.IsNotNull(view);
			Assert.IsNotNull(view.Model);
			Assert.IsTrue(view.Model is CharacteristicModel);
			Assert.AreEqual(attribute.Id, ((CharacteristicModel)view.Model).Id);
			Assert.AreEqual(attribute.RegisterId, ((CharacteristicModel)view.Model).RegisterId);
			Assert.AreEqual(attribute.Name, ((CharacteristicModel)view.Model).Name);
			Assert.AreEqual(attribute.Type, (long)((CharacteristicModel)view.Model).Type);
		}

		[Test]
		public void Can_Get_View_For_Existed_Characteristic_With_Reference_Type()
		{
			var attribute = new OMAttribute
			{
				Type = (long)RegisterAttributeType.STRING,
				ReferenceId = RandomGenerator.GenerateRandomInteger()
			};
			RegisterAttributeService.Setup(x => x.GetRegisterAttribute(attribute.Id))
				.Returns(attribute);

			var result = ObjectsCharacteristicsController.EditCharacteristic(attribute.Id);

			var view = result as ViewResult;
			Assert.IsNotNull(view);
			Assert.IsNotNull(view.Model);
			Assert.IsTrue(view.Model is CharacteristicModel);
			Assert.AreEqual(RegisterAttributeType.REFERENCE, ((CharacteristicModel)view.Model).Type);
		}

		[Test]
		public void Can_Get_View_For_Existed_Characteristic_Without_Settings()
		{
			var attribute = new OMAttribute
			{
				Id = RandomGenerator.GenerateRandomInteger(),
			};
			RegisterAttributeService.Setup(x => x.GetRegisterAttribute(attribute.Id))
				.Returns(attribute);
			ObjectsCharacteristicsService.Setup(x => x.GetRegisterAttributeSettings(attribute.Id))
				.Returns((OMAttributeSettings)null);

			var result = ObjectsCharacteristicsController.EditCharacteristic(attribute.Id);

			var view = result as ViewResult;
			Assert.IsNotNull(view);
			Assert.IsNotNull(view.Model);
			Assert.IsTrue(view.Model is CharacteristicModel);
			Assert.AreEqual(attribute.Id, ((CharacteristicModel)view.Model).Id);
			Assert.IsFalse(((CharacteristicModel)view.Model).UseParentAttributeForLivingPlacement);
			Assert.IsFalse(((CharacteristicModel)view.Model).UseParentAttributeForNotLivingPlacement);
			Assert.IsFalse(((CharacteristicModel)view.Model).UseParentAttributeForCarPlace);
		}

		[Test]
		[TestCase(true, false, false)]
		[TestCase(false, true, false)]
		[TestCase(false, false, true)]
		public void Can_Get_View_For_Existed_Characteristic_With_Settings(bool useParentAttributeForLivingPlacement,
			bool useParentAttributeForNotLivingPlacement, bool useParentAttributeForCarPlace)
		{
			var attribute = new OMAttribute
			{
				Id = RandomGenerator.GenerateRandomInteger(),
			};
			var settings = new OMAttributeSettings()
			{
				AttributeId = attribute.Id,
				UseParentAttributeForLivingPlacements = useParentAttributeForLivingPlacement,
				UseParentAttributeForNotLivingPlacements = useParentAttributeForNotLivingPlacement,
				UseParentAttributeForCarPlace = useParentAttributeForCarPlace
			};
			RegisterAttributeService.Setup(x => x.GetRegisterAttribute(attribute.Id))
				.Returns(attribute);
			ObjectsCharacteristicsService.Setup(x => x.GetRegisterAttributeSettings(attribute.Id))
				.Returns(settings);

			var result = ObjectsCharacteristicsController.EditCharacteristic(attribute.Id);

			var view = result as ViewResult;
			Assert.IsNotNull(view);
			Assert.IsNotNull(view.Model);
			Assert.IsTrue(view.Model is CharacteristicModel);
			Assert.AreEqual(attribute.Id, ((CharacteristicModel) view.Model).Id);
			Assert.AreEqual(useParentAttributeForLivingPlacement,
				((CharacteristicModel) view.Model).UseParentAttributeForLivingPlacement);
			Assert.AreEqual(useParentAttributeForNotLivingPlacement,
				((CharacteristicModel) view.Model).UseParentAttributeForNotLivingPlacement);
			Assert.AreEqual(useParentAttributeForCarPlace,
				((CharacteristicModel) view.Model).UseParentAttributeForCarPlace);
		}

		[Test]
		public void CanNot_Get_View_For_NonExisted_Characteristic()
		{
			var attribute = new OMAttribute
			{
				Id = RandomGenerator.GenerateRandomInteger(),
			};
			RegisterAttributeService.Setup(x => x.GetRegisterAttribute(attribute.Id))
				.Returns((OMAttribute)null);

			Assert.Throws<Exception>(() => ObjectsCharacteristicsController.EditCharacteristic(attribute.Id));
		}

		[Test]
		public void Can_Get_Delete_View_For_Existed_Characteristic()
		{
			var attribute = new OMAttribute
			{
				Id = RandomGenerator.GenerateRandomInteger(),
				Name = RandomGenerator.GetRandomString(),
			};
			RegisterAttributeService.Setup(x => x.GetRegisterAttribute(attribute.Id))
				.Returns(attribute);

			var result = ObjectsCharacteristicsController.DeleteCharacteristic(attribute.Id);

			var view = result as ViewResult;
			Assert.IsNotNull(view);
			Assert.IsNotNull(view.Model);
			Assert.IsTrue(view.Model is CharacteristicModel);
			Assert.AreEqual(attribute.Id, ((CharacteristicModel)view.Model).Id);
			Assert.AreEqual(attribute.Name, ((CharacteristicModel)view.Model).Name);
		}

		[Test]
		public void CanNot_Get_Delete_View_For_NonExisted_Characteristic()
		{
			var attribute = new OMAttribute
			{
				Id = RandomGenerator.GenerateRandomInteger(),
			};
			RegisterAttributeService.Setup(x => x.GetRegisterAttribute(attribute.Id))
				.Returns((OMAttribute)null);

			Assert.Throws<Exception>(() => ObjectsCharacteristicsController.DeleteCharacteristic(attribute.Id));
		}
	}
}
