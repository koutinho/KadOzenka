using System;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Documents.Dto;
using ObjectModel.Core.TD;

namespace KadOzenka.Web.Models.Document
{
    public class DocumentModel
    {
        public long? Id { get; set; }
        public int RegisterId => OMInstance.GetRegisterId();

        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Не заполнено Наименование")]
        public string Description { get; set; }

        [Display(Name = "Номер")]
        [Required(ErrorMessage = "Не заполнен Номер")]
        public string RegNumber { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Дата выпуска документа")]
        [Required(ErrorMessage = "Не заполнена Дата выпуска документа")]
        public DateTime? ApproveDate { get; set; }

        [Display(Name = "Дата последнего изменения")]
        public DateTime? ChangeDate { get; set; }


        public static DocumentModel ToModel(OMInstance entity)
        {
            //OMInstance.ChangeDate в ОРМ не null, но по факту у нас в БД можно сохранять null
            var documentChangeDate = entity.ChangeDate == DateTime.MinValue
                ? (DateTime?) null
                : entity.ChangeDate;

            return new DocumentModel
            {
                Id = entity.Id,
                Description = entity.Description,
                RegNumber = entity.RegNumber,
                CreateDate = entity.CreateDate,
                ApproveDate = entity.ApproveDate,
                ChangeDate = documentChangeDate
            };
        }

        public DocumentDto ToEntity(DocumentModel model)
        {
            return new DocumentDto
            {
                Id = model.Id,
                Description = model.Description,
                RegNumber = model.RegNumber,
                ApproveDate = model.ApproveDate,
                ChangeDate = model.ChangeDate
            };
        }
    }
}
