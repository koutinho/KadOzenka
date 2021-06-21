using System;
using System.Linq.Expressions;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.ObjectsCharacteristics.Exceptions;
using KadOzenka.Dal.ObjectsCharacteristics.Resources;
using Moq;
using NUnit.Framework;
using ObjectModel.Core.Register;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.ObjectsCharacteristics.ObjectsCharacteristicsSource
{
	[TestFixture]
	public class GetSourceTests: BaseObjectCharacteristicsTests
	{
		[Test]
		public void Can_Get_Source()
		{
			var registerId = RandomGenerator.GenerateRandomInteger();
			var registerDescription = RandomGenerator.GetRandomString();
			RegisterService.Setup(x => x.GetRegister(registerId))
				.Returns(new OMRegister {RegisterId = registerId, RegisterDescription = registerDescription});
			ObjectCharacteristicsRepository
				.Setup(x => x.GetEntityByCondition(It.IsAny<Expression<Func<OMObjectsCharacteristicsRegister, bool>>>(),
					null)).Returns(new OMObjectsCharacteristicsRegister());

			var source = ObjectsCharacteristicsSourceService.GetSource(registerId);

			Assert.AreEqual(registerId, source.RegisterId);
			Assert.AreEqual(registerDescription, source.RegisterDescription);
		}

		[Test]
		public void CanNot_Get_Source_For_NonExisted_Register()
		{
			var registerId = RandomGenerator.GenerateRandomInteger();
			RegisterService.Setup(x => x.GetRegister(registerId))
				.Returns((OMRegister) null);

			var ex = Assert.Throws<SourceDoesNotExistException>(() => ObjectsCharacteristicsSourceService.GetSource(registerId));

			Assert.AreEqual(string.Format(Messages.SourceDoesNotExist, registerId), ex.Message);
		}
	}
}
