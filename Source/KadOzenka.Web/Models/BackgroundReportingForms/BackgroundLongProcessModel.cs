using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.BackgroundReportingForms
{
    public class BackgroundLongProcessModel : IValidatableObject
    {
        [Display(Name = "Тип фонового процесса")]
        [Required(ErrorMessage = "Не выбран Тип фонового процесса")]
        public BackgroundProcessType BackgroundProcessType { get; set; }

        [Display(Name = "Периодичность формирования")]
        [Required(ErrorMessage = "Не выбрана Периодичность формирования")]
        public SchedulerType SchedulerType { get; set; }

        [Display(Name = "Дата первого формирования")]
        [Required(ErrorMessage = "Не выбрана Дата первого формирования")]
        public DateTime FirstFormationDate{ get; set; }

        [Display(Name = "Время формирования")]
        [Required(ErrorMessage = "Не выбрано Время формирования")]
        public string FirstFormationTimeStr { get; set; }


        protected BackgroundLongProcessModel()
        {
            FirstFormationDate = DateTime.Today;
            FirstFormationTimeStr = DateTime.Now.ToString("HH:mm");
        }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            ValidatePlaningDate(errors);

            return errors;
        }


        #region Support Methods

        protected void ValidatePlaningDate(List<ValidationResult> errors)
        {
            if (string.IsNullOrWhiteSpace(FirstFormationTimeStr))
                errors.Add(new ValidationResult("Не выбрано Время формирования"));
            else
            {
                if (!TimeSpan.TryParse(FirstFormationTimeStr, out var planingTime))
                {
                    errors.Add(new ValidationResult("Не верное Время формирования"));
                }
                //var firstPlaningDate = FirstFormationDate.Add(planingTime);
            }
        }

        #endregion
    }
}
