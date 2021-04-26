using Core.Register;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using KadOzenka.Dal.ObjectsCharacteristics.Exceptions;
using KadOzenka.Dal.ObjectsCharacteristics.Resources;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Dal.Tests.ObjectsCharacteristics.ObjectsCharacteristics
{
	public class EditCharacteristicTest : BaseObjectCharacteristicsTests
	{
		[Test]
		[TestCase("")]
		[TestCase(" ")]
		[TestCase(null)]
		public void CanNot_Edit_Characteristic_Without_Name(string name)
		{
			var dto = new CharacteristicDto {Name = name};

			var ex = Assert.Throws<EmptyCharacteristicNameException>(() =>
				ObjectsCharacteristicsService.EditCharacteristic(dto));

			Assert.AreEqual(Messages.EmptyCharacteristicName, ex.Message);
			RegisterAttributeService.Verify(
				x => x.CreateRegisterAttribute(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<RegisterAttributeType>(),
					It.IsAny<bool>(), It.IsAny<long>(), It.IsAny<bool>()), Times.Never);
			ObjectCharacteristicsRepository.Verify(
				x => x.CreateOrUpdateCharacteristicSetting(It.IsAny<long>(), It.IsAny<bool>(), It.IsAny<bool>(),
					It.IsAny<bool>(), It.IsAny<bool>()), Times.Never);
		}

		[Test]
		public void Can_Edit_Characteristic_Name()
		{
			var dto = new CharacteristicDto
				{Id = RandomGenerator.GenerateRandomInteger(), Name = RandomGenerator.GetRandomString()};

			ObjectsCharacteristicsService.EditCharacteristic(dto);

			RegisterAttributeService.Verify(x => x.RenameRegisterAttribute(dto.Id, dto.Name), Times.Once);
		}

		[Test]
		public void Can_Edit_Characteristic_Parent_Attribute_For_Living_Placement()
		{
			var dto = new CharacteristicDto
			{
				Id = RandomGenerator.GenerateRandomInteger(),
				Name = RandomGenerator.GetRandomString(),
				UseParentAttributeForLivingPlacement = true
			};

			ObjectsCharacteristicsService.EditCharacteristic(dto);

			ObjectCharacteristicsRepository.Verify(
				x => x.CreateOrUpdateCharacteristicSetting(It.IsAny<long>(), It.IsAny<bool>(), It.IsAny<bool>(),
					It.IsAny<bool>(),It.IsAny<bool>()), Times.Once);
			ObjectCharacteristicsRepository.Verify(
				x => x.CreateOrUpdateCharacteristicSetting(dto.Id, dto.UseParentAttributeForLivingPlacement,
					dto.UseParentAttributeForNotLivingPlacement, dto.UseParentAttributeForCarPlace,It.IsAny<bool>()), Times.Once);
		}

		[Test]
		public void Can_Edit_Characteristic_Parent_Attribute_For_NotLiving_Placement()
		{
			var dto = new CharacteristicDto
			{
				Id = RandomGenerator.GenerateRandomInteger(),
				Name = RandomGenerator.GetRandomString(),
				UseParentAttributeForNotLivingPlacement = true
			};

			ObjectsCharacteristicsService.EditCharacteristic(dto);

			ObjectCharacteristicsRepository.Verify(
				x => x.CreateOrUpdateCharacteristicSetting(It.IsAny<long>(), It.IsAny<bool>(), It.IsAny<bool>(),
					It.IsAny<bool>(),It.IsAny<bool>()), Times.Once);
			ObjectCharacteristicsRepository.Verify(
				x => x.CreateOrUpdateCharacteristicSetting(dto.Id, dto.UseParentAttributeForLivingPlacement,
					dto.UseParentAttributeForNotLivingPlacement, dto.UseParentAttributeForCarPlace,dto.DisableAttributeEditing), Times.Once);
		}

		[Test]
		public void Can_Edit_Characteristic_Parent_Attribute_For_Car_Place()
		{
			var dto = new CharacteristicDto
			{
				Id = RandomGenerator.GenerateRandomInteger(),
				Name = RandomGenerator.GetRandomString(),
				UseParentAttributeForCarPlace = true
			};

			ObjectsCharacteristicsService.EditCharacteristic(dto);

			ObjectCharacteristicsRepository.Verify(
				x => x.CreateOrUpdateCharacteristicSetting(It.IsAny<long>(), It.IsAny<bool>(), It.IsAny<bool>(),
					It.IsAny<bool>(),It.IsAny<bool>()), Times.Once);
			ObjectCharacteristicsRepository.Verify(
				x => x.CreateOrUpdateCharacteristicSetting(dto.Id, dto.UseParentAttributeForLivingPlacement,
					dto.UseParentAttributeForNotLivingPlacement, dto.UseParentAttributeForCarPlace, dto.DisableAttributeEditing), Times.Once);
		}
	}
}
