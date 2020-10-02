using System.Collections.Generic;
using ObjectModel.Directory;

namespace KadOzenka.Dal.GbuObject.Dto
{
	/// <summary>
	/// Настройки переноса атрибутов из ГБУшной части в КОшную
	/// </summary>
	public class GbuExportAttributeSettings
	{
		/// <summary>
		/// Список заданий на оценку
		/// </summary>
		public List<long> TaskFilter { get; set; }

		/// <summary>
		/// Список сопоставленных атрибутов
		/// </summary>
		public List<ExportAttributeItem> Attributes { get; set; }

		/// <summary>
		/// Тип объектов
		/// </summary>
		public ObjectTypeExtended ObjType { get; set; }

		/// <summary>
		/// Дополнительные фильтры для объектов ОКС
		/// </summary>
		public OksAdditionalFilters OksAdditionalFilters { get; set; }

		public GbuExportAttributeSettings()
		{
			OksAdditionalFilters = new OksAdditionalFilters();
		}
	}

	/// <summary>
	/// Соответствие атрибутов ГБУ и КО
	/// </summary>
	public class ExportAttributeItem
	{
		/// <summary>
		/// Идентификатор фактора КО
		/// </summary>
		public long IdAttributeKO;
		/// <summary>
		/// Идентификатор фактора ГБУ
		/// </summary>
		public long IdAttributeGBU;
	}

	public class OksAdditionalFilters
	{
		public bool IsBuildings { get; set; }
		public bool IsPlacements { get; set; }
		public bool IsUncompletedBuildings { get; set; }
		public bool IsConstructions { get; set; }
		public PlacementPurpose PlacementPurpose { get; set; }

		public List<PropertyTypes> ObjectTypes
		{
			get
			{
				var objectTypes = new List<PropertyTypes>();

				if (IsBuildings)
					objectTypes.Add(PropertyTypes.Building);

				if (IsPlacements)
					objectTypes.Add(PropertyTypes.Pllacement);

				if (IsUncompletedBuildings)
					objectTypes.Add(PropertyTypes.UncompletedBuilding);

				if (IsConstructions)
					objectTypes.Add(PropertyTypes.Construction);

				return objectTypes;
			}
		}
	}
}
