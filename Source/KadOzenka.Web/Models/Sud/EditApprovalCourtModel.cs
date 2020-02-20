using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.Enum;
using ObjectModel.Sud;

namespace KadOzenka.Web.Models.Sud
{
    public class EditApprovalCourtModel
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "Выберите наименование суда")]
        [Display(Name = "Наименование суда")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Выберите номер дела")]
        [Display(Name = "Номер дела")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Выберите дату заседания")]
        [Display(Name = "Дата заседания")]
        public string Date { get; set; }

        [Required(ErrorMessage = "Выберите дату получения")]
        [Display(Name = "Дата получения")]
        public string SudDate { get; set; }

        [Required(ErrorMessage = "Выберите статус")]
        [Display(Name = "Статус")]
        public string Status { get; set; }

		[Required(ErrorMessage = "Выберите архивный номер")]
		[Display(Name = "Архивный номер")]
        public string ArchiveNumber { get; set; }

		[Required(ErrorMessage = "Выберите номер апелляции")]
		[Display(Name = "Номер апелляции")]
        public string AppealNumber { get; set; }

		public string NumberForTab { get; set; }
        public string DateForTab { get; set; }
        public string NameForTab { get; set; }

        public bool IsDisableButton { get; set; }

        public static EditApprovalCourtModel FromEntity(List<OMParam> param)
        {
            var model = new EditApprovalCourtModel
            {
                Name = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Name.GetEnumDescription())?.Pid.ToString(),
                NameForTab = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Name.GetEnumDescription())?.ToString(),
                Number = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Number.GetEnumDescription())?.Pid.ToString(),
                NumberForTab = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Number.GetEnumDescription())?.ToString(),
                Date = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Date.GetEnumDescription())?.Pid.ToString(),
                DateForTab = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Date.GetEnumDescription())?.ToString(),
                SudDate = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.SudDate.GetEnumDescription())?.Pid.ToString(),
                Status = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Status.GetEnumDescription())?.Pid.ToString(),
                ArchiveNumber = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.ArchiveNumber.GetEnumDescription())?.Pid.ToString(),
                AppealNumber = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.AppealNumber.GetEnumDescription())?.Pid.ToString(),
            };
            model.IsDisableButton = model.Name != null && model.Number != null && model.Date != null &&
                                    model.SudDate != null && model.Status != null && model.ArchiveNumber != null && model.AppealNumber != null;
            return model;
        }
    }
}
