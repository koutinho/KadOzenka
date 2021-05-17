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
		/// 10007700 Вид объекта недвижимости (PROPERTY_TYPETS_CIPJS)
		/// </summary>
		public string PropertyTypesCIPJS { get; set; }


		/// <summary>
		/// 10007700 Вид объекта недвижимости (справочный код) (PROPERTY_TYPETS_CIPJS_CODE)
		/// </summary>
		public PropertyTypesCIPJS PropertyTypesCIPJS_Code { get; set; }

		///// <summary>
		///// 10009009 Состояние отделки (RENOVATION)
		///// </summary>
		//public string Renovation { get; set; }

		///// <summary>
		///// 10009010 Линия застройки (BUILDING_LINE)
		///// </summary>
		//public string BuildingLine { get; set; }
    }
}
