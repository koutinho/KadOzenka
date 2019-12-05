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

        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Выберите экспертное заключение")]
        [Display(Name = "Экспертное заключение")]
        public string IdConclusion { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Выберите текущее использование")]
        [Display(Name = "Текущее использование")]
        public string Use { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Выберите рыночную стоимость")]
        [Display(Name = "Рыночная стоимость")]
        public string Rs { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Выберите удельную стоимость")]
        [Display(Name = "Удельная стоимость")]
        public string Uprs { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Выберите примечание")]
        [Display(Name = "Примечание")]
        public string Descr { get; set; }

        public EditApprovalConclusionModel Conclusion { get; set; }

        public string NumberForTab { get; set; }

        public string DateForTab { get; set; }

        public static EditApprovalConclusionLinkModel FromEntity(List<OMParam> param)
        {
            return new EditApprovalConclusionLinkModel
            {
                IdConclusion = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.IdConclusion.GetEnumDescription())?.Pid.ToString(),
                Use = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Use.GetEnumDescription())?.Pid.ToString(),
                Rs = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Rs.GetEnumDescription())?.Pid.ToString(),
                Uprs = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Uprs.GetEnumDescription())?.Pid.ToString(),
                Descr = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Descr.GetEnumDescription())?.Pid.ToString(),
                Conclusion = EditApprovalConclusionModel.FromEntity(param)
            };
        }
    }
}