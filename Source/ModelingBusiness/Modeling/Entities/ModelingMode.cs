using System.ComponentModel;

namespace ModelingBusiness.Modeling.Entities
{
    public enum ModelingMode
    {
        [Description("Обучение")]
        Training,
        [Description("Прогнозирование")]
        Prediction,
        [Description("Корреляция")]
        Correlation
    }
}
