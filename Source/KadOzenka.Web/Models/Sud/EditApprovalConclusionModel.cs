using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.Enum;
using ObjectModel.Sud;

namespace KadOzenka.Web.Models.Sud
{
    public class EditApprovalConclusionModel
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "Выберите номер заключения")]
        [Display(Name = "Номер заключения")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Выберите номер заключения")]
        [Display(Name = "Дата составления")]
        public string CreateDate { get; set; }

        [Required(ErrorMessage = "Выберите организацию")]
        [Display(Name = "Организация")]
        public string Org { get; set; }

        [Required(ErrorMessage = "Выберите эксперта")]
        [Display(Name = "Эксперт")]
        public string Fio { get; set; }

        [Required(ErrorMessage = "Выберите СРО")]
        [Display(Name = "СРО")]
        public string Sro { get; set; }

        [Required(ErrorMessage = "Выберите дату сдачи рецензии")]
        [Display(Name = "Дата сдачи рецензии")]
        public string RecDate { get; set; }

        [Required(ErrorMessage = "ыберите исполнителя")]
        [Display(Name = "Исполнитель рецензии")]
        public string RecUser { get; set; }

        [Required(ErrorMessage = "Выберите номер письма")]
        [Display(Name = "Номер письма")]
        public string RecLetter { get; set; }

        [Required(ErrorMessage = "Выберите предварительную рецензию")]
        [Display(Name = "Предварительная рецензия")]
        public string RecBefore { get; set; }

        [Required(ErrorMessage = "Выберите рецензию после анализа")]
        [Display(Name = "Рецензия после анализа")]
        public string RecAfter { get; set; }

        [Required(ErrorMessage = "Выберите поле рассмотрено")]
        [Display(Name = "Утверждено")]
        public string RecSoglas { get; set; }

        public string NumberForTab { get; set; }

        public string DateForTab { get; set; }

        public bool IsDisableButton { get; set; }
        public static EditApprovalConclusionModel FromEntity(List<OMParam> param)
        {
            var model =  new EditApprovalConclusionModel
            {
                Number = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Number.GetEnumDescription())?.Pid.ToString(),
                NumberForTab = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Number.GetEnumDescription())?.ToString(),
                CreateDate = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Date.GetEnumDescription())?.Pid.ToString(),
                DateForTab = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Date.GetEnumDescription())?.ToString(),
                Org = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Org.GetEnumDescription())?.Pid.ToString(),
                Fio = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Fio.GetEnumDescription())?.Pid.ToString(),
                Sro = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Sro.GetEnumDescription())?.Pid.ToString(),
                RecDate = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.RecDate.GetEnumDescription())?.Pid.ToString(),
                RecUser = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.RecUser.GetEnumDescription())?.Pid.ToString(),
                RecLetter = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.RecLetter.GetEnumDescription())?.Pid.ToString(),
                RecBefore = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.RecBefore.GetEnumDescription())?.Pid.ToString(),
                RecAfter = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.RecAfter.GetEnumDescription())?.Pid.ToString(),
                RecSoglas = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.RecSoglas.GetEnumDescription())?.Pid.ToString()
            };

            model.IsDisableButton = model.Number != null && model.CreateDate != null && model.Org != null &&
                                    model.Fio != null && model.Sro != null && model.RecDate != null
                                    && model.RecUser != null && model.RecLetter != null && model.RecBefore != null &&
                                    model.RecAfter != null && model.RecSoglas != null;

            return model;
        }
    }
}