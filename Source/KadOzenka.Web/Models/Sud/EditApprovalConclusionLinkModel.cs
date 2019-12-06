using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.Enum;
using ObjectModel.Sud;

namespace KadOzenka.Web.Models.Sud
{
    public class EditApprovalConclusionLinkModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Выберите экспертное заключение")]
        [Display(Name = "Экспертное заключение")]
        public string IdConclusion { get; set; }

        [Required(ErrorMessage = "Выберите текущее использование")]
        [Display(Name = "Текущее использование")]
        public string Use { get; set; }

        [Required(ErrorMessage = "Выберите рыночную стоимость")]
        [Display(Name = "Рыночная стоимость")]
        public string Rs { get; set; }

        [Required(ErrorMessage = "Выберите удельную стоимость")]
        [Display(Name = "Удельная стоимость")]
        public string Uprs { get; set; }

        [Required(ErrorMessage = "Выберите примечание")]
        [Display(Name = "Примечание")]
        public string Descr { get; set; }

        public EditApprovalConclusionModel Conclusion { get; set; }

        public string NumberForTab { get; set; }

        public string DateForTab { get; set; }

        public bool IsDisableButton { get; set; }
        public static EditApprovalConclusionLinkModel FromEntity(List<OMParam> param)
        {
            var model = new EditApprovalConclusionLinkModel
            {
                IdConclusion = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.IdConclusion.GetEnumDescription())?.Pid.ToString(),
                Use = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Use.GetEnumDescription())?.Pid.ToString(),
                Rs = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Rs.GetEnumDescription())?.Pid.ToString(),
                Uprs = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Uprs.GetEnumDescription())?.Pid.ToString(),
                Descr = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Descr.GetEnumDescription())?.Pid.ToString(),
                Conclusion = EditApprovalConclusionModel.FromEntity(param)
            };

            model.IsDisableButton = model.IdConclusion != null && model.Use != null && model.Rs != null &&
                                    model.Uprs != null && model.Descr != null;

            return model;
        }
    }
}