using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Modeling
{
	public class ModelingObjectsModel 
    {
	    public long Id { get; set; }
	    public List<ModelAttributeRelationPure> Attributes { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Тур")]
        public long? TourYear { get; set; }

		[Display(Name = "Группа")]
        public string GroupName { get; set; }



		public static ModelingObjectsModel ToModel(OMModel model, long? tourYear, List<ModelAttributeRelationPure> attributes)
		{
			return new ModelingObjectsModel
			{
				Id = model.Id,
				Name = model.Name,
				TourYear = tourYear,
				GroupName = $"{model.ParentGroup?.Number}.{model.ParentGroup?.GroupName}",
				Attributes = attributes
			};
        }
    }
}
