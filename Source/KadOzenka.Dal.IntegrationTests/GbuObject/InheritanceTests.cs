using System.Collections.Generic;
using System.Linq;
using KadOzenka.Common.Tests.Consts;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Integration._Builders.GbuObject;
using KadOzenka.Dal.Integration._Builders.Task;
using KadOzenka.Dal.Integration.GbuObject;
using NUnit.Framework;
using ObjectModel.Directory;

namespace KadOzenka.Dal.IntegrationTests.GbuObject
{
	public class InheritanceTests : BaseGbuObjectTests
	{
		[Test]
		public void Can_Inherit_BuildToFlat()
		{
			var task = new TaskBuilder().Build();
			var childObject = new GbuObjectBuilder().Build();
			var parentObject = new GbuObjectBuilder().Build();
			var unit = new UnitBuilder().Task(task).Object(childObject).Type(PropertyTypes.Pllacement).Build();

			var dateForAttributes = unit.CreationDate.GetValueOrDefault().AddDays(-1);
			var parentCadastralNumberAttribute = new GbuObjectAttributeBuilder()
				.Attribute(EgrnAttributes.CadastralNumber.Id)
				.Object(childObject.Id)
				.OtAndSDates(dateForAttributes)
				.Value(parentObject.CadastralNumber).Build();
			var attributeToCopy = new GbuObjectAttributeBuilder()
				.Attribute(EgrnAttributes.Address.Id)
				.Object(parentObject.Id)
				.OtAndSDates(dateForAttributes)
				.Build();


			var settings = new GbuInheritanceAttributeSettings
			{
				TaskFilter = new List<long> { task.Id },
				BuildToFlat = true,
				ParentCadastralNumberAttribute = parentCadastralNumberAttribute.AttributeId,
				Attributes = new List<long> { attributeToCopy.AttributeId }
			};
			new GbuObjectInheritanceAttribute(settings).Run();


			var copiedAttributes = GetAttributeValue(EgrnAttributes.Address, childObject.Id, attributeToCopy.ChangeDocId);
			Assert.That(copiedAttributes.Count, Is.EqualTo(1));
			var copiedAttribute = copiedAttributes.First();
			Assert.That(copiedAttribute, Is.Not.Null, attributeToCopy.Id.ToString);
			Assert.That(copiedAttribute.Value, Is.EqualTo(attributeToCopy.StringValue));
		}
	}
}