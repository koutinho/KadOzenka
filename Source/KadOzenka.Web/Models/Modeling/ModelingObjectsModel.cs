using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ModelingBusiness.Factors.Entities;
using ModelingBusiness.Model.Entities;

namespace KadOzenka.Web.Models.Modeling
{
	public class ModelingObjectsModel 
    {
	    public long Id { get; set; }
	    public List<ModelFactorRelation> Attributes { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Тур")]
        public long? TourYear { get; set; }

		[Display(Name = "Группа")]
        public string GroupName { get; set; }



		public static ModelingObjectsModel ToModel(ModelDto model, List<ModelFactorRelation> attributes)
		{
			attributes.ForEach(x => x.AttributeName = PreprocessAttributeName(x.AttributeName));
			return new ModelingObjectsModel
			{
				Id = model.ModelId,
				Name = model.Name,
				TourYear = model.TourYear,
				GroupName = model.FullGroupName,
				Attributes = attributes
			};
        }

		public static string PreprocessAttributeName(string name)
		{
			//json не парсит строки с обратным слешем
			return name.Replace(@"\", @"\\");
		}
    }
}
