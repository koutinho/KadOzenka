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
		/// 10002800 Дата предложения (сделки) (PARSER_TIME)
		/// </summary>
		public DateTime? ParserTime { get; set; }

		/// <summary>
		/// 10002900 Регион (REGION)
		/// </summary>
		public string Region { get; set; }

		/// <summary>
		/// 10003100 Адресный ориентир (ADDRESS)
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// 10003400 Текст объявления (DESCRIPTION)
		/// </summary>
		public string Description { get; set; }


		/// <summary>
		/// 10003500 Широта (LAT)
		/// </summary>
		public decimal? Lat { get; set; }

		/// <summary>
		/// 10003600 Тип сделки (DEAL_TYPE)
		/// </summary>
		public string DealType { get; set; }


		/// <summary>
		/// 10003600 Тип сделки (справочный код) (DEAL_TYPE_CODE)
		/// </summary>
		public DealType DealType_Code { get; set; }

		/// <summary>
		/// 10004000 Долгота (LNG)
		/// </summary>
		public decimal? Lng { get; set; }

		/// <summary>
		/// 10004100 Номер этажа (FLOOR_NUMBER)
		/// </summary>
		public long? FloorNumber { get; set; }

		/// <summary>
		/// 10004200 Этажность (FLOORS_COUNT)
		/// </summary>
		public long? FloorsCount { get; set; }

		/// <summary>
		/// 10004300 Общая площадь (AREA)
		/// </summary>
		public decimal? Area { get; set; }

		/// <summary>
		/// 10004400 Площадь кухни (AREA_KITCHEN)
		/// </summary>
		public decimal? AreaKitchen { get; set; }

		/// <summary>
		/// 10004600 Площадь ЗУ (AREA_LAND)
		/// </summary>
		public decimal? AreaLand { get; set; }


		/// <summary>
		/// 10004700 Год постройки (BUILDING_YEAR)
		/// </summary>
		public long? BuildingYear { get; set; }


		/// <summary>
		/// 10005300 Район (NEIGHBORHOOD)
		/// </summary>
		public string Neighborhood { get; set; }


		/// <summary>
		/// 10005300 Район (справочный код) (NEIGHBORHOOD_CODE)
		/// </summary>
		public Districts Neighborhood_Code { get; set; }

		/// <summary>
		/// 10005400 Кадастровый номер (CADASTRAL_NUMBER)
		/// </summary>
		public string CadastralNumber { get; set; }

		/// <summary>
		/// 10005600 Кадастровый квартал (CADASTRAL_QUARTAL)
		/// </summary>
		public string CadastralQuartal { get; set; }


		/// <summary>
		/// 10005800 Подгруппа сегмента рынка (KO_SUBGROUP)
		/// </summary>
		public string Subgroup { get; set; }


		/// <summary>
		/// 10005800 Подгруппа сегмента рынка (справочный код) (KO_SUBGROUP_CODE)
		/// </summary>
		public long? Subgroup_Code { get; set; }

		/// <summary>
		/// 10006000 Статус (PROCESS_TYPE)
		/// </summary>
		public string ProcessType { get; set; }

		/// <summary>
		/// 10006000 Статус (справочный код) (PROCESS_TYPE_CODE)
		/// </summary>
		public ProcessStep ProcessType_Code { get; set; }

		/// <summary>
		/// 10006001 Причина исключения (EXCLUSION_STATUS)
		/// </summary>
		public string ExclusionStatus { get; set; }


		/// <summary>
		/// 10006001 Причина исключения (справочный код) (EXCLUSION_STATUS_CODE)
		/// </summary>
		public ExclusionStatus ExclusionStatus_Code { get; set; }


		/// <summary>
		/// 10006500 Телефонный номер (PHONE_NUMBER)
		/// </summary>
		public string PhoneNumber { get; set; }


		/// <summary>
		/// 10007000 Сегмент рынка (PROPERTY_MARKET_SEGMENT)
		/// </summary>
		public string PropertyMarketSegment { get; set; }


		/// <summary>
		/// 10007000 Сегмент рынка (справочный код) (PROPERTY_MARKET_SEGMENT_CODE)
		/// </summary>
		public MarketSegment PropertyMarketSegment_Code { get; set; }


		/// <summary>
		/// 10007100 Материал стен (WALL_MATERIAL)
		/// </summary>
		public string WallMaterial { get; set; }


		/// <summary>
		/// 10007100 Материал стен (справочный код) (WALL_MATERIAL_CODE)
		/// </summary>
		public WallMaterial WallMaterial_Code { get; set; }


		/// <summary>
		/// 10007200 Класс качества (QUALITY_CLASS)
		/// </summary>
		public string QualityClass { get; set; }


		/// <summary>
		/// 10007200 Класс качества (справочный код) (QUALITY_CLASS_CODE)
		/// </summary>
		public QualityClass QualityClass_Code { get; set; }

		/// <summary>
		/// 10007300 Расстояние до ближайшей станции метро (SUBWAY_SPACE)
		/// </summary>
		public decimal? SubwaySpace { get; set; }


		/// <summary>
		/// 10007700 Вид объекта недвижимости (PROPERTY_TYPETS_CIPJS)
		/// </summary>
		public string PropertyTypesCIPJS { get; set; }


		/// <summary>
		/// 10007700 Вид объекта недвижимости (справочный код) (PROPERTY_TYPETS_CIPJS_CODE)
		/// </summary>
		public PropertyTypesCIPJS PropertyTypesCIPJS_Code { get; set; }


		/// <summary>
		/// 10007900 Размер доли (PROPERTY_PART_SIZE)
		/// </summary>
		public string PropertyPartSize { get; set; }


		/// <summary>
		/// 10008100 Цена после корректировки на дату (PRICE_AFTER_CORRECTION_BY_DATE)
		/// </summary>
		public decimal? PriceAfterCorrectionByDate { get; set; }


		/// <summary>
		/// 10008200 Цена после корректировки на торг (PRICE_AFTER_CORRECTION_BY_BARGAIN)
		/// </summary>
		public decimal? PriceAfterCorrectionByBargain { get; set; }


		/// <summary>
		/// 10008400 Цена после корректировки на комнатность (PRICE_AFTER_CORRECTION_BY_ROOMS)
		/// </summary>
		public decimal? PriceAfterCorrectionByRooms { get; set; }


		/// <summary>
		/// 10008800 Цена после корректировки на первый этаж (PRICE_AFTER_CORRECTION_FOR_FIRST_FLOOR)
		/// </summary>
		public decimal? PriceAfterCorrectionForFirstFloor { get; set; }


		/// <summary>
		/// 10008900 Цена после корректировки на цоколь/подвал (PRICE_AFTER_CORRECTION_BY_STAGE)
		/// </summary>
		public decimal? PriceAfterCorrectionByStage { get; set; }


		/// <summary>
		/// 10009003 Тип помещения (PLACEMENT_TYPE)
		/// </summary>
		public string PlacementType { get; set; }


		/// <summary>
		/// 10009004 Состояние (QUALITY)
		/// </summary>
		public string Quality { get; set; }


		/// <summary>
		/// 10009005 Эксплуатационные расходы включены (IS_OPERATING_COSTS_INCLUDED)
		/// </summary>
		public bool? IsOperatingCostsIncluded { get; set; }


		/// <summary>
		/// 10009008 Тип входа (ENTRANCE_TYPE)
		/// </summary>
		public string EntranceType { get; set; }


		/// <summary>
		/// 10009009 Состояние отделки (RENOVATION)
		/// </summary>
		public string Renovation { get; set; }


		/// <summary>
		/// 10009010 Линия застройки (BUILDING_LINE)
		/// </summary>
		public string BuildingLine { get; set; }
    }
}
