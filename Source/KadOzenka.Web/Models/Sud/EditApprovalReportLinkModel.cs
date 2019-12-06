using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Shared.Extensions;
using DevExpress.DataAccess.Native.Sql.ConnectionProviders;
using KadOzenka.Dal.Enum;
using ObjectModel.Sud;

namespace KadOzenka.Web.Models.Sud
{
    public class EditApprovalReportLinkModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Выберите отчет")]
        [Display(Name = "Отчет об оценке")]
        public string IdReport { get; set; }

        [Required(ErrorMessage = "Выберите рыночную стоимость")]
        [Display(Name = "Рыночная стоимость")]
        public string Rs { get; set; }

        [Required(ErrorMessage = "Выберите удельную стоимость")]
        [Display(Name = "Удельная стоимость")]
        public string Uprs { get; set; }

        [Required(ErrorMessage = "Выберите текущее использование")]
        [Display(Name = "Текущее использование")]
        public string Use { get; set; }

        [Required(ErrorMessage = "Выберите примечание")]
        [Display(Name = "Примечание")]
        public string Descr { get; set; }

       
        public EditApprovalReportModel Report { get; set; }

        public static EditApprovalReportLinkModel FromEntity(List<OMParam> param)
        {
            return new EditApprovalReportLinkModel
            {
                IdReport = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.IdReport.GetEnumDescription())?.Pid.ToString(),
                Rs = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Rs.GetEnumDescription())?.Pid.ToString(),
                Uprs = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Uprs.GetEnumDescription())?.Pid.ToString(),
                Use = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Use.GetEnumDescription())?.Pid.ToString(),
                Descr = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Uprs.GetEnumDescription())?.Pid.ToString(),
                Report = EditApprovalReportModel.FromEntity(param)
            };
        }
    }
}