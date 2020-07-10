using System.ComponentModel;

namespace KadOzenka.Web.Models.BackgroundScheduler.Common
{
    public enum SchedulerType
    {
        [Description("Каждый день")]
        EveryDay,
        [Description("Раз в неделю")]
        OnceAWeek,
        [Description("Раз в месяц")]
        OnceAMonth,
        [Description("Раз в квартал")]
        OnceAQuarter,
        [Description("Раз в год")]
        OnceAYear
    }
}
