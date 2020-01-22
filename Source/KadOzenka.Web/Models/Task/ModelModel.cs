using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Model.Dto;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.Task
{
    public class ModelModel
    {
        public long Id { get; set; }
        public long? GroupId { get; set; }

        [Display(Name= "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Формула")]
        public string Formula { get; set; }

        public string AlgorithmType { get; set; }

        [Display(Name = "Алгоритм расчета")]
        public KoAlgoritmType AlgorithmTypeCode { get; set; }

        [Display(Name = "Свободный член")]
        public decimal? A0 { get; set; }

        public static ModelModel ToModel(ModelDto model)
        {
            return new ModelModel
            {
                Id = model.Id,
                GroupId = model.GroupId,
                Name = model.Name,
                Description = model.Description,
                Formula = model.Formula,
                AlgorithmType = model.AlgorithmType,
                AlgorithmTypeCode = model.AlgorithmTypeCode,
                A0 = model.A0
            };
        }
    }
}
