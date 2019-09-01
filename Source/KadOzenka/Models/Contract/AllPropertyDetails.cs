using CIPJS.DAL.Building;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CIPJS.Models.Contract
{
    public class AllPropertyDetails
    {
        public long Id { get; set; }

        [Display(Name = "Номер договора")]
        public string ContractNumber { get; set; }

        [Display(Name = "Дата начала действия")]
        public DateTime? BeginDate { get; set; }

        [Display(Name = "Статус")]
        public ContractStatus? Status { get; set; }

        [Display(Name = "Страховая компания")]
        public string InsuranceCompany { get; set; }

        [Display(Name = "Доля ответственности СК, %")]
        public decimal? PartInsuranceCompany { get; set; }

        [Display(Name = "Доля города Москвы, %")]
        public decimal? PartMoscow { get; set; }

        [Display(Name = "Страхователь")]
        public string Policyholder { get; set; }

        [Display(Name = "Форма объединения собственников")]
        public FormAssociationOwners? OrgType { get; set; }

        [Display(Name = "Признак рассрочки платежа")]
        public SignInstallmentPayment? Paysign { get; set; }

        public decimal? St1 { get; set; }

        public decimal? St2 { get; set; }

        public decimal? St3 { get; set; }

        public decimal? Ss1 { get; set; }

        public decimal? Ss2 { get; set; }

        public decimal? Ss3 { get; set; }

        public decimal? DamageElem1 { get; set; }

        public decimal? DamageElem2 { get; set; }

        public decimal? DamageElem3 { get; set; }

        [Display(Name = "Размер страховой премии")]
        public decimal? RasPripay { get; set; }

        public decimal? SizeBonusMkd { get; set; }

        public long? BuildingId { get; set; }

        [Display(Name = "UNOM")]
        public long? Unom { get; set; }

        [Display(Name = "Кадастровый номер")]
        public string CadastralNumber { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        /// <summary>
        /// Действительная стоимость дома, руб
        /// </summary>
        [Display(Name = "Действительная стоимость дома, руб.")]
        public decimal? ActualCost { get; set; }
        /// <summary>
        /// Коэффициент пересчета действительной стоимости
        /// </summary>
        [Display(Name = "Коэффициент пересчета действительной стоимости")]
        public decimal? CoefActualCost { get; set; }
        /// <summary>
        /// Действительная стоимость дома (в пересчете на текущие цены), руб.
        /// </summary>
        [Display(Name = "Действительная стоимость дома (в пересчете на текущие цены), руб.")]
        public decimal? ActualCostCurrent { get; set; }
        /// <summary>
        /// Показатель рациональности объемно-планировочного решени, R
        /// </summary>
        [Display(Name = "Показатель рациональности объемно-планировочного решени, R")]
        public decimal? IndicatorR { get; set; }
        /// <summary>
        /// Общая площадь по зданию (включая холодные помещения), кв.м.
        /// </summary>
        [Display(Name = "Общая площадь по зданию (включая холодные помещения), кв.м.")]
        public decimal? Oplc { get; set; }
        /// <summary>
        /// Расчетная площадь для определения стоимости общего имущества в МКД, кв.м.
        /// </summary>
        [Display(Name = "Расчетная площадь для определения страховой стоимости общего имущества в МКД, кв.м.")]
        public decimal? CalculatedArea { get; set; }
        /// <summary>
        /// Земляные работы, фундамент
        /// </summary>
        [Display(Name = "Земляные работы, фундамент")]
        public decimal? Ui1 { get; set; }
        public decimal? Ui1ValueByBuilding { get; set; }
        public decimal? Ui1ValueCommonArea { get; set; }
        /// <summary>
        /// Стены и перегородки
        /// </summary>
        [Display(Name = "Стены и перегородки")]
        public decimal? Ui2 { get; set; }
        public decimal? Ui2ValueByBuilding { get; set; }
        public decimal? Ui2ValueCommonArea { get; set; }
        /// <summary>
        /// Перекрытия
        /// </summary>
        [Display(Name = "Перекрытия")]
        public decimal? Ui3 { get; set; }
        public decimal? Ui3ValueByBuilding { get; set; }
        public decimal? Ui3ValueCommonArea { get; set; }
        /// <summary>
        /// Полы
        /// </summary>
        [Display(Name = "Полы")]
        public decimal? Ui4 { get; set; }
        public decimal? Ui4ValueByBuilding { get; set; }
        public decimal? Ui4ValueCommonArea { get; set; }
        /// <summary>
        /// Проемы
        /// </summary>
        [Display(Name = "Проемы")]
        public decimal? Ui5 { get; set; }
        public decimal? Ui5ValueByBuilding { get; set; }
        public decimal? Ui5ValueCommonArea { get; set; }
        /// <summary>
        /// Отделочные работы
        /// </summary>
        [Display(Name = "Отделочные работы")]
        public decimal? Ui6 { get; set; }
        public decimal? Ui6ValueByBuilding { get; set; }
        public decimal? Ui6ValueCommonArea { get; set; }
        /// <summary>
        /// Прочие
        /// </summary>
        [Display(Name = "Прочие")]
        public decimal? Ui7 { get; set; }
        public decimal? Ui7ValueByBuilding { get; set; }
        public decimal? Ui7ValueCommonArea { get; set; }
        /// <summary>
        /// Крыша
        /// </summary>
        [Display(Name = "Крыша")]
        public decimal? Ui8 { get; set; }
        public decimal? Ui8ValueByBuilding { get; set; }
        public decimal? Ui8ValueCommonArea { get; set; }
        /// <summary>
        /// Санитарно-технические работы и внутридомовое инженерное оборудование (исключая лифты)
        /// </summary>
        [Display(Name = "Санитарно-технические работы и внутридомовое инженерное оборудование (исключая лифты)")]
        public decimal? Ui9 { get; set; }
        public decimal? Ui9ValueByBuilding { get; set; }
        public decimal? Ui9ValueCommonArea { get; set; }
        /// <summary>
        /// Лифты и лифтовое оборудование
        /// </summary>
        [Display(Name = "Лифты и лифтовое оборудование")]
        public decimal? Ui10 { get; set; }
        public decimal? Ui10ValueByBuilding { get; set; }
        public decimal? Ui10ValueCommonArea { get; set; }
        /// <summary>
        /// Всего
        /// </summary>
        [Display(Name = "Всего")]
        public decimal? Ui11 { get; set; }
        public decimal? Ui11ValueByBuilding { get; set; }
        public decimal? Ui11ValueCommonArea { get; set; }
        /// <summary>
        /// Общая стоимость конструкций без санитарно-технических работ и внутридомового инженерного оборудования
        /// </summary>
        [Display(Name = "Страховая стоимость конструкций")]
        public decimal? TotalCost1 { get; set; }
        /// <summary>
        /// Сумма конструкций без санитарно-технических работ и внутридомового инженерного оборудования
        /// </summary>
        [Display(Name = "Страховая сумма")]
        public decimal? DesignCost1 { get; set; }
        /// <summary>
        /// Базовый тариф
        /// </summary>
        [Display(Name = "Базовый тариф")]
        public decimal? BasicRate1 { get; set; }
        /// <summary>
        /// Годовая премия
        /// </summary>
        [Display(Name = "Годовая премия")]
        public decimal? AnnualBonus1 { get; set; }
        /// <summary>
        /// Общая стоимость конструкций по санитарно-техническим работам и внутридомовому инженерному оборудованию
        /// </summary>
        [Display(Name = "Страховая стоимость конструкций")]
        public decimal? TotalCost2 { get; set; }
        /// <summary>
        /// Сумма конструкций по санитарно-техническим работам и внутридомовому инженерному оборудованию
        /// </summary>
        [Display(Name = "Страховая сумма")]
        public decimal? DesignCost2 { get; set; }
        /// <summary>
        /// Базовый тариф
        /// </summary>
        [Display(Name = "Базовый тариф")]
        public decimal? BasicRate2 { get; set; }
        /// <summary>
        /// Годовая премия
        /// </summary>
        [Display(Name = "Годовая премия")]
        public decimal? AnnualBonus2 { get; set; }

        /// <summary>
        /// Общая стоимость конструкций лифтов и лифтового оборудования
        /// </summary>
        [Display(Name = "Страховая стоимость конструкций")]
        public decimal? TotalCost3 { get; set; }
        /// <summary>
        /// Сумма конструкций лифтов и лифтового оборудования
        /// </summary>
        [Display(Name = "Страховая сумма")]
        public decimal? DesignCost3 { get; set; }
        /// <summary>
        /// Базовый тариф
        /// </summary>
        [Display(Name = "Базовый тариф")]
        public decimal? BasicRate3 { get; set; }
        /// <summary>
        /// Годовая премия
        /// </summary>
        [Display(Name = "Годовая премия")]
        public decimal? AnnualBonus3 { get; set; }
        /// <summary>
        /// Размер годовой премии по дому, руб
        /// </summary>
        [Display(Name = "Размер годовой премии по дому, руб.")]
        public decimal? SizeAnnualBonus { get; set; }
        /// <summary>
        /// Размер годовой премии по дому, руб
        /// </summary>
        [Display(Name = "Размер годовой премии по квартире, руб.")]
        public decimal? SizeAnnualBonusFlat { get; set; }
        /// <summary>
        /// Размер годовой премии по дому, руб
        /// </summary>
        [Display(Name = "Размер ежемесячной премии, руб.")]
        public decimal? MonthlyBonus1 { get; set; }
        /// <summary>
        /// Размер годовой премии по дому, руб
        /// </summary>
        [Display(Name = "Размер ежемесячной премии, руб.")]
        public decimal? MonthlyBonus2 { get; set; }
        /// <summary>
        /// Размер годовой премии по дому, руб
        /// </summary>
        [Display(Name = "Размер ежемесячной премии, руб.")]
        public decimal? MonthlyBonus3 { get; set; }
        /// <summary>
        /// Размер годовой премии по дому, руб
        /// </summary>
        [Display(Name = "Размер ежемесячной премии по дому, руб.")]
        public decimal? SizeAnnualBonusMonthlyBonus { get; set; }
        /// <summary>
        /// Размер годовой премии по дому, руб
        /// </summary>
        [Display(Name = "Размер ежемесячной премии по квартире, руб.")]
        public decimal? SizeAnnualBonusMonthlyBonusFlat { get; set; }
        /// <summary>
        /// Номер заявки
        /// </summary>
        [Display(Name = "Номер заявки")]
        public string RequestNumber { get; set; }
        /// <summary>
        /// Размер годовой премии по дому, руб
        /// </summary>
        [DataType(DataType.Date, ErrorMessage = "Неверное значение {0} для даты {1}")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата заявки")]
        public DateTime? RequestDate { get; set; }
        /// <summary>
        /// Пользователь, который создал расчет
        /// </summary>
        [Display(Name = "Сотрудник:")]
        public string CreatedUserName { get; set; }
        /// <summary>
        /// Идентификатор пользователя, который создал расчет
        /// </summary>
        public long? CreatedUserId { get; set; }
        /// <summary>
        /// Пользователь, который согласовал расчет
        /// </summary>
        [Display(Name = "Ответственный сотрудник:")]
        public string ApprovalUserName { get; set; }
        /// <summary>
        /// Идентификатор пользователя, который согласовал расчет
        /// </summary>
        public long? ApprovalUserId { get; set; }
        /// <summary>
        /// максимальная сумма возврата
        /// </summary>
        [Display(Name = "максимальная сумма возврата")]
        public string NMax { get; set; }

        #region Agreement
        public bool CanChecked { get; set; }

        public bool CanAgreed { get; set; }

        [Display(Name = "Дата")]
        public DateTime? CalculatePersonDate { get; set; }

        [Display(Name = "Первый проверяющий")]
        public long? CalculatePersonId { get; set; }

        [Display(Name = "Сотрудник")]
        public string CalculatePersonFIO { get; set; }

        [Display(Name = "Должность")]
        public string CalculatePersonPost { get; set; }

        [Display(Name = "Дата")]
        public DateTime? InspectorPersonDate { get; set; }

        [Display(Name = "Второй проверяющий")]
        public long? InspectorPersonId { get; set; }

        [Display(Name = "Сотрудник")]
        public string InspectorPersonFIO { get; set; }

        [Display(Name = "Должность")]
        public string InspectorPersonPost { get; set; }

        [Display(Name = "Дата")]
        public DateTime? PrimaryMatchingDate { get; set; }

        [Display(Name = "Основной согласующий")]
        public long? PrimaryMatchingPersonId { get; set; }

        [Display(Name = "Сотрудник")]
        public string PrimaryMatchingFIO { get; set; }

        [Display(Name = "Должность")]
        public string PrimaryMatchingPost { get; set; }
        #endregion

        /// <summary>
        /// Всего к выплате
        /// </summary>
        public decimal? TotalSumPay { get; set; }

        /// <summary>
        /// Выплачено
        /// </summary>
        public decimal? TotalSumPaid { get; set; }

        /// <summary>
        /// Подлежит оплате
        /// </summary>
        public decimal? TotalMustPay { get; set; }

        /// <summary>
        /// Остаток
        /// </summary>
        public decimal? TotalRemainder { get; set; }

        #region field with limit
        /// <summary>
        /// Общая стоимость конструкций без санитарно-технических работ и внутридомового инженерного оборудования
        /// </summary>
        [Display(Name = "Страховая стоимость конструкций")]
        public decimal? TotalCost1Limit { get; set; }
        /// <summary>
        /// Сумма конструкций без санитарно-технических работ и внутридомового инженерного оборудования
        /// </summary>
        [Display(Name = "Страховая сумма")]
        public decimal? DesignCost1Limit { get; set; }
        /// <summary>
        /// Общая стоимость конструкций по санитарно-техническим работам и внутридомовому инженерному оборудованию
        /// </summary>
        [Display(Name = "Страховая стоимость конструкций")]
        public decimal? TotalCost2Limit { get; set; }
        /// <summary>
        /// Сумма конструкций по санитарно-техническим работам и внутридомовому инженерному оборудованию
        /// </summary>
        [Display(Name = "Страховая сумма")]
        public decimal? DesignCost2Limit { get; set; }
        /// <summary>
        /// Общая стоимость конструкций лифтов и лифтового оборудования
        /// </summary>
        [Display(Name = "Страховая стоимость конструкций")]
        public decimal? TotalCost3Limit { get; set; }
        /// <summary>
        /// Сумма конструкций лифтов и лифтового оборудования
        /// </summary>
        [Display(Name = "Страховая сумма")]
        public decimal? DesignCost3Limit { get; set; }
        #endregion

        public bool HasCalculation { get; set; }

        public static AllPropertyDetails Map(OMAllProperty entity, OMParamCalculation omParamCalculation, OMAgreementProject project)
        {
            if (entity is null) return null;

            BuildingService buildingService = new BuildingService();

            //TODO дублирование кода с подсчетом общей площади на клиентской части в ComonData.cshtml
            decimal? sizeAnnualBonusFlat = omParamCalculation?.SizeAnnualBonus != null && (entity.ParentBuilding?.KolGp ?? 0) > 0
                ? omParamCalculation?.SizeAnnualBonus / entity.ParentBuilding.KolGp : null;

            decimal? ui1ValueBuilding = GetUiBuildingValue(omParamCalculation?.Ui1, omParamCalculation?.ActualCostCurrent);
            decimal? ui1ValueCommonArea = GetUiCommonAreaValue(ui1ValueBuilding, omParamCalculation?.IndicatorR);
            decimal? ui2ValueBuilding = GetUiBuildingValue(omParamCalculation?.Ui2, omParamCalculation?.ActualCostCurrent);
            decimal? ui2ValueCommonArea = GetUiCommonAreaValue(ui2ValueBuilding, omParamCalculation?.IndicatorR);
            decimal? ui3ValueBuilding = GetUiBuildingValue(omParamCalculation?.Ui3, omParamCalculation?.ActualCostCurrent);
            decimal? ui3ValueCommonArea = GetUiCommonAreaValue(ui3ValueBuilding, omParamCalculation?.IndicatorR);
            decimal? ui4ValueBuilding = GetUiBuildingValue(omParamCalculation?.Ui4, omParamCalculation?.ActualCostCurrent);
            decimal? ui4ValueCommonArea = GetUiCommonAreaValue(ui4ValueBuilding, omParamCalculation?.IndicatorR);
            decimal? ui5ValueBuilding = GetUiBuildingValue(omParamCalculation?.Ui5, omParamCalculation?.ActualCostCurrent);
            decimal? ui5ValueCommonArea = GetUiCommonAreaValue(ui5ValueBuilding, omParamCalculation?.IndicatorR);
            decimal? ui6ValueBuilding = GetUiBuildingValue(omParamCalculation?.Ui6, omParamCalculation?.ActualCostCurrent);
            decimal? ui6ValueCommonArea = GetUiCommonAreaValue(ui6ValueBuilding, omParamCalculation?.IndicatorR);
            decimal? ui7ValueBuilding = GetUiBuildingValue(omParamCalculation?.Ui7, omParamCalculation?.ActualCostCurrent);
            decimal? ui7ValueCommonArea = GetUiCommonAreaValue(ui7ValueBuilding, omParamCalculation?.IndicatorR);
            decimal? ui8ValueBuilding = GetUiBuildingValue(omParamCalculation?.Ui8, omParamCalculation?.ActualCostCurrent);
            decimal? ui8ValueCommonArea = GetUiCommonAreaValue(ui8ValueBuilding, omParamCalculation?.IndicatorR);
            decimal? ui9ValueBuilding = GetUiBuildingValue(omParamCalculation?.Ui9, omParamCalculation?.ActualCostCurrent);
            decimal? ui9ValueCommonArea = GetUiCommonAreaValue(ui9ValueBuilding, omParamCalculation?.IndicatorR);
            decimal? ui10ValueBuilding = GetUiBuildingValue(omParamCalculation?.Ui10, omParamCalculation?.ActualCostCurrent);
            decimal? ui10ValueCommonArea = GetUiCommonAreaValue(ui10ValueBuilding, omParamCalculation?.IndicatorR);
            decimal? ui11ValueBuilding = GetUiBuildingValue(omParamCalculation?.Ui11, omParamCalculation?.ActualCostCurrent);
            decimal? ui11ValueCommonArea = GetUiCommonAreaValue(ui11ValueBuilding, omParamCalculation?.IndicatorR);


            var st1 = entity.St1;
            var totalCost1 = omParamCalculation?.TotalCost1;
            var st2 = entity.St2;
            var totalCost2 = omParamCalculation?.TotalCost1;
            var st3 = entity.St3;
            var totalCost3 = omParamCalculation?.TotalCost1;
            var ss1 = entity.Ss1;
            var designCost1 = omParamCalculation?.DesignCost1;
            var ss2 = entity.Ss2;
            var designCost2 = omParamCalculation?.DesignCost2;
            var ss3 = entity.Ss3;
            var designCost3 = omParamCalculation?.DesignCost3;

            if (project != null)
            {
                st1 = project.Kat1.HasValue && project.Kat1.Value ? entity.St1 : 0;
                totalCost1 = project.Kat1.HasValue && project.Kat1.Value ? omParamCalculation?.TotalCost1 : 0;
                st2 = project.Kat2.HasValue && project.Kat2.Value ? entity.St2 : 0;
                totalCost2 = project.Kat2.HasValue && project.Kat2.Value ? omParamCalculation?.TotalCost2 : 0;
                st3 = project.Kat3.HasValue && project.Kat3.Value ? entity.St3 : 0;
                totalCost3 = project.Kat3.HasValue && project.Kat3.Value ? omParamCalculation?.TotalCost3 : 0;
                ss1 = project.Kat1.HasValue && project.Kat1.Value ? entity.Ss1 : 0;
                designCost1 = project.Kat1.HasValue && project.Kat1.Value ? omParamCalculation?.DesignCost1 : 0;
                ss2 = project.Kat2.HasValue && project.Kat2.Value ? entity.Ss2: 0;
                designCost2 = project.Kat2.HasValue && project.Kat2.Value ? omParamCalculation?.DesignCost2 : 0;
                ss3 = project.Kat3.HasValue && project.Kat3.Value ? entity.Ss3 : 0;
                designCost3 = project.Kat3.HasValue && project.Kat3.Value ? omParamCalculation?.DesignCost3 : 0;
            }

            var result = new AllPropertyDetails()
            {
                Id = entity.EmpId,
                ContractNumber = entity.Ndog,
                BeginDate = entity.Ndogdat,
                Status = entity.Status_Code,
                InsuranceCompany = entity.ParentInsuranceOrganization?.ShortName,
                PartInsuranceCompany = entity.Part * 100,
                PartMoscow = entity.PartCity,
                Policyholder = entity.Name,
                OrgType = entity.OrgType_Code,
                Paysign = entity.Paysign_Code,
                St1 = st1,
                TotalCost1Limit = totalCost1,
                St2 = st2,
                TotalCost2Limit = totalCost2,
                St3 = st3,
                TotalCost3Limit = totalCost3,
                Ss1 = ss1,
                DesignCost1Limit = designCost1,
                Ss2 = ss2,
                DesignCost2Limit = designCost2,
                Ss3 = ss3,
                DesignCost3Limit = designCost3,
                RasPripay = entity.RasPripay,
                SizeBonusMkd = project?.SizeBonusMkd,
                BuildingId = entity.ObjId,
                Unom = entity.ParentBuilding?.Unom,
                CadastralNumber = entity.ParentBuilding?.CadasrNum,
                Address = entity.ParentBuilding?.ParentAddress?.FullAddress,

                ActualCost = omParamCalculation?.ActualCost,
                CoefActualCost = omParamCalculation?.CoefActualCost,
                ActualCostCurrent = omParamCalculation?.ActualCostCurrent,
                IndicatorR = omParamCalculation?.IndicatorR,
                //TODO дублирование кода с подсчетом общей площади на клиентской части в ComonData.cshtml
                Oplc = buildingService.GetBuildingOplc(entity.ParentBuilding),
                CalculatedArea = omParamCalculation?.CalculatedArea,
                Ui1 = omParamCalculation?.Ui1,
                Ui1ValueByBuilding = ui1ValueBuilding,
                Ui1ValueCommonArea = ui1ValueCommonArea,
                Ui2 = omParamCalculation?.Ui2,
                Ui2ValueByBuilding = ui2ValueBuilding,
                Ui2ValueCommonArea = ui2ValueCommonArea,
                Ui3 = omParamCalculation?.Ui3,
                Ui3ValueByBuilding = ui3ValueBuilding,
                Ui3ValueCommonArea = ui3ValueCommonArea,
                Ui4 = omParamCalculation?.Ui4,
                Ui4ValueByBuilding = ui4ValueBuilding,
                Ui4ValueCommonArea = ui4ValueCommonArea,
                Ui5 = omParamCalculation?.Ui5,
                Ui5ValueByBuilding = ui5ValueBuilding,
                Ui5ValueCommonArea = ui5ValueCommonArea,
                Ui6 = omParamCalculation?.Ui6,
                Ui6ValueByBuilding = ui6ValueBuilding,
                Ui6ValueCommonArea = ui6ValueCommonArea,
                Ui7 = omParamCalculation?.Ui7,
                Ui7ValueByBuilding = ui7ValueBuilding,
                Ui7ValueCommonArea = ui7ValueCommonArea,
                Ui8 = omParamCalculation?.Ui8,
                Ui8ValueByBuilding = ui8ValueBuilding,
                Ui8ValueCommonArea = ui8ValueCommonArea,
                Ui9 = omParamCalculation?.Ui9,
                Ui9ValueByBuilding = ui9ValueBuilding,
                Ui9ValueCommonArea = ui9ValueCommonArea,
                Ui10 = omParamCalculation?.Ui10,
                Ui10ValueByBuilding = ui10ValueBuilding,
                Ui10ValueCommonArea = ui10ValueCommonArea,
                Ui11 = omParamCalculation?.Ui11,
                Ui11ValueByBuilding = ui11ValueBuilding,
                Ui11ValueCommonArea = ui11ValueCommonArea,
                TotalCost1 = omParamCalculation?.TotalCost1,
                DesignCost1 = omParamCalculation?.DesignCost1,
                BasicRate1 = omParamCalculation?.BasicRate1,
                AnnualBonus1 = omParamCalculation?.AnnualBonus1,
                //TODO дублирование кода с подсчетом на клиентской части в ComonData.cshtml
                MonthlyBonus1 = omParamCalculation?.AnnualBonus1 != null ? omParamCalculation?.AnnualBonus1 / 12 : null,
                TotalCost2 = omParamCalculation?.TotalCost2,
                DesignCost2 = omParamCalculation?.DesignCost2,
                BasicRate2 = omParamCalculation?.BasicRate2,
                AnnualBonus2 = omParamCalculation?.AnnualBonus2,
                //TODO дублирование кода с подсчетом на клиентской части в ComonData.cshtml
                MonthlyBonus2 = omParamCalculation?.AnnualBonus2 != null ? omParamCalculation?.AnnualBonus2 / 12 : null,
                TotalCost3 = omParamCalculation?.TotalCost3,
                DesignCost3 = omParamCalculation?.DesignCost3,
                BasicRate3 = omParamCalculation?.BasicRate3,
                AnnualBonus3 = omParamCalculation?.AnnualBonus3,
                //TODO дублирование кода с подсчетом на клиентской части в ComonData.cshtml
                MonthlyBonus3 = omParamCalculation?.AnnualBonus3 != null ? omParamCalculation?.AnnualBonus3 / 12 : null,
                SizeAnnualBonus = omParamCalculation?.SizeAnnualBonus,
                //TODO дублирование кода с подсчетом на клиентской части в ComonData.cshtml
                SizeAnnualBonusMonthlyBonus = omParamCalculation?.SizeAnnualBonus != null ? omParamCalculation?.SizeAnnualBonus / 12 : null,
                //TODO дублирование кода с подсчетом премии по квартире на клиентской части в ComonData.cshtml
                SizeAnnualBonusFlat = sizeAnnualBonusFlat,
                SizeAnnualBonusMonthlyBonusFlat = sizeAnnualBonusFlat != null ? sizeAnnualBonusFlat / 12 : null,

                RequestNumber = omParamCalculation?.RequestNumber,
                RequestDate = omParamCalculation?.RequestDate,
                CreatedUserId = omParamCalculation?.CreatedUserId,
                ApprovalUserId = omParamCalculation?.ApprovalUserId,

                NMax = Math.Round((entity.RasPripay * entity.PartCity) / 100 ?? 0, 2).ToString("n2"),

                //CalculatePersonId = entity.AgreementId1,
                //CalculatePersonDate = entity.DateFill1,
                //InspectorPersonId = entity.AgreementId2,
                //InspectorPersonDate = entity.DateFill2,
                //PrimaryMatchingPersonId = entity.MainAgreementId,
                //PrimaryMatchingDate = entity.DateFillMain,
                CanChecked = true,
                CanAgreed = true,

                //TotalSumPay = totalSumPay,
                //TotalSumPaid = totalSumPaid,
                //TotalMustPay = totalMustPay,
                //TotalRemainder = totalRemainder,

                HasCalculation = OMParamCalculation.Where(x => x.ContractId == entity.EmpId).ExecuteExists()
            };

            FillControlData(entity, result);

            return result;
        }

        private static decimal? GetUiBuildingValue(decimal? uiValue, decimal? actualCostCurrent)
        {
            if (!uiValue.HasValue || !actualCostCurrent.HasValue)
            {
                return null;
            }

            return (uiValue.Value / 100) * actualCostCurrent.Value;
        }
        private static decimal? GetUiCommonAreaValue(decimal? uiBuildingValue, decimal? indicatorR)
        {
            if (!uiBuildingValue.HasValue || !indicatorR.HasValue)
            {
                return null;
            }

            return indicatorR.Value * uiBuildingValue.Value;
        }

        public static void FillControlData(OMAllProperty entity, AllPropertyDetails model)
        {
            if (entity != null && model != null)
            {
                decimal? oplDog = OMInputPlat.Where(x => x.LinkAllPropertyId == entity.EmpId).Select(x => x.SumOpl).Execute().Sum(x => x.SumOpl);
                decimal? invocieSum = OMInvoice.Where(x => x.LinkAllProperty == entity.EmpId
                    && (x.Status_Code == InvoiceStatus.Included || x.Status_Code == InvoiceStatus.TransferredPayment))
                    .Select(x => x.SumOpl).Execute().Sum(x => x.SumOpl);
                var totalSumPay = entity.RasPripay * entity.PartCity / 100;
                var totalSumPaid = OMInvoice.Where(x => x.LinkAllProperty == entity.EmpId && (x.Status_Code == InvoiceStatus.Included || x.Status_Code == InvoiceStatus.TransferredPayment))
                       .Select(x => x.SumOpl).Execute().Sum(x => x.SumOpl);

                decimal totalMustPay = 0;
                if (entity.RasPripay == oplDog)
                {
                    totalMustPay = Math.Round((totalSumPay ?? 0) - (invocieSum ?? 0), 2);
                }
                //CIPJS-136 Если INSUR_ALL_PROPERTY.RAS_PRIPAY ( размер годовой премии) > OPL_DOG, то
                //считаем ДОЛЮ, сколько уже выплачено от суммы премии = OPL_DOG / INSUR_ALL_PROPERTY.RAS_PRIPAY(округлили до 2 - х знаков после запятой)
                //СУММА СЧЕТА = ДОЛЯ * Nmax – OPL_max
                else if (entity.RasPripay > oplDog)
                {
                    decimal partDog = (oplDog / entity.RasPripay) ?? 0;
                    totalMustPay = totalSumPay.HasValue && invocieSum.HasValue ? Math.Round(partDog * (totalSumPay.Value - invocieSum.Value), 2) : 0m;
                }

                model.TotalSumPay = totalSumPay;
                model.TotalSumPaid = totalSumPaid;
                model.TotalMustPay = totalMustPay;
                model.TotalRemainder = totalSumPay - totalSumPaid - totalMustPay;
            }
        }
    }
}