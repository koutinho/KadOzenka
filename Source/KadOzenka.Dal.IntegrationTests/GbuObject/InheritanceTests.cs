using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Shared.Misc;
using KadOzenka.Common.Tests;
using KadOzenka.Common.Tests.Builders.GbuObject;
using KadOzenka.Common.Tests.Consts;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Integration._Builders.GbuObject;
using KadOzenka.Dal.Integration._Builders.Task;
using NUnit.Framework;
using ObjectModel.Directory;

namespace KadOzenka.Dal.Integration.GbuObject
{
	public class InheritanceTests : BaseGbuObjectTests
	{
		//TODO отрефакторить
		[Test]
		public void Can_Inherit_BuildToFlat()
		{
			var task = new TaskBuilder().Build();
			var parentObjectObject = new GbuObjectBuilder().Build();
			var childObject = new GbuObjectBuilder().Build();
			var unit = new UnitBuilder().Task(task).Object(childObject).Type(PropertyTypes.Pllacement).Build();

			var date = unit.CreationDate.Value.AddDays(-1);

			var parentCadastralNumberAttribute = new GbuObjectAttributeBuilder()
				.Attribute(EgrnAttributes.CadastralNumber.Id)
				.Object(childObject.Id).OtAndSDates(date).Value(parentObjectObject.CadastralNumber).Build();

			var attributeToCopy = new GbuObjectAttributeBuilder().Attribute(EgrnAttributes.Address.Id).Object(parentObjectObject.Id)
				.OtAndSDates(date).Build();

			var attributeIdsToCopy = new List<long> {attributeToCopy.AttributeId};
			var settings = new GbuInheritanceAttributeSettings
			{
				TaskFilter = new List<long> { task.Id },
				BuildToFlat = true,
				ParentCadastralNumberAttribute = parentCadastralNumberAttribute.AttributeId,
				Attributes = attributeIdsToCopy
			};
			new GbuObjectInheritanceAttribute(settings).Run();


			var copiedAttributes = new GbuObjectService().GetAllAttributes(childObject.Id, null, attributeIdsToCopy, date);
			Assert.That(copiedAttributes.Count, Is.EqualTo(1));
			var copiedAttribute = copiedAttributes.FirstOrDefault(x => x.AttributeId == attributeToCopy.AttributeId);
			Assert.That(copiedAttribute, Is.Not.Null, attributeToCopy.Id.ToString);
			Assert.That(copiedAttribute.StringValue, Is.EqualTo(attributeToCopy.StringValue));
		}
	}
}