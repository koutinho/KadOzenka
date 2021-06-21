using System.Collections.Generic;
using System.Linq;
using Core.Register.RegisterEntities;
using KadOzenka.Common.Tests;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace KadOzenka.Web.UnitTests.Tours
{
	public class AttributesTests : BaseTourTests
	{
		[Test]
		public void Can_Get_View_With_Attributes_Settings()
		{
			var registerId = RandomGenerator.GenerateRandomInteger();
			var attributeName = RandomGenerator.GetRandomString();
			var gbuRegistersIds = new List<long> { registerId };
			var registers = new Dictionary<int, RegisterData>
			{
				{
					registerId, new RegisterData {Id = registerId}
				}
			};
			var registerAttributes = new Dictionary<long, RegisterAttribute>
			{
				{
					registers.First().Key, new RegisterAttribute { RegisterId = registers.First().Key, Name = attributeName}
				}
			};
			
			GbuObjectService.Setup(x => x.GetGbuRegistersIds()).Returns(gbuRegistersIds);
			RegisterCacheWrapper.Setup(x => x.GetRegistersCache()).Returns(registers);
			RegisterCacheWrapper.Setup(x => x.GetRegisterAttributesCache()).Returns(registerAttributes);

			var result = TourController.TourAttributeSettings();
			var view = result as ViewResult;
			var attributes = (view?.ViewData["TreeAttributes"] as IEnumerable<DropDownTreeItemModel>)?.ToList();

			Assert.IsNotNull(attributes);
			Assert.That(attributes.Count, Is.EqualTo(registerAttributes.Count));
			Assert.That(attributes.SelectMany(x => x.Items).Select(x => x.Text).Contains(attributeName), string.Join(";\n", attributes.Select(x => x.Text)));
		}
	}
}