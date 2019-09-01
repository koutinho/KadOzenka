using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CIPJS.DAL.HierarchicalGridAnalysisDamage.Models
{
    public class SepTableEl
    {
        public long? InvoiceId { get; set; }
        public long? InvoiceStatus { get; set; }
        public long? DamageId { get; set; }
        public long? DamageStatus { get; set; }

        public bool? FlagZakluchReissue { get; set; }

        /// <summary>
        /// Номер заключения
        /// </summary>
        public string NumConclusions { get; set; }

        /// <summary>
        /// Дата выпуска заключения
        /// </summary>
        public DateTime? DateIssue { get; set; }

        /// <summary>
        /// ФИО страхователя
        /// </summary>
        public string NamePolicyholder { get; set; }

        /// <summary>
        /// № Дела
        /// </summary>
        public string NumCases { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime? DateCreate { get; set; }

        /// <summary>
        /// Дата поступления в ГЦИП и ЖС пакета документов по выплатному делу
        /// </summary>
        public DateTime? DateReceiptGcipAndJc { get; set; }

        /// <summary>
        /// Дата досылки
        /// </summary>
        public DateTime? DateShipment { get; set; }

        /// <summary>
        /// Комментарий из дела
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        public string Performer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? DaysCount
        {
            get
            {
                if (DateShipment != null)
                    return (DateTime.Now - DateShipment.Value).Days;

                if (DateReceiptGcipAndJc != null)
                    return (DateTime.Now - DateReceiptGcipAndJc.Value).Days;

                return null;
            }
        }
    }
}
