using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Modeling.Objects.Entities;
using KadOzenka.Dal.Modeling.Objects.Import;
using Microsoft.AspNetCore.Http;

namespace KadOzenka.Web.Models.Modeling
{
    public class ModelObjectsConstructorModel : IValidatableObject
    {
	    public bool IsBackgroundDownload { get; set; }
        public IFormFile File { get; set; }
        public int? IdColumnIndex { get; set; }
        public bool IsCreation => IdColumnIndex == null;
        public long ModelId { get; set; }
        public List<ColumnToAttributeMapping> Columns { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
	        if (File == null)
		        yield return new ValidationResult("Не передан файл");

	        if (Columns.Count <= 1)
		        yield return new ValidationResult("Минимальное число сопоставляемых полей - 2");

	        if (IsCreation)
	        {
		        ModelObjectsImporter.ValidateCreationParameters(ModelId, Columns);
	        }
		}

		public ModelObjectsConstructor Map()
		{
			return new ModelObjectsConstructor
			{
				IdColumnIndex = IdColumnIndex,
				ModelId = ModelId,
				ColumnsMapping = Columns
			};
		}
    }
}
