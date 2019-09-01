namespace DebugApplication.Report
{
    public enum DataType
    {
        /// <summary>
        /// Текстовое поле.
        /// Данное значение должно быть первым, чтобы при сериализации оно использовалось по умолчанию
        /// </summary>
        TextBox,

        HiddenValue,

        Date,

        Integer,

        Year,

        Decimal,

        FlsList,

        DatePeriod,

        List,

        CheckBox,

        ObjectsList
    }
}
