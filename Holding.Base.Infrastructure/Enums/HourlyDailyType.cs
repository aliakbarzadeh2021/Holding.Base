using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "HourlyDailyType")]
    public enum HourlyDailyType
    {
        [Description("ساعتی")]
        Hourly = 1,
        [Description("روزانه")]
        Daily = 2
    }
}