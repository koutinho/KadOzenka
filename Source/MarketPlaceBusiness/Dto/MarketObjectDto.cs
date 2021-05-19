using System;
using ObjectModel.Directory;

namespace MarketPlaceBusiness.Dto
{
	public class MarketObjectDto
	{
		public long Id { get; set; }

		/// <summary>
		/// 10002300 Источник информации (MARKET)
		/// </summary>
		public string Market { get; set; }

		/// <summary>
		/// 10002300 Источник информации (справочный код) (MARKET_CODE)
		/// </summary>
		public MarketTypes Market_Code { get; set; }

		/// <summary>
		/// 10002700 Цена сделки/предложения (PRICE)
		/// </summary>
		public decimal? Price { get; set; }


		/// <summary>
		/// 10002701 Цена за кв. м ()
		/// </summary>
		public decimal? PricePerMeter { get; set; }

		/// <summary>
		/// 10003100 Адресный ориентир (ADDRESS)
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// 10004100 Номер этажа (FLOOR_NUMBER)
		/// </summary>
		public long? FloorNumber { get; set; }

		/// <summary>
		/// 10004300 Общая площадь (AREA)
		/// </summary>
		public decimal? Area { get; set; }

		/// <summary>
		/// 10004700 Год постройки (BUILDING_YEAR)
		/// </summary>
		public long? BuildingYear { get; set; }

		/// <summary>
		/// 10005400 Кадастровый номер (CADASTRAL_NUMBER)
		/// </summary>
		public string CadastralNumber { get; set; }

		/// <summary>
		/// 10005600 Кадастровый квартал (CADASTRAL_QUARTAL)
		/// </summary>
		public string CadastralQuartal { get; set; }

		/// <summary>
		/// 10007000 Сегмент рынка (PROPERTY_MARKET_SEGMENT)
		/// </summary>
		public string PropertyMarketSegment { get; set; }

		/// <summary>
		/// 10007000 Сегмент рынка (справочный код) (PROPERTY_MARKET_SEGMENT_CODE)
		/// </summary>
		public MarketSegment PropertyMarketSegment_Code { get; set; }

		/// <summary>
		/// 10007200 Класс качества (QUALITY_CLASS)
		/// </summary>
		public string QualityClass { get; set; }


		/// <summary>
		/// 10007200 Класс качества (справочный код) (QUALITY_CLASS_CODE)
		/// </summary>
		public QualityClass QualityClass_Code { get; set; }


		/// <summary>
		/// 10007700 Вид объекта недвижимости (PROPERTY_TYPETS_CIPJS)
		/// </summary>
		public string PropertyTypesCIPJS { get; set; }


		/// <summary>
		/// 10007700 Вид объекта недвижимости (справочный код) (PROPERTY_TYPETS_CIPJS_CODE)
		/// </summary>
		public PropertyTypesCIPJS PropertyTypesCIPJS_Code { get; set; }

		/// <summary>
		/// 10007800 Дата загрузки (download_date)
		/// </summary>
		public DateTime? DownloadDate { get; set; }

		/// <summary>
		/// 10007900 Внешний Id объявления (external_advertisement_id)
		/// </summary>
		public string ExternalAdvertisementId { get; set; }

		/// <summary>
		/// 10008000 Текст объявления (advertisement_description)
		/// </summary>
		public string AdvertisementDescription { get; set; }

		// <summary>
		/// 10008100 Площадь от (area_from)
		/// </summary>
		public decimal? AreaFrom { get; set; }

		/// <summary>
		/// 10008200 Название (name)
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 10008300 Номер квартиры на площадке (flat_number)
		/// </summary>
		public long? FlatNumber { get; set; }

		/// <summary>
		/// 10008400 Номер секции (section_number)
		/// </summary>
		public string SectionNumber { get; set; }

		/// <summary>
		/// 10008500 Тип квартиры (flat_type)
		/// </summary>
		public string FlatType { get; set; }

		/// <summary>
		/// 10003600 Тип сделки (DEAL_TYPE)
		/// </summary>
		public string DealType { get; set; }

		/// <summary>
		/// 10003600 Тип сделки (справочный код) (DEAL_TYPE_CODE)
		/// </summary>
		public DealType DealType_Code { get; set; }

		/// <summary>
		/// 10008600 Линия застройки домов (house_line)
		/// </summary>
		public string HouseLine { get; set; }

		/// <summary>
		/// 10008600 Линия застройки домов (справочный код) (house_line_code)
		/// </summary>
		public HouseLineType HouseLine_Code { get; set; }

		/// <summary>
		/// 10008700 Застройщик (developer)
		/// </summary>
		public string Developer { get; set; }

		/// <summary>
		/// 10008800 Состояние отделки (finishing_condition)
		/// </summary>
		public string FinishingCondition { get; set; }

		/// <summary>
		/// 10008800 Состояние отделки (справочный код) ()
		/// </summary>
		public FinishingCondition FinishingCondition_Code { get; set; }

		/// <summary>
		/// 10008900 Тип дома (house_type)
		/// </summary>
		public string HouseType { get; set; }

		/// <summary>
		/// 10008900 Тип дома (справочный код) (house_type_code)
		/// </summary>
		public HouseType HouseType_Code { get; set; }

		/// <summary>
		/// 10009000 Планировка (layout)
		/// </summary>
		public string Layout { get; set; }

		/// <summary>
		/// 10009000 Планировка (справочный код) (layout_code)
		/// </summary>
		public Layout Layout_Code { get; set; }

		/// <summary>
		/// 10009100 Вид разрешённого использования (permitted_use_type)
		/// </summary>
		public string PermittedUseType { get; set; }

		/// <summary>
		/// 10009100 Вид разрешённого использования (справочный код) (permitted_use_type_code)
		/// </summary>
		public PermittedUseType PermittedUseType_Code { get; set; }

		/// <summary>
		/// 10009200 Подъездные пути (driveway_type)
		/// </summary>
		public string DrivewayType { get; set; }

		/// <summary>
		/// 10009200 Подъездные пути (справочный код) (driveway_type_code)
		/// </summary>
		public DrivewayType DrivewayType_Code { get; set; }

		/// <summary>
		/// 10009300 Единица измерения площади участка (parcel_area_unit_type)
		/// </summary>
		public string ParcelAreaUnitType { get; set; }

		/// <summary>
		/// 10009300 Единица измерения площади участка (справочный код) (parcel_area_unit_type_code)
		/// </summary>
		public ParcelAreaUnitType ParcelAreaUnitType_Code { get; set; }

		public string ParcelType { get; set; }

		public ParcelType ParcelType_Code { get; set; }

		public string ParcelStatus { get; set; }

		public ParcelStatus ParcelStatus_Code { get; set; }
	}
}
