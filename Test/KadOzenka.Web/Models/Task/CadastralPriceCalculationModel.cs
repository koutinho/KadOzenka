using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Task
{
    public class CadastralPriceCalculationModel
    {
        [Display(Name = "Тур оценки")]
        public long TourId { get; set; }

        [Display(Name = "Задания на оценку")]
        public List<long> TaskFilter { get; set; }

        [Display(Name = "Тип объекта")]
        public bool IsParcel { get; set; }

        [Display(Name = "Предварительный расчет")]
        public bool IsEstimation { get; set; }

        [Display(Name = "Расчет поправок/коэффициентов")]
        public bool IsCorrectionsOrCoefficients{ get; set; }

        [Display(Name = "Окончательный расчет")]
        public bool IsFinalCalculation { get; set; }

        [Display(Name = "Все группы")]
        public bool IsAllGroups { get; set; }

        public List<long> SubGroups { get; set; }


        public static KOCalcSettings UnMap(CadastralPriceCalculationModel model)
        {
            return new KOCalcSettings
            {
                IdTour = model.TourId,
                TaskFilter = model.TaskFilter,
                CalcParcel = model.IsParcel,
                CalcStage1 = model.IsEstimation,
                CalcStage2 = model.IsCorrectionsOrCoefficients,
                CalcStage3 = model.IsFinalCalculation,
                CalcAllGroups = model.IsAllGroups,
                CalcGroups = model.IsAllGroups ? null : model.SubGroups
            };
        }
    }
}