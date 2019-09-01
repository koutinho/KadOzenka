using System.ComponentModel.DataAnnotations;

namespace CIPJS.DAL.Calculation
{
    public class CalculationValuesOutDto
    {
        [Display(Name = "Общая площадь по зданию (включая холодные помещения), кв.м.")]
        public decimal? Oplc { get; set; }

        /// <summary>
        /// Признак Hpl участвует в расчете общей площади по зданию
        /// </summary>
        public bool OplcIncludeHpl { get; set; }
        
        /// <summary>
        /// Расчетная площадь для определения стоимости общего имущества в МКД, кв.м.
        /// </summary>
        [Display(Name = "Расчетная площадь для определения страховой стоимости общего имущества в МКД, кв.м.")]
        public decimal? CalculatedArea { get; set; }

        /// <summary>
        /// Признак Hpl учасвтует в расчете расчетной площади
        /// </summary>
        public bool CalculatedAreaIncludeHpl { get; set; }
        
        /// <summary>
        /// Показатель рациональности объемно-планировочного решени, R
        /// </summary>
        [Display(Name = "Показатель рациональности объемно-планировочного решени, R")]
        public decimal? IndicatorR { get; set; }

        /// <summary>
        /// Действительная стоимость дома, руб
        /// </summary>
        [Display(Name = "Действительная стоимость дома, руб.")]
        public decimal? ActualCost { get; set; }

        /// <summary>
        /// Действительная стоимость дома (в пересчете на текущие цены), руб.
        /// </summary>
        [Display(Name = "Действительная стоимость дома (в пересчете на текущие цены), руб.")]
        public decimal? ActualCostCurrent { get; set; }

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
        /// Всего
        /// </summary>
        [Display(Name = "Всего")]
        public decimal? Ui11 { get; set; }

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
        /// Страховая стоимость - общая зона дома без санитарно-технических работи внутридомового инженерного оборудования
        /// </summary>
        [Display(Name = "Страховая стоимость - общая зона дома без санитарно-технических работи внутридомового инженерного оборудования")]
        public decimal? TotalCost1Category { get; set; }

        /// <summary>
        /// Страховая стоимость - общая зона дома по санитарно техническим работам и внутридомовому инженерному оборудованию
        /// </summary>
        [Display(Name = "Страховая стоимость - общая зона дома по санитарно техническим работам и внутридомовому инженерному оборудованию")]
        public decimal? TotalCost2Category { get; set; }

        /// <summary>
        /// Страховая стоимость - лифты и лифтовое оборудование
        /// </summary>
        [Display(Name = "Страховая стоимость - лифты и лифтовое оборудование")]
        public decimal? TotalCost3Category { get; set; }

        /// <summary>
        /// Страховая сумма - общая зона дома без санитарно-технических работи внутридомового инженерного оборудования
        /// </summary>
        [Display(Name = "Страховая сумма - общая зона дома без санитарно-технических работи внутридомового инженерного оборудования")]
        public decimal? DesignCost1Category { get; set; }

        /// <summary>
        /// Страховая сумма - общая зона дома по санитарно техническим работам и внутридомовому инженерному оборудованию
        /// </summary>
        [Display(Name = "Страховая сумма - общая зона дома по санитарно техническим работам и внутридомовому инженерному оборудованию")]
        public decimal? DesignCost2Category { get; set; }

        /// <summary>
        /// Страховая сумма - лифты и лифтовое оборудование
        /// </summary>
        [Display(Name = "Страховая сумма - лифты и лифтовое оборудование")]
        public decimal? DesignCost3Category { get; set; }

        /// <summary>
        /// Страховая сумма - общая зона дома без санитарно-технических работи внутридомового инженерного оборудования
        /// </summary>
        [Display(Name = "Годовая страховая премия - общая зона дома без санитарно-технических работи внутридомового инженерного оборудования")]
        public decimal? AnnualBonus1Category { get; set; }

        /// <summary>
        /// Страховая сумма - общая зона дома по санитарно техническим работам и внутридомовому инженерному оборудованию
        /// </summary>
        [Display(Name = "Годовая страховая премия - общая зона дома по санитарно техническим работам и внутридомовому инженерному оборудованию")]
        public decimal? AnnualBonus2Category { get; set; }

        /// <summary>
        /// Страховая сумма - лифты и лифтовое оборудование
        /// </summary>
        [Display(Name = "Годовая страховая премия - лифты и лифтовое оборудование")]
        public decimal? AnnualBonus3Category { get; set; }

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
        [Display(Name = "Годовая страховая премия - по дому в целом")]
        public decimal? SizeAnnualBonus { get; set; }
        
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

        /// <summary>
        /// Годовая премия по дому в целом
        /// </summary>
        [Display(Name = "Годовая премия по дому в целом")]
        public decimal? SizeBonusMkd { get; set; }

        /// <summary>
        /// Премия по дому в целом в месяц
        /// </summary>
        [Display(Name = "Премия по дому в целом в месяц")]
        public decimal? SizeBonusMkdMonthly { get; set; }

        /// <summary>
        /// Взнос
        /// </summary>
        [Display(Name = "Взнос")]
        public decimal? Contribution { get; set; }
    }
}
