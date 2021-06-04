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
		private OMTask _task;
		private OMMainObject _childObject;
		private OMMainObject _parentObject;
		private OMUnit _unit;

		[SetUp]
		public void InheritanceSetUp()
		{
			var childType = PropertyTypes.Pllacement;
			_task = new TaskBuilder().Tour(Tour.Id).Document(FirstDocument.Id).Build();
			_childObject = new GbuObjectBuilder().Type(childType).Build();
			_parentObject = new GbuObjectBuilder().Type(PropertyTypes.Building).Build();
			_unit = new UnitBuilder().Task(_task).Object(_childObject).Type(childType).Build();
		}


		[Test]
		public void Can_Inherit_BuildToFlat_From_One_Task_With_One_Unit()
		{
			var dateForAttributes = _unit.CreationDate.GetValueOrDefault().AddDays(-1);
			var parentCadastralNumberAttribute = new GbuObjectAttributeBuilder()
				.Attribute(EgrnAttributes.CadastralNumber.Id)
				.Object(_childObject.Id)
				.OtAndSDates(dateForAttributes)
				.Value(_parentObject.CadastralNumber).Build();
			var copyAttributeToBuilder = new GbuObjectAttributeBuilder().Object(_parentObject.Id).OtAndSDates(dateForAttributes);
			var firstAttributeCopyFrom = copyAttributeToBuilder.ShallowCopy().Attribute(EgrnAttributes.Address.Id).Build();
			var secondAttributeCopyFrom = copyAttributeToBuilder.ShallowCopy().Attribute(EgrnAttributes.Square.Id).Build();
			var firstAttributeCopyTo = EgrnAttributes.AddressOrLocation;
			var secondAttributeCopyTo = EgrnAttributes.Square;


			RunInheritance(new List<long> { _task.Id}, parentCadastralNumberAttribute.AttributeId,
				new List<long> { firstAttributeCopyFrom.AttributeId, secondAttributeCopyFrom.AttributeId },
				new List<long> { firstAttributeCopyTo.Id, secondAttributeCopyTo.Id });


			CheckCopiedAttribute(firstAttributeCopyTo, _childObject.Id, firstAttributeCopyFrom);
			CheckCopiedAttribute(secondAttributeCopyTo, _childObject.Id, secondAttributeCopyFrom);
		}

		[Test]
		public void Can_Inherit_BuildToFlat_From_One_Task_With_Several_Units()
		{
			var childType = PropertyTypes.Pllacement;
			
			var objectBuilder = new GbuObjectBuilder().Type(childType);
			var firstChildObject = objectBuilder.ShallowCopy().Build();
			var secondChildObject = objectBuilder.ShallowCopy().Build();

			var unitsCreationDate = DateTime.Now;
			var unitBuilder = new UnitBuilder().Task(_task).Type(childType).CreationDate(unitsCreationDate);
			unitBuilder.Object(firstChildObject).ShallowCopy().Build();
			unitBuilder.Object(secondChildObject).ShallowCopy().Build();

			var dateForAttributes = unitsCreationDate.AddDays(-1);
			var parentCadastralNumberAttributeBuilder = new GbuObjectAttributeBuilder()
				.Attribute(EgrnAttributes.CadastralNumber.Id)
				.OtAndSDates(dateForAttributes)
				.Value(_parentObject.CadastralNumber);
			parentCadastralNumberAttributeBuilder.Object(firstChildObject.Id).Build();
			parentCadastralNumberAttributeBuilder.Object(secondChildObject.Id).Build();
			var attributeCopyFrom = new GbuObjectAttributeBuilder()
				.Attribute(EgrnAttributes.Address.Id)
				.Object(_parentObject.Id)
				.OtAndSDates(dateForAttributes)
				.Build();
			var attributeCopyTo = EgrnAttributes.AddressOrLocation;


			RunInheritance(new List<long> { _task.Id}, parentCadastralNumberAttributeBuilder.AttributeId,
				attributeCopyFrom.AttributeId, attributeCopyTo.Id);


			CheckCopiedAttribute(attributeCopyTo, firstChildObject.Id, attributeCopyFrom);
			CheckCopiedAttribute(attributeCopyTo, secondChildObject.Id, attributeCopyFrom);
		}

		[Test]
		public void Can_Inherit_BuildToFlat_From_Several_Tasks()
		{
			var childType = PropertyTypes.Pllacement;
			var secondTask = new TaskBuilder().Tour(Tour.Id).Document(SecondDocument.Id).Build();

			var objectBuilder = new GbuObjectBuilder().Type(childType);
			var firstChildObject = objectBuilder.ShallowCopy().Build();
			var secondChildObject = objectBuilder.ShallowCopy().Build();

			var firstUnitCreationDate = DateTime.Now.AddDays(1);
			var secondUnitCreationDate = DateTime.Now.AddDays(2);
			new UnitBuilder().Type(childType).Task(_task).CreationDate(firstUnitCreationDate).Object(firstChildObject).Build();
			new UnitBuilder().Type(childType).Task(secondTask).CreationDate(secondUnitCreationDate).Object(secondChildObject).Build();

			var parentCadastralNumberAttributeBuilder = new GbuObjectAttributeBuilder()
				.Attribute(EgrnAttributes.CadastralNumber.Id)
				.Value(_parentObject.CadastralNumber);
			parentCadastralNumberAttributeBuilder.Object(firstChildObject.Id).OtAndSDates(firstUnitCreationDate).Build();
			parentCadastralNumberAttributeBuilder.Object(secondChildObject.Id).OtAndSDates(secondUnitCreationDate).Build();
			
			var attributeCopyFrom = new GbuObjectAttributeBuilder()
				.Attribute(EgrnAttributes.Address.Id)
				.Object(_parentObject.Id);
			var firstParentAttribute = attributeCopyFrom.ShallowCopy().OtAndSDates(firstUnitCreationDate).Build();
			var secondParentAttribute = attributeCopyFrom.ShallowCopy().OtAndSDates(secondUnitCreationDate).Build();
			var attributeCopyTo = EgrnAttributes.AddressOrLocation;


			RunInheritance(new List<long> { _task.Id, secondTask.Id},
				parentCadastralNumberAttributeBuilder.AttributeId, attributeCopyFrom.AttributeId, attributeCopyTo.Id);


			CheckCopiedAttribute(attributeCopyTo, firstChildObject.Id, firstParentAttribute);
			CheckCopiedAttribute(attributeCopyTo, secondChildObject.Id, secondParentAttribute);
		}

		[Test]
		public void CanNot_Inherit_BuildToFlat_If_There_Are_No_Info_About_Parent_Cadastral_Number()
		{
			var dateForAttributes = _unit.CreationDate.GetValueOrDefault().AddDays(-1);
			var attributeCopyFrom = new GbuObjectAttributeBuilder()
				.Attribute(EgrnAttributes.Address.Id)
				.Object(_parentObject.Id)
				.OtAndSDates(dateForAttributes)
				.Build();
			var attributeCopyTo = EgrnAttributes.AddressOrLocation;


			RunInheritance(new List<long> { _task.Id }, EgrnAttributes.CadastralNumber.Id,
				attributeCopyFrom.AttributeId, attributeCopyTo.Id);


			var copiedAttributes = GetAttributeValue(attributeCopyTo, _childObject.Id, attributeCopyFrom.ChangeDocId);
			Assert.That(copiedAttributes.Count, Is.EqualTo(0));
		}

		
		#region Support Methods

		private void RunInheritance(List<long> taskIds, long parentCadastralNumberAttributeId,
			List<long> attributeIdsCopyFrom, List<long> attributeIdsCopyTo)
		{
			var mapping = new List<AttributeMapping>();
			for (var i = 0; i < attributeIdsCopyFrom.Count; i++)
			{
				mapping.Add(new AttributeMapping {IdFrom = attributeIdsCopyFrom[i], IdTo = attributeIdsCopyTo[i]});
			}

			DoProcess(taskIds, parentCadastralNumberAttributeId, mapping);
		}

		private void RunInheritance(List<long> taskIds, long parentCadastralNumberAttributeId,
			long attributeIdCopyFrom, long attributeIdCopyTo)
		{
			var mapping = new List<AttributeMapping>
			{
				new AttributeMapping {IdFrom = attributeIdCopyFrom, IdTo = attributeIdCopyTo}
			};

			DoProcess(taskIds, parentCadastralNumberAttributeId, mapping);
		}

		private void DoProcess(List<long> taskIds, long parentCadastralNumberAttributeId, List<AttributeMapping> mapping)
		{
			var settings = new GbuInheritanceAttributeSettings
			{
				TaskFilter = taskIds,
				BuildToFlat = true,
				ParentCadastralNumberAttribute = parentCadastralNumberAttributeId,
				Attributes = mapping
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
			Assert.That(copiedAttribute.Value, Is.EqualTo(attributeCopyFrom.GetValue()));
			//сравниваем даты с учетом +- 50 сек
			Assert.That(copiedAttribute.S, Is.EqualTo(attributeCopyFrom.S).Within(TimeSpan.FromMilliseconds(50000)));
			Assert.That(copiedAttribute.Ot, Is.EqualTo(attributeCopyFrom.Ot).Within(TimeSpan.FromMilliseconds(50000)));
		}

		#endregion
	}
}