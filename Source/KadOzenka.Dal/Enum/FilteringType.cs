using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Dal.Enum
{
    public enum FilteringType
    {
        None,
        String,
        Boolean,
        Number,
        Date,
        Reference
    }
    public enum FilteringTypeString
    {
        //[Display(Name = "Равно")]
        Equal,

        //[Display(Name = "Равно (без учета регистра)")]
        EqualIgnoreCase,

        //[Display(Name = "Не равно")]
        NotEqual,

        //[Display(Name = "Не равно (без учета регистра)")]
        NotEqualIgnoreCase,

        //[Display(Name = "Начинается c")]
        BeginsFrom,

        //[Display(Name = "Начинается c (без учета регистра)")]
        BeginsFromIgnoreCase,

        //[Display(Name = "Не начинается c")]
        NotBeginsFrom,

        //[Display(Name = "Не начинается c (без учета регистра)")]
        NotBeginsFromIgnoreCase,

        //[Display(Name = "Заканчивается на")]
        EndsWith,

        //[Display(Name = "Заканчивается на (без учета регистра)")]
        EndsWithIgnoreCase,

        //[Display(Name = "Не заканчивается на")]
        NotEndsWith,

        //[Display(Name = "Не заканчивается на (без учета регистра)")]
        NotEndsWithIgnoreCase,

        //[Display(Name = "Содержит")]
        Contains,

        //[Display(Name = "Содержит (без учета регистра)")]
        ContainsIgnoreCase,

        //[Display(Name = "Не содержит")]
        NotContains,

        //[Display(Name = "Не содержит (без учета регистра)")]
        NotContainsIgnoreCase,

        //[Display(Name = "Пусто")]
        IsNull,

        //[Display(Name = "Не пусто")]
        IsNotNull
    }
    public enum FilteringTypeNumber
    {
        //[Display(Name = "Равно")]
        Equal,

        //[Display(Name = "Не равно")]
        NotEqual,

        //[Display(Name = "Меньше")]
        Less,

        //[Display(Name = "Меньше или равно")]
        LessOrEqual,

        //[Display(Name = "Больше")]
        Greater,

        //[Display(Name = "Больше или равно")]
        GreaterOrEqual,

        //[Display(Name = "В диапазоне")]
        InRange,

        //[Display(Name = "В диапазоне (включая границы)")]
        InRangeIncludingBoundaries,

        //[Display(Name = "Пусто")]
        IsNull,

        //[Display(Name = "Не пусто")]
        IsNotNull
    }
    public enum FilteringTypeReference
    {
        //[Display(Name = "Равно")]
        Equal,

        //[Display(Name = "Не равно")]
        NotEqual,

        //[Display(Name = "Пусто")]
        IsNull,

        //[Display(Name = "Не пусто")]
        IsNotNull
    }

    public enum FilteringTypeBool
    {
        //[Display(Name = "Равно")]
        Equal,

        //[Display(Name = "Не равно")]
        NotEqual,

        //[Display(Name = "Пусто")]
        IsNull,

        //[Display(Name = "Не пусто")]
        IsNotNull
    }

    public enum FilteringTypeDate
    {
        //[Display(Name = "Раньше")]
        Before,

        //[Display(Name = "Раньше (включительно)")]
        BeforeIncludingBoundary,

        //[Display(Name = "Позже")]
        After,

        //[Display(Name = "Позже (включительно)")]
        AfterIncludingBoundary,

        //[Display(Name = "В диапазоне")]
        InRange,

        //[Display(Name = "В диапазоне (включительно)")]
        InRangeIncludingBoundaries,

        //[Display(Name = "Пусто")]
        IsNull,

        //[Display(Name = "Не пусто")]
        IsNotNull
    }
}