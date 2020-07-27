using System.ComponentModel.DataAnnotations;
using ObjectModel.ES;

namespace KadOzenka.Web.Models.ExpressScore
{
    public class WallMaterialViewModel
    {
        public long Id { get; set; }

        /// <summary>
        /// Материал стен
        /// </summary>
        [Display(Name = "Материал стен")]
        [Required(ErrorMessage = "Поле Материал стен обязательное")]
        public string WallMaterial { get; set; }

        /// <summary>
        /// Метка
        /// </summary>
        [Display(Name = "Метка")]
        [Required(ErrorMessage = "Поле Метка обязательное")]
        public long? Mark { get; set; }

        public static WallMaterialViewModel FromEntity(OMWallMaterial entity)
        {
            if (entity == null)
            {
                return new WallMaterialViewModel
                {
                    Id = -1
                };
            }

            return new WallMaterialViewModel
            {
                Id = entity.Id,
                WallMaterial = entity.WallMaterial,
                Mark = entity.Mark
            };
        }
    }
}
