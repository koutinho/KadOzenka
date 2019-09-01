using CIPJS.DAL.Fias;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.ComponentModel.DataAnnotations;

namespace CIPJS.Models.Building
{
    public class BuildingDetails
    {
        #region properties
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Дата загрузки
        /// </summary>
        [Display(Name = "Адрес МКД")]
        public DateTime? LoadDate { get; set; }
        /// <summary>
        /// Кадастровый номер МКД
        /// </summary>
        [Display(Name = "Кадастровый номер МКД")]
        public string CadasrNum { get; set; }
        /// <summary>
        /// Номер из справочника «Статус объекта ЕГРН»
        /// </summary>
        [Display(Name = "Номер из справочника «Статус объекта ЕГРН»")]
        public string StatusEgrn { get; set; }
        /// <summary>
        /// Номер из справочника «Статус объекта БТИ»
        /// </summary>
        [Display(Name = "Номер из справочника «Статус объекта БТИ»")]
        public string StatusSostBti { get; set; }
        /// <summary>
        /// Дата постановки на кадастровый учет
        /// </summary>
        [Display(Name = "Дата постановки на кадастровый учет")]
        public DateTime? CadastrDate { get; set; }
        /// <summary>
        /// Идентификатор округа
        /// </summary>
        [Display(Name = "Идентификатор округа")]
        public long? OkrugId { get; set; }
        /// <summary>
        /// Идентификтор района
        /// </summary>
        [Display(Name = "Идентификтор района")]
        public long? DistrictId { get; set; }
        /// <summary>
        /// УНОМ МКД
        /// </summary>
        [Display(Name = "УНОМ МКД")]
        public long? Unom { get; set; }
        /// <summary>
        /// Код типа дома из справочника «Статус дома ГБУ» 
        /// </summary>
        [Display(Name = "Код типа дома из справочника «Статус дома ГБУ» ")]
        public long? TypeMkd { get; set; }
        /// <summary>
        /// Год постройки
        /// </summary>
        [Display(Name = "Год постройки")]
        public long? YearStroi { get; set; }
        /// <summary>
        /// Кол-во этажей
        /// </summary>
        [Display(Name = "Кол-во этажей")]
        public long? CountFloor { get; set; }
        /// <summary>
        /// Кол-во квартир в доме
        /// </summary>
        [Display(Name = "Кол-во квартир в доме")]
        public long? KolGp { get; set; }
        /// <summary>
        /// Общая площадь
        /// </summary>
        [Display(Name = "Общая площадь")]
        public decimal? Opl { get; set; }
        /// <summary>
        /// Площадь жилых помещений
        /// </summary>
        [Display(Name = "Площадь жилых помещений")]
        public decimal? OplG { get; set; }
        /// <summary>
        /// Площадь нежилых помещений
        /// </summary>
        [Display(Name = "Площадь нежилых помещений")]
        public decimal? OplN { get; set; }
        /// <summary>
        /// Площадь балконов
        /// </summary>
        [Display(Name = "Площадь балконов")]
        public decimal? Bpl { get; set; }
        /// <summary>
        /// Площадь холодных помещений
        /// </summary>
        [Display(Name = "Площадь холодных помещений")]
        public decimal? Hpl { get; set; }
        /// <summary>
        /// Площадь лоджий
        /// </summary>
        [Display(Name = "Площадь лоджий")]
        public decimal? Lpl { get; set; }
        /// <summary>
        /// Кол-во лифтов пассажирских
        /// </summary>
        [Display(Name = "Кол-во лифтов пассажирских")]
        public long? Lfpq { get; set; }
        /// <summary>
        /// Кол-во лифтов грузопассажирских
        /// </summary>
        [Display(Name = "Кол-во лифтов грузопассажирских")]
        public long? Lfgpq { get; set; }
        /// <summary>
        /// Кол-во лифтов грузовых
        /// </summary>
        [Display(Name = "Кол-во лифтов грузовых")]
        public long? Lfgq { get; set; }
        /// <summary>
        /// GUID-ссылка в справочнике ФИАС на адрес МКД
        /// </summary>
        [Display(Name = "GUID-ссылка в справочнике ФИАС на адрес МКД")]
        public string GuidFiasMkd { get; set; }
        /// <summary>
        /// Адрес МКД в ФИАС 
        /// </summary>
        [Display(Name = "Адрес МКД в справочнике ФИАС")]
        public string AddressFiasMkd { get; set; }
        /// <summary>
        /// Ссылка на Реестр связей с объектами БТИ  
        /// </summary>
        [Display(Name = "Ссылка на Реестр связей с объектами БТИ  ")]
        public long? LinkBtiFsks { get; set; }
        /// <summary>
        /// Ссылка на Реестр зданий  в Росреестре
        /// </summary>
        [Display(Name = "Ссылка на Реестр зданий  в Росреестре")]
        public long? LinkEgrnBild { get; set; }
        /// <summary>
        /// Источник заполнения
        /// </summary>
        [Display(Name = "Источник заполнения")]
        public string SourceAtrib { get; set; }
        /// <summary>
        /// Код источника заполнения
        /// </summary>
        [Display(Name = "Код источника заполнения")]
        public SourceInput SourceAtribCode { get; set; }
        /// <summary>
        /// Признак участия в программе страхования
        /// </summary>
        [Display(Name = "Признак участия в программе страхования")]
        public bool? FlagInsur { get; set; }
        /// <summary>
        /// Признак участия в программе страхования (расчетный)
        /// </summary>
        [Display(Name = "Признак участия в программе страхования (расчетный)")]
        public bool? FlagInsurCalculated { get; set; }

        /// <summary>
        /// Площадь кровли
        /// </summary>
        [Display(Name = "Площадь кровли")]
        public decimal? Krovpl { get; set; }

        /// <summary>
        /// Строительная стоимость
        /// </summary>
        [Display(Name = "Строительная стоимость")]
        public decimal? StroiPrice { get; set; }

        #endregion

        public static BuildingDetails OMMap(OMBuilding entity)
        {
            var buildingDetails = new BuildingDetails
            {
                Id = entity.EmpId,
                LoadDate = entity.LoadDate,
                CadasrNum = entity.CadasrNum,
                StatusEgrn = entity.StatusEgrn,
                StatusSostBti = entity.StatusSostBti,
                CadastrDate = entity.CadastrDate,
                OkrugId = entity.OkrugId,
                DistrictId = entity.DistrictId,
                Unom = entity.Unom,
                TypeMkd = entity.TypeMkd_Code,
                YearStroi = entity.YearStroi,
                CountFloor = entity.CountFloor,
                KolGp = entity.KolGp,
                Opl = entity.Opl,
                OplG = entity.OplG,
                OplN = entity.OplN,
                Bpl = entity.Bpl,
                Lpl = entity.Lpl,
                Hpl = entity.Hpl,
                Lfpq = entity.Lfpq,
                Lfgpq = entity.Lfgpq,
                GuidFiasMkd = entity.GuidFiasMkd,
                LinkBtiFsks = entity.LinkBtiFsks,
                LinkEgrnBild = entity.LinkEgrnBild,
                SourceAtribCode = entity.SourceAtrib_Code,
                SourceAtrib = entity.SourceAtrib,
                FlagInsur = entity.FlagInsur,
                FlagInsurCalculated = entity.FlagInsurCalculated,
                Krovpl = entity.Krovpl,
                StroiPrice = entity.StroiPrice,
            };

            if (entity.ParentAddress != null)
                buildingDetails.AddressFiasMkd = entity.ParentAddress.FullAddress;

            return buildingDetails;
        }

        public static OMBuilding OMMap(BuildingDetails entity)
        {
            if (entity is null)
                return null;

            return new OMBuilding
            {
                EmpId = entity.Id,
                LoadDate = entity.LoadDate,
                CadasrNum = entity.CadasrNum,
                StatusEgrn = entity.StatusEgrn,
                StatusSostBti = entity.StatusSostBti,
                CadastrDate = entity.CadastrDate,
                OkrugId = entity.OkrugId,
                DistrictId = entity.DistrictId,
                Unom = entity.Unom,
                TypeMkd_Code = entity.TypeMkd,
                YearStroi = entity.YearStroi,
                CountFloor = entity.CountFloor,
                KolGp = entity.KolGp,
                Opl = entity.Opl,
                OplG = entity.OplG,
                OplN = entity.OplN,
                Bpl = entity.Bpl,
                Lpl = entity.Lpl,
                Hpl = entity.Hpl,
                Lfpq = entity.Lfpq,
                Lfgpq = entity.Lfgpq,
                GuidFiasMkd = entity.GuidFiasMkd,
                //AddressFiasMkd = entity.AddressFiasMkd,
                LinkBtiFsks = entity.LinkBtiFsks,
                LinkEgrnBild = entity.LinkEgrnBild,
                SourceAtrib_Code = entity.SourceAtribCode,
                SourceAtrib = entity.SourceAtrib,
                FlagInsur = entity.FlagInsur,
                Krovpl = entity.Krovpl,
                StroiPrice = entity.StroiPrice,
            };
        }
    }
}
