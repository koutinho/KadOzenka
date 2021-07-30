﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ModelingBusiness.Factors.Entities;
using ModelingBusiness.Model.Entities;
using ObjectModel.KO;

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
			//json не парсит строки с обратным слешем
			attributes.ForEach(x => x.AttributeName =  x.AttributeName.Replace(@"\", @"\\"));
			return new ModelingObjectsModel
			{
				Id = model.ModelId,
				Name = model.Name,
				TourYear = model.TourYear,
				GroupName = model.FullGroupName,
				Attributes = attributes
			};
        }
    }
}
