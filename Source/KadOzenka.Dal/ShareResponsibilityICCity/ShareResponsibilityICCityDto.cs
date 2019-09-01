using System;

namespace CIPJS.DAL.ShareResponsibilityICCity
{
    public class ShareResponsibilityICCityDto
    {
        public long Id { get; set; }

        /// <summary>
        /// Дата начала  действия
        /// </summary>
        public DateTime? DateBegin { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Доля СК,%
        /// </summary>
        public long? ICShare { get; set; }

        /// <summary>
        /// Доля города,%
        /// </summary>
        public long? CityShare { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        public string Note { get; set; }
    }
}
