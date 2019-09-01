using System.ComponentModel.DataAnnotations;

namespace CIPJS.DAL.Calculation
{
    public class CalculationValuesInDto
    {
        /// <summary>
        /// Ссылка на проект договора
        /// </summary>
        [Display(Name = "Ссылка на проект договора")]
        public long? AgreementId { get; set; }

        /// <summary>
        /// Перерасчитать годовую премию по дому в целом
        /// </summary>
        [Display(Name = "Перерасчитать годовую премию по дому в целом")]
        public bool RecalcSizeBonusMkd { get; set; }

        /// <summary>
        /// Округлять
        /// </summary>
        [Display(Name = "Округлять")]
        public bool? FlagOkrugl { get; set; }

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
        [Display(Name = "Общая площадь по зданию (ЕГРН) , кв.м ")]
        public decimal? OplEgrn { get; set; }

        /// <summary>
        /// Площадь помещений, не входящих в общую зону, кв.м.
        /// </summary>
        [Display(Name = "Площадь помещений, не входящих в общую зону, кв.м.")]
        public decimal? Epl { get; set; }

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
        /// Площадь кровли, кв.м.
        /// </summary>
        [Display(Name = "Площадь кровли, кв.м.")]
        public decimal? Krovpl { get; set; }

        /// <summary>
        /// Площадь холодных помещений, кв.м.
        /// </summary>
        [Display(Name = "Площадь холодных помещений, кв.м.")]
        public decimal? Hpl { get; set; }

        /// <summary>
        /// Строительная стоимость
        /// </summary>
        [Display(Name = "Строительная стоимость")]
        public decimal? StroiPrice { get; set; }

        /// <summary>
        /// Процент износа
        /// </summary>
        [Display(Name = "Процент износа")]
        public decimal? Pizn { get; set; }

        /// <summary>
        /// Коэффициент пересчета действительной стоимости
        /// </summary>
        [Display(Name = "Коэффициент пересчета действительной стоимости")]
        public decimal? CoefActualCost { get; set; }

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
        /// Доля ответственности СК, %
        /// </summary>
        [Display(Name = "Доля ответственности СК, %")]
        public decimal? PartCompensation { get; set; }

        /// <summary>
        /// Доля города Москвы, %
        /// </summary>
        [Display(Name = "Размер доли города, в праве на имущество, %")]
        public decimal? AgreementPartMoscow { get; set; }

        /// <summary>
        /// Базовый тариф - общая зона дома без санитарно-технических работи внутридомового инженерного оборудования
        /// </summary>
        [Display(Name = "Базовый тариф - общая зона дома без санитарно-технических работи внутридомового инженерного оборудования")]
        public decimal? BasicRate1 { get; set; }

        /// <summary>
        /// Базовый тариф - общая зона дома по санитарно техническим работам и внутридомовому инженерному оборудованию
        /// </summary>
        [Display(Name = "Базовый тариф - общая зона дома по санитарно техническим работам и внутридомовому инженерному оборудованию")]
        public decimal? BasicRate2 { get; set; }

        /// <summary>
        /// Базовый тариф - лифты и лифтовое оборудование
        /// </summary>
        [Display(Name = "Базовый тариф - лифты и лифтовое оборудование")]
        public decimal? BasicRate3 { get; set; }

        /// <summary>
        /// Количество квартир
        /// </summary>
        [Display(Name = "Кол-во квартир")]
        public long? KolGp { get; set; }

        #region Поля с возможным вводом вручную
        /// <summary>
        /// Общая площадь по зданию (включая холодные помещения), кв.м.
        /// </summary>
        [Display(Name = "Общая площадь по зданию (включая холодные помещения), кв.м.")]
        public decimal? Oplc { get; set; }
        public bool OplcSet { get; set; }

        /// <summary>
        /// Признак Hpl участвует в расчете общей площади по зданию
        /// </summary>
        public bool OplcIncludeHpl { get; set; }

        /// <summary>
        /// Расчетная площадь для определения стоимости общего имущества в МКД, кв.м.
        /// </summary>
        [Display(Name = "Расчетная площадь для определения страховой стоимости общего имущества в МКД, кв.м.")]
        public decimal? CalculatedArea { get; set; }
        public bool CalculatedAreaSet { get; set; }

        /// <summary>
        /// Признак Hpl учасвтует в расчете расчетной площади
        /// </summary>
        public bool CalculatedAreaIncludeHpl { get; set; }

        /// <summary>
        /// Показатель рациональности объемно-планировочного решени, R
        /// </summary>
        [Display(Name = "Показатель рациональности объемно-планировочного решени, R")]
        public decimal? IndicatorR { get; set; }
        public bool IndicatorRSet { get; set; }

        /// <summary>
        /// Действительная стоимость дома, руб
        /// </summary>
        [Display(Name = "Действительная стоимость дома, руб.")]
        public decimal? ActualCost { get; set; }
        public bool ActualCostSet { get; set; }

        /// <summary>
        /// Действительная стоимость дома (в пересчете на текущие цены), руб.
        /// </summary>
        [Display(Name = "Действительная стоимость дома (в пересчете на текущие цены), руб.")]
        public decimal? ActualCostCurrent { get; set; }
        public bool ActualCostCurrentSet { get; set; }
        #endregion
    }
}
