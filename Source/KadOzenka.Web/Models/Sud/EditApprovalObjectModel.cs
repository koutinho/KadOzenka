using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Shared.Extensions;
using DevExpress.DataProcessing;
using KadOzenka.Dal.Enum;
using ObjectModel.Directory.Sud;
using ObjectModel.Sud;

namespace KadOzenka.Web.Models.Sud
{
    public class EditApprovalObjectModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Выбирите кадастровый номер")]
        [Display(Name = "Кадастровый номер")]
        public string Kn { get; set; }

        [Required(ErrorMessage = "Выбирите дату определения")]
        [Display(Name = "Дата определения")]
        public string Date { get; set; }

        [Required(ErrorMessage = "Выбирите площадь")]
        [Display(Name = "Площадь")]
        public string Square { get; set; }

        [Required(ErrorMessage = "Выбирите оспариваемую площадь")]
        [Display(Name = "Оспариваемая стоимость")]
        public string Kc { get; set; }

        [Required(ErrorMessage = "Выбирите тип объекта")]
        [Display(Name = "Тип объекта")]
        public string TypeObj { get; set; }

        [Required(ErrorMessage = "Выбирите адрес")]
        [Display(Name = "Адрес")]
        public string Adres { get; set; }

        [Required(ErrorMessage = "Выбирите наименование")]
        [Display(Name = "Наименование (ТЦ, БЦ)")]
        public string NameCenter { get; set; }

        [Required(ErrorMessage = "Выбирите статистику ДГИ")]
        [Display(Name = "Внесено в статистику ДГИ")]
        public string StatDgi { get; set; }

        [Required(ErrorMessage = "Выбирите заказчика")]
        [Display(Name = "Заказчик / Истец")]
        public string Owner { get; set; }

        [Display(Name = "Тип заявителя")]
        [Required(ErrorMessage = "Тип заявителя обязательное поле")]
        public string ApplicantType { get; set; }

        [Display(Name = "Форма собственности")]
        [Required(ErrorMessage = "Форма собственности обязательное поле")]
		public string TypeOfOwnership { get; set; }

        [Display(Name = "Исключение")]
        [Required(ErrorMessage = "Исключение обязательное поле")]
		public string IsException { get; set; }

		[Display(Name = "Требуется дополнительный анализ")]
		[Required(ErrorMessage = "Требуется дополнительный анализ обязательное поле")]
		public string AdditionalAnalysisRequired { get; set; }

		[Display(Name = "Статус удовлетворения объекта")]
		[Required(ErrorMessage = "Статус удовлетворения объекта обязательное поле")]
		public string IsSatisfied { get; set; }

		public bool IsDisableButton { get; set; }
        public static EditApprovalObjectModel FromEntity(List<OMParam> param)
        {
            var model = new EditApprovalObjectModel
            {
                Kn = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Kn.GetEnumDescription())?.Pid.ToString(),
                Date = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Date.GetEnumDescription())?.Pid.ToString(),
                Square = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Square.GetEnumDescription())?.Pid.ToString(),
                Kc = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Kc.GetEnumDescription())?.Pid.ToString(),
                TypeObj = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.TypeObj.GetEnumDescription())?.Pid.ToString(),
                Adres = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Adres.GetEnumDescription())?.Pid.ToString(),
                NameCenter = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.NameCenter.GetEnumDescription())?.Pid.ToString(),
                StatDgi = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.StatDgi.GetEnumDescription())?.Pid.ToString(),
                Owner = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Owner.GetEnumDescription())?.Pid.ToString(),
                ApplicantType = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.ApplicantType.GetEnumDescription())?.Pid.ToString(),
                TypeOfOwnership = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.TypeOfOwnership.GetEnumDescription())?.Pid.ToString(),
                AdditionalAnalysisRequired = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.AdditionalAnalysisRequired.GetEnumDescription())?.Pid.ToString(),
                IsException = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.IsException.GetEnumDescription())?.Pid.ToString(),
                IsSatisfied = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.IsSatisfied.GetEnumDescription())?.Pid.ToString()
            };

            model.IsDisableButton = model.Kn != null && model.Date != null && model.Square != null &&
                                    model.Kc != null && model.TypeObj != null && model.Adres != null
                                    && model.NameCenter != null && model.StatDgi != null && model.Owner != null 
                                    && model.ApplicantType != null && model.TypeOfOwnership != null 
                                    && model.AdditionalAnalysisRequired != null && model.IsException != null && model.IsSatisfied != null;

            return model;
        }
    }


}