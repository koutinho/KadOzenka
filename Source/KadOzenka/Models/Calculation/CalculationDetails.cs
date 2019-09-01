using CIPJS.DAL.Building;
using CIPJS.DAL.Calculation;
using CIPJS.DAL.Dictionaries;
using Core.Shared.Exceptions;
using Core.Shared.Extensions;
using Core.SRD;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Bti;
using ObjectModel.Core.SRD;
using ObjectModel.Directory;
using ObjectModel.Ehd;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;

namespace CIPJS.Models.Calculation
{
    public class CalculationDetails
    {
        public static Dictionary<long, SelectListItem> InsuranceList = new Dictionary<long, SelectListItem>();

        static CalculationDetails()
        {
            var incuranceCompanyDict = DictionaryService.GetIncuranceCompanyList();

            if (incuranceCompanyDict.IsNotEmpty())
                InsuranceList = incuranceCompanyDict.ToDictionary(x => x.Key, x => new SelectListItem { Value = x.Key.ToString(), Text = x.Value });
        }

        #region properties
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Доступ только для чтения
        /// </summary>
        public bool IsReadOnly { get; set; }
        /// <summary>
        /// Идентификатор объекта общего имущества
        /// </summary>
        public long? ObjId { get; set; }
        /// <summary>
        /// UNOM МКД
        /// </summary>
        [Display(Name = "UNOM")]
        public long? Unom { get; set; }
        /// <summary>
        /// Адрес МКД
        /// </summary>
        [Display(Name = "Адрес МКД")]
        public string Address { get; set; }
        /// <summary>
        /// Общая площадь по зданию, кв.м.
        /// </summary>
        [Display(Name = "Общая площадь по зданию кв.м.")]
        public decimal? Opl { get; set; }
        /// <summary>
        /// Общая площадь по зданию (БТИ) , кв.м 
        /// </summary>
        [Display(Name = "Общая площадь по зданию (БТИ) , кв.м ")]
        public decimal? OplBti { get; set; }
        /// <summary>
        /// Общая площадь по зданию (ЕГРН) , кв.м
        /// </summary>
        [Display(Name = "Общая площадь по зданию (ЕГРН) , кв.м")]
        public decimal? OplEgrn { get; set; }
        /// <summary>
        /// Площадь балконов, кв.м.
        /// </summary>
        [Display(Name = "Площадь балконов, кв.м.")]
        public decimal? Bpl { get; set; }
        /// <summary>
        /// Площадь лоджий, кв.м.
        /// </summary>
        [Display(Name = "Площадь лоджий, кв.м.")]
        public decimal? Lpl { get; set; }
        /// <summary>
        /// Площадь холодных помещений, кв.м.
        /// </summary>
        [Display(Name = "Площадь холодных помещений, кв.м.")]
        public decimal? Hpl { get; set; }
        /// <summary>
        /// Площадь кровли, кв.м.
        /// </summary>
        [Display(Name = "Площадь кровли, кв.м.")]
        public decimal? Krovpl { get; set; }
        /// <summary>
        /// Площадь помещений, не входящих в общую зону, кв.м.
        /// </summary>
        [Display(Name = "Площадь помещений, не входящих в общую зону, кв.м.")]
        public decimal? Epl { get; set; }
        /// <summary>
        /// Процент износа
        /// </summary>
        [Display(Name = "Процент износа")]
        public decimal? Pizn { get; set; }
        /// <summary>
        /// Строительная стоимость
        /// </summary>
        [Display(Name = "Строительная стоимость")]
        public decimal? StroiPrice { get; set; }
        /// <summary>
        /// Год постройки
        /// </summary>
        [Display(Name = "Год постройки")]
        public long? YearBuild { get; set; }
        /// <summary>
        /// Этажность
        /// </summary>
        [Display(Name = "Этажность")]
        public long? CountFloor { get; set; }
        /// <summary>
        /// Количество квартир
        /// </summary>
        [Display(Name = "Кол-во квартир")]
        public long? KolGp { get; set; }
        /// <summary>
        /// Лифты пассажирские
        /// </summary>
        [Display(Name = "пассажирские")]
        public long? PassengerElevators { get; set; }
        /// <summary>
        /// Лифты грузопассажирские
        /// </summary>
        [Display(Name = "грузопассажирские")]
        public long? CargoElevators { get; set; }
        /// <summary>
        /// Идентификатор страховой компании
        /// </summary>
        public long? InsuranceId { get; set; }
        /// <summary>
        /// Страховая компания
        /// </summary>
        [Display(Name = "Страховая компания")]
        public string InsuranceCompanyName { get; set; }
        /// <summary>
        /// Доля ответственности СК, %
        /// </summary>
        [Display(Name = "Доля ответственности СК, %")]
        public decimal? PartCompensation { get; set; }
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
        /// Показатель рациональности объемно-планировочного решения, R
        /// </summary>
        [Display(Name = "Показатель рациональности объемно-планировочного решения, R")]
        public decimal? IndicatorR { get; set; }
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
        /// <summary>
        /// Стены и перегородки
        /// </summary>
        [Display(Name = "Стены и перегородки")]
        public decimal? Ui2 { get; set; }
        /// <summary>
        /// Перекрытия
        /// </summary>
        [Display(Name = "Перекрытия")]
        public decimal? Ui3 { get; set; }
        /// <summary>
        /// Полы
        /// </summary>
        [Display(Name = "Полы")]
        public decimal? Ui4 { get; set; }
        /// <summary>
        /// Проемы
        /// </summary>
        [Display(Name = "Проемы")]
        public decimal? Ui5 { get; set; }
        /// <summary>
        /// Отделочные работы
        /// </summary>
        [Display(Name = "Отделочные работы")]
        public decimal? Ui6 { get; set; }
        /// <summary>
        /// Прочие
        /// </summary>
        [Display(Name = "Прочие")]
        public decimal? Ui7 { get; set; }
        /// <summary>
        /// Крыша
        /// </summary>
        [Display(Name = "Крыша")]
        public decimal? Ui8 { get; set; }
        /// <summary>
        /// Санитарно-технические работы и внутридомовое инженерное оборудование (исключая лифты)
        /// </summary>
        [Display(Name = "Санитарно-технические работы и внутридомовое инженерное оборудование (исключая лифты)")]
        public decimal? Ui9 { get; set; }
        /// <summary>
        /// Лифты и лифтовое оборудование
        /// </summary>
        [Display(Name = "Лифты и лифтовое оборудование")]
        public decimal? Ui10 { get; set; }
        /// <summary>
        /// Всего
        /// </summary>
        [Display(Name = "Всего")]
        public decimal? Ui11 { get; set; }
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
        /// Номер заявки
        /// </summary>
        [Display(Name = "Номер заявки")]
        public string RequestNumber { get; set; }

        [Display(Name = "Номер пакета")]
        public string PackageNum { get; set; }
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

        [Display(Name = "Статус")]
        public CalculationStatus? StatusCode { get; set; }

        public long? SubjectId { get; set; }

        /// <summary>
        /// Страхователь
        /// </summary>
        [Display(Name = "Страхователь")]
        public string SubjectName { get; set; }

        public long? ContractId { get; set; }
        #endregion

        #region Проект договора
        public long AgreementId { get; set; }

        /// <summary>
        /// Дата получения проекта договора
        /// </summary>
        [Display(Name = "Дата")]
        public DateTime? AgreementGotDate { get; set; }
        /// <summary>
        /// Пользователь, получивший проект договора
        /// </summary>
        [Display(Name = "Сотрудник")]
        public string AgreementGotUserName { get; set; }
        /// <summary>
        /// Должность пользователя, получивший проект договора
        /// </summary>
        [Display(Name = "Должность")]
        public string AgreementGotUserPost { get; set; }
        /// <summary>
        /// Идентификатор пользователя, который получил договор
        /// </summary>
        public long? AgreementGotUserId { get; set; }
        /// <summary>
        /// Дата согласования договора
        /// </summary>
        [Display(Name = "Дата")]
        public DateTime? AgreementApprovalDate { get; set; }
        /// <summary>
        /// Идентификатор пользователя, который согласовал договор
        /// </summary>
        public long? AgreementApprovalUserId { get; set; }
        /// <summary>
        /// Пользователь, который согласовал договор
        /// </summary>
        [Display(Name = "Сотрудник")]
        public string AgreementApprovalUserName { get; set; }
        /// <summary>
        /// Должность пользователя, который согласовал договор
        /// </summary>
        [Display(Name = "Должность")]
        public string AgreementApprovalUserPost { get; set; }
        /// <summary>
        /// Примечание
        /// </summary>
        [Display(Name = "Примечание")]
        public string AgreementNote { get; set; }
        /// <summary>
        /// Для отчета Справка, пункт Замечания по пакету документов
        /// </summary>
        [Display(Name = "Замечания по пакету документов")]
        public string AgreementCommentSpravka { get; set; }
        /// <summary>
        /// Для отчета Справка, пункт Принятое решение с учетом замечаний
        /// </summary>
        [Display(Name = "Принятое решение с учетом замечаний")]
        public string AgreementResumeSpravka { get; set; }
        /// <summary>
        /// Доля города Москвы, %
        /// </summary>
        [Display(Name = "Размер доли города, в праве на имущество, %")]
        public decimal? AgreementPartMoscow { get; set; }

        [Display(Name = "№ проекта договора")]
        public string ProgectNum { get; set; }

        /// <summary>
        /// Предполагаемая дата начала действия страхования. 
        /// </summary>
        [Display(Name = "Предполагаемая дата начала действия страхования")]
        public DateTime? EstimatedInsuranceStartDate { get; set; }

        [Display(Name = "Премия")]
        public decimal? SizeBonusMkd { get; set; }

        [Display(Name = "Премия в месяц")]
        public decimal? SizeBonusMkdMonthly { get; set; }

        [Display(Name = "Взнос")]
        public decimal? Contribution { get; set; }

        List<AgreementProjectCategory> AgreementProjectCategories { get; set; }
        #endregion

        #region Согласование
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
        #endregion

        public long? OkrugId { get; set; }

        [Display(Name = "Округлять")]
        public bool? FlagOkrugl { get; set; }

        public decimal? Oplc { get; set; }

        /// <summary>
        /// Признак Hpl участвует в расчете общей площади по зданию
        /// </summary>
        public bool OplcIncludeHpl { get; set; }

        /// <summary>
        /// Признак Hpl учасвтует в расчете расчетной площади
        /// </summary>
        public bool CalculatedAreaIncludeHpl { get; set; }

        /// <summary>
        /// Может ли пользователь изменять имя страхователя.
        /// </summary>
        public bool CanEditInsuredName { get; set; }


        #region Расчетные значения
        /// <summary>
        /// Стоимость констр. по зданию: Земляные работы, фундамент
        /// </summary>
        [Display(Name = "Земляные работы, фундамент")]
        public decimal? Ci1 { get; set; }

        /// <summary>
        /// Стоимость констр. по зданию: Стены и перегородки
        /// </summary>
        [Display(Name = "Стены и перегородки")]
        public decimal? Ci2 { get; set; }

        /// <summary>
        /// Стоимость констр. по зданию: Перекрытия
        /// </summary>
        [Display(Name = "Перекрытия")]
        public decimal? Ci3 { get; set; }

        /// <summary>
        /// Стоимость констр. по зданию: Полы
        /// </summary>
        [Display(Name = "Полы")]
        public decimal? Ci4 { get; set; }

        /// <summary>
        /// Стоимость констр. по зданию: Проемы
        /// </summary>
        [Display(Name = "Проемы")]
        public decimal? Ci5 { get; set; }

        /// <summary>
        /// Стоимость констр. по зданию: Отделочные работы
        /// </summary>
        [Display(Name = "Отделочные работы")]
        public decimal? Ci6 { get; set; }

        /// <summary>
        /// Стоимость констр. по зданию: Прочие
        /// </summary>
        [Display(Name = "Прочие")]
        public decimal? Ci7 { get; set; }

        /// <summary>
        /// Стоимость констр. по зданию: Крыша
        /// </summary>
        [Display(Name = "Крыша")]
        public decimal? Ci8 { get; set; }

        /// <summary>
        /// Стоимость констр. по зданию: Санитарно-технические работы и внутридомовое инженерное оборудование (исключая лифты)
        /// </summary>
        [Display(Name = "Санитарно-технические работы и внутридомовое инженерное оборудование (исключая лифты)")]
        public decimal? Ci9 { get; set; }

        /// <summary>
        /// Стоимость констр. по зданию: Лифты и лифтовое оборудование
        /// </summary>
        [Display(Name = "Лифты и лифтовое оборудование")]
        public decimal? Ci10 { get; set; }

        /// <summary>
        /// Стоимость констр. общей зоны: Земляные работы, фундамент
        /// </summary>
        [Display(Name = "Земляные работы, фундамент")]
        public decimal? Cim1 { get; set; }

        /// <summary>
        /// Стоимость констр. общей зоны: Стены и перегородки
        /// </summary>
        [Display(Name = "Стены и перегородки")]
        public decimal? Cim2 { get; set; }

        /// <summary>
        /// Стоимость констр. общей зоны: Перекрытия
        /// </summary>
        [Display(Name = "Перекрытия")]
        public decimal? Cim3 { get; set; }

        /// <summary>
        /// Стоимость констр. общей зоны: Полы
        /// </summary>
        [Display(Name = "Полы")]
        public decimal? Cim4 { get; set; }

        /// <summary>
        /// Стоимость констр. общей зоны: Проемы
        /// </summary>
        [Display(Name = "Проемы")]
        public decimal? Cim5 { get; set; }

        /// <summary>
        /// Стоимость констр. общей зоны: Отделочные работы
        /// </summary>
        [Display(Name = "Отделочные работы")]
        public decimal? Cim6 { get; set; }

        /// <summary>
        /// Стоимость констр. общей зоны: Прочие
        /// </summary>
        [Display(Name = "Прочие")]
        public decimal? Cim7 { get; set; }

        /// <summary>
        /// Стоимость констр. общей зоны: Крыша
        /// </summary>
        [Display(Name = "Крыша")]
        public decimal? Cim8 { get; set; }

        /// <summary>
        /// Стоимость констр. общей зоны: Санитарно-технические работы и внутридомовое инженерное оборудование (исключая лифты)
        /// </summary>
        [Display(Name = "Санитарно-технические работы и внутридомовое инженерное оборудование (исключая лифты)")]
        public decimal? Cim9 { get; set; }

        /// <summary>
        /// Стоимость констр. общей зоны: Лифты и лифтовое оборудование
        /// </summary>
        [Display(Name = "Лифты и лифтовое оборудование")]
        public decimal? Cim10 { get; set; }

        /// <summary>
        /// Стоимость констр. по зданию: Всего
        /// </summary>
        [Display(Name = "Всего")]
        public decimal? Ci11 { get; set; }

        /// <summary>
        /// Стоимость констр. общей зоны: Всего
        /// </summary>
        [Display(Name = "Всего")]
        public decimal? Cim11 { get; set; }

        /// <summary>
        /// Страховая сумма - общая зона дома без санитарно-технических работи внутридомового инженерного оборудования
        /// </summary>
        [Display(Name = "Ежемесячный страховой взнос - общая зона дома без санитарно-технических работи внутридомового инженерного оборудования")]
        public decimal? MonthlyBonus1Category { get; set; }

        /// <summary>
        /// Страховая сумма - общая зона дома по санитарно техническим работам и внутридомовому инженерному оборудованию
        /// </summary>
        [Display(Name = "Ежемесячный страховой взнос - общая зона дома по санитарно техническим работам и внутридомовому инженерному оборудованию")]
        public decimal? MonthlyBonus2Category { get; set; }

        /// <summary>
        /// Страховая сумма - лифты и лифтовое оборудование
        /// </summary>
        [Display(Name = "Ежемесячный страховой взнос - лифты и лифтовое оборудование")]
        public decimal? MonthlyBonus3Category { get; set; }

        /// <summary>
        /// Страховая сумма - по дому в целом
        /// </summary>
        [Display(Name = "Ежемесячный страховой взнос - по дому в целом")]
        public decimal? SizeAnnualBonusMonthlyBonus { get; set; }

        /// <summary>
        /// Страховая сумма - по дому в целом
        /// </summary>
        [Display(Name = "Годовая страховая премия - по одной квартире")]
        public decimal? SizeAnnualBonusFlat { get; set; }

        /// <summary>
        /// Страховая сумма - По одной квартире
        /// </summary>
        [Display(Name = "Ежемесячный страховой взнос - по одной квартире")]
        public decimal? SizeAnnualBonusMonthlyBonusFlat { get; set; }
        #endregion

        public static CalculationDetails OMMap(OMParamCalculation entity)
        {

            var calculationDetails = new CalculationDetails
            {
                Id = entity.EmpId,
                ObjId = entity.ObjId,
                InsuranceId = entity.InsuranceId,
                PartCompensation = entity.PartСоmpensation * 100,
                ActualCost = entity.ActualCost,
                CoefActualCost = entity.CoefActualCost,
                ActualCostCurrent = entity.ActualCostCurrent,
                IndicatorR = entity.IndicatorR,
                CalculatedArea = entity.CalculatedArea,
                Oplc = entity.AllArea,
                Ui1 = entity.Ui1,
                Ui2 = entity.Ui2,
                Ui3 = entity.Ui3,
                Ui4 = entity.Ui4,
                Ui5 = entity.Ui5,
                Ui6 = entity.Ui6,
                Ui7 = entity.Ui7,
                Ui8 = entity.Ui8,
                Ui9 = entity.Ui9,
                Ui10 = entity.Ui10,
                Ui11 = entity.Ui11,
                TotalCost1 = entity.TotalCost1,
                DesignCost1 = entity.DesignCost1,
                BasicRate1 = entity.BasicRate1,
                AnnualBonus1 = entity.AnnualBonus1,
                TotalCost2 = entity.TotalCost2,
                DesignCost2 = entity.DesignCost2,
                BasicRate2 = entity.BasicRate2,
                AnnualBonus2 = entity.AnnualBonus2,
                TotalCost3 = entity.TotalCost3,
                DesignCost3 = entity.DesignCost3,
                BasicRate3 = entity.BasicRate3,
                AnnualBonus3 = entity.AnnualBonus3,
                SizeAnnualBonus = entity.SizeAnnualBonus,
                RequestNumber = entity.RequestNumber,
                PackageNum = entity.PackageNum,
                RequestDate = entity.RequestDate,
                CreatedUserId = entity.CreatedUserId,
                ApprovalUserId = entity.ApprovalUserId,
                StatusCode = entity.Status_Code,
                SubjectId = entity.SubjectId,
                CalculatePersonId = entity.AgreementId1,
                CalculatePersonDate = entity.DateFill1,
                InspectorPersonId = entity.AgreementId2,
                InspectorPersonDate = entity.DateFill2,
                ContractId = entity.ContractId,
                FlagOkrugl = entity.FlagOkrugl,
                OplcIncludeHpl = entity.AllAreaIncludeHpl.HasValue && entity.AllAreaIncludeHpl.Value,
                CalculatedAreaIncludeHpl = entity.CalculatedAreaIncludeHpl.HasValue && entity.CalculatedAreaIncludeHpl.Value,
                EstimatedInsuranceStartDate = entity.EstimatedInsuranceStartDate,
                CanEditInsuredName = true
            };




            if (entity.ObjId.HasValue)
            {
                BuildingService _buildingService = new BuildingService();
                OMBuilding building = _buildingService.GetById(entity.ObjId);

                if (building != null)
                {
                    if (building.LinkBtiFsks != null)
                    {
                        calculationDetails.Epl = _buildingService.GetEpl(building.LinkBtiFsks);
                    }

                    calculationDetails.OplBti = OMBtiBuilding.Where(x => x.LinkBuildBti[0].IdInsurBuild == building.EmpId)
                        .Select(x => x.Opl)
                        .ExecuteFirstOrDefault()?.Opl;

                    if (building.LinkEgrnBild.HasValue)
                    {
                        calculationDetails.OplEgrn = OMBuildParcel.Where(x => x.EmpId == building.LinkEgrnBild.Value).Select(x => x.Area).ExecuteFirstOrDefault()?.Area;
                    }

                    calculationDetails.YearBuild = building.YearStroi;
                    calculationDetails.Opl = building.Opl;
                    calculationDetails.CountFloor = building.CountFloor;
                    //CIPJS-767 Если в поле кол-во квартир пусто то выводить ноль
                    calculationDetails.KolGp = building.KolGp ?? 0;
                    calculationDetails.PassengerElevators = building.Lfpq;
                    calculationDetails.CargoElevators = building.Lfgpq;
                    calculationDetails.Address = building.ParentAddress?.FullAddress;
                    calculationDetails.Unom = building.Unom;
                    calculationDetails.Bpl = building.Bpl;
                    calculationDetails.Lpl = building.Lpl;
                    calculationDetails.Hpl = building.Hpl;
                    calculationDetails.Krovpl = building.Krovpl;
                    calculationDetails.StroiPrice = building.StroiPrice;
                    calculationDetails.Pizn = building.Pizn;
                    calculationDetails.OkrugId = building.OkrugId;
                }
            }

            if (entity.CreatedUserId.HasValue)
            {
                var createdUser = SRDCacheUser.GetUser((int)entity.CreatedUserId.Value);

                if (createdUser != null)
                    calculationDetails.CreatedUserName = createdUser.FullName;
            }

            if (entity.ApprovalUserId.HasValue)
            {
                var approvalUser = SRDCacheUser.GetUser((int)entity.ApprovalUserId.Value);

                if (approvalUser != null)
                    calculationDetails.ApprovalUserName = approvalUser.FullName;
            }

            if (calculationDetails.InsuranceId.HasValue)
            {
                calculationDetails.InsuranceCompanyName = InsuranceList.ContainsKey(calculationDetails.InsuranceId.Value) ?
                                                            InsuranceList[calculationDetails.InsuranceId.Value].Text :
                                                            string.Empty;
            }

            CalculationService calculationService = new CalculationService();
            OMAgreementProject project = calculationService.GetAgreementProjectsByCalculationId(calculationDetails.Id);

            if (project != null)
            {
                string gotUserName = string.Empty;
                string gotUserPost = string.Empty;
                string approvalUserName = string.Empty;
                string approvalUserPost = string.Empty;

                if (project.GotUserId.HasValue)
                {
                    var gotUser = SRDCacheUser.GetUser((int)project.GotUserId.Value);

                    if (gotUser != null)
                    {
                        gotUserName = gotUser.FullName;
                        gotUserPost = gotUser.Position;
                    }
                }

                if (project.ApprovalUserId.HasValue)
                {
                    var approvalUser = SRDCacheUser.GetUser((int)project.ApprovalUserId.Value);

                    if (approvalUser != null)
                    {
                        approvalUserName = approvalUser.FullName;
                        approvalUserPost = approvalUser.Position;
                    }

                    var defaultUserRole = OMRole.Where(x => x.RoleName == "Пользователь").Select(x => x.Id).ExecuteFirstOrDefault();
                    if (defaultUserRole != null)
                    {
                        calculationDetails.CanEditInsuredName = OMUserRole.Where(x => x.UserId == SRDSession.GetCurrentUserId()).SelectAll().Execute().All(x => x.RoleId != defaultUserRole.Id);
                    }
                }

                calculationDetails.AgreementId = project.EmpId;
                calculationDetails.AgreementGotDate = project.GotDate;
                calculationDetails.AgreementGotUserId = project.GotUserId;
                calculationDetails.AgreementApprovalDate = project.ApprovalDate;
                calculationDetails.ApprovalUserId = project.ApprovalUserId;
                calculationDetails.AgreementCommentSpravka = project.CommentSpravka;
                calculationDetails.AgreementResumeSpravka = project.ResumeSpravka;
                calculationDetails.AgreementPartMoscow = project.PartMoscow;
                calculationDetails.AgreementGotUserName = gotUserName;
                calculationDetails.AgreementGotUserPost = gotUserPost;
                calculationDetails.AgreementApprovalUserId = project.ApprovalUserId;
                calculationDetails.AgreementApprovalUserName = approvalUserName;
                calculationDetails.AgreementApprovalUserPost = approvalUserPost;
                calculationDetails.AgreementPartMoscow = project.PartMoscow;
                calculationDetails.ProgectNum = project.ProgectNum;
            }
            else
            {
                decimal? partMoscow = null;

                if (entity != null && entity.RequestDate.HasValue)
                    partMoscow = DictionaryService.GetPartCompensationCity(entity.RequestDate.Value); ;

                calculationDetails.AgreementId = -1;
                calculationDetails.AgreementPartMoscow = partMoscow;
            }

            if (calculationDetails.SubjectId.HasValue)
            {
                calculationDetails.SubjectName = DictionaryService.GetSubjectById(calculationDetails.SubjectId.Value)?.SubjectName;
            }

            if (calculationDetails.CalculatePersonId.HasValue)
            {
                OMUser user = OMUser.Where(x => x.Id == calculationDetails.CalculatePersonId).Select(x => x.FullName).Select(x => x.Position).Execute().FirstOrDefault();
                if (user != null)
                {
                    calculationDetails.CalculatePersonFIO = user.FullName;
                    calculationDetails.CalculatePersonPost = user.Position;
                }
            }

            if (calculationDetails.InspectorPersonId.HasValue)
            {
                OMUser user = OMUser.Where(x => x.Id == calculationDetails.InspectorPersonId).Select(x => x.FullName).Select(x => x.Position).Execute().FirstOrDefault();
                if (user != null)
                {
                    calculationDetails.InspectorPersonFIO = user.FullName;
                    calculationDetails.InspectorPersonPost = user.Position;
                }
            }
            if (entity.ObjId.HasValue)
            {
                CalculationValuesOutDto calculationValues = calculationService.Calculate(new CalculationValuesInDto
                {
                    AgreementId = calculationDetails.AgreementId,
                    ActualCost = calculationDetails.ActualCost,
                    ActualCostSet = true,
                    ActualCostCurrent = calculationDetails.ActualCostCurrent,
                    ActualCostCurrentSet = true,
                    AgreementPartMoscow = calculationDetails.AgreementPartMoscow,
                    BasicRate1 = calculationDetails.BasicRate1,
                    BasicRate2 = calculationDetails.BasicRate2,
                    BasicRate3 = calculationDetails.BasicRate3,
                    Bpl = calculationDetails.Bpl,
                    CalculatedArea = calculationDetails.CalculatedArea,
                    CalculatedAreaSet = true,
                    CalculatedAreaIncludeHpl = calculationDetails.CalculatedAreaIncludeHpl,
                    CoefActualCost = calculationDetails.CoefActualCost,
                    FlagOkrugl = calculationDetails.FlagOkrugl,
                    IndicatorR = calculationDetails.IndicatorR,
                    IndicatorRSet = true,
                    KolGp = calculationDetails.KolGp,
                    Krovpl = calculationDetails.Krovpl,
                    Lpl = calculationDetails.Lpl,
                    Opl = calculationDetails.Opl,
                    OplBti = calculationDetails.OplBti,
                    OplEgrn = calculationDetails.OplEgrn,
                    Oplc = calculationDetails.Oplc,
                    OplcSet = calculationDetails.Oplc.HasValue,
                    OplcIncludeHpl = calculationDetails.OplcIncludeHpl,
                    PartCompensation = calculationDetails.PartCompensation,
                    Pizn = calculationDetails.Pizn,
                    StroiPrice = calculationDetails.StroiPrice,
                    Ui1 = calculationDetails.Ui1,
                    Ui2 = calculationDetails.Ui2,
                    Ui3 = calculationDetails.Ui3,
                    Ui4 = calculationDetails.Ui4,
                    Ui5 = calculationDetails.Ui5,
                    Ui6 = calculationDetails.Ui6,
                    Ui7 = calculationDetails.Ui7,
                    Ui8 = calculationDetails.Ui8,
                    Ui9 = calculationDetails.Ui9,
                    Ui10 = calculationDetails.Ui10
                });

                calculationDetails.Ci1 = calculationValues.Ci1;
                calculationDetails.Ci2 = calculationValues.Ci2;
                calculationDetails.Ci3 = calculationValues.Ci3;
                calculationDetails.Ci4 = calculationValues.Ci4;
                calculationDetails.Ci5 = calculationValues.Ci5;
                calculationDetails.Ci6 = calculationValues.Ci6;
                calculationDetails.Ci7 = calculationValues.Ci7;
                calculationDetails.Ci8 = calculationValues.Ci8;
                calculationDetails.Ci9 = calculationValues.Ci9;
                calculationDetails.Ci10 = calculationValues.Ci10;
                calculationDetails.Cim1 = calculationValues.Cim1;
                calculationDetails.Cim2 = calculationValues.Cim2;
                calculationDetails.Cim3 = calculationValues.Cim3;
                calculationDetails.Cim4 = calculationValues.Cim4;
                calculationDetails.Cim5 = calculationValues.Cim5;
                calculationDetails.Cim6 = calculationValues.Cim6;
                calculationDetails.Cim7 = calculationValues.Cim7;
                calculationDetails.Cim8 = calculationValues.Cim8;
                calculationDetails.Cim9 = calculationValues.Cim9;
                calculationDetails.Cim10 = calculationValues.Cim10;
                calculationDetails.Ci11 = calculationValues.Ci11;
                calculationDetails.Cim11 = calculationValues.Cim11;
                calculationDetails.MonthlyBonus1Category = calculationValues.MonthlyBonus1Category;
                calculationDetails.MonthlyBonus2Category = calculationValues.MonthlyBonus2Category;
                calculationDetails.MonthlyBonus3Category = calculationValues.MonthlyBonus3Category;
                calculationDetails.SizeBonusMkd = calculationValues.SizeBonusMkd;
                calculationDetails.SizeBonusMkdMonthly = calculationValues.SizeBonusMkdMonthly;
                calculationDetails.SizeAnnualBonusMonthlyBonus = calculationValues.SizeAnnualBonusMonthlyBonus;
                calculationDetails.SizeAnnualBonusFlat = calculationValues.SizeAnnualBonusFlat;
                calculationDetails.SizeAnnualBonusMonthlyBonusFlat = calculationValues.SizeAnnualBonusMonthlyBonusFlat;
                calculationDetails.Contribution = calculationValues.Contribution;
                calculationDetails.OplcIncludeHpl = calculationValues.OplcIncludeHpl;
                calculationDetails.CalculatedAreaIncludeHpl = calculationValues.CalculatedAreaIncludeHpl;
            }

            return calculationDetails;
        }

        public static OMParamCalculation OMMap(CalculationDetails entity)
        {
            if (entity is null)
                return null;

            return new OMParamCalculation
            {
                EmpId = entity.Id,
                ObjId = entity.ObjId,
                InsuranceId = entity.InsuranceId,
                PartСоmpensation = entity.PartCompensation / 100,
                ActualCost = entity.ActualCost,
                CoefActualCost = entity.CoefActualCost,
                ActualCostCurrent = entity.ActualCostCurrent,
                IndicatorR = entity.IndicatorR,
                CalculatedArea = entity.CalculatedArea,
                AllArea = entity.Oplc,
                Ui1 = entity.Ui1,
                Ui2 = entity.Ui2,
                Ui3 = entity.Ui3,
                Ui4 = entity.Ui4,
                Ui5 = entity.Ui5,
                Ui6 = entity.Ui6,
                Ui7 = entity.Ui7,
                Ui8 = entity.Ui8,
                Ui9 = entity.Ui9,
                Ui10 = entity.Ui10,
                Ui11 = entity.Ui11,
                TotalCost1 = entity.TotalCost1,
                DesignCost1 = entity.DesignCost1,
                BasicRate1 = entity.BasicRate1,
                AnnualBonus1 = entity.AnnualBonus1,
                TotalCost2 = entity.TotalCost2,
                DesignCost2 = entity.DesignCost2,
                BasicRate2 = entity.BasicRate2,
                AnnualBonus2 = entity.AnnualBonus2,
                TotalCost3 = entity.TotalCost3,
                DesignCost3 = entity.DesignCost3,
                BasicRate3 = entity.BasicRate3,
                AnnualBonus3 = entity.AnnualBonus3,
                SizeAnnualBonus = entity.SizeAnnualBonus,
                RequestNumber = entity.RequestNumber,
                PackageNum = entity.PackageNum,
                RequestDate = entity.RequestDate,
                CreatedUserId = entity.CreatedUserId,
                ApprovalUserId = entity.ApprovalUserId,
                SubjectId = entity.SubjectId,
                FlagOkrugl = entity.FlagOkrugl,
                DateFill1 = entity.CalculatePersonDate,
                DateFill2 = entity.InspectorPersonDate,
                AllAreaIncludeHpl = entity.OplcIncludeHpl,
                CalculatedAreaIncludeHpl = entity.CalculatedAreaIncludeHpl,
                EstimatedInsuranceStartDate = entity.EstimatedInsuranceStartDate
            };
        }
    }
}
