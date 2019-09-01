using System;
using System.Collections.Generic;
using System.Text;

namespace CIPJS.DAL.HierarchicalGridAnalysisDamage.Models
{
    public class SepTablePayEl
    {
        /// <summary>
        /// № Реестра
        /// </summary>
        public string Num { get; set; }

        /// <summary>
        /// Дата формирования Реестра
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Кол-во счетов в Реестре
        /// </summary>
        public int? Count { get; set; }

        public int? Status_code { get; set; }
    }
}
