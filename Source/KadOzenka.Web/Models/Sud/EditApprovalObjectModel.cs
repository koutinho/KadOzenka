using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Shared.Extensions;
using DevExpress.DataProcessing;
using KadOzenka.Dal.Enum;
using ObjectModel.Sud;

namespace KadOzenka.Web.Models.Sud
{
    public class EditApprovalObjectModel
    {
        public int Id { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Выбирите кадастровый номер")]
        [Display(Name = "Кадастровый номер")]
        public string Kn { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Выбирите дату определения")]
        [Display(Name = "Дата определения")]
        public string Date { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Выбирите площадь")]
        [Display(Name = "Площадь")]
        public string Square { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Выбирите оспариваемую площадь")]
        [Display(Name = "Оспариваемая стоимость")]
        public string Kc { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Выбирите тип объекта")]
        [Display(Name = "Тип объекта")]
        public string TypeObj { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Выбирите адрес")]
        [Display(Name = "Адрес")]
        public string Adres { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Выбирите наименование")]
        [Display(Name = "Наименование (ТЦ, БЦ)")]
        public string NameCenter { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Выбирите статистику ДГИ")]
        [Display(Name = "Внесено в статистику ДГИ")]
        public string StatDgi { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Выбирите заказчика")]
        [Display(Name = "Заказчик / Истец")]
        public string Owner { get; set; }

        public static EditApprovalObjectModel FromEntity(List<OMParam> param)
        {
            //OMObject
            return new EditApprovalObjectModel
            {
                Kn = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Kn.GetEnumDescription())?.Pid.ToString(),
                Date = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Date.GetEnumDescription())?.Pid.ToString(),
                Square = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Square.GetEnumDescription())?.Pid.ToString(),
                Kc = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Kc.GetEnumDescription())?.Pid.ToString(),
                TypeObj = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.TypeObj.GetEnumDescription())?.Pid.ToString(),
                Adres = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Adres.GetEnumDescription())?.Pid.ToString(),
                NameCenter = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.NameCenter.GetEnumDescription())?.Pid.ToString(),
                StatDgi = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.StatDgi.GetEnumDescription())?.Pid.ToString(),
                Owner = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.StatDgi.GetEnumDescription())?.Pid.ToString()
            };
        }
    }


}