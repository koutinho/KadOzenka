using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Web.Models.Tour;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Web.Tests.Tours
{
	public class AttributesTests : BaseTourTests
	{
		[Test]
		public void Can_Get_View_With_Attributes_Settings()
		{
			var gbuAttributes = new List<GbuAttributesTreeDto>
			{
				new GbuAttributesTreeDto
				{
					Text = RandomGenerator.GetRandomString(),
					Value = RandomGenerator.GetRandomString(),
					Items = new List<SelectListItem>()
				}
			};
			GbuObjectService.Setup(x => x.GetGbuAttributesTree()).Returns(gbuAttributes);

			var result = TourController.TourAttributeSettings();
			var view = result as ViewResult;
			var attributes = (view?.ViewData["TreeAttributes"] as IEnumerable<DropDownTreeItemModel>)?.ToList();

			Assert.IsNotNull(attributes);
			Assert.That(attributes.Count, Is.EqualTo(gbuAttributes.Count));
		}
	}
}