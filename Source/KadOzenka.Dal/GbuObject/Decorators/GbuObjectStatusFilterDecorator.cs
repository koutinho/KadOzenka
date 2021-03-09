using System;
using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.GbuObject.Entities;
using Serilog;

namespace KadOzenka.Dal.GbuObject.Decorators
{
	public class GbuObjectStatusFilterDecorator<T> : ADecorator<T> where T : ItemBase
	{
		public List<ObjectChangeStatus> Statuses { get; set; }
		public DateTime ActualDate { get; set; }

		public GbuObjectStatusFilterDecorator(AItemsGetter<T> itemsGetter, ILogger logger, List<ObjectChangeStatus> statuses, DateTime actualDate) 
			: base(itemsGetter, logger)
		{
			Statuses = statuses;
			ActualDate = actualDate;
		}

		public override List<T> GetItems()
		{
			var allItems = base.GetItems();
			if (allItems.Count == 0 || Statuses == null || Statuses.Count == 0)
				return allItems;

			Logger.ForContext("InputParameters", Statuses, true).Debug($"Начало фильтрации по статусам ОН. Количество объектов до фильтрации - {allItems.Count}");

			var attributeIds = GetAttributeIds();
			Logger.ForContext("AttributeIds", attributeIds, true).Debug("Найдены ИД соответствующих атрибутов Росреестра");
			
			var statusAttributesGrouping = GbuObjectService.GetAllAttributes(
				allItems.Select(x => x.ObjectId).ToList(),
				new List<long> { RosreestrRegisterService.RegisterId },
				attributeIds, ActualDate,
				attributesToDownload: new List<GbuColumnsToDownload> { GbuColumnsToDownload.Value })
				.GroupBy(x => x.ObjectId).ToList();
			Logger.Debug($"Найдено {statusAttributesGrouping.Count} ОН со значениями из Росреестра");

			var resultObjectIds = new List<long>();
			foreach (var statusAttribute in statusAttributesGrouping)
			{
				if (statusAttribute.Any(x => x?.GetValueInString() == "1"))
				{
					resultObjectIds.Add(statusAttribute.Key);
				}
			}
			Logger.Debug($"После фильтрации по статусам осталось {resultObjectIds.Count} объектов");

			return allItems.Where(x => resultObjectIds.Contains(x.ObjectId)).ToList();
		}

		private List<long> GetAttributeIds()
		{
			var attributeIds = new List<long>();

			Statuses.ForEach(status =>
			{
				switch (status)
				{
					case ObjectChangeStatus.Group:
						attributeIds.Add(RosreestrRegisterService.GetPGroupAttribute().Id);
						break;
					case ObjectChangeStatus.Fs:
						attributeIds.Add(RosreestrRegisterService.GetPFsAttribute().Id);
						break;
					case ObjectChangeStatus.WallMaterial:
						attributeIds.Add(RosreestrRegisterService.GetPWallMaterialAttribute().Id);
						break;
					case ObjectChangeStatus.BuildYear:
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
