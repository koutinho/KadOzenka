using System.Collections.Generic;

namespace DebugApplication.Report
{
    public class FilterValue
    {
        /// <summary>
        /// Тип 
        /// </summary>
        public DataType ParamType;

        public bool CheckBoxes;

        /// <summary>
        /// Параметр для полей типа Decimal. Указывает количество знаков после запятой.
        /// </summary>
        public int? DecimalDigits;

        /// <summary>
        /// Specifies whether the control will keep its not rounded value ( when set to true ) and show it when it is focused. 
        /// Otherwise ( when set to false ) the old behavior is preserved, where theactual value of the control is the rounded value. 
        /// By default this property is set to false .
        /// </summary>
        public bool KeepNotRoundedValue;

        public string ParamName;

        public string ParamLabel;

        public bool RefreshFormOnValueChange;

        public List<string> PosibleValues;

        public int? ReferenceID;

        public string DefaultValue;

        public string SrdFunctionTag;

        public List<Parameter> Parameters { get; set; }

        public string Group { get; set; }
    }
}
