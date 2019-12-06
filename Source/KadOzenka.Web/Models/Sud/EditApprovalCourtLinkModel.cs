using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.Enum;
using ObjectModel.Sud;

namespace KadOzenka.Web.Models.Sud
{
    public class EditApprovalCourtLinkModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Выберите судебное решение")]
        [Display(Name = "Судебное решение")]
        public string SudId { get; set; }

        [Required(ErrorMessage = "Выберите рыночную стоимость")]
        [Display(Name = "Рыночная стоимость")]
        public string Rs { get; set; }

        [Required(ErrorMessage = "Выберите удельную стоимосоть")]
        [Display(Name = "Удельная стоимость")]
        public string Uprs { get; set; }

        [Required(ErrorMessage = "Выберите источник информации")]
        [Display(Name = "Источник информации")]
        public string Use { get; set; }

        [Required(ErrorMessage = "Выберите примечание")]
        [Display(Name = "Примечание")]
        public string Description { get; set; }

        public EditApprovalCourtModel Court { get; set; }

        public static EditApprovalCourtLinkModel FromEntity(List<OMParam> param)
        {
            return new EditApprovalCourtLinkModel
            {
                SudId = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.SudId.GetEnumDescription())?.Pid.ToString(),
                Rs = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Rs.GetEnumDescription())?.Pid.ToString(),
                Uprs = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Uprs.GetEnumDescription())?.Pid.ToString(),
                Use = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Use.GetEnumDescription())?.Pid.ToString(),
                Description = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Descr.GetEnumDescription())?.Pid.ToString(),
                Court = EditApprovalCourtModel.FromEntity(param)
            };
        }
    }
}