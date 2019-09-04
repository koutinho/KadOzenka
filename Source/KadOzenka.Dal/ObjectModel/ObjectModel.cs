using System;
using Core.ObjectModel;
using Core.ObjectModel.CustomAttribute;
using Core.Shared.Extensions;
using ObjectModel.Directory;
namespace ObjectModel.Market
{
    /// <summary>
    /// 100 Объект аналог (MARKET_CORE_OBJECT)
    /// </summary>
    [RegisterInfo(RegisterID = 100)]
    [Serializable]
    public sealed partial class OMCoreObject : OMBaseClass<OMCoreObject>
    {

        private long _id;
        /// <summary>
        /// 10000100 Уникальный идентификатор объекта недвижимости сторонней площадки (ID)
        /// </summary>
        [PrimaryKey(AttributeID = 10000100)]
        public long Id
        {
            get
            {
                CheckPropertyInited("Id");
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        private string _url;
        /// <summary>
        /// 10000200 URL-адрес объявления (URL)
        /// </summary>
        [RegisterAttribute(AttributeID = 10000200)]
        public string Url
        {
            get
            {
                CheckPropertyInited("Url");
                return _url;
            }
            set
            {
                _url = value;
                NotifyPropertyChanged("Url");
            }
        }


        private long _referenceid;
        /// <summary>
        /// 10000300 Тип справочника (REFERENCEID)
        /// </summary>
        [RegisterAttribute(AttributeID = 10000300)]
        public long Referenceid
        {
            get
            {
                CheckPropertyInited("Referenceid");
                return _referenceid;
            }
            set
            {
                _referenceid = value;
                NotifyPropertyChanged("Referenceid");
            }
        }


        private string _code;
        /// <summary>
        /// 10000400 Тип сторонней площадки (CODE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10000400)]
        public string Code
        {
            get
            {
                CheckPropertyInited("Code");
                return _code;
            }
            set
            {
                _code = value;
                NotifyPropertyChanged("Code");
            }
        }


        private long _marketid;
        /// <summary>
        /// 10000500 Уникальный идентификатор в рамках сторонней площадки (MARKET_ID)
        /// </summary>
        [RegisterAttribute(AttributeID = 10000500)]
        public long MarketId
        {
            get
            {
                CheckPropertyInited("MarketId");
                return _marketid;
            }
            set
            {
                _marketid = value;
                NotifyPropertyChanged("MarketId");
            }
        }


        private long? _price;
        /// <summary>
        /// 10000600 Цена объекта недвижимости (PRICE)
        /// </summary>
        [RegisterAttribute(AttributeID = 10000600)]
        public long? Price
        {
            get
            {
                CheckPropertyInited("Price");
                return _price;
            }
            set
            {
                _price = value;
                NotifyPropertyChanged("Price");
            }
        }


        private DateTime? _parsertime;
        /// <summary>
        /// 10000700 Время обнаружения объявления парсером (PARSER_TIME)
        /// </summary>
        [RegisterAttribute(AttributeID = 10000700)]
        public DateTime? ParserTime
        {
            get
            {
                CheckPropertyInited("ParserTime");
                return _parsertime;
            }
            set
            {
                _parsertime = value;
                NotifyPropertyChanged("ParserTime");
            }
        }


        private string _region;
        /// <summary>
        /// 10000800 Название региона, к которому относится объект недвижимости (REGION)
        /// </summary>
        [RegisterAttribute(AttributeID = 10000800)]
        public string Region
        {
            get
            {
                CheckPropertyInited("Region");
                return _region;
            }
            set
            {
                _region = value;
                NotifyPropertyChanged("Region");
            }
        }


        private string _city;
        /// <summary>
        /// 10000900 Название города, к которому относится объект недвижимости (CITY)
        /// </summary>
        [RegisterAttribute(AttributeID = 10000900)]
        public string City
        {
            get
            {
                CheckPropertyInited("City");
                return _city;
            }
            set
            {
                _city = value;
                NotifyPropertyChanged("City");
            }
        }


        private string _address;
        /// <summary>
        /// 10001000 Адрес объекта недвижимости (ADDRESS)
        /// </summary>
        [RegisterAttribute(AttributeID = 10001000)]
        public string Address
        {
            get
            {
                CheckPropertyInited("Address");
                return _address;
            }
            set
            {
                _address = value;
                NotifyPropertyChanged("Address");
            }
        }


        private string _metro;
        /// <summary>
        /// 10001100 Ближайшие станции метро списком через запятую (METRO)
        /// </summary>
        [RegisterAttribute(AttributeID = 10001100)]
        public string Metro
        {
            get
            {
                CheckPropertyInited("Metro");
                return _metro;
            }
            set
            {
                _metro = value;
                NotifyPropertyChanged("Metro");
            }
        }


        private string _images;
        /// <summary>
        /// 10001200 URL-адреса изображений объекта недвижимости списком через запятую (IMAGES)
        /// </summary>
        [RegisterAttribute(AttributeID = 10001200)]
        public string Images
        {
            get
            {
                CheckPropertyInited("Images");
                return _images;
            }
            set
            {
                _images = value;
                NotifyPropertyChanged("Images");
            }
        }


        private string _description;
        /// <summary>
        /// 10001300 Описание объекта недвижимости (DESCRIPTION)
        /// </summary>
        [RegisterAttribute(AttributeID = 10001300)]
        public string Description
        {
            get
            {
                CheckPropertyInited("Description");
                return _description;
            }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }


        private decimal? _lat;
        /// <summary>
        /// 10001400 Широта (LAT)
        /// </summary>
        [RegisterAttribute(AttributeID = 10001400)]
        public decimal? Lat
        {
            get
            {
                CheckPropertyInited("Lat");
                return _lat;
            }
            set
            {
                _lat = value;
                NotifyPropertyChanged("Lat");
            }
        }


        private decimal? _lng;
        /// <summary>
        /// 10001500 Долгота (LNG)
        /// </summary>
        [RegisterAttribute(AttributeID = 10001500)]
        public decimal? Lng
        {
            get
            {
                CheckPropertyInited("Lng");
                return _lng;
            }
            set
            {
                _lng = value;
                NotifyPropertyChanged("Lng");
            }
        }

    }
}
