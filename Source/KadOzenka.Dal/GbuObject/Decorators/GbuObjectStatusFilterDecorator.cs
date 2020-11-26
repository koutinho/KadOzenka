using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.Registers.GbuRegistersServices;

namespace KadOzenka.Dal.GbuObject.Decorators
{
	public class GbuObjectStatusFilterDecorator<T> : ADecorator<T> where T : ItemBase
	{
		public List<UnitChangeStatus> Statuses { get; set; }
		public RosreestrRegisterService RosreestrRegisterService { get; set; }

		public GbuObjectStatusFilterDecorator(AItemsGetter<T> comp, List<UnitChangeStatus> statuses) : base(comp)
		{
			Statuses = statuses;
			RosreestrRegisterService = new RosreestrRegisterService();
		}

		public override List<T> GetItems()
		{
			var allItems = base.GetItems();

			if (Statuses == null || Statuses.Count == 0)
				return allItems;

			var attributeIds = GetAttributeIds();
			var statusAttributes = new GbuObjectService().GetAllAttributes(
				allItems.Select(x => x.ObjectId).Distinct().ToList(),
				new List<long> { RosreestrRegisterService.RegisterId },
				attributeIds,
				DateTime.Now.GetEndOfTheDay(), isLight: true)
				.GroupBy(x => x.ObjectId, (k, g) => new 
				{
					ObjectId = k,
					Attributes = g.ToList()
				}).ToList();

			var resultObjectIds = new List<long>();
			foreach (var statusAttribute in statusAttributes)
			{
				if (statusAttribute.Attributes.Count == attributeIds.Count && statusAttribute.Attributes.All(x => x?.GetValueInString() == "1"))
				{
					resultObjectIds.Add(statusAttribute.ObjectId);
				}
			}

			return allItems.Where(x => resultObjectIds.Contains(x.ObjectId)).ToList();
		}

		private List<long> GetAttributeIds()
		{
			var attributeIds = new List<long>();
			Statuses.ForEach(status =>
			{
				switch (status)
				{
					case UnitChangeStatus.Group:
						attributeIds.Add(RosreestrRegisterService.GetPGroupAttribute().Id);
						break;
					case UnitChangeStatus.Fs:
						attributeIds.Add(RosreestrRegisterService.GetPFsAttribute().Id);
						break;
					case UnitChangeStatus.WallMaterial:
						attributeIds.Add(RosreestrRegisterService.GetPWallMaterialAttribute().Id);
						break;
					case UnitChangeStatus.BuildYear:
						attributeIds.Add(RosreestrRegisterService.GetPBuildYearAttribute().Id);
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(status), status, null);
				}
			});
			return attributeIds;
		}
	}
}
