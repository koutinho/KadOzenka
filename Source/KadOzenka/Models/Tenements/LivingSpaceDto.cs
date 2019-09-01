using CIPJS.DAL.Flat;
using CIPJS.Models.Fsp;
using Core.Shared.Extensions;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CIPJS.Models.Tenements
{
    public class LivingSpaceDto
    {
        public long? BuildingId { get; set; }

        /// <summary>
        /// Дата загрузки данных
        /// </summary>
        [Display(Name = "Дата загрузки данных")]
        public DateTime? UploadDate { get; set; }

        /// <summary>
        /// Подлежит страхованию
        /// </summary>
        [Display(Name = "Ручной")]
        public bool? InInsuranceProgram { get; set; }

        /// <summary>
        /// Подлежит страхованию
        /// </summary>
        [Display(Name = "Система")]
        public bool? InInsuranceProgramCalc { get; set; }

        /// <summary>
        /// UNOM МКД
        /// </summary>
        [Display(Name = "UNOM МКД")]
        public long? Unom { get; set; }

        /// <summary>
        /// Идентификатор МКД
        /// </summary>
        public long? LinkObjectMkd { get; set; }

        /// <summary>
        /// Идентификатор помещения
        /// </summary>
        public long FlatId { get; set; }

        /// <summary>
        /// Номер квартиры
        /// </summary>
        [Display(Name = "Номер квартиры")]
        public string FlatNumber { get; set; }

        [Display(Name = "Кадастровый номер")]
        public string CadastralNumber { get; set; }

        [Display(Name = "Дата постановки на кадастровый учет")]
        public DateTime? CadastralRegistrationDate { get; set; }

        [Display(Name = "Назначение помещения")]
        public Assftp_cd? SpacePurpose { get; set; }

        [Display(Name = "Тип помещения")]
        public Assftp1? SpaceType { get; set; }
        
        [Display(Name = "Общая площадь")]
        public decimal? TotalArea { get; set; }

        [Display(Name = "Общая площадь ЕГРН")]
        public decimal? TotalAreaEgrn { get; set; }

        [Display(Name = "Общая площадь БТИ")]
        public decimal? TotalAreaBti { get; set; }

        [Display(Name = "Общая площадь МФЦ")]
        public decimal? TotalAreaMfc { get; set; }

        [Display(Name = "Кол-во комнат")]
        public long? RoomsCount { get; set; }

        [Display(Name = "Кол-во комнат БТИ")]
        public long? RoomsCountBti { get; set; }

        [Display(Name = "Кол-во комнат Mfc")]
        public long? RoomsCountMfc { get; set; }

        [Display(Name = "Округ")]
        public string Okrug { get; set; }

        [Display(Name = "Район")]
        public string District { get; set; }

        [Display(Name = "Адрес")]
        public string FullName { get; set; }

        [Display(Name = "Примечание")]
        public string Note { get; set; }

        [Display(Name = "Тип последнего решения по помещению")]
        public string Tres { get; set; }

        [Display(Name = "Содержание последнего решения")]
        public string Sres { get; set; }

        [Display(Name = "Дата вынесения последнего решения")]
        public DateTime? Dres { get; set; }

        [Display(Name = "№ последнего решения")]
        public string Nres { get; set; }

        [Display(Name = "Последняя дата ввода информации об обременении помещения")]
        public DateTime? ArDtvv { get; set; }

        [Display(Name = "Дата регистрации последнего обременения")]
        public DateTime? ArDt { get; set; }

        [Display(Name = "Тип обременения")]
        public string ArOsn { get; set; }

        [Display(Name = "Дата снятия последнего обременения")]
        public DateTime? ArDtsn { get; set; }

        [Display(Name = "Тип снятия обременения")]
        public string ArOsnsn { get; set; }

        public string FullNameWithFlat => FullName + (!string.IsNullOrWhiteSpace(FlatNumber) ? ", кв. " + FlatNumber : "");

        /// <summary>
        /// ФСП
        /// </summary>
        public List<FspDetails> Fsps { get; set; }

        /// <summary>
        /// Сумма взносов за квартиру на текущий месяц
        /// </summary>
        public decimal? AccruedSumCurrent { get; set; }

        /// <summary>
        /// Сумма площадей за квартиру на текущий месяц
        /// </summary>
        public decimal? AccruedOplCurrent { get; set; }

        /// <summary>
        /// Сумма взносов за квартиру на прошлый месяц
        /// </summary>
        public decimal? AccruedSumLast { get; set; }

        /// <summary>
        /// Сумма площадей за квартиру на прошлый месяц
        /// </summary>
        public decimal? AccruedOplLast { get; set; }

        /// <summary>
        /// Сумма оплат за квартиру на текущий месяц
        /// </summary>
        public decimal? CreditedSumCurrent { get; set; }

        /// <summary>
        /// Сумма площадей за квартиру на текущий месяц
        /// </summary>
        public decimal? CreditedOplCurrent { get; set; }

        /// <summary>
        /// Сумма оплат за квартиру на прошлый месяц
        /// </summary>
        public decimal? CreditedSumLast { get; set; }

        /// <summary>
        /// Сумма площадей за квартиру на прошлый месяц
        /// </summary>
        public decimal? CreditedOplLast { get; set; }

        /// <summary>
        /// ФСП на несколько квартир
        /// </summary>
        public bool FspFlagManyObj { get; set; }

        public static LivingSpaceDto Empty()
        {
            return new LivingSpaceDto
            {
                SpacePurpose = Assftp_cd.Dwelling,
                SpaceType = Assftp1.Flat,
                UploadDate = DateTime.Now
            };
        }

        public static LivingSpaceDto Map(OMFlat omFlat)
        {
            // CIPJS-974: На карточке ЖП не отражается номер UNOM ( для некоторых квартир в доме) , при этом по ссылке переходит на верный МКД
            if (omFlat.Unom == null)
            {
                var build = OMBuilding.Where(x => x.EmpId == omFlat.LinkObjectMkd && x.SpecialActual == 1).Select(x => x.Unom).ExecuteFirstOrDefault();
                omFlat.Unom = build?.Unom;
                if (omFlat != null)
                {
                    omFlat.Save();
                }
            }

            // Получаем дату изменения ЖП.
            var chagesValues = new FlatService().GetFlatChangesData(omFlat.EmpId);
            DateTime chageDate;

            return new LivingSpaceDto
            {
                BuildingId = omFlat.LinkObjectMkd,
                UploadDate = DateTime.TryParse(chagesValues.changeDate, out chageDate) ? chageDate : omFlat.LoadDate,
                InInsuranceProgram = omFlat.FlagInsur,
                InInsuranceProgramCalc = omFlat.ParentBuilding?.FlagInsurCalculated ?? false,
                Unom = omFlat.Unom,
                FlatId = omFlat.EmpId,
                LinkObjectMkd = omFlat.LinkObjectMkd,
                FlatNumber = omFlat.Kvnom,
                CadastralNumber = omFlat.CadastrNum,
                CadastralRegistrationDate = omFlat.CadastrDate,
                SpacePurpose = omFlat.KlassFlat_Code,
                SpaceType = omFlat.TypeFlat_Code,
                TotalArea = omFlat.Fopl,
                TotalAreaEgrn = omFlat.ParentBuilding?.ParentBuildParcel?.Area,
                TotalAreaBti = omFlat.ParentPremase?.TotalArea,
                RoomsCount = omFlat.KolGp,
                RoomsCountBti = omFlat.ParentPremase?.RoomsCount,
                Note = omFlat.Note,
                Okrug = omFlat.ParentBuilding?.ParentBtiOkrug?.Name,
                District = omFlat.ParentBuilding?.ParentBtiDistrict?.Name,
                FullName = omFlat.ParentBuilding?.ParentAddress?.FullAddress,
                AccruedSumCurrent = omFlat.AccruedSumCurrent,
                AccruedOplCurrent = omFlat.AccruedOplCurrent,
                AccruedSumLast = omFlat.AccruedSumLast,
                AccruedOplLast = omFlat.AccruedOplLast,
                CreditedSumCurrent = omFlat.CreditedSumCurrent,
                CreditedOplCurrent = omFlat.CreditedOplCurrent,
                CreditedSumLast = omFlat.CreditedSumLast,
                CreditedOplLast = omFlat.CreditedOplLast,
                Tres = omFlat.ParentPremase?.Tres,
                Sres = omFlat.ParentPremase?.Sres,
                Dres = omFlat.ParentPremase?.Dres,
                Nres = omFlat.ParentPremase?.Nres,
                ArDtvv = omFlat.ParentPremase?.ArDtvv,
                ArDt = omFlat.ParentPremase?.ArDt,
                ArOsn = omFlat.ParentPremase?.ArOsn,
                ArDtsn = omFlat.ParentPremase?.ArDtsn,
                ArOsnsn = omFlat.ParentPremase?.ArOsnsn
            };
        }

        public static OMFlat Map(LivingSpaceDto model)
        {
            return new OMFlat
            {
                EmpId = model.FlatId,
                LinkObjectMkd = model.BuildingId,
                LoadDate = model.UploadDate,
                FlagInsur = model.InInsuranceProgram,
                Unom = model.Unom,
                Kvnom = model.FlatNumber.ToString(),
                CadastrNum = model.CadastralNumber,
                CadastrDate = model.CadastralRegistrationDate,
                KlassFlat_Code = model.SpacePurpose.HasValue ? model.SpacePurpose.Value : Assftp_cd.None,
                TypeFlat_Code = model.SpaceType.HasValue ? model.SpaceType.Value : Assftp1.None,
                Fopl = model.TotalArea,
                KolGp = model.RoomsCount,
                Note = model.Note
            };
        }
    }
}