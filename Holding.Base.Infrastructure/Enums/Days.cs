
using System.ComponentModel;
using Holding.Base.Infrastructure.Attributes;

namespace Holding.Base.Infrastructure.Enums
{
    [EnumType(Name = "Days")]
    public enum Days
    {
        [Description("شنبه")]
        Saturday = 1,
        [Description("یکشنبه")]
        Sunday = 2,
        [Description("دوشنبه")]
        Monday = 3,
        [Description("سه شنبه")]
        Tuesday = 4,
        [Description("چهارشنبه")]
        Wednesday = 5,
        [Description("پنجشنبه")]
        Thursday = 6,
        [Description("جمعه")]
        Friday = 7
    }
}
