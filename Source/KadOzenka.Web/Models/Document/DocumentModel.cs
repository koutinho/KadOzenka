using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Core.TD;

namespace KadOzenka.Web.Models.Document
{
    public class DocumentModel : IValidatableObject
    {
        public long Id { get; set; }
        public int RegisterId => OMInstance.GetRegisterId();

        [Display(Name = "Наименование")]
        public string Description { get; set; }

        [Display(Name = "Номер")]
        public string RegNumber { get; set; }

        [Display(Name = "Дата")]
        public DateTime CreateDate { get; private set; }

        [Display(Name = "Дата выпуска документа")]
        public DateTime? ApproveDate { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if(CreateDate == DateTime.MinValue)
                errors.Add(new ValidationResult("Не установлена дата"));

            return errors;
        }

        public static DocumentModel ToModel(OMInstance entity)
        {
            return new DocumentModel
            {
                Id = entity.Id,
                Description = entity.Description,
                RegNumber = entity.RegNumber,
                CreateDate = entity.CreateDate,
                ApproveDate = entity.ApproveDate
            };
        }
    }
}
