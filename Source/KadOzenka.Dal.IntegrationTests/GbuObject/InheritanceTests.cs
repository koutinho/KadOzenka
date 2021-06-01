using System;
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
using ObjectModel.Gbu;
using ObjectModel.KO;

namespace KadOzenka.Dal.IntegrationTests.GbuObject
{
	public class InheritanceTests : BaseGbuObjectTests
	{
		[Test]
		public void Can_Inherit_BuildToFlat_From_One_Task_With_One_Unit()
		{
			var childType = PropertyTypes.Pllacement;
			var task = new TaskBuilder().Tour(Tour.Id).Document(FirstDocument.Id).Build();
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


			RanInheritance(task, parentCadastralNumberAttribute, attributeCopyFrom, attributeCopyTo);


			CheckCopiedAttribute(attributeCopyTo, childObject, attributeCopyFrom);
		}

		[Test]
		public void Can_Inherit_BuildToFlat_From_One_Task_With_Several_Units()
		{
			var childType = PropertyTypes.Pllacement;
			var task = new TaskBuilder().Tour(Tour.Id).Document(FirstDocument.Id).Build();
			
			var objectBuilder = new GbuObjectBuilder().Type(childType);
			var firstChildObject = objectBuilder.ShallowCopy().Build();
			var secondChildObject = objectBuilder.ShallowCopy().Build();
			var parentObject = new GbuObjectBuilder().Type(PropertyTypes.Building).Build();
			
			var unitsCreationDate = DateTime.Now;
			var unitBuilder = new UnitBuilder().Task(task).Type(childType).CreationDate(unitsCreationDate);
			unitBuilder.Object(firstChildObject).ShallowCopy().Build();
			unitBuilder.Object(secondChildObject).ShallowCopy().Build();

			var dateForAttributes = unitsCreationDate.AddDays(-1);
			var parentCadastralNumberAttribute = EgrnAttributes.CadastralNumber;
			var parentCadastralNumberAttributeBuilder = new GbuObjectAttributeBuilder()
				.Attribute(parentCadastralNumberAttribute.Id)
				.OtAndSDates(dateForAttributes)
				.Value(parentObject.CadastralNumber);
			parentCadastralNumberAttributeBuilder.Object(firstChildObject.Id).Build();
			parentCadastralNumberAttributeBuilder.Object(secondChildObject.Id).Build();
			var attributeCopyFrom = new GbuObjectAttributeBuilder()
				.Attribute(EgrnAttributes.Address.Id)
				.Object(parentObject.Id)
				.OtAndSDates(dateForAttributes)
				.Build();
			var attributeCopyTo = EgrnAttributes.AddressOrLocation;


			RanInheritance(task, parentCadastralNumberAttribute, attributeCopyFrom, attributeCopyTo);


			CheckCopiedAttribute(attributeCopyTo, firstChildObject, attributeCopyFrom);
			CheckCopiedAttribute(attributeCopyTo, secondChildObject, attributeCopyFrom);
		}


		#region Support Methods

		private static void RanInheritance(OMTask task, EgrnAttributeForTest parentCadastralNumberAttribute,
			GbuObjectAttribute attributeCopyFrom, EgrnAttributeForTest attributeCopyTo)
		{
			var settings = new GbuInheritanceAttributeSettings
			{
				TaskFilter = new List<long> {task.Id},
				BuildToFlat = true,
				ParentCadastralNumberAttribute = parentCadastralNumberAttribute.Id,
				Attributes = new List<AttributeMapping>
				{
					new AttributeMapping {IdFrom = attributeCopyFrom.AttributeId, IdTo = attributeCopyTo.Id}
				}
			};
			new GbuObjectInheritanceAttribute(settings).Run();
		}

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