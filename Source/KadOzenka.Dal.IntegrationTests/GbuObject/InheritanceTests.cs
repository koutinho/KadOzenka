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
			var parentCadastralNumberAttribute = new GbuObjectAttributeBuilder()
				.Attribute(EgrnAttributes.CadastralNumber.Id)
				.Object(childObject.Id)
				.OtAndSDates(dateForAttributes)
				.Value(parentObject.CadastralNumber).Build();
			var attributeCopyFrom = new GbuObjectAttributeBuilder()
				.Attribute(EgrnAttributes.Address.Id)
				.Object(parentObject.Id)
				.OtAndSDates(dateForAttributes)
				.Build();
			var attributeCopyTo = EgrnAttributes.AddressOrLocation;


			RanInheritance(new List<long> {task.Id}, parentCadastralNumberAttribute.AttributeId,
				attributeCopyFrom.AttributeId, attributeCopyTo.Id);


			CheckCopiedAttribute(attributeCopyTo, childObject.Id, attributeCopyFrom);
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
			var parentCadastralNumberAttributeBuilder = new GbuObjectAttributeBuilder()
				.Attribute(EgrnAttributes.CadastralNumber.Id)
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


			RanInheritance(new List<long> {task.Id}, parentCadastralNumberAttributeBuilder.AttributeId,
				attributeCopyFrom.AttributeId, attributeCopyTo.Id);


			CheckCopiedAttribute(attributeCopyTo, firstChildObject.Id, attributeCopyFrom);
			CheckCopiedAttribute(attributeCopyTo, secondChildObject.Id, attributeCopyFrom);
		}

		[Test]
		public void Can_Inherit_BuildToFlat_From_Several_Tasks()
		{
			var childType = PropertyTypes.Pllacement;
			var firstTask = new TaskBuilder().Tour(Tour.Id).Document(FirstDocument.Id).Build();
			var secondTask = new TaskBuilder().Tour(Tour.Id).Document(SecondDocument.Id).Build();

			var objectBuilder = new GbuObjectBuilder().Type(childType);
			var firstChildObject = objectBuilder.ShallowCopy().Build();
			var secondChildObject = objectBuilder.ShallowCopy().Build();
			var parentObject = new GbuObjectBuilder().Type(PropertyTypes.Building).Build();

			var firstUnitCreationDate = DateTime.Now.AddDays(1);
			var secondUnitCreationDate = DateTime.Now.AddDays(2);
			new UnitBuilder().Type(childType).Task(firstTask).CreationDate(firstUnitCreationDate).Object(firstChildObject).Build();
			new UnitBuilder().Type(childType).Task(secondTask).CreationDate(secondUnitCreationDate).Object(secondChildObject).Build();

			var parentCadastralNumberAttributeBuilder = new GbuObjectAttributeBuilder()
				.Attribute(EgrnAttributes.CadastralNumber.Id)
				.Value(parentObject.CadastralNumber);
			parentCadastralNumberAttributeBuilder.Object(firstChildObject.Id).OtAndSDates(firstUnitCreationDate).Build();
			parentCadastralNumberAttributeBuilder.Object(secondChildObject.Id).OtAndSDates(secondUnitCreationDate).Build();
			
			var attributeCopyFrom = new GbuObjectAttributeBuilder()
				.Attribute(EgrnAttributes.Address.Id)
				.Object(parentObject.Id);
			var firstParentAttribute = attributeCopyFrom.ShallowCopy().OtAndSDates(firstUnitCreationDate).Build();
			var secondParentAttribute = attributeCopyFrom.ShallowCopy().OtAndSDates(secondUnitCreationDate).Build();
			var attributeCopyTo = EgrnAttributes.AddressOrLocation;


			RanInheritance(new List<long> {firstTask.Id, secondTask.Id},
				parentCadastralNumberAttributeBuilder.AttributeId, attributeCopyFrom.AttributeId, attributeCopyTo.Id);


			CheckCopiedAttribute(attributeCopyTo, firstChildObject.Id, firstParentAttribute);
			CheckCopiedAttribute(attributeCopyTo, secondChildObject.Id, secondParentAttribute);
		}


		#region Support Methods

		private static void RanInheritance(List<long> taskIds, long parentCadastralNumberAttributeId,
			long attributeCopyFromId, long attributeIdCopyTo)
		{
			var settings = new GbuInheritanceAttributeSettings
			{
				TaskFilter = taskIds,
				BuildToFlat = true,
				ParentCadastralNumberAttribute = parentCadastralNumberAttributeId,
				Attributes = new List<AttributeMapping>
				{
					new AttributeMapping {IdFrom = attributeCopyFromId, IdTo = attributeIdCopyTo}
				}
			};

			new GbuObjectInheritanceAttribute(settings).Run();
		}

		private void CheckCopiedAttribute(EgrnAttributeForTest attributeCopyTo, long childObjectId,
			GbuObjectAttribute attributeCopyFrom)
		{
			var copiedAttributes = GetAttributeValue(attributeCopyTo, childObjectId, attributeCopyFrom.ChangeDocId);
			Assert.That(copiedAttributes.Count, Is.EqualTo(1));

			var copiedAttribute = copiedAttributes.First();
			Assert.That(copiedAttribute, Is.Not.Null, attributeCopyFrom.Id.ToString);
			Assert.That(copiedAttribute.Value, Is.EqualTo(attributeCopyFrom.StringValue));
			//сравниваем даты с учетом +- 50 сек
			Assert.That(copiedAttribute.S, Is.EqualTo(attributeCopyFrom.S).Within(TimeSpan.FromMilliseconds(50000)));
			Assert.That(copiedAttribute.Ot, Is.EqualTo(attributeCopyFrom.Ot).Within(TimeSpan.FromMilliseconds(50000)));
		}

		#endregion
	}
}