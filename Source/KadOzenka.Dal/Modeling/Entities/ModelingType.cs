using System.ComponentModel;

namespace KadOzenka.Dal.Modeling.Entities
{
    public enum ModelingType
    {
        [Description("Обучение")]
        Training,
        [Description("Прогнозирование")]
        Prediction,
        [Description("Корреляция")]
        Correlation
    }
}
