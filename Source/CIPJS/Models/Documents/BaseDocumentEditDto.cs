using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIPJS.Models.Documents
{
    public class BaseDocumentEditDto
    {
        public long Id { get; set; }

        public string Type { get; set; }

        [Display(Name = "Вид документа-основания")]
        public string DocumentBase { get; set; }

        public long? Order { get; set; }

        public bool? NeedSetDate { get; set; }

        public static BaseDocumentEditDto OMMap(OMDocBaseType entity)
        {
            return new BaseDocumentEditDto
            {
                Id = entity.Id,
                Type = entity.Type,
                DocumentBase = entity.DocumentBase,
                Order = entity.Order,
                NeedSetDate = entity.NeedSetDate
            };
        }

        public static OMDocBaseType OMMap(BaseDocumentEditDto model)
        {
            return new OMDocBaseType
            {
                Id = model.Id,
                Type = model.Type,
                DocumentBase = model.DocumentBase,
                Order = model.Order,
                NeedSetDate = model.NeedSetDate
            };
        }
    }
}
