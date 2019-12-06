using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.Enum;
using ObjectModel.Sud;

namespace KadOzenka.Web.Models.Sud
{
    public class EditApprovalReportModel
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "Выберите номер отчета")]
        [Display(Name = "Номер отчета")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Выберите дату отчета")]
        [Display(Name = "Дата отчета")]
        public string ReportDate { get; set; }

        [Required(ErrorMessage = "Выберите организацию")]
        [Display(Name = "Организация")]
        public string Org { get; set; }

        [Required(ErrorMessage = "Выберите оценщика")]
        [Display(Name = "Оценщик")]
        public string Fio { get; set; }

        [Required(ErrorMessage = "Выберите СРО")]
        [Display(Name = "СРО")]
        public string Sro { get; set; }

        [Required(ErrorMessage = "Выберите дату получения")]
        [Display(Name = "Дата получения")]
        public string DateIn { get; set; }

        [Required(ErrorMessage = "Выберите жалобу")]
        [Display(Name = "Жалоба в СРО")]
        public string Claim { get; set; }

        public string DateForTab { get; set; }

        public string NumberForTab { get; set; }
        public static EditApprovalReportModel FromEntity(List<OMParam> param)
        {
            return new EditApprovalReportModel
            {
                Number = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Number.GetEnumDescription())?.Pid.ToString(),
                NumberForTab = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Number.GetEnumDescription())?.ToString(),
                ReportDate = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.ReportDate.GetEnumDescription())?.Pid.ToString(),
                DateForTab = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.ReportDate.GetEnumDescription())?.ToString(),
                Org = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Org.GetEnumDescription())?.Pid.ToString(),
                Fio = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Fio.GetEnumDescription())?.Pid.ToString(),
                Sro = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Sro.GetEnumDescription())?.Pid.ToString(),
                DateIn = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.DateIn.GetEnumDescription())?.Pid.ToString(),
                Claim = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Claim.GetEnumDescription())?.Pid.ToString(),
            };
        }
    }
}