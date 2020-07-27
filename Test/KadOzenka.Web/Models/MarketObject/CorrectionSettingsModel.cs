using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Correction.Dto.CorrectionSettings;
using ObjectModel.Directory.MarketObjects;

namespace KadOzenka.Web.Models.MarketObject
{
    public class CorrectionSettingsModel : IValidatableObject
    {
        [Display(Name = "Тип корректировки")]
        [Required(ErrorMessage = "Тип корректировки обязателен")]
        public CorrectionTypes? CorrectionType { get; set; }
        public decimal? LowerLimitForCoefficient { get; set; }
        public decimal? UpperLimitForCoefficient { get; set; }
        public decimal? LowerLimitForTheSecondCoefficient { get; set; }
        public decimal? UpperLimitForTheSecondCoefficient { get; set; }

        public CorrectionSettingsModel() { }

        public CorrectionSettingsModel(CorrectionSettings settings, CorrectionTypes correctionType)
        {
            CorrectionType = correctionType;
            LowerLimitForCoefficient = settings.LowerLimitForCoefficient;
            UpperLimitForCoefficient = settings.UpperLimitForCoefficient;
            LowerLimitForTheSecondCoefficient = settings.LowerLimitForTheSecondCoefficient;
            UpperLimitForTheSecondCoefficient = settings.UpperLimitForTheSecondCoefficient;
        }

        public CorrectionSettings ToModel()
        {
            return new CorrectionSettings
            {
                LowerLimitForCoefficient = LowerLimitForCoefficient,
                UpperLimitForCoefficient = UpperLimitForCoefficient,
                LowerLimitForTheSecondCoefficient = LowerLimitForTheSecondCoefficient,
                UpperLimitForTheSecondCoefficient = UpperLimitForTheSecondCoefficient
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((LowerLimitForCoefficient.HasValue && UpperLimitForCoefficient.HasValue && LowerLimitForCoefficient > UpperLimitForCoefficient)
                || (LowerLimitForTheSecondCoefficient.HasValue && UpperLimitForTheSecondCoefficient.HasValue && LowerLimitForTheSecondCoefficient > UpperLimitForTheSecondCoefficient))
            {
                yield return
                    new ValidationResult(errorMessage: "Указан некорректный диапазон значений коэффициента");
            }
        }
    }

    
}
