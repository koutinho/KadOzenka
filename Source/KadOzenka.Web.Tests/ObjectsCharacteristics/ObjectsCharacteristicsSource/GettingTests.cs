using KadOzenka.Common.Tests;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using KadOzenka.Web.Models.ObjectsCharacteristics;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace KadOzenka.Web.Tests.ObjectsCharacteristics.ObjectsCharacteristicsSource
{
	public class GettingTests : BaseObjectCharacteristicsTests
	{
		[Test]
		public void Can_Get_View_For_New_Source()
		{
			var result = ObjectsCharacteristicsController.AddSource();

			var view = result as ViewResult;
			Assert.IsNotNull(view);
			Assert.IsNotNull(view.Model);
			Assert.IsTrue(view.Model is SourceModel);
			Assert.AreEqual(-1, ((SourceModel)view.Model).RegisterId);
			Assert.IsTrue(view.ViewName == "~/Views/ObjectsCharacteristics/EditSource.cshtml");
		}

		[Test]
		public void Can_Get_View_For_Existed_Source()
		{
			var registerId = RandomGenerator.GenerateRandomInteger();
			var registerDescription = RandomGenerator.GetRandomString();
			ObjectsCharacteristicsSourceService.Setup(x => x.GetSource(registerId))
				.Returns(new SourceDto {RegisterId = registerId, RegisterDescription = registerDescription});

			var result = ObjectsCharacteristicsController.EditSource(registerId);

			var view = result as ViewResult;
			Assert.IsNotNull(view);
			Assert.IsNotNull(view.Model);
			Assert.IsTrue(view.Model is SourceModel);
			Assert.AreEqual(registerId, ((SourceModel)view.Model).RegisterId);
			Assert.AreEqual(registerDescription, ((SourceModel)view.Model).Name);
		}
	}
}
