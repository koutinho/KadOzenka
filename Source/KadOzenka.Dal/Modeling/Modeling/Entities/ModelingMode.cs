using System.ComponentModel;

namespace KadOzenka.Dal.Modeling.Modeling.Entities
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
