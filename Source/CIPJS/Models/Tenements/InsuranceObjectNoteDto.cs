using System;
using System.ComponentModel.DataAnnotations;

namespace CIPJS.Models.Tenements
{
    public class InsuranceObjectNoteDto
    {
        /// <summary>
        /// Идентификатор объекта МКД
        /// </summary>
        public long EmpId { get; set; }

        [Display(Name = "Примечание")]
        public string Note { get; set; }

        [Display(Name = "Пользователь")]
        public string ChangeUser { get; set; }

        [Display(Name = "Дата изменения")]
        public string ChangeDateTime  { get; set; }
    }
}