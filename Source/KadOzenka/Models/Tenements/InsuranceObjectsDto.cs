using CIPJS.DAL.Building;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.ComponentModel.DataAnnotations;

namespace CIPJS.Models.Tenements
{
    public class InsuranceObjectsDto
    {
        /// <summary>
        /// Идентификатор объекта МКД
        /// </summary>
        public long EmpId { get; set; }

        [Display(Name = "Дата загрузки данных")]
        public DateTime? UploadDate { get; set; }

        [Display(Name = "Статус по ГБУ")]
        public long? BuildingStatusByGBU { get; set; }

        [Display(Name = "UNOM")]
        public long? Unom { get; set; }

        [Display(Name = "Ручной")]
        public bool? InInsuranceProgram { get; set; }

		[Display(Name = "Система")]
		public bool? InInsuranceProgramSystem { get; set; }

		[Display(Name = "Кадастровый номер")]
        public string CadastralNumber { get; set; }

        [Display(Name = "Дата постановки на кадастровый учет")]
        public DateTime? CadastralRegistrationDate { get; set; }

        [Display(Name = "Статус объекта")]
        public State? BuildingStatus { get; set; }

        [Display(Name = "Назначение объекта")]
        public long? BuildingPurpose { get; set; }

        [Display(Name = "Статус состояния")]
        public long? BtiStatus { get; set; }

        [Display(Name = "Округ")]
        public ObjectModel.Bti.OMBtiOkrug Okrug { get; set; }

        [Display(Name = "Район")]
        public ObjectModel.Bti.OMBtiDistrict District { get; set; }

        public InsuranceObjectAddressDto Address { get; set; }

        [Display(Name = "Год постройки")]
        public long? ConstructionYear { get; set; }

        [Display(Name = "Количество квартир в доме")]
        public long? ApartmentsCount { get; set; }

        [Display(Name = "Количество этажей")]
        public long? FloorsCount { get; set; }

        [Display(Name = "Количество пассажирских лифтов")]
        public long? PassengerElevatorsCount { get; set; }

        [Display(Name = "Количество грузо-пассажирских лифтов")]
        public long? FreightElevatorsCount { get; set; }

        [Display(Name = "Общая площадь")]
        public decimal? TotalArea { get; set; }
        
        [Display(Name = "Площадь нежилых помещений")]
        public decimal? UninhabitedPremiseArea { get; set; }

        [Display(Name = "Площадь жилых помещений")]
        public decimal? DwellingArea { get; set; }

        [Display(Name = "Площадь балконов")]
        public decimal? BalconyArea { get; set; }

        [Display(Name = "Площадь лоджий")]
        public decimal? LoggiasArea { get; set; }

        [Display(Name = "Площадь холодных помещений")]
        public decimal? ColdRoomArea { get; set; }

        [Display(Name = "Примечание")]
        public string Note { get; set; }

        [Display(Name = "Площадь кровли")]
        public decimal? RoofArea { get; set; }

        public long? DistrictId { get; set; }

        /// <summary>
        /// Сумма взносов за МКД на текущий месяц
        /// </summary>
        public decimal? AccruedSumCurrent { get; set; }

        /// <summary>
        /// Сумма площадей за МКД на текущий месяц
        /// </summary>
        public decimal? AccruedOplCurrent { get; set; }

        /// <summary>
        /// Сумма взносов за МКД на прошлый месяц
        /// </summary>
        public decimal? AccruedSumLast { get; set; }

        /// <summary>
        /// Сумма площадей за МКД на прошлый месяц
        /// </summary>
        public decimal? AccruedOplLast { get; set; }

        /// <summary>
        /// Сумма оплат за МКД на текущий месяц
        /// </summary>
        public decimal? CreditedSumCurrent { get; set; }

        /// <summary>
        /// Сумма площадей за МКД на текущий месяц
        /// </summary>
        public decimal? CreditedOplCurrent { get; set; }

        /// <summary>
        /// Сумма оплат за МКД на прошлый месяц
        /// </summary>
        public decimal? CreditedSumLast { get; set; }

        /// <summary>
        /// Сумма площадей за МКД на прошлый месяц
        /// </summary>
        public decimal? CreditedOplLast { get; set; }

        public static InsuranceObjectsDto Empty()
        {
            return new InsuranceObjectsDto
            {
                Address = new InsuranceObjectAddressDto()
            };
        }

        public static InsuranceObjectsDto Map(OMBuilding omBuilding)
        {
            var chagesValues = new BuildingService().GetBuildChangesData(omBuilding.EmpId);
            DateTime chageDate;
            return new InsuranceObjectsDto
            {
                EmpId = omBuilding.EmpId,
                UploadDate = DateTime.TryParse(chagesValues.changeDate, out chageDate) ? chageDate : omBuilding.LoadDate,
                BuildingStatusByGBU = omBuilding.TypeMkd_Code,
                Unom = omBuilding.Unom,
                InInsuranceProgram = omBuilding.FlagInsur,
				InInsuranceProgramSystem = omBuilding.FlagInsurCalculated,
				CadastralNumber = omBuilding.CadasrNum,
                CadastralRegistrationDate = omBuilding.CadastrDate,
                BuildingStatus = omBuilding.StatusEgrn_Code,
                BuildingPurpose = (long?)omBuilding.PurposeName_Code,
                BtiStatus = (long?)omBuilding.StatusSostBti_Code,
                Okrug = omBuilding.OkrugId.HasValue ? new ObjectModel.Bti.OMBtiOkrug { Id = omBuilding.OkrugId.Value } : null,
                District = omBuilding.ParentBtiDistrict,
                DistrictId = omBuilding.DistrictId,
                ConstructionYear = omBuilding.YearStroi,
                ApartmentsCount = omBuilding.KolGp,
                FloorsCount = omBuilding.CountFloor,
                PassengerElevatorsCount = omBuilding.Lfpq,
                FreightElevatorsCount = omBuilding.Lfgpq,
                TotalArea = omBuilding.Opl,
                UninhabitedPremiseArea = omBuilding.OplN,
                DwellingArea = omBuilding.OplG,
                BalconyArea = omBuilding.Bpl,
                LoggiasArea = omBuilding.Lpl,
                ColdRoomArea = omBuilding.Hpl,
                RoofArea = omBuilding.Krovpl,
                Address = InsuranceObjectAddressDto.Map(omBuilding),
                AccruedSumCurrent = omBuilding.AccruedSumCurrent,
                AccruedOplCurrent = omBuilding.AccruedOplCurrent,
                AccruedSumLast = omBuilding.AccruedSumLast,
                AccruedOplLast = omBuilding.AccruedOplLast,
                CreditedSumCurrent = omBuilding.CreditedSumCurrent,
                CreditedOplCurrent = omBuilding.CreditedOplCurrent,
                CreditedSumLast = omBuilding.CreditedSumLast,
                CreditedOplLast = omBuilding.CreditedOplLast,
                Note = omBuilding.Note
            };
        }

        public static OMBuilding Map(InsuranceObjectsDto model)
        {
            return new OMBuilding
            {
                EmpId = model.EmpId,
                LoadDate = model.UploadDate,
                FlagInsur = model.InInsuranceProgram,
                Unom = model.Unom,
                TypeMkd_Code = model.BuildingStatusByGBU,
                CadasrNum = model.CadastralNumber,
                CadastrDate = model.CadastralRegistrationDate,
                StatusEgrn_Code = model.BuildingStatus ?? State.None,
                PurposeName_Code = model.BuildingPurpose != null ? (Purpose)model.BuildingPurpose : Purpose.None,
                StatusSostBti_Code = model.BtiStatus != null ? (StructureStatus)model.BtiStatus : StructureStatus.None,
                OkrugId = model.Okrug?.Id == -1 ? null : model.Okrug?.Id,
                DistrictId = (model.District?.Id == -1 || model.District?.Id == null) ? model.DistrictId : model.District?.Id,
                YearStroi = model.ConstructionYear,
                KolGp = model.ApartmentsCount,
                CountFloor = model.FloorsCount,
                Lfpq = model.PassengerElevatorsCount,
                Lfgpq = model.FreightElevatorsCount,
                Opl = model.TotalArea,
                OplN = model.UninhabitedPremiseArea,
                OplG = model.DwellingArea,
                Bpl = model.BalconyArea,
                Lpl = model.LoggiasArea,
                Hpl = model.ColdRoomArea,
                Krovpl = model.RoofArea,
                GuidFiasMkd = model.Address?.FIAS,
                ParentAddress = InsuranceObjectAddressDto.Map(model.Address),
                Note = model.Note
            };
        }
    }
}