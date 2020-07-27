using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Core.Register;

namespace KadOzenka.Web.Models.GbuObject
{
	public class PartialDocumentViewModel
    {
        public string ModelPrefix { get; set; }

        /// <summary>
        /// Идентификатор документа, куда будет записан результат 
        /// </summary>
        [Display(Name = "Документ")]
        public long? IdDocument { get; set; }

        /// <summary>
        /// Имя нового документа
        /// </summary>
        [Display(Name = "Номер")]
	    public string NewDocumentRegNumber { get; set; }

        /// <summary>
        /// Имя нового документа
        /// </summary>
        [Display(Name = "Имя документа")]
        public string NewDocumentName { get; set; }

        [Display(Name = "Дата")]
        public DateTime? NewDocumentDate { get; set; }

        /// <summary>
        /// Флаг указывающий используем старый или новый документ
        /// </summary>
        public bool IsNewDocument { get; set; } = false;
	}
}