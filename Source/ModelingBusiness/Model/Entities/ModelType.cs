using System.ComponentModel;

namespace ModelingBusiness.Model.Entities
{
    public enum ModelType
    {
        [Description("Линейная")]
        Linear,
        [Description("Экспоненциальная")]
        Exponential,
        [Description("Мультипликативная")]
        Multiplicative,
        [Description("По всем")]
        All
    }
}
