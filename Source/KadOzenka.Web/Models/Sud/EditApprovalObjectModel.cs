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

        public long ObjectId { get; set; }

        [Display(Name = "Кадастровый номер")]
        public string Kn { get; set; }

        [Display(Name = "Дата определения")]
        public string Date { get; set; }

        [Display(Name = "Площадь")]
        public string Square { get; set; }

        [Display(Name = "Оспариваемая стоимость")]
        public string Kc { get; set; }

        [Display(Name = "Тип объекта")]
        public string TypeObj { get; set; }

        [Display(Name = "Адрес")]
        public string Adres { get; set; }

        [Display(Name = "Наименование (ТЦ, БЦ)")]
        public string NameCenter { get; set; }

        [Display(Name = "Внесено в статистику ДГИ")]
        public string StatDgi { get; set; }

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