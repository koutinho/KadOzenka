using System;
using System.Collections.Generic;

namespace CIPJS.DAL.InputFilePackage
{
    public class InputFilePackageProcessDto
    {
        /// <summary>
        /// Список файлов начислений для обработки
        /// </summary>
        public List<long> NachInputFileIds { get; set; }

        /// <summary>
        /// Список файлов зачислений для обработки
        /// </summary>
        public List<long> PlatInputFileIds { get; set; }

        /// <summary>
        /// Список районов для удаления оплат
        /// </summary>
        public List<long?> OplDistrictIds { get; set; }

        public DateTime? PeriodRegDate { get; set; }
    }
}
