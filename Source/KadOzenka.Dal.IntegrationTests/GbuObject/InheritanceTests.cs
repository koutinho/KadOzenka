using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Common.Tests.Consts;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Integration._Builders.GbuObject;
using KadOzenka.Dal.Integration._Builders.Task;
using KadOzenka.Dal.Integration.GbuObject;
using NUnit.Framework;
using ObjectModel.Directory;
using ObjectModel.Gbu;

namespace KadOzenka.Dal.IntegrationTests.GbuObject
{
	public class InheritanceTests : BaseGbuObjectTests
	{
		[Test]
		public void Can_Inherit_BuildToFlat_From_One_Task_With_One_Unit()
		{
			var childType = PropertyTypes.Pllacement;
			var task = new TaskBuilder().Tour(Tour.Id).Document(Document.Id).Build();
			var childObject = new GbuObjectBuilder().Type(childType).Build();
			var parentObject = new GbuObjectBuilder().Type(PropertyTypes.Building).Build();
			var unit = new UnitBuilder().Task(task).Object(childObject).Type(childType).Build();

			var dateForAttributes = unit.CreationDate.GetValueOrDefault().AddDays(-1);
			var parentCadastralNumberAttribute = EgrnAttributes.CadastralNumber;
			new GbuObjectAttributeBuilder()
				.Attribute(parentCadastralNumberAttribute.Id)
				.Object(childObject.Id)
				.OtAndSDates(dateForAttributes)
				.Value(parentObject.CadastralNumber).Build();
			var attributeCopyFrom = new GbuObjectAttributeBuilder()
				.Attribute(EgrnAttributes.Address.Id)
				.Object(parentObject.Id)
				.OtAndSDates(dateForAttributes)
				.Build();
			var attributeCopyTo = EgrnAttributes.AddressOrLocation;


			var settings = new GbuInheritanceAttributeSettings
			{
				TaskFilter = new List<long> { task.Id },
				BuildToFlat = true,
				ParentCadastralNumberAttribute = parentCadastralNumberAttribute.Id,
				Attributes = new List<AttributeMapping>
				{
					new AttributeMapping{IdFrom = attributeCopyFrom.AttributeId, IdTo = attributeCopyTo.Id}
				}
			};
			new GbuObjectInheritanceAttribute(settings).Run();


			CheckCopiedAttribute(attributeCopyTo, childObject, attributeCopyFrom);
		}


		#region Support Methods

		private void CheckCopiedAttribute(EgrnAttributeForTest attributeCopyTo, OMMainObject firstChildObject,
			GbuObjectAttribute attributeCopyFrom)
		{
			var copiedAttributes = GetAttributeValue(attributeCopyTo, firstChildObject.Id, attributeCopyFrom.ChangeDocId);
			Assert.That(copiedAttributes.Count, Is.EqualTo(1));

			var copiedAttribute = copiedAttributes.First();
			Assert.That(copiedAttribute, Is.Not.Null, attributeCopyFrom.Id.ToString);
			Assert.That(copiedAttribute.Value, Is.EqualTo(attributeCopyFrom.StringValue));
			Assert.That(copiedAttribute.S, Is.EqualTo(attributeCopyFrom.S).Within(TimeSpan.FromMilliseconds(50000)));
			Assert.That(copiedAttribute.Ot, Is.EqualTo(attributeCopyFrom.Ot).Within(TimeSpan.FromMilliseconds(50000)));
		}

		#endregion
	}
}