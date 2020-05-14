﻿using System.ComponentModel;

namespace KadOzenka.Dal.Modeling.Entities
{
    public enum PredictionType
    {
        [Description("Линейная")]
        Linear,
        [Description("Экспоненциальная")]
        Exponential,
        [Description("Мультипликативная")]
        Multiplicative
    }
}
