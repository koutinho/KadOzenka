using System;

namespace CIPJS.DAL.InputFilePackage
{
    public class InputFilePackageDto
    {
        /// <summary>
        /// Идентификатор пакета
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор округа
        /// </summary>
        public long? OkrugId { get; set; }

        /// <summary>
        /// Наименование округа
        /// </summary>
        public string OkrugName { get; set; }

        /// <summary>
        /// Период
        /// </summary>
        public DateTime? PeriodRegDate { get; set; }
    }
}
