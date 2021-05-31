using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Shared.Misc;
using KadOzenka.Common.Tests;
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
			
			var egrnRegisterId = 2;
			var egrnAttributes = RegisterCache.RegisterAttributes.Values.Where(x => x.RegisterId == egrnRegisterId && x.Type == RegisterAttributeType.STRING).ToList();
			var parentCadastralNumberAttributeId = egrnAttributes.First().Id;
			var attributesToCopy = egrnAttributes.Where(x => x.Id != parentCadastralNumberAttributeId).Take(2)
				.Select(x => new
				{
					x.Id, 
					Value = RandomGenerator.GetRandomString()
				}).ToList();

			var date = unit.CreationDate.Value.AddDays(-1);
			var parentCadastralNumberAttribute = new GbuObjectAttribute
			{
				Id = -1,
				AttributeId = parentCadastralNumberAttributeId,
				ObjectId = childObject.Id,
				ChangeDocId = -1,
				S = date,
				ChangeUserId = -1,
				ChangeDate = DateTime.Now,
				Ot = date,
				StringValue = parentObjectObject.CadastralNumber
			};
			parentCadastralNumberAttribute.Save();
			attributesToCopy.ForEach(x =>
			{
				var cur = new GbuObjectAttribute
				{
					Id = -1,
					AttributeId = x.Id,
					ObjectId = parentObjectObject.Id,
					ChangeDocId = -1,
					S = date,
					ChangeUserId = -1,
					ChangeDate = DateTime.Now,
					Ot = date,
					StringValue = x.Value
				};
				cur.Save();
			});

			var attributeIdsToCopy = attributesToCopy.Select(x => x.Id).ToList();
			var settings = new GbuInheritanceAttributeSettings
			{
				TaskFilter = new List<long> { task.Id },
				BuildToFlat = true,
				ParentCadastralNumberAttribute = parentCadastralNumberAttributeId,
				Attributes = attributeIdsToCopy
			};
			new GbuObjectInheritanceAttribute(settings).Run();


			var copiedAttributes = new GbuObjectService().GetAllAttributes(childObject.Id, null, attributeIdsToCopy, date);
			Assert.That(copiedAttributes.Count, Is.EqualTo(attributesToCopy.Count));
			attributesToCopy.ForEach(attributeInfo =>
			{
				var copiedAttribute = copiedAttributes.FirstOrDefault(x => x.AttributeId == attributeInfo.Id);
				Assert.That(copiedAttribute, Is.Not.Null, attributeInfo.Id.ToString);
				Assert.That(copiedAttribute.StringValue, Is.EqualTo(attributeInfo.Value));
			});
		}
	}
}