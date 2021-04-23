using System;
using Core.Register;
using Core.Register.RegisterEntities;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using KadOzenka.Dal.ObjectsCharacteristics.Exceptions;
using KadOzenka.Dal.ObjectsCharacteristics.Resources;
using Moq;
using NUnit.Framework;
using ObjectModel.Core.Register;
using Platform.Configurator.DbConfigurator;
using Platform.Register;

namespace KadOzenka.Dal.Tests.ObjectsCharacteristics.ObjectsCharacteristics
{
	[TestFixture]
	public class AddCharacteristicTests : BaseObjectCharacteristicsTests
	{
		[Test]
		[TestCase("")]
		[TestCase(" ")]
		[TestCase(null)]
		public void CanNot_Create_Characteristic_Without_Name(string name)
		{
			var dto = new CharacteristicDto {Name = name};

			var ex = Assert.Throws<EmptyCharacteristicNameException>(() =>
				ObjectsCharacteristicsService.AddCharacteristic(dto));

			Assert.AreEqual(Messages.EmptyCharacteristicName, ex.Message);
			RegisterAttributeService.Verify(
				x => x.CreateRegisterAttribute(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<RegisterAttributeType>(),
					It.IsAny<bool>(), It.IsAny<long>(), It.IsAny<bool>()), Times.Never);
			ObjectCharacteristicsRepository.Verify(
				x => x.CreateOrUpdateCharacteristicSetting(It.IsAny<long>(), It.IsAny<bool>(), It.IsAny<bool>(),
					It.IsAny<bool>(),It.IsAny<bool>()), Times.Never);
		}

		[Test]
		public void Can_Create_Characteristic()
		{
			var dto = new CharacteristicDto
			{
				Name = RandomGenerator.GetRandomString(),
				RegisterId = RandomGenerator.GenerateRandomInteger(),
				ReferenceId = RandomGenerator.GenerateRandomInteger()
			};
			MockRegisterAttributeServiceCreateRegisterAttribute(dto);
			MockRegisterCacheGetRegisterData(dto.RegisterId);

			ObjectsCharacteristicsService.AddCharacteristic(dto);

			RegisterAttributeService.Verify(
				x => x.CreateRegisterAttribute(dto.Name, dto.RegisterId, dto.Type, false, dto.ReferenceId, true), Times.Once);
			RegisterConfiguratorWrapper.Verify(x => x.CreateDbColumnForRegister(It.IsAny<OMAttribute>(), It.IsAny<DbConfiguratorBase>()), Times.Once);
		}


		[Test]
		public void Can_Create_Characteristic_With_Attribute_Allpri_Partitioning()
		{
			var dto = new CharacteristicDto
			{
				Name = RandomGenerator.GetRandomString(),
				RegisterId = RandomGenerator.GenerateRandomInteger(),
				ReferenceId = RandomGenerator.GenerateRandomInteger()
			};
			MockRegisterAttributeServiceCreateRegisterAttribute(dto);
			MockRegisterCacheGetRegisterData(dto.RegisterId, AllpriPartitioningType.AttributeId);

			ObjectsCharacteristicsService.AddCharacteristic(dto);

			RegisterAttributeService.Verify(
				x => x.CreateRegisterAttribute(dto.Name, dto.RegisterId, dto.Type, false, dto.ReferenceId, true), Times.Once);
			RegisterConfiguratorWrapper.Verify(x => x.CreateDbTableForRegister(It.IsAny<long>()), Times.Once);
		}

		[Test]
		public void CanNot_Create_Characteristic_With_Attribute_Allpri_Partitioning_If_User_HasNot_Admin_Permission()
		{
			var dto = new CharacteristicDto
			{
				Name = RandomGenerator.GetRandomString(),
			};
			MockRegisterAttributeServiceCreateRegisterAttribute(dto);
			MockRegisterCacheGetRegisterData(dto.RegisterId, AllpriPartitioningType.AttributeId);
			SRDSessionWrapper.Setup(y => y.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.ADMIN, true, false, false))
				.Throws(new Exception());

			Assert.Throws<Exception>(() => ObjectsCharacteristicsService.AddCharacteristic(dto));

			RegisterConfiguratorWrapper.Verify(x => x.CreateDbTableForRegister(It.IsAny<long>()), Times.Never);
		}

		[Test]
		public void Characteristic_Settings_Are_Created()
		{
			var dto = new CharacteristicDto
			{
				Name = RandomGenerator.GetRandomString(),
			};
			MockRegisterAttributeServiceCreateRegisterAttribute(dto);
			MockRegisterCacheGetRegisterData(dto.RegisterId);

			ObjectsCharacteristicsService.AddCharacteristic(dto);

			ObjectCharacteristicsRepository.Verify(
				x => x.CreateOrUpdateCharacteristicSetting(It.IsAny<long>(), It.IsAny<bool>(), It.IsAny<bool>(),
					It.IsAny<bool>(),It.IsAny<bool>()), Times.Once);
		}

		private void MockRegisterAttributeServiceCreateRegisterAttribute(CharacteristicDto dto)
		{
			RegisterAttributeService
				.Setup(x => x.CreateRegisterAttribute(dto.Name, dto.RegisterId, dto.Type, false, dto.ReferenceId, true))
				.Returns(new OMAttribute() {Id = RandomGenerator.GenerateRandomInteger()});
		}

		private void MockRegisterCacheGetRegisterData(long registerId,
			AllpriPartitioningType? allpriPartitioningType = null)
		{
			RegisterCacheWrapper.Setup(x => x.GetRegisterData((int)registerId)).Returns(new RegisterData(){AllpriPartitioning = allpriPartitioningType});
		}
	}
}
