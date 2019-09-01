using System;
using System.Collections.Generic;
using System.Text;

namespace CIPJS.DAL.HierarchicalGridAnalysisDamage.Models
{
    public class SepTableRetEl
    {
        /// <summary>
        /// Сумма Ущерба, руб.
        /// </summary>
        public decimal? Sum_damage { get; set; }

        /// <summary>
        /// в том числе Выплата из бюджета города, руб.
        /// </summary>
        public decimal? Sum_opl { get; set; }

        /// <summary>
        /// в том числе Возмещение СК, руб.
        /// </summary>
        public decimal? Strah_plat { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string Fio { get; set; }

        /// <summary>
        /// № счета
        /// </summary>
        public string Num_invoice { get; set; }

        /// <summary>
        /// № Реестра (изначально в кот-рый  было включено дело )
        /// </summary>
        public string Izn_num { get; set; }

        /// <summary>
        /// Дата формирования Реестра (изначально в кот-рый  было включено дело )
        /// </summary>
        public DateTime? Izn_date { get; set; }

        /// <summary>
        /// Повторно включен в Реестр №
        /// </summary>
        public string Povt_num { get; set; }

        /// <summary>
        /// Дата формирования Реестра ( в кот-рый повторно  включено дело )
        /// </summary>
        public DateTime? Povt_date { get; set; }
    }
}
