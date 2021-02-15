using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace KadOzenka.Web.Models.Modeling
{
    public class ModelingObjectsUpdatingModel : IValidatableObject
    {
	    public bool IsBackgroundDownload { get; set; }
        public IFormFile File { get; set; }
        public List<ColumnToAttributeMapping> Columns { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
	        if (File == null)
	        {
				yield return new ValidationResult("Не передан файл", new[] { nameof(File) });
			}
	        if (Columns.Count <= 1)
	        {
		        yield return new ValidationResult("Минимальное число сопоставляемых полей - 2", new[] { nameof(Columns) });
	        }
        }

		public List<Dal.Modeling.Entities.ColumnToAttributeMapping> Map()
		{
			return Columns.Select(x => new Dal.Modeling.Entities.ColumnToAttributeMapping
			{
				AttributeId = x.AttributeId,
				ColumnIndex = x.ColumnIndex
			}).ToList();
		}
    }


    public class ColumnToAttributeMapping
    {
	    public int ColumnIndex { get; set; }

		//для нормализованных атрибутов ИД идет с префиксом _
	    public string AttributeId { get; set; }
    }
}
